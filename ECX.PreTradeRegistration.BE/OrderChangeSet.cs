using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.PreTradeRegistration.BE
{
    public class OrderChangeSet
    {
        public string OrderId{get;set;}
        public Guid CommodityGradeId{get;set;}
        public decimal Quantity{get;set;}
        public Guid MemberId{get;set;}
        public Guid RepId{get;set;}
        public Guid ClientId{get;set;}
        public Guid WarehouseId{get;set;}
        public int ProductionYear{get;set;}
        public bool IsClientOrder{get;set;}
        public DateTime SubmittedTimestamp { get; set; }
    }
}
