using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Represents a collection of <see cref="T:System.Data.DataColumn" /> objects for a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200001F RID: 31
	[DefaultEvent("CollectionChanged")]
	[Editor("Microsoft.VSDesigner.Data.Design.ColumnsCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class DataColumnCollection : InternalDataCollectionBase
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000AC8C File Offset: 0x00008E8C
		internal DataColumnCollection(DataTable table)
		{
			this.parentTable = table;
		}

		/// <summary>Occurs when the columns collection changes, either by adding or removing a column.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000161 RID: 353 RVA: 0x0000AD48 File Offset: 0x00008F48
		// (remove) Token: 0x06000162 RID: 354 RVA: 0x0000AD64 File Offset: 0x00008F64
		[ResDescription("Occurs whenever this collection's membership changes.")]
		public event CollectionChangeEventHandler CollectionChanged;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000163 RID: 355 RVA: 0x0000AD80 File Offset: 0x00008F80
		// (remove) Token: 0x06000164 RID: 356 RVA: 0x0000AD9C File Offset: 0x00008F9C
		internal event CollectionChangeEventHandler CollectionMetaDataChanged;

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the column to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000025 RID: 37
		public DataColumn this[int index]
		{
			get
			{
				if (index < 0 || index >= base.List.Count)
				{
					throw new IndexOutOfRangeException("Cannot find column " + index + ".");
				}
				return (DataColumn)base.List[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataColumn" /> from the collection with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.DataColumn" /> in the collection with the specified <see cref="P:System.Data.DataColumn.ColumnName" />; otherwise a null value if the <see cref="T:System.Data.DataColumn" /> does not exist.</returns>
		/// <param name="name">The <see cref="P:System.Data.DataColumn.ColumnName" /> of the column to return. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000026 RID: 38
		public DataColumn this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				DataColumn dataColumn = this.columnFromName[name] as DataColumn;
				if (dataColumn != null)
				{
					return dataColumn;
				}
				int num = this.IndexOf(name, true);
				return (num != -1) ? ((DataColumn)base.List[num]) : null;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000AE6C File Offset: 0x0000906C
		protected override ArrayList List
		{
			get
			{
				return base.List;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000AE74 File Offset: 0x00009074
		internal ArrayList AutoIncrmentColumns
		{
			get
			{
				return this.autoIncrement;
			}
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000169 RID: 361 RVA: 0x0000AE7C File Offset: 0x0000907C
		public DataColumn Add()
		{
			DataColumn dataColumn = new DataColumn(null);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Copies the entire collection into an existing array, starting at a specified index within the array.</summary>
		/// <param name="array">An array of <see cref="T:System.Data.DataColumn" /> objects to copy the collection into. </param>
		/// <param name="index">The index to start from.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600016A RID: 362 RVA: 0x0000AE98 File Offset: 0x00009098
		public void CopyTo(DataColumn[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000AEA4 File Offset: 0x000090A4
		internal void RegisterName(string name, DataColumn column)
		{
			try
			{
				this.columnFromName.Add(name, column);
			}
			catch (ArgumentException)
			{
				throw new DuplicateNameException("A DataColumn named '" + name + "' already belongs to this DataTable.");
			}
			Doublet doublet = (Doublet)this.columnNameCount[name];
			if (doublet != null)
			{
				doublet.count++;
				doublet.columnNames.Add(name);
			}
			else
			{
				doublet = new Doublet(1, name);
				this.columnNameCount[name] = doublet;
			}
			if (name.Length <= DataColumnCollection.ColumnPrefix.Length || !name.StartsWith(DataColumnCollection.ColumnPrefix, StringComparison.Ordinal))
			{
				return;
			}
			if (name == DataColumnCollection.MakeName(this.defaultColumnIndex + 1))
			{
				do
				{
					this.defaultColumnIndex++;
				}
				while (this.Contains(DataColumnCollection.MakeName(this.defaultColumnIndex + 1)));
			}
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000AFB0 File Offset: 0x000091B0
		internal void UnregisterName(string name)
		{
			if (this.columnFromName.Contains(name))
			{
				this.columnFromName.Remove(name);
			}
			Doublet doublet = (Doublet)this.columnNameCount[name];
			if (doublet != null)
			{
				doublet.count--;
				doublet.columnNames.Remove(name);
				if (doublet.count == 0)
				{
					this.columnNameCount.Remove(name);
				}
			}
			if (name.StartsWith(DataColumnCollection.ColumnPrefix) && name == DataColumnCollection.MakeName(this.defaultColumnIndex - 1))
			{
				do
				{
					this.defaultColumnIndex--;
				}
				while (!this.Contains(DataColumnCollection.MakeName(this.defaultColumnIndex - 1)) && this.defaultColumnIndex > 1);
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000B080 File Offset: 0x00009280
		private string GetNextDefaultColumnName()
		{
			string text = DataColumnCollection.MakeName(this.defaultColumnIndex);
			int num = this.defaultColumnIndex + 1;
			while (this.Contains(text))
			{
				text = DataColumnCollection.MakeName(num);
				this.defaultColumnIndex++;
				num++;
			}
			this.defaultColumnIndex++;
			return text;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000B0DC File Offset: 0x000092DC
		private static string MakeName(int index)
		{
			if (index < 10)
			{
				return DataColumnCollection.TenColumns[index];
			}
			return DataColumnCollection.ColumnPrefix + index.ToString();
		}

		/// <summary>Creates and adds the specified <see cref="T:System.Data.DataColumn" /> object to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The column already belongs to this collection, or to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600016F RID: 367 RVA: 0x0000B100 File Offset: 0x00009300
		public void Add(DataColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column", "'column' argument cannot be null.");
			}
			if (column.ColumnName.Length == 0)
			{
				column.ColumnName = this.GetNextDefaultColumnName();
			}
			if (column.Table != null)
			{
				throw new ArgumentException("Column '" + column.ColumnName + "' already belongs to this or another DataTable.");
			}
			column.SetTable(this.parentTable);
			this.RegisterName(column.ColumnName, column);
			int num = base.List.Add(column);
			column.Ordinal = num;
			if (column.CompiledExpression != null)
			{
				if (this.parentTable.Rows.Count == 0)
				{
					column.CompiledExpression.Eval(this.parentTable.NewRow());
				}
				else
				{
					column.CompiledExpression.Eval(this.parentTable.Rows[0]);
				}
			}
			if (this.parentTable.Rows.Count > 0)
			{
				column.DataContainer.Capacity = this.parentTable.RecordCache.CurrentCapacity;
			}
			if (column.AutoIncrement)
			{
				DataRowCollection rows = column.Table.Rows;
				for (int i = 0; i < rows.Count; i++)
				{
					rows[i][num] = column.AutoIncrementValue();
				}
			}
			if (column.AutoIncrement)
			{
				this.autoIncrement.Add(column);
			}
			column.PropertyChanged += this.ColumnPropertyChanged;
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, column));
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="columnName">The name of the column. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000170 RID: 368 RVA: 0x0000B29C File Offset: 0x0000949C
		public DataColumn Add(string columnName)
		{
			DataColumn dataColumn = new DataColumn(columnName);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name and type to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="columnName">The <see cref="P:System.Data.DataColumn.ColumnName" /> to use when you create the column. </param>
		/// <param name="type">The <see cref="P:System.Data.DataColumn.DataType" /> of the new column. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000171 RID: 369 RVA: 0x0000B2B8 File Offset: 0x000094B8
		public DataColumn Add(string columnName, Type type)
		{
			if (columnName == null || columnName == string.Empty)
			{
				columnName = this.GetNextDefaultColumnName();
			}
			DataColumn dataColumn = new DataColumn(columnName, type);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Creates and adds a <see cref="T:System.Data.DataColumn" /> object that has the specified name, type, and expression to the <see cref="T:System.Data.DataColumnCollection" />.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataColumn" />.</returns>
		/// <param name="columnName">The name to use when you create the column. </param>
		/// <param name="type">The <see cref="P:System.Data.DataColumn.DataType" /> of the new column. </param>
		/// <param name="expression">The expression to assign to the <see cref="P:System.Data.DataColumn.Expression" /> property. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a column with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidExpressionException">The expression is invalid. See the <see cref="P:System.Data.DataColumn.Expression" /> property for more information about how to create expressions. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000172 RID: 370 RVA: 0x0000B2F4 File Offset: 0x000094F4
		public DataColumn Add(string columnName, Type type, string expression)
		{
			if (columnName == null || columnName == string.Empty)
			{
				columnName = this.GetNextDefaultColumnName();
			}
			DataColumn dataColumn = new DataColumn(columnName, type, expression);
			this.Add(dataColumn);
			return dataColumn;
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.DataColumn" /> array to the end of the collection.</summary>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000173 RID: 371 RVA: 0x0000B330 File Offset: 0x00009530
		public void AddRange(DataColumn[] columns)
		{
			if (this.parentTable.InitInProgress)
			{
				this._mostRecentColumns = columns;
				return;
			}
			if (columns == null)
			{
				return;
			}
			foreach (DataColumn dataColumn in columns)
			{
				if (dataColumn != null)
				{
					this.Add(dataColumn);
				}
			}
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000B388 File Offset: 0x00009588
		private string GetColumnDependency(DataColumn column)
		{
			foreach (object obj in this.parentTable.ParentRelations)
			{
				DataRelation dataRelation = (DataRelation)obj;
				if (Array.IndexOf<DataColumn>(dataRelation.ChildColumns, column) != -1)
				{
					return string.Format(" child key for relationship {0}.", dataRelation.RelationName);
				}
			}
			foreach (object obj2 in this.parentTable.ChildRelations)
			{
				DataRelation dataRelation2 = (DataRelation)obj2;
				if (Array.IndexOf<DataColumn>(dataRelation2.ParentColumns, column) != -1)
				{
					return string.Format(" parent key for relationship {0}.", dataRelation2.RelationName);
				}
			}
			foreach (object obj3 in this.parentTable.Constraints)
			{
				Constraint constraint = (Constraint)obj3;
				if (constraint.IsColumnContained(column))
				{
					return string.Format(" constraint {0} on the table {1}.", constraint.ConstraintName, this.parentTable);
				}
			}
			if (this.parentTable.DataSet != null)
			{
				foreach (object obj4 in this.parentTable.DataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj4;
					foreach (object obj5 in dataTable.Constraints)
					{
						Constraint constraint2 = (Constraint)obj5;
						if (constraint2 is ForeignKeyConstraint && constraint2.IsColumnContained(column))
						{
							return string.Format(" constraint {0} on the table {1}.", constraint2.ConstraintName, dataTable.TableName);
						}
					}
				}
			}
			foreach (object obj6 in this)
			{
				DataColumn dataColumn = (DataColumn)obj6;
				if (dataColumn.CompiledExpression != null && dataColumn.CompiledExpression.DependsOn(column))
				{
					return dataColumn.Expression;
				}
			}
			return string.Empty;
		}

		/// <summary>Checks whether a specific column can be removed from the collection.</summary>
		/// <returns>true if the column can be removed; otherwise, false.</returns>
		/// <param name="column">A <see cref="T:System.Data.DataColumn" /> in the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The column does not belong to this collection.-Or- The column is part of a relationship.-Or- Another column's expression depends on this column. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000175 RID: 373 RVA: 0x0000B6D8 File Offset: 0x000098D8
		public bool CanRemove(DataColumn column)
		{
			return column != null && column.Table == this.parentTable && !(this.GetColumnDependency(column) != string.Empty);
		}

		/// <summary>Clears the collection of any columns.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000176 RID: 374 RVA: 0x0000B718 File Offset: 0x00009918
		public void Clear()
		{
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this);
			if (this.parentTable.Constraints.Count != 0 || this.parentTable.ParentRelations.Count != 0 || this.parentTable.ChildRelations.Count != 0)
			{
				foreach (object obj in this)
				{
					DataColumn dataColumn = (DataColumn)obj;
					string columnDependency = this.GetColumnDependency(dataColumn);
					if (columnDependency != string.Empty)
					{
						throw new ArgumentException("Cannot remove this column, because it is part of the" + columnDependency);
					}
				}
			}
			if (this.parentTable.DataSet != null)
			{
				foreach (object obj2 in this.parentTable.DataSet.Tables)
				{
					DataTable dataTable = (DataTable)obj2;
					foreach (object obj3 in dataTable.Constraints)
					{
						Constraint constraint = (Constraint)obj3;
						if (constraint is ForeignKeyConstraint && ((ForeignKeyConstraint)constraint).RelatedTable == this.parentTable)
						{
							throw new ArgumentException(string.Format("Cannot remove this column, because it is part of the constraint {0} on the table {1}", constraint.ConstraintName, dataTable.TableName));
						}
					}
				}
			}
			foreach (object obj4 in this)
			{
				DataColumn dataColumn2 = (DataColumn)obj4;
				dataColumn2.ResetColumnInfo();
			}
			this.columnFromName.Clear();
			this.autoIncrement.Clear();
			this.columnNameCount.Clear();
			base.List.Clear();
			this.defaultColumnIndex = 1;
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		/// <summary>Checks whether the collection contains a column with the specified name.</summary>
		/// <returns>true if a column exists with this name; otherwise, false.</returns>
		/// <param name="name">The <see cref="P:System.Data.DataColumn.ColumnName" /> of the column to look for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000177 RID: 375 RVA: 0x0000B9A8 File Offset: 0x00009BA8
		public bool Contains(string name)
		{
			return this.columnFromName.Contains(name) || this.IndexOf(name, false) != -1;
		}

		/// <summary>Gets the index of a column specified by name.</summary>
		/// <returns>The index of the column specified by <paramref name="column" /> if it is found; otherwise, -1.</returns>
		/// <param name="column">The name of the column to return. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000178 RID: 376 RVA: 0x0000B9CC File Offset: 0x00009BCC
		public int IndexOf(DataColumn column)
		{
			if (column == null)
			{
				return -1;
			}
			return base.List.IndexOf(column);
		}

		/// <summary>Gets the index of the column with the specific name (the name is not case sensitive).</summary>
		/// <returns>The zero-based index of the column with the specified name, or -1 if the column does not exist in the collection.</returns>
		/// <param name="columnName">The name of the column to find. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000179 RID: 377 RVA: 0x0000B9E4 File Offset: 0x00009BE4
		public int IndexOf(string columnName)
		{
			if (columnName == null)
			{
				return -1;
			}
			DataColumn dataColumn = this.columnFromName[columnName] as DataColumn;
			if (dataColumn != null)
			{
				return this.IndexOf(dataColumn);
			}
			return this.IndexOf(columnName, false);
		}

		// Token: 0x0600017A RID: 378 RVA: 0x0000BA24 File Offset: 0x00009C24
		internal void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			this.parentTable.ResetPropertyDescriptorsCache();
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, ccevent);
			}
		}

		// Token: 0x0600017B RID: 379 RVA: 0x0000BA4C File Offset: 0x00009C4C
		internal void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanged != null)
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataColumn" /> object from the collection.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="column" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The column does not belong to this collection.-Or- The column is part of a relationship.-Or- Another column's expression depends on this column. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600017C RID: 380 RVA: 0x0000BA60 File Offset: 0x00009C60
		public void Remove(DataColumn column)
		{
			if (column == null)
			{
				throw new ArgumentNullException("column", "'column' argument cannot be null.");
			}
			if (!this.Contains(column.ColumnName))
			{
				throw new ArgumentException("Cannot remove a column that doesn't belong to this table.");
			}
			string columnDependency = this.GetColumnDependency(column);
			if (columnDependency != string.Empty)
			{
				throw new ArgumentException("Cannot remove this column, because it is part of " + columnDependency);
			}
			CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Remove, column);
			int ordinal = column.Ordinal;
			this.UnregisterName(column.ColumnName);
			base.List.Remove(column);
			column.ResetColumnInfo();
			for (int i = ordinal; i < this.Count; i++)
			{
				this[i].Ordinal = i;
			}
			if (this.parentTable != null)
			{
				this.parentTable.OnRemoveColumn(column);
			}
			if (column.AutoIncrement)
			{
				this.autoIncrement.Remove(column);
			}
			column.PropertyChanged -= this.ColumnPropertyChanged;
			this.OnCollectionChanged(collectionChangeEventArgs);
		}

		/// <summary>Removes the <see cref="T:System.Data.DataColumn" /> object that has the specified name from the collection.</summary>
		/// <param name="name">The name of the column to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a column with the specified name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600017D RID: 381 RVA: 0x0000BB60 File Offset: 0x00009D60
		public void Remove(string name)
		{
			DataColumn dataColumn = this[name];
			if (dataColumn == null)
			{
				throw new ArgumentException(string.Concat(new string[]
				{
					"Column '",
					name,
					"' does not belong to table ",
					(this.parentTable != null) ? this.parentTable.TableName : string.Empty,
					"."
				}));
			}
			this.Remove(dataColumn);
		}

		/// <summary>Removes the column at the specified index from the collection.</summary>
		/// <param name="index">The index of the column to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a column at the specified index. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600017E RID: 382 RVA: 0x0000BBD4 File Offset: 0x00009DD4
		public void RemoveAt(int index)
		{
			if (this.Count <= index)
			{
				throw new IndexOutOfRangeException("Cannot find column " + index + ".");
			}
			DataColumn dataColumn = this[index];
			this.Remove(dataColumn);
		}

		// Token: 0x0600017F RID: 383 RVA: 0x0000BC18 File Offset: 0x00009E18
		internal void PostAddRange()
		{
			if (this._mostRecentColumns == null)
			{
				return;
			}
			foreach (DataColumn dataColumn in this._mostRecentColumns)
			{
				if (dataColumn != null)
				{
					this.Add(dataColumn);
				}
			}
			this._mostRecentColumns = null;
		}

		// Token: 0x06000180 RID: 384 RVA: 0x0000BC6C File Offset: 0x00009E6C
		internal void UpdateAutoIncrement(DataColumn col, bool isAutoIncrement)
		{
			if (isAutoIncrement)
			{
				if (!this.autoIncrement.Contains(col))
				{
					this.autoIncrement.Add(col);
				}
			}
			else if (this.autoIncrement.Contains(col))
			{
				this.autoIncrement.Remove(col);
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000BCC0 File Offset: 0x00009EC0
		private int IndexOf(string name, bool error)
		{
			Doublet doublet = (Doublet)this.columnNameCount[name];
			if (doublet == null)
			{
				return -1;
			}
			if (doublet.count == 1)
			{
				return base.List.IndexOf(this.columnFromName[doublet.columnNames[0]]);
			}
			if (doublet.count > 1 && error)
			{
				throw new ArgumentException("There is no match for '" + name + "' in the same case and there are multiple matches in different case.");
			}
			return -1;
		}

		// Token: 0x06000182 RID: 386 RVA: 0x0000BD40 File Offset: 0x00009F40
		private void OnCollectionMetaDataChanged(CollectionChangeEventArgs ccevent)
		{
			this.parentTable.ResetPropertyDescriptorsCache();
			if (this.CollectionMetaDataChanged != null)
			{
				this.CollectionMetaDataChanged(this, ccevent);
			}
		}

		// Token: 0x06000183 RID: 387 RVA: 0x0000BD68 File Offset: 0x00009F68
		private void ColumnPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			this.OnCollectionMetaDataChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, sender));
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000BD78 File Offset: 0x00009F78
		internal void MoveColumn(int oldOrdinal, int newOrdinal)
		{
			if (newOrdinal == -1 || newOrdinal > this.Count)
			{
				throw new ArgumentOutOfRangeException("ordinal", "Ordinal '" + newOrdinal + "' exceeds the maximum number.");
			}
			if (oldOrdinal == newOrdinal)
			{
				return;
			}
			int num = ((newOrdinal <= oldOrdinal) ? newOrdinal : oldOrdinal);
			int num2 = ((newOrdinal <= oldOrdinal) ? oldOrdinal : newOrdinal);
			int num3 = ((newOrdinal <= oldOrdinal) ? (-1) : 1);
			DataColumn dataColumn = this[num];
			for (int i = num; i < num2; i += num3)
			{
				this.List[i] = this.List[i + num3];
				((DataColumn)this.List[i]).Ordinal = i;
			}
			this.List[num2] = dataColumn;
			dataColumn.Ordinal = num2;
		}

		// Token: 0x040000BE RID: 190
		private Hashtable columnNameCount = new Hashtable(StringComparer.OrdinalIgnoreCase);

		// Token: 0x040000BF RID: 191
		private Hashtable columnFromName = new Hashtable();

		// Token: 0x040000C0 RID: 192
		private ArrayList autoIncrement = new ArrayList();

		// Token: 0x040000C1 RID: 193
		private int defaultColumnIndex = 1;

		// Token: 0x040000C2 RID: 194
		private DataTable parentTable;

		// Token: 0x040000C3 RID: 195
		private DataColumn[] _mostRecentColumns;

		// Token: 0x040000C4 RID: 196
		private static readonly string ColumnPrefix = "Column";

		// Token: 0x040000C5 RID: 197
		private static readonly string[] TenColumns = new string[] { "Column0", "Column1", "Column2", "Column3", "Column4", "Column5", "Column6", "Column7", "Column8", "Column9" };
	}
}
