using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Xml;

namespace System.Data
{
	/// <summary>Represents a row of data in a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000029 RID: 41
	public class DataRow
	{
		/// <summary>Initializes a new instance of the DataRow. Constructs a row from the builder. Only for internal usage..</summary>
		/// <param name="builder">builder </param>
		// Token: 0x06000201 RID: 513 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		protected internal DataRow(DataRowBuilder builder)
		{
			this._table = builder.Table;
			this._rowId = builder._rowId;
			this.rowError = string.Empty;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000D818 File Offset: 0x0000BA18
		internal DataRow(DataTable table, int rowId)
		{
			this._table = table;
			this._rowId = rowId;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000203 RID: 515 RVA: 0x0000D844 File Offset: 0x0000BA44
		// (set) Token: 0x06000204 RID: 516 RVA: 0x0000D864 File Offset: 0x0000BA64
		private ArrayList ColumnErrors
		{
			get
			{
				if (this._columnErrors == null)
				{
					this._columnErrors = new ArrayList();
				}
				return this._columnErrors;
			}
			set
			{
				this._columnErrors = value;
			}
		}

		/// <summary>Gets a value that indicates whether there are errors in a row.</summary>
		/// <returns>true if the row contains an error; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000205 RID: 517 RVA: 0x0000D870 File Offset: 0x0000BA70
		public bool HasErrors
		{
			get
			{
				if (this.RowError != string.Empty)
				{
					return true;
				}
				foreach (object obj in this.ColumnErrors)
				{
					string text = (string)obj;
					if (text != null && text != string.Empty)
					{
						return true;
					}
				}
				return false;
			}
		}

		/// <summary>Gets or sets the data stored in the column specified by name.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="columnName" /> cannot be found. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when you try to set a value on a deleted row. </exception>
		/// <exception cref="T:System.InvalidCastException">Occurs when you set a value and its <see cref="T:System.Type" /> does not match <see cref="P:System.Data.DataColumn.DataType" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000047 RID: 71
		public object this[string columnName]
		{
			get
			{
				return this[columnName, DataRowVersion.Default];
			}
			set
			{
				DataColumn dataColumn = this._table.Columns[columnName];
				if (dataColumn == null)
				{
					throw new ArgumentException("The column '" + columnName + "' does not belong to the table : " + this._table.TableName);
				}
				this[dataColumn.Ordinal] = value;
			}
		}

		/// <summary>Gets or sets the data stored in the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> that contains the data. </param>
		/// <exception cref="T:System.ArgumentException">The column does not belong to this table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> is null. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to set a value on a deleted row. </exception>
		/// <exception cref="T:System.InvalidCastException">The data types of the value and the column do not match. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000048 RID: 72
		public object this[DataColumn column]
		{
			get
			{
				return this[column, DataRowVersion.Default];
			}
			set
			{
				if (column == null)
				{
					throw new ArgumentNullException("column");
				}
				int num = this._table.Columns.IndexOf(column);
				if (num == -1)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The column '{0}' does not belong to the table : {1}.", new object[]
					{
						column.ColumnName,
						this._table.TableName
					}));
				}
				this[num] = value;
			}
		}

		/// <summary>Gets or sets the data stored in the column specified by index.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">Occurs when you try to set a value on a deleted row. </exception>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range. </exception>
		/// <exception cref="T:System.InvalidCastException">Occurs when you set the value and the new value's <see cref="T:System.Type" /> does not match <see cref="P:System.Data.DataColumn.DataType" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000049 RID: 73
		public object this[int columnIndex]
		{
			get
			{
				return this[columnIndex, DataRowVersion.Default];
			}
			set
			{
				if (columnIndex < 0 || columnIndex > this._table.Columns.Count)
				{
					throw new IndexOutOfRangeException();
				}
				if (this.RowState == DataRowState.Deleted)
				{
					throw new DeletedRowInaccessibleException();
				}
				DataColumn dataColumn = this._table.Columns[columnIndex];
				this._table.ChangingDataColumn(this, dataColumn, value);
				if (value == null && dataColumn.DataType.IsValueType)
				{
					throw new ArgumentException("Canot set column '" + dataColumn.ColumnName + "' to be null. Please use DBNull instead.");
				}
				this._rowChanged = true;
				this.CheckValue(value, dataColumn);
				bool flag = this.Proposed >= 0;
				if (!flag)
				{
					this.BeginEdit();
				}
				dataColumn[this.Proposed] = value;
				this._table.ChangedDataColumn(this, dataColumn, value);
				if (!flag)
				{
					this.EndEdit();
				}
			}
		}

		/// <summary>Gets the specified version of data stored in the named column.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version that you want. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The column specified by <paramref name="columnName" /> cannot be found. </exception>
		/// <exception cref="T:System.InvalidCastException">The data types of the value and the column do not match. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The row was deleted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700004A RID: 74
		public object this[string columnName, DataRowVersion version]
		{
			get
			{
				DataColumn dataColumn = this._table.Columns[columnName];
				if (dataColumn == null)
				{
					throw new ArgumentException("The column '" + columnName + "' does not belong to the table : " + this._table.TableName);
				}
				return this[dataColumn.Ordinal, version];
			}
		}

		/// <summary>Gets the specified version of data stored in the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> that contains information about the column. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version that you want. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The column does not belong to the table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> argument contains null. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700004B RID: 75
		public object this[DataColumn column, DataRowVersion version]
		{
			get
			{
				if (column == null)
				{
					throw new ArgumentNullException("column");
				}
				if (column.Table != this.Table)
				{
					throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "The column '{0}' does not belong to the table : {1}.", new object[]
					{
						column.ColumnName,
						this._table.TableName
					}));
				}
				return this[column.Ordinal, version];
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000DBB0 File Offset: 0x0000BDB0
		internal void SetValue(int column, object value, int version)
		{
			DataColumn dataColumn = this.Table.Columns[column];
			if (value == null && !dataColumn.AutoIncrement)
			{
				value = dataColumn.DefaultValue;
			}
			this.Table.ChangingDataColumn(this, dataColumn, value);
			this.CheckValue(value, dataColumn);
			if (!dataColumn.AutoIncrement)
			{
				dataColumn[version] = value;
			}
			else if (this._proposed >= 0 && this._proposed != version)
			{
				dataColumn[version] = dataColumn[this._proposed];
			}
		}

		/// <summary>Gets the data stored in the column, specified by index and version of the data to retrieve.</summary>
		/// <returns>An <see cref="T:System.Object" /> that contains the data.</returns>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version that you want. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range. </exception>
		/// <exception cref="T:System.InvalidCastException">The data types of the value and the column do not match. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to set a value on a deleted row. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700004C RID: 76
		public object this[int columnIndex, DataRowVersion version]
		{
			get
			{
				if (columnIndex < 0 || columnIndex > this._table.Columns.Count)
				{
					throw new IndexOutOfRangeException();
				}
				DataColumn dataColumn = this._table.Columns[columnIndex];
				int num = this.IndexFromVersion(version);
				if (dataColumn.Expression != string.Empty && this._table.Rows.IndexOf(this) != -1)
				{
					object obj = dataColumn.CompiledExpression.Eval(this);
					if (obj != null && obj != DBNull.Value)
					{
						obj = Convert.ChangeType(obj, dataColumn.DataType);
					}
					dataColumn[num] = obj;
					return dataColumn[num];
				}
				return dataColumn[num];
			}
		}

