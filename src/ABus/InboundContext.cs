using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABus.Contracts;

namespace ABus
{
    public static class InboundContext
    {
        public static InboundMapper Inbound(this MessageContext messageContext)
        {
            return new InboundMapper(messageContext);
        }

        public class InboundMapper
        {
            private readonly MessageContext _messageContext;

            public InboundMapper(MessageContext messageContext)
            {
                _messageContext = messageContext;
            }

            public RawMessage RawMessage
            {
                get { return _messageContext.GetValue<RawMessage>("RawMessage"); }
                set { _messageContext.SetValue("RawMessage", value); }
            }
        }
    }
    // Notes:
    // There is a small perf hit by asking for properties from the collection each time
    // however, attempting to cache it introduces potential sync issues, so until its deeemed an actual
    // perf issues the simplier code is favoured.
}
