using ECX.PreTradeRegistration.BLL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECX.PreTradeRegistration.BE;
using System.Text;


namespace ECX.PreTradeRegistration.UI.Pages
{
    public partial class SellOrderRegistration : System.Web.UI.Page
    {

        static Dictionary<string, string> availBalance = new Dictionary<string, string>();

        List<DateTime> GetAvailableTradesDates()
        {
            List<DateTime> lstDatetime = new List<DateTime>();
            lstDatetime.Add(new PreTradeOrderBLL().GetNextTradeDate());
            DateTime dtToday = DateTime.Now;
            if (!(dtToday.DayOfWeek == DayOfWeek.Saturday) || !(dtToday.DayOfWeek == DayOfWeek.Sunday))
            {
                lstDatetime.Add(dtToday);
            }
            
            return lstDatetime;
        }

        private void BindTradeDateDropDown()
        {
            ddlTradeDate.DataSource = GetAvailableTradesDates();
            ddlTradeDate.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["LoggedUser"] == null)
                Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                BindTradeDateDropDown();
                //txtNxtTradeDate.Text = new PreTradeOrderBLL().GetNextTradeDate().ToShortDateString();
                BindPreTradeOrders();
            }
            BindPositionSummaryToGrid();
        }

        private void BindPreTradeOrders()
        {
            // gvPreTradeOrders.DataSource = null;
            // gvPreTradeOrders.DataBind();
            DataTable dtMember = (DataTable)this.Session["Member"];
            if (dtMember != null && dtMember.Rows.Count > 0)
            {
                var memberId = dtMember.Rows[0]["MemberId"];
                gvPreTradeOrders.DataSource = new PreTradeOrderBLL().GetPreTradeOrderByMemberIdAndTradeDate(new Guid(memberId.ToString()), Convert.ToDateTime(ddlTradeDate.SelectedValue));
                gvPreTradeOrders.DataBind();
            }

        }

        private void BindPositionSummaryToGrid()
        {
            if (this.Session["LoggedUser"] == null)
                Response.Redirect("~/Login.aspx");
            //UserInfo userI = (UserInfo)this.Session["LoggedUser"];
            DataTable dtMember = (DataTable)this.Session["Member"];

            if (dtMember != null && dtMember.Rows.Count > 0)
            {
                DataRow dr = dtMember.Rows[0];
                DataTable dataTable = new PositionSummary().GetPreTradePositionSummary(new Guid(dr["MemberId"].ToString()));
                Table table = this.FindControl("dtSellOrder") as Table;
                int index = 1;

                StringBuilder html = new StringBuilder();

                foreach (DataRow row in dataTable.Rows)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + index + "</td>");
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        if (column.ColumnName == "MemberIDNO" || column.ColumnName == "OwnerIDNO" || column.ColumnName == "OwnerName" || column.ColumnName == "GRNNumber" || column.ColumnName == "Symbol"
                            || column.ColumnName == "WarehouseName" || column.ColumnName == "ProductionYear" || column.ColumnName == "Quantity")
                        {
                            if (column.ColumnName == "GRNNumber")
                            {
                                html.Append("<td style='padding: 6px 0px 0px 0px'>");
                                html.Append("<a href='#' onclick='populateForm(\"" + row["Id"] + "\")'>" + row[column.ColumnName] + "</a>");
                                html.Append("</td>");
                            }
                            else
                            {
                                html.Append("<td style='padding: 6px 0px 0px 0px'>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                        }
                        //else
                        //{
                        //    html.Append("<td style='display:none;'>");
                        //    html.Append(row[column.ColumnName]);
                        //    html.Append("</td>");
                        //}
                    }
                    html.Append("</tr>");
                    index++;
                }
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                //  gvPositionSummary.DataSource = new PositionSummary().GetPreTradePositionSummary(new Guid(dr["MemberId"].ToString()));
                // gvPositionSummary.DataBind();
            }
        }

        protected void gvPositionSummary_SelectedIndexChanged(object sender, EventArgs e)
        {
            /* AssigenTextToNotificationLabel("", System.Drawing.Color.Wheat);
             ClearControls();
             GridViewRow row = gvPositionSummary.SelectedRow;
             var whrId = gvPositionSummary.DataKeys[row.RowIndex].Value;
             if (whrId != null)
             {
                 DataTable available = new PositionSummary().GetAvailableBalance(new Guid(whrId.ToString()));
                 if (available.Rows.Count > 0)
                 {
                     PopulateControls(available.Rows[0]);*/


            /*txtOwnerId.Text = available.Rows[0]["OwnerIDNO"].ToString();
            hdOwnerId.Value = available.Rows[0]["OwnerId"].ToString();
            hdOwnerName.Value = available.Rows[0]["OwnerName"].ToString();
            txtSymbol.Text = available.Rows[0]["Symbol"].ToString();
            hdCommodityGrade.Value = available.Rows[0]["CommodityGrade"].ToString();
            txtWarehouse.Text = available.Rows[0]["WarehouseName"].ToString();
            hdWarehouse.Value = available.Rows[0]["WarehouseId"].ToString();
            txtPY.Text = available.Rows[0]["ProductionYear"].ToString();
            txtAvailable.Text = available.Rows[0]["AvailableQuantity"].ToString();
            if (Convert.ToDecimal(txtAvailable.Text) <= Convert.ToDecimal(ConfigurationManager.AppSettings["MinimumSellAmount"]))
            {
                txtQuantity.Enabled = false;
                txtQuantity.Text = available.Rows[0]["AvailableQuantity"].ToString();
            }
            */

            // }
            // }

        }

        void PopulateControls(DataRow dr)
        {
            if (Convert.ToDecimal(dr["AvailableQuantity"]) <= 0)
            {
                AssigenTextToNotificationLabel("Available quantity must be greater than zero", "error");
            }
            else
            {
                txtOwnerId.Value = dr["OwnerIDNO"].ToString();
                hdOwnerId.Value = dr["OwnerId"].ToString();
                hdOwnerName.Value = dr["OwnerName"].ToString();
                txtSymbol.Value = dr["Symbol"].ToString();
                hdCommodityGrade.Value = dr["CommodityGrade"].ToString();
                txtWarehouse.Value = dr["WarehouseName"].ToString();
                hdWarehouse.Value = dr["WarehouseId"].ToString();
                txtPY.Value = dr["ProductionYear"].ToString();

                txtAvailable.Value = dr["AvailableQuantity"].ToString();
                //if (Convert.ToDecimal(txtAvailable.Text) <= Convert.ToDecimal(ConfigurationManager.AppSettings["MinimumSellAmount"]))
                //{
                //    txtQuantity.Enabled = false;
                //    txtQuantity.Text = dr["AvailableQuantity"].ToString();
                //}


                txtQuantity.Value = "";
            }
        }

        void ClearControls()
        {
            txtOwnerId.Value = "";
            hdOwnerId.Value = "";
            hdOwnerName.Value = "";
            txtSymbol.Value = "";
            hdCommodityGrade.Value = "";
            txtWarehouse.Value = "";
            hdWarehouse.Value = "";
            txtPY.Value = "";
            txtAvailable.Value = "";
            txtQuantity.Disabled = false;
            txtQuantity.Value = "";
            lblNotification.Text = "";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        private void SaveRecord()
        {
            if (this.Session["LoggedUser"] == null)
                Response.Redirect("~/Login.aspx");
            if (txtAvailable.Value != "")
            {
                decimal available = Math.Round(Convert.ToDecimal(txtAvailable.Value), 4);

                if (available > 0)
                {
                    decimal quantity = Convert.ToDecimal(txtQuantity.Value);


                    if (available > Convert.ToDecimal(ConfigurationManager.AppSettings["MinimumAvailableAmount"]) && Convert.ToDecimal(txtQuantity.Value) < Convert.ToDecimal(ConfigurationManager.AppSettings["MinimumSellAmount"]))
                    {
                        AssigenTextToNotificationLabel("Miniumum Sell Order Amount is 50 Quintal", "error");
                        //AssigenTextToNotificationLabel("Tetst", "error");
                        txtQuantity.Focus();
                        txtQuantity.Disabled = false;
                        txtQuantity.Value = "";
                    }
                    else
                    {
                        if (quantity > available)
                        {
                            AssigenTextToNotificationLabel("Quantity shall not exceed available quantity", "error");
                            txtQuantity.Focus();
                            txtQuantity.Disabled = false;
                            txtQuantity.Value = "";
                        }
                        else
                        {
                            DataTable dtMember = (DataTable)this.Session["Member"];

                            UserInfo userI = (UserInfo)this.Session["LoggedUser"];

                            if (dtMember != null && dtMember.Rows.Count > 0)
                            {

                                DataRow dr = dtMember.Rows[0];
                                if (txtQuantity.Value != "" || txtPY.Value != "" || txtSymbol.Value != "" || txtWarehouse.Value != "" ||
                                    txtOwnerId.Value != "" || txtAvailable.Value != "")
                                {
                                    if (availBalance != null && availBalance.Count > 0)
                                    {

                                        if (Convert.ToDateTime(ddlTradeDate.SelectedValue).ToShortDateString() == DateTime.Now.ToShortDateString() && DateTime.Now >
                                           Convert.ToDateTime(DateTime.Now.Date.ToShortDateString() + " " + ConfigurationManager.AppSettings["MaxTime"] + ":00:00 PM"))
                                        {
                                            AssigenTextToNotificationLabel("You can not register order past " + ConfigurationManager.AppSettings["MaxTime"] + ":00:00 PM " + "for today", "error");
                                            return;
                                        }
                                        

                                        //check if order exists (if an order with pending or edited status exists don't allow an order to be saved)
                                        DataTable dtPreTOrder = new PreTradeOrderBLL().GetPreTradeOrderByMemberIdAndTradeDate(new Guid(Convert.ToString(dr["MemberId"])), Convert.ToDateTime(ddlTradeDate.SelectedValue));
                                        if (dtPreTOrder.Rows.Count > 0)
                                        {
                                            if (dtPreTOrder.AsEnumerable().Where(x => (string)x["OwnerIDNO"] == availBalance["OwnerIDNO"] && (string)x["Symbol"] == availBalance["Symbol"] &&
                                            (string)x["WarehouseName"] == availBalance["Warehousename"] && (int)x["ProductionYear"] == Convert.ToInt32(availBalance["ProductionYear"]) &&
                                            ((string)x["StatusName"] == "Pending" || (string)x["StatusName"] == "Edited")).Count() == 0)
                                            {
                                                if (new PreTradeOrderBLL().SavePreTradeOrder(Guid.NewGuid(), Convert.ToDateTime(ddlTradeDate.SelectedValue),
                                                                                           new Guid(Convert.ToString(dr["MemberId"])), Convert.ToString(dr["IDNO"]), Convert.ToString(dr["OrganizationName"]),
                                                                                          new Guid(availBalance["OwnerId"]), availBalance["OwnerIDNO"], availBalance["OwnerName"], new Guid(this.Session["RepId"].ToString()),
                                                                                          userI.UserName, Convert.ToString(dr["FullName"]), availBalance["Symbol"], new Guid(availBalance["CommodityGrade"]),
                                                                                           new Guid(availBalance["WarehouseId"]), availBalance["Warehousename"], Convert.ToInt32(availBalance["ProductionYear"]),
                                                                                           Math.Round(Convert.ToDecimal(txtQuantity.Value), 4), 1, new Guid(this.Session["RepId"].ToString())))
                                                {
                                                    AssigenTextToNotificationLabel("Record Saved successfully", "success");
                                                    ClearControls();
                                                }

                                                else
                                                {
                                                    AssigenTextToNotificationLabel("error while saving record", "error");
                                                }
                                            }
                                            else
                                            {
                                                AssigenTextToNotificationLabel("Your already have submitted and order with the same contract. If you want to change quantity you can use edit.", "error");
                                                ClearControls();
                                            }
                                        }
                                        else
                                        {
                                            if (new PreTradeOrderBLL().SavePreTradeOrder(Guid.NewGuid(), Convert.ToDateTime(ddlTradeDate.SelectedValue),
                                                                                          new Guid(Convert.ToString(dr["MemberId"])), Convert.ToString(dr["IDNO"]), Convert.ToString(dr["OrganizationName"]),
                                                                                         new Guid(availBalance["OwnerId"]), availBalance["OwnerIDNO"], availBalance["OwnerName"], new Guid(this.Session["RepId"].ToString()),
                                                                                         userI.UserName, Convert.ToString(dr["FullName"]), availBalance["Symbol"], new Guid(availBalance["CommodityGrade"]),
                                                                                          new Guid(availBalance["WarehouseId"]), availBalance["Warehousename"], Convert.ToInt32(availBalance["ProductionYear"]),
                                                                                          Math.Round(Convert.ToDecimal(txtQuantity.Value), 4), 1, new Guid(this.Session["RepId"].ToString())))
                                            {
                                                AssigenTextToNotificationLabel("Record Saved successfully", "success");
                                                ClearControls();
                                            }

                                            else
                                            {
                                                AssigenTextToNotificationLabel("error while saving record", "error");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        AssigenTextToNotificationLabel("error while saving record", "error");
                                    }
                                }
                                else
                                {
                                    AssigenTextToNotificationLabel("Please provide quantity ", "error");
                                }
                            }
                        }
                    }
                }
            }
            BindPreTradeOrders();
            BindPositionSummaryToGrid();
        }

        void AssigenTextToNotificationLabel(string strMsg, string msgType)
        {
            //lblNotification.Text = strMsg;
            // lblNotification.ForeColor = _color;
            ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "", "showToast('" + strMsg + "','" + msgType + "')", true);

        }

        protected void gvPreTradeOrders_RowEditing(object sender, GridViewEditEventArgs e)
        {
            gvPreTradeOrders.EditIndex = e.NewEditIndex;
            BindPreTradeOrders();
            BindPositionSummaryToGrid();
        }

        protected void gvPreTradeOrders_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            gvPreTradeOrders.EditIndex = -1;
            BindPreTradeOrders();
            BindPositionSummaryToGrid();
        }

        protected void gvPreTradeOrders_RowUpdating(object sender, GridViewUpdateEventArgs e) //updates data from gridview
        {
            GridViewRow row = gvPreTradeOrders.Rows[e.RowIndex];
            var dtK = gvPreTradeOrders.DataKeys[e.RowIndex];
            if (dtK != null)
            {
                //UserInfo userI = (UserInfo)this.Session["LoggedUser"];
                Guid orderId = new Guid(dtK.Value.ToString());
                var quantity = (TextBox)(row.FindControl("txtQuantity"));
                Label lblOwnerId = (Label)(row.FindControl("lblOwnerId"));
                Label strSymbol = (Label)(row.FindControl("lblSymbol"));
                Label strWH = (Label)(row.FindControl("lblWarehouse"));
                Label strPY = (Label)(row.FindControl("lblPy"));
                Label strStatus = (Label)(row.FindControl("lblStatusName"));

                if (strStatus.Text.Equals("Pending") || strStatus.Text.Equals("Edited"))
                {
                    if (!string.IsNullOrWhiteSpace(quantity.Text))
                    {
                        decimal avilableQTY = Math.Round(new PreTradeOrderBLL().GetAvailableQuantityViaContractNOwner(orderId, strSymbol.Text, strWH.Text, Convert.ToInt32(strPY.Text), Convert.ToDateTime(ddlTradeDate.SelectedValue), lblOwnerId.Text), 4);
                        if (Convert.ToDecimal(quantity.Text) > avilableQTY || Convert.ToDecimal(quantity.Text) <= 0)
                        {
                            AssigenTextToNotificationLabel("Quantity should not be greater than available quantity (" + avilableQTY.ToString() + ") and quantity must be greater than zero", "error");
                        }
                        else
                        {
                            if (Convert.ToDecimal(quantity.Text) < Convert.ToDecimal(ConfigurationManager.AppSettings["MinimumSellAmount"])
                                && avilableQTY > Convert.ToDecimal(quantity.Text)
                                && Convert.ToDecimal(quantity.Text) <= 0)
                            {
                                AssigenTextToNotificationLabel("Quantity should not be less than the minimum quantity (" + Convert.ToDecimal(ConfigurationManager.AppSettings["MinimumSellAmount"]) + ") and quantity must be greater than zero ", "error");
                            }
                            else
                            {
                                if (new PreTradeOrderBLL().UpdatePreTradeOrder(orderId, Convert.ToDecimal(quantity.Text), 2, new Guid(Session["RepId"].ToString())))
                                    AssigenTextToNotificationLabel("Your order has been edited successfully.", "success");
                                else
                                    AssigenTextToNotificationLabel("Error while order is edited.", "error");
                                gvPreTradeOrders.EditIndex = -1;
                                BindPreTradeOrders();
                                BindPositionSummaryToGrid();
                            }
                        }
                    }
                }
                else
                {
                    AssigenTextToNotificationLabel("You can't edit orders with " + strStatus.Text, "error");
                }
            }
        }

        protected void gvPreTradeOrders_RowDeleting(object sender, GridViewDeleteEventArgs e) //cancel data from gridview
        {
            try
            {
                GridViewRow row = gvPreTradeOrders.Rows[e.RowIndex];
                var dtK = gvPreTradeOrders.DataKeys[e.RowIndex];
                if (dtK != null)
                {
                    Label strStatus = (Label)(row.FindControl("lblStatusName"));
                    if (strStatus.Text.Equals("Pending") || strStatus.Text.Equals("Edited"))
                    {
                        //UserInfo userI = (UserInfo)this.Session["LoggedUser"];
                        Guid orderId = new Guid(gvPreTradeOrders.DataKeys[e.RowIndex].Values[0].ToString());
                        if (new PreTradeOrderBLL().CancelPreTradeOrder(orderId, 3, new Guid(Session["RepId"].ToString())))
                            AssigenTextToNotificationLabel("Your Order has been Canceled Successfully.", "success");
                        else
                            AssigenTextToNotificationLabel("Error while order is being cancelled.", "error");
                        BindPreTradeOrders();
                        BindPositionSummaryToGrid();
                    }
                    else
                    {
                        AssigenTextToNotificationLabel("You can't cancel orders with " + strStatus.Text, "error");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void lnk_Click(object sender, EventArgs e)
        {
            lblOwner.Text = hdOwnerName.Value;
            lblSymbol.Text = txtSymbol.Value;
            lblWarehouse.Text = txtWarehouse.Value;
            lblPY.Text = txtPY.Value;
            lblQuantity.Text = txtQuantity.Value;
            StringBuilder sb = new StringBuilder();
            sb.Append(@"<script type='text/javascript'>");
            sb.Append("$('#DetailModalNew').modal('show');");
            sb.Append(@"</script>");
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(),
                    "OpenDetailModalScript", sb.ToString(), false);

        }
        public void CancelOrder(string oId)
        {
            /* try
             {

                 GridViewRow row = gvPreTradeOrders.SelectedRow.RowIndex;
                  row.RowIndex.ToString();

                 GridViewRow row = gvPreTradeOrders.Rows[e.RowIndex];
                 var dtK = gvPreTradeOrders.DataKeys[e.RowIndex];
                 if (dtK != null)
                 {
                     Label strStatus = (Label)(row.FindControl("lblStatusName"));
                     if (strStatus.Text.Equals("Pending") || strStatus.Text.Equals("Edited"))
                     {

                         UserInfo userI = (UserInfo)this.Session["LoggedUser"];
                         Guid orderId = new Guid(gvPreTradeOrders.DataKeys[e.RowIndex].Values[0].ToString());
                         if (new PreTradeOrderBLL().CancelPreTradeOrder(orderId, 3, userI.UniqueIdentifier))
                             AssigenTextToNotificationLabel("Your Order has been Canceled Successfully.", System.Drawing.Color.Green);
                         else
                             AssigenTextToNotificationLabel("Error while order is being cancelled.", System.Drawing.Color.Red);
                         BindPreTradeOrders();
                         BindPositionSummaryToGrid();
                     }
                     else
                     {
                         AssigenTextToNotificationLabel("You can't cancel orders with " + strStatus.Text, System.Drawing.Color.Red);
                     }
                 }
             }
             catch (Exception ex)
             {
                 throw ex;
             }*/
        }
        [System.Web.Services.WebMethod()]
        public static Dictionary<string, string> lnkGrn_Click(string whrID)
        {
            // ClearControls();          
            if (!String.IsNullOrEmpty(whrID))
            {
                DataTable available = new PositionSummary().GetAvailableBalance(new Guid(whrID.ToString()));
                if (available.Rows.Count > 0)
                {
                    //PopulateControls(available.Rows[0]);
                    availBalance.Clear();
                    availBalance.Add("OwnerIDNO", available.Rows[0]["OwnerIDNO"].ToString());
                    availBalance.Add("OwnerId", available.Rows[0]["OwnerId"].ToString());
                    availBalance.Add("OwnerName", available.Rows[0]["OwnerName"].ToString());
                    availBalance.Add("CommodityGrade", available.Rows[0]["CommodityGrade"].ToString());
                    availBalance.Add("Symbol", available.Rows[0]["Symbol"].ToString());
                    availBalance.Add("Warehousename", available.Rows[0]["Warehousename"].ToString());
                    availBalance.Add("WarehouseId", available.Rows[0]["WarehouseId"].ToString());
                    availBalance.Add("ProductionYear", available.Rows[0]["ProductionYear"].ToString());
                    availBalance.Add("AvailableQuantity", available.Rows[0]["AvailableQuantity"].ToString());
                    return availBalance;
                }
            }
            return null;
        }

        protected void gvPreTradeOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPreTradeOrders.PageIndex = e.NewPageIndex;
            BindPreTradeOrders();
            BindPositionSummaryToGrid();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            ClearControls();
            BindPreTradeOrders();
            BindPositionSummaryToGrid();
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            SaveRecord();
        }

        protected void no_Click(object sender, EventArgs e)
        {

        }

    }
}