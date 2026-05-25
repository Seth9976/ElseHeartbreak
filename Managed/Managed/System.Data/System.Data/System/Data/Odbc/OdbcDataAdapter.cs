using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Represents a set of data commands and a connection to a data source that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200013B RID: 315
	[DefaultEvent("RowUpdated")]
	[Designer("Microsoft.VSDesigner.Data.VS.OdbcDataAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.OdbcDataAdapterToolboxItem, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class OdbcDataAdapter : DbDataAdapter, IDataAdapter, IDbDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class.</summary>
		// Token: 0x0600110A RID: 4362 RVA: 0x0004319C File Offset: 0x0004139C
		public OdbcDataAdapter()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with the specified SQL SELECT statement.</summary>
		/// <param name="selectCommand">An <see cref="T:System.Data.Odbc.OdbcCommand" /> that is an SQL SELECT statement or stored procedure, and is set as the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />. </param>
		// Token: 0x0600110B RID: 4363 RVA: 0x000431A8 File Offset: 0x000413A8
		public OdbcDataAdapter(OdbcCommand selectCommand)
		{
			this.SelectCommand = selectCommand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with an SQL SELECT statement and an <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <param name="selectCommandText">A string that is a SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />. </param>
		/// <param name="selectConnection">An <see cref="T:System.Data.Odbc.OdbcConnection" /> that represents the connection. </param>
		// Token: 0x0600110C RID: 4364 RVA: 0x000431B8 File Offset: 0x000413B8
		public OdbcDataAdapter(string selectCommandText, OdbcConnection selectConnection)
			: this(new OdbcCommand(selectCommandText, selectConnection))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" /> class with an SQL SELECT statement and a connection string.</summary>
		/// <param name="selectCommandText">A string that is a SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.Odbc.OdbcDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.Odbc.OdbcDataAdapter" />. </param>
		/// <param name="selectConnectionString">The connection string. </param>
		// Token: 0x0600110D RID: 4365 RVA: 0x000431C8 File Offset: 0x000413C8
		public OdbcDataAdapter(string selectCommandText, string selectConnectionString)
			: this(selectCommandText, new OdbcConnection(selectConnectionString))
		{
		}

		/// <summary>Occurs during an update operation after a command is executed against the data source.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x0600110E RID: 4366 RVA: 0x000431D8 File Offset: 0x000413D8
		// (remove) Token: 0x0600110F RID: 4367 RVA: 0x000431F4 File Offset: 0x000413F4
		public event OdbcRowUpdatedEventHandler RowUpdated;

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06001110 RID: 4368 RVA: 0x00043210 File Offset: 0x00041410
		// (remove) Token: 0x06001111 RID: 4369 RVA: 0x0004322C File Offset: 0x0004142C
		public event OdbcRowUpdatingEventHandler RowUpdating;

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.DeleteCommand" />.</summary>
		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x06001112 RID: 4370 RVA: 0x00043248 File Offset: 0x00041448
		// (set) Token: 0x06001113 RID: 4371 RVA: 0x00043250 File Offset: 0x00041450
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this.DeleteCommand;
			}
			set
			{
				this.DeleteCommand = (OdbcCommand)value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.InsertCommand" />.</summary>
		// Token: 0x170002E2 RID: 738
		// (get) Token: 0x06001114 RID: 4372 RVA: 0x00043260 File Offset: 0x00041460
		// (set) Token: 0x06001115 RID: 4373 RVA: 0x00043268 File Offset: 0x00041468
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this.InsertCommand;
			}
			set
			{
				this.InsertCommand = (OdbcCommand)value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.SelectCommand" />.</summary>
		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x00043278 File Offset: 0x00041478
		// (set) Token: 0x06001117 RID: 4375 RVA: 0x00043280 File Offset: 0x00041480
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this.SelectCommand;
			}
			set
			{
				this.SelectCommand = (OdbcCommand)value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.UpdateCommand" />.</summary>
		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06001118 RID: 4376 RVA: 0x00043290 File Offset: 0x00041490
		// (set) Token: 0x06001119 RID: 4377 RVA: 0x00043298 File Offset: 0x00041498
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this.UpdateCommand;
			}
			set
			{
				this.UpdateCommand = (OdbcCommand)value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		// Token: 0x0600111A RID: 4378 RVA: 0x000432A8 File Offset: 0x000414A8
		[MonoTODO]
		object ICloneable.Clone()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to delete records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to delete records in the data source that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x000432B0 File Offset: 0x000414B0
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x000432B8 File Offset: 0x000414B8
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[OdbcDescription("Used during Update for deleted rows in DataSet.")]
		[OdbcCategory("Update")]
		[DefaultValue(null)]
		public new OdbcCommand DeleteCommand
		{
			get
			{
				return this.deleteCommand;
			}
			set
			{
				this.deleteCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to insert new records into the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to insert records in the data source that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x000432C4 File Offset: 0x000414C4
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x000432CC File Offset: 0x000414CC
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[OdbcDescription("Used during Update for new rows in DataSet.")]
		[OdbcCategory("Update")]
		[DefaultValue(null)]
		public new OdbcCommand InsertCommand
		{
			get
			{
				return this.insertCommand;
			}
			set
			{
				this.insertCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> that is used during a fill operation to select records from data source for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x000432D8 File Offset: 0x000414D8
		// (set) Token: 0x06001120 RID: 4384 RVA: 0x000432E0 File Offset: 0x000414E0
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[OdbcCategory("Fill")]
		[OdbcDescription("Used during Fill/FillSchema.")]
		[DefaultValue(null)]
		public new OdbcCommand SelectCommand
		{
			get
			{
				return this.selectCommand;
			}
			set
			{
				this.selectCommand = value;
			}
		}

		/// <summary>Gets or sets an SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> used during an update operation to update records in the data source that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x000432EC File Offset: 0x000414EC
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x000432F4 File Offset: 0x000414F4
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[OdbcCategory("Update")]
		[OdbcDescription("Used during Update for modified rows in DataSet.")]
		[DefaultValue(null)]
		public new OdbcCommand UpdateCommand
		{
			get
			{
				return this.updateCommand;
			}
			set
			{
				this.updateCommand = value;
			}
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x00043300 File Offset: 0x00041500
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x0004330C File Offset: 0x0004150C
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OdbcRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x00043318 File Offset: 0x00041518
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			if (this.RowUpdated != null)
			{
				this.RowUpdated(this, (OdbcRowUpdatedEventArgs)value);
			}
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x00043338 File Offset: 0x00041538
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			if (this.RowUpdating != null)
			{
				this.RowUpdating(this, (OdbcRowUpdatingEventArgs)value);
			}
		}

		// Token: 0x04000661 RID: 1633
		private OdbcCommand deleteCommand;

		// Token: 0x04000662 RID: 1634
		private OdbcCommand insertCommand;

		// Token: 0x04000663 RID: 1635
		private OdbcCommand selectCommand;

		// Token: 0x04000664 RID: 1636
		private OdbcCommand updateCommand;
	}
}
