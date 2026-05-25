using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020004FE RID: 1278
	internal class DelayedFixupRecord : BaseFixupRecord
	{
		// Token: 0x0600331E RID: 13086 RVA: 0x000A5830 File Offset: 0x000A3A30
		public DelayedFixupRecord(ObjectRecord objectToBeFixed, string memberName, ObjectRecord objectRequired)
			: base(objectToBeFixed, objectRequired)
		{
			this._memberName = memberName;
		}

		// Token: 0x0600331F RID: 13087 RVA: 0x000A5844 File Offset: 0x000A3A44
		protected override void FixupImpl(ObjectManager manager)
		{
			this.ObjectToBeFixed.SetMemberValue(manager, this._memberName, this.ObjectRequired.ObjectInstance);
		}

		// Token: 0x04001540 RID: 5440
		public string _memberName;
	}
}
