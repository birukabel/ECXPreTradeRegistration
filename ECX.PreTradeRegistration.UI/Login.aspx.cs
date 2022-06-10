using ECX.PreTradeRegistration.BE;
using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ECX.PreTradeRegistration.BLL;
using System.Web.SessionState;

namespace ECX.PreTradeRegistration.UI
{
    public partial class Login : System.Web.UI.Page
    {
        //static Guid userADID;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            //userADID = new Guid("47d43279-eea0-442e-ae4a-db7a31b66552");
            if (!string.IsNullOrWhiteSpace(txtUserName.Text) && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                if (IsAuthenticated(System.Configuration.ConfigurationManager.AppSettings["DirPath"], System.Configuration.ConfigurationManager.AppSettings["domain"], HttpUtility.HtmlEncode(txtUserName.Text), txtPassword.Text, System.Configuration.ConfigurationManager.AppSettings["ACDUser"], System.Configuration.ConfigurationManager.AppSettings["ACDPass"]))
                {
                    //Session["LoggedUser2"] = "f7a8d459-1dbe-49e4-b422-2f1a472acc89";
                    //SessionIDManager manager = new SessionIDManager();
                    //string newSessionId = manager.CreateSessionID(HttpContext.Current);

                    UserInfo user = new UserInfo();

                    if (Session["LoggedUser2"] != null)
                    {

                        user.UniqueIdentifier = new Guid(Session["LoggedUser2"].ToString());
                        string strUserName = txtUserName.Text.Trim().Replace('r', 'R');
                        user.UserName = HttpUtility.HtmlEncode(strUserName);
                        //user.UniqueIdentifier = new Guid(Session["LoggedUser2"].ToString());
                    }

                    string ipAddress = GetClientIPAddress(Request);//GetClientIPAddress(Request); //"10.1.16.10"; 
                    Guid repId = new Guid(new PreTradeLoginDetialBLL().GetRepIdByADId(user.UniqueIdentifier).Rows[0]["RepId"].ToString());
                    if (!repId.Equals(Guid.Empty))
                    {
                        if (new PreTradeLoginDetialBLL().CheckRepIsActive(repId))
                        {
                            if (new PreTradeLoginDetialBLL().SavePreTradeLoginRecord(Guid.NewGuid(), repId, ipAddress))
                            {
                                if (user.UniqueIdentifier != null)
                                {
                                    this.Session["LoggedUser"] = user;
                                    this.Session["RepId"] = repId;
                                    this.Session["Member"] = new PreTradeLoginDetialBLL().GetMemberIdByRepId(repId);
                                    this.Response.Redirect("~/Pages/Home.aspx");

                                }
                            }
                            else
                            {
                                lblStatus.Text = "Error occured while user atenticates";
                            } 
                        }
                        else
                        {
                            lblStatus.Text = "Rep Id:" + txtUserName.Text + " is not active";
                        }                         
                    }
                    else
                    {
                        lblStatus.Text = "Rep Id:"+ txtUserName.Text +" is not mapped";
                    }                                                      
                }
                else
                {
                    lblStatus.Text = "Invalid user name or password";
                }
            }            
        }
        private static string GetClientIPAddress(System.Web.HttpRequest httpRequest)
        {
            //string hostName = Dns.GetHostName(); // Retrive the Name of HOST  

            // Get the IP  
            //string myIP = Dns.GetHostByName(hostName).AddressList[0].ToString();

            string originalIP = string.Empty;
            string remoteIP = string.Empty;
            originalIP = httpRequest.ServerVariables["HTTP_X_FORWARDED_FOR"]; //original IP will be updated by Proxy/Load Balancer.
            remoteIP = httpRequest.ServerVariables["REMOTE_ADDR"]; //Proxy/Load Balancer IP or original IP if no proxy was used
            if (originalIP != null && originalIP.Trim().Length > 0)
            {
                return originalIP + "(" + remoteIP + ")"; //Lets return both the IPs.
            }
            return remoteIP;
            //return myIP;
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
                        //userADID = userADEntry.Guid;
                        Session["LoggedUser2"] = userADEntry.Guid;
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