using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	public struct Navigation
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000299 RID: 665 RVA: 0x0000CA0C File Offset: 0x0000AC0C
		// (set) Token: 0x0600029A RID: 666 RVA: 0x0000CA14 File Offset: 0x0000AC14
		public Navigation.Mode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000CA20 File Offset: 0x0000AC20
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000CA28 File Offset: 0x0000AC28
		public Selectable selectOnUp
		{
			get
			{
				return this.m_SelectOnUp;
			}
			set
			{
				this.m_SelectOnUp = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000CA34 File Offset: 0x0000AC34
		// (set) Token: 0x0600029E RID: 670 RVA: 0x0000CA3C File Offset: 0x0000AC3C
		public Selectable selectOnDown
		{
			get
			{
				return this.m_SelectOnDown;
			}
			set
			{
				this.m_SelectOnDown = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000CA48 File Offset: 0x0000AC48
		// (set) Token: 0x060002A0 RID: 672 RVA: 0x0000CA50 File Offset: 0x0000AC50
		public Selectable selectOnLeft
		{
			get
			{
				return this.m_SelectOnLeft;
			}
			set
			{
				this.m_SelectOnLeft = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x0000CA5C File Offset: 0x0000AC5C
		// (set) Token: 0x060002A2 RID: 674 RVA: 0x0000CA64 File Offset: 0x0000AC64
		public Selectable selectOnRight
		{
			get
			{
				return this.m_SelectOnRight;
			}
			set
			{
				this.m_SelectOnRight = value;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x0000CA70 File Offset: 0x0000AC70
		public static Navigation defaultNavigation
		{
			get
			{
				return new Navigation
				{
					m_Mode = Navigation.Mode.Automatic
				};
			}
		}

		// Token: 0x0400015C RID: 348
		[FormerlySerializedAs("mode")]
		[SerializeField]
		private Navigation.Mode m_Mode;

		// Token: 0x0400015D RID: 349
		[SerializeField]
		[FormerlySerializedAs("selectOnUp")]
		private Selectable m_SelectOnUp;

		// Token: 0x0400015E RID: 350
		[FormerlySerializedAs("selectOnDown")]
		[SerializeField]
		private Selectable m_SelectOnDown;

		// Token: 0x0400015F RID: 351
		[FormerlySerializedAs("selectOnLeft")]
		[SerializeField]
		private Selectable m_SelectOnLeft;

		// Token: 0x04000160 RID: 352
		[SerializeField]
		[FormerlySerializedAs("selectOnRight")]
		private Selectable m_SelectOnRight;

		// Token: 0x02000057 RID: 87
		[Flags]
		public enum Mode
		{
			// Token: 0x04000162 RID: 354
			None = 0,
			// Token: 0x04000163 RID: 355
			Horizontal = 1,
			// Token: 0x04000164 RID: 356
			Vertical = 2,
			// Token: 0x04000165 RID: 357
			Automatic = 3,
			// Token: 0x04000166 RID: 358
			Explicit = 4
		}
	}
}
