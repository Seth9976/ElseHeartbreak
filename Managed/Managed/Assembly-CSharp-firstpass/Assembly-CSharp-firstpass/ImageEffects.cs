using System;
using UnityEngine;

// Token: 0x02000009 RID: 9
[AddComponentMenu("")]
public class ImageEffects
{
	// Token: 0x06000028 RID: 40 RVA: 0x00002D1C File Offset: 0x00000F1C
	public static void RenderDistortion(Material material, RenderTexture source, RenderTexture destination, float angle, Vector2 center, Vector2 radius)
	{
		bool flag = source.texelSize.y < 0f;
		if (flag)
		{
			center.y = 1f - center.y;
			angle = -angle;
		}
		Matrix4x4 matrix4x = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0f, 0f, angle), Vector3.one);
		material.SetMatrix("_RotationMatrix", matrix4x);
		material.SetVector("_CenterRadius", new Vector4(center.x, center.y, radius.x, radius.y));
		material.SetFloat("_Angle", angle * 0.017453292f);
		Graphics.Blit(source, destination, material);
	}

	// Token: 0x06000029 RID: 41 RVA: 0x00002DD0 File Offset: 0x00000FD0
	[Obsolete("Use Graphics.Blit(source,dest) instead")]
	public static void Blit(RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, dest);
	}

	// Token: 0x0600002A RID: 42 RVA: 0x00002DDC File Offset: 0x00000FDC
	[Obsolete("Use Graphics.Blit(source, destination, material) instead")]
	public static void BlitWithMaterial(Material material, RenderTexture source, RenderTexture dest)
	{
		Graphics.Blit(source, dest, material);
	}
}
