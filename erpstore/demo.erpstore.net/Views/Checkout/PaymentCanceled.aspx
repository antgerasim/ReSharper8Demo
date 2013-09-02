<%@ Page Title="" Language="C#" MasterPageFile="Order.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Panier de commande : Annulation Carte Bleue</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<div id="grid3">
	<div id="sgrid33">
        <div class="chemin">
            <span><a href="/" title="accueil">accueil</a></span><b>Votre commande</b>
        </div>
        <h2 class="titleh2">
              <span>Annulation de la transaction</span>
        </h2>
        <div class="form_elements">
            <!-- gestion des notes //-->
            <div class="notes important">
                <strong>
                    <img src="/content/images/icon_noter.png" alt="" />
                    A noter !
                 </strong>
                <p class="note">
                    <span>
                        <%=ViewData["message"] %>               
                    </span>
                </p>
            </div>
 		</div>
	</div>	
</div>

</asp:Content>