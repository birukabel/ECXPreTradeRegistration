using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECX.PreTradeRegistration.BE;
using ECX.PreTradeRegistration.DAL;
using System.Data;

namespace ECX.PreTradeRegistration.BLL
{
    public class PreTradeOrderBLL
    {
        public bool SavePreTradeOrder(Guid Id, DateTime TradeDate, Guid MmemberId, string MemberIDNO, string MemberName, Guid OwnerId, string OwnerIDNO
  , string OwnerName, Guid RepId, string RepIDNO, string RepName, string Symbol, Guid CommodityGradeId, Guid Warehouse, string WarehouseName, int ProductionYear
  , decimal Quantity, int Status, Guid CreatedBy)        
        { 
            return new PreTradeOrderDAL().SavePreTradeOrder(Id,TradeDate,MmemberId,MemberIDNO,MemberName,OwnerId,OwnerIDNO,OwnerName,RepId,RepIDNO,RepName,Symbol,CommodityGradeId,Warehouse,WarehouseName,ProductionYear,Quantity,Status,CreatedBy);
        }

        public bool UpdatePreTradeOrder(Guid Id, decimal NewQuantity, int OrderStatus, Guid UpdatedBy)
        { 
            return  new PreTradeOrderDAL().UpdatePreTradeOrder(Id,NewQuantity,OrderStatus,UpdatedBy);
        }

        public DataTable GetPreTradeOrderByTradeDateNMemberId(DateTime TradeDate, Guid MemberId) 
        {
        return new PreTradeOrderDAL().GetPreTradeOrderByTradeDateNMemberId(TradeDate,MemberId);
        }
        public DateTime GetNextTradeDate()
        {
            return new PreTradeOrderDAL().GetNextTradeDate();
        }
        public DataTable GetPreTradeOrderByMemberIdAndTradeDate(Guid memberId, DateTime tradeDate)
        {
            return new PreTradeOrderDAL().GetPreTradeOrderByMemberIdAndTradeDate(memberId, tradeDate);
        }

        public DataTable GetPreTradeOrderByMemberIdAndDatePending(Guid memberId, DateTime tradeDate)
        {
            return new PreTradeOrderDAL().GetPreTradeOrderByMemberIdAndDatePending(memberId, tradeDate);
        }

        public bool CancelPreTradeOrder(Guid orderId, int status, Guid updatedBy)
        {
            return new PreTradeOrderDAL().CancelPreTradeOrder(orderId, status, updatedBy);
        }

        public decimal GetAvailableQuantityViaContractNOwner(Guid id, string symbol, string warehouse, int pY, DateTime nextTradeDate, string ownerIDNO)
        {
            return new PreTradeOrderDAL().GetAvailableQuantityViaContractNOwner(id,symbol, warehouse, pY, nextTradeDate, ownerIDNO);
        }

        public DataTable GetBuyerPreTradeReport(DateTime TradeDate)
        {
            return new PreTradeOrderDAL().GetBuyerPreTradeReport(TradeDate);
        }

        public DataTable GetSellerPreTradeReport(DateTime from, DateTime to, Guid memberId)
        {
            return new PreTradeOrderDAL().GetSellerPreTradeReport(from,to, memberId);
        }

        public List<Order> GetOrderFromTrade(DateTime TradeDate)
        {
            return new PreTradeOrderDAL().GetOrderFromTrade(TradeDate);
        }
        public List<PreTradeOrderForReport> GetOrderFromPreTrade(DateTime TradeDate)
        {
            return new PreTradeOrderDAL().GetOrderFromPreTrade(TradeDate);
        }
        public List<AcceptedTrade> GetAcceptedTrade(DateTime TradeDate)
        {
            return new PreTradeOrderDAL().GetAcceptedTrade(TradeDate);
        }
        public List<OrderChangeSet> GetOrderChangeSet(DateTime TradeDate)
        {
            return new PreTradeOrderDAL().GetOrderChangeSet(TradeDate);
        }


        public DataTable GetMemberGuidById(string memberId)
        {
            return new PreTradeOrderDAL().GetMemberGuidById(memberId);
        }

        public DataTable GetSellerPreTradeReport(DateTime dateFrom, DateTime dateTo, string memberId)
        {
            return new PreTradeOrderDAL().GetSellerPreTradeReport(dateFrom, dateTo, memberId);
        }
        public DataTable GetSellerPreTradeReportForHistory(DateTime dateFrom, DateTime dateTo, string memberId)
        {
            return new PreTradeOrderDAL().GetSellerPreTradeReportForHistory(dateFrom, dateTo, memberId);
        }
        public DataTable GetSellerPreTradeReportDetailById(Guid Id)
        {
            return new PreTradeOrderDAL().GetSellerPreTradeReportDetailById(Id);
        }
    }
}
