<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="accountstatus">
	<ul>
           <% if (!Request.IsAuthenticated) { %>
				<%--<li class="status-connect"><img src="/content/images/flechegrise.png" alt=""/>&nbsp;<a href="<%=Url.AccountHref()%>" title="me connecter" class="connect">Connexion</a></li>--%>
           <li class="status-deconnect">Bonjour</li>
           <% } else {
               var user = Context.User.GetUserPrincipal().CurrentUser;
           %>
           <li class="status-deconnect">
           <%=user.Presentation.GetLocalizedName()%>&nbsp;<%=user.FirstName%>&nbsp;<%=user.LastName%><br/>
		<small>(<a href="<%=Url.LogoffHref()%>">déconnexion</a>)</small>
		<br/>
           </li>
           <% } %>
           <li class="status-compte">
	           <img src="/content/images/flechegrise.png" alt=""/>&nbsp;
	           <% if (Request.IsAuthenticated) { %>
	           <a href="<%=Url.AccountHref()%>"><b>Votre compte</b></a>
	           <% } else { %>
	           <a href="<%=Url.LoginHref() %>"><b>Se connecter</b></a>
	           <% } %> 
	           <span><a href="<%=Url.HelpHref()%>">Aide</a> | <a href="<%=Url.ContactHref()%>">Contact</a></span>
            </li>
        </ul>

</div>