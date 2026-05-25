using System;
using UnityEngine;

// Token: 0x02000032 RID: 50
[AddComponentMenu("Image Effects/Vignette and Chromatic Aberration")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[Serializable]
public class Vignetting : PostEffectsBase
{
	// Token: 0x060000A9 RID: 169 RVA: 0x0000A420 File Offset: 0x00008620
	public Vignetting()
	{
		this.mode = Vignetting.AberrationMode.Simple;
		this.intensity = 0.375f;
		this.chromaticAberration = 0.2f;
		this.axialAberration = 0.5f;
		this.blurSpread = 0.75f;
		this.luminanceDependency = 0.25f;
	}

	// Token: 0x060000AA RID: 170 RVA: 0x0000A474 File Offset: 0x00008674
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.vignetteMaterial = this.CheckShaderAndCreateMaterial(this.vignetteShader, this.vignetteMaterial);
		this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
		this.chromAberrationMaterial = this.CheckShaderAndCreateMaterial(this.chromAberrationShader, this.chromAberrationMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x060000AB RID: 171 RVA: 0x0000A4E8 File Offset: 0x000086E8
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			bool flag = (Mathf.Abs(this.blur) > (float)0) ?? (Mathf.Abs(this.intensity) > (float)0);
			float num = 1f * (float)source.width / (1f * (float)source.height);
			float num2 = 0.001953125f;
			RenderTexture renderTexture = null;
			RenderTexture renderTexture2 = null;
			RenderTexture renderTexture3 = null;
			if (flag)
			{
				renderTexture = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
				if (Mathf.Abs(this.blur) > (float)0)
				{
					renderTexture2 = RenderTexture.GetTemporary((int)((float)source.width / 2f), (int)((float)source.height / 2f), 0, source.format);
					renderTexture3 = RenderTexture.GetTemporary((int)((float)source.width / 2f), (int)((float)source.height / 2f), 0, source.format);
					Graphics.Blit(source, renderTexture2, this.chromAberrationMaterial, 0);
					for (int i = 0; i < 2; i++)
					{
						this.separableBlurMaterial.SetVector("offsets", new Vector4((float)0, this.blurSpread * num2, (float)0, (float)0));
						Graphics.Blit(renderTexture2, renderTexture3, this.separableBlurMaterial);
						this.separableBlurMaterial.SetVector("offsets", new Vector4(this.blurSpread * num2 / num, (float)0, (float)0, (float)0));
						Graphics.Blit(renderTexture3, renderTexture2, this.separableBlurMaterial);
					}
				}
				this.vignetteMaterial.SetFloat("_Intensity", this.intensity);
				this.vignetteMaterial.SetFloat("_Blur", this.blur);
				this.vignetteMaterial.SetTexture("_VignetteTex", renderTexture2);
				Graphics.Blit(source, renderTexture, this.vignetteMaterial, 0);
			}
			this.chromAberrationMaterial.SetFloat("_ChromaticAberration", this.chromaticAberration);
			this.chromAberrationMaterial.SetFloat("_AxialAberration", this.axialAberration);
			this.chromAberrationMaterial.SetFloat("_Luminance", 1f / (float.Epsilon + this.luminanceDependency));
			if (flag)
			{
				renderTexture.wrapMode = TextureWrapMode.Clamp;
			}
			else
			{
				source.wrapMode = TextureWrapMode.Clamp;
			}
			Graphics.Blit((!flag) ? source : renderTexture, destination, this.chromAberrationMaterial, (this.mode != Vignetting.AberrationMode.Advanced) ? 1 : 2);
			if (renderTexture)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
			if (renderTexture2)
			{
				RenderTexture.ReleaseTemporary(renderTexture2);
			}
			if (renderTexture3)
			{
				RenderTexture.ReleaseTemporary(renderTexture3);
			}
		}
	}

	// Token: 0x060000AC RID: 172 RVA: 0x0000A784 File Offset: 0x00008984
	public override void Main()
	{
	}

	// Token: 0x040001B5 RID: 437
	public Vignetting.AberrationMode mode;

	// Token: 0x040001B6 RID: 438
	public float intensity;

	// Token: 0x040001B7 RID: 439
	public float chromaticAberration;

	// Token: 0x040001B8 RID: 440
	public float axialAberration;

	// Token: 0x040001B9 RID: 441
	public float blur;

	// Token: 0x040001BA RID: 442
	public float blurSpread;

	// Token: 0x040001BB RID: 443
	public float luminanceDependency;

	// Token: 0x040001BC RID: 444
	public Shader vignetteShader;

	// Token: 0x040001BD RID: 445
	private Material vignetteMaterial;

	// Token: 0x040001BE RID: 446
	public Shader separableBlurShader;

	// Token: 0x040001BF RID: 447
	private Material separableBlurMaterial;

	// Token: 0x040001C0 RID: 448
	public Shader chromAberrationShader;

	// Token: 0x040001C1 RID: 449
	private Material chromAberrationMaterial;

	// Token: 0x02000033 RID: 51
	[Serializable]
	public enum AberrationMode
	{
		// Token: 0x040001C3 RID: 451
		Simple,
		// Token: 0x040001C4 RID: 452
		Advanced
	}
}
