using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.PreTradeRegistration.BE
{
    public class PreTradeReport
    {
        public Guid MemberId { get; set; }
        public string MemberIdNo { get; set; }
        public string MemberName { get; set; }
        public Guid OwnerId { get; set; }
        public string OwnerIdNo { get; set; }
        public string OwnerName { get; set; }
        public string RepId{get;set;}
        public string WarehouseName{get;set;}
        public string Symbol{get;set;}
        public int ProductionYear{get;set;}
        public decimal SellOrderQuantity{get;set;}
        public decimal TradeQuantity{get;set;}
        public decimal AcceptedTradeQuantity { get; set; }
        public string OrderStatus{get;set;}
        public string Remark{get;set;}
        public decimal CancelQuantity { get; set; }
        public Guid CommodityGrade { get; set; }
        public Guid WarehouseId { get; set; }
        public string SessionName { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal Variance { get; set; }
    }
}
