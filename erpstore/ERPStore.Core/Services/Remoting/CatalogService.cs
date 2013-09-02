using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using System.Web.Mvc;

namespace ERPStore.Services.Remoting
{
	public class CatalogService : ICatalogService
	{
		private ICatalogService m_ContreteCatalogService;

		public CatalogService(IDependencyResolver container)
		{
			m_ContreteCatalogService = container.GetService<ICatalogService>();
		}

		public CatalogService()
            : this(DependencyResolver.Current)
		{

		}

		#region ICatalogService Members

		public string Name
		{
			get { return m_ContreteCatalogService.Name; }
		}

		public ERPStore.Models.Product GetProductById(int productId)
		{
			return m_ContreteCatalogService.GetProductById(productId);
		}

		public ERPStore.Models.Product GetProductByCode(string productCode)
		{
			return m_ContreteCatalogService.GetProductByCode(productCode);
		}

		public ERPStore.Models.Product GetHeadProductOfCategory(int productCategoryId)
		{
			return m_ContreteCatalogService.GetHeadProductOfCategory(productCategoryId);
		}

		public IList<ERPStore.Models.Product> GetProductList(int index, int pageSize, out int count)
		{
			return m_ContreteCatalogService.GetProductList(index, pageSize, out count);
		}

		//public IList<ERPStore.Models.Product> GetDestockedProductList()
		//{
		//    return m_ContreteCatalogService.GetDestockedProductList();
		//}

		//public IList<ERPStore.Models.Product> GetTopSellProductList()
		//{
		//    return m_ContreteCatalogService.GetTopSellProductList();
		//}

		//public IList<ERPStore.Models.Product> GetFirstPriceProductList()
		//{
		//    return m_ContreteCatalogService.GetFirstPriceProductList();
		//}

		public IList<ERPStore.Models.Product> GetProductListBySearch(ERPStore.Models.ProductListFilter filter, int index, int pageSize, out int count)
		{
			return m_ContreteCatalogService.GetProductListBySearch(filter, index, pageSize, out count);
		}

		public IList<ERPStore.Models.Product> GetProductListByCustomer(ERPStore.Models.ProductListFilter filter, int index, int pageSize, out int count)
		{
			return m_ContreteCatalogService.GetProductListByCustomer(filter, index, pageSize, out count);
		}

		public IList<ERPStore.Models.PropertyGroup> GetProductExtendedPropertyListBySearch(ERPStore.Models.ProductListFilter searchFilter)
		{
			return m_ContreteCatalogService.GetProductExtendedPropertyListBySearch(searchFilter);
		}

		public IList<ERPStore.Models.Product> GetProductListByCategoryId(int categoryId, System.Collections.Specialized.NameValueCollection parameters, int index, int pageSize, out int count)
		{
			count = 0;
			return null; // m_ContreteCatalogService.GetProductListByCategoryId(categoryId, parameters, index, pageSize, out count);
		}

		public IList<ERPStore.Models.PropertyGroup> GetProductExtendedPropertyListByCategoryId(int categoryId)
		{
			return m_ContreteCatalogService.GetProductExtendedPropertyListByCategoryId(categoryId);
		}

		public IList<ERPStore.Models.Product> GetCrossSellingList(int productId)
		{
			return m_ContreteCatalogService.GetCrossSellingList(productId);
		}

		public IList<ERPStore.Models.Product> GetCrossSellingList(ERPStore.Models.OrderCart cart)
		{
			return m_ContreteCatalogService.GetCrossSellingList(cart);
		}

		public IList<ERPStore.Models.Product> GetProductListByBrandId(int brandId, System.Collections.Specialized.NameValueCollection parameters, int index, int pageSize, out int count)
		{
			count = 0;
			return null; // m_ContreteCatalogService.GetProductListByBrandId(brandId, parameters, index, pageSize, out count);
		}

		public IList<ERPStore.Models.PropertyGroup> GetProductExtendedPropertyListByBrandId(int brandId)
		{
			return m_ContreteCatalogService.GetProductExtendedPropertyListByBrandId(brandId);
		}

		public IList<ERPStore.Models.ProductRelation> GetProductRelations(int productId)
		{
			return m_ContreteCatalogService.GetProductRelations(productId);
		}

		public IList<ERPStore.Models.Product> GetProductListByIdList(IEnumerable<int> productIdList)
		{
			return m_ContreteCatalogService.GetProductListByIdList(productIdList);
		}

		public IList<ERPStore.Models.ProductStockInfo> GetProductStockInfoList(IEnumerable<int> productIdList)
		{
			return m_ContreteCatalogService.GetProductStockInfoList(productIdList);
		}

