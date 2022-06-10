using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.PreTradeRegistration.BE
{
    public class PreTradeOrderHistory
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string Action { get; set; }

        public decimal OldValue { get; set; }

        public decimal NewValue { get; set; }

        public int OrderStatus { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
