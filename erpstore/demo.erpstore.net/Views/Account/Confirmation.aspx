<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<User>" %>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">

        <h2 class="titleh2">
            <span>
                Identification confirmée
            </span>
        </h2>
        
        <div class="chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><b>Tableau de bord</b></span>Bienvenue <b><a href="<%=Url.EditUserHref()%>" title="Modifier ses informations personnelles"><%=Model.FullName%></a></b> identification confirmée !
        </div>
        
        <div class="form_elements"> 
        
        <fieldset class="form" id="form_etape4">
            <legend class="corner">Récaptitulatif des informations fournies :</legend>
            
                <p><b><%=Model.FullName%></b></p>
                <p>
                    La création de votre compte est confirmé.
                </p>
                <p>&nbsp;</p>
                <p>
                Nous vous remercions de votre confiance.
                </p>
                <p>Votre identifiant d'accès est : <b><%=Model.Login%></b></p>
        </fieldset>
        
        </div>

</asp:Content>


