<%@ Page Title="" Language="C#" MasterPageFile="Order.Master" Inherits="System.Web.Mvc.ViewPage<OrderCart>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Panier de commande : Configuration</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div id="grid3">
    <div id="sgrid33">
        <div class="mbilling">
            <h4><span>commande</span></h4>
            <ul class="nav mbilling_etapes">
                <li class="menubilling_etape" id="mbilling_etape1"><img src="/content/images/commande/commande_1off.jpg" alt="identification"/><span><b>1</b> Identification</span></li>
                <li class="menubilling_etape " id="mbilling_etape2"><img src="/content/images/commande/commande_1off.jpg" alt="adresse"/><span><b>2</b> Adresse</span></li>
                <li class="menubilling_etape mbilling_etape_on" id="mbilling_etape3"><img src="/content/images/commande/commande_1on.jpg" alt="adresse"/><span><b>3</b> Configuration</span></li>
                <li class="menubilling_etape" id="mbilling_etape4"><img src="/content/images/commande/commande_1off.jpg" alt="récapitulatif"/><span><b>3</b> Paiement</span></li>
                <li class="menubilling_etape" id="mbilling_etape5"><img src="/content/images/commande/commande_1off.jpg" alt="paiement"/><span><b>4</b> Récapitulatif</span></li>
            </ul>
        </div> 
        <br class="clear"/>
        <% Html.RenderPartial("validationsummary"); %>
        
        <% Html.BeginForm(); %>
        
        <table class="cols colsforms">
            <tbody>
                <tr>
                    <td class="col col-50">
                    <div class="form_elements">
                        <fieldset class="form form_destinataire">
                            <legend class="corner">Message pour le destinataire</legend>
                                    <p>
                                    <label for="message">Message :</label>
                                    <span><%=Html.TextArea("message", Model.Message)%></span>
                                     </p>
                                     <p>
                                        <label for="documentReference">Reference :</label>
                                    <span><%=Html.TextBox("documentReference", Model.CustomerDocumentReference)%></span>
                                    </p>
                                    <div class="notes">
                                        <strong><img src="/content/images/icon_noter.png" alt=""/> A noter !</strong>
                                        <p class="note"> 
                                            <span>Vous pouvez ajouter une référence (exemple : clous). Cette référence apparaitra sur tous les documents (Commande, Livraison, Facture)..</span>
                                        </p>
                                        <p class="note">
                                        	Le message sera affiché sur les documents imprimés de votre commande.
                                        </p>
                                   </div> 
                        </fieldset>
                    </div>          
                    </td>
                    <td class="col col-50">
                        <div class="form_elements">
                        <fieldset class="form form_livraison">
                            <legend class="corner">Livraison partielle</legend>
                           
                               <h4>
                                <span class="form_element_radio"><%=Html.CheckBox("partialDelivery", Model.AllowPartialDelivery) %></span>&nbsp;J'accepte la livraison partielle des produits.
                               </h4>
                               <div class="notes">
                                   <strong><img src="/content/images/icon_noter.png" alt=""/> A noter !</strong>
                                   <p class="note"> 
                                       <span>En acceptant la livraison partielle des produits, vous pouvez recevoir vos produits en plusieurs fois, selon la quantité de stock disponible. </span>
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
            <td class="col col-33 col1"> <span><a class="go_commande_no" href='<%=Url.CheckOutHref() %>'>Choix de l'adresse</a></span></td>
            <td class="col col-33 col2"> <span>&nbsp;</span></td>
            <td class="col col-33 col3"> <span><input type="submit" value="Etape suivante"/></span></td>
        </tr>
    </table>  

	<%=Html.HiddenDefaultConveyor() %>

   <% Html.EndForm(); %>
   </div>
</div>

</asp:Content>
