using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Indicates the COM alias for a parameter or field type.</summary>
	// Token: 0x02000376 RID: 886
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class ComAliasNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.ComAliasNameAttribute" /> class with the alias for the attributed field or parameter.</summary>
		/// <param name="alias">The alias for the field or parameter as found in the type library when it was imported. </param>
		// Token: 0x06002A20 RID: 10784 RVA: 0x00092270 File Offset: 0x00090470
		public ComAliasNameAttribute(string alias)
		{
			this.val = alias;
		}

		/// <summary>Gets the alias for the field or parameter as found in the type library when it was imported.</summary>
		/// <returns>The alias for the field or parameter as found in the type library when it was imported.</returns>
		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06002A21 RID: 10785 RVA: 0x00092280 File Offset: 0x00090480
		public string Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x040010D6 RID: 4310
		private string val;
	}
}
