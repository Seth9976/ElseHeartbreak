using System;
using UnityEngine;

// Token: 0x0200001B RID: 27
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Depth of Field (3.4)")]
[Serializable]
public class DepthOfField34 : PostEffectsBase
{
	// Token: 0x0600003F RID: 63 RVA: 0x000053C0 File Offset: 0x000035C0
	public DepthOfField34()
	{
		this.quality = Dof34QualitySetting.OnlyBackground;
		this.resolution = DofResolution.Low;
		this.simpleTweakMode = true;
		this.focalPoint = 1f;
		this.smoothness = 0.5f;
		this.focalZStartCurve = 1f;
		this.focalZEndCurve = 1f;
		this.focalStartCurve = 2f;
		this.focalEndCurve = 2f;
		this.focalDistance01 = 0.1f;
		this.bluriness = DofBlurriness.High;
		this.maxBlurSpread = 1.75f;
		this.foregroundBlurExtrude = 1.15f;
		this.bokehDestination = BokehDestination.Background;
		this.widthOverHeight = 1.25f;
		this.oneOverBaseSize = 0.001953125f;
		this.bokehSupport = true;
		this.bokehScale = 2.4f;
		this.bokehIntensity = 0.15f;
		this.bokehThreshholdContrast = 0.1f;
		this.bokehThreshholdLuminance = 0.55f;
		this.bokehDownsample = 1;
	}

	// Token: 0x06000041 RID: 65 RVA: 0x000054C0 File Offset: 0x000036C0
	public virtual void CreateMaterials()
	{
		this.dofBlurMaterial = this.CheckShaderAndCreateMaterial(this.dofBlurShader, this.dofBlurMaterial);
		this.dofMaterial = this.CheckShaderAndCreateMaterial(this.dofShader, this.dofMaterial);
		this.bokehSupport = this.bokehShader.isSupported;
		if (this.bokeh && this.bokehSupport && this.bokehShader)
		{
			this.bokehMaterial = this.CheckShaderAndCreateMaterial(this.bokehShader, this.bokehMaterial);
		}
	}

	// Token: 0x06000042 RID: 66 RVA: 0x0000554C File Offset: 0x0000374C
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.dofBlurMaterial = this.CheckShaderAndCreateMaterial(this.dofBlurShader, this.dofBlurMaterial);
		this.dofMaterial = this.CheckShaderAndCreateMaterial(this.dofShader, this.dofMaterial);
		this.bokehSupport = this.bokehShader.isSupported;
		if (this.bokeh && this.bokehSupport && this.bokehShader)
		{
			this.bokehMaterial = this.CheckShaderAndCreateMaterial(this.bokehShader, this.bokehMaterial);
		}
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000043 RID: 67 RVA: 0x000055F8 File Offset: 0x000037F8
	public virtual void OnDisable()
	{
		Quads.Cleanup();
	}

	// Token: 0x06000044 RID: 68 RVA: 0x00005600 File Offset: 0x00003800
	public override void OnEnable()
	{
		this.camera.depthTextureMode = this.camera.depthTextureMode | DepthTextureMode.Depth;
	}

	// Token: 0x06000045 RID: 69 RVA: 0x00005628 File Offset: 0x00003828
	public virtual float FocalDistance01(float worldDist)
	{
		return this.camera.WorldToViewportPoint((worldDist - this.camera.nearClipPlane) * this.camera.transform.forward + this.camera.transform.position).z / (this.camera.farClipPlane - this.camera.nearClipPlane);
	}

	// Token: 0x06000046 RID: 70 RVA: 0x00005698 File Offset: 0x00003898
	public virtual int GetDividerBasedOnQuality()
	{
		int num = 1;
		if (this.resolution == DofResolution.Medium)
		{
			num = 2;
		}
		else if (this.resolution == DofResolution.Low)
		{
			num = 2;
		}
		return num;
	}

	// Token: 0x06000047 RID: 71 RVA: 0x000056CC File Offset: 0x000038CC
	public virtual int GetLowResolutionDividerBasedOnQuality(int baseDivider)
	{
		int num = baseDivider;
		if (this.resolution == DofResolution.High)
		{
			num *= 2;
		}
		if (this.resolution == DofResolution.Low)
		{
			num *= 2;
		}
		return num;
	}

