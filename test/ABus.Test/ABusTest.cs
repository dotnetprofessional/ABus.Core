using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using ABus;
using ABus.Contracts;

namespace ABus.Test
{
    public class ABusTest
    {
        [Fact]
        public void Inbound_Test()
        {
            var context = new MessageContext();

            var b = context.Inbound().RawMessage;
        }
    }
}
