<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<User>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Edition du compte</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
		<%Html.RenderPartial("~/views/account/managemenu.ascx", Model);%>
    </div>
	<div id="sgrid32">
          <h2 class="titleh2">
              <span>Vos informations personnelles</span>
          </h2>
        <div class="chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><a href="<%=Url.AccountHref()%>">Tableau de bord</a></span><b>Vos informations personnelles</b>
        </div> 
	    <div class="form_elements">
            <fieldset class="form">
                <legend>Information personnelles</legend>   
			<%Html.BeginEditUserForm();%>
            <% if (ViewData["EditUserSuccess"] != null) { %>
            <!-- gestion des notes //-->
            <div class="notes errors">
                <strong>
                    <img src="/content/images/icon_noter.png" alt="" />
                    A noter !</strong>
                <p class="note">
                    <span>Vos modifications viennent d'etre enregistrées avec succès.</span>
                </p>
            </div>
            <!-- fin gestion des notes //-->
            <% } %>
            <p class="form_element_radio input">
                    <input type="radio" name="Presentation" value="1" <%=(Model.Presentation == UserPresentation.Mister) ? "checked=checked" : ""%>/> Monsieur &nbsp; &nbsp; &nbsp;
                    <input type="radio" name="Presentation" value="2" <%=(Model.Presentation == UserPresentation.Misses) ? "checked=checked" : ""%>/> Mademoiselle &nbsp; &nbsp; &nbsp;
                    <input type="radio" name="Presentation" value="3" <%=(Model.Presentation == UserPresentation.Miss) ? "checked=checked" : ""%>/> Madame
            </p>
        
            <p>
                <label>
                    Prénom :
                </label>
                <span class="form_element_input">
                    <%= Html.TextBox("FirstName")%>
                    <%= Html.ValidationMessage("FirstName") %>
                </span>
            </p>
            <p>
                <label>
                    Nom :
                </label>
                <span class="form_element_input">
                    <%= Html.TextBox("LastName")%>
                    <%= Html.ValidationMessage("LastName") %>
                </span>
            </p>
            <p style="display:none;">
                <small>
                    <label>
                        Compte créé le :
                    </label>
                    <span>
                        <%=string.Format("{0:dddd dd MMMM yyyy}", Model.CreationDate)%></span></small>
            </p>
            <p>
                <label>
                    Email :
                </label>
                <span class="form_element_input">
                    <%= Html.TextBox("Email")%>
                    <%= Html.ValidationMessage("Email") %>
                </span>
            </p>
            <p>
                <label>
                    Numéro de téléphone :
                </label>
                <span class="form_element_input">
                    <%= Html.TextBox("PhoneNumber")%>
                    <%= Html.ValidationMessage("PhoneNumber") %>
                </span>
            </p>
            <p>
                <label>
                    Numéro de mobile :
                </label>
                <span class="form_element_input">
                    <%= Html.TextBox("MobileNumber")%>
                    <%= Html.ValidationMessage("MobileNumber") %>
                </span>
            </p>
            <p>
                <label>
                    Numéro de fax :
                </label>
                <span class="form_element_input">
                    <%= Html.TextBox("FaxNumber")%>
                    <%= Html.ValidationMessage("FaxNumber") %>
                </span>
            </p>
            <p class="submit submit_right">
                <input type="submit" value="Modifier" class="corner" />
            </p>
            <% Html.EndForm(); %>
            </fieldset>
            <fieldset class="form">
                <legend>Informations de connexion</legend> 
                <p>
                    <label>
                        Identifiant de connexion :
                    </label>
                    <span class="form_element_input">
                        <%=Model.Login%></span>
                </p>
            </fieldset>
        </div>

	</div>
</div>
</asp:Content>
