using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Represents a collection of shared properties. This class cannot be inherited.</summary>
	// Token: 0x02000044 RID: 68
	[ComVisible(false)]
	public sealed class SharedPropertyGroup
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00002BB0 File Offset: 0x00000DB0
		internal SharedPropertyGroup(ISharedPropertyGroup propertyGroup)
		{
			this.propertyGroup = propertyGroup;
		}

		/// <summary>Creates a property with the given name.</summary>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		/// <param name="name">The name of the new property. </param>
		/// <param name="fExists">Determines whether the property exists. Set to true on return if the property exists. </param>
		// Token: 0x06000134 RID: 308 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public SharedProperty CreateProperty(string name, out bool fExists)
		{
			return new SharedProperty(this.propertyGroup.CreateProperty(name, out fExists));
		}

		/// <summary>Creates a property at the given position.</summary>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		/// <param name="position">The index of the new property </param>
		/// <param name="fExists">Determines whether the property exists. Set to true on return if the property exists. </param>
		// Token: 0x06000135 RID: 309 RVA: 0x00002BD4 File Offset: 0x00000DD4
		public SharedProperty CreatePropertyByPosition(int position, out bool fExists)
		{
			return new SharedProperty(this.propertyGroup.CreatePropertyByPosition(position, out fExists));
		}

		/// <summary>Returns the property with the given name.</summary>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		/// <param name="name">The name of requested property. </param>
		// Token: 0x06000136 RID: 310 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public SharedProperty Property(string name)
		{
			return new SharedProperty(this.propertyGroup.Property(name));
		}

		/// <summary>Returns the property at the given position.</summary>
		/// <returns>The requested <see cref="T:System.EnterpriseServices.SharedProperty" />.</returns>
		/// <param name="position">The index of the property. </param>
		// Token: 0x06000137 RID: 311 RVA: 0x00002BFC File Offset: 0x00000DFC
		public SharedProperty PropertyByPosition(int position)
		{
			return new SharedProperty(this.propertyGroup.PropertyByPosition(position));
		}

		// Token: 0x0400007A RID: 122
		private ISharedPropertyGroup propertyGroup;
	}
}
