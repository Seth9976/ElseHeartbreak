using System;
using UnityEngine;

// Token: 0x02000008 RID: 8
[RequireComponent(typeof(Camera))]
[AddComponentMenu("")]
public class ImageEffectBase : MonoBehaviour
{
	// Token: 0x06000024 RID: 36 RVA: 0x00002C74 File Offset: 0x00000E74
	protected virtual void Start()
	{
		if (!SystemInfo.supportsImageEffects)
		{
			base.enabled = false;
			return;
		}
		if (!this.shader || !this.shader.isSupported)
		{
			base.enabled = false;
		}
	}

	// Token: 0x17000009 RID: 9
	// (get) Token: 0x06000025 RID: 37 RVA: 0x00002CBC File Offset: 0x00000EBC
	protected Material material
	{
		get
		{
			if (this.m_Material == null)
			{
				this.m_Material = new Material(this.shader);
				this.m_Material.hideFlags = HideFlags.HideAndDontSave;
			}
			return this.m_Material;
		}
	}

	// Token: 0x06000026 RID: 38 RVA: 0x00002CF4 File Offset: 0x00000EF4
	protected virtual void OnDisable()
	{
		if (this.m_Material)
		{
			global::UnityEngine.Object.DestroyImmediate(this.m_Material);
		}
	}

	// Token: 0x04000020 RID: 32
	public Shader shader;

	// Token: 0x04000021 RID: 33
	private Material m_Material;
}
