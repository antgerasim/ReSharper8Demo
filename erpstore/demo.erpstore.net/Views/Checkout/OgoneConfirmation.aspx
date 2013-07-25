<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2Columns.Master"  Inherits="System.Web.Mvc.ViewPage<OrderCart>" %>

<asp:Content ID="indexHead" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Confirmation de commande et reglement via Société Générale</title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="bloc">
         <h3>Ma commande</h3>
    </div>
	<div class="bloc chemin">
         <b>récapitulatif</b>
    </div>

<div class="mbilling">
	<h4><span>commande</span></h4>
	<ul class="nav mbilling_etapes">
		<li class="menubilling_etape" id="mbilling_etape1"><img src="/content/images/commande/commande_1off.jpg" alt="identification"/><span><b>1</b> identification</span></li>
		<li class="menubilling_etape" id="mbilling_etape2"><img src="/content/images/commande/commande_2off.jpg" alt="adresse"/><span><b>2</b> adresse</span></li>
		<li class="menubilling_etape" id="mbilling_etape3"><img src="/content/images/commande/commande_3off.jpg" alt="récapitulatif"/><span><b>3</b> paiement</span></li>
		<li class="menubilling_etape mbilling_etape_on" id="mbilling_etape4"><img src="/content/images/commande/commande_4on.jpg" alt="paiement"/><span><b>4</b> récapitulatif</span></li>
	</ul>
</div>

<br class="clear"/> 

<!-- gestion des erreurs //-->
<% if (!ViewData.ModelState.IsValid) { %>
<div class="notes errors">
    <strong><img src="/content/images/icon_error.png" alt=""/> Attention !</strong>
    <% foreach (var item in ViewData.ModelState.GetAllErrors()) { %>
    <p class="error"> 
        <span><%=item%></span>
    </p>
    <% } %>
</div>
<% } %>
<!-- fin gestion des erreurs //-->

<div class="bloc-list cards" id="productlist">
    
    <ul class="nav entete ui-tabs-nav">
            <li id="entete-20">photo</li>
            <li id="entete-25">produit</li>
            <li id="entete-10"><abbr title="quantité">Qté</abbr></li>
            <li id="entete-10">U.V</li>
            <li id="entete-20">total ht</li> 
            <li id="entete-10">&nbsp;</li>
    </ul>

	<% foreach (var item in Model.Items){ %>
     <div class="prod prodligne<%=Model.Items.ColumnIndex(item, 2) %>">
        <div class="prodcontent">
        
            <div class="prod20 sprodcontent">
                 <a href="<%=Url.Href(item.Product)%>" title="<%=Html.Encode(item.Product.Title)%>">
                    <img src="<% =Url.ImageSrc(item.Product, 90, "/content/images/prodvignette90.gif")%>" alt="<%=Html.Encode(item.Product.Title)%>" />
                  </a>
            </div>        
	        <div class="prod25 sprodcontent">
                <h3>
                    <% =Html.Encode(item.Product.Title) %>
                	<br />
                	<span>REF : <%=item.Product.Code%></span> 
                </h3>
                <div class="stock" id="psi-<%=item.Product.Code %>"></div>
			</div>
			<div class="prod10 sprodcontent form_element_radio">
				<p><% =Html.TextBox("quantity", item.Quantity, new { size = 3 , name="quantity" })%></p>
			</div>
            <div class="prod10 sprodcontent">
                 <p><small><%=item.SaleUnitValue%></small></p>
            </div>
			<div class="prod20 sprodcontent">
				<p class="prix actuel corner"><b><% =item.Total.ToCurrency() %></b></p>
                        <% if (item.RecyclePrice.Value != 0) { %>
                <p class="prodecotaxe">
                   	<img src="/content/images/puce_ecotaxe.png" alt="" align="absmiddle" /> eco taxe : <%=item.RecycleTotal.ToCurrency()%>
                </p>
                        <%}%>
			</div>
            <div class="prod10 sprodcontent">
                <p>&nbsp;</p>
			</div>
            
		</div>
     </div>
	<% } %>
   
</div>

