using System;
using UnityEngine;

// Token: 0x02000021 RID: 33
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Fisheye")]
[Serializable]
public class Fisheye : PostEffectsBase
{
	// Token: 0x06000062 RID: 98 RVA: 0x0000754C File Offset: 0x0000574C
	public Fisheye()
	{
		this.strengthX = 0.05f;
		this.strengthY = 0.05f;
	}

	// Token: 0x06000063 RID: 99 RVA: 0x0000756C File Offset: 0x0000576C
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.fisheyeMaterial = this.CheckShaderAndCreateMaterial(this.fishEyeShader, this.fisheyeMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000064 RID: 100 RVA: 0x000075A8 File Offset: 0x000057A8
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			float num = 0.15625f;
			float num2 = (float)source.width * 1f / ((float)source.height * 1f);
			this.fisheyeMaterial.SetVector("intensity", new Vector4(this.strengthX * num2 * num, this.strengthY * num, this.strengthX * num2 * num, this.strengthY * num));
			Graphics.Blit(source, destination, this.fisheyeMaterial);
		}
	}

	// Token: 0x06000065 RID: 101 RVA: 0x00007634 File Offset: 0x00005834
	public override void Main()
	{
	}

	// Token: 0x0400013C RID: 316
	public float strengthX;

	// Token: 0x0400013D RID: 317
	public float strengthY;

	// Token: 0x0400013E RID: 318
	public Shader fishEyeShader;

	// Token: 0x0400013F RID: 319
	private Material fisheyeMaterial;
}
