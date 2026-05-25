using System;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000110 RID: 272
	public struct Vector2
	{
		// Token: 0x06000A10 RID: 2576 RVA: 0x00016FB8 File Offset: 0x000151B8
		public Vector2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x17000246 RID: 582
		public float this[int index]
		{
			get
			{
				if (index == 0)
				{
					return this.x;
				}
				if (index != 1)
				{
					throw new IndexOutOfRangeException("Invalid Vector2 index!");
				}
				return this.y;
			}
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException("Invalid Vector2 index!");
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x00017048 File Offset: 0x00015248
		public void Set(float new_x, float new_y)
		{
			this.x = new_x;
			this.y = new_y;
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x00017058 File Offset: 0x00015258
		public static Vector2 Lerp(Vector2 from, Vector2 to, float t)
		{
			t = Mathf.Clamp01(t);
			return new Vector2(from.x + (to.x - from.x) * t, from.y + (to.y - from.y) * t);
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000170A4 File Offset: 0x000152A4
		public static Vector2 MoveTowards(Vector2 current, Vector2 target, float maxDistanceDelta)
		{
			Vector2 vector = target - current;
			float magnitude = vector.magnitude;
			if (magnitude <= maxDistanceDelta || magnitude == 0f)
			{
				return target;
			}
			return current + vector / magnitude * maxDistanceDelta;
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000170E8 File Offset: 0x000152E8
		public static Vector2 Scale(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x00017110 File Offset: 0x00015310
		public void Scale(Vector2 scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x00017148 File Offset: 0x00015348
		public void Normalize()
		{
			float magnitude = this.magnitude;
			if (magnitude > 1E-05f)
			{
				this /= magnitude;
			}
			else
			{
				this = Vector2.zero;
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000A19 RID: 2585 RVA: 0x0001718C File Offset: 0x0001538C
		public Vector2 normalized
		{
			get
			{
				Vector2 vector = new Vector2(this.x, this.y);
				vector.Normalize();
				return vector;
			}
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x000171B4 File Offset: 0x000153B4
		public override string ToString()
		{
			return UnityString.Format("({0:F1}, {1:F1})", new object[] { this.x, this.y });
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x000171F0 File Offset: 0x000153F0
		public string ToString(string format)
		{
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format),
				this.y.ToString(format)
			});
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0001722C File Offset: 0x0001542C
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ (this.y.GetHashCode() << 2);
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x00017248 File Offset: 0x00015448
		public override bool Equals(object other)
		{
			if (!(other is Vector2))
			{
				return false;
			}
			Vector2 vector = (Vector2)other;
			return this.x.Equals(vector.x) && this.y.Equals(vector.y);
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x00017298 File Offset: 0x00015498
		public static float Dot(Vector2 lhs, Vector2 rhs)
		{
			return lhs.x * rhs.x + lhs.y * rhs.y;
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x000172BC File Offset: 0x000154BC
		public float magnitude
		{
			get
			{
				return Mathf.Sqrt(this.x * this.x + this.y * this.y);
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x000172EC File Offset: 0x000154EC
		public float sqrMagnitude
		{
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0001730C File Offset: 0x0001550C
		public static float Angle(Vector2 from, Vector2 to)
		{
			return Mathf.Acos(Mathf.Clamp(Vector2.Dot(from.normalized, to.normalized), -1f, 1f)) * 57.29578f;
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00017348 File Offset: 0x00015548
		public static float Distance(Vector2 a, Vector2 b)
		{
			return (a - b).magnitude;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00017364 File Offset: 0x00015564
		public static Vector2 ClampMagnitude(Vector2 vector, float maxLength)
		{
			if (vector.sqrMagnitude > maxLength * maxLength)
			{
				return vector.normalized * maxLength;
			}
			return vector;
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x00017384 File Offset: 0x00015584
		public static float SqrMagnitude(Vector2 a)
		{
			return a.x * a.x + a.y * a.y;
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x000173A8 File Offset: 0x000155A8
		public float SqrMagnitude()
		{
			return this.x * this.x + this.y * this.y;
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x000173C8 File Offset: 0x000155C8
		public static Vector2 Min(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x000173F8 File Offset: 0x000155F8
		public static Vector2 Max(Vector2 lhs, Vector2 rhs)
		{
			return new Vector2(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00017428 File Offset: 0x00015628
		[ExcludeFromDocs]
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00017448 File Offset: 0x00015648
		[ExcludeFromDocs]
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Vector2.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0001746C File Offset: 0x0001566C
		public static Vector2 SmoothDamp(Vector2 current, Vector2 target, ref Vector2 currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			Vector2 vector = current - target;
			Vector2 vector2 = target;
			float num4 = maxSpeed * smoothTime;
			vector = Vector2.ClampMagnitude(vector, num4);
			target = current - vector;
			Vector2 vector3 = (currentVelocity + num * vector) * deltaTime;
			currentVelocity = (currentVelocity - num * vector3) * num3;
			Vector2 vector4 = target + (vector + vector3) * num3;
			if (Vector2.Dot(vector2 - current, vector4 - vector2) > 0f)
			{
				vector4 = vector2;
				currentVelocity = (vector4 - vector2) / deltaTime;
			}
			return vector4;
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000A2B RID: 2603 RVA: 0x00017568 File Offset: 0x00015768
		public static Vector2 zero
		{
			get
			{
				return new Vector2(0f, 0f);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0001757C File Offset: 0x0001577C
		public static Vector2 one
		{
			get
			{
				return new Vector2(1f, 1f);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000A2D RID: 2605 RVA: 0x00017590 File Offset: 0x00015790
		public static Vector2 up
		{
			get
			{
				return new Vector2(0f, 1f);
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000A2E RID: 2606 RVA: 0x000175A4 File Offset: 0x000157A4
		public static Vector2 right
		{
			get
			{
				return new Vector2(1f, 0f);
			}
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x000175B8 File Offset: 0x000157B8
		public static Vector2 operator +(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x000175E0 File Offset: 0x000157E0
		public static Vector2 operator -(Vector2 a, Vector2 b)
		{
			return new Vector2(a.x - b.x, a.y - b.y);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00017608 File Offset: 0x00015808
		public static Vector2 operator -(Vector2 a)
		{
			return new Vector2(-a.x, -a.y);
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x00017620 File Offset: 0x00015820
		public static Vector2 operator *(Vector2 a, float d)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0001763C File Offset: 0x0001583C
		public static Vector2 operator *(float d, Vector2 a)
		{
			return new Vector2(a.x * d, a.y * d);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00017658 File Offset: 0x00015858
		public static Vector2 operator /(Vector2 a, float d)
		{
			return new Vector2(a.x / d, a.y / d);
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00017674 File Offset: 0x00015874
		public static bool operator ==(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) < 9.9999994E-11f;
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x0001768C File Offset: 0x0001588C
		public static bool operator !=(Vector2 lhs, Vector2 rhs)
		{
			return Vector2.SqrMagnitude(lhs - rhs) >= 9.9999994E-11f;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x000176A4 File Offset: 0x000158A4
		public static implicit operator Vector2(Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x000176BC File Offset: 0x000158BC
		public static implicit operator Vector3(Vector2 v)
		{
			return new Vector3(v.x, v.y, 0f);
		}

		// Token: 0x040004DC RID: 1244
		public const float kEpsilon = 1E-05f;

		// Token: 0x040004DD RID: 1245
		public float x;

		// Token: 0x040004DE RID: 1246
		public float y;
	}
}
