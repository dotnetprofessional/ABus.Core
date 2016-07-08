using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABus.Contracts;

namespace ABus
{
    /// <summary>
    /// Responsible for ensuring messages are sent as a transaction
    /// </summary>
    /// <remarks>
    /// The UnitOfWork implementation follows a two step process
    /// 1. Record all messages to be sent in memory
    /// 2. Commit all messages to be sent with IManageTransactions implementation (provides an atomic transaction)
    /// </remarks>
    public class UnitOfWork : IUnitOfWork
    {
        private IManageTransactions TransactionManager { get; set; }
        private List<QueuedMessage> QueuedMessages { get; } = new List<QueuedMessage>();

        public UnitOfWork(IManageTransactions transactionManager)
        {
            this.TransactionManager = transactionManager;
        }

        public void Commit()
        {
            this.TransactionManager.CommitMessages(this.CorrelationId, this.QueuedMessages);
        }

        public string CorrelationId { get; set; }

        public void QueueMessage(IMessageTransport transport, QueueEndpoint endpoint, RawMessage message)
        {
            this.QueuedMessages.Add(new QueuedMessage(transport, endpoint, message));
        }

    }
}
