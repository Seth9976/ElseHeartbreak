using System;
using UnityEngine;

// Token: 0x02000010 RID: 16
[AddComponentMenu("Image Effects/Camera Motion Blur")]
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[Serializable]
public class CameraMotionBlur : PostEffectsBase
{
	// Token: 0x06000019 RID: 25 RVA: 0x00003A00 File Offset: 0x00001C00
	public CameraMotionBlur()
	{
		this.filterType = CameraMotionBlur.MotionBlurFilter.Reconstruction;
		this.previewScale = Vector3.one;
		this.rotationScale = 1f;
		this.maxVelocity = 8f;
		this.maxNumSamples = 17;
		this.minVelocity = 0.1f;
		this.velocityScale = 0.375f;
		this.softZDistance = 0.005f;
		this.velocityDownsample = 1;
		this.showVelocityScale = 1f;
		this.prevFrameForward = Vector3.forward;
		this.prevFrameRight = Vector3.right;
		this.prevFrameUp = Vector3.up;
		this.prevFramePos = Vector3.zero;
	}

	// Token: 0x0600001B RID: 27 RVA: 0x00003AB4 File Offset: 0x00001CB4
	private void CalculateViewProjection()
	{
		Matrix4x4 worldToCameraMatrix = this.camera.worldToCameraMatrix;
		Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(this.camera.projectionMatrix, true);
		this.currentViewProjMat = gpuprojectionMatrix * worldToCameraMatrix;
	}

	// Token: 0x0600001C RID: 28 RVA: 0x00003AEC File Offset: 0x00001CEC
	public override void Start()
	{
		this.CheckResources();
		this.wasActive = this.gameObject.activeInHierarchy;
		this.CalculateViewProjection();
		this.Remember();
		this.prevFrameCount = -1;
		this.wasActive = false;
	}

	// Token: 0x0600001D RID: 29 RVA: 0x00003B2C File Offset: 0x00001D2C
	public override void OnEnable()
	{
		this.camera.depthTextureMode = this.camera.depthTextureMode | DepthTextureMode.Depth;
	}

	// Token: 0x0600001E RID: 30 RVA: 0x00003B54 File Offset: 0x00001D54
	public virtual void OnDisable()
	{
		if (null != this.motionBlurMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(this.motionBlurMaterial);
			this.motionBlurMaterial = null;
		}
		if (null != this.dx11MotionBlurMaterial)
		{
			global::UnityEngine.Object.DestroyImmediate(this.dx11MotionBlurMaterial);
			this.dx11MotionBlurMaterial = null;
		}
		if (null != this.tmpCam)
		{
			global::UnityEngine.Object.DestroyImmediate(this.tmpCam);
			this.tmpCam = null;
		}
	}

