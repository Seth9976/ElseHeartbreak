using System;

namespace Newtonsoft.Json
{
	// Token: 0x0200003B RID: 59
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false)]
	public sealed class JsonObjectAttribute : JsonContainerAttribute
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00008CCC File Offset: 0x00006ECC
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00008CD4 File Offset: 0x00006ED4
		public MemberSerialization MemberSerialization
		{
			get
			{
				return this._memberSerialization;
			}
			set
			{
				this._memberSerialization = value;
			}
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00008CDD File Offset: 0x00006EDD
		public JsonObjectAttribute()
		{
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00008CE5 File Offset: 0x00006EE5
		public JsonObjectAttribute(MemberSerialization memberSerialization)
		{
			this.MemberSerialization = memberSerialization;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x00008CF4 File Offset: 0x00006EF4
		public JsonObjectAttribute(string id)
			: base(id)
		{
		}

		// Token: 0x040000B5 RID: 181
		private MemberSerialization _memberSerialization;
	}
}
