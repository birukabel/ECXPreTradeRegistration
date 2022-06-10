<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/mTop.Master" CodeBehind="SellOrderHistory.aspx.cs" Inherits="ECX.PreTradeRegistration.BackOffice.SellOrderHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function ShowPopup() {
            $("#btnShowPopup").click();
        }

    </script>
      
    <div class="ml-4 mt-3 mr-3">
        <div class="ml-2 mt-3 row">
            <h3>Sell Order History</h3>
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
            <div class="form-group mb-2">
                <label class="col-form-label">Date From:</label>
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
        <asp:GridView ID="gvReport" runat="server" AutoGenerateColumns="False" DataKeyNames="Id" OnSelectedIndexChanged="gvReport_SelectedIndexChanged"
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
                <asp:CommandField HeaderText="Detail" ShowSelectButton="true" />

            </Columns>
        </asp:GridView>
    </div>
    <div class="modal fade" id="detailModal" tabindex="-1" role="dialog" aria-labelledby="DetailModalNewLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Order history</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="font-size: medium;"">
                    <asp:GridView ID="gvDetail" runat="server" AutoGenerateColumns="false"
                        CssClass="table grid-condensed table-hover table table-responsive table-striped table-bordered dataTable">
                        <Columns>
                            <asp:BoundField DataField="Action" HeaderText="Action" />
                            <asp:BoundField DataField="OldValue" HeaderText="Old Value" />
                            <asp:BoundField DataField="NewValue" HeaderText="New Value" />
                            <asp:BoundField DataField="StatusName" HeaderText="Status" />
                            <asp:BoundField DataField="CreatedDate" HeaderText="Changed Date" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="modal-footer">
                    <input type="button" class="btn btn-warning" data-dismiss="modal" value="close" />
                </div>
            </div>
        </div>
    </div>
    <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                data-toggle="modal" data-target="#detailModal">
            </button>   
</asp:Content>
