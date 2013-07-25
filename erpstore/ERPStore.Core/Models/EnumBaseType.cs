using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace ERPStore.Models
{
	/// <summary>
	/// Type de base pour les enums etendus
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable]
    public abstract class EnumBaseType<T> where T : EnumBaseType<T>
    {
        protected static List<T> enumValues = new List<T>();
		
		#region Properties
        
        [Bindable(true)]
        public int Id { get; set; }

        [Bindable(true)]
        public string Name { get; set; }

		/// <summary>
		/// Gets the current localized text.
		/// </summary>
		/// <value>The name of the localized.</value>
        [Bindable(true)]
		public string LocalizedName { get ; private set ; }

        #endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="EnumBaseType&lt;T&gt;"/> class.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="name">The name.</param>
		/// <param name="fr">The fr.</param>
		/// <param name="en">The en.</param>
        public EnumBaseType(int id, string name, string fr, string en)
        {
            Id = id;
            Name = name;

			LocalizedName = fr;						
            enumValues.Add((T)this);
        }

        protected EnumBaseType()
        {

        }

		/// <summary>
		/// Gets the base values.
		/// </summary>
		/// <returns></returns>
        protected static ReadOnlyCollection<T> GetBaseValues()
        {
            return enumValues.AsReadOnly();
        }

		/// <summary>
		/// Gets the base by key.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <returns></returns>
        protected static T GetBaseByKey(int id)
        {
            foreach (T t in enumValues)
            {
				if (t.Id == id)
				{
					return t;
				}
            }
			throw new KeyNotFoundException("The item with the Id " + id.ToString() + " does not exist in the collection");
        }

		protected static bool GetBaseExists(int id)
		{
			return enumValues.Exists(delegate(T t) { return t.Id ==id;});
		}


		/// <summary>
		/// Gets the base by key.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns></returns>
		protected static T GetBaseByName(string name)
		{
			foreach (T t in enumValues)
			{
				if (t.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase))
				{
					return t;
				}
			}
			throw new KeyNotFoundException("The item with the name " + name + " does not exist in the collection");
			//return null;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </summary>
		/// <returns>
		/// A <see cref="T:System.String"/> that represents the current <see cref="T:System.Object"/>.
		/// </returns>
        public override string ToString()
        {
			return Name; // string.Format("{0}({1})", Name, Id);
        }

    }
}
