using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Repositories
{
	/// <summary>
	/// Repository pour les inscriptions
	/// </summary>
	public interface IRegistrationRepository
	{
		/// <summary>
		/// Sauvegarde une inscription
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <param name="registration">The registration.</param>
		void SaveRegistrationUser(string visitorId, ERPStore.Models.RegistrationUser registration);
		/// <summary>
		/// Retourne une inscription en fonction du user en cours
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <returns></returns>
		Models.RegistrationUser GetRegistrationUser(string visitorId);

		/// <summary>
		/// Fin d'inscription pour un identifiant donné
		/// </summary>
		/// <param name="visitorId">The visitor id.</param>
		/// <param name="userId">The user id.</param>
		void CloseRegistrationUser(string visitorId, int userId);

		/// <summary>
		/// Retourne l'ensemble des inscriptions
		/// </summary>
		/// <returns></returns>
		IQueryable<ERPStore.Models.RegistrationUser> GetAll();
	}
}
