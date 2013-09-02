<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<p class="prix actuel corner"><b><%=Model.SalePrice.IntegerPart%>,<small><%=Model.SalePrice.DecimalPart%></small></b> €</p>
