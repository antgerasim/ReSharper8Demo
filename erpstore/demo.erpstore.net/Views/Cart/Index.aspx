<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<OrderCart>" %>

<asp:Content ID="indexHead" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Votre panier</title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
        <div class="mbilling">
			<h4><span>commande</span></h4>
            <ul class="nav mbilling_etapes">
             	<li class="menubilling_etape mbilling_etape_on" id="mbilling_etape1"><img src="/content/images/commande/commande_1on.jpg" alt="identification"/><span><b>1</b> Panier</span></li>
                <li class="menubilling_etape" id="mbilling_etape2"><img src="/content/images/commande/commande_1off.jpg" alt="identification"/><span><b>2</b> Adresses</span></li>
                <li class="menubilling_etape" id="mbilling_etape3"><img src="/content/images/commande/commande_1off.jpg" alt="adresse"/><span><b>3</b> Configuration</span></li>
                <li class="menubilling_etape" id="mbilling_etape4"><img src="/content/images/commande/commande_1off.jpg" alt="récapitulatif"/><span><b>4</b> Paiement</span></li>
                <li class="menubilling_etape" id="mbilling_etape5"><img src="/content/images/commande/commande_1off.jpg" alt="paiement"/><span><b>5</b> Récapitulatif</span></li>
            </ul>
        </div> 
        <br class="clear"/>
          
         <% Html.BeginCartForm("cartForm"); %>
             <table class="nav cols">
            
                <thead>
                    <tr class="entete-cols">
                        <th class="col col-20 col1">Produit</th>
                        <th class="col col-25 col2">&nbsp;</th>
                        <th class="col col-10 col3">Dispo.</th>
                        <th class="col col-15 col4">Quantité.</th>
                        <th class="col col-12 col5">Prix HT</th> 
                        <th class="col col-15 col6">Total HT</th> 
                        <th class="col col-3 col7"><abbr title="Supprimer">Suppr</abbr></th>
                    </tr>
                </thead>
                
              	<tfoot>
                    <tr class="entete-cols">
                        <th class="col col-55 col1" colspan="3">&nbsp;</th>
                        <th class="col col-42 col5" colspan="3">Total</th> 
                        <th class="col col-3 col6">Tout supprimer</th>
                    </tr>
                    <tr class="prod">
                        <td class="col col-55 col1" colspan="3">
                        
                        	<table class="cols codepromo">
                            	<thead>
                                    <tr>
                                        <th>
                                        Si vous disposez d'un code promotionnel, saisissez-le dans la boite de texte ci-dessous puis cliquez sur le bouton Recalculer
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
                                        <input name="couponcode" type="text" Value="valeur du coupon" />
                                        </td>
                                    </tr>
                                </tbody>
                            </table> 
                        
                        </td>        
                        <td class="col col-42 col5" colspan="3">
                        
                        <table class="prixtotal cols">
                            <tr>
                                <td class=" col col1">Total HT :</td>
                                <td class=" col col2 prixtotalht"><%=Model.Total.ToCurrency() %></td>
                            </tr>
                            <% if (Model.RecycleTotal > 0) { %>
                            <tr>
                                <td class=" col col1">Eco Taxe : </td>
                                <td class=" col col2 prodecotaxe"><%=Model.RecycleTotal.ToCurrency() %></td>
                            </tr>
                            <%}%>
                            <tr>
								<% if (Model.ShippingFee.Total > 0) { %>
									<td class="col col1">Frais de Port HT :<br /> 
														(Gratuit à partir de <b><%=Model.FreeOfShippingAmount.ToCurrency() %></b>)<br /> 
									</td>
                            	<% } else { %> 
                            		<td class="col col1">Frais de Port HT : </td>
								<% } %>
                                <td class="col col2 prodfraisdeport"><%=(Model.ShippingFee.Total > 0) ? Model.ShippingFee.Total.ToCurrency() + "" : "Franco" %></td>
                            </tr>
                            <tr>
                                <td class=" col col1">Total TVA : </td>
                                <td class=" col col2 prixtva"><%=Model.GrandTaxTotal.ToCurrency() %></td>
                            </tr>
                            <tr>
                                <td class=" col col1">Total TTC :</td>
                                <td class=" col col2 prixtotalttc"><%=Model.GrandTotalWithTax.ToCurrency() %></td>
                            </tr> 
                        </table>
                        
                        </td>
                         
                        <td class="col col-3 col6">
                            <a href="<%=Url.ClearCartHref()%>" title="tout supprimer"><img src="/content/images/poubelle_all.png" alt="tout supprimer"/></a>
                        </td> 
                    </tr>
                 </tfoot> 
 
                <tbody>
				<% foreach (var item in Model.Items){ %>
                 <tr class="prodligne<%=Model.Items.ColumnIndex(item, 2) %>">
                    <td class="col col-20 col1">
                         <a href="<%=Url.Href(item.Product)%>" title="<%=Html.Encode(item.Product.Title)%>">
                            <img src="<% =Url.ImageSrc(item.Product, 140,140, "/content/images/vignette140.png")%>" alt="<%=Html.Encode(item.Product.Title)%>" />
                          </a>
                    </td>        
                    <td class="col col-25 col2 colg">
                            <strong><% =Html.Encode(item.Product.Title) %></strong>
                            <br />
                            <small><span>REF : <%=item.Product.Code%></span></small>
                    </td>
                    <td class="col col-10 col3">
                        <div id="psi-<%=item.Product.Code %>"></div>
                    </td>
                    <td class="col col-15 col4 form_element_radio">
                         <div class="modif_quantite">
								<a href="#" id="decButton|<%=Model.Items.IndexOf(item) %>|<%=item.Product.Packaging.Value %>"><img src="/content/images/icos/ico_moins.png" alt="moins" /></a>
								<% =Html.TextBox("quantity", item.Quantity, new { size = 3 , name="quantity", id= string.Format("qty{0}", Model.Items.IndexOf(item)) })%>
								<a href="#" id="incButton|<%=Model.Items.IndexOf(item) %>|<%=item.Product.Packaging.Value %>"><img src="/content/images/icos/ico_plus.png" alt="plus" /></a>
						</div>
                    </td>
                    <td class="col col-12 col5 cold">
                        <p class="prix actuel corner"><b><% =item.SalePrice.Value.ToCurrency() %></b></p>
                    </td>
                    <td class="col col-15 col6 cold">
                        <p class="prix actuel corner"><b><% =item.Total.ToCurrency() %></b></p>
                        <% if (item.RecyclePrice.Value != 0) { %>
                        <p class="prodecotaxe">
                            <img src="/content/images/puce_ecotaxe.png" alt="" align="absmiddle" /> eco taxe : <%=item.RecycleTotal.ToCurrency()%>
                        </p>
                        <%}%>
                    </td>
                    <td class="col col-3 col7">
                        <p><a href="<%=Url.DeleteCartItemHref(Model.Items.IndexOf(item)) %>" title="supprimer"><img src="/content/images/poubelle.png" alt="supprimer"/></a></p>
                    </td>
              </tr>
            <% } %>
        </tbody>
        
   </table> 
        
   <table class="go_commande cols">
       <tr>
            <td class="col col-33 col1"> <span><a class="go_commande_no" href='<%=Model.LastPage%>'>Je continue mes achats</a></span></td>
            <td class="col col-33 col2"> <span><input type="submit" value="Recalculer" class="recalculer" /></span></td>
            <% if (Model.Total >= Model.MinimalOrderAmount) { %>
				<td class="col col-33 col3"> <span><% =Html.CheckoutLink()%></span></td>
            <% } else {%>
				<td class="col col-33 col3"> <span>Le minimum de commande est de <%=Model.MinimalOrderAmount.ToCurrency()%></span></td>
            <% } %>
        </tr>
    </table> 
     

   
        <% Html.EndForm(); %>
        
       <table class="cols">
            <tr>
                <td class="col col-50">
				    <%Html.ShowCurrentCartList("Cartlist.ascx");%>
                </td>
                <td class="col col-50">
					<%Html.RenderPartial("~/views/Shared/RightMenu2.ascx");%>
                </td>
            </tr>
       </table>
	</div>
</div>

<script type="text/javascript">
	$(document).ready(function() {

		var incList = $("a[id^='incButton|']");
		var decList = $("a[id^='decButton|']");
		$.each(incList, function() {
			$(this).click(function() {
				var parts = this.id.split('|');
				var packaging = parts[2];
				var qtytbid = '#qty' + parts[1];
				var qtytb = $(qtytbid);
				var qty = parseInt(qtytb.val());
				qty += parseInt(packaging);
				$(qtytb).val(qty);

				$('#cartForm').submit();
				return false;
			});
		});

		$.each(decList, function() {
			$(this).click(function() {
				var parts = this.id.split('|');
				var packaging = parts[2];
				var qtytbid = '#qty' + parts[1];
				var qtytb = $(qtytbid);
				var qty = parseInt(qtytb.val());
				qty -= parseInt(packaging);
				$(qtytb).val(Math.max(1, qty));
				$('#cartForm').submit();
				return false;
			});
		});

	});

</script>
</asp:Content>
