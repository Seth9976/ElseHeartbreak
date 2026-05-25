using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Sets the synchronization value of the component. This class cannot be inherited.</summary>
	// Token: 0x02000047 RID: 71
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class SynchronizationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SynchronizationAttribute" /> class with the default <see cref="T:System.EnterpriseServices.SynchronizationOption" />.</summary>
		// Token: 0x0600013C RID: 316 RVA: 0x00002C30 File Offset: 0x00000E30
		public SynchronizationAttribute()
			: this(SynchronizationOption.Required)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SynchronizationAttribute" /> class with the specified <see cref="T:System.EnterpriseServices.SynchronizationOption" />.</summary>
		/// <param name="val">One of the <see cref="T:System.EnterpriseServices.SynchronizationOption" /> values. </param>
		// Token: 0x0600013D RID: 317 RVA: 0x00002C3C File Offset: 0x00000E3C
		public SynchronizationAttribute(SynchronizationOption val)
		{
			this.val = val;
		}

		/// <summary>Gets the current setting of the <see cref="P:System.EnterpriseServices.SynchronizationAttribute.Value" /> property.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.SynchronizationOption" /> values. The default is Required.</returns>
		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00002C4C File Offset: 0x00000E4C
		public SynchronizationOption Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400007F RID: 127
		private SynchronizationOption val;
	}
}
