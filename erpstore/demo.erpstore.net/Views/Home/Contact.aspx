<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<ContactInfo>" %>

<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" runat="server">
<title>Contact <%=ERPStoreApplication.WebSiteSettings.SiteName%></title>
</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
	<div id="grid3">
        <div id="sgrid31">
			<%Html.ShowMenu("RightHelp.ascx");%>
        </div>
        
        <div id="sgrid32">
        <h2 class="titleh2"><span>Contact</span></h2>
        <div class="bloc chemin">
             <span><a href="<%=Url.HomeHref()%>">accueil</a></span><b>Contact</b>
        </div>  
		<div class="form_elements">
        <%=ERPStoreApplication.WebSiteSettings.SiteName%> mets à votre dispositions tous les moyens pour contacter notre service client :
        <br />
        <br/>
        Par téléphone : <%=ERPStoreApplication.WebSiteSettings.Contact.ContactPhoneNumber%><br />
        Nos horaires d'ouverture des bureaux sont les suivants :
        <b><%=ERPStoreApplication.WebSiteSettings.Contact.OfficeHours%></b><br/><br/>
        <ul>
			<li>  Par email : <a href="mailto://<%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%>"><%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail%></a></li>
			<li> Par fax : <%=ERPStoreApplication.WebSiteSettings.Contact.ContactFaxNumber%></li>
			<li> Par courrier : <%=ERPStoreApplication.WebSiteSettings.Contact.CorporateName%> Service client - <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.Street%> - <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.ZipCode%> <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.City%></li>
        </ul>
        <br/>
        Par ce formulaire : <br />
           
                <fieldset class="form">
                    <legend class="corner"> Votre Message</legend> 
                    <% if (ViewData["IsSent"] != null) { %>
                        <div class="notes">
                            <strong><img src="/content/images/icon_noter.png" alt=""/> A noter !</strong>
                            <p class="note"> 
                               <span><b>Votre message a bien été transmis, nous vous remercions.</b></span>
                            </p>
                        </div> 
                    <% } else { %>
                    <%Html.RenderPartial("validationsummary");%>
                    <% using (Html.BeginContactFrom()) { %>
                    <% =Html.AntiForgeryToken()%>
                    <p>
                        <label for='FullName'>Votre Nom</label>
                        <span class="form_element_input"><% =Html.TextBox("FullName")%></span>
                        <% =Html.ValidationMessage("FullName")%>
                    </p>
                    <p>
                        <label for='CorporateName'>Votre Société</label>
                        <span class="form_element_input"><% =Html.TextBox("CorporateName")%></span>
                        <% =Html.ValidationMessage("CorporateName")%>
                    </p>
                    <p>
                        <label for='Email'>Votre Email</label>
                        <span class="form_element_input"><% =Html.TextBox("Email")%></span>
                        <% =Html.ValidationMessage("Email")%>
                    </p>
                    <p>
                        <label for='PhoneNumber'>Votre Téléphone</label>
                        <span class="form_element_input"><% =Html.TextBox("PhoneNumber")%></span>
                    </p>
                    <p>
                        <label for='Message'>Le Message</label>
                        <span class="form_element_input">
                        <% =Html.TextArea("Message", new { rows = 5, cols = 35 })%>
                        </span>
                        <% =Html.ValidationMessage("Message")%>
                    </p>
                   <table class="go_commande cols">
                       <tr>
                            <td class="col col-30 col1">
                            	 &nbsp;
                            </td>
                            <td class="col col-30 col2">
                            	 &nbsp;
                            </td>
                            <td class="col col-30 col3">
                            	 <input type="submit" value="Envoyer" />
                            </td>
                        </tr>
                    </table>   
                    <% } %>
                <% } %>
                </fieldset>
                <br />
            </div>
   
        </div>
    </div>
</asp:Content>


