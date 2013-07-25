<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Address>" %>
<p><label for="recipientName">Nom du destinataire :</label>
<span><%=Html.TextBox("recipientName") %></span>
<%=Html.ValidationMessage("recipientName", "*") %></p>
<p><label for="street">Rue ou lieu dit :</label>
<span><%=Html.TextArea("street") %></span>
<%=Html.ValidationMessage("street", "*") %></p>
<p><label for="zipCode">Code postal :</label>
<span><%=Html.TextBox("zipCode")%></span>
<%=Html.ValidationMessage("zipCode", "*")%></p>
<p><label for="city">Ville :</label>
<span><%=Html.TextBox("city")%></span>
<%=Html.ValidationMessage("city", "*")%></p>
<p><label for="countryId">Pays :</label>
<%= Html.DropDownList("countryId", Html.CountryList())%>
<%= Html.ValidationMessage("countryId")%></p>