using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000116 RID: 278
	public struct Matrix4x4
	{
		// Token: 0x1700027E RID: 638
		public float this[int row, int column]
		{
			get
			{
				return this[row + column * 4];
			}
			set
			{
				this[row + column * 4] = value;
			}
		}

		// Token: 0x1700027F RID: 639
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.m00;
				case 1:
					return this.m10;
				case 2:
					return this.m20;
				case 3:
					return this.m30;
				case 4:
					return this.m01;
				case 5:
					return this.m11;
				case 6:
					return this.m21;
				case 7:
					return this.m31;
				case 8:
					return this.m02;
				case 9:
					return this.m12;
				case 10:
					return this.m22;
				case 11:
					return this.m32;
				case 12:
					return this.m03;
				case 13:
					return this.m13;
				case 14:
					return this.m23;
				case 15:
					return this.m33;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.m00 = value;
					break;
				case 1:
					this.m10 = value;
					break;
				case 2:
					this.m20 = value;
					break;
				case 3:
					this.m30 = value;
					break;
				case 4:
					this.m01 = value;
					break;
				case 5:
					this.m11 = value;
					break;
				case 6:
					this.m21 = value;
					break;
				case 7:
					this.m31 = value;
					break;
				case 8:
					this.m02 = value;
					break;
				case 9:
					this.m12 = value;
					break;
				case 10:
					this.m22 = value;
					break;
				case 11:
					this.m32 = value;
					break;
				case 12:
					this.m03 = value;
					break;
				case 13:
					this.m13 = value;
					break;
				case 14:
					this.m23 = value;
					break;
				case 15:
					this.m33 = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid matrix index!");
				}
			}
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x00019D90 File Offset: 0x00017F90
		public override int GetHashCode()
		{
			return this.GetColumn(0).GetHashCode() ^ (this.GetColumn(1).GetHashCode() << 2) ^ (this.GetColumn(2).GetHashCode() >> 2) ^ (this.GetColumn(3).GetHashCode() >> 1);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x00019DE4 File Offset: 0x00017FE4
		public override bool Equals(object other)
		{
			if (!(other is Matrix4x4))
			{
				return false;
			}
			Matrix4x4 matrix4x = (Matrix4x4)other;
			return this.GetColumn(0).Equals(matrix4x.GetColumn(0)) && this.GetColumn(1).Equals(matrix4x.GetColumn(1)) && this.GetColumn(2).Equals(matrix4x.GetColumn(2)) && this.GetColumn(3).Equals(matrix4x.GetColumn(3));
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x00019E88 File Offset: 0x00018088
		public static Matrix4x4 Inverse(Matrix4x4 m)
		{
			return Matrix4x4.INTERNAL_CALL_Inverse(ref m);
		}

		// Token: 0x06000B14 RID: 2836
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Matrix4x4 INTERNAL_CALL_Inverse(ref Matrix4x4 m);

		// Token: 0x06000B15 RID: 2837 RVA: 0x00019E94 File Offset: 0x00018094
		public static Matrix4x4 Transpose(Matrix4x4 m)
		{
			return Matrix4x4.INTERNAL_CALL_Transpose(ref m);
		}

		// Token: 0x06000B16 RID: 2838
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Matrix4x4 INTERNAL_CALL_Transpose(ref Matrix4x4 m);

		// Token: 0x06000B17 RID: 2839 RVA: 0x00019EA0 File Offset: 0x000180A0
		internal static bool Invert(Matrix4x4 inMatrix, out Matrix4x4 dest)
		{
			return Matrix4x4.INTERNAL_CALL_Invert(ref inMatrix, out dest);
		}

		// Token: 0x06000B18 RID: 2840
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Invert(ref Matrix4x4 inMatrix, out Matrix4x4 dest);

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00019EAC File Offset: 0x000180AC
		public Matrix4x4 inverse
		{
			get
			{
				return Matrix4x4.Inverse(this);
			}
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B1A RID: 2842 RVA: 0x00019EBC File Offset: 0x000180BC
		public Matrix4x4 transpose
		{
			get
			{
				return Matrix4x4.Transpose(this);
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B1B RID: 2843
		public extern bool isIdentity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000B1C RID: 2844 RVA: 0x00019ECC File Offset: 0x000180CC
		public Vector4 GetColumn(int i)
		{
			return new Vector4(this[0, i], this[1, i], this[2, i], this[3, i]);
		}

		// Token: 0x06000B1D RID: 2845 RVA: 0x00019F00 File Offset: 0x00018100
		public Vector4 GetRow(int i)
		{
			return new Vector4(this[i, 0], this[i, 1], this[i, 2], this[i, 3]);
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x00019F34 File Offset: 0x00018134
		public void SetColumn(int i, Vector4 v)
		{
			this[0, i] = v.x;
			this[1, i] = v.y;
			this[2, i] = v.z;
			this[3, i] = v.w;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x00019F80 File Offset: 0x00018180
		public void SetRow(int i, Vector4 v)
		{
			this[i, 0] = v.x;
			this[i, 1] = v.y;
			this[i, 2] = v.z;
			this[i, 3] = v.w;
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x00019FCC File Offset: 0x000181CC
		public Vector3 MultiplyPoint(Vector3 v)
		{
			Vector3 vector;
			vector.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z + this.m03;
			vector.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z + this.m13;
			vector.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z + this.m23;
			float num = this.m30 * v.x + this.m31 * v.y + this.m32 * v.z + this.m33;
			num = 1f / num;
			vector.x *= num;
			vector.y *= num;
			vector.z *= num;
			return vector;
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x0001A0F4 File Offset: 0x000182F4
		public Vector3 MultiplyPoint3x4(Vector3 v)
		{
			Vector3 vector;
			vector.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z + this.m03;
			vector.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z + this.m13;
			vector.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z + this.m23;
			return vector;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x0001A1B0 File Offset: 0x000183B0
		public Vector3 MultiplyVector(Vector3 v)
		{
			Vector3 vector;
			vector.x = this.m00 * v.x + this.m01 * v.y + this.m02 * v.z;
			vector.y = this.m10 * v.x + this.m11 * v.y + this.m12 * v.z;
			vector.z = this.m20 * v.x + this.m21 * v.y + this.m22 * v.z;
			return vector;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x0001A258 File Offset: 0x00018458
		public static Matrix4x4 Scale(Vector3 v)
		{
			return new Matrix4x4
			{
				m00 = v.x,
				m01 = 0f,
				m02 = 0f,
				m03 = 0f,
				m10 = 0f,
				m11 = v.y,
				m12 = 0f,
				m13 = 0f,
				m20 = 0f,
				m21 = 0f,
				m22 = v.z,
				m23 = 0f,
				m30 = 0f,
				m31 = 0f,
				m32 = 0f,
				m33 = 1f
			};
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001A334 File Offset: 0x00018534
		public static Matrix4x4 zero
		{
			get
			{
				return new Matrix4x4
				{
					m00 = 0f,
					m01 = 0f,
					m02 = 0f,
					m03 = 0f,
					m10 = 0f,
					m11 = 0f,
					m12 = 0f,
					m13 = 0f,
					m20 = 0f,
					m21 = 0f,
					m22 = 0f,
					m23 = 0f,
					m30 = 0f,
					m31 = 0f,
					m32 = 0f,
					m33 = 0f
				};
			}
		}

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000B25 RID: 2853 RVA: 0x0001A40C File Offset: 0x0001860C
		public static Matrix4x4 identity
		{
			get
			{
				return new Matrix4x4
				{
					m00 = 1f,
					m01 = 0f,
					m02 = 0f,
					m03 = 0f,
					m10 = 0f,
					m11 = 1f,
					m12 = 0f,
					m13 = 0f,
					m20 = 0f,
					m21 = 0f,
					m22 = 1f,
					m23 = 0f,
					m30 = 0f,
					m31 = 0f,
					m32 = 0f,
					m33 = 1f
				};
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x0001A4E4 File Offset: 0x000186E4
		public void SetTRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			this = Matrix4x4.TRS(pos, q, s);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0001A4F4 File Offset: 0x000186F4
		public static Matrix4x4 TRS(Vector3 pos, Quaternion q, Vector3 s)
		{
			return Matrix4x4.INTERNAL_CALL_TRS(ref pos, ref q, ref s);
		}

		// Token: 0x06000B28 RID: 2856
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Matrix4x4 INTERNAL_CALL_TRS(ref Vector3 pos, ref Quaternion q, ref Vector3 s);

		// Token: 0x06000B29 RID: 2857 RVA: 0x0001A504 File Offset: 0x00018704
		public override string ToString()
		{
			return UnityString.Format("{0:F5}\t{1:F5}\t{2:F5}\t{3:F5}\n{4:F5}\t{5:F5}\t{6:F5}\t{7:F5}\n{8:F5}\t{9:F5}\t{10:F5}\t{11:F5}\n{12:F5}\t{13:F5}\t{14:F5}\t{15:F5}\n", new object[]
			{
				this.m00, this.m01, this.m02, this.m03, this.m10, this.m11, this.m12, this.m13, this.m20, this.m21,
				this.m22, this.m23, this.m30, this.m31, this.m32, this.m33
			});
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x0001A60C File Offset: 0x0001880C
		public string ToString(string format)
		{
			return UnityString.Format("{0}\t{1}\t{2}\t{3}\n{4}\t{5}\t{6}\t{7}\n{8}\t{9}\t{10}\t{11}\n{12}\t{13}\t{14}\t{15}\n", new object[]
			{
				this.m00.ToString(format),
				this.m01.ToString(format),
				this.m02.ToString(format),
				this.m03.ToString(format),
				this.m10.ToString(format),
				this.m11.ToString(format),
				this.m12.ToString(format),
				this.m13.ToString(format),
				this.m20.ToString(format),
				this.m21.ToString(format),
				this.m22.ToString(format),
				this.m23.ToString(format),
				this.m30.ToString(format),
				this.m31.ToString(format),
				this.m32.ToString(format),
				this.m33.ToString(format)
			});
		}

		// Token: 0x06000B2B RID: 2859
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Matrix4x4 Ortho(float left, float right, float bottom, float top, float zNear, float zFar);

		// Token: 0x06000B2C RID: 2860
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Matrix4x4 Perspective(float fov, float aspect, float zNear, float zFar);

		// Token: 0x06000B2D RID: 2861 RVA: 0x0001A724 File Offset: 0x00018924
		public static Matrix4x4 operator *(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return new Matrix4x4
			{
				m00 = lhs.m00 * rhs.m00 + lhs.m01 * rhs.m10 + lhs.m02 * rhs.m20 + lhs.m03 * rhs.m30,
				m01 = lhs.m00 * rhs.m01 + lhs.m01 * rhs.m11 + lhs.m02 * rhs.m21 + lhs.m03 * rhs.m31,
				m02 = lhs.m00 * rhs.m02 + lhs.m01 * rhs.m12 + lhs.m02 * rhs.m22 + lhs.m03 * rhs.m32,
				m03 = lhs.m00 * rhs.m03 + lhs.m01 * rhs.m13 + lhs.m02 * rhs.m23 + lhs.m03 * rhs.m33,
				m10 = lhs.m10 * rhs.m00 + lhs.m11 * rhs.m10 + lhs.m12 * rhs.m20 + lhs.m13 * rhs.m30,
				m11 = lhs.m10 * rhs.m01 + lhs.m11 * rhs.m11 + lhs.m12 * rhs.m21 + lhs.m13 * rhs.m31,
				m12 = lhs.m10 * rhs.m02 + lhs.m11 * rhs.m12 + lhs.m12 * rhs.m22 + lhs.m13 * rhs.m32,
				m13 = lhs.m10 * rhs.m03 + lhs.m11 * rhs.m13 + lhs.m12 * rhs.m23 + lhs.m13 * rhs.m33,
				m20 = lhs.m20 * rhs.m00 + lhs.m21 * rhs.m10 + lhs.m22 * rhs.m20 + lhs.m23 * rhs.m30,
				m21 = lhs.m20 * rhs.m01 + lhs.m21 * rhs.m11 + lhs.m22 * rhs.m21 + lhs.m23 * rhs.m31,
				m22 = lhs.m20 * rhs.m02 + lhs.m21 * rhs.m12 + lhs.m22 * rhs.m22 + lhs.m23 * rhs.m32,
				m23 = lhs.m20 * rhs.m03 + lhs.m21 * rhs.m13 + lhs.m22 * rhs.m23 + lhs.m23 * rhs.m33,
				m30 = lhs.m30 * rhs.m00 + lhs.m31 * rhs.m10 + lhs.m32 * rhs.m20 + lhs.m33 * rhs.m30,
				m31 = lhs.m30 * rhs.m01 + lhs.m31 * rhs.m11 + lhs.m32 * rhs.m21 + lhs.m33 * rhs.m31,
				m32 = lhs.m30 * rhs.m02 + lhs.m31 * rhs.m12 + lhs.m32 * rhs.m22 + lhs.m33 * rhs.m32,
				m33 = lhs.m30 * rhs.m03 + lhs.m31 * rhs.m13 + lhs.m32 * rhs.m23 + lhs.m33 * rhs.m33
			};
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x0001AB9C File Offset: 0x00018D9C
		public static Vector4 operator *(Matrix4x4 lhs, Vector4 v)
		{
			Vector4 vector;
			vector.x = lhs.m00 * v.x + lhs.m01 * v.y + lhs.m02 * v.z + lhs.m03 * v.w;
			vector.y = lhs.m10 * v.x + lhs.m11 * v.y + lhs.m12 * v.z + lhs.m13 * v.w;
			vector.z = lhs.m20 * v.x + lhs.m21 * v.y + lhs.m22 * v.z + lhs.m23 * v.w;
			vector.w = lhs.m30 * v.x + lhs.m31 * v.y + lhs.m32 * v.z + lhs.m33 * v.w;
			return vector;
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x0001ACC4 File Offset: 0x00018EC4
		public static bool operator ==(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return lhs.GetColumn(0) == rhs.GetColumn(0) && lhs.GetColumn(1) == rhs.GetColumn(1) && lhs.GetColumn(2) == rhs.GetColumn(2) && lhs.GetColumn(3) == rhs.GetColumn(3);
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x0001AD38 File Offset: 0x00018F38
		public static bool operator !=(Matrix4x4 lhs, Matrix4x4 rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x040004F4 RID: 1268
		public float m00;

		// Token: 0x040004F5 RID: 1269
		public float m10;

		// Token: 0x040004F6 RID: 1270
		public float m20;

		// Token: 0x040004F7 RID: 1271
		public float m30;

		// Token: 0x040004F8 RID: 1272
		public float m01;

		// Token: 0x040004F9 RID: 1273
		public float m11;

		// Token: 0x040004FA RID: 1274
		public float m21;

		// Token: 0x040004FB RID: 1275
		public float m31;

		// Token: 0x040004FC RID: 1276
		public float m02;

		// Token: 0x040004FD RID: 1277
		public float m12;

		// Token: 0x040004FE RID: 1278
		public float m22;

		// Token: 0x040004FF RID: 1279
		public float m32;

		// Token: 0x04000500 RID: 1280
		public float m03;

		// Token: 0x04000501 RID: 1281
		public float m13;

		// Token: 0x04000502 RID: 1282
		public float m23;

		// Token: 0x04000503 RID: 1283
		public float m33;
	}
}
