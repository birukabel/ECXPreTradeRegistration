using ECX.PreTradeRegistration.BE;
using ECX.PreTradeRegistration.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECX.PreTradeRegistration.BackOffice
{
    public partial class SellOrderReport : System.Web.UI.Page
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
            lblMsg.Text = "";
            if (txtDateFrom.Text.Equals("") && !txtDateTo.Text.Equals("") || !txtDateFrom.Text.Equals("") && txtDateTo.Text.Equals(""))
            {
                lblMsg.Text =  "Please select both dates";
                return;
            }
            else
            {
                if (!txtDateFrom.Text.Equals("") && !txtDateTo.Text.Equals("") && txtMemberId.Text.Equals(""))
                {
                   
                    dt = new PreTradeOrderBLL().GetSellerPreTradeReport(Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), "");
                    
                }
                else if (txtDateFrom.Text.Equals("") && txtDateTo.Text.Equals("") && !txtMemberId.Text.Equals(""))
                {
                    dt = new PreTradeOrderBLL().GetSellerPreTradeReport(Convert.ToDateTime("01/01/0001"), Convert.ToDateTime("12/31/9999"), txtMemberId.Text);
                }
                else if (!txtDateFrom.Text.Equals("") && !txtDateTo.Text.Equals("") && !txtMemberId.Text.Equals(""))
                {
                    dt = new PreTradeOrderBLL().GetSellerPreTradeReport(Convert.ToDateTime(txtDateFrom.Text), Convert.ToDateTime(txtDateTo.Text), txtMemberId.Text);
                }
                else
                {
                    lblMsg.Text = "Please select either member Id or date range";
                    return;
                }

            }
            if (dt.Rows.Count == 0)
            {
                lblMsg.Text = "No record found";
               
            }
            gvReport.DataSource = dt;
            gvReport.DataBind();
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            if (dt != null && dt.Rows.Count > 0)
            {
                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ClearContent();
                HttpContext.Current.Response.ClearHeaders();
                HttpContext.Current.Response.Buffer = true;
                HttpContext.Current.Response.ContentType = "application/ms-excel";
                HttpContext.Current.Response.Write(@"<!DOCTYPE HTML PUBLIC ""-//W3C//DTD HTML 4.0 Transitional//EN"">");
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=SellOrderReport.xls");

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
    }
}