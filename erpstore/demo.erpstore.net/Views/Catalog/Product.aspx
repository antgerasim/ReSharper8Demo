<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<Product>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<%=Html.MetaInformations()%>
    <script type="text/javascript" src="/content/zoombox/zoombox.js"></script>
    <link href="/content/zoombox/zoombox.css" rel="stylesheet" type="text/css" media="screen" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
   
    <div class="bloc"> 
          <h2 class="titleh2 titleproduct">
          	<span><%=Model.Title%></span>
          </h2>
    </div>
    
	<div class="product">
         
        <div class="productcol1">
        	<div class="productimage">
<%--				 <% if (Model.PromotionnalDiscount > 0) { %>
                 <div class="etiquette_promo"> - <%=Model.PromotionnalDiscount.ToString("P")%></div>
	             <% } %>
--%>                <div class="fullsize">
	                <%Html.ShowPictureList(Model, 30, 250, 0, "productpicturelist.ascx");%>
	                <% var title = Html.Encode(Model.Title.Replace("\"", "''")); %>
	                <a href="<%=Url.ImageSrc(Model, 0,0, "/content/images/prodvignettebig.gif")%>" rel="zoombox[catalogue]" title="<%=title%>" target="_blank">
						<img src='<%=Url.ImageSrc(Model, 250,250, "/content/images/prodvignettebig.gif")%>' alt="<%=title%>" id="productPicture" />
                    </a>
                 </div>
             </div>
         <%Html.ShowAttachmentList(Model);%> 
         </div>

          <div class="productcol2">
                	<h3>Quantité</h3>
                    <p>
                    <br/>
					<button id="addtocartqtydec">-</button><input type="text" value="<%=Model.Packaging.Value%>" class="qte" id="addtocartqty"/><button id="addtocartqtyinc">+</button>
					</p>
                    <div class="prodcaddie">
                        <a href="#" class="open_cart" title="ajouter au panier : <%=Html.Encode(Model.Title)%>" id="addtocart|<%=Model.Code%>">
                           <img src="/content/images/btcaddieproduct.jpg" alt="ajouter au panier : <%=Html.Encode(Model.Title)%>" />
                        </a>
                   </div>
                    <div class="proddevis">
                        <a class="quotecart" href="#" title="ajouter au devis : <%=Html.Encode(Model.Title)%>" id="addtoquotecart|<%=Model.Code%>">
                           <img src="/content/images/btquoteproduct.jpg" alt="ajouter au devis : <%=Html.Encode(Model.Title)%>" />
                        </a> 
                    </div>
           </div>
         
           <div class="productcol3">
                    <h3>Notre Prix</h3>
                    <div class="productprix">
                    <% if (Model.IsCustomerPrice && Model.CustomerPrice != null && Model.CustomerPrice.Value > 0) { %>
						<p>Tarif client</p>
                        <p class="prix barre"><%=Model.SalePrice.Value.ToCurrency()%></p>
                        <p class="prix first"><b><%=Model.CustomerPrice.IntegerPart%>,<small><%=Model.CustomerPrice.DecimalPart%></small></b> € <small>HT</small></p>
                     <% } else if (Model.IsPromotion && Model.PromotionalPrice != null && Model.PromotionalPrice.Value > 0) { %>
                        <p class="prix barre"><%=Model.SalePrice.Value.ToCurrency()%></p>
                        <p class="prix first"><b><%=Model.PromotionalPrice.IntegerPart%>,<small><%=Model.PromotionalPrice.DecimalPart%></small></b> € <small>HT</small></p>
                     <% } else if (Model.IsDestock && Model.DestockPrice != null && Model.DestockPrice.Value > 0) { %>
                        <p class="prix barre"><%=Model.SalePrice.Value.ToCurrency()%></p>
                        <p class="prix actuel"><b><%=Model.DestockPrice.IntegerPart%>,<small><%=Model.DestockPrice.DecimalPart%></small></b> € <small>HT</small></p>
                      <% } else if (Model.IsFirstPrice && Model.MarketPrice != null && Model.MarketPrice.Value > 0) { %>
                        <p class="prix barre"><%=Model.MarketPrice.Value.ToCurrency()%></p>
                        <p class="prix actuel"><b><%=Model.SalePrice.IntegerPart%>,<small><%=Model.SalePrice.DecimalPart%></small></b> € <small>HT</small></p>
                      <% } else { %>
                        <p class="prix actuel"><b><%=Model.SalePrice.IntegerPart%>,<small><%=Model.SalePrice.DecimalPart%></small></b> € <small>HT</small></p>
					  <% } %>
                    
                       <% if (Model.SaleUnitValue > 1) { %>
                        <p class="prix quantite"><small>tarif pour <%=Model.SaleUnitValue%></small></p>
                        <% } %>
						<% if (Model.Packaging.Value > 1) {  %>
                        <p class="prix quantite"><small>vendu par <%=Model.Packaging.Value%></small></p>
                        <% } %>
                        <%Html.RenderPartial("ProductCaracter", Model);%>
                    </div>
                    <h3>
                     	Informations
                    </h3>
                    <ul>
                       	<li class="dispo"><strong>Disponibilité :</strong> <b><%=Html.GetDisponibility(Model)%></b></li>
                        <li><strong>Conditionnement : </strong>
							<b>
								<% if (Model.Packaging.Value == 1) { %>
								à l'unité
								<% } else { %> 
								par <%=Model.Packaging.Value %>
								<% } %> 
							</b>
						</li>
                        <li><strong>Référence : </strong><b><%= Html.Encode(Model.Code) %></b></li>
                         <% if (Model.RecyclePrice.Value != 0) { %>
                        <li><strong><img src="/content/images/puce_ecotaxe.png" alt="" align="absmiddle" /> eco taxe : </strong><b><%=Model.RecyclePrice.Value.ToCurrency() %></b></li>
                         <%}%>
						<% if (Model.PromotionnalDiscount > 0) { %>
									<li><strong>Remise : </strong><b><%=Model.PromotionnalDiscount.ToString("P")%></b></li>
						<% } %>
                        <% if (Model.ManufacturerUrl != null) { %>
						<li><strong>Constructeur : </strong><a href="<%=Model.ManufacturerUrl%>" target="_blank">voir la fiche</a></li>
                        <%}%>
                        <% if (Model.Weight > 0) { %>
						<li><strong>Poids : </strong><b><%=string.Format("{0:#,##}",Model.Weight)%> grammes</b></li>
                        <%}%>
						<% if (Model.Brand != null ) { %>
                        <li><strong>Catégorie : </strong><b><%=Html.ProductCategoryLink(Model.Category) %></b></li>
                        <li><strong>Marque :  </strong> <b><a href="<%=Url.Href(Model.Brand)%>"><%=Model.Brand.Name%></a></b></li>
                        <% } else { %>
                        <li><strong>Catégorie : </strong> <b><%=Html.ProductCategoryLink(Model.Category) %></b></li>
                        <% } %> 
                    </ul>
	        </div>
        
                    <br class="clear"/>
                     
                    <% if (Model.ShortDescription != null) { %>
						<% if (Model.LongDescription == null 
								|| Model.LongDescription.GetHashCode() != Model.ShortDescription.GetHashCode()) { %>
							<h3>Description</h3>
							<ul><li><%=Model.ShortDescription%></li></ul>
						<% } %> 
                    <% } %> 
	               	<% if (Model.StrengthsPoints != null) { %>
                    <h3 style="clear:both;">Points Forts</h3>
					<ul><li><%=Model.StrengthsPoints%></li></ul>
                    <%}%> 
                	<% if (Model.LongDescription != null) { %>
                    <h3>Présentation / Caractéristiques</h3>
					<ul><li><%=Model.LongDescription%></li></ul> 
                 	<% } %>
                    <% if (!Model.Keywords.IsNullOrTrimmedEmpty()) { %>
                    <ul><li><small><small>Mots clés : <%=Model.Keywords %></small></small></li></ul>  
                    <% } %>

        <div id="complementaryproductlist"></div>
        <div id="substituteproductlist"></div>
        <div id="similarproductlist"></div>
        <div id="variantproductlist"></div>

	</div>

