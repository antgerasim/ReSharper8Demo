<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ProductList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<%=Html.MetaInformations()%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!-- colonne 1 //-->
<div id="grid1">
<%Html.ShowProductCategoryListByProductList("CategoriesBySearchSubNav.ascx", Model); %>
</div>
<!-- fin colonne 1 //-->
<div id="grid2">
    <div id="sgrid21">
			<%Html.RenderPartial("~/views/Shared/RightMenu.ascx");%>
            <% Html.ShowProductList(ProductListType.Promotional, "Components/Promotions_verticales_v1/ProductsDayPromotion.ascx",1); %>
            <% Html.ShowProductList(ProductListType.New, "Components/Promotions_verticales_v1/ProductsDayNew.ascx",1); %>
            <% Html.ShowProductList(ProductListType.Destock, "Components/Promotions_verticales_v1/ProductsDayDestock.ascx",1); %> 
    </div>
    <div id="sgrid22">
        <h2 class="titleh2">
        	<span>
            	Marque : <%=Model.Brand.Name%> <%=Model.ItemCount %> Produit<%=Model.ItemCount > 1 ? "s" : "" %>
            </span>
        </h2>
        
        <div class="chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><a href="<%=Url.BrandsHref()%>" title="Toutes les marques">marques</a></span><b><%=Model.Brand.Name%></b>
            <div class="logo_marque">
            <% if (Model.Brand.DefaultImage != null) { %>
                <% if (Model.Brand.ExternalBrandLink.IsNullOrTrimmedEmpty()) { %>
                    <img src="<%=Url.ImageSrc(Model.Brand, 100, "") %>" alt="<%=Model.Brand.Name%>" />
                <% } else { %>
                    <a href="<%=Model.Brand.ExternalBrandLink%>" target="_blank" rel="nofollow">
                        <img src="<%=Url.ImageSrc(Model.Brand, 100, "") %>" alt="<%=Model.Brand.Name%>" />
                    </a>
                <% } %>
            <% } %>
            </div> 
        </div> 
        
        <% Html.ShowPager(Model); %>
        <% Html.RenderPartial("ProductList",ViewData.Model); %>
        <% Html.ShowPager(Model); %>
    </div>
</div>
</asp:Content>


