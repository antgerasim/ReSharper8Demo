<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Order>" %>

	<table class="cols">
        <tbody>
        	<tr>
            	<td class="col col-60 col2">
                    <ul>
                        <li>Référence : <span><%=Model.CustomerDocumentReference%></span></li>
                        <li>Mode de reglement : <span><%=Model.PaymentModeName%></span></li>
                    </ul>                  
                </td>
                <td class="col col-40 col2">
                    <ul>
                        <li>Livraison partielle : <span><%=Model.AllowPartialDelivery ? "Acceptée" : "Refusée"%></span></li>
                        <li><a href="<%=Url.DownloadDocumentHref(SaleDocumentType.Order, Model.Id, Model.Code)%>">Téléchargez la commande au format PDF</a></li>
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
    <table class="cols">
               <thead>
                    <tr class="entete-cols">
                        <th class="col col-5 col1">N°</th>
                        <th class="col col-20 col2">Photo</th>
                        <th class="col col-25 col3">Produit</th>
                        <th class="col col-10 col4">Qté.</th>
                        <th class="col col-10 col5">Solde</th>
                        <th class="col col-15 col6">Prix HT</th> 
                        <th class="col col-15 col7">Total HT</th>  
                    </tr>
                </thead>
                
              	<tfoot>
                    <tr class="entete-cols">
                        <th class="col col-70 col1" colspan="4">&nbsp;</th>
                        <th class="col col-30 col2" colspan="3">Total</th> 
                    </tr>
                    <tr class="prod">
                        <td class="col col-70 col3" colspan="4">
                        
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
                        <td class="col col-30 col4" colspan="3">
                        
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
    			<% foreach (OrderItem orderItem in Model.Items) { %>
                 <tr class="<%=Model.Items.ColumnIndexName(orderItem, 2, "prodligne")%>">
                    <td class="col col-5 col1">
						<%=Model.Items.IndexOf(orderItem) + 1%>
                    </td>        
                    <td class="col col-20 col2">
                         <a href="<%=Url.Href(orderItem.Product)%>" title="<%=Html.Encode(orderItem.Product.Title)%>">
                            <img src="<% =Url.ImageSrc(orderItem.Product, 140,140, "/content/images/vignette140.png")%>" alt="<%=Html.Encode(orderItem.Product.Title)%>" />
                        </a>                   
                    </td>
                    <td class="col col-25 col3 colg">
                        <a href="<%=Url.Href(orderItem.Product)%>" title="<%=Html.Encode(orderItem.Product.Title)%>"><% =Html.Encode(orderItem.Product.Title) %></a>
                        <br />
                        <span>REF : <%=orderItem.Product.Code%></span>
						<% if (!orderItem.CustomerProductCode.IsNullOrTrimmedEmpty()) { %>
                        Votre code : <%=orderItem.CustomerProductCode%>
                        <% } %> 
                    </td>
                    <td class="col col-10 col4 form_element_radio">
                    <p>
						<%=orderItem.Quantity%>
                    </p>
                    </td>
                    <td class="col col-10 col5">
					   <% if(orderItem.IsBalanced && orderItem.Balance != 0){ %> 
                            <p class="button_0">
                                <small abbr="<%= (orderItem.IsBalanced) ? 0 : orderItem.Balance %>">soldé</small>
                           </p>                    
                        <% } else if (orderItem.Balance == 0) { %>
                            <p class="button_0">
                                <small abbr="<%= (orderItem.IsBalanced) ? 0 : orderItem.Balance %>">livré</small>
                           </p>                    
                        <% } else if (orderItem.Balance != orderItem.Quantity) { %>
                            <p class="button_1">
                                <small><abbr title="livraison partielle">Partiel (<%=orderItem.Quantity - orderItem.Balance%>)<br />Solde : <%=orderItem.Balance%></abbr></small>
                            </p>
                        <% } else { %>
                            <p class="button_1">
                                <small><abbr title="non livré"><%=orderItem.Balance%></abbr></small>
                            </p>
                        <% } %>
                    </td>
                    <td class="col col-15 col6">
                     <p class="prix prixtotalht"><%=orderItem.SalePrice.Value.ToCurrency()%></p>
                     <% if (orderItem.RecyclePrice.Value > 0) { %> 
                    <p class="prodecotaxe">
                        eco taxe : <%=orderItem.RecyclePrice.Value.ToCurrency() %>
                    </p> 
                    <% } %>
                    </td>
                    <td class="col col-15 col7">
						<%=orderItem.GrandTotal.ToCurrency() %>
                    </td>                    
              </tr>    
    <% } %>
         </tbody>
        
   </table>  
        <!-- gestion des notes //-->
       
        <div class="important">
            <strong>
                <img src="/content/images/icon_noter.png" alt="" />
                A noter !
             </strong>
            <p class="note">
                <span>
                    Cette commande est soumise aux <a href="<%=Url.TermsAndConditionsHref()%>">conditions générales de vente</a></span>
            </p>
        </div>





