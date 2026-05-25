using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Schema
{
	// Token: 0x02000077 RID: 119
	public class ValidationEventArgs : EventArgs
	{
		// Token: 0x060005C9 RID: 1481 RVA: 0x00013DAF File Offset: 0x00011FAF
		internal ValidationEventArgs(JsonSchemaException ex)
		{
			ValidationUtils.ArgumentNotNull(ex, "ex");
			this._ex = ex;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x00013DC9 File Offset: 0x00011FC9
		public JsonSchemaException Exception
		{
			get
			{
				return this._ex;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x060005CB RID: 1483 RVA: 0x00013DD1 File Offset: 0x00011FD1
		public string Message
		{
			get
			{
				return this._ex.Message;
			}
		}

		// Token: 0x0400018D RID: 397
		private readonly JsonSchemaException _ex;
	}
}
