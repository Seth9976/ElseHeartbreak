using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class MissingReferenceException : SystemException
	{
		// Token: 0x06000132 RID: 306 RVA: 0x00005324 File Offset: 0x00003524
		public MissingReferenceException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000533C File Offset: 0x0000353C
		public MissingReferenceException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00005350 File Offset: 0x00003550
		public MissingReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00005368 File Offset: 0x00003568
		protected MissingReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040000FB RID: 251
		private const int Result = -2147467261;

		// Token: 0x040000FC RID: 252
		private string unityStackTrace;
	}
}
