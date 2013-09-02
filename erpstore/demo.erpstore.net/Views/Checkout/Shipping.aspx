<%@ Page Title="" Language="C#" MasterPageFile="Order.Master" Inherits="System.Web.Mvc.ViewPage<OrderCart>" %>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Panier de commande : Livraison</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
        <div class="mbilling">
			<h4><span>commande</span></h4>
            <ul class="nav mbilling_etapes">
             	<li class="menubilling_etape" id="mbilling_etape1"><img src="/content/images/commande/commande_1off.jpg" alt="identification"/><span><b>1</b> Panier</span></li>
                <li class="menubilling_etape mbilling_etape_on" id="mbilling_etape2"><img src="/content/images/commande/commande_1on.jpg" alt="identification"/><span><b>2</b> Adresses</span></li>
                <li class="menubilling_etape" id="mbilling_etape3"><img src="/content/images/commande/commande_1off.jpg" alt="adresse"/><span><b>3</b> Configuration</span></li>
                <li class="menubilling_etape" id="mbilling_etape4"><img src="/content/images/commande/commande_1off.jpg" alt="récapitulatif"/><span><b>4</b> Paiement</span></li>
                <li class="menubilling_etape" id="mbilling_etape5"><img src="/content/images/commande/commande_1off.jpg" alt="paiement"/><span><b>5</b> Récapitulatif</span></li>
            </ul>
        </div> 
        <br class="clear"/>
        
        <%Html.RenderPartial("validationsummary"); %>

	<% Html.BeginForm(); %>

<table class="cols colsforms">
	<tbody>
    	<tr>
        	<td class="col col-50">
				
                <div class="form_elements"> 
                <fieldset class="form form_recapitulatif">
                    <legend class="corner">Adresse(s) de livraison</legend>
                    
                    <%=Html.ValidationSummary() %>
                    
                    	<br/>
                        <h3>
                            adresse(s) de livraison existante(s)
                        </h3>
                        <br/>
                        <div class="form_adresse corner adr_existantes">
                     <% foreach (var item in User.GetUserPrincipal().CurrentUser.DeliveryAddressList) { %>
                                <p>
                                   <label class="form_element_radio"><%=Html.RadioButton("addressIndex", User.GetUserPrincipal().CurrentUser.DeliveryAddressList.IndexOf(item), User.GetUserPrincipal().CurrentUser.DeliveryAddressList.IndexOf(item) == (int)ViewData["SelectedAddressId"])%> <% =item.RecipientName.EllipsisAt(13) %></label>
                                   <small><%=item.Street %>, <%=item.ZipCode %><br/><%=item.City%> <%=item.Country.LocalizedName%></small>
                                </p>
                                <p class="submit submit_right">
                                   <%=Html.EditAddressLink(User.GetUserPrincipal().CurrentUser.DeliveryAddressList.IndexOf(item), "Modifier")%>
                                </p>
                        <%  } %>
                        </div>
                        </fieldset>
                       <fieldset class="form form_recapitulatif">     
                            <h4>
                                <span  class="form_element_radio"><%=Html.RadioButton("addressIndex", -1, -1 == (int)ViewData["SelectedAddressId"])%></span>&nbsp; nouvelle adresse de livraison
                            </h4>
                            <div class="form_adresse corner">
                                <%Html.RenderPartial("~/views/shared/editaddress.ascx", Model.DeliveryAddress); %>
                            </div>
                      <br class="clear" />
                    <div class="notes">
                        <strong><img src="/content/images/icon_noter.png" alt=""/> A noter !</strong>
                         <p class="note"> 
                             <span>Si vous utilisez une nouvelle adresse, elle sera ajoutée aux informations de votre compte.</span>
                        </p>
                    </div> 
                 </fieldset> 
                 </div>            
            </td>
            <td class="col col-50">
            	<div class="form_elements">
                    <fieldset class="form form_facturation">
                        <legend class="corner">Adresse de facturation :</legend>
                        <%=Html.ValidationSummary() %>
                            <div class="form_adresse form_ok corner">
                                <p><label>Nom : </label><%=Model.BillingAddress.RecipientName%></p>
                                <p><label>Rue : </label><%=Model.BillingAddress.Street %></p>
                                <p><label>Code postal et ville : </label><%=Model.BillingAddress.ZipCode%>&nbsp;<%=Model.BillingAddress.City%></p>
                                <p><label>Pays : </label><%=Model.BillingAddress.Country.LocalizedName%></p>	
                                <p class="submit submit_right">
                                    <%=Html.EditAddressLink(-1,"Modifier")%>
                                </p>
                            </div>
                     </fieldset>
                 </div>
            </td>
        </tr>
    </tbody>
</table>

<br class="clear"/>

   <table class="go_commande cols">
       <tr>
            <td class="col col-33 col1"> <span><a class="go_commande_no" href='<%=Url.CartHref() %>'>Voir le panier</a></span></td>
            <td class="col col-33 col2"> <span>&nbsp;</span></td>
            <td class="col col-33 col3"> <span><input type="submit" value="Etape suivante"/></span></td>
        </tr>
    </table>  
    
	<%Html.RenderPartial("~/views/Shared/RightMenu2.ascx");%>

	<%=Html.HiddenDefaultConveyor() %>
    <% Html.EndForm(); %>

	</div>
</div>

</asp:Content>
