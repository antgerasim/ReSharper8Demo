<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<% /* 
<p class="prix barre corner"><=Model.SalePrice.Value.ToCurrency()></p>
*/ %>
<p class="prix customer corner"><b><%=Model.BestPrice.IntegerPart%></b>,<%=Model.BestPrice.DecimalPart%> <small>€</small></p>
<% if (Model.SaleUnitValue > 1) { %>
<p class="prix quantite corner">pour <%=Model.SaleUnitValue%></p>
<% } %>
<% if (Model.Packaging.Value > 1) {  %>
<p class="prix pack corner">vendu par <%=Model.Packaging.Value%></p>
<% } %>
<% /* 
 <p>Remise : <=Model.CustomerDiscount.ToString("P2")></p>
 
*/ %>