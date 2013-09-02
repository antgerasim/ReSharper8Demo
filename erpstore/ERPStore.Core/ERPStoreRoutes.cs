using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;

using ERPStore.Html;

namespace ERPStore
{
	/// <summary>
	/// Service d'enregistrement des routes de base
	/// </summary>
	public class ERPStoreRoutes : Services.IRoutesRegistrationService
	{
		private RouteCollection m_Routes;

		public ERPStoreRoutes()
		{
			this.m_Routes = System.Web.Routing.RouteTable.Routes;
		}

		#region  Account Routes

		/// <summary>
		/// Page d'accueil de gestion du compte
		/// </summary>
		public const string ACCOUNT = "Account";
		/// <summary>
		/// Deconnexion
		/// </summary>
		public const string LOGOFF = "Logoff";
		public const string COMPLETE_ACCOUNT = "CompleteAccount";
		public const string ACCOUNT_CONFIRMATION = "AccountConfirmation";
		public const string EDIT_ADDRESS = "EditAddress";
		public const string ACCOUNT_EDIT_ADDRESS = "AccountEditAddress";
		public const string AJAX_EDIT_ADDRESS = "AjaxEditAddress";
		public const string ACCOUNT_DELETE_ADDRESS = "AccountDeleteAddress";
		public const string ACCOUNT_RECOVER_PASSWORD = "AccountRecoverPassword";
		public const string ACCOUNT_CHANGE_PASSWORD = "AccountChangePassword";
		public const string EDIT_PASSWORD = "EditPassword";
		public const string ACCOUNT_LOGIN = "AccountLogin";
		public const string EDIT_USER = "EditUser";
		public const string EDIT_CORPORATE = "EditCorporate";
		public const string EDIT_ADDRESS_LIST = "EditAddressList";
		public const string USER_DASHBOARD = "UserDashboard";
		public const string DEFAULT_ACCOUNT = "DefaultAccount";

		#endregion

		#region Registration Routes

		public const string REGISTER_ACCOUNT = "RegisterAccount";
		public const string REGISTER_ACCOUNT_CORPORATE = "RegisterAccountCorporate";
		public const string REGISTER_BILLING_ADDRESS = "RegisterAccountBillingAddress";
		public const string REGISTER_ACCOUNT_CONFIRMATION = "RegisterAccountConfirmation";
		public const string REGISTER_ACCOUNT_FINALIZED = "RegisterAccountFinalized";

		#endregion

		#region Catalog Routes

		public const string PRODUCT = "Product";
		public const string PRODUCT_SEARCH = "ProductSearch";
		public const string SEO_PRODUCT_SEARCH = "SEOProductSearch";
		public const string CUSTOMER_PRODUCT = "CustomerProduct";
		public const string PRODUCT_CATEGORIES = "ProductCategories";
		public const string PRODUCT_BY_CATEGORY = "ProductByCategory";
		public const string PRODUCT_BY_SELECTION = "ProductBySelection";
		public const string PRODUCT_BY_BRAND = "ProductByBrand";
		public const string BRANDS = "Brands";
		public const string CROSS_PRODUCT_LIST = "CrossProductList";
		public const string SIMILAR_PRODUCT_LIST = "SimilarProductList";
		public const string COMPLEMENTARY_PRODUCT_LIST = "ComplementaryProductList";
		public const string VARIANT_PRODUCT_LIST = "VariantProductList";
		public const string SUBSTITUTE_PRODUCT_LIST = "SubstituteProductList";
		public const string RELATIONS_PRODUCT_LIST = "RelationsProductList";
		public const string CONTEXTUAL_PRODUCT_LIST = "ContextualProductList";
		public const string TOP_SELLS = "TopSells";
		public const string NEW_PRODUCTS = "NewProducts";
		public const string PROMOTIONS = "Promotions";
		public const string DESTOCK = "Destock";
		public const string FIRST_PRICE = "FirstPrice";
		public const string DEFAULT_CATALOG = "DefaultCatalog";
		public const string PRODUCT_STOCK_INFO = "ShowProductStockInfo";
		public const string PRODUCT_SUB_CATEGORIES = "ProductSubCategories";

		#endregion

		#region Checkout Routes

