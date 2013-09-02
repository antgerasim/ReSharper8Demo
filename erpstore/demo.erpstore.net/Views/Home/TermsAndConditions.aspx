<%@ Page Language="C#" MasterPageFile="~/Views/Shared/2Columns.Master" Inherits="System.Web.Mvc.ViewPage<WebSiteSettings>" %>

<asp:Content ID="aboutHead" ContentPlaceHolderID="HeaderContent" runat="server">
	<title>Conditions générales de vente de
		<%=Model.SiteName %></title>
</asp:Content>
<asp:Content ID="aboutContent" ContentPlaceHolderID="MainContent" runat="server">
	<div id="grid3">
        <div id="sgrid31">
			<%Html.ShowMenu("RightHelp.ascx");%>
        </div>
        
        <div id="sgrid32">
            <h2 class="titleh2"><span>Conditions générales de vente</span></h2>
            <div class="bloc chemin">
                 <span><a href="<%=Url.HomeHref()%>">accueil</a></span><b>Conditions générales de vente</b>
            </div>   
                <div class="corner aide">
                    <div class="corner texte bloc">
                        <h4 class="corner"><a name="commandes">1 COMMANDES.</a></h4>
                        <p>
                            Les commandes doivent indiquer clairement les références et désignations des produits,
                            tels qu'ils figurent dans les catalogues des fournisseurs ou tarifs. Nous déclinons
                            toutes responsabilités lorsque cette clause n'aura pas été appliquée et tous les
                            frais de retour des produits livrés en conformité aux références et désignations
                            figurant sur les bons de commandes, seront à la charge du client. 
                        </p>
                        <p>
                            Les commandes reçues et les engagements ne seront définitifs pour <%=Model.SiteName%> qu'après acceptation
                            écrite de notre part formalisée par un accusé de réception. Les annulations de commandes
                            ne sont acceptées qu'après accord de nos services et, suivant les conditions négociées
                            entre <%=Model.SiteName%> et le client.
                         </p>
                        <h4 class="corner"><a name="commandes_speciales">2 COMMANDES SPECIALES.</a></h4>
                        <p>
                            Le client s'engage à communiquer à <%=Model.SiteName%> toutes les informations nécessaires pour
                            la réalisation de toute commande spéciale.
                        </p>
                        <h4 class="corner"><a name="prix">3 PRIX.</a></h4>
                        <p>
                            Les prix de ventes sont les tarifs en vigueur de nos fournisseurs à la date de confirmation
                            de commande de <%=Model.SiteName%>, sauf accord entre les deux parties. Toutefois, une majoration
                            des prix peut être faite si le client demande un conditionnement autre que celui
                            du fournisseur.
                        </p>
                        <h4 class="corner"><a name="reglement">4 R&Egrave;GLEMENT.</a></h4>
                        <p>
                            Les conditions et lieux de paiements sont ceux indiqués sur les factures et relevés.
                            En cas de défaut de paiement par le client, le contrat de vente sera résilié automatiquement
                            de plein droit après mise en demeure, notifié par <%=Model.SiteName%>, et non suivi d'effet.
                            Il en sera de même lorsque la commande stipulera le versement d'un acompte non effectué
                            à la date prévue. Toute somme impayée à son échéance fera courir des intérêts calculés
                            sur la base du taux d'escompte de la Banque de France majoré de 1.5 points de plein
                            droit et sans mise en demeure préalable.
                        </p>
                        <h4 class="corner"><a name="transport">5 TRANSPORT.</a></h4>
                        <p>
                            Les produits voyagent aux risques et périls de l'acheteur, quel que soit le mode
                            de transport et les conditions de mise à disposition, sauf autrement décidé par
                            les parties et précisé dans l'accusé de réception de commande.
                         </p>
                        <h4 class="corner"><a name="delai_mise_disposition">6 D&Eacute;LAI DE MISE A DISPOSITION.</a></h4>
                        <p>
                            Les délais de livraison figurant sur nos remises de prix ou accusés de réception
                            de commandes sont purement indicatifs et ne constituent pas un engagement ferme
                            de notre part. Quand bien même, à titre exceptionnel, ces délais de livraison auraient
                            été acceptés par nos services, ces délais ne pourront plus être opposés en aucune
                            façon et il y aura annulation de ce délai dans les cas suivants&nbsp;: - En cas
                            de retard de livraison du fait d'un fournisseur imposé par le client. -Dans le cas
                            ou l'exécution de la commande nécessite des précisions complémentaires ou un accord
                            du client qui ne nous est pas parvenu à temps. -Non-règlement d'une seule de nos
                            factures à la date prévue pour quelque clause que ce soit.
                        </p>
                        <h4 class="corner"><a name="reclamations">7 R&Eacute;CLAMATIONS ET RETOURS.</a></h4>
                        <p>
                            Tout retour d'un produit doit préalablement faire l'objet d'un accord formel entre
                            <%=Model.SiteName%> et le client.
                        </p>
                
                        <p>
                        <ul type="a">
                            <li>Les réclamations pour retour des produits ne pourront être prises en considération
                                que si elles sont formulées par écrit dans les deux mois qui suivent la livraison.
                                Ces réclamations n'excluent pas les réserves éventuelles à l'encontre des transporteurs
                                qui resteront à la charge du client quel que soit le mode de transport et les conditions
                                de livraisons adoptées.
                            </li>
                            <li>Les réclamations et demandes d'avoirs doivent toujours indiquer les numéros et dates
                                des bons de livraisons sur lesquels figurent les quantités en litiges, références
                                <%=Model.SiteName%>, et le motif détaillé de la réclamation.</li>
                            <li>Les matériels retournés doivent parvenir à notre magasin et les colis doivent porter,
                                de façon apparente, le nom du client. Tout retour devra être accompagné d'un bon
                                de livraison rappelant le numéro du bon de livraison <%=Model.SiteName%> ainsi que le numéro
                                d'autorisation de retour.
                            </li>
                            <li>Pour le retour de l'étranger, il doit être spécifié sur la déclaration au transporteur&nbsp;:&nbsp;"&nbsp;Marchandises
                                françaises en retour en franchises de douane&nbsp;". A défaut, tous les frais inhérents
                                au retour seront entièrement à la charge du client.
                            </li>
                            <li>En cas de livraison défectueuse, notre responsabilité se limite au remplacement
                                pur et simple et exclut tout autre dédommagement de quelque nature que ce soit.
                            </li>
                        </ul>
                        </p>
                        <h4 class="corner"><a name="reserve_propriete">8 R&Eacute;SERVE DE PROPRI&Eacute;T&Eacute;.</a></h4>
                        <p>
                            Le vendeur se réserve la propriété des produits et pièces livrées jusqu'au paiement
                            complet du prix et application des dispositions de la loi N 80-335 du 12 mai 1980.
                            A cet égard ,ne constitue pas un paiement, au sens de la présente disposition ,
                            la remise de traites ou tout titre créant des obligations de payer. Le client est
                            autorisé, dans le cadre de l'exploitation normale de sa société, à revendre les
                            produits livrés. Mais il ne peut ni les donner en gage, ni en transférer la propriété
                            à titre de garantie. L'autorisation de revente est automatiquement retirée en cas
                            de cessation de paiement du client.
                        </p>
                        <h4 class="corner"><a name="garanties">9 GARANTIES.</a></h4>
                        <p>
                            Tous nos produits ont la garantie du fournisseur, à condition qu'ils aient été stockés
                            par le client dans les conditions préconisées par le fournisseur. Les garanties
                            sont exclues dans les cas suivants&nbsp;:
                            <ul>
                                <li>a) Le produit aura été transformé ou modifié
                                sans notre accord écrit au préalable. La garantie ne couvre pas les détériorations
                                dues à une négligence, une mauvaise utilisation ou un mauvais entretien. </li>
                               <li> b)&nbsp;Notre
                                garantie ne s'étend pas aux produits ou accessoires non fournis par <%=Model.SiteName%>.</li>
                            </ul>
                         </p>
                        <h4 class="corner"><a name="juridiction">10 JURIDICTION.</a></h4>
                        <p>
                            Tous différents découlant du présent contrat seront tranchés par les tribunaux d'Albi. 
                            La loi française s'applique aux présentes Conditions Générales de Vente
                            à l'exclusion de tout autre. Nonobstant toute clause contraire, toute vente ou commande
                            emporte de plein droit de la part du client, l'acceptation de nos Conditions Générales
                            de Vente. En l'absence de contrat écrit et signé par les deux parties, nos Conditions
                            Générales de Vente, avec notre confirmation de commande et nos factures, représentent
                            l'intégralité de l' accord entre <%=Model.SiteName%> et le client.&nbsp;
                        </p>
                        <br/>
                        <br/>
                   </div>
                </div>
        </div>
    </div>

</asp:Content>

