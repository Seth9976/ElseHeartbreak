using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace System.Data
{
	/// <summary>Represents the collection of tables for the <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000035 RID: 53
	[DefaultEvent("CollectionChanged")]
	[Editor("Microsoft.VSDesigner.Data.Design.TablesCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class DataTableCollection : InternalDataCollectionBase
	{
		// Token: 0x060003F6 RID: 1014 RVA: 0x0001944C File Offset: 0x0001764C
		internal DataTableCollection(DataSet dataSet)
		{
			this.dataSet = dataSet;
		}

		/// <summary>Occurs after the <see cref="T:System.Data.DataTableCollection" /> is changed because of <see cref="T:System.Data.DataTable" /> objects being added or removed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060003F7 RID: 1015 RVA: 0x0001945C File Offset: 0x0001765C
		// (remove) Token: 0x060003F8 RID: 1016 RVA: 0x00019478 File Offset: 0x00017678
		[ResDescription("Occurs whenever this collection's membership changes.")]
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>Occurs while the <see cref="T:System.Data.DataTableCollection" /> is changing because of <see cref="T:System.Data.DataTable" /> objects being added or removed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060003F9 RID: 1017 RVA: 0x00019494 File Offset: 0x00017694
		// (remove) Token: 0x060003FA RID: 1018 RVA: 0x000194B0 File Offset: 0x000176B0
		public event CollectionChangeEventHandler CollectionChanging;

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object at the specified index.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000097 RID: 151
		public DataTable this[int index]
		{
			get
			{
				if (index < 0 || index >= this.List.Count)
				{
					throw new IndexOutOfRangeException(string.Format("Cannot find table {0}", index));
				}
				return (DataTable)this.List[index];
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object with the specified name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> with the specified name; otherwise null if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		/// <param name="name">The name of the DataTable to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000098 RID: 152
		public DataTable this[string name]
		{
			get
			{
				int num = this.IndexOf(name, true);
				return (num >= 0) ? ((DataTable)this.List[num]) : null;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060003FD RID: 1021 RVA: 0x0001954C File Offset: 0x0001774C
		protected override ArrayList List
		{
			get
			{
				return base.List;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Data.DataTable" /> object by using a default name and adds it to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataTable" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060003FE RID: 1022 RVA: 0x00019554 File Offset: 0x00017754
		public DataTable Add()
		{
			DataTable dataTable = new DataTable();
			this.Add(dataTable);
			return dataTable;
		}

		/// <summary>Adds the specified DataTable to the collection.</summary>
		/// <param name="table">The DataTable object to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The value specified for the table is null. </exception>
		/// <exception cref="T:System.ArgumentException">The table already belongs to this collection, or belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">A table in the collection has the same name. The comparison is not case sensitive. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060003FF RID: 1023 RVA: 0x00019570 File Offset: 0x00017770
		public void Add(DataTable table)
		{
			this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
			if (table == null)
			{
				throw new ArgumentNullException("table");
			}
			if (this.List.Contains(table))
			{
				throw new ArgumentException("DataTable already belongs to this DataSet.");
			}
			if (table.DataSet != null && table.DataSet != this.dataSet)
			{
				throw new ArgumentException("DataTable already belongs to another DataSet");
			}
			if (table.TableName == null || table.TableName == string.Empty)
			{
				this.NameTable(table);
			}
			int num = this.IndexOf(table.TableName, table.Namespace);
			if (num != -1 && table.TableName == this[num].TableName)
			{
				throw new DuplicateNameException("A DataTable named '" + table.TableName + "' already belongs to this DataSet.");
			}
			this.List.Add(table);
			table.dataSet = this.dataSet;
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, table));
		}

		/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object by using the specified name and adds it to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="name">The name to give the created <see cref="T:System.Data.DataTable" />. </param>
		/// <exception cref="T:System.Data.DuplicateNameException">A table in the collection has the same name. (The comparison is not case sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000400 RID: 1024 RVA: 0x0001967C File Offset: 0x0001787C
		public DataTable Add(string name)
		{
			DataTable dataTable = new DataTable(name);
			this.Add(dataTable);
			return dataTable;
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.DataTable" /> array to the end of the collection.</summary>
		/// <param name="tables">The array of <see cref="T:System.Data.DataTable" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000401 RID: 1025 RVA: 0x00019698 File Offset: 0x00017898
		public void AddRange(DataTable[] tables)
		{
			if (this.dataSet != null && this.dataSet.InitInProgress)
			{
				this.mostRecentTables = tables;
				return;
			}
			if (tables == null)
			{
				return;
			}
			foreach (DataTable dataTable in tables)
			{
				if (dataTable != null)
				{
					this.Add(dataTable);
				}
			}
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000196FC File Offset: 0x000178FC
		internal void PostAddRange()
		{
			if (this.mostRecentTables == null)
			{
				return;
			}
			foreach (DataTable dataTable in this.mostRecentTables)
			{
				if (dataTable != null)
				{
					this.Add(dataTable);
				}
			}
			this.mostRecentTables = null;
		}

		/// <summary>Verifies whether the specified <see cref="T:System.Data.DataTable" /> object can be removed from the collection.</summary>
		/// <returns>true if the table can be removed; otherwise false.</returns>
		/// <param name="table">The DataTable in the collection to perform the check against. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000403 RID: 1027 RVA: 0x00019750 File Offset: 0x00017950
		public bool CanRemove(DataTable table)
		{
			return this.CanRemove(table, false);
		}

		/// <summary>Clears the collection of all <see cref="T:System.Data.DataTable" /> objects.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000404 RID: 1028 RVA: 0x0001975C File Offset: 0x0001795C
		public void Clear()
		{
			this.List.Clear();
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Data.DataTable" /> object with the specified name exists in the collection.</summary>
		/// <returns>true if the specified table exists; otherwise false.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000405 RID: 1029 RVA: 0x0001976C File Offset: 0x0001796C
		public bool Contains(string name)
		{
			return -1 != this.IndexOf(name, false);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <returns>The zero-based index of the table, or -1 if the table is not found in the collection.</returns>
		/// <param name="table">The DataTable to search for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000406 RID: 1030 RVA: 0x0001977C File Offset: 0x0001797C
		public int IndexOf(DataTable table)
		{
			return this.List.IndexOf(table);
		}

		/// <summary>Gets the index in the collection of the <see cref="T:System.Data.DataTable" /> object with the specified name.</summary>
		/// <returns>The zero-based index of the DataTable with the specified name, or -1 if the table does not exist in the collection.Note:Returns -1 when two or more tables have the same name but different namespaces. The call does not succeed if there is any ambiguity when matching a table name to exactly one table.</returns>
		/// <param name="tableName">The name of the DataTable object to look for. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000407 RID: 1031 RVA: 0x0001978C File Offset: 0x0001798C
		public int IndexOf(string tableName)
		{
			return this.IndexOf(tableName, false);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.DataTable" /> object from the collection.</summary>
		/// <param name="table">The DataTable to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The value specified for the table is null. </exception>
		/// <exception cref="T:System.ArgumentException">The table does not belong to this collection.-or- The table is part of a relationship. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000408 RID: 1032 RVA: 0x00019798 File Offset: 0x00017998
		public void Remove(DataTable table)
		{
			this.OnCollectionChanging(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
			if (this.CanRemove(table, true))
			{
				table.dataSet = null;
			}
			this.List.Remove(table);
			table.dataSet = null;
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, table));
		}

		/// <summary>Removes the <see cref="T:System.Data.DataTable" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> object to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a table with the specified name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000409 RID: 1033 RVA: 0x000197E8 File Offset: 0x000179E8
		public void Remove(string name)
		{
			int num = this.IndexOf(name, false);
			if (num == -1)
			{
				throw new ArgumentException("Table " + name + " does not belong to this DataSet");
			}
			this.RemoveAt(num);
		}

		/// <summary>Removes the <see cref="T:System.Data.DataTable" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the DataTable to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a table at the specified index. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600040A RID: 1034 RVA: 0x00019824 File Offset: 0x00017A24
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00019834 File Offset: 0x00017A34
		internal void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanging != null)
			{
				this.CollectionChanging(this, ccevent);
			}
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00019850 File Offset: 0x00017A50
		internal void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, ccevent);
			}
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001986C File Offset: 0x00017A6C
		private int IndexOf(string name, bool error, int start)
		{
			int num = 0;
			int num2 = -1;
			for (int i = start; i < this.List.Count; i++)
			{
				string tableName = ((DataTable)this.List[i]).TableName;
				if (string.Compare(name, tableName, false) == 0)
				{
					return i;
				}
				if (string.Compare(name, tableName, true) == 0)
				{
					num2 = i;
					num++;
				}
			}
			if (num == 1)
			{
				return num2;
			}
			if (num > 1 && error)
			{
				throw new ArgumentException("There is no match for the name in the same case and there are multiple matches in different case.");
			}
			return -1;
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000198F4 File Offset: 0x00017AF4
		private void NameTable(DataTable Table)
		{
			string text = "Table";
			int num = 1;
			while (this.Contains(text + num))
			{
				num++;
			}
			Table.TableName = text + num;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0001993C File Offset: 0x00017B3C
		private bool CanRemove(DataTable table, bool throwException)
		{
			if (table == null)
			{
				if (throwException)
				{
					throw new ArgumentNullException("table");
				}
				return false;
			}
			else if (table.DataSet != this.dataSet)
			{
				if (!throwException)
				{
					return false;
				}
				throw new ArgumentException("Table " + table.TableName + " does not belong to this DataSet.");
			}
			else
			{
				if (table.ParentRelations.Count <= 0 && table.ChildRelations.Count <= 0)
				{
					foreach (object obj in table.Constraints)
					{
						Constraint constraint = (Constraint)obj;
						UniqueConstraint uniqueConstraint = constraint as UniqueConstraint;
						if (uniqueConstraint != null)
						{
							if (uniqueConstraint.ChildConstraint == null)
							{
								continue;
							}
							if (!throwException)
							{
								return false;
							}
							this.RaiseForeignKeyReferenceException(table.TableName, uniqueConstraint.ChildConstraint.ConstraintName);
						}
						ForeignKeyConstraint foreignKeyConstraint = constraint as ForeignKeyConstraint;
						if (foreignKeyConstraint != null)
						{
							if (!throwException)
							{
								return false;
							}
							this.RaiseForeignKeyReferenceException(table.TableName, foreignKeyConstraint.ConstraintName);
						}
					}
					return true;
				}
				if (!throwException)
				{
					return false;
				}
				throw new ArgumentException("Cannot remove a table that has existing relations. Remove relations first.");
			}
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00019AA4 File Offset: 0x00017CA4
		private void RaiseForeignKeyReferenceException(string table, string constraint)
		{
			throw new ArgumentException(string.Format("Cannot remove table {0}, because it is referenced in ForeignKeyConstraint {1}. Remove the constraint first.", table, constraint));
		}

		/// <summary>Gets the <see cref="T:System.Data.DataTable" /> object with the specified name in the specified namespace.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> with the specified name; otherwise null if the <see cref="T:System.Data.DataTable" /> does not exist.</returns>
		/// <param name="name">The name of the DataTable to find.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700009A RID: 154
		public DataTable this[string name, string tbNamespace]
		{
			get
			{
				int num = this.IndexOf(name, tbNamespace, true);
				return (num >= 0) ? ((DataTable)this.List[num]) : null;
			}
		}

		/// <summary>Creates a <see cref="T:System.Data.DataTable" /> object by using the specified name and adds it to the collection.</summary>
		/// <returns>The newly created <see cref="T:System.Data.DataTable" />.</returns>
		/// <param name="name">The name to give the created <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="tableNamespace">The namespace to give the created <see cref="T:System.Data.DataTable" />.</param>
		/// <exception cref="T:System.Data.DuplicateNameException">A table in the collection has the same name. (The comparison is not case sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000412 RID: 1042 RVA: 0x00019AF0 File Offset: 0x00017CF0
		public DataTable Add(string name, string tbNamespace)
		{
			DataTable dataTable = new DataTable(name, tbNamespace);
			this.Add(dataTable);
			return dataTable;
		}

		/// <summary>Gets a value that indicates whether a <see cref="T:System.Data.DataTable" /> object with the specified name and table namespace exists in the collection.</summary>
		/// <returns>true if the specified table exists; otherwise false.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> to find.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000413 RID: 1043 RVA: 0x00019B10 File Offset: 0x00017D10
		public bool Contains(string name, string tableNamespace)
		{
			return this.IndexOf(name, tableNamespace) != -1;
		}

		/// <summary>Gets the index in the collection of the specified <see cref="T:System.Data.DataTable" /> object.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Data.DataTable" /> with the specified name, or -1 if the table does not exist in the collection.</returns>
		/// <param name="tableName">The name of the <see cref="T:System.Data.DataTable" /> object to look for.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000414 RID: 1044 RVA: 0x00019B20 File Offset: 0x00017D20
		public int IndexOf(string tableName, string tableNamespace)
		{
			if (tableNamespace == null)
			{
				throw new ArgumentNullException("'tableNamespace' argument cannot be null.", "tableNamespace");
			}
			return this.IndexOf(tableName, tableNamespace, false);
		}

		/// <summary>Removes the <see cref="T:System.Data.DataTable" /> object with the specified name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.DataTable" /> object to remove.</param>
		/// <param name="tableNamespace">The name of the <see cref="T:System.Data.DataTable" /> namespace to look in.</param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a table with the specified name. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000415 RID: 1045 RVA: 0x00019B44 File Offset: 0x00017D44
		public void Remove(string name, string tableNamespace)
		{
			int num = this.IndexOf(name, tableNamespace, true);
			if (num == -1)
			{
				throw new ArgumentException("Table " + name + " does not belong to this DataSet");
			}
			this.RemoveAt(num);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x00019B80 File Offset: 0x00017D80
		private int IndexOf(string name, string ns, bool error)
		{
			int num = -1;
			int num2 = 0;
			int num3 = -1;
			do
			{
				num = this.IndexOf(name, error, num + 1);
				if (num == -1)
				{
					break;
				}
				if (ns == null)
				{
					if (num2 > 1)
					{
						break;
					}
					num2++;
					num3 = num;
				}
				else if (this[num].Namespace.Equals(ns))
				{
					return num;
				}
			}
			while (num != -1 && num < this.Count);
			if (num2 == 1)
			{
				return num3;
			}
			if (num2 == 0 || !error)
			{
				return -1;
			}
			throw new ArgumentException("The given name '" + name + "' matches atleast two namesin the collection object with different namespaces");
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00019C20 File Offset: 0x00017E20
		private int IndexOf(string name, bool error)
		{
			return this.IndexOf(name, null, error);
		}

		/// <summary>Copies all the elements of the current <see cref="T:System.Data.DataTableCollection" /> to a one-dimensional <see cref="T:System.Array" />, starting at the specified destination array index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> to copy the current <see cref="T:System.Data.DataTableCollection" /> object's elements into.</param>
		/// <param name="index">The destination <see cref="T:System.Array" /> index to start copying into.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000418 RID: 1048 RVA: 0x00019C2C File Offset: 0x00017E2C
		public void CopyTo(DataTable[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x00019C38 File Offset: 0x00017E38
		internal void BinarySerialize_Schema(SerializationInfo si)
		{
			si.AddValue("DataSet.Tables.Count", this.Count);
			for (int i = 0; i < this.Count; i++)
			{
				DataTable dataTable = (DataTable)this.List[i];
				if (dataTable.dataSet != this.dataSet)
				{
					throw new SystemException("Internal Error: inconsistent DataTable");
				}
				MemoryStream memoryStream = new MemoryStream();
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				binaryFormatter.Serialize(memoryStream, dataTable);
				byte[] array = memoryStream.ToArray();
				memoryStream.Close();
				si.AddValue("DataSet.Tables_" + i, array, typeof(byte[]));
			}
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00019CE0 File Offset: 0x00017EE0
		internal void BinarySerialize_Data(SerializationInfo si)
		{
			for (int i = 0; i < this.Count; i++)
			{
				DataTable dataTable = (DataTable)this.List[i];
				for (int j = 0; j < dataTable.Columns.Count; j++)
				{
					si.AddValue(string.Concat(new object[] { "DataTable_", i, ".DataColumn_", j, ".Expression" }), dataTable.Columns[j].Expression);
				}
				dataTable.BinarySerialize(si, "DataTable_" + i + ".");
			}
		}

		// Token: 0x0400015D RID: 349
		private DataSet dataSet;

		// Token: 0x0400015E RID: 350
		private DataTable[] mostRecentTables;
	}
}