		/// <summary>Gets or sets all the values for this row through an array.</summary>
		/// <returns>An array of type <see cref="T:System.Object" />.</returns>
		/// <exception cref="T:System.ArgumentException">The array is larger than the number of columns in the table. </exception>
		/// <exception cref="T:System.InvalidCastException">A value in the array does not match its <see cref="P:System.Data.DataColumn.DataType" /> in its respective <see cref="T:System.Data.DataColumn" />. </exception>
		/// <exception cref="T:System.Data.ConstraintException">An edit broke a constraint. </exception>
		/// <exception cref="T:System.Data.ReadOnlyException">An edit tried to change the value of a read-only column. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">An edit tried to put a null value in a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> of the <see cref="T:System.Data.DataColumn" /> object is false. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The row has been deleted. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000210 RID: 528 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		// (set) Token: 0x06000211 RID: 529 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public object[] ItemArray
		{
			get
			{
				if (this.RowState == DataRowState.Deleted)
				{
					throw new DeletedRowInaccessibleException("Deleted row information cannot be accessed through the row.");
				}
				int num = this.Current;
				if (this.RowState == DataRowState.Detached)
				{
					if (this.Proposed < 0)
					{
						throw new RowNotInTableException("This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.");
					}
					num = this.Proposed;
				}
				object[] array = new object[this._table.Columns.Count];
				foreach (object obj in this._table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					array[dataColumn.Ordinal] = dataColumn[num];
				}
				return array;
			}
			set
			{
				if (value.Length > this._table.Columns.Count)
				{
					throw new ArgumentException();
				}
				if (this.RowState == DataRowState.Deleted)
				{
					throw new DeletedRowInaccessibleException();
				}
				this.BeginEdit();
				DataColumnChangeEventArgs dataColumnChangeEventArgs = new DataColumnChangeEventArgs();
				foreach (object obj in this._table.Columns)
				{
					DataColumn dataColumn = (DataColumn)obj;
					int ordinal = dataColumn.Ordinal;
					object obj2 = ((ordinal >= value.Length) ? null : value[ordinal]);
					if (obj2 != null)
					{
						dataColumnChangeEventArgs.Initialize(this, dataColumn, obj2);
						this.CheckValue(dataColumnChangeEventArgs.ProposedValue, dataColumn);
						this._table.RaiseOnColumnChanging(dataColumnChangeEventArgs);
						dataColumn[this.Proposed] = dataColumnChangeEventArgs.ProposedValue;
						this._table.RaiseOnColumnChanged(dataColumnChangeEventArgs);
					}
				}
				this.EndEdit();
			}
		}

		/// <summary>Gets the current state of the row with regard to its relationship to the <see cref="T:System.Data.DataRowCollection" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowState" /> values.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000DEF8 File Offset: 0x0000C0F8
		// (set) Token: 0x06000213 RID: 531 RVA: 0x0000DF50 File Offset: 0x0000C150
		public DataRowState RowState
		{
			get
			{
				if (this.Original == -1 && this.Current == -1)
				{
					return DataRowState.Detached;
				}
				if (this.Original == this.Current)
				{
					return DataRowState.Unchanged;
				}
				if (this.Original == -1)
				{
					return DataRowState.Added;
				}
				if (this.Current == -1)
				{
					return DataRowState.Deleted;
				}
				return DataRowState.Modified;
			}
			internal set
			{
				if (value == DataRowState.Detached)
				{
					this.Original = -1;
					this.Current = -1;
				}
				if (value == DataRowState.Unchanged)
				{
					this.Original = this.Current;
				}
				if (value == DataRowState.Added)
				{
					this.Original = -1;
				}
				if (value == DataRowState.Deleted)
				{
					this.Current = -1;
				}
			}
		}

		/// <summary>Changes the <see cref="P:System.Data.DataRow.Rowstate" /> of a <see cref="T:System.Data.DataRow" /> to Added. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000214 RID: 532 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
		public void SetAdded()
		{
			if (this.RowState != DataRowState.Unchanged)
			{
				throw new InvalidOperationException("SetAdded and SetModified can only be called on DataRows with Unchanged DataRowState.");
			}
			this.Original = -1;
		}

