using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x02000069 RID: 105
	public static class StencilMaterial
	{
		// Token: 0x06000380 RID: 896 RVA: 0x0001031C File Offset: 0x0000E51C
		public static Material Add(Material baseMat, int stencilID)
		{
			if (stencilID <= 0 || baseMat == null)
			{
				return null;
			}
			if (!baseMat.HasProperty("_Stencil"))
			{
				Debug.LogWarning("Material " + baseMat.name + " doesn't have stencil properties", baseMat);
				return null;
			}
			for (int i = 0; i < StencilMaterial.m_List.Count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				if (matEntry.baseMat == baseMat && matEntry.stencilID == stencilID)
				{
					matEntry.count++;
					return matEntry.customMat;
				}
			}
			StencilMaterial.MatEntry matEntry2 = new StencilMaterial.MatEntry();
			matEntry2.count = 1;
			matEntry2.baseMat = baseMat;
			matEntry2.customMat = new Material(baseMat);
			matEntry2.customMat.name = string.Concat(new object[] { "Stencil ", stencilID, " (", baseMat.name, ")" });
			matEntry2.customMat.hideFlags = HideFlags.HideAndDontSave;
			matEntry2.stencilID = stencilID;
			if (baseMat.HasProperty("_StencilComp"))
			{
				matEntry2.customMat.SetInt("_StencilComp", 3);
			}
			matEntry2.customMat.SetInt("_Stencil", stencilID);
			StencilMaterial.m_List.Add(matEntry2);
			return matEntry2.customMat;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001047C File Offset: 0x0000E67C
		public static void Remove(Material customMat)
		{
			if (customMat == null)
			{
				return;
			}
			for (int i = 0; i < StencilMaterial.m_List.Count; i++)
			{
				StencilMaterial.MatEntry matEntry = StencilMaterial.m_List[i];
				if (!(matEntry.customMat != customMat))
				{
					if (--matEntry.count == 0)
					{
						Misc.DestroyImmediate(matEntry.customMat);
						matEntry.baseMat = null;
						StencilMaterial.m_List.RemoveAt(i);
					}
					return;
				}
			}
		}

		// Token: 0x040001C9 RID: 457
		private static List<StencilMaterial.MatEntry> m_List = new List<StencilMaterial.MatEntry>();

		// Token: 0x0200006A RID: 106
		private class MatEntry
		{
			// Token: 0x040001CA RID: 458
			public Material baseMat;

			// Token: 0x040001CB RID: 459
			public Material customMat;

			// Token: 0x040001CC RID: 460
			public int count;

			// Token: 0x040001CD RID: 461
			public int stencilID;
		}
	}
}
