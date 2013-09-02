<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IList<Serialcoder.ERPStore.Models.Product>>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<%=Html.MetaInformations()%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!-- colonne 1 //-->
<div id="grid1">
<%Html.RenderPartial("CategoriesSubNav", Model.Category.Children);%>
<%Html.ShowBrandListByProductCategory(Model.Category, "BrandSubNav.ascx");%>
</div>
<!-- fin colonne 1 //-->
<div id="grid2">
    <div id="sgrid21">
		<%Html.RenderPartial("~/views/Shared/RightMenu.ascx");%>
		<%Html.RenderPartial("~/views/Shared/RightMenu2.ascx");%>
    </div>
    <div id="sgrid22">
        <h2 class="titleh2">
        	<span>
            <%=Model.Category.Name%> <%=Model.ItemCount%> produit(s)
            </span>
        </h2>
        <%Html.RenderPartial("breadcrumb", Url.GetBreadcrumb(Model.Category));%>
        <% Html.ShowPager(Model); %>
        <% Html.RenderPartial("ProductList",ViewData.Model); %>
        <% Html.ShowPager(Model); %>
    </div>
</div>
</asp:Content>

