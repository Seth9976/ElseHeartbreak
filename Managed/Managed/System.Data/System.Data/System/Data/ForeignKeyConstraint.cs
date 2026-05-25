using System;
using System.ComponentModel;
using System.Text;

namespace System.Data
{
	/// <summary>Represents an action restriction enforced on a set of columns in a primary key/foreign key relationship when a value or row is either deleted or updated.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000048 RID: 72
	[Editor("Microsoft.VSDesigner.Data.Design.ForeignKeyConstraintEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("ConstraintName")]
	public class ForeignKeyConstraint : Constraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x0600054F RID: 1359 RVA: 0x0001D89C File Offset: 0x0001BA9C
		public ForeignKeyConstraint(DataColumn parentColumn, DataColumn childColumn)
		{
			if (parentColumn == null || childColumn == null)
			{
				throw new NullReferenceException("Neither parentColumn or childColumn can be null.");
			}
			this._foreignKeyConstraint(null, new DataColumn[] { parentColumn }, new DataColumn[] { childColumn });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000550 RID: 1360 RVA: 0x0001D8F0 File Offset: 0x0001BAF0
		public ForeignKeyConstraint(DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			this._foreignKeyConstraint(null, parentColumns, childColumns);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified name, parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="constraintName">The name of the constraint. </param>
		/// <param name="parentColumn">The parent <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <param name="childColumn">The child <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000551 RID: 1361 RVA: 0x0001D910 File Offset: 0x0001BB10
		public ForeignKeyConstraint(string constraintName, DataColumn parentColumn, DataColumn childColumn)
		{
			if (parentColumn == null || childColumn == null)
			{
				throw new NullReferenceException("Neither parentColumn or childColumn can be null.");
			}
			this._foreignKeyConstraint(constraintName, new DataColumn[] { parentColumn }, new DataColumn[] { childColumn });
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> class with the specified name, and arrays of parent and child <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="constraintName">The name of the <see cref="T:System.Data.ForeignKeyConstraint" />. If null or empty string, a default name will be given when added to the constraints collection. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <param name="childColumns">An array of child <see cref="T:System.Data.DataColumn" /> in the constraint. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000552 RID: 1362 RVA: 0x0001D964 File Offset: 0x0001BB64
		public ForeignKeyConstraint(string constraintName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			this._foreignKeyConstraint(constraintName, parentColumns, childColumns);
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio  environment. <see cref="T:System.Data.ForeignKeyConstraint" /> objects created by using this constructor must then be added to the collection via <see cref="M:System.Data.ConstraintCollection.AddRange(System.Data.Constraint[])" />. Tables and columns with the specified names must exist at the time the method is called, or if <see cref="M:System.Data.DataTable.BeginInit" /> has been called prior to calling this constructor, the tables and columns with the specified names must exist at the time that <see cref="M:System.Data.DataTable.EndInit" /> is called.</summary>
		/// <param name="constraintName">The name of the constraint. </param>
		/// <param name="parentTableName">The name of the parent <see cref="T:System.Data.DataTable" /> that contains parent <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="parentColumnNames">An array of the names of parent <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="childColumnNames">An array of the names of child <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="acceptRejectRule">One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include None, Cascade, and Default. </param>
		/// <param name="deleteRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is deleted. The default is Cascade. Possible values include: None, Cascade, SetNull, SetDefault, and Default. </param>
		/// <param name="updateRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is updated. The default is Cascade. Possible values include: None, Cascade, SetNull, SetDefault, and Default. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000553 RID: 1363 RVA: 0x0001D984 File Offset: 0x0001BB84
		[Browsable(false)]
		public ForeignKeyConstraint(string constraintName, string parentTableName, string[] parentColumnNames, string[] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule)
		{
			this.InitInProgress = true;
			base.ConstraintName = constraintName;
			this._parentTableName = parentTableName;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._acceptRejectRule = acceptRejectRule;
			this._deleteRule = deleteRule;
			this._updateRule = updateRule;
		}

		/// <summary>This constructor is provided for design time support in the Visual Studio  environment. <see cref="T:System.Data.ForeignKeyConstraint" /> objects created by using this constructor must then be added to the collection via <see cref="M:System.Data.ConstraintCollection.AddRange(System.Data.Constraint[])" />. Tables and columns with the specified names must exist at the time the method is called, or if <see cref="M:System.Data.DataTable.BeginInit" /> has been called prior to calling this constructor, the tables and columns with the specified names must exist at the time that <see cref="M:System.Data.DataTable.EndInit" /> is called.</summary>
		/// <param name="constraintName">The name of the constraint. </param>
		/// <param name="parentTableName">The name of the parent <see cref="T:System.Data.DataTable" /> that contains parent <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="parentTableNamespace">The name of the <see cref="P:System.Data.DataTable.Namespace" />. </param>
		/// <param name="parentColumnNames">An array of the names of parent <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="childColumnNames">An array of the names of child <see cref="T:System.Data.DataColumn" /> objects in the constraint. </param>
		/// <param name="acceptRejectRule">One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include None, Cascade, and Default. </param>
		/// <param name="deleteRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is deleted. The default is Cascade. Possible values include: None, Cascade, SetNull, SetDefault, and Default. </param>
		/// <param name="updateRule">One of the <see cref="T:System.Data.Rule" /> values to use when a row is updated. The default is Cascade. Possible values include: None, Cascade, SetNull, SetDefault, and Default. </param>
		/// <exception cref="T:System.ArgumentNullException">One or both of the columns is null. </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The columns have different data types.-Or - The tables don't belong to the same <see cref="T:System.Data.DataSet" />. </exception>
		// Token: 0x06000554 RID: 1364 RVA: 0x0001D9E4 File Offset: 0x0001BBE4
		[Browsable(false)]
		public ForeignKeyConstraint(string constraintName, string parentTableName, string parentTableNamespace, string[] parentColumnNames, string[] childColumnNames, AcceptRejectRule acceptRejectRule, Rule deleteRule, Rule updateRule)
		{
			this.InitInProgress = true;
			base.ConstraintName = constraintName;
			this._parentTableName = parentTableName;
			this._parentTableNamespace = parentTableNamespace;
			this._parentColumnNames = parentColumnNames;
			this._childColumnNames = childColumnNames;
			this._acceptRejectRule = acceptRejectRule;
			this._deleteRule = deleteRule;
			this._updateRule = updateRule;
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x0001DA4C File Offset: 0x0001BC4C
		internal override void FinishInit(DataTable childTable)
		{
			if (childTable.DataSet == null)
			{
				throw new InvalidConstraintException("ChildTable : " + childTable.TableName + " does not belong to any DataSet");
			}
			DataSet dataSet = childTable.DataSet;
			this._childTableName = childTable.TableName;
			if (!dataSet.Tables.Contains(this._parentTableName))
			{
				throw new InvalidConstraintException(string.Concat(new object[] { "Table : ", this._parentTableName, "does not exist in DataSet : ", dataSet }));
			}
			DataTable dataTable = dataSet.Tables[this._parentTableName];
			int num = 0;
			int num2 = 0;
			if (this._parentColumnNames.Length < 0 || this._childColumnNames.Length < 0)
			{
				throw new InvalidConstraintException("Neither parent nor child columns can be zero length");
			}
			if (this._parentColumnNames.Length != this._childColumnNames.Length)
			{
				throw new InvalidConstraintException("Both parent and child columns must be of same length");
			}
			DataColumn[] array = new DataColumn[this._parentColumnNames.Length];
			DataColumn[] array2 = new DataColumn[this._childColumnNames.Length];
			foreach (string text in this._parentColumnNames)
			{
				if (!dataTable.Columns.Contains(text))
				{
					throw new InvalidConstraintException("Table : " + this._parentTableName + "does not contain the column :" + text);
				}
				array[num++] = dataTable.Columns[text];
			}
			foreach (string text2 in this._childColumnNames)
			{
				if (!childTable.Columns.Contains(text2))
				{
					throw new InvalidConstraintException("Table : " + this._childTableName + "does not contain the column : " + text2);
				}
				array2[num2++] = childTable.Columns[text2];
			}
			this._validateColumns(array, array2);
			this._parentColumns = array;
			this._childColumns = array2;
			dataTable.Namespace = this._parentTableNamespace;
			this.InitInProgress = false;
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x0001DC58 File Offset: 0x0001BE58
		private void _foreignKeyConstraint(string constraintName, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			this._validateColumns(parentColumns, childColumns);
			base.ConstraintName = constraintName;
			this._parentColumns = parentColumns;
			this._childColumns = childColumns;
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x0001DC78 File Offset: 0x0001BE78
		private void _validateColumns(DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			if (parentColumns == null || childColumns == null)
			{
				throw new ArgumentNullException();
			}
			if (parentColumns.Length < 1 || childColumns.Length < 1)
			{
				throw new ArgumentException("Neither ParentColumns or ChildColumns can't be zero length.");
			}
			if (parentColumns.Length != childColumns.Length)
			{
				throw new ArgumentException("Parent columns and child columns must be the same length.");
			}
			DataTable table = parentColumns[0].Table;
			DataTable table2 = childColumns[0].Table;
			for (int i = 0; i < parentColumns.Length; i++)
			{
				DataColumn dataColumn = parentColumns[i];
				DataColumn dataColumn2 = childColumns[i];
				if (dataColumn.Table == null)
				{
					throw new ArgumentException("All columns must belong to a table. ColumnName: " + dataColumn.ColumnName + " does not belong to a table.");
				}
				if (table != dataColumn.Table)
				{
					throw new InvalidConstraintException("Parent columns must all belong to the same table.");
				}
				if (dataColumn2.Table == null)
				{
					throw new ArgumentException("All columns must belong to a table. ColumnName: " + dataColumn.ColumnName + " does not belong to a table.");
				}
				if (table2 != dataColumn2.Table)
				{
					throw new InvalidConstraintException("Child columns must all belong to the same table.");
				}
				if (dataColumn.CompiledExpression != null)
				{
					throw new ArgumentException(string.Format("Cannot create a constraint based on Expression column {0}.", dataColumn.ColumnName));
				}
				if (dataColumn2.CompiledExpression != null)
				{
					throw new ArgumentException(string.Format("Cannot create a constraint based on Expression column {0}.", dataColumn2.ColumnName));
				}
			}
			if (table.DataSet != table2.DataSet)
			{
				throw new InvalidOperationException("Parent column and child column must belong to tables that belong to the same DataSet.");
			}
			int num = 0;
			for (int j = 0; j < parentColumns.Length; j++)
			{
				DataColumn dataColumn3 = parentColumns[j];
				DataColumn dataColumn4 = childColumns[j];
				if (dataColumn3 == dataColumn4)
				{
					num++;
				}
				else if (!dataColumn3.DataTypeMatches(dataColumn4))
				{
					throw new InvalidOperationException("Parent column is not type compatible with it's child column.");
				}
			}
			if (num == parentColumns.Length)
			{
				throw new InvalidOperationException("Property not accessible because 'ParentKey and ChildKey are identical.'.");
			}
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x0001DE40 File Offset: 0x0001C040
		private void _ensureUniqueConstraintExists(ConstraintCollection collection, DataColumn[] parentColumns)
		{
			if (parentColumns == null)
			{
				throw new ArgumentNullException("ParentColumns can't be null");
			}
			UniqueConstraint uniqueConstraint = null;
			if (parentColumns[0] != null)
			{
				uniqueConstraint = UniqueConstraint.GetUniqueConstraintForColumnSet(parentColumns[0].Table.Constraints, parentColumns);
			}
			if (uniqueConstraint == null)
			{
				uniqueConstraint = new UniqueConstraint(parentColumns, false);
				parentColumns[0].Table.Constraints.Add(uniqueConstraint);
			}
			this._parentUniqueConstraint = uniqueConstraint;
			this._parentUniqueConstraint.ChildConstraint = this;
		}

		/// <summary>Indicates the action that should take place across this constraint when <see cref="M:System.Data.DataTable.AcceptChanges" /> is invoked.</summary>
		/// <returns>One of the <see cref="T:System.Data.AcceptRejectRule" /> values. Possible values include None, and Cascade. The default is None.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0001DEB0 File Offset: 0x0001C0B0
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0001DEB8 File Offset: 0x0001C0B8
		[DataCategory("Data")]
		[DefaultValue(AcceptRejectRule.None)]
		public virtual AcceptRejectRule AcceptRejectRule
		{
			get
			{
				return this._acceptRejectRule;
			}
			set
			{
				this._acceptRejectRule = value;
			}
		}

		/// <summary>Gets the child columns of this constraint.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the child columns of the constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x0001DEC4 File Offset: 0x0001C0C4
		[ReadOnly(true)]
		[DataCategory("Data")]
		public virtual DataColumn[] Columns
		{
			get
			{
				return this._childColumns;
			}
		}

		/// <summary>Gets or sets the action that occurs across this constraint when a row is deleted.</summary>
		/// <returns>One of the <see cref="T:System.Data.Rule" /> values. The default is Cascade.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0001DECC File Offset: 0x0001C0CC
		// (set) Token: 0x0600055D RID: 1373 RVA: 0x0001DED4 File Offset: 0x0001C0D4
		[DataCategory("Data")]
		[DefaultValue(Rule.Cascade)]
		public virtual Rule DeleteRule
		{
			get
			{
				return this._deleteRule;
			}
			set
			{
				this._deleteRule = value;
			}
		}

		/// <summary>Gets or sets the action that occurs across this constraint on when a row is updated.</summary>
		/// <returns>One of the <see cref="T:System.Data.Rule" /> values. The default is Cascade.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0001DEE0 File Offset: 0x0001C0E0
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x0001DEE8 File Offset: 0x0001C0E8
		[DataCategory("Data")]
		[DefaultValue(Rule.Cascade)]
		public virtual Rule UpdateRule
		{
			get
			{
				return this._updateRule;
			}
			set
			{
				this._updateRule = value;
			}
		}

		/// <summary>The parent columns of this constraint.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects that are the parent columns of the constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x0001DEF4 File Offset: 0x0001C0F4
		[DataCategory("Data")]
		[ReadOnly(true)]
		public virtual DataColumn[] RelatedColumns
		{
			get
			{
				return this._parentColumns;
			}
		}

		/// <summary>Gets the parent table of this constraint.</summary>
		/// <returns>The parent <see cref="T:System.Data.DataTable" /> of this constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0001DEFC File Offset: 0x0001C0FC
		[DataCategory("Data")]
		[ReadOnly(true)]
		public virtual DataTable RelatedTable
		{
			get
			{
				if (this._parentColumns != null && this._parentColumns.Length > 0)
				{
					return this._parentColumns[0].Table;
				}
				throw new InvalidOperationException("Property not accessible because 'Object reference not set to an instance of an object'");
			}
		}

		/// <summary>Gets the child table of this constraint.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that is the child table in the constraint.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0001DF30 File Offset: 0x0001C130
		[DataCategory("Data")]
		[ReadOnly(true)]
		public override DataTable Table
		{
			get
			{
				if (this._childColumns != null && this._childColumns.Length > 0)
				{
					return this._childColumns[0].Table;
				}
				throw new InvalidOperationException("Property not accessible because 'Object reference not set to an instance of an object'");
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0001DF64 File Offset: 0x0001C164
		internal UniqueConstraint ParentConstraint
		{
			get
			{
				return this._parentUniqueConstraint;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Data.ForeignKeyConstraint" /> is identical to the specified object.</summary>
		/// <returns>true, if the objects are identical; otherwise, false.</returns>
		/// <param name="key">The object to which this <see cref="T:System.Data.ForeignKeyConstraint" /> is compared. Two <see cref="T:System.Data.ForeignKeyConstraint" /> are equal if they constrain the same columns. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000564 RID: 1380 RVA: 0x0001DF6C File Offset: 0x0001C16C
		public override bool Equals(object key)
		{
			ForeignKeyConstraint foreignKeyConstraint = key as ForeignKeyConstraint;
			return foreignKeyConstraint != null && DataColumn.AreColumnSetsTheSame(this.RelatedColumns, foreignKeyConstraint.RelatedColumns) && DataColumn.AreColumnSetsTheSame(this.Columns, foreignKeyConstraint.Columns);
		}

		/// <summary>Gets the hash code of this instance of the <see cref="T:System.Data.ForeignKeyConstraint" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000565 RID: 1381 RVA: 0x0001DFBC File Offset: 0x0001C1BC
		public override int GetHashCode()
		{
			int num = 32;
			int num2 = 88;
			if (this.Columns.Length > 0)
			{
				num ^= this.Columns[0].GetHashCode();
			}
			for (int i = 1; i < this.Columns.Length; i++)
			{
				num ^= this.Columns[1].GetHashCode();
			}
			if (this.RelatedColumns.Length > 0)
			{
				num2 ^= this.Columns[0].GetHashCode();
			}
			for (int i = 1; i < this.RelatedColumns.Length; i++)
			{
				num2 ^= this.RelatedColumns[1].GetHashCode();
			}
			return num ^ num2;
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001E060 File Offset: 0x0001C260
		internal override void AddToConstraintCollectionSetup(ConstraintCollection collection)
		{
			if (collection.Table != this.Table)
			{
				throw new InvalidConstraintException("This constraint cannot be added since ForeignKey doesn't belong to table " + this.RelatedTable.TableName + ".");
			}
			this._validateColumns(this._parentColumns, this._childColumns);
			this._ensureUniqueConstraintExists(collection, this._parentColumns);
			if (((this.Table.DataSet != null && this.Table.DataSet.EnforceConstraints) || (this.Table.DataSet == null && this.Table.EnforceConstraints)) && this.IsConstraintViolated())
			{
				throw new ArgumentException("This constraint cannot be enabled as not all values have corresponding parent values.");
			}
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x0001E118 File Offset: 0x0001C318
		internal override void RemoveFromConstraintCollectionCleanup(ConstraintCollection collection)
		{
			this._parentUniqueConstraint.ChildConstraint = null;
			base.Index = null;
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x0001E130 File Offset: 0x0001C330
		internal override bool IsConstraintViolated()
		{
			if (this.Table.DataSet == null || this.RelatedTable.DataSet == null)
			{
				return false;
			}
			bool flag = false;
			foreach (object obj in this.Table.Rows)
			{
				DataRow dataRow = (DataRow)obj;
				if (!dataRow.IsNullColumns(this._childColumns))
				{
					if (!this.RelatedTable.RowsExist(this._parentColumns, this._childColumns, dataRow))
					{
						flag = true;
						string[] array = new string[this._childColumns.Length];
						for (int i = 0; i < this._childColumns.Length; i++)
						{
							DataColumn dataColumn = this._childColumns[i];
							array[i] = dataRow[dataColumn].ToString();
						}
						dataRow.RowError = string.Format("ForeignKeyConstraint {0} requires the child key values ({1}) to exist in the parent table.", this.ConstraintName, string.Join(",", array));
					}
				}
			}
			return flag;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x0001E26C File Offset: 0x0001C46C
		internal override void AssertConstraint(DataRow row)
		{
			if (row.IsNullColumns(this._childColumns))
			{
				return;
			}
			if (!this.RelatedTable.RowsExist(this._parentColumns, this._childColumns, row))
			{
				throw new InvalidConstraintException(this.GetErrorMessage(row));
			}
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x0001E2B8 File Offset: 0x0001C4B8
		internal override bool IsColumnContained(DataColumn column)
		{
			for (int i = 0; i < this._parentColumns.Length; i++)
			{
				if (column == this._parentColumns[i])
				{
					return true;
				}
			}
			for (int j = 0; j < this._childColumns.Length; j++)
			{
				if (column == this._childColumns[j])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x0001E318 File Offset: 0x0001C518
		internal override bool CanRemoveFromCollection(ConstraintCollection col, bool shouldThrow)
		{
			return true;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x0001E31C File Offset: 0x0001C51C
		private string GetErrorMessage(DataRow row)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this._childColumns.Length; i++)
			{
				stringBuilder.Append(row[this._childColumns[0]].ToString());
				if (i != this._childColumns.Length - 1)
				{
					stringBuilder.Append(',');
				}
			}
			string text = stringBuilder.ToString();
			return string.Concat(new string[] { "ForeignKeyConstraint ", this.ConstraintName, " requires the child key values (", text, ") to exist in the parent table." });
		}

		// Token: 0x040001C1 RID: 449
		private UniqueConstraint _parentUniqueConstraint;

		// Token: 0x040001C2 RID: 450
		private DataColumn[] _parentColumns;

		// Token: 0x040001C3 RID: 451
		private DataColumn[] _childColumns;

		// Token: 0x040001C4 RID: 452
		private Rule _deleteRule = Rule.Cascade;

		// Token: 0x040001C5 RID: 453
		private Rule _updateRule = Rule.Cascade;

		// Token: 0x040001C6 RID: 454
		private AcceptRejectRule _acceptRejectRule;

		// Token: 0x040001C7 RID: 455
		private string _parentTableName;

		// Token: 0x040001C8 RID: 456
		private string _parentTableNamespace;

		// Token: 0x040001C9 RID: 457
		private string _childTableName;

		// Token: 0x040001CA RID: 458
		private string[] _parentColumnNames;

		// Token: 0x040001CB RID: 459
		private string[] _childColumnNames;
	}
}
