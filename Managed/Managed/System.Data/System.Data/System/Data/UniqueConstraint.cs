using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Text;

namespace System.Data
{
	/// <summary>Represents a restriction on a set of columns in which all values must be unique.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000085 RID: 133
	[DefaultProperty("ConstraintName")]
	[Editor("Microsoft.VSDesigner.Data.Design.UniqueConstraintEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class UniqueConstraint : Constraint
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		// Token: 0x0600066C RID: 1644 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		public UniqueConstraint(DataColumn column)
		{
			this._uniqueConstraint(string.Empty, column, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the given array of <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		// Token: 0x0600066D RID: 1645 RVA: 0x0001F5E4 File Offset: 0x0001D7E4
		public UniqueConstraint(DataColumn[] columns)
		{
			this._uniqueConstraint(string.Empty, columns, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the <see cref="T:System.Data.DataColumn" /> to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x0600066E RID: 1646 RVA: 0x0001F5FC File Offset: 0x0001D7FC
		public UniqueConstraint(DataColumn column, bool isPrimaryKey)
		{
			this._uniqueConstraint(string.Empty, column, isPrimaryKey);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x0600066F RID: 1647 RVA: 0x0001F614 File Offset: 0x0001D814
		public UniqueConstraint(DataColumn[] columns, bool isPrimaryKey)
		{
			this._uniqueConstraint(string.Empty, columns, isPrimaryKey);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name and <see cref="T:System.Data.DataColumn" />.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		// Token: 0x06000670 RID: 1648 RVA: 0x0001F62C File Offset: 0x0001D82C
		public UniqueConstraint(string name, DataColumn column)
		{
			this._uniqueConstraint(name, column, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name and array of <see cref="T:System.Data.DataColumn" /> objects.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columns">The array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		// Token: 0x06000671 RID: 1649 RVA: 0x0001F640 File Offset: 0x0001D840
		public UniqueConstraint(string name, DataColumn[] columns)
		{
			this._uniqueConstraint(name, columns, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, the <see cref="T:System.Data.DataColumn" /> to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000672 RID: 1650 RVA: 0x0001F654 File Offset: 0x0001D854
		public UniqueConstraint(string name, DataColumn column, bool isPrimaryKey)
		{
			this._uniqueConstraint(name, column, isPrimaryKey);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000673 RID: 1651 RVA: 0x0001F668 File Offset: 0x0001D868
		public UniqueConstraint(string name, DataColumn[] columns, bool isPrimaryKey)
		{
			this._uniqueConstraint(name, columns, isPrimaryKey);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.UniqueConstraint" /> class with the specified name, an array of <see cref="T:System.Data.DataColumn" /> objects to constrain, and a value specifying whether the constraint is a primary key.</summary>
		/// <param name="name">The name of the constraint. </param>
		/// <param name="columnNames">An array of <see cref="T:System.Data.DataColumn" /> objects to constrain. </param>
		/// <param name="isPrimaryKey">true to indicate that the constraint is a primary key; otherwise, false. </param>
		// Token: 0x06000674 RID: 1652 RVA: 0x0001F67C File Offset: 0x0001D87C
		[Browsable(false)]
		public UniqueConstraint(string name, string[] columnNames, bool isPrimaryKey)
		{
			this.InitInProgress = true;
			this._dataColumnNames = columnNames;
			base.ConstraintName = name;
			this._isPrimaryKey = isPrimaryKey;
		}

		// Token: 0x06000675 RID: 1653 RVA: 0x0001F6AC File Offset: 0x0001D8AC
		private void _uniqueConstraint(string name, DataColumn column, bool isPrimaryKey)
		{
			this._validateColumn(column);
			base.ConstraintName = name;
			this._isPrimaryKey = isPrimaryKey;
			this._dataColumns = new DataColumn[] { column };
			this._dataTable = column.Table;
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0001F6EC File Offset: 0x0001D8EC
		private void _uniqueConstraint(string name, DataColumn[] columns, bool isPrimaryKey)
		{
			this._validateColumns(columns, out this._dataTable);
			base.ConstraintName = name;
			this._dataColumns = columns;
			this._isPrimaryKey = isPrimaryKey;
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0001F71C File Offset: 0x0001D91C
		private void _validateColumns(DataColumn[] columns)
		{
			DataTable dataTable;
			this._validateColumns(columns, out dataTable);
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0001F734 File Offset: 0x0001D934
		private void _validateColumns(DataColumn[] columns, out DataTable table)
		{
			table = null;
			if (columns == null)
			{
				throw new ArgumentNullException();
			}
			if (columns.Length < 1)
			{
				throw new InvalidConstraintException("Must be at least one column.");
			}
			DataTable table2 = columns[0].Table;
			foreach (DataColumn dataColumn in columns)
			{
				this._validateColumn(dataColumn);
				if (table2 != dataColumn.Table)
				{
					throw new InvalidConstraintException("Columns must be from the same table.");
				}
			}
			table = table2;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0001F7A8 File Offset: 0x0001D9A8
		private void _validateColumn(DataColumn column)
		{
			if (column == null)
			{
				throw new NullReferenceException("Object reference not set to an instance of an object.");
			}
			if (column.Table == null)
			{
				throw new ArgumentException("Column must belong to a table.");
			}
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
		internal static void SetAsPrimaryKey(ConstraintCollection collection, UniqueConstraint newPrimaryKey)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("ConstraintCollection can't be null.");
			}
			if (collection.IndexOf(newPrimaryKey) < 0 && newPrimaryKey != null)
			{
				throw new ArgumentException("newPrimaryKey must belong to collection.");
			}
			UniqueConstraint primaryKeyConstraint = UniqueConstraint.GetPrimaryKeyConstraint(collection);
			if (primaryKeyConstraint != null)
			{
				primaryKeyConstraint._isPrimaryKey = false;
			}
			if (newPrimaryKey != null)
			{
				newPrimaryKey._isPrimaryKey = true;
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0001F834 File Offset: 0x0001DA34
		internal static UniqueConstraint GetPrimaryKeyConstraint(ConstraintCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("Collection can't be null.");
			}
			foreach (object obj in collection)
			{
				UniqueConstraint uniqueConstraint = obj as UniqueConstraint;
				if (uniqueConstraint != null)
				{
					if (uniqueConstraint.IsPrimaryKey)
					{
						return uniqueConstraint;
					}
				}
			}
			return null;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0001F890 File Offset: 0x0001DA90
		internal static UniqueConstraint GetUniqueConstraintForColumnSet(ConstraintCollection collection, DataColumn[] columns)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("Collection can't be null.");
			}
			if (columns == null)
			{
				return null;
			}
			foreach (object obj in collection)
			{
				Constraint constraint = (Constraint)obj;
				if (constraint is UniqueConstraint)
				{
					UniqueConstraint uniqueConstraint = constraint as UniqueConstraint;
					if (DataColumn.AreColumnSetsTheSame(uniqueConstraint.Columns, columns))
					{
						return uniqueConstraint;
					}
				}
			}
			return null;
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x0600067D RID: 1661 RVA: 0x0001F93C File Offset: 0x0001DB3C
		// (set) Token: 0x0600067E RID: 1662 RVA: 0x0001F944 File Offset: 0x0001DB44
		internal ForeignKeyConstraint ChildConstraint
		{
			get
			{
				return this._childConstraint;
			}
			set
			{
				this._childConstraint = value;
			}
		}

		// Token: 0x0600067F RID: 1663 RVA: 0x0001F950 File Offset: 0x0001DB50
		internal override void FinishInit(DataTable _setTable)
		{
			this._dataTable = _setTable;
			if (this._isPrimaryKey && _setTable.PrimaryKey.Length != 0)
			{
				throw new ArgumentException("Cannot add primary key constraint since primary keyis already set for the table");
			}
			DataColumn[] array = new DataColumn[this._dataColumnNames.Length];
			int num = 0;
			foreach (string text in this._dataColumnNames)
			{
				if (!_setTable.Columns.Contains(text))
				{
					throw new InvalidConstraintException("The named columns must exist in the table");
				}
				array[num] = _setTable.Columns[text];
				num++;
			}
			this._dataColumns = array;
			this._validateColumns(array);
			this.InitInProgress = false;
		}

		/// <summary>Gets the array of columns that this constraint affects.</summary>
		/// <returns>An array of <see cref="T:System.Data.DataColumn" /> objects.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000680 RID: 1664 RVA: 0x0001FA04 File Offset: 0x0001DC04
		[DataCategory("Data")]
		[ReadOnly(true)]
		public virtual DataColumn[] Columns
		{
			get
			{
				return this._dataColumns;
			}
		}

		/// <summary>Gets a value indicating whether or not the constraint is on a primary key.</summary>
		/// <returns>true, if the constraint is on a primary key; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013A RID: 314
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0001FA0C File Offset: 0x0001DC0C
		[DataCategory("Data")]
		public bool IsPrimaryKey
		{
			get
			{
				return this.Table != null && this._belongsToCollection && this._isPrimaryKey;
			}
		}

		/// <summary>Gets the table to which this constraint belongs.</summary>
		/// <returns>The <see cref="T:System.Data.DataTable" /> to which the constraint belongs.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x0001FA2C File Offset: 0x0001DC2C
		[ReadOnly(true)]
		[DataCategory("Data")]
		public override DataTable Table
		{
			get
			{
				return this._dataTable;
			}
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0001FA34 File Offset: 0x0001DC34
		internal void SetIsPrimaryKey(bool value)
		{
			this._isPrimaryKey = value;
		}

		/// <summary>Compares this constraint to a second to determine if both are identical.</summary>
		/// <returns>true, if the contraints are equal; otherwise, false.</returns>
		/// <param name="key2">The object to which this <see cref="T:System.Data.UniqueConstraint" /> is compared. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000684 RID: 1668 RVA: 0x0001FA40 File Offset: 0x0001DC40
		public override bool Equals(object key2)
		{
			UniqueConstraint uniqueConstraint = key2 as UniqueConstraint;
			return uniqueConstraint != null && DataColumn.AreColumnSetsTheSame(uniqueConstraint.Columns, this.Columns);
		}

		/// <summary>Gets the hash code of this instance of the <see cref="T:System.Data.UniqueConstraint" /> object.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000685 RID: 1669 RVA: 0x0001FA70 File Offset: 0x0001DC70
		public override int GetHashCode()
		{
			int num = 42;
			if (this.Columns.Length > 0)
			{
				num ^= this.Columns[0].GetHashCode();
			}
			for (int i = 1; i < this.Columns.Length; i++)
			{
				num ^= this.Columns[1].GetHashCode();
			}
			return num;
		}

		// Token: 0x06000686 RID: 1670 RVA: 0x0001FAC8 File Offset: 0x0001DCC8
		internal override void AddToConstraintCollectionSetup(ConstraintCollection collection)
		{
			for (int i = 0; i < this.Columns.Length; i++)
			{
				if (this.Columns[i].Table != collection.Table)
				{
					throw new ArgumentException("These columns don't point to this table.");
				}
			}
			this._validateColumns(this._dataColumns);
			UniqueConstraint uniqueConstraint = UniqueConstraint.GetUniqueConstraintForColumnSet(collection, this.Columns);
			if (uniqueConstraint != null)
			{
				throw new ArgumentException("Unique constraint already exists for these columns. Existing ConstraintName is " + uniqueConstraint.ConstraintName);
			}
			if (this.IsPrimaryKey)
			{
				uniqueConstraint = UniqueConstraint.GetPrimaryKeyConstraint(collection);
				if (uniqueConstraint != null)
				{
					uniqueConstraint._isPrimaryKey = false;
				}
			}
			if (this._dataColumns.Length == 1)
			{
				this._dataColumns[0].SetUnique();
			}
			if (this.IsConstraintViolated())
			{
				throw new ArgumentException("These columns don't currently have unique values.");
			}
			this._belongsToCollection = true;
		}

		// Token: 0x06000687 RID: 1671 RVA: 0x0001FBA0 File Offset: 0x0001DDA0
		internal override void RemoveFromConstraintCollectionCleanup(ConstraintCollection collection)
		{
			if (this.Columns.Length == 1)
			{
				this.Columns[0].Unique = false;
			}
			this._belongsToCollection = false;
			Index index = base.Index;
			base.Index = null;
		}

		// Token: 0x06000688 RID: 1672 RVA: 0x0001FBE0 File Offset: 0x0001DDE0
		internal override bool IsConstraintViolated()
		{
			if (base.Index == null)
			{
				base.Index = this.Table.GetIndex(this.Columns, null, DataViewRowState.None, null, false);
			}
			if (base.Index.HasDuplicates)
			{
				int[] duplicates = base.Index.Duplicates;
				for (int i = 0; i < duplicates.Length; i++)
				{
					DataRow dataRow = this.Table.RecordCache[duplicates[i]];
					ArrayList arrayList = new ArrayList();
					ArrayList arrayList2 = new ArrayList();
					foreach (DataColumn dataColumn in this.Columns)
					{
						arrayList.Add(dataColumn.ColumnName);
						arrayList2.Add(dataRow[dataColumn].ToString());
					}
					string text = string.Join(", ", (string[])arrayList.ToArray(typeof(string)));
					string text2 = string.Join(", ", (string[])arrayList2.ToArray(typeof(string)));
					dataRow.RowError = string.Format("Column '{0}' is constrained to be unique.  Value '{1}' is already present.", text, text2);
					for (int k = 0; k < this.Columns.Length; k++)
					{
						dataRow.SetColumnError(this.Columns[k], dataRow.RowError);
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000689 RID: 1673 RVA: 0x0001FD3C File Offset: 0x0001DF3C
		internal override void AssertConstraint(DataRow row)
		{
			if (this.IsPrimaryKey && row.HasVersion(DataRowVersion.Default))
			{
				for (int i = 0; i < this.Columns.Length; i++)
				{
					if (row.IsNull(this.Columns[i]))
					{
						throw new NoNullAllowedException("Column '" + this.Columns[i].ColumnName + "' does not allow nulls.");
					}
				}
			}
			if (base.Index == null)
			{
				base.Index = this.Table.GetIndex(this.Columns, null, DataViewRowState.None, null, false);
			}
			if (base.Index.HasDuplicates)
			{
				throw new ConstraintException(this.GetErrorMessage(row));
			}
		}

		// Token: 0x0600068A RID: 1674 RVA: 0x0001FDF8 File Offset: 0x0001DFF8
		internal override bool IsColumnContained(DataColumn column)
		{
			for (int i = 0; i < this._dataColumns.Length; i++)
			{
				if (column == this._dataColumns[i])
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x0001FE30 File Offset: 0x0001E030
		internal override bool CanRemoveFromCollection(ConstraintCollection col, bool shouldThrow)
		{
			if (this.IsPrimaryKey)
			{
				if (shouldThrow)
				{
					throw new ArgumentException("Cannot remove unique constraint since it's the primary key of a table.");
				}
				return false;
			}
			else
			{
				if (this.Table.DataSet == null)
				{
					return true;
				}
				if (this.ChildConstraint == null)
				{
					return true;
				}
				if (!shouldThrow)
				{
					return false;
				}
				throw new ArgumentException(string.Format("Cannot remove unique constraint '{0}'.Remove foreign key constraint '{1}' first.", this.ConstraintName, this.ChildConstraint.ConstraintName));
			}
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001FEA4 File Offset: 0x0001E0A4
		private string GetErrorMessage(DataRow row)
		{
			StringBuilder stringBuilder = new StringBuilder(row[this._dataColumns[0]].ToString());
			for (int i = 1; i < this._dataColumns.Length; i++)
			{
				stringBuilder = stringBuilder.Append(", ").Append(row[this._dataColumns[i].ColumnName]);
			}
			string text = stringBuilder.ToString();
			stringBuilder = new StringBuilder(this._dataColumns[0].ColumnName);
			for (int i = 1; i < this._dataColumns.Length; i++)
			{
				stringBuilder = stringBuilder.Append(", ").Append(this._dataColumns[i].ColumnName);
			}
			string text2 = stringBuilder.ToString();
			return string.Concat(new string[] { "Column '", text2, "' is constrained to be unique.  Value '", text, "' is already present." });
		}

		// Token: 0x0400024E RID: 590
		private bool _isPrimaryKey;

		// Token: 0x0400024F RID: 591
		private bool _belongsToCollection;

		// Token: 0x04000250 RID: 592
		private DataTable _dataTable;

		// Token: 0x04000251 RID: 593
		private DataColumn[] _dataColumns;

		// Token: 0x04000252 RID: 594
		private string[] _dataColumnNames;

		// Token: 0x04000253 RID: 595
		private ForeignKeyConstraint _childConstraint;
	}
}
