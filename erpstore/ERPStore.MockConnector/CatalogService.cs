using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;

namespace ERPStore.MockConnector
{
	public class CatalogService : Services.ICatalogService
	{
		public CatalogService(Repositories.CatalogRepository catalogRepository)
		{
			this.CatalogRepository = catalogRepository;
		}

		protected Repositories.CatalogRepository CatalogRepository { get; set; }

        public string Name 
        {
            get
            {
                return "MockCatalog";
            }
        }

		#region ICatalogService Members

		public ERPStore.Models.Product GetProductById(int productId)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Product GetProductByCode(string productCode)
		{
			int count = 0;
			return GetProductList(0, int.MaxValue, out count).SingleOrDefault(i => i.Code.Equals(productCode, StringComparison.InvariantCultureIgnoreCase));
		}

		public ERPStore.Models.Product GetHeadProductOfCategory(int productCategoryId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetProductList(int index, int pageSize, out int count)
		{
			var list = GetProducts().Skip(index * pageSize).Take(pageSize).ToList();
			count = list.Count;
			return list;
		}

		public IList<ERPStore.Models.Product> GetDestockedProductList()
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetTopSellProductList()
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetFirstPriceProductList()
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetProductListBySearch(string query, int index, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetProductListByCategoryId(int categoryId, int index, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetPromotionnalProductListByCategory(int? categoryId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetNewProductListByCategory(int? categoryId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetDestockedProductListByCategory(int? categoryId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetTopSellProductListByCategory(int? categoryId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetFirstPriceProductListByCategory(int? categoryId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetOurSelectionProductListByCategory(int? categoryId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetBlowOfHeartProductListByCategory(int? categoryId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetPromotionnalProductList()
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetNewProductList()
		{
			return GetProducts().Where(i => i.IsNew).ToList();
		}

		public IList<ERPStore.Models.Product> GetCrossSellingList(int productId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetProductListByBrandId(int brandId, int index, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.ProductRelation> GetProductRelations(int productId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetProductListByIdList(IEnumerable<int> productIdList)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.ProductCategory> GetCategories()
		{
			return CatalogRepository.GetAllCategories().ToList();
		}

		public IList<ERPStore.Models.ProductCategory> GetForefrontCategories()
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.ProductCategory GetCategoryByCode(string categoryCode)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.ProductCategory GetCategoryByLink(string link)
		{
			var category = GetCategories().SingleOrDefault(i => i.Link.Equals(link, StringComparison.InvariantCultureIgnoreCase));
			return category;
		}

		public ERPStore.Models.ProductCategory GetCategoryById(int categoryId)
		{
			return CatalogRepository.GetAllCategories().Single(i => i.Id == categoryId);
		}

		public IList<ERPStore.Models.Brand> GetBrands()
		{
			return CatalogRepository.GetAllBrands().ToList();
		}

		public ERPStore.Models.Brand GetBrandById(int? brandId)
		{
			return CatalogRepository.GetAllBrands().SingleOrDefault(i => i.Id == brandId.Value);
		}

		public ERPStore.Models.Brand GetBrandByLink(string link)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Brand> GetTopSellBrands()
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetProducts()
		{
			return CatalogRepository.GetAllProducts().ToList();
		}

		public ERPStore.Models.ProductStockInfo GetProductStockInfo(ERPStore.Models.Product product)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.ProductStockInfo GetProductStockInfo(string productCode)
		{
			var stockInfo = new Models.ProductStockInfo()
			{
				DeliveryDaysCount = 1,
				PhysicalStock = 5,
				ProductCode = productCode,
				ProvisionnedQuantity = 0,
				ProvisionningDaysCount = 10,
				ReservedQuantity = 1,
			};
			return stockInfo;
		}

		public void ReloadCategories()
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Brand> GetBrandListForefront()
		{
			throw new NotImplementedException();
		}

		public void ReloadBrands()
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetProductListBySearch(string query, Dictionary<string, string> parameters, int index, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.PropertyGroup> GetProductExtendedPropertyListBySearch(ERPStore.Models.ProductListFilter filter)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetProductListByCategoryId(int categoryId, NameValueCollection parameters, int index, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.PropertyGroup> GetProductExtendedPropertyListByCategoryId(int categoryId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetProductListByBrandId(int brandId, NameValueCollection parameters, int index, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.PropertyGroup> GetProductExtendedPropertyListByBrandId(int brandId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.PropertyGroup> GetProductPropertyGroupList()
		{
			return new List<Models.PropertyGroup>();
		}

		public IList<ERPStore.Models.Product> GetProductListBySearch(ERPStore.Models.ProductListFilter filter, int index, int pageSize, out int count)
		{
			IEnumerable<ERPStore.Models.Product> result = null;
			switch (filter.ListType)
			{
				case ERPStore.Models.ProductListType.All:
					result = CatalogRepository.GetAllProducts();
					break;
				case ERPStore.Models.ProductListType.Promotional:
					break;
				case ERPStore.Models.ProductListType.New:
					result = CatalogRepository.GetAllProducts().Where(i => i.IsNew);
					break;
				case ERPStore.Models.ProductListType.Destock:
					break;
				case ERPStore.Models.ProductListType.TopSell:
					break;
				case ERPStore.Models.ProductListType.FirstPrice:
					break;
				case ERPStore.Models.ProductListType.Front:
					break;
				case ERPStore.Models.ProductListType.Customer:
					break;
				case ERPStore.Models.ProductListType.Search:
					break;
				case ERPStore.Models.ProductListType.Category:
					break;
				case ERPStore.Models.ProductListType.Brand:
					break;
				default:
					break;
			}

			count = 0;
			if (result == null)
			{
				result = new List<Models.Product>();
			}
			count = result.Count();
			return result.Skip(index * pageSize).Take(pageSize).ToList();
		}

		public IList<ERPStore.Models.ProductCategory> GetProductCategoryListByBrandId(int brandId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Brand> GetBrandListByProductCategoryId(int productCategoryId)
		{
			throw new NotImplementedException();
		}


		public IList<ERPStore.Models.Product> GetDestockedProductListByBrand(int brandId)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.ProductCategory> GetProductCategoryListBySearch(ERPStore.Models.ProductListFilter search)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.Brand GetBrandByName(string name)
		{
			return GetBrands().SingleOrDefault(i => i.Name.Equals(name, StringComparison.CurrentCultureIgnoreCase));
		}

		public IList<ERPStore.Models.Brand> GetBrandListBySearch(ERPStore.Models.ProductListFilter search)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.Product> GetProductListByCustomer(ERPStore.Models.ProductListFilter filter, int index, int pageSize, out int count)
		{
			throw new NotImplementedException();
		}

		public IList<ERPStore.Models.ProductStockInfo> GetProductStockInfoList(IEnumerable<int> productIdList)
		{
			var psiList = from psi in CatalogRepository.GetAllProductStockInfo()
						  from product in CatalogRepository.GetAllProducts()
						  where psi.ProductCode == product.Code
								&& productIdList.Contains(product.Id)
						  select psi;

			return psiList.ToList();
		}

		public void ApplyBestPrice(IList<ERPStore.Models.Product> productList, ERPStore.Models.User user)
		{
			foreach (var item in productList)
			{
				ApplyBestPrice(item, user);
			}
		}

		public void ApplyBestPrice(ERPStore.Models.Product product, ERPStore.Models.User user)
		{
			product.BestPrice = product.SalePrice;
		}

		public NameValueCollection RemoveNotFilteredParameters(NameValueCollection parameters)
		{
			return null;
		}

		public IEnumerable<ERPStore.Models.Profession> GetProfessionList()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<ERPStore.Models.SearchTerm> GetTopSearchTermList(int count)
		{
			throw new NotImplementedException();
		}

		public ERPStore.Models.ProductListFilter CreateProductListFilter(System.Web.HttpContextBase context)
		{
			return new ERPStore.Models.ProductListFilter();
		}

		public string GetProductKeywords(ERPStore.Models.Product Product)
		{
			throw new NotImplementedException();
		}


		public IList<ERPStore.Models.EntitySelection> GetProductSelectionList()
		{
			throw new NotImplementedException();
		}

		public bool SelectionContainsProduct(int selectionId, int productId)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
