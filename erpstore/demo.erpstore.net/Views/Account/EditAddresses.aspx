<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<User>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Edition du compte</title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid31">
		<%Html.RenderPartial("~/views/account/managemenu.ascx", Model);%>    
    </div>
    <div id="sgrid32">
          <h2 class="titleh2">
              <span>Vos adresses</span>
          </h2>
        <div class="chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><a href="<%=Url.AccountHref()%>">Tableau de bord</a></span><b>Vos adresses</b>
        </div>
        <br class="clear"/>
        <div class="form_elements">
             <fieldset class="form">
                <legend>Adresse de facturation</legend> 
            <%if (ViewData["EditAddressSuccess"] != null && (int)ViewData["EditAddressSuccess"] == -1) { %>
                <p>Cette adresse vient d'etre modifiée avec succès</p>
            <% } %>
        
            <%Html.BeginEditAddressForm(); %>
            <%Html.RenderPartial("~/views/shared/editaddress.ascx", Model.DefaultAddress); %>
            <p class="submit submit_right">
                <input type="submit" value="Modifier" class="corner" />
            </p>
            <input type="hidden" name="addressId" value="-1" />
            <%Html.EndForm();%>
            </fieldset>
        </div>
        <div class="form_elements">
             <fieldset class="form">
                <legend>Adresse(s) de livraison</legend> 
            	<% foreach (var address in Model.DeliveryAddressList) { %>
                <%if (ViewData["EditAddressSuccess"] != null && (int)ViewData["EditAddressSuccess"] == Model.DeliveryAddressList.IndexOf(address)) { %>
                <p>Cette adresse vient d'etre modifiée avec succès</p>
                <% } %>
               <%Html.BeginEditAddressForm(); %>
               <%Html.RenderPartial("~/views/shared/editaddress.ascx", address); %>
                <p class="submit submit_right">
                    <input type="submit" value="Modifier" class="corner" />
                </p>
                <input type="hidden" name="addressId" value="<%=Model.DeliveryAddressList.IndexOf(address)%>" />
                <%Html.EndForm();%>
                <%Html.BeginDeleteAddressForm();%>
                <p class="submit submit_right">
                    <input type="submit" value="Supprimer" class="corner return" />
                </p>
                <input type="hidden" name="addressId" value="<%=Model.DeliveryAddressList.IndexOf(address)%>" />
                <%Html.EndForm();%>
            <% } %>
            </fieldset>
        <br class="clear" />
        <!-- gestion des notes //-->
        <div class="notes">
            <strong>
                <img src="/content/images/icon_noter.png" alt="" />
                A noter !</strong>
            <p class="note">
                <span>Vous devez impérativement indiquer le code postal et la ville. Ces informations
                    sont indispensables pour que votre commande soit enregistrée et traitée.</span>
            </p>
        </div>
        <!-- fin gestion des notes //-->
        </div>
    </div>
</div>

</asp:Content>
