using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace System.Data.Common
{
	/// <summary>Aids implementation of the <see cref="T:System.Data.IDbDataAdapter" /> interface. Inheritors of <see cref="T:System.Data.Common.DbDataAdapter" /> implement a set of functions to provide strong typing, but inherit most of the functionality needed to fully implement a DataAdapter.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000BD RID: 189
	public abstract class DbDataAdapter : DataAdapter, IDataAdapter, IDbDataAdapter, ICloneable
	{
		/// <summary>Initializes a new instance of a DataAdapter class.</summary>
		// Token: 0x060008D8 RID: 2264 RVA: 0x0002C2B8 File Offset: 0x0002A4B8
		protected DbDataAdapter()
		{
		}

		/// <summary>Initializes a new instance of a DataAdapter class from an existing object of the same type.</summary>
		/// <param name="adapter">A DataAdapter object used to create the new DataAdapter. </param>
		// Token: 0x060008D9 RID: 2265 RVA: 0x0002C2C0 File Offset: 0x0002A4C0
		protected DbDataAdapter(DbDataAdapter adapter)
			: base(adapter)
		{
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x0002C2CC File Offset: 0x0002A4CC
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x0002C2D4 File Offset: 0x0002A4D4
		IDbCommand IDbDataAdapter.SelectCommand
		{
			get
			{
				return this.SelectCommand;
			}
			set
			{
				this.SelectCommand = (DbCommand)value;
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x0002C2E4 File Offset: 0x0002A4E4
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x0002C2EC File Offset: 0x0002A4EC
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return this.UpdateCommand;
			}
			set
			{
				this.UpdateCommand = (DbCommand)value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x0002C2FC File Offset: 0x0002A4FC
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x0002C304 File Offset: 0x0002A504
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return this.DeleteCommand;
			}
			set
			{
				this.DeleteCommand = (DbCommand)value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0002C314 File Offset: 0x0002A514
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0002C31C File Offset: 0x0002A51C
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return this.InsertCommand;
			}
			set
			{
				this.InsertCommand = (DbCommand)value;
			}
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x0002C32C File Offset: 0x0002A52C
		[MonoTODO]
		[Obsolete("use 'protected DbDataAdapter(DbDataAdapter)' ctor")]
		object ICloneable.Clone()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the behavior of the command used to fill the data adapter.</summary>
		/// <returns>The <see cref="T:System.Data.CommandBehavior" /> of the command used to fill the data adapter.</returns>
		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060008E3 RID: 2275 RVA: 0x0002C334 File Offset: 0x0002A534
		// (set) Token: 0x060008E4 RID: 2276 RVA: 0x0002C33C File Offset: 0x0002A53C
		protected internal CommandBehavior FillCommandBehavior
		{
			get
			{
				return this._behavior;
			}
			set
			{
				this._behavior = value;
			}
		}

		/// <summary>Gets or sets a command used to select records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> that is used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to select records from data source for placement in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060008E5 RID: 2277 RVA: 0x0002C348 File Offset: 0x0002A548
		// (set) Token: 0x060008E6 RID: 2278 RVA: 0x0002C358 File Offset: 0x0002A558
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand SelectCommand
		{
			get
			{
				return (DbCommand)this._selectCommand;
			}
			set
			{
				if (this._selectCommand != value)
				{
					this._selectCommand = value;
					((IDbDataAdapter)this).SelectCommand = value;
				}
			}
		}

		/// <summary>Gets or sets a command for deleting records from the data set.</summary>
		/// <returns>An <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to delete records in the data source for deleted rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0002C374 File Offset: 0x0002A574
		// (set) Token: 0x060008E8 RID: 2280 RVA: 0x0002C384 File Offset: 0x0002A584
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DbCommand DeleteCommand
		{
			get
			{
				return (DbCommand)this._deleteCommand;
			}
			set
			{
				if (this._deleteCommand != value)
				{
					this._deleteCommand = value;
					((IDbDataAdapter)this).DeleteCommand = value;
				}
			}
		}

		/// <summary>Gets or sets a command used to insert new records into the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to insert records in the data source for new rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060008E9 RID: 2281 RVA: 0x0002C3A0 File Offset: 0x0002A5A0
		// (set) Token: 0x060008EA RID: 2282 RVA: 0x0002C3B0 File Offset: 0x0002A5B0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand InsertCommand
		{
			get
			{
				return (DbCommand)this._insertCommand;
			}
			set
			{
				if (this._insertCommand != value)
				{
					this._insertCommand = value;
					((IDbDataAdapter)this).InsertCommand = value;
				}
			}
		}

		/// <summary>Gets or sets a command used to update records in the data source.</summary>
		/// <returns>A <see cref="T:System.Data.IDbCommand" /> used during <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" /> to update records in the data source for modified rows in the data set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x0002C3CC File Offset: 0x0002A5CC
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x0002C3DC File Offset: 0x0002A5DC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DbCommand UpdateCommand
		{
			get
			{
				return (DbCommand)this._updateCommand;
			}
			set
			{
				if (this._updateCommand != value)
				{
					this._updateCommand = value;
					((IDbDataAdapter)this).UpdateCommand = value;
				}
			}
		}

		/// <summary>Gets or sets a value that enables or disables batch processing support, and specifies the number of commands that can be executed in a batch. </summary>
		/// <returns>The number of rows to process per batch. Value isEffect0There is no limit on the batch size.1Disables batch updating.&gt; 1Changes are sent using batches of <see cref="P:System.Data.Common.DbDataAdapter.UpdateBatchSize" /> operations at a time.When setting this to a value other than 1 ,all the commands associated with the <see cref="T:System.Data.Common.DbDataAdapter" /> must have their <see cref="P:System.Data.IDbCommand.UpdatedRowSource" /> property set to None or OutputParameters. An exception will be thrown otherwise. </returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001A0 RID: 416
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x0002C3F8 File Offset: 0x0002A5F8
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x0002C3FC File Offset: 0x0002A5FC
		[DefaultValue(1)]
		public virtual int UpdateBatchSize
		{
			get
			{
				return 1;
			}
			set
			{
				if (value != 1)
				{
					throw new NotSupportedException();
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.RowUpdatedEventArgs" /> class.</summary>
		/// <returns>A new instance of the <see cref="T:System.Data.Common.RowUpdatedEventArgs" /> class.</returns>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> used to update the data source. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> executed during the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">Whether the command is an UPDATE, INSERT, DELETE, or SELECT statement. </param>
		/// <param name="tableMapping">A <see cref="T:System.Data.Common.DataTableMapping" /> object. </param>
		// Token: 0x060008EF RID: 2287 RVA: 0x0002C40C File Offset: 0x0002A60C
		protected virtual RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new RowUpdatedEventArgs(dataRow, command, statementType, tableMapping);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.RowUpdatingEventArgs" /> class.</summary>
		/// <returns>A new instance of the <see cref="T:System.Data.Common.RowUpdatingEventArgs" /> class.</returns>
		/// <param name="dataRow">The <see cref="T:System.Data.DataRow" /> that updates the data source. </param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to execute during the <see cref="M:System.Data.IDataAdapter.Update(System.Data.DataSet)" />. </param>
		/// <param name="statementType">Whether the command is an UPDATE, INSERT, DELETE, or SELECT statement. </param>
		/// <param name="tableMapping">A <see cref="T:System.Data.Common.DataTableMapping" /> object. </param>
		// Token: 0x060008F0 RID: 2288 RVA: 0x0002C418 File Offset: 0x0002A618
		protected virtual RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			return new RowUpdatingEventArgs(dataRow, command, statementType, tableMapping);
		}

		/// <summary>Raises the RowUpdated event of a .NET Framework data provider.</summary>
		/// <param name="value">A <see cref="T:System.Data.Common.RowUpdatedEventArgs" /> that contains the event data. </param>
		// Token: 0x060008F1 RID: 2289 RVA: 0x0002C424 File Offset: 0x0002A624
		protected virtual void OnRowUpdated(RowUpdatedEventArgs value)
		{
			if (base.Events["RowUpdated"] != null)
			{
				Delegate[] invocationList = base.Events["RowUpdated"].GetInvocationList();
				foreach (Delegate @delegate in invocationList)
				{
					MethodInfo method = @delegate.Method;
					method.Invoke(value, null);
				}
			}
		}

		/// <summary>Raises the RowUpdating event of a .NET Framework data provider.</summary>
		/// <param name="value">An <see cref="T:System.Data.Common.RowUpdatingEventArgs" />  that contains the event data. </param>
		// Token: 0x060008F2 RID: 2290 RVA: 0x0002C488 File Offset: 0x0002A688
		protected virtual void OnRowUpdating(RowUpdatingEventArgs value)
		{
			if (base.Events["RowUpdating"] != null)
			{
				Delegate[] invocationList = base.Events["RowUpdating"].GetInvocationList();
				foreach (Delegate @delegate in invocationList)
				{
					MethodInfo method = @delegate.Method;
					method.Invoke(value, null);
				}
			}
		}

		/// <summary>Releases the unmanaged resources used by the <see cref="T:System.Data.Common.DbDataAdapter" /> and optionally releases the managed resources.</summary>
		/// <param name="disposing">true to release both managed and unmanaged resources; false to release only unmanaged resources. </param>
		// Token: 0x060008F3 RID: 2291 RVA: 0x0002C4EC File Offset: 0x0002A6EC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (((IDbDataAdapter)this).SelectCommand != null)
				{
					((IDbDataAdapter)this).SelectCommand.Dispose();
					((IDbDataAdapter)this).SelectCommand = null;
				}
				if (((IDbDataAdapter)this).InsertCommand != null)
				{
					((IDbDataAdapter)this).InsertCommand.Dispose();
					((IDbDataAdapter)this).InsertCommand = null;
				}
				if (((IDbDataAdapter)this).UpdateCommand != null)
				{
					((IDbDataAdapter)this).UpdateCommand.Dispose();
					((IDbDataAdapter)this).UpdateCommand = null;
				}
				if (((IDbDataAdapter)this).DeleteCommand != null)
				{
					((IDbDataAdapter)this).DeleteCommand.Dispose();
					((IDbDataAdapter)this).DeleteCommand = null;
				}
			}
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008F4 RID: 2292 RVA: 0x0002C578 File Offset: 0x0002A778
		public override int Fill(DataSet dataSet)
		{
			return this.Fill(dataSet, 0, 0, "Table", ((IDbDataAdapter)this).SelectCommand, this._behavior);
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataTable" /> name.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">The name of the <see cref="T:System.Data.DataTable" /> to use for table mapping. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008F5 RID: 2293 RVA: 0x0002C5A0 File Offset: 0x0002A7A0
		public int Fill(DataTable dataTable)
		{
			if (dataTable == null)
			{
				throw new ArgumentNullException("DataTable");
			}
			return this.Fill(dataTable, ((IDbDataAdapter)this).SelectCommand, this._behavior);
		}

		/// <summary>Adds or refreshes rows in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.SystemException">The source table is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008F6 RID: 2294 RVA: 0x0002C5D4 File Offset: 0x0002A7D4
		public int Fill(DataSet dataSet, string srcTable)
		{
			return this.Fill(dataSet, 0, 0, srcTable, ((IDbDataAdapter)this).SelectCommand, this._behavior);
		}

		/// <summary>Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in the data source using the specified <see cref="T:System.Data.DataTable" />, <see cref="T:System.Data.IDbCommand" /> and <see cref="T:System.Data.CommandBehavior" />.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataTable">A <see cref="T:System.Data.DataTable" /> to fill with records and, if necessary, schema. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		// Token: 0x060008F7 RID: 2295 RVA: 0x0002C5F8 File Offset: 0x0002A7F8
		protected virtual int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
		{
			CommandBehavior commandBehavior = behavior;
			if (command.Connection.State == ConnectionState.Closed)
			{
				command.Connection.Open();
				commandBehavior |= CommandBehavior.CloseConnection;
			}
			return this.Fill(dataTable, command.ExecuteReader(commandBehavior));
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.SystemException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid.-or- The connection is invalid. </exception>
		/// <exception cref="T:System.InvalidCastException">The connection could not be found. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="startRecord" /> parameter is less than 0.-or- The <paramref name="maxRecords" /> parameter is less than 0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008F8 RID: 2296 RVA: 0x0002C638 File Offset: 0x0002A838
		public int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable)
		{
			return this.Fill(dataSet, startRecord, maxRecords, srcTable, ((IDbDataAdapter)this).SelectCommand, this._behavior);
		}

		/// <summary>Adds or refreshes rows in a <see cref="T:System.Data.DataTable" /> to match those in the data source starting at the specified record and retrieving up to the specified maximum number of records.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataTable" />. This value does not include rows affected by statements that do not return rows.</returns>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="dataTables">The <see cref="T:System.Data.DataTable" /> objects to fill from the data source.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008F9 RID: 2297 RVA: 0x0002C65C File Offset: 0x0002A85C
		[MonoTODO]
		public int Fill(int startRecord, int maxRecords, params DataTable[] dataTables)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and <see cref="T:System.Data.DataTable" /> names.</summary>
		/// <returns>The number of rows added to or refreshed in the data tables.</returns>
		/// <param name="dataTables">The <see cref="T:System.Data.DataTable" /> objects to fill from the data source.</param>
		/// <param name="startRecord">The zero-based record number to start with.</param>
		/// <param name="maxRecords">The maximum number of records to retrieve.</param>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> executed to fill the <see cref="T:System.Data.DataTable" /> objects.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values.</param>
		/// <exception cref="T:System.SystemException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid.-or- The connection is invalid. </exception>
		/// <exception cref="T:System.InvalidCastException">The connection could not be found. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="startRecord" /> parameter is less than 0.-or- The <paramref name="maxRecords" /> parameter is less than 0. </exception>
		// Token: 0x060008FA RID: 2298 RVA: 0x0002C664 File Offset: 0x0002A864
		[MonoTODO]
		protected virtual int Fill(DataTable[] dataTables, int startRecord, int maxRecords, IDbCommand command, CommandBehavior behavior)
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds or refreshes rows in a specified range in the <see cref="T:System.Data.DataSet" /> to match those in the data source using the <see cref="T:System.Data.DataSet" /> and source table names, command string, and command behavior.</summary>
		/// <returns>The number of rows successfully added to or refreshed in the <see cref="T:System.Data.DataSet" />. This does not include rows affected by statements that do not return rows.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to fill with records and, if necessary, schema. </param>
		/// <param name="startRecord">The zero-based record number to start with. </param>
		/// <param name="maxRecords">The maximum number of records to retrieve. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="startRecord" /> parameter is less than 0.-or- The <paramref name="maxRecords" /> parameter is less than 0. </exception>
		// Token: 0x060008FB RID: 2299 RVA: 0x0002C66C File Offset: 0x0002A86C
		protected virtual int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			if (command.Connection == null)
			{
				throw new InvalidOperationException("Connection state is closed");
			}
			if (this.MissingSchemaAction == MissingSchemaAction.AddWithKey)
			{
				behavior |= CommandBehavior.KeyInfo;
			}
			CommandBehavior commandBehavior = behavior;
			if (command.Connection.State == ConnectionState.Closed)
			{
				command.Connection.Open();
				commandBehavior |= CommandBehavior.CloseConnection;
			}
			return this.Fill(dataSet, srcTable, command.ExecuteReader(commandBehavior), startRecord, maxRecords);
		}

		// Token: 0x060008FC RID: 2300 RVA: 0x0002C6DC File Offset: 0x0002A8DC
		internal static int FillFromReader(DataTable table, IDataReader reader, int start, int length, int[] mapping, LoadOption loadOption)
		{
			if (reader.FieldCount == 0)
			{
				return 0;
			}
			for (int i = 0; i < start; i++)
			{
				reader.Read();
			}
			int num = 0;
			object[] array = new object[mapping.Length];
			while (reader.Read() && (length == 0 || num < length))
			{
				for (int j = 0; j < mapping.Length; j++)
				{
					array[j] = ((mapping[j] >= 0) ? reader[mapping[j]] : null);
				}
				table.BeginLoadData();
				table.LoadDataRow(array, loadOption);
				table.EndLoadData();
				num++;
			}
			return num;
		}

		// Token: 0x060008FD RID: 2301 RVA: 0x0002C788 File Offset: 0x0002A988
		internal static int FillFromReader(DataTable table, IDataReader reader, int start, int length, int[] mapping, LoadOption loadOption, FillErrorEventHandler errorHandler)
		{
			if (reader.FieldCount == 0)
			{
				return 0;
			}
			for (int i = 0; i < start; i++)
			{
				reader.Read();
			}
			int num = 0;
			object[] array = new object[mapping.Length];
			while (reader.Read() && (length == 0 || num < length))
			{
				for (int j = 0; j < mapping.Length; j++)
				{
					array[j] = ((mapping[j] >= 0) ? reader[mapping[j]] : null);
				}
				table.BeginLoadData();
				try
				{
					table.LoadDataRow(array, loadOption);
				}
				catch (Exception ex)
				{
					FillErrorEventArgs fillErrorEventArgs = new FillErrorEventArgs(table, array);
					fillErrorEventArgs.Errors = ex;
					fillErrorEventArgs.Continue = false;
					errorHandler(table, fillErrorEventArgs);
					if (!fillErrorEventArgs.Continue)
					{
						throw ex;
					}
				}
				table.EndLoadData();
				num++;
			}
			return num;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> named "Table" to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to insert the schema in. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values that specify how to insert the schema. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008FE RID: 2302 RVA: 0x0002C88C File Offset: 0x0002AA8C
		public override DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType)
		{
			return this.FillSchema(dataSet, schemaType, ((IDbDataAdapter)this).SelectCommand, "Table", this._behavior);
		}

		/// <summary>Configures the schema of the specified <see cref="T:System.Data.DataTable" /> based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information returned from the data source.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060008FF RID: 2303 RVA: 0x0002C8B4 File Offset: 0x0002AAB4
		public DataTable FillSchema(DataTable dataTable, SchemaType schemaType)
		{
			return this.FillSchema(dataTable, schemaType, ((IDbDataAdapter)this).SelectCommand, this._behavior);
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based upon the specified <see cref="T:System.Data.SchemaType" /> and <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>A reference to a collection of <see cref="T:System.Data.DataTable" /> objects that were added to the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">A <see cref="T:System.Data.DataSet" /> to insert the schema in. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values that specify how to insert the schema. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.ArgumentException">A source table from which to get the schema could not be found. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000900 RID: 2304 RVA: 0x0002C8CC File Offset: 0x0002AACC
		public DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, string srcTable)
		{
			return this.FillSchema(dataSet, schemaType, ((IDbDataAdapter)this).SelectCommand, srcTable, this._behavior);
		}

		/// <summary>Configures the schema of the specified <see cref="T:System.Data.DataTable" /> based on the specified <see cref="T:System.Data.SchemaType" />, command string, and <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>A of <see cref="T:System.Data.DataTable" /> object that contains schema information returned from the data source.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		// Token: 0x06000901 RID: 2305 RVA: 0x0002C8F0 File Offset: 0x0002AAF0
		protected virtual DataTable FillSchema(DataTable dataTable, SchemaType schemaType, IDbCommand command, CommandBehavior behavior)
		{
			if (dataTable == null)
			{
				throw new ArgumentNullException("DataTable");
			}
			behavior |= CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo;
			if (command.Connection.State == ConnectionState.Closed)
			{
				command.Connection.Open();
				behavior |= CommandBehavior.CloseConnection;
			}
			IDataReader dataReader = command.ExecuteReader(behavior);
			try
			{
				string text = base.SetupSchema(schemaType, dataTable.TableName);
				if (text != null)
				{
					MissingSchemaAction missingSchemaAction = this.MissingSchemaAction;
					if (missingSchemaAction != MissingSchemaAction.Ignore && missingSchemaAction != MissingSchemaAction.Error)
					{
						missingSchemaAction = MissingSchemaAction.AddWithKey;
					}
					DataAdapter.BuildSchema(dataReader, dataTable, schemaType, missingSchemaAction, this.MissingMappingAction, base.TableMappings);
				}
			}
			finally
			{
				dataReader.Close();
			}
			return dataTable;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataTable" /> to the specified <see cref="T:System.Data.DataSet" /> and configures the schema to match that in the data source based on the specified <see cref="T:System.Data.SchemaType" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataTable" /> objects that contain schema information returned from the data source.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to be filled with the schema from the data source. </param>
		/// <param name="schemaType">One of the <see cref="T:System.Data.SchemaType" /> values. </param>
		/// <param name="command">The SQL SELECT statement used to retrieve rows from the data source. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		// Token: 0x06000902 RID: 2306 RVA: 0x0002C9AC File Offset: 0x0002ABAC
		protected virtual DataTable[] FillSchema(DataSet dataSet, SchemaType schemaType, IDbCommand command, string srcTable, CommandBehavior behavior)
		{
			if (dataSet == null)
			{
				throw new ArgumentNullException("DataSet");
			}
			behavior |= CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo;
			if (command.Connection.State == ConnectionState.Closed)
			{
				command.Connection.Open();
				behavior |= CommandBehavior.CloseConnection;
			}
			IDataReader dataReader = command.ExecuteReader(behavior);
			ArrayList arrayList = new ArrayList();
			string text = srcTable;
			int num = 0;
			try
			{
				MissingSchemaAction missingSchemaAction = this.MissingSchemaAction;
				if (this.MissingSchemaAction != MissingSchemaAction.Ignore && this.MissingSchemaAction != MissingSchemaAction.Error)
				{
					missingSchemaAction = MissingSchemaAction.AddWithKey;
				}
				do
				{
					text = base.SetupSchema(schemaType, text);
					if (text != null)
					{
						DataTable dataTable;
						if (dataSet.Tables.Contains(text))
						{
							dataTable = dataSet.Tables[text];
						}
						else
						{
							if (this.MissingSchemaAction == MissingSchemaAction.Ignore)
							{
								goto IL_00FA;
							}
							dataTable = dataSet.Tables.Add(text);
						}
						DataAdapter.BuildSchema(dataReader, dataTable, schemaType, missingSchemaAction, this.MissingMappingAction, base.TableMappings);
						arrayList.Add(dataTable);
						text = string.Format("{0}{1}", srcTable, ++num);
					}
					IL_00FA:;
				}
				while (dataReader.NextResult());
			}
			finally
			{
				dataReader.Close();
			}
			return (DataTable[])arrayList.ToArray(typeof(DataTable));
		}

		/// <summary>Gets the parameters set by the user when executing an SQL SELECT statement.</summary>
		/// <returns>An array of <see cref="T:System.Data.IDataParameter" /> objects that contains the parameters set by the user.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000903 RID: 2307 RVA: 0x0002CAFC File Offset: 0x0002ACFC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public override IDataParameter[] GetFillParameters()
		{
			IDbCommand selectCommand = ((IDbDataAdapter)this).SelectCommand;
			IDataParameter[] array = new IDataParameter[selectCommand.Parameters.Count];
			selectCommand.Parameters.CopyTo(array, 0);
			return array;
		}

		/// <summary>Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified array of <see cref="T:System.Data.DataRow" /> objects.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataRows">An array of <see cref="T:System.Data.DataRow" /> objects used to update the data source. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.SystemException">No <see cref="T:System.Data.DataRow" /> exists to update.-or- No <see cref="T:System.Data.DataTable" /> exists to update.-or- No <see cref="T:System.Data.DataSet" /> exists to use as a source. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000904 RID: 2308 RVA: 0x0002CB30 File Offset: 0x0002AD30
		public int Update(DataRow[] dataRows)
		{
			if (dataRows == null)
			{
				throw new ArgumentNullException("dataRows");
			}
			if (dataRows.Length == 0)
			{
				return 0;
			}
			if (dataRows[0] == null)
			{
				throw new ArgumentException("dataRows[0].");
			}
			DataTable table = dataRows[0].Table;
			if (table == null)
			{
				throw new ArgumentException("table is null reference.");
			}
			for (int i = 0; i < dataRows.Length; i++)
			{
				if (dataRows[i] == null)
				{
					throw new ArgumentException("dataRows[" + i + "].");
				}
				if (dataRows[i].Table != table)
				{
					throw new ArgumentException(" DataRow[" + i + "] is from a different DataTable than DataRow[0].");
				}
			}
			DataTableMapping dataTableMapping = base.TableMappings.GetByDataSetTable(table.TableName);
			if (dataTableMapping == null)
			{
				dataTableMapping = DataTableMappingCollection.GetTableMappingBySchemaAction(base.TableMappings, table.TableName, table.TableName, this.MissingMappingAction);
				if (dataTableMapping != null)
				{
					foreach (object obj in table.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						if (dataTableMapping.ColumnMappings.IndexOf(dataColumn.ColumnName) < 0)
						{
							DataColumnMapping dataColumnMapping = DataColumnMappingCollection.GetColumnMappingBySchemaAction(dataTableMapping.ColumnMappings, dataColumn.ColumnName, this.MissingMappingAction);
							if (dataColumnMapping == null)
							{
								dataColumnMapping = new DataColumnMapping(dataColumn.ColumnName, dataColumn.ColumnName);
							}
							dataTableMapping.ColumnMappings.Add(dataColumnMapping);
						}
					}
				}
				else
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj2 in table.Columns)
					{
						DataColumn dataColumn2 = (DataColumn)obj2;
						arrayList.Add(new DataColumnMapping(dataColumn2.ColumnName, dataColumn2.ColumnName));
					}
					dataTableMapping = new DataTableMapping(table.TableName, table.TableName, arrayList.ToArray(typeof(DataColumnMapping)) as DataColumnMapping[]);
				}
			}
			DataRow[] array = table.NewRowArray(dataRows.Length);
			Array.Copy(dataRows, 0, array, 0, dataRows.Length);
			return this.Update(array, dataTableMapping);
		}

		/// <summary>Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataSet" />.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> used to update the data source. </param>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000905 RID: 2309 RVA: 0x0002CDB0 File Offset: 0x0002AFB0
		public override int Update(DataSet dataSet)
		{
			return this.Update(dataSet, "Table");
		}

		/// <summary>Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified <see cref="T:System.Data.DataTable" />.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> used to update the data source. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.SystemException">No <see cref="T:System.Data.DataRow" /> exists to update.-or- No <see cref="T:System.Data.DataTable" /> exists to update.-or- No <see cref="T:System.Data.DataSet" /> exists to use as a source. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000906 RID: 2310 RVA: 0x0002CDC0 File Offset: 0x0002AFC0
		public int Update(DataTable dataTable)
		{
			DataTableMapping dataTableMapping = base.TableMappings.GetByDataSetTable(dataTable.TableName);
			if (dataTableMapping == null)
			{
				dataTableMapping = DataTableMappingCollection.GetTableMappingBySchemaAction(base.TableMappings, dataTable.TableName, dataTable.TableName, this.MissingMappingAction);
				if (dataTableMapping != null)
				{
					foreach (object obj in dataTable.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						if (dataTableMapping.ColumnMappings.IndexOf(dataColumn.ColumnName) < 0)
						{
							DataColumnMapping dataColumnMapping = DataColumnMappingCollection.GetColumnMappingBySchemaAction(dataTableMapping.ColumnMappings, dataColumn.ColumnName, this.MissingMappingAction);
							if (dataColumnMapping == null)
							{
								dataColumnMapping = new DataColumnMapping(dataColumn.ColumnName, dataColumn.ColumnName);
							}
							dataTableMapping.ColumnMappings.Add(dataColumnMapping);
						}
					}
				}
				else
				{
					ArrayList arrayList = new ArrayList();
					foreach (object obj2 in dataTable.Columns)
					{
						DataColumn dataColumn2 = (DataColumn)obj2;
						arrayList.Add(new DataColumnMapping(dataColumn2.ColumnName, dataColumn2.ColumnName));
					}
					dataTableMapping = new DataTableMapping(dataTable.TableName, dataTable.TableName, arrayList.ToArray(typeof(DataColumnMapping)) as DataColumnMapping[]);
				}
			}
			return this.Update(dataTable, dataTableMapping);
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x0002CF78 File Offset: 0x0002B178
		private int Update(DataTable dataTable, DataTableMapping tableMapping)
		{
			DataRow[] array = dataTable.NewRowArray(dataTable.Rows.Count);
			dataTable.Rows.CopyTo(array, 0);
			return this.Update(array, tableMapping);
		}

		/// <summary>Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the specified array of <see cref="T:System.Data.DataRow" /> objects.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataRows">An array of <see cref="T:System.Data.DataRow" /> objects used to update the data source. </param>
		/// <param name="tableMapping">The <see cref="P:System.Data.IDataAdapter.TableMappings" /> collection to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.SystemException">No <see cref="T:System.Data.DataRow" /> exists to update.-or- No <see cref="T:System.Data.DataTable" /> exists to update.-or- No <see cref="T:System.Data.DataSet" /> exists to use as a source. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		// Token: 0x06000908 RID: 2312 RVA: 0x0002CFAC File Offset: 0x0002B1AC
		protected virtual int Update(DataRow[] dataRows, DataTableMapping tableMapping)
		{
			int num = 0;
			int i = 0;
			while (i < dataRows.Length)
			{
				DataRow dataRow = dataRows[i];
				StatementType statementType = StatementType.Update;
				IDbCommand dbCommand = null;
				string text = string.Empty;
				DataRowState rowState = dataRow.RowState;
				switch (rowState)
				{
				case DataRowState.Detached:
				case DataRowState.Unchanged:
					break;
				default:
					if (rowState != DataRowState.Modified)
					{
						goto IL_00A5;
					}
					statementType = StatementType.Update;
					dbCommand = ((IDbDataAdapter)this).UpdateCommand;
					text = "Update";
					goto IL_00A5;
				case DataRowState.Added:
					statementType = StatementType.Insert;
					dbCommand = ((IDbDataAdapter)this).InsertCommand;
					text = "Insert";
					goto IL_00A5;
				case DataRowState.Deleted:
					statementType = StatementType.Delete;
					dbCommand = ((IDbDataAdapter)this).DeleteCommand;
					text = "Delete";
					goto IL_00A5;
				}
				IL_066A:
				i++;
				continue;
				IL_00A5:
				RowUpdatingEventArgs rowUpdatingEventArgs = this.CreateRowUpdatingEvent(dataRow, dbCommand, statementType, tableMapping);
				dataRow.RowError = null;
				this.OnRowUpdating(rowUpdatingEventArgs);
				switch (rowUpdatingEventArgs.Status)
				{
				case UpdateStatus.Continue:
				{
					dbCommand = rowUpdatingEventArgs.Command;
					try
					{
						if (dbCommand != null)
						{
							DataColumnMappingCollection columnMappings = tableMapping.ColumnMappings;
							foreach (object obj in dbCommand.Parameters)
							{
								IDataParameter dataParameter = (IDataParameter)obj;
								if ((dataParameter.Direction & ParameterDirection.Input) != (ParameterDirection)0)
								{
									DataRowVersion dataRowVersion = dataParameter.SourceVersion;
									if (statementType == StatementType.Delete)
									{
										dataRowVersion = DataRowVersion.Original;
									}
									string text2 = dataParameter.SourceColumn;
									if (columnMappings.Contains(text2))
									{
										text2 = columnMappings[text2].DataSetColumn;
										dataParameter.Value = dataRow[text2, dataRowVersion];
									}
									else
									{
										dataParameter.Value = null;
									}
									DbParameter dbParameter = dataParameter as DbParameter;
									if (dbParameter != null && dbParameter.SourceColumnNullMapping)
									{
										if (dataParameter.Value != null && dataParameter.Value != DBNull.Value)
										{
											dbParameter.Value = 0;
										}
										else
										{
											dbParameter.Value = 1;
										}
									}
								}
							}
						}
					}
					catch (Exception ex)
					{
						rowUpdatingEventArgs.Errors = ex;
						rowUpdatingEventArgs.Status = UpdateStatus.ErrorsOccurred;
					}
					IDataReader dataReader = null;
					try
					{
						if (dbCommand == null)
						{
							throw ExceptionHelper.UpdateRequiresCommand(text);
						}
						CommandBehavior commandBehavior = CommandBehavior.Default;
						if (dbCommand.Connection.State == ConnectionState.Closed)
						{
							dbCommand.Connection.Open();
							commandBehavior |= CommandBehavior.CloseConnection;
						}
						dataReader = dbCommand.ExecuteReader(commandBehavior);
						DataColumnMappingCollection columnMappings2 = tableMapping.ColumnMappings;
						if ((dbCommand.UpdatedRowSource == UpdateRowSource.Both || dbCommand.UpdatedRowSource == UpdateRowSource.FirstReturnedRecord) && dataReader.Read())
						{
							DataTable schemaTable = dataReader.GetSchemaTable();
							foreach (object obj2 in schemaTable.Rows)
							{
								DataRow dataRow2 = (DataRow)obj2;
								string text3 = dataRow2["ColumnName"].ToString();
								string text4 = text3;
								if (columnMappings2 != null && columnMappings2.Contains(text3))
								{
									text4 = columnMappings2[text4].DataSetColumn;
								}
								DataColumn dataColumn = dataRow.Table.Columns[text4];
								if (dataColumn != null && (dataColumn.Expression == null || dataColumn.Expression.Length <= 0))
								{
									bool readOnly = dataColumn.ReadOnly;
									dataColumn.ReadOnly = false;
									try
									{
										dataRow[text4] = dataReader[text3];
									}
									finally
									{
										dataColumn.ReadOnly = readOnly;
									}
								}
							}
						}
						dataReader.Close();
						int recordsAffected = dataReader.RecordsAffected;
						if (recordsAffected == 0)
						{
							throw new DBConcurrencyException("Concurrency violation: the " + text + "Command affected 0 records.", null, new DataRow[] { dataRow });
						}
						num += recordsAffected;
						if (dbCommand.UpdatedRowSource == UpdateRowSource.Both || dbCommand.UpdatedRowSource == UpdateRowSource.OutputParameters)
						{
							foreach (object obj3 in dbCommand.Parameters)
							{
								IDataParameter dataParameter2 = (IDataParameter)obj3;
								if (dataParameter2.Direction == ParameterDirection.InputOutput || dataParameter2.Direction == ParameterDirection.Output || dataParameter2.Direction == ParameterDirection.ReturnValue)
								{
									string text5 = dataParameter2.SourceColumn;
									if (columnMappings2 != null && columnMappings2.Contains(dataParameter2.SourceColumn))
									{
										text5 = columnMappings2[dataParameter2.SourceColumn].DataSetColumn;
									}
									DataColumn dataColumn2 = dataRow.Table.Columns[text5];
									if (dataColumn2 != null && (dataColumn2.Expression == null || dataColumn2.Expression.Length <= 0))
									{
										bool readOnly2 = dataColumn2.ReadOnly;
										dataColumn2.ReadOnly = false;
										try
										{
											dataRow[text5] = dataParameter2.Value;
										}
										finally
										{
											dataColumn2.ReadOnly = readOnly2;
										}
									}
								}
							}
						}
						RowUpdatedEventArgs rowUpdatedEventArgs = this.CreateRowUpdatedEvent(dataRow, dbCommand, statementType, tableMapping);
						this.OnRowUpdated(rowUpdatedEventArgs);
						switch (rowUpdatedEventArgs.Status)
						{
						case UpdateStatus.ErrorsOccurred:
						{
							if (rowUpdatedEventArgs.Errors == null)
							{
								rowUpdatedEventArgs.Errors = ExceptionHelper.RowUpdatedError();
							}
							DataRow dataRow3 = dataRow;
							dataRow3.RowError += rowUpdatedEventArgs.Errors.Message;
							if (!base.ContinueUpdateOnError)
							{
								throw rowUpdatedEventArgs.Errors;
							}
							break;
						}
						case UpdateStatus.SkipCurrentRow:
							goto IL_066A;
						case UpdateStatus.SkipAllRemainingRows:
							return num;
						}
						if (base.AcceptChangesDuringUpdate)
						{
							dataRow.AcceptChanges();
						}
					}
					catch (Exception ex2)
					{
						dataRow.RowError = ex2.Message;
						if (!base.ContinueUpdateOnError)
						{
							throw ex2;
						}
					}
					finally
					{
						if (dataReader != null && !dataReader.IsClosed)
						{
							dataReader.Close();
						}
					}
					goto IL_066A;
				}
				case UpdateStatus.ErrorsOccurred:
				{
					if (rowUpdatingEventArgs.Errors == null)
					{
						rowUpdatingEventArgs.Errors = ExceptionHelper.RowUpdatedError();
					}
					DataRow dataRow4 = dataRow;
					dataRow4.RowError += rowUpdatingEventArgs.Errors.Message;
					if (!base.ContinueUpdateOnError)
					{
						throw rowUpdatingEventArgs.Errors;
					}
					goto IL_066A;
				}
				case UpdateStatus.SkipCurrentRow:
					num++;
					goto IL_066A;
				case UpdateStatus.SkipAllRemainingRows:
					return num;
				default:
					throw ExceptionHelper.InvalidUpdateStatus(rowUpdatingEventArgs.Status);
				}
			}
			return num;
		}

		/// <summary>Calls the respective INSERT, UPDATE, or DELETE statements for each inserted, updated, or deleted row in the <see cref="T:System.Data.DataSet" /> with the specified <see cref="T:System.Data.DataTable" /> name.</summary>
		/// <returns>The number of rows successfully updated from the <see cref="T:System.Data.DataSet" />.</returns>
		/// <param name="dataSet">The <see cref="T:System.Data.DataSet" /> to use to update the data source. </param>
		/// <param name="srcTable">The name of the source table to use for table mapping. </param>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="T:System.Data.DataSet" /> is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The source table is invalid. </exception>
		/// <exception cref="T:System.Data.DBConcurrencyException">An attempt to execute an INSERT, UPDATE, or DELETE statement resulted in zero records affected. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000909 RID: 2313 RVA: 0x0002D6F8 File Offset: 0x0002B8F8
		public int Update(DataSet dataSet, string srcTable)
		{
			MissingMappingAction missingMappingAction = this.MissingMappingAction;
			if (missingMappingAction == MissingMappingAction.Ignore)
			{
				missingMappingAction = MissingMappingAction.Error;
			}
			DataTableMapping tableMappingBySchemaAction = DataTableMappingCollection.GetTableMappingBySchemaAction(base.TableMappings, srcTable, srcTable, missingMappingAction);
			DataTable dataTable = dataSet.Tables[tableMappingBySchemaAction.DataSetTable];
			if (dataTable == null)
			{
				throw new ArgumentException(string.Format("Missing table {0}", srcTable));
			}
			return this.Update(dataTable, tableMappingBySchemaAction);
		}

		/// <summary>Adds a <see cref="T:System.Data.IDbCommand" /> to the current batch.</summary>
		/// <returns>The number of commands in the batch before adding the <see cref="T:System.Data.IDbCommand" />.</returns>
		/// <param name="command">The <see cref="T:System.Data.IDbCommand" /> to add to the batch.</param>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x0600090A RID: 2314 RVA: 0x0002D758 File Offset: 0x0002B958
		protected virtual int AddToBatch(IDbCommand command)
		{
			throw this.CreateMethodNotSupportedException();
		}

		/// <summary>Removes all <see cref="T:System.Data.IDbCommand" /> objects from the batch.</summary>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x0600090B RID: 2315 RVA: 0x0002D760 File Offset: 0x0002B960
		protected virtual void ClearBatch()
		{
			throw this.CreateMethodNotSupportedException();
		}

		/// <summary>Executes the current batch.</summary>
		/// <returns>The return value from the last command in the batch.</returns>
		// Token: 0x0600090C RID: 2316 RVA: 0x0002D768 File Offset: 0x0002B968
		protected virtual int ExecuteBatch()
		{
			throw this.CreateMethodNotSupportedException();
		}

		/// <summary>Returns a <see cref="T:System.Data.IDataParameter" /> from one of the commands in the current batch.</summary>
		/// <returns>The <see cref="T:System.Data.IDataParameter" /> specified.</returns>
		/// <param name="commandIdentifier">The index of the command to retrieve the parameter from.</param>
		/// <param name="parameterIndex">The index of the parameter within the command.</param>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x0600090D RID: 2317 RVA: 0x0002D770 File Offset: 0x0002B970
		protected virtual IDataParameter GetBatchedParameter(int commandIdentifier, int parameterIndex)
		{
			throw this.CreateMethodNotSupportedException();
		}

		/// <summary>Returns information about an individual update attempt within a larger batched update.</summary>
		/// <param name="commandIdentifier">The zero-based column ordinal of the individual command within the batch.</param>
		/// <param name="recordsAffected">The number of rows affected in the data store by the specified command within the batch.</param>
		/// <param name="error">An <see cref="T:System.Exception" /> thrown during execution of the specified command. Returns null (Nothing in Visual Basic) if no exception is thrown.</param>
		// Token: 0x0600090E RID: 2318 RVA: 0x0002D778 File Offset: 0x0002B978
		protected virtual bool GetBatchedRecordsAffected(int commandIdentifier, out int recordsAffected, out Exception error)
		{
			recordsAffected = 1;
			error = null;
			return true;
		}

		/// <summary>Initializes batching for the <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x0600090F RID: 2319 RVA: 0x0002D784 File Offset: 0x0002B984
		protected virtual void InitializeBatching()
		{
			throw this.CreateMethodNotSupportedException();
		}

		/// <summary>Ends batching for the <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <exception cref="T:System.NotSupportedException">The adapter does not support batches. </exception>
		// Token: 0x06000910 RID: 2320 RVA: 0x0002D78C File Offset: 0x0002B98C
		protected virtual void TerminateBatching()
		{
			throw this.CreateMethodNotSupportedException();
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x0002D794 File Offset: 0x0002B994
		private Exception CreateMethodNotSupportedException()
		{
			return new NotSupportedException("Method is not supported.");
		}

		/// <summary>The default name used by the <see cref="T:System.Data.Common.DataAdapter" /> object for table mappings.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x04000325 RID: 805
		public const string DefaultSourceTableName = "Table";

		// Token: 0x04000326 RID: 806
		private const string DefaultSourceColumnName = "Column";

		// Token: 0x04000327 RID: 807
		private CommandBehavior _behavior;

		// Token: 0x04000328 RID: 808
		private IDbCommand _selectCommand;

		// Token: 0x04000329 RID: 809
		private IDbCommand _updateCommand;

		// Token: 0x0400032A RID: 810
		private IDbCommand _deleteCommand;

		// Token: 0x0400032B RID: 811
		private IDbCommand _insertCommand;
	}
}
