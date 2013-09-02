<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<% Html.BeginERPStoreRouteForm("ProductSearch", FormMethod.Get, new { name = "searchform" }); %>
Recherche :<br />
<input type="text" name="s" class="textsearch" id="schbox" value="<%=Request["s"]%>" size="40"/>
<input type="submit" class="btsearch" id="" value="Go"/>
<% Html.EndForm(); %>



