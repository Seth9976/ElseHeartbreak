using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables event tracking for a component. This class cannot be inherited.</summary>
	// Token: 0x02000016 RID: 22
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventTrackingEnabledAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.EventTrackingEnabledAttribute" /> class, enabling event tracking.</summary>
		// Token: 0x0600005E RID: 94 RVA: 0x000024D8 File Offset: 0x000006D8
		public EventTrackingEnabledAttribute()
		{
			this.val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.EventTrackingEnabledAttribute" /> class, optionally disabling event tracking.</summary>
		/// <param name="val">true to enable event tracking; otherwise, false. </param>
		// Token: 0x0600005F RID: 95 RVA: 0x000024E8 File Offset: 0x000006E8
		public EventTrackingEnabledAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets the value of the <see cref="P:System.EnterpriseServices.EventTrackingEnabledAttribute.Value" /> property, which indicates whether tracking is enabled.</summary>
		/// <returns>true if tracking is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000024F8 File Offset: 0x000006F8
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x04000046 RID: 70
		private bool val;
	}
}
