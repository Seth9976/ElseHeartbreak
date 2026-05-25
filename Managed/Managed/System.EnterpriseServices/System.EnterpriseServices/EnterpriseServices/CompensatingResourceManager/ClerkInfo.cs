using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Contains information describing an active Compensating Resource Manager (CRM) Clerk object.</summary>
	// Token: 0x02000058 RID: 88
	public sealed class ClerkInfo
	{
		// Token: 0x0600015A RID: 346 RVA: 0x00002DC0 File Offset: 0x00000FC0
		internal ClerkInfo()
		{
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00002DC8 File Offset: 0x00000FC8
		[MonoTODO]
		~ClerkInfo()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the activity ID of the current Compensating Resource Manager (CRM) Worker.</summary>
		/// <returns>Gets the activity ID of the current Compensating Resource Manager (CRM) Worker.</returns>
		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600015C RID: 348 RVA: 0x00002E04 File Offset: 0x00001004
		[MonoTODO]
		public string ActivityId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets <see cref="F:System.Runtime.InteropServices.UnmanagedType.IUnknown" /> for the current Clerk.</summary>
		/// <returns>
		///   <see cref="F:System.Runtime.InteropServices.UnmanagedType.IUnknown" /> for the current Clerk.</returns>
		// Token: 0x1700006C RID: 108
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00002E0C File Offset: 0x0000100C
		[MonoTODO]
		public Clerk Clerk
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the ProgId of the Compensating Resource Manager (CRM) Compensator for the current CRM Clerk.</summary>
		/// <returns>The ProgId of the CRM Compensator for the current CRM Clerk.</returns>
		// Token: 0x1700006D RID: 109
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00002E14 File Offset: 0x00001014
		[MonoTODO]
		public string Compensator
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the description of the Compensating Resource Manager (CRM) Compensator for the current CRM Clerk. The description string is the string that was provided by the ICrmLogControl::RegisterCompensator method.</summary>
		/// <returns>The description of the CRM Compensator for the current CRM Clerk.</returns>
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00002E1C File Offset: 0x0000101C
		[MonoTODO]
		public string Description
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the instance class ID (CLSID) of the current Compensating Resource Manager (CRM) Clerk.</summary>
		/// <returns>The instance CLSID of the current CRM Clerk.</returns>
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000160 RID: 352 RVA: 0x00002E24 File Offset: 0x00001024
		[MonoTODO]
		public string InstanceId
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the unit of work (UOW) of the transaction for the current Compensating Resource Manager (CRM) Clerk.</summary>
		/// <returns>The UOW of the transaction for the current CRM Clerk.</returns>
		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00002E2C File Offset: 0x0000102C
		[MonoTODO]
		public string TransactionUOW
		{
			get
			{
				throw new NotImplementedException();
			}
		}
	}
}
