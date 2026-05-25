using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Accesses a shared property. This class cannot be inherited.</summary>
	// Token: 0x02000043 RID: 67
	[ComVisible(false)]
	public sealed class SharedProperty
	{
		// Token: 0x06000130 RID: 304 RVA: 0x00002B80 File Offset: 0x00000D80
		internal SharedProperty(ISharedProperty property)
		{
			this.property = property;
		}

		/// <summary>Gets or sets the value of the shared property.</summary>
		/// <returns>The value of the shared property.</returns>
		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000131 RID: 305 RVA: 0x00002B90 File Offset: 0x00000D90
		// (set) Token: 0x06000132 RID: 306 RVA: 0x00002BA0 File Offset: 0x00000DA0
		public object Value
		{
			get
			{
				return this.property.Value;
			}
			set
			{
				this.property.Value = value;
			}
		}

		// Token: 0x04000079 RID: 121
		private ISharedProperty property;
	}
}
