using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.Services
{
	public interface IAddressService
	{
		/// <summary>
		/// Sauvegarde d'une adresse d'un utilisateur ou d'une société
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="addressId">The address id.</param>
		void SaveAddress(Models.User user, int addressId);

		/// <summary>
		/// Sauvegarde d'une adresse d'un utilisateur ou d'une société
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="address">The address.</param>
		/// <param name="isDeliveryAddress">if set to <c>true</c> [is delivery address].</param>
		void SaveAddress(Models.User user, Models.Address address, bool isDeliveryAddress);

		/// <summary>
		/// Suppression d'une adresse de livraison d'un utilisateur
		/// </summary>
		/// <param name="user">The user.</param>
		/// <param name="addressId">The address id.</param>
		void DeleteAddress(Models.User user, int addressId);

		/// <summary>
		/// Retourne une addresse via son Id
		/// </summary>
		/// <param name="addressId">The address id.</param>
		/// <returns></returns>
		ERPStore.Models.Address GetAddressById(int addressId);
	}
}
