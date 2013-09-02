using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPStore.MockConnector.Repositories
{
	public class AddressRepository
	{
		private List<ERPStore.Models.Address> m_AddressList;

		public AddressRepository()
		{
			m_AddressList = new List<ERPStore.Models.Address>();
		}

		public ERPStore.Models.Address GetById(int addressId)
		{
			return m_AddressList.SingleOrDefault(i => i.Id == addressId);
		}

		public void Save(ERPStore.Models.Address address)
		{
			if (!m_AddressList.Contains(address))
			{
				m_AddressList.Add(address);
			}
		}
	}
}
