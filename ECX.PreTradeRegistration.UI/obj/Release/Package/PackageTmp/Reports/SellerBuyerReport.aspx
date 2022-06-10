<%@ Page Title="" Language="C#" MasterPageFile="~/mTop.master" AutoEventWireup="true" CodeBehind="SellerBuyerReport.aspx.cs" Inherits="ECX.PreTradeRegistration.UI.Reports.SellerBuyerReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/main.js?V=1" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dtSellBuyerReport').DataTable({
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
    <div class="row ml-4 mt-3">
        <h3>Buyer Report</h3>
    </div>
    <div class="row ml-4 mt-2">
        <div class="form-group form-inline ">
            <label for="txtfrom" class="mr-2">Trade Date</label>
            <asp:TextBox runat="server" ID="txtTradeDate" ValidationGroup="g1" CssClass="form-control form-control-sm"></asp:TextBox>
            <ajaxToolkit:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" BehaviorID="txtFrom_CalendarExtender" TargetControlID="txtTradeDate"></ajaxToolkit:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtTradeDate" ErrorMessage="Please select from date" ForeColor="Red" ValidationGroup="g1">*</asp:RequiredFieldValidator>
        </div>
        
        <div class="form-group form-inline">
            <div>
                <asp:LinkButton ID="btnShow" runat="server" ValidationGroup="g1" OnClick="btnSearch_Click" class="btn btn-secondary  btn-sm" ToolTip="Search"><span class="fa fa-search" ></span></asp:LinkButton>
                <asp:LinkButton ID="lbtnExport" runat="server" OnClick="lbtnExport_Click" class="btn btn-secondary  btn-sm" ToolTip="Export"><span class="fas fa-file-export" >&nbsp; Export</span></asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="row col-12 ml-2">
        <table id="dtSellBuyerReport" class="display nowrap" style="width: 100%" cellspacing="0">
            <thead style="background-color: #ffcc00;">
                <tr>
                    <th>Trade Date</th>
                    <th>Symbol</th>
                    <th>Warehouse Name</th>
                    <th>Production Year</th>
                    <th>Aggregate Quantity</th>
                </tr>
            </thead>
            <tbody>
                <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
            </tbody>
        </table>
    </div>
</asp:Content>
