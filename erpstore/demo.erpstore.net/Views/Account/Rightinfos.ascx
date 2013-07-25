<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
<div id="infoscompte">
   	<div id="infosclient">
        <%Html.RenderPartial("~/views/account/status.ascx");%>
    </div>
    <div id="infoscommande">
        <%Html.ShowCartStatus();%>
        <%Html.ShowQuoteCartStatus();%>
    </div>
</div>

