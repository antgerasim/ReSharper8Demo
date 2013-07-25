<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<CartItemNeeded>" %>
<a name="addtocart" class="addtocart">&nbsp;</a>
             <table class="nav cols">
            
                <thead>
                    <tr class="entete-cols">
                        <th class="col col-20 col1 colc">Produit</th>
                        <th class="col col-40 col2 colg">&nbsp;</th>
                        <th class="col col-15 col3 colc">Quantité.</th>
                    </tr>
                </thead>

                <tbody>
                 <tr class="prodligne">
                    <td class="col col-20 col1 colc">
                            <img src="<%=Url.ImageSrc(Model.Product, 140,140, "/content/images/vignette140.png") %>" alt="<%=Model.Product.Title%>" />
                    </td>        
                    <td class="col col-30 col2 colg">
                            <strong><%=Model.Product.Title%></strong>
                            <br />
                            <small><span>REF : <%=Model.Product.Code%></span></small>
                    </td>
                    <td class="col col-10 col3 colc">
                         <p>
							<%=Model.Quantity%>
						</p>
                    </td>
              </tr>
         </tbody>
        
   </table>
   <table class="go_commande cols">
       <tr>
            <td class="col col-33 col1"> <span><a class="go_commande_no" href="#" onclick="javascript:return closeDialog();">Continuer ma consultation</a></span></td>
            <td class="col col-33 col2"> <span>&nbsp;</span></td>
			<td class="col col-33 col3"> <span><a href="<%=Url.QuoteCartHref()%>">Voir mon devis</a></span></td>
        </tr>
    </table> 

<script type="text/javascript">
	$("#ui-dialog-title-dialog").empty();
	$("#ui-dialog-title-dialog").html('Vous venez d\'ajouter au devis le produit suivant :');
	$('.ui-icon-closethick').html('[x]');

	function closeDialog() {
		$('#dialog').dialog('close');
		return false;
	}
</script>
