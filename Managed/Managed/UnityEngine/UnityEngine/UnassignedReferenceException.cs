using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x0200004A RID: 74
	[Serializable]
	public class UnassignedReferenceException : SystemException
	{
		// Token: 0x0600012E RID: 302 RVA: 0x000052D4 File Offset: 0x000034D4
		public UnassignedReferenceException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000052EC File Offset: 0x000034EC
		public UnassignedReferenceException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00005300 File Offset: 0x00003500
		public UnassignedReferenceException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005318 File Offset: 0x00003518
		protected UnassignedReferenceException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040000F9 RID: 249
		private const int Result = -2147467261;

		// Token: 0x040000FA RID: 250
		private string unityStackTrace;
	}
}
