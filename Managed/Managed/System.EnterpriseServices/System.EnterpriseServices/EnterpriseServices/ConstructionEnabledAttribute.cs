using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables COM+ object construction support. This class cannot be inherited.</summary>
	// Token: 0x02000012 RID: 18
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ConstructionEnabledAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ConstructionEnabledAttribute" /> class and initializes the default settings for <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Enabled" /> and <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Default" />.</summary>
		// Token: 0x06000039 RID: 57 RVA: 0x0000235C File Offset: 0x0000055C
		public ConstructionEnabledAttribute()
		{
			this.def = string.Empty;
			this.enabled = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ConstructionEnabledAttribute" /> class, setting <see cref="P:System.EnterpriseServices.ConstructionEnabledAttribute.Enabled" /> to the specified value.</summary>
		/// <param name="val">true to enable COM+ object construction support; otherwise, false. </param>
		// Token: 0x0600003A RID: 58 RVA: 0x00002378 File Offset: 0x00000578
		public ConstructionEnabledAttribute(bool val)
		{
			this.def = string.Empty;
			this.enabled = val;
		}

		/// <summary>Gets or sets a default value for the constructor string.</summary>
		/// <returns>The value to be used for the default constructor string. The default is an empty string ("").</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002394 File Offset: 0x00000594
		// (set) Token: 0x0600003C RID: 60 RVA: 0x0000239C File Offset: 0x0000059C
		public string Default
		{
			get
			{
				return this.def;
			}
			set
			{
				this.def = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether COM+ object construction support is enabled.</summary>
		/// <returns>true if COM+ object construction support is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000023A8 File Offset: 0x000005A8
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000023B0 File Offset: 0x000005B0
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		// Token: 0x0400003F RID: 63
		private string def;

		// Token: 0x04000040 RID: 64
		private bool enabled;
	}
}
