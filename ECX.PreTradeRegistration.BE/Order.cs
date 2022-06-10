using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.PreTradeRegistration.BE
{
    public class Order
    {
        public string WarehouseName { get; set; }
        public string OrderId { get; set; }
        public decimal TradeQuantity { get; set; }

        public decimal AcceptedTradeQuantity { get; set; }
        public Guid MemberId { get; set; }
        public string MemberIdNo { get; set; }

        public string MemberName { get; set; }
        public string OwnerIdNo { get; set; }
        public string OwnerName { get; set; }
        public Guid RepId { get; set; }
        public string RepIdNo { get; set; }
        public Guid ClientId { get; set; }
        public Guid CommodityGradeId { get; set; }
        public string SessionName { get; set; }
        public string Symbol { get; set; }

        public Guid WarehouseId { get; set; }
        public int ProductionYear { get; set; }
        public string OrderStatus { get; set; }
        public DateTime TradeDate { get; set; }
        public bool IsClientOrder { get; set; }
    }
    public class AcceptedTrade
    {
        public Guid CommodityGradeId { get; set; }
        public Guid WarehouseId { get; set; }
        public int ProductionYear { get; set; }
        public string OwnerId { get; set; }
        public decimal AcceptedTradeQuantity { get; set; }
    }
}
