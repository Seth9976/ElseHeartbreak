using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting
{
	/// <summary>Implements a base class that holds the configuration information used to activate an instance of a remote type.</summary>
	// Token: 0x02000434 RID: 1076
	[ComVisible(true)]
	public class TypeEntry
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.TypeEntry" /> class.</summary>
		// Token: 0x06002DC7 RID: 11719 RVA: 0x00098940 File Offset: 0x00096B40
		protected TypeEntry()
		{
		}

		/// <summary>Gets the assembly name of the object type configured to be a remote-activated type.</summary>
		/// <returns>The assembly name of the object type configured to be a remote-activated type.</returns>
		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x06002DC8 RID: 11720 RVA: 0x00098948 File Offset: 0x00096B48
		// (set) Token: 0x06002DC9 RID: 11721 RVA: 0x00098950 File Offset: 0x00096B50
		public string AssemblyName
		{
			get
			{
				return this.assembly_name;
			}
			set
			{
				this.assembly_name = value;
			}
		}

		/// <summary>Gets the full type name of the object type configured to be a remote-activated type.</summary>
		/// <returns>The full type name of the object type configured to be a remote-activated type.</returns>
		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002DCA RID: 11722 RVA: 0x0009895C File Offset: 0x00096B5C
		// (set) Token: 0x06002DCB RID: 11723 RVA: 0x00098964 File Offset: 0x00096B64
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x040013AB RID: 5035
		private string assembly_name;

		// Token: 0x040013AC RID: 5036
		private string type_name;
	}
}
