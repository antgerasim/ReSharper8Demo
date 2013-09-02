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
            <b><%=Model.UserFullName%></b>, si vous ne parvenez pas � lire cet email, vous pouvez le visualiser gr�ce � ce <b><a href='<%=ViewData["encryptedUrl"]%>'>lien</a></b> Pour �tre s�r(e) de recevoir tous nos emails, ajoutez <b><%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%></b> � votre carnet d'adresses </font>
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
            Apr�s �tude de votre demande, nous allons vous envoyer rapidement par mail notre meilleure offre de prix.
            <br />

            <hr noshade color="#dddddd" height="1" />
            <h3>Votre r�f�rence :</h3>
            <p>
            <font size="2">
            <%=Model.CustomerDocumentReference%>
            </font>
            </p>

            <hr noshade color="#dddddd" height="1" />
            <h3>D�tail des produits : </h3>
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
            <br />
            </font>
					</td>
        		</tr>
       		</table>
		</td>
    </tr>
</table>
</asp:Content>