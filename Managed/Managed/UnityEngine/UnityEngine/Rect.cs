using System;

namespace UnityEngine
{
	// Token: 0x02000115 RID: 277
	public struct Rect
	{
		// Token: 0x06000ADD RID: 2781 RVA: 0x000192F8 File Offset: 0x000174F8
		public Rect(float left, float top, float width, float height)
		{
			this.m_XMin = left;
			this.m_YMin = top;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x00019318 File Offset: 0x00017518
		public Rect(Rect source)
		{
			this.m_XMin = source.m_XMin;
			this.m_YMin = source.m_YMin;
			this.m_Width = source.m_Width;
			this.m_Height = source.m_Height;
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x0001935C File Offset: 0x0001755C
		public static Rect MinMaxRect(float left, float top, float right, float bottom)
		{
			return new Rect(left, top, right - left, bottom - top);
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x0001936C File Offset: 0x0001756C
		public void Set(float left, float top, float width, float height)
		{
			this.m_XMin = left;
			this.m_YMin = top;
			this.m_Width = width;
			this.m_Height = height;
		}

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000AE1 RID: 2785 RVA: 0x0001938C File Offset: 0x0001758C
		// (set) Token: 0x06000AE2 RID: 2786 RVA: 0x00019394 File Offset: 0x00017594
		public float x
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				this.m_XMin = value;
			}
		}

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000AE3 RID: 2787 RVA: 0x000193A0 File Offset: 0x000175A0
		// (set) Token: 0x06000AE4 RID: 2788 RVA: 0x000193A8 File Offset: 0x000175A8
		public float y
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				this.m_YMin = value;
			}
		}

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AE5 RID: 2789 RVA: 0x000193B4 File Offset: 0x000175B4
		// (set) Token: 0x06000AE6 RID: 2790 RVA: 0x000193C8 File Offset: 0x000175C8
		public Vector2 position
		{
			get
			{
				return new Vector2(this.m_XMin, this.m_YMin);
			}
			set
			{
				this.m_XMin = value.x;
				this.m_YMin = value.y;
			}
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AE7 RID: 2791 RVA: 0x000193E4 File Offset: 0x000175E4
		// (set) Token: 0x06000AE8 RID: 2792 RVA: 0x0001941C File Offset: 0x0001761C
		public Vector2 center
		{
			get
			{
				return new Vector2(this.x + this.m_Width / 2f, this.y + this.m_Height / 2f);
			}
			set
			{
				this.m_XMin = value.x - this.m_Width / 2f;
				this.m_YMin = value.y - this.m_Height / 2f;
			}
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000AE9 RID: 2793 RVA: 0x00019460 File Offset: 0x00017660
		// (set) Token: 0x06000AEA RID: 2794 RVA: 0x00019474 File Offset: 0x00017674
		public Vector2 min
		{
			get
			{
				return new Vector2(this.xMin, this.yMin);
			}
			set
			{
				this.xMin = value.x;
				this.yMin = value.y;
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x00019490 File Offset: 0x00017690
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x000194A4 File Offset: 0x000176A4
		public Vector2 max
		{
			get
			{
				return new Vector2(this.xMax, this.yMax);
			}
			set
			{
				this.xMax = value.x;
				this.yMax = value.y;
			}
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x000194C0 File Offset: 0x000176C0
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x000194C8 File Offset: 0x000176C8
		public float width
		{
			get
			{
				return this.m_Width;
			}
			set
			{
				this.m_Width = value;
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000194D4 File Offset: 0x000176D4
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x000194DC File Offset: 0x000176DC
		public float height
		{
			get
			{
				return this.m_Height;
			}
			set
			{
				this.m_Height = value;
			}
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000194E8 File Offset: 0x000176E8
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x000194FC File Offset: 0x000176FC
		public Vector2 size
		{
			get
			{
				return new Vector2(this.m_Width, this.m_Height);
			}
			set
			{
				this.m_Width = value.x;
				this.m_Height = value.y;
			}
		}

		// Token: 0x17000276 RID: 630
		// (get) Token: 0x06000AF3 RID: 2803 RVA: 0x00019518 File Offset: 0x00017718
		[Obsolete("use xMin")]
		public float left
		{
			get
			{
				return this.m_XMin;
			}
		}

		// Token: 0x17000277 RID: 631
		// (get) Token: 0x06000AF4 RID: 2804 RVA: 0x00019520 File Offset: 0x00017720
		[Obsolete("use xMax")]
		public float right
		{
			get
			{
				return this.m_XMin + this.m_Width;
			}
		}

		// Token: 0x17000278 RID: 632
		// (get) Token: 0x06000AF5 RID: 2805 RVA: 0x00019530 File Offset: 0x00017730
		[Obsolete("use yMin")]
		public float top
		{
			get
			{
				return this.m_YMin;
			}
		}

		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000AF6 RID: 2806 RVA: 0x00019538 File Offset: 0x00017738
		[Obsolete("use yMax")]
		public float bottom
		{
			get
			{
				return this.m_YMin + this.m_Height;
			}
		}

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00019548 File Offset: 0x00017748
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x00019550 File Offset: 0x00017750
		public float xMin
		{
			get
			{
				return this.m_XMin;
			}
			set
			{
				float xMax = this.xMax;
				this.m_XMin = value;
				this.m_Width = xMax - this.m_XMin;
			}
		}

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000AF9 RID: 2809 RVA: 0x0001957C File Offset: 0x0001777C
		// (set) Token: 0x06000AFA RID: 2810 RVA: 0x00019584 File Offset: 0x00017784
		public float yMin
		{
			get
			{
				return this.m_YMin;
			}
			set
			{
				float yMax = this.yMax;
				this.m_YMin = value;
				this.m_Height = yMax - this.m_YMin;
			}
		}

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000AFB RID: 2811 RVA: 0x000195B0 File Offset: 0x000177B0
		// (set) Token: 0x06000AFC RID: 2812 RVA: 0x000195C0 File Offset: 0x000177C0
		public float xMax
		{
			get
			{
				return this.m_Width + this.m_XMin;
			}
			set
			{
				this.m_Width = value - this.m_XMin;
			}
		}

		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000AFD RID: 2813 RVA: 0x000195D0 File Offset: 0x000177D0
		// (set) Token: 0x06000AFE RID: 2814 RVA: 0x000195E0 File Offset: 0x000177E0
		public float yMax
		{
			get
			{
				return this.m_Height + this.m_YMin;
			}
			set
			{
				this.m_Height = value - this.m_YMin;
			}
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000195F0 File Offset: 0x000177F0
		public override string ToString()
		{
			return UnityString.Format("(x:{0:F2}, y:{1:F2}, width:{2:F2}, height:{3:F2})", new object[] { this.x, this.y, this.width, this.height });
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x00019648 File Offset: 0x00017848
		public string ToString(string format)
		{
			return UnityString.Format("(x:{0}, y:{1}, width:{2}, height:{3})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format),
				this.width.ToString(format),
				this.height.ToString(format)
			});
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000196B0 File Offset: 0x000178B0
		public bool Contains(Vector2 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x00019708 File Offset: 0x00017908
		public bool Contains(Vector3 point)
		{
			return point.x >= this.xMin && point.x < this.xMax && point.y >= this.yMin && point.y < this.yMax;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x00019760 File Offset: 0x00017960
		public bool Contains(Vector3 point, bool allowInverse)
		{
			if (!allowInverse)
			{
				return this.Contains(point);
			}
			bool flag = false;
			if ((this.width < 0f && point.x <= this.xMin && point.x > this.xMax) || (this.width >= 0f && point.x >= this.xMin && point.x < this.xMax))
			{
				flag = true;
			}
			return flag && ((this.height < 0f && point.y <= this.yMin && point.y > this.yMax) || (this.height >= 0f && point.y >= this.yMin && point.y < this.yMax));
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x00019858 File Offset: 0x00017A58
		private static Rect OrderMinMax(Rect rect)
		{
			if (rect.xMin > rect.xMax)
			{
				float xMin = rect.xMin;
				rect.xMin = rect.xMax;
				rect.xMax = xMin;
			}
			if (rect.yMin > rect.yMax)
			{
				float yMin = rect.yMin;
				rect.yMin = rect.yMax;
				rect.yMax = yMin;
			}
			return rect;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000198C8 File Offset: 0x00017AC8
		public bool Overlaps(Rect other)
		{
			return other.xMax > this.xMin && other.xMin < this.xMax && other.yMax > this.yMin && other.yMin < this.yMax;
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x00019920 File Offset: 0x00017B20
		public bool Overlaps(Rect other, bool allowInverse)
		{
			Rect rect = this;
			if (allowInverse)
			{
				rect = Rect.OrderMinMax(rect);
				other = Rect.OrderMinMax(other);
			}
			return rect.Overlaps(other);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x00019954 File Offset: 0x00017B54
		public static Vector2 NormalizedToPoint(Rect rectangle, Vector2 normalizedRectCoordinates)
		{
			return new Vector2(Mathf.Lerp(rectangle.x, rectangle.xMax, normalizedRectCoordinates.x), Mathf.Lerp(rectangle.y, rectangle.yMax, normalizedRectCoordinates.y));
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x0001999C File Offset: 0x00017B9C
		public static Vector2 PointToNormalized(Rect rectangle, Vector2 point)
		{
			return new Vector2(Mathf.InverseLerp(rectangle.x, rectangle.xMax, point.x), Mathf.InverseLerp(rectangle.y, rectangle.yMax, point.y));
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000199E4 File Offset: 0x00017BE4
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.width.GetHashCode() << 2) ^ (this.y.GetHashCode() >> 2) ^ (this.height.GetHashCode() >> 1);
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x00019A34 File Offset: 0x00017C34
		public override bool Equals(object other)
		{
			if (!(other is Rect))
			{
				return false;
			}
			Rect rect = (Rect)other;
			return this.x.Equals(rect.x) && this.y.Equals(rect.y) && this.width.Equals(rect.width) && this.height.Equals(rect.height);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x00019ABC File Offset: 0x00017CBC
		public static bool operator !=(Rect lhs, Rect rhs)
		{
			return lhs.x != rhs.x || lhs.y != rhs.y || lhs.width != rhs.width || lhs.height != rhs.height;
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x00019B18 File Offset: 0x00017D18
		public static bool operator ==(Rect lhs, Rect rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.width == rhs.width && lhs.height == rhs.height;
		}

		// Token: 0x040004F0 RID: 1264
		private float m_XMin;

		// Token: 0x040004F1 RID: 1265
		private float m_YMin;

		// Token: 0x040004F2 RID: 1266
		private float m_Width;

		// Token: 0x040004F3 RID: 1267
		private float m_Height;
	}
}
