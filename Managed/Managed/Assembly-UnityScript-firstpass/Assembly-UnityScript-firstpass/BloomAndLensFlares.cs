using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/BloomAndFlares (3.5, Deprecated)")]
[Serializable]
public class BloomAndLensFlares : PostEffectsBase
{
	// Token: 0x0600000F RID: 15 RVA: 0x00002FC8 File Offset: 0x000011C8
	public BloomAndLensFlares()
	{
		this.screenBlendMode = BloomScreenBlendMode.Add;
		this.hdr = HDRBloomMode.Auto;
		this.sepBlurSpread = 1.5f;
		this.useSrcAlphaAsMask = 0.5f;
		this.bloomIntensity = 1f;
		this.bloomThreshhold = 0.5f;
		this.bloomBlurIterations = 2;
		this.hollywoodFlareBlurIterations = 2;
		this.lensflareMode = LensflareStyle34.Anamorphic;
		this.hollyStretchWidth = 3.5f;
		this.lensflareIntensity = 1f;
		this.lensflareThreshhold = 0.3f;
		this.flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);
		this.flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);
		this.flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);
		this.flareColorD = new Color(0.8f, 0.4f, (float)0, 0.75f);
		this.blurWidth = 1f;
	}

	// Token: 0x06000010 RID: 16 RVA: 0x000030D0 File Offset: 0x000012D0
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.screenBlend = this.CheckShaderAndCreateMaterial(this.screenBlendShader, this.screenBlend);
		this.lensFlareMaterial = this.CheckShaderAndCreateMaterial(this.lensFlareShader, this.lensFlareMaterial);
		this.vignetteMaterial = this.CheckShaderAndCreateMaterial(this.vignetteShader, this.vignetteMaterial);
		this.separableBlurMaterial = this.CheckShaderAndCreateMaterial(this.separableBlurShader, this.separableBlurMaterial);
		this.addBrightStuffBlendOneOneMaterial = this.CheckShaderAndCreateMaterial(this.addBrightStuffOneOneShader, this.addBrightStuffBlendOneOneMaterial);
		this.hollywoodFlaresMaterial = this.CheckShaderAndCreateMaterial(this.hollywoodFlaresShader, this.hollywoodFlaresMaterial);
		this.brightPassFilterMaterial = this.CheckShaderAndCreateMaterial(this.brightPassFilterShader, this.brightPassFilterMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000011 RID: 17 RVA: 0x000031A4 File Offset: 0x000013A4
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.doHdr = false;
			if (this.hdr == HDRBloomMode.Auto)
			{
				bool flag;
				if (flag = source.format == RenderTextureFormat.ARGBHalf)
				{
					flag = this.camera.hdr;
				}
				this.doHdr = flag;
			}
			else
			{
				this.doHdr = this.hdr == HDRBloomMode.On;
			}
			bool supportHDRTextures;
			if (supportHDRTextures = this.doHdr)
			{
				supportHDRTextures = this.supportHDRTextures;
			}
			this.doHdr = supportHDRTextures;
			BloomScreenBlendMode bloomScreenBlendMode = this.screenBlendMode;
			if (this.doHdr)
			{
				bloomScreenBlendMode = BloomScreenBlendMode.Add;
			}
			RenderTextureFormat renderTextureFormat = ((!this.doHdr) ? RenderTextureFormat.Default : RenderTextureFormat.ARGBHalf);
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, renderTextureFormat);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, renderTextureFormat);
			RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, renderTextureFormat);
			RenderTexture temporary4 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, renderTextureFormat);
			float num = 1f * (float)source.width / (1f * (float)source.height);
			float num2 = 0.001953125f;
			Graphics.Blit(source, temporary, this.screenBlend, 2);
			Graphics.Blit(temporary, temporary2, this.screenBlend, 2);
			RenderTexture.ReleaseTemporary(temporary);
			this.BrightFilter(this.bloomThreshhold, this.useSrcAlphaAsMask, temporary2, temporary3);
			temporary2.DiscardContents();
			if (this.bloomBlurIterations < 1)
			{
				this.bloomBlurIterations = 1;
			}
			for (int i = 0; i < this.bloomBlurIterations; i++)
			{
				float num3 = (1f + (float)i * 0.5f) * this.sepBlurSpread;
				this.separableBlurMaterial.SetVector("offsets", new Vector4((float)0, num3 * num2, (float)0, (float)0));
				RenderTexture renderTexture = ((i != 0) ? temporary2 : temporary3);
				Graphics.Blit(renderTexture, temporary4, this.separableBlurMaterial);
				renderTexture.DiscardContents();
				this.separableBlurMaterial.SetVector("offsets", new Vector4(num3 / num * num2, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary4, temporary2, this.separableBlurMaterial);
				temporary4.DiscardContents();
			}
			if (this.lensflares)
			{
				if (this.lensflareMode == LensflareStyle34.Ghosting)
				{
					this.BrightFilter(this.lensflareThreshhold, (float)0, temporary2, temporary4);
					temporary2.DiscardContents();
					this.Vignette(0.975f, temporary4, temporary3);
					temporary4.DiscardContents();
					this.BlendFlares(temporary3, temporary2);
					temporary3.DiscardContents();
				}
				else
				{
					this.hollywoodFlaresMaterial.SetVector("_Threshhold", new Vector4(this.lensflareThreshhold, 1f / (1f - this.lensflareThreshhold), (float)0, (float)0));
					this.hollywoodFlaresMaterial.SetVector("tintColor", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.flareColorA.a * this.lensflareIntensity);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 2);
					temporary4.DiscardContents();
					Graphics.Blit(temporary3, temporary4, this.hollywoodFlaresMaterial, 3);
					temporary3.DiscardContents();
					this.hollywoodFlaresMaterial.SetVector("offsets", new Vector4(this.sepBlurSpread * 1f / num * num2, (float)0, (float)0, (float)0));
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 1);
					temporary4.DiscardContents();
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth * 2f);
					Graphics.Blit(temporary3, temporary4, this.hollywoodFlaresMaterial, 1);
					temporary3.DiscardContents();
					this.hollywoodFlaresMaterial.SetFloat("stretchWidth", this.hollyStretchWidth * 4f);
					Graphics.Blit(temporary4, temporary3, this.hollywoodFlaresMaterial, 1);
					temporary4.DiscardContents();
					if (this.lensflareMode == LensflareStyle34.Anamorphic)
					{
						for (int j = 0; j < this.hollywoodFlareBlurIterations; j++)
						{
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary3, temporary4, this.separableBlurMaterial);
							temporary3.DiscardContents();
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary4, temporary3, this.separableBlurMaterial);
							temporary4.DiscardContents();
						}
						this.AddTo(1f, temporary3, temporary2);
						temporary3.DiscardContents();
					}
					else
					{
						for (int k = 0; k < this.hollywoodFlareBlurIterations; k++)
						{
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary3, temporary4, this.separableBlurMaterial);
							temporary3.DiscardContents();
							this.separableBlurMaterial.SetVector("offsets", new Vector4(this.hollyStretchWidth * 2f / num * num2, (float)0, (float)0, (float)0));
							Graphics.Blit(temporary4, temporary3, this.separableBlurMaterial);
							temporary4.DiscardContents();
						}
						this.Vignette(1f, temporary3, temporary4);
						temporary3.DiscardContents();
						this.BlendFlares(temporary4, temporary3);
						temporary4.DiscardContents();
						this.AddTo(1f, temporary3, temporary2);
						temporary3.DiscardContents();
					}
				}
			}
			this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
			this.screenBlend.SetTexture("_ColorBuffer", source);
			Graphics.Blit(temporary2, destination, this.screenBlend, (int)bloomScreenBlendMode);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
			RenderTexture.ReleaseTemporary(temporary4);
		}
	}

	// Token: 0x06000012 RID: 18 RVA: 0x00003798 File Offset: 0x00001998
	private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
	{
		this.addBrightStuffBlendOneOneMaterial.SetFloat("_Intensity", intensity_);
		Graphics.Blit(from, to, this.addBrightStuffBlendOneOneMaterial);
	}

	// Token: 0x06000013 RID: 19 RVA: 0x000037B8 File Offset: 0x000019B8
	private void BlendFlares(RenderTexture from, RenderTexture to)
	{
		this.lensFlareMaterial.SetVector("colorA", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorB", new Vector4(this.flareColorB.r, this.flareColorB.g, this.flareColorB.b, this.flareColorB.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorC", new Vector4(this.flareColorC.r, this.flareColorC.g, this.flareColorC.b, this.flareColorC.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorD", new Vector4(this.flareColorD.r, this.flareColorD.g, this.flareColorD.b, this.flareColorD.a) * this.lensflareIntensity);
		Graphics.Blit(from, to, this.lensFlareMaterial);
	}

	// Token: 0x06000014 RID: 20 RVA: 0x00003904 File Offset: 0x00001B04
	private void BrightFilter(float thresh, float useAlphaAsMask, RenderTexture from, RenderTexture to)
	{
		if (this.doHdr)
		{
			this.brightPassFilterMaterial.SetVector("threshhold", new Vector4(thresh, 1f, (float)0, (float)0));
		}
		else
		{
			this.brightPassFilterMaterial.SetVector("threshhold", new Vector4(thresh, 1f / (1f - thresh), (float)0, (float)0));
		}
		this.brightPassFilterMaterial.SetFloat("useSrcAlphaAsMask", useAlphaAsMask);
		Graphics.Blit(from, to, this.brightPassFilterMaterial);
	}

	// Token: 0x06000015 RID: 21 RVA: 0x0000398C File Offset: 0x00001B8C
	private void Vignette(float amount, RenderTexture from, RenderTexture to)
	{
		if (this.lensFlareVignetteMask)
		{
			this.screenBlend.SetTexture("_ColorBuffer", this.lensFlareVignetteMask);
			Graphics.Blit(from, to, this.screenBlend, 3);
		}
		else
		{
			this.vignetteMaterial.SetFloat("vignetteIntensity", amount);
			Graphics.Blit(from, to, this.vignetteMaterial);
		}
	}

	// Token: 0x06000016 RID: 22 RVA: 0x000039F0 File Offset: 0x00001BF0
	public override void Main()
	{
	}

	// Token: 0x0400005D RID: 93
	public TweakMode34 tweakMode;

	// Token: 0x0400005E RID: 94
	public BloomScreenBlendMode screenBlendMode;

	// Token: 0x0400005F RID: 95
	public HDRBloomMode hdr;

	// Token: 0x04000060 RID: 96
	private bool doHdr;

	// Token: 0x04000061 RID: 97
	public float sepBlurSpread;

	// Token: 0x04000062 RID: 98
	public float useSrcAlphaAsMask;

	// Token: 0x04000063 RID: 99
	public float bloomIntensity;

	// Token: 0x04000064 RID: 100
	public float bloomThreshhold;

	// Token: 0x04000065 RID: 101
	public int bloomBlurIterations;

	// Token: 0x04000066 RID: 102
	public bool lensflares;

	// Token: 0x04000067 RID: 103
	public int hollywoodFlareBlurIterations;

	// Token: 0x04000068 RID: 104
	public LensflareStyle34 lensflareMode;

	// Token: 0x04000069 RID: 105
	public float hollyStretchWidth;

	// Token: 0x0400006A RID: 106
	public float lensflareIntensity;

	// Token: 0x0400006B RID: 107
	public float lensflareThreshhold;

	// Token: 0x0400006C RID: 108
	public Color flareColorA;

	// Token: 0x0400006D RID: 109
	public Color flareColorB;

	// Token: 0x0400006E RID: 110
	public Color flareColorC;

	// Token: 0x0400006F RID: 111
	public Color flareColorD;

	// Token: 0x04000070 RID: 112
	public float blurWidth;

	// Token: 0x04000071 RID: 113
	public Texture2D lensFlareVignetteMask;

	// Token: 0x04000072 RID: 114
	public Shader lensFlareShader;

	// Token: 0x04000073 RID: 115
	private Material lensFlareMaterial;

	// Token: 0x04000074 RID: 116
	public Shader vignetteShader;

	// Token: 0x04000075 RID: 117
	private Material vignetteMaterial;

	// Token: 0x04000076 RID: 118
	public Shader separableBlurShader;

	// Token: 0x04000077 RID: 119
	private Material separableBlurMaterial;

	// Token: 0x04000078 RID: 120
	public Shader addBrightStuffOneOneShader;

	// Token: 0x04000079 RID: 121
	private Material addBrightStuffBlendOneOneMaterial;

	// Token: 0x0400007A RID: 122
	public Shader screenBlendShader;

	// Token: 0x0400007B RID: 123
	private Material screenBlend;

	// Token: 0x0400007C RID: 124
	public Shader hollywoodFlaresShader;

	// Token: 0x0400007D RID: 125
	private Material hollywoodFlaresMaterial;

	// Token: 0x0400007E RID: 126
	public Shader brightPassFilterShader;

	// Token: 0x0400007F RID: 127
	private Material brightPassFilterMaterial;
}
