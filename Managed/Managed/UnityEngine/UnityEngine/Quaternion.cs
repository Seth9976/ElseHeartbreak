using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000114 RID: 276
	public struct Quaternion
	{
		// Token: 0x06000AA1 RID: 2721 RVA: 0x00018B2C File Offset: 0x00016D2C
		public Quaternion(float x, float y, float z, float w)
		{
			this.x = x;
			this.y = y;
			this.z = z;
			this.w = w;
		}

		// Token: 0x1700026A RID: 618
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
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
					throw new IndexOutOfRangeException("Invalid Quaternion index!");
				}
			}
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x00018C04 File Offset: 0x00016E04
		public void Set(float new_x, float new_y, float new_z, float new_w)
		{
			this.x = new_x;
			this.y = new_y;
			this.z = new_z;
			this.w = new_w;
		}

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x00018C24 File Offset: 0x00016E24
		public static Quaternion identity
		{
			get
			{
				return new Quaternion(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x00018C40 File Offset: 0x00016E40
		public static float Dot(Quaternion a, Quaternion b)
		{
			return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x00018C8C File Offset: 0x00016E8C
		public static Quaternion AngleAxis(float angle, Vector3 axis)
		{
			return Quaternion.INTERNAL_CALL_AngleAxis(angle, ref axis);
		}

		// Token: 0x06000AA8 RID: 2728
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_AngleAxis(float angle, ref Vector3 axis);

		// Token: 0x06000AA9 RID: 2729 RVA: 0x00018C98 File Offset: 0x00016E98
		public void ToAngleAxis(out float angle, out Vector3 axis)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
			angle *= 57.29578f;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x00018CB4 File Offset: 0x00016EB4
		public static Quaternion FromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			return Quaternion.INTERNAL_CALL_FromToRotation(ref fromDirection, ref toDirection);
		}

		// Token: 0x06000AAB RID: 2731
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_FromToRotation(ref Vector3 fromDirection, ref Vector3 toDirection);

		// Token: 0x06000AAC RID: 2732 RVA: 0x00018CC0 File Offset: 0x00016EC0
		public void SetFromToRotation(Vector3 fromDirection, Vector3 toDirection)
		{
			this = Quaternion.FromToRotation(fromDirection, toDirection);
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x00018CD0 File Offset: 0x00016ED0
		public static Quaternion LookRotation(Vector3 forward, [DefaultValue("Vector3.up")] Vector3 upwards)
		{
			return Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref upwards);
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x00018CDC File Offset: 0x00016EDC
		[ExcludeFromDocs]
		public static Quaternion LookRotation(Vector3 forward)
		{
			Vector3 up = Vector3.up;
			return Quaternion.INTERNAL_CALL_LookRotation(ref forward, ref up);
		}

		// Token: 0x06000AAF RID: 2735
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_LookRotation(ref Vector3 forward, ref Vector3 upwards);

		// Token: 0x06000AB0 RID: 2736 RVA: 0x00018CF8 File Offset: 0x00016EF8
		[ExcludeFromDocs]
		public void SetLookRotation(Vector3 view)
		{
			Vector3 up = Vector3.up;
			this.SetLookRotation(view, up);
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x00018D14 File Offset: 0x00016F14
		public void SetLookRotation(Vector3 view, [DefaultValue("Vector3.up")] Vector3 up)
		{
			this = Quaternion.LookRotation(view, up);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x00018D24 File Offset: 0x00016F24
		public static Quaternion Slerp(Quaternion from, Quaternion to, float t)
		{
			return Quaternion.INTERNAL_CALL_Slerp(ref from, ref to, t);
		}

		// Token: 0x06000AB3 RID: 2739
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_Slerp(ref Quaternion from, ref Quaternion to, float t);

		// Token: 0x06000AB4 RID: 2740 RVA: 0x00018D30 File Offset: 0x00016F30
		public static Quaternion Lerp(Quaternion from, Quaternion to, float t)
		{
			return Quaternion.INTERNAL_CALL_Lerp(ref from, ref to, t);
		}

		// Token: 0x06000AB5 RID: 2741
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_Lerp(ref Quaternion from, ref Quaternion to, float t);

		// Token: 0x06000AB6 RID: 2742 RVA: 0x00018D3C File Offset: 0x00016F3C
		public static Quaternion RotateTowards(Quaternion from, Quaternion to, float maxDegreesDelta)
		{
			float num = Quaternion.Angle(from, to);
			if (num == 0f)
			{
				return to;
			}
			float num2 = Mathf.Min(1f, maxDegreesDelta / num);
			return Quaternion.UnclampedSlerp(from, to, num2);
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x00018D74 File Offset: 0x00016F74
		private static Quaternion UnclampedSlerp(Quaternion from, Quaternion to, float t)
		{
			return Quaternion.INTERNAL_CALL_UnclampedSlerp(ref from, ref to, t);
		}

		// Token: 0x06000AB8 RID: 2744
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_UnclampedSlerp(ref Quaternion from, ref Quaternion to, float t);

		// Token: 0x06000AB9 RID: 2745 RVA: 0x00018D80 File Offset: 0x00016F80
		public static Quaternion Inverse(Quaternion rotation)
		{
			return Quaternion.INTERNAL_CALL_Inverse(ref rotation);
		}

		// Token: 0x06000ABA RID: 2746
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_Inverse(ref Quaternion rotation);

		// Token: 0x06000ABB RID: 2747 RVA: 0x00018D8C File Offset: 0x00016F8C
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1}, {2:F1}, {3:F1})", new object[] { this.x, this.y, this.z, this.w });
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x00018DE4 File Offset: 0x00016FE4
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

		// Token: 0x06000ABD RID: 2749 RVA: 0x00018E40 File Offset: 0x00017040
		public static float Angle(Quaternion a, Quaternion b)
		{
			float num = Quaternion.Dot(a, b);
			return Mathf.Acos(Mathf.Min(Mathf.Abs(num), 1f)) * 2f * 57.29578f;
		}

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000ABE RID: 2750 RVA: 0x00018E78 File Offset: 0x00017078
		// (set) Token: 0x06000ABF RID: 2751 RVA: 0x00018E90 File Offset: 0x00017090
		public Vector3 eulerAngles
		{
			get
			{
				return Quaternion.Internal_ToEulerRad(this) * 57.29578f;
			}
			set
			{
				this = Quaternion.Internal_FromEulerRad(value * 0.017453292f);
			}
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x00018EA8 File Offset: 0x000170A8
		public static Quaternion Euler(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z) * 0.017453292f);
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x00018EC4 File Offset: 0x000170C4
		public static Quaternion Euler(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler * 0.017453292f);
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x00018ED8 File Offset: 0x000170D8
		private static Vector3 Internal_ToEulerRad(Quaternion rotation)
		{
			return Quaternion.INTERNAL_CALL_Internal_ToEulerRad(ref rotation);
		}

		// Token: 0x06000AC3 RID: 2755
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_Internal_ToEulerRad(ref Quaternion rotation);

		// Token: 0x06000AC4 RID: 2756 RVA: 0x00018EE4 File Offset: 0x000170E4
		private static Quaternion Internal_FromEulerRad(Vector3 euler)
		{
			return Quaternion.INTERNAL_CALL_Internal_FromEulerRad(ref euler);
		}

		// Token: 0x06000AC5 RID: 2757
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_Internal_FromEulerRad(ref Vector3 euler);

		// Token: 0x06000AC6 RID: 2758 RVA: 0x00018EF0 File Offset: 0x000170F0
		private static void Internal_ToAxisAngleRad(Quaternion q, out Vector3 axis, out float angle)
		{
			Quaternion.INTERNAL_CALL_Internal_ToAxisAngleRad(ref q, out axis, out angle);
		}

		// Token: 0x06000AC7 RID: 2759
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_ToAxisAngleRad(ref Quaternion q, out Vector3 axis, out float angle);

		// Token: 0x06000AC8 RID: 2760 RVA: 0x00018EFC File Offset: 0x000170FC
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerRotation(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x00018F0C File Offset: 0x0001710C
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerRotation(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x00018F14 File Offset: 0x00017114
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerRotation(float x, float y, float z)
		{
			this = Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x00018F2C File Offset: 0x0001712C
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerRotation(Vector3 euler)
		{
			this = Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x00018F3C File Offset: 0x0001713C
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public Vector3 ToEuler()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x00018F4C File Offset: 0x0001714C
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerAngles(float x, float y, float z)
		{
			return Quaternion.Internal_FromEulerRad(new Vector3(x, y, z));
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x00018F5C File Offset: 0x0001715C
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion EulerAngles(Vector3 euler)
		{
			return Quaternion.Internal_FromEulerRad(euler);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x00018F64 File Offset: 0x00017164
		[Obsolete("Use Quaternion.ToAngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public void ToAxisAngle(out Vector3 axis, out float angle)
		{
			Quaternion.Internal_ToAxisAngleRad(this, out axis, out angle);
		}

		// Token: 0x06000AD0 RID: 2768 RVA: 0x00018F74 File Offset: 0x00017174
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerAngles(float x, float y, float z)
		{
			this.SetEulerRotation(new Vector3(x, y, z));
		}

		// Token: 0x06000AD1 RID: 2769 RVA: 0x00018F84 File Offset: 0x00017184
		[Obsolete("Use Quaternion.Euler instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetEulerAngles(Vector3 euler)
		{
			this = Quaternion.EulerRotation(euler);
		}

		// Token: 0x06000AD2 RID: 2770 RVA: 0x00018F94 File Offset: 0x00017194
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public static Vector3 ToEulerAngles(Quaternion rotation)
		{
			return Quaternion.Internal_ToEulerRad(rotation);
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x00018F9C File Offset: 0x0001719C
		[Obsolete("Use Quaternion.eulerAngles instead. This function was deprecated because it uses radians instead of degrees")]
		public Vector3 ToEulerAngles()
		{
			return Quaternion.Internal_ToEulerRad(this);
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x00018FAC File Offset: 0x000171AC
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public static Quaternion AxisAngle(Vector3 axis, float angle)
		{
			return Quaternion.INTERNAL_CALL_AxisAngle(ref axis, angle);
		}

		// Token: 0x06000AD5 RID: 2773
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion INTERNAL_CALL_AxisAngle(ref Vector3 axis, float angle);

		// Token: 0x06000AD6 RID: 2774 RVA: 0x00018FB8 File Offset: 0x000171B8
		[Obsolete("Use Quaternion.AngleAxis instead. This function was deprecated because it uses radians instead of degrees")]
		public void SetAxisAngle(Vector3 axis, float angle)
		{
			this = Quaternion.AxisAngle(axis, angle);
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x00018FC8 File Offset: 0x000171C8
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2) ^ (this.z.GetHashCode() >> 2) ^ (this.w.GetHashCode() >> 1);
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x0001900C File Offset: 0x0001720C
		public override bool Equals(object other)
		{
			if (!(other is Quaternion))
			{
				return false;
			}
			Quaternion quaternion = (Quaternion)other;
			return this.x.Equals(quaternion.x) && this.y.Equals(quaternion.y) && this.z.Equals(quaternion.z) && this.w.Equals(quaternion.w);
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x00019088 File Offset: 0x00017288
		public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
		{
			return new Quaternion(lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y, lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z, lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x, lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z);
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x00019198 File Offset: 0x00017398
		public static Vector3 operator *(Quaternion rotation, Vector3 point)
		{
			float num = rotation.x * 2f;
			float num2 = rotation.y * 2f;
			float num3 = rotation.z * 2f;
			float num4 = rotation.x * num;
			float num5 = rotation.y * num2;
			float num6 = rotation.z * num3;
			float num7 = rotation.x * num2;
			float num8 = rotation.x * num3;
			float num9 = rotation.y * num3;
			float num10 = rotation.w * num;
			float num11 = rotation.w * num2;
			float num12 = rotation.w * num3;
			Vector3 vector;
			vector.x = (1f - (num5 + num6)) * point.x + (num7 - num12) * point.y + (num8 + num11) * point.z;
			vector.y = (num7 + num12) * point.x + (1f - (num4 + num6)) * point.y + (num9 - num10) * point.z;
			vector.z = (num8 - num11) * point.x + (num9 + num10) * point.y + (1f - (num4 + num5)) * point.z;
			return vector;
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000192D4 File Offset: 0x000174D4
		public static bool operator ==(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.Dot(lhs, rhs) > 0.999999f;
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000192E4 File Offset: 0x000174E4
		public static bool operator !=(Quaternion lhs, Quaternion rhs)
		{
			return Quaternion.Dot(lhs, rhs) <= 0.999999f;
		}

		// Token: 0x040004EB RID: 1259
		public const float kEpsilon = 1E-06f;

		// Token: 0x040004EC RID: 1260
		public float x;

		// Token: 0x040004ED RID: 1261
		public float y;

		// Token: 0x040004EE RID: 1262
		public float z;

		// Token: 0x040004EF RID: 1263
		public float w;
	}
}
