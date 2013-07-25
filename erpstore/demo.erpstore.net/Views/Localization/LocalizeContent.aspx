<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IList<ContentLocalization>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Traduction du contenu texte</title>
</head>
<body>
    <div>
		<p>Traduction du terme : <strong><%=ViewData["Key"]%></strong> </p>
		<% Html.BeginForm("SaveLocalizedContent", "Localization", FormMethod.Post); %>
		<table>
			<thead>
				<tr>	
					<td>Language</td>
					<td>Traduction</td>
				</tr>
			</thead>
			<% foreach (var item in Model) { %>
				<tr>
					<td><%=item.Language%>
					<%=Html.Hidden("lg", item.Language) %>
					</td>
					<td><%=Html.TextBox("value", item.Value)%>
					</td>
				</tr>
			<% } %>
		</table>
		<%=Html.Hidden("content", ViewData["content"]) %>
		<input type="submit" value="Sauvegarder" />
		<% Html.EndForm(); %>
    </div>
</body>
</html>
