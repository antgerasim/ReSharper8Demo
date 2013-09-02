using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace ERPStore.Models
{
	[Serializable]
	public class Metric : EnumBaseType<Metric>
	{
		public static readonly Metric Unit = new Metric(-1, "Unit", "Unité", "Unit", "U", 1);

		/// <summary>
		/// 
		/// Unité de longueur
		/// 
		/// http://fr.wikipedia.org/wiki/M%C3%A8tre
		/// </summary>
		public static readonly Metric Meter = new Metric(-100, "Meter", "Metre", "Meter", "m", 1);
		public static readonly Metric KiloMeter = new Metric(-101, "KiloMeter", "Kilometre", "Kilometer", "km", 1000);
		public static readonly Metric HectoMeter = new Metric(-102, "HectoMeter", "Hectometre", "Hectometer", "hm", 100);
		public static readonly Metric DecaMeter = new Metric(-103, "DecaMeter", "Decametre", "Decameter", "dam", 10);
		public static readonly Metric DeciMeter = new Metric(-104, "DeciMeter", "Decimetre", "Decimeter", "hm", 0.1);
		public static readonly Metric CentiMeter = new Metric(-105, "Centimeter", "Centimetre", "Centimeter", "cm", 0.01);
		public static readonly Metric MilliMeter = new Metric(-106, "Millimeter", "Millimetre", "Millimeter", "mm", 0.001);
		public static readonly Metric MicroMeter = new Metric(-107, "Micrometer", "Micron", "MicroMeter", "mm", 0.000001);

		/// <summary>
		/// Unité de temps
		/// </summary>
		public static readonly Metric Second = new Metric(-200, "Second", "Seconde", "Second", "sec", 1);
		public static readonly Metric Minute = new Metric(-201, "Minute", "Minute", "Minute", "min", 60);
		public static readonly Metric Hour = new Metric(-202, "Hour", "Heure", "Hour", "h", 60 * 60);
		public static readonly Metric Day = new Metric(-203, "Day", "Jour", "Day", "j", 60 * 60 * 24);

		public static readonly Metric Week = new Metric(-204, "Week", "Semaine", "Week", "sem", 7);
		public static readonly Metric Month = new Metric(-205, "Month", "Mois", "Month", "m", 1);

		/// <summary>
		/// Unités de poids
		/// </summary>
		public static readonly Metric Gram = new Metric(-300, "Gram", "Gramme", "Gram", "g", 1);
		public static readonly Metric Ton = new Metric(-305, "Ton", "Tonne", "Ton", "T", 1000000);
		public static readonly Metric KiloGram = new Metric(-301, "KiloGram", "Kilogramme", "Kilogram", "kg", 1000);
		public static readonly Metric HectoGram = new Metric(-302, "HectoGram", "Hectogramme", "Hectogram", "hg", 100);
		public static readonly Metric DecaGram = new Metric(-304, "DecaGram", "Decagramme", "Decagram", "dag", 10);
		public static readonly Metric DeciGram = new Metric(-306, "DeciGram", "Decigramme", "Decigram", "dg", 0.1);
		public static readonly Metric CentiGram = new Metric(-307, "CentiGram", "Centigramme", "Centigram", "cg", 0.01);
		public static readonly Metric MilliGram = new Metric(-308, "MilliGram", "Milligramme", "Milligram", "mg", 0.001);

		/// <summary>
		/// Unités de volume
		/// </summary>
		public static readonly Metric Liter = new Metric(-400, "Liter", "Litre", "Liter", "l", 1);
		public static readonly Metric KiloLiter = new Metric(-401, "KiloLiter", "Kilolitre (m3)", "Kiloliter (m3)", "kl", 1000);
		public static readonly Metric HectoLiter = new Metric(-402, "HectoLiter", "Hectolitre", "Hectoliter", "hl", 100);
		public static readonly Metric DecaLiter = new Metric(-404, "DecaLiter", "Decalitre", "Decaliter", "dal", 10);
		public static readonly Metric DeciLiter = new Metric(-406, "DeciLiter", "Decilitre", "Deciliter", "dl", 0.1);
		public static readonly Metric CentiLiter = new Metric(-407, "CentiLiter", "Centilitre", "Centiliter", "cl", 0.01);
		public static readonly Metric MilliLiter = new Metric(-408, "MilliLiter", "Millilitre", "Milliliter", "ml", 0.001);
		public static readonly Metric MicroLiter = new Metric(-409, "MicroLiter", "MicroLitre", "MicroLiter", "µl", 0.000001);

		public static Metric Default
		{
			get
			{
				return Unit;
			}
		}

		public Metric(int id, string name, string fr, string en, string abrevation, double multiple)
			: base(id, name, fr, en)
		{
			Abrevation = abrevation;
			Multiple = multiple;
		}

		public string Abrevation { get; set; }
		public double Multiple { get; set; }

		/// <summary>
		/// Gets the values.
		/// </summary>
		/// <returns></returns>
		public static ReadOnlyCollection<Metric> GetValues()
		{
			return GetBaseValues();
		}

		/// <summary>
		/// Gets the by key.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
		public static Metric GetByKey(int id)
		{
			return GetBaseByKey(id);
		}
	}
}
