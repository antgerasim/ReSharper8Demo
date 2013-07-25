<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<div id="productitem">
	<p>
		<%= Html.Encode(Model.Title) %><br />
		<%= Html.Encode(Model.ShortDescription) %>
	</p>
	<%=Html.ProductImage(Model) %>
	<p>
		Dispo : <%=Html.GetDisponibility(Model)%>
	</p>
	<% if (Model.Category != null)
	{ %>
	<p>
		Categorie : <%=Html.ProductCategoryLink(Model.Category)%>
	</p>
	<% } %>
	<% if (Model.Brand != null)
	{ %>
	<p>
		Marque : <%=Model.Brand.Name%>
	</p>
	<% } %>
	<p>
		Tarif : <%= Model.SalePrice.ToCurrency() %>
		<% Html.BeginAddToCartRouteForm(); %>
		<input type="hidden" name="productCode" value="<%=Html.Encode(Model.Code)%>" />
		<input type="submit" value="Ajouter au panier" />
		<% Html.EndForm(); %>
	</p>
</div>