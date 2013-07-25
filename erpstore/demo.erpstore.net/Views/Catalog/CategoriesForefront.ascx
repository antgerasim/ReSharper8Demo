<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<ProductCategory>>" %>
<% var list = Model.Where(i => i.IsForefront)
					.OrderBy(i => i.FrontOrder)
					.ThenBy(i => i.Name); %>
					
<% foreach (var item in list) { %>
	<!-- produit //-->
	<div class="prod front corner prodcol<%=Model.ColumnIndex(item,3)%>" id="">
        <div class="prodcontent">
           <% Html.ShowHeadProductOfCategory(item); %>
       </div>
	</div>
	<!-- fin produit //--> 
<% } %>
