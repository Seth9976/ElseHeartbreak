using System;
using UnityEngine;

// Token: 0x0200000F RID: 15
[ExecuteInEditMode]
[RequireComponent(typeof(Camera))]
[AddComponentMenu("Image Effects/Camera Info")]
[Serializable]
public class CameraInfo : MonoBehaviour
{
	// Token: 0x06000018 RID: 24 RVA: 0x000039FC File Offset: 0x00001BFC
	public virtual void Main()
	{
	}

	// Token: 0x04000080 RID: 128
	public DepthTextureMode currentDepthMode;

	// Token: 0x04000081 RID: 129
	public RenderingPath currentRenderPath;

	// Token: 0x04000082 RID: 130
	public int recognizedPostFxCount;
}
