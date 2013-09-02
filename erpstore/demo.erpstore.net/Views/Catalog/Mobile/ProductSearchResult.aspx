<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<ProductList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Résultat de la recherche</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%Html.RenderPartial("searchbox");%>
<% Html.ShowPager(Model, "mobile/pager"); %>
<% Html.RenderPartial("Mobile/ProductList",ViewData.Model); %>
<% Html.ShowPager(Model, "mobile/pager"); %>

</asp:Content>

