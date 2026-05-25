using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007F RID: 127
	public class ErrorContext
	{
		// Token: 0x0600060D RID: 1549 RVA: 0x00015257 File Offset: 0x00013457
		internal ErrorContext(object originalObject, object member, Exception error)
		{
			this.OriginalObject = originalObject;
			this.Member = member;
			this.Error = error;
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00015274 File Offset: 0x00013474
		// (set) Token: 0x0600060F RID: 1551 RVA: 0x0001527C File Offset: 0x0001347C
		public Exception Error { get; private set; }

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00015285 File Offset: 0x00013485
		// (set) Token: 0x06000611 RID: 1553 RVA: 0x0001528D File Offset: 0x0001348D
		public object OriginalObject { get; private set; }

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00015296 File Offset: 0x00013496
		// (set) Token: 0x06000613 RID: 1555 RVA: 0x0001529E File Offset: 0x0001349E
		public object Member { get; private set; }

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000152A7 File Offset: 0x000134A7
		// (set) Token: 0x06000615 RID: 1557 RVA: 0x000152AF File Offset: 0x000134AF
		public bool Handled { get; set; }
	}
}
