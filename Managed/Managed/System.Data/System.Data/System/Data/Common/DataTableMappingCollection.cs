using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>A collection of <see cref="T:System.Data.Common.DataTableMapping" /> objects. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000AE RID: 174
	[Editor("Microsoft.VSDesigner.Data.Design.DataTableMappingCollectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ListBindable(false)]
	public sealed class DataTableMappingCollection : MarshalByRefObject, IList, ITableMappingCollection, IEnumerable, ICollection
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Common.DataTableMappingCollection" /> class. This new instance is empty, that is, it does not yet contain any <see cref="T:System.Data.Common.DataTableMapping" /> objects.</summary>
		// Token: 0x060007CC RID: 1996 RVA: 0x00025470 File Offset: 0x00023670
		public DataTableMappingCollection()
		{
			this.mappings = new ArrayList();
			this.sourceTables = new Hashtable();
			this.dataSetTables = new Hashtable();
		}

		// Token: 0x17000156 RID: 342
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is DataTableMapping))
				{
					throw new ArgumentException();
				}
				this[index] = (DataTableMapping)value;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x000254C8 File Offset: 0x000236C8
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.mappings.IsSynchronized;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x000254D8 File Offset: 0x000236D8
		object ICollection.SyncRoot
		{
			get
			{
				return this.mappings.SyncRoot;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x000254E8 File Offset: 0x000236E8
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x000254EC File Offset: 0x000236EC
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700015B RID: 347
		object ITableMappingCollection.this[string index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is DataTableMapping))
				{
					throw new ArgumentException();
				}
				this[index] = (DataTableMapping)value;
			}
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x0002551C File Offset: 0x0002371C
		ITableMapping ITableMappingCollection.Add(string sourceTableName, string dataSetTableName)
		{
			ITableMapping tableMapping = new DataTableMapping(sourceTableName, dataSetTableName);
			this.Add(tableMapping);
			return tableMapping;
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x0002553C File Offset: 0x0002373C
		ITableMapping ITableMappingCollection.GetByDataSetTable(string dataSetTableName)
		{
			return this[this.mappings.IndexOf(this.dataSetTables[dataSetTableName])];
		}

		/// <summary>Gets the number of <see cref="T:System.Data.Common.DataTableMapping" /> objects in the collection.</summary>
		/// <returns>The number of DataTableMapping objects in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0002555C File Offset: 0x0002375C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public int Count
		{
			get
			{
				return this.mappings.Count;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataTableMapping" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to return. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700015D RID: 349
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataTableMapping this[int index]
		{
			get
			{
				return (DataTableMapping)this.mappings[index];
			}
			set
			{
				DataTableMapping dataTableMapping = (DataTableMapping)this.mappings[index];
				this.sourceTables[dataTableMapping.SourceTable] = value;
				this.dataSetTables[dataTableMapping.DataSetTable] = value;
				this.mappings[index] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</returns>
		/// <param name="sourceTable">The case-sensitive name of the source table. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700015E RID: 350
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataTableMapping this[string sourceTable]
		{
			get
			{
				return (DataTableMapping)this.sourceTables[sourceTable];
			}
			set
			{
				this[this.mappings.IndexOf(this.sourceTables[sourceTable])] = value;
			}
		}

		/// <summary>Adds an <see cref="T:System.Object" /> that is a table mapping to the collection.</summary>
		/// <returns>The index of the DataTableMapping object added to the collection.</returns>
		/// <param name="value">A DataTableMapping object to add to the collection. </param>
		/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataTableMapping" /> object. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007DC RID: 2012 RVA: 0x00025604 File Offset: 0x00023804
		public int Add(object value)
		{
			if (!(value is DataTableMapping))
			{
				throw new InvalidCastException("The object passed in was not a DataTableMapping object.");
			}
			this.sourceTables[((DataTableMapping)value).SourceTable] = value;
			this.dataSetTables[((DataTableMapping)value).DataSetTable] = value;
			return this.mappings.Add(value);
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataTableMapping" /> object to the collection when given a source table name and a <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object that was added to the collection.</returns>
		/// <param name="sourceTable">The case-sensitive name of the source table to map from. </param>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> table to map to. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007DD RID: 2013 RVA: 0x00025664 File Offset: 0x00023864
		public DataTableMapping Add(string sourceTable, string dataSetTable)
		{
			DataTableMapping dataTableMapping = new DataTableMapping(sourceTable, dataSetTable);
			this.Add(dataTableMapping);
			return dataTableMapping;
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Array" /> to the end of the collection.</summary>
		/// <param name="values">An <see cref="T:System.Array" /> of values to add to the collection.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007DE RID: 2014 RVA: 0x00025684 File Offset: 0x00023884
		public void AddRange(Array values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				this.Add(values.GetValue(i));
			}
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.Common.DataTableMapping" /> array to the end of the collection.</summary>
		/// <param name="values">The array of <see cref="T:System.Data.Common.DataTableMapping" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007DF RID: 2015 RVA: 0x000256B8 File Offset: 0x000238B8
		public void AddRange(DataTableMapping[] values)
		{
			foreach (DataTableMapping dataTableMapping in values)
			{
				this.Add(dataTableMapping);
			}
		}

		/// <summary>Removes all <see cref="T:System.Data.Common.DataTableMapping" /> objects from the collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060007E0 RID: 2016 RVA: 0x000256E8 File Offset: 0x000238E8
		public void Clear()
		{
			this.sourceTables.Clear();
			this.dataSetTables.Clear();
			this.mappings.Clear();
		}

		/// <summary>Gets a value indicating whether the given <see cref="T:System.Data.Common.DataTableMapping" /> object exists in the collection.</summary>
		/// <returns>true if this collection contains the specified <see cref="T:System.Data.Common.DataTableMapping" />; otherwise false.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataTableMapping" />. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007E1 RID: 2017 RVA: 0x0002570C File Offset: 0x0002390C
		public bool Contains(object value)
		{
			return this.mappings.Contains(value);
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name exists in the collection.</summary>
		/// <returns>true if the collection contains a <see cref="T:System.Data.Common.DataTableMapping" /> object with this source table name; otherwise false.</returns>
		/// <param name="value">The case-sensitive source table name containing the <see cref="T:System.Data.Common.DataTableMapping" /> object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007E2 RID: 2018 RVA: 0x0002571C File Offset: 0x0002391C
		public bool Contains(string value)
		{
			return this.sourceTables.Contains(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataTableMappingCollection" /> to the specified array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> to which to copy the <see cref="T:System.Data.Common.DataTableMappingCollection" /> elements. </param>
		/// <param name="index">The starting index of the array. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007E3 RID: 2019 RVA: 0x0002572C File Offset: 0x0002392C
		public void CopyTo(Array array, int index)
		{
			this.mappings.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataTableMapping" /> to the specified array.</summary>
		/// <param name="array">A <see cref="T:System.Data.Common.DataTableMapping" /> to which to copy the <see cref="T:System.Data.Common.DataTableMappingCollection" /> elements.</param>
		/// <param name="index">The starting index of the array.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007E4 RID: 2020 RVA: 0x0002573C File Offset: 0x0002393C
		public void CopyTo(DataTableMapping[] array, int index)
		{
			this.mappings.CopyTo(array, index);
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> table name.</returns>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> table to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007E5 RID: 2021 RVA: 0x0002574C File Offset: 0x0002394C
		public DataTableMapping GetByDataSetTable(string dataSetTable)
		{
			if (this.dataSetTables[dataSetTable] != null)
			{
				return (DataTableMapping)this.dataSetTables[dataSetTable];
			}
			string text = dataSetTable.ToLower();
			object[] array = new object[this.dataSetTables.Count];
			this.dataSetTables.Keys.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = (string)array[i];
				if (text.Equals(text2.ToLower()))
				{
					return (DataTableMapping)this.dataSetTables[array[i]];
				}
			}
			return null;
		}

		/// <summary>Gets a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source table name and <see cref="T:System.Data.DataSet" /> table name, using the given <see cref="T:System.Data.MissingMappingAction" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DataTableMapping" /> object.</returns>
		/// <param name="tableMappings">The <see cref="T:System.Data.Common.DataTableMappingCollection" /> collection to search. </param>
		/// <param name="sourceTable">The case-sensitive name of the mapped source table. </param>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the mapped <see cref="T:System.Data.DataSet" /> table. </param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="mappingAction" /> parameter was set to Error, and no mapping was specified. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007E6 RID: 2022 RVA: 0x000257EC File Offset: 0x000239EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataTableMapping GetTableMappingBySchemaAction(DataTableMappingCollection tableMappings, string sourceTable, string dataSetTable, MissingMappingAction mappingAction)
		{
			if (tableMappings.Contains(sourceTable))
			{
				return tableMappings[sourceTable];
			}
			if (mappingAction == MissingMappingAction.Error)
			{
				throw new InvalidOperationException(string.Format("Missing source table mapping: '{0}'", sourceTable));
			}
			if (mappingAction == MissingMappingAction.Ignore)
			{
				return null;
			}
			return new DataTableMapping(sourceTable, dataSetTable);
		}

		/// <summary>Gets an enumerator that can iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007E7 RID: 2023 RVA: 0x00025838 File Offset: 0x00023A38
		public IEnumerator GetEnumerator()
		{
			return this.mappings.GetEnumerator();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.Common.DataTableMapping" /> object within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.Common.DataTableMapping" /> object within the collection.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataTableMapping" /> object to find. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007E8 RID: 2024 RVA: 0x00025848 File Offset: 0x00023A48
		public int IndexOf(object value)
		{
			return this.mappings.IndexOf(value);
		}

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</summary>
		/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name.</returns>
		/// <param name="sourceTable">The case-sensitive name of the source table. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007E9 RID: 2025 RVA: 0x00025858 File Offset: 0x00023A58
		public int IndexOf(string sourceTable)
		{
			return this.IndexOf(this.sourceTables[sourceTable]);
		}

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> table name.</summary>
		/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataTableMapping" /> object with the given <see cref="T:System.Data.DataSet" /> table name, or -1 if the <see cref="T:System.Data.Common.DataTableMapping" /> object does not exist in the collection.</returns>
		/// <param name="dataSetTable">The name, which is not case-sensitive, of the DataSet table to find. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060007EA RID: 2026 RVA: 0x0002586C File Offset: 0x00023A6C
		public int IndexOfDataSetTable(string dataSetTable)
		{
			if (this.dataSetTables[dataSetTable] != null)
			{
				return this.IndexOf((DataTableMapping)this.dataSetTables[dataSetTable]);
			}
			string text = dataSetTable.ToLower();
			object[] array = new object[this.dataSetTables.Count];
			this.dataSetTables.Keys.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = (string)array[i];
				if (text.Equals(text2.ToLower()))
				{
					return this.IndexOf((DataTableMapping)this.dataSetTables[array[i]]);
				}
			}
			return -1;
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataTableMapping" /> object into the <see cref="T:System.Data.Common.DataTableMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to insert. </param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to insert. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007EB RID: 2027 RVA: 0x00025918 File Offset: 0x00023B18
		public void Insert(int index, object value)
		{
			this.mappings.Insert(index, value);
			this.sourceTables[((DataTableMapping)value).SourceTable] = value;
			this.dataSetTables[((DataTableMapping)value).DataSetTable] = value;
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataTableMapping" /> object into the <see cref="T:System.Data.Common.DataTableMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to insert.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to insert.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007EC RID: 2028 RVA: 0x00025960 File Offset: 0x00023B60
		public void Insert(int index, DataTableMapping value)
		{
			this.mappings.Insert(index, value);
			this.sourceTables[value.SourceTable] = value;
			this.dataSetTables[value.DataSetTable] = value;
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DataTableMapping" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to remove. </param>
		/// <exception cref="T:System.InvalidCastException">The object specified was not a <see cref="T:System.Data.Common.DataTableMapping" /> object. </exception>
		/// <exception cref="T:System.ArgumentException">The object specified is not in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007ED RID: 2029 RVA: 0x000259A0 File Offset: 0x00023BA0
		public void Remove(object value)
		{
			if (!(value is DataTableMapping))
			{
				throw new InvalidCastException();
			}
			int num = this.mappings.IndexOf(value);
			if (num < 0 || num >= this.mappings.Count)
			{
				throw new ArgumentException("There is no such element in collection.");
			}
			this.mappings.Remove((DataTableMapping)value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DataTableMapping" /> object from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DataTableMapping" /> object to remove.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007EE RID: 2030 RVA: 0x00025A00 File Offset: 0x00023C00
		public void Remove(DataTableMapping value)
		{
			int num = this.mappings.IndexOf(value);
			if (num < 0 || num >= this.mappings.Count)
			{
				throw new ArgumentException("There is no such element in collection.");
			}
			this.mappings.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataTableMapping" /> object located at the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataTableMapping" /> object to remove. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">A <see cref="T:System.Data.Common.DataTableMapping" /> object does not exist with the specified index. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007EF RID: 2031 RVA: 0x00025A4C File Offset: 0x00023C4C
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.mappings.Count)
			{
				throw new IndexOutOfRangeException("There is no element in collection.");
			}
			this.mappings.RemoveAt(index);
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataTableMapping" /> object with the specified source table name from the collection.</summary>
		/// <param name="sourceTable">The case-sensitive source table name to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">A <see cref="T:System.Data.Common.DataTableMapping" /> object does not exist with the specified source table name. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060007F0 RID: 2032 RVA: 0x00025A80 File Offset: 0x00023C80
		public void RemoveAt(string sourceTable)
		{
			this.RemoveAt(this.mappings.IndexOf(this.sourceTables[sourceTable]));
		}

		// Token: 0x040002F8 RID: 760
		private ArrayList mappings;

		// Token: 0x040002F9 RID: 761
		private Hashtable sourceTables;

		// Token: 0x040002FA RID: 762
		private Hashtable dataSetTables;
	}
}
