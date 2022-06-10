using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data;
using ECX.PreTradeRegistration.BE;

namespace ECX.PreTradeRegistration.DAL
{
    public class PreTradeOrderDAL
    {

        public bool SavePreTradeOrder(Guid Id, DateTime TradeDate, Guid MmemberId, string MemberIDNO, string MemberName, Guid OwnerId, string OwnerIDNO
   , string OwnerName, Guid RepId, string RepIDNO, string RepName, string Symbol, Guid CommodityGradeId, Guid Warehouse, string WarehouseName, int ProductionYear
   , decimal Quantity, int Status, Guid CreatedBy)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@Id");
                paramName.Add("@TradeDate");
                paramName.Add("@MmemberId");
                paramName.Add("@MemberIDNO");
                paramName.Add("@MemberName");
                paramName.Add("@OwnerId");
                paramName.Add("@OwnerIDNO");
                paramName.Add("@OwnerName");
                paramName.Add("@RepId");
                paramName.Add("@RepIDNO");
                paramName.Add("@RepName");
                paramName.Add("@Symbol");
                paramName.Add("@CommodityGradeId");
                paramName.Add("@Warehouse");
                paramName.Add("@WarehouseName");
                paramName.Add("@ProductionYear");
                paramName.Add("@Quantity");
                paramName.Add("@Status");
                paramName.Add("@CreatedBy");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(Id);
                paramVal.Add(TradeDate);
                paramVal.Add(MmemberId);
                paramVal.Add(MemberIDNO);
                paramVal.Add(MemberName);
                paramVal.Add(OwnerId);
                paramVal.Add(OwnerIDNO);
                paramVal.Add(OwnerName);
                paramVal.Add(RepId);
                paramVal.Add(RepIDNO);
                paramVal.Add(RepName);
                paramVal.Add(Symbol);
                paramVal.Add(CommodityGradeId);
                paramVal.Add(Warehouse);
                paramVal.Add(WarehouseName);
                paramVal.Add(ProductionYear);
                paramVal.Add(Quantity);
                paramVal.Add(Status);
                paramVal.Add(CreatedBy);

                string errormesg = "";
                return DataAccessProvider.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spSavePreTradeOrder", paramName, paramVal, ref errormesg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdatePreTradeOrder(Guid Id, decimal NewQuantity, int OrderStatus, Guid UpdatedBy)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@Id");
                paramName.Add("@NewQuantity");
                paramName.Add("@OrderStatus");
                paramName.Add("@UpdatedBy");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(Id);
                paramVal.Add(NewQuantity);
                paramVal.Add(OrderStatus);
                paramVal.Add(UpdatedBy);

