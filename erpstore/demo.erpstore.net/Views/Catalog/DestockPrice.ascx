<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<% if (Model.DestockPrice != null) { %>
<p class="prix first corner"><small><small><span class="barre"><%=Model.SalePrice.Value.ToCurrency()%></span></small></small> <b><%=Model.BestPrice.IntegerPart%>,<small><%=Model.BestPrice.DecimalPart%></small></b> €</p>
<% } else { %>  
<p class="prix first corner"><b><%=Model.SalePrice.IntegerPart%>,<small><%=Model.SalePrice.DecimalPart%></small></b> €</p>
<% } %> 
