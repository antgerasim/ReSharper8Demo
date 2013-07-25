<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Quote>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
		<%Html.ShowMenu("RightHelp.ascx");%>
    </div>
    
	<div id="sgrid32">
          <h2 class="titleh2">
              <span>
                  Classement de devis
              </span>
          </h2>
        <div class="chemin">
			<span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><a href="<%=Url.AccountHref()%>">Tableau de bord</a></span><b>Classement de Devis</b>
        </div> 

        <%Html.BeginCancelQuoteForm();%>
        
        <div class="form_elements">
            <fieldset class="form">
                <legend class="corner">raison de votre classement de devis</legend>
                   
               <%Html.RenderPartial("ValidationSummary");%>
               <% foreach (var item in Html.CancelQuoteReasonDictionary("reason")) { %>
                <p><input type="radio" name="reason" value="<%=item.Value%>" <%=item.Selected ? "checked=\"checked\"" : "" %>  /><label><%=item.Text%></label></p>
                
                <% } %> 
                
               <p>
                <label>Commentaire :</label>
                <%=Html.TextArea("comment", new { cols= 40, rows = 4}) %>
                <%=Html.ValidationMessage("comment", "*")%>
               </p>
                <p class="submit">
                <a href="<%=Url.Href(Model)%>" class="button">Annuler</a>&nbsp;
                <input type="submit" value="Valider"  class="button" />
                </p>
            </fieldset>
        </div>
        <%Html.EndForm();%>
    </div>
</div>
</asp:Content>


