using System;
using UnityEngine;

// Token: 0x02000004 RID: 4
[AddComponentMenu("Image Effects/Bloom (4.0, HDR, Lens Flares)")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[Serializable]
public class Bloom : PostEffectsBase
{
	// Token: 0x06000006 RID: 6 RVA: 0x00002510 File Offset: 0x00000710
	public Bloom()
	{
		this.screenBlendMode = Bloom.BloomScreenBlendMode.Add;
		this.hdr = Bloom.HDRBloomMode.Auto;
		this.sepBlurSpread = 2.5f;
		this.quality = Bloom.BloomQuality.High;
		this.bloomIntensity = 0.5f;
		this.bloomThreshhold = 0.5f;
		this.bloomThreshholdColor = Color.white;
		this.bloomBlurIterations = 2;
		this.hollywoodFlareBlurIterations = 2;
		this.lensflareMode = Bloom.LensFlareStyle.Anamorphic;
		this.hollyStretchWidth = 2.5f;
		this.lensflareThreshhold = 0.3f;
		this.lensFlareSaturation = 0.75f;
		this.flareColorA = new Color(0.4f, 0.4f, 0.8f, 0.75f);
		this.flareColorB = new Color(0.4f, 0.8f, 0.8f, 0.75f);
		this.flareColorC = new Color(0.8f, 0.4f, 0.8f, 0.75f);
		this.flareColorD = new Color(0.8f, 0.4f, (float)0, 0.75f);
		this.blurWidth = 1f;
	}

	// Token: 0x06000007 RID: 7 RVA: 0x00002620 File Offset: 0x00000820
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.screenBlend = this.CheckShaderAndCreateMaterial(this.screenBlendShader, this.screenBlend);
		this.lensFlareMaterial = this.CheckShaderAndCreateMaterial(this.lensFlareShader, this.lensFlareMaterial);
		this.blurAndFlaresMaterial = this.CheckShaderAndCreateMaterial(this.blurAndFlaresShader, this.blurAndFlaresMaterial);
		this.brightPassFilterMaterial = this.CheckShaderAndCreateMaterial(this.brightPassFilterShader, this.brightPassFilterMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000008 RID: 8 RVA: 0x000026AC File Offset: 0x000008AC
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.doHdr = false;
			if (this.hdr == Bloom.HDRBloomMode.Auto)
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
				this.doHdr = this.hdr == Bloom.HDRBloomMode.On;
			}
			bool supportHDRTextures;
			if (supportHDRTextures = this.doHdr)
			{
				supportHDRTextures = this.supportHDRTextures;
			}
			this.doHdr = supportHDRTextures;
			Bloom.BloomScreenBlendMode bloomScreenBlendMode = this.screenBlendMode;
			if (this.doHdr)
			{
				bloomScreenBlendMode = Bloom.BloomScreenBlendMode.Add;
			}
			RenderTextureFormat renderTextureFormat = ((!this.doHdr) ? RenderTextureFormat.Default : RenderTextureFormat.ARGBHalf);
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / 2, source.height / 2, 0, renderTextureFormat);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, renderTextureFormat);
			RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, renderTextureFormat);
			RenderTexture temporary4 = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0, renderTextureFormat);
			float num = 1f * (float)source.width / (1f * (float)source.height);
			float num2 = 0.001953125f;
			if (this.quality > Bloom.BloomQuality.Cheap)
			{
				Graphics.Blit(source, temporary, this.screenBlend, 2);
				Graphics.Blit(temporary, temporary3, this.screenBlend, 2);
				Graphics.Blit(temporary3, temporary2, this.screenBlend, 6);
			}
			else
			{
				Graphics.Blit(source, temporary);
				Graphics.Blit(temporary, temporary2, this.screenBlend, 6);
			}
			this.BrightFilter(this.bloomThreshhold * this.bloomThreshholdColor, temporary2, temporary3);
			if (this.bloomBlurIterations < 1)
			{
				this.bloomBlurIterations = 1;
			}
			else if (this.bloomBlurIterations > 10)
			{
				this.bloomBlurIterations = 10;
			}
			for (int i = 0; i < this.bloomBlurIterations; i++)
			{
				float num3 = (1f + (float)i * 0.25f) * this.sepBlurSpread;
				this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4((float)0, num3 * num2, (float)0, (float)0));
				Graphics.Blit(temporary3, temporary4, this.blurAndFlaresMaterial, 4);
				if (this.quality > Bloom.BloomQuality.Cheap)
				{
					this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num3 / num * num2, (float)0, (float)0, (float)0));
					Graphics.Blit(temporary4, temporary3, this.blurAndFlaresMaterial, 4);
					if (i == 0)
					{
						Graphics.Blit(temporary3, temporary2);
					}
					else
					{
						Graphics.Blit(temporary3, temporary2, this.screenBlend, 10);
					}
				}
				else
				{
					this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num3 / num * num2, (float)0, (float)0, (float)0));
					Graphics.Blit(temporary4, temporary3, this.blurAndFlaresMaterial, 4);
				}
			}
			if (this.quality > Bloom.BloomQuality.Cheap)
			{
				Graphics.Blit(temporary2, temporary3, this.screenBlend, 6);
			}
			if (this.lensflareIntensity > 1E-45f)
			{
				if (this.lensflareMode == Bloom.LensFlareStyle.Ghosting)
				{
					this.BrightFilter(this.lensflareThreshhold, temporary3, temporary4);
					if (this.quality > Bloom.BloomQuality.Cheap)
					{
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4((float)0, 1.5f / (1f * (float)temporary2.height), (float)0, (float)0));
						Graphics.Blit(temporary4, temporary2, this.blurAndFlaresMaterial, 4);
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(1.5f / (1f * (float)temporary2.width), (float)0, (float)0, (float)0));
						Graphics.Blit(temporary2, temporary4, this.blurAndFlaresMaterial, 4);
					}
					this.Vignette(0.975f, temporary4, temporary4);
					this.BlendFlares(temporary4, temporary3);
				}
				else
				{
					float num4 = 1f * Mathf.Cos(this.flareRotation);
					float num5 = 1f * Mathf.Sin(this.flareRotation);
					float num6 = this.hollyStretchWidth * 1f / num * num2;
					float num7 = this.hollyStretchWidth * num2;
					this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num4, num5, (float)0, (float)0));
					this.blurAndFlaresMaterial.SetVector("_Threshhold", new Vector4(this.lensflareThreshhold, 1f, (float)0, (float)0));
					this.blurAndFlaresMaterial.SetVector("_TintColor", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.flareColorA.a * this.lensflareIntensity);
					this.blurAndFlaresMaterial.SetFloat("_Saturation", this.lensFlareSaturation);
					Graphics.Blit(temporary4, temporary2, this.blurAndFlaresMaterial, 2);
					Graphics.Blit(temporary2, temporary4, this.blurAndFlaresMaterial, 3);
					this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num4 * num6, num5 * num6, (float)0, (float)0));
					this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth);
					Graphics.Blit(temporary4, temporary2, this.blurAndFlaresMaterial, 1);
					this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth * 2f);
					Graphics.Blit(temporary2, temporary4, this.blurAndFlaresMaterial, 1);
					this.blurAndFlaresMaterial.SetFloat("_StretchWidth", this.hollyStretchWidth * 4f);
					Graphics.Blit(temporary4, temporary2, this.blurAndFlaresMaterial, 1);
					for (int i = 0; i < this.hollywoodFlareBlurIterations; i++)
					{
						num6 = this.hollyStretchWidth * 2f / num * num2;
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num6 * num4, num6 * num5, (float)0, (float)0));
						Graphics.Blit(temporary2, temporary4, this.blurAndFlaresMaterial, 4);
						this.blurAndFlaresMaterial.SetVector("_Offsets", new Vector4(num6 * num4, num6 * num5, (float)0, (float)0));
						Graphics.Blit(temporary4, temporary2, this.blurAndFlaresMaterial, 4);
					}
					if (this.lensflareMode == Bloom.LensFlareStyle.Anamorphic)
					{
						this.AddTo(1f, temporary2, temporary3);
					}
					else
					{
						this.Vignette(1f, temporary2, temporary4);
						this.BlendFlares(temporary4, temporary2);
						this.AddTo(1f, temporary2, temporary3);
					}
				}
			}
			int num8 = (int)bloomScreenBlendMode;
			this.screenBlend.SetFloat("_Intensity", this.bloomIntensity);
			this.screenBlend.SetTexture("_ColorBuffer", source);
			if (this.quality > Bloom.BloomQuality.Cheap)
			{
				Graphics.Blit(temporary3, temporary);
				Graphics.Blit(temporary, destination, this.screenBlend, num8);
			}
			else
			{
				Graphics.Blit(temporary3, destination, this.screenBlend, num8);
			}
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
			RenderTexture.ReleaseTemporary(temporary4);
		}
	}

	// Token: 0x06000009 RID: 9 RVA: 0x00002D68 File Offset: 0x00000F68
	private void AddTo(float intensity_, RenderTexture from, RenderTexture to)
	{
		this.screenBlend.SetFloat("_Intensity", intensity_);
		Graphics.Blit(from, to, this.screenBlend, 9);
	}

	// Token: 0x0600000A RID: 10 RVA: 0x00002D98 File Offset: 0x00000F98
	private void BlendFlares(RenderTexture from, RenderTexture to)
	{
		this.lensFlareMaterial.SetVector("colorA", new Vector4(this.flareColorA.r, this.flareColorA.g, this.flareColorA.b, this.flareColorA.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorB", new Vector4(this.flareColorB.r, this.flareColorB.g, this.flareColorB.b, this.flareColorB.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorC", new Vector4(this.flareColorC.r, this.flareColorC.g, this.flareColorC.b, this.flareColorC.a) * this.lensflareIntensity);
		this.lensFlareMaterial.SetVector("colorD", new Vector4(this.flareColorD.r, this.flareColorD.g, this.flareColorD.b, this.flareColorD.a) * this.lensflareIntensity);
		Graphics.Blit(from, to, this.lensFlareMaterial);
	}

	// Token: 0x0600000B RID: 11 RVA: 0x00002EE4 File Offset: 0x000010E4
	private void BrightFilter(float thresh, RenderTexture from, RenderTexture to)
	{
		this.brightPassFilterMaterial.SetVector("_Threshhold", new Vector4(thresh, thresh, thresh, thresh));
		Graphics.Blit(from, to, this.brightPassFilterMaterial, 0);
	}

	// Token: 0x0600000C RID: 12 RVA: 0x00002F10 File Offset: 0x00001110
	private void BrightFilter(Color threshColor, RenderTexture from, RenderTexture to)
	{
		this.brightPassFilterMaterial.SetVector("_Threshhold", threshColor);
		Graphics.Blit(from, to, this.brightPassFilterMaterial, 1);
	}

	// Token: 0x0600000D RID: 13 RVA: 0x00002F44 File Offset: 0x00001144
	private void Vignette(float amount, RenderTexture from, RenderTexture to)
	{
		if (this.lensFlareVignetteMask)
		{
			this.screenBlend.SetTexture("_ColorBuffer", this.lensFlareVignetteMask);
			Graphics.Blit((!(from == to)) ? from : null, to, this.screenBlend, (!(from == to)) ? 3 : 7);
		}
		else if (from != to)
		{
			Graphics.Blit(from, to);
		}
	}

	// Token: 0x0600000E RID: 14 RVA: 0x00002FC4 File Offset: 0x000011C4
	public override void Main()
	{
	}

	// Token: 0x0400001F RID: 31
	public Bloom.TweakMode tweakMode;

	// Token: 0x04000020 RID: 32
	public Bloom.BloomScreenBlendMode screenBlendMode;

	// Token: 0x04000021 RID: 33
	public Bloom.HDRBloomMode hdr;

	// Token: 0x04000022 RID: 34
	private bool doHdr;

	// Token: 0x04000023 RID: 35
	public float sepBlurSpread;

	// Token: 0x04000024 RID: 36
	public Bloom.BloomQuality quality;

	// Token: 0x04000025 RID: 37
	public float bloomIntensity;

	// Token: 0x04000026 RID: 38
	public float bloomThreshhold;

	// Token: 0x04000027 RID: 39
	public Color bloomThreshholdColor;

	// Token: 0x04000028 RID: 40
	public int bloomBlurIterations;

	// Token: 0x04000029 RID: 41
	public int hollywoodFlareBlurIterations;

	// Token: 0x0400002A RID: 42
	public float flareRotation;

	// Token: 0x0400002B RID: 43
	public Bloom.LensFlareStyle lensflareMode;

	// Token: 0x0400002C RID: 44
	public float hollyStretchWidth;

	// Token: 0x0400002D RID: 45
	public float lensflareIntensity;

	// Token: 0x0400002E RID: 46
	public float lensflareThreshhold;

	// Token: 0x0400002F RID: 47
	public float lensFlareSaturation;

	// Token: 0x04000030 RID: 48
	public Color flareColorA;

	// Token: 0x04000031 RID: 49
	public Color flareColorB;

	// Token: 0x04000032 RID: 50
	public Color flareColorC;

	// Token: 0x04000033 RID: 51
	public Color flareColorD;

	// Token: 0x04000034 RID: 52
	public float blurWidth;

	// Token: 0x04000035 RID: 53
	public Texture2D lensFlareVignetteMask;

	// Token: 0x04000036 RID: 54
	public Shader lensFlareShader;

	// Token: 0x04000037 RID: 55
	private Material lensFlareMaterial;

	// Token: 0x04000038 RID: 56
	public Shader screenBlendShader;

	// Token: 0x04000039 RID: 57
	private Material screenBlend;

	// Token: 0x0400003A RID: 58
	public Shader blurAndFlaresShader;

	// Token: 0x0400003B RID: 59
	private Material blurAndFlaresMaterial;

	// Token: 0x0400003C RID: 60
	public Shader brightPassFilterShader;

	// Token: 0x0400003D RID: 61
	private Material brightPassFilterMaterial;

	// Token: 0x02000005 RID: 5
	[Serializable]
	public enum LensFlareStyle
	{
		// Token: 0x0400003F RID: 63
		Ghosting,
		// Token: 0x04000040 RID: 64
		Anamorphic,
		// Token: 0x04000041 RID: 65
		Combined
	}

	// Token: 0x02000006 RID: 6
	[Serializable]
	public enum TweakMode
	{
		// Token: 0x04000043 RID: 67
		Basic,
		// Token: 0x04000044 RID: 68
		Complex
	}

	// Token: 0x02000007 RID: 7
	[Serializable]
	public enum HDRBloomMode
	{
		// Token: 0x04000046 RID: 70
		Auto,
		// Token: 0x04000047 RID: 71
		On,
		// Token: 0x04000048 RID: 72
		Off
	}

	// Token: 0x02000008 RID: 8
	[Serializable]
	public enum BloomScreenBlendMode
	{
		// Token: 0x0400004A RID: 74
		Screen,
		// Token: 0x0400004B RID: 75
		Add
	}

	// Token: 0x02000009 RID: 9
	[Serializable]
	public enum BloomQuality
	{
		// Token: 0x0400004D RID: 77
		Cheap,
		// Token: 0x0400004E RID: 78
		High
	}
}
