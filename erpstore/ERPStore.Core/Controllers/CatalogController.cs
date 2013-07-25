using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Routing;
using System.Collections.Specialized;
using ERPStore.Models;
using Microsoft.Practices.Unity;

using ERPStore.Services;
using ERPStore.Html;


namespace ERPStore.Controllers
{
	/// <summary>
	/// Controlleur du catalogue
	/// </summary>
	// [HandleError(View = "500")]
	public class CatalogController : StoreController
    {
		private static object m_lock = new object();

		public CatalogController(
			ICacheService cacheService,
			ICatalogService catalogService,
			IDocumentService documentService
			)
		{
			this.CacheService = cacheService;
			this.CatalogService = catalogService;
			this.DocumentService = documentService;
		}

		protected ICatalogService CatalogService { get; set; }

		protected ICacheService CacheService { get; set; }

		protected IDocumentService DocumentService { get; set; }

        public ActionResult Index()
        {
            return View();
		}

		#region Product

		[ActionFilters.TrackerActionFilter]
		public ActionResult Product(string code)
		{
			if (code.IsNullOrTrimmedEmpty())
			{
				return View("NoProductFound");
			}
			code = code.Trim().TrimEnd('/').Replace("__", "/");
			Product product = CatalogService.GetProductByCode(code);
			if (product == null)
			{
				Logger.Info("No Product Found : {0}", code);
				return View("NoProductFound");
			}
			// Recherche des tarifs client si celui-ci est connecté
			CatalogService.ApplyBestPrice(product, User.GetUserPrincipal().CurrentUser);
			ViewData["productCode"] = code;
			ViewData["productId"] = product.Id;
			ViewData.Model = product;
			return View();
		}

