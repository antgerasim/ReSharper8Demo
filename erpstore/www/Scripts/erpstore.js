function AddToCart(productCode) {

    $("#addtocartdialog h3").text("...");
    $("#addtocartdialog .proddesc b").text("...");

    var cartUrl = null;
    $.getJSON("/Cart/JsAdd", { productCode: productCode }, function(data) {
        $("#cartcount").text(data.status);
        $("#carttotal").text(data.cartTotal + ' €');
        $("#addtocartdialog h3").text(data.title);
        $("#addtocartdialog .proddesc b").text(data.quantity);
        cartUrl = data.cartUrl;
    });

    $("#addtocartdialog").dialog('option', 'buttons', {
        "Voir mon panier": function() {
            document.location = cartUrl;
            $(this).dialog("close");
        },
        "Continuer mes achats": function() {
            $(this).dialog("close");
        }
    });
    $("#addtocartdialog").dialog('open');    

}

function GetProductDisponibility(productCode, elmId) {
    $.getJSON("/Catalog/GetJSProductDisponibility", { productCode: productCode }, function(data) {
    $("#" + elmId).text(data.Disponibility);
    });
}




