<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IList<ProductCategory>>" %>

<script runat="server" language="c#">
	void ShowSubCategories(ProductCategory category)
	{
		if (category.Children == null)
		{
			return;
		}
        Response.Write("<ul class=\"prodlistscat\">");
		foreach (var subcategory in category.Children)
		{
			Response.Write(string.Format("<li>{0}&nbsp;({1})</li>", Html.ProductCategoryLink(subcategory), subcategory.DeepProductCount));
			if (subcategory.Children != null && subcategory.Children.Count > 0)
			{
				ShowSubCategories(subcategory);
			}
		}
		Response.Write("</ul>");
	}
	
</script>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<%=Html.MetaInformations()%>
    <style type="text/css">
	h3.prod_category{ background-color:#FFCF11; font-size:103%;}
	h3.prod_category img{ vertical-align:middle; margin-right:1em;}
	</style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
    <div id="sgrid31">
			<%Html.RenderPartial("~/views/Shared/RightMenu.ascx");%>
             <!-- bloc 2 //-->
             <% Html.ShowProductList(ProductListType.Destock, "Components/Promotions_verticales_v1/ProductsDayDestock.ascx",3); %>
             <!-- End  bloc 2 //-->
 			<%Html.RenderPartial("~/views/Shared/RightMenu2.ascx");%>
    </div>
    <div id="sgrid32">
          <h2 class="titleh2">
              <span>Les catégories</span>
          </h2>
          <div class="bloc chemin">
              <span><a href="<%=Url.HomeHref()%>">accueil</a></span>
              <b>Les catégories</b>
          </div>
          <div class="form_elements">
            <% foreach (var category in Model.OrderBy(i => i.Name)) { %>
            <h3 class="prod corner prod_category">
                <span>
					<% if (category.DefaultImage != null) { %>
					<img src="<%=Url.ImageSrc(category, 0, "") %>" alt="<%=category.Name%>" />
					<% } %> 
					<a href="<%=Url.Href(category)%>"><%=category.Name%></a>&nbsp;(<%=category.DeepProductCount%>)
				</span>
            </h3>
            <%	ShowSubCategories(category); %>
            <% } %>
            </div>
      </div>
</div>

</asp:Content>


