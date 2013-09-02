<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/2Columns.Master" Inherits="System.Web.Mvc.ViewPage<User>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Votre compte personnalisé</title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
    	<%Html.RenderPartial("~/views/account/managemenu.ascx", Model);%>
    </div>
	<div id="sgrid32">
          <h2 class="titleh2">
              <span>
                   Votre compte 
              </span>
          </h2>
        <div class="chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><b>Tableau de bord</b></span>Bienvenue dans votre espace personnel !
        </div> 
            <div class="bloc texte">
                <p>
                    <blockquote>
                        Dans votre espace dédié, vous pouvez gérer les informations vous concernant, consulter la liste et l’évolution de vos derniers devis, commandes, livraisons et factures. Pour modifier vos informations personnelles, <a href="<%=Url.EditUserHref()%>" title="Modifier ses informations personnelles">cliquez ici !</a>
                    </blockquote>
                </p>
            </div>
            <script type="text/javascript">
        
                $(document).ready(function() {
        
                    $('#quotelist').empty();
                    $('#quotelist').append('<img src="/content/images/AjaxLoadingIcon.gif" alt="chargement en cours..." class="ajaxloading" />');
                    $('#quotelist').load('<%=Url.AjaxQuoteListHref() %>', { viewName: 'smalllist.ascx', StatusId: 1, Size: 5 }, function(html) {
                    $('#quotelist').html(html);
                    });
        
                    $('#orderlist').empty();
                    $('#orderlist').append('<img src="/content/images/AjaxLoadingIcon.gif" alt="chargement en cours..." class="ajaxloading" />');
                    $('#orderlist').load('<%=Url.AjaxOrderListHref() %>', { viewName: 'smalllist.ascx', StatusId: 1, Size: 5 }, function(html) {
                    $('#orderlist').html(html);
                    });
        
                    $('#invoicelist').empty();
                    $('#invoicelist').append('<img src="/content/images/AjaxLoadingIcon.gif" alt="chargement en cours..." class="ajaxloading" />');
                    $('#invoicelist').load('<%=Url.AjaxInvoiceListHref() %>', { viewName: 'smalllist.ascx', StatusId: 1, Size: 5 }, function(html) {
                    $('#invoicelist').html(html);
                    });
                    
                });
                
            </script>
        
            <div class="compte-list">
                <div class="bloc_type_prod" id="quotelist">
                </div>
                <div class="bloc_type_prod" id="orderlist">
                </div>
                <div class="bloc_type_prod" id="invoicelist">
                </div>
            </div>
            
            <br class="clear" />
    
	</div>
</div>
</asp:Content>