using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;

namespace ERPStore.Models
{
    /// <summary>
    /// Liste des pays selon la norme ISO 3166
    /// http://fr.wikipedia.org/wiki/ISO_3166-1
    /// 
    /// </summary>
	[Serializable]
    public class Country : EnumBaseType<Country>
    {
		/// <summary>
		/// France
		/// </summary>
		public static readonly Country FRA = new Country(250, "FRA", "FR", "France", string.Empty, Currency.EUR, "33", "fr", "00 00 00 00 00");
        /// <summary>
        /// Afghanistan
        /// </summary>
        public static readonly Country AFG = new Country(4, "AFG", "AF", "Afghanistan", "", Currency.AFN, "93","fa");
        /// <summary>
        /// Afrikaans
        /// </summary>
        public static readonly Country ZAF = new Country(710, "ZAF","ZA", "Afrique du sud", "Afrikaans", Currency.ZAR, "27","af");
        /// <summary>
        /// Albanais
        /// </summary>
        public static readonly Country ALB = new Country(8, "ALB", "AL", "Albanie", "Albanian", Currency.ALL, "355","sq");
        /// <summary>
        /// Algérie
        /// </summary>
        public static readonly Country DZA = new Country(12, "DZA", "DZ", "Algérie", "", Currency.DZD, "213","ar");
        /// <summary>
        /// Allemand
        /// </summary>
        public static readonly Country DEU = new Country(276, "DEU","DE", "Allemagne", "German", Currency.EUR, "49","de");
        /// <summary>
        /// Andorre
        /// </summary>
        public static readonly Country AND = new Country(20, "AND", "AD", "Andorre", "", Currency.EUR, "376","es");
        /// <summary>
        /// Angola
        /// </summary>
        public static readonly Country AGO = new Country(24, "AGO", "AO", "Angola", "", Currency.AOA, "244","pt");
        /// <summary>
        /// Arabe
        /// </summary>
        public static readonly Country SAU = new Country(682,"SAU","SA", "Arabie saoudite","Arabic", Currency.SAR, "966","ar");
        /// <summary>
        /// Argentine
        /// </summary>
        public static readonly Country ARG = new Country(32, "ARG", "AR", "Argentine", "", Currency.ARS, "54","es");
        /// <summary>
        /// Arménie
        /// </summary>
        public static readonly Country ARM = new Country(51, "ARM", "AM", "Arménie", "", Currency.AMD, "374","hy");
        /// <summary>
        /// Australie
        /// </summary>
        public static readonly Country AUS = new Country(36, "AUS", "AU", "Australie", "", Currency.EUR, "61","en");
        /// <summary>
        /// Autriche
        /// </summary>
        public static readonly Country AUT = new Country(40, "AUT", "AT", "Autriche", "", Currency.EUR, "43","de");
        /// <summary>
        /// Azerbaïdjan
        /// </summary>
        public static readonly Country AZE = new Country(31, "AZE", "AZ", "Azerbaïdjan", "", Currency.AZN, "894","az");
        /// <summary>
        /// Bahamas
        /// </summary>
        public static readonly Country BHS = new Country(44, "BHS", "BS", "Bahamas", "", Currency.BSD, "1","en");
        /// <summary>
        /// Bahreïn
        /// </summary>
        public static readonly Country BHR = new Country(48, "BHR", "BH", "Bahreïn", "", Currency.BHD, "973","en");
        /// <summary>
        /// Bangladesh
        /// </summary>
        public static readonly Country BGD = new Country(50, "BGD", "BD", "Bangladesh", "", Currency.BDT, "880","en");
        /// <summary>
        /// Barbade
        /// </summary>
        public static readonly Country BRB = new Country(52, "BRB", "BB", "Barbade", "", Currency.BBD, "","en");
        /// <summary>
        /// Biélorusse
        /// </summary>
        public static readonly Country BLR = new Country(112, "BLR","BY", "Biélorussie", "Belarusian", Currency.BYR, "375","be");
        /// <summary>
        /// Belgique
        /// </summary>
        public static readonly Country BEL = new Country(56, "BEL", "BE", "Belgique", "", Currency.EUR, "32","fr","+32 00 000 00 00");
        /// <summary>
        /// Belize
        /// </summary>
        public static readonly Country BLZ = new Country(84, "BLZ", "BZ", "Belize", "", Currency.BZD, "501","en");
        /// <summary>
        /// Bénin
        /// </summary>
        public static readonly Country BEN = new Country(204, "BEN", "BJ", "Bénin", "", Currency.XAF, "229","en");
        /// <summary>
        /// Bermudes
        /// </summary>
        public static readonly Country BMU = new Country(60, "BMU", "BM", "Bermudes", "", Currency.USD, "","en");
        /// <summary>
        /// Bhoutan
        /// </summary>
        public static readonly Country BTN = new Country(64, "BTN", "BT", "Bhoutan", "", Currency.BTN, "975","en");
        /// <summary>
        /// Bolivie
        /// </summary>
        public static readonly Country BOL = new Country(68, "BOL", "BO", "Bolivie", "", Currency.BOB, "591","es");
        /// <summary>
        /// Bosnie-Herzégovine
        /// </summary>
        public static readonly Country BIH = new Country(70, "BIH", "BA", "Bosnie-Herzégovine", "", Currency.BAM, "387","sr");
        /// <summary>
        /// Botswana
        /// </summary>
        public static readonly Country BWA = new Country(72, "BWA", "BV", "Botswana", "", Currency.BWP, "267","en");
        /// <summary>
        /// Brézil
        /// </summary>
        public static readonly Country BRA = new Country(76, "BRA", "BR", "Brézil", "", Currency.BRL, "55","pt");
        /// <summary>
        /// Brunei
        /// </summary>
        public static readonly Country BRN = new Country(96, "BRN", "BN", "Brunei", "", Currency.BND, "273","ar");
        /// <summary>
        /// Bulgarie
        /// </summary>
        public static readonly Country BGR = new Country(100, "BGR", "BG", "Bulgarie", "Bulgarian", Currency.BGN, "359","bg");

