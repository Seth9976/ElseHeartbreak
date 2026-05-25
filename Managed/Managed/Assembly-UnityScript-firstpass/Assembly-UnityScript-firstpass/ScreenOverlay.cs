using System;
using UnityEngine;

// Token: 0x02000028 RID: 40
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Screen Overlay")]
[Serializable]
public class ScreenOverlay : PostEffectsBase
{
	// Token: 0x0600008E RID: 142 RVA: 0x00009000 File Offset: 0x00007200
	public ScreenOverlay()
	{
		this.blendMode = ScreenOverlay.OverlayBlendMode.Overlay;
		this.intensity = 1f;
	}

	// Token: 0x0600008F RID: 143 RVA: 0x0000901C File Offset: 0x0000721C
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.overlayMaterial = this.CheckShaderAndCreateMaterial(this.overlayShader, this.overlayMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000090 RID: 144 RVA: 0x00009058 File Offset: 0x00007258
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.overlayMaterial.SetFloat("_Intensity", this.intensity);
			this.overlayMaterial.SetTexture("_Overlay", this.texture);
			Graphics.Blit(source, destination, this.overlayMaterial, (int)this.blendMode);
		}
	}

	// Token: 0x06000091 RID: 145 RVA: 0x000090BC File Offset: 0x000072BC
	public override void Main()
	{
	}

	// Token: 0x04000168 RID: 360
	public ScreenOverlay.OverlayBlendMode blendMode;

	// Token: 0x04000169 RID: 361
	public float intensity;

	// Token: 0x0400016A RID: 362
	public Texture2D texture;

	// Token: 0x0400016B RID: 363
	public Shader overlayShader;

	// Token: 0x0400016C RID: 364
	private Material overlayMaterial;

	// Token: 0x02000029 RID: 41
	[Serializable]
	public enum OverlayBlendMode
	{
		// Token: 0x0400016E RID: 366
		Additive,
		// Token: 0x0400016F RID: 367
		ScreenBlend,
		// Token: 0x04000170 RID: 368
		Multiply,
		// Token: 0x04000171 RID: 369
		Overlay,
		// Token: 0x04000172 RID: 370
		AlphaBlend
	}
}
