// #define NOTRANSACTION

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Microsoft.Practices.Unity;
using System.Web.Security;

using ERPStore.Html;

namespace ERPStore.Controllers
{
	// [HandleError(View="500")]
	public class AccountController : StoreController
    {
	    readonly cxzcxzcxz _cxzcxzcxz;

	    public AccountController(
			Services.IAccountService accountService
			, Services.ICacheService cacheService
			, Services.IEmailerService emailerService
			, Services.ICartService cartService
			, Services.ISalesService salesService
			, Services.IAddressService addressService
			)
		{
			this.AccountService = accountService;
			this.CacheService = cacheService;
			this.EmailerService = emailerService;
			this.CartService = cartService;
			this.SalesService = salesService;
			this.AddressService = addressService;
            this.CryptoService = DependencyResolver.Current.GetService<Services.CryptoService>();
	        _cxzcxzcxz = new cxzcxzcxz(this);
		}

		#region Properties

		protected Services.IAccountService AccountService { get; set; }

		protected Services.IEmailerService EmailerService { get; set; }

		protected Services.ICartService CartService { get; set; }

		protected Services.ISalesService SalesService { get; set; }

		protected Services.IAddressService AddressService { get; set; }

		protected Services.ICacheService CacheService { get; set; }

		public Services.CryptoService CryptoService { get; set; }

		#endregion

		[Authorize(Roles="customer")]
		[ActionFilters.ZeroCacheActionFilter()]
		[ActionFilters.TrackerActionFilter]
        public ActionResult Index()
        {
			SetupDefault();
            return View();
        }

		[ActionFilters.TrackerActionFilter]
		public ActionResult Login()
		{
			return View();
		}

		public ActionResult ClearCookie()
		{
			var cookie = new System.Web.HttpCookie("erpstorevid");
			cookie.Expires = DateTime.Now.AddDays(60);
			cookie.Path = "/";
			var cookieDomain = ERPStore.Configuration.ConfigurationSettings.AppSettings["cookieDomain"];
			if (!cookieDomain.IsNullOrTrimmedEmpty())
			{
				cookie.Domain = cookieDomain;
			}

			var vid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 30);
			var value = string.Format("{0}|{1:yyyy|MM|dd}", vid, DateTime.Now);

			cookie.Value = value;
			Response.Cookies.Add(cookie);
			return RedirectToRoute(ERPStoreRoutes.HOME);
		}

