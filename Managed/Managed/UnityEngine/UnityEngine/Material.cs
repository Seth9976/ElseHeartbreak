using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200013B RID: 315
	public class Material : Object
	{
		// Token: 0x06000D45 RID: 3397 RVA: 0x0001D33C File Offset: 0x0001B53C
		public Material(string contents)
		{
			Material.Internal_CreateWithString(this, contents);
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0001D34C File Offset: 0x0001B54C
		public Material(Shader shader)
		{
			Material.Internal_CreateWithShader(this, shader);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0001D35C File Offset: 0x0001B55C
		public Material(Material source)
		{
			Material.Internal_CreateWithMaterial(this, source);
		}

		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06000D48 RID: 3400
		// (set) Token: 0x06000D49 RID: 3401
		public extern Shader shader
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000D4A RID: 3402 RVA: 0x0001D36C File Offset: 0x0001B56C
		// (set) Token: 0x06000D4B RID: 3403 RVA: 0x0001D37C File Offset: 0x0001B57C
		public Color color
		{
			get
			{
				return this.GetColor("_Color");
			}
			set
			{
				this.SetColor("_Color", value);
			}
		}

		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000D4C RID: 3404 RVA: 0x0001D38C File Offset: 0x0001B58C
		// (set) Token: 0x06000D4D RID: 3405 RVA: 0x0001D39C File Offset: 0x0001B59C
		public Texture mainTexture
		{
			get
			{
				return this.GetTexture("_MainTex");
			}
			set
			{
				this.SetTexture("_MainTex", value);
			}
		}

		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000D4E RID: 3406 RVA: 0x0001D3AC File Offset: 0x0001B5AC
		// (set) Token: 0x06000D4F RID: 3407 RVA: 0x0001D3BC File Offset: 0x0001B5BC
		public Vector2 mainTextureOffset
		{
			get
			{
				return this.GetTextureOffset("_MainTex");
			}
			set
			{
				this.SetTextureOffset("_MainTex", value);
			}
		}

		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0001D3CC File Offset: 0x0001B5CC
		// (set) Token: 0x06000D51 RID: 3409 RVA: 0x0001D3DC File Offset: 0x0001B5DC
		public Vector2 mainTextureScale
		{
			get
			{
				return this.GetTextureScale("_MainTex");
			}
			set
			{
				this.SetTextureScale("_MainTex", value);
			}
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0001D3EC File Offset: 0x0001B5EC
		public void SetColor(string propertyName, Color color)
		{
			this.SetColor(Shader.PropertyToID(propertyName), color);
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0001D3FC File Offset: 0x0001B5FC
		public void SetColor(int nameID, Color color)
		{
			Material.INTERNAL_CALL_SetColor(this, nameID, ref color);
		}

		// Token: 0x06000D54 RID: 3412
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetColor(Material self, int nameID, ref Color color);

		// Token: 0x06000D55 RID: 3413 RVA: 0x0001D408 File Offset: 0x0001B608
		public Color GetColor(string propertyName)
		{
			return this.GetColor(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000D56 RID: 3414
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Color GetColor(int nameID);

		// Token: 0x06000D57 RID: 3415 RVA: 0x0001D418 File Offset: 0x0001B618
		public void SetVector(string propertyName, Vector4 vector)
		{
			this.SetColor(propertyName, new Color(vector.x, vector.y, vector.z, vector.w));
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0001D450 File Offset: 0x0001B650
		public void SetVector(int nameID, Vector4 vector)
		{
			this.SetColor(nameID, new Color(vector.x, vector.y, vector.z, vector.w));
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0001D488 File Offset: 0x0001B688
		public Vector4 GetVector(string propertyName)
		{
			Color color = this.GetColor(propertyName);
			return new Vector4(color.r, color.g, color.b, color.a);
		}

		// Token: 0x06000D5A RID: 3418 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		public Vector4 GetVector(int nameID)
		{
			Color color = this.GetColor(nameID);
			return new Vector4(color.r, color.g, color.b, color.a);
		}

		// Token: 0x06000D5B RID: 3419 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
		public void SetTexture(string propertyName, Texture texture)
		{
			this.SetTexture(Shader.PropertyToID(propertyName), texture);
		}

		// Token: 0x06000D5C RID: 3420
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetTexture(int nameID, Texture texture);

		// Token: 0x06000D5D RID: 3421 RVA: 0x0001D508 File Offset: 0x0001B708
		public Texture GetTexture(string propertyName)
		{
			return this.GetTexture(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000D5E RID: 3422
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Texture GetTexture(int nameID);

		// Token: 0x06000D5F RID: 3423
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTextureOffset(Material mat, string name, out Vector2 output);

		// Token: 0x06000D60 RID: 3424
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTextureScale(Material mat, string name, out Vector2 output);

		// Token: 0x06000D61 RID: 3425 RVA: 0x0001D518 File Offset: 0x0001B718
		public void SetTextureOffset(string propertyName, Vector2 offset)
		{
			Material.INTERNAL_CALL_SetTextureOffset(this, propertyName, ref offset);
		}

		// Token: 0x06000D62 RID: 3426
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetTextureOffset(Material self, string propertyName, ref Vector2 offset);

		// Token: 0x06000D63 RID: 3427 RVA: 0x0001D524 File Offset: 0x0001B724
		public Vector2 GetTextureOffset(string propertyName)
		{
			Vector2 vector;
			Material.Internal_GetTextureOffset(this, propertyName, out vector);
			return vector;
		}

		// Token: 0x06000D64 RID: 3428 RVA: 0x0001D53C File Offset: 0x0001B73C
		public void SetTextureScale(string propertyName, Vector2 scale)
		{
			Material.INTERNAL_CALL_SetTextureScale(this, propertyName, ref scale);
		}

		// Token: 0x06000D65 RID: 3429
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetTextureScale(Material self, string propertyName, ref Vector2 scale);

		// Token: 0x06000D66 RID: 3430 RVA: 0x0001D548 File Offset: 0x0001B748
		public Vector2 GetTextureScale(string propertyName)
		{
			Vector2 vector;
			Material.Internal_GetTextureScale(this, propertyName, out vector);
			return vector;
		}

		// Token: 0x06000D67 RID: 3431 RVA: 0x0001D560 File Offset: 0x0001B760
		public void SetMatrix(string propertyName, Matrix4x4 matrix)
		{
			this.SetMatrix(Shader.PropertyToID(propertyName), matrix);
		}

		// Token: 0x06000D68 RID: 3432 RVA: 0x0001D570 File Offset: 0x0001B770
		public void SetMatrix(int nameID, Matrix4x4 matrix)
		{
			Material.INTERNAL_CALL_SetMatrix(this, nameID, ref matrix);
		}

		// Token: 0x06000D69 RID: 3433
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetMatrix(Material self, int nameID, ref Matrix4x4 matrix);

		// Token: 0x06000D6A RID: 3434 RVA: 0x0001D57C File Offset: 0x0001B77C
		public Matrix4x4 GetMatrix(string propertyName)
		{
			return this.GetMatrix(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000D6B RID: 3435
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Matrix4x4 GetMatrix(int nameID);

		// Token: 0x06000D6C RID: 3436 RVA: 0x0001D58C File Offset: 0x0001B78C
		public void SetFloat(string propertyName, float value)
		{
			this.SetFloat(Shader.PropertyToID(propertyName), value);
		}

		// Token: 0x06000D6D RID: 3437
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetFloat(int nameID, float value);

		// Token: 0x06000D6E RID: 3438 RVA: 0x0001D59C File Offset: 0x0001B79C
		public float GetFloat(string propertyName)
		{
			return this.GetFloat(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000D6F RID: 3439
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetFloat(int nameID);

		// Token: 0x06000D70 RID: 3440 RVA: 0x0001D5AC File Offset: 0x0001B7AC
		public void SetInt(string propertyName, int value)
		{
			this.SetFloat(propertyName, (float)value);
		}

		// Token: 0x06000D71 RID: 3441 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		public void SetInt(int nameID, int value)
		{
			this.SetFloat(nameID, (float)value);
		}

		// Token: 0x06000D72 RID: 3442 RVA: 0x0001D5C4 File Offset: 0x0001B7C4
		public int GetInt(string propertyName)
		{
			return (int)this.GetFloat(propertyName);
		}

		// Token: 0x06000D73 RID: 3443 RVA: 0x0001D5D0 File Offset: 0x0001B7D0
		public int GetInt(int nameID)
		{
			return (int)this.GetFloat(nameID);
		}

		// Token: 0x06000D74 RID: 3444
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetBuffer(string propertyName, ComputeBuffer buffer);

		// Token: 0x06000D75 RID: 3445 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		public bool HasProperty(string propertyName)
		{
			return this.HasProperty(Shader.PropertyToID(propertyName));
		}

		// Token: 0x06000D76 RID: 3446
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasProperty(int nameID);

		// Token: 0x06000D77 RID: 3447
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern string GetTag(string tag, bool searchFallbacks, [DefaultValue("\"\"")] string defaultValue);

		// Token: 0x06000D78 RID: 3448 RVA: 0x0001D5EC File Offset: 0x0001B7EC
		[ExcludeFromDocs]
		public string GetTag(string tag, bool searchFallbacks)
		{
			string empty = string.Empty;
			return this.GetTag(tag, searchFallbacks, empty);
		}

		// Token: 0x06000D79 RID: 3449
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Lerp(Material start, Material end, float t);

		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000D7A RID: 3450
		public extern int passCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000D7B RID: 3451
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool SetPass(int pass);

		// Token: 0x17000307 RID: 775
		// (get) Token: 0x06000D7C RID: 3452
		// (set) Token: 0x06000D7D RID: 3453
		public extern int renderQueue
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x0001D608 File Offset: 0x0001B808
		[Obsolete("Use the Material constructor instead.")]
		public static Material Create(string scriptContents)
		{
			return new Material(scriptContents);
		}

		// Token: 0x06000D7F RID: 3455
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWithString([Writable] Material mono, string contents);

		// Token: 0x06000D80 RID: 3456
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWithShader([Writable] Material mono, Shader shader);

		// Token: 0x06000D81 RID: 3457
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateWithMaterial([Writable] Material mono, Material source);

		// Token: 0x06000D82 RID: 3458
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyPropertiesFromMaterial(Material mat);

		// Token: 0x06000D83 RID: 3459
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void EnableKeyword(string keyword);

		// Token: 0x06000D84 RID: 3460
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DisableKeyword(string keyword);

		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000D85 RID: 3461
		// (set) Token: 0x06000D86 RID: 3462
		public extern string[] shaderKeywords
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
