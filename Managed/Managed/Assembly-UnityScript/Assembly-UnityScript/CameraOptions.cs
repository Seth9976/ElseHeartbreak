using System;
using UnityEngine;

// Token: 0x02000002 RID: 2
[Serializable]
public class CameraOptions
{
	// Token: 0x06000001 RID: 1 RVA: 0x000020EC File Offset: 0x000002EC
	public CameraOptions()
	{
		this.background = Color.black;
		this.farClipping = (float)1000;
	}

	// Token: 0x04000001 RID: 1
	public bool useBackground;

	// Token: 0x04000002 RID: 2
	public Color background;

	// Token: 0x04000003 RID: 3
	public float farClipping;
}
