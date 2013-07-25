<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="changePasswordHead" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>Changement de mot passe</title>
</asp:Content>

<asp:Content ID="changePasswordContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
    	<%Html.RenderPartial("~/views/account/managemenu.ascx", Model);%>
    </div>
	<div id="sgrid32">
        <h2 class="titleh2">
            <span>
                CHangement de mot de passe
            </span>
        </h2>
        <div class="chemin">
			<span><a href="<%=Url.HomeHref()%>">accueil</a></span><b>Changement de mot de passe</b>
        </div>

        <div class="form_elements">
        <p>
            Veuillez utiliser le formulaire suivant pour pouvoir changer de mot de passe. 
        </p>
        <%= Html.ValidationSummary() %>
    
        <% using (Html.BeginForm()) { %>
                <fieldset class="form">
                    <legend class="corner">Account Information</legend>
                    <p>
                        <label for="newPassword">Nouveau mot de passe:</label>
                        <%= Html.Password("newPassword") %>
                        <%= Html.ValidationMessage("newPassword") %>
                    </p>
                    <p>
                        <label for="confirmPassword">Confirmation du mot de passe:</label>
                        <%= Html.Password("confirmPassword") %>
                        <%= Html.ValidationMessage("confirmPassword") %>
                    </p>
                    <p class="submit submit_right">
                        <input type="submit" value="Changer de mot de passe" />
                    </p>
                </fieldset>
            <%=Html.Hidden("key", ViewData["ChangePasswordKey"])%>
        <% } %>
        </div>
	</div>
</div>
</asp:Content>
