<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<%--<div class="promo_pourcent"><small>&nbsp;-&nbsp;<%=Model.PromotionnalDiscount.ToString("P")%></small></div>--%>
<p class="prix actuel corner">
	<small><small><span class="barre"><%=Model.SalePrice.Value.ToCurrency()%></span></small></small> 
	<b><%=Model.PromotionalPrice.IntegerPart%>,<small><%=Model.PromotionalPrice.DecimalPart%></small></b> €
</p>
