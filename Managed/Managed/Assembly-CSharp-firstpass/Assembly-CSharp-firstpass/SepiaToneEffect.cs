using System;
using UnityEngine;

// Token: 0x0200000E RID: 14
[AddComponentMenu("Image Effects/Sepia Tone")]
[ExecuteInEditMode]
public class SepiaToneEffect : ImageEffectBase
{
	// Token: 0x0600003E RID: 62 RVA: 0x00003828 File Offset: 0x00001A28
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Graphics.Blit(source, destination, base.material);
	}
}
