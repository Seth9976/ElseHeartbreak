using System;

namespace UnityEngine
{
	// Token: 0x02000112 RID: 274
	public struct Color
	{
		// Token: 0x06000A77 RID: 2679 RVA: 0x0001820C File Offset: 0x0001640C
		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x06000A78 RID: 2680 RVA: 0x0001822C File Offset: 0x0001642C
		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = 1f;
		}

		// Token: 0x06000A79 RID: 2681 RVA: 0x0001825C File Offset: 0x0001645C
		public override string ToString()
		{
			return UnityString.Format("RGBA({0:F3}, {1:F3}, {2:F3}, {3:F3})", new object[] { this.r, this.g, this.b, this.a });
		}

		// Token: 0x06000A7A RID: 2682 RVA: 0x000182B4 File Offset: 0x000164B4
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

		// Token: 0x06000A7B RID: 2683 RVA: 0x00018310 File Offset: 0x00016510
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06000A7C RID: 2684 RVA: 0x00018330 File Offset: 0x00016530
		public override bool Equals(object other)
		{
			if (!(other is Color))
			{
				return false;
			}
			Color color = (Color)other;
			return this.r.Equals(color.r) && this.g.Equals(color.g) && this.b.Equals(color.b) && this.a.Equals(color.a);
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x000183AC File Offset: 0x000165AC
		public static Color Lerp(Color a, Color b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x0001842C File Offset: 0x0001662C
		internal Color RGBMultiplied(float multiplier)
		{
			return new Color(this.r * multiplier, this.g * multiplier, this.b * multiplier, this.a);
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00018454 File Offset: 0x00016654
		internal Color AlphaMultiplied(float multiplier)
		{
			return new Color(this.r, this.g, this.b, this.a * multiplier);
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00018478 File Offset: 0x00016678
		internal Color RGBMultiplied(Color multiplier)
		{
			return new Color(this.r * multiplier.r, this.g * multiplier.g, this.b * multiplier.b, this.a);
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000184B0 File Offset: 0x000166B0
		public static Color red
		{
			get
			{
				return new Color(1f, 0f, 0f, 1f);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x000184CC File Offset: 0x000166CC
		public static Color green
		{
			get
			{
				return new Color(0f, 1f, 0f, 1f);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000184E8 File Offset: 0x000166E8
		public static Color blue
		{
			get
			{
				return new Color(0f, 0f, 1f, 1f);
			}
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x00018504 File Offset: 0x00016704
		public static Color white
		{
			get
			{
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00018520 File Offset: 0x00016720
		public static Color black
		{
			get
			{
				return new Color(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0001853C File Offset: 0x0001673C
		public static Color yellow
		{
			get
			{
				return new Color(1f, 0.92156863f, 0.015686275f, 1f);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00018558 File Offset: 0x00016758
		public static Color cyan
		{
			get
			{
				return new Color(0f, 1f, 1f, 1f);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x00018574 File Offset: 0x00016774
		public static Color magenta
		{
			get
			{
				return new Color(1f, 0f, 1f, 1f);
			}
		}

		// Token: 0x17000263 RID: 611
		// (get) Token: 0x06000A89 RID: 2697 RVA: 0x00018590 File Offset: 0x00016790
		public static Color gray
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A8A RID: 2698 RVA: 0x000185AC File Offset: 0x000167AC
		public static Color grey
		{
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A8B RID: 2699 RVA: 0x000185C8 File Offset: 0x000167C8
		public static Color clear
		{
			get
			{
				return new Color(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000A8C RID: 2700 RVA: 0x000185E4 File Offset: 0x000167E4
		public float grayscale
		{
			get
			{
				return 0.299f * this.r + 0.587f * this.g + 0.114f * this.b;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000A8D RID: 2701 RVA: 0x00018618 File Offset: 0x00016818
		public Color linear
		{
			get
			{
				return new Color(Mathf.GammaToLinearSpace(this.r), Mathf.GammaToLinearSpace(this.g), Mathf.GammaToLinearSpace(this.b), this.a);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A8E RID: 2702 RVA: 0x00018654 File Offset: 0x00016854
		public Color gamma
		{
			get
			{
				return new Color(Mathf.LinearToGammaSpace(this.r), Mathf.LinearToGammaSpace(this.g), Mathf.LinearToGammaSpace(this.b), this.a);
			}
		}

		// Token: 0x17000269 RID: 617
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.r;
				case 1:
					return this.g;
				case 2:
					return this.b;
				case 3:
					return this.a;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.r = value;
					break;
				case 1:
					this.g = value;
					break;
				case 2:
					this.b = value;
					break;
				case 3:
					this.a = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x06000A91 RID: 2705 RVA: 0x00018748 File Offset: 0x00016948
		public static Color operator +(Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}

		// Token: 0x06000A92 RID: 2706 RVA: 0x00018798 File Offset: 0x00016998
		public static Color operator -(Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		// Token: 0x06000A93 RID: 2707 RVA: 0x000187E8 File Offset: 0x000169E8
		public static Color operator *(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		// Token: 0x06000A94 RID: 2708 RVA: 0x00018838 File Offset: 0x00016A38
		public static Color operator *(Color a, float b)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x06000A95 RID: 2709 RVA: 0x00018864 File Offset: 0x00016A64
		public static Color operator *(float b, Color a)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x00018890 File Offset: 0x00016A90
		public static Color operator /(Color a, float b)
		{
			return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x000188BC File Offset: 0x00016ABC
		public static bool operator ==(Color lhs, Color rhs)
		{
			return lhs == rhs;
		}

		// Token: 0x06000A98 RID: 2712 RVA: 0x000188D0 File Offset: 0x00016AD0
		public static bool operator !=(Color lhs, Color rhs)
		{
			return lhs != rhs;
		}

		// Token: 0x06000A99 RID: 2713 RVA: 0x000188E4 File Offset: 0x00016AE4
		public static implicit operator Vector4(Color c)
		{
			return new Vector4(c.r, c.g, c.b, c.a);
		}

		// Token: 0x06000A9A RID: 2714 RVA: 0x00018908 File Offset: 0x00016B08
		public static implicit operator Color(Vector4 v)
		{
			return new Color(v.x, v.y, v.z, v.w);
		}

		// Token: 0x040004E3 RID: 1251
		public float r;

		// Token: 0x040004E4 RID: 1252
		public float g;

		// Token: 0x040004E5 RID: 1253
		public float b;

		// Token: 0x040004E6 RID: 1254
		public float a;
	}
}
