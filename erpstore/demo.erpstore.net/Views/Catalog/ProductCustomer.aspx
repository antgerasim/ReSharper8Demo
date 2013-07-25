<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ProductList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Résultat de la recherche</title>
	<style type="text/css" media="screen">
		.hls { background: #D3E18A; }
	</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
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
        <h2 class="titleh2"><span>Les produits déjà commandés</span></h2>
        
		<% Html.ShowPager(Model); %>
        <% Html.RenderPartial("ProductList",ViewData.Model); %>
        <% Html.ShowPager(Model); %>
    </div>
</div>
</asp:Content>
