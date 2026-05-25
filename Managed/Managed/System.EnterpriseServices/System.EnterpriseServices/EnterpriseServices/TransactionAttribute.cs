using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the type of transaction that is available to the attributed object. Permissible values are members of the <see cref="T:System.EnterpriseServices.TransactionOption" /> enumeration.</summary>
	// Token: 0x02000050 RID: 80
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class TransactionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.TransactionAttribute" /> class, setting the component's requested transaction type to <see cref="F:System.EnterpriseServices.TransactionOption.Required" />.</summary>
		// Token: 0x06000147 RID: 327 RVA: 0x00002CB0 File Offset: 0x00000EB0
		public TransactionAttribute()
			: this(TransactionOption.Required)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.TransactionAttribute" /> class, specifying the transaction type.</summary>
		/// <param name="val">The specified transaction type, a <see cref="T:System.EnterpriseServices.TransactionOption" /> value. </param>
		// Token: 0x06000148 RID: 328 RVA: 0x00002CBC File Offset: 0x00000EBC
		public TransactionAttribute(TransactionOption val)
		{
			this.isolation = TransactionIsolationLevel.Serializable;
			this.timeout = -1;
			this.val = val;
		}

		/// <summary>Gets or sets the transaction isolation level.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionIsolationLevel" /> values.</returns>
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000149 RID: 329 RVA: 0x00002CDC File Offset: 0x00000EDC
		// (set) Token: 0x0600014A RID: 330 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public TransactionIsolationLevel Isolation
		{
			get
			{
				return this.isolation;
			}
			set
			{
				this.isolation = value;
			}
		}

		/// <summary>Gets or sets the time-out for this transaction.</summary>
		/// <returns>The transaction time-out in seconds.</returns>
		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00002CF0 File Offset: 0x00000EF0
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00002CF8 File Offset: 0x00000EF8
		public int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.EnterpriseServices.TransactionOption" /> value for the transaction, optionally disabling the transaction service.</summary>
		/// <returns>The specified transaction type, a <see cref="T:System.EnterpriseServices.TransactionOption" /> value.</returns>
		// Token: 0x17000067 RID: 103
		// (get) Token: 0x0600014D RID: 333 RVA: 0x00002D04 File Offset: 0x00000F04
		public TransactionOption Value
		{
			get
			{
				return this.val;
			}
		}

		// Token: 0x0400008C RID: 140
		private TransactionIsolationLevel isolation;

		// Token: 0x0400008D RID: 141
		private int timeout;

		// Token: 0x0400008E RID: 142
		private TransactionOption val;
	}
}
