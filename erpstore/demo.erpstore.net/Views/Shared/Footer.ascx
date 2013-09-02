<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<WebSiteSettings>" %>
<!-- footer //-->
<div id="footer" class="corner1-bottom">
    
<!-- bottom //-->
<div id="bottom">
         	<ul class="bottomnav" id="bottomnav1">
                <li id="bottomnav13"><a href="<%=Url.TermsAndConditionsHref()%>">Conditions générales de vente</a></li>
                <li id="bottomnav14"><a href="<%=Url.LegalInformationHref()%>" title="informations légales">Informations légales</a></li>
                <li id="bottomnav16"><a href="/statique/partenaires">Partenaires</a></li>
                <li id="bottomnav17"><a href="<%=Url.ContactHref()%>">Contact</a></li>
                <li id="bottomnav18">V1.0</li>
           </ul>
 </div>
	<!-- fin bottom //-->  
    
    <!-- last //-->
    <div id="last">
        <div style="text-align:center;">
        <small>Tous les tarifs affichés sont hors taxes (HT) et en Euros, les photos sont affichées à titre indicatif et ne sont pas contractuelles, les marques citées appartiennent à leurs propriétaires respectifs.</small>
        </div>  
        <br/>  
        Copyright © <%=string.Format("{0:yyyy}", DateTime.Now)%> <a href="<%=Url.ContactHref() %>"><%=Html.Encode(Model.SiteName)%></a>, tous droits reservés.
        <br/>
        <small><a href="http://www.sid-networks.com" title="agence de communication - votre site internet  : e-commerce, blog, CMS SPIP, Joomla, dans le gers, Toulouse, Midi-Pyrénées..." target="_blank">SID-Networks - Création de sites Internet sur mesure - agence de communication</a> </small>
    </div>
	<!-- fin last //-->   
</div>
<!-- fin footer //-->

