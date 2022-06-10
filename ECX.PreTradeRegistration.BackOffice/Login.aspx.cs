using ECX.PreTradeRegistration.BE;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECX.PreTradeRegistration.BLL;

namespace ECX.PreTradeRegistration.BackOffice
{
    public partial class Login : System.Web.UI.Page
    {
        static Guid userADID;
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsAuthenticated(System.Configuration.ConfigurationManager.AppSettings["DirPath"], System.Configuration.ConfigurationManager.AppSettings["domain"], HttpUtility.HtmlEncode(txtUserName.Text), txtPassword.Text, System.Configuration.ConfigurationManager.AppSettings["ACDUser"], System.Configuration.ConfigurationManager.AppSettings["ACDPass"]))
            {
                ECXSecurityAccess.AuthenticationStatus AuStatus = new ECXSecurityAccess.ECXSecurityAccess().IsAuthenticated(
                    HttpUtility.HtmlEncode(txtUserName.Text), txtPassword.Text, "", out userADID);

                if (AuStatus == ECXSecurityAccess.AuthenticationStatus.AccessGranted)
                {
                    UserInfo user = new UserInfo();
                    user.UniqueIdentifier = userADID;
                    user.UserName = HttpUtility.HtmlEncode(txtUserName.Text);
                    this.Session["LoggedUser"] = user;

                    this.Response.Redirect("~/Home.aspx");

                }
                else
                {
                    lblStatus.Text = "Invalid user name or password";
                }
            }
            else
            {
                lblStatus.Text = "Invalid user name or password";
            } 
        }
       

        public bool IsAuthenticated(string dirPath, string _domain, string userName, string pwd, string _adAdminUser, string _adAdminPass)
        {
            string domain = _domain;//SettingReader.ADDomainNameForEmployees;
            string LDAP_Path = dirPath;//SettingReader.ADPathForEmployees
            //string container = "OU=Trade, DC=Trade, DC=ECX, DC=com";
            string adAdminUser = _adAdminUser;//System.Configuration.ConfigurationManager.AppSettings["ACDUser"];
            string adAdminPass = _adAdminPass;//System.Configuration.ConfigurationManager.AppSettings["ACDPass"];

            if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(LDAP_Path))
                return false;
            string domainAndUsername = domain + "\\" + userName;

            try
            {
                #region Authenticate using Directory Search
                //DirectoryEntry entry = new DirectoryEntry(LDAP_Path, userName, pwd, AuthenticationTypes.Secure | AuthenticationTypes.Sealing);
                using (DirectoryEntry entry = new DirectoryEntry(LDAP_Path, userName, pwd, AuthenticationTypes.Secure | AuthenticationTypes.Sealing | AuthenticationTypes.Signing))
                {
                    //Bind to the native AdsObject to force authentication.
                    object obj = entry.NativeObject;
                    DirectorySearcher search = new DirectorySearcher(entry);

                    search.Filter = "(sAMAccountName=" + userName + ")";
                    search.PropertiesToLoad.Add("CN");
                    SearchResultCollection results = search.FindAll();
                    if (results == null || results.Count == 0)
                    {//no AD record found
                        return false;
                    }
                    if (results.Count > 1)
                    {//multiple AD records were found
                        results.Dispose();
                        return false;
                    }
                    SearchResult result = results[0];//take the first AD Record

                    if (result != null)
                    {
                        DirectoryEntry userADEntry = result.GetDirectoryEntry();
                        userADID = userADEntry.Guid;
                        Session["LoggedUser"] = userADID;
                    }
                    else
                    {
                        return false;
                    }
                    entry.Close();
                    return true;
                }
                #endregion
            }
            catch (Exception ex)
            {
                return false;//authentication fails - let the AD handle the # of trials

                //throw new Exception("Error authenticating user. \n" + ex.Message);
            }
        }
    }
}