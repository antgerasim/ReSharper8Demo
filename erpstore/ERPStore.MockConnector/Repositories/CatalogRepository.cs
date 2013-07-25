using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.MockConnector.Repositories
{
	public class CatalogRepository
	{
		public IEnumerable<Models.ProductCategory> GetAllCategories()
		{
			yield return new Models.ProductCategory()
			{
				Code = "Laptops",
				PageDescription = "Laptops",
				Id = 1,
				IsForefront = true,
				Keywords = "Laptops",
				Link = "laptops",
				Name = "Laptops",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Laptop Accessories",
				PageDescription = "Laptop Accessories",
				Id = 2,
				IsForefront = true,
				Keywords = "Laptop, Accessories",
				Link = "laptop_accessories",
				Name = "Laptop Accessories",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Desktop Computers",
				PageDescription = "Desktop Computers",
				Id = 3,
				IsForefront = true,
				Keywords = "Desktop, Computers",
				Link = "desktop_computers",
				Name = "Desktop Computers",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Apple",
				PageDescription = "Apple",
				Id = 4,
				IsForefront = true,
				Keywords = "Apple",
				Link = "apple",
				Name = "Apple",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Monitors",
				PageDescription = "Monitors",
				Id = 5,
				IsForefront = true,
				Keywords = "Monitors",
				Link = "monitors",
				Name = "Monitors",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Printers",
				PageDescription = "Printers",
				Id = 6,
				IsForefront = true,
				Keywords = "Printers",
				Link = "printers",
				Name = "Printers",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Drives & Storage",
				PageDescription = "Drives & Storage",
				Id = 7,
				IsForefront = true,
				Keywords = "Drives, Storage",
				Link = "drives_storage",
				Name = "Drives & Storage",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Networking",
				PageDescription = "Networking",
				Id = 8,
				IsForefront = true,
				Keywords = "Networking",
				Link = "networking",
				Name = "Networking",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Software",
				PageDescription = "Software",
				Id = 9,
				IsForefront = true,
				Keywords = "Software",
				Link = "software",
				Name = "Software",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Computer Upgrades",
				PageDescription = "Computer Upgrades",
				Id = 10,
				IsForefront = true,
				Keywords = "Computer, Upgrades",
				Link = "computer_upgrades",
				Name = "Computer Upgrades",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Computer Speakers",
				PageDescription = "Computer Speakers",
				Id = 11,
				IsForefront = true,
				Keywords = "Computer, Speakers",
				Link = "computer_speakers",
				Name = "Computer Speakers",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Mice & Keyboards",
				PageDescription = "Mice & Keyboards",
				Id = 12,
				IsForefront = true,
				Keywords = "Mice,Keyboards",
				Link = "mice_and_keyboards",
				Name = "Mice & Keyboards",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Webcams",
				PageDescription = "Webcams",
				Id = 13,
				IsForefront = true,
				Keywords = "Webcams",
				Link = "webcams",
				Name = "Webcams",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Computer Accessories",
				PageDescription = "Computer Accessories",
				Id = 14,
				IsForefront = true,
				Keywords = "Computer, Accessories",
				Link = "computer_accessories",
				Name = "Computer Accessories",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Media",
				PageDescription = "Media",
				Id = 15,
				IsForefront = true,
				Keywords = "Media",
				Link = "media",
				Name = "Media",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Projects",
				PageDescription = "Projects",
				Id = 16,
				IsForefront = true,
				Keywords = "Projects",
				Link = "projects",
				Name = "Projects",
				ProductCount = 0,
			};

			yield return new Models.ProductCategory()
			{
				Code = "Scanners",
				PageDescription = "Scanners",
				Id = 17,
				IsForefront = true,
				Keywords = "Scanners",
				Link = "scanners",
				Name = "Scanners",
				ProductCount = 0,
			};

			yield break;
		}

		public IEnumerable<Models.Brand> GetAllBrands()
		{ 
			yield return new ERPStore.Models.Brand()
				{
					ExternalBrandLink = "http://www.microsoft.com",
					Id = 1,
					Link = "microsoft",
					Name = "Microsoft",
					PageDescription = "Microsoft",
					Keywords = "Microsoft",
					PageTitle = "Micrososft",
				};

			yield return new ERPStore.Models.Brand()
				{
					ExternalBrandLink = "http://www.logitech.com",
					Id = 2,
					Link = "logitech",
					Name = "Logitech",
					PageDescription = "Logitech",
					Keywords = "Logitech",
					PageTitle = "Logitech",
				};

			yield break;
		}

		public IEnumerable<Models.Product> GetAllProducts()
		{
			yield return new ERPStore.Models.Product
				{
					Brand = GetAllBrands().First(),
					ManufacturerUrl = "",
					Category = GetAllCategories().First(),
					Code = "XBOX360",
					CreationDate = DateTime.Now,
					DefaultImage = new ERPStore.Models.Media()
					{
						ExternalUrl = null,
						Id = "1",
						LastUpdate = DateTime.Now,
						MimeType = "image/jpg",
						Url = "/content/images/xbox360.jpg"
					},
					DefaultTaxRate = 0.196,
					Id = 1,
					IsDestock = false,
					IsFirstPrice = false,
					IsNew = true,
					IsPromotion = false,
					IsTopSell = false,
					Keywords = "xbox, 360",
					Link = "Xbox_360_Arcade",
					LongDescription = "The Xbox 360 Arcade console is everything you need to hit the ground running. Plug in the console and connect the wireless controller and you're playing.",
					MarketPrice = null,
					MinimumSaleQuantity = 1,
					OrderCount = 5,
					Packaging = new ERPStore.Models.Packaging() { Value = 1 },
					PageDescription = "xbox360",
					PageTitle = "XBOX 360",
					PromotionExpirationDate = DateTime.Now.AddDays(2),
					SalePrice = new ERPStore.Models.Price(199.99, 0.196),
					SaleUnitValue = 1,
					ShortDescription = "The Xbox 360 Arcade console is everything you need",
					StrengthsPoints = null,
					Title = "Xbox 360 Arcade",
					Weight = 150,
					Metric = Models.Metric.Unit,
				};

			yield return new ERPStore.Models.Product
				{
					Brand = GetAllBrands().First(),
					ManufacturerUrl = "",
					Category = GetAllCategories().First(),
					Code = "XBOXWC",
					CreationDate = DateTime.Now,
					DefaultImage = new ERPStore.Models.Media()
					{
						ExternalUrl = null,
						Id = "2",
						LastUpdate = DateTime.Now,
						MimeType = "image/jpg",
						Url = "/content/images/Xbox360WirelessController.jpg"
					},
					DefaultTaxRate = 0.196,
					Id = 2,
					IsDestock = false,
					IsFirstPrice = false,
					IsNew = true,
					IsPromotion = false,
					IsTopSell = false,
					Keywords = "xbox, wireless, controller",
					Link = "Xbox_360_Wireless_Controller",
					LongDescription = "High-performance wireless gaming now comes in black! Using optimized technology, the black Xbox 360 Wireless Controller lets you enjoy a 30-foot range and up to 40 hours of life on the two included AA batteries.",
					MarketPrice = null,
					MinimumSaleQuantity = 1,
					OrderCount = 5,
					Packaging = new ERPStore.Models.Packaging() { Value = 1 },
					PageDescription = "xbox360",
					PageTitle = "Xbox 360 Wireless controller",
					PromotionExpirationDate = DateTime.Now.AddDays(2),
					SalePrice = new ERPStore.Models.Price(199.99, 0.196),
					SaleUnitValue = 1,
					ShortDescription = "High-performance wireless gaming now comes in black!",
					StrengthsPoints = null,
					Title = "Xbox 360 Wireless controller",
					Weight = 150,
					Metric = Models.Metric.Unit,
				};

			yield return new ERPStore.Models.Product
			{
				Brand = GetAllBrands().First(),
				ManufacturerUrl = "",
				Category = GetAllCategories().Last(),
				Code = "DUPTITLE",
				CreationDate = DateTime.Now,
				DefaultImage = new ERPStore.Models.Media()
				{
					ExternalUrl = null,
					Id = "2",
					LastUpdate = DateTime.Now,
					MimeType = "image/jpg",
					Url = "/content/images/Xbox360WirelessController.jpg"
				},
				DefaultTaxRate = 0.196,
				Id = 2,
				IsDestock = false,
				IsFirstPrice = false,
				IsNew = true,
				IsPromotion = false,
				IsTopSell = false,
				Keywords = "xbox, wireless, controller",
				Link = "Xbox_360_Wireless_Controller",
				LongDescription = null,
				MarketPrice = null,
				MinimumSaleQuantity = 1,
				OrderCount = 5,
				Packaging = new ERPStore.Models.Packaging() { Value = 1 },
				PageDescription = "xbox360",
				PageTitle = "Xbox 360 Wireless controller",
				PromotionExpirationDate = DateTime.Now.AddDays(2),
				SalePrice = new ERPStore.Models.Price(199.99, 0.196),
				SaleUnitValue = 1,
				ShortDescription = "Xbox 360 Wireless Controller",
				StrengthsPoints = null,
				Title = "Xbox 360 Wireless controller",
				Weight = 150,
				Metric = Models.Metric.Unit,
			};

			yield break;

		}

		public IEnumerable<Models.ProductStockInfo> GetAllProductStockInfo()
		{
			var products = GetAllProducts();

			foreach (var product in products)
			{
				yield return new Models.ProductStockInfo()
				{
					ProductCode = product.Code,
					DeliveryDaysCount = 2,
					MinimalQuantity = product.MinimumSaleQuantity,
					MostProvisionningDate = null,
					PhysicalStock = product.MinimumSaleQuantity,
					ProvisionnedQuantity = 0,
					ProvisionningDaysCount = 2,
					ReservedQuantity = 0,
				};
			}

			yield break;
		}
	}
}