	    [AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Login(string userName, string password, bool? rememberMe, string returnUrl)
		{
			if (User.Identity.IsAuthenticated)
			{
				if (!returnUrl.IsNullOrTrimmedEmpty())
				{
					return Redirect(HttpUtility.UrlDecode(returnUrl));
				}
				return RedirectToERPStoreRoute(ERPStoreRoutes.ACCOUNT);
			}

			int userId = 0;
			ViewData["rememberMe"] = rememberMe;

			if (userName.IsNullOrTrimmedEmpty() 
				|| password.IsNullOrTrimmedEmpty())
			{
				ModelState.AddModelError("_FORM", "l'identifiant et le mot de passe doivent etre indiqués");
				return View();
			}

			bool isValid = ValidateLogin(userName, password, out userId);
			if (!isValid)
			{
				ModelState.AddModelError("_FORM", "identifiant ou mot de passe incorrect");
				return View();
			}

			// returnUrl = returnUrl ?? this.HttpContext.Request.UrlReferrer.LocalPath;

			Response.AddAuthenticatedCookie(userId, rememberMe);
			EventPublisherService.Publish(new Models.Events.UserAuthenticatedEvent()
			{
				UserId = userId,
				VisitorId = User.GetUserPrincipal().VisitorId,
			});

			if (!returnUrl.IsNullOrTrimmedEmpty())
			{
				return Redirect(HttpUtility.UrlDecode(returnUrl));
			}
			return RedirectToERPStoreRoute(ERPStoreRoutes.ACCOUNT);
		}

		// [Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Logoff(string returnUrl)
		{
			this.HttpContext.SignOut();
			if (returnUrl != null)
			{
				return Redirect(returnUrl);
			}
			else
			{
				return RedirectToRoute(ERPStoreRoutes.HOME);
			}
		}

	    [Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditAddress(int addressId)
		{
			var principal = User.GetUserPrincipal();
			var cart = CartService.GetCurrentOrderCart(principal);
			if (addressId == -1)
			{
				ViewData.Model = cart.BillingAddress;
			}
			else
			{
				ViewData.Model = principal.CurrentUser.DeliveryAddressList[addressId];
			}
			return View("editaddress");
		}

	    [Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditAddressList()
		{
			var user = User.GetUserPrincipal().CurrentUser;
			SetupDefault();
			return View("EditAddresses");
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		[ActionFilters.ZeroCacheActionFilter]
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult EditAddressList(FormCollection form)
		{
			var user = User.GetUserPrincipal().CurrentUser;
			// SetupDefault();
			ViewData.Model = user;
			ViewData["success"] = 0;

			var addressId = 0;
			if (!int.TryParse(form["addressId"], out addressId))
			{
				ModelState.AddModelError("_FORM", "Adresse introuvable");
				return View("EditAddresses");
			}

			int oldAddressId = 0;
			var city = string.Format("city{0}", addressId);
			var corporateName = string.Format("corporateName{0}", addressId);
			var countryId = string.Format("countryId{0}", addressId);
			var recipientName = string.Format("recipientName{0}", addressId);
			var street = string.Format("street{0}", addressId);
			var zipCode = string.Format("zipCode{0}", addressId);

			var address = new ERPStore.Models.Address();
			address.City = form[city];
			address.CorporateName = form[corporateName];
			address.CountryId = Convert.ToInt32(form[countryId]);
			address.RecipientName = form[recipientName];
			address.Street = form[street];
			address.ZipCode = form[zipCode];

			var brokenRules = AccountService.ValidateUserAddress(address, HttpContext);
			if (brokenRules.IsNotNullOrEmpty())
			{
				foreach (var item in brokenRules)
				{
					foreach (var error in item.ErrorList)
					{
						ModelState.AddModelError(string.Format("{0}{1}", item.PropertyName, addressId) , error);
					}
				}
			}

			if (!ModelState.IsValid)
			{
				return View("EditAddresses");
			}

			if (addressId == -1)
			{
				oldAddressId = user.DefaultAddress.Id;
				user.DefaultAddress = address;
				user.DefaultAddress.Id = oldAddressId;
			}
			else
			{
				oldAddressId = user.DeliveryAddressList[addressId].Id;
				user.DeliveryAddressList[addressId] = address;
				// Cas ou l'adresse de livraison est la meme 
				// que l'adresse de facturation
				if (oldAddressId == user.DefaultAddress.Id)
				{
					user.DeliveryAddressList[addressId].Id = 0;
				}
				else
				{
					user.DeliveryAddressList[addressId].Id = oldAddressId;
				}
			}

			AddressService.SaveAddress(user, address, addressId != -1);
			ViewData["success"] = addressId.ToString();
			return View("EditAddresses");
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult RecoverPassword()
		{
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult RecoverPassword(string loginOrEmail)
		{
			var user = AccountService.GetUserByEmailOrLogin(loginOrEmail);
			if (user == null)
			{
				ModelState.AddModelError("_FORM", "Identifiant inconnu");
				return View();
			}

			var host = Request.Url.Host;
			var key = CryptoService.EncryptChangePassword(user.Id, user.Email);
			var urlHelper = new UrlHelper(ControllerContext.RequestContext);
			var url = string.Format("http://{0}{1}/{2}", host, urlHelper.RouteERPStoreUrl(ERPStoreRoutes.ACCOUNT_CHANGE_PASSWORD), key);
			ViewData["key"] = key;

			try
			{
				EmailerService.SendChangePassword(this, user, url);
				ViewData["PasswordSent"] = true;
			}
			catch(Exception ex)
			{
				LogError(Logger, ex);
				ModelState.AddModelError("_FORM", "Un problème technique empeche l'execution de cette opération");
			}

			return View();
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult ChangePassword(string key)
		{
			if (key.IsNullOrTrimmedEmpty())
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.ACCOUNT);
			}
			ViewData["ChangePasswordKey"] = key;
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult ChangePassword(string key, string newPassword, string confirmPassword)
		{
			if (key.IsNullOrTrimmedEmpty())
			{
				// return RedirectToAction("Account");
				return RedirectToERPStoreRoute(ERPStoreRoutes.ACCOUNT);
			}
			if (newPassword.IsNullOrTrimmedEmpty())
			{
				ModelState.AddModelError("newPassword", "Le mot de passe est incorrect");
			}
			if (newPassword.Length < 6)
			{
				ModelState.AddModelError("newPassword", "Le mot de passe doit au moins avoir 6 caractères");
			}
			if (!newPassword.Equals(confirmPassword))
			{
				ModelState.AddModelError("newPassword", "Le mot de passe n'est pas confirmé");
			}
			if (!ModelState.IsValid)
			{
				ViewData["ChangePasswordKey"] = key;
				return View();
			}

			try
			{
				int userId = 0;
				string userEmail = null;
				DateTime expirationDate = DateTime.MinValue;
				CryptoService.DecryptChangePassword(key, out userId, out userEmail, out expirationDate);
				if (expirationDate < DateTime.Now)
				{
					ModelState.AddModelError("_FORM", "date de validité de la clé expirée");
					return View();
				}
				var user = AccountService.GetUserById(userId);
				if (user != null)
				{
					AccountService.SetPassword(user, newPassword);
				}
				else
				{
					ModelState.AddModelError("_FORM", "un problème technique empeche cette action");
					return View();
				}
			}
			catch (Exception ex)
			{
				LogError(Logger, ex);
				ModelState.AddModelError("_FORM", "un problème technique empeche cette action");
				return View();
			}

			return View("ChangePasswordSuccess");
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditUser()
		{
			ViewData.Model = User.GetUserPrincipal().CurrentUser;
			return View();
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditUser(Models.User userForm)
		{
			var brokenRules = AccountService.ValidateUser(userForm, HttpContext);

			if (brokenRules.IsNotNullOrEmpty())
			{
				foreach (var item in brokenRules)
				{
					foreach (var error in item.ErrorList)
					{
						ModelState.AddModelError(item.PropertyName, error);
					}
				}
			}

			var user = User.GetUserPrincipal().CurrentUser;

			if (ModelState.IsValid)
			{
				try
				{
					user.Email = userForm.Email;
					user.FaxNumber = userForm.FaxNumber;
					user.FirstName = userForm.FirstName;
					user.LastName = userForm.LastName;
					user.MobileNumber = userForm.MobileNumber;
					user.PhoneNumber = userForm.PhoneNumber;
					user.Presentation = userForm.Presentation;
					AccountService.SaveUser(user);
					ViewData["EditUserSuccess"] = true;
				}
				catch (Exception ex)
				{
					LogError(Logger, ex);
					ModelState.AddModelError("_FORM", "des problèmes techniques empechent la sauvegarde de vos information");
				}
			}

			ViewData["CurrentTab"] = "edituser";
			SetupDefault();
			return View();
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditCorporate()
		{
			var user = User.GetUserPrincipal().CurrentUser;
			if (user.Corporate != null)
			{
				ViewData.Model = user.Corporate;
				return View();
			}
			else
			{
				SetupDefault();
				return View("Index");
			}
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditCorporate(Models.Corporate corporateForm)
		{
			var brokenRules = AccountService.ValidateCorporate(corporateForm, HttpContext);
			ModelState.AddModelErrors(brokenRules);

			var user = User.GetUserPrincipal().CurrentUser;
			ViewData.Model = corporateForm;

			if (ModelState.IsValid)
			{
				try
				{
					using (var ts = TransactionHelper.GetNewReadCommitted())
					{
						if (user.Corporate == null)
						{
							AccountService.CreateCorporateFromUser(user);
							AccountService.SaveUser(user);
						}
						user.Corporate.Email = corporateForm.Email;
						user.Corporate.FaxNumber = corporateForm.FaxNumber;
						user.Corporate.NafCode = corporateForm.NafCode;
						user.Corporate.Name = corporateForm.Name;
						user.Corporate.PhoneNumber = corporateForm.PhoneNumber;
						user.Corporate.RcsNumber = corporateForm.RcsNumber;
						user.Corporate.SiretNumber = corporateForm.SiretNumber;
						user.Corporate.SocialStatus = corporateForm.SocialStatus;
						user.Corporate.VatNumber = corporateForm.VatNumber;
						user.Corporate.WebSite = corporateForm.WebSite;
						AccountService.SaveCorporate(user.Corporate);
						ts.Complete();
						ViewData["EditCorporateSuccess"] = true;
					}
				}
				catch (Exception ex)
				{
					LogError(Logger, ex);
					ModelState.AddModelError("_FORM", "des problèmes techniques empechent la sauvegarde de vos information");
				}
			}
			return View();
		}

		[Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditPassword()
		{
			return View();
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditPassword(string oldpassword, string newpassword, string confirmnewpassword)
		{
			if (oldpassword.IsNullOrTrimmedEmpty())
			{
				ModelState.AddModelError("oldpassword", "Vous devez indiquer votre ancien mot de passe");
			}
			if (newpassword.IsNullOrTrimmedEmpty())
			{
				ModelState.AddModelError("newpassword", "Vous devez indiquer un nouveau mot de passe");
			}
			if (!newpassword.Equals(confirmnewpassword))
			{
				ModelState.AddModelError("confirmnewpassword", "Vous n'avez pas confirmé votre mot de passe");
			}
			if (ModelState.IsValid)
			{ 
				var user = User.GetUserPrincipal().CurrentUser;
				var result = AccountService.CheckPassword(user, oldpassword);
				if (result)
				{
					AccountService.SetPassword(user, newpassword);
					ViewData["EditPasswordSuccess"] = true;
				}
				else
				{
					ModelState.AddModelError("oldpassword", "Votre ancien mot de passe n'est pas valide");
				}
			}
			return View();
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult EditUserAddress(Models.Address address, int addressId)
		{
			ViewData.Model = address;
			ModelState.AddModelErrors(AccountService.ValidateUserAddress(address, HttpContext));
			if (ModelState.IsValid)
			{
				var user = User.GetUserPrincipal().CurrentUser;
				if (addressId == -1)
				{
					var oldId = user.DefaultAddress.Id;
					user.DefaultAddress = address;
					user.DefaultAddress.Id = oldId;
				}
				else
				{
					var oldId = user.DeliveryAddressList[addressId].Id;
					user.DeliveryAddressList[addressId] = address;
					user.DeliveryAddressList[addressId].Id = oldId;
				}

				AddressService.SaveAddress(user, address, addressId != -1);
				ViewData["EditAddressSuccess"] = addressId;
			}
			SetupDefault();
			return View("EditAddresses");
		}


		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult AjaxEditAddress(Models.Address address, int addressId)
		{
			ModelState.AddModelErrors(AccountService.ValidateUserAddress(address, HttpContext));
			if (ModelState.IsValid)
			{
				var user = User.GetUserPrincipal().CurrentUser;
				if (addressId == -1)
				{
					var oldId = user.DefaultAddress.Id;
					user.DefaultAddress = address;
					user.DefaultAddress.Id = oldId;
				}
				else
				{
					var oldId = user.DeliveryAddressList[addressId].Id;
					user.DeliveryAddressList[addressId] = address;
					user.DeliveryAddressList[addressId].Id = oldId;
				}

				AddressService.SaveAddress(user, address, addressId != -1);

				return Json(address);
			}

			return Json(ModelState.GetAllAjaxErrors());
		}

		[Authorize(Roles = "customer")]
		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult DeleteUserAddress(int addressId, string returnUrl)
		{
			var user = User.GetUserPrincipal().CurrentUser;
			var realAddress = user.DeliveryAddressList[addressId];
			if (realAddress == null)
			{
				ModelState.AddModelError("_FORM", "Cette addresse n'existe pas");
			}
			else if (realAddress.Id == user.DefaultAddress.Id)
			{
				ModelState.AddModelError("_FORM", "Cette addresse ne peut pas etre supprimée car il s'agit aussi de l'adresse de facturation");
			}

			if (ModelState.IsValid)
			{
				try
				{
					using (var ts = TransactionHelper.GetNewReadCommitted())
					{
						AddressService.DeleteAddress(user, realAddress.Id);
						ts.Complete();
					}
					user.DeliveryAddressList.Remove(realAddress);
				}
				catch (Exception ex)
				{
					LogError(Logger, ex);
					ModelState.AddModelError("_FORM", "Des problèmes techniques internes empêche le traitement de cette instruction");
				}
			}

			returnUrl = returnUrl ?? Request.UrlReferrer.ToString();

			return new RedirectResult(returnUrl);
		}

		#region Render partial

		public ActionResult ShowStatus(string viewName)
		{
			var principal = User as ERPStore.Models.UserPrincipal;
			if (principal != null)
			{
				ViewData.Model = principal.CurrentUser;
			}
			return PartialView(viewName);
		}

        public ActionResult ShowAccountMenu(string viewName)
        {
            if (User.GetUserPrincipal() != null)
            {
                ViewData.Model = User.GetUserPrincipal().CurrentUser;
            }
            return PartialView(viewName);
        }

		#endregion

		#region Privates

		void SetupDefault()
		{
			var user = User.GetUserPrincipal().CurrentUser;
			ViewData.Model = user;
			if (user.Corporate == null)
			{
				ViewData["Corporate"] = new ERPStore.Models.Corporate();
			}
			else
			{
				ViewData["Corporate"] = user.Corporate;
			}
		}

		private bool ValidateLogin(string userName, string password, out int userId)
		{
			userId = 0;
			if (String.IsNullOrEmpty(userName))
			{
				ModelState.AddModelError("username", "Vous devez indiquer un identifiant.");
			}
			if (String.IsNullOrEmpty(password))
			{
				ModelState.AddModelError("password", "Vous devez indiquer un mot de passe.");
			}
			if (ModelState.IsValid)
			{
				try
				{
					userId = AccountService.Authenticate(userName, password);
					if (userId == 0)
					{
						ModelState.AddModelError("_FORM", "L'identifiant ou le mot de passe sont incorrects.");
					}
				}
				catch
				{
					ModelState.AddModelError("_FORM", "L'identifiant ou le mot de passe sont incorrects.");
				}
			}
			return ModelState.IsValid;
		}

		#endregion

	}
}
