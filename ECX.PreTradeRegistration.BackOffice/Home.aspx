<%@ Page Title="" Language="C#" MasterPageFile="~/mTop.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ECX.PreTradeRegistration.BackOffice.Home" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="ml-4 mt-3 mr-3">
        <div class="ml-2 mt-3 row"><h3>Registered vs Traded</h3></div>
        <div class="form-inline">
            <div class="form-group mb-2">
                <label class="col-form-label">Member ID:</label>
            </div>
            <div class="form-group mx-sm-3 mb-2">
                <asp:TextBox ID="txtMemberId" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
            </div>
            <div class="form-group mb-2">
                <label class="col-form-label">Trade Date:</label>
            </div>
            <div class="form-group mx-sm-3 mb-2">
                <asp:TextBox ID="txtTradeDate" runat="server" ValidationGroup="g1" CssClass="form-control form-control-sm"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtTradeDate_CalendarExtender" runat="server" BehaviorID="txtTradeDate_CalendarExtender" TargetControlID="txtTradeDate"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="g1" ControlToValidate="txtTradeDate" ErrorMessage="Please select trade date" ForeColor="Red">*</asp:RequiredFieldValidator>
                <ajaxToolkit:ValidatorCalloutExtender ID="RequiredFieldValidator1_ValidatorCalloutExtender" runat="server" BehaviorID="RequiredFieldValidator1_ValidatorCalloutExtender" TargetControlID="RequiredFieldValidator1">
                </ajaxToolkit:ValidatorCalloutExtender>
            </div>
            <div class="form-group mx-sm-3 mb-2">
                <asp:DropDownList ID="ddlCommodity" class="btn btn-sm btn-outline-secondary" runat="server" />
            </div>
            <asp:LinkButton ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" class="btn btn-secondary  btn-sm mb-2" ToolTip="Search"><span class="fa fa-search" >&nbsp; Search</span></asp:LinkButton>&nbsp;
            <asp:LinkButton ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" class="btn btn-secondary  btn-sm mb-2" ToolTip="Export"><span class="fas fa-file-export" >&nbsp; Export</span></asp:LinkButton>
        </div>


        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="false"
            CssClass="table grid-condensed table-hover table table-responsive table-striped table-bordered dataTable">
            <Columns>
                <asp:BoundField DataField="MemberId" HeaderText="Member Id" />
                <asp:BoundField DataField="MemberName" HeaderText="Member Name" />
                <asp:BoundField DataField="OwnerId" HeaderText="Owner Id" />
                <asp:BoundField DataField="OwnerName" HeaderText="Owner Name" />
                <asp:BoundField DataField="RepId" HeaderText="RepId" />
                <asp:BoundField DataField="SessionName" HeaderText="Session Name" />
                <asp:BoundField DataField="TradeDate" HeaderText="Trade Date" />
                <asp:BoundField DataField="Symbol" HeaderText="Symbol" />
                <asp:BoundField DataField="Warehouse" HeaderText="Warehouse" />
                <asp:BoundField DataField="ProductionYear" HeaderText="Production Year" />
                <asp:BoundField DataField="SellOrderQuantity" HeaderText="Registered Sell Order Quantity" />
                <asp:BoundField DataField="TradeQuantity" HeaderText="Ordered Quantity" />
                <asp:BoundField DataField="AcceptedTradeQuantity" HeaderText="Accepted Trade Quantity" />
                <asp:BoundField DataField="CanceledQuantity" HeaderText="Canceled Quantity" />
                <asp:BoundField DataField="Variance" HeaderText="Variance(%)" />
                <asp:BoundField DataField="Remark" HeaderText="Remark" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

