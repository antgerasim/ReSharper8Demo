<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Les partenaires</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
          <h2 class="titleh2">
              <span>
                   Partenaires
              </span>
          </h2>
        <div class="bloc chemin">
           <span><a href="/accueil">accueil</a></span><b>Les partenaires</b>
        </div>
        <div>
			<br />
			<br />
			<p>Ce site est propulsé par la <a href="http://www.erpstore.net">plateforme eCommerce</a> ERPStore</p> et animé avec la <a href="http://www.erp360.net">gestion commerciale</a> ERP360
        </div>
	</div>
</div>
</asp:Content>

