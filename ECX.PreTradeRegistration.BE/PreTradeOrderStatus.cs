using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.PreTradeRegistration.BE
{
    public class PreTradeOrderStatus
    {
        public int Id { get; set; }

        public string StatusName { get; set; }

        public bool IsActive { get; set; }
    }
}
