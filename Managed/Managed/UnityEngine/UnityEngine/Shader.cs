using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200013A RID: 314
	public sealed class Shader : Object
	{
		// Token: 0x06000D28 RID: 3368
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Shader Find(string name);

		// Token: 0x06000D29 RID: 3369
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Shader FindBuiltin(string name);

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06000D2A RID: 3370
		public extern bool isSupported
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000D2B RID: 3371
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void EnableKeyword(string keyword);

		// Token: 0x06000D2C RID: 3372
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DisableKeyword(string keyword);

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06000D2D RID: 3373
		// (set) Token: 0x06000D2E RID: 3374
		public extern int maximumLOD
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06000D2F RID: 3375
		// (set) Token: 0x06000D30 RID: 3376
		public static extern int globalMaximumLOD
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06000D31 RID: 3377
		public extern int renderQueue
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000D32 RID: 3378 RVA: 0x0001D2AC File Offset: 0x0001B4AC
		public static void SetGlobalColor(string propertyName, Color color)
		{
			Shader.SetGlobalColor(Shader.PropertyToID(propertyName), color);
		}

		// Token: 0x06000D33 RID: 3379 RVA: 0x0001D2BC File Offset: 0x0001B4BC
		public static void SetGlobalColor(int nameID, Color color)
		{
			Shader.INTERNAL_CALL_SetGlobalColor(nameID, ref color);
		}

		// Token: 0x06000D34 RID: 3380
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetGlobalColor(int nameID, ref Color color);

		// Token: 0x06000D35 RID: 3381 RVA: 0x0001D2C8 File Offset: 0x0001B4C8
		public static void SetGlobalVector(string propertyName, Vector4 vec)
		{
			Shader.SetGlobalColor(propertyName, vec);
		}

		// Token: 0x06000D36 RID: 3382 RVA: 0x0001D2D8 File Offset: 0x0001B4D8
		public static void SetGlobalVector(int nameID, Vector4 vec)
		{
			Shader.SetGlobalColor(nameID, vec);
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0001D2E8 File Offset: 0x0001B4E8
		public static void SetGlobalFloat(string propertyName, float value)
		{
			Shader.SetGlobalFloat(Shader.PropertyToID(propertyName), value);
		}

		// Token: 0x06000D38 RID: 3384
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalFloat(int nameID, float value);

		// Token: 0x06000D39 RID: 3385 RVA: 0x0001D2F8 File Offset: 0x0001B4F8
		public static void SetGlobalInt(string propertyName, int value)
		{
			Shader.SetGlobalFloat(propertyName, (float)value);
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0001D304 File Offset: 0x0001B504
		public static void SetGlobalInt(int nameID, int value)
		{
			Shader.SetGlobalFloat(nameID, (float)value);
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0001D310 File Offset: 0x0001B510
		public static void SetGlobalTexture(string propertyName, Texture tex)
		{
			Shader.SetGlobalTexture(Shader.PropertyToID(propertyName), tex);
		}

		// Token: 0x06000D3C RID: 3388
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalTexture(int nameID, Texture tex);

		// Token: 0x06000D3D RID: 3389 RVA: 0x0001D320 File Offset: 0x0001B520
		public static void SetGlobalMatrix(string propertyName, Matrix4x4 mat)
		{
			Shader.SetGlobalMatrix(Shader.PropertyToID(propertyName), mat);
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0001D330 File Offset: 0x0001B530
		public static void SetGlobalMatrix(int nameID, Matrix4x4 mat)
		{
			Shader.INTERNAL_CALL_SetGlobalMatrix(nameID, ref mat);
		}

		// Token: 0x06000D3F RID: 3391
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetGlobalMatrix(int nameID, ref Matrix4x4 mat);

		// Token: 0x06000D40 RID: 3392
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalTexGenMode(string propertyName, TexGenMode mode);

		// Token: 0x06000D41 RID: 3393
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalTextureMatrixName(string propertyName, string matrixName);

		// Token: 0x06000D42 RID: 3394
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetGlobalBuffer(string propertyName, ComputeBuffer buffer);

		// Token: 0x06000D43 RID: 3395
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int PropertyToID(string name);

		// Token: 0x06000D44 RID: 3396
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void WarmupAllShaders();
	}
}
