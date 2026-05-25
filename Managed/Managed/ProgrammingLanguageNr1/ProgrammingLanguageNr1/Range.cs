using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000031 RID: 49
	public struct Range
	{
		// Token: 0x0600018E RID: 398 RVA: 0x0000B2AC File Offset: 0x000094AC
		public Range(int pStart, int pEnd, int pStep)
		{
			this.start = (float)pStart;
			this.end = (float)pEnd;
			this.step = (float)pStep;
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000190 RID: 400 RVA: 0x0000B2D8 File Offset: 0x000094D8
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000B2E0 File Offset: 0x000094E0
		public float start { get; set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000192 RID: 402 RVA: 0x0000B2EC File Offset: 0x000094EC
		// (set) Token: 0x06000193 RID: 403 RVA: 0x0000B2F4 File Offset: 0x000094F4
		public float end { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000194 RID: 404 RVA: 0x0000B300 File Offset: 0x00009500
		// (set) Token: 0x06000195 RID: 405 RVA: 0x0000B308 File Offset: 0x00009508
		public float step { get; set; }

		// Token: 0x06000196 RID: 406 RVA: 0x0000B314 File Offset: 0x00009514
		public override string ToString()
		{
			return string.Format("(from {0} to {1})", this.start, this.end);
		}

		// Token: 0x040000C9 RID: 201
		private static Range NONE = new Range(0, 0, 0);
	}
}
