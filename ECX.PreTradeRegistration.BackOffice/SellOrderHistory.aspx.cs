using ECX.PreTradeRegistration.BE;
using ECX.PreTradeRegistration.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECX.PreTradeRegistration.BackOffice
{
    public partial class SellOrderHistory : System.Web.UI.Page
    {
        static DataTable dt;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["LoggedUser"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
            if (!IsPostBack)
            {
               
                UserInfo user = (UserInfo)this.Session["LoggedUser"];
                if (user == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
                ECXSecurityAccess.ECXSecurityAccess Sec = new ECXSecurityAccess.ECXSecurityAccess();
                float[] rights = Sec.HasRights(user.UniqueIdentifier, new string[] { 
                        "ViewSellOrderReport" }, "");
                if (!(rights[0] == 1 || rights[0] == 3))
                    Response.Redirect("~/ErrorPage.aspx");
            }
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            if ((txtDateFrom.Text.Equals("") && !txtDateTo.Text.Equals("")) || (!txtDateFrom.Text.Equals("") && txtDateTo.Text.Equals("")))
            {
                lblMsg.Text = "Please select both dates";
                return;
            }
            else
            {
                if (!txtDateFrom.Text.Equals("") && !txtDateTo.Text.Equals("") && txtMemberId.Text.Equals(""))
                {
                    dt = new PreTradeOrderBLL().GetSellerPreTradeReportForHistory(Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), "");
                }
                else if (txtDateFrom.Text.Equals("") && txtDateTo.Text.Equals("") && !txtMemberId.Text.Equals(""))
                {
                    dt = new PreTradeOrderBLL().GetSellerPreTradeReportForHistory(Convert.ToDateTime("01/01/0001"), Convert.ToDateTime("12/31/9999"), txtMemberId.Text);
                }
                else if (!txtDateFrom.Text.Equals("") && !txtDateTo.Text.Equals("") && !txtMemberId.Text.Equals(""))
                {
                    dt = new PreTradeOrderBLL().GetSellerPreTradeReportForHistory(Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), txtMemberId.Text);
                }
                else
                {
                    lblMsg.Text = "Please select either member Id or date range";
                    return;
                }
                if (dt.Rows.Count == 0)
                {
                    lblMsg.Text = "No record found";
                }
              
            }
            gvReport.DataSource = dt;
            gvReport.DataBind();
        }

        protected void gvReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView gv = (GridView)sender;
            dt = new PreTradeOrderBLL().GetSellerPreTradeReportDetailById(new Guid(gv.SelectedDataKey.Value.ToString()));
            gvDetail.DataSource = dt;
            gvDetail.DataBind();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "ShowPopup();", true);
        }        
    }
}