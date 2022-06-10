using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using ECX.PreTradeRegistration.BE;
using ECX.PreTradeRegistration.BLL;

namespace ECX.PreTradeRegistration.BackOffice
{
    public partial class mTop : System.Web.UI.MasterPage
    {
        protected string BannerPath = VirtualPathUtility.ToAbsolute("~/Images/TopBack.gif");
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void lnkLogOut_Click(object sender, EventArgs e)
        {
            UserInfo user = (UserInfo)this.Session["LoggedUser"];

            if (new PreTradeLoginDetialBLL().UpdatePreTradeLogOutRecord(user.UniqueIdentifier))
            {
                this.Session["LoggedUser"] = null;
                this.Session.Clear();
                Response.Redirect("~/Login.aspx");
                
            }
            else
            {
               // lblNotification.Text = "Error occured while user logging out";
            }
        }
    }
}
