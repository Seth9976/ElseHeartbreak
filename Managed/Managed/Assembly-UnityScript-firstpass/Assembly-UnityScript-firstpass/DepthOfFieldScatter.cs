using System;
using Boo.Lang.Runtime;
using UnityEngine;

// Token: 0x0200001C RID: 28
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Depth of Field (Lens Blur, Scatter, DX11)")]
[Serializable]
public class DepthOfFieldScatter : PostEffectsBase
{
	// Token: 0x06000051 RID: 81 RVA: 0x000064CC File Offset: 0x000046CC
	public DepthOfFieldScatter()
	{
		this.focalLength = 10f;
		this.focalSize = 0.05f;
		this.aperture = 11.5f;
		this.maxBlurSize = 2f;
		this.blurType = DepthOfFieldScatter.BlurType.DiscBlur;
		this.blurSampleCount = DepthOfFieldScatter.BlurSampleCount.High;
		this.foregroundOverlap = 1f;
		this.dx11BokehThreshhold = 0.5f;
		this.dx11SpawnHeuristic = 0.0875f;
		this.dx11BokehScale = 1.2f;
		this.dx11BokehIntensity = 2.5f;
		this.focalDistance01 = 10f;
		this.internalBlurWidth = 1f;
	}

	// Token: 0x06000052 RID: 82 RVA: 0x00006568 File Offset: 0x00004768
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.dofHdrMaterial = this.CheckShaderAndCreateMaterial(this.dofHdrShader, this.dofHdrMaterial);
		if (this.supportDX11 && this.blurType == DepthOfFieldScatter.BlurType.DX11)
		{
			this.dx11bokehMaterial = this.CheckShaderAndCreateMaterial(this.dx11BokehShader, this.dx11bokehMaterial);
			this.CreateComputeResources();
		}
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000053 RID: 83 RVA: 0x000065E4 File Offset: 0x000047E4
	public override void OnEnable()
	{
		this.camera.depthTextureMode = this.camera.depthTextureMode | DepthTextureMode.Depth;
	}

	// Token: 0x06000054 RID: 84 RVA: 0x0000660C File Offset: 0x0000480C
	public virtual void OnDisable()
	{
		this.ReleaseComputeResources();
		if (this.dofHdrMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(this.dofHdrMaterial);
		}
		this.dofHdrMaterial = null;
		if (this.dx11bokehMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(this.dx11bokehMaterial);
		}
		this.dx11bokehMaterial = null;
	}

	// Token: 0x06000055 RID: 85 RVA: 0x00006664 File Offset: 0x00004864
	public virtual void ReleaseComputeResources()
	{
		if (this.cbDrawArgs != null)
		{
			this.cbDrawArgs.Release();
		}
		this.cbDrawArgs = null;
		if (this.cbPoints != null)
		{
			this.cbPoints.Release();
		}
		this.cbPoints = null;
	}

	// Token: 0x06000056 RID: 86 RVA: 0x000066AC File Offset: 0x000048AC
	public virtual void CreateComputeResources()
	{
		if (RuntimeServices.EqualityOperator(this.cbDrawArgs, null))
		{
			this.cbDrawArgs = new ComputeBuffer(1, 16, ComputeBufferType.DrawIndirect);
			int[] array = new int[] { 0, 1, 0, 0 };
			this.cbDrawArgs.SetData(array);
		}
		if (RuntimeServices.EqualityOperator(this.cbPoints, null))
		{
			this.cbPoints = new ComputeBuffer(90000, 28, ComputeBufferType.Append);
		}
	}

	// Token: 0x06000057 RID: 87 RVA: 0x00006724 File Offset: 0x00004924
	public virtual float FocalDistance01(float worldDist)
	{
		return this.camera.WorldToViewportPoint((worldDist - this.camera.nearClipPlane) * this.camera.transform.forward + this.camera.transform.position).z / (this.camera.farClipPlane - this.camera.nearClipPlane);
	}

