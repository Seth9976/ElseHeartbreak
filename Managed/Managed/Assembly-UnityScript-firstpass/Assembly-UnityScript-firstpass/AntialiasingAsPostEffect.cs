using System;
using UnityEngine;

// Token: 0x02000003 RID: 3
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Antialiasing (Fullscreen)")]
[Serializable]
public class AntialiasingAsPostEffect : PostEffectsBase
{
	// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
	public AntialiasingAsPostEffect()
	{
		this.mode = AAMode.FXAA3Console;
		this.offsetScale = 0.2f;
		this.blurRadius = 18f;
		this.edgeThresholdMin = 0.05f;
		this.edgeThreshold = 0.2f;
		this.edgeSharpness = 4f;
	}

	// Token: 0x06000002 RID: 2 RVA: 0x00002140 File Offset: 0x00000340
	public virtual Material CurrentAAMaterial()
	{
		AAMode aamode = this.mode;
		Material material;
		if (aamode == AAMode.FXAA3Console)
		{
			material = this.materialFXAAIII;
		}
		else if (aamode == AAMode.FXAA2)
		{
			material = this.materialFXAAII;
		}
		else if (aamode == AAMode.FXAA1PresetA)
		{
			material = this.materialFXAAPreset2;
		}
		else if (aamode == AAMode.FXAA1PresetB)
		{
			material = this.materialFXAAPreset3;
		}
		else if (aamode == AAMode.NFAA)
		{
			material = this.nfaa;
		}
		else if (aamode == AAMode.SSAA)
		{
			material = this.ssaa;
		}
		else if (aamode == AAMode.DLAA)
		{
			material = this.dlaa;
		}
		else
		{
			material = null;
		}
		return material;
	}

	// Token: 0x06000003 RID: 3 RVA: 0x000021E4 File Offset: 0x000003E4
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.materialFXAAPreset2 = this.CreateMaterial(this.shaderFXAAPreset2, this.materialFXAAPreset2);
		this.materialFXAAPreset3 = this.CreateMaterial(this.shaderFXAAPreset3, this.materialFXAAPreset3);
		this.materialFXAAII = this.CreateMaterial(this.shaderFXAAII, this.materialFXAAII);
		this.materialFXAAIII = this.CreateMaterial(this.shaderFXAAIII, this.materialFXAAIII);
		this.nfaa = this.CreateMaterial(this.nfaaShader, this.nfaa);
		this.ssaa = this.CreateMaterial(this.ssaaShader, this.ssaa);
		this.dlaa = this.CreateMaterial(this.dlaaShader, this.dlaa);
		if (!this.ssaaShader.isSupported)
		{
			this.NotSupported();
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000004 RID: 4 RVA: 0x000022C4 File Offset: 0x000004C4
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else if (this.mode == AAMode.FXAA3Console && this.materialFXAAIII != null)
		{
			this.materialFXAAIII.SetFloat("_EdgeThresholdMin", this.edgeThresholdMin);
			this.materialFXAAIII.SetFloat("_EdgeThreshold", this.edgeThreshold);
			this.materialFXAAIII.SetFloat("_EdgeSharpness", this.edgeSharpness);
			Graphics.Blit(source, destination, this.materialFXAAIII);
		}
		else if (this.mode == AAMode.FXAA1PresetB && this.materialFXAAPreset3 != null)
		{
			Graphics.Blit(source, destination, this.materialFXAAPreset3);
		}
		else if (this.mode == AAMode.FXAA1PresetA && this.materialFXAAPreset2 != null)
		{
			source.anisoLevel = 4;
			Graphics.Blit(source, destination, this.materialFXAAPreset2);
			source.anisoLevel = 0;
		}
		else if (this.mode == AAMode.FXAA2 && this.materialFXAAII != null)
		{
			Graphics.Blit(source, destination, this.materialFXAAII);
		}
		else if (this.mode == AAMode.SSAA && this.ssaa != null)
		{
			Graphics.Blit(source, destination, this.ssaa);
		}
		else if (this.mode == AAMode.DLAA && this.dlaa != null)
		{
			source.anisoLevel = 0;
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height);
			Graphics.Blit(source, temporary, this.dlaa, 0);
			Graphics.Blit(temporary, destination, this.dlaa, (!this.dlaaSharp) ? 1 : 2);
			RenderTexture.ReleaseTemporary(temporary);
		}
		else if (this.mode == AAMode.NFAA && this.nfaa != null)
		{
			source.anisoLevel = 0;
			this.nfaa.SetFloat("_OffsetScale", this.offsetScale);
			this.nfaa.SetFloat("_BlurRadius", this.blurRadius);
			Graphics.Blit(source, destination, this.nfaa, (!this.showGeneratedNormals) ? 0 : 1);
		}
		else
		{
			Graphics.Blit(source, destination);
		}
	}

	// Token: 0x06000005 RID: 5 RVA: 0x0000250C File Offset: 0x0000070C
	public override void Main()
	{
	}

	// Token: 0x04000009 RID: 9
	public AAMode mode;

	// Token: 0x0400000A RID: 10
	public bool showGeneratedNormals;

	// Token: 0x0400000B RID: 11
	public float offsetScale;

	// Token: 0x0400000C RID: 12
	public float blurRadius;

	// Token: 0x0400000D RID: 13
	public float edgeThresholdMin;

	// Token: 0x0400000E RID: 14
	public float edgeThreshold;

	// Token: 0x0400000F RID: 15
	public float edgeSharpness;

	// Token: 0x04000010 RID: 16
	public bool dlaaSharp;

	// Token: 0x04000011 RID: 17
	public Shader ssaaShader;

	// Token: 0x04000012 RID: 18
	private Material ssaa;

	// Token: 0x04000013 RID: 19
	public Shader dlaaShader;

	// Token: 0x04000014 RID: 20
	private Material dlaa;

	// Token: 0x04000015 RID: 21
	public Shader nfaaShader;

	// Token: 0x04000016 RID: 22
	private Material nfaa;

	// Token: 0x04000017 RID: 23
	public Shader shaderFXAAPreset2;

	// Token: 0x04000018 RID: 24
	private Material materialFXAAPreset2;

	// Token: 0x04000019 RID: 25
	public Shader shaderFXAAPreset3;

	// Token: 0x0400001A RID: 26
	private Material materialFXAAPreset3;

	// Token: 0x0400001B RID: 27
	public Shader shaderFXAAII;

	// Token: 0x0400001C RID: 28
	private Material materialFXAAII;

	// Token: 0x0400001D RID: 29
	public Shader shaderFXAAIII;

	// Token: 0x0400001E RID: 30
	private Material materialFXAAIII;
}
