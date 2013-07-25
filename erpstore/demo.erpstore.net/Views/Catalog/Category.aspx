<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ProductList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<%=Html.MetaInformations() %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!-- colonne 1 //-->
<div id="grid1">
<% Html.ShowProductCategoryListForefront("categories.ascx"); %>
<% Html.ShowBrandListByProductList("BrandSubNav.ascx", Model);%>
</div>
<!-- fin colonne 1 //-->
<div id="grid2">
    <div id="sgrid21">
            <div class="menu2">
				<%Html.RenderPartial("~/views/Shared/RightMenu.ascx");%>
            </div>
            <% Html.ShowProductList(ProductListType.Promotional, "Components/Promotions_verticales_v1/ProductsDayPromotion.ascx",1); %>
            <% Html.ShowProductList(ProductListType.New, "Components/Promotions_verticales_v1/ProductsDayNew.ascx",1); %>
            <% Html.ShowProductList(ProductListType.Destock, "Components/Promotions_verticales_v1/ProductsDayDestock.ascx",1); %> 
            <%Html.RenderPartial("~/views/Shared/RightMenu2.ascx");%>
    </div>
    <div id="sgrid22">

        <h2 class="titleh2">
            <span>
                <%=Model.Category.Name%> <%=Model.ItemCount%> produit(s)
            </span>
        </h2>
        
        <%Html.RenderPartial("breadcrumb", Url.GetBreadcrumb(Model.Category));%>
        
        <br class="clear" />
        
        <% Html.ShowPager(Model); %>
        <% Html.RenderPartial("ProductList",ViewData.Model); %>
        <% Html.ShowPager(Model); %>
    </div>
</div>
</asp:Content>

