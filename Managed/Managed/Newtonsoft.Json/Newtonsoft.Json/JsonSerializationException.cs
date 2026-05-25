using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x02000060 RID: 96
	[Serializable]
	public class JsonSerializationException : Exception
	{
		// Token: 0x06000412 RID: 1042 RVA: 0x0000F04D File Offset: 0x0000D24D
		public JsonSerializationException()
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000F055 File Offset: 0x0000D255
		public JsonSerializationException(string message)
			: base(message)
		{
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000F05E File Offset: 0x0000D25E
		public JsonSerializationException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000F068 File Offset: 0x0000D268
		public JsonSerializationException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
