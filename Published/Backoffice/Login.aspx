<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ECX.PreTradeRegistration.BackOffice.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="fontawesome/css/all.min.css" rel="stylesheet" />
    <link href="css/site-main.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <title>Pre Trade Order Registration Backoffice</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="row container-body">
            <div class="container-login">
                <div class="login">
                    <img class="login-form-title" style="margin: auto;" src="Images/log.png" />
                    <span class="login-form-title">Pre Trade Order Registration Backoffice</span>
                    <span class="login-form-title">Login</span>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="fa fa-lg fa-user-alt mr-1"></span>
                        </div>
                        <asp:TextBox CssClass="input" ID="txtUserName" runat="server" placeholder="User Name"></asp:TextBox>
                    </div>
                    <div class="input-group mb-3">
                        <div class="input-group-prepend">
                            <span class="fas fa-lg fa-key mr-1"></span>
                        </div>
                        <asp:TextBox CssClass="input" ID="txtPassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
                    </div>
                    <div class="wrap-login-form-btn mt-4">
                        <div>
                            <asp:Button ID="btnLogin" CssClass="btn btn-success btn-block" runat="server" CausesValidation="true" Text="Login" OnClick="btnLogin_Click" />
                        </div>
                    </div>
                    <asp:Label ID="lblStatus" runat="server" ForeColor="Red" Style="padding-left: 50px;"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
