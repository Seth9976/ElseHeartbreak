using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Marks the attributed method as an AutoComplete object. This class cannot be inherited.</summary>
	// Token: 0x0200000C RID: 12
	[AttributeUsage(AttributeTargets.Method)]
	[ComVisible(false)]
	public sealed class AutoCompleteAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.AutoCompleteAttribute" /> class, specifying that the application should automatically call <see cref="M:System.EnterpriseServices.ContextUtil.SetComplete" /> if the transaction completes successfully.</summary>
		// Token: 0x0600002D RID: 45 RVA: 0x000022CC File Offset: 0x000004CC
		public AutoCompleteAttribute()
		{
			this.val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.AutoCompleteAttribute" /> class, specifying whether COM+ AutoComplete is enabled.</summary>
		/// <param name="val">true to enable AutoComplete in the COM+ object; otherwise, false. </param>
		// Token: 0x0600002E RID: 46 RVA: 0x000022DC File Offset: 0x000004DC
		public AutoCompleteAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value indicating the setting of the AutoComplete option in COM+.</summary>
		/// <returns>true if AutoComplete is enabled in COM+; otherwise, false. The default is true.</returns>
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000022EC File Offset: 0x000004EC
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x04000038 RID: 56
		private bool val;
	}
}
