<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Invoice>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Facture N°<%=Model.Code%></title>
    <script src="/scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">

		<%Html.RenderPartial("~/views/account/managemenu.ascx", User.GetUserPrincipal().CurrentUser);%>
        <%Html.ShowWorkflow("~/views/account/workflow.ascx", Model);%>
        <% if (Model.Customer != null) { %>
        <h2 class="titleh2">Votre chargé d'affaire</h2>
        <div class="bloc texte">
            <div class="corner bloc_type_liste">
                <form method="get" action="<%=Request.Url.LocalPath%>" id="formFilter" >
                <ul>
                    <li>
                    <%=Model.Customer.Vendor.FullName%>
                    </li>
                    <li>
                    <img src='<%=Url.ImageSrc(Model.Customer.Vendor, 96, "/content/images/vendeur.png") %>' alt='photo :<%=Model.Customer.Vendor.FullName%>' />
                    </li>
                    <li>Email : <a href="mailto://<%=Model.Customer.Vendor.Email%>"><%=Model.Customer.Vendor.Email%></a></li>
                    <li>Tel : <%=Model.Customer.Vendor.PhoneNumber%></li>
                    <li>Mob : <%=Model.Customer.Vendor.MobileNumber%></li>
                </ul>
                </form>
             </div>
         </div>
         <% } %>        
    </div>
    
	<div id="sgrid32">
          <h2 class="titleh2">
              <span>Votre compte personnalisé</span>
          </h2>
         <div class="bloc chemin">
             <span><a href="<%=Url.HomeHref()%>" >accueil<img src="defaultIcon.jpg"></a></span>
             <span><a href="<%=Url.AccountHref()%>">Tableau de bord</a></span>
             <span><a href="<%=Url.InvoiceListHref()%>" >Factures<img src="defaultIcon.jpg"></a></span>
             <b>Facture N°<%=Model.Code%> du <%=Model.CreationDate.ToString("dddd dd MMMM yyyy")%></b>
         </div>
         <div class="ssgrid321">
    <table class="cols">
        <tbody>
        	<tr>
            	<td class="col col-60 col2">
                    <ul>
                        <li>Status : <span><%=Model.Status.ToLocalizedName()%></span></li>
                        <li>Nombre de produits : <span><% =Model.ItemCount%></span></li>
                        <li>Mode de reglement : <span><%=Model.PaymentModeName %></span></li>
                    </ul>                
                </td>
                <td class="col col-40 col2">
                    <ul>
                        <% if (Model.Status != InvoiceStatus.FullyRecovered) { %>
							<% if (Model.ExpirationDate < DateTime.Today) { %>
								<li>Echéance : <span style="color : Red;"><strong><%=string.Format("{0:dddd dd MMMM yyyy}", Model.ExpirationDate)%></strong></span></li>
							<% } else { %> 
		                        <li>Echéance : <span><strong><%=string.Format("{0:dddd dd MMMM yyyy}", Model.ExpirationDate)%></strong></span></li>
							<% } %> 
                        <% } else { %>
                        <li>Echéance : <br/><span><%=string.Format("{0:dddd dd MMMM yyyy}", Model.ExpirationDate)%></span></li>
                        <% } %> 
                        <li><a href="<%=Url.DownloadDocumentHref(SaleDocumentType.Invoice, Model.Id, Model.Code)%>">Téléchargez la facture au format PDF</a></li>
                    </ul>   
                </td>
            </tr>
          </tbody>
     </table>
     <br/>
     <table class="cols">
     	<tbody>
            <tr>
                <td class="col col-50 col1" colspan="2">
                <div style="text-align:left; padding:1em;">
					Adresse de facturation : <span><%Html.RenderPartial("address", Model.BillingAddress);%></span>
                </div>                      
                </td class="col col-50 col1">
            </tr>
        </tbody>
    </table> 
    <br/>

    <table class="cols">
               <thead>
                    <tr class="entete-cols">
                        <th class="col col-5 col1">N°</th>
                        <th class="col col-20 col2">Photo</th>
                        <th class="col col-30 col3">Produit</th>
                        <th class="col col-5 col4">Qté.</th>
                        <th class="col col-15 col5">Prix HT</th> 
                        <th class="col col-15 col6">Total HT</th>  
                    </tr>
                </thead>
                
              	<tfoot>
                    <tr>
                        <th class="col col-20 col1 frais" colspan="2">
                           <table summary="frais">
                           	<tr>
                                <td>Nom</td>
                                <td>Montant HT</td>
                            </tr>
                           </table>                            
                        </th>
                        <th class="col col-30 col3 tva" colspan="2">
                           <table summary="tva">
                           	<tr>
                                <td>Taux TVA</td>
                                <td>base HT</td>
                                <td>Montant TVA</td>
                            </tr>
                           </table>     
                        </th>
                        <th class="col col-50 col6" colspan="2">Total</th> 
                   </tr>
                    <tr class="prod">
                        <td class="col col-20 col1 frais" colspan="2">
       					<% if (Model.FeeList.IsNotNullOrEmpty()) { %>
                            <table summary="frais">
                                <tr>
                                <% foreach (Fee fee in Model.FeeList) { %>
                                    <td><p><%=fee.Name%></p></td>
                                    <td><p><%=fee.TotalWithTax.ToCurrency()%></p></td>
                                <% } %>
                                </tr>
                            </table> 
                        <% } %>
                        </td>
                        <td class="col col-30 col1 tva" colspan="2">
                           <table summary="tva">
                            <tr>
                               <% foreach (Price taxPrice in Model.TaxList) { %>
                                    <td><p><%=string.Format("{0:P}", taxPrice.TaxRate)%></p></td>
                                    <td><p><%=taxPrice.Value.ToCurrency()%></p></td>
                                    <td><p><%=taxPrice.TaxValue.ToCurrency()%></p></td>
                                <% } %>
                               </tr>
                           </table>                        
                        </td>
                        <td class="col col-50 col4" colspan="2">
                        
                        <table class="prixtotal cols">
                            <tr>
                                <td class="col col1">Total HT :</td>
                                <td class="col col2 prixtotalht"><%=Model.Total.ToCurrency() %></td>
                            </tr> 
                            <% if (Model.RecycleTotal > 0) { %>
                            <tr>
                                <td class="col col1">Eco Taxe : </td>
                                <td class="col col2 prodecotaxe"><%=Model.RecycleTotal.ToCurrency() %></td>
                            </tr>
                            <% } %> 
                            <tr>
                                <td class="col col1">Total TVA : </td>
                                <td class="col col2 prixtva"><%=Model.GrandTaxTotal.ToCurrency() %></td>
                            </tr>
                            <tr>
                                <td class="col col1">Total TTC :</td>
                                <td class="col col2 prixtotalttc"><%=Model.GrandTotalWithTax.ToCurrency() %></td>
                            </tr> 
                        </table>
                        
                        </td>
                         
                    </tr>
                 </tfoot>  
                   

   				<% var orderInfo = string.Empty; %>
				<% var deliveryInfo = string.Empty; %>
                <%foreach (InvoiceItem item in Model.Items.OrderBy(i => i.OrderCode).ThenBy(i => i.DeliveryCode)) {%>
              <thead>
				 <% if (Model.Customer != null && (orderInfo != item.OrderSourceInfo || deliveryInfo != item.DeliverySourceInfo)) { %>
               
               <tr>
               		<th class="col col-90 col1 colg" colspan="6">
                    <ul>
                    <% if (orderInfo != item.OrderSourceInfo) { %>
                       <li><b>Commande : <%=item.OrderCode%></b>, REF : (<%=item.CustomerDocumentReference%>) <small>&nbsp;du&nbsp;<%=string.Format("{0:dddd dd MMMM yyyy}", item.OrderCreationDate)%></small></li>
                       <% orderInfo = item.OrderSourceInfo; %>
                   <% } %>
                   <% if (deliveryInfo != item.DeliverySourceInfo) { %>
                       <li><b>BL : <%=item.DeliveryCode%></b><small>&nbsp;du&nbsp;<%=string.Format("{0:dddd dd MMMM yyyy}", item.DeliveryCreationDate)%></small></li>
                       <% deliveryInfo = item.DeliverySourceInfo; %>
                   <% } %> 
               </ul>
                    </th>
               </tr>
            </thead>
                <% } %>
			<tbody>
                 <tr class="<%=Model.Items.ColumnIndexName(item, 2, "prodligne")%>">
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
                    <td class="col col-5 col4 form_element_radio">
                    <p>
						<%=item.Quantity%>
                    </p>
                    </td>
                    <td class="col col-15 col5 cold">
                     <p class="prix prixtotalht"><%=item.SalePrice.Value.ToCurrency()%></p>
                     <% if (item.RecyclePrice.Value > 0) { %> 
                    <p class="prodecotaxe">
                        eco taxe : <%=item.RecyclePrice.Value.ToCurrency() %>
                    </p> 
                    <% } %>
                    </td>
                    <td class="col col-15 col6 cold">
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
					<%=ERPStoreApplication.WebSiteSettings.Texts.InvoiceDisclaimer.Replace("\r", "<br/>")%>
                </span>
            </p>
        </div>
        
        <Html.RenderPartial("~/views/account/quote/commentlist.ascx", Model);%>
		
        </div>        

	</div>
</div>

</asp:Content>
