function AddToCart(productCode, qty) {

	$("#dialog").load('/cart/showaddtocart', { productCode: productCode, quantity: qty }, function(html) {
		$.getJSON("/cart/JsAddItemWithQuantity", { productCode: productCode, quantity: qty }, function() {
			$('#dialog').empty();
			$("#dialog").html(html);
			$("#dialog").dialog('open');
			showCartStatus();
		});
	});
}

function AddToQuoteCart(productCode, qty) {

	$("#dialog").load('/cart-quote/showaddtocart', { productCode: productCode, quantity: qty }, function(html) {
		$.getJSON("/cart-quote/JsAddItemWithQuantity", { productCode: productCode, quantity: qty }, function() {
			$('#dialog').empty();
			$("#dialog").html(html);
			$("#dialog").dialog('open');
			showQuoteCartStatus();
		});
	});
}

function showQuoteCartStatus() {
    $('#quotecartstatus').empty();
    $('#quotecartstatus').load('/cart-quote/showstatus', { viewName: 'status.ascx' }, function(html) {
        $('#quotecartstatus')[0].value = html;
    });
}

function showCartStatus() {
    $('#cartstatus').empty();
    $('#cartstatus').load('/cart/ShowStatus', { viewName: 'status.ascx' }, function(html) {
        $('#cartstatus')[0].value = html;
    });
}

function GetProductDisponibility(productCode, elmId) {
    $.getJSON("/Catalog/GetJSProductDisponibility", { productCode: productCode }, function(data) {
        $("#" + elmId).text(data.Disponibility);
    });
	return false;
}

$(function() {
	// Tabs
	$('#tabs').tabs();


	// description
	$("#proddesc1").hide();
	//run the currently selected proddesc1
	function runproddesc1() {
		//get proddesc1 type from 
		var selectedproddesc1 = $('#proddesc1Types').val();

		//most proddesc1 types need no options passed by default
		var options = {};
		//check if it's scale, transfer, or size - they need options explicitly set
		if (selectedproddesc1 == 'scale') { options = { percent: 100 }; }
		else if (selectedproddesc1 == 'transfer') { options = { to: ".plusinfos", className: 'ui-proddesc1s-transfer' }; }
		else if (selectedproddesc1 == 'size') { options = { to: { width: 120, height: 105} }; }

		//run the effect
		$("#proddesc1").effect(selectedEffect, options, 500, callback);

		//run the proddesc1
		$("#proddesc1").show(selectedproddesc1, options, 500);
	};

	//callback function to bring a hidden box back
	function callback() {
		setTimeout(function() {
			$("#proddesc1:hidden").removeAttr('style').hide().fadeIn();
		}, 1000);
	};


	//set proddesc1 from select menu value
	$(".plusinfos").click(function() {
		runproddesc1();
		return false;
	});


	//run the currently selected proddesc1
	function offproddesc1() {
		//get proddesc1 type from 
		var selectedproddesc1 = $('#proddesc1Types').val();

		//most proddesc1 types need no options passed by default
		var options = {};
		//check if it's scale, transfer, or size - they need options explicitly set
		if (selectedproddesc1 == 'scale') { options = { percent: 0 }; }
		else if (selectedproddesc1 == 'transfer') { options = { to: "#button", className: 'ui-proddesc1s-transfer' }; }
		else if (selectedproddesc1 == 'size') { options = { to: { width: 200, height: 60} }; }

		//off the proddesc1
		$("#proddesc1").hide(selectedproddesc1, options, 500);
	};

	//set proddesc1 from select menu value
	$("#proddesc1").click(function() {
		offproddesc1();
		return false;
	});

	/*
	$(".open_card").click(function() {
	var productCode = $(this).attr('id').replace('addtocart|', '');
	AddToCart(productCode);
	return false;
	});

	$(".quotecart").click(function() {
	var productCode = $(this).attr('id').replace('addtoquotecart|', '');
	AddToQuoteCart(productCode);
	return false;
	});
	*/

	$("a[id^='addtocart|']").click(function() {
		var qty = $("#addtocartqty").val();
		if (qty == null) {
			qty = 1;
		}
		var productCode = $(this).attr('id').replace('addtocart|', '');
		AddToCart(productCode, qty);
		return false;
	});

	$("a[id^='addtoquotecart|']").click(function() {
		var qty = $("#addtocartqty").val();
		if (qty == null) {
			qty = 1;
		}
		var productCode = $(this).attr('id').replace('addtoquotecart|', '');
		AddToQuoteCart(productCode, qty);
		return false;
	});

	var psiList = $("div[id^='psi-']");
	$.each(psiList, function() {
		var productCode = this.id.replace('psi-', '');
		var divId = '#' + this.id.replace('.','\\.').replace('/','\\/');
		$(divId).load("/Catalog/ShowProductStockInfo", { productCode: productCode, viewName: 'productstockinfo.ascx' }, function(data) {
			$(divId).html(data);
		});
	});



});
	

$(document).ready(function(){
    $(".nav li").each(function(){
      $(this).mouseover(function(){
      $(this).children("ul").slideDown("fast");
      if($.browser.msie) { var hauteur = $(this).width();
	  $(this).children("ul").css({marginLeft:"-"+hauteur+"px"});   }
      $(this).prev().children("ul").fadeOut("fast");
      $(this).siblings().children("ul").fadeOut("fast");
      });
      });
		$("html").click(function(){
		$(".nav li ul").fadeOut("fast");
	});

	$("#dialog").dialog({
		show: 'slide',
		hide : 'slide',
		width: 764,
		autoOpen: false,
		bgiframe: true
	});
});

$(document).ready(function(){
    $("#headerinfos div .connect").click(function(){
		$("#headerinfos div #showaccountstatus").slideDown("fast");
    });
    $("#headerinfos div .btclose").click(function(){
		$("#headerinfos div #showaccountstatus").fadeOut("fast");
    });
});