		/// <summary>Changes the <see cref="P:System.Data.DataRow.Rowstate" /> of a <see cref="T:System.Data.DataRow" /> to Modified. </summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000215 RID: 533 RVA: 0x0000DFC4 File Offset: 0x0000C1C4
		public void SetModified()
		{
			if (this.RowState != DataRowState.Unchanged)
			{
				throw new InvalidOperationException("SetAdded and SetModified can only be called on DataRows with Unchanged DataRowState.");
			}
			this.Current = this._table.RecordCache.NewRecord();
			this._table.RecordCache.CopyRecord(this._table, this.Original, this.Current);
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> for which this row has a schema.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> to which this row belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000E024 File Offset: 0x0000C224
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000E02C File Offset: 0x0000C22C
		public DataTable Table
		{
			get
			{
				return this._table;
			}
			internal set
			{
				this._table = value;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000E038 File Offset: 0x0000C238
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000E040 File Offset: 0x0000C240
		internal int XmlRowID
		{
			get
			{
				return this.xmlRowID;
			}
			set
			{
				this.xmlRowID = value;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600021A RID: 538 RVA: 0x0000E04C File Offset: 0x0000C24C
		// (set) Token: 0x0600021B RID: 539 RVA: 0x0000E054 File Offset: 0x0000C254
		internal int RowID
		{
			get
			{
				return this._rowId;
			}
			set
			{
				this._rowId = value;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600021C RID: 540 RVA: 0x0000E060 File Offset: 0x0000C260
		// (set) Token: 0x0600021D RID: 541 RVA: 0x0000E068 File Offset: 0x0000C268
		internal int Original
		{
			get
			{
				return this._original;
			}
			set
			{
				if (this.Table != null)
				{
					this.Table.RecordCache[value] = this;
				}
				this._original = value;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600021E RID: 542 RVA: 0x0000E09C File Offset: 0x0000C29C
		// (set) Token: 0x0600021F RID: 543 RVA: 0x0000E0A4 File Offset: 0x0000C2A4
		internal int Current
		{
			get
			{
				return this._current;
			}
			set
			{
				if (this.Table != null)
				{
					this.Table.RecordCache[value] = this;
				}
				this._current = value;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000220 RID: 544 RVA: 0x0000E0D8 File Offset: 0x0000C2D8
		// (set) Token: 0x06000221 RID: 545 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
		internal int Proposed
		{
			get
			{
				return this._proposed;
			}
			set
			{
				if (this.Table != null)
				{
					this.Table.RecordCache[value] = this;
				}
				this._proposed = value;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000E114 File Offset: 0x0000C314
		internal void AttachAt(int row_id, DataRowAction action)
		{
			this._rowId = row_id;
			if (this.Proposed != -1)
			{
				if (this.Current >= 0)
				{
					this.Table.RecordCache.DisposeRecord(this.Current);
				}
				this.Current = this.Proposed;
				this.Proposed = -1;
			}
			if ((action & (DataRowAction.ChangeCurrentAndOriginal | DataRowAction.ChangeOriginal)) != DataRowAction.Nothing)
			{
				this.Original = this.Current;
			}
		}

		// Token: 0x06000223 RID: 547 RVA: 0x0000E180 File Offset: 0x0000C380
		private void Detach()
		{
			this.Table.DeleteRowFromIndexes(this);
			this._table.Rows.RemoveInternal(this);
			if (this.Proposed >= 0 && this.Proposed != this.Current && this.Proposed != this.Original)
			{
				this._table.RecordCache.DisposeRecord(this.Proposed);
			}
			this.Proposed = -1;
			if (this.Current >= 0 && this.Current != this.Original)
			{
				this._table.RecordCache.DisposeRecord(this.Current);
			}
			this.Current = -1;
			if (this.Original >= 0)
			{
				this._table.RecordCache.DisposeRecord(this.Original);
			}
			this.Original = -1;
			this._rowId = -1;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000E260 File Offset: 0x0000C460
		internal void ImportRecord(int record)
		{
			if (this.HasVersion(DataRowVersion.Proposed))
			{
				this.Table.RecordCache.DisposeRecord(this.Proposed);
			}
			this.Proposed = record;
			foreach (object obj in this.Table.Columns.AutoIncrmentColumns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				dataColumn.UpdateAutoIncrementValue(dataColumn.DataContainer.GetInt64(this.Proposed));
			}
			foreach (object obj2 in this.Table.Columns)
			{
				DataColumn dataColumn2 = (DataColumn)obj2;
				this.CheckValue(this[dataColumn2], dataColumn2, false);
			}
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000E388 File Offset: 0x0000C588
		private void CheckValue(object v, DataColumn col)
		{
			this.CheckValue(v, col, true);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000E394 File Offset: 0x0000C594
		private void CheckValue(object v, DataColumn col, bool doROCheck)
		{
			if (doROCheck && this._rowId != -1 && col.ReadOnly)
			{
				throw new ReadOnlyException();
			}
			if (v == null || v == DBNull.Value)
			{
				if (col.AllowDBNull || col.AutoIncrement || col.DefaultValue != DBNull.Value)
				{
					return;
				}
				this._nullConstraintViolation = true;
				if (this.Table._duringDataLoad || (this.Table.DataSet != null && !this.Table.DataSet.EnforceConstraints))
				{
					this.Table._nullConstraintViolationDuringDataLoad = true;
				}
				this._nullConstraintMessage = "Column '" + col.ColumnName + "' does not allow nulls.";
			}
		}

		/// <summary>Gets or sets the custom error description for a row.</summary>
		/// <returns>The text describing an error.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000E460 File Offset: 0x0000C660
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0000E468 File Offset: 0x0000C668
		public string RowError
		{
			get
			{
				return this.rowError;
			}
			set
			{
				this.rowError = value;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000E474 File Offset: 0x0000C674
		internal int IndexFromVersion(DataRowVersion version)
		{
			if (version == DataRowVersion.Original)
			{
				return this.AssertValidVersionIndex(version, this.Original);
			}
			if (version == DataRowVersion.Current)
			{
				return this.AssertValidVersionIndex(version, this.Current);
			}
			if (version == DataRowVersion.Proposed)
			{
				return this.AssertValidVersionIndex(version, this.Proposed);
			}
			if (version != DataRowVersion.Default)
			{
				throw new DataException("Version must be Original, Current, or Proposed.");
			}
			if (this.Proposed >= 0)
			{
				return this.Proposed;
			}
			if (this.Current >= 0)
			{
				return this.Current;
			}
			if (this.Original < 0)
			{
				throw new RowNotInTableException("This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.");
			}
			throw new DeletedRowInaccessibleException("Deleted row information cannot be accessed through the row.");
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000E530 File Offset: 0x0000C730
		private int AssertValidVersionIndex(DataRowVersion version, int index)
		{
			if (index >= 0)
			{
				return index;
			}
			throw new VersionNotFoundException(string.Format("There is no {0} data to accces.", version));
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000E550 File Offset: 0x0000C750
		internal DataRowVersion VersionFromIndex(int index)
		{
			if (index < 0)
			{
				throw new ArgumentException("Index must not be negative.");
			}
			if (index == this.Current)
			{
				return DataRowVersion.Current;
			}
			if (index == this.Original)
			{
				return DataRowVersion.Original;
			}
			if (index == this.Proposed)
			{
				return DataRowVersion.Proposed;
			}
			throw new ArgumentException(string.Format("The index {0} does not belong to this row.", index));
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000E648 File Offset: 0x0000C848
		internal XmlDataDocument.XmlDataElement DataElement
		{
			get
			{
				if (this.mappedElement != null || this._table.DataSet == null || this._table.DataSet._xmlDataDocument == null)
				{
					return this.mappedElement;
				}
				this.mappedElement = new XmlDataDocument.XmlDataElement(this, this._table.Prefix, XmlHelper.Encode(this._table.TableName), this._table.Namespace, this._table.DataSet._xmlDataDocument);
				return this.mappedElement;
			}
			set
			{
				this.mappedElement = value;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x0000E654 File Offset: 0x0000C854
		internal void SetOriginalValue(string columnName, object val)
		{
			DataColumn dataColumn = this._table.Columns[columnName];
			this._table.ChangingDataColumn(this, dataColumn, val);
			if (this.Original < 0 || this.Original == this.Current)
			{
				this.Original = this.Table.RecordCache.NewRecord();
			}
			this.CheckValue(val, dataColumn);
			dataColumn[this.Original] = val;
		}

		/// <summary>Commits all the changes made to this row since the last time <see cref="M:System.Data.DataRow.AcceptChanges" /> was called.</summary>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600022F RID: 559 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		public void AcceptChanges()
		{
			this.EndEdit();
			this._table.ChangingDataRow(this, DataRowAction.Commit);
			this.CheckChildRows(DataRowAction.Commit);
			DataRowState rowState = this.RowState;
			switch (rowState)
			{
			case DataRowState.Detached:
				throw new RowNotInTableException("Cannot perform this operation on a row not in the table.");
			default:
				if (rowState == DataRowState.Deleted)
				{
					this.Detach();
					goto IL_0096;
				}
				if (rowState != DataRowState.Modified)
				{
					goto IL_0096;
				}
				break;
			case DataRowState.Added:
				break;
			}
			if (this.Original >= 0)
			{
				this.Table.RecordCache.DisposeRecord(this.Original);
			}
			this.Original = this.Current;
			IL_0096:
			this._table.ChangedDataRow(this, DataRowAction.Commit);
		}

		/// <summary>Starts an edit operation on a <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The method was called upon a deleted row. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000230 RID: 560 RVA: 0x0000E77C File Offset: 0x0000C97C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void BeginEdit()
		{
			if (this._inChangingEvent)
			{
				throw new InRowChangingEventException("Cannot call BeginEdit inside an OnRowChanging event.");
			}
			if (this.RowState == DataRowState.Deleted)
			{
				throw new DeletedRowInaccessibleException();
			}
			if (!this.HasVersion(DataRowVersion.Proposed))
			{
				this.Proposed = this.Table.RecordCache.NewRecord();
				int num = ((!this.HasVersion(DataRowVersion.Current)) ? this.Table.DefaultValuesRowIndex : this.Current);
				for (int i = 0; i < this.Table.Columns.Count; i++)
				{
					DataColumn dataColumn = this.Table.Columns[i];
					dataColumn.DataContainer.CopyValue(num, this.Proposed);
				}
			}
		}

		/// <summary>Cancels the current edit on the row.</summary>
		/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000231 RID: 561 RVA: 0x0000E844 File Offset: 0x0000CA44
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void CancelEdit()
		{
			if (this._inChangingEvent)
			{
				throw new InRowChangingEventException("Cannot call CancelEdit inside an OnRowChanging event.");
			}
			if (this.HasVersion(DataRowVersion.Proposed))
			{
				int proposed = this.Proposed;
				DataRowState rowState = this.RowState;
				this.Table.RecordCache.DisposeRecord(this.Proposed);
				this.Proposed = -1;
				foreach (object obj in this.Table.Indexes)
				{
					Index index = (Index)obj;
					index.Update(this, proposed, DataRowVersion.Proposed, rowState);
				}
			}
		}

		/// <summary>Clears the errors for the row. This includes the <see cref="P:System.Data.DataRow.RowError" /> and errors set with <see cref="M:System.Data.DataRow.SetColumnError(System.Int32,System.String)" />.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000232 RID: 562 RVA: 0x0000E914 File Offset: 0x0000CB14
		public void ClearErrors()
		{
			this.rowError = string.Empty;
			this.ColumnErrors.Clear();
		}

		/// <summary>Deletes the <see cref="T:System.Data.DataRow" />.</summary>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">The <see cref="T:System.Data.DataRow" /> has already been deleted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000233 RID: 563 RVA: 0x0000E92C File Offset: 0x0000CB2C
		public void Delete()
		{
			this._table.DeletingDataRow(this, DataRowAction.Delete);
			DataRowState rowState = this.RowState;
			switch (rowState)
			{
			case DataRowState.Detached:
				break;
			default:
				if (rowState != DataRowState.Deleted)
				{
					this.CheckChildRows(DataRowAction.Delete);
				}
				break;
			case DataRowState.Added:
				this.CheckChildRows(DataRowAction.Delete);
				this.Detach();
				break;
			}
			if (this.Current >= 0)
			{
				int num = this.Current;
				DataRowState rowState2 = this.RowState;
				if (this.Current != this.Original)
				{
					this._table.RecordCache.DisposeRecord(this.Current);
				}
				this.Current = -1;
				foreach (object obj in this.Table.Indexes)
				{
					Index index = (Index)obj;
					index.Update(this, num, DataRowVersion.Current, rowState2);
				}
			}
			this._table.DeletedDataRow(this, DataRowAction.Delete);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000EA60 File Offset: 0x0000CC60
		private void CheckChildRows(DataRowAction action)
		{
			DataSet dataSet = this._table.DataSet;
			if (dataSet == null || !dataSet.EnforceConstraints)
			{
				return;
			}
			if (this._table.Constraints.Count == 0)
			{
				return;
			}
			foreach (object obj in dataSet.Tables)
			{
				DataTable dataTable = (DataTable)obj;
				foreach (object obj2 in dataTable.Constraints)
				{
					Constraint constraint = (Constraint)obj2;
					ForeignKeyConstraint foreignKeyConstraint = constraint as ForeignKeyConstraint;
					if (foreignKeyConstraint != null && foreignKeyConstraint.RelatedTable == this._table)
					{
						switch (action)
						{
						case DataRowAction.Delete:
							this.CheckChildRows(foreignKeyConstraint, action, foreignKeyConstraint.DeleteRule);
							continue;
						default:
							if (action != DataRowAction.Commit)
							{
								this.CheckChildRows(foreignKeyConstraint, action, foreignKeyConstraint.UpdateRule);
								continue;
							}
							break;
						case DataRowAction.Rollback:
							break;
						}
						if (foreignKeyConstraint.AcceptRejectRule != AcceptRejectRule.None)
						{
							this.CheckChildRows(foreignKeyConstraint, action, Rule.Cascade);
						}
					}
				}
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000EBF4 File Offset: 0x0000CDF4
		private void CheckChildRows(ForeignKeyConstraint fkc, DataRowAction action, Rule rule)
		{
			DataRow[] childRows = this.GetChildRows(fkc, DataRowVersion.Current);
			if (childRows == null)
			{
				return;
			}
			switch (rule)
			{
			case Rule.None:
			{
				for (int i = 0; i < childRows.Length; i++)
				{
					if (childRows[i].RowState != DataRowState.Deleted)
					{
						string text = "Cannot change this row because constraints are enforced on relation " + fkc.ConstraintName + ", and changing this row will strand child rows.";
						string text2 = "Cannot delete this row because constraints are enforced on relation " + fkc.ConstraintName + ", and deleting this row will strand child rows.";
						string text3 = ((action != DataRowAction.Delete) ? text : text2);
						throw new InvalidConstraintException(text3);
					}
				}
				break;
			}
			case Rule.Cascade:
				switch (action)
				{
				case DataRowAction.Delete:
				{
					for (int j = 0; j < childRows.Length; j++)
					{
						if (childRows[j].RowState != DataRowState.Deleted)
						{
							childRows[j].Delete();
						}
					}
					break;
				}
				case DataRowAction.Change:
				{
					for (int k = 0; k < childRows.Length; k++)
					{
						for (int l = 0; l < fkc.Columns.Length; l++)
						{
							if (!fkc.RelatedColumns[l].DataContainer[this.Current].Equals(fkc.RelatedColumns[l].DataContainer[this.Proposed]))
							{
								childRows[k][fkc.Columns[l]] = this[fkc.RelatedColumns[l], DataRowVersion.Proposed];
							}
						}
					}
					break;
				}
				case DataRowAction.Rollback:
				{
					for (int m = 0; m < childRows.Length; m++)
					{
						if (childRows[m].RowState != DataRowState.Unchanged)
						{
							childRows[m].RejectChanges();
						}
					}
					break;
				}
				}
				break;
			case Rule.SetNull:
			{
				for (int n = 0; n < childRows.Length; n++)
				{
					DataRow dataRow = childRows[n];
					if (childRows[n].RowState != DataRowState.Deleted)
					{
						for (int num = 0; num < fkc.Columns.Length; num++)
						{
							dataRow.SetNull(fkc.Columns[num]);
						}
					}
				}
				break;
			}
			case Rule.SetDefault:
				if (childRows.Length > 0)
				{
					int defaultValuesRowIndex = childRows[0].Table.DefaultValuesRowIndex;
					foreach (DataRow dataRow2 in childRows)
					{
						if (dataRow2.RowState != DataRowState.Deleted)
						{
							int num3 = dataRow2.IndexFromVersion(DataRowVersion.Default);
							foreach (DataColumn dataColumn in fkc.Columns)
							{
								dataColumn.DataContainer.CopyValue(defaultValuesRowIndex, num3);
							}
						}
					}
				}
				break;
			}
		}

		/// <summary>Ends the edit occurring on the row.</summary>
		/// <exception cref="T:System.Data.InRowChangingEventException">The method was called inside the <see cref="E:System.Data.DataTable.RowChanging" /> event. </exception>
		/// <exception cref="T:System.Data.ConstraintException">The edit broke a constraint. </exception>
		/// <exception cref="T:System.Data.ReadOnlyException">The row belongs to the table and the edit tried to change the value of a read-only column. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">The edit tried to put a null value into a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000236 RID: 566 RVA: 0x0000EEB4 File Offset: 0x0000D0B4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void EndEdit()
		{
			if (this._inChangingEvent)
			{
				throw new InRowChangingEventException("Cannot call EndEdit inside an OnRowChanging event.");
			}
			if (this.RowState == DataRowState.Detached || !this.HasVersion(DataRowVersion.Proposed))
			{
				return;
			}
			this.CheckReadOnlyStatus();
			this._inChangingEvent = true;
			try
			{
				this._table.ChangingDataRow(this, DataRowAction.Change);
			}
			finally
			{
				this._inChangingEvent = false;
			}
			DataRowState rowState = this.RowState;
			int num = this.Current;
			this.Current = this.Proposed;
			this.Proposed = -1;
			foreach (object obj in this.Table.Indexes)
			{
				Index index = (Index)obj;
				index.Update(this, num, DataRowVersion.Current, rowState);
			}
			try
			{
				this.AssertConstraints();
				this.Proposed = this.Current;
				this.Current = num;
				this.CheckChildRows(DataRowAction.Change);
				this.Current = this.Proposed;
				this.Proposed = -1;
			}
			catch
			{
				int num2 = ((this.Proposed < 0) ? this.Current : this.Proposed);
				this.Current = num;
				foreach (object obj2 in this.Table.Indexes)
				{
					Index index2 = (Index)obj2;
					index2.Update(this, num2, DataRowVersion.Current, this.RowState);
				}
				throw;
			}
			if (this.Original != num)
			{
				this.Table.RecordCache.DisposeRecord(num);
			}
			if (this._rowChanged)
			{
				this._table.ChangedDataRow(this, DataRowAction.Change);
				this._rowChanged = false;
			}
		}

		/// <summary>Gets the child rows of this <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The relation is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000237 RID: 567 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		public DataRow[] GetChildRows(DataRelation relation)
		{
			return this.GetChildRows(relation, DataRowVersion.Default);
		}

		/// <summary>Gets the child rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of the <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000238 RID: 568 RVA: 0x0000F108 File Offset: 0x0000D308
		public DataRow[] GetChildRows(string relationName)
		{
			return this.GetChildRows(this.Table.DataSet.Relations[relationName]);
		}

		/// <summary>Gets the child rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000239 RID: 569 RVA: 0x0000F134 File Offset: 0x0000D334
		public DataRow[] GetChildRows(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return this.Table.NewRowArray(0);
			}
			if (this.Table == null)
			{
				throw new RowNotInTableException("This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.");
			}
			if (relation.DataSet != this.Table.DataSet)
			{
				throw new ArgumentException();
			}
			if (this._table != relation.ParentTable)
			{
				throw new InvalidConstraintException(string.Concat(new object[] { "GetChildRow requires a row whose Table is ", relation.ParentTable, ", but the specified row's table is ", this._table }));
			}
			if (relation.ChildKeyConstraint != null)
			{
				return this.GetChildRows(relation.ChildKeyConstraint, version);
			}
			ArrayList arrayList = new ArrayList();
			DataColumn[] parentColumns = relation.ParentColumns;
			DataColumn[] childColumns = relation.ChildColumns;
			int num = parentColumns.Length;
			DataRow[] array = null;
			int num2 = this.IndexFromVersion(version);
			int num3 = relation.ChildTable.RecordCache.NewRecord();
			try
			{
				for (int i = 0; i < num; i++)
				{
					childColumns[i].DataContainer.CopyValue(parentColumns[i].DataContainer, num2, num3);
				}
				Index index = relation.ChildTable.FindIndex(childColumns);
				if (index != null)
				{
					int[] array2 = index.FindAll(num3);
					array = relation.ChildTable.NewRowArray(array2.Length);
					for (int j = 0; j < array2.Length; j++)
					{
						array[j] = relation.ChildTable.RecordCache[array2[j]];
					}
				}
				else
				{
					foreach (object obj in relation.ChildTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						bool flag = false;
						if (dataRow.HasVersion(DataRowVersion.Default))
						{
							flag = true;
							int num4 = dataRow.IndexFromVersion(DataRowVersion.Default);
							for (int k = 0; k < num; k++)
							{
								if (childColumns[k].DataContainer.CompareValues(num4, num3) != 0)
								{
									flag = false;
									break;
								}
							}
						}
						if (flag)
						{
							arrayList.Add(dataRow);
						}
					}
					array = relation.ChildTable.NewRowArray(arrayList.Count);
					arrayList.CopyTo(array, 0);
				}
			}
			finally
			{
				relation.ChildTable.RecordCache.DisposeRecord(num3);
			}
			return array;
		}

		/// <summary>Gets the child rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of the <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600023A RID: 570 RVA: 0x0000F3D4 File Offset: 0x0000D5D4
		public DataRow[] GetChildRows(string relationName, DataRowVersion version)
		{
			return this.GetChildRows(this.Table.DataSet.Relations[relationName], version);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000F400 File Offset: 0x0000D600
		private DataRow[] GetChildRows(ForeignKeyConstraint fkc, DataRowVersion version)
		{
			ArrayList arrayList = new ArrayList();
			DataColumn[] relatedColumns = fkc.RelatedColumns;
			DataColumn[] columns = fkc.Columns;
			int num = relatedColumns.Length;
			Index index = fkc.Index;
			int num2 = this.IndexFromVersion(version);
			int num3 = fkc.Table.RecordCache.NewRecord();
			for (int i = 0; i < num; i++)
			{
				columns[i].DataContainer.CopyValue(relatedColumns[i].DataContainer, num2, num3);
			}
			try
			{
				if (index != null)
				{
					int[] array = index.FindAll(num3);
					for (int j = 0; j < array.Length; j++)
					{
						arrayList.Add(columns[j].Table.RecordCache[array[j]]);
					}
				}
				else
				{
					foreach (object obj in fkc.Table.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						bool flag = false;
						if (dataRow.HasVersion(DataRowVersion.Default))
						{
							flag = true;
							int num4 = dataRow.IndexFromVersion(DataRowVersion.Default);
							for (int k = 0; k < num; k++)
							{
								if (columns[k].DataContainer.CompareValues(num4, num3) != 0)
								{
									flag = false;
									break;
								}
							}
						}
						if (flag)
						{
							arrayList.Add(dataRow);
						}
					}
				}
			}
			finally
			{
				fkc.Table.RecordCache.DisposeRecord(num3);
			}
			DataRow[] array2 = fkc.Table.NewRowArray(arrayList.Count);
			arrayList.CopyTo(array2, 0);
			return array2;
		}

		/// <summary>Gets the error description of the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <returns>The text of the error description.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600023C RID: 572 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		public string GetColumnError(DataColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column");
			}
			int num = this._table.Columns.IndexOf(column);
			if (num < 0)
			{
				throw new ArgumentException(string.Format("Column '{0}' does not belong to table {1}.", column.ColumnName, this.Table.TableName));
			}
			return this.GetColumnError(num);
		}

		/// <summary>Gets the error description for the column specified by index.</summary>
		/// <returns>The text of the error description.</returns>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600023D RID: 573 RVA: 0x0000F648 File Offset: 0x0000D848
		public string GetColumnError(int columnIndex)
		{
			if (columnIndex < 0 || columnIndex >= this.Table.Columns.Count)
			{
				throw new IndexOutOfRangeException();
			}
			string text = null;
			if (columnIndex < this.ColumnErrors.Count)
			{
				text = (string)this.ColumnErrors[columnIndex];
			}
			return (text == null) ? string.Empty : text;
		}

		/// <summary>Gets the error description for a column, specified by name.</summary>
		/// <returns>The text of the error description.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600023E RID: 574 RVA: 0x0000F6B0 File Offset: 0x0000D8B0
		public string GetColumnError(string columnName)
		{
			return this.GetColumnError(this._table.Columns.IndexOf(columnName));
		}

		/// <summary>Gets an array of columns that have errors.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that contain errors.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600023F RID: 575 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public DataColumn[] GetColumnsInError()
		{
			ArrayList arrayList = new ArrayList();
			int num = 0;
			foreach (object obj in this.ColumnErrors)
			{
				string text = (string)obj;
				if (text != null && text != string.Empty)
				{
					arrayList.Add(this._table.Columns[num]);
				}
				num++;
			}
			return (DataColumn[])arrayList.ToArray(typeof(DataColumn));
		}

		/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> does not belong to the <see cref="T:System.Data.DataTable" />.The row is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">This row does not belong to the child table of the <see cref="T:System.Data.DataRelation" /> object. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000240 RID: 576 RVA: 0x0000F788 File Offset: 0x0000D988
		public DataRow GetParentRow(DataRelation relation)
		{
			return this.GetParentRow(relation, DataRowVersion.Default);
		}

		/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000241 RID: 577 RVA: 0x0000F798 File Offset: 0x0000D998
		public DataRow GetParentRow(string relationName)
		{
			return this.GetParentRow(relationName, DataRowVersion.Default);
		}

		/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. </param>
		/// <exception cref="T:System.ArgumentNullException">The row is null.The <paramref name="relation" /> does not belong to this table's parent relations. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have this version of data. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000242 RID: 578 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		public DataRow GetParentRow(DataRelation relation, DataRowVersion version)
		{
			DataRow[] parentRows = this.GetParentRows(relation, version);
			if (parentRows.Length == 0)
			{
				return null;
			}
			return parentRows[0];
		}

		/// <summary>Gets the parent row of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataRow" /> of the current row.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000243 RID: 579 RVA: 0x0000F7CC File Offset: 0x0000D9CC
		public DataRow GetParentRow(string relationName, DataRowVersion version)
		{
			return this.GetParentRow(this.Table.DataSet.Relations[relationName], version);
		}

		/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.DataRelation" /> does not belong to this row's <see cref="T:System.Data.DataSet" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The row is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a <see cref="T:System.Data.DataTable" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000244 RID: 580 RVA: 0x0000F7F8 File Offset: 0x0000D9F8
		public DataRow[] GetParentRows(DataRelation relation)
		{
			return this.GetParentRows(relation, DataRowVersion.Default);
		}

		/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000245 RID: 581 RVA: 0x0000F808 File Offset: 0x0000DA08
		public DataRow[] GetParentRows(string relationName)
		{
			return this.GetParentRows(relationName, DataRowVersion.Default);
		}

		/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relation">The <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. </param>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Data.DataRelation" /> does not belong to this row's <see cref="T:System.Data.DataSet" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The row is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child table is not the table the row belongs to. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to a <see cref="T:System.Data.DataTable" />. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000246 RID: 582 RVA: 0x0000F818 File Offset: 0x0000DA18
		public DataRow[] GetParentRows(DataRelation relation, DataRowVersion version)
		{
			if (relation == null)
			{
				return this.Table.NewRowArray(0);
			}
			if (this.Table == null)
			{
				throw new RowNotInTableException("This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.");
			}
			if (relation.DataSet != this.Table.DataSet)
			{
				throw new ArgumentException();
			}
			if (this._table != relation.ChildTable)
			{
				throw new InvalidConstraintException(string.Concat(new object[] { "GetParentRows requires a row whose Table is ", relation.ChildTable, ", but the specified row's table is ", this._table }));
			}
			ArrayList arrayList = new ArrayList();
			DataColumn[] parentColumns = relation.ParentColumns;
			DataColumn[] childColumns = relation.ChildColumns;
			int num = parentColumns.Length;
			int num2 = this.IndexFromVersion(version);
			int num3 = relation.ParentTable.RecordCache.NewRecord();
			for (int i = 0; i < num; i++)
			{
				parentColumns[i].DataContainer.CopyValue(childColumns[i].DataContainer, num2, num3);
			}
			try
			{
				Index index = relation.ParentTable.FindIndex(parentColumns);
				if (index != null)
				{
					int[] array = index.FindAll(num3);
					for (int j = 0; j < array.Length; j++)
					{
						arrayList.Add(parentColumns[j].Table.RecordCache[array[j]]);
					}
				}
				else
				{
					foreach (object obj in relation.ParentTable.Rows)
					{
						DataRow dataRow = (DataRow)obj;
						bool flag = false;
						if (dataRow.HasVersion(DataRowVersion.Default))
						{
							flag = true;
							int num4 = dataRow.IndexFromVersion(DataRowVersion.Default);
							for (int k = 0; k < num; k++)
							{
								if (parentColumns[k].DataContainer.CompareValues(num4, num3) != 0)
								{
									flag = false;
									break;
								}
							}
						}
						if (flag)
						{
							arrayList.Add(dataRow);
						}
					}
				}
			}
			finally
			{
				relation.ParentTable.RecordCache.DisposeRecord(num3);
			}
			DataRow[] array2 = relation.ParentTable.NewRowArray(arrayList.Count);
			arrayList.CopyTo(array2, 0);
			return array2;
		}

		/// <summary>Gets the parent rows of a <see cref="T:System.Data.DataRow" /> using the specified <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />, and <see cref="T:System.Data.DataRowVersion" />.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataRow" /> objects or an array of length zero.</returns>
		/// <param name="relationName">The <see cref="P:System.Data.DataRelation.RelationName" /> of a <see cref="T:System.Data.DataRelation" />. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values specifying the version of the data to get. Possible values are Default, Original, Current, and Proposed. </param>
		/// <exception cref="T:System.ArgumentException">The relation and row do not belong to the same table. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> is null. </exception>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <exception cref="T:System.Data.VersionNotFoundException">The row does not have the requested <see cref="T:System.Data.DataRowVersion" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000247 RID: 583 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public DataRow[] GetParentRows(string relationName, DataRowVersion version)
		{
			return this.GetParentRows(this.Table.DataSet.Relations[relationName], version);
		}

		/// <summary>Gets a value that indicates whether a specified version exists.</summary>
		/// <returns>true if the version exists; otherwise, false.</returns>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000248 RID: 584 RVA: 0x0000FABC File Offset: 0x0000DCBC
		public bool HasVersion(DataRowVersion version)
		{
			if (version == DataRowVersion.Original)
			{
				return this.Original >= 0;
			}
			if (version == DataRowVersion.Current)
			{
				return this.Current >= 0;
			}
			if (version == DataRowVersion.Proposed)
			{
				return this.Proposed >= 0;
			}
			if (version != DataRowVersion.Default)
			{
				return this.IndexFromVersion(version) >= 0;
			}
			return this.Proposed >= 0 || this.Current >= 0;
		}

		/// <summary>Gets a value that indicates whether the specified <see cref="T:System.Data.DataColumn" /> contains a null value.</summary>
		/// <returns>true if the column contains a null value; otherwise, false.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000249 RID: 585 RVA: 0x0000FB4C File Offset: 0x0000DD4C
		public bool IsNull(DataColumn column)
		{
			return this.IsNull(column, DataRowVersion.Default);
		}

		/// <summary>Gets a value that indicates whether the column at the specified index contains a null value.</summary>
		/// <returns>true if the column contains a null value; otherwise, false.</returns>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600024A RID: 586 RVA: 0x0000FB5C File Offset: 0x0000DD5C
		public bool IsNull(int columnIndex)
		{
			return this.IsNull(this.Table.Columns[columnIndex]);
		}

		/// <summary>Gets a value that indicates whether the named column contains a null value.</summary>
		/// <returns>true if the column contains a null value; otherwise, false.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600024B RID: 587 RVA: 0x0000FB80 File Offset: 0x0000DD80
		public bool IsNull(string columnName)
		{
			return this.IsNull(this.Table.Columns[columnName]);
		}

		/// <summary>Gets a value that indicates whether the specified <see cref="T:System.Data.DataColumn" /> and <see cref="T:System.Data.DataRowVersion" /> contains a null value.</summary>
		/// <returns>true if the column contains a null value; otherwise, false.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" />. </param>
		/// <param name="version">One of the <see cref="T:System.Data.DataRowVersion" /> values that specifies the row version. Possible values are Default, Original, Current, and Proposed. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600024C RID: 588 RVA: 0x0000FBA4 File Offset: 0x0000DDA4
		public bool IsNull(DataColumn column, DataRowVersion version)
		{
			object obj = this[column, version];
			return column.DataContainer.IsNull(this.IndexFromVersion(version));
		}

		// Token: 0x0600024D RID: 589 RVA: 0x0000FBCC File Offset: 0x0000DDCC
		internal bool IsNullColumns(DataColumn[] columns)
		{
			int i;
			for (i = 0; i < columns.Length; i++)
			{
				if (!this.IsNull(columns[i]))
				{
					break;
				}
			}
			return i == columns.Length;
		}

		/// <summary>Rejects all changes made to the row since <see cref="M:System.Data.DataRow.AcceptChanges" /> was last called.</summary>
		/// <exception cref="T:System.Data.RowNotInTableException">The row does not belong to the table. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600024E RID: 590 RVA: 0x0000FC08 File Offset: 0x0000DE08
		public void RejectChanges()
		{
			if (this.RowState == DataRowState.Detached)
			{
				throw new RowNotInTableException("This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.");
			}
			this.Table.ChangingDataRow(this, DataRowAction.Rollback);
			DataRowState rowState = this.RowState;
			if (rowState != DataRowState.Added)
			{
				if (rowState != DataRowState.Deleted)
				{
					if (rowState == DataRowState.Modified)
					{
						int num = this.Current;
						this.Table.RecordCache.DisposeRecord(this.Current);
						this.CheckChildRows(DataRowAction.Rollback);
						this.Current = this.Original;
						foreach (object obj in this.Table.Indexes)
						{
							Index index = (Index)obj;
							index.Update(this, num, DataRowVersion.Current, DataRowState.Modified);
						}
					}
				}
				else
				{
					this.CheckChildRows(DataRowAction.Rollback);
					this.Current = this.Original;
					this.Validate();
				}
			}
			else
			{
				this.Detach();
			}
			this.Table.ChangedDataRow(this, DataRowAction.Rollback);
		}

		/// <summary>Sets the error description for a column specified as a <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to set the error description for. </param>
		/// <param name="error">The error description. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600024F RID: 591 RVA: 0x0000FD38 File Offset: 0x0000DF38
		public void SetColumnError(DataColumn column, string error)
		{
			this.SetColumnError(this._table.Columns.IndexOf(column), error);
		}

		/// <summary>Sets the error description for a column specified by index.</summary>
		/// <param name="columnIndex">The zero-based index of the column. </param>
		/// <param name="error">The error description. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The <paramref name="columnIndex" /> argument is out of range </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000250 RID: 592 RVA: 0x0000FD54 File Offset: 0x0000DF54
		public void SetColumnError(int columnIndex, string error)
		{
			if (columnIndex < 0 || columnIndex >= this.Table.Columns.Count)
			{
				throw new IndexOutOfRangeException();
			}
			while (columnIndex >= this.ColumnErrors.Count)
			{
				this.ColumnErrors.Add(null);
			}
			this.ColumnErrors[columnIndex] = error;
		}

		/// <summary>Sets the error description for a column specified by name.</summary>
		/// <param name="columnName">The name of the column. </param>
		/// <param name="error">The error description. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000251 RID: 593 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		public void SetColumnError(string columnName, string error)
		{
			this.SetColumnError(this._table.Columns.IndexOf(columnName), error);
		}

		/// <summary>Sets the value of the specified <see cref="T:System.Data.DataColumn" /> to a null value.</summary>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" />. </param>
		// Token: 0x06000252 RID: 594 RVA: 0x0000FDD0 File Offset: 0x0000DFD0
		protected void SetNull(DataColumn column)
		{
			this[column] = DBNull.Value;
		}

		/// <summary>Sets the parent row of a <see cref="T:System.Data.DataRow" /> with specified new parent <see cref="T:System.Data.DataRow" />.</summary>
		/// <param name="parentRow">The new parent <see cref="T:System.Data.DataRow" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000253 RID: 595 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public void SetParentRow(DataRow parentRow)
		{
			this.SetParentRow(parentRow, null);
		}

		/// <summary>Sets the parent row of a <see cref="T:System.Data.DataRow" /> with specified new parent <see cref="T:System.Data.DataRow" /> and <see cref="T:System.Data.DataRelation" />.</summary>
		/// <param name="parentRow">The new parent <see cref="T:System.Data.DataRow" />. </param>
		/// <param name="relation">The relation <see cref="T:System.Data.DataRelation" /> to use. </param>
		/// <exception cref="T:System.Data.RowNotInTableException">One of the rows does not belong to a table </exception>
		/// <exception cref="T:System.ArgumentNullException">One of the rows is null. </exception>
		/// <exception cref="T:System.ArgumentException">The relation does not belong to the <see cref="T:System.Data.DataRelationCollection" /> of the <see cref="T:System.Data.DataSet" /> object. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation's child <see cref="T:System.Data.DataTable" /> is not the table this row belongs to. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000254 RID: 596 RVA: 0x0000FDEC File Offset: 0x0000DFEC
		public void SetParentRow(DataRow parentRow, DataRelation relation)
		{
			if (this._table == null || parentRow.Table == null)
			{
				throw new RowNotInTableException("This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.");
			}
			if (parentRow != null && this._table.DataSet != parentRow.Table.DataSet)
			{
				throw new ArgumentException();
			}
			if (this.RowState == DataRowState.Detached && !this.HasVersion(DataRowVersion.Default))
			{
				throw new RowNotInTableException("This row has been removed from a table and does not have any data.  BeginEdit() will allow creation of new data in this row.");
			}
			this.BeginEdit();
			IEnumerable enumerable;
			if (relation == null)
			{
				enumerable = this._table.ParentRelations;
			}
			else
			{
				enumerable = new DataRelation[] { relation };
			}
			foreach (object obj in enumerable)
			{
				DataRelation dataRelation = (DataRelation)obj;
				DataColumn[] childColumns = dataRelation.ChildColumns;
				DataColumn[] parentColumns = dataRelation.ParentColumns;
				for (int i = 0; i < parentColumns.Length; i++)
				{
					if (parentRow == null)
					{
						childColumns[i].DataContainer[this.Proposed] = DBNull.Value;
					}
					else
					{
						int num = parentRow.IndexFromVersion(DataRowVersion.Default);
						childColumns[i].DataContainer.CopyValue(parentColumns[i].DataContainer, num, this.Proposed);
					}
				}
			}
			this.EndEdit();
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000FF68 File Offset: 0x0000E168
		internal void CopyValuesToRow(DataRow row)
		{
			if (row == null)
			{
				throw new ArgumentNullException("row");
			}
			if (row == this)
			{
				throw new ArgumentException("'row' is the same as this object");
			}
			if (this.HasVersion(DataRowVersion.Original))
			{
				if (row.Original < 0)
				{
					row.Original = row.Table.RecordCache.NewRecord();
				}
				else if (row.Original == row.Current)
				{
					row.Original = row.Table.RecordCache.NewRecord();
					row.Table.RecordCache.CopyRecord(row.Table, row.Current, row.Original);
				}
			}
			else if (row.Original > 0)
			{
				if (row.Original != row.Current)
				{
					row.Table.RecordCache.DisposeRecord(row.Original);
				}
				row.Original = -1;
			}
			if (this.HasVersion(DataRowVersion.Current))
			{
				if (this.Current == this.Original)
				{
					if (row.Current >= 0)
					{
						row.Table.RecordCache.DisposeRecord(row.Current);
					}
					row.Current = row.Original;
				}
				else if (row.Current < 0)
				{
					row.Current = row.Table.RecordCache.NewRecord();
				}
			}
			else if (row.Current > 0)
			{
				row.Table.RecordCache.DisposeRecord(row.Current);
				row.Current = -1;
			}
			if (this.HasVersion(DataRowVersion.Proposed))
			{
				if (row.Proposed < 0)
				{
					row.Proposed = row.Table.RecordCache.NewRecord();
				}
			}
			else if (row.Proposed > 0)
			{
				row.Table.RecordCache.DisposeRecord(row.Proposed);
				row.Proposed = -1;
			}
			foreach (object obj in this.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				DataColumn dataColumn2 = row.Table.Columns[dataColumn.ColumnName];
				if (dataColumn2 != null)
				{
					if (this.HasVersion(DataRowVersion.Original))
					{
						object obj2 = dataColumn[this.Original];
						row.CheckValue(obj2, dataColumn2);
						dataColumn2[row.Original] = obj2;
					}
					if (this.HasVersion(DataRowVersion.Current) && this.Current != this.Original)
					{
						object obj3 = dataColumn[this.Current];
						row.CheckValue(obj3, dataColumn2);
						dataColumn2[row.Current] = obj3;
					}
					if (this.HasVersion(DataRowVersion.Proposed))
					{
						object obj4 = dataColumn[row.Proposed];
						row.CheckValue(obj4, dataColumn2);
						dataColumn2[row.Proposed] = obj4;
					}
				}
			}
			if (this.HasErrors)
			{
				this.CopyErrors(row);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0001029C File Offset: 0x0000E49C
		internal void MergeValuesToRow(DataRow row, bool preserveChanges)
		{
			if (row == null)
			{
				throw new ArgumentNullException("row");
			}
			if (row == this)
			{
				throw new ArgumentException("'row' is the same as this object");
			}
			if (this.HasVersion(DataRowVersion.Original))
			{
				if (row.Original < 0)
				{
					row.Original = row.Table.RecordCache.NewRecord();
				}
				else if (row.Original == row.Current && (this.Original != this.Current || preserveChanges))
				{
					row.Original = row.Table.RecordCache.NewRecord();
					row.Table.RecordCache.CopyRecord(row.Table, row.Current, row.Original);
				}
			}
			else if (row.Original == row.Current)
			{
				row.Original = row.Table.RecordCache.NewRecord();
				row.Table.RecordCache.CopyRecord(row.Table, row.Current, row.Original);
			}
			if (this.HasVersion(DataRowVersion.Current))
			{
				if (!preserveChanges && row.Current < 0)
				{
					row.Current = row.Table.RecordCache.NewRecord();
				}
			}
			else if (row.Current > 0 && !preserveChanges)
			{
				row.Table.RecordCache.DisposeRecord(row.Current);
				row.Current = -1;
			}
			foreach (object obj in this.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				DataColumn dataColumn2 = row.Table.Columns[dataColumn.ColumnName];
				if (dataColumn2 != null)
				{
					if (this.HasVersion(DataRowVersion.Original))
					{
						object obj2 = dataColumn[this.Original];
						row.CheckValue(obj2, dataColumn2);
						dataColumn2[row.Original] = obj2;
					}
					if (this.HasVersion(DataRowVersion.Current) && !preserveChanges)
					{
						object obj3 = dataColumn[this.Current];
						row.CheckValue(obj3, dataColumn2);
						dataColumn2[row.Current] = obj3;
					}
				}
			}
			if (this.HasErrors)
			{
				this.CopyErrors(row);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00010520 File Offset: 0x0000E720
		internal void CopyErrors(DataRow row)
		{
			row.RowError = this.RowError;
			DataColumn[] columnsInError = this.GetColumnsInError();
			foreach (DataColumn dataColumn in columnsInError)
			{
				DataColumn dataColumn2 = row.Table.Columns[dataColumn.ColumnName];
				row.SetColumnError(dataColumn2, this.GetColumnError(dataColumn));
			}
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00010584 File Offset: 0x0000E784
		internal bool IsRowChanged(DataRowState rowState)
		{
			if ((this.RowState & rowState) != (DataRowState)0)
			{
				return true;
			}
			DataRowVersion dataRowVersion = ((rowState != DataRowState.Deleted) ? DataRowVersion.Current : DataRowVersion.Original);
			int count = this.Table.ChildRelations.Count;
			for (int i = 0; i < count; i++)
			{
				DataRelation dataRelation = this.Table.ChildRelations[i];
				DataRow[] childRows = this.GetChildRows(dataRelation, dataRowVersion);
				for (int j = 0; j < childRows.Length; j++)
				{
					if (childRows[j].IsRowChanged(rowState))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00010624 File Offset: 0x0000E824
		internal void Validate()
		{
			this.Table.AddRowToIndexes(this);
			this.AssertConstraints();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00010638 File Offset: 0x0000E838
		private void AssertConstraints()
		{
			if (this.Table == null || this.Table._duringDataLoad)
			{
				return;
			}
			if (this.Table.DataSet != null && !this.Table.DataSet.EnforceConstraints)
			{
				return;
			}
			for (int i = 0; i < this.Table.Columns.Count; i++)
			{
				DataColumn dataColumn = this.Table.Columns[i];
				if (!dataColumn.AllowDBNull && this.IsNull(dataColumn))
				{
					throw new NoNullAllowedException(this._nullConstraintMessage);
				}
			}
			foreach (object obj in this.Table.Constraints)
			{
				Constraint constraint = (Constraint)obj;
				try
				{
					constraint.AssertConstraint(this);
				}
				catch (Exception ex)
				{
					this.Table.DeleteRowFromIndexes(this);
					throw ex;
				}
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0001077C File Offset: 0x0000E97C
		internal void CheckNullConstraints()
		{
			if (this._nullConstraintViolation)
			{
				if (this.HasVersion(DataRowVersion.Proposed))
				{
					foreach (object obj in this.Table.Columns)
					{
						DataColumn dataColumn = (DataColumn)obj;
						if (this.IsNull(dataColumn) && !dataColumn.AllowDBNull)
						{
							throw new NoNullAllowedException(this._nullConstraintMessage);
						}
					}
				}
				this._nullConstraintViolation = false;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00010830 File Offset: 0x0000EA30
		internal void CheckReadOnlyStatus()
		{
			int num = this.IndexFromVersion(DataRowVersion.Default);
			foreach (object obj in this.Table.Columns)
			{
				DataColumn dataColumn = (DataColumn)obj;
				if (dataColumn.DataContainer.CompareValues(num, this.Proposed) != 0 && dataColumn.ReadOnly)
				{
					throw new ReadOnlyException();
				}
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000108D4 File Offset: 0x0000EAD4
		internal void Load(object[] values, LoadOption loadOption)
		{
			if (loadOption == LoadOption.OverwriteChanges || (loadOption == LoadOption.PreserveChanges && this.RowState == DataRowState.Unchanged))
			{
				this.Table.ChangingDataRow(this, DataRowAction.ChangeCurrentAndOriginal);
				int num = this.Table.CreateRecord(values);
				this.Table.DeleteRowFromIndexes(this);
				if (this.HasVersion(DataRowVersion.Original) && this.Current != this.Original)
				{
					this.Table.RecordCache.DisposeRecord(this.Original);
				}
				this.Original = num;
				if (this.HasVersion(DataRowVersion.Current))
				{
					this.Table.RecordCache.DisposeRecord(this.Current);
				}
				this.Current = num;
				this.Table.AddRowToIndexes(this);
				this.Table.ChangedDataRow(this, DataRowAction.ChangeCurrentAndOriginal);
				return;
			}
			if (loadOption == LoadOption.PreserveChanges)
			{
				this.Table.ChangingDataRow(this, DataRowAction.ChangeOriginal);
				int num = this.Table.CreateRecord(values);
				if (this.HasVersion(DataRowVersion.Original) && this.Current != this.Original)
				{
					this.Table.RecordCache.DisposeRecord(this.Original);
				}
				this.Original = num;
				this.Table.ChangedDataRow(this, DataRowAction.ChangeOriginal);
				return;
			}
			if (this.RowState != DataRowState.Deleted)
			{
				int num2 = ((!this.HasVersion(DataRowVersion.Proposed)) ? this.Current : this.Proposed);
				int num = this.Table.CreateRecord(values);
				if (this.RowState == DataRowState.Added || this.Table.CompareRecords(num2, num) != 0)
				{
					this.Table.ChangingDataRow(this, DataRowAction.Change);
					this.Table.DeleteRowFromIndexes(this);
					if (this.HasVersion(DataRowVersion.Proposed))
					{
						this.Table.RecordCache.DisposeRecord(this.Proposed);
						this.Proposed = -1;
					}
					if (this.Original != this.Current)
					{
						this.Table.RecordCache.DisposeRecord(this.Current);
					}
					this.Current = num;
					this.Table.AddRowToIndexes(this);
					this.Table.ChangedDataRow(this, DataRowAction.Change);
				}
				else
				{
					this.Table.ChangingDataRow(this, DataRowAction.Nothing);
					this.Table.RecordCache.DisposeRecord(num);
					this.Table.ChangedDataRow(this, DataRowAction.Nothing);
				}
			}
		}

		// Token: 0x040000EA RID: 234
		private DataTable _table;

		// Token: 0x040000EB RID: 235
		internal int _original = -1;

		// Token: 0x040000EC RID: 236
		internal int _current = -1;

		// Token: 0x040000ED RID: 237
		internal int _proposed = -1;

		// Token: 0x040000EE RID: 238
		private ArrayList _columnErrors;

		// Token: 0x040000EF RID: 239
		private string rowError;

		// Token: 0x040000F0 RID: 240
		internal int xmlRowID;

		// Token: 0x040000F1 RID: 241
		internal bool _nullConstraintViolation;

		// Token: 0x040000F2 RID: 242
		private string _nullConstraintMessage;

		// Token: 0x040000F3 RID: 243
		private bool _inChangingEvent;

		// Token: 0x040000F4 RID: 244
		private int _rowId;

		// Token: 0x040000F5 RID: 245
		internal bool _rowChanged;

		// Token: 0x040000F6 RID: 246
		private XmlDataDocument.XmlDataElement mappedElement;

		// Token: 0x040000F7 RID: 247
		internal bool _inExpressionEvaluation;
	}
}
