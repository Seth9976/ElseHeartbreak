using System;

namespace UnityEngine
{
	// Token: 0x02000113 RID: 275
	public struct Color32
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x0001892C File Offset: 0x00016B2C
		public Color32(byte r, byte g, byte b, byte a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x0001894C File Offset: 0x00016B4C
		public override string ToString()
		{
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[] { this.r, this.g, this.b, this.a });
		}

		// Token: 0x06000A9D RID: 2717 RVA: 0x000189A4 File Offset: 0x00016BA4
		public string ToString(string format)
		{
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r.ToString(format),
				this.g.ToString(format),
				this.b.ToString(format),
				this.a.ToString(format)
			});
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x00018A00 File Offset: 0x00016C00
		public static Color32 Lerp(Color32 a, Color32 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x00018A8C File Offset: 0x00016C8C
		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)(Mathf.Clamp01(c.r) * 255f), (byte)(Mathf.Clamp01(c.g) * 255f), (byte)(Mathf.Clamp01(c.b) * 255f), (byte)(Mathf.Clamp01(c.a) * 255f));
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00018AEC File Offset: 0x00016CEC
		public static implicit operator Color(Color32 c)
		{
			return new Color((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}

		// Token: 0x040004E7 RID: 1255
		public byte r;

		// Token: 0x040004E8 RID: 1256
		public byte g;

		// Token: 0x040004E9 RID: 1257
		public byte b;

		// Token: 0x040004EA RID: 1258
		public byte a;
	}
}
