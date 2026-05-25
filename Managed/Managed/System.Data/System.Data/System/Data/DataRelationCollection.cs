using System;
using System.Collections;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>Represents the collection of <see cref="T:System.Data.DataRelation" /> objects for this <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000025 RID: 37
	[Editor("Microsoft.VSDesigner.Data.Design.DataRelationCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("CollectionChanged")]
	[DefaultProperty("Table")]
	public abstract class DataRelationCollection : InternalDataCollectionBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataRelationCollection" /> class. </summary>
		// Token: 0x060001C4 RID: 452 RVA: 0x0000CAD8 File Offset: 0x0000ACD8
		protected DataRelationCollection()
		{
			this.inTransition = null;
		}

		/// <summary>Occurs when the collection has changed.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060001C5 RID: 453 RVA: 0x0000CAE8 File Offset: 0x0000ACE8
		// (remove) Token: 0x060001C6 RID: 454 RVA: 0x0000CB04 File Offset: 0x0000AD04
		[ResDescription("Occurs whenever this collection's membership changes.")]
		public event CollectionChangeEventHandler CollectionChanged;

		/// <summary>Gets the <see cref="T:System.Data.DataRelation" /> object specified by name.</summary>
		/// <returns>The named <see cref="T:System.Data.DataRelation" />, or a null value if the specified <see cref="T:System.Data.DataRelation" /> does not exist.</returns>
		/// <param name="name">The name of the relation to find. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000039 RID: 57
		public abstract DataRelation this[string name] { get; }

		/// <summary>Gets the <see cref="T:System.Data.DataRelation" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.DataRelation" />, or a null value if the specified <see cref="T:System.Data.DataRelation" /> does not exist.</returns>
		/// <param name="index">The zero-based index to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index value is greater than the number of items in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700003A RID: 58
		public abstract DataRelation this[int index] { get; }

		// Token: 0x060001C9 RID: 457 RVA: 0x0000CB20 File Offset: 0x0000AD20
		private string GetNextDefaultRelationName()
		{
			int num = 1;
			string text = "Relation" + num;
			while (this.Contains(text))
			{
				text = "Relation" + num;
				num++;
			}
			return text;
		}

		/// <summary>Adds a <see cref="T:System.Data.DataRelation" /> to the <see cref="T:System.Data.DataRelationCollection" />.</summary>
		/// <param name="relation">The DataRelation to add to the collection. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> parameter is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the specified name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001CA RID: 458 RVA: 0x0000CB68 File Offset: 0x0000AD68
		public void Add(DataRelation relation)
		{
			if (this.inTransition == relation)
			{
				return;
			}
			this.inTransition = relation;
			try
			{
				CollectionChangeEventArgs collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Add, this);
				this.OnCollectionChanging(collectionChangeEventArgs);
				this.AddCore(relation);
				if (relation.RelationName == string.Empty)
				{
					relation.RelationName = this.GenerateRelationName();
				}
				relation.ParentTable.ResetPropertyDescriptorsCache();
				relation.ChildTable.ResetPropertyDescriptorsCache();
				collectionChangeEventArgs = new CollectionChangeEventArgs(CollectionChangeAction.Add, this);
				this.OnCollectionChanged(collectionChangeEventArgs);
			}
			finally
			{
				this.inTransition = null;
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000CC10 File Offset: 0x0000AE10
		private string GenerateRelationName()
		{
			this.index++;
			return "Relation" + this.index;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with a specified parent and child column, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="parentColumn">The parent column of the relation. </param>
		/// <param name="childColumn">The child column of the relation. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001CC RID: 460 RVA: 0x0000CC38 File Offset: 0x0000AE38
		public virtual DataRelation Add(DataColumn parentColumn, DataColumn childColumn)
		{
			DataRelation dataRelation = new DataRelation(this.GetNextDefaultRelationName(), parentColumn, childColumn);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified parent and child columns, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="parentColumns">The parent columns of the relation. </param>
		/// <param name="childColumns">The child columns of the relation. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="relation" /> argument is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001CD RID: 461 RVA: 0x0000CC5C File Offset: 0x0000AE5C
		public virtual DataRelation Add(DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			DataRelation dataRelation = new DataRelation(this.GetNextDefaultRelationName(), parentColumns, childColumns);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name, and parent and child columns, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="name">The name of the relation. </param>
		/// <param name="parentColumn">The parent column of the relation. </param>
		/// <param name="childColumn">The child column of the relation. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001CE RID: 462 RVA: 0x0000CC80 File Offset: 0x0000AE80
		public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn)
		{
			if (name == null || name == string.Empty)
			{
				name = this.GetNextDefaultRelationName();
			}
			DataRelation dataRelation = new DataRelation(name, parentColumn, childColumn);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name and arrays of parent and child columns, and adds it to the collection.</summary>
		/// <returns>The created DataRelation.</returns>
		/// <param name="name">The name of the DataRelation to create. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child DataColumn objects. </param>
		/// <exception cref="T:System.ArgumentNullException">The relation name is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001CF RID: 463 RVA: 0x0000CCBC File Offset: 0x0000AEBC
		public virtual DataRelation Add(string name, DataColumn[] parentColumns, DataColumn[] childColumns)
		{
			if (name == null || name == string.Empty)
			{
				name = this.GetNextDefaultRelationName();
			}
			DataRelation dataRelation = new DataRelation(name, parentColumns, childColumns);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name, parent and child columns, with optional constraints according to the value of the <paramref name="createConstraints" /> parameter, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="name">The name of the relation. </param>
		/// <param name="parentColumn">The parent column of the relation. </param>
		/// <param name="childColumn">The child column of the relation. </param>
		/// <param name="createConstraints">true to create constraints; otherwise false. (The default is true). </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001D0 RID: 464 RVA: 0x0000CCF8 File Offset: 0x0000AEF8
		public virtual DataRelation Add(string name, DataColumn parentColumn, DataColumn childColumn, bool createConstraints)
		{
			if (name == null || name == string.Empty)
			{
				name = this.GetNextDefaultRelationName();
			}
			DataRelation dataRelation = new DataRelation(name, parentColumn, childColumn, createConstraints);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Creates a <see cref="T:System.Data.DataRelation" /> with the specified name, arrays of parent and child columns, and value specifying whether to create a constraint, and adds it to the collection.</summary>
		/// <returns>The created relation.</returns>
		/// <param name="name">The name of the DataRelation to create. </param>
		/// <param name="parentColumns">An array of parent <see cref="T:System.Data.DataColumn" /> objects. </param>
		/// <param name="childColumns">An array of child DataColumn objects. </param>
		/// <param name="createConstraints">true to create a constraint; otherwise false. </param>
		/// <exception cref="T:System.ArgumentNullException">The relation name is a null value. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		/// <exception cref="T:System.Data.InvalidConstraintException">The relation has entered an invalid state since it was created. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001D1 RID: 465 RVA: 0x0000CD38 File Offset: 0x0000AF38
		public virtual DataRelation Add(string name, DataColumn[] parentColumns, DataColumn[] childColumns, bool createConstraints)
		{
			if (name == null || name == string.Empty)
			{
				name = this.GetNextDefaultRelationName();
			}
			DataRelation dataRelation = new DataRelation(name, parentColumns, childColumns, createConstraints);
			this.Add(dataRelation);
			return dataRelation;
		}

		/// <summary>Performs verification on the table.</summary>
		/// <param name="relation">The relation to check.</param>
		/// <exception cref="T:System.ArgumentNullException">The relation is null. </exception>
		/// <exception cref="T:System.ArgumentException">The relation already belongs to this collection, or it belongs to another collection. </exception>
		/// <exception cref="T:System.Data.DuplicateNameException">The collection already has a relation with the same name. (The comparison is not case sensitive.) </exception>
		// Token: 0x060001D2 RID: 466 RVA: 0x0000CD78 File Offset: 0x0000AF78
		protected virtual void AddCore(DataRelation relation)
		{
			if (relation == null)
			{
				throw new ArgumentNullException();
			}
			if (this.List.IndexOf(relation) != -1)
			{
				throw new ArgumentException();
			}
			int num = this.IndexOf(relation.RelationName);
			if (num != -1 && relation.RelationName == this[num].RelationName)
			{
				throw new DuplicateNameException("A DataRelation named '" + relation.RelationName + "' already belongs to this DataSet.");
			}
			foreach (object obj in this)
			{
				DataRelation dataRelation = (DataRelation)obj;
				bool flag = false;
				foreach (DataColumn dataColumn in relation.ChildColumns)
				{
					bool flag2 = false;
					foreach (DataColumn dataColumn2 in dataRelation.ChildColumns)
					{
						if (dataColumn2 == dataColumn)
						{
							flag2 = true;
							break;
						}
					}
					if (!flag2)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					flag = false;
					foreach (DataColumn dataColumn3 in relation.ParentColumns)
					{
						bool flag3 = false;
						foreach (DataColumn dataColumn4 in dataRelation.ParentColumns)
						{
							if (dataColumn4 == dataColumn3)
							{
								flag3 = true;
								break;
							}
						}
						if (!flag3)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						throw new ArgumentException("A relation already exists for these child columns");
					}
				}
			}
			this.List.Add(relation);
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.DataRelation" /> array to the end of the collection.</summary>
		/// <param name="relations">The array of <see cref="T:System.Data.DataRelation" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001D3 RID: 467 RVA: 0x0000CF5C File Offset: 0x0000B15C
		public virtual void AddRange(DataRelation[] relations)
		{
			if (relations == null)
			{
				return;
			}
			foreach (DataRelation dataRelation in relations)
			{
				this.Add(dataRelation);
			}
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000CF94 File Offset: 0x0000B194
		internal virtual void PostAddRange()
		{
		}

		/// <summary>Verifies whether the specified <see cref="T:System.Data.DataRelation" /> can be removed from the collection.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataRelation" /> can be removed; otherwise, false.</returns>
		/// <param name="relation">The relation to perform the check against. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001D5 RID: 469 RVA: 0x0000CF98 File Offset: 0x0000B198
		public virtual bool CanRemove(DataRelation relation)
		{
			if (relation == null || !this.GetDataSet().Equals(relation.DataSet))
			{
				return false;
			}
			int num = this.IndexOf(relation.RelationName);
			return num != -1 && relation.RelationName == this[num].RelationName;
		}

		/// <summary>Clears the collection of any relations.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001D6 RID: 470 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		public virtual void Clear()
		{
			for (int i = 0; i < this.Count; i++)
			{
				this.Remove(this[i]);
			}
			this.List.Clear();
		}

		/// <summary>Verifies whether a <see cref="T:System.Data.DataRelation" /> with the specific name (case insensitive) exists in the collection.</summary>
		/// <returns>true, if a relation with the specified name exists; otherwise false.</returns>
		/// <param name="name">The name of the relation to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001D7 RID: 471 RVA: 0x0000D030 File Offset: 0x0000B230
		public virtual bool Contains(string name)
		{
			DataSet dataSet = this.GetDataSet();
			if (dataSet != null)
			{
				DataRelation dataRelation = dataSet.Relations[name];
				if (dataRelation != null)
				{
					return true;
				}
			}
			return -1 != this.IndexOf(name, false);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000D070 File Offset: 0x0000B270
		private CollectionChangeEventArgs CreateCollectionChangeEvent(CollectionChangeAction action)
		{
			return new CollectionChangeEventArgs(action, this);
		}

		/// <summary>This method supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		// Token: 0x060001D9 RID: 473
		protected abstract DataSet GetDataSet();

		/// <summary>Gets the index of the specified <see cref="T:System.Data.DataRelation" /> object.</summary>
		/// <returns>The 0-based index of the relation, or -1 if the relation is not found in the collection.</returns>
		/// <param name="relation">The relation to search for. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001DA RID: 474 RVA: 0x0000D07C File Offset: 0x0000B27C
		public virtual int IndexOf(DataRelation relation)
		{
			return this.List.IndexOf(relation);
		}

		/// <summary>Gets the index of the <see cref="T:System.Data.DataRelation" /> specified by name.</summary>
		/// <returns>The zero-based index of the relation with the specified name, or -1 if the relation does not exist in the collection.</returns>
		/// <param name="relationName">The name of the relation to find. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001DB RID: 475 RVA: 0x0000D08C File Offset: 0x0000B28C
		public virtual int IndexOf(string relationName)
		{
			return this.IndexOf(relationName, false);
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000D098 File Offset: 0x0000B298
		private int IndexOf(string name, bool error)
		{
			int num = 0;
			int num2 = -1;
			for (int i = 0; i < this.List.Count; i++)
			{
				string relationName = ((DataRelation)this.List[i]).RelationName;
				if (string.Compare(name, relationName, true) == 0)
				{
					if (string.Compare(name, relationName, false) == 0)
					{
						return i;
					}
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

		/// <summary>Raises the <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060001DD RID: 477 RVA: 0x0000D120 File Offset: 0x0000B320
		protected virtual void OnCollectionChanged(CollectionChangeEventArgs ccevent)
		{
			if (this.CollectionChanged != null)
			{
				this.CollectionChanged(this, ccevent);
			}
		}

		/// <summary>Raises the <see cref="E:System.Data.DataRelationCollection.CollectionChanged" /> event.</summary>
		/// <param name="ccevent">A <see cref="T:System.ComponentModel.CollectionChangeEventArgs" /> that contains the event data. </param>
		// Token: 0x060001DE RID: 478 RVA: 0x0000D13C File Offset: 0x0000B33C
		protected virtual void OnCollectionChanging(CollectionChangeEventArgs ccevent)
		{
		}

		/// <summary>Removes the specified relation from the collection.</summary>
		/// <param name="relation">The relation to remove. </param>
		/// <exception cref="T:System.ArgumentNullException">The relation is a null value.</exception>
		/// <exception cref="T:System.ArgumentException">The relation does not belong to the collection.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001DF RID: 479 RVA: 0x0000D140 File Offset: 0x0000B340
		public void Remove(DataRelation relation)
		{
			if (this.inTransition == relation)
			{
				return;
			}
			this.inTransition = relation;
			if (relation == null)
			{
				return;
			}
			try
			{
				if (!this.List.Contains(relation))
				{
					throw new ArgumentException("Relation doesnot belong to this Collection.");
				}
				this.OnCollectionChanging(this.CreateCollectionChangeEvent(CollectionChangeAction.Remove));
				this.RemoveCore(relation);
				string text = "Relation" + this.index;
				if (relation.RelationName == text)
				{
					this.index--;
				}
				this.OnCollectionChanged(this.CreateCollectionChangeEvent(CollectionChangeAction.Remove));
			}
			finally
			{
				this.inTransition = null;
			}
		}

		/// <summary>Removes the relation with the specified name from the collection.</summary>
		/// <param name="name">The name of the relation to remove. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The collection does not have a relation with the specified name.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001E0 RID: 480 RVA: 0x0000D204 File Offset: 0x0000B404
		public void Remove(string name)
		{
			DataRelation dataRelation = this[name];
			if (dataRelation == null)
			{
				throw new ArgumentException("Relation doesnot belong to this Collection.");
			}
			this.Remove(dataRelation);
		}

		/// <summary>Removes the relation at the specified index from the collection.</summary>
		/// <param name="index">The index of the relation to remove. </param>
		/// <exception cref="T:System.ArgumentException">The collection does not have a relation at the specified index. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001E1 RID: 481 RVA: 0x0000D234 File Offset: 0x0000B434
		public void RemoveAt(int index)
		{
			DataRelation dataRelation = this[index];
			if (dataRelation == null)
			{
				throw new IndexOutOfRangeException(string.Format("Cannot find relation {0}", index));
			}
			this.Remove(dataRelation);
		}

		/// <summary>Performs a verification on the specified <see cref="T:System.Data.DataRelation" /> object.</summary>
		/// <param name="relation">The DataRelation object to verify. </param>
		/// <exception cref="T:System.ArgumentNullException">The collection does not have a relation at the specified index. </exception>
		/// <exception cref="T:System.ArgumentException">The specified relation does not belong to this collection, or it belongs to another collection. </exception>
		// Token: 0x060001E2 RID: 482 RVA: 0x0000D26C File Offset: 0x0000B46C
		protected virtual void RemoveCore(DataRelation relation)
		{
			this.List.Remove(relation);
		}

		/// <summary>Copies the collection of <see cref="T:System.Data.DataRelation" /> objects starting at the specified index.</summary>
		/// <param name="array">The array of <see cref="T:System.Data.DataRelation" /> objects to copy the collection to.</param>
		/// <param name="index">The index to start from.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060001E3 RID: 483 RVA: 0x0000D27C File Offset: 0x0000B47C
		public void CopyTo(DataRelation[] array, int index)
		{
			this.CopyTo(array, index);
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000D288 File Offset: 0x0000B488
		internal void BinarySerialize(SerializationInfo si)
		{
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < this.Count; i++)
			{
				DataRelation dataRelation = (DataRelation)this.List[i];
				ArrayList arrayList2 = new ArrayList();
				arrayList2.Add(dataRelation.RelationName);
				int[] array = new int[2];
				DataTable dataTable = dataRelation.ParentTable;
				array[0] = dataTable.DataSet.Tables.IndexOf(dataTable);
				array[1] = dataTable.Columns.IndexOf(dataRelation.ParentColumns[0]);
				arrayList2.Add(array);
				array = new int[2];
				dataTable = dataRelation.ChildTable;
				array[0] = dataTable.DataSet.Tables.IndexOf(dataTable);
				array[1] = dataTable.Columns.IndexOf(dataRelation.ChildColumns[0]);
				arrayList2.Add(array);
				arrayList2.Add(false);
				arrayList2.Add(null);
				arrayList.Add(arrayList2);
			}
			si.AddValue("DataSet.Relations", arrayList, typeof(ArrayList));
		}

		// Token: 0x040000E3 RID: 227
		private DataRelation inTransition;

		// Token: 0x040000E4 RID: 228
		private int index;

		// Token: 0x02000026 RID: 38
		internal class DataSetRelationCollection : DataRelationCollection
		{
			// Token: 0x060001E5 RID: 485 RVA: 0x0000D39C File Offset: 0x0000B59C
			internal DataSetRelationCollection(DataSet dataSet)
			{
				this.dataSet = dataSet;
			}

			// Token: 0x060001E6 RID: 486 RVA: 0x0000D3AC File Offset: 0x0000B5AC
			protected override DataSet GetDataSet()
			{
				return this.dataSet;
			}

			// Token: 0x060001E7 RID: 487 RVA: 0x0000D3B4 File Offset: 0x0000B5B4
			protected override void AddCore(DataRelation relation)
			{
				if (relation.ChildTable.DataSet != this.dataSet || relation.ParentTable.DataSet != this.dataSet)
				{
					throw new DataException();
				}
				base.AddCore(relation);
				relation.ParentTable.ChildRelations.Add(relation);
				relation.ChildTable.ParentRelations.Add(relation);
				relation.SetDataSet(this.dataSet);
				relation.UpdateConstraints();
			}

			// Token: 0x060001E8 RID: 488 RVA: 0x0000D430 File Offset: 0x0000B630
			protected override void RemoveCore(DataRelation relation)
			{
				base.RemoveCore(relation);
				relation.SetDataSet(null);
				relation.ParentTable.ChildRelations.Remove(relation);
				relation.ChildTable.ParentRelations.Remove(relation);
				relation.SetParentKeyConstraint(null);
				relation.SetChildKeyConstraint(null);
			}

			// Token: 0x060001E9 RID: 489 RVA: 0x0000D47C File Offset: 0x0000B67C
			public override void AddRange(DataRelation[] relations)
			{
				if (relations == null)
				{
					return;
				}
				if (this.dataSet != null && this.dataSet.InitInProgress)
				{
					this.mostRecentRelations = relations;
					return;
				}
				foreach (DataRelation dataRelation in relations)
				{
					if (dataRelation != null)
					{
						base.Add(dataRelation);
					}
				}
			}

			// Token: 0x060001EA RID: 490 RVA: 0x0000D4E0 File Offset: 0x0000B6E0
			internal override void PostAddRange()
			{
				if (this.mostRecentRelations == null)
				{
					return;
				}
				foreach (DataRelation dataRelation in this.mostRecentRelations)
				{
					if (dataRelation != null)
					{
						if (dataRelation.InitInProgress)
						{
							dataRelation.FinishInit(this.dataSet);
						}
						base.Add(dataRelation);
					}
				}
				this.mostRecentRelations = null;
			}

			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060001EB RID: 491 RVA: 0x0000D548 File Offset: 0x0000B748
			protected override ArrayList List
			{
				get
				{
					return base.List;
				}
			}

			// Token: 0x1700003C RID: 60
			public override DataRelation this[string name]
			{
				get
				{
					int num = base.IndexOf(name, true);
					return (num >= 0) ? ((DataRelation)this.List[num]) : null;
				}
			}

			// Token: 0x1700003D RID: 61
			public override DataRelation this[int index]
			{
				get
				{
					if (index < 0 || index >= this.List.Count)
					{
						throw new IndexOutOfRangeException(string.Format("Cannot find relation {0}.", index));
					}
					return (DataRelation)this.List[index];
				}
			}

			// Token: 0x040000E6 RID: 230
			private DataSet dataSet;

			// Token: 0x040000E7 RID: 231
			private DataRelation[] mostRecentRelations;
		}

		// Token: 0x02000027 RID: 39
		internal class DataTableRelationCollection : DataRelationCollection
		{
			// Token: 0x060001EE RID: 494 RVA: 0x0000D5D0 File Offset: 0x0000B7D0
			internal DataTableRelationCollection(DataTable dataTable)
			{
				this.dataTable = dataTable;
			}

			// Token: 0x060001EF RID: 495 RVA: 0x0000D5E0 File Offset: 0x0000B7E0
			protected override DataSet GetDataSet()
			{
				return this.dataTable.DataSet;
			}

			// Token: 0x1700003E RID: 62
			public override DataRelation this[string name]
			{
				get
				{
					int num = base.IndexOf(name, true);
					return (num >= 0) ? ((DataRelation)this.List[num]) : null;
				}
			}

			// Token: 0x1700003F RID: 63
			public override DataRelation this[int index]
			{
				get
				{
					if (index < 0 || index >= this.List.Count)
					{
						throw new IndexOutOfRangeException(string.Format("Cannot find relation {0}.", index));
					}
					return (DataRelation)this.List[index];
				}
			}

			// Token: 0x060001F2 RID: 498 RVA: 0x0000D670 File Offset: 0x0000B870
			protected override void AddCore(DataRelation relation)
			{
				if (this.dataTable.ParentRelations == this && relation.ChildTable != this.dataTable)
				{
					throw new ArgumentException("Cannot add a relation to this table's ParentRelations where this table is not the Child table.");
				}
				if (this.dataTable.ChildRelations == this && relation.ParentTable != this.dataTable)
				{
					throw new ArgumentException("Cannot add a relation to this table's ChildRelations where this table is not the Parent table.");
				}
				this.dataTable.DataSet.Relations.Add(relation);
				base.AddCore(relation);
			}

			// Token: 0x060001F3 RID: 499 RVA: 0x0000D6F4 File Offset: 0x0000B8F4
			protected override void RemoveCore(DataRelation relation)
			{
				relation.DataSet.Relations.Remove(relation);
				base.RemoveCore(relation);
			}

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060001F4 RID: 500 RVA: 0x0000D71C File Offset: 0x0000B91C
			protected override ArrayList List
			{
				get
				{
					return base.List;
				}
			}

			// Token: 0x040000E8 RID: 232
			private DataTable dataTable;
		}
	}
}
