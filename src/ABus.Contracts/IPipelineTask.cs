using System;
using System.Threading.Tasks;

namespace ABus.Contracts
{
    public interface IPipelineTask
    {
        Task InvokeAsync(PipelineContext pipelineContext, Func<Task> next);
    }
}