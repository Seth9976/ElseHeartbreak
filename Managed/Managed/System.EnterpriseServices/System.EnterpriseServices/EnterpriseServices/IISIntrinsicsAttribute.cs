using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables access to ASP intrinsic values from <see cref="M:System.EnterpriseServices.ContextUtil.GetNamedProperty(System.String)" />. This class cannot be inherited.</summary>
	// Token: 0x0200001A RID: 26
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class IISIntrinsicsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.IISIntrinsicsAttribute" /> class, enabling access to the ASP intrinsic values.</summary>
		// Token: 0x06000067 RID: 103 RVA: 0x00002518 File Offset: 0x00000718
		public IISIntrinsicsAttribute()
		{
			this.val = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.IISIntrinsicsAttribute" /> class, optionally disabling access to the ASP intrinsic values.</summary>
		/// <param name="val">true to enable access to the ASP intrinsic values; otherwise, false. </param>
		// Token: 0x06000068 RID: 104 RVA: 0x00002528 File Offset: 0x00000728
		public IISIntrinsicsAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value that indicates whether access to the ASP intrinsic values is enabled.</summary>
		/// <returns>true if access is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002538 File Offset: 0x00000738
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x04000048 RID: 72
		private bool val;
	}
}
