using System;

namespace UnityEngine
{
	// Token: 0x0200008B RID: 139
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class RequireComponent : Attribute
	{
		// Token: 0x060002E5 RID: 741 RVA: 0x0000B074 File Offset: 0x00009274
		public RequireComponent(Type requiredComponent)
		{
			this.m_Type0 = requiredComponent;
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000B084 File Offset: 0x00009284
		public RequireComponent(Type requiredComponent, Type requiredComponent2)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000B09C File Offset: 0x0000929C
		public RequireComponent(Type requiredComponent, Type requiredComponent2, Type requiredComponent3)
		{
			this.m_Type0 = requiredComponent;
			this.m_Type1 = requiredComponent2;
			this.m_Type2 = requiredComponent3;
		}

		// Token: 0x0400021B RID: 539
		public Type m_Type0;

		// Token: 0x0400021C RID: 540
		public Type m_Type1;

		// Token: 0x0400021D RID: 541
		public Type m_Type2;
	}
}
