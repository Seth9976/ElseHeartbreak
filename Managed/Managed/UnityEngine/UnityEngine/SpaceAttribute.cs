using System;

namespace UnityEngine
{
	// Token: 0x0200003C RID: 60
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class SpaceAttribute : PropertyAttribute
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00003F60 File Offset: 0x00002160
		public SpaceAttribute(float height)
		{
			this.height = height;
		}

		// Token: 0x040000DF RID: 223
		public readonly float height;
	}
}
