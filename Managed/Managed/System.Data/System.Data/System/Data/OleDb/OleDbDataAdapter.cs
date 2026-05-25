using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents a set of data commands and a database connection that are used to fill the <see cref="T:System.Data.DataSet" /> and update the data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000ED RID: 237
	[ToolboxItem("Microsoft.VSDesigner.Data.VS.OleDbDataAdapterToolboxItem, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Designer("Microsoft.VSDesigner.Data.VS.OleDbDataAdapterDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("RowUpdated")]
	public sealed class OleDbDataAdapter : DbDataAdapter, IDataAdapter, IDbDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class.</summary>
		// Token: 0x06000B64 RID: 2916 RVA: 0x00032098 File Offset: 0x00030298
		public OleDbDataAdapter()
			: this(null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with the specified <see cref="T:System.Data.OleDb.OleDbCommand" /> as the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property.</summary>
		/// <param name="selectCommand">An <see cref="T:System.Data.OleDb.OleDbCommand" /> that is a SELECT statement or stored procedure, and is set as the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />. </param>
		// Token: 0x06000B65 RID: 2917 RVA: 0x000320A4 File Offset: 0x000302A4
		public OleDbDataAdapter(OleDbCommand selectCommand)
		{
			this.SelectCommand = selectCommand;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with a <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" />.</summary>
		/// <param name="selectCommandText">A string that is an SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />. </param>
		/// <param name="selectConnection">An <see cref="T:System.Data.OleDb.OleDbConnection" /> that represents the connection. </param>
		// Token: 0x06000B66 RID: 2918 RVA: 0x000320B4 File Offset: 0x000302B4
		public OleDbDataAdapter(string selectCommandText, OleDbConnection selectConnection)
			: this(new OleDbCommand(selectCommandText, selectConnection))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> class with a <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" />.</summary>
		/// <param name="selectCommandText">A string that is an SQL SELECT statement or stored procedure to be used by the <see cref="P:System.Data.OleDb.OleDbDataAdapter.SelectCommand" /> property of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />. </param>
		/// <param name="selectConnectionString">The connection string. </param>
		// Token: 0x06000B67 RID: 2919 RVA: 0x000320C4 File Offset: 0x000302C4
		public OleDbDataAdapter(string selectCommandText, string selectConnectionString)
			: this(selectCommandText, new OleDbConnection(selectConnectionString))
		{
		}

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> after a command is executed against the data source. The attempt to update is made. Therefore, the event occurs.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06000B68 RID: 2920 RVA: 0x000320D4 File Offset: 0x000302D4
		// (remove) Token: 0x06000B69 RID: 2921 RVA: 0x000320F0 File Offset: 0x000302F0
		[DataCategory("DataCategory_Update")]
		public event OleDbRowUpdatedEventHandler RowUpdated;

		/// <summary>Occurs during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> before a command is executed against the data source. The attempt to update is made. Therefore, the event occurs.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001E RID: 30
		// (add) Token: 0x06000B6A RID: 2922 RVA: 0x0003210C File Offset: 0x0003030C
		// (remove) Token: 0x06000B6B RID: 2923 RVA: 0x00032128 File Offset: 0x00030328
		[DataCategory("DataCategory_Update")]
		public event OleDbRowUpdatingEventHandler RowUpdating;

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.DeleteCommand" />.</summary>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000B6C RID: 2924 RVA: 0x00032144 File Offset: 0x00030344
		// (set) Token: 0x06000B6D RID: 2925 RVA: 0x0003214C File Offset: 0x0003034C
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this.DeleteCommand;
			}
			set
			{
				this.DeleteCommand = (OleDbCommand)value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.InsertCommand" />.</summary>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000B6E RID: 2926 RVA: 0x0003215C File Offset: 0x0003035C
		// (set) Token: 0x06000B6F RID: 2927 RVA: 0x00032164 File Offset: 0x00030364
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this.InsertCommand;
			}
			set
			{
				this.InsertCommand = (OleDbCommand)value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.SelectCommand" />.</summary>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000B70 RID: 2928 RVA: 0x00032174 File Offset: 0x00030374
		// (set) Token: 0x06000B71 RID: 2929 RVA: 0x0003217C File Offset: 0x0003037C
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this.SelectCommand;
			}
			set
			{
				this.SelectCommand = (OleDbCommand)value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbDataAdapter.UpdateCommand" />.</summary>
		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000B72 RID: 2930 RVA: 0x0003218C File Offset: 0x0003038C
		// (set) Token: 0x06000B73 RID: 2931 RVA: 0x00032194 File Offset: 0x00030394
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this.UpdateCommand;
			}
			set
			{
				this.UpdateCommand = (OleDbCommand)value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		// Token: 0x06000B74 RID: 2932 RVA: 0x000321A4 File Offset: 0x000303A4
		[MonoTODO]
		object ICloneable.Clone()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets an SQL statement or stored procedure for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source that correspond to deleted rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x000321AC File Offset: 0x000303AC
		// (set) Token: 0x06000B76 RID: 2934 RVA: 0x000321B4 File Offset: 0x000303B4
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DataCategory("Update")]
		[DefaultValue(null)]
		public new OleDbCommand DeleteCommand
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
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source that correspond to new rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000B77 RID: 2935 RVA: 0x000321C0 File Offset: 0x000303C0
		// (set) Token: 0x06000B78 RID: 2936 RVA: 0x000321C8 File Offset: 0x000303C8
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DataCategory("Update")]
		[DefaultValue(null)]
		public new OleDbCommand InsertCommand
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
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> that is used during <see cref="M:System.Data.Common.DbDataAdapter.Fill(System.Data.DataSet)" /> to select records from data source for placement in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000B79 RID: 2937 RVA: 0x000321D4 File Offset: 0x000303D4
		// (set) Token: 0x06000B7A RID: 2938 RVA: 0x000321DC File Offset: 0x000303DC
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		[DataCategory("Fill")]
		public new OleDbCommand SelectCommand
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
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> used during <see cref="M:System.Data.Common.DbDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source that correspond to modified rows in the <see cref="T:System.Data.DataSet" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000B7B RID: 2939 RVA: 0x000321E8 File Offset: 0x000303E8
		// (set) Token: 0x06000B7C RID: 2940 RVA: 0x000321F0 File Offset: 0x000303F0
		[DataCategory("Update")]
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DBCommandEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new OleDbCommand UpdateCommand
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

		// Token: 0x06000B7D RID: 2941 RVA: 0x000321FC File Offset: 0x000303FC
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OleDbRowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06000B7E RID: 2942 RVA: 0x00032208 File Offset: 0x00030408
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new OleDbRowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		// Token: 0x06000B7F RID: 2943 RVA: 0x00032214 File Offset: 0x00030414
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			if (this.RowUpdated != null)
			{
				this.RowUpdated(this, (OleDbRowUpdatedEventArgs)value);
			}
		}

		// Token: 0x06000B80 RID: 2944 RVA: 0x00032234 File Offset: 0x00030434
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			if (this.RowUpdating != null)
			{
				this.RowUpdating(this, (OleDbRowUpdatingEventArgs)value);
			}
		}

		/// <summary>Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in an ADO Recordset or Record object using the specified <see cref="T:System.Data.DataTable" /> and ADO objects.</summary>
		/// <returns>The number of rows successfully refreshed to the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records and, if it is required, schema. </param>
		/// <param name="ADODBRecordSet">An ADO Recordset or Record object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B81 RID: 2945 RVA: 0x00032254 File Offset: 0x00030454
		[MonoTODO]
		public int Fill(DataTable dataTable, object ADODBRecordSet)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in an ADO Recordset or Record object using the specified <see cref="T:System.Data.DataSet" />, ADO object, and source table name.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if it is required, schema. </param>
		/// <param name="ADODBRecordSet">An ADO Recordset or Record object. </param>
		/// <param name="srcTable">The source table used for the table mappings. </param>
		/// <exception cref="T:System.SystemException">The source table is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B82 RID: 2946 RVA: 0x0003225C File Offset: 0x0003045C
		[MonoTODO]
		public int Fill(DataSet dataSet, object ADODBRecordSet, string srcTable)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400042B RID: 1067
		private OleDbCommand deleteCommand;

		// Token: 0x0400042C RID: 1068
		private OleDbCommand insertCommand;

		// Token: 0x0400042D RID: 1069
		private OleDbCommand selectCommand;

		// Token: 0x0400042E RID: 1070
		private OleDbCommand updateCommand;
	}
}
