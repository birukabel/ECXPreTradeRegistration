using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ECX.PreTradeRegistration.UI.Pages
{
    public partial class BuyOrderRegistration : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["LoggedUser"] == null)
                Response.Redirect("~/Login.aspx");
        }
    }
}