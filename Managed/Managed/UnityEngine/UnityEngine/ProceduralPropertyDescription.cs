using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000141 RID: 321
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ProceduralPropertyDescription
	{
		// Token: 0x040005A0 RID: 1440
		public string name;

		// Token: 0x040005A1 RID: 1441
		public string label;

		// Token: 0x040005A2 RID: 1442
		public string group;

		// Token: 0x040005A3 RID: 1443
		public ProceduralPropertyType type;

		// Token: 0x040005A4 RID: 1444
		public bool hasRange;

		// Token: 0x040005A5 RID: 1445
		public float minimum;

		// Token: 0x040005A6 RID: 1446
		public float maximum;

		// Token: 0x040005A7 RID: 1447
		public float step;

		// Token: 0x040005A8 RID: 1448
		public string[] enumOptions;

		// Token: 0x040005A9 RID: 1449
		public string[] componentLabels;
	}
}
