#define DEV
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace ERPStore.Models
{
	[Serializable]
    [DataContract]
	public class RegistrationUser
	{
		public RegistrationUser()
		{
			PresentationId = (int)UserPresentation.Mister;
			ExtendedParameters = new System.Collections.Specialized.NameValueCollection();
			VatMandatory = true;
			// this.CreationDate = DateTime.Now;
		}

        [DataMember]
		public int Id { get; set; }
        [DataMember]
		public DateTime CreationDate { get; set; }
        [DataMember]
		public int StateId { get; set; }

		// Gestion du user
        [DataMember]
		public int PresentationId { get; set; }
        [DataMember]
		public string FirstName { get; set; }
        [DataMember]
		public string LastName { get; set; }
        [DataMember]
		public string Email { get; set; }
        [DataMember]
		public string PhoneNumber { get; set; }
        [DataMember]
		public string MobileNumber { get; set; }
        [DataMember]
		public string FaxNumber { get; set; }
		// public Country Country { get; set; }
        [DataMember]
		public string VisitorId { get; set; }
        [DataMember]
		public string Password { get; set; }

		// Gestion de la société s'il y en a une
        [DataMember]
		public string CorporateName { get; set; }
        [DataMember]
		public string CorporatePhoneNumber { get; set; }
        [DataMember]
		public string CorporateFaxNumber { get; set; }
        [DataMember]
		public string CorporateEmail { get; set; }
        [DataMember]
		public string CorporateWebSite { get; set; }
        [DataMember]
		public string CorporateSocialStatus { get; set; }
        [DataMember]
		public string NAFCode { get; set; }
        [DataMember]
		public string SiretNumber { get; set; }
        [DataMember]
		public string RcsNumber { get; set; }

		[Obsolete("Pb de typo, va bientot disparaitre au profit de VATNumber", false)]
        [IgnoreDataMember]
		public string TVANumber 
		{
			get
			{
				return VATNumber;
			}
			set
			{
				VATNumber = value;
			}
		}
        [DataMember]
		public string VATNumber { get; set; }
        [DataMember]
		public bool VatMandatory { get; set; }

		// Gestion de l'adresse de Facturation
        [DataMember]
		public string BillingAddressRecipientName { get; set; }
        [DataMember]
		public string BillingAddressStreet { get; set; }
        [DataMember]
		public string BillingAddressZipCode { get; set; }
        [DataMember]
		public string BillingAddressCity { get; set; }
        [DataMember]
		public int BillingAddressCountryId { get; set; }
        [DataMember]
		public string BillingAddressRegion { get; set; }

        [DataMember]
		public bool IsSameBillingAddress { get; set; }

		// Gestion de l'adresse de livraison
        [DataMember]
		public string ShippingAddressRecipientName { get; set; }
        [DataMember]
		public string ShippingAddressStreet { get; set; }
        [DataMember]
		public string ShippingAddressZipCode { get; set; }
        [DataMember]
		public string ShippingAddressCity { get; set; }
        [DataMember]
		public int ShippingAddressCountryId { get; set; }
        [DataMember]
		public string ShippingAddressRegion { get; set; }

        // Adresse de retour après l'inscription
        public string ReturnUrl { get; set; }

        // Inscription incomplete
        [DataMember]
        public int? UserId { get; set; }

        [IgnoreDataMember]
        [System.Xml.Serialization.XmlIgnore]
		public System.Collections.Specialized.NameValueCollection ExtendedParameters { get; private set; }

        [IgnoreDataMember]
		public string FullName
		{
			get
			{
				var presentation = string.Empty;
				if (PresentationId == 1)
				{
					presentation = "M.";
				}
				else if (PresentationId == 2)
				{
					presentation = "Mlle";
				}
				else if (PresentationId == 3)
				{
					presentation = "Mme";
				}

				if (FirstName.IsNullOrTrimmedEmpty())
				{
					return string.Format("{0} {1}", presentation, LastName);
				}

				return string.Format("{0} {1} {2}", presentation, FirstName, LastName);
			}
		}

		public override string ToString()
		{
			var result = new StringBuilder();
			result.AppendFormat("Billing City : {0}\r\n", this.BillingAddressCity);
			result.AppendFormat("Billing CountryID : {0}\r\n", this.BillingAddressCountryId);
			result.AppendFormat("BillingAddressRecipientName : {0}\r\n", this.BillingAddressRecipientName);
			result.AppendFormat("BillingAddressRegion : {0}\r\n", this.BillingAddressRegion);
			result.AppendFormat("BillingAddressStreet : {0}\r\n", this.BillingAddressStreet);
			result.AppendFormat("BillingAddressZipCode : {0}\r\n", this.BillingAddressZipCode);
			result.AppendFormat("CorporateEmail : {0}\r\n", this.CorporateEmail);
			result.AppendFormat("CorporateFaxNumber : {0}\r\n", this.CorporateFaxNumber);
			result.AppendFormat("CorporateName : {0}\r\n", this.CorporateName);
			result.AppendFormat("CorporatePhoneNumber : {0}\r\n", this.CorporatePhoneNumber);
			result.AppendFormat("CorporateSocialStatus : {0}\r\n", this.CorporateSocialStatus);
			result.AppendFormat("CorporateWebSite : {0}\r\n", this.CorporateWebSite);
			result.AppendFormat("Email : {0}\r\n", this.Email);
			result.AppendFormat("FaxNumber : {0}\r\n", this.FaxNumber);
			result.AppendFormat("FirstName : {0}\r\n", this.FirstName);
			result.AppendFormat("FullName : {0}\r\n", this.FullName);
			result.AppendFormat("IsSameBillingAddress : {0}\r\n", this.IsSameBillingAddress);
			result.AppendFormat("LastName : {0}\r\n", this.LastName);
			result.AppendFormat("MobileNumber : {0}\r\n", this.MobileNumber);
			result.AppendFormat("NAFCode : {0}\r\n", this.NAFCode);
			result.AppendFormat("Password : {0}\r\n", this.Password);
			result.AppendFormat("PhoneNumber : {0}\r\n", this.PhoneNumber);
			result.AppendFormat("PresentationId : {0}\r\n", this.PresentationId);
			result.AppendFormat("RcsNumber : {0}\r\n", this.RcsNumber);
			result.AppendFormat("ReturnUrl : {0}\r\n", this.ReturnUrl);
			result.AppendFormat("ShippingAddressCity : {0}\r\n", this.ShippingAddressCity);
			result.AppendFormat("ShippingAddressCountryId : {0}\r\n", this.ShippingAddressCountryId);
			result.AppendFormat("ShippingAddressRecipientName : {0}\r\n", this.ShippingAddressRecipientName);
			result.AppendFormat("ShippingAddressRegion : {0}\r\n", this.ShippingAddressRegion);
			result.AppendFormat("ShippingAddressStreet : {0}\r\n", this.ShippingAddressStreet);
			result.AppendFormat("ShippingAddressZipCode : {0}\r\n", this.ShippingAddressZipCode);
			result.AppendFormat("SiretNumber : {0}\r\n", this.SiretNumber);
			result.AppendFormat("TVANumber : {0}\r\n", this.VATNumber);
			result.AppendFormat("UserId : {0}\r\n", this.UserId);
			result.AppendFormat("VatMandatory : {0}\r\n", this.VatMandatory);
			result.AppendFormat("VisitorId : {0}\r\n", this.VisitorId);

			return result.ToString();
		}
	}
}