                string errormesg = "";
                return DataAccessProvider.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spUpdatePreTradeOrder", paramName, paramVal, ref errormesg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPreTradeOrderByTradeDateNMemberId(DateTime TradeDate, Guid MemberId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@TradeDate");
                paramName.Add("@MemberId");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(TradeDate);
                paramVal.Add(MemberId);

                string errormesg = "";
                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetPreTradeOrderByTradeDateNMemberId", paramName, paramVal, ref errormesg);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetPositionSummary(Guid memberId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@MemberId");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(memberId);

                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["CDConnectionString"].ConnectionString, "dbo", "spGetPreTradePositionSummaryByMemberId", paramName, paramVal, ref errormesg);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetAvailableBalance(Guid whrId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@WHRId");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(whrId);

                string errormesg = "";

                DataSet ds = DataAccessProvider.ExecuteDataSet(ConfigurationManager.ConnectionStrings["CDConnectionString"].ConnectionString, "dbo", "spGetPreTradeGetAvailableQuantity", paramName, paramVal, ref errormesg);

                if (ds.Tables.Count > 0)
                {
                    return ds.Tables[1];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DateTime GetNextTradeDate()
        {
            try
            {
                string errormesg = "";
                
                return Convert.ToDateTime(DataAccessProvider.ExecuteScalar(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "fnGetNextTradeDate", ref errormesg));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public DataTable GetPreTradeOrderByMemberIdAndDatePending(Guid memberId, DateTime tradeDate)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@MemberId");
                paramName.Add("@TradeDate");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(memberId);
                paramVal.Add(tradeDate);

                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetPreTradeOrderByMemberIdAndDatePending", paramName, paramVal, ref errormesg);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetPreTradeOrderByMemberIdAndTradeDate(Guid memberId, DateTime tradeDate)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@MemberId");
                paramName.Add("@TradeDate");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(memberId);
                paramVal.Add(tradeDate);

                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetPreTradeOrderByMemberIdAndDate", paramName, paramVal, ref errormesg);
                return dt;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CancelPreTradeOrder(Guid orderId, int status, Guid updatedBy)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@Id");
                paramName.Add("@OrderStatus");
                paramName.Add("@UpdatedBy");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(orderId);
                paramVal.Add(status);
                paramVal.Add(updatedBy);

                string errormesg = "";
                return DataAccessProvider.ExecuteNonQuery(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spCancelPreTradeOrder", paramName, paramVal, ref errormesg);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public decimal GetAvailableQuantityViaContractNOwner(Guid id,string symbol , string warehouse,int pY,DateTime nextTradeDate,string ownerIDNO)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@Id");
                paramName.Add("@Symbol");
                paramName.Add("@Warehouse");
                paramName.Add("@PY");
                paramName.Add("@NextTradeDate");
                paramName.Add("@OwnerIDNO");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(id);
                paramVal.Add(symbol);
                paramVal.Add(warehouse);
                paramVal.Add(pY);
                paramVal.Add(nextTradeDate);
                paramVal.Add(ownerIDNO);

                string errormesg = "";

                return Convert.ToDecimal(DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["CDConnectionString"].ConnectionString, "dbo", "spGetAvailableQuantityViaContractNOwner", paramName, paramVal, ref errormesg).Rows[0]["AvilableQuantity"]);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetBuyerPreTradeReport(DateTime TradeDate)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@TradeDate");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(TradeDate);

                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetBuyerPreTradeReport", paramName, paramVal, ref errormesg);
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public DataTable GetSellerPreTradeReport(DateTime from, DateTime to, Guid memberId )
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@From");
                paramName.Add("@To");
                paramName.Add("@MmemberId");
                

                ArrayList paramVal = new ArrayList();

                paramVal.Add(from);
                paramVal.Add(to);
                paramVal.Add(memberId);
                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetSellerPreTradeReport", paramName, paramVal, ref errormesg);
                return dt;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Order> GetOrderFromTrade(DateTime TradeDate)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@TradeDate");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(TradeDate);
             
                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetPreTradeOrderFromOrder", paramName, paramVal, ref errormesg);
                return DataAccessProvider.ConvertDataTable<Order>(dt);
                               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PreTradeOrderForReport> GetOrderFromPreTrade(DateTime TradeDate)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@TradeDate");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(TradeDate);

                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetPreTradeOrderFromPreTrade", paramName, paramVal, ref errormesg);
                return DataAccessProvider.ConvertDataTable<PreTradeOrderForReport>(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<AcceptedTrade> GetAcceptedTrade(DateTime TradeDate)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@TradeDate");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(TradeDate);

                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetAcceptedTrade", paramName, paramVal, ref errormesg);
                return DataAccessProvider.ConvertDataTable<AcceptedTrade>(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        } 
        public List<OrderChangeSet> GetOrderChangeSet(DateTime TradeDate)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@TradeDate");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(TradeDate);

                string errormesg = "";

                DataTable dt = DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetPreTradeOrderChangeSet", paramName, paramVal, ref errormesg);
                return DataAccessProvider.ConvertDataTable<OrderChangeSet>(dt);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable GetMemberGuidById(string memberId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@MemberId");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(memberId);

                string errormesg = "";

                return DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["MembershipConnectionString"].ConnectionString, "dbo", "spGetMemberGuidById", paramName, paramVal, ref errormesg);
                

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSellerPreTradeReport(DateTime dateFrom, DateTime dateTo, string memberId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@MemberId");
                paramName.Add("@DateFrom");
                paramName.Add("@DateTo");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(memberId);
                paramVal.Add(dateFrom);
                paramVal.Add(dateTo);

                string errormesg = "";

                return DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetSellerPreTradeReportForBackOffice", paramName, paramVal, ref errormesg);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSellerPreTradeReportForHistory(DateTime dateFrom, DateTime dateTo, string memberId)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@MemberId");
                paramName.Add("@DateFrom");
                paramName.Add("@DateTo");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(memberId);
                paramVal.Add(dateFrom);
                paramVal.Add(dateTo);

                string errormesg = "";

                return DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetSellerPreTradeReportForHistory", paramName, paramVal, ref errormesg);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetSellerPreTradeReportDetailById(Guid Id)
        {
            try
            {
                ArrayList paramName = new ArrayList();

                paramName.Add("@Id");

                ArrayList paramVal = new ArrayList();

                paramVal.Add(Id);

                string errormesg = "";

                return DataAccessProvider.ExecuteDataTable(ConfigurationManager.ConnectionStrings["TradeConnectionString"].ConnectionString, "dbo", "spGetSellerPreTradeReportDetailById", paramName, paramVal, ref errormesg);


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
