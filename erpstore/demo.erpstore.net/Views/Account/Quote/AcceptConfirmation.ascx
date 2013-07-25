<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<p>Confirmez-vous la conversion de ce devis en commande ?</p>

<a href="#" onclick="javascript:return closeDialog();">Annuler</a>

<%Html.BeginAcceptQuoteForm(); %>
    
    <input type="hidden" name="key" value='<%=ViewData["key"]%>' />

    <input type="checkbox" name="confirmation" />En cochant cette case, vous acceptez les <a href="<%=Url.TermsAndConditionsHref()%>" target="_blank">conditions générales de vente</a>
    <input type="submit" value="J'accepte cette offre" />
    
<%Html.EndForm();%>

<script type="text/javascript">

	$("#ui-dialog-title-dialog").empty();
	$("#ui-dialog-title-dialog").html('Demande de confirmation');

	function closeDialog() {
		$('#dialog').dialog('close');
		return false;
	}
</script>

