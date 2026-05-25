using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
[AddComponentMenu("Image Effects/Twirl")]
[ExecuteInEditMode]
public class TwirlEffect : ImageEffectBase
{
	// Token: 0x06000040 RID: 64 RVA: 0x00003878 File Offset: 0x00001A78
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		ImageEffects.RenderDistortion(base.material, source, destination, this.angle, this.center, this.radius);
	}

	// Token: 0x04000046 RID: 70
	public Vector2 radius = new Vector2(0.3f, 0.3f);

	// Token: 0x04000047 RID: 71
	public float angle = 50f;

	// Token: 0x04000048 RID: 72
	public Vector2 center = new Vector2(0.5f, 0.5f);
}
