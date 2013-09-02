<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage"  %>
$(document).ready(function()
{
	$("a[id^='addtoquotecart|']").click(function() {
		var qty = $("#addtocartqty").val();
		if (qty == null) {
			qty = 1;
		}
		var productCode = $(this).attr('id').replace('addtoquotecart|', '');
		AddToQuoteCart(productCode, qty);
		return false;
	});
});

function AddToQuoteCart(productCode, qty) {

	$("#dialog").load('<%=Url.RouteERPStoreUrl("ShowAddToQuoteCartPopup", null)%>', { productCode: productCode, quantity: qty }, function(html) {
		$('#dialog').empty();
		$("#dialog").html(html);
		$("#dialog").dialog('open');
		showQuoteCartStatus();
<%--		$.getJSON('<%=Url.RouteERPStoreUrl("AjaxAddToQuoteCart", null)%>', { productCode: productCode, quantity: qty }, function() {
		});
--%>	});
}

function showQuoteCartStatus() {
    $('#quotecartstatus').empty();
    $('#quotecartstatus').load('<%=Url.RouteERPStoreUrl("ShowQuoteCartStatus", null)%>', { viewName: 'status.ascx' }, function(html) {
        $('#quotecartstatus')[0].value = html;
    });
}
