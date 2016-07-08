using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABus.Contracts;

namespace ABus
{
    public static class PipelineContext
    {
        public static PiplineMapper Pipeline(this MessageContext messageContext)
        {
            return new PiplineMapper(messageContext);
        }

        public class PiplineMapper
        {
            private readonly MessageContext _messageContext;

            public PiplineMapper(MessageContext messageContext)
            {
                _messageContext = messageContext;
            }
        }
    }
}
