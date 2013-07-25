<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<% if (!ViewData.ModelState.IsValid) { %>
<div class="notes errors">
	<strong><img src="/content/images/icon_error.png" alt=""/> Attention !</strong>
	<% foreach (var item in ViewData.ModelState.GetAllErrors()) { %>
    <p class="error"> 
        <span><%=item %></span>
    </p>
	<% } %>
</div>
<% } %>


