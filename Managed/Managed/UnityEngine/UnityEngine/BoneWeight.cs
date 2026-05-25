using System;

namespace UnityEngine
{
	// Token: 0x020000AC RID: 172
	public struct BoneWeight
	{
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x0000B754 File Offset: 0x00009954
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0000B75C File Offset: 0x0000995C
		public float weight0
		{
			get
			{
				return this.m_Weight0;
			}
			set
			{
				this.m_Weight0 = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000B768 File Offset: 0x00009968
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x0000B770 File Offset: 0x00009970
		public float weight1
		{
			get
			{
				return this.m_Weight1;
			}
			set
			{
				this.m_Weight1 = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x0000B77C File Offset: 0x0000997C
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0000B784 File Offset: 0x00009984
		public float weight2
		{
			get
			{
				return this.m_Weight2;
			}
			set
			{
				this.m_Weight2 = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000B790 File Offset: 0x00009990
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000B798 File Offset: 0x00009998
		public float weight3
		{
			get
			{
				return this.m_Weight3;
			}
			set
			{
				this.m_Weight3 = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000B7A4 File Offset: 0x000099A4
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000B7AC File Offset: 0x000099AC
		public int boneIndex0
		{
			get
			{
				return this.m_BoneIndex0;
			}
			set
			{
				this.m_BoneIndex0 = value;
			}
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x060003CD RID: 973 RVA: 0x0000B7B8 File Offset: 0x000099B8
		// (set) Token: 0x060003CE RID: 974 RVA: 0x0000B7C0 File Offset: 0x000099C0
		public int boneIndex1
		{
			get
			{
				return this.m_BoneIndex1;
			}
			set
			{
				this.m_BoneIndex1 = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x060003CF RID: 975 RVA: 0x0000B7CC File Offset: 0x000099CC
		// (set) Token: 0x060003D0 RID: 976 RVA: 0x0000B7D4 File Offset: 0x000099D4
		public int boneIndex2
		{
			get
			{
				return this.m_BoneIndex2;
			}
			set
			{
				this.m_BoneIndex2 = value;
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060003D1 RID: 977 RVA: 0x0000B7E0 File Offset: 0x000099E0
		// (set) Token: 0x060003D2 RID: 978 RVA: 0x0000B7E8 File Offset: 0x000099E8
		public int boneIndex3
		{
			get
			{
				return this.m_BoneIndex3;
			}
			set
			{
				this.m_BoneIndex3 = value;
			}
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x0000B7F4 File Offset: 0x000099F4
		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ (this.boneIndex1.GetHashCode() << 2) ^ (this.boneIndex2.GetHashCode() >> 2) ^ (this.boneIndex3.GetHashCode() >> 1) ^ (this.weight0.GetHashCode() << 5) ^ (this.weight1.GetHashCode() << 4) ^ (this.weight2.GetHashCode() >> 4) ^ (this.weight3.GetHashCode() >> 3);
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0000B88C File Offset: 0x00009A8C
		public override bool Equals(object other)
		{
			if (!(other is BoneWeight))
			{
				return false;
			}
			BoneWeight boneWeight = (BoneWeight)other;
			bool flag;
			if (this.boneIndex0.Equals(boneWeight.boneIndex0) && this.boneIndex1.Equals(boneWeight.boneIndex1) && this.boneIndex2.Equals(boneWeight.boneIndex2) && this.boneIndex3.Equals(boneWeight.boneIndex3))
			{
				Vector4 vector = new Vector4(this.weight0, this.weight1, this.weight2, this.weight3);
				flag = vector.Equals(new Vector4(boneWeight.weight0, boneWeight.weight1, boneWeight.weight2, boneWeight.weight3));
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0000B968 File Offset: 0x00009B68
		public static bool operator ==(BoneWeight lhs, BoneWeight rhs)
		{
			return lhs.boneIndex0 == rhs.boneIndex0 && lhs.boneIndex1 == rhs.boneIndex1 && lhs.boneIndex2 == rhs.boneIndex2 && lhs.boneIndex3 == rhs.boneIndex3 && new Vector4(lhs.weight0, lhs.weight1, lhs.weight2, lhs.weight3) == new Vector4(rhs.weight0, rhs.weight1, rhs.weight2, rhs.weight3);
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x0000BA0C File Offset: 0x00009C0C
		public static bool operator !=(BoneWeight lhs, BoneWeight rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0400026D RID: 621
		private float m_Weight0;

		// Token: 0x0400026E RID: 622
		private float m_Weight1;

		// Token: 0x0400026F RID: 623
		private float m_Weight2;

		// Token: 0x04000270 RID: 624
		private float m_Weight3;

		// Token: 0x04000271 RID: 625
		private int m_BoneIndex0;

		// Token: 0x04000272 RID: 626
		private int m_BoneIndex1;

		// Token: 0x04000273 RID: 627
		private int m_BoneIndex2;

		// Token: 0x04000274 RID: 628
		private int m_BoneIndex3;
	}
}
