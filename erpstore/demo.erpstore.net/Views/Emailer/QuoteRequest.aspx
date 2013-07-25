<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<QuoteCart>" MasterPageFile="~/Views/Emailer/HtmlEmail.Master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0" margin="0" padding="0" bgcolor="#E9EBED" height="100%" align="center">
  <tr>
    <td align="center" valign="top">
    
    <table width="650" border="0" cellspacing="0" cellpadding="20" margin="0" padding="0" bgcolor="#FFFFFF" height="100%" align="center">
  		<tr>
    		<td align="left" valign="top">
            <font face="Arial, Helvetica, sans-serif">
            <font color="#666" size="1">
            <b><%=Model.UserFullName%></b>, si vous ne parvenez pas à lire cet email, vous pouvez le visualiser grâce à ce <b><a href='<%=ViewData["encryptedUrl"]%>'>lien</a></b> Pour être sûr(e) de recevoir tous nos emails, ajoutez <b><%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%></b> à votre carnet d'adresses </font>
            <br/>
            <br/> 
            <font  color="#dddddd" size="2">
            <h2>De :  <%=ERPStoreApplication.WebSiteSettings.SiteName%>
            <br/> 
            Objet : votre demande de prix du <%=Model.CreationDate.ToString("dddd dd MMMM yyyy")%></h2> 
            </font>
            <hr noshade color="#dddddd" height="1" />
            <br />
            Cher <strong><%=Model.UserFullName%>,</strong><br />
            <br />
            <%=ERPStoreApplication.WebSiteSettings.SiteName%> vous confirme la prise en compte de votre demande de prix et vous remercie de votre confiance.
            <br />
            Après étude de votre demande, nous allons vous envoyer rapidement par mail notre meilleure offre de prix.
            <br />

            <hr noshade color="#dddddd" height="1" />
            <h3>Votre référence :</h3>
            <p>
            <font size="2">
            <%=Model.CustomerDocumentReference%>
            </font>
            </p>

            <hr noshade color="#dddddd" height="1" />
            <h3>Détail des produits : </h3>
            <br />
    		<table width="650" border="0" cellspacing="1" cellpadding="10" margin="0" padding="0" bgcolor="#dddddd" width="100%" align="center">
  				<%foreach (var item in Model.Items) { %>
                <tr>
    				<td align="left" valign="top" bgcolor="#eeeeee" width="500"> 
                    	<font size="1">
                        <br/>
                        <%=item.Quantity%> x <strong><%=item.Product.Title%></strong> (#Ref:<%=item.Product.Code%>)</font>
                    </td>
                    <td valign="top" bgcolor="#eeeeee"  width="150" align="right">
                    </td>
           		</tr>
				<%  } %>
           </table>
           <br/>
            <hr noshade color="#dddddd" height="1" />
            <font color="#888888" size="1">
            Nous vous souhaitons bonne réception. <br />
            <br />
            Le service clientelle<br />
            <%=ERPStoreApplication.WebSiteSettings.SiteName %><br />
            <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.ZipCode%><br />
			<%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.City%><br />
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
            Par notre intermédiaire, vous pouvez être amené à recevoir des propositions commerciales d'autres entreprises ou organismes, ou être informé également de nos offres par e-mail, téléphone, SMS ou par courrier. Vous demandez à recevoir nos offres commerciales. Si vous ne le souhaitez pas il suffit de nous le signaler par courrier à <%=ERPStoreApplication.WebSiteSettings.SiteName %>.
            <br/>
            <br/>
            Vous pouvez à tout moment avoir accès et changer les informations concernant vos coordonnées (adresses e-mail et courrier, changement de patronyme...) dans la rubrique "<b><a href="<%=ViewData["accountUrl"]%>"><font color="#22397F" size="1">mon compte</font></a></b>".<br/>
            <%=ERPStoreApplication.WebSiteSettings.SiteName %> se réserve le droit de collecter des données sur l'utilisateur, notamment par l'utilisation de cookies. 
            <br/>
            </font>
            <br />
            </font>
					</td>
        		</tr>
       		</table>
		</td>
    </tr>
</table>
</asp:Content>