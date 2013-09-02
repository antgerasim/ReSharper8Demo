<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<p class="prix first corner"><small><small><span class="barre"><%=Model.MarketPrice.Value.ToCurrency()%></span></small></small> <b><%=Model.SalePrice.IntegerPart%>,<small><%=Model.SalePrice.DecimalPart%></small></b> €</p>
