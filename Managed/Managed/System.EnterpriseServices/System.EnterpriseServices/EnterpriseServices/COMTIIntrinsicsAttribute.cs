using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables you to pass context properties from the COM Transaction Integrator (COMTI) into the COM+ context.</summary>
	// Token: 0x02000010 RID: 16
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class COMTIIntrinsicsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.COMTIIntrinsicsAttribute" /> class, setting the <see cref="P:System.EnterpriseServices.COMTIIntrinsicsAttribute.Value" /> property to true.</summary>
		// Token: 0x06000033 RID: 51 RVA: 0x0000230C File Offset: 0x0000050C
		public COMTIIntrinsicsAttribute()
		{
			this.val = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.COMTIIntrinsicsAttribute" /> class, enabling the setting of the <see cref="P:System.EnterpriseServices.COMTIIntrinsicsAttribute.Value" /> property.</summary>
		/// <param name="val">true if the COMTI context properties are passed into the COM+ context; otherwise, false. </param>
		// Token: 0x06000034 RID: 52 RVA: 0x0000231C File Offset: 0x0000051C
		public COMTIIntrinsicsAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value indicating whether the COM Transaction Integrator (COMTI) context properties are passed into the COM+ context.</summary>
		/// <returns>true if the COMTI context properties are passed into the COM+ context; otherwise, false. The default is true.</returns>
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000232C File Offset: 0x0000052C
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400003D RID: 61
		private bool val;
	}
}
