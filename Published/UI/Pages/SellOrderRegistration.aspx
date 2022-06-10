<%@ Page Title="Sell Order Registration" Language="C#" MasterPageFile="~/mTop.Master" AutoEventWireup="true" CodeBehind="SellOrderRegistration.aspx.cs" Inherits="ECX.PreTradeRegistration.UI.Pages.SellOrderRegistration" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../js/main.js?V=1" type="text/javascript"></script>
    <style type="text/css">
        .pagination .current {
            background: #26B;
            color: #fff;
            border: solid 1px #AAE;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#dtSellOrder').DataTable({
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
        function CancelOrder(Id) {

            $("#dialog-confirm").dialog({
                resizable: false,
                modal: true,
                title: "Confirm cancelation",
                open: function (e) {
                },
                buttons: {
                    Ok: function () { $(this).dialog("close"); CallCancelOrder(Id); },
                    Cancel: function () { $(this).dialog("close"); return; }
                }
            }).html("<span class='ui-icon ui-icon-alert' style='float:left; margin:0 7px 10px 0;'></span><span style='font-size:14px;'>The selected order will be cancceled. <br /><br />Are you sure?</span>");
        }
        function CallCancelOrder() {
            j.ajax({
                type: "post",
                url: "SellOrderRegistration.aspx/",
                data: JSON.stringify(parameter),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                success: function (data) {
                },
                error: function (e) {
                }
            });
        }

    </script>
    <div class="row ml-4 mt-3">
        <h3>Sell Order Registration</h3>
    </div>
    <div class="row ml-2">
        <div class="col-8">
            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <table id="dtSellOrder" class="display nowrap" style="width: 100%" cellspacing="0">
                        <thead style="background-color: #88AB2D; border-color: #88AB2D; color: White">
                            <tr>
                                <th>#</th>
                                <th>Member Id</th>
                                <th>Owner Id</th>
                                <th>Owner Name</th>
                                <th>GRN#</th>
                                <th>Symbol</th>
                                <th>WN</th>
                                <th>PY</th>
                                <th>Qty</th>
                            </tr>
                        </thead>
                        <tbody>
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server" />
                        </tbody>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="Col-4">
            <fieldset class="px-3 my-3 forminput stack-top" style="margin-left: 5px; margin-left: 5px; font-size: smaller; border: 1px solid lightgray; box-shadow: 3px 0px #e6e6e6;">
                <legend>Order Form</legend>
                <div class="form-group row mb-1">
                    <label for="ddlTradeDate" class="col-sm-5 col-form-label">Next Trade Date</label>
                    <div class="col-sm-6">
                      <!--  <asp:TextBox ID="txtNxtTradeDate" Enabled="false" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>-->
                        <asp:DropDownList ID="ddlTradeDate" runat="server"  class="form-control form-control-sm"  DataTextFormatString="{0:MM/dd/yyyy}"></asp:DropDownList>
                    </div>
                </div>
                <div class="form-group row mb-1">
                    <label for="txtOwnerId" class="col-sm-5 col-form-label">Owner Id</label>
                    <div class="col-sm-6">
                        <input id="txtOwnerId" type="text" runat="server" class="form-control form-control-sm" readonly="readonly" />
                        <asp:HiddenField ID="hdOwnerId" runat="server" />
                        <asp:HiddenField ID="hdOwnerName" runat="server" />
                    </div>
                </div>
                <div class="form-group row mb-1">
                    <label for="txtSymbol" class="col-sm-5 col-form-label">Symbol</label>
                    <div class="col-sm-6">
                        <input id="txtSymbol" type="text" runat="server" class="form-control form-control-sm" readonly="readonly" />
                        <asp:HiddenField ID="hdCommodityGrade" runat="server" />
                    </div>
                </div>
                <div class="form-group row mb-1">
                    <label for="txtWarehouse" class="col-sm-5 col-form-label">Warehouse</label>
                    <div class="col-sm-6">
                        <input id="txtWarehouse" type="text" runat="server" class="form-control form-control-sm" readonly="readonly" />
                        <asp:HiddenField ID="hdWarehouse" runat="server" />
                    </div>
                </div>
                <div class="form-group row mb-1">
                    <label for="txtPY" class="col-sm-5 col-form-label">Production Year</label>
                    <div class="col-sm-6">
                        <input id="txtPY" type="text" runat="server" class="form-control form-control-sm" readonly="readonly" />
                    </div>
                </div>
                <div class="form-group row mb-1">
                    <label for="txtAvailable" class="col-sm-5 col-form-label">Available Quantity</label>
                    <div class="col-sm-6">
                        <input id="txtAvailable" type="text" runat="server" class="form-control form-control-sm" readonly="readonly" />
                    </div>
                </div>
                <div class="form-group row mb-1">
                    <label for="txtQuantity" class="col-sm-5 col-form-label">Quantity</label>
                    <div class="col-sm-6">
                        <input id="txtQuantity" type="text" runat="server" class="form-control form-control-sm" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtQuantity" ValidationGroup="g1" ForeColor="Red" ErrorMessage="*">*</asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="mb-md-3">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="g1" CssClass="btn btn-success btn-sm mr-2" data-toggle="modal" OnClick="lnk_Click" CausesValidation="true" />
                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-danger btn-sm" CausesValidation="false" OnClick="btnClear_Click" />
                    <asp:Label ID="lblNotification" runat="server" />
                </div>
            </fieldset>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <div class="table-responsive">
                <asp:GridView ID="gvPreTradeOrders" runat="server" AutoGenerateColumns="False" Width="94%" AllowPaging="True"
                    PageSize="15" OnPageIndexChanging="gvPreTradeOrders_PageIndexChanging" DataKeyNames="Id" CssClass="table grid-condensed table-hover table table-responsive table-striped table-bordered dataTable" OnRowEditing="gvPreTradeOrders_RowEditing" OnRowCancelingEdit="gvPreTradeOrders_RowCancelingEdit"
                    OnRowDeleting="gvPreTradeOrders_RowDeleting" OnRowUpdating="gvPreTradeOrders_RowUpdating">
                    <%--  <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />--%>
                    <Columns>
                        <asp:TemplateField HeaderText="Trade Date" SortExpression="TradeDate">
                            <ItemTemplate>
                                <asp:Label ID="lblTradeDate" runat="server" Text='<%# Bind("TradeDate","{0:MM/dd/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Owner ID" SortExpression="OwnerIDNO">
                            <ItemTemplate>
                                <asp:Label ID="lblOwnerId" runat="server" Text='<%# Bind("OwnerIDNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Owner Name" SortExpression="OwnerName">
                            <ItemTemplate>
                                <asp:Label ID="lblOwnerName" runat="server" Text='<%# Bind("OwnerName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rep  ID" SortExpression="RepIDNO">
                            <ItemTemplate>
                                <asp:Label ID="lblRep" runat="server" Text='<%# Bind("RepIDNO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Symbol" SortExpression="Symbol">
                            <ItemTemplate>
                                <asp:Label ID="lblSymbol" runat="server" Text='<%# Bind("Symbol") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Warehouse" SortExpression="WarehouseName">
                            <ItemTemplate>
                                <asp:Label ID="lblWarehouse" runat="server" Text='<%# Bind("WarehouseName") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PY" SortExpression="ProductionYear">
                            <ItemTemplate>
                                <asp:Label ID="lblPy" runat="server" Text='<%# Bind("ProductionYear") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">
                            <ItemTemplate>
                                <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status" SortExpression="StatusName">
                            <ItemTemplate>
                                <asp:Label ID="lblStatusName" runat="server" Text='<%# Bind("StatusName") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="100px" ShowHeader="False">
                            <EditItemTemplate>
                                <asp:ImageButton runat="server" ID="lnkUpdate" CommandName="Update" ImageUrl="~/Images/right.JPG" />
                                <asp:ImageButton runat="server" ID="lnkCancel" CommandName="Cancel" ImageUrl="~/Images/wrong.JPG" />
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtEdit" runat="server" CausesValidation="False" CommandName="Edit"
                                    ToolTip="Edit" Text="Edit"></asp:LinkButton>
                                <asp:LinkButton ID="lbtnDelete" runat="server" CausesValidation="False"
                                    CommandName="Delete" ToolTip="Cancel" OnClientClick="CancelOrder(this) return false;" Text="Cancel">                                      
                                </asp:LinkButton>
                            </ItemTemplate>

<ItemStyle Width="100px"></ItemStyle>
                        </asp:TemplateField>

                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="lblEmptyData" runat="server" BackColor="White" BorderColor="White"
                            BorderStyle="None" BorderWidth="0px" Text="There is no data with your selection criteria"></asp:Label>
                    </EmptyDataTemplate>
                  
                    <HeaderStyle BackColor="#88AB2D" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div id="dialog-confirm" title="Are you sure you want to cancel the order?">
        <p style="visibility: hidden"><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 10px 0;"></span></p>
    </div>

    <!--modal-->
    <div class="modal fade" id="DetailModalNew" tabindex="-1" role="dialog" aria-labelledby="DetailModalNewLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel">Confirm Order</h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body" style="font-size: medium; font-weight: bold">
                    <div class="form-group">
                        Owner:
                        <asp:Label runat="server" ID="lblOwner"></asp:Label>
                    </div>
                    <div class="form-group">
                        Symbol:
                        <asp:Label runat="server" ID="lblSymbol"></asp:Label>
                    </div>
                    <div class="form-group">
                        Warehouse:
                        <asp:Label runat="server" ID="lblWarehouse"></asp:Label>
                    </div>
                    <div class="form-group">
                        PY:
                        <asp:Label runat="server" ID="lblPY"></asp:Label>
                    </div>
                    <div class="form-group">
                        Quantity:
                        <asp:Label runat="server" ID="lblQuantity"></asp:Label>
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:Button ID="btnYes" runat="server" class="btn btn-success" Text="Yes" OnClick="btnYes_Click" />
                    <input type="button" class="btn btn-warning" data-dismiss="modal" value="No" />

                </div>
            </div>
        </div>
    </div>
</asp:Content>
