using System;
using UnityEngine;

// Token: 0x02000025 RID: 37
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[Serializable]
public class PostEffectsBase : MonoBehaviour
{
	// Token: 0x06000071 RID: 113 RVA: 0x00008154 File Offset: 0x00006354
	public PostEffectsBase()
	{
		this.supportHDRTextures = true;
		this.isSupported = true;
	}

	// Token: 0x06000072 RID: 114 RVA: 0x0000816C File Offset: 0x0000636C
	public virtual Material CheckShaderAndCreateMaterial(Shader s, Material m2Create)
	{
		Material material;
		if (!s)
		{
			Debug.Log("Missing shader in " + this.ToString());
			this.enabled = false;
			material = null;
		}
		else if (s.isSupported && m2Create && m2Create.shader == s)
		{
			material = m2Create;
		}
		else if (!s.isSupported)
		{
			this.NotSupported();
			Debug.Log("The shader " + s.ToString() + " on effect " + this.ToString() + " is not supported on this platform!");
			material = null;
		}
		else
		{
			m2Create = new Material(s);
			m2Create.hideFlags = HideFlags.DontSave;
			material = ((!m2Create) ? null : m2Create);
		}
		return material;
	}

	// Token: 0x06000073 RID: 115 RVA: 0x00008248 File Offset: 0x00006448
	public virtual Material CreateMaterial(Shader s, Material m2Create)
	{
		Material material;
		if (!s)
		{
			Debug.Log("Missing shader in " + this.ToString());
			material = null;
		}
		else if (m2Create && m2Create.shader == s && s.isSupported)
		{
			material = m2Create;
		}
		else if (!s.isSupported)
		{
			material = null;
		}
		else
		{
			m2Create = new Material(s);
			m2Create.hideFlags = HideFlags.DontSave;
			material = ((!m2Create) ? null : m2Create);
		}
		return material;
	}

	// Token: 0x06000074 RID: 116 RVA: 0x000082E4 File Offset: 0x000064E4
	public virtual void OnEnable()
	{
		this.isSupported = true;
	}

	// Token: 0x06000075 RID: 117 RVA: 0x000082F0 File Offset: 0x000064F0
	public virtual bool CheckSupport()
	{
		return this.CheckSupport(false);
	}

	// Token: 0x06000076 RID: 118 RVA: 0x000082FC File Offset: 0x000064FC
	public virtual bool CheckResources()
	{
		Debug.LogWarning("CheckResources () for " + this.ToString() + " should be overwritten.");
		return this.isSupported;
	}

	// Token: 0x06000077 RID: 119 RVA: 0x00008324 File Offset: 0x00006524
	public virtual void Start()
	{
		this.CheckResources();
	}

	// Token: 0x06000078 RID: 120 RVA: 0x00008330 File Offset: 0x00006530
	public virtual bool CheckSupport(bool needDepth)
	{
		this.isSupported = true;
		this.supportHDRTextures = SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.ARGBHalf);
		bool flag;
		if (flag = SystemInfo.graphicsShaderLevel >= 50)
		{
			flag = SystemInfo.supportsComputeShaders;
		}
		this.supportDX11 = flag;
		bool flag2;
		if (!SystemInfo.supportsImageEffects || !SystemInfo.supportsRenderTextures)
		{
			this.NotSupported();
			flag2 = false;
		}
		else if (needDepth && !SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.NotSupported();
			flag2 = false;
		}
		else
		{
			if (needDepth)
			{
				this.camera.depthTextureMode = this.camera.depthTextureMode | DepthTextureMode.Depth;
			}
			flag2 = true;
		}
		return flag2;
	}

	// Token: 0x06000079 RID: 121 RVA: 0x000083CC File Offset: 0x000065CC
	public virtual bool CheckSupport(bool needDepth, bool needHdr)
	{
		bool flag;
		if (!this.CheckSupport(needDepth))
		{
			flag = false;
		}
		else if (needHdr && !this.supportHDRTextures)
		{
			this.NotSupported();
			flag = false;
		}
		else
		{
			flag = true;
		}
		return flag;
	}

	// Token: 0x0600007A RID: 122 RVA: 0x0000840C File Offset: 0x0000660C
	public virtual bool Dx11Support()
	{
		return this.supportDX11;
	}

	// Token: 0x0600007B RID: 123 RVA: 0x00008414 File Offset: 0x00006614
	public virtual void ReportAutoDisable()
	{
		Debug.LogWarning("The image effect " + this.ToString() + " has been disabled as it's not supported on the current platform.");
	}

	// Token: 0x0600007C RID: 124 RVA: 0x00008438 File Offset: 0x00006638
	public virtual bool CheckShader(Shader s)
	{
		Debug.Log("The shader " + s.ToString() + " on effect " + this.ToString() + " is not part of the Unity 3.2+ effects suite anymore. For best performance and quality, please ensure you are using the latest Standard Assets Image Effects (Pro only) package.");
		bool flag;
		if (!s.isSupported)
		{
			this.NotSupported();
			flag = false;
		}
		else
		{
			flag = false;
		}
		return flag;
	}

	// Token: 0x0600007D RID: 125 RVA: 0x00008498 File Offset: 0x00006698
	public virtual void NotSupported()
	{
		this.enabled = false;
		this.isSupported = false;
	}

	// Token: 0x0600007E RID: 126 RVA: 0x000084A8 File Offset: 0x000066A8
	public virtual void DrawBorder(RenderTexture dest, Material material)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		RenderTexture.active = dest;
		bool flag = true;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			float num5 = 0f;
			float num6 = 0f;
			if (flag)
			{
				num5 = 1f;
				num6 = (float)0;
			}
			else
			{
				num5 = (float)0;
				num6 = 1f;
			}
			num = (float)0;
			num2 = (float)0 + 1f / ((float)dest.width * 1f);
			num3 = (float)0;
			num4 = 1f;
			GL.Begin(7);
			GL.TexCoord2((float)0, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2((float)0, num6);
			GL.Vertex3(num, num4, 0.1f);
			num = 1f - 1f / ((float)dest.width * 1f);
			num2 = 1f;
			num3 = (float)0;
			num4 = 1f;
			GL.TexCoord2((float)0, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2((float)0, num6);
			GL.Vertex3(num, num4, 0.1f);
			num = (float)0;
			num2 = 1f;
			num3 = (float)0;
			num4 = (float)0 + 1f / ((float)dest.height * 1f);
			GL.TexCoord2((float)0, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2((float)0, num6);
			GL.Vertex3(num, num4, 0.1f);
			num = (float)0;
			num2 = 1f;
			num3 = 1f - 1f / ((float)dest.height * 1f);
			num4 = 1f;
			GL.TexCoord2((float)0, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2((float)0, num6);
			GL.Vertex3(num, num4, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00008750 File Offset: 0x00006950
	public virtual void Main()
	{
	}

	// Token: 0x04000163 RID: 355
	protected bool supportHDRTextures;

	// Token: 0x04000164 RID: 356
	protected bool supportDX11;

	// Token: 0x04000165 RID: 357
	protected bool isSupported;
}
