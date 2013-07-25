<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ISaleDocument>" %>
<br />
Vous avez choisi le <b>virement bancaire après validation</b>,<br/>
contactez votre banque et communiquez lui le montant total de votre commande et nos coordonnées bancaire indiquées ci-dessous :<br />
<br />
N° Transaction : <b><%=Model.Code %></b> <small>(celui-ci nous permetra de traiter très rapidement votre commande)</small><br />
<br />
<table width="650" border="0" cellspacing="1" cellpadding="5" margin="0" padding="0" bgcolor="#dddddd" width="100%" align="center">
  	<tr>
    	<td align="left" valign="top" bgcolor="#f1f1f1">
        	<font color="#666" size="2">  
            N° du compte : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.Account %><br />
            Nom et domiciliation de la banque : <br />
            <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.Designation%><br />
            Guichet : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.BankCode%><br />
            Clé : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.Key%><br />
            No IBAN : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.IBAN%><br />
            Code BIC/SWIFT : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.BIC%><br />
            </font>
        </td>
    </tr>
</table>
<br />
<br />
Votre commande sera expédiée après encaissement de votre paiement.<br />
<br />