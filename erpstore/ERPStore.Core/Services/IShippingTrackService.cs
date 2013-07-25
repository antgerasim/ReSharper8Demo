using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	/**
Voici donc une liste à jour des URL de tracking des principaux transporteurs. La chaîne “123456789″ est à remplacer par le numéro de colis à tracer:

Chronopost :
http://www.fr.chronopost.com/web/fr/tracking/suivi_inter.jsp?listeNumeros=123456789

Coliposte Particulier (Colissimo, Colissimo International) :
http://www.coliposte.fr/particulier/suivi_particulier.jsp?colispart=123456789

Coliposte Pro :
https://www.coliposte.net/pro/services/main.jsp?m=12003010&colispro=123456789

E-COMO :
http://www.coliposte.net/ec/suivi_ec.jsp?colispart=123456789

Courrier Suivi, Lettre Max, Lettre Suivie, Lettre Recommandée :
http://www.csuivi.courrier.laposte.fr/default.asp?EZ_ACTION=rechercheRapide&tousObj=&numObjet=123456789

GLS :
http://www.gls-group.eu/276-I-PORTAL-WEB/content/GLS/FR01/FR/5004.htm?txtAction=71010&un=2501859001&pw=grandvision&rf=123456789&crf=null&lc=FR&no=2440501

TNT :
http://www.tnt.com/webtracker/tracker.do?cons=123456789&trackType=CON&saveCons=N

UPS :
http://wwwapps.ups.com/etracking/tracking.cgi?InquiryNumber1=123456789&loc=fr_FR&TypeOfInquiryNumber=T

DHL USA :
http://track.dhl-usa.com/TrackByNbr.asp?nav=Tracknbr&ShipmentNumber=123456789

DHL France :
http://www.dhl.fr/publish/fr/fr/eshipping/track.high.html?pageToInclude=RESULTS&type=trackindex&brand=I&AWB=123456789

FEDEX :
http://fedex.com/Tracking?ascend_header=1&clienttype=dotcomreg&cntry_code=fr&language=french&tracknumbers=123456789

EXAPAQ :
http://e-trace.ils-consult.fr/exa-webtrace/webclients.aspx?sdg_landnr=250&sdg_mandnr=756&sdg_lfdnr=12345678&cmd=SDG_SEARCH

CIBLEX :
http://www.ciblex.fr/extranet/client/corps.php?module=colis&colis=123456789

SCHENKER :
http://was.schenker.nu/ctts-a/com.dcs.servicebroker.http.HttpXSLTServlet?request.service=CTTSTYPEA&request.method=search&clientid=&language=fr&country=FR&reference_type=*PKG&reference_number=12
	 * **/
	public interface IShippingTrackService
	{
		string Name { get; }
		string GetUrlTracker(Models.DeliveryPackage deliveryPackage);
	}
}
