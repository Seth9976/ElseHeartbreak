using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables security checking on calls to a component. This class cannot be inherited.</summary>
	// Token: 0x02000011 RID: 17
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ComponentAccessControlAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ComponentAccessControlAttribute" /> class.</summary>
		// Token: 0x06000036 RID: 54 RVA: 0x00002334 File Offset: 0x00000534
		public ComponentAccessControlAttribute()
		{
			this.val = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ComponentAccessControlAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.ComponentAccessControlAttribute.Value" /> property to indicate whether to enable COM+ security configuration.</summary>
		/// <param name="val">true to enable security checking on calls to a component; otherwise, false. </param>
		// Token: 0x06000037 RID: 55 RVA: 0x00002344 File Offset: 0x00000544
		public ComponentAccessControlAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value indicating whether to enable security checking on calls to a component.</summary>
		/// <returns>true if security checking is to be enabled; otherwise, false.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002354 File Offset: 0x00000554
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400003E RID: 62
		private bool val;
	}
}
