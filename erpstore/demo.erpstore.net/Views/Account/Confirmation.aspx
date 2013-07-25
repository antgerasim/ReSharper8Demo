<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<User>" %>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">

        <h2 class="titleh2">
            <span>
                Identification confirm�e
            </span>
        </h2>
        
        <div class="chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span><span><b>Tableau de bord</b></span>Bienvenue <b><a href="<%=Url.EditUserHref()%>" title="Modifier ses informations personnelles"><%=Model.FullName%></a></b> identification confirm�e !
        </div>
        
        <div class="form_elements"> 
        
        <fieldset class="form" id="form_etape4">
            <legend class="corner">R�captitulatif des informations fournies :</legend>
            
                <p><b><%=Model.FullName%></b></p>
                <p>
                    La cr�ation de votre compte est confirm�.
                </p>
                <p>&nbsp;</p>
                <p>
                Nous vous remercions de votre confiance.
                </p>
                <p>Votre identifiant d'acc�s est : <b><%=Model.Login%></b></p>
        </fieldset>
        
        </div>

</asp:Content>


