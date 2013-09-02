<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Order>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Commande <%=ERPStoreApplication.WebSiteSettings.Contact.CorporateName%> N°<%=Model.Code%> </title>
    <script src="/scripts/MicrosoftAjax.js" type="text/javascript"></script>
    <script src="/scripts/MicrosoftMvcAjax.js" type="text/javascript"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
        <%Html.RenderPartial("~/views/account/managemenu.ascx", Model.User);%>
        
        <div class="bloc texte">
            <div class="corner bloc_type_liste">
            <h2 class="titleh2"><span>Gestion de votre commande</span></h2>
                <ul>
                    <li><a href="<%=Url.EditOrderHref(Model)%>">modifier la commande</a></li>
                </ul>
            </div>
        </div>
        
        <%Html.ShowWorkflow("~/views/account/workflow.ascx", Model);%>
           
        <div class="bloc texte">
            <div class="corner bloc_type_liste">
            <h2 class="titleh2"><span>Liste des documents attachés</span></h2>
            <p>
                <%Html.ShowOrderDocuments(Model);%>
            </p>
            </div>
        </div> 
           
    </div>
    
	<div id="sgrid32">
          <h2 class="titleh2">
              <span>Votre compte</span>
          </h2>
         <div class="bloc chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span><a href="<%=Url.AccountHref()%>"><span>tableau de bord</span></a> <a href="<%=Url.OrderListHref()%>"><span>Commandes</span></a> <b>Commande N° <%=Model.Code%> du <%=Model.CreationDate.ToString("dddd dd MMMM yyyy")%></b>
         </div>
         <div class="ssgrid321">
		<%Html.RenderPartial("~/views/account/order/orderdetail.ascx", Model);%>
        <%Html.RenderPartial("~/views/account/order/commentlist.ascx", Model);%>
        </div>
	</div>
</div>

</asp:Content>

