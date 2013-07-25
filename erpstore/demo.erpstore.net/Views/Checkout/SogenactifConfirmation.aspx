<%@ Page Title="" Language="C#" MasterPageFile="Confirmation.Master"  Inherits="System.Web.Mvc.ViewPage<OrderCart>" %>

<asp:Content ID="indexHead" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Panier de commande : Confirmation via Société Générale</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderPageContent" runat="server">
	<h2 class="titleh2">
	  <span>Récapitulatif</span>
	</h2>
</asp:Content>

<asp:Content ContentPlaceHolderID="PaymentModeContent" runat="server">
    <legend class="corner">Mode de règlement :</legend>            
    <div class="form_adresse corner  form_adresse_ok">
        <p>Vous avez sélectionné le mode  de règlement : <b><%=Model.PaymentModeName%></b></p>
    </div> 
</asp:Content>

<asp:Content ContentPlaceHolderID="TermAndConditionContent" runat="server">
    <legend class="corner">Condition générale de vente :</legend> 
    <div class="corner">
        <p class="form_element_radio">En cliquant sur un moyen de payment Carte bleue, je suis d'accord avec les <%=Html.TermsAndConditionsLink("conditions générales de vente")%></p>
    </div> 
</asp:Content>

<asp:Content ContentPlaceHolderID="DisclaimerContent" runat="server">
	<p>
    <b>Veuillez selectionner ci-dessous le type de Carte Bleue que vous voulez utiliser, en cliquant sur celle-ci vous allez alors etre redirigé vers le centre de paiement sécurisé de la Société Générale.</b>
	</p>
    <br/>
</asp:Content>

<asp:Content ContentPlaceHolderID="ConfirmationContent" runat="server">
  	<span class="input_cb"><%=ViewData["sogenactifForm"]%></span>
</asp:Content>
