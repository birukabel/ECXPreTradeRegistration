<%@ Page Title="Seller Report" Language="C#" MasterPageFile="~/mTop.Master" AutoEventWireup="true" CodeBehind="SellOrderReport.aspx.cs" Inherits="ECX.PreTradeRegistration.BackOffice.SellOrderReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="ml-4 mt-3 mr-3">
        <div class="ml-2 mt-3 row">
            <h3>Sell Order Report</h3>
        </div>
         <div class="ml-2 mt-3 row">
             <asp:Label ID="lblMsg" CssClass="col-form-label" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label>
        </div>
        <div class="form-inline">
            <div class="form-group mb-2">
                <label class="col-form-label">Member ID:</label>
            </div>
            <div class="form-group mx-sm-3 mb-2">
                <asp:TextBox ID="txtMemberId" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="Please select Date From" ForeColor="Red">*</asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" BehaviorID="RequiredFieldValidator1_ValidatorCalloutExtender" TargetControlID="RequiredFieldValidator1">
                </ajaxToolkit:ValidatorCalloutExtender>
            </div>
            <div class="form-group mx-sm-3 mb-2">
                <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" BehaviorID="txtDateFrom_CalendarExtender" TargetControlID="txtDateFrom"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtDateFrom" ErrorMessage="Please select Date To" ForeColor="Red">*</asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" BehaviorID="RequiredFieldValidator2_ValidatorCalloutExtender" TargetControlID="RequiredFieldValidator3">
                </ajaxToolkit:ValidatorCalloutExtender>
            </div>
            <div class="form-group mb-2">
                <label class="col-form-label">Date To:</label>
            </div>
            <div class="form-group mx-sm-3 mb-2">
                <asp:TextBox ID="txtDateTo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" BehaviorID="txtDateTo_CalendarExtender" TargetControlID="txtDateTo"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDateTo" ErrorMessage="Please select Date From" ForeColor="Red">*</asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" BehaviorID="RequiredFieldValidator1_ValidatorCalloutExtender" TargetControlID="RequiredFieldValidator1">
                </ajaxToolkit:ValidatorCalloutExtender>
            </div>
           </div>
        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="btn btn-secondary  btn-sm  mb-2" ToolTip="Search"></asp:Button>
      <asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" class="btn btn-secondary  btn-sm  mb-2" ToolTip="Export"></asp:Button>
         
          <%-- <asp:LinkButton ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="btn btn-secondary  btn-sm  mb-2" ToolTip="Search" ><span class="fa fa-search" >&nbsp; Search</span></asp:LinkButton>&nbsp;
           <asp:LinkButton ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" class="btn btn-secondary  btn-sm  mb-2" ToolTip="Export"><span class="fas fa-file-export" >&nbsp; Export</span></asp:LinkButton>
       --%>
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
            CssClass="table grid-condensed table-hover table table-responsive table-striped table-bordered dataTable">
            <Columns>
                <asp:BoundField DataField="TradeDate" HeaderText="Trade Date" />
                <asp:BoundField DataField="MemberIDNO" HeaderText="Member Id" />
                <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                <asp:BoundField DataField="OwnerIDNO" HeaderText="Owner Id" />
                <asp:BoundField DataField="OwnerName" HeaderText="Owner Name" />
                <asp:BoundField DataField="RepIDNO" HeaderText="Rep Id" />
                <asp:BoundField DataField="RepName" HeaderText="Rep Name" />
                <asp:BoundField DataField="CommodityType" HeaderText="Commodity Type" />
                <asp:BoundField DataField="Symbol" HeaderText="Symbol" />
                <asp:BoundField DataField="WarehouseName" HeaderText="WarehouseName" />
                <asp:BoundField DataField="ProductionYear" HeaderText="Production Year" />
                <asp:BoundField DataField="Quantity" HeaderText="Sell Order Quantity" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
