using System;

namespace System.Transactions
{
	/// <summary>Contains additional information that specifies transaction behaviors.</summary>
	// Token: 0x02000023 RID: 35
	public struct TransactionOptions
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00002D0C File Offset: 0x00000F0C
		internal TransactionOptions(IsolationLevel level, TimeSpan timeout)
		{
			this.level = level;
			this.timeout = timeout;
		}

		/// <summary>Gets or sets the isolation level of the transaction.</summary>
		/// <returns>A <see cref="T:System.Transactions.IsolationLevel" /> enumeration that specifies the isolation level of the transaction.</returns>
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002D1C File Offset: 0x00000F1C
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00002D24 File Offset: 0x00000F24
		public IsolationLevel IsolationLevel
		{
			get
			{
				return this.level;
			}
			set
			{
				this.level = value;
			}
		}

		/// <summary>Gets or sets the timeout period for the transaction.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> value that specifies the timeout period for the transaction.</returns>
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002D30 File Offset: 0x00000F30
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00002D38 File Offset: 0x00000F38
		public TimeSpan Timeout
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

		/// <summary>Determines whether this <see cref="T:System.Transactions.TransactionOptions" /> instance and the specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> and this <see cref="T:System.Transactions.TransactionOptions" /> instance are identical; otherwise, false.</returns>
		/// <param name="obj">The object to compare with this instance.</param>
		// Token: 0x06000093 RID: 147 RVA: 0x00002D44 File Offset: 0x00000F44
		public override bool Equals(object obj)
		{
			return obj is TransactionOptions && this == (TransactionOptions)obj;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000094 RID: 148 RVA: 0x00002D64 File Offset: 0x00000F64
		public override int GetHashCode()
		{
			return (int)(this.level ^ (IsolationLevel)this.timeout.GetHashCode());
		}

		/// <summary>Tests whether two specified <see cref="T:System.Transactions.TransactionOptions" /> instances are equivalent.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> are equal; otherwise, false.</returns>
		/// <param name="x">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the right of the equality operator.</param>
		// Token: 0x06000095 RID: 149 RVA: 0x00002D78 File Offset: 0x00000F78
		public static bool operator ==(TransactionOptions o1, TransactionOptions o2)
		{
			return o1.level == o2.level && o1.timeout == o2.timeout;
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Transactions.TransactionOptions" /> instances are not equal.</summary>
		/// <returns>true if <paramref name="x" /> and <paramref name="y" /> are not equal; otherwise, false.</returns>
		/// <param name="x">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the left of the equality operator.</param>
		/// <param name="y">The <see cref="T:System.Transactions.TransactionOptions" /> instance that is to the right of the equality operator.</param>
		// Token: 0x06000096 RID: 150 RVA: 0x00002DA4 File Offset: 0x00000FA4
		public static bool operator !=(TransactionOptions o1, TransactionOptions o2)
		{
			return o1.level != o2.level || o1.timeout != o2.timeout;
		}

		// Token: 0x04000050 RID: 80
		private IsolationLevel level;

		// Token: 0x04000051 RID: 81
		private TimeSpan timeout;
	}
}
