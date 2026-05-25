using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000BD RID: 189
	public sealed class MaterialPropertyBlock
	{
		// Token: 0x060004E7 RID: 1255 RVA: 0x0000BE78 File Offset: 0x0000A078
		public MaterialPropertyBlock()
		{
			this.InitBlock();
		}

		// Token: 0x060004E8 RID: 1256
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void InitBlock();

		// Token: 0x060004E9 RID: 1257
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void DestroyBlock();

		// Token: 0x060004EA RID: 1258 RVA: 0x0000BE88 File Offset: 0x0000A088
		~MaterialPropertyBlock()
		{
			this.DestroyBlock();
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060004EB RID: 1259
		public extern bool isEmpty
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
		public void SetFloat(string name, float value)
		{
			this.SetFloat(Shader.PropertyToID(name), value);
		}

		// Token: 0x060004ED RID: 1261
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float value);

		// Token: 0x060004EE RID: 1262 RVA: 0x0000BED4 File Offset: 0x0000A0D4
		public void SetVector(string name, Vector4 value)
		{
			this.SetVector(Shader.PropertyToID(name), value);
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000BEE4 File Offset: 0x0000A0E4
		public void SetVector(int nameID, Vector4 value)
		{
			MaterialPropertyBlock.INTERNAL_CALL_SetVector(this, nameID, ref value);
		}

		// Token: 0x060004F0 RID: 1264
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetVector(MaterialPropertyBlock self, int nameID, ref Vector4 value);

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000BEF0 File Offset: 0x0000A0F0
		public void SetColor(string name, Color value)
		{
			this.SetColor(Shader.PropertyToID(name), value);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000BF00 File Offset: 0x0000A100
		public void SetColor(int nameID, Color value)
		{
			MaterialPropertyBlock.INTERNAL_CALL_SetColor(this, nameID, ref value);
		}

		// Token: 0x060004F3 RID: 1267
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetColor(MaterialPropertyBlock self, int nameID, ref Color value);

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000BF0C File Offset: 0x0000A10C
		public void SetMatrix(string name, Matrix4x4 value)
		{
			this.SetMatrix(Shader.PropertyToID(name), value);
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000BF1C File Offset: 0x0000A11C
		public void SetMatrix(int nameID, Matrix4x4 value)
		{
			MaterialPropertyBlock.INTERNAL_CALL_SetMatrix(this, nameID, ref value);
		}

		// Token: 0x060004F6 RID: 1270
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetMatrix(MaterialPropertyBlock self, int nameID, ref Matrix4x4 value);

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000BF28 File Offset: 0x0000A128
		public void SetTexture(string name, Texture value)
		{
			this.SetTexture(Shader.PropertyToID(name), value);
		}

		// Token: 0x060004F8 RID: 1272
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int nameID, Texture value);

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000BF38 File Offset: 0x0000A138
		public void AddFloat(string name, float value)
		{
			this.AddFloat(Shader.PropertyToID(name), value);
		}

		// Token: 0x060004FA RID: 1274
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddFloat(int nameID, float value);

		// Token: 0x060004FB RID: 1275 RVA: 0x0000BF48 File Offset: 0x0000A148
		public void AddVector(string name, Vector4 value)
		{
			this.AddVector(Shader.PropertyToID(name), value);
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000BF58 File Offset: 0x0000A158
		public void AddVector(int nameID, Vector4 value)
		{
			MaterialPropertyBlock.INTERNAL_CALL_AddVector(this, nameID, ref value);
		}

		// Token: 0x060004FD RID: 1277
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddVector(MaterialPropertyBlock self, int nameID, ref Vector4 value);

		// Token: 0x060004FE RID: 1278 RVA: 0x0000BF64 File Offset: 0x0000A164
		public void AddColor(string name, Color value)
		{
			this.AddColor(Shader.PropertyToID(name), value);
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x0000BF74 File Offset: 0x0000A174
		public void AddColor(int nameID, Color value)
		{
			MaterialPropertyBlock.INTERNAL_CALL_AddColor(this, nameID, ref value);
		}

		// Token: 0x06000500 RID: 1280
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddColor(MaterialPropertyBlock self, int nameID, ref Color value);

		// Token: 0x06000501 RID: 1281 RVA: 0x0000BF80 File Offset: 0x0000A180
		public void AddMatrix(string name, Matrix4x4 value)
		{
			this.AddMatrix(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x0000BF90 File Offset: 0x0000A190
		public void AddMatrix(int nameID, Matrix4x4 value)
		{
			MaterialPropertyBlock.INTERNAL_CALL_AddMatrix(this, nameID, ref value);
		}

		// Token: 0x06000503 RID: 1283
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddMatrix(MaterialPropertyBlock self, int nameID, ref Matrix4x4 value);

		// Token: 0x06000504 RID: 1284 RVA: 0x0000BF9C File Offset: 0x0000A19C
		public void AddTexture(string name, Texture value)
		{
			this.AddTexture(Shader.PropertyToID(name), value);
		}

		// Token: 0x06000505 RID: 1285
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddTexture(int nameID, Texture value);

		// Token: 0x06000506 RID: 1286 RVA: 0x0000BFAC File Offset: 0x0000A1AC
		public float GetFloat(string name)
		{
			return this.GetFloat(Shader.PropertyToID(name));
		}

		// Token: 0x06000507 RID: 1287
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(int nameID);

		// Token: 0x06000508 RID: 1288 RVA: 0x0000BFBC File Offset: 0x0000A1BC
		public Vector4 GetVector(string name)
		{
			return this.GetVector(Shader.PropertyToID(name));
		}

		// Token: 0x06000509 RID: 1289
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Vector4 GetVector(int nameID);

		// Token: 0x0600050A RID: 1290 RVA: 0x0000BFCC File Offset: 0x0000A1CC
		public Matrix4x4 GetMatrix(string name)
		{
			return this.GetMatrix(Shader.PropertyToID(name));
		}

		// Token: 0x0600050B RID: 1291
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Matrix4x4 GetMatrix(int nameID);

		// Token: 0x0600050C RID: 1292 RVA: 0x0000BFDC File Offset: 0x0000A1DC
		public Texture GetTexture(string name)
		{
			return this.GetTexture(Shader.PropertyToID(name));
		}

		// Token: 0x0600050D RID: 1293
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture GetTexture(int nameID);

		// Token: 0x0600050E RID: 1294
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Clear();

		// Token: 0x0400028F RID: 655
		internal IntPtr m_Ptr;
	}
}
