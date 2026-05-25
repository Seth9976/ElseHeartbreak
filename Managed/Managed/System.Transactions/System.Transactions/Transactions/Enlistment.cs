using System;

namespace System.Transactions
{
	/// <summary>Facilitates communication between an enlisted transaction participant and the transaction manager during the final phase of the transaction.</summary>
	// Token: 0x0200000D RID: 13
	public class Enlistment
	{
		// Token: 0x0600001C RID: 28 RVA: 0x000022C4 File Offset: 0x000004C4
		internal Enlistment()
		{
			this.done = false;
		}

		/// <summary>Indicates that the transaction participant has completed its work.</summary>
		// Token: 0x0600001D RID: 29 RVA: 0x000022D4 File Offset: 0x000004D4
		public void Done()
		{
			this.done = true;
		}

		// Token: 0x04000027 RID: 39
		internal bool done;
	}
}
