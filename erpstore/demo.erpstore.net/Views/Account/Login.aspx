<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexHead" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Connexion au compte</title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
    <div id="sgrid33">

      <h2 class="titleh2">
           <span>
               Se connecter
           </span>
      </h2>
        
      <div class="bloc chemin">
          <span><a href="<%=Url.HomeHref()%>">accueil</a></span>
          <b>identification</b>
      </div>
        
    <% using (Html.BeginLoginForm()) { %>

	<%=Html.Hidden("returnUrl", Request["returnUrl"])%>
        
         <div class="form_elements">

            <fieldset class="form" id="form_insc">
                 <legend class="corner">Je suis un nouveau client</legend>
                      <ul class="element_annexe">
                          <li><b><%=Html.RegisterAccountLink("M'inscrire")%></b></li>
                      </ul>
            </fieldset>
            <br class="clear" />
            <fieldset class="form" id="form_connect">
                <legend class="corner">J'ai déjà un compte</legend>                   
				<%Html.RenderPartial("ValidationSummary");%>
                      <p>
                            <label for="username">Identifiant :</label>
                            <span class="form_element_input"><input type="text" name="username" value='<%=Request["username"] %>' /></span>
                            <%= Html.ValidationMessage("username") %>
                        </p>
                        <p>
                            <label for="password">Mot de passe :</label>
                            <span class="form_element_input"><%= Html.Password("password") %></span>
                            <%= Html.ValidationMessage("password") %>
                        </p>
                        <p class="form_element_radio input">
                            <%= Html.CheckBox("rememberMe") %><label class="inline" for="rememberMe">Se souvenir de ma connexion ?</label>
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
                                     <input type="submit" value="Me connecter" class="corner"/>
                                </td>
                            </tr>
                        </table>    
                        <ul>
                        	<li><small><%=Html.RecoverPasswordLink("Retrouver mon mot de passe")%></small></li>
                        </ul>
               </fieldset>
          </div>
    <% } %>

    </div>
</div>
</asp:Content>

