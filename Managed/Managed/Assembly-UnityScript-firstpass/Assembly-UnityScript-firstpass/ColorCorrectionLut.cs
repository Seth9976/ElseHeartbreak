using System;
using UnityEngine;

// Token: 0x02000014 RID: 20
[AddComponentMenu("Image Effects/Color Correction (3D Lookup Texture)")]
[ExecuteInEditMode]
[Serializable]
public class ColorCorrectionLut : PostEffectsBase
{
	// Token: 0x0600002E RID: 46 RVA: 0x00004BB4 File Offset: 0x00002DB4
	public ColorCorrectionLut()
	{
		this.basedOnTempTex = string.Empty;
	}

	// Token: 0x0600002F RID: 47 RVA: 0x00004BC8 File Offset: 0x00002DC8
	public override bool CheckResources()
	{
		this.CheckSupport(false);
		this.material = this.CheckShaderAndCreateMaterial(this.shader, this.material);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000030 RID: 48 RVA: 0x00004C04 File Offset: 0x00002E04
	public virtual void OnDisable()
	{
		if (this.material)
		{
			global::UnityEngine.Object.DestroyImmediate(this.material);
			this.material = null;
		}
	}

	// Token: 0x06000031 RID: 49 RVA: 0x00004C34 File Offset: 0x00002E34
	public virtual void OnDestroy()
	{
		if (this.converted3DLut)
		{
			global::UnityEngine.Object.DestroyImmediate(this.converted3DLut);
		}
		this.converted3DLut = null;
	}

	// Token: 0x06000032 RID: 50 RVA: 0x00004C64 File Offset: 0x00002E64
	public virtual void SetIdentityLut()
	{
		int num = 16;
		Color[] array = new Color[num * num * num];
		float num2 = 1f / (1f * (float)num - 1f);
		for (int i = 0; i < num; i++)
		{
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < num; k++)
				{
					array[i + j * num + k * num * num] = new Color((float)i * 1f * num2, (float)j * 1f * num2, (float)k * 1f * num2, 1f);
				}
			}
		}
		if (this.converted3DLut)
		{
			global::UnityEngine.Object.DestroyImmediate(this.converted3DLut);
		}
		this.converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
		this.converted3DLut.SetPixels(array);
		this.converted3DLut.Apply();
		this.basedOnTempTex = string.Empty;
	}

	// Token: 0x06000033 RID: 51 RVA: 0x00004D64 File Offset: 0x00002F64
	public virtual bool ValidDimensions(Texture2D tex2d)
	{
		bool flag;
		if (!tex2d)
		{
			flag = false;
		}
		else
		{
			int height = tex2d.height;
			flag = height == Mathf.FloorToInt(Mathf.Sqrt((float)tex2d.width));
		}
		return flag;
	}

	// Token: 0x06000034 RID: 52 RVA: 0x00004DA8 File Offset: 0x00002FA8
	public virtual void Convert(Texture2D temp2DTex, string path)
	{
		if (temp2DTex)
		{
			int num = temp2DTex.width * temp2DTex.height;
			num = temp2DTex.height;
			if (!this.ValidDimensions(temp2DTex))
			{
				Debug.LogWarning("The given 2D texture " + temp2DTex.name + " cannot be used as a 3D LUT.");
				this.basedOnTempTex = string.Empty;
			}
			else
			{
				Color[] pixels = temp2DTex.GetPixels();
				Color[] array = new Color[pixels.Length];
				for (int i = 0; i < num; i++)
				{
					for (int j = 0; j < num; j++)
					{
						for (int k = 0; k < num; k++)
						{
							int num2 = num - j - 1;
							array[i + j * num + k * num * num] = pixels[k * num + i + num2 * num * num];
						}
					}
				}
				if (this.converted3DLut)
				{
					global::UnityEngine.Object.DestroyImmediate(this.converted3DLut);
				}
				this.converted3DLut = new Texture3D(num, num, num, TextureFormat.ARGB32, false);
				this.converted3DLut.SetPixels(array);
				this.converted3DLut.Apply();
				this.basedOnTempTex = path;
			}
		}
		else
		{
			Debug.LogError("Couldn't color correct with 3D LUT texture. Image Effect will be disabled.");
		}
	}

	// Token: 0x06000035 RID: 53 RVA: 0x00004EF4 File Offset: 0x000030F4
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.converted3DLut == null)
			{
				this.SetIdentityLut();
			}
			int width = this.converted3DLut.width;
			this.converted3DLut.wrapMode = TextureWrapMode.Clamp;
			this.material.SetFloat("_Scale", (float)(width - 1) / (1f * (float)width));
			this.material.SetFloat("_Offset", 1f / (2f * (float)width));
			this.material.SetTexture("_ClutTex", this.converted3DLut);
			Graphics.Blit(source, destination, this.material, (QualitySettings.activeColorSpace != ColorSpace.Linear) ? 0 : 1);
		}
	}

	// Token: 0x06000036 RID: 54 RVA: 0x00004FB8 File Offset: 0x000031B8
	public override void Main()
	{
	}

	// Token: 0x040000C1 RID: 193
	public Shader shader;

	// Token: 0x040000C2 RID: 194
	private Material material;

	// Token: 0x040000C3 RID: 195
	public Texture3D converted3DLut;

	// Token: 0x040000C4 RID: 196
	public string basedOnTempTex;
}
