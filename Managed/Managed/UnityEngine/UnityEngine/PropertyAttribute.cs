using System;

namespace UnityEngine
{
	// Token: 0x02000039 RID: 57
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public abstract class PropertyAttribute : Attribute
	{
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003F24 File Offset: 0x00002124
		// (set) Token: 0x060000ED RID: 237 RVA: 0x00003F2C File Offset: 0x0000212C
		public int order { get; set; }
	}
}