	// Token: 0x06000048 RID: 72 RVA: 0x000056FC File Offset: 0x000038FC
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.smoothness < 0.1f)
			{
				this.smoothness = 0.1f;
			}
			bool flag;
			if (flag = this.bokeh)
			{
				flag = this.bokehSupport;
			}
			this.bokeh = flag;
			float num = ((!this.bokeh) ? 1f : DepthOfField34.BOKEH_EXTRA_BLUR);
			bool flag2 = this.quality > Dof34QualitySetting.OnlyBackground;
			float num2 = this.focalSize / (this.camera.farClipPlane - this.camera.nearClipPlane);
			if (this.simpleTweakMode)
			{
				this.focalDistance01 = ((!this.objectFocus) ? this.FocalDistance01(this.focalPoint) : (this.camera.WorldToViewportPoint(this.objectFocus.position).z / this.camera.farClipPlane));
				this.focalStartCurve = this.focalDistance01 * this.smoothness;
				this.focalEndCurve = this.focalStartCurve;
				bool flag3;
				if (flag3 = flag2)
				{
					flag3 = this.focalPoint > this.camera.nearClipPlane + float.Epsilon;
				}
				flag2 = flag3;
			}
			else
			{
				if (this.objectFocus)
				{
					Vector3 vector = this.camera.WorldToViewportPoint(this.objectFocus.position);
					vector.z /= this.camera.farClipPlane;
					this.focalDistance01 = vector.z;
				}
				else
				{
					this.focalDistance01 = this.FocalDistance01(this.focalZDistance);
				}
				this.focalStartCurve = this.focalZStartCurve;
				this.focalEndCurve = this.focalZEndCurve;
				bool flag4;
				if (flag4 = flag2)
				{
					flag4 = this.focalPoint > this.camera.nearClipPlane + float.Epsilon;
				}
				flag2 = flag4;
			}
			this.widthOverHeight = 1f * (float)source.width / (1f * (float)source.height);
			this.oneOverBaseSize = 0.001953125f;
			this.dofMaterial.SetFloat("_ForegroundBlurExtrude", this.foregroundBlurExtrude);
			this.dofMaterial.SetVector("_CurveParams", new Vector4((!this.simpleTweakMode) ? this.focalStartCurve : (1f / this.focalStartCurve), (!this.simpleTweakMode) ? this.focalEndCurve : (1f / this.focalEndCurve), num2 * 0.5f, this.focalDistance01));
			this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)source.width), 1f / (1f * (float)source.height), (float)0, (float)0));
			int dividerBasedOnQuality = this.GetDividerBasedOnQuality();
			int lowResolutionDividerBasedOnQuality = this.GetLowResolutionDividerBasedOnQuality(dividerBasedOnQuality);
			this.AllocateTextures(flag2, source, dividerBasedOnQuality, lowResolutionDividerBasedOnQuality);
			Graphics.Blit(source, source, this.dofMaterial, 3);
			this.Downsample(source, this.mediumRezWorkTexture);
			this.Blur(this.mediumRezWorkTexture, this.mediumRezWorkTexture, DofBlurriness.Low, 4, this.maxBlurSpread);
			if (this.bokeh && (this.bokehDestination & BokehDestination.Background) != (BokehDestination)0)
			{
				this.dofMaterial.SetVector("_Threshhold", new Vector4(this.bokehThreshholdContrast, this.bokehThreshholdLuminance, 0.95f, (float)0));
				Graphics.Blit(this.mediumRezWorkTexture, this.bokehSource2, this.dofMaterial, 11);
				Graphics.Blit(this.mediumRezWorkTexture, this.lowRezWorkTexture);
				this.Blur(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 0, this.maxBlurSpread * num);
			}
			else
			{
				this.Downsample(this.mediumRezWorkTexture, this.lowRezWorkTexture);
				this.Blur(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 0, this.maxBlurSpread);
			}
			this.dofBlurMaterial.SetTexture("_TapLow", this.lowRezWorkTexture);
			this.dofBlurMaterial.SetTexture("_TapMedium", this.mediumRezWorkTexture);
			Graphics.Blit(null, this.finalDefocus, this.dofBlurMaterial, 3);
			if (this.bokeh && (this.bokehDestination & BokehDestination.Background) != (BokehDestination)0)
			{
				this.AddBokeh(this.bokehSource2, this.bokehSource, this.finalDefocus);
			}
			this.dofMaterial.SetTexture("_TapLowBackground", this.finalDefocus);
			this.dofMaterial.SetTexture("_TapMedium", this.mediumRezWorkTexture);
			Graphics.Blit(source, (!flag2) ? destination : this.foregroundTexture, this.dofMaterial, (!this.visualize) ? 0 : 2);
			if (flag2)
			{
				Graphics.Blit(this.foregroundTexture, source, this.dofMaterial, 5);
				this.Downsample(source, this.mediumRezWorkTexture);
				this.BlurFg(this.mediumRezWorkTexture, this.mediumRezWorkTexture, DofBlurriness.Low, 2, this.maxBlurSpread);
				if (this.bokeh && (this.bokehDestination & BokehDestination.Foreground) != (BokehDestination)0)
				{
					this.dofMaterial.SetVector("_Threshhold", new Vector4(this.bokehThreshholdContrast * 0.5f, this.bokehThreshholdLuminance, (float)0, (float)0));
					Graphics.Blit(this.mediumRezWorkTexture, this.bokehSource2, this.dofMaterial, 11);
					Graphics.Blit(this.mediumRezWorkTexture, this.lowRezWorkTexture);
					this.BlurFg(this.lowRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 1, this.maxBlurSpread * num);
				}
				else
				{
					this.BlurFg(this.mediumRezWorkTexture, this.lowRezWorkTexture, this.bluriness, 1, this.maxBlurSpread);
				}
				Graphics.Blit(this.lowRezWorkTexture, this.finalDefocus);
				this.dofMaterial.SetTexture("_TapLowForeground", this.finalDefocus);
				Graphics.Blit(source, destination, this.dofMaterial, (!this.visualize) ? 4 : 1);
				if (this.bokeh && (this.bokehDestination & BokehDestination.Foreground) != (BokehDestination)0)
				{
					this.AddBokeh(this.bokehSource2, this.bokehSource, destination);
				}
			}
			this.ReleaseTextures();
		}
	}

	// Token: 0x06000049 RID: 73 RVA: 0x00005D0C File Offset: 0x00003F0C
	public virtual void Blur(RenderTexture from, RenderTexture to, DofBlurriness iterations, int blurPass, float spread)
	{
		RenderTexture temporary = RenderTexture.GetTemporary(to.width, to.height);
		if (iterations > DofBlurriness.Low)
		{
			this.BlurHex(from, to, blurPass, spread, temporary);
			if (iterations > DofBlurriness.High)
			{
				this.dofBlurMaterial.SetVector("offsets", new Vector4((float)0, spread * this.oneOverBaseSize, (float)0, (float)0));
				Graphics.Blit(to, temporary, this.dofBlurMaterial, blurPass);
				this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
			}
		}
		else
		{
			this.dofBlurMaterial.SetVector("offsets", new Vector4((float)0, spread * this.oneOverBaseSize, (float)0, (float)0));
			Graphics.Blit(from, temporary, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (float)0, (float)0, (float)0));
			Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x0600004A RID: 74 RVA: 0x00005E4C File Offset: 0x0000404C
	public virtual void BlurFg(RenderTexture from, RenderTexture to, DofBlurriness iterations, int blurPass, float spread)
	{
		this.dofBlurMaterial.SetTexture("_TapHigh", from);
		RenderTexture temporary = RenderTexture.GetTemporary(to.width, to.height);
		if (iterations > DofBlurriness.Low)
		{
			this.BlurHex(from, to, blurPass, spread, temporary);
			if (iterations > DofBlurriness.High)
			{
				this.dofBlurMaterial.SetVector("offsets", new Vector4((float)0, spread * this.oneOverBaseSize, (float)0, (float)0));
				Graphics.Blit(to, temporary, this.dofBlurMaterial, blurPass);
				this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
			}
		}
		else
		{
			this.dofBlurMaterial.SetVector("offsets", new Vector4((float)0, spread * this.oneOverBaseSize, (float)0, (float)0));
			Graphics.Blit(from, temporary, this.dofBlurMaterial, blurPass);
			this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (float)0, (float)0, (float)0));
			Graphics.Blit(temporary, to, this.dofBlurMaterial, blurPass);
		}
		RenderTexture.ReleaseTemporary(temporary);
	}

	// Token: 0x0600004B RID: 75 RVA: 0x00005F9C File Offset: 0x0000419C
	public virtual void BlurHex(RenderTexture from, RenderTexture to, int blurPass, float spread, RenderTexture tmp)
	{
		this.dofBlurMaterial.SetVector("offsets", new Vector4((float)0, spread * this.oneOverBaseSize, (float)0, (float)0));
		Graphics.Blit(from, tmp, this.dofBlurMaterial, blurPass);
		this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, (float)0, (float)0, (float)0));
		Graphics.Blit(tmp, to, this.dofBlurMaterial, blurPass);
		this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, spread * this.oneOverBaseSize, (float)0, (float)0));
		Graphics.Blit(to, tmp, this.dofBlurMaterial, blurPass);
		this.dofBlurMaterial.SetVector("offsets", new Vector4(spread / this.widthOverHeight * this.oneOverBaseSize, -spread * this.oneOverBaseSize, (float)0, (float)0));
		Graphics.Blit(tmp, to, this.dofBlurMaterial, blurPass);
	}

	// Token: 0x0600004C RID: 76 RVA: 0x000060B8 File Offset: 0x000042B8
	public virtual void Downsample(RenderTexture from, RenderTexture to)
	{
		this.dofMaterial.SetVector("_InvRenderTargetSize", new Vector4(1f / (1f * (float)to.width), 1f / (1f * (float)to.height), (float)0, (float)0));
		Graphics.Blit(from, to, this.dofMaterial, DepthOfField34.SMOOTH_DOWNSAMPLE_PASS);
	}

	// Token: 0x0600004D RID: 77 RVA: 0x00006118 File Offset: 0x00004318
	public virtual void AddBokeh(RenderTexture bokehInfo, RenderTexture tempTex, RenderTexture finalTarget)
	{
		if (this.bokehMaterial)
		{
			Mesh[] meshes = Quads.GetMeshes(tempTex.width, tempTex.height);
			RenderTexture.active = tempTex;
			GL.Clear(false, true, new Color((float)0, (float)0, (float)0, (float)0));
			GL.PushMatrix();
			GL.LoadIdentity();
			bokehInfo.filterMode = FilterMode.Point;
			float num = (float)bokehInfo.width * 1f / ((float)bokehInfo.height * 1f);
			float num2 = 2f / (1f * (float)bokehInfo.width);
			num2 += this.bokehScale * this.maxBlurSpread * DepthOfField34.BOKEH_EXTRA_BLUR * this.oneOverBaseSize;
			this.bokehMaterial.SetTexture("_Source", bokehInfo);
			this.bokehMaterial.SetTexture("_MainTex", this.bokehTexture);
			this.bokehMaterial.SetVector("_ArScale", new Vector4(num2, num2 * num, 0.5f, 0.5f * num));
			this.bokehMaterial.SetFloat("_Intensity", this.bokehIntensity);
			this.bokehMaterial.SetPass(0);
			int i = 0;
			Mesh[] array = meshes;
			int length = array.Length;
			while (i < length)
			{
				if (array[i])
				{
					Graphics.DrawMeshNow(array[i], Matrix4x4.identity);
				}
				i++;
			}
			GL.PopMatrix();
			Graphics.Blit(tempTex, finalTarget, this.dofMaterial, 8);
			bokehInfo.filterMode = FilterMode.Bilinear;
		}
	}

	// Token: 0x0600004E RID: 78 RVA: 0x0000628C File Offset: 0x0000448C
	public virtual void ReleaseTextures()
	{
		if (this.foregroundTexture)
		{
			RenderTexture.ReleaseTemporary(this.foregroundTexture);
		}
		if (this.finalDefocus)
		{
			RenderTexture.ReleaseTemporary(this.finalDefocus);
		}
		if (this.mediumRezWorkTexture)
		{
			RenderTexture.ReleaseTemporary(this.mediumRezWorkTexture);
		}
		if (this.lowRezWorkTexture)
		{
			RenderTexture.ReleaseTemporary(this.lowRezWorkTexture);
		}
		if (this.bokehSource)
		{
			RenderTexture.ReleaseTemporary(this.bokehSource);
		}
		if (this.bokehSource2)
		{
			RenderTexture.ReleaseTemporary(this.bokehSource2);
		}
	}

	// Token: 0x0600004F RID: 79 RVA: 0x0000633C File Offset: 0x0000453C
	public virtual void AllocateTextures(bool blurForeground, RenderTexture source, int divider, int lowTexDivider)
	{
		this.foregroundTexture = null;
		if (blurForeground)
		{
			this.foregroundTexture = RenderTexture.GetTemporary(source.width, source.height, 0);
		}
		this.mediumRezWorkTexture = RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
		this.finalDefocus = RenderTexture.GetTemporary(source.width / divider, source.height / divider, 0);
		this.lowRezWorkTexture = RenderTexture.GetTemporary(source.width / lowTexDivider, source.height / lowTexDivider, 0);
		this.bokehSource = null;
		this.bokehSource2 = null;
		if (this.bokeh)
		{
			this.bokehSource = RenderTexture.GetTemporary(source.width / (lowTexDivider * this.bokehDownsample), source.height / (lowTexDivider * this.bokehDownsample), 0, RenderTextureFormat.ARGBHalf);
			this.bokehSource2 = RenderTexture.GetTemporary(source.width / (lowTexDivider * this.bokehDownsample), source.height / (lowTexDivider * this.bokehDownsample), 0, RenderTextureFormat.ARGBHalf);
			this.bokehSource.filterMode = FilterMode.Bilinear;
			this.bokehSource2.filterMode = FilterMode.Bilinear;
			RenderTexture.active = this.bokehSource2;
			GL.Clear(false, true, new Color((float)0, (float)0, (float)0, (float)0));
		}
		source.filterMode = FilterMode.Bilinear;
		this.finalDefocus.filterMode = FilterMode.Bilinear;
		this.mediumRezWorkTexture.filterMode = FilterMode.Bilinear;
		this.lowRezWorkTexture.filterMode = FilterMode.Bilinear;
		if (this.foregroundTexture)
		{
			this.foregroundTexture.filterMode = FilterMode.Bilinear;
		}
	}

	// Token: 0x06000050 RID: 80 RVA: 0x000064C8 File Offset: 0x000046C8
	public override void Main()
	{
	}

	// Token: 0x040000E4 RID: 228
	[NonSerialized]
	private static int SMOOTH_DOWNSAMPLE_PASS = 6;

	// Token: 0x040000E5 RID: 229
	[NonSerialized]
	private static float BOKEH_EXTRA_BLUR = 2f;

	// Token: 0x040000E6 RID: 230
	public Dof34QualitySetting quality;

	// Token: 0x040000E7 RID: 231
	public DofResolution resolution;

	// Token: 0x040000E8 RID: 232
	public bool simpleTweakMode;

	// Token: 0x040000E9 RID: 233
	public float focalPoint;

	// Token: 0x040000EA RID: 234
	public float smoothness;

	// Token: 0x040000EB RID: 235
	public float focalZDistance;

	// Token: 0x040000EC RID: 236
	public float focalZStartCurve;

	// Token: 0x040000ED RID: 237
	public float focalZEndCurve;

	// Token: 0x040000EE RID: 238
	private float focalStartCurve;

	// Token: 0x040000EF RID: 239
	private float focalEndCurve;

	// Token: 0x040000F0 RID: 240
	private float focalDistance01;

	// Token: 0x040000F1 RID: 241
	public Transform objectFocus;

	// Token: 0x040000F2 RID: 242
	public float focalSize;

	// Token: 0x040000F3 RID: 243
	public DofBlurriness bluriness;

	// Token: 0x040000F4 RID: 244
	public float maxBlurSpread;

	// Token: 0x040000F5 RID: 245
	public float foregroundBlurExtrude;

	// Token: 0x040000F6 RID: 246
	public Shader dofBlurShader;

	// Token: 0x040000F7 RID: 247
	private Material dofBlurMaterial;

	// Token: 0x040000F8 RID: 248
	public Shader dofShader;

	// Token: 0x040000F9 RID: 249
	private Material dofMaterial;

	// Token: 0x040000FA RID: 250
	public bool visualize;

	// Token: 0x040000FB RID: 251
	public BokehDestination bokehDestination;

	// Token: 0x040000FC RID: 252
	private float widthOverHeight;

	// Token: 0x040000FD RID: 253
	private float oneOverBaseSize;

	// Token: 0x040000FE RID: 254
	public bool bokeh;

	// Token: 0x040000FF RID: 255
	public bool bokehSupport;

	// Token: 0x04000100 RID: 256
	public Shader bokehShader;

	// Token: 0x04000101 RID: 257
	public Texture2D bokehTexture;

	// Token: 0x04000102 RID: 258
	public float bokehScale;

	// Token: 0x04000103 RID: 259
	public float bokehIntensity;

	// Token: 0x04000104 RID: 260
	public float bokehThreshholdContrast;

	// Token: 0x04000105 RID: 261
	public float bokehThreshholdLuminance;

	// Token: 0x04000106 RID: 262
	public int bokehDownsample;

	// Token: 0x04000107 RID: 263
	private Material bokehMaterial;

	// Token: 0x04000108 RID: 264
	private RenderTexture foregroundTexture;

	// Token: 0x04000109 RID: 265
	private RenderTexture mediumRezWorkTexture;

	// Token: 0x0400010A RID: 266
	private RenderTexture finalDefocus;

	// Token: 0x0400010B RID: 267
	private RenderTexture lowRezWorkTexture;

	// Token: 0x0400010C RID: 268
	private RenderTexture bokehSource;

	// Token: 0x0400010D RID: 269
	private RenderTexture bokehSource2;
}
