using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ERPStore.Models
{
	/// <summary>
	/// Definition d'un marque de produit
	/// </summary>
	[Serializable]
	public class Brand : ICloneable
	{

		/// <summary>
		/// Identifiant interne de la marque
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Nom de la marque
		/// </summary>
		/// <remarks>
		/// Il est unique et ne peut pas etre null
		/// </remarks>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Partie du lien figurant dans une url.
		/// </summary>
		/// <remarks>
		/// Ce lien est unique pour pouvoir retrouver la marque via celui-ci
		/// </remarks>
		/// <value>The link.</value>
		public string Link { get; set; }
		/// <summary>
		/// Description de la marque
		/// </summary>
		/// <remarks>
		/// Est utilisé en tant que metas informations dans le head d'une page html
		/// </remarks>
		/// <value>The page description.</value>
		public string PageDescription { get; set; }
		/// <summary>
		/// Mots clés representant la marque
		/// </summary>
		/// <remarks>
		/// Est utilisé en tant que metas informations dans le head d'une page html
		/// </remarks>
		/// <value>The page keywords.</value>
		public string Keywords { get; set; }
		/// <summary>
		/// Titre de la page html
		/// </summary>
		/// <value>The page title.</value>
		public string PageTitle { get; set; }
		/// <summary>
		/// Image par defaut de la marque
		/// </summary>
		/// <remarks>
		/// Cette valeur est nulle s'il n'y a pas d'image
		/// </remarks>
		/// <value>The default image.</value>
		public Media DefaultImage { get; set; }
		/// <summary>
		/// Retourne l'identifiant de l'image
		/// </summary>
		/// <remarks>
		/// Ne jamais montrer cette valeur sur le site
		/// </remarks>
		/// <value>The default image id.</value>
		public int? DefaultImageId { get; set; }
		/// <summary>
		/// Indique l'url de la marque ou du constructeur
		/// </summary>
		/// <value>The external brand link.</value>
		public string ExternalBrandLink { get; set; }
		/// <summary>
		/// Indique que cette marque sera affichée sur la page d'accueil du site
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is forefront; otherwise, <c>false</c>.
		/// </value>
		public bool IsForefront { get; set; }
		/// <summary>
		/// Nombre de produits associés à cette marque
		/// </summary>
		/// <value>The product count.</value>
		public int ProductCount { get; set; }

		/// <summary>
		/// Indique si la categorie est selectionnée
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
		/// </value>
		public bool IsSelected { get; set; }

		#region ICloneable Members

		public object Clone()
		{
			object obj = null;
			using (var ms = new MemoryStream())
			{
				var bf = new BinaryFormatter();
				bf.Serialize(ms, this);
				ms.Position = 0;
				obj = bf.Deserialize(ms);
				ms.Close();
			}
			return obj;
		}

		#endregion
	}
}