	// Token: 0x0600001F RID: 31 RVA: 0x00003BCC File Offset: 0x00001DCC
	public override bool CheckResources()
	{
		this.CheckSupport(true, true);
		this.motionBlurMaterial = this.CheckShaderAndCreateMaterial(this.shader, this.motionBlurMaterial);
		if (this.supportDX11 && this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11)
		{
			this.dx11MotionBlurMaterial = this.CheckShaderAndCreateMaterial(this.dx11MotionBlurShader, this.dx11MotionBlurMaterial);
		}
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000020 RID: 32 RVA: 0x00003C40 File Offset: 0x00001E40
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			if (this.filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
			{
				this.StartFrame();
			}
			RenderTextureFormat renderTextureFormat = ((!SystemInfo.SupportsRenderTextureFormat(RenderTextureFormat.RGHalf)) ? RenderTextureFormat.ARGBHalf : RenderTextureFormat.RGHalf);
			RenderTexture temporary = RenderTexture.GetTemporary(this.divRoundUp(source.width, this.velocityDownsample), this.divRoundUp(source.height, this.velocityDownsample), 0, renderTextureFormat);
			this.maxVelocity = Mathf.Max(2f, this.maxVelocity);
			float num = this.maxVelocity;
			bool flag = false;
			if (this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11 && this.dx11MotionBlurMaterial == null)
			{
				flag = true;
			}
			int num2;
			int num3;
			if (this.filterType == CameraMotionBlur.MotionBlurFilter.Reconstruction || flag)
			{
				this.maxVelocity = Mathf.Min(this.maxVelocity, (float)CameraMotionBlur.MAX_RADIUS);
				num2 = this.divRoundUp(temporary.width, (int)this.maxVelocity);
				num3 = this.divRoundUp(temporary.height, (int)this.maxVelocity);
				num = (float)(temporary.width / num2);
			}
			else
			{
				num2 = this.divRoundUp(temporary.width, (int)this.maxVelocity);
				num3 = this.divRoundUp(temporary.height, (int)this.maxVelocity);
				num = (float)(temporary.width / num2);
			}
			RenderTexture temporary2 = RenderTexture.GetTemporary(num2, num3, 0, renderTextureFormat);
			RenderTexture temporary3 = RenderTexture.GetTemporary(num2, num3, 0, renderTextureFormat);
			temporary.filterMode = FilterMode.Point;
			temporary2.filterMode = FilterMode.Point;
			temporary3.filterMode = FilterMode.Point;
			if (this.noiseTexture)
			{
				this.noiseTexture.filterMode = FilterMode.Point;
			}
			source.wrapMode = TextureWrapMode.Clamp;
			temporary.wrapMode = TextureWrapMode.Clamp;
			temporary3.wrapMode = TextureWrapMode.Clamp;
			temporary2.wrapMode = TextureWrapMode.Clamp;
			this.CalculateViewProjection();
			if (this.gameObject.activeInHierarchy && !this.wasActive)
			{
				this.Remember();
			}
			this.wasActive = this.gameObject.activeInHierarchy;
			Matrix4x4 matrix4x = Matrix4x4.Inverse(this.currentViewProjMat);
			this.motionBlurMaterial.SetMatrix("_InvViewProj", matrix4x);
			this.motionBlurMaterial.SetMatrix("_PrevViewProj", this.prevViewProjMat);
			this.motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", this.prevViewProjMat * matrix4x);
			this.motionBlurMaterial.SetFloat("_MaxVelocity", num);
			this.motionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", num);
			this.motionBlurMaterial.SetFloat("_MinVelocity", this.minVelocity);
			this.motionBlurMaterial.SetFloat("_VelocityScale", this.velocityScale);
			this.motionBlurMaterial.SetTexture("_NoiseTex", this.noiseTexture);
			this.motionBlurMaterial.SetTexture("_VelTex", temporary);
			this.motionBlurMaterial.SetTexture("_NeighbourMaxTex", temporary3);
			this.motionBlurMaterial.SetTexture("_TileTexDebug", temporary2);
			if (this.preview)
			{
				Matrix4x4 worldToCameraMatrix = this.camera.worldToCameraMatrix;
				Matrix4x4 identity = Matrix4x4.identity;
				identity.SetTRS(this.previewScale * 0.25f, Quaternion.identity, Vector3.one);
				Matrix4x4 gpuprojectionMatrix = GL.GetGPUProjectionMatrix(this.camera.projectionMatrix, true);
				this.prevViewProjMat = gpuprojectionMatrix * identity * worldToCameraMatrix;
				this.motionBlurMaterial.SetMatrix("_PrevViewProj", this.prevViewProjMat);
				this.motionBlurMaterial.SetMatrix("_ToPrevViewProjCombined", this.prevViewProjMat * matrix4x);
			}
			if (this.filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
			{
				Vector4 zero = Vector4.zero;
				float num4 = Vector3.Dot(this.transform.up, Vector3.up);
				Vector3 vector = this.prevFramePos - this.transform.position;
				float magnitude = vector.magnitude;
				float num5 = Vector3.Angle(this.transform.up, this.prevFrameUp) / this.camera.fieldOfView * ((float)source.width * 0.75f);
				zero.x = this.rotationScale * num5;
				num5 = Vector3.Angle(this.transform.forward, this.prevFrameForward) / this.camera.fieldOfView * ((float)source.width * 0.75f);
				zero.y = this.rotationScale * num4 * num5;
				num5 = Vector3.Angle(this.transform.forward, this.prevFrameForward) / this.camera.fieldOfView * ((float)source.width * 0.75f);
				zero.z = this.rotationScale * (1f - num4) * num5;
				if (magnitude > 1E-45f && this.movementScale > 1E-45f)
				{
					zero.w = this.movementScale * Vector3.Dot(this.transform.forward, vector) * ((float)source.width * 0.5f);
					zero.x += this.movementScale * Vector3.Dot(this.transform.up, vector) * ((float)source.width * 0.5f);
					zero.y += this.movementScale * Vector3.Dot(this.transform.right, vector) * ((float)source.width * 0.5f);
				}
				if (this.preview)
				{
					this.motionBlurMaterial.SetVector("_BlurDirectionPacked", new Vector4(this.previewScale.y, this.previewScale.x, (float)0, this.previewScale.z) * 0.5f * this.camera.fieldOfView);
				}
				else
				{
					this.motionBlurMaterial.SetVector("_BlurDirectionPacked", zero);
				}
			}
			else
			{
				Graphics.Blit(source, temporary, this.motionBlurMaterial, 0);
				Camera camera = null;
				if (this.excludeLayers.value != 0)
				{
					camera = this.GetTmpCam();
				}
				if (camera && this.excludeLayers.value != 0 && this.replacementClear && this.replacementClear.isSupported)
				{
					camera.targetTexture = temporary;
					camera.cullingMask = this.excludeLayers;
					camera.RenderWithShader(this.replacementClear, string.Empty);
				}
			}
			if (!this.preview && Time.frameCount != this.prevFrameCount)
			{
				this.prevFrameCount = Time.frameCount;
				this.Remember();
			}
			source.filterMode = FilterMode.Bilinear;
			if (this.showVelocity)
			{
				this.motionBlurMaterial.SetFloat("_DisplayVelocityScale", this.showVelocityScale);
				Graphics.Blit(temporary, destination, this.motionBlurMaterial, 1);
			}
			else if (this.filterType == CameraMotionBlur.MotionBlurFilter.ReconstructionDX11 && !flag)
			{
				this.dx11MotionBlurMaterial.SetFloat("_MaxVelocity", num);
				this.dx11MotionBlurMaterial.SetFloat("_MinVelocity", this.minVelocity);
				this.dx11MotionBlurMaterial.SetFloat("_VelocityScale", this.velocityScale);
				this.dx11MotionBlurMaterial.SetTexture("_NoiseTex", this.noiseTexture);
				this.dx11MotionBlurMaterial.SetTexture("_VelTex", temporary);
				this.dx11MotionBlurMaterial.SetTexture("_NeighbourMaxTex", temporary3);
				this.dx11MotionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));
				this.dx11MotionBlurMaterial.SetFloat("_MaxRadiusOrKInPaper", num);
				this.maxNumSamples = 2 * (this.maxNumSamples / 2) + 1;
				this.dx11MotionBlurMaterial.SetFloat("_SampleCount", (float)this.maxNumSamples * 1f);
				Graphics.Blit(temporary, temporary2, this.dx11MotionBlurMaterial, 0);
				Graphics.Blit(temporary2, temporary3, this.dx11MotionBlurMaterial, 1);
				Graphics.Blit(source, destination, this.dx11MotionBlurMaterial, 2);
			}
			else if (this.filterType == CameraMotionBlur.MotionBlurFilter.Reconstruction || flag)
			{
				this.motionBlurMaterial.SetFloat("_SoftZDistance", Mathf.Max(0.00025f, this.softZDistance));
				Graphics.Blit(temporary, temporary2, this.motionBlurMaterial, 2);
				Graphics.Blit(temporary2, temporary3, this.motionBlurMaterial, 3);
				Graphics.Blit(source, destination, this.motionBlurMaterial, 4);
			}
			else if (this.filterType == CameraMotionBlur.MotionBlurFilter.CameraMotion)
			{
				Graphics.Blit(source, destination, this.motionBlurMaterial, 6);
			}
			else
			{
				Graphics.Blit(source, destination, this.motionBlurMaterial, 5);
			}
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
		}
	}

	// Token: 0x06000021 RID: 33 RVA: 0x000044C8 File Offset: 0x000026C8
	public virtual void Remember()
	{
		this.prevViewProjMat = this.currentViewProjMat;
		this.prevFrameForward = this.transform.forward;
		this.prevFrameRight = this.transform.right;
		this.prevFrameUp = this.transform.up;
		this.prevFramePos = this.transform.position;
	}

	// Token: 0x06000022 RID: 34 RVA: 0x00004528 File Offset: 0x00002728
	public virtual Camera GetTmpCam()
	{
		if (this.tmpCam == null)
		{
			string text = "_" + this.camera.name + "_MotionBlurTmpCam";
			GameObject gameObject = GameObject.Find(text);
			if (null == gameObject)
			{
				this.tmpCam = new GameObject(text, new Type[] { typeof(Camera) });
			}
			else
			{
				this.tmpCam = gameObject;
			}
		}
		this.tmpCam.hideFlags = HideFlags.DontSave;
		this.tmpCam.transform.position = this.camera.transform.position;
		this.tmpCam.transform.rotation = this.camera.transform.rotation;
		this.tmpCam.transform.localScale = this.camera.transform.localScale;
		this.tmpCam.camera.CopyFrom(this.camera);
		this.tmpCam.camera.enabled = false;
		this.tmpCam.camera.depthTextureMode = DepthTextureMode.None;
		this.tmpCam.camera.clearFlags = CameraClearFlags.Nothing;
		return this.tmpCam.camera;
	}

	// Token: 0x06000023 RID: 35 RVA: 0x00004664 File Offset: 0x00002864
	public virtual void StartFrame()
	{
		this.prevFramePos = Vector3.Slerp(this.prevFramePos, this.transform.position, 0.75f);
	}

	// Token: 0x06000024 RID: 36 RVA: 0x00004694 File Offset: 0x00002894
	public virtual int divRoundUp(int x, int d)
	{
		return (x + d - 1) / d;
	}

	// Token: 0x06000025 RID: 37 RVA: 0x000046A0 File Offset: 0x000028A0
	public override void Main()
	{
	}

	// Token: 0x04000083 RID: 131
	[NonSerialized]
	public static int MAX_RADIUS = (int)10f;

	// Token: 0x04000084 RID: 132
	public CameraMotionBlur.MotionBlurFilter filterType;

	// Token: 0x04000085 RID: 133
	public bool preview;

	// Token: 0x04000086 RID: 134
	public Vector3 previewScale;

	// Token: 0x04000087 RID: 135
	public float movementScale;

	// Token: 0x04000088 RID: 136
	public float rotationScale;

	// Token: 0x04000089 RID: 137
	public float maxVelocity;

	// Token: 0x0400008A RID: 138
	public int maxNumSamples;

	// Token: 0x0400008B RID: 139
	public float minVelocity;

	// Token: 0x0400008C RID: 140
	public float velocityScale;

	// Token: 0x0400008D RID: 141
	public float softZDistance;

	// Token: 0x0400008E RID: 142
	public int velocityDownsample;

	// Token: 0x0400008F RID: 143
	public LayerMask excludeLayers;

	// Token: 0x04000090 RID: 144
	private GameObject tmpCam;

	// Token: 0x04000091 RID: 145
	public Shader shader;

	// Token: 0x04000092 RID: 146
	public Shader dx11MotionBlurShader;

	// Token: 0x04000093 RID: 147
	public Shader replacementClear;

	// Token: 0x04000094 RID: 148
	private Material motionBlurMaterial;

	// Token: 0x04000095 RID: 149
	private Material dx11MotionBlurMaterial;

	// Token: 0x04000096 RID: 150
	public Texture2D noiseTexture;

	// Token: 0x04000097 RID: 151
	public bool showVelocity;

	// Token: 0x04000098 RID: 152
	public float showVelocityScale;

	// Token: 0x04000099 RID: 153
	private Matrix4x4 currentViewProjMat;

	// Token: 0x0400009A RID: 154
	private Matrix4x4 prevViewProjMat;

	// Token: 0x0400009B RID: 155
	private int prevFrameCount;

	// Token: 0x0400009C RID: 156
	private bool wasActive;

	// Token: 0x0400009D RID: 157
	private Vector3 prevFrameForward;

	// Token: 0x0400009E RID: 158
	private Vector3 prevFrameRight;

	// Token: 0x0400009F RID: 159
	private Vector3 prevFrameUp;

	// Token: 0x040000A0 RID: 160
	private Vector3 prevFramePos;

	// Token: 0x02000011 RID: 17
	[Serializable]
	public enum MotionBlurFilter
	{
		// Token: 0x040000A2 RID: 162
		CameraMotion,
		// Token: 0x040000A3 RID: 163
		LocalBlur,
		// Token: 0x040000A4 RID: 164
		Reconstruction,
		// Token: 0x040000A5 RID: 165
		ReconstructionDX11
	}
}
