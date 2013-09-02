<%@ Page %>
<script runat="server" language="c#">
	
protected override void OnLoad(EventArgs e)
{
	HttpContext.Current.RewritePath(Request.ApplicationPath, false);
	IHttpHandler httpHandler = new MvcHttpHandler();
	httpHandler.ProcessRequest(HttpContext.Current);
}

</script>
