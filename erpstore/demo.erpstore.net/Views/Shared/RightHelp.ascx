<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>
     	<a href="<%=Url.ContactHref()%>" title="nous contacter"  style="background:none; list-style:none">
        	<img style="width:200px;" src="/content/images/pubs/photo_contact.jpg" alt="nous contacter"/>
        </a>

    <h2 class="titleh2"><span>Menu aide</span></h2>
    <div class="menu3">
    	<ul class="menu">
            <li class="navitem"><a href="<%=Url.HelpHref()%>#virement_bancaire"><span>Paiement par virement bancaire</span></a></li>
            <li class="navitem"><a href="<%=Url.HelpHref()%>#cheque_bancaire"><span>Paiement par ch�que bancaire</span></a></li>
    	</ul>
	</div>
    <h2 class="titleh2"><span>Conditions g�n�rales de vente</span></h2>
    <div class="menu3">
    	<ul class="menu">
           <li class="navitem"><a href="<%=Url.TermsAndConditionsHref()%>#commandes"><span>Commandes</span></a></li>
           <li class="navitem"><a href="<%=Url.TermsAndConditionsHref()%>#commandes_speciales"><span>Commandes sp�ciales</span></a></li>
           <li class="navitem"><a href="<%=Url.TermsAndConditionsHref()%>#prix"><span>Prix</span></a></li>
           <li class="navitem"><a href="<%=Url.TermsAndConditionsHref()%>#reglement"><span>R�glement</span></a></li> 
           <li class="navitem"><a href="<%=Url.TermsAndConditionsHref()%>#transport"><span>Transport</span></a></li> 
           <li class="navitem"><a href="<%=Url.TermsAndConditionsHref()%>#delai_mise_disposition"><span>D�lai de mise � disposition</span></a></li>
           <li class="navitem"><a href="<%=Url.TermsAndConditionsHref()%>#reclamations"><span>R�clamations</span></a></li> 
           <li class="navitem"><a href="<%=Url.TermsAndConditionsHref()%>#reserve_propriete"><span>R�serve de propri�t�</span></a></li>
           <li class="navitem"><a href="<%=Url.TermsAndConditionsHref()%>#garanties"><span>Garanties</span></a></li>
           <li class="navitem"><a href="<%=Url.TermsAndConditionsHref()%>#juridiction"><span>Juridiction</span></a></li>
        </ul>
	</div>
    <h2 class="titleh2"><span>Politique de confidentialit� des donn�es</span></h2>
    <div class="menu3">
    	<ul class="menu">
           <li class="navitem"><a href="<%=Url.LegalInformationHref()%>#cookies"><span>Gestion des cookies</span></a></li>
           <li class="navitem"><a href="<%=Url.LegalInformationHref()%>#protection"><span>Protection des donn�es personnelles</span></a></li>
           <li class="navitem"><a href="<%=Url.LegalInformationHref()%>#securite"><span>S�curit� des transactions</span></a></li>
           <li class="navitem"><a href="<%=Url.LegalInformationHref()%>#respect"><span>Respect&nbsp;des&nbsp;r�glementations Fran�aise&nbsp;et&nbsp;Europ�enne</span></a></li> 
        </ul>
    </div>


