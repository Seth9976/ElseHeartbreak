using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data
{
	/// <summary>Represents a collection of constraints for a <see cref="T:System.Data.DataTable" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000013 RID: 19
	[DefaultEvent("CollectionChanged")]
	[Editor("Microsoft.VSDesigner.Data.Design.ConstraintsCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class ConstraintCollection : InternalDataCollectionBase
	{
		// Token: 0x06000051 RID: 81 RVA: 0x00003BB8 File Offset: 0x00001DB8
		internal ConstraintCollection(DataTable table)
		{
			this.table = table;
		}

		/// <summary>Occurs whenever the <see cref="T:System.Data.ConstraintCollection" /> is changed because of <see cref="T:System.Data.Constraint" /> objects being added or removed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000052 RID: 82 RVA: 0x00003BC8 File Offset: 0x00001DC8
		// (remove) Token: 0x06000053 RID: 83 RVA: 0x00003BE4 File Offset: 0x00001DE4
		public event CollectionChangeEventHandler CollectionChanged;

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003C00 File Offset: 0x00001E00
		internal DataTable Table
		{
			get
			{
				return this.table;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Constraint" /> from the collection with the specified name.</summary>
		/// <returns>The <see cref="T:System.Data.Constraint" /> with the specified name; otherwise a null value if the <see cref="T:System.Data.Constraint" /> does not exist.</returns>
		/// <param name="name">The <see cref="P:System.Data.Constraint.ConstraintName" /> of the constraint to return. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000009 RID: 9
		public Constraint this[string name]
		{
			get
			{
				int num = this.IndexOf(name);
				return (num != -1) ? ((Constraint)this.List[num]) : null;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Constraint" /> from the collection at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Constraint" /> at the specified index.</returns>
		/// <param name="index">The index of the constraint to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700000A RID: 10
		public Constraint this[int index]
		{
			get
			{
				if (index < 0 || index >= this.List.Count)
				{
					throw new IndexOutOfRangeException();
				}
				return (Constraint)this.List[index];
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00003C78 File Offset: 0x00001E78
		private void _handleBeforeConstraintNameChange(object sender, string newName)
		{
			if (newName == null || newName == string.Empty)
			{
				throw new ArgumentException("ConstraintName cannot be set to null or empty after adding it to a ConstraintCollection.");
			}
			if (this._isDuplicateConstraintName(newName, (Constraint)sender))
			{
				throw new DuplicateNameException("Constraint name already exists.");
			}
		}

		// Token: 0x06000058 RID: 88 RVA: 0x00003CC4 File Offset: 0x00001EC4
		private bool _isDuplicateConstraintName(string constraintName, Constraint excludeFromComparison)
		{
			foreach (object obj in this.List)
			{
				Constraint constraint = (Constraint)obj;
				if (constraint != excludeFromComparison)
				{
					if (string.Compare(constraintName, constraint.ConstraintName, false, this.Table.Locale) == 0)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003D60 File Offset: 0x00001F60
		private string _createNewConstraintName()
		{
			int num = 1;
			string text;
			for (;;)
			{
				text = "Constraint" + num;
				if (this.IndexOf(text) == -1)
				{
					break;
				}
				num++;
			}
			return text;
		}

		/// <summary>Adds the specified <see cref="T:System.Data.Constraint" /> object to the collection.</summary>
		/// <param name="constraint">The Constraint to add. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="constraint" /> argument is null. </exception>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection, or belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the same name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600005A RID: 90 RVA: 0x00003D9C File Offset: 0x00001F9C
		public void Add(Constraint constraint)
		{
			if (constraint == null)
			{
				throw new ArgumentNullException("Can not add null.");
			}
			if (constraint.InitInProgress)
			{
				throw new ArgumentException("Hmm .. Failed to Add to collection");
			}
			if (this == constraint.ConstraintCollection)
			{
				throw new ArgumentException("Constraint already belongs to this collection.");
			}
			if (constraint.ConstraintCollection != null)
			{
				throw new ArgumentException("Constraint already belongs to another collection.");
			}
			foreach (object obj in this)
			{
				Constraint constraint2 = (Constraint)obj;
				if (constraint2.Equals(constraint))
				{
					throw new DataException("Constraint matches contraint named '" + constraint2.ConstraintName + "' already in collection");
				}
			}
			if (this._isDuplicateConstraintName(constraint.ConstraintName, null))
			{
				throw new DuplicateNameException("Constraint name already exists.");
			}
			constraint.AddToConstraintCollectionSetup(this);
			if (constraint.ConstraintName == null || constraint.ConstraintName == string.Empty)
			{
				constraint.ConstraintName = this._createNewConstraintName();
			}
			constraint.BeforeConstraintNameChange += this._handleBeforeConstraintNameChange;
			constraint.ConstraintCollection = this;
			this.List.Add(constraint);
			if (constraint is UniqueConstraint && ((UniqueConstraint)constraint).IsPrimaryKey)
			{
				this.table.PrimaryKey = ((UniqueConstraint)constraint).Columns;
			}
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Add, this));
		}

		/// <summary>Constructs a new <see cref="T:System.Data.UniqueConstraint" /> with the specified name, <see cref="T:System.Data.DataColumn" />, and value that indicates whether the column is a primary key, and adds it to the collection.</summary>
		/// <returns>A new UniqueConstraint.</returns>
		/// <param name="name">The name of the UniqueConstraint. </param>
		/// <param name="column">The <see cref="T:System.Data.DataColumn" /> to which the constraint applies. </param>
		/// <param name="primaryKey">Specifies whether the column should be the primary key. If true, the column will be a primary key column. </param>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection.-Or- The constraint belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600005B RID: 91 RVA: 0x00003F30 File Offset: 0x00002130
		public Constraint Add(string name, DataColumn column, bool primaryKey)
		{
			UniqueConstraint uniqueConstraint = new UniqueConstraint(name, column, primaryKey);
			this.Add(uniqueConstraint);
			return uniqueConstraint;
		}

		/// <summary>Constructs a new <see cref="T:System.Data.ForeignKeyConstraint" /> with the specified name, parent column, and child column, and adds the constraint to the collection.</summary>
		/// <returns>A new <see cref="T:System.Data.ForeignKeyConstraint" />.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.ForeignKeyConstraint" />. </param>
		/// <param name="primaryKeyColumn">The primary key, or parent, <see cref="T:System.Data.DataColumn" />. </param>
		/// <param name="foreignKeyColumn">The foreign key, or child, <see cref="T:System.Data.DataColumn" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600005C RID: 92 RVA: 0x00003F50 File Offset: 0x00002150
		public Constraint Add(string name, DataColumn primaryKeyColumn, DataColumn foreignKeyColumn)
		{
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(name, primaryKeyColumn, foreignKeyColumn);
			this.Add(foreignKeyConstraint);
			return foreignKeyConstraint;
		}

		/// <summary>Constructs a new <see cref="T:System.Data.UniqueConstraint" /> with the specified name, array of <see cref="T:System.Data.DataColumn" /> objects, and value that indicates whether the column is a primary key, and adds it to the collection.</summary>
		/// <returns>A new <see cref="T:System.Data.UniqueConstraint" />.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.UniqueConstraint" />. </param>
		/// <param name="columns">An array of <see cref="T:System.Data.DataColumn" /> objects to which the constraint applies. </param>
		/// <param name="primaryKey">Specifies whether the column should be the primary key. If true, the column will be a primary key column.</param>
		/// <exception cref="T:System.ArgumentException">The constraint already belongs to this collection.-Or- The constraint belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a constraint with the specified name. (The comparison is not case-sensitive.) </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600005D RID: 93 RVA: 0x00003F70 File Offset: 0x00002170
		public Constraint Add(string name, DataColumn[] columns, bool primaryKey)
		{
			UniqueConstraint uniqueConstraint = new UniqueConstraint(name, columns, primaryKey);
			this.Add(uniqueConstraint);
			return uniqueConstraint;
		}

		/// <summary>Constructs a new <see cref="T:System.Data.ForeignKeyConstraint" />, with the specified arrays of parent columns and child columns, and adds the constraint to the collection.</summary>
		/// <returns>A new <see cref="T:System.Data.ForeignKeyConstraint" />.</returns>
		/// <param name="name">The name of the <see cref="T:System.Data.ForeignKeyConstraint" />. </param>
		/// <param name="primaryKeyColumns">An array of <see cref="T:System.Data.DataColumn" /> objects that are the primary key, or parent, columns. </param>
		/// <param name="foreignKeyColumns">An array of <see cref="T:System.Data.DataColumn" /> objects that are the foreign key, or child, columns. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600005E RID: 94 RVA: 0x00003F90 File Offset: 0x00002190
		public Constraint Add(string name, DataColumn[] primaryKeyColumns, DataColumn[] foreignKeyColumns)
		{
			ForeignKeyConstraint foreignKeyConstraint = new ForeignKeyConstraint(name, primaryKeyColumns, foreignKeyColumns);
			this.Add(foreignKeyConstraint);
			return foreignKeyConstraint;
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.ConstraintCollection" /> array to the end of the collection.</summary>
		/// <param name="constraints">An array of <see cref="T:System.Data.ConstraintCollection" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600005F RID: 95 RVA: 0x00003FB0 File Offset: 0x000021B0
		public void AddRange(Constraint[] constraints)
		{
			if (this.Table.InitInProgress)
			{
				this._mostRecentConstraints = constraints;
				return;
			}
			if (constraints == null)
			{
				return;
			}
			for (int i = 0; i < constraints.Length; i++)
			{
				if (constraints[i] != null)
				{
					this.Add(constraints[i]);
				}
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00004004 File Offset: 0x00002204
		internal void PostAddRange()
		{
			if (this._mostRecentConstraints == null)
			{
				return;
			}
			for (int i = 0; i < this._mostRecentConstraints.Length; i++)
			{
				Constraint constraint = this._mostRecentConstraints[i];
				if (constraint != null)
				{
					if (constraint.InitInProgress)
					{
						constraint.FinishInit(this.Table);
					}
					this.Add(constraint);
				}
			}
			this._mostRecentConstraints = null;
		}

		/// <summary>Indicates whether a <see cref="T:System.Data.Constraint" /> can be removed.</summary>
		/// <returns>true if the <see cref="T:System.Data.Constraint" /> can be removed from collection; otherwise, false.</returns>
		/// <param name="constraint">The <see cref="T:System.Data.Constraint" /> to be tested for removal from the collection. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000061 RID: 97 RVA: 0x00004070 File Offset: 0x00002270
		public bool CanRemove(Constraint constraint)
		{
			return constraint.CanRemoveFromCollection(this, false);
		}

		/// <summary>Clears the collection of any <see cref="T:System.Data.Constraint" /> objects.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000062 RID: 98 RVA: 0x0000407C File Offset: 0x0000227C
		public void Clear()
		{
			this.Table.PrimaryKey = null;
			foreach (object obj in this.List)
			{
				Constraint constraint = (Constraint)obj;
				constraint.ConstraintCollection = null;
				constraint.BeforeConstraintNameChange -= this._handleBeforeConstraintNameChange;
			}
			this.List.Clear();
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Refresh, this));
		}

		/// <summary>Indicates whether the <see cref="T:System.Data.Constraint" /> object specified by name exists in the collection.</summary>
		/// <returns>true if the collection contains the specified constraint; otherwise, false.</returns>
		/// <param name="name">The <see cref="P:System.Data.Constraint.ConstraintName" /> of the constraint. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000063 RID: 99 RVA: 0x00004124 File Offset: 0x00002324
		public bool Contains(string name)
		{
			return -1 != this.IndexOf(name);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.Constraint" />.</summary>
		/// <returns>The zero-based index of the <see cref="T:System.Data.Constraint" /> if it is in the collection; otherwise, -1.</returns>
		/// <param name="constraint">The <see cref="T:System.Data.Constraint" /> to search for. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000064 RID: 100 RVA: 0x00004134 File Offset: 0x00002334
		public int IndexOf(Constraint constraint)
		{
			int num = 0;
			foreach (object obj in this)
			{
				Constraint constraint2 = (Constraint)obj;
				if (constraint2 == constraint)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		/// <summary>Gets the index of the <see cref="T:System.Data.Constraint" /> specified by name.</summary>
		/// <returns>The index of the <see cref="T:System.Data.Constraint" /> if it is in the collection; otherwise, -1.</returns>
		/// <param name="constraintName">The name of the <see cref="T:System.Data.Constraint" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000065 RID: 101 RVA: 0x000041B4 File Offset: 0x000023B4
		public int IndexOf(string constraintName)
		{
			int num = 0;
			foreach (object obj in this.List)
			{
				Constraint constraint = (Constraint)obj;
				if (string.Compare(constraintName, constraint.ConstraintName, !this.Table.CaseSensitive, this.Table.Locale) == 0)
				{
					return num;
				}
				num++;
			}
			return -1;
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Constraint" /> from the collection.</summary>
		/// <param name="constraint">The <see cref="T:System.Data.Constraint" /> to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="constraint" /> argument is null. </exception>
		/// <exception cref="T:System.ArgumentException">The constraint does not belong to the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000066 RID: 102 RVA: 0x0000425C File Offset: 0x0000245C
		public void Remove(Constraint constraint)
		{
			if (constraint == null)
			{
				throw new ArgumentNullException();
			}
			if (!constraint.CanRemoveFromCollection(this, true))
			{
				return;
			}
			constraint.RemoveFromConstraintCollectionCleanup(this);
			constraint.ConstraintCollection = null;
			this.List.Remove(constraint);
			this.OnCollectionChanged(new CollectionChangeEventArgs(CollectionChangeAction.Remove, this));
		}

		/// <summary>Removes the <see cref="T:System.Data.Constraint" /> object specified by name from the collection.</summary>
		/// <param name="name">The name of the <see cref="T:System.Data.Constraint" /> to remove. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000067 RID: 103 RVA: 0x000042AC File Offset: 0x000024AC
		public void Remove(string name)
		{
			int num = this.IndexOf(name);
			if (num == -1)
			{
				throw new ArgumentException("Constraint '" + name + "' does not belong to this DataTable.");
			}
			this.Remove(this[num]);
		}

		/// <summary>Removes the <see cref="T:System.Data.Constraint" /> object at the specified index from the collection.</summary>
		/// <param name="index">The index of the <see cref="T:System.Data.Constraint" /> to remove. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The collection does not have a constraint at this index. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000068 RID: 104 RVA: 0x000042EC File Offset: 0x000024EC
		public void RemoveAt(int index)
		{
			this.Remove(this[index]);
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000042FC File Offset: 0x000024FC
		protected override ArrayList List
		{
			get
			{
				return base.List;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004304 File Offset: 0x00002504
		internal void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, ccevent);
			}
		}

		/// <summary>Copies the collection objects to a one-dimensional <see cref="T:System.Array" /> instance starting at the specified index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the values copied from the collection.</param>
		/// <param name="index">The index of the array at which to start inserting. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600006B RID: 107 RVA: 0x00004320 File Offset: 0x00002520
		public void CopyTo(Constraint[] array, int index)
		{
			base.CopyTo(array, index);
		}

		// Token: 0x04000081 RID: 129
		private DataTable table;

		// Token: 0x04000082 RID: 130
		private Constraint[] _mostRecentConstraints;
	}
}
