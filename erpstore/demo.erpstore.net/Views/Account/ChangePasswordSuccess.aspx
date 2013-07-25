<%@Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="changePasswordSuccessHead" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>Mot de passe changé</title>
</asp:Content>

<asp:Content ID="changePasswordSuccessContent" ContentPlaceHolderID="MainContent" runat="server">
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
            Votre mot de passe vient d'etre changé.
        </p>
        </div>
    </div>
</div>
</asp:Content>
