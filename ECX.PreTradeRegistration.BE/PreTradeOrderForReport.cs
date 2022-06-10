using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECX.PreTradeRegistration.BE
{
    public class PreTradeOrderForReport
    {
        public Guid MmemberId{get;set;}
        public string MemberIDNO{get;set;}
        public string MemberName{get;set;}
        public Guid OwnerId{get;set;}
        public string OwnerIDNO{get;set;}
        public string OwnerName{get;set;}
        public Guid RepId{get;set;}
        public string RepIDNO{get;set;}
        public string RepName{get;set;}
        public decimal Quantity{get;set;}
        public DateTime TradeDate{get;set;}
        public Guid CommodityGradeId{get;set;}
        public string SessionName { get; set; }
        public Guid Warehouse{get;set;}
        public int ProductionYear { get; set; }
        public string Symbol { get; set; }
        public string WarehouseName { get; set; }
    }
}
