<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<IEnumerable<Profession>>" %>
<div class="bloc menu_pros">
<img src="/content/images/pubs/pub_votre-activite.jpg" alt="votre activité" />
    <ul class="menu">
    <%foreach (var profession in Model) { %>
		<li><a href="<%=Url.Href(profession)%>" title="<%=profession.Name %>"><span><%=profession.Name %></span></a></li>	  
    <% } %>
    </ul>
</div>




