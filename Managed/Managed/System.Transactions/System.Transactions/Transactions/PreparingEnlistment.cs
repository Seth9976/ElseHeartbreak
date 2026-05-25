using System;

namespace System.Transactions
{
	/// <summary>Facilitates communication between an enlisted transaction participant and the transaction manager during the Prepare phase of the transaction.</summary>
	// Token: 0x02000017 RID: 23
	public class PreparingEnlistment : Enlistment
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000022E0 File Offset: 0x000004E0
		internal PreparingEnlistment(Transaction tx, IEnlistmentNotification enlisted)
		{
			this.tx = tx;
			this.enlisted = enlisted;
		}

		/// <summary>Indicates that the transaction should be rolled back.</summary>
		// Token: 0x0600002C RID: 44 RVA: 0x000022F8 File Offset: 0x000004F8
		public void ForceRollback()
		{
			this.ForceRollback(null);
		}

		/// <summary>Indicates that the transaction should be rolled back.</summary>
		/// <param name="e">An explanation of why a rollback is triggered.</param>
		// Token: 0x0600002D RID: 45 RVA: 0x00002304 File Offset: 0x00000504
		[MonoTODO]
		public void ForceRollback(Exception ex)
		{
			this.tx.Rollback(ex, this.enlisted);
		}

		/// <summary>Indicates that the transaction can be committed.</summary>
		// Token: 0x0600002E RID: 46 RVA: 0x00002318 File Offset: 0x00000518
		[MonoTODO]
		public void Prepared()
		{
			this.prepared = true;
		}

		/// <summary>Gets the recovery information of an enlistment.</summary>
		/// <returns>The recovery information of an enlistment.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt to get recovery information inside a volatile enlistment, which does not generate any recovery information.</exception>
		// Token: 0x0600002F RID: 47 RVA: 0x00002324 File Offset: 0x00000524
		[MonoTODO]
		public byte[] RecoveryInformation()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000030 RID: 48 RVA: 0x0000232C File Offset: 0x0000052C
		internal bool IsPrepared
		{
			get
			{
				return this.prepared;
			}
		}

		// Token: 0x04000037 RID: 55
		private bool prepared;

		// Token: 0x04000038 RID: 56
		private Transaction tx;

		// Token: 0x04000039 RID: 57
		private IEnlistmentNotification enlisted;
	}
}
