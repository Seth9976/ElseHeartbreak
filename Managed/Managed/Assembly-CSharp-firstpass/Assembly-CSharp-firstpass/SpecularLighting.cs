using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
[RequireComponent(typeof(WaterBase))]
[ExecuteInEditMode]
public class SpecularLighting : MonoBehaviour
{
	// Token: 0x06000068 RID: 104 RVA: 0x00005104 File Offset: 0x00003304
	public void Start()
	{
		this.waterBase = (WaterBase)base.gameObject.GetComponent(typeof(WaterBase));
	}

	// Token: 0x06000069 RID: 105 RVA: 0x00005134 File Offset: 0x00003334
	public void Update()
	{
		if (!this.waterBase)
		{
			this.waterBase = (WaterBase)base.gameObject.GetComponent(typeof(WaterBase));
		}
		if (this.specularLight && this.waterBase.sharedMaterial)
		{
			this.waterBase.sharedMaterial.SetVector("_WorldLightDir", this.specularLight.transform.forward);
		}
	}

	// Token: 0x0400006A RID: 106
	public Transform specularLight;

	// Token: 0x0400006B RID: 107
	private WaterBase waterBase;
}
