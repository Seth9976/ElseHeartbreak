using System;
using System.Runtime.Serialization;

namespace UnityEngine
{
	// Token: 0x02000049 RID: 73
	[Serializable]
	public class MissingComponentException : SystemException
	{
		// Token: 0x0600012A RID: 298 RVA: 0x00005284 File Offset: 0x00003484
		public MissingComponentException()
			: base("A Unity Runtime error occurred!")
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000529C File Offset: 0x0000349C
		public MissingComponentException(string message)
			: base(message)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000052B0 File Offset: 0x000034B0
		public MissingComponentException(string message, Exception innerException)
			: base(message, innerException)
		{
			base.HResult = -2147467261;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x000052C8 File Offset: 0x000034C8
		protected MissingComponentException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x040000F7 RID: 247
		private const int Result = -2147467261;

		// Token: 0x040000F8 RID: 248
		private string unityStackTrace;
	}
}
