using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a container for connection management configuration elements. This class cannot be inherited.</summary>
	// Token: 0x020002CC RID: 716
	[ConfigurationCollection(typeof(ConnectionManagementElement), CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	public sealed class ConnectionManagementElementCollection : ConfigurationElementCollection
	{
		/// <summary>Gets or sets the element at the specified position in the collection.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> at the specified location.</returns>
		/// <param name="index">The zero-based index of the element.</param>
		// Token: 0x170005C9 RID: 1481
		[global::System.MonoTODO]
		public ConnectionManagementElement this[int index]
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the element with the specified key.</summary>
		/// <returns>The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> with the specified key or null if there is no element with the specified key.</returns>
		/// <param name="name">The key for an element in the collection. </param>
		// Token: 0x170005CA RID: 1482
		public ConnectionManagementElement this[string name]
		{
			get
			{
				return (ConnectionManagementElement)base[name];
			}
			set
			{
				base[name] = value;
			}
		}

		/// <summary>Adds an element to the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> to add to the collection.</param>
		// Token: 0x060018A4 RID: 6308 RVA: 0x00043C20 File Offset: 0x00041E20
		public void Add(ConnectionManagementElement element)
		{
			this.BaseAdd(element);
		}

		/// <summary>Removes all elements from the collection.</summary>
		// Token: 0x060018A5 RID: 6309 RVA: 0x00043C2C File Offset: 0x00041E2C
		public void Clear()
		{
			base.BaseClear();
		}

		// Token: 0x060018A6 RID: 6310 RVA: 0x00043C34 File Offset: 0x00041E34
		protected override ConfigurationElement CreateNewElement()
		{
			return new ConnectionManagementElement();
		}

		// Token: 0x060018A7 RID: 6311 RVA: 0x00043C3C File Offset: 0x00041E3C
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (!(element is ConnectionManagementElement))
			{
				throw new ArgumentException("element");
			}
			return ((ConnectionManagementElement)element).Address;
		}

		/// <summary>Returns the index of the specified configuration element.</summary>
		/// <returns>The zero-based index of <paramref name="element" />.</returns>
		/// <param name="element">A <see cref="T:System.Net.Configuration.ConnectionManagementElement" />.</param>
		// Token: 0x060018A8 RID: 6312 RVA: 0x00043C60 File Offset: 0x00041E60
		public int IndexOf(ConnectionManagementElement element)
		{
			return base.BaseIndexOf(element);
		}

		/// <summary>Removes the specified configuration element from the collection.</summary>
		/// <param name="element">The <see cref="T:System.Net.Configuration.ConnectionManagementElement" /> to remove.</param>
		// Token: 0x060018A9 RID: 6313 RVA: 0x00043C6C File Offset: 0x00041E6C
		public void Remove(ConnectionManagementElement element)
		{
			base.BaseRemove(element);
		}

		/// <summary>Removes the element with the specified key.</summary>
		/// <param name="name">The key of the element to remove.</param>
		// Token: 0x060018AA RID: 6314 RVA: 0x00043C78 File Offset: 0x00041E78
		public void Remove(string name)
		{
			base.BaseRemove(name);
		}

		/// <summary>Removes the element at the specified index.</summary>
		/// <param name="index">The zero-based index of the element to remove.</param>
		// Token: 0x060018AB RID: 6315 RVA: 0x00043C84 File Offset: 0x00041E84
		public void RemoveAt(int index)
		{
			base.BaseRemoveAt(index);
		}
	}
}
