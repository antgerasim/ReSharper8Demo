<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2Columns.Master" Inherits="System.Web.Mvc.ViewPage<Quote>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Classement d'un devis</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
		<%Html.ShowMenu("RightHelp.ascx");%>
    </div>
	<div id="sgrid32">
          <h2 class="titleh2">
              <span>
                  Devis classé
              </span>
          </h2>
        <div class="chemin">
			<span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><a href="<%=Url.AccountHref()%>">Tableau de bord</a></span><b>Devis classé</b>
        </div> 
        <div class="form_elements">
            <fieldset class="form">
            <legend class="corner">Classement de votre devis</legend>
                
                    <br/>
                    <p>Nous vous confirmons le classement de votre devis N°<b><%=Model.Code%></b></p>
                    <br/>
                    Raison : <%=((CancelQuoteReason) ViewData["reason"]).GetLocalizedName()%><br />
                    <%=ViewData["message"]%><br />
                    <br/>
               
            </fieldset>
        </div>
    </div>
</div>
</asp:Content>

