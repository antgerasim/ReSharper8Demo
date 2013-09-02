using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace ERPStore.Services
{
	/// <summary>
	/// Opérations liées à l'administration du site via ERP360
	/// </summary>
	[ServiceContract(Name = "AdminService"
	, Namespace = "http://www.erpstore.net/2009/09/20")]
	public interface IAdminService
	{
		/// <summary>
		/// Reloads the product categories.
		/// </summary>
		[OperationContract]
		[FaultContract(typeof(System.Security.SecurityException))]
		void ReloadProductCategories();

        /// <summary>
        /// Demande de rechagement de la liste des marques
        /// </summary>
        /// <remarks>
        /// Généralement une marque vient d'etre ajoutée via ERP360
        /// </remarks>
		[OperationContract]
		[FaultContract(typeof(System.Security.SecurityException))]
		void ReloadBrands();

        /// <summary>
        /// Rechargement des paramètres globaux
        /// </summary>
        /// <remarks>
        /// Une modification vient d'etre effectuée via ERP360
        /// </remarks>
		[OperationContract]
		[FaultContract(typeof(System.Security.SecurityException))]
		void ReloadSettings();


		/// <summary>
		/// Clears all cache.
		/// </summary>
		[OperationContract]
		[FaultContract(typeof(System.Security.SecurityException))]
		void ClearAllCache();

	}
}
