using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000038 RID: 56
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonArrayAttribute : JsonContainerAttribute
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000238 RID: 568 RVA: 0x00008C23 File Offset: 0x00006E23
		// (set) Token: 0x06000239 RID: 569 RVA: 0x00008C2B File Offset: 0x00006E2B
		public bool AllowNullItems
		{
			get
			{
				return this._allowNullItems;
			}
			set
			{
				this._allowNullItems = value;
			}
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008C34 File Offset: 0x00006E34
		public JsonArrayAttribute()
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00008C3C File Offset: 0x00006E3C
		public JsonArrayAttribute(bool allowNullItems)
		{
			this._allowNullItems = allowNullItems;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00008C4B File Offset: 0x00006E4B
		public JsonArrayAttribute(string id)
			: base(id)
		{
		}

		// Token: 0x040000AE RID: 174
		private bool _allowNullItems;
	}
}