<div class="bloc-list cards" id="productlist">

    <ul class="nav entete ui-tabs-nav">
            <li id="entete-20">&nbsp;</li>
            <li id="entete-25">&nbsp;</li>
            <li id="entete-10">&nbsp;</li>
            <li id="entete-10">&nbsp;</li> 
            <li id="entete-25">total de votre panier</li>
            <li id="entete-5">&nbsp;</li>
    </ul> 
           
    <div class="prod prod_bottom">
        <div class="prodcontent">
            <div class="prod20 sprodcontent">
				&nbsp;
            </div>        
	        <div class="prod25 sprodcontent">
				<p>&nbsp;</p>                
			</div>
			<div class="prod10 sprodcontent">
				<p>&nbsp;</p>                
			</div>  
			<div class="prod10 sprodcontent">
				<p>&nbsp;</p>                
                <p>&nbsp;</p>
			</div>
			<div class="prod25 sprodcontent">
                <table class="prixtotal">
                	<tr>
                    	<td class="td1">&nbsp;</td>
                        <td class="td2">&nbsp;</td>
                        <td class="td3">&nbsp;</td>
                        <td class="td4 prix prixtotalht"><%=Model.Total.ToCurrency() %></td>
                        <td class="td5"><small>HT</small></td>
                    </tr>
					<% if (Model.RecycleTotal > 0) { %>
                    <tr>
                    	<td class="td1">&nbsp;</td>
                        <td class="td2">&nbsp;</td>
                        <td class="td3">eco taxe : </td>
                        <td class="td4 prodecotaxe"><%=Model.RecycleTotal %></td>
                        <td class="td5">€</td>
                    </tr>
					<%}%>
                    <tr>
                        <td class="tdcol5 prodfraisdeport" colspan="5">frais de port : <%=(Model.ShippingFee.Total > 0) ? Model.ShippingFee.Total.ToCurrency() + "<small>HT</small>" : "Franco" %></td>
                    </tr>
                    <tr>
                    	<td class="td1">&nbsp;</td>
                        <td class="td2">&nbsp;</td>
                        <td class="td3">TVA : </td>
                        <td class="td4 prix prixtva"><%=Model.GrandTaxTotal.ToCurrency() %></td>
                        <td class="td5">&nbsp;</td>
                    </tr>
                    <tr>
                    	<td class="td1">&nbsp;</td>
                        <td class="td2">&nbsp;</td>
                        <td class="td3">&nbsp;</td>
                        <td class="td4 prix prixtotalttc"><%=Model.GrandTotalWithTax.ToCurrency() %></td>
                        <td class="td5"><small>TTC</small></td>
                    </tr>                      
                </table>
			</div> 
            <div class="prod10 sprodcontent">
                &nbsp;
			</div> 
        </div>
     </div>   

</div>

	<fieldset class="form">
    <legend class="corner">Informations de commande</legend>
    
    <div class="scontent2 form_elements">
    <h4>Adresse de facturation :</h4>
    	<div class="form_adresse corner">
            <p><%=Model.BillingAddress.RecipientName%></p>
            <p><%=Model.BillingAddress.Street %></p>
            <p><%=Model.BillingAddress.ZipCode%>&nbsp;<%=Model.BillingAddress.City%></p>
            <p><%=Model.BillingAddress.Country.LocalizedName%></p>	
        </div>
    </div>
    
    <div class="scontent2 form_elements">
    <h4>Adresse de livraison :</h4>
    	<div class="form_adresse corner">
            <p><%=Model.DeliveryAddress.RecipientName %></p>
            <p><%=Model.DeliveryAddress.Street %></p>
            <p><%=Model.DeliveryAddress.ZipCode%>&nbsp;<%=Model.DeliveryAddress.City%></p>
            <p><%=Model.DeliveryAddress.Country.LocalizedName%></p>	
        </div>
    </div>
    <br class="clear"/> 
    <div class="form_elements">
    <h4>Mode de règlement :</h4>
    	<div class="form_adresse corner  form_adresse_ok">
            <p>Ogone</p>
        </div>
    <h4>Condition générale de vente</h4>
    	<div class="separateur">&nbsp;</div>
    	<div class="corner">
            <p>
			En utilisant le systeme de paiement Ogone, vous acceptez les <%=Html.TermsAndConditionsLink("conditions générales de vente")%>
            </p>
        </div> 
    </div>
	</fieldset>       

<br class="clear"/>

<div class="go_commande">
	<p>
		<a class="go_commande_no" href="<%=Url.CheckOutPaymentHref()%>" >Retour</a>

		<p>
		Veuillez selectionner ci-dessous le type de Carte Bleue que vous voulez utiliser, en cliquant sur celle-ci vous allez alors
		etre redirigé vers le centre de paiement sécurisé de Ogone.
		</p>
		<%=ViewData["ogoneForm"]%>
	</p>
</div>

</asp:Content>

<asp:Content ID="LeftContent" ContentPlaceHolderID="LeftContent" runat="server">
<%Html.RenderPartial("~/views/Account/Rightinfos.ascx");%>

<%Html.ShowMenu("RightMenu.ascx");%>
</asp:Content>