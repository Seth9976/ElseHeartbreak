using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the release mode for the properties in the new shared property group.</summary>
	// Token: 0x02000033 RID: 51
	[ComVisible(false)]
	[Serializable]
	public enum PropertyReleaseMode
	{
		/// <summary>The property group is not destroyed until the process in which it was created has terminated.</summary>
		// Token: 0x0400006E RID: 110
		Process = 1,
		/// <summary>When all clients have released their references on the property group, the property group is automatically destroyed. This is the default COM mode.</summary>
		// Token: 0x0400006F RID: 111
		Standard = 0
	}
}
