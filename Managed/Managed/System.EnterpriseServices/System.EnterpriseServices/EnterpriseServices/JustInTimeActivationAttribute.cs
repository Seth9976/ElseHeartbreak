using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Turns just-in-time (JIT) activation on or off. This class cannot be inherited.</summary>
	// Token: 0x0200002C RID: 44
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class JustInTimeActivationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.JustInTimeActivationAttribute" /> class. The default constructor enables just-in-time (JIT) activation.</summary>
		// Token: 0x06000090 RID: 144 RVA: 0x0000258C File Offset: 0x0000078C
		public JustInTimeActivationAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.JustInTimeActivationAttribute" /> class, optionally allowing the disabling of just-in-time (JIT) activation by passing false as the parameter.</summary>
		/// <param name="val">true to enable JIT activation; otherwise, false. </param>
		// Token: 0x06000091 RID: 145 RVA: 0x00002598 File Offset: 0x00000798
		public JustInTimeActivationAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets the value of the <see cref="T:System.EnterpriseServices.JustInTimeActivationAttribute" /> setting.</summary>
		/// <returns>true if JIT activation is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000025A8 File Offset: 0x000007A8
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400005F RID: 95
		private bool val;
	}
}