        /// <summary>
        /// Burkina Faso
        /// </summary>
        public static readonly Country BFA = new Country(854, "BFA", "BF","Burkina Faso",string.Empty, Currency.XOF, "226","en");
        /// <summary>
        /// Burundi
        /// </summary>
        public static readonly Country BDI = new Country(108, "BDI", "BI","Burundi",string.Empty, Currency.BIF, "257","en");
        /// <summary>
        /// Îles Caïmanes
        /// </summary>
        public static readonly Country CYM = new Country(136, "CYM", "KY","Îles Caïmanes",string.Empty, Currency.KYD, "236","en");
        /// <summary>
        /// Cambodge
        /// </summary>
        public static readonly Country KHM = new Country(116, "KHM", "KH","Cambodge",string.Empty, Currency.KHR, "855","en");
        /// <summary>
        /// Cameroun
        /// </summary>
        public static readonly Country CMR = new Country(120, "CMR", "CM","Cameroun",string.Empty, Currency.XAF, "237","en");
        /// <summary>
        /// Canada
        /// </summary>
        public static readonly Country CAN = new Country(124, "CAN", "CA","Canada",string.Empty, Currency.CAD, "","en");
        /// <summary>
        /// Cap-Vert
        /// </summary>
        public static readonly Country CPV = new Country(132, "CPV", "CV","Cap-Vert",string.Empty, Currency.CVE, "238","en");
        /// <summary>
        /// Centrafrique
        /// </summary>
        public static readonly Country CAF = new Country(140, "CAF", "CF","Centrafrique",string.Empty, Currency.XAF, "236","fr");
        /// <summary>
        /// Chili
        /// </summary>
        public static readonly Country CHL = new Country(152, "CHL", "CL","Chili",string.Empty, Currency.CLP, "56","es");
        /// <summary>
        /// Chine
        /// </summary>
        public static readonly Country CHN = new Country(156, "CHN", "CN","Chine",string.Empty, Currency.CNY, "86","cn");
        /// <summary>
        /// Ile Christmas
        /// </summary>
        public static readonly Country CXR = new Country(162, "CXR", "CX","Ile Christmas",string.Empty, Currency.AUD, "","en");
        /// <summary>
        /// Chypre
        /// </summary>
        public static readonly Country CYP = new Country(196, "CYP", "CY","Chypre",string.Empty, Currency.CYP, "357","en");
        /// <summary>
        /// Iles Cocos
        /// </summary>
        public static readonly Country CCK = new Country(166, "CCK", "CC","Iles Cocos",string.Empty, Currency.AUD, string.Empty,"en");
        /// <summary>
        /// Colombie
        /// </summary>
        public static readonly Country COL = new Country(170, "COL", "CO","Colombie",string.Empty, Currency.COP, "57","es");
        /// <summary>
        /// Comores
        /// </summary>
        public static readonly Country COM = new Country(174, "COM", "KM","Comores",string.Empty, Currency.KMF, "269","fr");
        /// <summary>
        /// Congo
        /// </summary>
        public static readonly Country COG = new Country(178, "COG", "CG","Congo",string.Empty, Currency.XAF, string.Empty,"fr");
        /// <summary>
        /// République démocratique du Congo
        /// </summary>
        public static readonly Country COD = new Country(180, "COD", "CD","République démocratique du Congo",string.Empty, Currency.CDF, "243","en");
        /// <summary>
        /// Iles Cook
        /// </summary>
        public static readonly Country COK = new Country(184, "COK", "CK","Iles Cook",string.Empty, Currency.NZD, string.Empty,"en");
        /// <summary>
        /// Coree du Sud
        /// </summary>
        public static readonly Country KOR = new Country(410, "KOR", "KR","Coree du Sud",string.Empty, Currency.KRW, "82","ko");
        /// <summary>
        /// Coree du Nord
        /// </summary>
        public static readonly Country PRK = new Country(408, "PRK", "KP","Coree du Nord",string.Empty, Currency.KPW, "850","ko");
        /// <summary>
        /// Costa Rica
        /// </summary>
        public static readonly Country CRI = new Country(188, "CRI", "CR","Costa Rica",string.Empty, Currency.CRC, "82","en");
        /// <summary>
        /// Cote d'Ivoire
        /// </summary>
        public static readonly Country CIV = new Country(384, "CIV", "CI","Cote d'Ivoire",string.Empty, Currency.XOF, "225","fr");
        /// <summary>
        /// Croatie
        /// </summary>
        public static readonly Country HRV = new Country(191, "HRV", "HR","Croatie",string.Empty, Currency.HRK, "385","hr");
        /// <summary>
        /// Cuba
        /// </summary>
        public static readonly Country CUB = new Country(192, "CUB", "CU","Cuba",string.Empty, Currency.CUP, "63","es");
        /// <summary>
        /// Danemark
        /// </summary>
        public static readonly Country DNK = new Country(208, "DNK", "DK","Danemark",string.Empty, Currency.DKK, "45","da");
        /// <summary>
        /// Djibouti
        /// </summary>
        public static readonly Country DJI = new Country(262, "DJI", "DJ","Djibouti",string.Empty, Currency.DJF, "253","fr");
        /// <summary>
        /// Republique dominicaine
        /// </summary>
        public static readonly Country DOM = new Country(214, "DOM", "DO","Republique dominicaine",string.Empty, Currency.DOP, "","fr");
        /// <summary>
        /// Dominique
        /// </summary>
        public static readonly Country DMA = new Country(212, "DMA", "DM","Dominique",string.Empty, Currency.XCD, "","en");
        /// <summary>
        /// Egypte
        /// </summary>
        public static readonly Country EGY = new Country(818, "EGY", "EG","Egypte",string.Empty, Currency.EGP, "20","ar");
        /// <summary>
        /// Salvador
        /// </summary>
        public static readonly Country SLV = new Country(222, "SLV", "SV","Salvador",string.Empty, Currency.SVC, "503","en");
        /// <summary>
        /// Emirats arabes unis
        /// </summary>
        public static readonly Country ARE = new Country(784, "ARE", "AE","Emirats arabes unis",string.Empty, Currency.AED, "971","ar");
        /// <summary>
        /// Equateur
        /// </summary>
        public static readonly Country ECU = new Country(218, "ECU", "EC","Equateur",string.Empty, Currency.USD, "593","en");
        /// <summary>
        /// Erythree
        /// </summary>
        public static readonly Country ERI = new Country(232, "ERI", "ER","Erythree",string.Empty, Currency.ERN, "291","en");
        /// <summary>
        /// Espagne
        /// </summary>
        public static readonly Country ESP = new Country(724, "ESP", "ES","Espagne",string.Empty, Currency.EUR, "34","es");
        /// <summary>
        /// Estonie
        /// </summary>
        public static readonly Country EST = new Country(233, "EST", "EE","Estonie",string.Empty, Currency.EEK, "372","et");
        /// <summary>
        /// Etats-Unis
        /// </summary>
        public static readonly Country USA = new Country(840, "USA", "US","Etats-Unis",string.Empty, Currency.USD, "","en");
        /// <summary>
        /// Ethiopie
        /// </summary>
        public static readonly Country ETH = new Country(231, "ETH", "ET","Ethiopie",string.Empty, Currency.ETB, "251","en");
        /// <summary>
        /// Iles Malouines
        /// </summary>
        public static readonly Country FLK = new Country(238, "FLK", "FK","Iles Malouines",string.Empty, Currency.FKP, "","en");
        /// <summary>
        /// Îles Féroé
        /// </summary>
        public static readonly Country FRO = new Country(234, "FRO", "FO","Îles Féroé",string.Empty, Currency.DKK, "298","fo");
        /// <summary>
        /// Fidji
        /// </summary>
        public static readonly Country FJI = new Country(242, "FJI", "FJ","Fidji",string.Empty, Currency.FJD, "679","en");
        /// <summary>
        /// Finlande
        /// </summary>
        public static readonly Country FIN = new Country(246, "FIN", "FI","Finlande",string.Empty, Currency.EUR, "358","fi");
        /// <summary>
        /// Gabon
        /// </summary>
        public static readonly Country GAB = new Country(266, "GAB", "GA","Gabon",string.Empty, Currency.XAF, "241","fr");
        /// <summary>
        /// Gambie
        /// </summary>
        public static readonly Country GMB = new Country(270, "GMB", "GM","Gambie",string.Empty, Currency.GMD, "220","en");
        /// <summary>
        /// Georgie
        /// </summary>
        public static readonly Country GEO = new Country(268, "GEO", "GE","Georgie",string.Empty, Currency.GEL, "995","ka");
        /// <summary>
        /// Ghana
        /// </summary>
        public static readonly Country GHA = new Country(288, "GHA", "GH","Ghana",string.Empty, Currency.GHS, "233","en");
        /// <summary>
        /// Gibraltar
        /// </summary>
        public static readonly Country GIB = new Country(292, "GIB", "GI","Gibraltar",string.Empty, Currency.GIP, "350","en");
        /// <summary>
        /// Grece
        /// </summary>
        public static readonly Country GRC = new Country(300, "GRC", "GR","Grece",string.Empty, Currency.EUR, "30","el");
        /// <summary>
        /// Grenade
        /// </summary>
        public static readonly Country GRD = new Country(308, "GRD", "GD","Grenade",string.Empty, Currency.XCD, string.Empty,"en");
        /// <summary>
        /// Groenland
        /// </summary>
        public static readonly Country GRL = new Country(304, "GRL", "GL","Groenland",string.Empty, Currency.DKK, "299","de");
        /// <summary>
        /// Guadeloupe
        /// </summary>
        public static readonly Country GLP = new Country(312, "GLP", "GP","Guadeloupe",string.Empty, Currency.EUR, "590","fr");
        /// <summary>
        /// Guam
        /// </summary>
        public static readonly Country GUM = new Country(316, "GUM", "GU","Guam",string.Empty, Currency.USD, "","en");
        /// <summary>
        /// Guatemala
        /// </summary>
        public static readonly Country GTM = new Country(320, "GTM", "GT","Guatemala",string.Empty, Currency.USD, "502","es");
        /// <summary>
        /// Guernesey
        /// </summary>
        public static readonly Country GGY = new Country(831, "GGY", "GG","Guernesey",string.Empty, Currency.EUR, "224","en");
        /// <summary>
        /// Guinée
        /// </summary>
        public static readonly Country GIN = new Country(324, "GIN", "GN","Guinée",string.Empty, Currency.GNF, "240","fr");
        /// <summary>
        /// Guinée-Bissau
        /// </summary>
        public static readonly Country GNB = new Country(624, "GNB", "GW","Guinée-Bissau",string.Empty, Currency.XAF, "245","pt");
        /// <summary>
        /// Guinée équatoriale
        /// </summary>
        public static readonly Country GNQ = new Country(226, "GNQ", "GQ","Guinée équatoriale",string.Empty, Currency.XAF, "592","es");
        /// <summary>
        /// Guyana
        /// </summary>
        public static readonly Country GUY = new Country(328, "GUY", "GY","Guyana",string.Empty, Currency.GYD, "592","en");
        /// <summary>
        /// Guyane
        /// </summary>
        public static readonly Country GUF = new Country(254, "GUF", "GF","Guyane",string.Empty, Currency.EUR, "594","fr");
        /// <summary>
        /// Haïti
        /// </summary>
        public static readonly Country HTI = new Country(332, "HTI", "HT","Haïti",string.Empty, Currency.HTG, "509","fr");
        /// <summary>
        /// Honduras
        /// </summary>
        public static readonly Country HND = new Country(340, "HND", "HN","Honduras",string.Empty, Currency.HNL, "504","es");
        /// <summary>
        /// Hong Kong
        /// </summary>
        public static readonly Country HKG = new Country(344, "HKG", "HK","Hong Kong",string.Empty, Currency.HKD, "852","en");
        /// <summary>
        /// Hongrie
        /// </summary>
        public static readonly Country HUN = new Country(348, "HUN", "HU","Hongrie",string.Empty, Currency.HUF, "36","hu");
        /// <summary>
        /// Île de Man
        /// </summary>
        public static readonly Country IMN = new Country(833, "IMN", "IM","Île de Man",string.Empty, Currency.USD, "","en");
        /// <summary>
        /// Îles Vierges britanniques
        /// </summary>
        public static readonly Country VGB = new Country(92, "VGB", "VG","Îles Vierges britanniques",string.Empty, Currency.USD, string.Empty,"en");
        /// <summary>
        /// Îles Vierges américaines
        /// </summary>
        public static readonly Country VIR = new Country(850, "VIR", "VI","Îles Vierges américaines",string.Empty, Currency.USD, string.Empty,"en");
        /// <summary>
        /// Inde
        /// </summary>
        public static readonly Country IND = new Country(356, "IND", "IN","Inde",string.Empty, Currency.INR, "91","hi");
        /// <summary>
        /// Indonésie
        /// </summary>
        public static readonly Country IDN = new Country(360, "IDN", "ID","Indonésie",string.Empty, Currency.IDR, "62","id");
        /// <summary>
        /// Iran
        /// </summary>
        public static readonly Country IRN = new Country(364, "IRN", "IR","Iran",string.Empty, Currency.IRR, "98","fa");
        /// <summary>
        /// Irak
        /// </summary>
        public static readonly Country IRQ = new Country(368, "IRQ", "IQ","Irak",string.Empty, Currency.IQD, "964","ar");
        /// <summary>
        /// Irlande
        /// </summary>
        public static readonly Country IRL = new Country(372, "IRL", "IE","Irlande",string.Empty, Currency.EUR, "353","en");
        /// <summary>
        /// Islande
        /// </summary>
        public static readonly Country ISL = new Country(352, "ISL", "IS","Islande",string.Empty, Currency.ISK, "354","is");
        /// <summary>
        /// Israël
        /// </summary>
        public static readonly Country ISR = new Country(376, "ISR", "IL","Israël",string.Empty, Currency.ILS, "972","he");
        /// <summary>
        /// Italie
        /// </summary>
        public static readonly Country ITA = new Country(380, "ITA", "IT","Italie",string.Empty, Currency.EUR, "39","it");
        /// <summary>
        /// Jamaïque
        /// </summary>
        public static readonly Country JAM = new Country(388, "JAM", "JM","Jamaïque",string.Empty, Currency.JMD, string.Empty,"en");
        /// <summary>
        /// Japon
        /// </summary>
        public static readonly Country JPN = new Country(392, "JPN", "JP","Japon",string.Empty, Currency.JPY, "81","ja");
        /// <summary>
        /// Jersey
        /// </summary>
        public static readonly Country JEY = new Country(832, "JEY", "JE","Jersey",string.Empty, Currency.GBP, string.Empty,"en");
        /// <summary>
        /// Jordanie
        /// </summary>
        public static readonly Country JOR = new Country(400, "JOR", "JO","Jordanie",string.Empty, Currency.JOD, "962","ar");
        /// <summary>
        /// Kazakhstan
        /// </summary>
        public static readonly Country KAZ = new Country(398, "KAZ", "KZ","Kazakhstan",string.Empty, Currency.KZT, "7","kk");
        /// <summary>
        /// Kenya
        /// </summary>
        public static readonly Country KEN = new Country(404, "KEN", "KE","Kenya",string.Empty, Currency.KES, "254","en");
        /// <summary>
        /// Kirghizistan
        /// </summary>
        public static readonly Country KGZ = new Country(417, "KGZ", "KG","Kirghizistan",string.Empty, Currency.KGS, "996","ky");
        /// <summary>
        /// Kiribati
        /// </summary>
        public static readonly Country KIR = new Country(296, "KIR", "KI","Kiribati",string.Empty, Currency.AUD, "686","en");
        /// <summary>
        /// Koweït
        /// </summary>
        public static readonly Country KWT = new Country(414, "KWT", "KW","Koweït",string.Empty, Currency.KWD, "965","ar");
        /// <summary>
        /// Laos
        /// </summary>
        public static readonly Country LAO = new Country(418, "LAO", "LA","Laos",string.Empty, Currency.LAK, "856","en");
        /// <summary>
        /// Lesotho
        /// </summary>
        public static readonly Country LSO = new Country(426, "LSO", "LS","Lesotho",string.Empty, Currency.LSL, "266","en");
        /// <summary>
        /// Lettonie
        /// </summary>
        public static readonly Country LVA = new Country(428, "LVA", "LV","Lettonie",string.Empty, Currency.LVL, "371","lt");
        /// <summary>
        /// Liban
        /// </summary>
        public static readonly Country LBN = new Country(422, "LBN", "LB","Liban",string.Empty, Currency.USD, "391","ar");
        /// <summary>
        /// Libéria
        /// </summary>
        public static readonly Country LBR = new Country(430, "LBR", "LR","Libéria",string.Empty, Currency.LRD, "231","en");
        /// <summary>
        /// Libye
        /// </summary>
        public static readonly Country LBY = new Country(434, "LBY", "LY","Libye",string.Empty, Currency.LYD, "218","ar");
        /// <summary>
        /// Liechtenstein
        /// </summary>
        public static readonly Country LIE = new Country(438, "LIE", "LI","Liechtenstein",string.Empty, Currency.CHF, "423","de");
        /// <summary>
        /// Lituanie
        /// </summary>
        public static readonly Country LTU = new Country(440, "LTU", "LT","Lituanie",string.Empty, Currency.LTL, "370","en");
        /// <summary>
        /// Luxembourg
        /// </summary>
        public static readonly Country LUX = new Country(442, "LUX", "LU","Luxembourg",string.Empty, Currency.EUR, "352","fr");
        /// <summary>
        /// Macao
        /// </summary>
        public static readonly Country MAC = new Country(446, "MAC", "MO","Macao",string.Empty, Currency.MOP, "853","en");
        /// <summary>
        /// Macédoine
        /// </summary>
        public static readonly Country MKD = new Country(807, "MKD", "MK","Macédoine",string.Empty, Currency.MKD, "389","mk");
        /// <summary>
        /// Madagascar
        /// </summary>
        public static readonly Country MDG = new Country(450, "MDG", "MG","Madagascar",string.Empty, Currency.MGA, "261","fr");
        /// <summary>
        /// Malaisie
        /// </summary>
        public static readonly Country MYS = new Country(458, "MYS", "MY","Malaisie",string.Empty, Currency.MYR, "60","ms");
        /// <summary>
        /// Malawi
        /// </summary>
        public static readonly Country MWI = new Country(454, "MWI", "MW","Malawi",string.Empty, Currency.MWK, "265","en");
        /// <summary>
        /// Maldives
        /// </summary>
        public static readonly Country MDV = new Country(462, "MDV", "MV","Maldives",string.Empty, Currency.MVR, "960","en");
        /// <summary>
        /// Mali
        /// </summary>
        public static readonly Country MLI = new Country(466, "MLI", "ML","Mali",string.Empty, Currency.XOF, "223","fr");
        /// <summary>
        /// Malte
        /// </summary>
        public static readonly Country MLT = new Country(470, "MLT", "MT","Malte",string.Empty, Currency.MTL, "500","en");
        /// <summary>
        /// Maroc
        /// </summary>
        public static readonly Country MAR = new Country(504, "MAR", "MA","Maroc",string.Empty, Currency.MAD, "212","ar");
        /// <summary>
        /// Marshall
        /// </summary>
        public static readonly Country MHL = new Country(584, "MHL", "MH","Marshall",string.Empty, Currency.USD, "692","en");
        /// <summary>
        /// Martinique
        /// </summary>
        public static readonly Country MTQ = new Country(474, "MTQ", "MQ","Martinique",string.Empty, Currency.EUR, "596","fr");
        /// <summary>
        /// Maurice
        /// </summary>
        public static readonly Country MUS = new Country(480, "MUS", "MU","Maurice",string.Empty, Currency.MUR, "230","en");
        /// <summary>
        /// Mauritanie
        /// </summary>
        public static readonly Country MRT = new Country(478, "MRT", "MR","Mauritanie",string.Empty, Currency.MRO, "222","en");
        /// <summary>
        /// Mayotte
        /// </summary>
        public static readonly Country MYT = new Country(175, "MYT", "YT","Mayotte",string.Empty, Currency.EUR, "262","fr");
        /// <summary>
        /// Mexique
        /// </summary>
        public static readonly Country MEX = new Country(484, "MEX", "MX","Mexique",string.Empty, Currency.MXN, "52","es");
        /// <summary>
        /// Micronésie
        /// </summary>
        public static readonly Country FSM = new Country(583, "FSM", "FM","Micronésie",string.Empty, Currency.USD, "691","en");
        /// <summary>
        /// Moldavie
        /// </summary>
        public static readonly Country MDA = new Country(498, "MDA", "MD","Moldavie",string.Empty, Currency.MDL, "373","ro");
        /// <summary>
        /// Monaco
        /// </summary>
        public static readonly Country MCO = new Country(492, "MCO", "MC","Monaco",string.Empty, Currency.EUR, "377","fr");
        /// <summary>
        /// Mongolie
        /// </summary>
        public static readonly Country MNG = new Country(496, "MNG", "MN","Mongolie",string.Empty, Currency.MNT, "976","mn");
        /// <summary>
        /// Monténégro
        /// </summary>
        public static readonly Country MNE = new Country(499, "MNE", "ME","Monténégro",string.Empty, Currency.EUR, "382","en");
        /// <summary>
        /// Montserrat
        /// </summary>
        public static readonly Country MSR = new Country(500, "MSR", "MS","Montserrat",string.Empty, Currency.USD, "","en");
        /// <summary>
        /// Mozambique
        /// </summary>
        public static readonly Country MOZ = new Country(508, "MOZ", "MZ","Mozambique",string.Empty, Currency.MZN, "258","en");
        /// <summary>
        /// Birmanie
        /// </summary>
        public static readonly Country MMR = new Country(104, "MMR", "MM","Birmanie",string.Empty, Currency.MMK, "","en");
        /// <summary>
        /// Namibie
        /// </summary>
        public static readonly Country NAM = new Country(516, "NAM", "NA","Namibie",string.Empty, Currency.NAD, "264","en");
        /// <summary>
        /// Nauru
        /// </summary>
        public static readonly Country NRU = new Country(520, "NRU", "NR","Nauru",string.Empty, Currency.AUD, "674","en");
        /// <summary>
        /// Népal
        /// </summary>
        public static readonly Country NPL = new Country(524, "NPL", "NP","Népal",string.Empty, Currency.NPR, "977","en");
        /// <summary>
        /// Nicaragua
        /// </summary>
        public static readonly Country NIC = new Country(558, "NIC", "NI","Nicaragua",string.Empty, Currency.NIO, "505","en");
        /// <summary>
        /// Niger
        /// </summary>
        public static readonly Country NER = new Country(562, "NER", "NE","Niger",string.Empty, Currency.XOF, "227","fr");
        /// <summary>
        /// Nigeria
        /// </summary>
        public static readonly Country NGA = new Country(566, "NGA", "NG","Nigeria",string.Empty, Currency.NGN, "234","en");
        /// <summary>
        /// Niué
        /// </summary>
        public static readonly Country NIU = new Country(570, "NIU", "NU","Niué",string.Empty, Currency.EUR, "683","en");
        /// <summary>
        /// Norfolk
        /// </summary>
        public static readonly Country NFK = new Country(574, "NFK", "NF","Norfolk",string.Empty, Currency.AUD, "","en");
        /// <summary>
        /// Norvège
        /// </summary>
        public static readonly Country NOR = new Country(578, "NOR", "NO","Norvège",string.Empty, Currency.NOK, "47","no");
        /// <summary>
        /// Nouvelle-Calédonie
        /// </summary>
        public static readonly Country NCL = new Country(540, "NCL", "NC","Nouvelle-Calédonie",string.Empty, Currency.EUR, "687","en");
        /// <summary>
        /// Nouvelle-Zélande
        /// </summary>
        public static readonly Country NZL = new Country(554, "NZL", "NZ","Nouvelle-Zélande",string.Empty, Currency.NZD, "64","en");
        /// <summary>
        /// Oman
        /// </summary>
        public static readonly Country OMN = new Country(512, "OMN", "OM","Oman",string.Empty, Currency.OMR, "968","en");
        /// <summary>
        /// Ouganda
        /// </summary>
        public static readonly Country UGA = new Country(800, "UGA", "UG","Ouganda",string.Empty, Currency.UGX, "256","en");
        /// <summary>
        /// Ouzbékistan
        /// </summary>
        public static readonly Country UZB = new Country(860, "UZB", "UZ","Ouzbékistan",string.Empty, Currency.UZS, "998","ur");
        /// <summary>
        /// Pakistan
        /// </summary>
        public static readonly Country PAK = new Country(586, "PAK", "PK","Pakistan",string.Empty, Currency.PKR, "92","en");
        /// <summary>
        /// Palaos
        /// </summary>
        public static readonly Country PLW = new Country(585, "PLW", "PW","Palaos",string.Empty, Currency.USD, "680","en");
        /// <summary>
        /// Palestine
        /// </summary>
        public static readonly Country PSE = new Country(275, "PSE", "PS","Palestine",string.Empty, Currency.ILS, "","en");
        /// <summary>
        /// Panamá
        /// </summary>
        public static readonly Country PAN = new Country(591, "PAN", "PA","Panamá",string.Empty, Currency.PAB, "507","en");
        /// <summary>
        /// Papouasie-Nouvelle-Guinée
        /// </summary>
        public static readonly Country PNG = new Country(598, "PNG", "PG","Papouasie-Nouvelle-Guinée",string.Empty, Currency.PGK, "675","en");
        /// <summary>
        /// Paraguay
        /// </summary>
        public static readonly Country PRY = new Country(600, "PRY", "PY","Paraguay",string.Empty, Currency.PYG, "595","es");
        /// <summary>
        /// Pays-Bas
        /// </summary>
        public static readonly Country NLD = new Country(528, "NLD", "NL","Pays-Bas",string.Empty, Currency.EUR, "31","nl");
        /// <summary>
        /// Pérou
        /// </summary>
        public static readonly Country PER = new Country(604, "PER", "PE","Pérou",string.Empty, Currency.PEN, "51","es");
        /// <summary>
        /// Philippines
        /// </summary>
        public static readonly Country PHL = new Country(608, "PHL", "PH","Philippines",string.Empty, Currency.PHP, "63","en");
        /// <summary>
        /// Pitcairn
        /// </summary>
        public static readonly Country PCN = new Country(612, "PCN", "PN","Pitcairn",string.Empty, Currency.NZD, "","en");
        /// <summary>
        /// Pologne
        /// </summary>
        public static readonly Country POL = new Country(616, "POL", "PL","Pologne",string.Empty, Currency.PLN, "48","pa");
        /// <summary>
        /// Polynésie française
        /// </summary>
        public static readonly Country PYF = new Country(258, "PYF", "PF","Polynésie française",string.Empty, Currency.EUR, "689","fr");
        /// <summary>
        /// Porto Rico
        /// </summary>
        public static readonly Country PRI = new Country(630, "PRI", "PR","Porto Rico",string.Empty, Currency.USD, "","en");
        /// <summary>
        /// Portugal
        /// </summary>
        public static readonly Country PRT = new Country(620, "PRT", "PT","Portugal",string.Empty, Currency.EUR, "351","pt");
        /// <summary>
        /// Qatar
        /// </summary>
        public static readonly Country QAT = new Country(634, "QAT", "QA","Qatar",string.Empty, Currency.QAR, "974","ar");
        /// <summary>
        /// La Réunion
        /// </summary>
        public static readonly Country REU = new Country(638, "REU", "RE","La Réunion",string.Empty, Currency.EUR, "262","fr");
        /// <summary>
        /// Roumanie
        /// </summary>
        public static readonly Country ROU = new Country(642, "ROU", "RO","Roumanie",string.Empty, Currency.RON, "40","ro");
        /// <summary>
        /// Royaume-Uni
        /// </summary>
        public static readonly Country GBR = new Country(826, "GBR", "GB","Royaume-Uni",string.Empty, Currency.GBP, "44","en");
        /// <summary>
        /// Russie
        /// </summary>
        public static readonly Country RUS = new Country(643, "RUS", "RU","Russie",string.Empty, Currency.RUB, "7","ru");
        /// <summary>
        /// Rwanda
        /// </summary>
        public static readonly Country RWA = new Country(646, "RWA", "RW","Rwanda",string.Empty, Currency.RWF, "250","fr");
        /// <summary>
        /// Sahara occidental
        /// </summary>
        public static readonly Country ESH = new Country(732, "ESH", "EH","Sahara occidental",string.Empty, Currency.EUR, "","en");
        /// <summary>
        /// Saint-Barthélemy
        /// </summary>
        public static readonly Country BLM = new Country(652, "BLM", "BL","Saint-Barthélemy",string.Empty, Currency.USD, "","en");
        /// <summary>
        /// Saint-Christophe-et-Niévès
        /// </summary>
        public static readonly Country KNA = new Country(659, "KNA", "KN","Saint-Christophe-et-Niévès",string.Empty, Currency.EUR, "","en");
        /// <summary>
        /// Saint-Marin
        /// </summary>
        public static readonly Country SMR = new Country(674, "SMR", "SM","Saint-Marin",string.Empty, Currency.EUR, "378","en");
        /// <summary>
        /// Saint-Martin
        /// </summary>
        public static readonly Country MAF = new Country(663, "MAF", "MF","Saint-Martin",string.Empty, Currency.EUR, "","en");
        /// <summary>
        /// Saint-Pierre-et-Miquelon
        /// </summary>
        public static readonly Country SPM = new Country(666, "SPM", "PM","Saint-Pierre-et-Miquelon",string.Empty, Currency.EUR, "508","fr");
        /// <summary>
        /// Vatican / (Saint-Siège)
        /// </summary>
        public static readonly Country VAT = new Country(336, "VAT", "VA","Vatican / (Saint-Siège)",string.Empty, Currency.EUR, "39","it");
        /// <summary>
        /// Saint-Vincent-et-les Grenadines
        /// </summary>
        public static readonly Country VCT = new Country(670, "VCT", "VC","Saint-Vincent-et-les Grenadines",string.Empty, Currency.EUR, "","en");
        /// <summary>
        /// Sainte-Hélène (territoire)
        /// </summary>
        public static readonly Country SHN = new Country(654, "SHN", "SH","Sainte-Hélène (territoire)",string.Empty, Currency.EUR, "290","en");
        /// <summary>
        /// Sainte-Lucie
        /// </summary>
        public static readonly Country LCA = new Country(662, "LCA", "LC","Sainte-Lucie",string.Empty, Currency.EUR, "","en");
        /// <summary>
        /// Îles Salomon
        /// </summary>
        public static readonly Country SLB = new Country(90, "SLB", "SB","Îles Salomon",string.Empty, Currency.SBD, "677","en");
        /// <summary>
        /// Samoa
        /// </summary>
        public static readonly Country WSM = new Country(882, "WSM", "WS","Samoa",string.Empty, Currency.WST, "685","en");
        /// <summary>
        /// Samoa américaines
        /// </summary>
        public static readonly Country ASM = new Country(16, "ASM", "AS","Samoa américaines",string.Empty, Currency.EUR, "684","en");
        /// <summary>
        /// São Tomé-et-Principe
        /// </summary>
        public static readonly Country STP = new Country(678, "STP", "ST","São Tomé-et-Principe",string.Empty, Currency.EUR, "239","en");
        /// <summary>
        /// Sénégal
        /// </summary>
        public static readonly Country SEN = new Country(686, "SEN", "SN","Sénégal",string.Empty, Currency.XOF, "221","fr");
        /// <summary>
        /// Serbie
        /// </summary>
        public static readonly Country SRB = new Country(688, "SRB", "RS","Serbie",string.Empty, Currency.RSD, "381","sr");
        /// <summary>
        /// Seychelles
        /// </summary>
        public static readonly Country SYC = new Country(690, "SYC", "SC","Seychelles",string.Empty, Currency.SCR, "248","en");
        /// <summary>
        /// Sierra Leone
        /// </summary>
        public static readonly Country SLE = new Country(694, "SLE", "SL","Sierra Leone",string.Empty, Currency.SLL, "232","en");
        /// <summary>
        /// Singapour
        /// </summary>
        public static readonly Country SGP = new Country(702, "SGP", "SG","Singapour",string.Empty, Currency.SGD, "65","en");
        /// <summary>
        /// Slovaquie
        /// </summary>
        public static readonly Country SVK = new Country(703, "SVK", "SK","Slovaquie",string.Empty, Currency.SKK, "421","sk");
        /// <summary>
        /// Slovénie
        /// </summary>
        public static readonly Country SVN = new Country(705, "SVN", "SI","Slovénie",string.Empty, Currency.EUR, "386","sl");
        /// <summary>
        /// Somalie
        /// </summary>
        public static readonly Country SOM = new Country(706, "SOM", "SO","Somalie",string.Empty, Currency.SOS, "252","en");
        /// <summary>
        /// Soudan
        /// </summary>
        public static readonly Country SDN = new Country(736, "SDN", "SD","Soudan",string.Empty, Currency.SDG, "249","ar");
        /// <summary>
        /// Sri Lanka
        /// </summary>
        public static readonly Country LKA = new Country(144, "LKA", "LK","Sri Lanka",string.Empty, Currency.LKR, "94","en");
        /// <summary>
        /// Suède
        /// </summary>
        public static readonly Country SWE = new Country(752, "SWE", "SE","Suède",string.Empty, Currency.SEK, "46","sv");
        /// <summary>
        /// Suisse
        /// </summary>
        public static readonly Country CHE = new Country(756, "CHE", "CH","Suisse",string.Empty, Currency.CHF, "41","fr");
        /// <summary>
        /// Suriname
        /// </summary>
        public static readonly Country SUR = new Country(740, "SUR", "SR","Suriname",string.Empty, Currency.SRD, "597","en");
        /// <summary>
        /// Swaziland
        /// </summary>
        public static readonly Country SWZ = new Country(748, "SWZ", "SZ","Swaziland",string.Empty, Currency.USD, "268","en");
        /// <summary>
        /// Syrie
        /// </summary>
        public static readonly Country SYR = new Country(760, "SYR", "SY","Syrie",string.Empty, Currency.SYP, "963","ar");
        /// <summary>
        /// Tadjikistan
        /// </summary>
        public static readonly Country TJK = new Country(762, "TJK", "TJ","Tadjikistan",string.Empty, Currency.TJS, "992","en");
        /// <summary>
        /// Taïwan / (République de Chine)
        /// </summary>
        public static readonly Country TWN = new Country(158, "TWN", "TW","Taïwan / (République de Chine)",string.Empty, Currency.CNY, "886","en");
        /// <summary>
        /// Tanzanie
        /// </summary>
        public static readonly Country TZA = new Country(834, "TZA", "TZ","Tanzanie",string.Empty, Currency.TZS, "255","en");
        /// <summary>
        /// Tchad
        /// </summary>
        public static readonly Country TCD = new Country(148, "TCD", "TD","Tchad",string.Empty, Currency.XAF, "235","fr");
        /// <summary>
        /// République tchèque
        /// </summary>
        public static readonly Country CZE = new Country(203, "CZE", "CZ","République tchèque",string.Empty, Currency.CZK, "420","cs");
        /// <summary>
        /// Terres australes et antarctiques françaises
        /// </summary>
        public static readonly Country ATF = new Country(260, "ATF", "TF","Terres australes et antarctiques françaises",string.Empty, Currency.EUR, "","en");
        /// <summary>
        /// Thaïlande
        /// </summary>
        public static readonly Country THA = new Country(764, "THA", "TH","Thaïlande",string.Empty, Currency.THB, "66","th");
        /// <summary>
        /// Timor oriental
        /// </summary>
        public static readonly Country TLS = new Country(626, "TLS", "TL","Timor oriental",string.Empty, Currency.EUR, "670","en");
        /// <summary>
        /// Togo
        /// </summary>
        public static readonly Country TGO = new Country(768, "TGO", "TG","Togo",string.Empty, Currency.XAF, "228","fr");
        /// <summary>
        /// Tokelau
        /// </summary>
        public static readonly Country TKL = new Country(772, "TKL", "TK","Tokelau",string.Empty, Currency.NZD, "690","en");
        /// <summary>
        /// Tonga
        /// </summary>
        public static readonly Country TON = new Country(776, "TON", "TO","Tonga",string.Empty, Currency.EUR, "676","en");
        /// <summary>
        /// Trinité-et-Tobago
        /// </summary>
        public static readonly Country TTO = new Country(780, "TTO", "TT","Trinité-et-Tobago",string.Empty, Currency.TTD, "","en");
        /// <summary>
        /// Tunisie
        /// </summary>
        public static readonly Country TUN = new Country(788, "TUN", "TN","Tunisie",string.Empty, Currency.TND, "216","ar");
        /// <summary>
        /// Turkménistan
        /// </summary>
        public static readonly Country TKM = new Country(795, "TKM", "TM","Turkménistan",string.Empty, Currency.TMM, "993","en");
        /// <summary>
        /// Îles Turques et Caïques
        /// </summary>
        public static readonly Country TCA = new Country(796, "TCA", "TC","Îles Turques et Caïques",string.Empty, Currency.EUR, string.Empty,"en");
        /// <summary>
        /// Turquie
        /// </summary>
        public static readonly Country TUR = new Country(792, "TUR", "TR","Turquie",string.Empty, Currency.YTL, "90","tr");
        /// <summary>
        /// Tuvalu
        /// </summary>
        public static readonly Country TUV = new Country(798, "TUV", "TV","Tuvalu",string.Empty, Currency.AUD, "688","en");
        /// <summary>
        /// Ukraine
        /// </summary>
        public static readonly Country UKR = new Country(804, "UKR", "UA","Ukraine",string.Empty, Currency.UAH, "380","uk");
        /// <summary>
        /// Uruguay
        /// </summary>
        public static readonly Country URY = new Country(858, "URY", "UY","Uruguay",string.Empty, Currency.UYU, "598","en");
        /// <summary>
        /// Vanuatu
        /// </summary>
        public static readonly Country VUT = new Country(548, "VUT", "VU","Vanuatu",string.Empty, Currency.VUV, "678","en");
        /// <summary>
        /// Venezuela
        /// </summary>
        public static readonly Country VEN = new Country(862, "VEN", "VE","Venezuela",string.Empty, Currency.VEF, "58","es");
        /// <summary>
        /// Viêt Nam
        /// </summary>
        public static readonly Country VNM = new Country(704, "VNM", "VN","Viêt Nam",string.Empty, Currency.VND, "84","vi");
        /// <summary>
        /// Wallis-et-Futuna
        /// </summary>
        public static readonly Country WLF = new Country(876, "WLF", "WF","Wallis-et-Futuna",string.Empty, Currency.EUR, "681","fr");
        /// <summary>
        /// Yémen
        /// </summary>
        public static readonly Country YEM = new Country(887, "YEM", "YE","Yémen",string.Empty, Currency.YER, "967","en");
        /// <summary>
        /// Zambie
        /// </summary>
        public static readonly Country ZMB = new Country(894, "ZMB", "ZM","Zambie",string.Empty, Currency.MWK, "260","en");
        /// <summary>
        /// Zimbabwe
        /// </summary>
        public static readonly Country ZWE = new Country(716, "ZWE", "ZW","Zimbabwe",string.Empty, Currency.ZWD, "263","en");

