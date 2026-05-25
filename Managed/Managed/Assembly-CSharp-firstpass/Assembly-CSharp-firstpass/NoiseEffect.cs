using System;
using UnityEngine;

// Token: 0x0200000B RID: 11
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Noise")]
[RequireComponent(typeof(Camera))]
public class NoiseEffect : MonoBehaviour
{
	// Token: 0x06000030 RID: 48 RVA: 0x00002FC8 File Offset: 0x000011C8
	protected void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (this.shaderRGB == null || this.shaderYUV == null)
		{
			Debug.Log("Noise shaders are not set up! Disabling noise effect.");
			base.enabled = false;
		}
		else if (!this.shaderRGB.isSupported)
		{
			base.enabled = false;
		}
		else if (!this.shaderYUV.isSupported)
		{
			this.rgbFallback = true;
		}
	}

	// Token: 0x1700000A RID: 10
	// (get) Token: 0x06000031 RID: 49 RVA: 0x00003054 File Offset: 0x00001254
	protected Material material
	{
		get
		{
			if (this.m_MaterialRGB == null)
			{
				this.m_MaterialRGB = new Material(this.shaderRGB);
				this.m_MaterialRGB.hideFlags = HideFlags.HideAndDontSave;
			}
			if (this.m_MaterialYUV == null && !this.rgbFallback)
			{
				this.m_MaterialYUV = new Material(this.shaderYUV);
				this.m_MaterialYUV.hideFlags = HideFlags.HideAndDontSave;
			}
			return (this.rgbFallback || this.monochrome) ? this.m_MaterialRGB : this.m_MaterialYUV;
		}
	}

	// Token: 0x06000032 RID: 50 RVA: 0x000030F4 File Offset: 0x000012F4
	protected void OnDisable()
	{
		if (this.m_MaterialRGB)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_MaterialRGB);
		}
		if (this.m_MaterialYUV)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_MaterialYUV);
		}
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00003138 File Offset: 0x00001338
	private void SanitizeParameters()
	{
		this.grainIntensityMin = Mathf.Clamp(this.grainIntensityMin, 0f, 5f);
		this.grainIntensityMax = Mathf.Clamp(this.grainIntensityMax, 0f, 5f);
		this.scratchIntensityMin = Mathf.Clamp(this.scratchIntensityMin, 0f, 5f);
		this.scratchIntensityMax = Mathf.Clamp(this.scratchIntensityMax, 0f, 5f);
		this.scratchFPS = Mathf.Clamp(this.scratchFPS, 1f, 30f);
		this.scratchJitter = Mathf.Clamp(this.scratchJitter, 0f, 1f);
		this.grainSize = Mathf.Clamp(this.grainSize, 0.1f, 50f);
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00003204 File Offset: 0x00001404
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		this.SanitizeParameters();
		if (this.scratchTimeLeft <= 0f)
		{
			this.scratchTimeLeft = global::UnityEngine.Random.value * 2f / this.scratchFPS;
			this.scratchX = global::UnityEngine.Random.value;
			this.scratchY = global::UnityEngine.Random.value;
		}
		this.scratchTimeLeft -= Time.deltaTime;
		Material material = this.material;
		material.SetTexture("_GrainTex", this.grainTexture);
		material.SetTexture("_ScratchTex", this.scratchTexture);
		float num = 1f / this.grainSize;
		material.SetVector("_GrainOffsetScale", new Vector4(global::UnityEngine.Random.value, global::UnityEngine.Random.value, (float)Screen.width / (float)this.grainTexture.width * num, (float)Screen.height / (float)this.grainTexture.height * num));
		material.SetVector("_ScratchOffsetScale", new Vector4(this.scratchX + global::UnityEngine.Random.value * this.scratchJitter, this.scratchY + global::UnityEngine.Random.value * this.scratchJitter, (float)Screen.width / (float)this.scratchTexture.width, (float)Screen.height / (float)this.scratchTexture.height));
		material.SetVector("_Intensity", new Vector4(global::UnityEngine.Random.Range(this.grainIntensityMin, this.grainIntensityMax), global::UnityEngine.Random.Range(this.scratchIntensityMin, this.scratchIntensityMax), 0f, 0f));
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x04000025 RID: 37
	public bool monochrome = true;

	// Token: 0x04000026 RID: 38
	private bool rgbFallback;

	// Token: 0x04000027 RID: 39
	public float grainIntensityMin = 0.1f;

	// Token: 0x04000028 RID: 40
	public float grainIntensityMax = 0.2f;

	// Token: 0x04000029 RID: 41
	public float grainSize = 2f;

	// Token: 0x0400002A RID: 42
	public float scratchIntensityMin = 0.05f;

	// Token: 0x0400002B RID: 43
	public float scratchIntensityMax = 0.25f;

	// Token: 0x0400002C RID: 44
	public float scratchFPS = 10f;

	// Token: 0x0400002D RID: 45
	public float scratchJitter = 0.01f;

	// Token: 0x0400002E RID: 46
	public Texture grainTexture;

	// Token: 0x0400002F RID: 47
	public Texture scratchTexture;

	// Token: 0x04000030 RID: 48
	public Shader shaderRGB;

	// Token: 0x04000031 RID: 49
	public Shader shaderYUV;

	// Token: 0x04000032 RID: 50
	private Material m_MaterialRGB;

	// Token: 0x04000033 RID: 51
	private Material m_MaterialYUV;

	// Token: 0x04000034 RID: 52
	private float scratchTimeLeft;

	// Token: 0x04000035 RID: 53
	private float scratchX;

	// Token: 0x04000036 RID: 54
	private float scratchY;
}
