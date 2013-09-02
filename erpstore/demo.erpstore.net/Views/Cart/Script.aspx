<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage"  %>
$(document).ready(function()
{
	$("a[id^='addtocart|']").click(function() {
		var qty = $("#addtocartqty").val();
		if (qty == null) {
			qty = 1;
		}
		var productCode = $(this).attr('id').replace('addtocart|', '');
		AddToCart(productCode, qty);
		return false;
	});
});

function AddToCart(productCode, qty) {

	$("#dialog").load('<%=Url.RouteERPStoreUrl("ShowAddToCartPopup", null)%>', { productCode: productCode, quantity: qty }, function(html) {
		$('#dialog').empty();
		$("#dialog").html(html);
		$("#dialog").dialog('open');
		showCartStatus();
<%--		$.getJSON('<%=Url.RouteLocalizedUrl("AjaxAddToCart", null)%>', { productCode: productCode, quantity: qty }, function() {
		});
--%>	});
}

function showCartStatus() {
    $('#cartstatus').empty();
    $('#cartstatus').load('<%=Url.RouteERPStoreUrl("ShowCartStatus", null)%>', { viewName: 'status.ascx' }, function(html) {
        $('#cartstatus')[0].value = html;
    });
}