	// Token: 0x06000058 RID: 88 RVA: 0x00006794 File Offset: 0x00004994
	private void WriteCoc(RenderTexture fromTo, RenderTexture temp1, RenderTexture temp2, bool fgDilate)
	{
		this.dofHdrMaterial.SetTexture("_FgOverlap", null);
		if (this.nearBlur && fgDilate)
		{
			Graphics.Blit(fromTo, temp2, this.dofHdrMaterial, 4);
			float num = this.internalBlurWidth * this.foregroundOverlap;
			this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, num, (float)0, num));
			Graphics.Blit(temp2, temp1, this.dofHdrMaterial, 2);
			this.dofHdrMaterial.SetVector("_Offsets", new Vector4(num, (float)0, (float)0, num));
			Graphics.Blit(temp1, temp2, this.dofHdrMaterial, 2);
			this.dofHdrMaterial.SetTexture("_FgOverlap", temp2);
			Graphics.Blit(fromTo, fromTo, this.dofHdrMaterial, 13);
		}
		else
		{
			Graphics.Blit(fromTo, fromTo, this.dofHdrMaterial, 0);
		}
	}

	// Token: 0x06000059 RID: 89 RVA: 0x00006868 File Offset: 0x00004A68
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.aperture < (float)0)
			{
				this.aperture = (float)0;
			}
			if (this.maxBlurSize < 0.1f)
			{
				this.maxBlurSize = 0.1f;
			}
			this.focalSize = Mathf.Clamp(this.focalSize, (float)0, 2f);
			this.internalBlurWidth = Mathf.Max(this.maxBlurSize, (float)0);
			this.focalDistance01 = ((!this.focalTransform) ? this.FocalDistance01(this.focalLength) : (this.camera.WorldToViewportPoint(this.focalTransform.position).z / this.camera.farClipPlane));
			this.dofHdrMaterial.SetVector("_CurveParams", new Vector4(1f, this.focalSize, this.aperture / 10f, this.focalDistance01));
			RenderTexture renderTexture = null;
			float num = this.internalBlurWidth * this.foregroundOverlap;
			RenderTexture renderTexture2;
			if (this.visualizeFocus)
			{
				renderTexture2 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				renderTexture = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				this.WriteCoc(source, renderTexture2, renderTexture, true);
				Graphics.Blit(source, destination, this.dofHdrMaterial, 16);
			}
			else if (this.blurType == DepthOfFieldScatter.BlurType.DX11 && this.dx11bokehMaterial)
			{
				if (this.highResolution)
				{
					this.internalBlurWidth = ((this.internalBlurWidth >= 0.1f) ? this.internalBlurWidth : 0.1f);
					num = this.internalBlurWidth * this.foregroundOverlap;
					renderTexture2 = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
					RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0, source.format);
					this.WriteCoc(source, null, null, false);
					RenderTexture renderTexture3 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
					RenderTexture renderTexture4 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
					Graphics.Blit(source, renderTexture3, this.dofHdrMaterial, 15);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, 1.5f, (float)0, 1.5f));
					Graphics.Blit(renderTexture3, renderTexture4, this.dofHdrMaterial, 19);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(1.5f, (float)0, (float)0, 1.5f));
					Graphics.Blit(renderTexture4, renderTexture3, this.dofHdrMaterial, 19);
					if (this.nearBlur)
					{
						Graphics.Blit(source, renderTexture4, this.dofHdrMaterial, 4);
					}
					this.dx11bokehMaterial.SetTexture("_BlurredColor", renderTexture3);
					this.dx11bokehMaterial.SetFloat("_SpawnHeuristic", this.dx11SpawnHeuristic);
					this.dx11bokehMaterial.SetVector("_BokehParams", new Vector4(this.dx11BokehScale, this.dx11BokehIntensity, Mathf.Clamp(this.dx11BokehThreshhold, 0.005f, 4f), this.internalBlurWidth));
					this.dx11bokehMaterial.SetTexture("_FgCocMask", (!this.nearBlur) ? null : renderTexture4);
					Graphics.SetRandomWriteTarget(1, this.cbPoints);
					Graphics.Blit(source, renderTexture2, this.dx11bokehMaterial, 0);
					Graphics.ClearRandomWriteTargets();
					if (this.nearBlur)
					{
						this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, num, (float)0, num));
						Graphics.Blit(renderTexture4, renderTexture3, this.dofHdrMaterial, 2);
						this.dofHdrMaterial.SetVector("_Offsets", new Vector4(num, (float)0, (float)0, num));
						Graphics.Blit(renderTexture3, renderTexture4, this.dofHdrMaterial, 2);
						Graphics.Blit(renderTexture4, renderTexture2, this.dofHdrMaterial, 3);
					}
					Graphics.Blit(renderTexture2, temporary, this.dofHdrMaterial, 20);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(this.internalBlurWidth, (float)0, (float)0, this.internalBlurWidth));
					Graphics.Blit(renderTexture2, source, this.dofHdrMaterial, 5);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, this.internalBlurWidth, (float)0, this.internalBlurWidth));
					Graphics.Blit(source, temporary, this.dofHdrMaterial, 21);
					Graphics.SetRenderTarget(temporary);
					ComputeBuffer.CopyCount(this.cbPoints, this.cbDrawArgs, 0);
					this.dx11bokehMaterial.SetBuffer("pointBuffer", this.cbPoints);
					this.dx11bokehMaterial.SetTexture("_MainTex", this.dx11BokehTexture);
					this.dx11bokehMaterial.SetVector("_Screen", new Vector3(1f / (1f * (float)source.width), 1f / (1f * (float)source.height), this.internalBlurWidth));
					this.dx11bokehMaterial.SetPass(2);
					Graphics.DrawProceduralIndirect(MeshTopology.Points, this.cbDrawArgs, 0);
					Graphics.Blit(temporary, destination);
					RenderTexture.ReleaseTemporary(temporary);
					RenderTexture.ReleaseTemporary(renderTexture3);
					RenderTexture.ReleaseTemporary(renderTexture4);
				}
				else
				{
					renderTexture2 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
					renderTexture = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
					num = this.internalBlurWidth * this.foregroundOverlap;
					this.WriteCoc(source, null, null, false);
					source.filterMode = FilterMode.Bilinear;
					Graphics.Blit(source, renderTexture2, this.dofHdrMaterial, 6);
					RenderTexture renderTexture3 = RenderTexture.GetTemporary(renderTexture2.width >> 1, renderTexture2.height >> 1, 0, renderTexture2.format);
					RenderTexture renderTexture4 = RenderTexture.GetTemporary(renderTexture2.width >> 1, renderTexture2.height >> 1, 0, renderTexture2.format);
					Graphics.Blit(renderTexture2, renderTexture3, this.dofHdrMaterial, 15);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, 1.5f, (float)0, 1.5f));
					Graphics.Blit(renderTexture3, renderTexture4, this.dofHdrMaterial, 19);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(1.5f, (float)0, (float)0, 1.5f));
					Graphics.Blit(renderTexture4, renderTexture3, this.dofHdrMaterial, 19);
					RenderTexture renderTexture5 = null;
					if (this.nearBlur)
					{
						renderTexture5 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
						Graphics.Blit(source, renderTexture5, this.dofHdrMaterial, 4);
					}
					this.dx11bokehMaterial.SetTexture("_BlurredColor", renderTexture3);
					this.dx11bokehMaterial.SetFloat("_SpawnHeuristic", this.dx11SpawnHeuristic);
					this.dx11bokehMaterial.SetVector("_BokehParams", new Vector4(this.dx11BokehScale, this.dx11BokehIntensity, Mathf.Clamp(this.dx11BokehThreshhold, 0.005f, 4f), this.internalBlurWidth));
					this.dx11bokehMaterial.SetTexture("_FgCocMask", renderTexture5);
					Graphics.SetRandomWriteTarget(1, this.cbPoints);
					Graphics.Blit(renderTexture2, renderTexture, this.dx11bokehMaterial, 0);
					Graphics.ClearRandomWriteTargets();
					RenderTexture.ReleaseTemporary(renderTexture3);
					RenderTexture.ReleaseTemporary(renderTexture4);
					if (this.nearBlur)
					{
						this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, num, (float)0, num));
						Graphics.Blit(renderTexture5, renderTexture2, this.dofHdrMaterial, 2);
						this.dofHdrMaterial.SetVector("_Offsets", new Vector4(num, (float)0, (float)0, num));
						Graphics.Blit(renderTexture2, renderTexture5, this.dofHdrMaterial, 2);
						Graphics.Blit(renderTexture5, renderTexture, this.dofHdrMaterial, 3);
					}
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4(this.internalBlurWidth, (float)0, (float)0, this.internalBlurWidth));
					Graphics.Blit(renderTexture, renderTexture2, this.dofHdrMaterial, 5);
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, this.internalBlurWidth, (float)0, this.internalBlurWidth));
					Graphics.Blit(renderTexture2, renderTexture, this.dofHdrMaterial, 5);
					Graphics.SetRenderTarget(renderTexture);
					ComputeBuffer.CopyCount(this.cbPoints, this.cbDrawArgs, 0);
					this.dx11bokehMaterial.SetBuffer("pointBuffer", this.cbPoints);
					this.dx11bokehMaterial.SetTexture("_MainTex", this.dx11BokehTexture);
					this.dx11bokehMaterial.SetVector("_Screen", new Vector3(1f / (1f * (float)renderTexture.width), 1f / (1f * (float)renderTexture.height), this.internalBlurWidth));
					this.dx11bokehMaterial.SetPass(1);
					Graphics.DrawProceduralIndirect(MeshTopology.Points, this.cbDrawArgs, 0);
					this.dofHdrMaterial.SetTexture("_LowRez", renderTexture);
					this.dofHdrMaterial.SetTexture("_FgOverlap", renderTexture5);
					this.dofHdrMaterial.SetVector("_Offsets", 1f * (float)source.width / (1f * (float)renderTexture.width) * this.internalBlurWidth * Vector4.one);
					Graphics.Blit(source, destination, this.dofHdrMaterial, 9);
					if (renderTexture5)
					{
						RenderTexture.ReleaseTemporary(renderTexture5);
					}
				}
			}
			else
			{
				renderTexture2 = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				renderTexture = RenderTexture.GetTemporary(source.width >> 1, source.height >> 1, 0, source.format);
				source.filterMode = FilterMode.Bilinear;
				if (this.highResolution)
				{
					this.internalBlurWidth *= 2f;
				}
				this.WriteCoc(source, renderTexture2, renderTexture, true);
				int num2 = ((this.blurSampleCount != DepthOfFieldScatter.BlurSampleCount.High && this.blurSampleCount != DepthOfFieldScatter.BlurSampleCount.Medium) ? 11 : 17);
				if (this.highResolution)
				{
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, this.internalBlurWidth, 0.025f, this.internalBlurWidth));
					Graphics.Blit(source, destination, this.dofHdrMaterial, num2);
				}
				else
				{
					this.dofHdrMaterial.SetVector("_Offsets", new Vector4((float)0, this.internalBlurWidth, 0.1f, this.internalBlurWidth));
					Graphics.Blit(source, renderTexture2, this.dofHdrMaterial, 6);
					Graphics.Blit(renderTexture2, renderTexture, this.dofHdrMaterial, num2);
					this.dofHdrMaterial.SetTexture("_LowRez", renderTexture);
					this.dofHdrMaterial.SetTexture("_FgOverlap", null);
					this.dofHdrMaterial.SetVector("_Offsets", Vector4.one * (1f * (float)source.width / (1f * (float)renderTexture.width)) * this.internalBlurWidth);
					Graphics.Blit(source, destination, this.dofHdrMaterial, (this.blurSampleCount != DepthOfFieldScatter.BlurSampleCount.High) ? 12 : 18);
				}
			}
			if (renderTexture2)
			{
				RenderTexture.ReleaseTemporary(renderTexture2);
			}
			if (renderTexture)
			{
				RenderTexture.ReleaseTemporary(renderTexture);
			}
		}
	}

	// Token: 0x0600005A RID: 90 RVA: 0x00007344 File Offset: 0x00005544
	public override void Main()
	{
	}

	// Token: 0x0400010E RID: 270
	public bool visualizeFocus;

	// Token: 0x0400010F RID: 271
	public float focalLength;

	// Token: 0x04000110 RID: 272
	public float focalSize;

	// Token: 0x04000111 RID: 273
	public float aperture;

	// Token: 0x04000112 RID: 274
	public Transform focalTransform;

	// Token: 0x04000113 RID: 275
	public float maxBlurSize;

	// Token: 0x04000114 RID: 276
	public bool highResolution;

	// Token: 0x04000115 RID: 277
	public DepthOfFieldScatter.BlurType blurType;

	// Token: 0x04000116 RID: 278
	public DepthOfFieldScatter.BlurSampleCount blurSampleCount;

	// Token: 0x04000117 RID: 279
	public bool nearBlur;

	// Token: 0x04000118 RID: 280
	public float foregroundOverlap;

	// Token: 0x04000119 RID: 281
	public Shader dofHdrShader;

	// Token: 0x0400011A RID: 282
	private Material dofHdrMaterial;

	// Token: 0x0400011B RID: 283
	public Shader dx11BokehShader;

	// Token: 0x0400011C RID: 284
	private Material dx11bokehMaterial;

	// Token: 0x0400011D RID: 285
	public float dx11BokehThreshhold;

	// Token: 0x0400011E RID: 286
	public float dx11SpawnHeuristic;

	// Token: 0x0400011F RID: 287
	public Texture2D dx11BokehTexture;

	// Token: 0x04000120 RID: 288
	public float dx11BokehScale;

	// Token: 0x04000121 RID: 289
	public float dx11BokehIntensity;

	// Token: 0x04000122 RID: 290
	private float focalDistance01;

	// Token: 0x04000123 RID: 291
	private ComputeBuffer cbDrawArgs;

	// Token: 0x04000124 RID: 292
	private ComputeBuffer cbPoints;

	// Token: 0x04000125 RID: 293
	private float internalBlurWidth;

	// Token: 0x0200001D RID: 29
	[Serializable]
	public enum BlurType
	{
		// Token: 0x04000127 RID: 295
		DiscBlur,
		// Token: 0x04000128 RID: 296
		DX11
	}

	// Token: 0x0200001E RID: 30
	[Serializable]
	public enum BlurSampleCount
	{
		// Token: 0x0400012A RID: 298
		Low,
		// Token: 0x0400012B RID: 299
		Medium,
		// Token: 0x0400012C RID: 300
		High
	}
}
