using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.PreTradeRegistration.BE
{
   public class PreTradeLoginDetial
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public DateTime LoggedInDateTime { get; set; }

        public DateTime LoggedOutDateTime { get; set; }

        public string IPAddress { get; set; }
    }
}
