using System;

namespace ABus.Contracts
{
    public class RawMessage
    {
        public RawMessage(string messageId)
        {
            this.MessageId = messageId;
            this.MetaData = new MetaDataCollection();
            this.State = MessageState.Active;
        }

        /// <summary>
        /// Gets/sets other applicative out-of-band information.
        /// </summary>
        public MetaDataCollection MetaData { get; set; }

        /// <summary>
        /// Gets/sets the maximum time limit in which the message  must be received.
        /// </summary>
        public TimeSpan TimeToBeReceived { get; set; }

        /// <summary>
        /// The unique Id for this message
        /// </summary>
        public string MessageId { get; private set; }

        /// <summary>
        /// Gets/sets the Id that is used to track messages within a process flow
        /// </summary>
        public string CorrelationId { get; set; }

        /// <summary>
        /// Gets/sets the body content of the message
        /// </summary>
        public byte[] Body { get; set; }

        public MessageState State { get; set; }

        public MessageIntent MessageIntent
        {
            get
            {
                if (this.MetaData.Contains(StandardMetaData.MessageIntent))
                    return ((MessageIntent)Enum.Parse(typeof(MessageIntent), this.MetaData[StandardMetaData.MessageIntent].Value));

                return MessageIntent.Unknown;
            }
        }
    }
}
