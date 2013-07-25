using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ERPStore.Models
{
	/// <summary>
	/// Genre d'une personne
	/// </summary>
	public enum UserPresentation
	{
		/// <summary>
		/// Inconnu
		/// </summary>
		Unkwnon = 0,
		/// <summary>
		/// Monsieur
		/// </summary>
		Mister = 1,
		/// <summary>
		/// Madame
		/// </summary>
		Miss = 2,
		/// <summary>
		/// Mademoiselle
		/// </summary>
		Misses = 3,
	}

	/// <summary>
	/// Les modes de reglement
	/// </summary>
	[Obsolete("", true)]
	public enum PaymentMode
	{
		/// <summary>
		/// Paypal
		/// </summary>
		Paypal = 1,
		/// <summary>
		/// Cheque
		/// </summary>
		Check = 3,
		/// <summary>
		/// Virement bancaire
		/// </summary>
		WireTransfer = 4,
		/// <summary>
		/// En compte
		/// </summary>
		InAccount = 5,
		/// <summary>
		/// Systeme de payment Ogone
		/// </summary>
		Ogone = 6,
		/// <summary>
		/// Systeme de payement Sogenactif
		/// </summary>
		Sogenactif = 7,
	}

	/// <summary>
	/// Statut de verficiation d'un email
	/// </summary>
	public enum EmailVerificationStatus
	{
		/// <summary>
		/// Non verifié
		/// </summary>
		NotVerified = -1,
		/// <summary>
		/// En cours de verification
		/// </summary>
		Pending = -2,
		/// <summary>
		/// Confirmé
		/// </summary>
		UserConfirmed = -3
	}

	/// <summary>
	/// Liste des types d'affichage
	/// </summary>
	public enum ProductListType
	{
		/// <summary>
		/// Tous les produits
		/// </summary>
		All,
		/// <summary>
		/// Les produits en promotion
		/// </summary>
		Promotional,
		/// <summary>
		/// Les nouveaux produits
		/// </summary>
		New,
		/// <summary>
		/// Les produits en destockage
		/// </summary>
		Destock,
		/// <summary>
		/// Les produits les plus vendus
		/// </summary>
		TopSell,
		/// <summary>
		/// Les produits avec le prix le plus bas du marché
		/// </summary>
		FirstPrice,
		/// <summary>
		/// Affiché sur la page d'accueil 
		/// </summary>
		Front,
		/// <summary>
		/// Liste du client
		/// </summary>
		Customer,
		/// <summary>
		/// Résultat de recherche
		/// </summary>
		Search,
		/// <summary>
		/// Liste par categorie de produit
		/// </summary>
		Category,
		/// <summary>
		/// Par marque
		/// </summary>
		Brand,
	}

	///// <summary>
	///// Liste des types d'affichage des produits par categorie
	///// </summary>
	//public enum ProductListByCategoryType
	//{
	//    /// <summary>
	//    /// 
	//    /// </summary>
	//    Promotional,
	//    /// <summary>
	//    /// 
	//    /// </summary>
	//    New,
	//    /// <summary>
	//    /// 
	//    /// </summary>
	//    Destock,
	//    /// <summary>
	//    /// 
	//    /// </summary>
	//    TopSell,
	//    /// <summary>
	//    /// 
	//    /// </summary>
	//    FirstPrice,
	//    /// <summary>
	//    /// 
	//    /// </summary>
	//    OurSelection,
	//    /// <summary>
	//    /// 
	//    /// </summary>
	//    BlowOfHeart,
	//}

	//public enum BrandListType
	//{
	//    TopSell,
	//}

	/// <summary>
	/// Type de relation entre les produits
	/// </summary>
	public enum ProductRelationType
	{
		/// <summary>
		/// Toutes les relations possibles
		/// </summary>
		All,
		/// <summary>
		/// Similaires
		/// </summary>
		Similar,
		/// <summary>
		/// Complementaires
		/// </summary>
		Complementary,
		/// <summary>
		/// Variation
		/// </summary>
		Variant,
		/// <summary>
		/// Substitution
		/// </summary>
		Substitute,
	}

	/// <summary>
	/// Type de commande
	/// </summary>
	public enum SaleDocumentType
	{
		/// <summary>
		/// Commande
		/// </summary>
		Order,
		/// <summary>
		/// Devis
		/// </summary>
		Quote,
		/// <summary>
		/// Bon de livraison
		/// </summary>
		Delivery,
		/// <summary>
		/// Facture
		/// </summary>
		Invoice,
	}

	/// <summary>
	/// Statuts de la commande
	/// </summary>
	public enum OrderStatus
	{
		/// <summary>
		/// En attente de reglement
		/// </summary>
		WaitingPayment,
		/// <summary>
		/// Paiment accepté
		/// </summary>
		PaymentAccepted,
		/// <summary>
		/// Livraison partielle
		/// </summary>
		PartialyDelivered,
		/// <summary>
		/// Completement livrée
		/// </summary>
		Delivered,
		/// <summary>
		/// Facturée
		/// </summary>
		Invoiced,
		/// <summary>
		/// En attente de traitement
		/// </summary>
		WaitingProcess,
	}

	/// <summary>
	/// Liste des etats d'une facture
	/// </summary>
	public enum InvoiceStatus
	{
		/// <summary>
		/// Non recouverte
		/// </summary>
		Unrecovered = -1,
		/// <summary>
		/// Partiellement recouverte
		/// </summary>
		PartiallyRecovered = -2,
		/// <summary>
		/// Totalement reglée
		/// </summary>
		FullyRecovered = -3,
		/// <summary>
		/// Partiellement perdue
		/// </summary>
		PartiallyLost = -4,
		/// <summary>
		/// Totalement perdue
		/// </summary>
		FullyLost = -5,
		/// <summary>
		/// En cours de traitement
		/// </summary>
		Processing = -6, 
	}

	/// <summary>
	/// Type de Facture
	/// </summary>
	public enum InvoiceType
	{
		/// <summary>
		/// Facture comptant
		/// </summary>
		CashInvoice,
		/// <summary>
		/// Facture pour une société
		/// </summary>
		CorporateInvoice,
		/// <summary>
		/// Avoir
		/// </summary>
		CreditNote,
	}

	/// <summary>
	/// Raison du classement d'un devis
	/// </summary>
	public enum CancelQuoteReason
	{
		/// <summary>
		/// Autre
		/// </summary>
		Other,
		/// <summary>
		/// Trop cher
		/// </summary>
		TooExpensive,
		/// <summary>
		/// Délai trop longs
		/// </summary>
		TooLong,
		/// <summary>
		/// Projet non concrétisé
		/// </summary>
		ProjectNotConcretized,
		/// <summary>
		/// Marque non respectée
		/// </summary>
		TradmarkNotRespected,
		/// <summary>
		/// Quantité disponible insuffisante
		/// </summary>
		AvailableQuantityInsufficient,
	}

	/// <summary>
	/// Période de planification des tâches
	/// </summary>
	public enum ScheduledTaskTimePeriod
	{
		/// <summary>
		/// Mois
		/// </summary>
		Month,
		/// <summary>
		/// Jour
		/// </summary>
		Day,
		/// <summary>
		/// Jour ouvré
		/// </summary>
		WorkingDay,
		/// <summary>
		/// Heure
		/// </summary>
		Hour,
		/// <summary>
		/// Minute
		/// </summary>
		Minute,
		/// <summary>
		/// Executé une seule fois
		/// </summary>
		Once,
	}

	/// <summary>
	/// Type de panier
	/// </summary>
	public enum CartType
	{
		/// <summary>
		/// Panier de type commande
		/// </summary>
		Order,
		/// <summary>
		/// Panier de type devis
		/// </summary>
		Quote
	}

	/// <summary>
	/// Mode de calcul des frais de port par produit
	/// </summary>
	public enum FeeTransportStrategy
	{
		/// <summary>
		/// Tarif fixe
		/// </summary>
		FixedPrice = -2,
		/// <summary>
		/// Par niveau de transportabilité
		/// </summary>
		ByLevel = -1,
		/// <summary>
		/// Pas de frais
		/// </summary>
		Franco = -3,
	}

	/// <summary>
	/// Liste des mode de vente d'un produit
	/// </summary>
	public enum ProductSaleMode
	{
		/// <summary>
		/// Vendable
		/// </summary>
		Sellable,

		/// <summary>
		/// Fin de vie, est vendu jusqu'a epuisement des stocks
		/// </summary>
		EndOfLife,

		/// <summary>
		/// N'est pas vendable, ne pas presenter le bouton d'ajout au panier
		/// </summary>
		NotSellable,
	}


    /// <summary>
    /// Liste des etats d'un devis
    /// </summary>
    public enum QuoteStatus
    {
        /// <summary>
        /// En attente de conversion
        /// </summary>
    	Waiting = -1,
        /// <summary>
        /// Converti en commande
        /// </summary>
		ConvertedToOrder=-3,
        /// <summary>
        /// Trop cher
        /// </summary>
		TooExpensive=-4,
        /// <summary>
        /// Trop long
        /// </summary>
		TooLong = -5,
        /// <summary>
        /// Pas de reponse avant délai expiré
        /// </summary>
		NoReply = -6,
        /// <summary>
        /// Quantité en stock insuffisante
        /// </summary>
		AvailableQuantityInsufficient = -10,
        /// <summary>
        /// Refus partiel
        /// </summary>
		PartialDenied = -11,
        /// <summary>
        /// Marque non respectée
        /// </summary>
		TrademarkNotRespected = -12,
        /// <summary>
        /// Projet non concrétisé
        /// </summary>
		ProjectNotConcretized = -13,
        /// <summary>
        /// 
        /// </summary>
		MarketPriceProposal = -14,
        /// <summary>
        /// Pour offre
        /// </summary>
		ForFiguring = -15,
        /// <summary>
        /// Affaire perdue par le client
        /// </summary>
		LostByCustomer = -16,
        /// <summary>
        /// Délai trop long indiqué
        /// </summary>
		TimeTooLong = -18,
        /// <summary>
        /// Annulée par le client en ligne
        /// </summary>
		CanceledByUser = -19,
		/// <summary>
		/// Conversion manuelle
		/// </summary>
		ManualyConvertedToOrder = -17,
		/// <summary>
		/// En attente de reglement
		/// </summary>
		WaitingPayement = -20,
    }

    /// <summary>
    /// Liste des états d'un utilisateur
    /// </summary>
    public enum UserState
    {
        /// <summary>
        /// Profil incomplet
        /// </summary>
        Uncompleted = -1,
        /// <summary>
        /// Profil complet
        /// </summary>
        Completed = -2,
        /// <summary>
        /// Profil confirmé
        /// </summary>
        Confirmed = -3,
    }

	/// <summary>
	/// Niveau de stockage des produits
	/// </summary>
	public enum ProductStorageRank
	{
		/// <summary>
		/// Très utilisé
		/// </summary>
		A,
		/// <summary>
		/// Occasionnelement utilisé
		/// </summary>
		B,
		/// <summary>
		/// Rarement utilisé
		/// </summary>
		C,
	}

	/// <summary>
	/// Types de coupons
	/// </summary>
	public enum CouponType
	{
		/// <summary>
		/// Code du vendeur , dans le cadre d'une affiliation
		/// </summary>
		VendorCode,
		/// <summary>
		/// Pourcentage de la commande
		/// </summary>
		PercentOfOrder,
		/// <summary>
		/// Montant fixe sur la commande à retirer
		/// </summary>
		AmountOfOrder,
		/// <summary>
		/// Frais de port gratuits
		/// </summary>
		FreeTransport,

		/// <summary>
		/// Remise si un produit est present dans la liste des
		/// produits autorisés
		/// </summary>
		PercentOfProduct,

		/// <summary>
		/// Remise si un produit est present dans la liste des
		/// produits autorisés
		/// </summary>
		AmoutOfProduct,

		/// <summary>
		/// Remise sur tous les produits d'une categorie
		/// </summary>
		PercentOfProductCategory,

		/// <summary>
		/// Montant deduit sur tous les produits d'une categorie
		/// </summary>
		AmountOfProductCategory,

		/// <summary>
		/// Remise sur tous les produits d'une marque
		/// </summary>
		PercentOfBrand,

		/// <summary>
		/// Montant deduit sur tous les produits d'une marque
		/// </summary>
		AmountOfBrand,
	}

	/// <summary>
	/// Type du prix pratiqué pour un produit
	/// </summary>
	public enum PriceType
	{
		/// <summary>
		/// Tarif normal
		/// </summary>
		Normal,
		/// <summary>
		/// Tarif promotionnel
		/// </summary>
		Promotional,
		/// <summary>
		/// Tarif de destockage
		/// </summary>
		Destock,
		/// <summary>
		/// Tarif client
		/// </summary>
		Customer,
		/// <summary>
		/// Remise client pour la categorie de produit
		/// </summary>
		CustomerDiscountByCategory,
		/// <summary>
		/// Remise client négociée pour le produit
		/// </summary>
		CustomerDiscountByProduct,
		/// <summary>
		/// Remise par quantité de produit
		/// </summary>
		DiscountByQuantity,
		/// <summary>
		/// Remise via un coupon
		/// </summary>
		Coupon,
		/// <summary>
		/// Gratuit
		/// </summary>
		Free,
		/// <summary>
		/// Tarif catalogue, négociable par devis
		/// </summary>
		Catalog,
	}

	/// <summary>
	/// Type de contenu pour la traduction
	/// </summary>
	public enum LocalizedContentType
	{
		/// <summary>
		/// Texte libre
		/// </summary>
		Label,
		/// <summary>
		/// Texte dans un attribut
		/// </summary>
		Attribute,
		/// <summary>
		/// URL source d'une image
		/// </summary>
		ImageSource,
	}

	/// <summary>
	/// Type de livraison pour un détail de panier
	/// </summary>
	public enum ShippingType
	{
		/// <summary>
		/// Dès que le produit est disponible
		/// </summary>
		WhenAvailable,
		/// <summary>
		/// Au delai fixe indiqué par le client
		/// </summary>
		FixedDelay,
		/// <summary>
		/// Dernier délai
		/// </summary>
		Deadline,
	}

}