		public const string CHECKOUT = "Checkout";
		public const string CHECKOUT_CONFIGURATION = "CheckoutConfiguration";
		public const string CHECKOUT_PAYMENT = "CheckoutPayment";
		public const string CHECKOUT_CONFIRMATION = "CheckoutConfirmation";
		public const string CHECKOUT_DIRECT_CONFIRMATION = "CheckoutDirectConfirmation";
		public const string CHECKOUT_FINALIZE = "CheckoutFinalize";
		public const string CHECKOUT_FINALIZED = "CheckoutFinalized";

		#endregion

		#region Sales Routes

		#region Commandes

		public const string ORDER_DETAIL = "OrderDetail";
		public const string AJAX_ORDER_LIST = "AjaxOrderList";
		public const string ORDER_LIST = "OrderList";
		public const string EDIT_ORDER = "EditOrder";
		public const string ADD_COMMENT_TO_ORDER = "AddCommentToOrder";
		public const string SHOW_LAST_ORDER = "ShowLastOrder";
		public const string DEFAULT_ORDER = "DefaultOrder";

		#endregion

		#region Devis

		public const string ADD_COMMENT_TO_QUOTE = "AddCommentToQuote";
		public const string QUOTE_DETAIL = "QuoteDetail";
		public const string ACCEPT_QUOTE_CONFIRMATION = "AcceptQuoteConfirmation";
		public const string ACCEPT_QUOTE = "AcceptQuote";
		public const string QUOTE_LIST = "QuoteList";
		public const string AJAX_QUOTE_LIST = "AjaxQuoteList";
		public const string CANCEL_QUOTE = "CancelQuote";
		public const string DEFAULT_QUOTE = "DefaultQuote";

		#endregion

		#region Factures

		public const string INVOICE_DETAIL = "InvoiceDetail";
		public const string INVOICE_LIST = "InvoiceList";
		public const string AJAX_INVOICE_LIST = "AjaxInvoiceList";

		#endregion

		#endregion

		#region Media Routes 

		public const string IMAGES = "Images";
		public const string DOWNLOAD = "Download";
		public const string ROUNDED_CORNER = "RoundedCorner";
		public const string DOCUMENT_DOWNLOAD = "DocumentDownload";
		public const string DOCUMENT = "Document";

		#endregion

		#region Site Routes

		public const string HOME = "Home";
		public const string ABOUT = "About";
		public const string TERMS_AND_CONDITIONS = "TermsAndConditions";
		public const string LEGAL_INFORMATION = "LegalInformation";
		public const string HELP = "Help";
		public const string CONTACT = "Contact";
        public const string AJAXCONTACT = "AjaxContact";
        public const string ERROR_500 = "Error500";
		// public const string ERROR_404 = "Error404";
		// public const string ERROR_403 = "Error403";
		public const string STATIC_PAGE = "StaticPage";
		public const string EMAILER = "Emailer";
		public const string HIDE_REFERER = "HideReferer";
		public const string CLEAR_COOKIE = "ClearCookie";

		#endregion

		#region Order Cart

		public const string CART_ADD = "CartAdd";
		public const string CART_RECALC = "CartRecalc";
		public const string CART_ITEM_DELETE = "CartItemDelete";
		public const string CART_CLEAR = "CartClear";
		public const string CART_DELETE = "CartDelete";
		public const string CART_CHANGE = "CartChange";
		public const string CART_SHOW = "CartShow";
		public const string CART_ASSIGN = "CartAssign";
		public const string CART_CREATE = "CartCreate";
		public const string SHOW_ADD_TO_CART_POPUP = "ShowAddToCartPopup";
		public const string SHOW_CART_STATUS = "ShowCartStatus";
		public const string AJAX_ADD_TO_CART = "AjaxAddToCart";
		public const string CART_CONVERT_TO_QUOTE = "CartConvertToQuoteCart";
		public const string CART_SCRIPT = "CartScript";
		public const string CART = "Cart";
		public const string DEFAULT_CART = "DefaultCart";
		public const string LAST_CART_ITEM_LIST = "LastCartItemList";

		#endregion

		#region Quote Cart

