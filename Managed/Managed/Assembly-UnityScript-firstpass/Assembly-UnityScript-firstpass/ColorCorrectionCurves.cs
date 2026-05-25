using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Color Correction (Curves, Saturation)")]
[Serializable]
public class ColorCorrectionCurves : PostEffectsBase
{
	// Token: 0x06000026 RID: 38 RVA: 0x000046A4 File Offset: 0x000028A4
	public ColorCorrectionCurves()
	{
		this.saturation = 1f;
		this.selectiveFromColor = Color.white;
		this.selectiveToColor = Color.white;
		this.updateTextures = true;
		this.updateTexturesOnStartup = true;
	}

	// Token: 0x06000027 RID: 39 RVA: 0x000046DC File Offset: 0x000028DC
	public override void Start()
	{
		base.Start();
		this.updateTexturesOnStartup = true;
	}

	// Token: 0x06000028 RID: 40 RVA: 0x000046EC File Offset: 0x000028EC
	public virtual void Awake()
	{
	}

	// Token: 0x06000029 RID: 41 RVA: 0x000046F0 File Offset: 0x000028F0
	public override bool CheckResources()
	{
		this.CheckSupport(this.mode == ColorCorrectionMode.Advanced);
		this.ccMaterial = this.CheckShaderAndCreateMaterial(this.simpleColorCorrectionCurvesShader, this.ccMaterial);
		this.ccDepthMaterial = this.CheckShaderAndCreateMaterial(this.colorCorrectionCurvesShader, this.ccDepthMaterial);
		this.selectiveCcMaterial = this.CheckShaderAndCreateMaterial(this.colorCorrectionSelectiveShader, this.selectiveCcMaterial);
		if (!this.rgbChannelTex)
		{
			this.rgbChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
		}
		if (!this.rgbDepthChannelTex)
		{
			this.rgbDepthChannelTex = new Texture2D(256, 4, TextureFormat.ARGB32, false, true);
		}
		if (!this.zCurveTex)
		{
			this.zCurveTex = new Texture2D(256, 1, TextureFormat.ARGB32, false, true);
		}
		this.rgbChannelTex.hideFlags = HideFlags.DontSave;
		this.rgbDepthChannelTex.hideFlags = HideFlags.DontSave;
		this.zCurveTex.hideFlags = HideFlags.DontSave;
		this.rgbChannelTex.wrapMode = TextureWrapMode.Clamp;
		this.rgbDepthChannelTex.wrapMode = TextureWrapMode.Clamp;
		this.zCurveTex.wrapMode = TextureWrapMode.Clamp;
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00004820 File Offset: 0x00002A20
	public virtual void UpdateParameters()
	{
		if (this.redChannel != null && this.greenChannel != null && this.blueChannel != null)
		{
			for (float num = (float)0; num <= 1f; num += 0.003921569f)
			{
				float num2 = Mathf.Clamp(this.redChannel.Evaluate(num), (float)0, 1f);
				float num3 = Mathf.Clamp(this.greenChannel.Evaluate(num), (float)0, 1f);
				float num4 = Mathf.Clamp(this.blueChannel.Evaluate(num), (float)0, 1f);
				this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num2, num2, num2));
				this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 1, new Color(num3, num3, num3));
				this.rgbChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 2, new Color(num4, num4, num4));
				float num5 = Mathf.Clamp(this.zCurve.Evaluate(num), (float)0, 1f);
				this.zCurveTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num5, num5, num5));
				num2 = Mathf.Clamp(this.depthRedChannel.Evaluate(num), (float)0, 1f);
				num3 = Mathf.Clamp(this.depthGreenChannel.Evaluate(num), (float)0, 1f);
				num4 = Mathf.Clamp(this.depthBlueChannel.Evaluate(num), (float)0, 1f);
				this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 0, new Color(num2, num2, num2));
				this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 1, new Color(num3, num3, num3));
				this.rgbDepthChannelTex.SetPixel((int)Mathf.Floor(num * 255f), 2, new Color(num4, num4, num4));
			}
			this.rgbChannelTex.Apply();
			this.rgbDepthChannelTex.Apply();
			this.zCurveTex.Apply();
		}
	}

	// Token: 0x0600002B RID: 43 RVA: 0x00004A30 File Offset: 0x00002C30
	public virtual void UpdateTextures()
	{
		this.UpdateParameters();
	}

	// Token: 0x0600002C RID: 44 RVA: 0x00004A38 File Offset: 0x00002C38
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.updateTexturesOnStartup)
			{
				this.UpdateParameters();
				this.updateTexturesOnStartup = false;
			}
			if (this.useDepthCorrection)
			{
				this.camera.depthTextureMode = this.camera.depthTextureMode | DepthTextureMode.Depth;
			}
			RenderTexture renderTexture = destination;
			if (this.selectiveCc)
			{
				renderTexture = RenderTexture.GetTemporary(source.width, source.height);
			}
			if (this.useDepthCorrection)
			{
				this.ccDepthMaterial.SetTexture("_RgbTex", this.rgbChannelTex);
				this.ccDepthMaterial.SetTexture("_ZCurve", this.zCurveTex);
				this.ccDepthMaterial.SetTexture("_RgbDepthTex", this.rgbDepthChannelTex);
				this.ccDepthMaterial.SetFloat("_Saturation", this.saturation);
				Graphics.Blit(source, renderTexture, this.ccDepthMaterial);
			}
			else
			{
				this.ccMaterial.SetTexture("_RgbTex", this.rgbChannelTex);
				this.ccMaterial.SetFloat("_Saturation", this.saturation);
				Graphics.Blit(source, renderTexture, this.ccMaterial);
			}
			if (this.selectiveCc)
			{
				this.selectiveCcMaterial.SetColor("selColor", this.selectiveFromColor);
				this.selectiveCcMaterial.SetColor("targetColor", this.selectiveToColor);
				Graphics.Blit(renderTexture, destination, this.selectiveCcMaterial);
				RenderTexture.ReleaseTemporary(renderTexture);
			}
		}
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00004BB0 File Offset: 0x00002DB0
	public override void Main()
	{
	}

	// Token: 0x040000A9 RID: 169
	public AnimationCurve redChannel;

	// Token: 0x040000AA RID: 170
	public AnimationCurve greenChannel;

	// Token: 0x040000AB RID: 171
	public AnimationCurve blueChannel;

	// Token: 0x040000AC RID: 172
	public bool useDepthCorrection;

	// Token: 0x040000AD RID: 173
	public AnimationCurve zCurve;

	// Token: 0x040000AE RID: 174
	public AnimationCurve depthRedChannel;

	// Token: 0x040000AF RID: 175
	public AnimationCurve depthGreenChannel;

	// Token: 0x040000B0 RID: 176
	public AnimationCurve depthBlueChannel;

	// Token: 0x040000B1 RID: 177
	private Material ccMaterial;

	// Token: 0x040000B2 RID: 178
	private Material ccDepthMaterial;

	// Token: 0x040000B3 RID: 179
	private Material selectiveCcMaterial;

	// Token: 0x040000B4 RID: 180
	private Texture2D rgbChannelTex;

	// Token: 0x040000B5 RID: 181
	private Texture2D rgbDepthChannelTex;

	// Token: 0x040000B6 RID: 182
	private Texture2D zCurveTex;

	// Token: 0x040000B7 RID: 183
	public float saturation;

	// Token: 0x040000B8 RID: 184
	public bool selectiveCc;

	// Token: 0x040000B9 RID: 185
	public Color selectiveFromColor;

	// Token: 0x040000BA RID: 186
	public Color selectiveToColor;

	// Token: 0x040000BB RID: 187
	public ColorCorrectionMode mode;

	// Token: 0x040000BC RID: 188
	public bool updateTextures;

	// Token: 0x040000BD RID: 189
	public Shader colorCorrectionCurvesShader;

	// Token: 0x040000BE RID: 190
	public Shader simpleColorCorrectionCurvesShader;

	// Token: 0x040000BF RID: 191
	public Shader colorCorrectionSelectiveShader;

	// Token: 0x040000C0 RID: 192
	private bool updateTexturesOnStartup;
}