        /// <summary>
        /// Initializes a new instance of the <see cref="Country"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="fr">The fr.</param>
        /// <param name="en">The en.</param>
        public Country(int id, string name, string fr, string en) :
            base(id, name, fr, en)
        {

        }

		/// <summary>
		/// Initializes a new instance of the <see cref="Country"/> class.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="name">The name.</param>
		/// <param name="alpha2">The alpha2.</param>
		/// <param name="fr">The fr.</param>
		/// <param name="en">The en.</param>
		/// <param name="currency">The currency.</param>
		/// <param name="phonePrefix">The phone prefix.</param>
		/// <param name="languageName">Name of the language.</param>
		public Country(int id, string name, string alpha2, string fr, string en, Currency currency, string phonePrefix, string languageName)
			: this(id, name, alpha2, fr, en, currency, phonePrefix, languageName, null)
		{
		}

        public Country(int id, string name, string alpha2, string fr, string en, Currency currency, string phonePrefix, string languageName, string phoneMask) 
            : this(id, name, fr, en)
        {
            // m_CultureInfo = GetByTwoLetterISOLangageName(isoCode);
            // en = m_CultureInfo.NativeName;
			Alpha2 = alpha2;
            Currency = currency;
            PhonePrefix = phonePrefix;
            LanguageName = languageName;
            // Console.Write(CultureInfo.Name);
			PhoneMask = phoneMask;
        }


