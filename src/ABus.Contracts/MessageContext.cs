using System.Collections.Generic;

namespace ABus.Contracts
{
    /// <summary>
    /// Holds all data for the incomming message
    /// </summary>
    public class MessageContext
    {
        public MessageContext()
        {
            this.Reset();
        }

        /// <summary>
        /// Data stored here should live for the live of a single inbound message
        /// </summary>
        public Dictionary<string, object> Data { get; set; }

        public T GetValue<T>(string key)
        {
            if (this.Data.ContainsKey(key))
                return (T)this.Data[key];

            return default(T);
        }

        public void SetValue<T>(string key, T value)
        {
            if (this.Data.ContainsKey(key))
                this.Data[key] = value;
            else
                this.Data.Add(key, value);
        }

        public void Reset()
        {
            this.Data = new Dictionary<string, object>();
        }
    }
}
