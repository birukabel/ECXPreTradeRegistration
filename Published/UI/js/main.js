(function($) {

    "use strict";

    $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
        $('#content').toggleClass('active');
    });
    var table = $('#dtSellOrder').dataTable({
        "aLengthMenu": [[2, 4, 8, -1], [2, 4, 8, "All"]],
        "iDisplayLength": 4,
    });
})(jQuery);

function populateForm(whrID) {
  
   
$.ajax({
    type: "POST",
    url: "SellOrderRegistration.aspx/lnkGrn_Click",
    data: JSON.stringify({ whrID: whrID }),
    contentType: "application/json; charset=utf-8",
    dataType: "json",
    success: function (data) {
        $('#ContentPlaceHolder1_txtOwnerId').val(data.d.OwnerIDNO);
        $('#ContentPlaceHolder1_hdOwnerId').val(data.d.OwnerId);
        $('#ContentPlaceHolder1_hdOwnerName').val(data.d.OwnerName);
        $('#ContentPlaceHolder1_txtSymbol').val(data.d.Symbol);
        $('#ContentPlaceHolder1_hdCommodityGrade').val(data.d.CommodityGrade);
        $('#ContentPlaceHolder1_txtWarehouse').val(data.d.Warehousename);
        $('#ContentPlaceHolder1_hdWarehouse').val(data.d.WarehouseId);
        $('#ContentPlaceHolder1_txtPY').val(data.d.ProductionYear);
        $('#ContentPlaceHolder1_txtAvailable').val(data.d.AvailableQuantity);
       
        var qty = data.d.AvailableQuantity;
        if(qty<100)
        {
            $('#ContentPlaceHolder1_txtQuantity').val(data.d.AvailableQuantity);
            $("#ContentPlaceHolder1_txtQuantity").attr('readonly', true);
        }
        //else {
          //  $("#ContentPlaceHolder1_txtQuantity").prop('readonly', false);
        //}
    },
    error: function (xhr, status, e) {
        alert('Error' + xhr.responseText + ' ' + status + ' ' + e);
    },
    complete: function () {
  
    }
});
}