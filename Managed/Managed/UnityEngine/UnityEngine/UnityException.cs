using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x02000048 RID: 72
	[Serializable]
	public class UnityException : SystemException
	{
		// Token: 0x06000126 RID: 294 RVA: 0x00005234 File Offset: 0x00003434
		public UnityException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000524C File Offset: 0x0000344C
		public UnityException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005260 File Offset: 0x00003460
		public UnityException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005278 File Offset: 0x00003478
		protected UnityException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040000F5 RID: 245
		private const int Result = -2147467261;

		// Token: 0x040000F6 RID: 246
		private string unityStackTrace;
	}
}
