using ABus.Contracts;

namespace ABus
{
    public class QueuedMessage
    {
        public QueuedMessage(IMessageTransport transport, QueueEndpoint endpoint, RawMessage rawMessage)
        {
            this.Transport = transport;
            this.Endpoint = endpoint;
            this.RawMessage = rawMessage;
        }

        public IMessageTransport Transport { get; }

        public QueueEndpoint Endpoint { get; set; }

        public RawMessage RawMessage { get; }
    }
}