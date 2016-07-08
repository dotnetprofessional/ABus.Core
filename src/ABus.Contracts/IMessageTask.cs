using System;
using System.Threading.Tasks;

namespace ABus.Contracts
{
    public interface IMessageTask
    {
        Task InvokeAsync(PipelineContext pipelineContext, MessageContext messageContext, Func<Task> next);
    }
}
