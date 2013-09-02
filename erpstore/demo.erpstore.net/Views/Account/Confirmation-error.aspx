<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
    	<%Html.RenderPartial("~/views/account/managemenu.ascx", Model);%>
    </div>
	<div id="sgrid32">
        <h2 class="titleh2">
            <span>
                Identification
            </span>
        </h2>
        <div class="chemin">
			<span><a href="<%=Url.HomeHref()%>">accueil</a></span><b>Confirmation de le l'adresse email</b>
        </div>
        <div class="form_elements"> 
    	<fieldset class="form" id="form_etape4">
        	<legend class="corner">Récaptitulatif des informations fournies :</legend>
            <p>
                <%=ViewData["Message"]%>
            </p>
	    </fieldset>
    </div>
    </div>
</div>
</asp:Content>

