<%@ Page Title="Seller Order Report" Language="C#" MasterPageFile="~/mTop.master" AutoEventWireup="true" CodeBehind="SellerReport.aspx.cs" Inherits="ECX.PreTradeRegistration.UI.Reports.SellerReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/main.js?V=1" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dtSellOrderReport').DataTable({
                responsive: {
                    details: {
                        display: $.fn.dataTable.Responsive.display.childRowImmediate,
                        type: 'none',
                        target: ''
                    }
                }
            });
            $('.dataTables_length').addClass('bs-select');
        });
    </script>
    <div class="ml-4 mt-3">
        <div class="row ml-4 mt-3">
            <h3>Sell Order Report</h3>
        </div>
        <div class="row ml-4 mt-2">
            <div class="form-group form-inline ">
                <label for="txtfrom" class="mr-2">From</label>
                <asp:TextBox runat="server" ID="txtFrom" ValidationGroup="g1" CssClass="form-control form-control-sm"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" BehaviorID="txtFrom_CalendarExtender" TargetControlID="txtFrom"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFrom" ErrorMessage="Please select from date" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </div>
            <div class="form-group form-inline">
                <label for="txtTo" class="mr-2">To</label>
                <asp:TextBox runat="server" ID="txtTo" ValidationGroup="g1" CssClass="form-control form-control-sm"></asp:TextBox>
                <ajaxToolkit:CalendarExtender ID="txtTo_CalendarExtender" runat="server" BehaviorID="txtTo_CalendarExtender" TargetControlID="txtTo"></ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtTo" ErrorMessage="Please select to date" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
            </div>
            <div class="form-group form-inline">
                <div>
                    <asp:LinkButton ID="btnShow" runat="server" ValidationGroup="g1" OnClick="btnShow_Click" class="btn btn-secondary  btn-sm" ToolTip="Search"><span class="fa fa-search" ></span></asp:LinkButton>
                    <asp:LinkButton ID="lbtnExport" runat="server" OnClick="lbtnExport_Click" class="btn btn-secondary  btn-sm" ToolTip="Export"><span class="fas fa-file-export" >&nbsp; Export</span></asp:LinkButton>
                </div>
            </div>
        </div>
        <div class="row col-12 ml-2">
            <table id="dtSellOrderReport" class="display nowrap" style="width: 100%" cellspacing="0">
                <thead style="background-color: #88AB2D; border-color: #88AB2D; color: White">
                    <tr>
                        <th>Owner ID</th>
                        <th>Owner Name</th>
                        <th>Rep ID</th>
                        <th>Trade Date</th>
                        <th>Commodity Type</th>
                        <th>Symbol</th>
                        <th>Warehouse</th>
                        <th>Production Year</th>
                        <th>Quantity</th>
                        <th>Status</th>
                    </tr>
                </thead>
                <tbody>
                    <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
