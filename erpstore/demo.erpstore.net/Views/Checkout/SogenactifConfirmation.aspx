<%@ Page Title="" Language="C#" MasterPageFile="Confirmation.Master"  Inherits="System.Web.Mvc.ViewPage<OrderCart>" %>

<asp:Content ID="indexHead" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Panier de commande : Confirmation via Soci�t� G�n�rale</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderPageContent" runat="server">
	<h2 class="titleh2">
	  <span>R�capitulatif</span>
	</h2>
</asp:Content>

<asp:Content ContentPlaceHolderID="PaymentModeContent" runat="server">
    <legend class="corner">Mode de r�glement :</legend>            
    <div class="form_adresse corner  form_adresse_ok">
        <p>Vous avez s�lectionn� le mode  de r�glement : <b><%=Model.PaymentModeName%></b></p>
    </div> 
</asp:Content>

<asp:Content ContentPlaceHolderID="TermAndConditionContent" runat="server">
    <legend class="corner">Condition g�n�rale de vente :</legend> 
    <div class="corner">
        <p class="form_element_radio">En cliquant sur un moyen de payment Carte bleue, je suis d'accord avec les <%=Html.TermsAndConditionsLink("conditions g�n�rales de vente")%></p>
    </div> 
</asp:Content>

<asp:Content ContentPlaceHolderID="DisclaimerContent" runat="server">
	<p>
    <b>Veuillez selectionner ci-dessous le type de Carte Bleue que vous voulez utiliser, en cliquant sur celle-ci vous allez alors etre redirig� vers le centre de paiement s�curis� de la Soci�t� G�n�rale.</b>
	</p>
    <br/>
</asp:Content>

<asp:Content ContentPlaceHolderID="ConfirmationContent" runat="server">
  	<span class="input_cb"><%=ViewData["sogenactifForm"]%></span>
</asp:Content>
