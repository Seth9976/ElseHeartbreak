using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables queuing support for the marked interface. This class cannot be inherited.</summary>
	// Token: 0x0200001E RID: 30
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = true)]
	[ComVisible(false)]
	public sealed class InterfaceQueuingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.InterfaceQueuingAttribute" /> class setting the <see cref="P:System.EnterpriseServices.InterfaceQueuingAttribute.Enabled" /> and <see cref="P:System.EnterpriseServices.InterfaceQueuingAttribute.Interface" /> properties to their default values.</summary>
		// Token: 0x0600006A RID: 106 RVA: 0x00002540 File Offset: 0x00000740
		public InterfaceQueuingAttribute()
			: this(true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.InterfaceQueuingAttribute" /> class, optionally disabling queuing support.</summary>
		/// <param name="enabled">true to enable queuing support; otherwise, false. </param>
		// Token: 0x0600006B RID: 107 RVA: 0x0000254C File Offset: 0x0000074C
		public InterfaceQueuingAttribute(bool enabled)
		{
			this.enabled = enabled;
			this.interfaceName = null;
		}

		/// <summary>Gets or sets a value indicating whether queuing support is enabled.</summary>
		/// <returns>true if queuing support is enabled; otherwise, false. The default is true.</returns>
		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002564 File Offset: 0x00000764
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000256C File Offset: 0x0000076C
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

		/// <summary>Gets or sets the name of the interface on which queuing is enabled.</summary>
		/// <returns>The name of the interface on which queuing is enabled.</returns>
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002578 File Offset: 0x00000778
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002580 File Offset: 0x00000780
		public string Interface
		{
			get
			{
				return this.interfaceName;
			}
			set
			{
				this.interfaceName = value;
			}
		}

		// Token: 0x0400005D RID: 93
		private bool enabled;

		// Token: 0x0400005E RID: 94
		private string interfaceName;
	}
}
