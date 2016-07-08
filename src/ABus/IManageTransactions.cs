using System.Collections.Generic;

namespace ABus
{
    /// <summary>
    /// This interface ensure committed messages are delivered to the destination queue
    /// </summary>
    public interface IManageTransactions
    {
        /// <summary>
        /// Commits outbound messages with the transaction manager
        /// </summary>
        /// <param name="correlationId">correlationId of the inbound message</param>
        /// <param name="messages">collection of outbound messages</param>
        /// <remarks>
        /// A successful call to this method transfers the responsibility to ensure messages are
        /// sent to the transaction manager.
        /// </remarks>
        void CommitMessages(string correlationId, IEnumerable<QueuedMessage> messages);

        /// <summary>
        /// Returns the next message with the correlationId
        /// </summary>
        /// <param name="correlationId">correlationId of the inbound message</param>
        /// <returns></returns>
        QueuedMessage GetMessage(string correlationId);

        /// <summary>
        /// Marks a message as having been successfully delivered to the destination queue
        /// </summary>
        /// <param name="correlationId">correlationId of the inbound message</param>
        /// <param name="messageId">messageId of the outbound message</param>
        void MarkAsComplete(string correlationId, string messageId);

        bool Exists(string correlationId);
    }
}