		[AcceptVerbs(HttpVerbs.Get)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult DirectSearch(string s, int? page, int? pageSize)
		{
			s = System.Web.HttpUtility.UrlDecode(s);
			return Search(s, page, pageSize);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Search(string s, int? page, int? pageSize)
		{
			if (s.IsNullOrTrimmedEmpty())
			{
				return View("NoProductFound");
			}
			var pageId = GetPageId(page);

			s = s.Trim();
			s = Noises.Clean("fr", s);

			int count = 0;
			pageSize = pageSize.GetValueOrDefault(ERPStoreApplication.WebSiteSettings.Catalog.PageSize);
			var parameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var category = CatalogService.GetCategoryByCode(Request["category"]);
			var brand = CatalogService.GetBrandByName(Request["brand"]);


			var filter = CatalogService.CreateProductListFilter(HttpContext);
			filter.ExtendedParameters = parameters;
			filter.Search = s;
			if (category != null)
			{
				filter.ProductCategoryId = category.Id;
			}
			if (brand != null)
			{
				filter.BrandId = brand.Id;
			}
			var productList = CatalogService.GetProductListBySearch(filter, pageId, pageSize.Value, out count);

			if (pageId == 0)
			{
				EventPublisherService.Publish(new Models.Events.ProductSearchEvent(User.GetUserPrincipal(), s, count, CatalogService.Name));
				ViewData["search"] = filter.Search;
				ViewData["resultCount"] = count;
			}

			if (productList == null || productList.Count == 0)
			{
				return View("NoProductFound");
			}

			// Afficher directement la fiche produit
			if (productList.Count == 1)
			{
				var p = productList.First();
				var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);
				var url = urlHelper.Href(p);
				return Redirect(url);
			}

			// Recherche des tarifs client si celui-ci est connecté
			CatalogService.ApplyBestPrice(productList, User.GetUserPrincipal().CurrentUser);

			var result = new Models.ProductList(productList);
			result.Query = s;
			result.ItemCount = count;
			result.PageIndex = pageId + 1;
			result.PageSize = pageSize.Value;
			result.ExtendedParameters = parameters;
			result.Category = category;
			result.Brand = brand;
			result.ListType = ERPStore.Models.ProductListType.Search;

			ViewData.Model = result;
			return View("ProductSearchResult");
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult CustomerProduct(string s, int? page, int? pageSize)
		{
			var pageId = GetPageId(page);

			int count = 0;
			pageSize = pageSize.GetValueOrDefault(ERPStoreApplication.WebSiteSettings.Catalog.PageSize);
			var parameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var category = CatalogService.GetCategoryByCode(Request["category"]);
			var brand = CatalogService.GetBrandByName(Request["brand"]);

			var filter = CatalogService.CreateProductListFilter(HttpContext);
			filter.ExtendedParameters = parameters;
			filter.Search = s;
			if (category != null)
			{
				filter.ProductCategoryId = category.Id;
			}
			if (brand != null)
			{
				filter.BrandId = brand.Id;
			}
			if (User.GetUserPrincipal().CurrentUser.Corporate != null)
			{
				filter.CorporateId = User.GetUserPrincipal().CurrentUser.Corporate.Id;
			}
			else
			{
				filter.UserId = User.GetUserPrincipal().CurrentUser.Id;
			}
			var productList = CatalogService.GetProductListByCustomer(filter, pageId, pageSize.Value, out count);

			if (productList == null || productList.Count == 0)
			{
				return View("NoProductFound");
			}

			// Recherche des tarifs client si celui-ci est connecté
			CatalogService.ApplyBestPrice(productList, User.GetUserPrincipal().CurrentUser);

			var result = new Models.ProductList(productList);
			result.ItemCount = count;
			result.PageIndex = pageId + 1;
			result.PageSize = pageSize.Value;
			result.ExtendedParameters = parameters;
			result.Category = category;
			result.Brand = brand;
			result.ListType = ERPStore.Models.ProductListType.Customer;

			ViewData.Model = result;
			return View("ProductCustomer");
		}

		[ActionFilters.TrackerActionFilter]
		public JsonResult JsProductInfo(string productCode)
		{
			var product = CatalogService.GetProductByCode(productCode);
			if (product == null)
			{
				return new JsonResult();
			}
			return Json(new
			{
				Title = product.Title,
				Quantity = product.Packaging,
			});
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult ProductList(Models.ProductListType type, int? page, int? pageSize)
		{
			var pageId = GetPageId(page);

			int count = 0;
			pageSize = pageSize.GetValueOrDefault(ERPStoreApplication.WebSiteSettings.Catalog.PageSize);
			var parameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var category = CatalogService.GetCategoryByCode(Request["category"]);
			var brand = CatalogService.GetBrandByName(Request["brand"]);

			var filter = CatalogService.CreateProductListFilter(HttpContext);

			filter.ExtendedParameters = parameters;
			filter.Search = Request["s"];
			if (category != null)
			{
				filter.ProductCategoryId = category.Id;
			}
			if (brand != null)
			{
				filter.BrandId = brand.Id;
			}

			filter.ListType = type;

			string viewName = null;
			switch (type)
			{
				case ERPStore.Models.ProductListType.Promotional:
					viewName = "promotions";
					filter.SortList.Add(new ERPStore.Models.SortProductList()
					{
						PropertyName = "CreationDate",
						Direction = System.ComponentModel.ListSortDirection.Descending,
					});
					break;
				case ERPStore.Models.ProductListType.New:
					viewName = "newproducts";
					filter.SortList.Add(new ERPStore.Models.SortProductList()
					{
						PropertyName = "CreationDate",
						Direction = System.ComponentModel.ListSortDirection.Descending,
					});
					break;
				case ERPStore.Models.ProductListType.Destock:
					viewName = "destock"; 
					break;
				case ERPStore.Models.ProductListType.TopSell:
					viewName = "topsells";
					filter.SortList.Add(new ERPStore.Models.SortProductList()
					{
						PropertyName = "CreationDate",
						Direction = System.ComponentModel.ListSortDirection.Descending,
					});
					break;
				case ERPStore.Models.ProductListType.FirstPrice:
					viewName = "firstprice";
					break;
			}

			var list = CatalogService.GetProductListBySearch(filter, pageId, pageSize.Value, out count);

			var result = new Models.ProductList(list);
			result.ItemCount = count;
			result.PageIndex = pageId + 1;
			result.PageSize = pageSize.Value;
			result.ListType = type;
			result.Brand = brand;
			result.Category = category;

			var user = User.GetUserPrincipal();
			CatalogService.ApplyBestPrice(list, user.CurrentUser);

			ViewData.Model = result;
			return View(viewName);
		}


		#region PartialRendering

		public ActionResult ShowProductList(IEnumerable<ERPStore.Models.Product> list)
		{
			return PartialView("ProductList", list);
		}

		/// <summary>
		/// Shows the product list view.
		/// </summary>
		/// <param name="type">The type.</param>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="productCount">The product count.</param>
		/// <param name="sort">The sort.</param>
		/// <returns></returns>
		// [ActionFilters.PartialRenderActionFilter(Order = 1)]
		// [ActionFilters.CacheActionFilter(CacheKey = "ProductList", Duration = 3600, Order = 2, VaryByParam="*")]
		public ActionResult ShowProductListView(Models.ProductListType type, string viewName, int? productCount, List<Models.SortProductList> sort)
		{
			var key = string.Format("ShowProductListView|{0}|{1}", type, productCount.GetValueOrDefault(int.MaxValue));
			var list = CacheService[key] as IList<Models.Product>;

			if (list == null)
			{
				var filter = CatalogService.CreateProductListFilter(HttpContext);
				filter.ListType = type;

				int count = 0;
				if (sort.IsNotNullOrEmpty())
				{
					foreach (var item in sort)
					{
						filter.SortList.Add(item);
					}
				}
				list = CatalogService.GetProductListBySearch(filter, 0, productCount.GetValueOrDefault(int.MaxValue), out count);
				CacheService.Add(key, list, DateTime.Now.AddHours(1));
			}

			var user = User.GetUserPrincipal();
			CatalogService.ApplyBestPrice(list, user.CurrentUser);
			// var result = new Models.ProductList(list);

			return PartialView(viewName, list);
		}


		//// [ActionFilters.PartialRenderActionFilter(Order = 1)]
		//[ActionFilters.CacheActionFilter(CacheKey = "ProductListByCategory", Duration = 3600, Order = 2, VaryByParam = "*")]
		//public ActionResult ShowProductListByCategoryView(Models.ProductListByCategoryType? productListType, int? categoryId, string productByCategoryViewName, int? productByCatagoryCount)
		//{
		//    IList<Models.Product> list = null;
		//    switch (productListType)
		//    {
		//        case Models.ProductListByCategoryType.Promotional:
		//            list = CatalogService.GetPromotionnalProductListByCategory(categoryId);
		//            break;
		//        case Models.ProductListByCategoryType.New:
		//            list = CatalogService.GetNewProductListByCategory(categoryId);
		//            break;
		//        case Models.ProductListByCategoryType.Destock:
		//            list = CatalogService.GetDestockedProductListByCategory(categoryId);
		//            break;
		//        case Models.ProductListByCategoryType.TopSell:
		//            list = CatalogService.GetTopSellProductListByCategory(categoryId);
		//            break;
		//        case Models.ProductListByCategoryType.FirstPrice:
		//            list = CatalogService.GetFirstPriceProductListByCategory(categoryId);
		//            break;
		//        case Models.ProductListByCategoryType.OurSelection :
		//            list = CatalogService.GetOurSelectionProductListByCategory(categoryId);
		//            break;
		//        case Models.ProductListByCategoryType.BlowOfHeart:
		//            list = CatalogService.GetBlowOfHeartProductListByCategory(categoryId);
		//            break;
		//    }
		//    if (productByCatagoryCount.HasValue && productByCatagoryCount.Value > 0)
		//    {
		//        list = list.Take(productByCatagoryCount.Value).ToList();
		//    }
		//    return View(string.Format("~/views/catalog/{0}", productByCategoryViewName), list);
		//}

		public ActionResult ShowProductCategoryByCode(string viewName, string categoryCode)
		{
			var category = CatalogService.GetCategoryByCode(categoryCode);
			return PartialView(viewName);
		}

		//[AcceptVerbs(HttpVerbs.Post)]
		//[ActionFilters.CompressFilter]
		//public ActionResult ShowRelationalProductList(string relationTypeName, string productCode, string viewName)
		//{
		//    Models.ProductList model = null;
		//    lock (m_lock)
		//    {
		//        var relationType = (Models.ProductRelationType)Enum.Parse(typeof(Models.ProductRelationType), relationTypeName);
		//        string key = string.Format("RelationalProductList|{0}", productCode);
		//        string key2 = string.Format("RelationalProductListRelation|{0}", productCode);
		//        IList<Models.Product> productList = CacheService[key] as List<Models.Product>;
		//        IList<Models.ProductRelation> relationList = CacheService[key2] as List<Models.ProductRelation>;
		//        if (productList == null)
		//        {
		//            var product = CatalogService.GetProductByCode(productCode);

		//            if (product == null)
		//            {
		//                return Content(string.Empty);
		//            }

		//            relationList = CatalogService.GetProductRelations(product.Id);
		//            var expirationDate = DateTime.Now.AddHours(1);
		//            if (relationList.IsNotNullOrEmpty())
		//            {
		//                var productIdList = relationList.Select(i => i.ChildProductId).Distinct();
		//                productList = CatalogService.GetProductListByIdList(productIdList);
		//            }
		//            else
		//            {
		//                relationList = new List<Models.ProductRelation>();
		//                productList = new List<Models.Product>();
		//            }
		//            CacheService.Add(key2, relationList, expirationDate);
		//            CacheService.Add(key, productList, expirationDate);
		//        }

		//        var result = (from product in productList
		//                      from relation in relationList
		//                      where relation.ChildProductId == product.Id
		//                         && relation.ProductRelationType == relationType
		//                      select product).ToList();

		//        if (result.IsNullOrEmpty())
		//        {
		//            return Content(string.Empty);
		//        }

		//        CatalogService.ApplyBestPrice(result, User.GetUserPrincipal().CurrentUser);

		//        model = new Models.ProductList(result);
		//        model.RelationType = relationType;
		//    }

		//    return View(string.Format("~/views/catalog/{0}", viewName), model);
		//}

		public ActionResult ShowRelationalProductList(string productCode, string viewName)
		{
			// Hack bot sans paramètre
			if (productCode.IsNullOrTrimmedEmpty())
			{
				return Content(string.Empty);
			}

			string key = string.Format("RelationalProductList{0}", productCode);
			var result = CacheService[key] as Dictionary<Models.ProductRelationType, List<Models.Product>>;
			if (result.IsNullOrEmpty())
			{
				result = new Dictionary<ERPStore.Models.ProductRelationType,List<ERPStore.Models.Product>>();
				var p = CatalogService.GetProductByCode(productCode);
				if (p == null)
				{
					return Content(string.Empty, "text/html");
				}
				var list = CatalogService.GetProductRelations(p.Id);
				if (list.IsNotNullOrEmpty())
				{
					var productList = CatalogService.GetProductListByIdList(list.Select(i => i.ChildProductId));

					var complementaryRelations = from product in productList
												 from relation in list
												 where relation.ChildProductId == product.Id
													   && relation.ProductRelationType == ERPStore.Models.ProductRelationType.Complementary
												 select product;

					var similarRelations = from product in productList
										   from relation in list
										   where relation.ChildProductId == product.Id
												 && relation.ProductRelationType == ERPStore.Models.ProductRelationType.Similar
										   select product;

					var variantRelations = from product in productList
										   from relation in list
										   where relation.ChildProductId == product.Id
												 && relation.ProductRelationType == ERPStore.Models.ProductRelationType.Variant
										   select product;

					var substituteRelations = from product in productList
											  from relation in list
											  where relation.ChildProductId == product.Id
													&& relation.ProductRelationType == ERPStore.Models.ProductRelationType.Substitute
											  select product;


					result.Add(Models.ProductRelationType.Complementary, complementaryRelations.ToList());
					result.Add(Models.ProductRelationType.Variant, variantRelations.ToList());
					result.Add(Models.ProductRelationType.Substitute, substituteRelations.ToList());
					result.Add(Models.ProductRelationType.Similar, similarRelations.ToList());
				}
				CacheService.Add(key, result, DateTime.Now.AddHours(1));
			}

			if (result.IsNullOrEmpty())
			{
				return Content(string.Empty);
			}

			// Application du meilleur tarif
			// TODO : refaire, pas bon entre 2 requetes
			foreach (var item in result)
			{
				CatalogService.ApplyBestPrice(item.Value, User.GetUserPrincipal().CurrentUser);
			}

            return PartialView(viewName, result);
		}

		public ActionResult ShowContextualProductList(string viewName, List<Models.Product> list)
		{
			var keywords = this.ControllerContext.HttpContext.GetSearchKeywords();
			if (keywords.IsNullOrTrimmedEmpty())
			{
				return new EmptyResult();
			}
			var filter = new Models.ProductListFilter();
			filter.Search = keywords;
			int count = 0;
			var productList = CatalogService.GetProductListBySearch(filter, 0, 10, out count);
			if (productList.IsNullOrEmpty())
			{
				return new EmptyResult();
			}
			// on retire les produits affichés
			if (!list.IsNullOrEmpty())
			{
				foreach (var item in list)
				{
					productList.RemoveAll(i => i.Id == item.Id);
				}
			}
			ViewData.Model = productList;
			return PartialView(viewName);
		}

		public ActionResult ShowProduct(ERPStore.Models.Product product)
		{
			return PartialView("ProductItem", product);
		}

		[AcceptVerbs(HttpVerbs.Post)]
		// [ActionFilters.CacheActionFilter(CacheKey = "ProductCrossList", Duration = 3600, Order = 2, VaryByParam = "*")]
		public ActionResult ShowCrossProductList(string productCode, string viewName)
		{
			var key = string.Format("ProductCrossList:{0}:{1}", productCode, viewName);
			var crossSellingList = CacheService[key] as IList<Models.Product>;
			if (crossSellingList == null)
			{
				var product = CatalogService.GetProductByCode(productCode);

				if (product == null)
				{
					return Content(string.Empty);
				}

				crossSellingList = CatalogService.GetCrossSellingList(product.Id);
				if (crossSellingList == null)
				{
					crossSellingList = new List<Models.Product>();
				}
				CacheService.Add(key, crossSellingList, DateTime.Now.AddHours(1));
			}
			CatalogService.ApplyBestPrice(crossSellingList, User.GetUserPrincipal().CurrentUser);

			return PartialView(viewName, crossSellingList);
		}

		// [ActionFilters.PartialRenderActionFilter(Order = 1)]
		// [ActionFilters.CacheActionFilter(CacheKey = "ProductCrossSelling", Duration = 3600, Order = 2, VaryByParam = "*")]
		public ActionResult ShowCrossSellingList(int? productId, string crossSellingViewName, int productCount)
		{
			var crossSellingList = CatalogService.GetCrossSellingList(productId.Value);

			if (crossSellingList != null)
			{
				crossSellingList = crossSellingList.Take(productCount).ToList();
				CatalogService.ApplyBestPrice(crossSellingList, User.GetUserPrincipal().CurrentUser);
			}
			else
			{
				crossSellingList = new List<Models.Product>();
			}
			return PartialView(crossSellingViewName, crossSellingList);
		}

		// [ActionFilters.PartialRenderActionFilter(Order=1)]
		public ActionResult ShowHeadProductOfCategory(int productCategoryId, string viewName)
		{
			var product = CatalogService.GetHeadProductOfCategory(productCategoryId);
			if (product == null)
			{
				Logger.Warn(string.Format("Product missing in category {0}", productCategoryId));
				return Content(string.Empty);
			}
	
			return PartialView(viewName, product);
		}

		/// <summary>
		/// Retourne la disponibilité des produits sous forme de texte
		/// </summary>
		/// <param name="productCode">The product code.</param>
		/// <returns></returns>
		// [OutputCache(Duration=300,VaryByParam="productCode")]
		public string GetProductDisponibility(string productCode)
		{
			var stockInfo = GetProductStockInfoByCode(productCode);
			if (stockInfo.HasStock)
			{
				return string.Format("{0} en stock", stockInfo.AvailableStock);
			}
			if (stockInfo.TotalProvisionningDayCount < 0)
			{
				return "Nous contacter";
			}
			var plurial = (stockInfo.TotalProvisionningDayCount > 1) ? "s" : string.Empty;
			var result = string.Format("disponible en {0} jour{1}", stockInfo.TotalProvisionningDayCount, plurial);
			return result;
		}

		public JsonResult GetJSProductDisponibility(string productCode)
		{
			var result = GetProductDisponibility(productCode);
			return Json(new
			{
				Disponibility = result,
				Code = productCode,
			});
		}

        public ActionResult ShowProductDisponibility(ERPStore.Models.Product product, string viewName)
        {
            ViewData.Model = product;
            return PartialView(viewName);
        }

		// [OutputCache(Duration = 300, VaryByParam = "*")]
		public ActionResult ShowProductStockInfo(string productCode, string viewName)
		{
			if (productCode.IsNullOrTrimmedEmpty()
				|| viewName.IsNullOrTrimmedEmpty())
			{
				return Content(string.Empty, "text/plain");
			}
			var stockInfo = GetProductStockInfoByCode(productCode);
			ViewData.Model = stockInfo;
			return PartialView(viewName);
		}

		// [ActionFilters.PartialRenderActionFilter]
		public ActionResult ShowProductExtendedPropertyList(string viewName)
		{
			ViewData.Model = CatalogService.GetProductPropertyGroupList();
			return PartialView(viewName);
		}

		public ActionResult ShowContextualProductExtendedPropertyList(string viewName)
		{
			var searchFilter = CatalogService.CreateProductListFilter(HttpContext);
			searchFilter.Search = Request["s"];
			searchFilter.ExtendedParameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var categoryCode = Request["category"];
			if (!categoryCode.IsNullOrTrimmedEmpty())
			{
				var category = CatalogService.GetCategoryByCode(categoryCode);
				if (category != null)
				{
					searchFilter.ProductCategoryId = category.Id;
				}
			}
			var brandname = Request["brand"];
			if (!brandname.IsNullOrTrimmedEmpty())
			{
				var brand = CatalogService.GetBrandByName(brandname);
				if (brand != null)
				{
					searchFilter.BrandId = brand.Id;
				}
			}
			ViewData.Model = CatalogService.GetProductExtendedPropertyListBySearch(searchFilter);
			return PartialView(viewName);
		}

		// [ActionFilters.PartialRenderActionFilter]
		[Obsolete("use ShowContextualProductExtendedPropertyList instead", true)]
		public ActionResult ShowProductExtendedPropertyListBySearch(string viewName, string query)
		{
			var searchFilter = CatalogService.CreateProductListFilter(HttpContext);
			searchFilter.Search = query;
			searchFilter.ExtendedParameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			ViewData.Model = CatalogService.GetProductExtendedPropertyListBySearch(searchFilter);
			var categoryCode = Request["category"];
			if (!categoryCode.IsNullOrTrimmedEmpty())
			{
				var category = CatalogService.GetCategoryByCode(categoryCode);
				if (category != null)
				{
					searchFilter.ProductCategoryId = category.Id;
				}
			}
			var brandname = Request["brand"];
			if (!brandname.IsNullOrTrimmedEmpty())
			{
				var brand = CatalogService.GetBrandByName(brandname);
				if (brand != null)
				{
					searchFilter.BrandId = brand.Id;
				}
			}
			return PartialView(viewName);
		}

		// [Obsolete("use ShowContextualProductExtendedPropertyList instead", true)]
		public ActionResult ShowProductExtendedPropertyListByCategory(string viewName, Models.ProductCategory category)
		{
			var searchFilter = CatalogService.CreateProductListFilter(HttpContext);
			searchFilter.Search = Request["s"];
			searchFilter.ExtendedParameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			searchFilter.ProductCategoryId = category.Id;
			var brandname = Request["brand"];
			if (!brandname.IsNullOrTrimmedEmpty())
			{
				var brand = CatalogService.GetBrandByName(brandname);
				if (brand != null)
				{
					searchFilter.BrandId = brand.Id;
				}
			}
			ViewData.Model = CatalogService.GetProductExtendedPropertyListBySearch(searchFilter);
			return PartialView(viewName);
		}

		[Obsolete("use ShowContextualProductExtendedPropertyList instead", true)]
		public ActionResult ShowProductExtendedPropertyListByBrand(string viewName, Models.Brand brand)
		{
			var searchFilter = CatalogService.CreateProductListFilter(HttpContext);
			searchFilter.Search = Request["s"];
			searchFilter.ExtendedParameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			searchFilter.BrandId = brand.Id;
			var categoryCode = Request["category"];
			if (!categoryCode.IsNullOrTrimmedEmpty())
			{
				var category = CatalogService.GetCategoryByCode(categoryCode);
				if (category != null)
				{
					searchFilter.ProductCategoryId = category.Id;
				}
			}
			ViewData.Model = CatalogService.GetProductExtendedPropertyListBySearch(searchFilter);
			return PartialView(viewName);
		}

        public ActionResult ShowProductPrice(Models.Product product)
        {
            ViewData["ShowPriceWithTax"] = ERPStoreApplication.WebSiteSettings.Payment.ShowPriceWithTax;

            if (product.SaleMode == ERPStore.Models.ProductSaleMode.Sellable)
            {
                if (product.SelectedPrice == ERPStore.Models.PriceType.Customer)
                {
                    return PartialView("_customerprice", product);
                }
                else if (product.SelectedPrice == ERPStore.Models.PriceType.Destock)
                {
                    return PartialView("_destockprice", product);
                }
                else if (product.SelectedPrice == ERPStore.Models.PriceType.Promotional)
                {
                    return PartialView("_promotionnalprice", product);
                }
                else if (product.IsFirstPrice
                    && product.MarketPrice != null
                    && product.MarketPrice.Value > 0)
                {
                    return PartialView("_firstprice", product);
                }
                else
                {
                    return PartialView("_basicprice", product);
                }
            }
            else
            {
                return PartialView("_noprice", product);
            }
        }

        public ActionResult ShowPictureList(Models.Product product, int size, int normalWidth, int normalHeight, string viewName)
        {
            var list = DocumentService.GetMediaList(product);
            var pictureList = new List<Models.Media>();
            if (list.IsNullOrEmpty())
            {
                return Content("<!-- pas d'image -->");
            }

            var allowedExtensionList = new List<string>()
				{
					".gif"
					,".jpg"
					,".png"
					,".jpeg"
					,".bmp"
				};

            foreach (var item in list)
            {
                var fileName = item.FileName;

                Logger.Debug("Process attached picture : " + item.SerializeToXml());

                if (item.MimeType == null
                    && !item.ExternalUrl.IsNullOrTrimmedEmpty()
                    && item.ExternalUrl.IndexOf("doc.ashx") == -1)
                {
                    try
                    {
                        fileName = DocumentService.GetFileNameFromUrl(item.ExternalUrl);
                    }
                    catch (Exception ex)
                    {
                        Logger.Warn(ex.Message);
                        continue;
                    }

                    // On verifie s'il s'agit d'une image
                }
                else if (item.MimeType != null
                    && !item.MimeType.StartsWith("image/"))
                {
                    Logger.Debug("Is not picture : {0}", item.MimeType);
                    continue;
                }

                string ext = ".png";
                if (!fileName.IsNullOrTrimmedEmpty())
                {
                    ext = System.IO.Path.GetExtension(fileName);
                }
                if (!allowedExtensionList.Any(i => i.Equals(ext, StringComparison.InvariantCultureIgnoreCase)))
                {
                    Logger.Debug("Is not picture extension : {0}", ext);
                    continue;
                }

                var pos = list.IndexOf(item);
                string imagePattern = "/images/{0}/{1}/{2}/{3}-PIC{4:###}{5}";
                item.IconeSrc = string.Format(imagePattern, item.Id, size, size, product.Code, pos, ext);
                item.Url = string.Format(imagePattern, item.Id, normalWidth, normalHeight, product.Code, pos, ext);
                pictureList.Add(item);
            }
            ViewData.Model = pictureList;
            return PartialView(viewName);
        }


		#endregion

		#endregion

		#region Categories

		[ActionFilters.TrackerActionFilter]
		public ActionResult CategoryList()
		{
			var list = CatalogService.GetCategories().OrderBy(i => i.Name).ToList();
			ViewData.Model = list;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult Category(string link, int? page, int? pageSize)
		{
			if (link != null)
			{
				link = link.Trim().TrimEnd('/');
			}
			else
			{
				// return RedirectToAction("CategoryList");
				return RedirectToERPStoreRoute(ERPStoreRoutes.PRODUCT_CATEGORIES);
			}
			var category = CatalogService.GetCategoryByLink(link);
			if (category == null)
			{
				// Recherche dans la liste de compensation
				string fileName = Server.MapPath(@"/app_data/categories-compensation.txt");
				var categoryCompensationListFileInfo = new System.IO.FileInfo(fileName);
				if (!categoryCompensationListFileInfo.Exists)
				{
					Logger.Notification("Missing category : {0} on {1}", link, fileName);
					return new RedirectResult("/");
				}
				using (var content = categoryCompensationListFileInfo.OpenText())
				{
					while(true)
					{
						var line = content.ReadLine();
						if (line.IsNullOrTrimmedEmpty())
						{
							break;
						}
						string[] compensation = line.Split(':');
						var badlink = compensation[0];
						if (badlink.Equals(link, StringComparison.InvariantCultureIgnoreCase))
						{
							category = CatalogService.GetCategoryByLink(compensation[1]);
							if (category != null)
							{
								break;
							}
						}
					}
				}
				if (category == null)
				{
					var adminUrl = string.Format("http://{0}/admin/ProductCategoryUrlMovedPermanently?badUrl={1}", ERPStoreApplication.WebSiteSettings.CurrentUrl, link);
					Logger.Notification("Missing category : {0}\r\n {1}", link, adminUrl);
					return new RedirectResult("/");
				}
				else
				{
					var urlHelper = new UrlHelper(this.ControllerContext.RequestContext);
					var redirect = urlHelper.Href(category);
					MovedPermanently(redirect);
				}
			}
			if (!page.HasValue || page < 0)
			{
				page = 0;
			}
			else
			{
				page = page.Value - 1;
			}
			if (page < 0)
			{
				page = 0;
			}
			int count = 0;
			pageSize = pageSize.GetValueOrDefault(ERPStoreApplication.WebSiteSettings.Catalog.PageSize);
			var parameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var brand = CatalogService.GetBrandByName(Request["brand"]);

			var productSearch = CatalogService.CreateProductListFilter(this.HttpContext);
			if (brand != null)
			{
				productSearch.BrandId = brand.Id;
			}
			productSearch.ProductCategoryId = category.Id;
			productSearch.ExtendedParameters = parameters;

			var productList = CatalogService.GetProductListBySearch(productSearch, page.Value, pageSize.Value, out count);

			// Recherche des tarifs client si celui-ci est connecté
			CatalogService.ApplyBestPrice(productList, User.GetUserPrincipal().CurrentUser);

			var pageIndex = page.Value + 1;

			var result = new Models.ProductList(productList);
			result.Category = category;
			result.ItemCount = count;
			result.PageIndex = pageIndex;
			result.PageSize = pageSize.Value;
			result.ExtendedParameters = parameters;
			result.ListType = ERPStore.Models.ProductListType.Category;
			result.Brand = brand;
			ViewData.Model = result;

			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult ProductListBySelection(string selectionName, int? page, int? pageSize)
		{
			if (selectionName != null)
			{
				selectionName = selectionName.Trim().TrimEnd('/');
			}
			else
			{
				// return RedirectToAction("CategoryList");
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			var selectionList = CatalogService.GetProductSelectionList();
			var selection = selectionList.FirstOrDefault(i => i.Name.Equals(selectionName, StringComparison.InvariantCultureIgnoreCase));

			if (selection == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
			}

			if (!page.HasValue || page < 0)
			{
				page = 0;
			}
			else
			{
				page = page.Value - 1;
			}
			if (page < 0)
			{
				page = 0;
			}
			int count = 0;
			pageSize = pageSize.GetValueOrDefault(ERPStoreApplication.WebSiteSettings.Catalog.PageSize);
			var parameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var brand = CatalogService.GetBrandByName(Request["brand"]);
			var category = CatalogService.GetCategoryByLink(Request["category"]);

			var productSearch = CatalogService.CreateProductListFilter(this.HttpContext);
			if (brand != null)
			{
				productSearch.BrandId = brand.Id;
			}
			if (category != null)
			{
				productSearch.ProductCategoryId = category.Id;
			}
			productSearch.ExtendedParameters = parameters;
			productSearch.SelectionId = selection.Id;

			var productList = CatalogService.GetProductListBySearch(productSearch, page.Value, pageSize.Value, out count);

			// Recherche des tarifs client si celui-ci est connecté
			CatalogService.ApplyBestPrice(productList, User.GetUserPrincipal().CurrentUser);

			var pageIndex = page.Value + 1;

			var result = new Models.ProductList(productList);
			result.Category = category;
			result.ItemCount = count;
			result.PageIndex = pageIndex;
			result.PageSize = pageSize.Value;
			result.ExtendedParameters = parameters;
			result.ListType = ERPStore.Models.ProductListType.Category;
			result.Brand = brand;
			result.SelectionName = selection.Name;
			ViewData.Model = result;

			return View("Selection");
		}

		#region Partial Rendering

		// [ActionFilters.PartialRenderActionFilter]
		public ActionResult ShowCategoriesBox(string viewName, System.Web.Routing.RouteData routeData)
		{
			var list = CatalogService.GetCategories();

			return PartialView(viewName, list);
		}

        public ActionResult ShowProductListBySelection(string viewName, string selectionName, int productCount)
        {
            var selectionList = CacheService["productSelectionList"] as IList<Models.EntitySelection>;
            if (selectionList == null)
            {
                selectionList = CatalogService.GetProductSelectionList();
                CacheService.Add("productSelectionList", selectionList, DateTime.Now.AddHours(1));
            }
            if (selectionList == null)
            {
                return new EmptyResult();
            }
            var selection = selectionList.SingleOrDefault(i => i.Name.Equals(selectionName, StringComparison.CurrentCultureIgnoreCase));
            if (selection == null)
            {
                return new EmptyResult();
            }

            var filter = new Models.ProductListFilter()
            {
                SelectionId = selection.Id,
            };
            int count = 0;
            var list = CatalogService.GetProductListBySearch(filter, 0, productCount, out count);

            CatalogService.ApplyBestPrice(list, User.GetUserPrincipal().CurrentUser);

            return PartialView(viewName, list);
        }

		// [ActionFilters.PartialRenderActionFilter]
		public ActionResult ShowCategorieListForefront(string viewName, System.Web.Routing.RouteData routeData)
		{
			var list = CatalogService.GetForefrontCategories();
			return PartialView( viewName, list);
		}

		// [ActionFilters.CacheActionFilter(CacheKey = "ProductCategoryListByBrand", Duration = 3600, VaryByParam = "*")]
		public ActionResult ShowProductCategoryListByBrand(Models.Brand brand, string viewName)
		{
			var parameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var searchFilter = CatalogService.CreateProductListFilter(HttpContext);
			searchFilter.ExtendedParameters = parameters;

			searchFilter.BrandId = brand.Id;

			// var list = CatalogService.GetProductCategoryListByBrandId(brand.Id);
			var list = CatalogService.GetProductCategoryListBySearch(searchFilter);
			ViewData.Model = list;
			return PartialView(viewName);
		}

		public ActionResult ShowProductCategoryListBySearch(string viewName, string search)
		{
			if (!search.IsNullOrTrimmedEmpty())
			{
				search = search.Trim();
				search = Noises.Clean("fr", search);
			}

			var parameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var searchFilter = CatalogService.CreateProductListFilter(HttpContext);
			searchFilter.ExtendedParameters = parameters;
			searchFilter.Search = search;

			var brand = CatalogService.GetBrandByName(Request["brand"]);
			if (brand != null)
			{
				searchFilter.BrandId = brand.Id;
			}

			var category = CatalogService.GetCategoryByCode(Request["category"]);
			if (category != null)
			{
				searchFilter.ProductCategoryId = category.Id;
			}

			var list = CatalogService.GetProductCategoryListBySearch(searchFilter);
			ViewData.Model = list;
			return PartialView(viewName);
		}

		public ActionResult ShowProductCategoryListByProductList(Models.ProductListFilter filter, string viewName)
		{
			if (!filter.Search.IsNullOrTrimmedEmpty())
			{
				filter.Search = filter.Search.Trim();
				filter.Search = Noises.Clean("fr", filter.Search);
			}

			var list = CatalogService.GetProductCategoryListBySearch(filter);
			ViewData.Model = list;
			return PartialView(viewName);
		}

		// Moteurs de recherche
		public ActionResult ShowSubCategories()
		{
			return new EmptyResult();
		}

		[HttpPost]
		public ActionResult ShowSubCategories(string productCategoryCode, string viewName)
		{
			var category = CatalogService.GetCategories().DeepFirst(i => i.Code == productCategoryCode);

			if (category == null)
			{
				return new EmptyResult();
			}
			ViewData.Model = category;
			return PartialView(viewName);
		}

		#endregion

		#endregion

		#region Brands

		[ActionFilters.TrackerActionFilter]
		public ActionResult Brand(string link, int? page, int? pageSize)
		{
			if (link.IsNullOrTrimmedEmpty())
			{
				return Redirect("/");
			}
			link = link.Replace("_", " ");
			var brand = CatalogService.GetBrandByLink(link);
			if (brand == null)
			{
				return Redirect("/");
			}
			if (!page.HasValue || page < 0)
			{
				page = 0;
			}
			else
			{
				page = page.Value - 1;
			}
			if (page < 0)
			{
				page = 0;
			}
			int count = 0;
			pageSize = pageSize.GetValueOrDefault(ERPStoreApplication.WebSiteSettings.Catalog.PageSize);
			var parameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var category = CatalogService.GetCategoryByCode(Request["category"]);

			var filter = CatalogService.CreateProductListFilter(HttpContext);
			if (category != null)
			{
				filter.ProductCategoryId = category.Id;
			}
			filter.ExtendedParameters = parameters;
			filter.BrandId = brand.Id;

			// var productList = CatalogService.GetProductListByBrandId(brand.Id, parameters, page.Value, pageSize.Value , out count);
			var productList = CatalogService.GetProductListBySearch(filter, page.Value, pageSize.Value, out count);

			// Recherche des tarifs client si celui-ci est connecté
			CatalogService.ApplyBestPrice(productList, User.GetUserPrincipal().CurrentUser);

			var pageIndex = page.Value + 1;

			var result = new Models.ProductList(productList);
			result.Brand = brand;
			result.ItemCount = count;
			result.PageIndex = pageIndex;
			result.PageSize = pageSize.Value;
			result.ExtendedParameters = parameters;
			result.Category = category;
			result.ListType = ERPStore.Models.ProductListType.Brand;

			ViewData.Model = result;
			return View();
		}

		[ActionFilters.TrackerActionFilter]
		public ActionResult BrandList()
		{
			var key = "BrandList";
			var list = CacheService[key] as IList<Models.Brand>;
			if (list == null)
			{
				list = CatalogService.GetBrands().OrderBy(i => i.Name).ToList();
				CacheService.Add(key, list, DateTime.Now.AddHours(1));
				Logger.Debug("Brand list in cache");
			}
			ViewData.Model = list;
			return View();
		}

		#region Partial rendering

		// [ActionFilters.PartialRenderActionFilter(Order=1)]
		// [ActionFilters.CacheActionFilter(CacheKey = "BrandList", Duration = 3600, VaryByParam="*")]
		public ActionResult ShowBrandListView(string brandViewName)
		{
			var list = CatalogService.GetBrands();
			ViewData.Model = list;
			return PartialView( brandViewName);
		}

		/// <summary>
		/// Shows the brand list forefront.
		/// </summary>
		/// <param name="viewName">Name of the view.</param>
		/// <param name="routeData">The route data.</param>
		/// <returns></returns>
		// [ActionFilters.PartialRenderActionFilter(Order=1)]
		// [ActionFilters.CacheActionFilter(CacheKey = "BrandListForefront", Duration = 3600)]
		public ActionResult ShowBrandListForefront(string viewName, System.Web.Routing.RouteData routeData)
		{
			var list = CatalogService.GetBrandListForefront().ToList();

			return PartialView(viewName, list);
		}

		// [ActionFilters.CacheActionFilter(CacheKey = "BrandListByProductCategory", Duration = 3600, VaryByParam="*")]
		// [OutputCache(Duration=3600, VaryByParam="*")]
		public ActionResult ShowBrandListByProductCategory(Models.ProductCategory productCategory, string viewName)
		{
			var parameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var searchFilter = CatalogService.CreateProductListFilter(HttpContext);

			searchFilter.ExtendedParameters = parameters;

			searchFilter.ProductCategoryId = productCategory.Id;

			var list = CatalogService.GetBrandListBySearch(searchFilter);
			ViewData.Model = list;
			return PartialView(viewName);
		}

		public ActionResult ShowBrandListBySearch(string viewName, string search)
		{
			if (!search.IsNullOrTrimmedEmpty())
			{
				search = search.Trim();
				search = Noises.Clean("fr", search);
			}

			var parameters = CatalogService.RemoveNotFilteredParameters(Request.Url.Query.ToHtmlDecodedNameValueDictionary());
			var searchFilter = CatalogService.CreateProductListFilter(HttpContext);

			searchFilter.ExtendedParameters = parameters;
			searchFilter.Search = search;

			var category = CatalogService.GetCategoryByCode(Request["category"]);
			if (category != null)
			{
				searchFilter.ProductCategoryId = category.Id;
			}

			var list = CatalogService.GetBrandListBySearch(searchFilter);
			ViewData.Model = list;
			return PartialView(viewName);
		}

		public ActionResult ShowBrandListByProductList(string viewName, Models.ProductListFilter filter)
		{
			if (!filter.Search.IsNullOrTrimmedEmpty())
			{
				filter.Search = filter.Search.Trim();
				filter.Search = Noises.Clean("fr", filter.Search);
			}

			var list = CatalogService.GetBrandListBySearch(filter);
			ViewData.Model = list;
			return PartialView(viewName);
		}

		#endregion

		#endregion

		#region Profession

		#region Partial Views

		public ActionResult ShowProfessionList(string viewName)
		{
			var list = CatalogService.GetProfessionList();
			ViewData.Model = list;
			return PartialView(viewName);
		}

		#endregion

		#endregion

		#region Search

		#region Partial Render

		// [ActionFilters.CacheActionFilter(CacheKey="ShowTopSearchTermList", Duration=3600, VaryByParam="*")]
		public ActionResult ShowTopSearchTermList(string viewName, int count)
		{
			var list = CatalogService.GetTopSearchTermList(count);
			ViewData.Model = list;
			return PartialView(viewName);
		}

		#endregion

		#endregion

		#region Helpers

		public Models.ProductStockInfo GetProductStockInfoByCode(string productCode)
		{
			string key = string.Format("ProductDisponibility:{0}", productCode);
			var stockInfo = CacheService[key] as Models.ProductStockInfo;
			if (stockInfo == null)
			{
				try
				{
					stockInfo = CatalogService.GetProductStockInfo(productCode);
				}
				catch (Exception ex)
				{
					Logger.Warn(ex.Message);
				}
				if (stockInfo == null)
				{
					stockInfo = new ERPStore.Models.ProductStockInfo();
					stockInfo.PhysicalStock = 0;
					stockInfo.ProvisionningDaysCount = -1;
				}
				CacheService.Add(key, stockInfo, DateTime.Now.AddMinutes(15));
			}
			return stockInfo;
        }

        #endregion

    }
}