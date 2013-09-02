<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<User>" %>

<asp:Content ID="registerContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
    <div id="sgrid33">

        <h2 class="titleh2">
            <span>
                Confirmation de la creation de votre compte
            </span>
        </h2>
        
        <div class="bloc chemin">
            <span><a href="<%=Url.HomeHref()%>">accueil</a></span>
            <b>Finalisation de votre compte</b>
        </div>
        <div class="form_elements">        
            <fieldset class="form" id="form_etape1">
                <legend class="corner">Votre inscription est confirmée :</legend>
                    <p><b><%=Model.FullName%></b>,<br/><br/>
                    vous allez recevoir un email de confirmation concernant la création de votre compte,<br/>
                    celui-ci contiendra un lien sur lequel il faudra cliquer pour terminer la création de votre compte.
                    </p>    
                    <p>Nous vous remercions de la confiance que vous nous accordez</p>
                    <br />
                    <br />
                   <table class="go_commande cols">
                       <tr>
                            <td class="col col-33 col1">
                                <span>
                                <% if (ViewData["returnUrl"] != null) { %>
                                    <a class="go_commande_no" href='<%=ViewData["returnUrl"]%>'>Retour</a>
                                <% } else { %> 
                                    <a class="go_commande_no" href="<%=Url.HomeHref() %>">Retour</a>
                                <% } %>
                                </span>
                            </td>
                        </tr>
                    </table>    
            </fieldset>
        </div>
        
    </div>

</div>
   
</asp:Content>