		public const string QUOTECART_ADD = "QuoteCartAdd";
		// public const string QUOTECART_RECALC = "QuoteCartRecalc";
		public const string QUOTECART_ITEM_DELETE = "QuoteCartItemDelete";
		public const string QUOTECART_CLEAR = "QuoteCartClear";
		public const string QUOTECART_DELETE = "QuoteCartDelete";
		public const string QUOTECART_CHANGE = "QuoteCartChange";
		public const string QUOTECART_SHOW = "QuoteCartShow";
		public const string QUOTECART_CREATE = "QuoteCartCreate";
		public const string QUOTECART_ADD_ITEM = "QuoteAddItem";
		public const string JS_QUOTECART_ADD_ITEM = "QuoteJsAddItem";
		public const string JS_QUOTECART_ADD_ITEM_WITH_QUANTITY = "QuoteJsAddItemWithQuantity";
		public const string QUOTECART_STATUS = "QuoteStatus";
		public const string SHOW_ADD_TO_QUOTECART_POPUP = "ShowAddToQuoteCartPopup";
		public const string SHOW_QUOTECART_STATUS = "ShowQuoteCartStatus";
		public const string AJAX_ADD_TO_QUOTECART = "AjaxAddToQuoteCart";
		public const string QUOTECART_SCRIPT = "QuoteCartScript";
		public const string QUOTECART = "QuoteCart";
		public const string QUOTECART_SENT = "QuoteSent";
		public const string DEFAULT_QUOTECART = "DefaultQuoteCart";

		#endregion

		#region Routes

		public virtual void Register()
		{
			RegisterRegistrationRoutes();
			RegisterAccountRoutes();
			RegisterOrderCartRoutes();
			RegisterQuoteCartRoutes();
			RegisterCatalogRoutes();
			RegisterCheckoutRoutes();
			RegisterSalesRoutes();
			RegisterMediasRoutes();
			RegisterSiteRoutes();
		}

