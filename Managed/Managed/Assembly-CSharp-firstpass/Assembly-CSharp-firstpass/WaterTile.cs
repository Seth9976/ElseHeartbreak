using System;
using UnityEngine;

// Token: 0x0200001A RID: 26
[ExecuteInEditMode]
public class WaterTile : MonoBehaviour
{
	// Token: 0x0600006F RID: 111 RVA: 0x00005308 File Offset: 0x00003508
	public void Start()
	{
		this.AcquireComponents();
	}

	// Token: 0x06000070 RID: 112 RVA: 0x00005310 File Offset: 0x00003510
	private void AcquireComponents()
	{
		if (!this.reflection)
		{
			if (base.transform.parent)
			{
				this.reflection = base.transform.parent.GetComponent<PlanarReflection>();
			}
			else
			{
				this.reflection = base.transform.GetComponent<PlanarReflection>();
			}
		}
		if (!this.waterBase)
		{
			if (base.transform.parent)
			{
				this.waterBase = base.transform.parent.GetComponent<WaterBase>();
			}
			else
			{
				this.waterBase = base.transform.GetComponent<WaterBase>();
			}
		}
	}

	// Token: 0x06000071 RID: 113 RVA: 0x000053C0 File Offset: 0x000035C0
	public void OnWillRenderObject()
	{
		if (this.reflection)
		{
			this.reflection.WaterTileBeingRendered(base.transform, Camera.current);
		}
		if (this.waterBase)
		{
			this.waterBase.WaterTileBeingRendered(base.transform, Camera.current);
		}
	}

	// Token: 0x04000073 RID: 115
	public PlanarReflection reflection;

	// Token: 0x04000074 RID: 116
	public WaterBase waterBase;
}
