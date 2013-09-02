<%@ Page Title="" Language="C#" Inherits="System.Web.Mvc.ViewPage<OrderCart>" MasterPageFile="~/Views/Emailer/HtmlEmail.Master" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server">
<% var user = ViewData["user"] as User; %>
<table width="100%" border="0" cellspacing="0" cellpadding="0" margin="0" padding="0" bgcolor="#E9EBED" height="100%" align="center">
  <tr>
    <td align="center" valign="top">
    
    <table width="650" border="0" cellspacing="0" cellpadding="20" margin="0" padding="0" bgcolor="#FFFFFF" height="100%" align="center">
  		<tr>
    		<td align="left" valign="top">
            <font face="Arial, Helvetica, sans-serif">
            <font color="#666" size="1">
            <b><%=user.FullName%></b>, si vous ne parvenez pas à lire cet email, vous pouvez le visualiser grâce à ce <b><a href='<%=ViewData["encryptedUrl"]%>'>lien</a></b> Pour être sûr(e) de recevoir tous nos emails, ajoutez <b><%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%></b> à votre carnet d'adresses </font>
            <br/>
            <br/> 
            <font  color="#dddddd" size="2">
            <h2>De :  <%=ERPStoreApplication.WebSiteSettings.SiteName%>
            <br/> 
            Objet : Echec du règlement par carte bleue</h2> 
            </font>
            <hr noshade color="#dddddd" height="1" />
            <br />
            Cher <strong><%=user.FullName%>,</strong><br />
            <br />
            <%=ViewData["message"] %>
            <br />
            <br />

            <hr noshade color="#dddddd" height="1" />
            <h3>Votre référence de panier :</h3>
            <p>
            <font size="2">
            <%=Model.CustomerDocumentReference%>
            </font>
            </p>

            <hr noshade color="#dddddd" height="1" />
            <h3>Détail du panier numéro : <strong><%=Model.Code%></strong></h3>
           	du <%=Model.CreationDate.ToString("dddd dd MMMM yyyy")%>
            <br />
    		<table width="650" border="0" cellspacing="1" cellpadding="10" margin="0" padding="0" bgcolor="#dddddd" width="100%" align="center">
  				<%foreach (var item in Model.Items) { %>
                <tr>
    				<td align="left" valign="top" bgcolor="#eeeeee" width="500"> 
                    	<font size="1">
                        <br/>
                        <%=item.Quantity%> x <strong><%=item.Product.Title%></strong> (#Ref:<%=item.Product.Code%>) <small>pour un montant de 
                        </small></font>
                    </td>
                    <td valign="top" bgcolor="#eeeeee"  width="150" align="right">
                    <font size="2">
                        <% if (item.RecyclePrice.Value != 0) {  %>
                        dont <%=item.RecyclePrice.Value.ToCurrency()%><br /> d'eco taxe<br />
                        <% } %>
                        <br />  
                    <%=item.GrandTotal.ToString("#,##0.00")%> Euros <small>HT</small>
                    </font>
                    </td>
           		</tr>
				<%  } %>
  				<tr>
               	 	<td align="right" valign="top" bgcolor="#eeeeee"> 
                      <font size="1">
                      	Sous Total HT : <br/>
                        <% if (Model.RecycleTotal != 0) { %>éco taxe HT : <br/><%}%>
                        Frais de port TTC :
                        <br/>
                        </font> 
                    </td>
    				<td align="right" valign="top" bgcolor="#eeeeee"> 
                      <font size="1">
                       	<%=Model.Total.ToCurrency() %><br />
                        <% if (Model.RecycleTotal != 0) { %>
                        <%=Model.RecycleTotal.ToCurrency()%>
                        &nbsp;<br/><%}%>
						<% if (Model.ShippingFeeTotalWithTax > 0) { %>
                        <%=Model.ShippingFeeTotalWithTax.ToCurrency()%><br/>
                        <%}%> 
                        <%else{%>
                        Franco<br/>
                        <%}%>
                        </font>
                     </td>
           		</tr>
                <tr>
               	 	<td align="right" valign="top" bgcolor="#eeeeee"> 
                        <font size="3">Total HT:</font><br/>
						<font size="2">Total TTC:</font>
                    </td>
    				<td align="right" valign="top" bgcolor="#eeeeee"> 
                        <font size="3" color="#22397F"><strong><%=Model.GrandTotal.ToCurrency() %></strong></font> <br/>
                        <font size="2"><strong><%=Model.GrandTotalWithTax.ToCurrency() %></strong></font>
                     </td>                
                </tr>
           </table>
           <br/>
    		<table width="650" border="0" cellspacing="0" cellpadding="10" margin="0" padding="0" bgcolor="#dddddd" width="100%" align="center">
  				<tr>
    				<td align="left" valign="top">  
                    	<font size="1">          
                        <h3>Adresse de livraison : </h3>
                        <%=Model.DeliveryAddress.RecipientName %><br />
                        <%=Model.DeliveryAddress.Street%><br />
                        <%=Model.DeliveryAddress.ZipCode%>&nbsp;<%=Model.DeliveryAddress.City%><br />
                        <%=Model.DeliveryAddress.Country.LocalizedName%><br />
                        </font>
                    </td>
                    <td align="left" valign="top">
                    	<font size="1">
                        <h3>Adresse de facturation</h3>
                        <%=Model.BillingAddress.RecipientName %> <br />
                        <%=Model.BillingAddress.Street%><br />
                        <%=Model.BillingAddress.ZipCode%>&nbsp;<%=Model.BillingAddress.City %><br />
                        <%=Model.BillingAddress.Country.LocalizedName%><br />
                        </font>
                    </td>
           		</tr>
           </table>
            <br />
            <hr noshade color="#dddddd" height="1" />
            <br />
            Le service clientelle<br />
            <%=ERPStoreApplication.WebSiteSettings.SiteName %><br />
            <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.Street%><br />
            <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.ZipCode%>&nbsp;
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
					</td>
        		</tr>
       		</table>
		</td>
    </tr>
</table>
</asp:Content>