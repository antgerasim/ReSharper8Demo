<%@ Page Title="" Language="C#" MasterPageFile="Order.Master" Inherits="System.Web.Mvc.ViewPage<ISaleDocument>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
    	<div class="corner aide">
        <h2 class="titleh2"><span><a name="virement_bancaire">Nous vous remercions pour votre confiance</a></span></h2>
        <div class="corner texte bloc">
            <p>Notez votre numéro de commande : <b><%=Model.Code%></b>.
            <br/>
                <% switch (Model.PaymentMode)
                   {
                       case PaymentMode.Check :
                           Html.RenderPartial("~/views/account/order/payment/OrderPaymentByCheck.ascx", Model);
                           break;
                       case PaymentMode.WireTransfer :
                           Html.RenderPartial("~/views/account/order/payment/OrderPaymentByWireTransfer.ascx", Model);
                           break;
                   }
               %>
            <div class="separateur">&nbsp;</div>
            <br/>
            Vous aller recevoir un email de confirmation.
            <br/>
            Vous pouvez suivre votre commande directement sur la page de <a href="<%=Url.AccountHref() %>" title="suivre ma commande">votre compte</a>.</p>
        </div>
        </div>
    </div>
</div>

<!-- Google Code for Commande Conversion Page -->
<script type="text/javascript"><!--
var google_conversion_id = 1022039019;
var google_conversion_language = "fr";
var google_conversion_format = "3";
var google_conversion_color = "ffffff";
var google_conversion_label = "ygeJCJXX0wEQ66es5wM";
var google_conversion_value = <%=Model.GrandTotal %>;
//-->
</script>
<script type="text/javascript" src="http://www.googleadservices.com/pagead/conversion.js">
</script>
<noscript>
<div style="display:inline;">
<img height="1" width="1" style="border-style:none;" alt="" src="http://www.googleadservices.com/pagead/conversion/1022039019/?value=10&amp;label=ygeJCJXX0wEQ66es5wM&amp;guid=ON&amp;script=0"/>
</div>
</noscript>

</asp:Content>

