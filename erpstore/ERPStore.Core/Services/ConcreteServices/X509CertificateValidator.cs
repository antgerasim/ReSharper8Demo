using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IdentityModel.Tokens;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Selectors;

namespace ERPStore.Services
{
	public class ERPStoreX509CertificateValidator : X509CertificateValidator
	{
		public override void Validate(X509Certificate2 certificate)
		{
			// validate argument
			if (certificate == null)
			{
				throw new ArgumentNullException("certificate");
			}

			// check if the name of the certifcate matches
			if (certificate.SubjectName.Name != "CN=ERPStoreServerCert")
			{
				throw new SecurityTokenValidationException("Certificated was not issued by thrusted issuer");
			}
		}
	}
}
