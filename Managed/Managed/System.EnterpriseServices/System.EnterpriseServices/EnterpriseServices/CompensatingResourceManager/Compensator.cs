using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Represents the base class for all Compensating Resource Manager (CRM) Compensators.</summary>
	// Token: 0x0200005A RID: 90
	public class Compensator : ServicedComponent
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Compensator" /> class.</summary>
		// Token: 0x06000169 RID: 361 RVA: 0x00002EA4 File Offset: 0x000010A4
		[MonoTODO]
		public Compensator()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a value representing the Compensating Resource Manager (CRM) <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> object.</summary>
		/// <returns>The <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> object.</returns>
		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00002EB4 File Offset: 0x000010B4
		public Clerk Clerk
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Delivers a log record to the Compensating Resource Manager (CRM) Compensator during the abort phase.</summary>
		/// <returns>true if the delivered record should be forgotten; otherwise, false.</returns>
		/// <param name="rec">The log record to be delivered. </param>
		// Token: 0x0600016B RID: 363 RVA: 0x00002EBC File Offset: 0x000010BC
		[MonoTODO]
		public virtual bool AbortRecord(LogRecord rec)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator of the abort phase of the transaction completion, and the upcoming delivery of records.</summary>
		/// <param name="fRecovery">true to begin abort phase; otherwise, false. </param>
		// Token: 0x0600016C RID: 364 RVA: 0x00002EC4 File Offset: 0x000010C4
		[MonoTODO]
		public virtual void BeginAbort(bool fRecovery)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator of the commit phase of the transaction completion and the upcoming delivery of records.</summary>
		/// <param name="fRecovery">true to begin commit phase; otherwise, false. </param>
		// Token: 0x0600016D RID: 365 RVA: 0x00002ECC File Offset: 0x000010CC
		[MonoTODO]
		public virtual void BeginCommit(bool fRecovery)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator of the prepare phase of the transaction completion and the upcoming delivery of records.</summary>
		// Token: 0x0600016E RID: 366 RVA: 0x00002ED4 File Offset: 0x000010D4
		[MonoTODO]
		public virtual void BeginPrepare()
		{
			throw new NotImplementedException();
		}

		/// <summary>Delivers a log record in forward order during the commit phase.</summary>
		/// <returns>true if the delivered record should be forgotten; otherwise, false.</returns>
		/// <param name="rec">The log record to forward. </param>
		// Token: 0x0600016F RID: 367 RVA: 0x00002EDC File Offset: 0x000010DC
		[MonoTODO]
		public virtual bool CommitRecord(LogRecord rec)
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator that it has received all the log records available during the abort phase.</summary>
		// Token: 0x06000170 RID: 368 RVA: 0x00002EE4 File Offset: 0x000010E4
		[MonoTODO]
		public virtual void EndAbort()
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator that it has delivered all the log records available during the commit phase.</summary>
		// Token: 0x06000171 RID: 369 RVA: 0x00002EEC File Offset: 0x000010EC
		[MonoTODO]
		public virtual void EndCommit()
		{
			throw new NotImplementedException();
		}

		/// <summary>Notifies the Compensating Resource Manager (CRM) Compensator that it has had all the log records available during the prepare phase.</summary>
		/// <returns>true if successful; otherwise, false.</returns>
		// Token: 0x06000172 RID: 370 RVA: 0x00002EF4 File Offset: 0x000010F4
		[MonoTODO]
		public virtual bool EndPrepare()
		{
			throw new NotImplementedException();
		}

		/// <summary>Delivers a log record in forward order during the prepare phase.</summary>
		/// <returns>true if the delivered record should be forgotten; otherwise, false.</returns>
		/// <param name="rec">The log record to forward. </param>
		// Token: 0x06000173 RID: 371 RVA: 0x00002EFC File Offset: 0x000010FC
		[MonoTODO]
		public virtual bool PrepareRecord(LogRecord rec)
		{
			throw new NotImplementedException();
		}
	}
}
