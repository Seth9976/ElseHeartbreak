using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000080 RID: 128
	public struct LayerMask
	{
		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000AF80 File Offset: 0x00009180
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000AF88 File Offset: 0x00009188
		public int value
		{
			get
			{
				return this.m_Mask;
			}
			set
			{
				this.m_Mask = value;
			}
		}

		// Token: 0x060002B6 RID: 694
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string LayerToName(int layer);

		// Token: 0x060002B7 RID: 695
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int NameToLayer(string layerName);

		// Token: 0x060002B8 RID: 696 RVA: 0x0000AF94 File Offset: 0x00009194
		public static int GetMask(params string[] layerNames)
		{
			int num = 0;
			foreach (string text in layerNames)
			{
				int num2 = LayerMask.NameToLayer(text);
				if (num2 != 0)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000AFDC File Offset: 0x000091DC
		public static implicit operator int(LayerMask mask)
		{
			return mask.m_Mask;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000AFE8 File Offset: 0x000091E8
		public static implicit operator LayerMask(int intVal)
		{
			LayerMask layerMask;
			layerMask.m_Mask = intVal;
			return layerMask;
		}

		// Token: 0x040001C7 RID: 455
		private int m_Mask;
	}
}
