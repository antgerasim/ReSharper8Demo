<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Votre panier</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
          <h2 class="titleh2">
              <span>Votre demande de prix est vide</span>
          </h2>
          <div class="chemin">
               <span><a href="/" title="accueil">accueil</a></span><b>Devis</b>
          </div> 
   		<img src="/content/images/panier_grd.jpg" alt="votre demande de prix est vide"/>
	</div>
</div>
</asp:Content>