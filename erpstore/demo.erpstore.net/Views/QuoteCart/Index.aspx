<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<QuoteCart>" %>

<asp:Content ID="indexHead" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>Votre demande de devis</title>
    <%=Html.MetaDescription("Demande de devis")%>
    <style type="text/css">
		#productGrid 
		{
			margin-left : 0.5em;
			width : 98%;
		}
    </style>
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">

	<div id="sgrid33">
<%--          <h2 class="titleh2">
              <span>Votre demande de prix</span>
        </h2>
        <div class="chemin">
            <span><a href="/" title="accueil">accueil</a></span><b>Demande de prix</b>
        </div> --%>

    <% Html.BeginQuoteCartForm(); %> 
    <%Html.RenderPartial("ValidationSummary");%>
   
    <div class="form_elements form_devis corner" id="Div1">
		<fieldset class="form">
    	<legend>Votre devis personnalisé en moins d'une heure*</legend>
		<p><small><b>Simple, rapide, sans engagement.</b> Remplissez le formulaire ci-dessous pour obtenir par email  :
        <br/>
        <br/>
        Un devis détaillé et personnalisé avec prix, remises et délais de livraison
        <br/>
       	Les fiches techniques des produits sélectionnés.</small>
		</p>
		<% if (!User.Identity.IsAuthenticated) { %>
        <div>
        <p class="form_element_radio input">
            <label>
                <% =Html.RadioButton("PresentationId", 1)%>
                Monsieur
                <% =Html.RadioButton("PresentationId", 2)%>
                Mademoiselle
                <% =Html.RadioButton("PresentationId", 3)%>
                Madame
            </label>
            <br/>
        </p>
        <p>
            <label for="lastname">
                Nom:
            </label>
            <span class="form_element_input">
                <input type="text" value="<%=Model.LastName%>" name="lastname" /></span>
            <%= Html.ValidationMessage("LastName") %>
            
        </p>
        <p>
            <label for="firstName">
            	Prénom:
            </label>
            <span class="form_element_input">
                <%= Html.TextBox("firstname") %>
            <%= Html.ValidationMessage("firstname")%>
            </span>
        </p>
           <p class="form_element_error">
                <label for="email">
                    Adresse email:
                </label>
                <span class="form_element_input">
                    <input type="text" value="<%=Model.Email%>" name="email" /></span>
                    <%= Html.ValidationMessage("email") %>
             </p>
        </div>
        </fieldset>
        <div class="notes">
            <strong>
                <img src="/content/images/icon_noter.png" alt="" />
                A noter !
            </strong>
            <p class="note">
                 * aux heures d'ouverture des bureaux<br />
				Votre nom et votre adresse email (valide) sont obligatoires pour que nous puissions vous repondre.
            </p>
        </div>
        <% } %> 
        
    </div> 

     <table class="nav cols" id="productGrid">
    
        <thead>
            <tr class="entete-cols">
                <th class="col col-20 col1">Produit</th>
                <th class="col col-30 col2">Description</th>
                <th class="col col-20 col3">Disponibilité</th>
                <th class="col col-20 col4">Quantité.</th>
                <th class="col col-10 col5">Supprimer</th>
            </tr>
        <thead>

        <tbody>
		<% foreach (var item in Model.Items){ %>
         <tr class="prodligne<%=Model.Items.ColumnIndex(item, 2) %>">
            <td class="col col-20 col1">
                 <a href="<%=Url.Href(item.Product)%>" title="<%=Html.Encode(item.Product.Title)%>">
                    <img src="<% =Url.ImageSrc(item.Product, 140,140, "/content/images/vignette140.png")%>" alt="<%=Html.Encode(item.Product.Title)%>" />
                  </a>
            </td>        
            <td class="col col-30 col2 colg">
                    <b><% =Html.Encode(item.Product.Title) %></b>
                    <br />
                    <span>REF : <%=item.Product.Code%></span> 
                </h3>
            </td>
            <td class="col col-20 col3">
                <div id="psi-<%=item.Product.Code %>"></div>
            </td>
            <td class="col col-20 col4 form_element_radio">
                 <div class="modif_quantite">
						<a href="#" id="decButton|<%=Model.Items.IndexOf(item) %>|<%=item.Product.Packaging.Value %>"><img src="/content/images/icos/ico_moins.png" alt="moins" /></a>
						<% =Html.TextBox("quantity", item.Quantity, new { size = 3 , name="quantity", id= string.Format("qty{0}", Model.Items.IndexOf(item)) })%>
						<a href="#" id="incButton|<%=Model.Items.IndexOf(item) %>|<%=item.Product.Packaging.Value %>"><img src="/content/images/icos/ico_plus.png" alt="plus" /></a>
				</div> 
            </td>
            <td class="col col-10 col5">
                <p><a href="<%=Url.DeleteQuoteCartItemHref(Model.Items.IndexOf(item)) %>" title="supprimer"><img src="/content/images/poubelle.png" alt="supprimer"/></a></p>
            </td>
      </tr>
    <% } %>
	</tbody>

	</table> 
        
    <br class="clear" />

	<% if (!User.Identity.IsAuthenticated) { %>

	   <table class="go_commande cols">
		   <tr>
				<td class="col col-33 col1"> <span><a class="go_commande_no" href='<%=Model.LastPage%>'>Continuer ma consultation</a></span></td>
				<td class="col col-33 col2"> <span>&nbsp;</span></td>
				<td class="col col-33 col3"> <span> <input class="submit_devis" type="submit" value="Envoyer ma demande" /></span></td>
			</tr>
		</table>   

    <% } %> 
    
    <div class="form_elements form_devis corner" id="devis1">
	<fieldset class="form">    
    	<legend><a href="#">Vous pouvez ajouter des informations complémentaires (facultatives) pour obtenir une réponse plus précise</a></legend>
        <% if (!User.Identity.IsAuthenticated) { %>
        <p>
            <label for="corporatename">
                Société:
             </label>
            <span class="form_element_input">
                <%= Html.TextBox("corporatename")%> </span>
            <%= Html.ValidationMessage("corporatename") %>
           
        </p>
        <p class="form_element_error">
            <label for="phonenumber">
                N° Téléphone:
            </label>
            <span class="form_element_input">
                <%= Html.TextBox("phonenumber")%> </span>
            <%= Html.ValidationMessage("phonenumber") %>
           
        </p>
        <p class="form_element_error">
            <label for="faxnumber">
                N° Fax:
            </label>
            <span class="form_element_input">
            <%= Html.TextBox("faxnumber")%> </span>
            <%= Html.ValidationMessage("faxnumber")%>
           
        </p>
        <p>
            <label for="countryId">
                Pays :
            </label>
            <span class="form_element_input">
			<%= Html.DropDownList("countryId", Html.CountryList())%></span>
            <%= Html.ValidationMessage("countryId")%>
           
        </p>
        <p class="form_element_error">
            <label for="zipcode">
                Code Postal :
            </label>
            <span class="form_element_input">
                <input type="text" value="<%=Model.ZipCode%>" name="ZipCode" /></span>
            <%= Html.ValidationMessage("zipcode")%>
        </p>

        <div class="notes">
            <strong>
                <img src="/content/images/icon_noter.png" alt="" />
                A noter !
            </strong>
            <p class="note">
            	En indiquant votre code postal, nous pourrons calculer <b>au plus juste les frais de port</b>.
            </p>
        </div> 
        <% } %> 
        <p class="form_element_error">
            <label for="message">
                Message :
             </label>
        <span class="form_element_input">
            <%=Html.TextArea("message", Model.Message)%>
        </span>
        </p>
        <p class="form_element_error">
            <label for="documentReference">
                Référence :
            </label>
            <span class="form_element_input">
                <%=Html.TextBox("documentReference")%>
            </span>
        </p>
        </fieldset>
        <div class="notes">
            <strong>
                <img src="/content/images/icon_noter.png" alt="" />
                A noter !
            </strong>
            <p class="note">
                Vous pouvez ajouter votre référence. Cette référence apparaitra
                sur tous les documents (Commande, Livraison, Facture)...
            </p>
        </div>
       

    </div>

   <table class="go_commande cols">
       <tr>
            <td class="col col-33 col1"> <span><a class="go_commande_no" href='<%=Model.LastPage%>'>Continuer ma consultation</a></span></td>
            <td class="col col-33 col2"> <span>&nbsp;</span></td>
            <td class="col col-33 col3"> <span> <input class="submit_devis" type="submit" value="Envoyer ma demande" /></span></td>
        </tr>
    </table>      

    <% Html.EndForm(); %>
        
       <table class="cols">
            <tr>
                <td class="col col-50">
				    <%Html.ShowCurrentQuoteCartList("Cartlist.ascx");%>  
                </td>
                <td class="col col-50">
					<%Html.RenderPartial("~/views/Shared/RightMenu2.ascx");%>
                </td>
            </tr>
       </table>
          
	</div>
</div>

</asp:Content>

