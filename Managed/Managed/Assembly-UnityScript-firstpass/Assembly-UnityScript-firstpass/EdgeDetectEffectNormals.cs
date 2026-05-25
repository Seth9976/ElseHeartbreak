using System;
using UnityEngine;

// Token: 0x02000020 RID: 32
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Edge Detection (Geometry)")]
[Serializable]
public class EdgeDetectEffectNormals : PostEffectsBase
{
	// Token: 0x0600005B RID: 91 RVA: 0x00007348 File Offset: 0x00005548
	public EdgeDetectEffectNormals()
	{
		this.mode = EdgeDetectMode.SobelDepthThin;
		this.sensitivityDepth = 1f;
		this.sensitivityNormals = 1f;
		this.edgeExp = 1f;
		this.sampleDist = 1f;
		this.edgesOnlyBgColor = Color.white;
		this.oldMode = EdgeDetectMode.SobelDepthThin;
	}

	// Token: 0x0600005C RID: 92 RVA: 0x000073A0 File Offset: 0x000055A0
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.edgeDetectMaterial = this.CheckShaderAndCreateMaterial(this.edgeDetectShader, this.edgeDetectMaterial);
		if (this.mode != this.oldMode)
		{
			this.SetCameraFlag();
		}
		this.oldMode = this.mode;
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x0600005D RID: 93 RVA: 0x00007408 File Offset: 0x00005608
	public override void Start()
	{
		this.oldMode = this.mode;
	}

	// Token: 0x0600005E RID: 94 RVA: 0x00007418 File Offset: 0x00005618
	public virtual void SetCameraFlag()
	{
		if (this.mode > EdgeDetectMode.RobertsCrossDepthNormals)
		{
			this.camera.depthTextureMode = this.camera.depthTextureMode | DepthTextureMode.Depth;
		}
		else
		{
			this.camera.depthTextureMode = this.camera.depthTextureMode | DepthTextureMode.DepthNormals;
		}
	}

	// Token: 0x0600005F RID: 95 RVA: 0x00007468 File Offset: 0x00005668
	public override void OnEnable()
	{
		this.SetCameraFlag();
	}

	// Token: 0x06000060 RID: 96 RVA: 0x00007470 File Offset: 0x00005670
	[ImageEffectOpaque]
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			Vector2 vector = new Vector2(this.sensitivityDepth, this.sensitivityNormals);
			this.edgeDetectMaterial.SetVector("_Sensitivity", new Vector4(vector.x, vector.y, 1f, vector.y));
			this.edgeDetectMaterial.SetFloat("_BgFade", this.edgesOnly);
			this.edgeDetectMaterial.SetFloat("_SampleDistance", this.sampleDist);
			this.edgeDetectMaterial.SetVector("_BgColor", this.edgesOnlyBgColor);
			this.edgeDetectMaterial.SetFloat("_Exponent", this.edgeExp);
			Graphics.Blit(source, destination, this.edgeDetectMaterial, (int)this.mode);
		}
	}

	// Token: 0x06000061 RID: 97 RVA: 0x00007548 File Offset: 0x00005748
	public override void Main()
	{
	}

	// Token: 0x04000132 RID: 306
	public EdgeDetectMode mode;

	// Token: 0x04000133 RID: 307
	public float sensitivityDepth;

	// Token: 0x04000134 RID: 308
	public float sensitivityNormals;

	// Token: 0x04000135 RID: 309
	public float edgeExp;

	// Token: 0x04000136 RID: 310
	public float sampleDist;

	// Token: 0x04000137 RID: 311
	public float edgesOnly;

	// Token: 0x04000138 RID: 312
	public Color edgesOnlyBgColor;

	// Token: 0x04000139 RID: 313
	public Shader edgeDetectShader;

	// Token: 0x0400013A RID: 314
	private Material edgeDetectMaterial;

	// Token: 0x0400013B RID: 315
	private EdgeDetectMode oldMode;
}
