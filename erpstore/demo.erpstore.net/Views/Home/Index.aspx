<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<WebSiteSettings>" %>

<asp:Content ID="indexHead" ContentPlaceHolderID="HeaderContent" runat="server">
    <title><%=Model.SiteTitle%></title>
    <%=Html.MetaDescription(Model.HomeMetaDescription) %>
    <%=Html.MetaKeywords(Model.HomeMetaKeywords) %>
    <%=ERPStoreApplication.WebSiteSettings.HomeOthersMetas %>
    <link type="text/css" href="/content/une.css" rel="stylesheet"/>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
	<!-- colonne 1 //-->
    <div id="grid1">
    <% Html.ShowProductCategories("categories.ascx"); %>
    <%Html.RenderPartial("~/views/Shared/RightMenu2.ascx");%>
    </div>
	<!-- fin colonne 1 //-->
	<div id="grid2">
        <div id="sgrid21">
			 <%Html.RenderPartial("~/views/Shared/RightMenu.ascx");%>
             <!-- bloc 2 //-->
             <% Html.ShowProductList(ProductListType.Promotional, "Components/Promotions_verticales_v1/ProductsDayPromotion.ascx",3); %>
             <!-- End  bloc 2 //-->
        </div>
        <div class="bloc bloc_home" id="bloc1">
            <h2 class="titleh2"><span>Commandez !</span></h2>
            <% Html.ShowProductCategories("CategoriesForefront.ascx"); %>
        </div>
    </div>
    <% Html.ShowBrandListForefront("BrandListFooter.ascx"); %>
</asp:Content>
