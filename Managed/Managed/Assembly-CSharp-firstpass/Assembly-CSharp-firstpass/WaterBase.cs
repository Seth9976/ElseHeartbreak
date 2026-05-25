using System;
using UnityEngine;

// Token: 0x02000019 RID: 25
[ExecuteInEditMode]
public class WaterBase : MonoBehaviour
{
	// Token: 0x0600006B RID: 107 RVA: 0x000051D8 File Offset: 0x000033D8
	public void UpdateShader()
	{
		if (this.waterQuality > WaterQuality.Medium)
		{
			this.sharedMaterial.shader.maximumLOD = 501;
		}
		else if (this.waterQuality > WaterQuality.Low)
		{
			this.sharedMaterial.shader.maximumLOD = 301;
		}
		else
		{
			this.sharedMaterial.shader.maximumLOD = 201;
		}
		if (!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.Depth))
		{
			this.edgeBlend = false;
		}
		if (this.edgeBlend)
		{
			Shader.EnableKeyword("WATER_EDGEBLEND_ON");
			Shader.DisableKeyword("WATER_EDGEBLEND_OFF");
			if (Camera.main)
			{
				Camera.main.depthTextureMode |= DepthTextureMode.Depth;
			}
		}
		else
		{
			Shader.EnableKeyword("WATER_EDGEBLEND_OFF");
			Shader.DisableKeyword("WATER_EDGEBLEND_ON");
		}
	}

	// Token: 0x0600006C RID: 108 RVA: 0x000052B4 File Offset: 0x000034B4
	public void WaterTileBeingRendered(Transform tr, Camera currentCam)
	{
		if (currentCam && this.edgeBlend)
		{
			currentCam.depthTextureMode |= DepthTextureMode.Depth;
		}
	}

	// Token: 0x0600006D RID: 109 RVA: 0x000052E8 File Offset: 0x000034E8
	public void Update()
	{
		if (this.sharedMaterial)
		{
			this.UpdateShader();
		}
	}

	// Token: 0x04000070 RID: 112
	public Material sharedMaterial;

	// Token: 0x04000071 RID: 113
	public WaterQuality waterQuality = WaterQuality.High;

	// Token: 0x04000072 RID: 114
	public bool edgeBlend = true;
}
