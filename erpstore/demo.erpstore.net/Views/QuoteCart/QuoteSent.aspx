<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<QuoteCart>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Votre demande de devis</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
        <div class="chemin">
            <span><a href="/" title="accueil">accueil</a></span><b>Demande de prix</b>
        </div>
          <h2 class="titleh2">
              <span>Votre demande de prix</span>
          </h2>
            <br class="clear"/>
            <div class="corner aide">
                <h3>Votre demande de prix à bien été envoyé,</h3>
                <p>vous allez recevoir une confirmation par email à l'adresse suivante : <b><%=Model.Email%></b></p>
                <p>Nous allons vous repondre dans les plus brefs délais</p>
                <p>Merci au nom de l'equipe commerciale de <%=ERPStoreApplication.WebSiteSettings.SiteName%></p>
            </div>
	</div>
</div>
</asp:Content>

