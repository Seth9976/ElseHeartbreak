using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Determines whether the component participates in load balancing, if the component load balancing service is installed and enabled on the server.</summary>
	// Token: 0x0200002D RID: 45
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class LoadBalancingSupportedAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.LoadBalancingSupportedAttribute" /> class, specifying load balancing support.</summary>
		// Token: 0x06000093 RID: 147 RVA: 0x000025B0 File Offset: 0x000007B0
		public LoadBalancingSupportedAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.LoadBalancingSupportedAttribute" /> class, optionally disabling load balancing support.</summary>
		/// <param name="val">true to enable load balancing support; otherwise, false. </param>
		// Token: 0x06000094 RID: 148 RVA: 0x000025BC File Offset: 0x000007BC
		public LoadBalancingSupportedAttribute(bool val)
		{
			this.val = val;
		}

		/// <summary>Gets a value that indicates whether load balancing support is enabled.</summary>
		/// <returns>true if load balancing support is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000025CC File Offset: 0x000007CC
		public bool Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x04000060 RID: 96
		private bool val;
	}
}
