<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>testemailmarc</title>
</head>
<body>
    <div>
		<%
			var message = new System.Net.Mail.MailMessage();
			message.To.Add("chouteau@gmail.com");
			message.From = new System.Net.Mail.MailAddress("quincaillerie.pro@erpstore.net");
			message.Bcc.Add("contact@quincaillerie.pro");	
			message.Subject = "Test marc";
			message.Body = "test marc";

			var client = new System.Net.Mail.SmtpClient();

			client.Send(message);	
		%>
    </div>
</body>
</html>