        /// <summary>
        /// Gets the default.
        /// </summary>
        /// <value>The default.</value>
        public static Country Default
        {
            get
            {
                return Country.FRA;
            }
        }



        public static void GenerateCountryList()
        {
            System.Globalization.CultureInfo[] list = System.Globalization.CultureInfo.GetCultures(System.Globalization.CultureTypes.AllCultures);
            List<System.Globalization.CultureInfo> newList = new List<System.Globalization.CultureInfo>();
            foreach (System.Globalization.CultureInfo ci in list)
            {
                newList.Add(ci);
            }

            newList.Sort(delegate(System.Globalization.CultureInfo x, System.Globalization.CultureInfo y)
            {
                return x.ThreeLetterISOLanguageName.CompareTo(y.ThreeLetterISOLanguageName);
            });
            foreach (System.Globalization.CultureInfo ci in newList)
            {
                if (ci.EnglishName.IndexOf("(") == -1)
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}"
                        , ci.ThreeLetterISOLanguageName.ToUpper()
                        , ci.TextInfo.LCID
                        , ci.DisplayName
                        , ci.EnglishName
                        , ci.Name
                        , ci.TextInfo.CultureName);
                 }

            }
            Console.WriteLine();
            Console.WriteLine();
        }

        private System.Globalization.CultureInfo m_CultureInfo;

        /// <summary>
        /// Gets the Lazy loaded culture info
        /// </summary>
        /// <value>The culture info.</value>
        public System.Globalization.CultureInfo CultureInfo
        {
            get 
            {
                if (m_CultureInfo == null)
                {
					m_CultureInfo = System.Globalization.CultureInfo.GetCultureInfo(LanguageName);
                }
                return m_CultureInfo; 
            }
        }
		        
		/// <summary>
		/// Gets or sets the currency.
		/// </summary>
		/// <value>The currency.</value>
		public Currency Currency { get; private set; }
               
		/// <summary>
		/// Gets or sets the phone prefix.
		/// </summary>
		/// <value>The phone prefix.</value>
		public string PhonePrefix { get; private set; }
        
        
		/// <summary>
		/// Gets or sets the name of the language.
		/// </summary>
		/// <value>The name of the language.</value>
		public string LanguageName { get; private set; }
        
				
		/// <summary>
		/// Gets or sets the alpha2.
		/// </summary>
		/// <value>The alpha2.</value>
		public string Alpha2 { get; private set; }
		

		/// <summary>
		/// Gets or sets the phone mask.
		/// </summary>
		/// <value>The phone mask.</value>
		public string PhoneMask  { get; private set; }


		/// <summary>
		/// Gets or sets the free of carriage amount.
		/// </summary>
		/// <value>The free of carriage amount.</value>
		public decimal FreeOfCarriageAmount { get; set; }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns></returns>
        public static ReadOnlyCollection<Country> GetValues()
        {
			return GetBaseValues();
        }

        /// <summary>
        /// Gets the by key.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns></returns>
        public static Country GetByKey(int id)
        {
            return GetBaseByKey(id);
        }

        public static Country GetByISOTheeLetterCountryName(string threeLetterCountryName)
        {
            foreach (Country t in enumValues)
            {
                if (t.Name.Equals(threeLetterCountryName, StringComparison.InvariantCultureIgnoreCase))
                {
                    return t;
                }
            }
            return null;
        }
		public static Country GetByAlpha2(string alpha2)
		{
			foreach (Country t in enumValues)
			{
				if (t.Alpha2.Equals(alpha2, StringComparison.InvariantCultureIgnoreCase))
				{
					return t;
				}
			}
			return null;
		}

    }
}
