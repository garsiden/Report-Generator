using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RSMTenon.Data
{
    public partial class Client
    {

        public static Client GetClientByGUID(Guid clientGuid)
        {
            var ctx = new RepGenDataContext();

            return ctx.Clients.Single(c => c.GUID == clientGuid);
        }
    }
}
