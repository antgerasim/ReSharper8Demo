using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ERPStore.Models
{
	/// <summary>
	/// Represente une adresse
	/// </summary>
	[Serializable]
	public class Address : IComparable, ICloneable
	{
		public Address()
		{

		}

		/// <summary>
		/// Identifiant interne de l'adresse
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Nom du destinataire
		/// </summary>
		/// <value>The name of the recipient.</value>
		public string RecipientName { get; set; }
		/// <summary>
		/// Nom de la société
		/// </summary>
		/// <value>The name of the corporate.</value>
		public string CorporateName { get; set; }
		/// <summary>
		/// Rue
		/// </summary>
		/// <value>The street.</value>
		public string Street { get; set; }
		/// <summary>
		/// Code postal
		/// </summary>
		/// <value>The zip code.</value>
		public string ZipCode { get; set; }
		/// <summary>
		/// Ville
		/// </summary>
		/// <value>The city.</value>
		public string City { get; set; }
		/// <summary>
		/// Region ou nom du departement
		/// </summary>
		/// <value>The region.</value>
		public string Region { get; set; }
		/// <summary>
		/// Pays
		/// </summary>
		/// <value>The country.</value>
		[XmlIgnore]
		public Country Country 
		{
			get
			{
				return Country.GetByKey(CountryId);
			}
		}
		/// <summary>
		/// Identifiant du pays
		/// </summary>
		/// <value>The country id.</value>
		public int CountryId { get; set; }

		#region IComparable Members

		public int CompareTo(object obj)
		{
			var address = obj as Address;
			if (this.Id != 0)
			{
				return this.Id.CompareTo(address.Id);
			}

			if (this.RecipientName == address.RecipientName
				&& this.Street == address.Street
				&& this.ZipCode == address.ZipCode
				&& this.City == address.City
				&& this.Region == address.Region
				&& this.CountryId == address.CountryId)
			{
				return 0;
			}
			return 1;
		}

		#endregion

		#region ICloneable Members

		public object Clone()
		{
			return this.MemberwiseClone();
		}

		#endregion
	}
}
