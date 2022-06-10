using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECX.PreTradeRegistration.BE;
using ECX.PreTradeRegistration.BLL;
using System.Data;
using System.Text;

namespace ECX.PreTradeRegistration.BackOffice
{
    public partial class Home : System.Web.UI.Page
    {
        #region memberVariables

        static DataTable dTable;

        #endregion

        #region memberMethods

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["LoggedUser"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
         
            if (!IsPostBack)
            {
                ECXSecurityAccess.ECXSecurityAccess Sec = new ECXSecurityAccess.ECXSecurityAccess();
                UserInfo user = (UserInfo)this.Session["LoggedUser"];
                if (user == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                float[] rights = Sec.HasRights(user.UniqueIdentifier, new string[] { 
                    "ViewDifferenceReport" }, "");
                if (rights[0] == 1 || rights[0] == 3)
                    GetAllActiveCommodities();
                else
                {
                    Response.Redirect("~/ErrorPage.aspx");
                }
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            GetVarianceReport();
        }

        void GetAllActiveCommodities()
        {
            ddlCommodity.Items.Clear();
            ddlCommodity.DataSource = new LookUpBLL().GetAllActiveCommodites();
            ddlCommodity.DataTextField = "Description";
            ddlCommodity.DataValueField = "Guid";
            ddlCommodity.DataBind();
            ddlCommodity.Items.Insert(0, "Select Commodity");
        }

        void GetVarianceReport()
        {
            if (!string.IsNullOrWhiteSpace(txtTradeDate.Text))
            {
                List<Order> dtOrder = new PreTradeOrderBLL().GetOrderFromTrade(Convert.ToDateTime(txtTradeDate.Text));
                List<PreTradeOrderForReport> dtPretrade = new PreTradeOrderBLL().GetOrderFromPreTrade(Convert.ToDateTime(txtTradeDate.Text));
                List<OrderChangeSet> dtChangeSet = new PreTradeOrderBLL().GetOrderChangeSet(Convert.ToDateTime(txtTradeDate.Text));
                List<AcceptedTrade> dtAcceptedTrade = new PreTradeOrderBLL().GetAcceptedTrade(Convert.ToDateTime(txtTradeDate.Text));

                Dictionary<Guid, List<PreTradeReport>> report = new Dictionary<Guid, List<PreTradeReport>>();
                Dictionary<Guid, List<PreTradeReport>> reports = new Dictionary<Guid, List<PreTradeReport>>();

                ProcessFromOrder(dtOrder, dtPretrade, report,dtAcceptedTrade);
                ProcessFromPreTrade(dtPretrade, report);
                CheckQuantity(report);

                if (!string.IsNullOrWhiteSpace(txtMemberId.Text) && ddlCommodity.SelectedIndex > 0)
                {
                    string memberId = txtMemberId.Text.Replace(" ", "").ToLower();

                    List<CommodityGrade> commodityGrades = new LookUpBLL().GetAllActiveCommodityGradesByCommodity(new Guid(ddlCommodity.SelectedValue));
                    if (commodityGrades.Count > 0)
                    {
                        foreach (var dic in report)
                        {
                            if (dic.Value.Exists(x => x.MemberIdNo.ToLower() == memberId && commodityGrades.Exists(y => y.Id == x.CommodityGrade)))
                            {
                                reports.Add(dic.Key, dic.Value);
                            }
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(txtMemberId.Text) && ddlCommodity.SelectedIndex == 0)
                {
                    string memberId = txtMemberId.Text.Replace(" ", "").ToLower();

                    foreach (var dic in report)
                    {
                        if (dic.Value.Exists(x => x.MemberIdNo.ToLower() == memberId))
                        {
                            reports.Add(dic.Key, dic.Value);
                        }
                    }
                }
                else if (string.IsNullOrWhiteSpace(txtMemberId.Text) && ddlCommodity.SelectedIndex > 0)
                {
                    List<CommodityGrade> commodityGrades = new LookUpBLL().GetAllActiveCommodityGradesByCommodity(new Guid(ddlCommodity.SelectedValue));
                    if (commodityGrades.Count > 0)
                    {
                        foreach (var dic in report)
                        {
                            if (dic.Value.Exists(x => commodityGrades.Exists(y => y.Id == x.CommodityGrade)))
                            {
                                reports.Add(dic.Key, dic.Value);
                            }
                        }
                    }
                }
                else
                {
                    reports = report;
                }
                DataTable dtReport = new DataTable();
                dtReport.Columns.Add("MemberId");
                dtReport.Columns.Add("MemberName");
                dtReport.Columns.Add("OwnerId");
                dtReport.Columns.Add("OwnerName");
                dtReport.Columns.Add("RepId");
                dtReport.Columns.Add("SessionName");
                dtReport.Columns.Add("TradeDate");
                dtReport.Columns.Add("Symbol");
                dtReport.Columns.Add("Warehouse");
                dtReport.Columns.Add("ProductionYear");
                dtReport.Columns.Add("SellOrderQuantity");
                dtReport.Columns.Add("TradeQuantity");
                dtReport.Columns.Add("AcceptedTradeQuantity");
                dtReport.Columns.Add("CanceledQuantity");
                dtReport.Columns.Add("Variance");
                dtReport.Columns.Add("Remark");
                foreach (var dic in reports)
                {
                    foreach (var r in dic.Value)
                    {
                        DataRow row = dtReport.NewRow();
                        row["MemberId"] = r.MemberIdNo;
                        row["MemberName"] = r.MemberName;
                        row["OwnerId"] = r.OwnerIdNo;
                        row["OwnerName"] = r.OwnerName;
                        row["RepId"] = r.RepId;
                        row["SessionName"] = r.SessionName;

                        row["TradeDate"] = r.TradeDate;
                        row["Symbol"] = r.Symbol;
                        row["Warehouse"] = r.WarehouseName;
                        row["ProductionYear"] = r.ProductionYear;
                        row["SellOrderQuantity"] = r.SellOrderQuantity;
                        row["TradeQuantity"] = r.TradeQuantity;
                        row["AcceptedTradeQuantity"] = r.AcceptedTradeQuantity;
                        row["CanceledQuantity"] = r.CancelQuantity;
                        row["Variance"] = Math.Round(r.Variance,2);
                        row["Remark"] = r.Remark;
                        dtReport.Rows.Add(row);
                    }
                }
                dTable = new DataTable();
                dTable = dtReport;
                gvReport.DataSource = dtReport;
                gvReport.DataBind();
            }
        }

        private void CheckQuantity(Dictionary<Guid, List<PreTradeReport>> report)
        {
            if (report != null)
            {
                decimal toleranceLimt = 0.08m;
                foreach (var dic in report)
                {
                    foreach (var order in dic.Value)
                    {
                        decimal TQ = 0m;
                        decimal SQ = 0m;
                        decimal varianceLimit;
                        decimal upperLimit = 108m;
                        decimal lowerLimit = 92m;
                        TQ = Math.Round(order.TradeQuantity, 2, MidpointRounding.AwayFromZero);
                        SQ = Math.Round(order.SellOrderQuantity, 2,MidpointRounding.AwayFromZero);
                        if (SQ > 0)
                        {
                            if (TQ > SQ)
                            {
                                varianceLimit = Math.Round((TQ / SQ),2,MidpointRounding.AwayFromZero)*100;
                                
                                //if( Math.Round(Convert.ToDecimal(order.TradeQuantity/order.SellOrderQuantity),2)> Convert.ToDecimal(0.04))
                                if (varianceLimit > upperLimit)
                                    order.Remark = "Exceed tolerance Limit";//"Trade Order Quantity exceds Sell Order Registered Quantity";
                            }
                            else if (TQ == 0)
                            {
                                if (order.AcceptedTradeQuantity > 0)
                                {
                                    order.Remark = "Registered but not executed"; // "Trade Order is not Registered for this Contract";
                                }
                                else
                                {
                                    order.Remark = "Registered but not ordered";
                                }
                            }
                            else if (TQ < SQ)
                            {
                                if (order.CancelQuantity > 0)
                                    order.Remark = "User Has Canceled Trade Order";
                                else
                                {
                                    varianceLimit = Math.Round((TQ/SQ), 2,MidpointRounding.AwayFromZero)*100;
                                    if (Math.Round(varianceLimit,2) == 0.6489m)
                                    { 
                                    
                                    }
                                    //if (Math.Round(Convert.ToDecimal(order.TradeQuantity / order.SellOrderQuantity), 2) < Convert.ToDecimal(0.04))
                                    if(varianceLimit<lowerLimit)
                                        order.Remark = "Below tolerance limit";  //"Trade Order Quantity is less than Sell Order Registered Quantity";
                                }
                            }
                        }
                        else
                        {
                            if (order.AcceptedTradeQuantity > 0)
                            {
                                order.Remark = "Executed but not registered"; // "Sell Order Not Registered for This Contract";
                            }
                            else
                            {
                                order.Remark = "Ordered but not registered"; 
                            }
                        }
                            


                    }
                }
            }
        }

        private static void ProcessFromPreTrade(List<PreTradeOrderForReport> dtPretrade, Dictionary<Guid, List<PreTradeReport>> report)
        {
            if (dtPretrade != null)
            {
              
                foreach (PreTradeOrderForReport or in dtPretrade)
                {

                    PreTradeReport rep = new PreTradeReport();
                    if (report.ContainsKey(or.OwnerId))
                    {
                        if (report[or.OwnerId].Exists(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.Warehouse && x.ProductionYear == or.ProductionYear))
                        {
                            //report[or.OwnerId].Exists(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.Warehouse && x.ProductionYear == or.ProductionYear)
                            continue;
                        }
                        else
                        {
                            rep.MemberId = or.MmemberId;
                            rep.MemberIdNo = or.MemberIDNO;
                            rep.MemberName = or.MemberName;
                            rep.OwnerId = or.OwnerId;
                            rep.OwnerName = or.OwnerName;
                            rep.OwnerIdNo = or.OwnerIDNO;
                            rep.CommodityGrade = or.CommodityGradeId;
                            rep.SessionName = or.SessionName;
                            rep.TradeDate = or.TradeDate;
                            rep.WarehouseId = or.Warehouse;
                            rep.RepId = or.RepIDNO;
                            rep.ProductionYear = or.ProductionYear;
                            rep.Symbol = or.Symbol;
                            rep.WarehouseName = or.WarehouseName;
                            rep.SellOrderQuantity += dtPretrade.Where(x => x.CommodityGradeId == or.CommodityGradeId && x.Warehouse == or.Warehouse && x.ProductionYear == or.ProductionYear && x.OwnerId == rep.OwnerId).Select(y => y.Quantity).Sum();

                            report[rep.OwnerId].Add(rep);
                        }
                    }
                    else
                    {
                        rep.MemberId = or.MmemberId;
                        rep.MemberIdNo = or.MemberIDNO;
                        rep.MemberName = or.MemberName;
                        rep.OwnerId = or.OwnerId;
                        rep.OwnerName = or.OwnerName;
                        rep.OwnerIdNo = or.OwnerIDNO;
                        rep.CommodityGrade = or.CommodityGradeId;
                        rep.SessionName = or.SessionName;
                        rep.TradeDate = or.TradeDate;
                        rep.WarehouseId = or.Warehouse;
                        rep.SellOrderQuantity = rep.SellOrderQuantity += dtPretrade.Where(x => x.CommodityGradeId == or.CommodityGradeId && x.Warehouse == or.Warehouse && x.ProductionYear == or.ProductionYear && x.OwnerId == rep.OwnerId).Select(y => y.Quantity).Sum();
                        rep.RepId = or.RepIDNO;
                        rep.ProductionYear = or.ProductionYear;
                        rep.Symbol = or.Symbol;
                        rep.WarehouseName = or.WarehouseName;
                        List<PreTradeReport> rp = new List<PreTradeReport>();
                        rp.Add(rep);
                        report.Add(rep.OwnerId, rp);
                    }
                }
            }
        }

        private static void ProcessFromOrder(List<Order> dtOrder, List<PreTradeOrderForReport> dtPretrade, Dictionary<Guid, List<PreTradeReport>> report,List<AcceptedTrade> dtAcceptedTrade)
        {
            if (dtOrder != null)
            {

                foreach (Order or in dtOrder)
                {

                    List<PreTradeReport> rp = new List<PreTradeReport>();
                    PreTradeReport rep = new PreTradeReport();
                    if (or.IsClientOrder)
                    {

                        if (report.ContainsKey(or.ClientId))
                        {
                            if (report[or.ClientId].Exists(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear))
                            {
                                if(or.OrderStatus!="Canceled")
                                    report[or.ClientId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().TradeQuantity += or.TradeQuantity;
                                decimal tradeQuantity = report[or.ClientId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().TradeQuantity;
                                decimal registerQuantity = report[or.ClientId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().SellOrderQuantity;
                                decimal variance;
                                if (registerQuantity == 0)
                                    variance =Math.Round((tradeQuantity / 1) * 100,2);
                                else
                                    variance = Math.Round((tradeQuantity / registerQuantity) * 100,2);
                                
                                report[or.ClientId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().Variance = variance;
                                
                                if (or.OrderStatus == "Canceled")
                                {
                                    decimal cancel =report[or.ClientId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().CancelQuantity += or.TradeQuantity;
                                    //check real trade quantity
                                    if (registerQuantity == 0)
                                        variance = Math.Round((tradeQuantity / 1) * 100,2);
                                    else
                                        variance = Math.Round((tradeQuantity / registerQuantity) * 100,2);
                                    report[or.ClientId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().Variance = variance;
                                
                                }
                            }
                            else
                            {
                                

                                rep.MemberId = or.MemberId;
                                rep.MemberIdNo = or.MemberIdNo;
                                rep.MemberName = or.MemberName;
                                rep.OwnerId = or.ClientId;
                                rep.OwnerName = or.OwnerName;
                                rep.OwnerIdNo = or.OwnerIdNo;
                                rep.CommodityGrade = or.CommodityGradeId;
                                rep.SessionName = or.SessionName;
                                rep.TradeDate = or.TradeDate;
                                rep.WarehouseId = or.WarehouseId;
                                
                                rep.RepId = or.RepIdNo;
                                rep.ProductionYear = or.ProductionYear;
                                rep.Symbol = or.Symbol;

                                rep.WarehouseName = or.WarehouseName;
                                if (or.OrderStatus == "Canceled")
                                    rep.CancelQuantity = rep.TradeQuantity;
                                else
                                    rep.TradeQuantity = or.TradeQuantity;
                                if (dtPretrade.Exists(x => x.CommodityGradeId == or.CommodityGradeId && x.Warehouse == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == rep.OwnerId))
                                {
                                    rep.SellOrderQuantity += dtPretrade.Where(x => x.CommodityGradeId == or.CommodityGradeId && x.Warehouse == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == rep.OwnerId).Select(y => y.Quantity).Sum();
                                }
                                if(rep.SellOrderQuantity==0)
                                    rep.Variance = (or.TradeQuantity / 1) * 100;
                                else
                                    rep.Variance = (or.TradeQuantity / rep.SellOrderQuantity) * 100;
                                if(dtAcceptedTrade.Exists(x=>x.CommodityGradeId==or.CommodityGradeId && x.WarehouseId==or.WarehouseId && x.ProductionYear==or.ProductionYear && x.OwnerId==or.OwnerIdNo))
                                {
                                    rep.AcceptedTradeQuantity = dtAcceptedTrade.Where(x => x.CommodityGradeId == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == or.OwnerIdNo).FirstOrDefault().AcceptedTradeQuantity;
                                }
                                else{
                                    rep.AcceptedTradeQuantity = 0;
                                }
                              
                                report[rep.OwnerId].Add(rep);
                            }
                        }
                        else
                        {
                            rep.MemberId = or.MemberId;
                            rep.MemberIdNo = or.MemberIdNo;
                            rep.MemberName = or.MemberName;
                            rep.OwnerId = or.ClientId;
                            rep.OwnerName = or.OwnerName;
                            rep.OwnerIdNo = or.OwnerIdNo;
                            rep.CommodityGrade = or.CommodityGradeId;
                            rep.SessionName = or.SessionName;
                            rep.TradeDate = or.TradeDate;
                            rep.WarehouseId = or.WarehouseId;
                            rep.TradeQuantity = or.TradeQuantity;
                            rep.RepId = or.RepIdNo;
                            rep.ProductionYear = or.ProductionYear;
                            rep.Symbol = or.Symbol;
                            rep.WarehouseName = or.WarehouseName;
                            if (or.OrderStatus == "Canceled")
                                rep.CancelQuantity = rep.TradeQuantity;
                            else
                                rep.TradeQuantity = or.TradeQuantity;
                            if (dtPretrade.Exists(x => x.CommodityGradeId == or.CommodityGradeId && x.Warehouse == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == rep.OwnerId))
                            {
                                rep.SellOrderQuantity += dtPretrade.Where(x => x.CommodityGradeId == or.CommodityGradeId && x.Warehouse == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == rep.OwnerId).Select(y => y.Quantity).Sum();
                            }
                            if (rep.SellOrderQuantity == 0)
                                rep.Variance = Math.Round((or.TradeQuantity / 1) * 100,2);
                            else
                                rep.Variance = Math.Round((or.TradeQuantity / rep.SellOrderQuantity) * 100,2);
                            if (dtAcceptedTrade.Exists(x => x.CommodityGradeId == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == or.OwnerIdNo))
                            {
                                rep.AcceptedTradeQuantity = dtAcceptedTrade.Where(x => x.CommodityGradeId == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == or.OwnerIdNo).FirstOrDefault().AcceptedTradeQuantity;
                            }
                            else
                            {
                                rep.AcceptedTradeQuantity = 0;
                            }
                            rp.Add(rep);
                            report.Add(rep.OwnerId, rp);
                        }
                    }
                    else
                    {//Member Trade
                        if (report.ContainsKey(or.MemberId))
                        {
                            if (report[or.MemberId].Exists(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear))
                            {

                                if (or.OrderStatus != "Canceled")
                                    report[or.MemberId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().TradeQuantity += or.TradeQuantity;
                                decimal tradeQuantity = report[or.MemberId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().TradeQuantity;
                                decimal registerQuantity = report[or.MemberId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().SellOrderQuantity;
                                decimal variance;
                                if (registerQuantity == 0)
                                    variance = Math.Round((tradeQuantity / 1) * 100,2);
                                else
                                    variance = Math.Round((tradeQuantity / registerQuantity) * 100,2);

                                report[or.MemberId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().Variance = variance;

                                if (or.OrderStatus == "Canceled")
                                {
                                    decimal cancel = report[or.MemberId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().CancelQuantity += or.TradeQuantity;
                                    //check real trade quantity
                                    if (registerQuantity == 0)
                                        variance = Math.Round((tradeQuantity / 1) * 100,2);
                                    else
                                        variance = Math.Round((tradeQuantity / registerQuantity) * 100,2);
                                    report[or.MemberId].Where(x => x.CommodityGrade == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear).FirstOrDefault().Variance = variance;

                                }

                            }
                            else
                            {
                                rep.MemberId = or.MemberId;
                                rep.MemberIdNo = or.MemberIdNo;
                                rep.MemberName = or.MemberName;
                                rep.OwnerId = or.MemberId;
                                rep.OwnerName = or.OwnerName;
                                rep.OwnerIdNo = or.OwnerIdNo;
                                rep.CommodityGrade = or.CommodityGradeId;
                                rep.SessionName = or.SessionName;
                                rep.TradeDate = or.TradeDate;
                                rep.WarehouseId = or.WarehouseId;
                                
                                rep.RepId = or.RepIdNo;
                                rep.ProductionYear = or.ProductionYear;
                                rep.Symbol = or.Symbol;
                                rep.WarehouseName = or.WarehouseName;
                                if (or.OrderStatus == "Canceled")
                                    rep.CancelQuantity = rep.TradeQuantity;
                                else
                                    rep.TradeQuantity = or.TradeQuantity;
                                if (dtPretrade.Exists(x => x.CommodityGradeId == or.CommodityGradeId && x.Warehouse == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == rep.OwnerId))
                                {
                                    rep.SellOrderQuantity += dtPretrade.Where(x => x.CommodityGradeId == or.CommodityGradeId && x.Warehouse == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == rep.OwnerId).Select(y => y.Quantity).Sum();
                                }
                                if (rep.SellOrderQuantity == 0)
                                    rep.Variance = Math.Round((or.TradeQuantity / 1) * 100,2);
                                else
                                    rep.Variance = Math.Round((or.TradeQuantity / rep.SellOrderQuantity) * 100,2);
                                if (dtAcceptedTrade.Exists(x => x.CommodityGradeId == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == or.OwnerIdNo))
                                {
                                    rep.AcceptedTradeQuantity = dtAcceptedTrade.Where(x => x.CommodityGradeId == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == or.OwnerIdNo).FirstOrDefault().AcceptedTradeQuantity;
                                }
                                else
                                {
                                    rep.AcceptedTradeQuantity = 0;
                                }
                                report[rep.OwnerId].Add(rep);
                            }
                        }
                        else
                        {
                            rep.MemberId = or.MemberId;
                            rep.MemberIdNo = or.MemberIdNo;
                            rep.MemberName = or.MemberName;
                            rep.OwnerId = or.MemberId;
                            rep.OwnerName = or.OwnerName;
                            rep.OwnerIdNo = or.OwnerIdNo;
                            rep.CommodityGrade = or.CommodityGradeId;
                            rep.SessionName = or.SessionName;
                            rep.TradeDate = or.TradeDate;
                            rep.WarehouseId = or.WarehouseId;
                            rep.RepId = or.RepIdNo;
                            rep.ProductionYear = or.ProductionYear;
                            rep.Symbol = or.Symbol;
                            rep.WarehouseName = or.WarehouseName;
                            if (or.OrderStatus == "Canceled")
                                rep.CancelQuantity = rep.TradeQuantity;
                            else
                                rep.TradeQuantity = or.TradeQuantity;
                            if (dtPretrade.Exists(x => x.CommodityGradeId == or.CommodityGradeId && x.Warehouse == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == rep.OwnerId))
                            {
                                rep.SellOrderQuantity += dtPretrade.Where(x => x.CommodityGradeId == or.CommodityGradeId && x.Warehouse == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == rep.OwnerId).Select(y => y.Quantity).Sum();
                            }
                            if (rep.SellOrderQuantity == 0)
                                rep.Variance = Math.Round((or.TradeQuantity / 1) * 100,2);
                            else
                                rep.Variance = Math.Round((or.TradeQuantity / rep.SellOrderQuantity) * 100,2);
                            if (dtAcceptedTrade.Exists(x => x.CommodityGradeId == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == or.OwnerIdNo))
                            {
                                rep.AcceptedTradeQuantity = dtAcceptedTrade.Where(x => x.CommodityGradeId == or.CommodityGradeId && x.WarehouseId == or.WarehouseId && x.ProductionYear == or.ProductionYear && x.OwnerId == or.OwnerIdNo).FirstOrDefault().AcceptedTradeQuantity;
                            }
                            else
                            {
                                rep.AcceptedTradeQuantity = 0;
                            }
                            rp.Add(rep);
                            report.Add(rep.OwnerId, rp);
                        }
                    }
                }
            }
        }

        void ExportToExcel(DataTable dt)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=VarianceReport.xls");

                HttpContext.Current.Response.Charset = "utf-8";
                HttpContext.Current.Response.ContentEncoding = Encoding.GetEncoding("windows-1250");
                HttpContext.Current.Response.Write("<BR><BR><BR>");

                HttpContext.Current.Response.Write("<Table border='1' bgColor='#ffffff' " +
                  "borderColor='#000000' cellSpacing='0' cellPadding='0' " +
                  "style='font-size:10.0pt; font-family:Calibri; background:white;'> <TR bgcolor='seagreen'>");

                int columnscount = dt.Columns.Count;

                for (int j = 0; j < columnscount; j++)
                {
                    HttpContext.Current.Response.Write("<Td>");
                    HttpContext.Current.Response.Write("<B>");
                    HttpContext.Current.Response.Write(dt.Columns[j].ColumnName);
                    HttpContext.Current.Response.Write("</B>");
                    HttpContext.Current.Response.Write("</Td>");
                }
                HttpContext.Current.Response.Write("</TR>");
                foreach (DataRow row in dt.Rows)
                {
                    HttpContext.Current.Response.Write("<TR>");
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        HttpContext.Current.Response.Write("<Td>");
                        HttpContext.Current.Response.Write(row[i].ToString());
                        HttpContext.Current.Response.Write("</Td>");
                    }

                    HttpContext.Current.Response.Write("</TR>");
                }
                HttpContext.Current.Response.Write("</Table>");
                HttpContext.Current.Response.Write("</font>");
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.End();
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {

            ExportToExcel(dTable);
        }

        #endregion

    }
}