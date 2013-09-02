using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.ServiceModel;

namespace ERPStore.Services
{
	/// <summary>
	/// Service d'interrogation et manipulation du catalogue
	/// 
	/// Permet d'obtenir la liste des produits ou un produit donné
	/// Permet d'obtenir la liste des categories ou une categorie donnée
	/// Permet d'obtenir la liste des marques ou marque donnée
	/// 
	/// </summary>
	[ServiceContract(Name = "CatalogService"
	, Namespace = "http://www.erpstore.net/2010/05/20")]
	public interface ICatalogService
	{
		string Name { get; }

		#region Products

		/// <summary>
		/// Retourne un produit via son id
		/// </summary>
		/// <param name="productId">The product id.</param>
		/// <returns></returns>
		[OperationContract]
		Models.Product GetProductById(int productId);
		/// <summary>
		/// Retourne un produit via son code
		/// </summary>
		/// <param name="productCode">The product code.</param>
		/// <returns></returns>
		[OperationContract]
		Models.Product GetProductByCode(string productCode);
		/// <summary>
		/// Retourne le produit phare d'une categorie
		/// </summary>
		/// <param name="productCategoryId">The product category id.</param>
		/// <returns></returns>
		[OperationContract]
		Models.Product GetHeadProductOfCategory(int productCategoryId);
		/// <summary>
		/// Retourne la liste des produits de manière paginée
		/// </summary>
		/// <param name="index">The index.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		IList<Models.Product> GetProductList(int index, int pageSize, out int count);

		/// <summary>
		/// Gets the product list by search.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="index">The index.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.Product> GetProductListBySearch(Models.ProductListFilter filter, int index, int pageSize, out int count);

		/// <summary>
		/// Gets the product list by customer.
		/// </summary>
		/// <param name="filter">The filter.</param>
		/// <param name="index">The index.</param>
		/// <param name="pageSize">Size of the page.</param>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		IList<Models.Product> GetProductListByCustomer(Models.ProductListFilter filter, int index, int pageSize, out int count);

		/// <summary>
		/// Retourne les groupes de propriété et les valeurs distinctes des produits recherchés
		/// </summary>
		/// <param name="searchFilter">The search filter.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.PropertyGroup> GetProductExtendedPropertyListBySearch(ERPStore.Models.ProductListFilter searchFilter);

		/// <summary>
		/// Retourne les groupes de propriété et les valeurs distinctes des produits pour une categorie donnée
		/// </summary>
		/// <param name="categoryId">The category id.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.PropertyGroup> GetProductExtendedPropertyListByCategoryId(int categoryId);

		/// <summary>
		/// Gets the cross selling list.
		/// </summary>
		/// <param name="productId">The product id.</param>
		/// <returns></returns>
		[OperationContract]
		IList<ERPStore.Models.Product> GetCrossSellingList(int productId);

		/// <summary>
		/// Retourne une liste de produits interessants liés à un panier
		/// pour un client donné
		/// </summary>
		/// <param name="productId">The product id.</param>
		/// <returns></returns>
		[OperationContract]
		IList<ERPStore.Models.Product> GetCrossSellingList(Models.OrderCart cart);

		/// <summary>
		/// Retourne les groupes de propriété et les valeurs distinctes des produits pour une marque donnée
		/// </summary>
		/// <param name="brandId">The brand id.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.PropertyGroup> GetProductExtendedPropertyListByBrandId(int brandId);

		/// <summary>
		/// Gets the product relations.
		/// </summary>
		/// <param name="productId">The product id.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.ProductRelation> GetProductRelations(int productId);

		/// <summary>
		/// Gets the product list by id list.
		/// </summary>
		/// <param name="productIdList">The product id list.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.Product> GetProductListByIdList(IEnumerable<int> productIdList);

		/// <summary>
		/// Retourne une liste de disponibilité stock pour une liste d'id de produit
		/// </summary>
		/// <param name="productIdList">The product id list.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.ProductStockInfo> GetProductStockInfoList(IEnumerable<int> productIdList);

		/// <summary>
		/// Retourne les informations sur le stock d'un produit donnée
		/// </summary>
		/// <param name="product">The product.</param>
		/// <returns></returns>
		[OperationContract(Name="GetProductStockInfoByProduct")]
		Models.ProductStockInfo GetProductStockInfo(Models.Product product);

		/// <summary>
		/// Retourne les informations sur le stock d'un produit donnée
		/// en fonction de son code
		/// </summary>
		/// <param name="productCode">The product code.</param>
		/// <returns></returns>
		[OperationContract(Name="GetProductStockInfoByProductCode")]
		Models.ProductStockInfo GetProductStockInfo(string productCode);

		/// <summary>
		/// Retourne la liste des groupes de propriétés etendus 
		/// pour les produits
		/// </summary>
		/// <returns>La liste des groups de propriété etendus</returns>
		[OperationContract]
		IList<Models.PropertyGroup> GetProductPropertyGroupList();

		/// <summary>
		/// Indique si une selection contient un produit
		/// </summary>
		/// <param name="selectionId">The selection id.</param>
		/// <param name="productId">The product id.</param>
		/// <returns></returns>
		bool SelectionContainsProduct(int selectionId, int productId);

		#endregion

		#region Category

