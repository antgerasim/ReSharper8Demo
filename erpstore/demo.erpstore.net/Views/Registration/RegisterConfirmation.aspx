<%@ Page Language="C#" MasterPageFile="Register.Master" Inherits="System.Web.Mvc.ViewPage<RegistrationUser>" %>

<asp:Content ID="registerContent" ContentPlaceHolderID="FormContent" runat="server">

        <h2 class="titleh2">
            <span>
               Récaptitulatif des informations fournies
            </span>
        </h2>
        
        <div class="bloc chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span>
            <b>Confirmation</b>
        </div>
        
				<%Html.RenderPartial("ValidationSummary");%>
                
                <% Html.BeginForm(); %>
                <div class="form_elements">
                <fieldset class="form" id="form_etape4">
                    <legend class="corner">Récaptitulatif des informations fournies :</legend>
                        <p>
                            <label>Vous vous appelez : </label><b><%=Model.FullName%></b>
                        </p>
                        <% if (!Model.CorporateName.IsNullOrTrimmedEmpty()) { %>
                        <p>
                            <label>Votre société est : </label><b><%=Model.CorporateName%></b>
                        </p>
                        <% } %>         
                        <p>
                            <label>Votre numéro de téléphone est : </label><b><%=Model.PhoneNumber%></b>
                        </p>
                        <p>
                            <label>Votre numéro de mobile est : </label><b><%=Model.MobileNumber%></b>
                        </p>
                        <% if (!string.IsNullOrEmpty(Model.FaxNumber)) { %>
                        <p>
                            <label>Votre numéro de fax est : </label><b><%=Model.FaxNumber%></b>
                        </p>
                        <% } else { %>
                        <p>
                            <label>Vous n'avez pas de numéro de fax</label>
                        </p>
                        <% } %>
                        <p>
                            <label>Votre Email est : </label><b><%=Model.Email%></b>
                        </p>
                </fieldset>
                <!-- gestion des notes //-->
                <div class="notes">
                    <strong><img src="/content/images/icon_noter.png" alt=""/> A noter !</strong>
                    <p class="note form_element_radio"> 
                        <span>
                            <input type="checkbox" name="confirmation" />&nbsp;En cochant cette case je suis d'accord avec les <%=Html.TermsAndConditionsLink("conditions générales de vente")%>                            <%=Html.ValidationMessage("confirmation", "*")%>
                        </span>
                    </p>
                
                </div> 
                <!-- fin gestion des notes //-->
				<br class="clear"/>
               <table class="go_commande cols">
                   <tr>
                        <td class="col col-33 col1"> <span><a class="go_commande_no" style="padding-left:0.5em;" href="<%=Url.RegisterBillingAddressHref()%>">Retour adresse de facturation</a></span></td>
                        <td class="col col-33 col2"> <span>&nbsp;</span></td>
                        <td class="col col-33 col3"> <span><input type="submit" value="Enregistrer"  class="corner"/></span></td>
                    </tr>
                </table>                      

        </div>
 


</asp:Content>
