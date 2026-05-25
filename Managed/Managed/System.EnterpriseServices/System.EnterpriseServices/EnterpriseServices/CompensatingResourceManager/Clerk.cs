using System;

namespace System.EnterpriseServices.CompensatingResourceManager
{
	/// <summary>Writes records of transactional actions to a log.</summary>
	// Token: 0x02000057 RID: 87
	public sealed class Clerk
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> class.</summary>
		/// <param name="compensator">The name of the compensator. </param>
		/// <param name="description">The description of the compensator. </param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.CompensatorOptions" /> values. </param>
		// Token: 0x06000151 RID: 337 RVA: 0x00002D34 File Offset: 0x00000F34
		[MonoTODO]
		public Clerk(string compensator, string description, CompensatorOptions flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.Clerk" /> class.</summary>
		/// <param name="compensator">A type that represents the compensator. </param>
		/// <param name="description">The description of the compensator. </param>
		/// <param name="flags">A bitwise combination of the <see cref="T:System.EnterpriseServices.CompensatingResourceManager.CompensatorOptions" /> values. </param>
		// Token: 0x06000152 RID: 338 RVA: 0x00002D44 File Offset: 0x00000F44
		[MonoTODO]
		public Clerk(Type compensator, string description, CompensatorOptions flags)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the number of log records.</summary>
		/// <returns>The number of log records.</returns>
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00002D54 File Offset: 0x00000F54
		public int LogRecordCount
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets a value representing the transaction unit of work (UOW).</summary>
		/// <returns>A GUID representing the UOW.</returns>
		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00002D5C File Offset: 0x00000F5C
		public string TransactionUOW
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00002D64 File Offset: 0x00000F64
		[MonoTODO]
		~Clerk()
		{
			throw new NotImplementedException();
		}

		/// <summary>Forces all log records to disk.</summary>
		// Token: 0x06000156 RID: 342 RVA: 0x00002DA0 File Offset: 0x00000FA0
		[MonoTODO]
		public void ForceLog()
		{
			throw new NotImplementedException();
		}

		/// <summary>Performs an immediate abort call on the transaction.</summary>
		// Token: 0x06000157 RID: 343 RVA: 0x00002DA8 File Offset: 0x00000FA8
		[MonoTODO]
		public void ForceTransactionToAbort()
		{
			throw new NotImplementedException();
		}

		/// <summary>Does not deliver the last log record that was written by this instance of this interface.</summary>
		// Token: 0x06000158 RID: 344 RVA: 0x00002DB0 File Offset: 0x00000FB0
		[MonoTODO]
		public void ForgetLogRecord()
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes unstructured log records to the log.</summary>
		/// <param name="record">The log record to write to the log. </param>
		// Token: 0x06000159 RID: 345 RVA: 0x00002DB8 File Offset: 0x00000FB8
		[MonoTODO]
		public void WriteLogRecord(object record)
		{
			throw new NotImplementedException();
		}
	}
}
