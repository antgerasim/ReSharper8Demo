using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Models
{
	/// <summary>
	/// Definition d'un produit
	/// </summary>
	[Serializable]
	public class Product : IExtendable, ILocalizable
	{
		public Product()
		{
			Transport = new TransportSettings();
			SaleMode = ProductSaleMode.Sellable;
			PriceByQuantityList = new List<PriceByQuantity>();
		}

		/// <summary>
		/// Identifiant du produit interne
		/// </summary>
		/// <value>The id.</value>
		public int Id { get; set; }
		/// <summary>
		/// Date de création du produit
		/// </summary>
		/// <value>The creation date.</value>
		public DateTime CreationDate { get; set; }
		/// <summary>
		/// Code unique du produit
		/// </summary>
		/// <value>The code.</value>
		[Searchable]
		public string Code { get; set; }
		/// <summary>
		/// Titre du produit
		/// </summary>
		/// <value>The title.</value>
		[Searchable]
		public string Title { get; set; }
		/// <summary>
		/// Description courte , normalement pas plus de 2 lignes
		/// </summary>
		/// <value>The short description.</value>
		[Searchable]
		public string ShortDescription { get; set; }
		/// <summary>
		/// Description longue (pas de limite de longueur) peut contenir du texte en html
		/// </summary>
		/// <value>The long description.</value>
		[Searchable]
		public string LongDescription { get; set; }
		/// <summary>
		/// Category à laquelle appartient le produit
		/// si la catégorie n'est pas référencée pour le site, cette valeur sera null
		/// </summary>
		/// <value>The category.</value>
		public ProductCategory Category { get; set; }
		/// <summary>
		/// Marque à laquelle appartient le produit
		/// si la marque n'est pas référencée pour le site, alors il est attaché a "Sans marque"
		/// </summary>
		/// <value>La marque, cette valeur n'est jamais nulle.</value>
		public Brand Brand { get; set; }
		/// <summary>
		/// Empaquetage du produit
		/// </summary>
		/// <value>The packaging.</value>
		public Packaging Packaging { get; set; }
		/// <summary>
		/// Unité de vente les valeurs peuvent etre 1 , 100 , 1000 ou 10000
		/// </summary>
		/// <value>The sale unit value.</value>
		public int SaleUnitValue { get; set; }
		/// <summary>
		/// Taux de taxe par defaut
		/// </summary>
		/// <value>The default tax rate.</value>
		public double DefaultTaxRate { get; set; }
		/// <summary>
		/// Liste des mots clés, utilisé comme information meta dans la page html du produit
		/// </summary>
		/// <value>The keywords.</value>
		[Searchable]
		public string Keywords { get; set; }
		/// <summary>
		/// Indique si le produit est en promotion
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is promotion; otherwise, <c>false</c>.
		/// </value>
		public bool IsPromotion { get; set; }
		/// <summary>
		/// Indique si le produit est un nouveau produit
		/// </summary>
		/// <value><c>true</c> if this instance is new; otherwise, <c>false</c>.</value>
		public bool IsNew { get; set; }
		/// <summary>
		/// Indique si le produit est en destockage
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is destock; otherwise, <c>false</c>.
		/// </value>
		public bool IsDestock { get; set; }
		/// <summary>
		/// Indique que le produit est dans le top vente
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is top sell; otherwise, <c>false</c>.
		/// </value>
		public bool IsTopSell { get; set; }
		/// <summary>
		/// Indique que le prix est le meilleur prix du marché
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is first price; otherwise, <c>false</c>.
		/// </value>
		public bool IsFirstPrice { get; set; }

		/// <summary>
		/// Indique que le prix est un prix client remisé
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance is customer price; otherwise, <c>false</c>.
		/// </value>
		public bool IsCustomerPrice 
		{
			get
			{
				return CustomerPrice != null;
			}
		}
		/// <summary>
		/// Partie du lien vers la fiche produit
		/// </summary>
		/// <value>The link.</value>
		public string Link { get; set; }
		/// <summary>
		/// Texte sur les points forts du produit
		/// </summary>
		/// <value>The strengths points.</value>
		[Searchable]
		public string StrengthsPoints { get; set; }
		/// <summary>
		/// Url du constructeur du produit
		/// </summary>
		/// <value>The manufacturer URL.</value>
		public string ManufacturerUrl { get; set; }
		/// <summary>
		/// Poids du produit en grammes
		/// </summary>
		/// <value>The weight.</value>
		public int Weight { get; set; }
		/// <summary>
		/// Titre de la page web
		/// </summary>
		/// <remarks>
		/// Ne doit etre utilisé que dans le header de la page
		/// </remarks>
		/// <value>The page title.</value>
		[Searchable]
		public string PageTitle { get; set; }
		/// <summary>
		/// Description de la page web
		/// </summary>
		/// <remarks>
		/// Ne doit etre utilisé que dans le header de la page
		/// </remarks>
		/// <value>The page description.</value>
		[Searchable]
		public string PageDescription { get; set; }
		/// <summary>
		/// Nombre de commande, permet de trier les listes sur ce critère
		/// </summary>
		/// <value>The order count.</value>
		public int OrderCount { get; set; }
		/// <summary>
		/// Indique si le produit à du stock
		/// </summary>
		/// <value>The has stock.</value>
		public bool HasStock { get; set; }
		/// <summary>
		/// Date d'expiration de la promotion
		/// </summary>
		/// <value>The promotion expiration date, cette valeur peut etre nulle</value>
		public DateTime? PromotionExpirationDate { get; set; }

		/// <summary>
		/// Quantité minimale de vente
		/// </summary>
		/// <value>The minimum sale quantity.</value>
		public int MinimumSaleQuantity { get; set; }

		/// <summary>
		/// Image par defaut
		/// </summary>
		/// <value>The default image., s'il n'y a pas d'image, cette valeur est nulle</value>
		public Media DefaultImage { get; set; }
		/// <summary>
		/// Retourne la valeur en % de la promotion
		/// </summary>
		/// <value>The promotionnal discount.</value>
		public double PromotionnalDiscount
		{
			get
			{
				if (PromotionalPrice != null && PromotionalPrice.Value > 0)
				{
					return 1 - (Convert.ToDouble(PromotionalPrice.Value) / Convert.ToDouble(SalePrice.Value));
				}
				return 0;
			}
		}

		/// <summary>
		/// Remise client
		/// </summary>
		/// <value>The customer discount.</value>
		public double CustomerDiscount
		{
			get
			{
				if (CustomerPrice != null && CustomerPrice.Value > 0)
				{
					return 1 - (Convert.ToDouble(CustomerPrice.Value) / Convert.ToDouble(SalePrice.Value));
				}
				return 0;
			}
		}

		/// <summary>
		/// Indique qu'un produit possède une image par defaut
		/// </summary>
		/// <value>
		/// 	<c>true</c> if this instance has picture; otherwise, <c>false</c>.
		/// </value>
		public bool HasPicture 
		{
			get
			{
				return DefaultImage != null;
			}
		}

		/// <summary>
		/// Liste des propriétés etendues 
		/// </summary>
		/// <value>The extended properties.</value>
		public ExtendedPropertyList ExtendedProperties { get; set; }

		/// <summary>
		/// Unité du produit (metre, kilogramme, temps, unité)
		/// </summary>
		/// <value>The metric.</value>
		public Metric Metric { get; set; }

		/// <summary>
		/// Liste des propriétés de type string traduites
		/// </summary>
		/// <value>The localization list.</value>
		public IEnumerable<EntityLocalization> LocalizationList { get; set; }

		/// <summary>
		/// Dernière modification sur le produit
		/// </summary>
		/// <value>The last update.</value>
		public DateTime LastUpdate { get; set; }

		/// <summary>
		/// Paramètre de calcul des frais de port
		/// </summary>
		/// <value>The transport.</value>
		public TransportSettings Transport { get; private set; }

		/// <summary>
		/// Mode de vente du produit
		/// </summary>
		/// <value>The sale mode.</value>
		public ProductSaleMode SaleMode { get; set; }

		/// <summary>
		/// Liste de prix de vente en fonction d'interval de quantité
		/// </summary>
		/// <value>The price by quantity list.</value>
		public List<PriceByQuantity> PriceByQuantityList { get; private set; }

		/// <summary>
		/// Niveau de stockage du produit
		/// </summary>
		/// <value>The storage rank.</value>
		public ProductStorageRank? StorageRank { get; set; }

		/// <summary>
		/// Chiffre de 0 à X indiquant le caractère du produit
		/// en fonction de s'il est nouveau, en promo, destockage ect...
		/// </summary>
		/// <value>The character.</value>
		public string Character 
		{
			get
			{
				if (this.IsCustomerPrice)
				{
					return "6";
				}
				if (this.IsPromotion)
				{
					return "1";
				}
				else if (this.IsTopSell)
				{
					return "2";
				}
				else if (this.IsDestock)
				{
					return "3";
				}
				else if (this.IsFirstPrice)
				{
					return "4";
				}
				else if (this.IsNew)
				{
					return "5";
				}
				return "0";
			}
		}

		/// <summary>
		/// Designe la garantie du produit, le texte est libre
		/// </summary>
		/// <value>The warranty.</value>
		public string Warranty { get; set; }

		#region Prices

		/// <summary>
		/// Prix de destockage
		/// </summary>
		/// <value>The destock price.</value>
		public Price DestockPrice { get; set; }
		/// <summary>
		/// Prix de vente du produit
		/// </summary>
		/// <value>The sale price.</value>
		public Price SalePrice { get; set; }
		/// <summary>
		/// Prix de vente promotionnel du produit, est toujours inférieur au prix de vente
		/// </summary>
		/// <value>The promotional price.</value>
		public Price PromotionalPrice { get; set; }
		/// <summary>
		/// Prix du marché constaté, est toujours supérieur au prix de vente
		/// </summary>
		/// <value>The market price.</value>
		public Price MarketPrice { get; set; }
		/// <summary>
		/// Montant de l'eco taxe
		/// </summary>
		/// <value>The recycle price.</value>
		public Price RecyclePrice { get; set; }
		/// <summary>
		/// Tarif appliqué au client
		/// </summary>
		/// <value>The customer price.</value>
		public Price CustomerPrice { get; set; }

		/// <summary>
		/// Meilleur prix pratiqué (le plus bas)
		/// </summary>
		/// <value>The best price.</value>
		public Price BestPrice { get; set; }

		public Price StrikedPrice
		{
			get
			{
				if (SelectedPrice == PriceType.Destock)
				{
					if (DestockPrice != null
						&& DestockPrice.Value < SalePrice.Value)
					{
						return SalePrice;
					}
				}
				if (SelectedPrice == PriceType.Promotional)
				{
					if (PromotionalPrice != null
						&& PromotionalPrice.Value < SalePrice.Value)
					{
						return PromotionalPrice;
					}
				}
				if (SelectedPrice == PriceType.Customer)
				{
					if (CustomerPrice != null
						&& CustomerPrice.Value < SalePrice.Value)
					{
						return CustomerPrice;
					}
				}
				return null;
			}
		}

		/// <summary>
		/// Type de prix selectionné pour avoir le meilleur prix
		/// </summary>
		/// <value>The selected price.</value>
		public PriceType SelectedPrice { get; set; }

		/// <summary>
		/// Type de prix par defaut
		/// </summary>
		/// <value>The selected price.</value>
		public PriceType DefaultPriceType { get; set; }

		#endregion
	}
}
