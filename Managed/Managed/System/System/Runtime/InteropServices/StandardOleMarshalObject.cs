using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Replaces the standard common language runtime (CLR) free-threaded marshaler with the standard OLE STA marshaler. </summary>
	// Token: 0x020004CB RID: 1227
	[global::System.MonoLimitation("The runtime does nothing special apart from what it already does with marshal-by-ref objects")]
	[ComVisible(true)]
	public class StandardOleMarshalObject : MarshalByRefObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.StandardOleMarshalObject" /> class. </summary>
		// Token: 0x06002BE1 RID: 11233 RVA: 0x00098EA8 File Offset: 0x000970A8
		protected StandardOleMarshalObject()
		{
		}
	}
}
