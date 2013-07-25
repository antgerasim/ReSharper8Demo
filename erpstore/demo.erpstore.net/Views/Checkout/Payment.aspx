<%@ Page Title="" Language="C#" MasterPageFile="Order.Master" Inherits="System.Web.Mvc.ViewPage<OrderCart>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Panier de commande : Mode de paiement</title>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="grid3">
	<div id="sgrid33">
        <div class="mbilling">
			<h4><span>commande</span></h4>
            <ul class="nav mbilling_etapes">
             	<li class="menubilling_etape" id="mbilling_etape1"><img src="/content/images/commande/commande_1off.jpg" alt="identification"/><span><b>1</b> Panier</span></li>
                <li class="menubilling_etape" id="mbilling_etape2"><img src="/content/images/commande/commande_1off.jpg" alt="identification"/><span><b>2</b> Adresses</span></li>
                <li class="menubilling_etape" id="mbilling_etape3"><img src="/content/images/commande/commande_1off.jpg" alt="adresse"/><span><b>3</b> Configuration</span></li>
                <li class="menubilling_etape mbilling_etape_on" id="mbilling_etape4"><img src="/content/images/commande/commande_1on.jpg" alt="récapitulatif"/><span><b>4</b> Paiement</span></li>
                <li class="menubilling_etape" id="mbilling_etape5"><img src="/content/images/commande/commande_1off.jpg" alt="paiement"/><span><b>5</b> Récapitulatif</span></li>
            </ul>
        </div> 
        <br class="clear"/>

	<% Html.BeginForm(); %>

<table class="cols colsforms colspaiement">
	<tr>
    	<td class="col">
        <div class="form_elements form_element_radio">
            <fieldset class="form">
                <legend class="corner">Sélectionner le mode de reglement de cette commande</legend>
                    <% var paymentList = ViewData["paymentList"] as List<Payment>; %>
                    <% foreach (var payment in paymentList) { %>
                        <p id="paiement<%=payment.Name %>"><label><img src="<%=payment.PictoUrl%>" alt="" style="vertical-align:middle;"/> <%=payment.Description%> </label><span style="padding:1.4em 0;"><input type="radio" name="paymentModeName" value="<%=payment.Name%>" <%=payment.IsSelected ? "checked='checked'" : ""%>/></span></p>	   
                    <% } %>
            </fieldset>
          </div>
        </td>
    </tr>
</table>
<br class="clear"/>  
   <table class="go_commande cols">
       <tr>
            <td class="col col-33 col1"> <span><a class="go_commande_no" href='<%=Url.CheckOutConfigurationHref() %>'>Configuration</a></span></td>
            <td class="col col-33 col2"> <span>&nbsp;</span></td>
            <td class="col col-33 col3"> <span><input type="submit" value="Etape suivante"/></span></td>
        </tr>
    </table>  

    <% Html.EndForm(); %>
    
	<%Html.RenderPartial("~/views/Shared/RightMenu2.ascx");%>
	</div>
</div>	


</asp:Content>