using System;
using UnityEngine;

// Token: 0x02000005 RID: 5
[AddComponentMenu("Image Effects/Edge Detection (Color)")]
[ExecuteInEditMode]
public class EdgeDetectEffect : ImageEffectBase
{
	// Token: 0x06000016 RID: 22 RVA: 0x00002794 File Offset: 0x00000994
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		base.material.SetFloat("_Treshold", this.threshold * this.threshold);
		Graphics.Blit(source, destination, base.material);
	}

	// Token: 0x04000013 RID: 19
	public float threshold = 0.2f;
}
