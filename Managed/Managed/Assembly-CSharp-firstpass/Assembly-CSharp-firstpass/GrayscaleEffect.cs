using System;
using UnityEngine;

// Token: 0x02000007 RID: 7
[AddComponentMenu("Image Effects/Grayscale")]
[ExecuteInEditMode]
public class GrayscaleEffect : ImageEffectBase
{
	// Token: 0x06000022 RID: 34 RVA: 0x00002C24 File Offset: 0x00000E24
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetTexture("_RampTex", this.textureRamp);
		base.material.SetFloat("_RampOffset", this.rampOffset);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x0400001E RID: 30
	public Texture textureRamp;

	// Token: 0x0400001F RID: 31
	public float rampOffset;
}
