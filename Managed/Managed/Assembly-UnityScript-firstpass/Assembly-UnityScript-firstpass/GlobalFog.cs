using System;
using UnityEngine;

// Token: 0x02000022 RID: 34
[AddComponentMenu("Image Effects/Global Fog")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[Serializable]
public class GlobalFog : PostEffectsBase
{
	// Token: 0x06000066 RID: 102 RVA: 0x00007638 File Offset: 0x00005838
	public GlobalFog()
	{
		this.fogMode = GlobalFog.FogMode.AbsoluteYAndDistance;
		this.CAMERA_NEAR = 0.5f;
		this.CAMERA_FAR = 50f;
		this.CAMERA_FOV = 60f;
		this.CAMERA_ASPECT_RATIO = 1.333333f;
		this.startDistance = 200f;
		this.globalDensity = 1f;
		this.heightScale = 100f;
		this.globalFogColor = Color.grey;
	}

	// Token: 0x06000067 RID: 103 RVA: 0x000076AC File Offset: 0x000058AC
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.fogMaterial = this.CheckShaderAndCreateMaterial(this.fogShader, this.fogMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000068 RID: 104 RVA: 0x000076E8 File Offset: 0x000058E8
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			this.CAMERA_NEAR = this.camera.nearClipPlane;
			this.CAMERA_FAR = this.camera.farClipPlane;
			this.CAMERA_FOV = this.camera.fieldOfView;
			this.CAMERA_ASPECT_RATIO = this.camera.aspect;
			Matrix4x4 identity = Matrix4x4.identity;
			Vector4 vector = default(Vector4);
			Vector3 vector2 = default(Vector3);
			float num = this.CAMERA_FOV * 0.5f;
			Vector3 vector3 = this.camera.transform.right * this.CAMERA_NEAR * Mathf.Tan(num * 0.017453292f) * this.CAMERA_ASPECT_RATIO;
			Vector3 vector4 = this.camera.transform.up * this.CAMERA_NEAR * Mathf.Tan(num * 0.017453292f);
			Vector3 vector5 = this.camera.transform.forward * this.CAMERA_NEAR - vector3 + vector4;
			float num2 = vector5.magnitude * this.CAMERA_FAR / this.CAMERA_NEAR;
			vector5.Normalize();
			vector5 *= num2;
			Vector3 vector6 = this.camera.transform.forward * this.CAMERA_NEAR + vector3 + vector4;
			vector6.Normalize();
			vector6 *= num2;
			Vector3 vector7 = this.camera.transform.forward * this.CAMERA_NEAR + vector3 - vector4;
			vector7.Normalize();
			vector7 *= num2;
			Vector3 vector8 = this.camera.transform.forward * this.CAMERA_NEAR - vector3 - vector4;
			vector8.Normalize();
			vector8 *= num2;
			identity.SetRow(0, vector5);
			identity.SetRow(1, vector6);
			identity.SetRow(2, vector7);
			identity.SetRow(3, vector8);
			this.fogMaterial.SetMatrix("_FrustumCornersWS", identity);
			this.fogMaterial.SetVector("_CameraWS", this.camera.transform.position);
			this.fogMaterial.SetVector("_StartDistance", new Vector4(1f / this.startDistance, num2 - this.startDistance));
			this.fogMaterial.SetVector("_Y", new Vector4(this.height, 1f / this.heightScale));
			this.fogMaterial.SetFloat("_GlobalDensity", this.globalDensity * 0.01f);
			this.fogMaterial.SetColor("_FogColor", this.globalFogColor);
			GlobalFog.CustomGraphicsBlit(source, destination, this.fogMaterial, (int)this.fogMode);
		}
	}

	// Token: 0x06000069 RID: 105 RVA: 0x000079F0 File Offset: 0x00005BF0
	public static void CustomGraphicsBlit(RenderTexture source, RenderTexture dest, Material fxMaterial, int passNr)
	{
		RenderTexture.active = dest;
		fxMaterial.SetTexture("_MainTex", source);
		GL.PushMatrix();
		GL.LoadOrtho();
		fxMaterial.SetPass(passNr);
		GL.Begin(7);
		GL.MultiTexCoord2(0, (float)0, (float)0);
		GL.Vertex3((float)0, (float)0, 3f);
		GL.MultiTexCoord2(0, 1f, (float)0);
		GL.Vertex3(1f, (float)0, 2f);
		GL.MultiTexCoord2(0, 1f, 1f);
		GL.Vertex3(1f, 1f, 1f);
		GL.MultiTexCoord2(0, (float)0, 1f);
		GL.Vertex3((float)0, 1f, (float)0);
		GL.End();
		GL.PopMatrix();
	}

	// Token: 0x0600006A RID: 106 RVA: 0x00007AA8 File Offset: 0x00005CA8
	public override void Main()
	{
	}

	// Token: 0x04000140 RID: 320
	public GlobalFog.FogMode fogMode;

	// Token: 0x04000141 RID: 321
	private float CAMERA_NEAR;

	// Token: 0x04000142 RID: 322
	private float CAMERA_FAR;

	// Token: 0x04000143 RID: 323
	private float CAMERA_FOV;

	// Token: 0x04000144 RID: 324
	private float CAMERA_ASPECT_RATIO;

	// Token: 0x04000145 RID: 325
	public float startDistance;

	// Token: 0x04000146 RID: 326
	public float globalDensity;

	// Token: 0x04000147 RID: 327
	public float heightScale;

	// Token: 0x04000148 RID: 328
	public float height;

	// Token: 0x04000149 RID: 329
	public Color globalFogColor;

	// Token: 0x0400014A RID: 330
	public Shader fogShader;

	// Token: 0x0400014B RID: 331
	private Material fogMaterial;

	// Token: 0x02000023 RID: 35
	[Serializable]
	public enum FogMode
	{
		// Token: 0x0400014D RID: 333
		AbsoluteYAndDistance,
		// Token: 0x0400014E RID: 334
		AbsoluteY,
		// Token: 0x0400014F RID: 335
		Distance,
		// Token: 0x04000150 RID: 336
		RelativeYAndDistance
	}
}
