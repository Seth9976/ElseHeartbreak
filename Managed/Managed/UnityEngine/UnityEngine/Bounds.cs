using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000117 RID: 279
	public struct Bounds
	{
		// Token: 0x06000B31 RID: 2865 RVA: 0x0001AD44 File Offset: 0x00018F44
		public Bounds(Vector3 center, Vector3 size)
		{
			this.m_Center = center;
			this.m_Extents = size * 0.5f;
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x0001AD60 File Offset: 0x00018F60
		public override int GetHashCode()
		{
			return this.center.GetHashCode() ^ (this.extents.GetHashCode() << 2);
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x0001AD8C File Offset: 0x00018F8C
		public override bool Equals(object other)
		{
			if (!(other is Bounds))
			{
				return false;
			}
			Bounds bounds = (Bounds)other;
			return this.center.Equals(bounds.center) && this.extents.Equals(bounds.extents);
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000B34 RID: 2868 RVA: 0x0001ADEC File Offset: 0x00018FEC
		// (set) Token: 0x06000B35 RID: 2869 RVA: 0x0001ADF4 File Offset: 0x00018FF4
		public Vector3 center
		{
			get
			{
				return this.m_Center;
			}
			set
			{
				this.m_Center = value;
			}
		}

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000B36 RID: 2870 RVA: 0x0001AE00 File Offset: 0x00019000
		// (set) Token: 0x06000B37 RID: 2871 RVA: 0x0001AE14 File Offset: 0x00019014
		public Vector3 size
		{
			get
			{
				return this.m_Extents * 2f;
			}
			set
			{
				this.m_Extents = value * 0.5f;
			}
		}

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000B38 RID: 2872 RVA: 0x0001AE28 File Offset: 0x00019028
		// (set) Token: 0x06000B39 RID: 2873 RVA: 0x0001AE30 File Offset: 0x00019030
		public Vector3 extents
		{
			get
			{
				return this.m_Extents;
			}
			set
			{
				this.m_Extents = value;
			}
		}

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000B3A RID: 2874 RVA: 0x0001AE3C File Offset: 0x0001903C
		// (set) Token: 0x06000B3B RID: 2875 RVA: 0x0001AE50 File Offset: 0x00019050
		public Vector3 min
		{
			get
			{
				return this.center - this.extents;
			}
			set
			{
				this.SetMinMax(value, this.max);
			}
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x0001AE60 File Offset: 0x00019060
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x0001AE74 File Offset: 0x00019074
		public Vector3 max
		{
			get
			{
				return this.center + this.extents;
			}
			set
			{
				this.SetMinMax(this.min, value);
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x0001AE84 File Offset: 0x00019084
		public void SetMinMax(Vector3 min, Vector3 max)
		{
			this.extents = (max - min) * 0.5f;
			this.center = min + this.extents;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x0001AEBC File Offset: 0x000190BC
		public void Encapsulate(Vector3 point)
		{
			this.SetMinMax(Vector3.Min(this.min, point), Vector3.Max(this.max, point));
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x0001AEE8 File Offset: 0x000190E8
		public void Encapsulate(Bounds bounds)
		{
			this.Encapsulate(bounds.center - bounds.extents);
			this.Encapsulate(bounds.center + bounds.extents);
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x0001AF28 File Offset: 0x00019128
		public void Expand(float amount)
		{
			amount *= 0.5f;
			this.extents += new Vector3(amount, amount, amount);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x0001AF58 File Offset: 0x00019158
		public void Expand(Vector3 amount)
		{
			this.extents += amount * 0.5f;
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x0001AF78 File Offset: 0x00019178
		public bool Intersects(Bounds bounds)
		{
			return this.min.x <= bounds.max.x && this.max.x >= bounds.min.x && this.min.y <= bounds.max.y && this.max.y >= bounds.min.y && this.min.z <= bounds.max.z && this.max.z >= bounds.min.z;
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x0001B05C File Offset: 0x0001925C
		private static bool Internal_Contains(Bounds m, Vector3 point)
		{
			return Bounds.INTERNAL_CALL_Internal_Contains(ref m, ref point);
		}

		// Token: 0x06000B45 RID: 2885
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_Contains(ref Bounds m, ref Vector3 point);

		// Token: 0x06000B46 RID: 2886 RVA: 0x0001B068 File Offset: 0x00019268
		public bool Contains(Vector3 point)
		{
			return Bounds.Internal_Contains(this, point);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x0001B078 File Offset: 0x00019278
		private static float Internal_SqrDistance(Bounds m, Vector3 point)
		{
			return Bounds.INTERNAL_CALL_Internal_SqrDistance(ref m, ref point);
		}

		// Token: 0x06000B48 RID: 2888
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float INTERNAL_CALL_Internal_SqrDistance(ref Bounds m, ref Vector3 point);

		// Token: 0x06000B49 RID: 2889 RVA: 0x0001B084 File Offset: 0x00019284
		public float SqrDistance(Vector3 point)
		{
			return Bounds.Internal_SqrDistance(this, point);
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x0001B094 File Offset: 0x00019294
		private static bool Internal_IntersectRay(ref Ray ray, ref Bounds bounds, out float distance)
		{
			return Bounds.INTERNAL_CALL_Internal_IntersectRay(ref ray, ref bounds, out distance);
		}

		// Token: 0x06000B4B RID: 2891
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_IntersectRay(ref Ray ray, ref Bounds bounds, out float distance);

		// Token: 0x06000B4C RID: 2892 RVA: 0x0001B0A0 File Offset: 0x000192A0
		public bool IntersectRay(Ray ray)
		{
			float num;
			return Bounds.Internal_IntersectRay(ref ray, ref this, out num);
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x0001B0B8 File Offset: 0x000192B8
		public bool IntersectRay(Ray ray, out float distance)
		{
			return Bounds.Internal_IntersectRay(ref ray, ref this, out distance);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x0001B0C4 File Offset: 0x000192C4
		public override string ToString()
		{
			return UnityString.Format("Center: {0}, Extents: {1}", new object[] { this.m_Center, this.m_Extents });
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x0001B100 File Offset: 0x00019300
		public string ToString(string format)
		{
			return UnityString.Format("Center: {0}, Extents: {1}", new object[]
			{
				this.m_Center.ToString(format),
				this.m_Extents.ToString(format)
			});
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x0001B13C File Offset: 0x0001933C
		public static bool operator ==(Bounds lhs, Bounds rhs)
		{
			return lhs.center == rhs.center && lhs.extents == rhs.extents;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x0001B178 File Offset: 0x00019378
		public static bool operator !=(Bounds lhs, Bounds rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x04000504 RID: 1284
		private Vector3 m_Center;

		// Token: 0x04000505 RID: 1285
		private Vector3 m_Extents;
	}
}
