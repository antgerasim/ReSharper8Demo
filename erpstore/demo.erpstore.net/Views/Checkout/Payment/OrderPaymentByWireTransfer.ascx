<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<ISaleDocument>" %>
<br />
Vous avez choisi le <b>virement bancaire apr�s validation</b>,<br/>
contactez votre banque et communiquez lui le montant total de votre commande et nos coordonn�es bancaire indiqu�es ci-dessous :<br />
<br />
N� Transaction : <b><%=Model.Code %></b> <small>(celui-ci nous permetra de traiter tr�s rapidement votre commande)</small><br />
<br />
N� du compte : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.Account %><br />
Nom et domiciliation de la banque : <br />
<%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.Designation%><br />
Guichet : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.BankCode%><br />
Cl� : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.Key%><br />
No IBAN : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.IBAN%><br />
Code BIC/SWIFT : <%=ERPStoreApplication.WebSiteSettings.Payment.BankAccount.BIC%><br />
<br />
<br />
Votre commande sera exp�di�e d�s l'encaissement de votre paiement.<br />
<br />

