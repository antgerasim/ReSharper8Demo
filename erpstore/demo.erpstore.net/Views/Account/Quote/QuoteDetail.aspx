<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Quote>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Devis N°<%=Model.Code%> (ref : <%=Model.CustomerDocumentReference %>)</title>
    <script src="/scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
		
      	<div class="convert_devis">
             <a href="#" class="convertToOrder">Accepter ce Devis</a>
        </div>
		<% if (Model.Status == QuoteStatus.Waiting) { %>
        <% } %>
       <%Html.RenderPartial("~/views/account/managemenu.ascx", Model.User);%>
        <% if (User.Identity.IsAuthenticated) { %>
        <%Html.ShowWorkflow("~/views/account/workflow.ascx", Model);%>
        <% } %>    
        <% if (Model.Vendor != null) { %>    
        <h2 class="titleh2"><span>Votre chargé d'affaire</span></h2>
        <div class="bloc texte">
            <div class="corner bloc_type_liste">
                <form method="get" action="<%=Request.Url.LocalPath%>" id="formFilter" >
                <ul>
                    <li>
                    <%=Model.Vendor.FullName %>
                    </li>
                    <li class="vendor">
                    <img src='<%=Url.ImageSrc(Model.Vendor, 96, "/content/images/vendor.png") %>' alt='photo :<%=Model.Vendor.FullName%>' />
                    </li>
                    <li>Email : <a href="mailto:<%=Model.Vendor.Email%>"><%=Model.Vendor.Email%></a></li>
                    <li>Tel : <%=Model.Vendor.PhoneNumber %></li>
                    <li>Mob : <%=Model.Vendor.MobileNumber %></li>
                </ul>
                </form>
             </div>
         </div> 
          <% } %>    
    </div>
    
	<div id="sgrid32">
          <h2 class="titleh2">
              <span>
                  Votre compte personnalisé
              </span>
          </h2>
        <div class="chemin">
			<span><a href="<%=Url.HomeHref() %>">accueil</a></span>
			<span><a href="<%=Url.AccountHref()%>">Tableau de bord</a></span> 
			<span><a href="<%=Url.QuoteListHref() %>">Les devis</a></span> 
			<b>Devis N°<%=Model.Code%> du <%=string.Format("{0:dddd dd MMMM yyyy}", Model.CreationDate) %></b>
        </div> 

	<div class="ssgrid321">
    
	<table class="cols">
        <tbody>
        	<tr>
            	<td class="col col-50 col2">
                    <ul>
                        <li>Référence : <span><%=Model.CustomerDocumentReference%></span></li>
                        <li>Status : <span><strong><%=Model.Status.ToLocalizedName() %></strong></span></li>
                        <li>Mode de reglement : <span><%=Model.PaymentModeName%></span></li>
                    </ul>                  
                </td>
                <td class="col col-50 col2">
                    <ul>
                        <li>Livraison partielle : <span><%=Model.AllowPartialDelivery ? "Acceptée" : "Refusée"%></span></li>
                        <li><a href="<%=Url.DownloadDocumentHref(SaleDocumentType.Quote, Model.Id, Model.Code)%>">Téléchargez le devis au format PDF</a></li>
	                    <li>Adresse de facturation :</li>
                    </ul>
                </td>
            </tr>
         </tbody>
 </table>
 <br/>
 <table class="cols">
 	<tbody>
            <tr>
            	<td class="col col-50 col1">
                    <% if (Model.ShippingAddress != null) { %>
                    <div style="text-align:left; padding:1em;">
                        Adresse de livraison : <span><%Html.RenderPartial("address", Model.ShippingAddress); %></span>
                    </div>
                    <% } %>   
                </td>
                <td class="col col-50 col1">
                <div style="text-align:left; padding:1em;">
					Adresse de facturation : <span><%Html.RenderPartial("address", Model.BillingAddress);%></span>
                </div>                      
                </td class="col col-50 col1">
            </tr>
        </tbody>
    </table>
	<br/>

        <table class="nav cols">
            
                <thead>
                    <tr class="entete-cols">
                        <th class="col col-5 col1">N°</th>
                        <th class="col col-20 col2">Photo</th>
                        <th class="col col-25 col3">Produit</th>
                        <th class="col col-10 col4"><span title="Quantité requise">Qté.</span></th>
                        <%if (Model.Status.IsWaiting()) { %>
                        <th class="col col-10 col5"><span title="Disponibilité du stock en temps réel">Dispo.</span></th>
                        <th class="col col-15 col6">Prix HT</th> 
                        <th class="col col-15 col7">Total HT</th>  
                        <% } else { %>
                        <th class="col col-20 col5">Prix HT</th> 
                        <th class="col col-20 col6">Total HT</th>  
                        <% } %>
                    </tr>
                </thead>
                
              	<tfoot>
                    <tr class="entete-cols">
						<%if (Model.Status.IsWaiting())	{ %>
						<th class="col col-70 col4" colspan="5">&nbsp;</th>
						<% } else { %>
                        <th class="col col-70 col4" colspan="4">&nbsp;</th>
                        <% } %>
                        <th class="col col-30 col5" colspan="2">Total</th> 
                    </tr>
                    <tr class="prod">
						<%if (Model.Status.IsWaiting())	{ %>
                        <td class="col col-70 col3" colspan="5">
                        <% } else { %>
                        <td class="col col-70 col3" colspan="4">
                        <% } %>
                        	<table class="cols message">
                            	<thead>
                                    <tr>
                                        <th>
                                        Message :
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td>
	                                        <% if (Model.MessageForCustomer !=null) {%>
												<span><%=Model.MessageForCustomer %></span>
											<%}%> 
                                        </td>
                                    </tr>
                                </tbody>
                            </table>

						</td>
                        <td class="col col-30 col4" colspan="2">
                        
                        <table class="prixtotal cols">
                            <tr>
                                <td class=" col col1">Total HT :</td>
                                <td class=" col col2 prixtotalht"><%=Model.Total.ToCurrency() %></td>
                            </tr>
                            <tr>
                            	<td class="col col1">Frais de Port HT : </td>
                                <td class="col col2 prodfraisdeport"><%=(Model.ShippingFee.Total > 0) ? Model.ShippingFee.Total.ToCurrency() + "" : "Franco" %></td>
                            </tr>
                            <% if (Model.RecycleTotal > 0) { %>
                            <tr>
                                <td class=" col col1">Eco Taxe : </td>
                                <td class=" col col2 prodecotaxe"><%=Model.RecycleTotal.ToCurrency() %></td>
                            </tr>
                            <% } %> 
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
                         
                    </tr>
                 </tfoot>  
                <tbody>
				<%foreach (QuoteItem item in Model.Items) {%>
                  
                 <tr class="prodligne<%=Model.Items.ColumnIndex(item, 2) %>">
                    <td class="col col-5 col1">
						<%=Model.Items.IndexOf(item) + 1%>
                    </td>        
                    <td class="col col-20 col2">
                         <a href="<%=Url.Href(item.Product)%>" title="<%=Html.Encode(item.Product.Title)%>">
                            <img src="<% =Url.ImageSrc(item.Product, 140,140, "/content/images/vignette140.png")%>" alt="<%=Html.Encode(item.Product.Title)%>" />
                        </a>                   
                    </td>
                    <td class="col col-25 col3 colg">
                        <a href="<%=Url.Href(item.Product)%>" title="<%=Html.Encode(item.Product.Title)%>"><% =Html.Encode(item.Product.Title) %></a>
                        <br />
                        <span>REF : <%=item.Product.Code%></span>
                        <% if (!item.CustomerProductCode.IsNullOrTrimmedEmpty()) { %>
                        Votre code : <%=item.CustomerProductCode%>
                        <% } %> 
                    </td>
                    <td class="col col-10 col4 form_element_radio">
                    <p>
						<%=item.Quantity%>
                    </p>
                    </td>
                    <%if (Model.Status.IsWaiting()) { %>
                    <td class="col col-10 col5">
						<p><small><%=item.Disponibility %></small></p>
                    </td>
                    <% } %> 
                    <td class="col col-20 col5">
                     <p class="prix prixtotalht"><%=item.SalePrice.Value.ToCurrency()%></p>
                     <% if (item.RecyclePrice.Value > 0) { %> 
                    <p class="prodecotaxe">
                        eco taxe : <%=item.RecyclePrice.Value.ToCurrency() %>
                    </p> 
                    <% } %>
                    </td>
                    <td class="col col-20 col6">
						<%=item.GrandTotal.ToCurrency() %>
                    </td>                    
              </tr>
            <% } %>
        </tbody>
        
   </table>

        <!-- gestion des notes //-->
        <div class="notes important">
            <strong>
                <img src="/content/images/icon_noter.png" alt="" />
                A noter !
             </strong>
            <p class="note">
                <span>
                    La durée de validité de cette offre de prix court jusqu'au 
                    <%=string.Format("{0:dd.MM.yyyy}", Model.ExpirationDate)%> sauf changement de tarif du fabricant.<br/><br/>
                    La disponibilité des produit est donnée en temps réel et peut varier dans 
                    le temps en fonction de notre activité.
                    L'offre est valable pour la globalité de l'affaire.
                    La presente offre est soumise aux <a href="<%=Url.TermsAndConditionsHref() %>">conditions générales de vente</a> que nous 
                    tenons à votre dispostion sur simple demande.                
                </span>
            </p>
        </div>
        
        <%Html.RenderPartial("~/views/account/quote/commentlist.ascx", Model);%>
        
		<% if (Model.Status == QuoteStatus.Waiting) { %>
           <table class="go_commande cols">
       <tr>
            <td class="col col-33 col1"> <span><a class="go_commande_no" href='<%=Url.CancelQuoteHref(Model)%>'>Classer ce Devis</a></span></td>
            <td class="col col-33 col2"> <span>&nbsp;</span></td>
            <td class="col col-33 col3"> <span><a href="#" class="convertToOrder">Accepter ce Devis</a></span></td>
        </tr>
    </table>  

		<% } %> 

    <script type="text/javascript">
    	$(document).ready(function() {

    		$(".convertToOrder").click(function() {
    			$("#dialog").load('<%=Url.AcceptQuoteConfirmationUrl()%>', { key: '<%=ViewData["key"]%>' }, function(html) {
    				$('#dialog').empty();
    				$("#dialog").html(html);
    				$("#dialog").dialog('open');
    			});
    		});
    	});

	</script>
    </div>
    
    </div>
</div>
</asp:Content>
