<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<WebSiteSettings>" %>

<asp:Content ID="aboutHead" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>A propos de <%=Model.SiteName%></title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
	<div id="grid3">
        <div id="sgrid31">
			<%Html.ShowMenu("RightHelp.ascx");%>
        </div>
        
        <div id="sgrid32">
        <div class="bloc chemin">
             <span><a href="<%=Url.HomeHref()%>">accueil</a></span><b><%=Model.Sloggan%></b>
        </div>        
        <h2 class="titleh2"><span>A propos de <%=Model.SiteName%></span></h2>
            <div class="corner aide">
                <div class="corner texte bloc">
                    <h4 class="corner">Contact commercial <%=Model.Contact.CorporateName%></h4>
                    <p>
                    <ul>
                        <li>Numéro de téléphone : <b><%=Model.Contact.ContactPhoneNumber%></b></li>
                        <li>Fax : <b><%=Model.Contact.ContactFaxNumber%></b></li>
                        <li>Email : <b><a href="mailto:<%=Model.Contact.ContactEmail %>" title="email : <%=Model.Contact.ContactEmail %>"><%=ERPStoreApplication.WebSiteSettings.Contact.ContactEmail %></a></b></li>
                        <li>Horaires : <b><%=Model.Contact.OfficeHours%></b><br/><br/></li>
                        <li>Adresse : <br/></li>
                        <li>Rue : <b><%=Model.Contact.DefaultAddress.Street%></b></li>
                        <li>Code postal : <b><%=Model.Contact.DefaultAddress.ZipCode%></b></li>
                        <li>Ville : <b><%=Model.Contact.DefaultAddress.City%></b></li>
                        <li>Pays : <b><%=Model.Contact.DefaultAddress.Country.Name%></b><br/><br/></li>
                        <li>Code NAF : <b><%=Model.Contact.NafCode%></b></li>
                        <li>Numéro de SIRET : <b><%=Model.Contact.SiretNumber%></b></li>
                        <li>Numéro de RCS : <b><%=Model.Contact.RcsNumber%></b></li>
                        <li>Status Social : <b><%=Model.Contact.SocialStatus%></b></li>
                    </ul>
                    </p>
                </div>
            </div>
    </div>

</asp:Content>



