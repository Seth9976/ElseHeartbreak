using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json
{
	// Token: 0x0200005D RID: 93
	[Serializable]
	public class JsonReaderException : Exception
	{
		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000E4DB File Offset: 0x0000C6DB
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000E4E3 File Offset: 0x0000C6E3
		public int LineNumber { get; private set; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000E4EC File Offset: 0x0000C6EC
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000E4F4 File Offset: 0x0000C6F4
		public int LinePosition { get; private set; }

		// Token: 0x060003CB RID: 971 RVA: 0x0000E4FD File Offset: 0x0000C6FD
		public JsonReaderException()
		{
		}

		// Token: 0x060003CC RID: 972 RVA: 0x0000E505 File Offset: 0x0000C705
		public JsonReaderException(string message)
			: base(message)
		{
		}

		// Token: 0x060003CD RID: 973 RVA: 0x0000E50E File Offset: 0x0000C70E
		public JsonReaderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x060003CE RID: 974 RVA: 0x0000E518 File Offset: 0x0000C718
		public JsonReaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060003CF RID: 975 RVA: 0x0000E522 File Offset: 0x0000C722
		internal JsonReaderException(string message, Exception innerException, int lineNumber, int linePosition)
			: base(message, innerException)
		{
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}
