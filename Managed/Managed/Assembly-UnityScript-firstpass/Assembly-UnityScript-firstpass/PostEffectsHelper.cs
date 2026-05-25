using System;
using UnityEngine;

// Token: 0x02000026 RID: 38
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[Serializable]
public class PostEffectsHelper : MonoBehaviour
{
	// Token: 0x06000081 RID: 129 RVA: 0x0000875C File Offset: 0x0000695C
	public virtual void Start()
	{
	}

	// Token: 0x06000082 RID: 130 RVA: 0x00008760 File Offset: 0x00006960
	public virtual void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		Debug.Log("OnRenderImage in Helper called ...");
	}

	// Token: 0x06000083 RID: 131 RVA: 0x0000876C File Offset: 0x0000696C
	public static void DrawLowLevelPlaneAlignedWithCamera(float dist, RenderTexture source, RenderTexture dest, Material material, Camera cameraForProjectionMatrix)
	{
		RenderTexture.active = dest;
		material.SetTexture("_MainTex", source);
		bool flag = true;
		GL.PushMatrix();
		GL.LoadIdentity();
		GL.LoadProjectionMatrix(cameraForProjectionMatrix.projectionMatrix);
		float num = cameraForProjectionMatrix.fieldOfView * 0.5f * 0.017453292f;
		float num2 = Mathf.Cos(num) / Mathf.Sin(num);
		float aspect = cameraForProjectionMatrix.aspect;
		float num3 = aspect / -num2;
		float num4 = aspect / num2;
		float num5 = 1f / -num2;
		float num6 = 1f / num2;
		float num7 = 1f;
		num3 *= dist * num7;
		num4 *= dist * num7;
		num5 *= dist * num7;
		num6 *= dist * num7;
		float num8 = -dist;
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			GL.Begin(7);
			float num9 = 0f;
			float num10 = 0f;
			if (flag)
			{
				num9 = 1f;
				num10 = (float)0;
			}
			else
			{
				num9 = (float)0;
				num10 = 1f;
			}
			GL.TexCoord2((float)0, num9);
			GL.Vertex3(num3, num5, num8);
			GL.TexCoord2(1f, num9);
			GL.Vertex3(num4, num5, num8);
			GL.TexCoord2(1f, num10);
			GL.Vertex3(num4, num6, num8);
			GL.TexCoord2((float)0, num10);
			GL.Vertex3(num3, num6, num8);
			GL.End();
		}
		GL.PopMatrix();
	}

	// Token: 0x06000084 RID: 132 RVA: 0x000088E0 File Offset: 0x00006AE0
	public static void DrawBorder(RenderTexture dest, Material material)
	{
		float num = 0f;
		float num2 = 0f;
		float num3 = 0f;
		float num4 = 0f;
		RenderTexture.active = dest;
		bool flag = true;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			float num5 = 0f;
			float num6 = 0f;
			if (flag)
			{
				num5 = 1f;
				num6 = (float)0;
			}
			else
			{
				num5 = (float)0;
				num6 = 1f;
			}
			num = (float)0;
			num2 = (float)0 + 1f / ((float)dest.width * 1f);
			num3 = (float)0;
			num4 = 1f;
			GL.Begin(7);
			GL.TexCoord2((float)0, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2((float)0, num6);
			GL.Vertex3(num, num4, 0.1f);
			num = 1f - 1f / ((float)dest.width * 1f);
			num2 = 1f;
			num3 = (float)0;
			num4 = 1f;
			GL.TexCoord2((float)0, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2((float)0, num6);
			GL.Vertex3(num, num4, 0.1f);
			num = (float)0;
			num2 = 1f;
			num3 = (float)0;
			num4 = (float)0 + 1f / ((float)dest.height * 1f);
			GL.TexCoord2((float)0, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2((float)0, num6);
			GL.Vertex3(num, num4, 0.1f);
			num = (float)0;
			num2 = 1f;
			num3 = 1f - 1f / ((float)dest.height * 1f);
			num4 = 1f;
			GL.TexCoord2((float)0, num5);
			GL.Vertex3(num, num3, 0.1f);
			GL.TexCoord2(1f, num5);
			GL.Vertex3(num2, num3, 0.1f);
			GL.TexCoord2(1f, num6);
			GL.Vertex3(num2, num4, 0.1f);
			GL.TexCoord2((float)0, num6);
			GL.Vertex3(num, num4, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}

	// Token: 0x06000085 RID: 133 RVA: 0x00008B88 File Offset: 0x00006D88
	public static void DrawLowLevelQuad(float x1, float x2, float y1, float y2, RenderTexture source, RenderTexture dest, Material material)
	{
		RenderTexture.active = dest;
		material.SetTexture("_MainTex", source);
		bool flag = true;
		GL.PushMatrix();
		GL.LoadOrtho();
		for (int i = 0; i < material.passCount; i++)
		{
			material.SetPass(i);
			GL.Begin(7);
			float num = 0f;
			float num2 = 0f;
			if (flag)
			{
				num = 1f;
				num2 = (float)0;
			}
			else
			{
				num = (float)0;
				num2 = 1f;
			}
			GL.TexCoord2((float)0, num);
			GL.Vertex3(x1, y1, 0.1f);
			GL.TexCoord2(1f, num);
			GL.Vertex3(x2, y1, 0.1f);
			GL.TexCoord2(1f, num2);
			GL.Vertex3(x2, y2, 0.1f);
			GL.TexCoord2((float)0, num2);
			GL.Vertex3(x1, y2, 0.1f);
			GL.End();
		}
		GL.PopMatrix();
	}

	// Token: 0x06000086 RID: 134 RVA: 0x00008C7C File Offset: 0x00006E7C
	public virtual void Main()
	{
	}
}
