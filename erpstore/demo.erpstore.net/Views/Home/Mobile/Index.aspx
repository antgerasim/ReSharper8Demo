<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Mobile.Master" Inherits="System.Web.Mvc.ViewPage<WebSiteSettings>" %>
<%@ Import Namespace="ERPStore.Mobile.Html" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
    <title><%=Model.SiteTitle%></title>
    <%=Html.MetaDescription(Model.HomeMetaDescription) %>
    <%=Html.MetaKeywords(Model.HomeMetaKeywords) %>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<%Html.RenderPartial("searchbox");%> 
    <table width="<%=Request.Browser.ScreenPixelsWidth%>"> 
		<tr>
			<td>Panier de commande</td>
		</tr>
    </table>
    <%=Html.MobileLabel("ceci est un test") %>
</asp:Content>

