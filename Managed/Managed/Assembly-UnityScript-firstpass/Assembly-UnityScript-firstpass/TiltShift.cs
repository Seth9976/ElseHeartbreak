using System;
using UnityEngine;

// Token: 0x0200002D RID: 45
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Tilt shift")]
[Serializable]
public class TiltShift : PostEffectsBase
{
	// Token: 0x06000097 RID: 151 RVA: 0x00009584 File Offset: 0x00007784
	public TiltShift()
	{
		this.renderTextureDivider = 2;
		this.blurIterations = 2;
		this.enableForegroundBlur = true;
		this.foregroundBlurIterations = 2;
		this.maxBlurSpread = 1.5f;
		this.focalPoint = 30f;
		this.smoothness = 1.65f;
		this.distance01 = 0.2f;
		this.end01 = 1f;
		this.curve = 1f;
	}

	// Token: 0x06000098 RID: 152 RVA: 0x000095F8 File Offset: 0x000077F8
	public override bool CheckResources()
	{
		this.CheckSupport(true);
		this.tiltShiftMaterial = this.CheckShaderAndCreateMaterial(this.tiltShiftShader, this.tiltShiftMaterial);
		if (!this.isSupported)
		{
			this.ReportAutoDisable();
		}
		return this.isSupported;
	}

	// Token: 0x06000099 RID: 153 RVA: 0x00009634 File Offset: 0x00007834
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (!this.CheckResources())
		{
			Graphics.Blit(source, destination);
		}
		else
		{
			float num = 1f * (float)source.width / (1f * (float)source.height);
			float num2 = 0.001953125f;
			this.renderTextureDivider = ((this.renderTextureDivider >= 1) ? this.renderTextureDivider : 1);
			this.renderTextureDivider = ((this.renderTextureDivider <= 4) ? this.renderTextureDivider : 4);
			this.blurIterations = ((this.blurIterations >= 1) ? this.blurIterations : 0);
			this.blurIterations = ((this.blurIterations <= 4) ? this.blurIterations : 4);
			float num3 = this.camera.WorldToViewportPoint(this.focalPoint * this.camera.transform.forward + this.camera.transform.position).z / this.camera.farClipPlane;
			this.distance01 = num3;
			this.start01 = (float)0;
			this.end01 = 1f;
			this.start01 = Mathf.Min(num3 - float.Epsilon, this.start01);
			this.end01 = Mathf.Max(num3 + float.Epsilon, this.end01);
			this.curve = this.smoothness * this.distance01;
			RenderTexture temporary = RenderTexture.GetTemporary(source.width, source.height, 0);
			RenderTexture temporary2 = RenderTexture.GetTemporary(source.width, source.height, 0);
			RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / this.renderTextureDivider, source.height / this.renderTextureDivider, 0);
			RenderTexture temporary4 = RenderTexture.GetTemporary(source.width / this.renderTextureDivider, source.height / this.renderTextureDivider, 0);
			this.tiltShiftMaterial.SetVector("_SimpleDofParams", new Vector4(this.start01, this.distance01, this.end01, this.curve));
			this.tiltShiftMaterial.SetTexture("_Coc", temporary);
			if (this.enableForegroundBlur)
			{
				Graphics.Blit(source, temporary, this.tiltShiftMaterial, 0);
				Graphics.Blit(temporary, temporary3);
				for (int i = 0; i < this.foregroundBlurIterations; i++)
				{
					this.tiltShiftMaterial.SetVector("offsets", new Vector4((float)0, this.maxBlurSpread * 0.75f * num2, (float)0, (float)0));
					Graphics.Blit(temporary3, temporary4, this.tiltShiftMaterial, 3);
					this.tiltShiftMaterial.SetVector("offsets", new Vector4(this.maxBlurSpread * 0.75f / num * num2, (float)0, (float)0, (float)0));
					Graphics.Blit(temporary4, temporary3, this.tiltShiftMaterial, 3);
				}
				Graphics.Blit(temporary3, temporary2, this.tiltShiftMaterial, 7);
				this.tiltShiftMaterial.SetTexture("_Coc", temporary2);
			}
			else
			{
				RenderTexture.active = temporary;
				GL.Clear(false, true, Color.black);
			}
			Graphics.Blit(source, temporary, this.tiltShiftMaterial, 5);
			this.tiltShiftMaterial.SetTexture("_Coc", temporary);
			Graphics.Blit(source, temporary4);
			for (int j = 0; j < this.blurIterations; j++)
			{
				this.tiltShiftMaterial.SetVector("offsets", new Vector4((float)0, this.maxBlurSpread * 1f * num2, (float)0, (float)0));
				Graphics.Blit(temporary4, temporary3, this.tiltShiftMaterial, 6);
				this.tiltShiftMaterial.SetVector("offsets", new Vector4(this.maxBlurSpread * 1f / num * num2, (float)0, (float)0, (float)0));
				Graphics.Blit(temporary3, temporary4, this.tiltShiftMaterial, 6);
			}
			this.tiltShiftMaterial.SetTexture("_Blurred", temporary4);
			Graphics.Blit(source, destination, this.tiltShiftMaterial, (!this.visualizeCoc) ? 1 : 4);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
			RenderTexture.ReleaseTemporary(temporary3);
			RenderTexture.ReleaseTemporary(temporary4);
		}
	}

	// Token: 0x0600009A RID: 154 RVA: 0x00009A34 File Offset: 0x00007C34
	public override void Main()
	{
	}

	// Token: 0x04000188 RID: 392
	public Shader tiltShiftShader;

	// Token: 0x04000189 RID: 393
	private Material tiltShiftMaterial;

	// Token: 0x0400018A RID: 394
	public int renderTextureDivider;

	// Token: 0x0400018B RID: 395
	public int blurIterations;

	// Token: 0x0400018C RID: 396
	public bool enableForegroundBlur;

	// Token: 0x0400018D RID: 397
	public int foregroundBlurIterations;

	// Token: 0x0400018E RID: 398
	public float maxBlurSpread;

	// Token: 0x0400018F RID: 399
	public float focalPoint;

	// Token: 0x04000190 RID: 400
	public float smoothness;

	// Token: 0x04000191 RID: 401
	public bool visualizeCoc;

	// Token: 0x04000192 RID: 402
	private float start01;

	// Token: 0x04000193 RID: 403
	private float distance01;

	// Token: 0x04000194 RID: 404
	private float end01;

	// Token: 0x04000195 RID: 405
	private float curve;
}
