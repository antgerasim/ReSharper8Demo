<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" MasterPageFile="~/Views/Emailer/HtmlEmail.Master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0" margin="0" padding="0" bgcolor="#E9EBED" height="100%" align="center">
  <tr>
    <td align="center" valign="top">
    
    <table width="650" border="0" cellspacing="0" cellpadding="20" margin="0" padding="0" bgcolor="#FFFFFF" height="100%" align="center">
  		<tr>
    		<td align="left" valign="top">
                <font face="Arial, Helvetica, sans-serif">
                <font color="#666" size="1">
                <b><%=ViewData["FullName"]%> </b>, si vous ne parvenez pas � lire cet email et pour �tre s�r(e) de recevoir tous nos emails, ajoutez <b><%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%></b> � votre carnet d'adresses </font>
                <br/>
                <br/> 
                <font color="#dddddd" size="2">
            <h2>De :  <%=ERPStoreApplication.WebSiteSettings.SiteName%>
            <br/> 
            Objet : Changement de mot de passe</h2> 
            </font>
            <hr noshade color="#dddddd" height="1" />
            <br />
            Cher <strong><%=ViewData["FullName"]%>,</strong><br />
            <br />
			Vous venez de nous faire une demande de r�cup�ration de mot de passe sur notre site <%=ERPStoreApplication.WebSiteSettings.SiteName%>, veuillez cliquer sur le lien suivant :
            <%=ViewData["EncryptedUrl"]%>
            </font>
            <br />
            <hr noshade color="#dddddd" height="1" />
            <font color="#888888" size="1">
            Nous vous souhaitons bonne r�ception. <br />
            <br />
            Le service clientelle<br />
            <%=ERPStoreApplication.WebSiteSettings.SiteName %><br />
            <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.ZipCode%><br />
			<%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.City%><br />
            T�l : <%=ERPStoreApplication.WebSiteSettings.Contact.ContactPhoneNumber%><br />
            Fax : <%=ERPStoreApplication.WebSiteSettings.Contact.ContactFaxNumber%><br />
            E-mail : <%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%><br />
            <br/>
            <br/>
            <br/>
            <b>Veuillez ne pas r�pondre � cet email. Cette bo�te aux lettres n�est pas consult�e et vous ne recevrez aucune r�ponse.</b>
            <br/>
            <br/>
            Conform�ment � la loi Informatique et Libert�s du 06/01/1978, vous disposez d'un droit d'acc�s, de rectification et d'opposition aux informations vous concernant qui peut s'exercer par courrier � : <%=ERPStoreApplication.WebSiteSettings.SiteName %> - Service Relation Client - <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.ZipCode%>&nbsp;<%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.City%> en indiquant nom, pr�nom, adresse et n� de client.<br/><br/>
            Par notre interm�diaire, vous pouvez �tre amen� � recevoir des propositions commerciales d'autres entreprises ou organismes, ou �tre inform� �galement de nos offres par e-mail, t�l�phone, SMS ou par courrier. Vous demandez � recevoir nos offres commerciales. Si vous ne le souhaitez pas il suffit de nous le signaler par courrier � <%=ERPStoreApplication.WebSiteSettings.SiteName %>.
            <br/>
            <br/>
            Vous pouvez � tout moment avoir acc�s et changer les informations concernant vos coordonn�es (adresses e-mail et courrier, changement de patronyme...) dans la rubrique "<b><a href="<%=ViewData["accountUrl"]%>"><font color="#22397F" size="1">mon compte</font></a></b>".<br/>
            <%=ERPStoreApplication.WebSiteSettings.SiteName %> se r�serve le droit de collecter des donn�es sur l'utilisateur, notamment par l'utilisation de cookies. 
            <br/>
            </font>
            </font>
            <br />
					</td>
        		</tr>
       		</table>
            
		</td>
    </tr>
</table>
</asp:Content>
