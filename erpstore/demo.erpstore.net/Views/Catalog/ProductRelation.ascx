<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ProductList>" %>
<h3>
<% switch (Model.RelationType)
   {
	   case ProductRelationType.Similar:
			%>Les produits similaires<%	   
		   break;
	   case ProductRelationType.Complementary:
		   %>Les produits complémentaires<%
		   break;
	   case ProductRelationType.Variant:
		   %>Les variations<%
		   break;
	   case ProductRelationType.Substitute:
		   %>Les produits substituables<%
		   break;
	   default:
		   break;
   } %>
<h3>
<%Html.RenderPartial("productList", Model);%>
