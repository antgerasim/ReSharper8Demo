<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<IList<Brand>>" %>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
<%=Html.MetaInformations()%>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
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
              <span>Les marques</span>
          </h2>
          <div class="bloc chemin">
              <span><a href="<%=Url.HomeHref()%>">accueil</a></span>
              <b>Les marques</b>
          </div>
          <div class="bloc bloc_home">  
			<% foreach (var brand in Model.OrderBy(i => i.DefaultImage != null ? 0 : 1).ThenBy(i => i.Name))	{	%>
            <!-- produit //-->
            <div class="prod corner prod_marque">
                <div class="prodcontent">
                    <h3>
                    <a href="<%=Url.Href(brand) %>"><%=brand.Name%> (<%=brand.ProductCount%>)</a>
                    <br/>
<%--                    <small><a href="<%=brand.ExternalBrandLink%>" title="<%=brand.Name %>" target="_blank">visiter leur site Internet</a></small>
--%>                    </h3>
                    <div class="prodimginfos">
                    <a href="<%=Url.Href(brand)%>" title="">
                     <%=Html.BrandImage(brand, 140,140, "/content/images/vignette140.png")%>
                    </a>
                    </div>
                 </div>
            </div>
            <!-- fin produit //--> 
            <% } %>
            </div>
    	</div>
    </div>
</div>

</asp:Content>



