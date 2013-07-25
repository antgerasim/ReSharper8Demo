<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Product>>" %>

<% foreach(var item in ViewData.Model) { %>

<div class="prod prodligne<%=Model.ColumnIndex(item,2)%>">
	<div class="prodcontent">
        <div class="prodimginfos">
               <a href="<%=Url.Href(item)%>" title="<% =item.Title %>"><% =Html.ProductImage(item, 90, "/content/images/prodvignette.gif")%></a>
        </div>
        <div class="prodinfos">
            <h3>
                <a href="<%=Url.Href(item)%>" title="<% =item.Title %>"><% =item.Title %></a>
            </h3>
			<%Html.ShowProductPrice(item);%>
			<div id="psi=<%=Model.Code%>"></div>
            <div class="prodcontentbottom">
            <a class="prodajout" href="#" onclick="javascript:AddToCart('<%=item.Code%>')"  title="ajouter : <% =item.Title %>">
            	<img src="/content/images/btlistcaddie.gif" alt="ajouter au panier : <% =item.Title %>" />
            </a>
            </div>
        </div>
   </div>
 </div>
<% } %>
