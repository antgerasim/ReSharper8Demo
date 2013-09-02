using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace ERPStore.Models
{
    /// <summary>
    /// Liste des monnaies Norme ISO 4217
    /// http://fr.wikipedia.org/wiki/Codes_des_monnaies
    /// http://www.iso.org/iso/fr/support/faqs/faqs_widely_used_standards/widely_used_standards_other/currency_codes/currency_codes_list-1.htm
    /// </summary>
	[Serializable]
    public class Currency : EnumBaseType<Currency>
    {
        /*
        public static readonly Currency EUR = new Currency(1,"EUR", "EUR (Euros)", "EUR");
        public static readonly Currency USD = new Currency(2, "USD", "USD (Dollars)", "USD");
        public static readonly Currency GBP = new Currency(3, "GBP", "GBP (Livre Sterling)", "GBP");
        public static readonly Currency CNY = new Currency(4, "CNY", "CNY (Yuan)", "CNY");
         */

        public static readonly Currency AFN = new Currency(971,"AFN", "AFN (Afghani)", "AFN");
        public static readonly Currency ZAR = new Currency(710,"ZAR", "ZAR (Rand)", "ZAR");
        public static readonly Currency ALL = new Currency(8,"ALL", "ALL (Lek)", "ALL");
        public static readonly Currency DZD = new Currency(12,"DZD", "DZD (Dinar algérien)", "DZD");
        /// <summary>
        /// Euros
        /// </summary>
        public static readonly Currency EUR = new Currency(978,"EUR", "EUR (Euro)", "EUR");
        public static readonly Currency AOA = new Currency(973,"AOA", "AOA (Kwanza)", "AOA");
        public static readonly Currency XCD = new Currency(951,"XCD", "XCD (Dollar des Caraïbes orientales)", "XCD");
        public static readonly Currency ANG = new Currency(532,"ANG", "ANG (Florin des Antilles)", "ANG");
        public static readonly Currency SAR = new Currency(682,"SAR", "SAR (Riyal saoudien)", "SAR");
        public static readonly Currency ARS = new Currency(32,"ARS", "ARS (Peso argentin)", "ARS");
        public static readonly Currency AMD = new Currency(51,"AMD", "AMD (Dram arménien)", "AMD");
        public static readonly Currency AWG = new Currency(533,"AWG", "AWG (Florin d'Aruba)", "AWG");
        public static readonly Currency AUD = new Currency(36,"AUD", "AUD (Dollar australien)", "AUD");
        public static readonly Currency AZN = new Currency(944,"AZN", "AZN (Manat)", "AZN");
        public static readonly Currency BSD = new Currency(44,"BSD", "BSD (Dollar des Bahamas)", "BSD");
        public static readonly Currency BHD = new Currency(48,"BHD", "BHD (Dinar de Bahreïn)", "BHD");
        public static readonly Currency BDT = new Currency(50,"BDT", "BDT (Taka)", "BDT");
        public static readonly Currency BBD = new Currency(52,"BBD", "BBD (Dollar de Barbade)", "BBD");
        public static readonly Currency BYR = new Currency(974,"BYR", "BYR (Rouble bélorusse)", "BYR");
        public static readonly Currency BZD = new Currency(84,"BZD", "BZD (Dollar de Belize)", "BZD");
        public static readonly Currency XOF = new Currency(952,"XOF", "XOF (Franc CFA - BCEAO †)", "XOF");
        public static readonly Currency BMD = new Currency(60,"BMD", "BMD (Dollar des Bermudes)", "BMD");
        public static readonly Currency INR = new Currency(356,"INR", "INR (Roupie indienne)", "INR");
        public static readonly Currency BTN = new Currency(64,"BTN", "BTN (Ngultrum)", "BTN");
        public static readonly Currency BOB = new Currency(68,"BOB", "BOB (Boliviano)", "BOB");
        public static readonly Currency BOV = new Currency(984,"BOV", "BOV (Mvdol)", "BOV");
        public static readonly Currency BAM = new Currency(977,"BAM", "BAM (Mark convertible)", "BAM");
        public static readonly Currency BWP = new Currency(72,"BWP", "BWP (Pula)", "BWP");
        public static readonly Currency NOK = new Currency(578,"NOK", "NOK (Couronne norvégienne)", "NOK");
        public static readonly Currency BRL = new Currency(986,"BRL", "BRL (Real)", "BRL");
        public static readonly Currency BND = new Currency(96,"BND", "BND (Dollar de Brunei)", "BND");
        public static readonly Currency BGN = new Currency(975,"BGN", "BGN (Bulgarian Lev)", "BGN");
        public static readonly Currency BIF = new Currency(108,"BIF", "BIF (Franc du Burundi)", "BIF");
        public static readonly Currency KYD = new Currency(136,"KYD", "KYD (Dollar des Iles Caïmanes)", "KYD");
        public static readonly Currency KHR = new Currency(116,"KHR", "KHR (Riel)", "KHR");
        public static readonly Currency XAF = new Currency(950,"XAF", "XAF (Franc CFA - BEAC  ‡)", "XAF");
        public static readonly Currency CAD = new Currency(124,"CAD", "CAD (Dollar canadien)", "CAD");
        public static readonly Currency CVE = new Currency(132,"CVE", "CVE (Escudo du Cap-Vert)", "CVE");
        public static readonly Currency CLP = new Currency(152,"CLP", "CLP (Peso chilien)", "CLP");
        public static readonly Currency CLF = new Currency(990,"CLF", "CLF (Unité d'investissement)", "CLF");
        public static readonly Currency CNY = new Currency(156,"CNY", "CNY (Yuan Ren-Min-Bi)", "CNY");
        public static readonly Currency CYP = new Currency(196,"CYP", "CYP (Livre cypriote)", "CYP");
        public static readonly Currency COP = new Currency(170,"COP", "COP (Peso colombien)", "COP");
        public static readonly Currency COU = new Currency(970,"COU", "COU (Unidad de Valor Real)", "COU");
        public static readonly Currency KMF = new Currency(174,"KMF", "KMF (Franc des Comores)", "KMF");
        public static readonly Currency CDF = new Currency(976,"CDF", "CDF (Franc Congolais)", "CDF");
        public static readonly Currency NZD = new Currency(554,"NZD", "NZD (Dollar néo-zélandais)", "NZD");
        public static readonly Currency KRW = new Currency(410,"KRW", "KRW (Won)", "KRW");
        public static readonly Currency KPW = new Currency(408,"KPW", "KPW (Won de la Corée du Nord)", "KPW");
        public static readonly Currency CRC = new Currency(188,"CRC", "CRC (Colon de Costa Rica)", "CRC");
        public static readonly Currency HRK = new Currency(191,"HRK", "HRK (Kuna)", "HRK");
        public static readonly Currency CUP = new Currency(192,"CUP", "CUP (Peso cubain)", "CUP");
        public static readonly Currency DKK = new Currency(208,"DKK", "DKK (Couronne danoise)", "DKK");
        public static readonly Currency DJF = new Currency(262,"DJF", "DJF (Franc de Djibouti)", "DJF");
        public static readonly Currency DOP = new Currency(214,"DOP", "DOP (Peso dominicain)", "DOP");
        public static readonly Currency EGP = new Currency(818,"EGP", "EGP (Livre égyptienne)", "EGP");
        public static readonly Currency SVC = new Currency(222,"SVC", "SVC (Colon du El Salvador)", "SVC");
        /// <summary>
        /// Dollar
        /// </summary>
        public static readonly Currency USD = new Currency(840,"USD", "USD (Dollar des États-Unis)", "USD");
        public static readonly Currency AED = new Currency(784,"AED", "AED (Dirham des émirats arabes unis)", "AED");
        public static readonly Currency ERN = new Currency(232,"ERN", "ERN (Nakfa)", "ERN");
        public static readonly Currency EEK = new Currency(233,"EEK", "EEK (Couronne estonienne)", "EEK");
        public static readonly Currency USS = new Currency(998,"USS", "USS ((Même jour))", "USS");
        public static readonly Currency USN = new Currency(997,"USN", "USN ((Lendemain))", "USN");
        public static readonly Currency ETB = new Currency(230,"ETB", "ETB (Birr éthiopien)", "ETB");
        public static readonly Currency FKP = new Currency(238,"FKP", "FKP (Livre de Falkland)", "FKP");
        public static readonly Currency FJD = new Currency(242,"FJD", "FJD (Dollar de Fidji)", "FJD");
        public static readonly Currency XDR = new Currency(960,"XDR", "XDR (Droit de tirage spécial (D.T.S.))", "XDR");
        public static readonly Currency GMD = new Currency(270,"GMD", "GMD (Dalasi)", "GMD");
        public static readonly Currency GEL = new Currency(981,"GEL", "GEL (Lari)", "GEL");
        public static readonly Currency GHS = new Currency(936,"GHS", "GHS (Ghana Cedi)", "GHS");
        public static readonly Currency GIP = new Currency(292,"GIP", "GIP (Livre de Gibraltar)", "GIP");
        public static readonly Currency GTQ = new Currency(320,"GTQ", "GTQ (Quetzal)", "GTQ");
        public static readonly Currency GNF = new Currency(324,"GNF", "GNF (Franc guinéen)", "GNF");
        public static readonly Currency GWP = new Currency(624,"GWP", "GWP (Peso de Guinée-Bissau)", "GWP");
        public static readonly Currency GYD = new Currency(328,"GYD", "GYD (Dollar de Guyane)", "GYD");
        public static readonly Currency HTG = new Currency(332,"HTG", "HTG (Gourde)", "HTG");
        public static readonly Currency HNL = new Currency(340,"HNL", "HNL (Lempira)", "HNL");
        public static readonly Currency HKD = new Currency(344,"HKD", "HKD (Dollar de Hong-Kong)", "HKD");
        public static readonly Currency HUF = new Currency(348,"HUF", "HUF (Forint)", "HUF");
        public static readonly Currency IDR = new Currency(360,"IDR", "IDR (Rupiah)", "IDR");
        public static readonly Currency IRR = new Currency(364,"IRR", "IRR (Rial iranien)", "IRR");
        public static readonly Currency IQD = new Currency(368,"IQD", "IQD (Dinar iraquien)", "IQD");
        public static readonly Currency ISK = new Currency(352,"ISK", "ISK (Couronne islandaise)", "ISK");
        public static readonly Currency ILS = new Currency(376,"ILS", "ILS (Nouveau israëli sheqel)", "ILS");
        public static readonly Currency JMD = new Currency(388,"JMD", "JMD (Dollar jamaïcain)", "JMD");
        public static readonly Currency JPY = new Currency(392,"JPY", "JPY (Yen)", "JPY");
        public static readonly Currency JOD = new Currency(400,"JOD", "JOD (Dinar jordanien)", "JOD");
        public static readonly Currency KZT = new Currency(398,"KZT", "KZT (Tenge)", "KZT");
        public static readonly Currency KES = new Currency(404,"KES", "KES (Shilling du Kenya)", "KES");
        public static readonly Currency KGS = new Currency(417,"KGS", "KGS (Som)", "KGS");
        public static readonly Currency KWD = new Currency(414,"KWD", "KWD (Dinar koweïtien)", "KWD");
        public static readonly Currency LAK = new Currency(418,"LAK", "LAK (Kip)", "LAK");
        public static readonly Currency LSL = new Currency(426,"LSL", "LSL (Loti)", "LSL");
        public static readonly Currency LVL = new Currency(428,"LVL", "LVL (Lats letton)", "LVL");
        public static readonly Currency LBP = new Currency(422,"LBP", "LBP (Livre libanaise)", "LBP");
        public static readonly Currency LRD = new Currency(430,"LRD", "LRD (Dollar libérien)", "LRD");
        public static readonly Currency LYD = new Currency(434,"LYD", "LYD (Dinar libyen)", "LYD");
        public static readonly Currency CHF = new Currency(756,"CHF", "CHF (Franc suisse)", "CHF");
        public static readonly Currency LTL = new Currency(440,"LTL", "LTL (Litas lituanien)", "LTL");
        public static readonly Currency MOP = new Currency(446,"MOP", "MOP (Pataca)", "MOP");
        public static readonly Currency MKD = new Currency(807,"MKD", "MKD (Denar)", "MKD");
        public static readonly Currency MGA = new Currency(969,"MGA", "MGA (Malagasy Ariary)", "MGA");
        public static readonly Currency MYR = new Currency(458,"MYR", "MYR (Ringgit de Malaisie)", "MYR");
        public static readonly Currency MWK = new Currency(454,"MWK", "MWK (Kwacha)", "MWK");
        public static readonly Currency MVR = new Currency(462,"MVR", "MVR (Rufiyaa)", "MVR");
        public static readonly Currency MTL = new Currency(470,"MTL", "MTL (Livre maltaise)", "MTL");
        public static readonly Currency MAD = new Currency(504,"MAD", "MAD (Dirham marocain)", "MAD");
        public static readonly Currency MUR = new Currency(480,"MUR", "MUR (Roupie de Maurice)", "MUR");
        public static readonly Currency MRO = new Currency(478,"MRO", "MRO (Ouguija)", "MRO");
        public static readonly Currency MXN = new Currency(484,"MXN", "MXN (Peso mexicain)", "MXN");
        public static readonly Currency MXV = new Currency(979,"MXV", "MXV (Mexican Unidad de Inversion)", "MXV");
        public static readonly Currency MDL = new Currency(498,"MDL", "MDL (Leu de Moldovie)", "MDL");
        public static readonly Currency MNT = new Currency(496,"MNT", "MNT (Tugrik)", "MNT");
        public static readonly Currency MZN = new Currency(943,"MZN", "MZN (Metical)", "MZN");
        public static readonly Currency MMK = new Currency(104,"MMK", "MMK (Kyat)", "MMK");
        public static readonly Currency NAD = new Currency(516,"NAD", "NAD (Dollar namibien)", "NAD");
        public static readonly Currency NPR = new Currency(524,"NPR", "NPR (Roupie du Népal)", "NPR");
        public static readonly Currency NIO = new Currency(558,"NIO", "NIO (Cordoba Oro)", "NIO");
        public static readonly Currency NGN = new Currency(566,"NGN", "NGN (Naira)", "NGN");
        public static readonly Currency XPF = new Currency(953,"XPF", "XPF (Franc CFP)", "XPF");
        public static readonly Currency OMR = new Currency(512,"OMR", "OMR (Rial Omani)", "OMR");
        public static readonly Currency UGX = new Currency(800,"UGX", "UGX (Shilling ougandais)", "UGX");
        public static readonly Currency UZS = new Currency(860,"UZS", "UZS (Soum d'Ouzbékistan)", "UZS");
        public static readonly Currency PKR = new Currency(586,"PKR", "PKR (Roupie du Pakistan)", "PKR");
        public static readonly Currency PAB = new Currency(590,"PAB", "PAB (Balboa)", "PAB");
        public static readonly Currency PGK = new Currency(598,"PGK", "PGK (Kina)", "PGK");
        public static readonly Currency PYG = new Currency(600,"PYG", "PYG (Guarani)", "PYG");
        public static readonly Currency PEN = new Currency(604,"PEN", "PEN (Nouveau Sol)", "PEN");
        public static readonly Currency PHP = new Currency(608,"PHP", "PHP (Peso philippin)", "PHP");
        public static readonly Currency PLN = new Currency(985,"PLN", "PLN (Zloty)", "PLN");
        public static readonly Currency QAR = new Currency(634,"QAR", "QAR (Riyal du Qatar)", "QAR");
        public static readonly Currency RON = new Currency(946,"RON", "RON (Les nouveaux noms seront communiqués ultérieurement)", "RON");
        public static readonly Currency GBP = new Currency(826,"GBP", "GBP (Livre sterling)", "GBP");
        public static readonly Currency RUB = new Currency(643,"RUB", "RUB (Rouble russe)", "RUB");
        public static readonly Currency RWF = new Currency(646,"RWF", "RWF (Franc du Rwanda)", "RWF");
        public static readonly Currency SHP = new Currency(654,"SHP", "SHP (Livre de Sainte-Hélène)", "SHP");
        public static readonly Currency SBD = new Currency(90,"SBD", "SBD (Dollar de Salomon)", "SBD");
        public static readonly Currency WST = new Currency(882,"WST", "WST (Tala)", "WST");
        public static readonly Currency STD = new Currency(678,"STD", "STD (Dobra)", "STD");
        public static readonly Currency RSD = new Currency(941,"RSD", "RSD (Dinar serbe)", "RSD");
        public static readonly Currency SCR = new Currency(690,"SCR", "SCR (Roupie des Seychelles)", "SCR");
        public static readonly Currency SLL = new Currency(694,"SLL", "SLL (Leone)", "SLL");
        public static readonly Currency SGD = new Currency(702,"SGD", "SGD (Dollar de Singapour)", "SGD");
        public static readonly Currency SKK = new Currency(703,"SKK", "SKK (Couronne slovaque)", "SKK");
        public static readonly Currency SOS = new Currency(706,"SOS", "SOS (Shilling de Somalie)", "SOS");
        public static readonly Currency SDG = new Currency(938,"SDG", "SDG (Livre soudanaise)", "SDG");
        public static readonly Currency LKR = new Currency(144,"LKR", "LKR (Roupie de Sri Lanka)", "LKR");
        public static readonly Currency SEK = new Currency(752,"SEK", "SEK (Couronne suédoise)", "SEK");
        public static readonly Currency SRD = new Currency(968,"SRD", "SRD (Dollar de Surinam)", "SRD");
        public static readonly Currency CHW = new Currency(948,"CHW", "CHW (WIR Franc)", "CHW");
        public static readonly Currency CHE = new Currency(947,"CHE", "CHE (WIR Euro)", "CHE");
        public static readonly Currency SZL = new Currency(748,"SZL", "SZL (Lilangeni)", "SZL");
        public static readonly Currency SYP = new Currency(760,"SYP", "SYP (Livre syrienne)", "SYP");
        public static readonly Currency TJS = new Currency(972,"TJS", "TJS (Somoni)", "TJS");
        public static readonly Currency TWD = new Currency(901,"TWD", "TWD (Nouveau dollar de Taïwan)", "TWD");
        public static readonly Currency TZS = new Currency(834,"TZS", "TZS (Shilling de Tanzanie)", "TZS");
        public static readonly Currency CZK = new Currency(203,"CZK", "CZK (Couronne tchèque)", "CZK");
        public static readonly Currency THB = new Currency(764,"THB", "THB (Baht)", "THB");
        public static readonly Currency TOP = new Currency(776,"TOP", "TOP (Pa'anga)", "TOP");
        public static readonly Currency TTD = new Currency(780,"TTD", "TTD (Dollar de la Trinité et de Tobago)", "TTD");
        public static readonly Currency TND = new Currency(788,"TND", "TND (Dinar tunisien)", "TND");
        public static readonly Currency TMM = new Currency(795,"TMM", "TMM (Manat)", "TMM");
        public static readonly Currency TRY = new Currency(949,"TRY", "TRY (Le nouveau nom sera communiqué ultérieurement)", "TRY");
        public static readonly Currency UAH = new Currency(980,"UAH", "UAH (Hryvnia)", "UAH");
        public static readonly Currency UYU = new Currency(858,"UYU", "UYU (Peso uruguayen)", "UYU");
        public static readonly Currency UYI = new Currency(940,"UYI", "UYI (Uruguay Peso en Unidades Indexadas)", "UYI");
        public static readonly Currency VUV = new Currency(548,"VUV", "VUV (Vatu)", "VUV");
        public static readonly Currency VEF = new Currency(937,"VEF", "VEF (Bolivar Fuerte)", "VEF");
        public static readonly Currency VND = new Currency(704,"VND", "VND (Dong)", "VND");
        public static readonly Currency YER = new Currency(886,"YER", "YER (Riyal du Yémen)", "YER");
        public static readonly Currency Kwacha = new Currency(894,"Kwacha", "Kwacha (Kwacha)", "Kwacha");
        public static readonly Currency ZWD = new Currency(716,"ZWD", "ZWD (Dollar du Zimbabwe)", "ZWD");
        public static readonly Currency YTL = new Currency(10000,"YTL", "YTL (Yeni lirasi)","YTL");

        public Currency(int id, string name, string fr, string en)
            : base(id, name, fr, en)
        {
        }

        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static Currency Default
        {
            get
            {
                return Currency.EUR;
            }
        }

        public static ReadOnlyCollection<Currency> GetValues()
        {
            return GetBaseValues();
        }

        public static Currency GetByKey(int id)
        {
            return GetBaseByKey(id);
        }

    }

}
