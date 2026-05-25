using System;

namespace System.Data
{
	/// <summary>Provides additional information for the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000074 RID: 116
	public sealed class StatementCompletedEventArgs : EventArgs
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Data.StatementCompletedEventArgs" /> class.</summary>
		/// <param name="recordCount">Indicates the number of rows affected by the statement that caused the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" />  event to occur.</param>
		// Token: 0x0600064C RID: 1612 RVA: 0x0001F338 File Offset: 0x0001D538
		public StatementCompletedEventArgs(int recordCount)
		{
			this.recordCount = recordCount;
		}

		/// <summary>Indicates the number of rows affected by the statement that caused the <see cref="E:System.Data.SqlClient.SqlCommand.StatementCompleted" /> event to occur.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x0600064D RID: 1613 RVA: 0x0001F348 File Offset: 0x0001D548
		public int RecordCount
		{
			get
			{
				return this.recordCount;
			}
		}

		// Token: 0x0400022D RID: 557
		private int recordCount;
	}
}
