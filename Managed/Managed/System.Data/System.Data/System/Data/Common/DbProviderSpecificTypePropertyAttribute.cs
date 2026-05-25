using System;

namespace System.Data.Common
{
	/// <summary>Identifies which provider-specific property in the strongly typed parameter classes is to be used when setting a provider-specific type.</summary>
	// Token: 0x020000CE RID: 206
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	[Serializable]
	public sealed class DbProviderSpecificTypePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of a <see cref="T:System.Data.Common.DbProviderSpecificTypePropertyAttribute" /> class.</summary>
		/// <param name="isProviderSpecificTypeProperty">Specifies whether this property is a provider-specific property.</param>
		// Token: 0x06000A01 RID: 2561 RVA: 0x0002F068 File Offset: 0x0002D268
		public DbProviderSpecificTypePropertyAttribute(bool isProviderSpecificTypeProperty)
		{
			this.isProviderSpecificTypeProperty = isProviderSpecificTypeProperty;
		}

		/// <summary>Indicates whether the attributed property is a provider-specific type.</summary>
		/// <returns>true if the property that this attribute is applied to is a provider-specific type property; otherwise false.</returns>
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x0002F078 File Offset: 0x0002D278
		public bool IsProviderSpecificTypeProperty
		{
			get
			{
				return this.isProviderSpecificTypeProperty;
			}
		}

		// Token: 0x04000370 RID: 880
		private bool isProviderSpecificTypeProperty;
	}
}
