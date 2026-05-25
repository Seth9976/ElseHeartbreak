using System;

namespace Newtonsoft.Json
{
	// Token: 0x02000037 RID: 55
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, AllowMultiple = false)]
	public abstract class JsonContainerAttribute : Attribute
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00008BA4 File Offset: 0x00006DA4
		// (set) Token: 0x0600022F RID: 559 RVA: 0x00008BAC File Offset: 0x00006DAC
		public string Id { get; set; }

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00008BB5 File Offset: 0x00006DB5
		// (set) Token: 0x06000231 RID: 561 RVA: 0x00008BBD File Offset: 0x00006DBD
		public string Title { get; set; }

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000232 RID: 562 RVA: 0x00008BC6 File Offset: 0x00006DC6
		// (set) Token: 0x06000233 RID: 563 RVA: 0x00008BCE File Offset: 0x00006DCE
		public string Description { get; set; }

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00008BD8 File Offset: 0x00006DD8
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00008BFE File Offset: 0x00006DFE
		public bool IsReference
		{
			get
			{
				return this._isReference ?? false;
			}
			set
			{
				this._isReference = new bool?(value);
			}
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008C0C File Offset: 0x00006E0C
		protected JsonContainerAttribute()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008C14 File Offset: 0x00006E14
		protected JsonContainerAttribute(string id)
		{
			this.Id = id;
		}

		// Token: 0x040000AA RID: 170
		internal bool? _isReference;
	}
}
