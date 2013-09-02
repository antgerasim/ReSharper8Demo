<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<WebSiteSettings>" %>

<asp:Content ID="aboutHead" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Politique de confidentialité des données <%=Model.SiteName %></title>
</asp:Content>
<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
	<div id="grid3">
        <div id="sgrid31">
			<%Html.ShowMenu("RightHelp.ascx");%>
        </div>
        
        <div id="sgrid32">
            <h2 class="titleh2"><span>Politique de confidentialité des données</span></h2>
            <div class="bloc chemin">
                 <span><a href="<%=Url.HomeHref()%>">accueil</a></span><b>informations légales</b>
            </div>  
                <div class="corner aide">
                    <div class="corner texte bloc">
                    <p>
                        <strong>
                        <%=Model.SiteName%> s'engage auprès de vous afin de vous garantir la confidentialité des informations
                        personnelles que vous nous fournissez, cela veut dire que :
                        </strong>
                    </p>
                    <h4 class="corner"><a name="cookies">Gestion des cookies</a></h4>
                    <p>
                        Afin de traiter votre commande, nous devons être en mesure de connaître et de mémoriser
                        certains des paramètres qui nous sont communiqués par votre ordinateur : il nous
                        faut savoir qui vous êtes ainsi que les produits que vous êtes en train d'acheter.
                        Les cookies sont des programmes utilisés dans cet unique but : mémoriser, le temps
                        de votre visite, votre identité (connue grâce à votre pseudo et à votre mot de passe)
                        et le contenu de votre caddie au fur et à mesure que vous le remplissez. Une fois
                        votre shopping terminé, ces informations sont automatiquement effacées, nos " cookies
                        " étant ce ceux que l'on nomme " cookies volatiles ". Pas d'inquiétude donc.
                    </p>
                    <h4 class="corner">
                        <a name="protection">Protection des données personnelles</a>
                    </h4>
                    <p>
                        En tant que site marchand,
                        <%=Model.SiteName%>
                        recueille un certain nombre d'informations nécessaires au traitement des commandes.
                        <%=Model.SiteName%>
                        traite toutes ces informations avec la plus grande confidentialité. Le traitement
                        automatisé d'informations nominatives sur le site
                        <%=Model.SiteName%>
                        a été déclaré auprès de la Commission Nationale de l'Informatique et des Libertés
                        sous le numéro <%=Model.Texts.CnilNumber%>.
                    </p>
                    <h4 class="corner">
                        <a name="respect">Respect des réglementations française et européenne</a>
                    </h4>
                    <p>
                        Conformément à la loi française "Informatique et libertés" n°78-17 du 6 janvier
                        1978, vous disposez d'un droit d'accès et de rectification aux données vous concernant.
                        Vous pouvez exercer ce droit en nous envoyant un courrier à l'adresse suivante :
                        <br />
                        <br/>
                    </p>
                    <fieldset>
                        <b><%=Model.Contact.DefaultAddress.RecipientName%>, Service Internet</b><br />
                        <b><%=Model.Contact.DefaultAddress.Street%></b><br />
                        <b><%=Model.Contact.DefaultAddress.ZipCode%>&nbsp;<%=Model.Contact.DefaultAddress.City%>&nbsp;<%=Model.Contact.DefaultAddress.Country.LocalizedName%></b>
                    </fieldset>
               		<h4 class="corner">www.quincaillerie.pro est édité par RC Web</h4>
               		<p>
       				Siège social : <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.Street%>, <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.ZipCode%> <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.City%> <%=ERPStoreApplication.WebSiteSettings.Contact.DefaultAddress.Country.LocalizedName%><br />
					Code NAF : <%=ERPStoreApplication.WebSiteSettings.Contact.NafCode %><br />
					Numero au registre du commerce : <%=ERPStoreApplication.WebSiteSettings.Contact.RcsNumber%><br />
					Numero Siret : <%=ERPStoreApplication.WebSiteSettings.Contact.SiretNumber%><br />
					Numero TVA : <%=ERPStoreApplication.WebSiteSettings.Contact.VATNumber%><br />
					</p>
                    </div>
                </div>
        </div>
    </div>

</asp:Content>



