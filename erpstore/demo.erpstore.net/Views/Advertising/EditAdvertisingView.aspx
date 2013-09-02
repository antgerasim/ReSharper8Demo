<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<AdvertisingView>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EditAdvertisingView</title>
</head>
<body>
    <div>
		<% Html.BeginForm("EditAdView", "Advertising"); %>
		Title : <%=Html.TextBox("title", Model.Title, new { size = 80 })%><br />
		DestinationUrl : <%=Html.TextBox("destinationurl", Model.DestinationUrl, new { size = 80 })%><br />
		ImageUrl : <%=Html.TextBox("imageurl", Model.ImageUrl, new { size = 80 }) %><br />
		Zone : <%=Html.TextBox("area") %><br />
		<%=Html.Hidden("adid", ViewData["adid"]) %>
		<%=Html.Hidden("Id") %>
		<input type="submit" value="Sauvegarder"/><br />
		<% Html.EndForm(); %>
    </div>
</body>
</html>
