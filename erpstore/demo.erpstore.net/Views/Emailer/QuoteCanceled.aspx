<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Quote>" MasterPageFile="~/Views/Emailer/HtmlEmail.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<table width="100%" border="0" cellspacing="0" cellpadding="0" margin="0" padding="0" bgcolor="#E9EBED" height="100%" align="center">
  <tr>
    <td align="center" valign="top">
    
    <table width="650" border="0" cellspacing="0" cellpadding="20" margin="0" padding="0" bgcolor="#FFFFFF" height="100%" align="center">
  		<tr>
    		<td align="left" valign="top">
                <font face="Arial, Helvetica, sans-serif">
                <font color="#666" size="1">
                <b><%=Model.User.FullName%></b>, si vous ne parvenez pas à lire cet email, vous pouvez le visualiser grâce à ce <b><a href='<%=ViewData["encryptedUrl"]%>'>lien</a></b> Pour être sûr(e) de recevoir tous nos emails, ajoutez <b><%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%></b> à votre carnet d'adresses </font>
                <br/>
                <br/> 
                <font color="#dddddd" size="2">
                        <h2>De :  <%=ERPStoreApplication.WebSiteSettings.SiteName%>
                        <br/> 
                        Objet : annulation de votre devis</h2> 
                        </font>
                        <hr noshade color="#dddddd" height="1" />
                        <br />
                        <p>Bonjour <%=Model.User.FullName%>,</p>
                        <br />
                        Votre devis N°<%=Model.Code%> vient d'etre annulée.
                        <ul>
                            <li>raison : <%=((ERPStore.Models.CancelQuoteReason)ViewData["reason"]).GetLocalizedName()%></li>
                            <li>message associé : <%=ViewData["message"]%></li>
                        </ul>
                    </font>
					</td>
        		</tr>
       		</table>
            
		</td>
    </tr>
</table>
</asp:Content>