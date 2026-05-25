using System;
using System.Runtime.Serialization;

namespace System.Transactions
{
	/// <summary>Describes a clone of a transaction providing guarantee that the transaction cannot be committed until the application comes to rest regarding work on the transaction. This class cannot be inherited.</summary>
	// Token: 0x0200000C RID: 12
	[MonoTODO("Not supported yet")]
	[Serializable]
	public sealed class DependentTransaction : Transaction, ISerializable
	{
		// Token: 0x06000018 RID: 24 RVA: 0x00002298 File Offset: 0x00000498
		internal DependentTransaction(Transaction parent, DependentCloneOption option)
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000022A0 File Offset: 0x000004A0
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			this.completed = info.GetBoolean("completed");
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022B4 File Offset: 0x000004B4
		internal bool Completed
		{
			get
			{
				return this.completed;
			}
		}

		/// <summary>Attempts to complete the dependent transaction.</summary>
		/// <exception cref="T:System.Transactions.TransactionException">Any attempt for additional work on the transaction after this method is called. These include invoking methods such as <see cref="M:System.Transactions.Transaction.EnlistVolatile" />, <see cref="M:System.Transactions.Transaction.EnlistDurable" />, <see cref="M:System.Transactions.Transaction.Clone" />, <see cref="M:System.Transactions.Transaction.DependentClone(System.Transactions.DependentCloneOption)" /> , or any serialization operations on the transaction. </exception>
		// Token: 0x0600001B RID: 27 RVA: 0x000022BC File Offset: 0x000004BC
		[MonoTODO]
		public void Complete()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000026 RID: 38
		private bool completed;
	}
}
