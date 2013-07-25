<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Quote>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
        <div class="chemin">
            <span><a href="/" title="accueil">accueil</a></span><b>Confirmation</b>
        </div>
        <h2 class="titleh2">
           <span>Confirmation</span>
        </h2>
        <br class="clear"/>
    	<%=Model.Code%>
        <br />    
        <%Html.BeginAcceptQuoteForm(); %>
        <input type="hidden" name="key" value='<%=ViewData["key"]%>' />
        <input type="submit" value="J'accepte le devis" />
        <%Html.EndForm();%>
	</div>
</div>
 
</asp:Content>

