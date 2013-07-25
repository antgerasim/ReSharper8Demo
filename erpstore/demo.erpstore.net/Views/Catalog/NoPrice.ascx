<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<p class="prix first corner">Nous consulter</p>
<% if (Model.Packaging.Value > 1) {  %>
<p class="prix pack corner">vendu par <%=Model.Packaging.Value%></p>
<% } %>
