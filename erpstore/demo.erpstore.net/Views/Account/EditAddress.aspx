<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Address>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
          <h2 class="titleh2">
              <span>Votre compte</span>
          </h2>
        <div class="chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span><b>modification d'une adresse</b>
        </div>   
		
		
		<div class="form_elements">
		<%=Html.ValidationSummary() %>
			<%Html.BeginForm(); %>
       
            <%Html.RenderPartial("~/views/shared/EditAddress.ascx", Model); %>
            
         </div>
         <br class="clear"/>
         <table class="go_commande cols">
               <tr>
                    <td class="col col-33 col1"> <span><a class="go_commande_no" href="" onclick="javascript:history.back()">Retour</a></span></td>
                    <td class="col col-33 col2"> <span>&nbsp;</span></td>
                    <td class="col col-33 col3"> <span><input type="submit" value="Enregistrer"/></span></td>
                </tr>
            </table> 

        <%Html.EndForm(); %>

	</div>
</div>
</asp:Content>