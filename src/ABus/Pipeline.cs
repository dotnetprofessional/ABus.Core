using System;
using System.Linq;
using System.Threading.Tasks;
using ABus.Contracts;
using ABus.DataStructures;

namespace ABus
{
    public class ConfigurationPipeline : Pipeline<IPipelineTask>
    {
        protected override async Task InvokeAsync(Node<IPipelineTask> task)
        {
            await task.Data.InvokeAsync(null, async () =>
            {
                if (task.Next != null)
                    await this.InvokeAsync(task.Next);
            });
        }
    }

    public class MessagePipeline : Pipeline<IMessageTask>
    {
        protected override async Task InvokeAsync(Node<IMessageTask> task)
        {
            await task.Data.InvokeAsync(null, null, async () =>
            {
                if (task.Next != null)
                    await this.InvokeAsync(task.Next);
            });
        }
    }

    public abstract class Pipeline<T>
    {
        internal GroupedNodeList<T> TaskGroups { get; set; }

        public void AddLast(string group, T data)
        {
            this.TaskGroups.AddLast(group, data);
        }

        public void AddFirst(string group, T data)
        {
            this.TaskGroups.AddFirst(group, data);
        }

        /// <summary>
        /// Executes the pipeline
        /// </summary>
        /// <returns></returns>
        public async Task ExecuteAsync()
        {
            await this.InvokeAsync(this.TaskGroups.JoinGroups());
        }
        protected abstract Task InvokeAsync(Node<T> task);
    }
}
