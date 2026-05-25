using System;
using UnityEngine;

// Token: 0x02000016 RID: 22
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Crease")]
[Serializable]
public class Crease : PostEffectsBase
{
	// Token: 0x0600003B RID: 59 RVA: 0x000051AC File Offset: 0x000033AC
	public Crease()
	{
		this.intensity = 0.5f;
		this.softness = 1;
		this.spread = 1f;
	}

	// Token: 0x0600003C RID: 60 RVA: 0x000051D4 File Offset: 0x000033D4
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.blurMaterial = this.CheckShaderAndCreateMaterial(this.blurShader, this.blurMaterial);
		this.depthFetchMaterial = this.CheckShaderAndCreateMaterial(this.depthFetchShader, this.depthFetchMaterial);
		this.creaseApplyMaterial = this.CheckShaderAndCreateMaterial(this.creaseApplyShader, this.creaseApplyMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x0600003D RID: 61 RVA: 0x00005248 File Offset: 0x00003448
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			float num = 1f * (float)source.width / (1f * (float)source.height);
			float num2 = 0.001953125f;
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0);
			RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0);
			Graphics.Blit(source, temporary, this.depthFetchMaterial);
			Graphics.Blit(temporary, temporary2);
			for (int i = 0; i < this.softness; i++)
			{
				this.blurMaterial.SetVector("offsets", new Vector4((float)0, this.spread * num2, (float)0, (float)0));
				Graphics.Blit(temporary2, temporary3, this.blurMaterial);
				this.blurMaterial.SetVector("offsets", new Vector4(this.spread * num2 / num, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary3, temporary2, this.blurMaterial);
			}
			this.creaseApplyMaterial.SetTexture("_HrDepthTex", temporary);
			this.creaseApplyMaterial.SetTexture("_LrDepthTex", temporary2);
			this.creaseApplyMaterial.SetFloat("intensity", this.intensity);
			Graphics.Blit(source, destination, this.creaseApplyMaterial);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
		}
	}

	// Token: 0x0600003E RID: 62 RVA: 0x000053BC File Offset: 0x000035BC
	public override void Main()
	{
	}

	// Token: 0x040000CC RID: 204
	public float intensity;

	// Token: 0x040000CD RID: 205
	public int softness;

	// Token: 0x040000CE RID: 206
	public float spread;

	// Token: 0x040000CF RID: 207
	public Shader blurShader;

	// Token: 0x040000D0 RID: 208
	private Material blurMaterial;

	// Token: 0x040000D1 RID: 209
	public Shader depthFetchShader;

	// Token: 0x040000D2 RID: 210
	private Material depthFetchMaterial;

	// Token: 0x040000D3 RID: 211
	public Shader creaseApplyShader;

	// Token: 0x040000D4 RID: 212
	private Material creaseApplyMaterial;
}
