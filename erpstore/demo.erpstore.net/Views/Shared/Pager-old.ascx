<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<div class="pager">
<%=Html.FirstPageLink("<< Premier")%>
&nbsp;|&nbsp;
<%=Html.PreviousPageLink("< Precedent")%>
&nbsp;<span>&nbsp;
Page : <b><%=(int)ViewData["PageIndex"]%></b> / <%=Math.Ceiling((int)ViewData["ItemCount"] / (ERPStoreApplication.WebSiteSettings.Catalog.PageSize * 1.0))%>
&nbsp;</span>&nbsp;
<% =Html.NextPageLink("Suivant >") %>
&nbsp;|&nbsp;
<% =Html.LastPageLink("Dernier >>") %>
</div>
