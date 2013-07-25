<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<% var elmId = Guid.NewGuid().ToString().Substring(0, 6); %>
<div class="dispo" id="psi-<%=Model.Code %>"><small></small></div>
<% if (Model.SaleUnitValue > 1) { %>
<p class="prix quantite corner">tarif pour <%=Model.SaleUnitValue%></p>
<% } %>
<% if (Model.Packaging.Value > 1) {  %>
<p class="prix pack corner">vendu par <%=Model.Packaging.Value%></p>
<% } %> 

