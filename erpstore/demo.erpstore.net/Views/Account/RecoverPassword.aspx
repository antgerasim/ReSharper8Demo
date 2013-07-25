<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Retrouver son mot de passe</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
    	<%Html.RenderPartial("~/views/account/managemenu.ascx", Model);%>
    </div>
	<div id="sgrid32">
        <h2 class="titleh2">
            <span>
                se connecter
            </span>
        </h2>
        <div class="chemin">
			<span><a href="<%=Url.HomeHref()%>">accueil</a></span><b>retrouver son mot de passe</b>
        </div>
        <div class="form_elements">
        <p>Votre mot de passe est stocké de manière cryptée non réversible dans notre base de donnée, vous ne pouvez que changer votre mot de passe.
        <br/>
        <br/>
        Veuillez indiquer dans le formulaire suivant, l'identifiant ou l'adresse email avec lequel vous vous êtes inscrit, nous vous enverons alors en retour un email contenant un lien sur lequel il faudra cliquer pour pouvoir changer votre mot de passe</p>
        </div>
        <div class="form_elements">
            <% if (ViewData["PasswordSent"] == null) { %>
            <fieldset class="form">
                <legend class="corner">Vos informations</legend>    
                <%=Html.ValidationSummary()%>
                <%Html.BeginForm();%>
                <p><label>Identifiant ou Email : </label><%=Html.TextBox("login")%><%=Html.ValidationMessage("login")%></p>
                <p class="submit submit_right">
                	<input type="submit" value="Envoyer" class="corner" />
                  </p>
                <%Html.EndForm(); %>
            </fieldset>
            <% } else { %>
            Un email contenant la procédure à suivre vient de vous être envoyé.
            <% } %>
        </div>
    </div>
</div>
</asp:Content>
