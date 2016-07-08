using System.Collections.Generic;

namespace ABus.Contracts
{
    /// <summary>
    /// Holds all data for the incomming message
    /// </summary>
    public class PipelineContext
    {
        /// <summary>
        /// Data stored here should live for the life of the system
        /// </summary>
        public static Dictionary<string, object> Data { get; set; } = new Dictionary<string, object>();


        public T GetValue<T>(string key)
        {
            if (Data.ContainsKey(key))
                return (T)Data[key];

            return default(T);
        }

        public void SetValue<T>(string key, T value)
        {
            if (Data.ContainsKey(key))
                Data[key] = value;
            else
                Data.Add(key, value);
        }
    }
}