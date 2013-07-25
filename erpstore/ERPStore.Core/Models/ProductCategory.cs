using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace ERPStore.Models
{
	/// <summary>
	/// Categorie de produit
	/// </summary>
	[Serializable]
	[DataContract]
	public class ProductCategory : ICloneable, ILocalizable, IDisposable
	{
		public ProductCategory()
		{

		}

		/// <summary>
		/// Identifiant interne de la categorie
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Nom de la categorie
		/// </summary>
		/// <value>The name.</value>
		public string Name { get; set; }
		/// <summary>
		/// Partie du lien permettant d'afficher la categorie dans une URL, le lien est unique
		/// </summary>
		/// <value>The link.</value>
		public string Link { get; set; }
		/// <summary>
		/// Description de la catégorie
		/// </summary>
		/// <remarks>
		/// Est utilisé en tant que metas informations dans le head d'une page html
		/// </remarks>
		/// <value>The description.</value>
		public string PageDescription { get; set; }
		/// <summary>
		/// Code de la categorie
		/// </summary>
		/// <value>The code.</value>
		public string Code { get; set; }
		/// <summary>
		/// Retourne une image par defaut
		/// </summary>
		/// <value>The default image. retourne null s'il n'y a pas d'image associée</value>
		public Media DefaultImage { get; set; }
		/// <summary>
		/// Retourne l'identifiant interne de l'image
		/// </summary>
		/// <remarks>
		/// Ne pas utiliser cette valeur sur le site
		/// </remarks>
		/// <value>The defautl imageid.</value>
		public int? DefaultImageId { get; set; }
		/// <summary>
		/// Indique le nombre de produits associés à cette categorie
		/// </summary>
		/// <value>The product count.</value>
		public int ProductCount { get; set; }
		/// <summary>
		/// Indique la categorie parente
		/// </summary>
		/// <remarks>
		/// S'il s'agit d'une categorie racine, ce paramettre sera à null
		/// </remarks>
		/// <value>The parent.</value>
		[System.Xml.Serialization.XmlIgnore]
		[IgnoreDataMember]
		public ProductCategory Parent { get; set; }
		/// <summary>
		/// Retourne le nombre de produits y compris dans les sous-categories
		/// </summary>
		/// <value>The deep product count.</value>
		public int DeepProductCount
		{
			get
			{
				return ProductCount + this.Children.DeepProductCount();
			}
		}
		/// <summary>
		/// Indique les mots clés associés a la categorie
		/// </summary>
		/// <remarks>
		/// Est utilisé en tant que metas données dans une page html
		/// </remarks>
		/// <value>The keywords.</value>
		public string Keywords { get; set; }

		private List<ProductCategory> m_Children;
		/// <summary>
		/// Liste des categories filles
		/// </summary>
		/// <remarks>
		/// Ce paramtre est vide (null) s'il n'y a pas de categorie fille
		/// </remarks>
		/// <value>The children.</value>
		[System.Xml.Serialization.XmlIgnore]
		public List<ProductCategory> Children
		{
			get
			{
				if (m_Children == null)
				{
					m_Children = new List<ProductCategory>();
				}
				return m_Children;
			}
		}
		/// <summary>
		/// Indique que la catégorie peut etre affiché sur la page d'accueil du site
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is forefront; otherwise, <c>false</c>.
		/// </value>
		public bool IsForefront { get; set; }

		/// <summary>
		/// Niveau dans la hierarchie
		/// </summary>
		/// <value>The level.</value>
		public int Level { get; set; }

		/// <summary>
		/// Indique que la categorie est selectionnée
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is selected; otherwise, <c>false</c>.
		/// </value>
		public bool IsSelected { get; set; }

		/// <summary>
		/// Order d'apparition lorsque la categorie est visible sur la page d'accueil
		/// </summary>
		/// <value>The front order.</value>
		public int? FrontOrder { get; set; }

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
				bf = null;
			}
			return obj;
		}

		#endregion

		#region ILocalizable Members

		/// <summary>
		/// Liste des propriétés de type string traduites
		/// </summary>
		/// <value>The localization list.</value>
		public IEnumerable<EntityLocalization> LocalizationList { get; set; }

		#endregion

		#region IDisposable Members

		public void Dispose()
		{
			if (Children != null)
			{
				foreach (var item in Children)
				{
					item.Dispose();
				}
				this.DefaultImage = null;
				this.LocalizationList = null;
			}
		}

		#endregion
	}
}
