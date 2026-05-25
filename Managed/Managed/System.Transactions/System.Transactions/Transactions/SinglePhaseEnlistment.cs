using System;

namespace System.Transactions
{
	/// <summary>Provides a set of callbacks that facilitate communication between a participant enlisted for Single Phase Commit and the transaction manager when the <see cref="M:System.Transactions.ISinglePhaseNotification.SinglePhaseCommit(System.Transactions.SinglePhaseEnlistment)" /> notification is received.</summary>
	// Token: 0x02000018 RID: 24
	public class SinglePhaseEnlistment : Enlistment
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00002334 File Offset: 0x00000534
		internal SinglePhaseEnlistment(Transaction tx, ISinglePhaseNotification enlisted)
		{
			this.tx = tx;
			this.enlisted = enlisted;
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the transaction should be rolled back.</summary>
		// Token: 0x06000032 RID: 50 RVA: 0x0000234C File Offset: 0x0000054C
		public void Aborted()
		{
			this.Aborted(null);
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the transaction should be rolled back, and provides an explanation.</summary>
		/// <param name="e">An explanation of why a rollback is initiated.</param>
		// Token: 0x06000033 RID: 51 RVA: 0x00002358 File Offset: 0x00000558
		public void Aborted(Exception e)
		{
			this.tx.Rollback(e, this.enlisted);
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the SinglePhaseCommit was successful.</summary>
		// Token: 0x06000034 RID: 52 RVA: 0x0000236C File Offset: 0x0000056C
		[MonoTODO]
		public void Committed()
		{
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the status of the transaction is in doubt.</summary>
		// Token: 0x06000035 RID: 53 RVA: 0x00002370 File Offset: 0x00000570
		[MonoTODO("Not implemented")]
		public void InDoubt()
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents a callback that is used to indicate to the transaction manager that the status of the transaction is in doubt, and provides an explanation.</summary>
		/// <param name="e">An explanation of why the transaction is in doubt.</param>
		// Token: 0x06000036 RID: 54 RVA: 0x00002378 File Offset: 0x00000578
		[MonoTODO("Not implemented")]
		public void InDoubt(Exception e)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400003A RID: 58
		private Transaction tx;

		// Token: 0x0400003B RID: 59
		private ISinglePhaseNotification enlisted;
	}
}
