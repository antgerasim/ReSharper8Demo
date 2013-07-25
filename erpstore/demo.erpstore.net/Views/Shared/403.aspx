<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
<title>Accès non autorisé</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
          <h2 class="titleh2">
              <span>
                   Erreur 403
              </span>
          </h2>
        <div class="bloc chemin">
           <span><a href="/accueil">accueil</a></span>span><b>Erreur type 403.</b>
        </div>
        <div class="corner aide">
            <div class="corner texte bloc">
           <p> Répertoire interdit (Forbidden)</p>
           <img src="/content/images/pubs/femme_furieuse.jpg" alt=""/>
           <br/>
           <br/>
            </div>
        </div>
    </div>
</div>

</asp:Content>