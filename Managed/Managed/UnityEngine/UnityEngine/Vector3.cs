using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000111 RID: 273
	public struct Vector3
	{
		// Token: 0x06000A39 RID: 2617 RVA: 0x000176D8 File Offset: 0x000158D8
		public Vector3(float x, float y, float z)
		{
			this.x = x;
			this.y = y;
			this.z = z;
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x000176F0 File Offset: 0x000158F0
		public Vector3(float x, float y)
		{
			this.x = x;
			this.y = y;
			this.z = 0f;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x0001770C File Offset: 0x0001590C
		public static Vector3 Lerp(Vector3 from, Vector3 to, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector3(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t, from.z + (to.z - from.z) * t);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x00017774 File Offset: 0x00015974
		public static Vector3 Slerp(Vector3 from, Vector3 to, float t)
		{
			return Vector3.INTERNAL_CALL_Slerp(ref from, ref to, t);
		}

		// Token: 0x06000A3D RID: 2621
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_Slerp(ref Vector3 from, ref Vector3 to, float t);

		// Token: 0x06000A3E RID: 2622 RVA: 0x00017780 File Offset: 0x00015980
		private static void Internal_OrthoNormalize2(ref Vector3 a, ref Vector3 b)
		{
			Vector3.INTERNAL_CALL_Internal_OrthoNormalize2(ref a, ref b);
		}

		// Token: 0x06000A3F RID: 2623
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_OrthoNormalize2(ref Vector3 a, ref Vector3 b);

		// Token: 0x06000A40 RID: 2624 RVA: 0x0001778C File Offset: 0x0001598C
		private static void Internal_OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c)
		{
			Vector3.INTERNAL_CALL_Internal_OrthoNormalize3(ref a, ref b, ref c);
		}

		// Token: 0x06000A41 RID: 2625
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_OrthoNormalize3(ref Vector3 a, ref Vector3 b, ref Vector3 c);

		// Token: 0x06000A42 RID: 2626 RVA: 0x00017798 File Offset: 0x00015998
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent)
		{
			Vector3.Internal_OrthoNormalize2(ref normal, ref tangent);
		}

		// Token: 0x06000A43 RID: 2627 RVA: 0x000177A4 File Offset: 0x000159A4
		public static void OrthoNormalize(ref Vector3 normal, ref Vector3 tangent, ref Vector3 binormal)
		{
			Vector3.Internal_OrthoNormalize3(ref normal, ref tangent, ref binormal);
		}

		// Token: 0x06000A44 RID: 2628 RVA: 0x000177B0 File Offset: 0x000159B0
		public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
		{
			Vector3 vector = target - current;
			float magnitude = vector.magnitude;
			if (magnitude <= maxDistanceDelta || magnitude == 0f)
			{
				return target;
			}
			return current + vector / magnitude * maxDistanceDelta;
		}

		// Token: 0x06000A45 RID: 2629 RVA: 0x000177F4 File Offset: 0x000159F4
		public static Vector3 RotateTowards(Vector3 current, Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta)
		{
			return Vector3.INTERNAL_CALL_RotateTowards(ref current, ref target, maxRadiansDelta, maxMagnitudeDelta);
		}

		// Token: 0x06000A46 RID: 2630
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_RotateTowards(ref Vector3 current, ref Vector3 target, float maxRadiansDelta, float maxMagnitudeDelta);

		// Token: 0x06000A47 RID: 2631 RVA: 0x00017804 File Offset: 0x00015A04
		[ExcludeFromDocs]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06000A48 RID: 2632 RVA: 0x00017824 File Offset: 0x00015A24
		[ExcludeFromDocs]
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector3.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00017848 File Offset: 0x00015A48
		public static Vector3 SmoothDamp(Vector3 current, Vector3 target, ref Vector3 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			Vector3 vector = current - target;
			Vector3 vector2 = target;
			float num4 = maxSpeed * smoothTime;
			vector = Vector3.ClampMagnitude(vector, num4);
			target = current - vector;
			Vector3 vector3 = (currentVelocity + num * vector) * deltaTime;
			currentVelocity = (currentVelocity - num * vector3) * num3;
			Vector3 vector4 = target + (vector + vector3) * num3;
			if (Vector3.Dot(vector2 - current, vector4 - vector2) > 0f)
			{
				vector4 = vector2;
				currentVelocity = (vector4 - vector2) / deltaTime;
			}
			return vector4;
		}

		// Token: 0x1700024E RID: 590
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
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
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
				default:
					throw new IndexOutOfRangeException("Invalid Vector3 index!");
				}
			}
		}

		// Token: 0x06000A4C RID: 2636 RVA: 0x000179E4 File Offset: 0x00015BE4
		public void Set(float new_x, float new_y, float new_z)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
		}

		// Token: 0x06000A4D RID: 2637 RVA: 0x000179FC File Offset: 0x00015BFC
		public static Vector3 Scale(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06000A4E RID: 2638 RVA: 0x00017A3C File Offset: 0x00015C3C
		public void Scale(Vector3 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x06000A4F RID: 2639 RVA: 0x00017A88 File Offset: 0x00015C88
		public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(lhs.y * rhs.z - lhs.z * rhs.y, lhs.z * rhs.x - lhs.x * rhs.z, lhs.x * rhs.y - lhs.y * rhs.x);
		}

		// Token: 0x06000A50 RID: 2640 RVA: 0x00017AF8 File Offset: 0x00015CF8
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2);
		}

		// Token: 0x06000A51 RID: 2641 RVA: 0x00017B2C File Offset: 0x00015D2C
		public override bool Equals(object other)
		{
			if (!(other is Vector3))
			{
				return false;
			}
			Vector3 vector = (Vector3)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y) && this.z.Equals(vector.z);
		}

		// Token: 0x06000A52 RID: 2642 RVA: 0x00017B90 File Offset: 0x00015D90
		public static Vector3 Reflect(Vector3 inDirection, Vector3 inNormal)
		{
			return -2f * Vector3.Dot(inNormal, inDirection) * inNormal + inDirection;
		}

		// Token: 0x06000A53 RID: 2643 RVA: 0x00017BAC File Offset: 0x00015DAC
		public static Vector3 Normalize(Vector3 value)
		{
			float num = Vector3.Magnitude(value);
			if (num > 1E-05f)
			{
				return value / num;
			}
			return Vector3.zero;
		}

		// Token: 0x06000A54 RID: 2644 RVA: 0x00017BD8 File Offset: 0x00015DD8
		public void Normalize()
		{
			float num = Vector3.Magnitude(this);
			if (num > 1E-05f)
			{
				this /= num;
			}
			else
			{
				this = Vector3.zero;
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000A55 RID: 2645 RVA: 0x00017C20 File Offset: 0x00015E20
		public Vector3 normalized
		{
			get
			{
				return Vector3.Normalize(this);
			}
		}

		// Token: 0x06000A56 RID: 2646 RVA: 0x00017C30 File Offset: 0x00015E30
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1})", new object[] { this.x, this.y, this.z });
		}

		// Token: 0x06000A57 RID: 2647 RVA: 0x00017C78 File Offset: 0x00015E78
		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format),
				this.z.ToString(format)
			});
		}

		// Token: 0x06000A58 RID: 2648 RVA: 0x00017CC4 File Offset: 0x00015EC4
		public static float Dot(Vector3 lhs, Vector3 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y + lhs.z * rhs.z;
		}

		// Token: 0x06000A59 RID: 2649 RVA: 0x00017CF8 File Offset: 0x00015EF8
		public static Vector3 Project(Vector3 vector, Vector3 onNormal)
		{
			float num = Vector3.Dot(onNormal, onNormal);
			if (num < 1E-45f)
			{
				return Vector3.zero;
			}
			return onNormal * Vector3.Dot(vector, onNormal) / num;
		}

		// Token: 0x06000A5A RID: 2650 RVA: 0x00017D34 File Offset: 0x00015F34
		public static Vector3 ProjectOnPlane(Vector3 vector, Vector3 planeNormal)
		{
			return vector - Vector3.Project(vector, planeNormal);
		}

		// Token: 0x06000A5B RID: 2651 RVA: 0x00017D44 File Offset: 0x00015F44
		[Obsolete("Use Vector3.ProjectOnPlane instead.")]
		public static Vector3 Exclude(Vector3 excludeThis, Vector3 fromThat)
		{
			return fromThat - Vector3.Project(fromThat, excludeThis);
		}

		// Token: 0x06000A5C RID: 2652 RVA: 0x00017D54 File Offset: 0x00015F54
		public static float Angle(Vector3 from, Vector3 to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
		}

		// Token: 0x06000A5D RID: 2653 RVA: 0x00017D90 File Offset: 0x00015F90
		public static float Distance(Vector3 a, Vector3 b)
		{
			Vector3 vector = new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
			return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y + vector.z * vector.z);
		}

		// Token: 0x06000A5E RID: 2654 RVA: 0x00017E08 File Offset: 0x00016008
		public static Vector3 ClampMagnitude(Vector3 vector, float maxLength)
		{
			if (vector.sqrMagnitude > maxLength * maxLength)
			{
				return vector.normalized * maxLength;
			}
			return vector;
		}

		// Token: 0x06000A5F RID: 2655 RVA: 0x00017E28 File Offset: 0x00016028
		public static float Magnitude(Vector3 a)
		{
			return Mathf.Sqrt(a.x * a.x + a.y * a.y + a.z * a.z);
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A60 RID: 2656 RVA: 0x00017E6C File Offset: 0x0001606C
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(this.x * this.x + this.y * this.y + this.z * this.z);
			}
		}

		// Token: 0x06000A61 RID: 2657 RVA: 0x00017EA8 File Offset: 0x000160A8
		public static float SqrMagnitude(Vector3 a)
		{
			return a.x * a.x + a.y * a.y + a.z * a.z;
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A62 RID: 2658 RVA: 0x00017EDC File Offset: 0x000160DC
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x06000A63 RID: 2659 RVA: 0x00017F08 File Offset: 0x00016108
		public static Vector3 Min(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x06000A64 RID: 2660 RVA: 0x00017F54 File Offset: 0x00016154
		public static Vector3 Max(Vector3 lhs, Vector3 rhs)
		{
			return new Vector3(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00017FA0 File Offset: 0x000161A0
		public static Vector3 zero
		{
			get
			{
				return new Vector3(0f, 0f, 0f);
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00017FB8 File Offset: 0x000161B8
		public static Vector3 one
		{
			get
			{
				return new Vector3(1f, 1f, 1f);
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00017FD0 File Offset: 0x000161D0
		public static Vector3 forward
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00017FE8 File Offset: 0x000161E8
		public static Vector3 back
		{
			get
			{
				return new Vector3(0f, 0f, -1f);
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000A69 RID: 2665 RVA: 0x00018000 File Offset: 0x00016200
		public static Vector3 up
		{
			get
			{
				return new Vector3(0f, 1f, 0f);
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A6A RID: 2666 RVA: 0x00018018 File Offset: 0x00016218
		public static Vector3 down
		{
			get
			{
				return new Vector3(0f, -1f, 0f);
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A6B RID: 2667 RVA: 0x00018030 File Offset: 0x00016230
		public static Vector3 left
		{
			get
			{
				return new Vector3(-1f, 0f, 0f);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A6C RID: 2668 RVA: 0x00018048 File Offset: 0x00016248
		public static Vector3 right
		{
			get
			{
				return new Vector3(1f, 0f, 0f);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A6D RID: 2669 RVA: 0x00018060 File Offset: 0x00016260
		[Obsolete("Use Vector3.forward instead.")]
		public static Vector3 fwd
		{
			get
			{
				return new Vector3(0f, 0f, 1f);
			}
		}

		// Token: 0x06000A6E RID: 2670 RVA: 0x00018078 File Offset: 0x00016278
		[Obsolete("Use Vector3.Angle instead. AngleBetween uses radians instead of degrees and was deprecated for this reason")]
		public static float AngleBetween(Vector3 from, Vector3 to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector3.Dot(from.normalized, to.normalized), -1f, 1f));
		}

		// Token: 0x06000A6F RID: 2671 RVA: 0x000180AC File Offset: 0x000162AC
		public static Vector3 operator +(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06000A70 RID: 2672 RVA: 0x000180EC File Offset: 0x000162EC
		public static Vector3 operator -(Vector3 a, Vector3 b)
		{
			return new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06000A71 RID: 2673 RVA: 0x0001812C File Offset: 0x0001632C
		public static Vector3 operator -(Vector3 a)
		{
			return new Vector3(-a.x, -a.y, -a.z);
		}

		// Token: 0x06000A72 RID: 2674 RVA: 0x0001814C File Offset: 0x0001634C
		public static Vector3 operator *(Vector3 a, float d)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06000A73 RID: 2675 RVA: 0x0001817C File Offset: 0x0001637C
		public static Vector3 operator *(float d, Vector3 a)
		{
			return new Vector3(a.x * d, a.y * d, a.z * d);
		}

		// Token: 0x06000A74 RID: 2676 RVA: 0x000181AC File Offset: 0x000163AC
		public static Vector3 operator /(Vector3 a, float d)
		{
			return new Vector3(a.x / d, a.y / d, a.z / d);
		}

		// Token: 0x06000A75 RID: 2677 RVA: 0x000181DC File Offset: 0x000163DC
		public static bool operator ==(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x06000A76 RID: 2678 RVA: 0x000181F4 File Offset: 0x000163F4
		public static bool operator !=(Vector3 lhs, Vector3 rhs)
		{
			return Vector3.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x040004DF RID: 1247
		public const float kEpsilon = 1E-05f;

		// Token: 0x040004E0 RID: 1248
		public float x;

		// Token: 0x040004E1 RID: 1249
		public float y;

		// Token: 0x040004E2 RID: 1250
		public float z;
	}
}
