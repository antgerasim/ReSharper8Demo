<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<System.Web.Mvc.HandleErrorInfo>" %>
<%@ Import Namespace="Microsoft.Practices.Unity" %>

<script runat="server" language="c#">
	
	protected override void OnLoadComplete(EventArgs e)
	{
		base.OnLoadComplete(e);
		if (Model != null)
		{
			try
			{
				Model.Exception.AddWebContext(this.ViewContext.HttpContext);
				Model.Exception.Data.Add("ActionName", Model.ActionName);
				Model.Exception.Data.Add("ControllerName", Model.ControllerName);
				var logger = ERPStoreApplication.Container.Resolve<ERPStore.Logging.ILogger>();
				logger.Error(Model.Exception);
			}
			catch
			{ 
				
			}
		}
	}

</script>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
<title>Une erreur vient de se produire</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
          <h2 class="titleh2">
              <span>
                   Erreur serveur
              </span>
          </h2>
        <div class="bloc chemin">
           <span><a href="/accueil">accueil</a></span><b>Erreur serveur</b>
        </div>
        <div class="corner aide">
            <div class="corner texte bloc">
           <p>Il vient de se produire une erreur de traitement sur le serveur, veuillez nous excuser pour la gène occasionnée, <br/>un rapport vient d'etre envoyé à notre equipe technique</p>
           <img src="/content/images/pubs/femme_furieuse.jpg" alt=""/>
           <br/>
           <br/>
        </div>
        </div>
        <!--
        <% if (Model != null) {  %>
        
        <% = Model.Exception.Message %>
        
        <% } %>
        -->
    </div>
</div>

</asp:Content>
