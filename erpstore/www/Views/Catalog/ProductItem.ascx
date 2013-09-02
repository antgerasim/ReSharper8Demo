<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<div id="p_1" class="gridItem" onmouseover="enlarge(this);" onmouseout="enlarge(this);">
    <div class="frame">
        <img src="<%=Url.ImageSrc(Model, 0, "")%>" alt="<%=Model.Title%>" />
    </div>
    <div class="content">
        <div class="price"><%=Html.ShowPrice(Model)%></div>
        <div class="addtocart">
			<a href="javascript:AddToCart('<%=Model.Code%>')"><img src="Content/images/AddToCart.png" alt="" /></a>
		</div>
        <div class="name"><a href="<%=Url.Href(Model) %>" ><%=Model.Title%><img src="defaultIcon.jpg"></a></div>
        <div class="desc"><%=Model.LongDescription%></div>
    </div>
</div>


