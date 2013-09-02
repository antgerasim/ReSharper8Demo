<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ISaleDocument>" %>

<br />
Vous avez choisi le <b>règlement par chèque bancaire</b>,<br/>
Veuillez envoyer votre chèque signé a l'adresse ci-dessous en indiquant le numéro de la commande (<%=Model.Code%>) :<br />
<br />
<table width="650" border="0" cellspacing="1" cellpadding="5" margin="0" padding="0" bgcolor="#dddddd" width="100%" align="center">
  	<tr>
    	<td align="left" valign="top" bgcolor="#f1f1f1">
        	<font color="#666" size="2">  
            <%=ERPStoreApplication.WebSiteSettings.Payment.CheckDeliveryAddress.RecipientName%><br />
            <%=ERPStoreApplication.WebSiteSettings.Payment.CheckDeliveryAddress.Street%><br />
            <%=ERPStoreApplication.WebSiteSettings.Payment.CheckDeliveryAddress.ZipCode%>&nbsp;
            <%=ERPStoreApplication.WebSiteSettings.Payment.CheckDeliveryAddress.City%>&nbsp;
            <%=ERPStoreApplication.WebSiteSettings.Payment.CheckDeliveryAddress.Country.LocalizedName%><br />
            </font>
        </td>
    </tr>
</table>
<br />
<br />
Votre commande sera expédiée dès l'encaissement de votre paiement.<br />
<br />