		public ERPStore.Models.ProductStockInfo GetProductStockInfo(ERPStore.Models.Product product)
		{
			return m_ContreteCatalogService.GetProductStockInfo(product);
		}

		public ERPStore.Models.ProductStockInfo GetProductStockInfo(string productCode)
		{
			return m_ContreteCatalogService.GetProductStockInfo(productCode);
		}

		public IList<ERPStore.Models.PropertyGroup> GetProductPropertyGroupList()
		{
			return m_ContreteCatalogService.GetProductPropertyGroupList();
		}

		public IList<ERPStore.Models.ProductCategory> GetCategories()
		{
			return m_ContreteCatalogService.GetCategories();
		}

		public IList<ERPStore.Models.ProductCategory> GetForefrontCategories()
		{
			return m_ContreteCatalogService.GetForefrontCategories();
		}

		public ERPStore.Models.ProductCategory GetCategoryByCode(string categoryCode)
		{
			return m_ContreteCatalogService.GetCategoryByCode(categoryCode);
		}

		public ERPStore.Models.ProductCategory GetCategoryByLink(string link)
		{
			return m_ContreteCatalogService.GetCategoryByLink(link);
		}

		public ERPStore.Models.ProductCategory GetCategoryById(int categoryId)
		{
			return m_ContreteCatalogService.GetCategoryById(categoryId);
		}

		public void ReloadCategories()
		{
			m_ContreteCatalogService.ReloadCategories();
		}

		public IList<ERPStore.Models.ProductCategory> GetProductCategoryListByBrandId(int brandId)
		{
			return m_ContreteCatalogService.GetProductCategoryListByBrandId(brandId);
		}

		public IList<ERPStore.Models.ProductCategory> GetProductCategoryListBySearch(ERPStore.Models.ProductListFilter search)
		{
			return m_ContreteCatalogService.GetProductCategoryListBySearch(search);
		}

		public IList<ERPStore.Models.Brand> GetBrands()
		{
			return m_ContreteCatalogService.GetBrands();
		}

		public ERPStore.Models.Brand GetBrandById(int? brandId)
		{
			return m_ContreteCatalogService.GetBrandById(brandId);
		}

		public ERPStore.Models.Brand GetBrandByName(string name)
		{
			return m_ContreteCatalogService.GetBrandByName(name);
		}

		public ERPStore.Models.Brand GetBrandByLink(string link)
		{
			return m_ContreteCatalogService.GetBrandByLink(link);
		}

		public IList<ERPStore.Models.Brand> GetBrandListForefront()
		{
			return m_ContreteCatalogService.GetBrandListForefront();
		}

		public void ReloadBrands()
		{
			m_ContreteCatalogService.ReloadBrands();
		}

		public IList<ERPStore.Models.Brand> GetBrandListByProductCategoryId(int productCategoryId)
		{
			return m_ContreteCatalogService.GetBrandListByProductCategoryId(productCategoryId);
		}

		public IList<ERPStore.Models.Brand> GetBrandListBySearch(ERPStore.Models.ProductListFilter search)
		{
			return m_ContreteCatalogService.GetBrandListBySearch(search);
		}

		public void ApplyBestPrice(IList<ERPStore.Models.Product> productList, ERPStore.Models.User user)
		{
			m_ContreteCatalogService.ApplyBestPrice(productList, user);
		}

		public void ApplyBestPrice(ERPStore.Models.Product product, ERPStore.Models.User user)
		{
			m_ContreteCatalogService.ApplyBestPrice(product, user);
		}

		public System.Collections.Specialized.NameValueCollection RemoveNotFilteredParameters(System.Collections.Specialized.NameValueCollection parameters)
		{
			return m_ContreteCatalogService.RemoveNotFilteredParameters(parameters);
		}

		public IEnumerable<ERPStore.Models.Profession> GetProfessionList()
		{
			return m_ContreteCatalogService.GetProfessionList();
		}

		public IEnumerable<ERPStore.Models.SearchTerm> GetTopSearchTermList(int count)
		{
			return m_ContreteCatalogService.GetTopSearchTermList(count);
		}

		public ERPStore.Models.ProductListFilter CreateProductListFilter(System.Web.HttpContextBase context)
		{
			return m_ContreteCatalogService.CreateProductListFilter(context);
		}

		public string GetProductKeywords(ERPStore.Models.Product product)
		{
			return m_ContreteCatalogService.GetProductKeywords(product);
		}

		public IList<ERPStore.Models.EntitySelection> GetProductSelectionList()
		{
			return m_ContreteCatalogService.GetProductSelectionList();
		}


		public bool SelectionContainsProduct(int selectionId, int productId)
		{
			return m_ContreteCatalogService.SelectionContainsProduct(selectionId, productId);
		}

		#endregion
	}
}
