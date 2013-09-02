<%@ Page Title="" Language="C#" MasterPageFile="Confirmation.Master"  Inherits="System.Web.Mvc.ViewPage<OrderCart>" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Panier de commande : Confirmation</title>
</asp:Content>

<asp:Content ContentPlaceHolderID="PaymentModeContent" runat="server">
    <legend class="corner">Mode de règlement :</legend>
    <br/>      
    <div>
        <p>Vous avez sélectionné le mode  de règlement : </p>
        <br/>
       <p><b><img src="/content/images/flechegrise.png" alt="" style="vertical-align:middle;"/>&nbsp;<%=Model.PaymentModeName%></b></p>

    </div> 
    <br/>
</asp:Content>

<asp:Content ContentPlaceHolderID="HeaderPageContent" runat="server">

</asp:Content>

<asp:Content ContentPlaceHolderID="BeginFormContent" runat="server">
	<% Html.BeginForm(); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="EndFormContent" runat="server">
	<% Html.EndForm(); %>
</asp:Content>

<asp:Content ContentPlaceHolderID="DisclaimerContent" runat="server">
</asp:Content>

<asp:Content ContentPlaceHolderID="ConfirmationContent" runat="server">
	<span><input type="submit" value="Confirmer" align="middle" /></span>
</asp:Content>

<asp:Content ContentPlaceHolderID="TermAndConditionContent" runat="server">
    <legend class="corner">Condition générale de vente :</legend> 
    <div class="corner">
        <p class="form_element_radio"><input type="checkbox" name="condition" />&nbsp;
        En cochant cette case je suis d'accord avec les <%=Html.TermsAndConditionsLink("conditions générales de vente")%>
        <%=Html.ValidationMessage("condition","*")%></p>
    </div> 
</asp:Content>

