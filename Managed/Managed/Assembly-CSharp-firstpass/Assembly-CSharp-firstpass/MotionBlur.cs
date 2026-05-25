using System;
using UnityEngine;

// Token: 0x0200000A RID: 10
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Motion Blur (Color Accumulation)")]
public class MotionBlur : ImageEffectBase
{
	// Token: 0x0600002C RID: 44 RVA: 0x00002DFC File Offset: 0x00000FFC
	protected override void Start()
	{
		if (!SystemInfo.supportsRenderTextures)
		{
			base.enabled = false;
			return;
		}
		base.Start();
	}

	// Token: 0x0600002D RID: 45 RVA: 0x00002E18 File Offset: 0x00001018
	protected override void OnDisable()
	{
		base.OnDisable();
		global::UnityEngine.Object.DestroyImmediate(this.accumTexture);
	}

	// Token: 0x0600002E RID: 46 RVA: 0x00002E2C File Offset: 0x0000102C
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (this.accumTexture == null || this.accumTexture.width != source.width || this.accumTexture.height != source.height)
		{
			global::UnityEngine.Object.DestroyImmediate(this.accumTexture);
			this.accumTexture = new RenderTexture(source.width, source.height, 0);
			this.accumTexture.hideFlags = HideFlags.HideAndDontSave;
			Graphics.Blit(source, this.accumTexture);
		}
		if (this.extraBlur)
		{
			RenderTexture temporary = RenderTexture.GetTemporary(source.width / 4, source.height / 4, 0);
			Graphics.Blit(this.accumTexture, temporary);
			Graphics.Blit(temporary, this.accumTexture);
			RenderTexture.ReleaseTemporary(temporary);
		}
		this.blurAmount = Mathf.Clamp(this.blurAmount, 0f, 0.92f);
		base.material.SetTexture("_MainTex", this.accumTexture);
		base.material.SetFloat("_AccumOrig", 1f - this.blurAmount);
		Graphics.Blit(source, this.accumTexture, base.material);
		Graphics.Blit(this.accumTexture, destination);
	}

	// Token: 0x04000022 RID: 34
	public float blurAmount = 0.8f;

	// Token: 0x04000023 RID: 35
	public bool extraBlur;

	// Token: 0x04000024 RID: 36
	private RenderTexture accumTexture;
}
