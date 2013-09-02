<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Résultat de recherche</title>
	<%=Html.MetaDescription("Recherche sans résultat")%>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<!-- colonne 1 //-->
    <div id="grid1">
    <% Html.ShowProductCategoryListForefront("categories.ascx"); %>
    </div>
	<!-- fin colonne 1 //-->
<div id="grid2">
    <div id="sgrid21">
		<%Html.RenderPartial("~/views/Shared/RightMenu.ascx");%>
		<% Html.ShowTopSearchTermList("searchcloud.ascx", 30); %>
		<%Html.RenderPartial("~/views/Shared/RightMenu2.ascx");%>
    </div>
    <div id="sgrid22">

<blockquote>
	Il n’y a aucun produit correspondant à vos critères de recherche
</blockquote>

<div class="form_elements">
<img src="/content/images/icos/pictos_r1_c5.png" width="30" height="30" alt="mouton 5 pattes" /> Nous pouvons rechercher pour vous les produits qu'il vous manque, il suffit de renseigner le formulaire suivant :

                <fieldset class="form">
                    <legend class="corner"> Votre message</legend> 
                    <% if (ViewData["IsSent"] != null) { %>
                        <div class="notes">
                            <strong><img src="/content/images/icon_noter.png" alt=""/> A noter !</strong>
                            <p class="note"> 
                               <span>Votre message a bien été transmis, nous vous remercions.</span>
                            </p>
                        </div> 
                    <% } else { %>
                    <%Html.RenderPartial("validationsummary");%>
                    <% using (Html.BeginContactFrom()) { %>
                    <% =Html.AntiForgeryToken()%>
                    <p>
                        <label for='FullName'>Votre nom</label>
                        <span class="form_element_input"><% =Html.TextBox("FullName")%></span>
                        <% =Html.ValidationMessage("FullName", "*")%>
                    </p>
                    <p>
                        <label for='CorporateName'>Votre société</label>
                        <span class="form_element_input"><% =Html.TextBox("CorporateName")%></span>
                        <% =Html.ValidationMessage("CorporateName", "*")%>
                    </p>
                    <p>
                        <label for='Email'>Votre Email</label>
                        <span class="form_element_input"><% =Html.TextBox("Email")%></span>
                        <% =Html.ValidationMessage("Email", "*")%>
                    </p>
                    <p>
                        <label for='PhoneNumber'>Votre Téléphone</label>
                        <span class="form_element_input"><% =Html.TextBox("PhoneNumber")%></span>
                    </p>
                    <p>
                        <label for='Message'>Le Message</label>
                        <span class="form_element_input">
                        <% =Html.TextArea("Message", "Ma recherche : " + Request["s"],  new { rows = 6, cols = 50 })%>
                        </span>
                    </p>
                    <p class="submit submit_right">
                    <input type="submit" value="Envoyer" />
                    </p>
                    <% } %>
                <% } %>
                </fieldset>
                
                </div>

    </div>
</div>

</asp:Content>

