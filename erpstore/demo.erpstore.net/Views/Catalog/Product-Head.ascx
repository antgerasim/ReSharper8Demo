<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<h3>
   <a href="<%=Url.Href(Model)%>" title="<%=Html.Encode(Model.Title)%>"><%=Model.Title.EllipsisAt(50)%></a>
</h3>
<div class="prodinfos">
	  <%Html.ShowProductPrice(Model);%>
</div>
<div class="prodimginfos" style=" clear:both;">
	<a href="<%=Url.Href(Model)%>" title="<%=Html.Encode(Model.Title)%>"><img src="<%=Url.ImageSrc(Model, 140,140, "/content/images/vignette140.png")%>" alt=""/></a>
</div>

