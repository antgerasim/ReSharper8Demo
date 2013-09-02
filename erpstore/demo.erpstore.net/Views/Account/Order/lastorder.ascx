<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Order>" %>
<p>
Votre commande N°<%=Model.Code%> est validée par notre centre serveur.
</p>

<p>
Voici le récapitulatif :
</p>

<%Html.RenderPartial("~/views/account/order/orderdetail.ascx", Model);%>

<br/>
<p>Vous pouvez suivre votre commande directement sur la page de <a href="<%=Url.AccountHref() %>" title="suivre ma commande">votre compte</a>.</p>




