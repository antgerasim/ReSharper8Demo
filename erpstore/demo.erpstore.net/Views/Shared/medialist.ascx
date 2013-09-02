<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Media>>" %>
<% if (Model.IsNotNullOrEmpty()) { %>
<div class="docs">
	<h3>documents associés : </h3>
	<% foreach (var item in Model) { %>
		<li>
		<% if (!item.ExternalUrl.IsNullOrTrimmedEmpty()) { %>
		<a class="docs_ico" href="<%=item.ExternalUrl%>" target="_blank" title="">
        	<img src="<%=item.IconeSrc%>" alt="voir le document" />
            <br/>
            <small><%=item.FileName%></small>
		<br/>
        </a>
		<% } else {  %> 
		<a class="docs_ico" href="<%=item.Url%>" title="télécharger le document <%=item.FileName%>" target="_blank">
        	<img src="<%=item.IconeSrc%>" alt="télécharger le document <%=item.FileName%>" />
            <br/>
            <small><%=item.FileName%></small>
            <% if (item.FileSize != "0.00 Ko") { %>
<%--            <br/>
            <span><small><%=item.FileSize%></small></span>
--%>            <% } %>
        </a>
        <% } %>
        </li>
	<% } %>
    </div>
<% } %>