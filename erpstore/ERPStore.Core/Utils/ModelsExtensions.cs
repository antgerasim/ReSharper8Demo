using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore
{
	public static class ModelsExtensions
	{
		public static string GetStatusText(this Models.OrderCart cart)
		{
			if (cart == null || cart.ItemCount == 0)
			{
				return "est vide";
			} 
			else if (cart.ItemCount == 1)
			{
				return "1 produit";
			}
			else
			{
				return string.Format("{0} produits", cart.ItemCount);
			}
		}

		public static IList<Models.Brand> GetBrandList(this IEnumerable<Models.Product> productList)
		{
			if (productList.IsNullOrEmpty())
			{
				return new List<Models.Brand>();
			}
			return productList.Where(i => i.Brand != null).Select(i => i.Brand).Distinct().OrderBy(i => i.Name).ToList();
		}

		public static IList<Models.ProductCategory> GetCategoryList(this IEnumerable<Models.Product> productList)
		{
			if (productList.IsNullOrEmpty())
			{
				return new List<Models.ProductCategory>();
			}
			var result = productList.Where(i => i.Category != null).Select(i => i.Category);
			if (result.IsNullOrEmpty())
			{
				return new List<Models.ProductCategory>();
			}
			return result.Distinct().OrderBy(i => i.Name).ToList();
		}

		public static Models.Media GetFirstMedia(this Models.ProductCategory category)
		{
			if (category == null)
			{
				return null;
			}
			if (category.DefaultImage != null)
			{
				return category.DefaultImage;
			}
			if (category.Parent != null)
			{
				return category.Parent.GetFirstMedia();
			}
			return null;
		}

		/// <summary>
		/// Permet de recuperer la liste des propriétés d'un groupe de propriété pour un proudit donné
		/// </summary>
		/// <param name="product">The product.</param>
		/// <param name="groupName">Name of the group.</param>
		/// <returns></returns>
		public static IEnumerable<string> GetPropertyNameList(this Models.Product product, string groupName)
		{
			return product.ExtendedProperties[groupName].Select(i => i.Key);
		}

		/// <summary>
		/// Permet de recuperer la liste des noms des groupes de propriété
		/// </summary>
		/// <param name="product">The product.</param>
		/// <returns></returns>
		public static IEnumerable<Models.PropertyGroup> GetPropertyGroupNameList(this Models.Product product)
		{
			return product.ExtendedProperties.Select(i => i.Key);
		}

		/// <summary>
		/// Localizes the specified product.
		/// </summary>
		/// <param name="product">The product.</param>
		/// <param name="language">The language.</param>
		/// <param name="propertyName">Name of the property.</param>
		/// <returns></returns>
		public static string Localize(this Models.Product product, Models.Language language, string propertyName)
		{
			var localization = product.LocalizationList.SingleOrDefault(i => i.Language == language && i.PropertyName.Equals(propertyName, StringComparison.InvariantCultureIgnoreCase));
			if (localization != null)
			{
				return localization.Value;
			}
			return product.GetPropertyValue(propertyName).ToString();
		}

		public static string GetLocalizedName(this Models.OrderStatus orderStatus)
		{
			switch (orderStatus)
			{
				case ERPStore.Models.OrderStatus.WaitingPayment:
					return "En attente de règlement";
				case ERPStore.Models.OrderStatus.PaymentAccepted:
					return "Règlement reçu, en attente de livraison";
				case ERPStore.Models.OrderStatus.PartialyDelivered:
					return "En cours de livraison partielle";
				case ERPStore.Models.OrderStatus.Delivered:
					return "Commande livrée";
				case ERPStore.Models.OrderStatus.Invoiced:
					return "Commande livrée et facturée";
				case ERPStore.Models.OrderStatus.WaitingProcess:
					return "En attente de livraison";
				default:
					return "A definir";
			}
		}

		public static string GetLocalizedName(this Models.UserPresentation presentation)
		{
			switch (presentation)
			{
				case ERPStore.Models.UserPresentation.Unkwnon:
					return string.Empty;
				case ERPStore.Models.UserPresentation.Mister:
					return "M";
				case ERPStore.Models.UserPresentation.Miss:
					return "Mme";
				case ERPStore.Models.UserPresentation.Misses:
					return "Mlle";
				default:
					return string.Empty;
			}
		}


		//public static string GetLocalizedName(this Models.PaymentMode paymentMode)
		//{
		//    switch (paymentMode)
		//    {
		//        case ERPStore.Models.PaymentMode.Paypal:
		//            return "Paypal";
		//        case ERPStore.Models.PaymentMode.Check:
		//            return "Cheque";
		//        case ERPStore.Models.PaymentMode.WireTransfer:
		//            return "Virement bancaire";
		//        case ERPStore.Models.PaymentMode.InAccount:
		//            return "En compte";
		//        case ERPStore.Models.PaymentMode.Ogone :
		//            return "Systeme de paiement par Carte Bleue (Ogone)";
		//        case ERPStore.Models.PaymentMode.Sogenactif:
		//            return "Système de paiement par Carte Bleue (Société générale)";
		//        default:
		//            return "A definir";
		//    }
		//}

		public static string GetLocalizedName(this bool condition)
		{
			if (condition)
			{
				return "Vrai";
			}
			else
			{
				return "Faux";
			}
		}

		public static string GetLocalizedName(this Models.CancelQuoteReason reason)
		{
			switch (reason)
			{
				case ERPStore.Models.CancelQuoteReason.TooExpensive:
					return "Trop cher";
				case ERPStore.Models.CancelQuoteReason.TooLong:
					return "Délai trop long";
				case ERPStore.Models.CancelQuoteReason.ProjectNotConcretized:
					return "Projet non concretisé";
				case ERPStore.Models.CancelQuoteReason.TradmarkNotRespected:
					return "Non respect des marques";
				case ERPStore.Models.CancelQuoteReason.AvailableQuantityInsufficient:
					return "Stock insuffisant";
				case ERPStore.Models.CancelQuoteReason.Other:
					return "Autre";
			}
			return "A definir";
		}

		public static string EncodedCode(this Models.Product product)
		{
			var code = product.Code.ToLower().Trim();
			code = code.Replace("/", "__");
			return code;
		}

		public static string EncodedLink(this Models.Product product)
		{
			var link = (product.Link.IsNullOrTrimmedEmpty()) ? SEOHelper.SEOUrlEncode(product.Title) : product.Link;
			return link.ToLower();
		}

        public static bool IsConverted(this Models.QuoteStatus status)
        {
            if (status == ERPStore.Models.QuoteStatus.ConvertedToOrder
				|| status == ERPStore.Models.QuoteStatus.ManualyConvertedToOrder)
            {
                return true;
            }
            return false;
        }

		public static bool IsWaiting(this Models.QuoteStatus status)
		{
			if (status == ERPStore.Models.QuoteStatus.Waiting
				|| status == ERPStore.Models.QuoteStatus.WaitingPayement)
			{
				return true;
			}
			return false;
		}

		public static string ToLocalizedName(this Models.QuoteStatus status)
		{
			if (status == ERPStore.Models.QuoteStatus.Waiting)
			{
				return "En cours";
			}
			else if (status == ERPStore.Models.QuoteStatus.WaitingPayement)
			{
				return "En attente de règlement";
			}
			else if (status == ERPStore.Models.QuoteStatus.ConvertedToOrder
				|| status == ERPStore.Models.QuoteStatus.ManualyConvertedToOrder)
			{
				return "Converti en commande";
			}
			else
			{
				return "Archivé";
			}
		}

		/// <summary>
		/// Retourne la catégorie raçine.
		/// </summary>
		/// <param name="category">The category.</param>
		/// <returns></returns>
		public static Models.ProductCategory GetRootProductCategory(this Models.ProductCategory category)
		{
			var result = category;
			if (result != null 
				&& result.Parent != null)
			{
				result = GetRootProductCategory(category.Parent);
			}
			return result;
		}

		/// <summary>
		/// Retourne la categorie racine en fonction de l'id d'une categorie
		/// </summary>
		/// <param name="list">The list.</param>
		/// <param name="categoryId">The category id.</param>
		/// <returns></returns>
		public static Models.ProductCategory GetRootProductCategory(this IEnumerable<Models.ProductCategory> list, int categoryId)
		{
			foreach (var category in list)
			{
				if (category.Id == categoryId)
				{
					return category;
				}
				else if (category.Parent != null)
				{
					var result = list.GetRootProductCategory(category.Parent.Id);
					if (result != null)
					{
						return result;
					}
				}
			}
			return null;
		}

		public static Models.ProductCategory DeepFindParent<T>(this IEnumerable<T> list, int parentId)
			where T : Models.ProductCategory
		{
			foreach (var item in list)
			{
				if (item.Id == parentId)
				{
					return item;
				}
				if (item.Children.Count() > 0)
				{
					var result = item.Children.DeepFindParent(parentId);
					if (result != null)
					{
						return result;
					}
				}
			}
			return null;
		}

		public static IEnumerable<T> DeepSelect<T>(this IEnumerable<Models.ProductCategory> list, Func<Models.ProductCategory, T> selector)
		{
			var result = list.Select(selector).ToList();
			foreach (var item in list)
			{
				if (!item.Children.IsNullOrEmpty())
				{
					var subList = item.Children.DeepSelect(selector);
					result.AddRange(subList);
				}
			}
			return result;
		}

		public static IList<T> ToFlatList<T>(this IEnumerable<T> list)
			where T : Models.ProductCategory
		{
			var result = new List<T>();

			foreach (var item in list)
			{
				result.Add(item);
				if (item.Children.Count() > 0)
				{
					var flatChildren = item.Children.ToFlatList();
					foreach (T subitem in flatChildren)
					{
						result.Add(subitem);
					}
					item.Children.Clear();
				}
			}

			return result;
		}

		public static Models.ProductCategory DeepFirst<T>(this IEnumerable<T> list, Predicate<Models.ProductCategory> predicate)
			where T : Models.ProductCategory
		{
			foreach (var item in list)
			{
				if (predicate.Invoke(item))
				{
					return item;
				}
				if (item.Children.Count() > 0)
				{
					var result = item.Children.DeepFirst(predicate);
					if (result != null)
					{
						return result;
					}
				}
			}
			return null;
		}

		public static Models.ProductCategory DeepFirstOrDefault<T>(this IEnumerable<T> list, Func<Models.ProductCategory, bool> predicate)
			where T : Models.ProductCategory
		{
			foreach (var item in list)
			{
				if (predicate.Invoke(item))
				{
					return item;
				}
				if (item.Children.Count() > 0)
				{
					var result = item.Children.DeepFirstOrDefault(predicate);
					if (result != null)
					{
						return result;
					}
				}
			}
			return default(T);
		}

		public static void ActionToRoot<T>(this IEnumerable<T> list, Models.ProductCategory leaf, Action<Models.ProductCategory> action)
			where T : Models.ProductCategory
		{
			action.Invoke(leaf);
			if (leaf.Parent != null)
			{
				list.ActionToRoot(leaf.Parent, action);
			}
		}

		public static void DeepRemoveAll<T>(this IList<T> list, Func<Models.ProductCategory, bool> predicate)
			where T : Models.ProductCategory
		{
			while (true)
			{
				var first = list.DeepFirstOrDefault(predicate);
				if (first == null)
				{
					break;
				}
				if (first.Parent != null)
				{
					first.Parent.Children.Remove(first);
				}
				else
				{
					list.Remove((T)first);
				}
			}
		}

		public static IList<T> ToClonedList<T>(this IEnumerable<T> list)
			where T : ICloneable
		{
			var clonedList = new List<T>();
			foreach (var item in list)
			{
				clonedList.Add((T)item.Clone());
			}
			return clonedList;
		}

		public static int DeepProductCount<T>(this IEnumerable<T> list)
			where T : Models.ProductCategory
		{
			var result = 0;
			if (list.IsNotNullOrEmpty())
			{
				foreach (var item in list)
				{
					result += item.ProductCount + DeepProductCount(item.Children);
				}
			}
			return result;
		}

		public static void Traverse<T>(this IEnumerable<T> list, Action<Models.ProductCategory> action)
			where T : Models.ProductCategory
		{
			foreach (var item in list)
			{
				action.Invoke(item);
				if (item.Children.IsNotNullOrEmpty())
				{
					item.Children.Traverse(action);
				}
			}
		}

		public static void Hierarchize<T>(this IList<T> list)
			where T : Models.ProductCategory
		{
			var flatList = list.OrderBy(i => i.Level).ToList();
			list.Clear();
			while (true)
			{
				// Premier de la liste plate
				var first = flatList.FirstOrDefault();

				if (first == null)
				{
					break;
				}

				// Recherche du parent
				if (first.Parent == null)
				{
					list.Add(first);
				}
				else
				{
					var parent = list.DeepFindParent(first.Parent.Id);
					if (parent != null)
					{
						parent.Children.Add(first);
					}
				}

				// suppression on passe au suivant
				flatList.Remove(first);
			}
		}

		public static string ToLocalizedName(this Models.InvoiceStatus status)
		{
			switch (status)
			{
				case ERPStore.Models.InvoiceStatus.Unrecovered:
					return "Non reglée";
				case ERPStore.Models.InvoiceStatus.PartiallyRecovered:
					return "Reglement partiel";
				case ERPStore.Models.InvoiceStatus.FullyRecovered:
					return "Totalement reglée";
				case ERPStore.Models.InvoiceStatus.PartiallyLost:
				case ERPStore.Models.InvoiceStatus.FullyLost:
					return "Perte";
				case ERPStore.Models.InvoiceStatus.Processing:
					return "Reglement en traitement";
			}
			return "Inconnu";
		}

		public static int GetPageCount(this Models.IPaginable pager)
		{
			return Convert.ToInt32(Math.Ceiling(pager.ItemCount / (pager.PageSize * 1.0)));
		}


	}
}
