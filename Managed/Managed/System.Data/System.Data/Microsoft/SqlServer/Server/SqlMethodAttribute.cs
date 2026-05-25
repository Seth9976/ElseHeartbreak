using System;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Indicates the determinism and data access properties of a method or property on a user-defined type (UDT). The properties on the attribute reflect the physical characteristics that are used when the type is registered with SQL Server.</summary>
	// Token: 0x0200014A RID: 330
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	[Serializable]
	public sealed class SqlMethodAttribute : SqlFunctionAttribute
	{
		/// <summary>An attribute on a user-defined type (UDT), used to indicate the determinism and data access properties of a method or a property on a UDT.</summary>
		// Token: 0x060011AE RID: 4526 RVA: 0x00045C30 File Offset: 0x00043E30
		public SqlMethodAttribute()
		{
			this.isMutator = false;
			this.onNullCall = false;
		}

		/// <summary>Indicates whether a method on a user-defined type (UDT) is a mutator.</summary>
		/// <returns>true if the method is a mutator; otherwise false.</returns>
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x060011AF RID: 4527 RVA: 0x00045C48 File Offset: 0x00043E48
		// (set) Token: 0x060011B0 RID: 4528 RVA: 0x00045C50 File Offset: 0x00043E50
		public bool IsMutator
		{
			get
			{
				return this.isMutator;
			}
			set
			{
				this.isMutator = value;
			}
		}

		/// <summary>Indicates whether the method on a user-defined type (UDT) is called when null input arguments are specified in the method invocation.</summary>
		/// <returns>true if the method is called when null input arguments are specified in the method invocation; false if the method returns a null value when any of its input parameters are null.</returns>
		// Token: 0x17000309 RID: 777
		// (get) Token: 0x060011B1 RID: 4529 RVA: 0x00045C5C File Offset: 0x00043E5C
		// (set) Token: 0x060011B2 RID: 4530 RVA: 0x00045C64 File Offset: 0x00043E64
		public bool OnNullCall
		{
			get
			{
				return this.onNullCall;
			}
			set
			{
				this.onNullCall = value;
			}
		}

		// Token: 0x0400068B RID: 1675
		private bool isMutator;

		// Token: 0x0400068C RID: 1676
		private bool onNullCall;
	}
}
