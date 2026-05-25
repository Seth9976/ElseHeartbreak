using System;

namespace System.Transactions
{
	/// <summary>Represents a non-rooted transaction that can be delegated. This class cannot be inherited.</summary>
	// Token: 0x02000019 RID: 25
	[Serializable]
	public sealed class SubordinateTransaction : Transaction
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Transactions.SubordinateTransaction" /> class. </summary>
		/// <param name="isoLevel">The isolation level of the transaction</param>
		/// <param name="superior">A <see cref="T:System.Transactions.ISimpleTransactionSuperior" /></param>
		// Token: 0x06000037 RID: 55 RVA: 0x00002380 File Offset: 0x00000580
		public SubordinateTransaction(IsolationLevel level, ISimpleTransactionSuperior superior)
		{
			throw new NotImplementedException();
		}
	}
}
