<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Product>" %>
<% var title = Html.Encode(Model.Title.Replace("\"", "''")); %>
<a  name="<%=Model.Code%>" class="prod_ancre"></a>
<div class="prodcontent" id="prod<%=Model.Code.GetHashCode()%>">
    <h3>
        <a href="<%=Url.Href(Model)%>" title="<%=title%>"><%=Html.Encode(Model.Title)%></a>
    </h3>
    <div class="prodprix">
        <%Html.RenderPartial("~/views/catalog/productdisponibility.ascx", Model);%>
        <%Html.ShowProductPrice(Model);%>
    </div>
    <div class="prodimginfos">
        <a href="<%=Url.Href(Model)%>" title="<%=title%>"><img src="<%=Url.ImageSrc(Model, 140,140, "/content/images/vignette140.png")%>" alt="<%=title%>"/></a>
    </div>
    <div class="prodinfos">
       <a href="#" title="ajouter au panier : <%=title%>" class="open_card" id="addtocart|<%=Model.Code%>">
           <img src="/content/images/addpanier.png" alt="ajouter au panier : <%=Html.Encode(Model.Title)%>" />
       </a>
       <a href="#" title="demande de devis : <%=title%>" class="quotecart" id="addtoquotecart|<%=Model.Code%>">
           <img src="/content/images/adddevis.png" alt="demande de devis : <%=Html.Encode(Model.Title)%>" />
       </a>
    </div>
</div>
