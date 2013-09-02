<%@ Page Language="C#" MasterPageFile="Register.Master" Inherits="System.Web.Mvc.ViewPage<RegistrationUser>" %>

<asp:Content ID="registerContent" ContentPlaceHolderID="FormContent" runat="server">
        <h2 class="titleh2">
            <span>
                informations personnelles
            </span>
        </h2>
        
        <div class="bloc chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span>
            <b>Confirmation</b>
        </div>
		<% Html.BeginRegistrationForm(); %>
       
     <div class="form_elements">
	 <%Html.RenderPartial("ValidationSummary");%>
        <fieldset class="form" id="form_etape1">
        <legend class="corner">identité</legend>
            <p class="form_elements_radio">
                <span><b class="form_element_radio"><% =Html.RadioButton("PresentationId", 1)%></b> Monsieur </span>
                <span><b class="form_element_radio"><% =Html.RadioButton("PresentationId", 2)%></b> Mademoiselle </span>
                <span><b class="form_element_radio"><% =Html.RadioButton("PresentationId", 3)%></b> Madame </span>
            </p>
            <p>
                <label for="firstName">Prénom:</label>
               <span class="form_element_input"><%= Html.TextBox("firstname") %></span>
                <%= Html.ValidationMessage("firstname")%>
            </p>
            <p>
                <label for="lastname">Nom:</label>
                <span class="form_element_input"><%= Html.TextBox("lastname") %></span>
                <%= Html.ValidationMessage("lastname") %>
            </p>
            <p>
                <label for="corporatename">Société:</label>
                <span class="form_element_input"><%= Html.TextBox("corporatename")%></span>
                <%= Html.ValidationMessage("corporatename") %>
            </p>
            <p>
                <label for="BillingAddressCountryId">Pays:</label>
                <%= Html.DropDownList("BillingAddressCountryId", Html.CountryList())%>
                <%= Html.ValidationMessage("BillingAddressCountryId")%>
            </p>
            <p class="form_element_error">
                <label for="email">Email:</label>
               <span class="form_element_input"> <%= Html.TextBox("email", Model.Email)%></span>
                <%= Html.ValidationMessage("email") %>
            </p>
            <p class="form_element_error">
                <label for="emailConfirmation">Confirmation de l'Email:</label>
                <span class="form_element_input"><%= Html.TextBox("emailConfirmation")%></span>
                <%= Html.ValidationMessage("emailConfirmation")%>
            </p>
            <p>
                <label for="mobilenumber">Numéro de mobile:</label>
                <span class="form_element_input"><%= Html.TextBox("mobilenumber")%></span>
                <%= Html.ValidationMessage("mobilenumber", "*")%>
            </p>
            <p>
                <label for="phonenumber">Numéro de téléphone:</label>
                <span class="form_element_input"><%= Html.TextBox("phonenumber")%></span>
                <%= Html.ValidationMessage("phonenumber","*")%>
            </p>
            <p>
                <label for="faxnumber">Numéro de FAX:</label>
                <span class="form_element_input"><%= Html.TextBox("faxnumber")%></span>
                <%= Html.ValidationMessage("faxnumber")%>
            </p>
            <p>
                <label for="password">Mot de passe:</label>
                <span class="form_element_input"><%= Html.Password("password")%></span>
                <%= Html.ValidationMessage("password")%>
            </p>
            <p>
                <label for="passwordConfirmation">Confirmation du mot de passe:</label>
                <span class="form_element_input"><%= Html.Password("passwordConfirmation")%></span>
                <%= Html.ValidationMessage("passwordConfirmation")%>
            </p>
            </fieldset>
         
        <!-- gestion des notes //-->
        <div class="notes">
            <strong><img src="/content/images/icon_noter.png" alt=""/> A noter !</strong>
            <p class="note"> 
                <span>Vous devez impérativement indiquer un numéro de téléphone fixe ou portable et une adresse e-mail.
                <br/>
                Ces informations sont indispensables pour que votre commande soit enregistrée et traitée.</span>
            </p>
        </div> 
        <!-- fin gestion des notes //-->
           <table class="go_commande cols">
               <tr>
                    <td class="col col-33 col1"> <span>&nbsp;</span></td>
                    <td class="col col-33 col2"> <span>&nbsp;</span></td>
                    <td class="col col-33 col3"> <span><input type="submit" value="Etape suivante"  class="corner"/></span></td>
                </tr>
            </table>     
        <br class="clear" />        
  </div>


</asp:Content>
