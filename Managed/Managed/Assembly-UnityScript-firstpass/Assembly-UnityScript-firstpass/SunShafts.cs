using System;
using UnityEngine;

// Token: 0x0200002C RID: 44
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Sun Shafts")]
[Serializable]
public class SunShafts : PostEffectsBase
{
	// Token: 0x06000092 RID: 146 RVA: 0x000090C0 File Offset: 0x000072C0
	public SunShafts()
	{
		this.resolution = SunShaftsResolution.Normal;
		this.screenBlendMode = ShaftsScreenBlendMode.Screen;
		this.radialBlurIterations = 2;
		this.sunColor = Color.white;
		this.sunShaftBlurRadius = 2.5f;
		this.sunShaftIntensity = 1.15f;
		this.useSkyBoxAlpha = 0.75f;
		this.maxRadius = 0.75f;
		this.useDepthTexture = true;
	}

	// Token: 0x06000093 RID: 147 RVA: 0x00009128 File Offset: 0x00007328
	public override bool CheckResources()
	{
		this.CheckSupport(this.useDepthTexture);
		this.sunShaftsMaterial = this.CheckShaderAndCreateMaterial(this.sunShaftsShader, this.sunShaftsMaterial);
		this.simpleClearMaterial = this.CheckShaderAndCreateMaterial(this.simpleClearShader, this.simpleClearMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000094 RID: 148 RVA: 0x0000918C File Offset: 0x0000738C
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.useDepthTexture)
			{
				this.camera.depthTextureMode = this.camera.depthTextureMode | DepthTextureMode.Depth;
			}
			float num = 4f;
			if (this.resolution == SunShaftsResolution.Normal)
			{
				num = 2f;
			}
			else if (this.resolution == SunShaftsResolution.High)
			{
				num = 1f;
			}
			Vector3 vector = Vector3.one * 0.5f;
			if (this.sunTransform)
			{
				vector = this.camera.WorldToViewportPoint(this.sunTransform.position);
			}
			else
			{
				vector = new Vector3(0.5f, 0.5f, (float)0);
			}
			RenderTexture temporary = RenderTexture.GetTemporary((int)((float)source.width / num), (int)((float)source.height / num), 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary((int)((float)source.width / num), (int)((float)source.height / num), 0);
			this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(1f, 1f, (float)0, (float)0) * this.sunShaftBlurRadius);
			this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(vector.x, vector.y, vector.z, this.maxRadius));
			this.sunShaftsMaterial.SetFloat("_NoSkyBoxMask", 1f - this.useSkyBoxAlpha);
			if (!this.useDepthTexture)
			{
				RenderTexture temporary3 = RenderTexture.GetTemporary(source.width, source.height, 0);
				RenderTexture.active = temporary3;
				GL.ClearWithSkybox(false, this.camera);
				this.sunShaftsMaterial.SetTexture("_Skybox", temporary3);
				Graphics.Blit(source, temporary2, this.sunShaftsMaterial, 3);
				RenderTexture.ReleaseTemporary(temporary3);
			}
			else
			{
				Graphics.Blit(source, temporary2, this.sunShaftsMaterial, 2);
			}
			this.DrawBorder(temporary2, this.simpleClearMaterial);
			this.radialBlurIterations = this.ClampBlurIterationsToSomethingThatMakesSense(this.radialBlurIterations);
			float num2 = this.sunShaftBlurRadius * 0.0013020834f;
			this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num2, num2, (float)0, (float)0));
			this.sunShaftsMaterial.SetVector("_SunPosition", new Vector4(vector.x, vector.y, vector.z, this.maxRadius));
			for (int i = 0; i < this.radialBlurIterations; i++)
			{
				Graphics.Blit(temporary2, temporary, this.sunShaftsMaterial, 1);
				num2 = this.sunShaftBlurRadius * (((float)i * 2f + 1f) * 6f) / 768f;
				this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num2, num2, (float)0, (float)0));
				Graphics.Blit(temporary, temporary2, this.sunShaftsMaterial, 1);
				num2 = this.sunShaftBlurRadius * (((float)i * 2f + 2f) * 6f) / 768f;
				this.sunShaftsMaterial.SetVector("_BlurRadius4", new Vector4(num2, num2, (float)0, (float)0));
			}
			if (vector.z >= (float)0)
			{
				this.sunShaftsMaterial.SetVector("_SunColor", new Vector4(this.sunColor.r, this.sunColor.g, this.sunColor.b, this.sunColor.a) * this.sunShaftIntensity);
			}
			else
			{
				this.sunShaftsMaterial.SetVector("_SunColor", Vector4.zero);
			}
			this.sunShaftsMaterial.SetTexture("_ColorBuffer", temporary2);
			Graphics.Blit(source, destination, this.sunShaftsMaterial, (this.screenBlendMode != ShaftsScreenBlendMode.Screen) ? 4 : 0);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary);
		}
	}

	// Token: 0x06000095 RID: 149 RVA: 0x00009550 File Offset: 0x00007750
	private int ClampBlurIterationsToSomethingThatMakesSense(int its)
	{
		return (its >= 1) ? ((its <= 4) ? its : 4) : 1;
	}

	// Token: 0x06000096 RID: 150 RVA: 0x00009580 File Offset: 0x00007780
	public override void Main()
	{
	}

	// Token: 0x0400017A RID: 378
	public SunShaftsResolution resolution;

	// Token: 0x0400017B RID: 379
	public ShaftsScreenBlendMode screenBlendMode;

	// Token: 0x0400017C RID: 380
	public Transform sunTransform;

	// Token: 0x0400017D RID: 381
	public int radialBlurIterations;

	// Token: 0x0400017E RID: 382
	public Color sunColor;

	// Token: 0x0400017F RID: 383
	public float sunShaftBlurRadius;

	// Token: 0x04000180 RID: 384
	public float sunShaftIntensity;

	// Token: 0x04000181 RID: 385
	public float useSkyBoxAlpha;

	// Token: 0x04000182 RID: 386
	public float maxRadius;

	// Token: 0x04000183 RID: 387
	public bool useDepthTexture;

	// Token: 0x04000184 RID: 388
	public Shader sunShaftsShader;

	// Token: 0x04000185 RID: 389
	private Material sunShaftsMaterial;

	// Token: 0x04000186 RID: 390
	public Shader simpleClearShader;

	// Token: 0x04000187 RID: 391
	private Material simpleClearMaterial;
}
