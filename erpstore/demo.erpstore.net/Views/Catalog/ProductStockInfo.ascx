<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProductStockInfo>" %>
<% if (Model == null) { %>
	<p><small>Nous contacter</small></p>
<% } else { %>
	<p>
	<% if (Model.AvailableStock == 0) { %> 
		<small><%=Model.Disponibility%></small>
	<% } else if (Model.AvailableStock <= Model.MinimalQuantity) { %>
		<img src="/content/images/ico_stock4.png" alt="<%=Model.Disponibility%>"/><br />
		<small>plus que <%=Model.AvailableStock%> dispo.</small>
	<% } else { %> 
		<img src="/content/images/ico_stock1.png" alt="<%=Model.Disponibility%>"/><br />
		<small><%=Model.Disponibility%></small>
	<% } %> 
	</p>
<% } %>