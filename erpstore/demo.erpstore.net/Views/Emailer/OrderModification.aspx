<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Order>" MasterPageFile="~/Views/Emailer/HtmlEmail.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0" margin="0" padding="0" bgcolor="#E9EBED" height="100%" align="center">
  <tr>
    <td align="center" valign="top">
    
    <table width="650" border="0" cellspacing="0" cellpadding="20" margin="0" padding="0" bgcolor="#FFFFFF" height="100%" align="center">
  		<tr>
    		<td align="left" valign="top">
                <font face="Arial, Helvetica, sans-serif">
                <font color="#666" size="1">
                <b><%=Model.User.FullName%></b>, si vous ne parvenez pas � lire cet email, vous pouvez le visualiser gr�ce � ce <b><a href='<%=ViewData["encryptedUrl"]%>'>lien</a></b> Pour �tre s�r(e) de recevoir tous nos emails, ajoutez <b><%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%></b> � votre carnet d'adresses </font>
                <br/>
                <br/> 
                <font color="#dddddd" size="2">
                        <h2>De :  <%=ERPStoreApplication.WebSiteSettings.SiteName%>
                        <br/> 
                        Objet : annulation de votre commande</h2> 
                        </font>
                        <hr noshade color="#dddddd" height="1" />
                        <br />
                        <p>Bonjour <%=Model.User.FullName%>,</p>
                        <br />
                        Nous avons bien pris en compte votre souhait de modifier votre commande N�<%=Model.Code%>
                        <ul>
                            <li>message associ� : <%=ViewData["message"]%></li>
                        </ul>
                        <br/>
                        Nous vous confirmerons la modification dans les plus brefs d�lais.
                    </font>
					</td>
        		</tr>
       		</table>
            
		</td>
    </tr>
</table>
</asp:Content>