using ECX.PreTradeRegistration.BLL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECX.PreTradeRegistration.UI.Reports
{
    public partial class SellerBuyerReport : System.Web.UI.Page
    {
        StringBuilder html = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["LoggedUser"] == null)
                Response.Redirect("~/Login.aspx");
            if (!IsPostBack)
            {
                BindToGrid();
            }
        }

        private void BindToGrid()
        {
            DataTable result = new PreTradeOrderBLL().GetBuyerPreTradeReport(Convert.ToDateTime("12/31/9999"));
            Session["buyReport"] = result;
            if(result.Rows.Count > 0)
            {
                foreach(DataRow r in result.Rows)
                {
                    html.Append("<tr>");
                    html.Append("<td>" + r["TradeDate"].ToString() + "</td>");
                    html.Append("<td>" + r["Symbol"].ToString() + "</td>");
                    html.Append("<td>" + r["WarehouseName"].ToString() + "</td>");
                    html.Append("<td>" + r["ProductionYear"].ToString() + "</td>");
                    html.Append("<td>" + r["AggregateQTY"].ToString() + "</td>");
                    html.Append("</tr>");
                }
                PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
            }
        }

        protected void lbtnExport_Click(object sender, EventArgs e)
        {
            ExportToExcel((DataTable)Session["buyReport"]);
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
                HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;filename=BuyerReport.xls");

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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtTradeDate.Text))
            {
                DataTable result = new PreTradeOrderBLL().GetBuyerPreTradeReport(Convert.ToDateTime(txtTradeDate.Text));
                Session["buyReport"] = result;
                if (result.Rows.Count > 0)
                {
                    foreach (DataRow r in result.Rows)
                    {
                        html.Append("<tr>");
                        html.Append("<td>" + r["TradeDate"].ToString() + "</td>");
                        html.Append("<td>" + r["Symbol"].ToString() + "</td>");
                        html.Append("<td>" + r["WarehouseName"].ToString() + "</td>");
                        html.Append("<td>" + r["ProductionYear"].ToString() + "</td>");
                        html.Append("<td>" + r["AggregateQTY"].ToString() + "</td>");
                        html.Append("</tr>");
                    }
                    PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                }
            }
            
        }
    }
}