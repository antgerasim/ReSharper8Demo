<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ISaleDocument>" %>
<br />
Vous avez choisi le <b>virement bancaire après validation</b>,<br/>
contactez votre banque et communiquez lui le montant total de votre commande et nos coordonnées bancaire indiquées ci-dessous :<br />
<br />
N° Transaction : <b><%=Model.Code %></b> <small>(celui-ci nous permetra de traiter très rapidement votre commande)</small><br />
<br />
N° du compte : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.Account %><br />
Nom et domiciliation de la banque : <br />
<%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.Designation%><br />
Guichet : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.BankCode%><br />
Clé : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.Key%><br />
No IBAN : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.IBAN%><br />
Code BIC/SWIFT : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.BIC%><br />
<br />
<br />
Votre commande sera expédiée dès l'encaissement de votre paiement.<br />
<br />

