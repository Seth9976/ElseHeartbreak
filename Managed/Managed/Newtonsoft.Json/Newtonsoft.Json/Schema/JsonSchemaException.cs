using System;
using System.Runtime.Serialization;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x0200006F RID: 111
	[Serializable]
	public class JsonSchemaException : Exception
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x00012C1C File Offset: 0x00010E1C
		// (set) Token: 0x06000570 RID: 1392 RVA: 0x00012C24 File Offset: 0x00010E24
		public int LineNumber { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00012C2D File Offset: 0x00010E2D
		// (set) Token: 0x06000572 RID: 1394 RVA: 0x00012C35 File Offset: 0x00010E35
		public int LinePosition { get; private set; }

		// Token: 0x06000573 RID: 1395 RVA: 0x00012C3E File Offset: 0x00010E3E
		public JsonSchemaException()
		{
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00012C46 File Offset: 0x00010E46
		public JsonSchemaException(string message)
			: base(message)
		{
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00012C4F File Offset: 0x00010E4F
		public JsonSchemaException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x00012C59 File Offset: 0x00010E59
		public JsonSchemaException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x00012C63 File Offset: 0x00010E63
		internal JsonSchemaException(string message, Exception innerException, int lineNumber, int linePosition)
			: base(message, innerException)
		{
			this.LineNumber = lineNumber;
			this.LinePosition = linePosition;
		}
	}
}
