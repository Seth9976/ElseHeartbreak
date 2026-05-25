using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000FD RID: 253
	[Serializable]
	public sealed class GUISettings
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x060008AD RID: 2221 RVA: 0x000143A4 File Offset: 0x000125A4
		// (set) Token: 0x060008AE RID: 2222 RVA: 0x000143AC File Offset: 0x000125AC
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.m_DoubleClickSelectsWord;
			}
			set
			{
				this.m_DoubleClickSelectsWord = value;
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060008AF RID: 2223 RVA: 0x000143B8 File Offset: 0x000125B8
		// (set) Token: 0x060008B0 RID: 2224 RVA: 0x000143C0 File Offset: 0x000125C0
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.m_TripleClickSelectsLine;
			}
			set
			{
				this.m_TripleClickSelectsLine = value;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060008B1 RID: 2225 RVA: 0x000143CC File Offset: 0x000125CC
		// (set) Token: 0x060008B2 RID: 2226 RVA: 0x000143D4 File Offset: 0x000125D4
		public Color cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
			set
			{
				this.m_CursorColor = value;
			}
		}

		// Token: 0x060008B3 RID: 2227
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetCursorFlashSpeed();

		// Token: 0x170001E0 RID: 480
		// (get) Token: 0x060008B4 RID: 2228 RVA: 0x000143E0 File Offset: 0x000125E0
		// (set) Token: 0x060008B5 RID: 2229 RVA: 0x00014400 File Offset: 0x00012600
		public float cursorFlashSpeed
		{
			get
			{
				if (this.m_CursorFlashSpeed >= 0f)
				{
					return this.m_CursorFlashSpeed;
				}
				return GUISettings.Internal_GetCursorFlashSpeed();
			}
			set
			{
				this.m_CursorFlashSpeed = value;
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060008B6 RID: 2230 RVA: 0x0001440C File Offset: 0x0001260C
		// (set) Token: 0x060008B7 RID: 2231 RVA: 0x00014414 File Offset: 0x00012614
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				this.m_SelectionColor = value;
			}
		}

		// Token: 0x0400035B RID: 859
		[SerializeField]
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x0400035C RID: 860
		[SerializeField]
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x0400035D RID: 861
		[SerializeField]
		private Color m_CursorColor = Color.white;

		// Token: 0x0400035E RID: 862
		[SerializeField]
		private float m_CursorFlashSpeed = -1f;

		// Token: 0x0400035F RID: 863
		[SerializeField]
		private Color m_SelectionColor = new Color(0.5f, 0.5f, 1f);
	}
}
