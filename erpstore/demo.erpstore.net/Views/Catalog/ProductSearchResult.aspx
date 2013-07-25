<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ProductList>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Résultat de la recherche</title>
	<style type="text/css" media="screen">
		.hls { background: #D3E18A; }
	</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<!-- colonne 1 //-->
<div id="grid1">
    <%Html.ShowProductCategoryListByProductList("CategoriesBySearchSubNav.ascx", Model); %>
    <hr />
    <%Html.ShowBrandListByProductList("BrandSubNav.ascx", Model); %>
</div>
<!-- fin colonne 1 //-->
<div id="grid2">
    <div id="sgrid21">
		<%Html.RenderPartial("~/views/Shared/RightMenu.ascx");%>
        <% Html.ShowTopSearchTermList("searchcloud.ascx", 30); %>
    </div>
    <div id="sgrid22">
       <h2 class="titleh2">
           <span>
           Résultat(s) de votre recherche  
           <%=Html.Encode(ViewData["Query"])%>.
           <%=Model.ItemCount%> produits(s) trouvé(s).
           </span>
       </h2>
        
    <%Html.RenderPartial("breadcrumb", Url.GetBreadcrumb(Model.Category));%> <%=Html.Encode(ViewData["Query"])%>
        
	<% Html.ShowPager(Model); %>
    <% Html.RenderPartial("ProductList",ViewData.Model); %>
    <% Html.ShowPager(Model); %>

  <script type="text/javascript">
  	jQuery.fn.extend({
  		highlight: function(search, insensitive, hls_class) {
  			var regex = new RegExp("(<[^>]*>)|(\\b" + search.replace(/([-.*+?^${}()|[\]\/\\])/g, "\\$1") + ")", insensitive ? "ig" : "g");
  			return this.html(this.html().replace(regex, function(a, b, c) {
  				return (a.charAt(0) == "<") ? a : "<strong class=\"" + hls_class + "\">" + c + "</strong>";
  			}));
  		}
  	});

  	jQuery(document).ready(function($) {
  		$("#grid2").highlight('<%=Request["s"]%>', 1, "hls");
  	});
  </script>
    </div>
</div>


</asp:Content>

