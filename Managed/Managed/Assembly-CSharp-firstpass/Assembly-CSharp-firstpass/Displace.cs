using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
[RequireComponent(typeof(WaterBase))]
[ExecuteInEditMode]
public class Displace : MonoBehaviour
{
	// Token: 0x06000051 RID: 81 RVA: 0x000047A4 File Offset: 0x000029A4
	public void Awake()
	{
		if (base.enabled)
		{
			this.OnEnable();
		}
		else
		{
			this.OnDisable();
		}
	}

	// Token: 0x06000052 RID: 82 RVA: 0x000047C4 File Offset: 0x000029C4
	public void OnEnable()
	{
		Shader.EnableKeyword("WATER_VERTEX_DISPLACEMENT_ON");
		Shader.DisableKeyword("WATER_VERTEX_DISPLACEMENT_OFF");
	}

	// Token: 0x06000053 RID: 83 RVA: 0x000047DC File Offset: 0x000029DC
	public void OnDisable()
	{
		Shader.EnableKeyword("WATER_VERTEX_DISPLACEMENT_OFF");
		Shader.DisableKeyword("WATER_VERTEX_DISPLACEMENT_ON");
	}
}
