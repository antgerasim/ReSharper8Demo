<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<WebSiteSettings>" %>
<h1><%=Context.Items["OptimizedH1"]%></h1>
<!-- header //-->
<div id="header">
	<div class="sheader">
        <div class="logo">
            <a href="<%=Url.HomeHref()%>" title="<%=Model.Sloggan%>"></a>
        </div>
        <%Html.RenderPartial("~/views/Account/Rightinfos.ascx");%>
    </div>
</div>
<div id="infosheader">
	<div class="sinfosheader">
        <div class="titre-categories"><h2><a href="<%=Url.ProductCategoriesHref() %>" title="toutes les catégories">Catégories</a></h2></div>
        <% Html.RenderPartial("SearchBox"); %>
    </div>
</div>
<!-- fin header //-->