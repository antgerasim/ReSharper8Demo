<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Default.Master" Inherits="System.Web.Mvc.ViewPage<WebSiteSettings>" %>

<asp:Content ID="aboutHead" ContentPlaceHolderID="HeaderContent" runat="server">
    <title>Aide</title>
</asp:Content>

<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
	<div id="grid3">
        <div id="sgrid31">
			<%Html.ShowMenu("RightHelp.ascx");%>
        </div>
        
        <div id="sgrid32">
            <h2 class="titleh2"><span>Aide</span></h2>
            <div class="bloc chemin">
                 <span><a href="<%=Url.HomeHref()%>">accueil</a></span><b>Aide</b>
            </div> 
                <div class="corner aide">
                <h3><a name="virement_bancaire">Paiement par Virement bancaire</a></h3>
                <div class="corner texte bloc">
                    <p>
                    <strong>Un virement bancaire est une opération de transfert de fonds d'un compte à un autre.
                    Il s’effectue électroniquement entre deux comptes bancaires, qui ne sont pas nécessairement tenus dans la même agence ou la même banque.
                    Depuis 2008, les virements bancaires font l’objet d’une harmonisation au niveau européen.</strong>
                    </p>
                    <h4 class="corner">
                        Les différents types de virement
                    </h4>
                    <p>
                        On distingue différents types de virement bancaire :
                        <ul>
                        <li>le virement interne (entre deux comptes ouverts dans la même banque) et le virement externe (dans deux banques différentes)</li>
                        <li>le virement domestique (réalisé dans le même pays), le virement à destination d'un pays de l'Union européenne et le transfert international.</li>
                        <li>le virement ponctuel et le virement permanent (virement automatique pour le paiement du Loyer par exemple).</li>
                        </ul>
                    </p> 
                
                    <h4 class="corner">Fonctionnement</h4>
                    <p>  
                    Un virement bancaire est toujours initié par le titulaire du compte à débiter, également appelé l'émetteur du virement ou le donneur d'ordre.
                    La personne qui reçoit l'argent sur son compte est appelée le bénéficiaire.
                    </p>
                    <p>
                    Le transfert de fonds est effectué électroniquement.
                    Cette opération exige pour la banque émettrice de connaître les coordonnées bancaires précises du compte bénéficiaire.
                    </p>
                    <p>
                    <u>Ces coordonnées, à reporter sur l'ordre de virement, sont :</u>
                    <ul>
                    <li><b>le code IBAN</b> (International Banking Account Number) permettant d’identifier un compte bancaire au niveau international</li>
                    <li><b>le code BIC</b> (Bank Identifier Code) permettant d’identifier la banque du destinataire.</li>
                    </ul>
                    </p>
                    <p>
                   <big><b> En France, ces informations figurent sur les relevés d’identité bancaire (RIB) et les relevés de compte.</b></big>
                   <br/>
                   <br/>
                    Donner vos coordonnées bancaires ne vous expose pas à des prélèvements non-autorisés.<br/>Seules les opérations de crédit sont possibles sans autorisation explicite du titulaire du compte.
                    </p>
                    <p>
                    Après traitement par la ou les banques, les fonds virés sont crédités sur le compte du bénéficiaire.<br/>
                    <b>A noter :</b> un virement bancaire est une opération irréversible.
                    En cas d'erreur de l'émetteur, c'est le bénéficiaire du virement erroné qui doit à son tour émettre un virement dans l’autre sens.
                    </p>
                    <h4 class="corner">Harmonisation européenne : le virement SEPA</h4>
                    <p>
                    Depuis le 28 janvier 2008, les opérations de virement bancaire sont harmonisées au niveau européen, pour les virements domestiques (de France à France) aussi bien que pour les virements à destination d'un pays de l'Union européenne.
                    </p>
                    <p>
                    Avec la création progressive d'un espace unique de paiement appelé SEPA (Single European Payments Area), toute personne ayant un compte bancaire dans cet espace peut envoyer et recevoir des virements en euros dans les mêmes conditions qu'à l'intérieur de son pays.
                    </p>
                    <p>
                    Pour faire un virement SEPA, vous aurez toujours besoin des coordonnées bancaires (IBAN + BIC) du bénéficiaire.
                    Si vous effectuez un virement SEPA (en euros) sur un compte tenu dans une autre devise, c’est la banque du bénéficiaire qui assure la conversion à réception du virement.
                    Cette opération de change, distincte du virement proprement dit, est facturée à la personne par sa banque, aux conditions habituelles.
                    </p>
                    
                    <h4 class="corner">Frais</h4>
                    <p>
                    Si vous faites un virement entre deux comptes ouverts à votre nom dans la même agence, l'opération est en général effectuée gratuitement par votre banque.
                    Vous pouvez également effectuer vous-même l'opération par Minitel ou par Internet sans aucun frais.
                    </p>
                    <p>
                    En revanche l'émission d'un virement bancaire à destination d'un tiers, est généralement facturée par les banques pour un montant forfaitaire ne dépassant pas 2 à 3€.
                    La réception d'un virement est toujours gratuite.
                    </p>
                    <p>
                    Attention : pour effectuer un virement, votre compte doit être suffisamment approvisionné, sinon l'ordre de virement peut être rejeté.
                    Cet incident de paiement peut donner lieu à des frais bancaires. 
                    </p>
                </div>
                <hr />
                <br/>
                <br/>
                <h3><a name="cheque_bancaire">Paiement par chèque bancaire</a></h3>
                <div class="corner texte bloc">
                     <p>
                    Vous pouvez régler vos commandes sur notre site par chèque bancaire. En fin de commande, vous accédez à une page récapitulative contenant toutes les informations indispensables au traitement de votre commande :
                    <br/>
                    <br/>
                        <ul>
                            <li>le montant total de la commande en euros</li>
                            <li>l'ordre de paiement : <%=Model.Contact.DefaultAddress.RecipientName%></li>
                            <li>les informations à inscrire au dos de votre chèque : <b>le numéro de votre commande</b> et votre <b>adresse e-mail</b></li>
                            <li>l'adresse d'envoi :</li>
                        </ul>
                        </p>
                        <p>
                        <fieldset class="form">
                            <legend class="corner">Adresse de facturation</legend>
                            <br/>
                            <ul>
                                <li><b><%=Model.SiteName%></b>, Service Internet</li>
                                <li><b><%=Model.Contact.DefaultAddress.RecipientName%></b></li>
                                <li><b><%=Model.Contact.DefaultAddress.Street%></b></li>
                                <li><b><%=Model.Contact.DefaultAddress.ZipCode%>&nbsp;<%=Model.Contact.DefaultAddress.City%></b></li>
                                <li><b><%=Model.Contact.DefaultAddress.Country.LocalizedName%></b></li>
                            </ul>
                            <br/>
                        </fieldset>
                        </p>
                        <p>
                    Vous disposez d'un délai de 30 jours pour nous faire parvenir votre paiement. Passé ce délai, votre commande se voit annulée par notre Service Client qui vous en informe alors par e-mail.
                    </p>
                    <p>
                    À noter : Pour tout besoin d'information concernant le paiement par chèque ou votre commande, veuillez contacter notre Service client.
                    </p>
                    <p>
                    N'oubliez pas:
                        <ul>
                            <li>de signer votre chèque.</li>
                            <li>de poster votre chèque. Votre commande ne sera traitée qu'après encaissement de votre paiement.</li>
                            <li>d'imprimer la page de confirmation de commande, si vous le pouvez, et de la joindre à votre chèque dans votre courrier. Cela facilitera le traitement de votre commande.</li>
                        </ul>
                    </p>
                    <h4 class="corner">Important</h4>
                    <p>
                        <ul>
                            <li>Votre compte doit être ouvert dans une banque domiciliée en France (métropolitaine et DOM-TOM) ou à Monaco.</li>
                            <li>Votre chèque doit être rédigé en euros uniquement.</li>
                            <li>Ce mode de paiement ajoute en moyenne 7 à 10 jours au délai de traitement de votre commande.</li>
                            <li>Les eurochèques ne sont pas acceptés comme moyen de paiement.</li>
                        </ul>
                    </p>
                </div>
             </div>
        </div>
    </div>

</asp:Content>



