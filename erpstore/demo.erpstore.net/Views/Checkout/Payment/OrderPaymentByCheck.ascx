<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ISaleDocument>" %>

<br />
Vous avez choisi le <b>r�glement par ch�que bancaire</b>,<br/>
Veuillez envoyer votre ch�que sign� a l'adresse ci-dessous en indiquant le num�ro de la commande (<b><%=Model.Code%></b>) :
<br/>
<br/>
<div class="form_elements">
    <div class="form_adresse corner  form_adresse_ok">
		<%=ERPStoreApplication.WebSiteSettings.Payment.CheckDeliveryAddress.RecipientName%><br />
        <%=ERPStoreApplication.WebSiteSettings.Payment.CheckDeliveryAddress.Street%><br />
        <%=ERPStoreApplication.WebSiteSettings.Payment.CheckDeliveryAddress.ZipCode%>&nbsp;
        <%=ERPStoreApplication.WebSiteSettings.Payment.CheckDeliveryAddress.City%>&nbsp;
        <%=ERPStoreApplication.WebSiteSettings.Payment.CheckDeliveryAddress.Country.LocalizedName%>
    </div>
</div>
<br/>
<br />
Votre commande sera exp�di�e d�s l'encaissement de votre paiement.<br />
<br />
