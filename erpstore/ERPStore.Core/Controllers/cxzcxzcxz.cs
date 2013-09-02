using ERPStore.Controllers;

public class cxzcxzcxz
{
    AccountController _accountController;

    public cxzcxzcxz(AccountController accountController)
    {
        _accountController = accountController;
    }

    [AcceptVerbs(HttpVerbs.Post)]
    public ActionResult AjaxLogin(string userName, string password, bool? rememberMe)
    {
        if (_accountController.User.Identity.IsAuthenticated)
        {


            return _accountController.RedirectToERPStoreRoute(ERPStoreRoutes.ACCOUNT);
        }

        int userId = 0;
        _accountController.ViewData["rememberMe"] = rememberMe;

        if (userName.IsNullOrTrimmedEmpty()
            || password.IsNullOrTrimmedEmpty())
        {
            _accountController.ModelState.AddModelError("_FORM", "l'identifiant et le mot de passe doivent etre indiqués");
            return _accountController.PartialView("~/views/account/_login.cshtml");
        }

        bool isValid = _accountController.ValidateLogin(userName, password, out userId);
        if (!isValid)
        {
            _accountController.ModelState.AddModelError("_FORM", "identifiant ou mot de passe incorrect");
            return _accountController.PartialView("~/views/account/_login.cshtml");
        }

        _accountController.Response.AddAuthenticatedCookie(userId, rememberMe);
        _accountController.EventPublisherService.Publish(new Models.Events.UserAuthenticatedEvent()
        {
            UserId = userId,
            VisitorId = _accountController.User.GetUserPrincipal().VisitorId,
        });

        var user = _accountController.AccountService.GetUserById(userId);

        return _accountController.PartialView("~/views/account/_logged.cshtml", user);
    }

    [ActionFilters.ZeroCacheActionFilter]
    [ActionFilters.TrackerActionFilter]
    public ActionResult Complete(string key, string returnUrl)
    {
        int userId = 0;
        _accountController.CryptoService.DecryptCompleteAccount(key, out userId);
        var user = _accountController.AccountService.GetUserById(userId);
        if (user == null)
        {
            return _accountController.RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
        }

        var registration = _accountController.AccountService.CreateRegistrationUser();
        // Registration.Country = user.DefaultAddress.Country;
        registration.Email = user.Email;
        registration.FaxNumber = user.FaxNumber;
        registration.FirstName = user.FirstName;
        registration.LastName = user.LastName;
        registration.MobileNumber = user.MobileNumber;
        registration.PhoneNumber = user.PhoneNumber;
        registration.PresentationId = (int)user.Presentation;

        if (user.Corporate != null)
        {
            registration.CorporateName = user.Corporate.Name;
            registration.CorporateEmail = user.Corporate.Email;
            registration.CorporateFaxNumber = user.Corporate.FaxNumber;
            registration.CorporatePhoneNumber = user.Corporate.PhoneNumber;
            registration.CorporateSocialStatus = user.Corporate.SocialStatus;
            registration.CorporateWebSite = user.Corporate.WebSite;
        }
        // Adresse de facturation
        registration.BillingAddressCity = user.DefaultAddress.City;
        registration.BillingAddressCountryId = user.DefaultAddress.Country.Id;
        registration.BillingAddressRecipientName = user.DefaultAddress.RecipientName;
        registration.BillingAddressStreet = user.DefaultAddress.Street;
        registration.BillingAddressZipCode = user.DefaultAddress.ZipCode;

        // Url de retour post inscription
        registration.ReturnUrl = HttpUtility.UrlDecode(returnUrl);
        registration.UserId = user.Id;

        _accountController.ViewData.Model = registration;
        _accountController.ViewData["emailConfirmation"] = user.Email;

        _accountController.AccountService.SaveRegistrationUser(_accountController.User.GetUserPrincipal().VisitorId, registration);

        return _accountController.View("Register");
    }

