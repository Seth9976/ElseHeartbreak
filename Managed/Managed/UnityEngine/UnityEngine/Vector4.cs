using System;

namespace UnityEngine
{
	// Token: 0x02000118 RID: 280
	public struct Vector4
	{
		// Token: 0x06000B52 RID: 2898 RVA: 0x0001B184 File Offset: 0x00019384
		public Vector4(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0001B1A4 File Offset: 0x000193A4
		public Vector4(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = 0f;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x0001B1D4 File Offset: 0x000193D4
		public Vector4(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
			this.w = 0f;
		}

		// Token: 0x1700028A RID: 650
		public float this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.x;
				case 1:
					return this.y;
				case 2:
					return this.z;
				case 3:
					return this.w;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
			}
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				case 3:
					this.w = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Vector4 index!");
				}
			}
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x0001B2C0 File Offset: 0x000194C0
		public void Set(float new_x, float new_y, float new_z, float new_w)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
			this.w = new_w;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x0001B2E0 File Offset: 0x000194E0
		public static Vector4 Lerp(Vector4 from, Vector4 to, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector4(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t, from.w + (to.w - from.w) * t);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0001B360 File Offset: 0x00019560
		public static Vector4 MoveTowards(Vector4 current, Vector4 target, float maxDistanceDelta)
		{
			Vector4 vector = target - current;
			float magnitude = vector.magnitude;
			if (magnitude <= maxDistanceDelta || magnitude == 0f)
			{
				return target;
			}
			return current + vector / magnitude * maxDistanceDelta;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x0001B3A4 File Offset: 0x000195A4
		public static Vector4 Scale(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x * b.x, a.y * b.y, a.z * b.z, a.w * b.w);
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x0001B3F4 File Offset: 0x000195F4
		public void Scale(Vector4 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
			this.w *= scale.w;
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x0001B454 File Offset: 0x00019654
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x0001B498 File Offset: 0x00019698
		public override bool Equals(object other)
		{
			if (!(other is Vector4))
			{
				return false;
			}
			Vector4 vector = (Vector4)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z) && this.w.Equals(vector.w);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x0001B514 File Offset: 0x00019714
		public static Vector4 Normalize(Vector4 a)
		{
			float num = Vector4.Magnitude(a);
			if (num > 1E-05f)
			{
				return a / num;
			}
			return Vector4.zero;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x0001B540 File Offset: 0x00019740
		public void Normalize()
		{
			float num = Vector4.Magnitude(this);
			if (num > 1E-05f)
			{
				this /= num;
			}
			else
			{
				this = Vector4.zero;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000B60 RID: 2912 RVA: 0x0001B588 File Offset: 0x00019788
		public Vector4 normalized
		{
			get
			{
				return Vector4.Normalize(this);
			}
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x0001B598 File Offset: 0x00019798
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[] { this.x, this.y, this.z, this.w });
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x0001B5F0 File Offset: 0x000197F0
		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1}, {2}, {3})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format),
				this.z.ToString(format),
				this.w.ToString(format)
			});
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x0001B64C File Offset: 0x0001984C
		public static float Dot(Vector4 a, Vector4 b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x0001B698 File Offset: 0x00019898
		public static Vector4 Project(Vector4 a, Vector4 b)
		{
			return b * Vector4.Dot(a, b) / Vector4.Dot(b, b);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x0001B6B4 File Offset: 0x000198B4
		public static float Distance(Vector4 a, Vector4 b)
		{
			return Vector4.Magnitude(a - b);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x0001B6C4 File Offset: 0x000198C4
		public static float Magnitude(Vector4 a)
		{
			return Mathf.Sqrt(Vector4.Dot(a, a));
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x0001B6D4 File Offset: 0x000198D4
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(Vector4.Dot(this, this));
			}
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x0001B6EC File Offset: 0x000198EC
		public static float SqrMagnitude(Vector4 a)
		{
			return Vector4.Dot(a, a);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x0001B6F8 File Offset: 0x000198F8
		public float SqrMagnitude()
		{
			return Vector4.Dot(this, this);
		}

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000B6A RID: 2922 RVA: 0x0001B70C File Offset: 0x0001990C
		public float sqrMagnitude
		{
			get
			{
				return Vector4.Dot(this, this);
			}
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x0001B720 File Offset: 0x00019920
		public static Vector4 Min(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z), Mathf.Min(lhs.w, rhs.w));
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x0001B780 File Offset: 0x00019980
		public static Vector4 Max(Vector4 lhs, Vector4 rhs)
		{
			return new Vector4(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z), Mathf.Max(lhs.w, rhs.w));
		}

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000B6D RID: 2925 RVA: 0x0001B7E0 File Offset: 0x000199E0
		public static Vector4 zero
		{
			get
			{
				return new Vector4(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0001B7FC File Offset: 0x000199FC
		public static Vector4 one
		{
			get
			{
				return new Vector4(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x0001B818 File Offset: 0x00019A18
		public static Vector4 operator +(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x + b.x, a.y + b.y, a.z + b.z, a.w + b.w);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x0001B868 File Offset: 0x00019A68
		public static Vector4 operator -(Vector4 a, Vector4 b)
		{
			return new Vector4(a.x - b.x, a.y - b.y, a.z - b.z, a.w - b.w);
		}

		// Token: 0x06000B71 RID: 2929 RVA: 0x0001B8B8 File Offset: 0x00019AB8
		public static Vector4 operator -(Vector4 a)
		{
			return new Vector4(-a.x, -a.y, -a.z, -a.w);
		}

		// Token: 0x06000B72 RID: 2930 RVA: 0x0001B8E0 File Offset: 0x00019AE0
		public static Vector4 operator *(Vector4 a, float d)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x06000B73 RID: 2931 RVA: 0x0001B90C File Offset: 0x00019B0C
		public static Vector4 operator *(float d, Vector4 a)
		{
			return new Vector4(a.x * d, a.y * d, a.z * d, a.w * d);
		}

		// Token: 0x06000B74 RID: 2932 RVA: 0x0001B938 File Offset: 0x00019B38
		public static Vector4 operator /(Vector4 a, float d)
		{
			return new Vector4(a.x / d, a.y / d, a.z / d, a.w / d);
		}

		// Token: 0x06000B75 RID: 2933 RVA: 0x0001B964 File Offset: 0x00019B64
		public static bool operator ==(Vector4 lhs, Vector4 rhs)
		{
			return Vector4.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x06000B76 RID: 2934 RVA: 0x0001B97C File Offset: 0x00019B7C
		public static bool operator !=(Vector4 lhs, Vector4 rhs)
		{
			return Vector4.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0001B994 File Offset: 0x00019B94
		public static implicit operator Vector4(Vector3 v)
		{
			return new Vector4(v.x, v.y, v.z, 0f);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0001B9B8 File Offset: 0x00019BB8
		public static implicit operator Vector3(Vector4 v)
		{
			return new Vector3(v.x, v.y, v.z);
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0001B9D4 File Offset: 0x00019BD4
		public static implicit operator Vector4(Vector2 v)
		{
			return new Vector4(v.x, v.y, 0f, 0f);
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001B9F4 File Offset: 0x00019BF4
		public static implicit operator Vector2(Vector4 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x04000506 RID: 1286
		public const float kEpsilon = 1E-05f;

		// Token: 0x04000507 RID: 1287
		public float x;

		// Token: 0x04000508 RID: 1288
		public float y;

		// Token: 0x04000509 RID: 1289
		public float z;

		// Token: 0x0400050A RID: 1290
		public float w;
	}
}
