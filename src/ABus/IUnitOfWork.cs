using ABus.Contracts;

namespace ABus
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// The Id to associate with all outbound messages so they can be tracted.
        /// </summary>
        string CorrelationId { get; set; }

        void QueueMessage(IMessageTransport transport, QueueEndpoint endpoint, RawMessage message);

        void Commit();
    }
}