    [ActionFilters.ZeroCacheActionFilter]
    [ActionFilters.TrackerActionFilter]
    public ActionResult Confirmation(string key)
    {
        Models.User user = null;
        try
        {
            int userId = 0;
            string email = null;
            _accountController.CryptoService.DecryptAccountConfirmation(key, out email, out userId);
            user = _accountController.AccountService.SetConfirmationByUser(userId);
            _accountController.ViewData["ConfirmationKey"] = key;
            _accountController.Response.AddAuthenticatedCookie(user.Id, false);
            bool isNewVisitor = false;
            _accountController.EventPublisherService.Publish(new Models.Events.UserAuthenticatedEvent()
            {
                UserId = userId,
                VisitorId = _accountController.ControllerContext.HttpContext.GetOrCreateVisitorId(out isNewVisitor),
            });
        }
        catch (Exception ex)
        {
            _accountController.Logger.Error(ex);
            _accountController.ViewData["Message"] = "Ce compte est déjà confirmé";
            return _accountController.View("confirmation-error");
        }
        return _accountController.View(user);
    }

    [AcceptVerbs(HttpVerbs.Post)]
    [ActionFilters.ZeroCacheActionFilter]
    [ActionFilters.TrackerActionFilter]
    public ActionResult Confirmation(string oldpassword, string newpassword, string newpasswordconfirmation, string confirmationKey)
    {
        int userId = 0;
        string userEmail = null;
        _accountController.CryptoService.DecryptAccountConfirmation(confirmationKey, out userEmail, out userId);
        var user = _accountController.AccountService.GetUserById(userId);
        if (user == null)
        {
            _accountController.ViewData["Message"] = "Un problème d'authentification de la demande empeche la création du mot de passe";
            return _accountController.View("confirmation-error");
        }
        //if (user.TemporaryPassword != oldpassword)
        //{
        //    ModelState.AddModelError("oldpassword", "Mot de passe temporaire invalide");
        //}
        if (newpassword.IsNullOrTrimmedEmpty())
        {
            _accountController.ModelState.AddModelError("newpassword", "Vous devez indiquer un mot de passe");
        }
        if (newpassword != newpasswordconfirmation)
        {
            _accountController.ModelState.AddModelError("newpassword", "Le nouveau mot de passe n'est pas confirmé");
        }
        if (_accountController.ModelState.IsValid)
        {
            _accountController.AccountService.SetPassword(user, newpassword);
            FormsAuthentication.SetAuthCookie(user.Id.ToString(), true);
            // return RedirectToAction("Index", "Home");
            return _accountController.RedirectToERPStoreRoute(ERPStoreRoutes.HOME);
        }
        _accountController.ViewData["ConfirmationKey"] = confirmationKey;
        return _accountController.View(user);
    }

    [Authorize(Roles = "customer")]
    [AcceptVerbs(HttpVerbs.Post)]
    [ActionFilters.ZeroCacheActionFilter]
    [ActionFilters.TrackerActionFilter]
    public ActionResult EditAddress(Models.Address address)
    {
        _accountController.ViewData.Model = address;
        _accountController.ModelState.AddModelErrors(_accountController.AccountService.ValidateUserAddress(address, _accountController.HttpContext));
        if (!_accountController.ModelState.IsValid)
        {
            return _accountController.View("editaddress");
        }

        var principal = _accountController.User.GetUserPrincipal();
        int addressId = Convert.ToInt32(_accountController.Request["addressId"]);
        if (addressId == -1)
        {
            var oldId = principal.CurrentUser.DefaultAddress.Id;
            principal.CurrentUser.DefaultAddress = address;
            principal.CurrentUser.DefaultAddress.Id = oldId;
        }
        else
        {
            var oldId = principal.CurrentUser.DeliveryAddressList[addressId].Id;
            principal.CurrentUser.DeliveryAddressList[addressId] = address;
            principal.CurrentUser.DeliveryAddressList[addressId].Id = oldId;
        }

        _accountController.AddressService.SaveAddress(principal.CurrentUser, address, addressId != -1);

        var returnUrl = _accountController.Request["returnUrl"];
        if (returnUrl != null)
        {
            return _accountController.Redirect(HttpUtility.UrlDecode(returnUrl));
        }

        return _accountController.View("editaddress");
    }
}