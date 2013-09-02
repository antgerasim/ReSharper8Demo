<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<SearchTerm>>" %>
<% if (Model != null) { %>
<div class="bloc clouds">
    <h2 class="titleh2">
        <span>
             TOP recherches
        </span>
    </h2>
    <ul>
    <% foreach (var item in Model.OrderBy(i => i.Name)) { %>  
        <li class="cloud cloud<%=item.Level%>"><a href="<%=Url.Href(item)%>" title="<%=item.Name%>" class="acloud<%=item.Level%>"><%=item.Name%></a></li>
    <% } %>
    <li class="clear"></li>
    </ul>
</div>
<% } %>

