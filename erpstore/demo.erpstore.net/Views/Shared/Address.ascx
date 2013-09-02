<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Address>" %>
<p><label for="recipientName">Nom du destinataire :</label>
<span><%=Model.RecipientName %></span>
</p>
<p><label for="street">Rue :</label>
<span><%=Model.Street %></span>
</p>
<p><label for="zipCode">Code postal :</label>
<span><%=Model.ZipCode%></span>
</p>
<p><label for="city">Ville :</label>
<span><%=Model.City%></span>
</p>
<p><label for="countryId">Pays :</label>
<%=Model.Country.LocalizedName%>
</p>