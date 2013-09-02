using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.MockConnector
{
	public class AddressService : Services.IAddressService
	{
		public AddressService()
		{
			this.AddressRepository = new ERPStore.MockConnector.Repositories.AddressRepository();
		}

		protected Repositories.AddressRepository AddressRepository { get; private set; }

		#region IAddressService Members

		public ERPStore.Models.Address GetAddressById(int addressId)
		{
			return AddressRepository.GetById(addressId);
		}

		public void SaveAddress(ERPStore.Models.User user, int addressId)
		{
			
		}

		public void SaveAddress(ERPStore.Models.User user, ERPStore.Models.Address address, bool isDeliveryAddress)
		{
			AddressRepository.Save(address);
		}

		public void DeleteAddress(ERPStore.Models.User user, int addressId)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}
