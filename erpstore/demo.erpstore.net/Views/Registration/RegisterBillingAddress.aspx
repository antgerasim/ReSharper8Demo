<%@ Page Language="C#" MasterPageFile="Register.Master" Inherits="System.Web.Mvc.ViewPage<RegistrationUser>" %>

<asp:Content ID="registerContent" ContentPlaceHolderID="FormContent" runat="server">

        <h2 class="titleh2">
            <span>
                informations personnelles
            </span>
        </h2>
        
        <div class="bloc chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span>
            <b>Adresse de facturation</b>
        </div>

		
        <div class="form_elements">
		
		<%Html.RenderPartial("ValidationSummary");%>
        
        <% Html.BeginForm(); %>
        <fieldset class="form" id="form_etape2">
        <legend>Adresse de facturation</legend>
            <p>
                <label for="billingaddressrecipientname">Nom :</label>
                <span class="form_element_input"><%= Html.TextBox("billingaddressrecipientname")%></span>
                <%= Html.ValidationMessage("billingaddressrecipientname", "*")%>
            </p>
            <p>
                <label for="billingaddresscountryid">Pays:</label>
                <span class="form_element_input"><%= Html.DropDownList("billingaddresscountryid", Html.CountryList())%></span>
                <%= Html.ValidationMessage("billingaddresscountryid", "*")%>
            </p>
            <p>
                <label for="billingaddresszipcode">Code Postal:</label>
                <span class="form_element_input"><%= Html.TextBox("billingaddresszipcode")%></span>
                <%= Html.ValidationMessage("billingaddresszipcode")%>
            </p>
            <p>
                <label for="billingaddresscity">Ville:</label>
                <span class="form_element_input"><%= Html.TextBox("billingaddresscity")%></span>
                <%= Html.ValidationMessage("billingaddresscity")%>
            </p>
            <p>
                <label for="billingaddressstreet">Rue:</label>
                <span class="form_element_input"><%= Html.TextArea("billingaddressstreet")%></span>
                <%= Html.ValidationMessage("billingaddressstreet")%>
            </p>
        </fieldset>
        <!-- gestion des notes //-->
        <div class="notes">
            <strong><img src="/content/images/icon_noter.png" alt=""/> A noter !</strong>
            <p class="note"> 
                <span>Vous devez impérativement indiquer le code postal et la ville. Ces informations sont indispensables pour que votre commande soit enregistrée et traitée.</span>
            </p>
        </div> 
        
    
     <!-- fin gestion des notes //-->
            <table class="go_commande cols">
               <tr>
                    <td class="col col-33 col1">
                    <span>
					<% if (!Model.CorporateName.IsNullOrTrimmedEmpty()) { %>
                        <a class="go_commande_no" style="padding-left:1em;" href="<%=Url.RegisterAccountCorporateHref()%>">Informations Société</a>
                    <% } else { %> 
						<a class="go_commande_no" style="padding-left:1em;" href="<%=Url.RegisterAccountHref()%>">Informations Personnelles</a>
                    <% } %>
                    </span>
           			</td>
                    <td class="col col-33 col2"> <span>&nbsp;</span></td>
                    <td class="col col-33 col3"> <span><input type="submit" value="Etape suivante"  class="corner"/></span></td>
                </tr>
            </table>     
        <br class="clear" />   
 </div>
</asp:Content>
