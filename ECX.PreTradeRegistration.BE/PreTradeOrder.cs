using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.PreTradeRegistration.BE
{
    public class PreTradeOrder
    {
        public Guid Id { get; set; }

        public DateTime TradeDate { get; set; }

        public Guid MmemberId { get; set; }

        public string MemberIDNO { get; set; }

        public string MemberName { get; set; }

        public Guid OwnerId { get; set; }

        public string OwnerIDNO { get; set; }

        public string OwnerName { get; set; }

        public Guid RepId { get; set; }

        public string RepIDNO { get; set; }

        public string RepName { get; set; }

        public string Symbol { get; set; }

        public Guid CommodityGradeId { get; set; }

        public Guid Warehouse { get; set; }

        public string WarehouseName { get; set; }

        public int ProductionYear { get; set; }

        public decimal Quantity { get; set; }

        public int Status { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