		/// <summary>
		/// Gets the categories.
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		IList<Models.ProductCategory> GetCategories();
		/// <summary>
		/// Gets the forefront categories.
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		IList<Models.ProductCategory> GetForefrontCategories();
		/// <summary>
		/// Gets the category by code.
		/// </summary>
		/// <param name="categoryCode">The category code.</param>
		/// <returns></returns>
		[OperationContract]
		Models.ProductCategory GetCategoryByCode(string categoryCode);
		/// <summary>
		/// Gets the category by link.
		/// </summary>
		/// <param name="link">The link.</param>
		/// <returns></returns>
		[OperationContract]
		Models.ProductCategory GetCategoryByLink(string link);
		/// <summary>
		/// Gets the category by id.
		/// </summary>
		/// <param name="categoryId">The category id.</param>
		/// <returns></returns>
		[OperationContract]
		Models.ProductCategory GetCategoryById(int categoryId);
		/// <summary>
		/// Rechargement de la liste des categories
		/// </summary>
		/// <remarks>
		/// Cette methode est appelée si une modification a lieu sur une des categories
		/// via ERP360 ou tout autre client
		/// </remarks>
		[OperationContract]
		void ReloadCategories();

		/// <summary>
		/// Retourne la liste des marques pour une catégorie donnée
		/// </summary>
		/// <param name="brandId">The brand id.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.ProductCategory> GetProductCategoryListByBrandId(int brandId);

		/// <summary>
		/// Retourne la liste des categories contenues dans une recherche de produit
		/// </summary>
		/// <param name="search">The search.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.ProductCategory> GetProductCategoryListBySearch(Models.ProductListFilter search);

		#endregion

		#region Brands

		/// <summary>
		/// Gets the brands.
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		IList<Models.Brand> GetBrands();
		/// <summary>
		/// Gets the brand by id.
		/// </summary>
		/// <param name="brandId">The brand id.</param>
		/// <returns></returns>
		[OperationContract]
		Models.Brand GetBrandById(int? brandId);
		/// <summary>
		/// Retourne une marque en fonction de son code
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		[OperationContract]
		Models.Brand GetBrandByName(string name);
		/// <summary>
		/// Gets the name of the brand by.
		/// </summary>
		/// <param name="link">The link.</param>
		/// <returns></returns>
		[OperationContract]
		Models.Brand GetBrandByLink(string link);
		/// <summary>
		/// Retourne la liste des marques pour la page d'accueil
		/// </summary>
		/// <remarks>
		/// Cette liste doit etre limitée a 10 items
		/// </remarks>
		/// <returns>La liste des marques</returns>
		[OperationContract]
		IList<Models.Brand> GetBrandListForefront();
		/// <summary>
		/// Rechargement de la liste des marques
		/// </summary>
		/// <remarks>
		/// Cette methode est appelée si une modification a lieu sur une des marques
		/// via ERP360 ou tout autre client
		/// </remarks>
		[OperationContract]
		void ReloadBrands();

		/// <summary>
		/// Retourne la liste des marques pour une categorie de produit donné
		/// </summary>
		/// <param name="productCategoryId">The product category id.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.Brand> GetBrandListByProductCategoryId(int productCategoryId);

		/// <summary>
		/// Retourne la liste des marques contenues dans une recherche de produit
		/// </summary>
		/// <param name="search">The search.</param>
		/// <returns></returns>
		[OperationContract]
		IList<Models.Brand> GetBrandListBySearch(Models.ProductListFilter search);

		#endregion

		#region Misc

		/// <summary>
		/// Applique le tarif client à une liste de produit
		/// </summary>
		/// <param name="productList">The product list.</param>
		/// <param name="user">The user.</param>
		[OperationContract(Name = "ApplyBestPriceByProductListByUser")]
		void ApplyBestPrice(IList<Models.Product> productList, Models.User user);

		/// <summary>
		/// Applique le tarif client à un produit
		/// </summary>
		/// <param name="product">The product.</param>
		/// <param name="user">The user.</param>
		[OperationContract(Name="ApplyBestPriceByProductByUser")]
		void ApplyBestPrice(Models.Product product, Models.User user);

		/// <summary>
		/// Supprime tous les paramètre n'apparaissants pas comme filtrés
		/// </summary>
		/// <param name="parameters">The parameters.</param>
		/// <returns></returns>
		[OperationContract]
		NameValueCollection RemoveNotFilteredParameters(NameValueCollection parameters);

		/// <summary>
		/// Retourne la liste des professions
		/// ce parmètre permet d'afficher les produits
		/// par ordre préférentiel pour le metier choisi par
		/// l'internaute
		/// </summary>
		/// <returns></returns>
		[OperationContract]
		IEnumerable<Models.Profession> GetProfessionList();

		/// <summary>
		/// Retourne la liste des termes de recherche les plus utilisés
		/// </summary>
		/// <param name="count">The count.</param>
		/// <returns></returns>
		[OperationContract]
		IEnumerable<Models.SearchTerm> GetTopSearchTermList(int count);

		/// <summary>
		/// Creation d'un filtre pour l'affichage d'une liste de produit.
		/// </summary>
		/// <param name="context">The context.</param>
		/// <returns></returns>
		[OperationContract]
		Models.ProductListFilter CreateProductListFilter(System.Web.HttpContextBase context);

		/// <summary>
		/// Retourne la liste des mots clés d'un produit
		/// utilisé principalement pour la génération du catalogue fulltext
		/// </summary>
		/// <param name="Product">The product.</param>
		/// <returns></returns>
		[OperationContract]
		string GetProductKeywords(ERPStore.Models.Product Product);

		/// <summary>
		/// Liste des selection de produit.
		/// </summary>
		/// <returns></returns>
		IList<Models.EntitySelection> GetProductSelectionList();

		#endregion
	}
}
