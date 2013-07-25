<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ProductList>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<%=Html.MetaInformations()%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<!-- colonne 1 //-->
<div id="grid1">
<%Html.ShowProductCategoryListByProductList("CategoriesBySearchSubNav.ascx", Model); %>
<%Html.ShowBrandListByProductList("BrandSubNav.ascx", Model); %>
</div>
<!-- fin colonne 1 //-->
<div id="grid2">
    <div id="sgrid21">
		<%Html.RenderPartial("~/views/Shared/RightMenu.ascx");%>
		<%Html.RenderPartial("~/views/Shared/RightMenu2.ascx");%>
    </div>
    <div id="sgrid22">
        <h2 class="titleh2"><span>Promotions</span></h2>
         <div class="bloc chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span> 
             <b>Promotions (<%=Model.ItemCount%> produit<%=(Model.ItemCount > 1) ? "s" : ""%>)</b>
        </div> 
		<% Html.ShowPager(Model); %>
        <% Html.RenderPartial("ProductList",ViewData.Model); %>
        <% Html.ShowPager(Model); %>
    </div>
</div>
</asp:Content>


