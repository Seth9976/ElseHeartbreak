using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000033 RID: 51
	public class ErrorEventArgs : EventArgs
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600021F RID: 543 RVA: 0x00008A88 File Offset: 0x00006C88
		// (set) Token: 0x06000220 RID: 544 RVA: 0x00008A90 File Offset: 0x00006C90
		public object CurrentObject { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000221 RID: 545 RVA: 0x00008A99 File Offset: 0x00006C99
		// (set) Token: 0x06000222 RID: 546 RVA: 0x00008AA1 File Offset: 0x00006CA1
		public ErrorContext ErrorContext { get; private set; }

		// Token: 0x06000223 RID: 547 RVA: 0x00008AAA File Offset: 0x00006CAA
		public ErrorEventArgs(object currentObject, ErrorContext errorContext)
		{
			this.CurrentObject = currentObject;
			this.ErrorContext = errorContext;
		}
	}
}
