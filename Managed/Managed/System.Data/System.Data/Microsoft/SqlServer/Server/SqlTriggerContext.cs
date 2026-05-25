using System;
using System.Data.SqlTypes;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Provides contextual information about the trigger that was fired. </summary>
	// Token: 0x02000150 RID: 336
	public sealed class SqlTriggerContext
	{
		// Token: 0x060011D2 RID: 4562 RVA: 0x00045E28 File Offset: 0x00044028
		internal SqlTriggerContext(TriggerAction triggerAction, bool[] columnsUpdated, SqlXml eventData)
		{
			this.triggerAction = triggerAction;
			this.columnsUpdated = columnsUpdated;
			this.eventData = eventData;
		}

		/// <summary>Gets the number of columns contained by the data table bound to the trigger. This property is read-only.</summary>
		/// <returns>The number of columns contained by the data table bound to the trigger, as an integer.</returns>
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x060011D3 RID: 4563 RVA: 0x00045E48 File Offset: 0x00044048
		public int ColumnCount
		{
			get
			{
				return (this.columnsUpdated != null) ? this.columnsUpdated.Length : 0;
			}
		}

		/// <summary>Gets the event data specific to the action that fired the trigger.</summary>
		/// <returns>The event data specific to the action that fired the trigger as a <see cref="T:System.Data.SqlTypes.SqlXml" /> if more information is available; null otherwise.</returns>
		// Token: 0x1700031A RID: 794
		// (get) Token: 0x060011D4 RID: 4564 RVA: 0x00045E64 File Offset: 0x00044064
		public SqlXml EventData
		{
			get
			{
				return this.eventData;
			}
		}

		/// <summary>Indicates what action fired the trigger.</summary>
		/// <returns>The action that fired the trigger as a <see cref="T:Microsoft.SqlServer.Server.TriggerAction" />.</returns>
		// Token: 0x1700031B RID: 795
		// (get) Token: 0x060011D5 RID: 4565 RVA: 0x00045E6C File Offset: 0x0004406C
		public TriggerAction TriggerAction
		{
			get
			{
				return this.triggerAction;
			}
		}

		/// <summary>Returns true if a column was affected by an INSERT or UPDATE statement.</summary>
		/// <returns>true if the column was affected by an INSERT or UPDATE operation.</returns>
		/// <param name="columnOrdinal">The zero-based ordinal of the column.</param>
		/// <exception cref="T:System.InvalidOperationException">Called in the context of a trigger where the value of the <see cref="P:Microsoft.SqlServer.Server.SqlTriggerContext.TriggerAction" /> property is not Insert or Update.</exception>
		// Token: 0x060011D6 RID: 4566 RVA: 0x00045E74 File Offset: 0x00044074
		public bool IsUpdatedColumn(int columnOrdinal)
		{
			if (this.columnsUpdated == null)
			{
				throw new IndexOutOfRangeException("The index specified does not exist");
			}
			return this.columnsUpdated[columnOrdinal];
		}

		// Token: 0x040006ED RID: 1773
		private TriggerAction triggerAction;

		// Token: 0x040006EE RID: 1774
		private bool[] columnsUpdated;

		// Token: 0x040006EF RID: 1775
		private SqlXml eventData;
	}
}
