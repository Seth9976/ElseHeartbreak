using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Enables Compensating Resource Manger (CRM) on the tagged application.</summary>
	// Token: 0x02000056 RID: 86
	[ProgId("System.EnterpriseServices.Crm.ApplicationCrmEnabledAttribute")]
	[AttributeUsage(AttributeTargets.Assembly)]
	[ComVisible(false)]
	public sealed class ApplicationCrmEnabledAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute" /> class, setting the <see cref="P:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute.Value" /> property to true.</summary>
		// Token: 0x0600014E RID: 334 RVA: 0x00002D0C File Offset: 0x00000F0C
		public ApplicationCrmEnabledAttribute()
		{
			this.val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute" /> class, optionally setting the <see cref="P:System.EnterpriseServices.CompensatingResourceManager.ApplicationCrmEnabledAttribute.Value" /> property to false.</summary>
		/// <param name="val">true to enable Compensating Resource Manager (CRM); otherwise, false. </param>
		// Token: 0x0600014F RID: 335 RVA: 0x00002D1C File Offset: 0x00000F1C
		public ApplicationCrmEnabledAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Enables or disables Compensating Resource Manager (CRM) on the tagged application.</summary>
		/// <returns>true if CRM is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000150 RID: 336 RVA: 0x00002D2C File Offset: 0x00000F2C
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x040000AB RID: 171
		private bool val;
	}
}
