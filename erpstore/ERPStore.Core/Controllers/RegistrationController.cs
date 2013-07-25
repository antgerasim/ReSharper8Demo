using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

using Microsoft.Practices.Unity;

namespace ERPStore.Controllers
{
	public class RegistrationController : StoreController
	{
		public RegistrationController(
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
		}

		protected Services.IAccountService AccountService { get; set; }

		protected Services.IEmailerService EmailerService { get; set; }

		protected Services.ICartService CartService { get; set; }

		protected Services.ISalesService SalesService { get; set; }

		protected Services.IAddressService AddressService { get; set; }

		protected Services.ICacheService CacheService { get; set; }

		public Services.CryptoService CryptoService { get; set; }

		/// <summary>
		/// Registers this instance.
		/// </summary>
		/// <returns></returns>
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Register()
		{
			var registration = this.AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
			if (registration == null)
			{
				registration = this.AccountService.CreateRegistrationUser();
				// Pays par defaut assigné
				registration.BillingAddressCountryId = ERPStoreApplication.WebSiteSettings.Country.Id;
				registration.ReturnUrl = Request["returnUrl"];
			}
			this.AccountService.SaveRegistrationUser(User.GetUserPrincipal().VisitorId, registration);
			ViewData.Model = registration;

            return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult Register(Models.RegistrationUser user, string emailConfirmation, string passwordConfirmation)
		{
			var registration = this.AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
			if (registration == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT);
			}

			ViewData.Model = registration;

			var brokenRules = AccountService.ValidateRegistrationUser(user, HttpContext);
			ModelState.AddModelErrors(brokenRules);

			if (emailConfirmation.IsNullOrTrimmedEmpty()
				|| !user.Email.Equals(emailConfirmation, StringComparison.InvariantCultureIgnoreCase))
			{
				ModelState.AddModelError("EmailConfirmation", "L'Email indiqué n'est pas confirmé");
			}

			if (user.Password.IsNullOrTrimmedEmpty()
				|| !user.Password.Equals(passwordConfirmation))
			{
				ModelState.AddModelError("PasswordConfirmation", "Le mot de passe n'est pas confirmé");
			}

			if (!ModelState.IsValid)
			{
                return View();
			}

			registration.PresentationId = user.PresentationId;
			registration.FirstName = user.FirstName;
			registration.LastName = user.LastName;
			registration.MobileNumber = user.MobileNumber;
			registration.Password = user.Password;
			registration.PhoneNumber = user.PhoneNumber;
			registration.CorporateName = user.CorporateName;
			registration.FaxNumber = user.FaxNumber;
			registration.Email = user.Email;
			registration.BillingAddressCountryId = user.BillingAddressCountryId;

			try
			{
				AccountService.ProcessRegistrationUser(registration, HttpContext);
				this.AccountService.SaveRegistrationUser(User.GetUserPrincipal().VisitorId, registration);
			}
			catch (Exception ex)
			{
				Logger.Error(ex);
			}

			if (registration.CorporateName.IsNullOrTrimmedEmpty())
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_BILLING_ADDRESS);
			}
			else
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT_CORPORATE);
			}
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult RegisterCorporate()
		{
			var registration = this.AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
			if (registration == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT);
				// return RedirectToAction("Register");
			}
			if (registration.CorporatePhoneNumber.IsNullOrTrimmedEmpty())
			{
				registration.CorporatePhoneNumber = registration.PhoneNumber;
			}
			if (registration.CorporateEmail.IsNullOrTrimmedEmpty())
			{
				registration.CorporateEmail = registration.Email;
			}
			if (registration.CorporateFaxNumber.IsNullOrTrimmedEmpty())
			{
				registration.CorporateFaxNumber = registration.FaxNumber;
			}
			ViewData.Model = registration;
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult RegisterCorporate(Models.RegistrationUser user)
		{
			var registration = this.AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
			if (registration == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT);
			}

			ViewData.Model = registration;
			var brokenRules = AccountService.ValidateRegistrationCorporate(user, HttpContext);
			ModelState.AddModelErrors(brokenRules);

			if (!ModelState.IsValid)
			{
				return View();
			}

			registration.CorporateEmail = user.CorporateEmail;
			registration.CorporateFaxNumber = user.CorporateFaxNumber;
			registration.CorporateName = user.CorporateName;
			registration.CorporatePhoneNumber = user.CorporatePhoneNumber;
			registration.CorporateSocialStatus = user.CorporateSocialStatus;
			registration.CorporateWebSite = user.CorporateWebSite;
			registration.NAFCode = user.NAFCode;
			registration.SiretNumber = user.SiretNumber;
			registration.VATNumber = user.TVANumber;
			registration.RcsNumber = user.RcsNumber;

			this.AccountService.SaveRegistrationUser(User.GetUserPrincipal().VisitorId, registration);
			AccountService.ProcessRegistrationCorporate(registration, HttpContext);

			return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_BILLING_ADDRESS);
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult RegisterBillingAddress()
		{
			var registration = this.AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
			if (registration == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT);
			}

			if (registration.BillingAddressRecipientName.IsNullOrTrimmedEmpty())
			{
				if (registration.CorporateName.IsNullOrTrimmedEmpty())
				{
					registration.BillingAddressRecipientName = registration.FullName;
				}
				else
				{
					registration.BillingAddressRecipientName = registration.CorporateName;
				}
			}

			ViewData.Model = registration;
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult RegisterBillingAddress(Models.RegistrationUser user)
		{
			var registration = this.AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
			if (registration == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT);
			}
			ViewData.Model = registration;

			ModelState.AddModelErrors(AccountService.ValidateBillingAdressRegistrationUser(user, HttpContext));
			if (!ModelState.IsValid)
			{
				return View();
			}

			registration.BillingAddressCity = user.BillingAddressCity;
			registration.BillingAddressCountryId = user.BillingAddressCountryId;
			registration.BillingAddressRecipientName = user.BillingAddressRecipientName;
			registration.BillingAddressRegion = user.BillingAddressRegion;
			registration.BillingAddressStreet = user.BillingAddressStreet;
			registration.BillingAddressZipCode = user.BillingAddressZipCode;

			AccountService.ProcessRegistrationBillingAddress(registration, HttpContext);
			this.AccountService.SaveRegistrationUser(User.GetUserPrincipal().VisitorId, registration);

			return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT_CONFIRMATION);
		}

		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult RegisterConfirmation()
		{
			var registration = this.AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
			if (registration == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT);
			}
			ViewData.Model = registration;
			return View();
		}

		[AcceptVerbs(HttpVerbs.Post)]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult RegisterConfirmation(string confirmation)
		{
			var registration = this.AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
			if (registration == null)
			{
				return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT);
			}
			ViewData.Model = registration;

			if (confirmation != "on")
			{
				ModelState.AddModelError("Confirmation", "Vous devez confirmer que vous etes d'accord avec les conditions générales de vente pour pouvoir creer votre compte");
				return View();
			}

			Models.User account = null;
			var returnUrl = registration.ReturnUrl;
			try
			{
				Logger.Debug("Contact is valid, registration");
				using (var ts = TransactionHelper.GetNewReadCommitted())
				{
					if (registration.UserId.HasValue)
					{
						account = AccountService.CompleteRegistration(registration, HttpContext);
					}
					else
					{
						account = AccountService.RegisterUser(registration);
					}

					Logger.Debug("Try to update post registration");
					var updated = AccountService.UpdateAccountPostRegistration(account, registration, HttpContext);
					if (updated)
					{
						Logger.Debug("Update post registration");
						this.AccountService.SaveUser(account);
					}

					ts.Complete();
					Logger.Notification("Registration of new contact {0} with success", account.FullName);

				}

                if (SiteSettings.SignInWhenRegistered)
                {
                    Response.AddAuthenticatedCookie(account.Id, true);
                    bool isNewVisitor = false;
                    EventPublisherService.Publish(new Models.Events.UserAuthenticatedEvent()
                    {
                        UserId = account.Id,
                        VisitorId = HttpContext.GetOrCreateVisitorId(out isNewVisitor),
                    });
                }
			}
			catch (Exception ex)
			{
				LogError(Logger, ex);
				ModelState.AddModelError("_FORM", "Un problème technique empêche cette opération, veuillez réessayer ultérieurment");
			}

			if (!ViewData.ModelState.IsValid)
			{
				return View();
			}

			ViewData.Model = account;

			var subject = new 
			{ 
				UserId = account.Id, 
				ExpirationDate = DateTime.Now.AddDays(1) 
			};
			var key = CryptoService.Encrypt(subject);
			return RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT_FINALIZED, new { returnUrl = returnUrl, key = key });
		}

		// [Authorize(Roles = "customer")]
		[ActionFilters.ZeroCacheActionFilter]
		[ActionFilters.TrackerActionFilter]
		public ActionResult RegisterFinalized(string returnUrl, string key)
		{
			var registration = this.AccountService.GetRegistrationUser(User.GetUserPrincipal().VisitorId);
			if (registration != null)
			{
				RedirectToERPStoreRoute(ERPStoreRoutes.REGISTER_ACCOUNT);
			}

			var user = User.GetUserPrincipal().CurrentUser;

			if (user == null)
			{
				// Recherche du user avec la clé :
				var subject = new { UserId = 0, ExpirationDate = DateTime.MinValue };
				var result = CryptoService.Decrypt(key, subject);
				var userId = Convert.ToInt32(result[0]);
				var expirationDate = Convert.ToDateTime(result[1]);
				if (expirationDate < DateTime.Now)
				{
					return RedirectToERPStoreRoute(ERPStore.ERPStoreRoutes.HOME);
				}
				user = AccountService.GetUserById(userId);
			}

			if (SiteSettings.SendAccountRegistrationConfirmation
				&& !user.EmailRegistrationSent)
			{
				try
				{
					EmailerService.SendAccountConfirmation(this, user);
					user.EmailRegistrationSent = true;
				}
				catch (Exception ex)
				{
					LogError(Logger, ex);
				}
			}

			ViewData.Model = user;
			ViewData["ReturnUrl"] = returnUrl;

			return View("registered");
		}

	}
}
