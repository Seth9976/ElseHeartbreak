using System;

namespace UnityEngine
{
	// Token: 0x0200003E RID: 62
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class RangeAttribute : PropertyAttribute
	{
		// Token: 0x060000F2 RID: 242 RVA: 0x00003F80 File Offset: 0x00002180
		public RangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040000E1 RID: 225
		public readonly float min;

		// Token: 0x040000E2 RID: 226
		public readonly float max;
	}
}