</div>
<script type="text/javascript">

	$(function() {
		$('#complementaryproductlist').load('<%=Url.ComplementaryProductListHref() %>'
			, { viewName: 'productrelation.ascx'
				, productCode: '<%=Model.Code%>'
				, relationTypeName : '<%=ProductRelationType.Complementary.ToString() %>' }
			, function(html) {
				$('#complementaryproductlist')[0].value = html;
			}
		);

		$('#substituteproductlist').load('<%=Url.SubstituteProductListHref() %>'
			, { viewName: 'productrelation.ascx'
				, productCode: '<%=Model.Code%>'
				, relationTypeName : '<%=ProductRelationType.Substitute.ToString() %>' }
			, function(html) {
				$('#substituteproductlist')[0].value = html;
			}
		);
	     
		$('#similarproductlist').load('<%=Url.SimilarProductListHref()%>'
			, { viewName: 'productrelation.ascx'
				, productCode: '<%=Model.Code%>'
				, relationTypeName : '<%=ProductRelationType.Similar.ToString() %>' }
			, function(html) {
				$('#similarproductlist')[0].value = html;
			}
		);

		$('#variantproductlist').load('<%=Url.VariantProductListHref() %>'
			, { viewName: 'productrelation.ascx'
				, productCode: '<%=Model.Code%>'
				, relationTypeName : '<%=ProductRelationType.Variant.ToString() %>' }
			, function(html) {
				$('#variantproductlist')[0].value = html;
			}
		);
	});

	$(document).ready(function() {
		$("#addtocartqtyinc").click(function() {
			var qty = parseInt($("#addtocartqty").val());
			if (qty == null) {
				qty = 0;
			}
			qty += <%=Model.Packaging.Value%>;
			$("#addtocartqty").val(qty);
		});
		$("#addtocartqtydec").click(function() {
			var qty = parseInt($("#addtocartqty").val());
			if (qty == null) {
				qty = <%=Model.Packaging.Value%> * 2;
			}
			qty -= <%=Model.Packaging.Value%>;
			$("#addtocartqty").val(Math.max(1, qty));
		});
	});
</script>
</asp:Content>


