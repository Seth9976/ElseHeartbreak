using System;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x020004FD RID: 1277
	internal class FixupRecord : BaseFixupRecord
	{
		// Token: 0x0600331C RID: 13084 RVA: 0x000A57FC File Offset: 0x000A39FC
		public FixupRecord(ObjectRecord objectToBeFixed, MemberInfo member, ObjectRecord objectRequired)
			: base(objectToBeFixed, objectRequired)
		{
			this._member = member;
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000A5810 File Offset: 0x000A3A10
		protected override void FixupImpl(ObjectManager manager)
		{
			this.ObjectToBeFixed.SetMemberValue(manager, this._member, this.ObjectRequired.ObjectInstance);
		}

		// Token: 0x0400153F RID: 5439
		public MemberInfo _member;
	}
}
