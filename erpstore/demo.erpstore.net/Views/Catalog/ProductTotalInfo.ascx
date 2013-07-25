<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
	<div class="prodcontent" id="prod<%=Model.Code.GetHashCode()%>">
        <div class="prodimginfos sprodcontent">
                <a href="<%=Url.Href(Model)%>" title="<%=Html.Encode(Model.Title)%>">
					<% =Html.ProductImage(Model, 90, "/content/images/prodvignette.gif")%>
                </a>
        </div>
        <div class="prodcategory sprodcontent">
		<% if (Model.Category != null) { %>
        		<p><a href="<%=Html.Href(Model.Category)%>"><%=Model.Category.Name%></a></p>
		<% } %>
        </div>
        <div class="prodmarque sprodcontent">
		<% if (Model.Brand != null) { %>
        		<p><a href="<%=Html.Href(Model.Brand)%>"><%=Model.Brand.Name%></a></p>
		<% } %>
        </div>
        <div class="prodinfos sprodcontent">
            <h3>
                <%=Html.ProductLink(Model, Model.Title) %>
                <br />
                <span>REF : <%=Model.Code%></span>
            </h3>
        </div>
        <div class="prodprix sprodcontent">
        	<%Html.ShowProductPrice(Model);%>
        </div>
        <div class="proddesc sprodcontent">
        	<p><%=Model.ShortDescription %></p>
        </div>
   </div>

