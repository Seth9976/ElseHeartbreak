using System;

namespace System.Transactions
{
	/// <summary>Provides additional information regarding a transaction.</summary>
	// Token: 0x0200001F RID: 31
	public class TransactionInformation
	{
		// Token: 0x06000071 RID: 113 RVA: 0x00002B40 File Offset: 0x00000D40
		internal TransactionInformation()
		{
			this.status = TransactionStatus.Active;
			this.creation_time = DateTime.Now.ToUniversalTime();
			this.local_id = Guid.NewGuid().ToString() + ":1";
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00002B98 File Offset: 0x00000D98
		private TransactionInformation(TransactionInformation other)
		{
			this.local_id = other.local_id;
			this.dtcId = other.dtcId;
			this.creation_time = other.creation_time;
			this.status = other.status;
		}

		/// <summary>Gets the creation time of the transaction.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the creation time of the transaction.</returns>
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002BE8 File Offset: 0x00000DE8
		public DateTime CreationTime
		{
			get
			{
				return this.creation_time;
			}
		}

		/// <summary>Gets a unique identifier of the escalated transaction.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that contains the unique identifier of the escalated transaction.</returns>
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002BF0 File Offset: 0x00000DF0
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00002BF8 File Offset: 0x00000DF8
		public Guid DistributedIdentifier
		{
			get
			{
				return this.dtcId;
			}
			internal set
			{
				this.dtcId = value;
			}
		}

		/// <summary>Gets a unique identifier of the transaction.</summary>
		/// <returns>A unique identifier of the transaction.</returns>
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002C04 File Offset: 0x00000E04
		public string LocalIdentifier
		{
			get
			{
				return this.local_id;
			}
		}

		/// <summary>Gets the status of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.TransactionStatus" /> that contains the status of the transaction.</returns>
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002C0C File Offset: 0x00000E0C
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00002C14 File Offset: 0x00000E14
		public TransactionStatus Status
		{
			get
			{
				return this.status;
			}
			internal set
			{
				this.status = value;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002C20 File Offset: 0x00000E20
		internal TransactionInformation Clone(TransactionInformation other)
		{
			return new TransactionInformation(other);
		}

		// Token: 0x04000049 RID: 73
		private string local_id;

		// Token: 0x0400004A RID: 74
		private Guid dtcId = Guid.Empty;

		// Token: 0x0400004B RID: 75
		private DateTime creation_time;

		// Token: 0x0400004C RID: 76
		private TransactionStatus status;
	}
}
