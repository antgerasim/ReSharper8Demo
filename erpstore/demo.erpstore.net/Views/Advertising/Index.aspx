<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IList<Advertising>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Index</title>
</head>
<body>
    <div>
		Liste des publicités
		<hr />
		<% foreach (var item in Model) { %>
			Titre : <%=item.Title %>&nbsp;|&nbsp;<%=Html.ActionLink("Modifier", "Edit", new { controller = "Advertising", id = item.Id, }) %><br />
			<br />
			Liste des vues<br />
			<% foreach (var adview in item.AdvertisingViewList) { %>
				Titre : <%=adview.Title%><br />
				Url de destination : <%=adview.DestinationUrl %><br />
				Url de l'image : <%=adview.ImageUrl %><br />
				Durée d'affichage en seconde : <%=adview.DisplayDuration %><br />
				<%=Html.ActionLink("Modifier", "EditAdView", new { controller = "Advertising", id = adview.Id }) %>
				&nbsp;|&nbsp;
				<%=Html.ActionLink("Supprimer", "DeleteAdView", new { controller = "Advertising", id = adview.Id })%><br />
			<% } %>			
			<%=Html.ActionLink("Ajouter une vue", "NewAdView", new { controller = "Advertising", adid = item.Id }) %>
			<br />
			<%=Html.ActionLink("Supprimer", "Delete", new { controller = "Advertising", id = item.Id })%><br />
		<% } %>
		<hr />
		<%=Html.ActionLink("Ajouter une publicité", "New", "Advertising") %>
    </div>
</body>
</html>
