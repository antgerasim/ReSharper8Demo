<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2Columns.Master" Inherits="System.Web.Mvc.ViewPage<Order>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
		<%Html.RenderPartial("~/views/account/managemenu.ascx", Model.User);%>
        
        <% if (User.Identity.IsAuthenticated) { %>
        <%Html.ShowWorkflow("~/views/account/workflow.ascx", Model);%>
        <% } %> 
    </div>
	<div id="sgrid32">
        <h2 class="titleh2">
              <span>Commande</span>
          </h2>
        <div class="chemin">
            <span><a href="/" title="accueil">accueil</a></span><b>modification de commande</b>
        </div>

          
    
<% if (ViewData["MessageSent"] == null) { %>
    
    
    <%Html.BeginEditOrderForm();%> 
     <div class="form_elements">
     <fieldset class="form">
    <legend class="corner">modification de commande</legend>
       
        	<%Html.RenderPartial("ValidationSummary");%>
        	<h4>Veuillez indiquer ci-dessous ce que vous voulez modifier dans cette commande :</h4>
	   
	<%=Html.Hidden("orderCode", Model.Code)%>
	<p><%=Html.TextArea("message", new { cols = 50, rows = 10 })%><br />
	<%=Html.ValidationMessage("message", "*")%></p>
	<p class="submit">
    	<input type="submit" value="Valider" />
    </p>
	</fieldset>
	 </div>
	 <%Html.EndForm();%>
	
<% } else { %> 
    <fieldset class="form">
    <legend class="corner">modification de commande</legend>
        <div class="form_elements">
        	<h4>Votre demande de modification de la commande N°<%=Model.Code%> vient d'etre envoyé a votre responsable de compte.</h4>
		</div>
    </fieldset>
<% } %>

</div>
</div>
</asp:Content>

