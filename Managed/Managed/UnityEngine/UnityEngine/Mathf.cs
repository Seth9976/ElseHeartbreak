using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200011C RID: 284
	public struct Mathf
	{
		// Token: 0x06000B98 RID: 2968 RVA: 0x0001BD90 File Offset: 0x00019F90
		public static float Sin(float f)
		{
			return (float)Math.Sin((double)f);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x0001BD9C File Offset: 0x00019F9C
		public static float Cos(float f)
		{
			return (float)Math.Cos((double)f);
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x0001BDA8 File Offset: 0x00019FA8
		public static float Tan(float f)
		{
			return (float)Math.Tan((double)f);
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x0001BDB4 File Offset: 0x00019FB4
		public static float Asin(float f)
		{
			return (float)Math.Asin((double)f);
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x0001BDC0 File Offset: 0x00019FC0
		public static float Acos(float f)
		{
			return (float)Math.Acos((double)f);
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x0001BDCC File Offset: 0x00019FCC
		public static float Atan(float f)
		{
			return (float)Math.Atan((double)f);
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x0001BDD8 File Offset: 0x00019FD8
		public static float Atan2(float y, float x)
		{
			return (float)Math.Atan2((double)y, (double)x);
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x0001BDE4 File Offset: 0x00019FE4
		public static float Sqrt(float f)
		{
			return (float)Math.Sqrt((double)f);
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x0001BDF0 File Offset: 0x00019FF0
		public static float Abs(float f)
		{
			return Math.Abs(f);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x0001BDFC File Offset: 0x00019FFC
		public static int Abs(int value)
		{
			return Math.Abs(value);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x0001BE04 File Offset: 0x0001A004
		public static float Min(float a, float b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x0001BE14 File Offset: 0x0001A014
		public static float Min(params float[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0f;
			}
			float num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] < num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x0001BE58 File Offset: 0x0001A058
		public static int Min(int a, int b)
		{
			return (a >= b) ? b : a;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x0001BE68 File Offset: 0x0001A068
		public static int Min(params int[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0;
			}
			int num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] < num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x0001BEA8 File Offset: 0x0001A0A8
		public static float Max(float a, float b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x0001BEB8 File Offset: 0x0001A0B8
		public static float Max(params float[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0f;
			}
			float num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] > num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0001BEFC File Offset: 0x0001A0FC
		public static int Max(int a, int b)
		{
			return (a <= b) ? b : a;
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x0001BF0C File Offset: 0x0001A10C
		public static int Max(params int[] values)
		{
			int num = values.Length;
			if (num == 0)
			{
				return 0;
			}
			int num2 = values[0];
			for (int i = 1; i < num; i++)
			{
				if (values[i] > num2)
				{
					num2 = values[i];
				}
			}
			return num2;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0001BF4C File Offset: 0x0001A14C
		public static float Pow(float f, float p)
		{
			return (float)Math.Pow((double)f, (double)p);
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x0001BF58 File Offset: 0x0001A158
		public static float Exp(float power)
		{
			return (float)Math.Exp((double)power);
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0001BF64 File Offset: 0x0001A164
		public static float Log(float f, float p)
		{
			return (float)Math.Log((double)f, (double)p);
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x0001BF70 File Offset: 0x0001A170
		public static float Log(float f)
		{
			return (float)Math.Log((double)f);
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0001BF7C File Offset: 0x0001A17C
		public static float Log10(float f)
		{
			return (float)Math.Log10((double)f);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0001BF88 File Offset: 0x0001A188
		public static float Ceil(float f)
		{
			return (float)Math.Ceiling((double)f);
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0001BF94 File Offset: 0x0001A194
		public static float Floor(float f)
		{
			return (float)Math.Floor((double)f);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0001BFA0 File Offset: 0x0001A1A0
		public static float Round(float f)
		{
			return (float)Math.Round((double)f);
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001BFAC File Offset: 0x0001A1AC
		public static int CeilToInt(float f)
		{
			return (int)Math.Ceiling((double)f);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001BFB8 File Offset: 0x0001A1B8
		public static int FloorToInt(float f)
		{
			return (int)Math.Floor((double)f);
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		public static int RoundToInt(float f)
		{
			return (int)Math.Round((double)f);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0001BFD0 File Offset: 0x0001A1D0
		public static float Sign(float f)
		{
			return (f < 0f) ? (-1f) : 1f;
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001BFEC File Offset: 0x0001A1EC
		public static float Clamp(float value, float min, float max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0001C008 File Offset: 0x0001A208
		public static int Clamp(int value, int min, int max)
		{
			if (value < min)
			{
				value = min;
			}
			else if (value > max)
			{
				value = max;
			}
			return value;
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x0001C024 File Offset: 0x0001A224
		public static float Clamp01(float value)
		{
			if (value < 0f)
			{
				return 0f;
			}
			if (value > 1f)
			{
				return 1f;
			}
			return value;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001C04C File Offset: 0x0001A24C
		public static float Lerp(float from, float to, float t)
		{
			return from + (to - from) * Mathf.Clamp01(t);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001C05C File Offset: 0x0001A25C
		public static float LerpAngle(float a, float b, float t)
		{
			float num = Mathf.Repeat(b - a, 360f);
			if (num > 180f)
			{
				num -= 360f;
			}
			return a + num * Mathf.Clamp01(t);
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001C094 File Offset: 0x0001A294
		public static float MoveTowards(float current, float target, float maxDelta)
		{
			if (Mathf.Abs(target - current) <= maxDelta)
			{
				return target;
			}
			return current + Mathf.Sign(target - current) * maxDelta;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x0001C0B4 File Offset: 0x0001A2B4
		public static float MoveTowardsAngle(float current, float target, float maxDelta)
		{
			target = current + Mathf.DeltaAngle(current, target);
			return Mathf.MoveTowards(current, target, maxDelta);
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x0001C0CC File Offset: 0x0001A2CC
		public static float SmoothStep(float from, float to, float t)
		{
			t = Mathf.Clamp01(t);
			t = -2f * t * t * t + 3f * t * t;
			return to * t + from * (1f - t);
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x0001C108 File Offset: 0x0001A308
		public static float Gamma(float value, float absmax, float gamma)
		{
			bool flag = false;
			if (value < 0f)
			{
				flag = true;
			}
			float num = Mathf.Abs(value);
			if (num > absmax)
			{
				return (!flag) ? num : (-num);
			}
			float num2 = Mathf.Pow(num / absmax, gamma) * absmax;
			return (!flag) ? num2 : (-num2);
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x0001C15C File Offset: 0x0001A35C
		public static bool Approximately(float a, float b)
		{
			return Mathf.Abs(b - a) < Mathf.Max(1E-06f * Mathf.Max(Mathf.Abs(a), Mathf.Abs(b)), 1.1E-44f);
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x0001C18C File Offset: 0x0001A38C
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x0001C1AC File Offset: 0x0001A3AC
		[ExcludeFromDocs]
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x0001C1D0 File Offset: 0x0001A3D0
		public static float SmoothDamp(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			smoothTime = Mathf.Max(0.0001f, smoothTime);
			float num = 2f / smoothTime;
			float num2 = num * deltaTime;
			float num3 = 1f / (1f + num2 + 0.48f * num2 * num2 + 0.235f * num2 * num2 * num2);
			float num4 = current - target;
			float num5 = target;
			float num6 = maxSpeed * smoothTime;
			num4 = Mathf.Clamp(num4, -num6, num6);
			target = current - num4;
			float num7 = (currentVelocity + num * num4) * deltaTime;
			currentVelocity = (currentVelocity - num * num7) * num3;
			float num8 = target + (num4 + num7) * num3;
			if (num5 - current > 0f == num8 > num5)
			{
				num8 = num5;
				currentVelocity = (num8 - num5) / deltaTime;
			}
			return num8;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x0001C280 File Offset: 0x0001A480
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, float maxSpeed)
		{
			float deltaTime = Time.deltaTime;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x0001C2A0 File Offset: 0x0001A4A0
		[ExcludeFromDocs]
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime)
		{
			float deltaTime = Time.deltaTime;
			float positiveInfinity = float.PositiveInfinity;
			return Mathf.SmoothDampAngle(current, target, ref currentVelocity, smoothTime, positiveInfinity, deltaTime);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		public static float SmoothDampAngle(float current, float target, ref float currentVelocity, float smoothTime, [DefaultValue("Mathf.Infinity")] float maxSpeed, [DefaultValue("Time.deltaTime")] float deltaTime)
		{
			target = current + Mathf.DeltaAngle(current, target);
			return Mathf.SmoothDamp(current, target, ref currentVelocity, smoothTime, maxSpeed, deltaTime);
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x0001C2E0 File Offset: 0x0001A4E0
		public static float Repeat(float t, float length)
		{
			return t - Mathf.Floor(t / length) * length;
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x0001C2F0 File Offset: 0x0001A4F0
		public static float PingPong(float t, float length)
		{
			t = Mathf.Repeat(t, length * 2f);
			return length - Mathf.Abs(t - length);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x0001C30C File Offset: 0x0001A50C
		public static float InverseLerp(float from, float to, float value)
		{
			if (from < to)
			{
				if (value < from)
				{
					return 0f;
				}
				if (value > to)
				{
					return 1f;
				}
				value -= from;
				value /= to - from;
				return value;
			}
			else
			{
				if (from <= to)
				{
					return 0f;
				}
				if (value < to)
				{
					return 1f;
				}
				if (value > from)
				{
					return 0f;
				}
				return 1f - (value - to) / (from - to);
			}
		}

		// Token: 0x06000BC9 RID: 3017
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ClosestPowerOfTwo(int value);

		// Token: 0x06000BCA RID: 3018
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GammaToLinearSpace(float value);

		// Token: 0x06000BCB RID: 3019
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float LinearToGammaSpace(float value);

		// Token: 0x06000BCC RID: 3020
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsPowerOfTwo(int value);

		// Token: 0x06000BCD RID: 3021
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int NextPowerOfTwo(int value);

		// Token: 0x06000BCE RID: 3022 RVA: 0x0001C37C File Offset: 0x0001A57C
		public static float DeltaAngle(float current, float target)
		{
			float num = Mathf.Repeat(target - current, 360f);
			if (num > 180f)
			{
				num -= 360f;
			}
			return num;
		}

		// Token: 0x06000BCF RID: 3023
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float PerlinNoise(float x, float y);

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001C3AC File Offset: 0x0001A5AC
		internal static bool LineIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			if (num5 == 0f)
			{
				return false;
			}
			float num6 = p3.x - p1.x;
			float num7 = p3.y - p1.y;
			float num8 = (num6 * num4 - num7 * num3) / num5;
			result = new Vector2(p1.x + num8 * num, p1.y + num8 * num2);
			return true;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001C460 File Offset: 0x0001A660
		internal static bool LineSegmentIntersection(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4, ref Vector2 result)
		{
			float num = p2.x - p1.x;
			float num2 = p2.y - p1.y;
			float num3 = p4.x - p3.x;
			float num4 = p4.y - p3.y;
			float num5 = num * num4 - num2 * num3;
			if (num5 == 0f)
			{
				return false;
			}
			float num6 = p3.x - p1.x;
			float num7 = p3.y - p1.y;
			float num8 = (num6 * num4 - num7 * num3) / num5;
			if (num8 < 0f || num8 > 1f)
			{
				return false;
			}
			float num9 = (num6 * num2 - num7 * num) / num5;
			if (num9 < 0f || num9 > 1f)
			{
				return false;
			}
			result = new Vector2(p1.x + num8 * num, p1.y + num8 * num2);
			return true;
		}

		// Token: 0x04000511 RID: 1297
		public const float PI = 3.1415927f;

		// Token: 0x04000512 RID: 1298
		public const float Infinity = float.PositiveInfinity;

		// Token: 0x04000513 RID: 1299
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x04000514 RID: 1300
		public const float Deg2Rad = 0.017453292f;

		// Token: 0x04000515 RID: 1301
		public const float Rad2Deg = 57.29578f;

		// Token: 0x04000516 RID: 1302
		public const float Epsilon = 1E-45f;
	}
}
