using System;
using UnityEngine;

// Token: 0x02000015 RID: 21
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Contrast Enhance (Unsharp Mask)")]
[Serializable]
public class ContrastEnhance : PostEffectsBase
{
	// Token: 0x06000037 RID: 55 RVA: 0x00004FBC File Offset: 0x000031BC
	public ContrastEnhance()
	{
		this.intensity = 0.5f;
		this.blurSpread = 1f;
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00004FDC File Offset: 0x000031DC
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.contrastCompositeMaterial = this.CheckShaderAndCreateMaterial(this.contrastCompositeShader, this.contrastCompositeMaterial);
		this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00005038 File Offset: 0x00003238
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			RenderTexture temporary = RenderTexture.GetTemporary((int)((float)source.width / 2f), (int)((float)source.height / 2f), 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)source.width / 4f), (int)((float)source.height / 4f), 0);
			RenderTexture temporary3 = RenderTexture.GetTemporary((int)((float)source.width / 4f), (int)((float)source.height / 4f), 0);
			Graphics.Blit(source, temporary);
			Graphics.Blit(temporary, temporary2);
			this.separableBlurMaterial.SetVector("offsets", new Vector4((float)0, this.blurSpread * 1f / (float)temporary2.height, (float)0, (float)0));
			Graphics.Blit(temporary2, temporary3, this.separableBlurMaterial);
			this.separableBlurMaterial.SetVector("offsets", new Vector4(this.blurSpread * 1f / (float)temporary2.width, (float)0, (float)0, (float)0));
			Graphics.Blit(temporary3, temporary2, this.separableBlurMaterial);
			this.contrastCompositeMaterial.SetTexture("_MainTexBlurred", temporary2);
			this.contrastCompositeMaterial.SetFloat("intensity", this.intensity);
			this.contrastCompositeMaterial.SetFloat("threshhold", this.threshhold);
			Graphics.Blit(source, destination, this.contrastCompositeMaterial);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
		}
	}

	// Token: 0x0600003A RID: 58 RVA: 0x000051A8 File Offset: 0x000033A8
	public override void Main()
	{
	}

	// Token: 0x040000C5 RID: 197
	public float intensity;

	// Token: 0x040000C6 RID: 198
	public float threshhold;

	// Token: 0x040000C7 RID: 199
	private Material separableBlurMaterial;

	// Token: 0x040000C8 RID: 200
	private Material contrastCompositeMaterial;

	// Token: 0x040000C9 RID: 201
	public float blurSpread;

	// Token: 0x040000CA RID: 202
	public Shader separableBlurShader;

	// Token: 0x040000CB RID: 203
	public Shader contrastCompositeShader;
}
