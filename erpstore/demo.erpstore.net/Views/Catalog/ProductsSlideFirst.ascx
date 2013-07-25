<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IList<Product>>" %>

<div class="bloc" id="content_slide">

    <span class="numSlide">
        <a class="slideOn corner" href="javascript:gotoSlide(0)">1</a>
        <a class="slideOff corner" href="javascript:gotoSlide(1)">2</a>
        <a class="slideOff corner" href="javascript:gotoSlide(2)">3</a>
        <a class="slideOff corner" href="javascript:gotoSlide(3)">4</a>
        <a class="slideOff corner" href="javascript:gotoSlide(4)">5</a>
    </span>
 
<% foreach(var item in ViewData.Model) { %>
	<% if (Model.IndexOf(item) == 0) { %>
    <!-- 1 -->
     <div class="visible" id="Slide<%=Model.IndexOf(item)%>">
     	<p class="Slideimg">
        <a href="<%=Url.Href(item)%>" title="<% =item.Title %>"><% =Html.ProductImage(item, 130, "/content/images/prodvignette.gif")%></a>
        </p>
        <h2><a class="lien_une" title="Lire l'article" href="<%=Url.Href(item)%>"><%=item.Title.EllipsisAt(50)%></a></h2>
        <h3><a class="lien_une" title="Lire l'article" href="<%=Url.Href(item)%>"></a></h3>
        <span class="chapo">
           <% =item.ShortDescription %>
        </span>
        <p class="bigprix1"><b><%=item.SalePrice.IntegerPart%></b>,<small><%=item.SalePrice.DecimalPart%> €</small><span><small> tarif pour <%=item.SaleUnitValue%></small></span></p>
        <p class="btn_liresuite">
           <a href="#" onclick="javascript:AddToCart('<%=item.Code%>')" title="ajouter au panier : <% =item.Title %>" class="open_card">Ajouter au panier</a>
        </p>
    </div>
   <!-- end 1 -->                    
   <%}%>
   <% else {%>
	<!-- 2 -->
    <div class="hidden">
     	<p class="Slideimg">
        <a href="<%=Url.Href(item)%>" title="<% =item.Title %>"><% =Html.ProductImage(item, 130, "/content/images/prodvignette.gif")%></a>
        </p>
        <h2><a class="lien_une" title="Lire l'article" href="#"><% =item.Title %></a></h2>
        <h3><a class="lien_une" title="Lire l'article" href="#"></a></h3>
        <span class="chapo">
        	<% =item.ShortDescription %>
        </span>
        <p class="bigprix1"><b><%=item.SalePrice.IntegerPart%></b>,<small><%=item.SalePrice.DecimalPart%> €</small><span><small> tarif pour <%=item.SaleUnitValue%></small></span></p>
        <p class="btn_liresuite">
        	<a href="#" onclick="javascript:AddToCart('<%=item.Code%>')" title="ajouter au panier : <% =item.Title %>" class="open_card">Ajouter au panier</a>
        </p>
     </div>
     <!-- end 2 -->
 	<%}%>

<% } %>
</div>    
<script type="text/javascript" src="/Scripts/mp_lib.js"></script>
<script type="text/javascript">
	setTimeout(slideshow,timer);
	blurAnchors();
 </script>