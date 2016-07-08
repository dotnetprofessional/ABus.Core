namespace ABus.Contracts
{
    public static class StandardMetaData
    {
        public static readonly string MessageType = "ABus_MessageType";
        public static readonly string ContentType = "ABus_Content-Type";
        public static readonly string MessageIntent = "ABus_MessageIntent";
        public static readonly string Exception = "ABus_Exception";
        public static readonly string AuthenticatedUser = "ABus_AuthenticatedUser";
        public static readonly string AuthenticationType = "ABus_AuthenticationType";
        public static readonly string SourceEndpoint = "ABus_SourceEndpoint";
        /// <summary>
        /// Tracks all messages that are part of a transaction/conversation
        /// </summary>
        public static readonly string ConversationId = "ABus_ConversationId";
        public static readonly string CorrelationId = "ABus_CorrelationId";
        public static readonly string ReplyTo = "Abus_ReplyTo";
        public static readonly string DestinationEndpoint = "ABus_DestinationEndpoint";
    }
}