		protected virtual void RegisterOrderCartRoutes()
		{
			m_Routes.MapERPStoreRoute(
				CART_ADD
				, "panier/ajouter/{productCode}"
				, new { controller = "Cart", action = "AddCartItem", productCode = string.Empty  }
			);

			m_Routes.MapERPStoreRoute(
				CART_RECALC
				, "panier/recalculer"
				, new { controller = "Cart", action = "Recalc" }
			);

			m_Routes.MapERPStoreRoute(
				CART_ITEM_DELETE
				,  "panier/supprimer/{index}" 
				, new { controller = "Cart", action = "Remove", index = 0 }
				, new { index = @"\d+" }
			);

			m_Routes.MapERPStoreRoute(
				CART_CLEAR
				, "panier/vider" 
				, new { controller = "Cart", action = "Clear" }
			);

			m_Routes.MapERPStoreRoute(
				CART_DELETE
				, "panier/supprimer/{cartId}" 
				, new { controller = "Cart", action = "Delete", cartId = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				CART_CHANGE
				, "panier/changer/{cartId}" 
				, new { controller = "Cart", action = "Change", cartId = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				CART_SHOW
				, "panier/voir/{cartId}" 
				, new { controller = "Cart", action = "Show", cartId = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				CART_ASSIGN
				, "panier/assigne/{cartId}" 
				, new { controller = "Cart", action = "Assign" }
			);

			m_Routes.MapERPStoreRoute(
				CART_CREATE
				, "panier/creation" 
				, new { controller = "Cart", action = "Create" }
			);

			m_Routes.MapERPStoreRoute(
				SHOW_ADD_TO_CART_POPUP
				, "panier/popup-ajout"
				, new { controller = "Cart", action = "ShowAddToCart" }
			);

			m_Routes.MapERPStoreRoute(
				SHOW_CART_STATUS
				, "panier/statut" 
				, new { controller = "Cart", action = "ShowStatus" }
			);

			m_Routes.MapERPStoreRoute(
				AJAX_ADD_TO_CART
				, "panier/ajax-ajouter" 
				, new { controller = "Cart", action = "JsAddItemWithQuantity" }
			);

			m_Routes.MapERPStoreRoute(
				CART_SCRIPT
				, "panier/script" 
				, new { controller = "Cart", action = "Script" }
			);

			m_Routes.MapERPStoreRoute(
				CART_CONVERT_TO_QUOTE
				, "panier/conversion-devis/{cartId}" 
				, new { controller = "Cart", action = "ConverToQuoteCart", cartId = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				CART
				, "panier" 
				, new { controller = "Cart", action = "Index", id = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				LAST_CART_ITEM_LIST
				, "panier/derniers-produits"
				, new { controller = "Cart", action = "ShowLastCartItemList" }
			);

			m_Routes.MapERPStoreRoute(
				DEFAULT_CART
				, "panier/{action}/{id}" 
				, new { controller = "Cart", action = "Index", id = "" }  // Parameter defaults
			);
		}

		protected virtual void RegisterQuoteCartRoutes()
		{
			m_Routes.MapERPStoreRoute(
				QUOTECART_ADD
				, "panier-devis/ajouter" 
				, new { controller = "QuoteCart", action = "AddItem" }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART_ITEM_DELETE
				, "panier-devis/supprimer/{index}" 
				, new { controller = "QuoteCart", action = "Remove", index = 0 }
				, new { index = @"\d+" }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART_CLEAR
				, "panier-devis/vider" 
				, new { controller = "QuoteCart", action = "Clear" }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART_DELETE
				, "panier-devis/supprimer/{cartId}" 
				, new { controller = "QuoteCart", action = "Delete", cartId = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART_CHANGE
				, "panier-devis/changer/{cartId}" 
				, new { controller = "QuoteCart", action = "Change", cartId = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART_SHOW
				, "panier-devis/voir/{cartId}" 
				, new { controller = "QuoteCart", action = "Show", cartId = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART_CREATE
				, "panier-devis/creation" 
				, new { controller = "QuoteCart", action = "Create" }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART_ADD_ITEM
				, "panier-devis/ajouter/{productCode}" 
				, new { controller = "QuoteCart", action = "AddItem", productCode = string.Empty }
			);

			RouteTable.Routes.MapERPStoreRoute(
				JS_QUOTECART_ADD_ITEM
				, "panier-devis/jsadd/{productCode}"
				, new { controller = "QuoteCart", action = "JsAdd", productCode = string.Empty }
			);

			RouteTable.Routes.MapERPStoreRoute(
				JS_QUOTECART_ADD_ITEM_WITH_QUANTITY
				, "panier-devis/jsadd/{quantity}/{productCode}"
				, new { controller = "QuoteCart", action = "JsAddItemWithQuantity", quantity = 1, productCode = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART_STATUS
				, "panier-devis/status" 
				, new { controller = "QuoteCart", action = "ShowStatus" }
			);

			m_Routes.MapERPStoreRoute(
				SHOW_ADD_TO_QUOTECART_POPUP
				, "panier-devis/popup-ajout" 
				, new { controller = "QuoteCart", action = "ShowAddToCart" }
			);

			m_Routes.MapERPStoreRoute(
				SHOW_QUOTECART_STATUS
				, "panier-devis/statut"
				, new { controller = "QuoteCart", action = "ShowStatus" }
			);

			m_Routes.MapERPStoreRoute(
				AJAX_ADD_TO_QUOTECART
				, "panier-devis/ajax-ajouter" 
				, new { controller = "QuoteCart", action = "JsAddItemWithQuantity" }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART_SCRIPT
				, "panier-devis/script" 
				, new { controller = "QuoteCart", action = "Script" }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART
				, "panier-devis" 
				, new { controller = "QuoteCart", action = "Index", id = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				QUOTECART_SENT
				, "panier-devis/transmis" 
				, new { controller = "QuoteCart", action = "QuoteSent" }
			);

			m_Routes.MapERPStoreRoute(
				DEFAULT_QUOTECART
				, "panier-devis/{action}/{id}" 
				, new { controller = "QuoteCart", action = "Index", id = "" }  // Parameter defaults
			);
		}

		protected virtual void RegisterAccountRoutes()
		{
			m_Routes.MapERPStoreRoute(
				ACCOUNT
				, "compte" 
				, new { controller = "Account", action = "Index", id = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				LOGOFF
				, "compte/deconnexion" 
				, new { controller = "Account", action = "Logoff" }
			);

			m_Routes.MapERPStoreRoute(
				COMPLETE_ACCOUNT
				, "compte/completer" 
				, new { controller = "Account", action = "Complete" }
			);

			m_Routes.MapERPStoreRoute(
				ACCOUNT_CONFIRMATION
				, "compte/confirmation/{*key}" 
				, new { controller = "Account", action = "Confirmation", key = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				EDIT_ADDRESS
				, "adresse/edition" 
				, new { controller = "Account", action = "EditAddress" }
			);

			m_Routes.MapERPStoreRoute(
				ACCOUNT_EDIT_ADDRESS
				, "compte/adresse/edition" 
				, new { controller = "Account", action = "EditUserAddress" }
			);

			m_Routes.MapERPStoreRoute(
				AJAX_EDIT_ADDRESS
				, "compte/adresse/ajax-edition" 
				, new { controller = "Account", action = "AjaxEditAddress" }
			);

			m_Routes.MapERPStoreRoute(
				ACCOUNT_DELETE_ADDRESS
				, "compte/adresse/supprimer" 
				, new { controller = "Account", action = "DeleteUserAddress" }
			);

			m_Routes.MapERPStoreRoute(
				ACCOUNT_RECOVER_PASSWORD
				, "compte/retrouver-motdepasse" 
				, new { controller = "Account", action = "RecoverPassword" }
			);

			m_Routes.MapERPStoreRoute(
				ACCOUNT_CHANGE_PASSWORD
				, "compte/modifier/motdepasse/{*key}" 
				, new { controller = "Account", action = "ChangePassword" }
			);

			m_Routes.MapERPStoreRoute(
				EDIT_PASSWORD
				, "compte/editer-motdepasse" 
				, new { controller = "Account", action = "EditPassword" }
			);

			m_Routes.MapERPStoreRoute(
				ACCOUNT_LOGIN
				, "compte/connexion" 
				, new { controller = "Account", action = "Login" }
			);

			m_Routes.MapERPStoreRoute(
				EDIT_USER
				, "compte/edition" 
				, new { controller = "Account", action = "EditUser" }
			);

			m_Routes.MapERPStoreRoute(
				EDIT_CORPORATE
				, "compte/edition-societe" 
				, new { controller = "Account", action = "EditCorporate" }
			);

			m_Routes.MapERPStoreRoute(
				EDIT_ADDRESS_LIST
				, "compte/edition-adresses" 
				, new { controller = "Account", action = "EditAddressList" }
			);

			m_Routes.MapERPStoreRoute(
				USER_DASHBOARD
				, "compte/tableau-de-bord" 
				, new { controller = "Account", action = "Dashboard" }
			);

			m_Routes.MapERPStoreRoute(
				DEFAULT_ACCOUNT
				, "compte/{action}/{id}" 
				, new { controller = "Account", action = "Index", id = string.Empty }
			);

			m_Routes.MapRoute(
				CLEAR_COOKIE
				, "clearcookie"
				, new { controller = "Account", action = "ClearCookie" }
			);
		}

		protected virtual void RegisterRegistrationRoutes()
		{
			m_Routes.MapERPStoreRoute(
				REGISTER_ACCOUNT
				, "compte/enregistrement"
				, new { controller = "Registration", action = "Register" }
			);

			m_Routes.MapERPStoreRoute(
				REGISTER_ACCOUNT_CORPORATE
				, "compte/enregistrement/societe"
				, new { controller = "Registration", action = "RegisterCorporate" }
			);

			m_Routes.MapERPStoreRoute(
				REGISTER_BILLING_ADDRESS
				, "compte/enregistrement/adresse"
				, new { controller = "Registration", action = "RegisterBillingAddress" }
			);

			m_Routes.MapERPStoreRoute(
				REGISTER_ACCOUNT_CONFIRMATION
				, "compte/enregistrement/confirmation"
				, new { controller = "Registration", action = "RegisterConfirmation" }
			);

			m_Routes.MapERPStoreRoute(
				REGISTER_ACCOUNT_FINALIZED
				, "compte/enregistrement/merci"
				, new { controller = "Registration", action = "RegisterFinalized" }
			);
		}

		protected virtual void RegisterCatalogRoutes()
		{
			m_Routes.MapERPStoreRoute(
				PRODUCT
				, "produit/{code}/{*name}" 
				, new { controller = "Catalog", action = "Product" }
			);

			m_Routes.MapERPStoreRoute(
				PRODUCT_SEARCH
				, "catalogue/recherche" 
				, new { controller = "Catalog", action = "Search" }
			);

			m_Routes.MapERPStoreRoute(
				SEO_PRODUCT_SEARCH
				, "catalogue/r/{*s}" 
				, new { controller = "Catalog", action = "DirectSearch", s = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				CUSTOMER_PRODUCT
				, "catalogue/mes-produits" 
				, new { controller = "Catalog", action = "CustomerProduct" }
			);

			m_Routes.MapERPStoreRoute(
				PRODUCT_SUB_CATEGORIES
				, "subcategories"
				, new { controller = "Catalog", action = "ShowSubCategories" }
			);

			m_Routes.MapERPStoreRoute(
				PRODUCT_CATEGORIES
				, "categories" 
				, new { controller = "Catalog", action = "CategoryList" }
			);

			m_Routes.MapERPStoreRoute(
				PRODUCT_BY_CATEGORY
				, "categorie/{*link}" 
				, new { controller = "Catalog", action = "Category" }
			);

			m_Routes.MapERPStoreRoute(
				PRODUCT_BY_BRAND
				, "marque/{*link}"
				, new { controller = "Catalog", action = "Brand" }
			);

			m_Routes.MapERPStoreRoute(
				BRANDS
				, "marques" 
				, new { controller = "Catalog", action = "BrandList" }
			);

			m_Routes.MapERPStoreRoute(
				CROSS_PRODUCT_LIST
				, "catalogue/liste-produits-lies"
				, new { controller = "Catalog", action = "ShowCrossProductList" }
			);

			m_Routes.MapERPStoreRoute(
				SIMILAR_PRODUCT_LIST
				, "catalogue/liste-produits-similaires"
				, new { controller = "Catalog", action = "ShowRelationalProductList" }
			);

			m_Routes.MapERPStoreRoute(
				COMPLEMENTARY_PRODUCT_LIST
				, "catalogue/liste-produits-complementaires" 
				, new { controller = "Catalog", action = "ShowRelationalProductList" }
			);

			m_Routes.MapERPStoreRoute(
				VARIANT_PRODUCT_LIST
				, "catalogue/liste-produits-variation"
				, new { controller = "Catalog", action = "ShowRelationalProductList" }
			);

			m_Routes.MapERPStoreRoute(
				SUBSTITUTE_PRODUCT_LIST
				, "catalogue/liste-produits-substitution" 
				, new { controller = "Catalog", action = "ShowRelationalProductList" }
			);

			m_Routes.MapERPStoreRoute(
				RELATIONS_PRODUCT_LIST
				, "catalogue/relations-produits" 
				, new { controller = "Catalog", action = "ShowRelationalProductList" }
			);

			m_Routes.MapERPStoreRoute(
				CONTEXTUAL_PRODUCT_LIST
				, "catalogue/contexte-produits"
				, new { controller = "Catalog", action = "ShowContextualProductList" }
			);

			m_Routes.MapERPStoreRoute(
				TOP_SELLS
				, "top-ventes" 
				, new { controller = "Catalog", action = "ProductList", type = ERPStore.Models.ProductListType.TopSell, viewName = "topsells" }
			);

			m_Routes.MapERPStoreRoute(
				NEW_PRODUCTS
				, "nouveautes" 
				, new { controller = "Catalog", action = "ProductList", type = ERPStore.Models.ProductListType.New, viewName = "newproducts" }
			);

			m_Routes.MapERPStoreRoute(
				PROMOTIONS
				, "promotions" 
				, new { controller = "Catalog", action = "ProductList", type = ERPStore.Models.ProductListType.Promotional, viewName = "promotions" }
			);

			m_Routes.MapERPStoreRoute(
				DESTOCK
				, "destockage" 
				, new { controller = "Catalog", action = "ProductList", type = ERPStore.Models.ProductListType.Destock, viewName = "destock" }
			);

			m_Routes.MapERPStoreRoute(
				FIRST_PRICE
				,  "imbattable" 
				, new { controller = "Catalog", action = "ProductList", type = ERPStore.Models.ProductListType.FirstPrice, viewName = "firstprice" }
			);

			m_Routes.MapERPStoreRoute(
				PRODUCT_STOCK_INFO
				, "catalogue/produit/stock"
				, new { controller = "Catalog", action = "ShowProductStockInfo" }
			);

			m_Routes.MapERPStoreRoute(
				PRODUCT_BY_SELECTION
				, "catalogue/produits/s/{*selectionName}"
				, new { controller = "Catalog", action = "ShowProductListBySelection", selectionName = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				DEFAULT_CATALOG
				, "catalogue/{action}/{id}" 
				, new { controller = "Catalog", action = "Index", id = string.Empty }  
			);
		}

		protected virtual void RegisterAnonymousCheckoutRoutes()
		{
			m_Routes.MapERPStoreRoute(
				CHECKOUT
				, "commander/livraison"
				, new { controller = "AnonymousCheckout", action = "Shipping" }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_CONFIGURATION
				, "commander/configuration"
				, new { controller = "AnonymousCheckout", action = "Configuration" }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_PAYMENT
				, "commander/reglement"
				, new { controller = "AnonymousCheckout", action = "Payment" }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_CONFIRMATION
				, "commander/confirmation"
				, new { controller = "AnonymousCheckout", action = "Confirmation" }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_DIRECT_CONFIRMATION
				, "commander/confirmation-directe/{cartId}"
				, new { controller = "AnonymousCheckout", action = "DirectConfirmation", cartId = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_FINALIZE
				, "commander/finalisation"
				, new { controller = "AnonymousCheckout", action = "Finalize" }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_FINALIZED
				, "commander/merci/{*key}"
				, new { controller = "AnonymousCheckout", action = "Finalized", key = string.Empty,  }
			);
		}

		protected virtual void RegisterCheckoutRoutes()
		{
			m_Routes.MapERPStoreRoute(
				CHECKOUT
				, "commander/livraison"
				, new { controller = "Checkout", action = "Shipping" }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_CONFIGURATION
				, "commander/configuration"
				, new { controller = "Checkout", action = "Configuration" }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_PAYMENT
				, "commander/reglement"
				, new { controller = "Checkout", action = "Payment" }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_CONFIRMATION
				, "commander/confirmation"
				, new { controller = "Checkout", action = "Confirmation" }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_FINALIZE
				, "commander/finalisation"
				, new { controller = "Checkout", action = "Finalize" }
			);

			m_Routes.MapERPStoreRoute(
				CHECKOUT_FINALIZED
				, "commander/merci"
				, new { controller = "Checkout", action = "Finalized" }
			);
		}

		protected virtual void RegisterSalesRoutes()
		{
			#region Commandes

			m_Routes.MapERPStoreRoute(
				SHOW_LAST_ORDER
				, "commande/voir/derniere"
				, new { controller = "Order", action = "ShowLastOrder" }
			);

			m_Routes.MapERPStoreRoute(
				ORDER_DETAIL
				, "commande/voir/{*orderCode}" 
				, new { controller = "Order", action = "OrderDetail", orderCode = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				AJAX_ORDER_LIST
				, "commandes/liste" 
				, new { controller = "Order", action = "ShowOrderList" }
			);

			m_Routes.MapERPStoreRoute(
				ORDER_LIST
				, "commandes"
				, new { controller = "Order", action = "Index" }
			);

			m_Routes.MapERPStoreRoute(
				EDIT_ORDER
				, "commande/modifier/{*orderCode}" 
				, new { controller = "Order", action = "EditOrder", orderCode = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				ADD_COMMENT_TO_ORDER
				, "commande/commentaire/ajouter/{*orderCode}" 
				, new { controller = "Order", action = "AddCommentToOrder", orderCode = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				DEFAULT_ORDER
				, "commande/{action}/{id}" 
				, new { controller = "Order", action = "Index", id = string.Empty }
			);

			#endregion

			#region Devis

			m_Routes.MapERPStoreRoute(
				ADD_COMMENT_TO_QUOTE
				, "devis/commentaire/ajouter"
				, new { controller = "Quote", action = "AddCommentToQuote" }
			);

			m_Routes.MapERPStoreRoute(
				QUOTE_DETAIL
				, "devis/voir/{*key}" 
				, new { controller = "Quote", action = "QuoteDetail", key = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				ACCEPT_QUOTE_CONFIRMATION
				, "devis/confirmation" 
				, new { controller = "Quote", action = "AcceptConfirmation" }
			);

			m_Routes.MapERPStoreRoute(
				ACCEPT_QUOTE
				, "devis/accepter" 
				, new { controller = "Quote", action = "Accept" }
			);

			m_Routes.MapERPStoreRoute(
				QUOTE_LIST
				, "devis" 
				, new { controller = "Quote", action = "Index" }
			);

			m_Routes.MapERPStoreRoute(
				AJAX_QUOTE_LIST
				, "devis/liste" 
				, new { controller = "Quote", action = "ShowQuoteList" }
			);

			m_Routes.MapERPStoreRoute(
				CANCEL_QUOTE
				, "devis/classer/{*key}"
				, new { controller = "Quote", action = "Cancel", key = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				DEFAULT_QUOTE
				, "devis/{action}/{id}" 
				, new { controller = "Quote", action = "Index", id = string.Empty }
			);

			#endregion

			#region Factures

			m_Routes.MapERPStoreRoute(
				INVOICE_DETAIL
				, "facture/voir/{*key}"
				, new { controller = "Invoice", action = "Detail", key = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				INVOICE_LIST
				, "factures"
				, new { controller = "Invoice", action = "Index" }
			);

			m_Routes.MapERPStoreRoute(
				AJAX_INVOICE_LIST
				, "factures/liste"
				, new { controller = "Invoice", action = "ShowInvoiceList" }
			);

			#endregion
		}

		protected virtual void RegisterMediasRoutes()
		{
			m_Routes.MapRoute(
				IMAGES
				, "images/{externalDocumentId}/{width}/{height}/{*fileName}"
				, new { controller = "Media", action = "Image", width = 0, height = 0, fileName = string.Empty }
			);

			m_Routes.MapRoute(
				DOWNLOAD
				, "download/{externalDocumentId}/{*fileName}"
				, new { controller = "Media", action = "Download" }
			);

			m_Routes.MapRoute(
				ROUNDED_CORNER
				, "rc/{cornerType}/{radius}/{thickness}/{outsideColor}/{insideColor}/{curveColor}"
				, new { controller = "Media", action = "RoundedCorner", radius = 5, thickness = 1, outsidecolor = "#ffffff", insidecolor = "#ffffff", curvecolor = "#000000" }
			);

			m_Routes.MapERPStoreRoute(
				DOCUMENT_DOWNLOAD
				, "document/telecharger/{title}/{*key}"
				, new { controller = "Media", action = "DocumentDownload" }
			);

			m_Routes.MapRoute(
				DOCUMENT
				, "doc.ashx"
				, new { controller = "Media", action = "Document" }
			);
		}

		protected virtual void RegisterSiteRoutes()
		{
			m_Routes.MapERPStoreRoute(
				HOME
				, "accueil" 
				, new { controller = "Home", action = "Index" }
			);

			m_Routes.MapERPStoreRoute(
				ABOUT
				, "a-propos-de/{*siteName}" 
				, new { controller = "Home", action = "About" }
			);

			m_Routes.MapERPStoreRoute(
				TERMS_AND_CONDITIONS
				, "conditions-generales-vente" 
				, new { controller = "Home", action = "TermsAndConditions", Id = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				LEGAL_INFORMATION
				, "information-legale"
				, new { controller = "Home", action = "LegalInformation", Id = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				HELP
				, "aide" 
				, new { controller = "Home", action = "Help", Id = string.Empty }
			);

			m_Routes.MapERPStoreRoute(
				CONTACT
				, "contact" 
				, new { controller = "Home", action = "Contact", Id = string.Empty }
			);

            m_Routes.MapERPStoreRoute(
                AJAXCONTACT
                , "ajaxcontact"
                , new { controller = "Home", action = "AjaxContact", Id = string.Empty }
            );

			m_Routes.MapERPStoreRoute(
				ERROR_500
				, "oops" 
				, new { controller = "Home", action = "Error", Id = string.Empty }
			);

			//m_Routes.MapERPStoreRoute(
			//    ERROR_404
			//    , "page-absente" 
			//    , new { controller = "Home", action = "FileNotFound", Id = string.Empty }
			//);

			//m_Routes.MapERPStoreRoute(
			//    ERROR_403
			//    , "acces-interdit" 
			//    , new { controller = "Home", action = "AccessDenied", Id = string.Empty }
			//);

			m_Routes.MapERPStoreRoute(
				STATIC_PAGE
				, "statique/{viewname}"
				, new { controller = "Home", action = "StaticPage" }
			);

			m_Routes.MapERPStoreRoute(
				EMAILER
				, "email/{action}/{*key}"
				, new { controller = "Emailer", action = "index", key = string.Empty }
			);

			m_Routes.MapRoute(
				HIDE_REFERER
				, "redirect"
				, new { controller = "Home", action = "HideReferer", url = string.Empty }
			);
		}

		#endregion


	}
}
