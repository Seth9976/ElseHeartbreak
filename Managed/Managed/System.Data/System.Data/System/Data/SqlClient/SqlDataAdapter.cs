using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Represents a set of data commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update a SQL Server database. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000167 RID: 359
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.SqlDataAdapterToolboxItem, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RowUpdated")]
	[Designer("Microsoft.VSDesigner.Data.VS.SqlDataAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	public sealed class SqlDataAdapter : DbDataAdapter, IDataAdapter, IDbDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class.</summary>
		// Token: 0x060012F8 RID: 4856 RVA: 0x0004EFB4 File Offset: 0x0004D1B4
		public SqlDataAdapter()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with the specified <see cref="T:System.Data.SqlClient.SqlCommand" /> as the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property.</summary>
		/// <param name="selectCommand">A <see cref="T:System.Data.SqlClient.SqlCommand" /> that is a Transact-SQL SELECT statement or stored procedure and is set as the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		// Token: 0x060012F9 RID: 4857 RVA: 0x0004EFC0 File Offset: 0x0004D1C0
		public SqlDataAdapter(SqlCommand selectCommand)
		{
			this.SelectCommand = selectCommand;
			this.UpdateBatchSize = 1;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with a <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> and a <see cref="T:System.Data.SqlClient.SqlConnection" /> object.</summary>
		/// <param name="selectCommandText">A <see cref="T:System.String" /> that is a Transact-SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		/// <param name="selectConnection">A <see cref="T:System.Data.SqlClient.SqlConnection" /> that represents the connection. </param>
		// Token: 0x060012FA RID: 4858 RVA: 0x0004EFD8 File Offset: 0x0004D1D8
		public SqlDataAdapter(string selectCommandText, SqlConnection selectConnection)
			: this(new SqlCommand(selectCommandText, selectConnection))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> class with a <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> and a connection string.</summary>
		/// <param name="selectCommandText">A <see cref="T:System.String" /> that is a Transact-SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.SqlClient.SqlDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.SqlClient.SqlDataAdapter" />. </param>
		/// <param name="selectConnectionString">The connection string. </param>
		// Token: 0x060012FB RID: 4859 RVA: 0x0004EFE8 File Offset: 0x0004D1E8
		public SqlDataAdapter(string selectCommandText, string selectConnectionString)
			: this(selectCommandText, new SqlConnection(selectConnectionString))
		{
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> after a command is executed against the data source. The attempt to update is made, so the event fires.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000024 RID: 36
		// (add) Token: 0x060012FC RID: 4860 RVA: 0x0004EFF8 File Offset: 0x0004D1F8
		// (remove) Token: 0x060012FD RID: 4861 RVA: 0x0004F014 File Offset: 0x0004D214
		public event SqlRowUpdatedEventHandler RowUpdated;

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source. The attempt to update is made, so the event fires.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000025 RID: 37
		// (add) Token: 0x060012FE RID: 4862 RVA: 0x0004F030 File Offset: 0x0004D230
		// (remove) Token: 0x060012FF RID: 4863 RVA: 0x0004F04C File Offset: 0x0004D24C
		public event SqlRowUpdatingEventHandler RowUpdating;

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06001300 RID: 4864 RVA: 0x0004F068 File Offset: 0x0004D268
		// (set) Token: 0x06001301 RID: 4865 RVA: 0x0004F070 File Offset: 0x0004D270
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this.SelectCommand;
			}
			set
			{
				this.SelectCommand = (SqlCommand)value;
			}
		}

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06001302 RID: 4866 RVA: 0x0004F080 File Offset: 0x0004D280
		// (set) Token: 0x06001303 RID: 4867 RVA: 0x0004F088 File Offset: 0x0004D288
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this.InsertCommand;
			}
			set
			{
				this.InsertCommand = (SqlCommand)value;
			}
		}

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06001304 RID: 4868 RVA: 0x0004F098 File Offset: 0x0004D298
		// (set) Token: 0x06001305 RID: 4869 RVA: 0x0004F0A0 File Offset: 0x0004D2A0
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this.UpdateCommand;
			}
			set
			{
				this.UpdateCommand = (SqlCommand)value;
			}
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06001306 RID: 4870 RVA: 0x0004F0B0 File Offset: 0x0004D2B0
		// (set) Token: 0x06001307 RID: 4871 RVA: 0x0004F0B8 File Offset: 0x0004D2B8
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this.DeleteCommand;
			}
			set
			{
				this.DeleteCommand = (SqlCommand)value;
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x0004F0C8 File Offset: 0x0004D2C8
		[MonoTODO]
		object ICloneable.Clone()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure to delete records from the data set.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the database that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06001309 RID: 4873 RVA: 0x0004F0D0 File Offset: 0x0004D2D0
		// (set) Token: 0x0600130A RID: 4874 RVA: 0x0004F0E0 File Offset: 0x0004D2E0
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqlCommand DeleteCommand
		{
			get
			{
				return (SqlCommand)base.DeleteCommand;
			}
			set
			{
				base.DeleteCommand = value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure to insert new records into the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records into the database that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x0600130B RID: 4875 RVA: 0x0004F0EC File Offset: 0x0004D2EC
		// (set) Token: 0x0600130C RID: 4876 RVA: 0x0004F0FC File Offset: 0x0004D2FC
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		public new SqlCommand InsertCommand
		{
			get
			{
				return (SqlCommand)base.InsertCommand;
			}
			set
			{
				base.InsertCommand = value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure used to select records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Fill(System.Data.DataSet)" /> to select records from the database for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x0600130D RID: 4877 RVA: 0x0004F108 File Offset: 0x0004D308
		// (set) Token: 0x0600130E RID: 4878 RVA: 0x0004F118 File Offset: 0x0004D318
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqlCommand SelectCommand
		{
			get
			{
				return (SqlCommand)base.SelectCommand;
			}
			set
			{
				base.SelectCommand = value;
			}
		}

		/// <summary>Gets or sets a Transact-SQL statement or stored procedure used to update records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the database that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037A RID: 890
		// (get) Token: 0x0600130F RID: 4879 RVA: 0x0004F124 File Offset: 0x0004D324
		// (set) Token: 0x06001310 RID: 4880 RVA: 0x0004F134 File Offset: 0x0004D334
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		public new SqlCommand UpdateCommand
		{
			get
			{
				return (SqlCommand)base.UpdateCommand;
			}
			set
			{
				base.UpdateCommand = value;
			}
		}

		/// <summary>Gets or sets the number of rows that are processed in each round-trip to the server.</summary>
		/// <returns>The number of rows to process per-batch. Value isEffect0There is no limit on the batch size..1Disables batch updating.&gt;1Changes are sent using batches of <see cref="P:System.Data.SqlClient.SqlDataAdapter.UpdateBatchSize" /> operations at a time.When setting this to a value other than 1, all the commands associated with the <see cref="T:System.Data.SqlClient.SqlDataAdapter" /> have to have their UpdatedRowSource property set to None or OutputParameters. An exception is thrown otherwise.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037B RID: 891
		// (get) Token: 0x06001311 RID: 4881 RVA: 0x0004F140 File Offset: 0x0004D340
		// (set) Token: 0x06001312 RID: 4882 RVA: 0x0004F148 File Offset: 0x0004D348
		public override int UpdateBatchSize
		{
			get
			{
				return this.updateBatchSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("UpdateBatchSize");
				}
				this.updateBatchSize = value;
			}
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x0004F164 File Offset: 0x0004D364
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x0004F170 File Offset: 0x0004D370
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new SqlRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x0004F17C File Offset: 0x0004D37C
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			if (this.RowUpdated != null)
			{
				this.RowUpdated(this, (SqlRowUpdatedEventArgs)value);
			}
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x0004F19C File Offset: 0x0004D39C
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			if (this.RowUpdating != null)
			{
				this.RowUpdating(this, (SqlRowUpdatingEventArgs)value);
			}
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x0004F1BC File Offset: 0x0004D3BC
		[MonoTODO]
		protected override int AddToBatch(IDbCommand command)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x0004F1C4 File Offset: 0x0004D3C4
		[MonoTODO]
		protected override void ClearBatch()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x0004F1CC File Offset: 0x0004D3CC
		[MonoTODO]
		protected override int ExecuteBatch()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x0004F1D4 File Offset: 0x0004D3D4
		[MonoTODO]
		protected override IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x0004F1DC File Offset: 0x0004D3DC
		[MonoTODO]
		protected override void InitializeBatching()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x0004F1E4 File Offset: 0x0004D3E4
		[MonoTODO]
		protected override void TerminateBatching()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040007B3 RID: 1971
		private int updateBatchSize;
	}
}
