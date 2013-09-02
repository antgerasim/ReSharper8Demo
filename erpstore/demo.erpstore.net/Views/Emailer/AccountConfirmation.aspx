<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<User>" MasterPageFile="~/Views/Emailer/HtmlEmail.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0" margin="0" padding="0" bgcolor="#E9EBED" height="100%" align="center">
  <tr>
    <td align="center" valign="top">
    
    <table width="650" border="0" cellspacing="0" cellpadding="20" margin="0" padding="0" bgcolor="#FFFFFF" height="100%" align="center">
  		<tr>
    		<td align="left" valign="top">
            <font face="Arial, Helvetica, sans-serif">
            <font color="#666" size="1">
            <b><%=Model.FullName%></b>, si vous ne parvenez pas à lire cet email et pour être sûr(e) de recevoir tous nos emails, ajoutez <b><%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%></b> à votre carnet d'adresses </font>
<br/>
<br/> 
            <font color="#dddddd" size="2">
            <h2>De :  <%=ERPStoreApplication.WebSiteSettings.SiteName%>
            <br/> 
            Objet : création de votre commande</h2> 
            </font>
            <hr noshade color="#dddddd" height="1" />
            <br />
            <p>Bonjour <%=Model.FullName%>,</p>
            <br />
            <font size="1">
            <p>Vous venez de creer un compte sur notre site <b><%=ERPStoreApplication.WebSiteSettings.SiteName%></b>,<br/>
            pour terminer la configuration de votre compte, veuillez cliquer sur le lien suivant :</p>
            <a href="<%=ViewData["confirmationUrl"]%>"><%=ViewData["confirmationUrl"]%></a><br />
            <br />
            <p><big>Votre identifiant est : <b><%=Model.Login%></b></big></p>
            <br />
            Nous vous remercions de votre confiance<br />
            L'equipe commerciale.<br />
            </font>
            <hr noshade color="#dddddd" height="1" />
            <font color="#888888" size="1">
            <br />
            Le service commerciale<br />
            <%=ERPStoreApplication.WebSiteSettings.SiteName %><br />
            <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.City%><br />
            <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.ZipCode%>&nbsp;<%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.City%><br />
            Tél : <%=ERPStoreApplication.WebSiteSettings.Contact.ContactPhoneNumber%><br />
            Fax : <%=ERPStoreApplication.WebSiteSettings.Contact.ContactFaxNumber%><br />
            E-mail : <%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%><br />
            <br/>
            <br/>
            <br/>
            <b>Veuillez ne pas répondre à cet email. Cette boîte aux lettres n’est pas consultée et vous ne recevrez aucune réponse.</b>
            <br/>
            <br/>
            Conformément à la loi Informatique et Libertés du 06/01/1978, vous disposez d'un droit d'accès, de rectification et d'opposition aux informations vous concernant qui peut s'exercer par courrier à : <%=ERPStoreApplication.WebSiteSettings.SiteName %> - Service Relation Client - <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.ZipCode%>&nbsp;<%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.City%> en indiquant nom, prénom, adresse et n° de client.<br/><br/>
            Par notre intermédiaire, vous pouvez être amené à recevoir des propositions commerciales d'autres entreprises ou organismes, ou être informé également de nos offres par e-mail, téléphone, SMS ou par courrier. Vous demandez à recevoir nos offres commerciales. Si vous ne le souhaitez pas il suffit de nous le signaler par courrier à <%=ERPStoreApplication.WebSiteSettings.SiteName %>.<br/><br/>
            Vous pouvez à tout moment avoir accès et changer les informations concernant vos coordonnées (adresses e-mail et courrier, changement de patronyme...) dans la rubrique "<b><a href="<%=ViewData["accountUrl"]%>"><font color="#22397F" size="1">mon compte</font></a></b>".<br/>
            <%=ERPStoreApplication.WebSiteSettings.SiteName %> se réserve le droit de collecter des données sur l'utilisateur, notamment par l'utilisation de cookies. <br/> 
            </font>
           	</font>
            </font>
					</td>
        		</tr>
       		</table>
            
		</td>
    </tr>
</table>
</asp:Content>