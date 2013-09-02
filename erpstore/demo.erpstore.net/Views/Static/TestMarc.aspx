<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>TestMarc</title>
</head>
<body>
    <div>
		Ceci est ma page de test
		
		<%=Html.Action("ShowFooter", "Home", new { viewName = "footer.ascx" }) %>
    </div>
</body>
</html>
