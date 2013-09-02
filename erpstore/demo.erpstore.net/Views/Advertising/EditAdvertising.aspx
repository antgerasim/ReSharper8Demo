<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<Advertising>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>EditAdvertising</title>
</head>
<body>
    <div>
		<%Html.BeginForm("Save", "Advertising", FormMethod.Post); %>
		<%=Html.Hidden("Id") %><br />
		Titre : <%=Html.TextBox("Title") %><br />
		<input type="submit" value="sauvegarder" /><br />
		<%Html.EndForm(); %>
    </div>
</body>
</html>
