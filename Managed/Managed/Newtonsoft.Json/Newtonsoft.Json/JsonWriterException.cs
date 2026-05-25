using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x0200005C RID: 92
	[Serializable]
	public class JsonWriterException : Exception
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000E4B6 File Offset: 0x0000C6B6
		public JsonWriterException()
		{
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000E4BE File Offset: 0x0000C6BE
		public JsonWriterException(string message)
			: base(message)
		{
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000E4C7 File Offset: 0x0000C6C7
		public JsonWriterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000E4D1 File Offset: 0x0000C6D1
		public JsonWriterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
