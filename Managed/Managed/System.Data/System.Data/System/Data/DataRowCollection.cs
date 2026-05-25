using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data
{
	/// <summary>Represents a collection of rows for a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200002D RID: 45
	public sealed class DataRowCollection : InternalDataCollectionBase
	{
		// Token: 0x06000263 RID: 611 RVA: 0x00010B78 File Offset: 0x0000ED78
		internal DataRowCollection(DataTable table)
		{
			this.table = table;
		}

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000264 RID: 612 RVA: 0x00010B88 File Offset: 0x0000ED88
		// (remove) Token: 0x06000265 RID: 613 RVA: 0x00010BA4 File Offset: 0x0000EDA4
		internal event ListChangedEventHandler ListChanged;

		/// <summary>Gets the row at the specified index.</summary>
		/// <returns>The specified <see cref="T:System.Data.DataRow" />.</returns>
		/// <param name="index">The zero-based index of the row to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700005A RID: 90
		public DataRow this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new IndexOutOfRangeException("There is no row at position " + index + ".");
				}
				return (DataRow)this.List[index];
			}
		}

		/// <summary>Adds the specified <see cref="T:System.Data.DataRow" /> to the <see cref="T:System.Data.DataRowCollection" /> object.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The row is null. </exception>
		/// <exception cref="T:System.ArgumentException">The row either belongs to another table or already belongs to this table. </exception>
		/// <exception cref="T:System.Data.ConstraintException">The addition invalidates a constraint. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">The addition tries to put a null in a <see cref="T:System.Data.DataColumn" /> where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000267 RID: 615 RVA: 0x00010C0C File Offset: 0x0000EE0C
		public void Add(DataRow row)
		{
			if (row == null)
			{
				throw new ArgumentNullException("row", "'row' argument cannot be null.");
			}
			if (row.Table != this.table)
			{
				throw new ArgumentException("This row already belongs to another table.");
			}
			if (row.RowID != -1)
			{
				throw new ArgumentException("This row already belongs to this table.");
			}
			row.BeginEdit();
			row.Validate();
			this.AddInternal(row);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.DataRow" /> object.</summary>
		/// <returns>The zero-based index of the row, or -1 if the row is not found in the collection.</returns>
		/// <param name="row">The DataRow to search for.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000268 RID: 616 RVA: 0x00010C78 File Offset: 0x0000EE78
		public int IndexOf(DataRow row)
		{
			if (row == null || row.Table != this.table)
			{
				return -1;
			}
			int rowID = row.RowID;
			return (rowID < 0 || rowID >= this.List.Count || row != this.List[rowID]) ? (-1) : rowID;
		}

		// Token: 0x06000269 RID: 617 RVA: 0x00010CD8 File Offset: 0x0000EED8
		internal void AddInternal(DataRow row)
		{
			this.AddInternal(row, DataRowAction.Add);
		}

		// Token: 0x0600026A RID: 618 RVA: 0x00010CE4 File Offset: 0x0000EEE4
		internal void AddInternal(DataRow row, DataRowAction action)
		{
			row.Table.ChangingDataRow(row, action);
			this.List.Add(row);
			row.AttachAt(this.List.Count - 1, action);
			row.Table.ChangedDataRow(row, action);
			if (row._rowChanged)
			{
				row._rowChanged = false;
			}
		}

		/// <summary>Creates a row using specified values and adds it to the <see cref="T:System.Data.DataRowCollection" />.</summary>
		/// <returns>None.</returns>
		/// <param name="values">The array of values that are used to create the new row. </param>
		/// <exception cref="T:System.ArgumentException">The array is larger than the number of columns in the table. </exception>
		/// <exception cref="T:System.InvalidCastException">A value does not match its respective column type. </exception>
		/// <exception cref="T:System.Data.ConstraintException">Adding the row invalidates a constraint. </exception>
		/// <exception cref="T:System.Data.NoNullAllowedException">Trying to put a null in a column where <see cref="P:System.Data.DataColumn.AllowDBNull" /> is false. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600026B RID: 619 RVA: 0x00010D40 File Offset: 0x0000EF40
		public DataRow Add(params object[] values)
		{
			if (values == null)
			{
				throw new NullReferenceException();
			}
			DataRow dataRow = this.table.NewNotInitializedRow();
			int num = this.table.CreateRecord(values);
			dataRow.ImportRecord(num);
			dataRow.Validate();
			this.AddInternal(dataRow);
			return dataRow;
		}

		/// <summary>Clears the collection of all rows.</summary>
		/// <exception cref="T:System.Data.InvalidConstraintException">A <see cref="T:System.Data.ForeignKeyConstraint" /> is enforced on the <see cref="T:System.Data.DataRowCollection" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600026C RID: 620 RVA: 0x00010D88 File Offset: 0x0000EF88
		public void Clear()
		{
			if (this.table.DataSet != null && this.table.DataSet.EnforceConstraints)
			{
				foreach (object obj in this.table.Constraints)
				{
					Constraint constraint = (Constraint)obj;
					UniqueConstraint uniqueConstraint = constraint as UniqueConstraint;
					if (uniqueConstraint != null)
					{
						if (uniqueConstraint.ChildConstraint != null && uniqueConstraint.ChildConstraint.Table.Rows.Count != 0)
						{
							string text = string.Format("Cannot clear table Parent because ForeignKeyConstraint {0} enforces Child.", uniqueConstraint.ConstraintName);
							throw new InvalidConstraintException(text);
						}
					}
				}
			}
			this.table.DataTableClearing();
			this.List.Clear();
			this.table.ResetIndexes();
			this.table.DataTableCleared();
			this.OnListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, -1, -1));
		}

		/// <summary>Gets a value that indicates whether the primary key of any row in the collection contains the specified value.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Data.DataRow" /> with the specified primary key value; otherwise, false.</returns>
		/// <param name="key">The value of the primary key to test for. </param>
		/// <exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600026D RID: 621 RVA: 0x00010EAC File Offset: 0x0000F0AC
		public bool Contains(object key)
		{
			return this.Find(key) != null;
		}

		/// <summary>Gets a value that indicates whether the primary key columns of any row in the collection contain the values specified in the object array.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataRowCollection" /> contains a <see cref="T:System.Data.DataRow" /> with the specified key values; otherwise, false.</returns>
		/// <param name="keys">An array of primary key values to test for. </param>
		/// <exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600026E RID: 622 RVA: 0x00010EBC File Offset: 0x0000F0BC
		public bool Contains(object[] keys)
		{
			return this.Find(keys) != null;
		}

		/// <summary>Gets the row specified by the primary key value.</summary>
		/// <returns>A <see cref="T:System.Data.DataRow" /> that contains the primary key value specified; otherwise a null value if the primary key value does not exist in the <see cref="T:System.Data.DataRowCollection" />.</returns>
		/// <param name="key">The primary key value of the <see cref="T:System.Data.DataRow" /> to find. </param>
		/// <exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600026F RID: 623 RVA: 0x00010ECC File Offset: 0x0000F0CC
		public DataRow Find(object key)
		{
			return this.Find(new object[] { key }, DataViewRowState.CurrentRows);
		}

		/// <summary>Gets the row that contains the specified primary key values.</summary>
		/// <returns>A <see cref="T:System.Data.DataRow" /> object that contains the primary key values specified; otherwise a null value if the primary key value does not exist in the <see cref="T:System.Data.DataRowCollection" />.</returns>
		/// <param name="keys">An array of primary key values to find. The type of the array is Object. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No row corresponds to that index value. </exception>
		/// <exception cref="T:System.Data.MissingPrimaryKeyException">The table does not have a primary key. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000270 RID: 624 RVA: 0x00010EE0 File Offset: 0x0000F0E0
		public DataRow Find(object[] keys)
		{
			return this.Find(keys, DataViewRowState.CurrentRows);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00010EEC File Offset: 0x0000F0EC
		internal DataRow Find(object[] keys, DataViewRowState rowStateFilter)
		{
			if (this.table.PrimaryKey.Length == 0)
			{
				throw new MissingPrimaryKeyException("Table doesn't have a primary key.");
			}
			if (keys == null)
			{
				throw new ArgumentException("Expecting " + this.table.PrimaryKey.Length + " value(s) for the key being indexed, but received 0 value(s).");
			}
			Index index = this.table.GetIndex(this.table.PrimaryKey, null, rowStateFilter, null, false);
			int num = index.Find(keys);
			if (num != -1 || !this.table._duringDataLoad)
			{
				return (num == -1) ? null : this.table.RecordCache[num];
			}
			num = this.table.RecordCache.NewRecord();
			DataRow dataRow2;
			try
			{
				for (int i = 0; i < this.table.PrimaryKey.Length; i++)
				{
					this.table.PrimaryKey[i].DataContainer[num] = keys[i];
				}
				foreach (object obj in this)
				{
					DataRow dataRow = (DataRow)obj;
					int record = Key.GetRecord(dataRow, rowStateFilter);
					if (record != -1)
					{
						bool flag = true;
						for (int j = 0; j < this.table.PrimaryKey.Length; j++)
						{
							if (this.table.PrimaryKey[j].CompareValues(record, num) != 0)
							{
								flag = false;
								break;
							}
						}
						if (flag)
						{
							return dataRow;
						}
					}
				}
				dataRow2 = null;
			}
			finally
			{
				this.table.RecordCache.DisposeRecord(num);
			}
			return dataRow2;
		}

		/// <summary>Inserts a new row into the collection at the specified location.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to add. </param>
		/// <param name="pos">The (zero-based) location in the collection where you want to add the DataRow. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000272 RID: 626 RVA: 0x000110F0 File Offset: 0x0000F2F0
		public void InsertAt(DataRow row, int pos)
		{
			if (pos < 0)
			{
				throw new IndexOutOfRangeException("The row insert position " + pos + " is invalid.");
			}
			if (row == null)
			{
				throw new ArgumentNullException("row", "'row' argument cannot be null.");
			}
			if (row.Table != this.table)
			{
				throw new ArgumentException("This row already belongs to another table.");
			}
			if (row.RowID != -1)
			{
				throw new ArgumentException("This row already belongs to this table.");
			}
			row.Validate();
			row.Table.ChangingDataRow(row, DataRowAction.Add);
			if (pos >= this.List.Count)
			{
				pos = this.List.Count;
				this.List.Add(row);
			}
			else
			{
				this.List.Insert(pos, row);
				for (int i = pos + 1; i < this.List.Count; i++)
				{
					((DataRow)this.List[i]).RowID = i;
				}
			}
			row.AttachAt(pos, DataRowAction.Add);
			row.Table.ChangedDataRow(row, DataRowAction.Add);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00011208 File Offset: 0x0000F408
		internal void RemoveInternal(DataRow row)
		{
			if (row == null)
			{
				throw new IndexOutOfRangeException("The given datarow is not in the current DataRowCollection.");
			}
			int i = this.List.IndexOf(row);
			if (i < 0)
			{
				throw new IndexOutOfRangeException("The given datarow is not in the current DataRowCollection.");
			}
			this.List.RemoveAt(i);
			while (i < this.List.Count)
			{
				((DataRow)this.List[i]).RowID = i;
				i++;
			}
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataRow" /> from the collection.</summary>
		/// <param name="row">The <see cref="T:System.Data.DataRow" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000274 RID: 628 RVA: 0x00011284 File Offset: 0x0000F484
		public void Remove(DataRow row)
		{
			if (this.IndexOf(row) < 0)
			{
				throw new IndexOutOfRangeException("The given datarow is not in the current DataRowCollection.");
			}
			DataRowState rowState = row.RowState;
			if (rowState != DataRowState.Deleted && rowState != DataRowState.Detached)
			{
				row.Delete();
				if (row.RowState != DataRowState.Detached)
				{
					row.AcceptChanges();
				}
			}
		}

		/// <summary>Removes the row at the specified index from the collection.</summary>
		/// <param name="index">The index of the row to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000275 RID: 629 RVA: 0x000112D8 File Offset: 0x0000F4D8
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x000112E8 File Offset: 0x0000F4E8
		internal void OnListChanged(object sender, ListChangedEventArgs args)
		{
			if (this.ListChanged != null)
			{
				this.ListChanged(sender, args);
			}
		}

		/// <summary>Gets the total number of <see cref="T:System.Data.DataRow" /> objects in this collection.</summary>
		/// <returns>The total number of <see cref="T:System.Data.DataRow" /> objects in this collection.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00011304 File Offset: 0x0000F504
		public override int Count
		{
			get
			{
				return this.List.Count;
			}
		}

		/// <summary>Copies all the <see cref="T:System.Data.DataRow" /> objects from the collection into the given array, starting at the given destination array index.</summary>
		/// <param name="array">The one-dimensional array that is the destination of the elements copied from the DataRowCollection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000278 RID: 632 RVA: 0x00011314 File Offset: 0x0000F514
		public void CopyTo(DataRow[] array, int index)
		{
			this.CopyTo(array, index);
		}

		/// <summary>Copies all the <see cref="T:System.Data.DataRow" /> objects from the collection into the given array, starting at the given destination array index.</summary>
		/// <param name="ar">The one-dimensional array that is the destination of the elements copied from the DataRowCollection. The array must have zero-based indexing.</param>
		/// <param name="index">The zero-based index in the array at which copying begins.</param>
		// Token: 0x06000279 RID: 633 RVA: 0x00011320 File Offset: 0x0000F520
		public override void CopyTo(Array array, int index)
		{
			base.CopyTo(array, index);
		}

		/// <summary>Gets an <see cref="T:System.Collections.IEnumerator" /> for this collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for this collection.</returns>
		// Token: 0x0600027A RID: 634 RVA: 0x0001132C File Offset: 0x0000F52C
		public override IEnumerator GetEnumerator()
		{
			return base.GetEnumerator();
		}

		// Token: 0x04000105 RID: 261
		private DataTable table;
	}
}
