<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
<title>Page non trouvée</title>
<style type="text/css">
    /* Widget content container */
   #goog-wm { }

    /* Heading for "Closest match" */
   #goog-wm h3.closest-match { }

    /* "Closest match" link */
   #goog-wm h3.closest-match a { }

    /* Heading for "Other things" */
   #goog-wm h3.other-things { }

    /* "Other things" list item */
   #goog-wm ul li { }

    /* Site search box */
   #goog-wm li.search-goog { display: list-item; }
</style>

</asp:Content>


<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
          <h2 class="titleh2">
              <span>
                   Erreur 404
              </span>
          </h2>
        <div class="bloc chemin">
           <span><a href="/accueil">accueil</a></span><b>Fichier non trouvé.</b>
        </div>
        <div class="corner aide">
            <div class="corner texte bloc">
               <p> Fichier non trouvé (File not found)</p>
               <img src="/content/images/pubs/femme_furieuse.jpg" alt=""/>
               <br/>
               <br/>
            </div>
        </div>
        <br />
        <br />
        <script type="text/javascript">
            var GOOG_FIXURL_LANG = "fr";
            var GOOG_FIXURL_SITE = "http://www.google.fr";
        </script>
        <script type="text/javascript" src="http://linkhelp.clients.google.com/tbproxy/lh/wm/fixurl.js" >
        </script>
    </div>
</div>

</asp:Content>

