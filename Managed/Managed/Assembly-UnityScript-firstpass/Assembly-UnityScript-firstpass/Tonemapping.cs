using System;
using UnityEngine;
using UnityScript.Lang;

// Token: 0x0200002E RID: 46
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Tonemapping")]
[Serializable]
public class Tonemapping : PostEffectsBase
{
	// Token: 0x0600009B RID: 155 RVA: 0x00009A38 File Offset: 0x00007C38
	public Tonemapping()
	{
		this.type = Tonemapping.TonemapperType.Photographic;
		this.adaptiveTextureSize = Tonemapping.AdaptiveTexSize.Square256;
		this.exposureAdjustment = 1.5f;
		this.middleGrey = 0.4f;
		this.white = 2f;
		this.adaptionSpeed = 1.5f;
		this.validRenderTextureFormat = true;
		this.rtFormat = RenderTextureFormat.ARGBHalf;
	}

	// Token: 0x0600009C RID: 156 RVA: 0x00009A98 File Offset: 0x00007C98
	public override bool CheckResources()
	{
		this.CheckSupport(false, true);
		this.tonemapMaterial = this.CheckShaderAndCreateMaterial(this.tonemapper, this.tonemapMaterial);
		if (!this.curveTex && this.type == Tonemapping.TonemapperType.UserCurve)
		{
			this.curveTex = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
			this.curveTex.filterMode = FilterMode.Bilinear;
			this.curveTex.wrapMode = TextureWrapMode.Clamp;
			this.curveTex.hideFlags = HideFlags.DontSave;
		}
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x0600009D RID: 157 RVA: 0x00009B34 File Offset: 0x00007D34
	public virtual float UpdateCurve()
	{
		float num = 1f;
		if (Extensions.get_length(this.remapCurve.keys) < 1)
		{
			this.remapCurve = new AnimationCurve(new Keyframe[]
			{
				new Keyframe((float)0, (float)0),
				new Keyframe((float)2, (float)1)
			});
		}
		if (this.remapCurve != null)
		{
			if (this.remapCurve.length != 0)
			{
				num = this.remapCurve[this.remapCurve.length - 1].time;
			}
			for (float num2 = (float)0; num2 <= 1f; num2 += 0.003921569f)
			{
				float num3 = this.remapCurve.Evaluate(num2 * 1f * num);
				this.curveTex.SetPixel((int)Mathf.Floor(num2 * 255f), 0, new Color(num3, num3, num3));
			}
			this.curveTex.Apply();
		}
		return 1f / num;
	}

	// Token: 0x0600009E RID: 158 RVA: 0x00009C38 File Offset: 0x00007E38
	public virtual void OnDisable()
	{
		if (this.rt)
		{
			global::UnityEngine.Object.DestroyImmediate(this.rt);
			this.rt = null;
		}
		if (this.tonemapMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(this.tonemapMaterial);
			this.tonemapMaterial = null;
		}
		if (this.curveTex)
		{
			global::UnityEngine.Object.DestroyImmediate(this.curveTex);
			this.curveTex = null;
		}
	}

	// Token: 0x0600009F RID: 159 RVA: 0x00009CAC File Offset: 0x00007EAC
	public virtual bool CreateInternalRenderTexture()
	{
		bool flag;
		if (this.rt)
		{
			flag = false;
		}
		else
		{
			this.rtFormat = ((!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGHalf)) ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.RGHalf);
			this.rt = new RenderTexture(1, 1, 0, this.rtFormat);
			this.rt.hideFlags = HideFlags.DontSave;
			flag = true;
		}
		return flag;
	}

