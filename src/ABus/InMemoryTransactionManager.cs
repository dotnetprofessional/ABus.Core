using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using ABus.Contracts;

namespace ABus
{
    public class InMemoryTransactionManager : TransactionManager
    {
        protected override void StageMessages(string correlationId, IEnumerable<QueuedMessage> messages)
        {
            if (Repository.ContainsKey(correlationId))
                throw new ArgumentException("Duplicate messageId: " + correlationId);

            try
            {
                var queuedMessages = new ConcurrentDictionary<string, QueuedMessage>();
                foreach (var m in messages)
                    if (!queuedMessages.TryAdd(m.RawMessage.MessageId, m))
                        throw new ArgumentException("Unable to add message to queuedMessages.");

            }
            catch (Exception)
            {
                // PUtting this here to identify a rare bug that reprots a duplicate key
                throw;
            }
        }
    }
    public abstract class TransactionManager : IManageTransactions
    {
        protected static ConcurrentDictionary<string, ConcurrentDictionary<string, QueuedMessage>> Repository { get; set; } = new ConcurrentDictionary<string, ConcurrentDictionary<string, QueuedMessage>>();

        protected abstract void StageMessages(string correlationId, IEnumerable<QueuedMessage> messages);

        public void CommitMessages(string correlationId, IEnumerable<QueuedMessage> messages)
        {
            // This ensures all messages are durably stored for processing - this is where any atomic operations would happen
            this.StageMessages(correlationId, messages);

            // Send all messages that have been staged for dispatching
            this.DispatchMessages(correlationId);
        }

        public QueuedMessage GetMessage(string correlationId)
        {
            if (correlationId != null)
                if (Repository.ContainsKey(correlationId))
                {
                    return Repository[correlationId].Values.FirstOrDefault();
                }

            return null;
        }

        public void MarkAsComplete(string correlationId, string messageId)
        {
            // Locate messages for the inbound message id
            QueuedMessage msg;
            var success = Repository[correlationId].TryRemove(messageId,out msg);
            if(!success)
                throw new ArgumentException($"Unable to locate message id {messageId} with correlationId {correlationId}");
        }

        public bool Exists(string correlationId)
        {
            return Repository.ContainsKey(correlationId);
        }

        void DispatchMessages(string correlationId)
        {
            var msg = this.GetMessage(correlationId);
            while(msg != null)
                this.DispatchMessage(msg);
        }

        void DispatchMessage(QueuedMessage queuedMessage)
        {
            var msg = queuedMessage.RawMessage;
            var transport = queuedMessage.Transport;

            var messageIntent = msg.MetaData[StandardMetaData.MessageIntent].Value;

            if (messageIntent == MessageIntent.Send.ToString())
                transport.SendAsync(queuedMessage.Endpoint, msg);
            else if (messageIntent == MessageIntent.Publish.ToString())
                transport.PublishAsync(queuedMessage.Endpoint, msg);
            //else if (messageIntent == MessageIntent.Reply.ToString())
            //{
            //    var endpoint = queuedMessage.Endpoint;
            //    // If this is a reply then we need to override the Endpoint for this message type
            //    // and use the ReplyTo endpoint instead. Currently only support request/response on the same transport definition.
            //    var messageEndpoint = new QueueEndpoint { Host = endpoint.Host };
            //    messageEndpoint.SetQueueName(msg.MetaData[StandardMetaData.ReplyTo].Value);

            //    transport.SendAsync(messageEndpoint, msg).ConfigureAwait(false);
            //}
        }
    }
}
