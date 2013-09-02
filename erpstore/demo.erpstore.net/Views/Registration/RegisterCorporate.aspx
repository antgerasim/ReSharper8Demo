<%@ Page Language="C#" MasterPageFile="Register.Master" Inherits="System.Web.Mvc.ViewPage<RegistrationUser>" %>

<asp:Content ID="registerContent" ContentPlaceHolderID="FormContent" runat="server">
           
         <h2 class="titleh2">
            <span>
                informations société
            </span>
        </h2>
        
        <div class="bloc chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span>
            <b>Informations société</b>
        </div>      
        
       
        <div class="form_elements">
        
		<%Html.RenderPartial("ValidationSummary");%>
		<% Html.BeginForm(); %>
        <fieldset class="form" id="form_etape2">
            <legend>Informations sur la société</legend>
            <p>
                <label for="corporatename">Nom de la société:</label>
               <span class="form_element_input"> <%= Html.TextBox("corporatename") %></span>
                <%= Html.ValidationMessage("corporatename")%>
            </p>
            <p>
                <label for="corporatesocialstatus">Statut de la société (sarl, sa, sas, eurl):</label>
                <span class="form_element_input"><%= Html.TextBox("corporatesocialstatus")%></span>
                <%= Html.ValidationMessage("corporatesocialstatus")%>
            </p>
            <p>
                <label for="corporateemail">Email:</label>
               <span class="form_element_input"> <%= Html.TextBox("corporateemail")%></span>
                <%= Html.ValidationMessage("corporateemail")%>
            </p>
            <p>
                <label for="corporatephonenumber">Numéro de téléphone:</label>
                <span class="form_element_input"><%= Html.TextBox("corporatephonenumber")%></span>
                <%= Html.ValidationMessage("corporatephonenumber")%>
            </p>
            <p>
                <label for="corporatefaxnumber">Numéro de FAX:</label>
               <span class="form_element_input"> <%= Html.TextBox("corporatefaxnumber")%></span>
                <%= Html.ValidationMessage("corporatefaxnumber")%></
            </p>
            <p>
                <label for="corporatewebsite">Site web officiel:</label>
                <span class="form_element_input"><%= Html.TextBox("corporatewebsite")%></span>
                <%= Html.ValidationMessage("corporatewebsite")%>
            </p>
            <p>
                <label for="nafcode">Code NAF Version 2008:</label>
                <span class="form_element_input"><%= Html.TextBox("nafcode")%></span>
                <%= Html.ValidationMessage("nafcode")%>
            </p>
            <p>
                <label for="siretnumber">Numéro Siret:</label>
                <span class="form_element_input"><%= Html.TextBox("siretnumber")%></span>
                <%= Html.ValidationMessage("siretnumber")%>
            </p>
            <p>
                <label for="tvanumber">Numéro TVA:</label>
                <span class="form_element_input"><%= Html.TextBox("tvanumber")%></span>
                <%= Html.ValidationMessage("tvanumber")%>
            </p>
            <p>
                <label for="rcsnumber">Numéro du registre du commerce:</label>
                <span class="form_element_input"><%= Html.TextBox("rcsnumber")%></span>
                <%= Html.ValidationMessage("rcsnumber")%>
            </p>
        </fieldset>
        <!-- gestion des notes //-->
        <div class="notes">
            <strong><img src="/content/images/icon_noter.png" alt=""/> A noter !</strong>
            <p class="note"> 
                <span>Vous devez impérativement indiquer le statut social, le code NAF, le numéro de SIRET et le numéro de TVA de la société. Ces informations sont indispensables pour que votre commande soit enregistrée et traitée.</span>
            </p>
        </div> 

        
        <!-- fin gestion des notes //-->
        <table class="go_commande cols">
              <tr>
                  <td class="col col-33 col1"> <span><a class="go_commande_no" style="padding-left:0.7em; letter-spacing:-0.04em;" href="<%=Url.RegisterAccountHref()%>">retour informations personnelles</a></span></td>
                   <td class="col col-33 col2"> <span>&nbsp;</span></td>
                   <td class="col col-33 col3"> <span><input type="submit" value="Etape suivante"  class="corner"/></span></td>
               </tr>
        </table>     
        <br class="clear" />     
        
        </div>


</asp:Content>