	// Token: 0x060000A0 RID: 160 RVA: 0x00009D0C File Offset: 0x00007F0C
	[ImageEffectTransformsToLDR]
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.exposureAdjustment = ((this.exposureAdjustment >= 0.001f) ? this.exposureAdjustment : 0.001f);
			if (this.type == Tonemapping.TonemapperType.UserCurve)
			{
				float num = this.UpdateCurve();
				this.tonemapMaterial.SetFloat("_RangeScale", num);
				this.tonemapMaterial.SetTexture("_Curve", this.curveTex);
				Graphics.Blit(source, destination, this.tonemapMaterial, 4);
			}
			else if (this.type == Tonemapping.TonemapperType.SimpleReinhard)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 6);
			}
			else if (this.type == Tonemapping.TonemapperType.Hable)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 5);
			}
			else if (this.type == Tonemapping.TonemapperType.Photographic)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 8);
			}
			else if (this.type == Tonemapping.TonemapperType.OptimizedHejiDawson)
			{
				this.tonemapMaterial.SetFloat("_ExposureAdjustment", 0.5f * this.exposureAdjustment);
				Graphics.Blit(source, destination, this.tonemapMaterial, 7);
			}
			else
			{
				bool flag = this.CreateInternalRenderTexture();
				RenderTexture temporary = RenderTexture.GetTemporary((int)this.adaptiveTextureSize, (int)this.adaptiveTextureSize, 0, this.rtFormat);
				Graphics.Blit(source, temporary);
				int num2 = (int)Mathf.Log((float)temporary.width * 1f, (float)2);
				int num3 = 2;
				RenderTexture[] array = new RenderTexture[num2];
				for (int i = 0; i < num2; i++)
				{
					array[i] = RenderTexture.GetTemporary(temporary.width / num3, temporary.width / num3, 0, this.rtFormat);
					num3 *= 2;
				}
				float num4 = (float)source.width * 1f / ((float)source.height * 1f);
				RenderTexture renderTexture = array[num2 - 1];
				Graphics.Blit(temporary, array[0], this.tonemapMaterial, 1);
				if (this.type == Tonemapping.TonemapperType.AdaptiveReinhardAutoWhite)
				{
					for (int i = 0; i < num2 - 1; i++)
					{
						Graphics.Blit(array[i], array[i + 1], this.tonemapMaterial, 9);
						renderTexture = array[i + 1];
					}
				}
				else if (this.type == Tonemapping.TonemapperType.AdaptiveReinhard)
				{
					for (int i = 0; i < num2 - 1; i++)
					{
						Graphics.Blit(array[i], array[i + 1]);
						renderTexture = array[i + 1];
					}
				}
				this.adaptionSpeed = ((this.adaptionSpeed >= 0.001f) ? this.adaptionSpeed : 0.001f);
				this.tonemapMaterial.SetFloat("_AdaptionSpeed", this.adaptionSpeed);
				Graphics.Blit(renderTexture, this.rt, this.tonemapMaterial, (!flag) ? 2 : 3);
				this.middleGrey = ((this.middleGrey >= 0.001f) ? this.middleGrey : 0.001f);
				this.tonemapMaterial.SetVector("_HdrParams", new Vector4(this.middleGrey, this.middleGrey, this.middleGrey, this.white * this.white));
				this.tonemapMaterial.SetTexture("_SmallTex", this.rt);
				if (this.type == Tonemapping.TonemapperType.AdaptiveReinhard)
				{
					Graphics.Blit(source, destination, this.tonemapMaterial, 0);
				}
				else if (this.type == Tonemapping.TonemapperType.AdaptiveReinhardAutoWhite)
				{
					Graphics.Blit(source, destination, this.tonemapMaterial, 10);
				}
				else
				{
					Debug.LogError("No valid adaptive tonemapper type found!");
					Graphics.Blit(source, destination);
				}
				for (int i = 0; i < num2; i++)
				{
					RenderTexture.ReleaseTemporary(array[i]);
				}
				RenderTexture.ReleaseTemporary(temporary);
			}
		}
	}

	// Token: 0x060000A1 RID: 161 RVA: 0x0000A108 File Offset: 0x00008308
	public override void Main()
	{
	}

	// Token: 0x04000196 RID: 406
	public Tonemapping.TonemapperType type;

	// Token: 0x04000197 RID: 407
	public Tonemapping.AdaptiveTexSize adaptiveTextureSize;

	// Token: 0x04000198 RID: 408
	public AnimationCurve remapCurve;

	// Token: 0x04000199 RID: 409
	private Texture2D curveTex;

	// Token: 0x0400019A RID: 410
	public float exposureAdjustment;

	// Token: 0x0400019B RID: 411
	public float middleGrey;

	// Token: 0x0400019C RID: 412
	public float white;

	// Token: 0x0400019D RID: 413
	public float adaptionSpeed;

	// Token: 0x0400019E RID: 414
	public Shader tonemapper;

	// Token: 0x0400019F RID: 415
	public bool validRenderTextureFormat;

	// Token: 0x040001A0 RID: 416
	private Material tonemapMaterial;

	// Token: 0x040001A1 RID: 417
	private RenderTexture rt;

	// Token: 0x040001A2 RID: 418
	private RenderTextureFormat rtFormat;

	// Token: 0x0200002F RID: 47
	[Serializable]
	public enum TonemapperType
	{
		// Token: 0x040001A4 RID: 420
		SimpleReinhard,
		// Token: 0x040001A5 RID: 421
		UserCurve,
		// Token: 0x040001A6 RID: 422
		Hable,
		// Token: 0x040001A7 RID: 423
		Photographic,
		// Token: 0x040001A8 RID: 424
		OptimizedHejiDawson,
		// Token: 0x040001A9 RID: 425
		AdaptiveReinhard,
		// Token: 0x040001AA RID: 426
		AdaptiveReinhardAutoWhite
	}

	// Token: 0x02000030 RID: 48
	[Serializable]
	public enum AdaptiveTexSize
	{
		// Token: 0x040001AC RID: 428
		Square16 = 16,
		// Token: 0x040001AD RID: 429
		Square32 = 32,
		// Token: 0x040001AE RID: 430
		Square64 = 64,
		// Token: 0x040001AF RID: 431
		Square128 = 128,
		// Token: 0x040001B0 RID: 432
		Square256 = 256,
		// Token: 0x040001B1 RID: 433
		Square512 = 512,
		// Token: 0x040001B2 RID: 434
		Square1024 = 1024
	}
}
