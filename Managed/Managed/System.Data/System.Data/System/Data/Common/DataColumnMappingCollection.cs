using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Contains a collection of <see cref="T:System.Data.Common.DataColumnMapping" /> objects.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200009A RID: 154
	public sealed class DataColumnMappingCollection : MarshalByRefObject, IList, IColumnMappingCollection, IEnumerable, ICollection
	{
		/// <summary>Creates an empty <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</summary>
		// Token: 0x06000701 RID: 1793 RVA: 0x00023958 File Offset: 0x00021B58
		public DataColumnMappingCollection()
		{
			this.list = new ArrayList();
			this.sourceColumns = new Hashtable();
			this.dataSetColumns = new Hashtable();
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000702 RID: 1794 RVA: 0x00023984 File Offset: 0x00021B84
		object ICollection.SyncRoot
		{
			get
			{
				return this.list.SyncRoot;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000703 RID: 1795 RVA: 0x00023994 File Offset: 0x00021B94
		bool ICollection.IsSynchronized
		{
			get
			{
				return this.list.IsSynchronized;
			}
		}

		// Token: 0x17000149 RID: 329
		object IColumnMappingCollection.this[string index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is DataColumnMapping))
				{
					throw new ArgumentException();
				}
				this[index] = (DataColumnMapping)value;
			}
		}

		// Token: 0x1700014A RID: 330
		object IList.this[int index]
		{
			get
			{
				return this[index];
			}
			set
			{
				if (!(value is DataColumnMapping))
				{
					throw new ArgumentException();
				}
				this[index] = (DataColumnMapping)value;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x000239FC File Offset: 0x00021BFC
		bool IList.IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x00023A00 File Offset: 0x00021C00
		bool IList.IsFixedSize
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x00023A04 File Offset: 0x00021C04
		IColumnMapping IColumnMappingCollection.Add(string sourceColumnName, string dataSetColumnName)
		{
			return this.Add(sourceColumnName, dataSetColumnName);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x00023A10 File Offset: 0x00021C10
		IColumnMapping IColumnMappingCollection.GetByDataSetColumn(string dataSetColumnName)
		{
			return this.GetByDataSetColumn(dataSetColumnName);
		}

		/// <summary>Gets the number of <see cref="T:System.Data.Common.DataColumnMapping" /> objects in the collection.</summary>
		/// <returns>The number of items in the collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600070C RID: 1804 RVA: 0x00023A1C File Offset: 0x00021C1C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataColumnMapping" /> object at the specified index.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700014E RID: 334
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public DataColumnMapping this[int index]
		{
			get
			{
				return (DataColumnMapping)this.list[index];
			}
			set
			{
				DataColumnMapping dataColumnMapping = (DataColumnMapping)this.list[index];
				this.sourceColumns[dataColumnMapping] = value;
				this.dataSetColumns[dataColumnMapping] = value;
				this.list[index] = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name.</returns>
		/// <param name="sourceColumn">The case-sensitive name of the source column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700014F RID: 335
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DataColumnMapping this[string sourceColumn]
		{
			get
			{
				if (!this.Contains(sourceColumn))
				{
					throw new IndexOutOfRangeException("DataColumnMappingCollection doesn't contain DataColumnMapping with SourceColumn '" + sourceColumn + "'.");
				}
				return (DataColumnMapping)this.sourceColumns[sourceColumn];
			}
			set
			{
				this[this.list.IndexOf(this.sourceColumns[sourceColumn])] = value;
			}
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the collection.</summary>
		/// <returns>The index of the DataColumnMapping object that was added to the collection.</returns>
		/// <param name="value">A DataColumnMapping object to add to the collection. </param>
		/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000711 RID: 1809 RVA: 0x00023AE0 File Offset: 0x00021CE0
		public int Add(object value)
		{
			if (!(value is DataColumnMapping))
			{
				throw new InvalidCastException();
			}
			this.list.Add(value);
			this.sourceColumns[((DataColumnMapping)value).SourceColumn] = value;
			this.dataSetColumns[((DataColumnMapping)value).DataSetColumn] = value;
			return this.list.IndexOf(value);
		}

		/// <summary>Adds a <see cref="T:System.Data.Common.DataColumnMapping" /> object to the collection when given a source column name and a <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <returns>The DataColumnMapping object that was added to the collection.</returns>
		/// <param name="sourceColumn">The case-sensitive name of the source column to map to. </param>
		/// <param name="dataSetColumn">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to map to. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000712 RID: 1810 RVA: 0x00023B48 File Offset: 0x00021D48
		public DataColumnMapping Add(string sourceColumn, string dataSetColumn)
		{
			DataColumnMapping dataColumnMapping = new DataColumnMapping(sourceColumn, dataSetColumn);
			this.Add(dataColumnMapping);
			return dataColumnMapping;
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Array" /> to the end of the collection.</summary>
		/// <param name="values">The <see cref="T:System.Array" /> to add to the collection.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000713 RID: 1811 RVA: 0x00023B68 File Offset: 0x00021D68
		public void AddRange(Array values)
		{
			for (int i = 0; i < values.Length; i++)
			{
				this.Add(values.GetValue(i));
			}
		}

		/// <summary>Copies the elements of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> array to the end of the collection.</summary>
		/// <param name="values">The array of <see cref="T:System.Data.Common.DataColumnMapping" /> objects to add to the collection. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000714 RID: 1812 RVA: 0x00023B9C File Offset: 0x00021D9C
		public void AddRange(DataColumnMapping[] values)
		{
			foreach (DataColumnMapping dataColumnMapping in values)
			{
				this.Add(dataColumnMapping);
			}
		}

		/// <summary>Removes all <see cref="T:System.Data.Common.DataColumnMapping" /> objects from the collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000715 RID: 1813 RVA: 0x00023BCC File Offset: 0x00021DCC
		public void Clear()
		{
			this.list.Clear();
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the given <see cref="T:System.Object" /> exists in the collection.</summary>
		/// <returns>true if the collection contains the specified <see cref="T:System.Data.Common.DataColumnMapping" /> object; otherwise, false.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" />. </param>
		/// <exception cref="T:System.InvalidCastException">The object passed in was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000716 RID: 1814 RVA: 0x00023BDC File Offset: 0x00021DDC
		public bool Contains(object value)
		{
			if (!(value is DataColumnMapping))
			{
				throw new InvalidCastException("Object is not of type DataColumnMapping");
			}
			return this.list.Contains(value);
		}

		/// <summary>Gets a value indicating whether a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the given source column name exists in the collection.</summary>
		/// <returns>true if collection contains a <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name; otherwise, false.</returns>
		/// <param name="value">The case-sensitive source column name of the <see cref="T:System.Data.Common.DataColumnMapping" /> object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000717 RID: 1815 RVA: 0x00023C0C File Offset: 0x00021E0C
		public bool Contains(string value)
		{
			return this.sourceColumns.Contains(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> to the specified array.</summary>
		/// <param name="array">An <see cref="T:System.Array" /> to which to copy <see cref="T:System.Data.Common.DataColumnMappingCollection" /> elements. </param>
		/// <param name="index">The starting index of the array. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000718 RID: 1816 RVA: 0x00023C1C File Offset: 0x00021E1C
		public void CopyTo(Array array, int index)
		{
			this.list.CopyTo(array, index);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> to the specified <see cref="T:System.Data.Common.DataColumnMapping" /> array.</summary>
		/// <param name="array">A <see cref="T:System.Data.Common.DataColumnMapping" /> array to which to copy the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> elements.</param>
		/// <param name="index">The zero-based index in the <paramref name="array" /> at which copying begins.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000719 RID: 1817 RVA: 0x00023C2C File Offset: 0x00021E2C
		public void CopyTo(DataColumnMapping[] arr, int index)
		{
			this.list.CopyTo(arr, index);
		}

		/// <summary>Gets the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <returns>The <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified <see cref="T:System.Data.DataSet" /> column name.</returns>
		/// <param name="value">The name, which is not case-sensitive, of the <see cref="T:System.Data.DataSet" /> column to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600071A RID: 1818 RVA: 0x00023C3C File Offset: 0x00021E3C
		public DataColumnMapping GetByDataSetColumn(string value)
		{
			if (this.dataSetColumns[value] != null)
			{
				return (DataColumnMapping)this.dataSetColumns[value];
			}
			string text = value.ToLower();
			object[] array = new object[this.dataSetColumns.Count];
			this.dataSetColumns.Keys.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = (string)array[i];
				if (text.Equals(text2.ToLower()))
				{
					return (DataColumnMapping)this.dataSetColumns[array[i]];
				}
			}
			return null;
		}

		/// <summary>Gets a <see cref="T:System.Data.Common.DataColumnMapping" /> for the specified <see cref="T:System.Data.Common.DataColumnMappingCollection" />, source column name, and <see cref="T:System.Data.MissingMappingAction" />.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DataColumnMapping" /> object.</returns>
		/// <param name="columnMappings">The <see cref="T:System.Data.Common.DataColumnMappingCollection" />. </param>
		/// <param name="sourceColumn">The case-sensitive source column name to find. </param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="mappingAction" /> parameter was set to Error, and no mapping was specified. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600071B RID: 1819 RVA: 0x00023CDC File Offset: 0x00021EDC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumnMapping GetColumnMappingBySchemaAction(DataColumnMappingCollection columnMappings, string sourceColumn, MissingMappingAction mappingAction)
		{
			if (columnMappings.Contains(sourceColumn))
			{
				return columnMappings[sourceColumn];
			}
			if (mappingAction == MissingMappingAction.Ignore)
			{
				return null;
			}
			if (mappingAction == MissingMappingAction.Error)
			{
				throw new InvalidOperationException(string.Format("Missing SourceColumn mapping for '{0}'", sourceColumn));
			}
			return new DataColumnMapping(sourceColumn, sourceColumn);
		}

		/// <summary>A static method that returns a <see cref="T:System.Data.DataColumn" /> object without instantiating a <see cref="T:System.Data.Common.DataColumnMappingCollection" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.DataColumn" /> object.</returns>
		/// <param name="columnMappings">The <see cref="T:System.Data.Common.DataColumnMappingCollection" />.</param>
		/// <param name="sourceColumn">The case-sensitive column name from a data source.</param>
		/// <param name="dataType">The data type for the column being mapped.</param>
		/// <param name="dataTable">An instance of <see cref="T:System.Data.DataTable" />.</param>
		/// <param name="mappingAction">One of the <see cref="T:System.Data.MissingMappingAction" /> values.</param>
		/// <param name="schemaAction">Determines the action to take when the existing <see cref="T:System.Data.DataSet" /> schema does not match incoming data.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600071C RID: 1820 RVA: 0x00023D28 File Offset: 0x00021F28
		[MonoTODO]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public static DataColumn GetDataColumn(DataColumnMappingCollection columnMappings, string sourceColumn, Type dataType, DataTable dataTable, MissingMappingAction mappingAction, MissingSchemaAction schemaAction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets an enumerator that can iterate through the collection.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the collection.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600071D RID: 1821 RVA: 0x00023D30 File Offset: 0x00021F30
		public IEnumerator GetEnumerator()
		{
			return this.list.GetEnumerator();
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> within the collection.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> within the collection.</returns>
		/// <param name="value">An <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" /> to find. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600071E RID: 1822 RVA: 0x00023D40 File Offset: 0x00021F40
		public int IndexOf(object value)
		{
			return this.list.IndexOf(value);
		}

		/// <summary>Gets the location of the <see cref="T:System.Data.Common.DataColumnMapping" /> with the specified source column name.</summary>
		/// <returns>The zero-based location of the <see cref="T:System.Data.Common.DataColumnMapping" /> with the specified case-sensitive source column name.</returns>
		/// <param name="sourceColumn">The case-sensitive name of the source column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600071F RID: 1823 RVA: 0x00023D50 File Offset: 0x00021F50
		public int IndexOf(string sourceColumn)
		{
			return this.list.IndexOf(this.sourceColumns[sourceColumn]);
		}

		/// <summary>Gets the location of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> with the given <see cref="T:System.Data.DataSet" /> column name.</summary>
		/// <returns>The zero-based location of the specified <see cref="T:System.Data.Common.DataColumnMapping" /> with the given DataSet column name, or -1 if the DataColumnMapping object does not exist in the collection.</returns>
		/// <param name="dataSetColumn">The name, which is not case-sensitive, of the data set column to find. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000720 RID: 1824 RVA: 0x00023D6C File Offset: 0x00021F6C
		public int IndexOfDataSetColumn(string dataSetColumn)
		{
			if (this.dataSetColumns[dataSetColumn] != null)
			{
				return this.list.IndexOf(this.dataSetColumns[dataSetColumn]);
			}
			string text = dataSetColumn.ToLower();
			object[] array = new object[this.dataSetColumns.Count];
			this.dataSetColumns.Keys.CopyTo(array, 0);
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = (string)array[i];
				if (text.Equals(text2.ToLower()))
				{
					return this.list.IndexOf(this.dataSetColumns[array[i]]);
				}
			}
			return -1;
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataColumnMapping" /> object into the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to insert. </param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> object. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000721 RID: 1825 RVA: 0x00023E18 File Offset: 0x00022018
		public void Insert(int index, object value)
		{
			this.list.Insert(index, value);
			this.sourceColumns[((DataColumnMapping)value).SourceColumn] = value;
			this.dataSetColumns[((DataColumnMapping)value).DataSetColumn] = value;
		}

		/// <summary>Inserts a <see cref="T:System.Data.Common.DataColumnMapping" /> object into the <see cref="T:System.Data.Common.DataColumnMappingCollection" /> at the specified index.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to insert.</param>
		/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> object.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000722 RID: 1826 RVA: 0x00023E60 File Offset: 0x00022060
		public void Insert(int index, DataColumnMapping mapping)
		{
			this.list.Insert(index, mapping);
			this.sourceColumns[mapping.SourceColumn] = mapping;
			this.dataSetColumns[mapping.DataSetColumn] = mapping;
		}

		/// <summary>Removes the <see cref="T:System.Object" /> that is a <see cref="T:System.Data.Common.DataColumnMapping" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Object" /> that is the <see cref="T:System.Data.Common.DataColumnMapping" /> to remove. </param>
		/// <exception cref="T:System.InvalidCastException">The object specified was not a <see cref="T:System.Data.Common.DataColumnMapping" /> object. </exception>
		/// <exception cref="T:System.ArgumentException">The object specified is not in the collection. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000723 RID: 1827 RVA: 0x00023EA0 File Offset: 0x000220A0
		public void Remove(object value)
		{
			int num = this.list.IndexOf(value);
			this.sourceColumns.Remove(((DataColumnMapping)value).SourceColumn);
			this.dataSetColumns.Remove(((DataColumnMapping)value).DataSetColumn);
			if (num < 0 || num >= this.list.Count)
			{
				throw new ArgumentException("There is no such element in collection.");
			}
			this.list.Remove(value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.Common.DataColumnMapping" /> from the collection.</summary>
		/// <param name="value">The <see cref="T:System.Data.Common.DataColumnMapping" /> to remove.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000724 RID: 1828 RVA: 0x00023F18 File Offset: 0x00022118
		public void Remove(DataColumnMapping value)
		{
			int num = this.list.IndexOf(value);
			this.sourceColumns.Remove(value.SourceColumn);
			this.dataSetColumns.Remove(value.DataSetColumn);
			if (num < 0 || num >= this.list.Count)
			{
				throw new ArgumentException("There is no such element in collection.");
			}
			this.list.Remove(value);
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.Common.DataColumnMapping" /> object to remove. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified index. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000725 RID: 1829 RVA: 0x00023F84 File Offset: 0x00022184
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.list.Count)
			{
				throw new IndexOutOfRangeException("There is no element in collection.");
			}
			this.Remove(this.list[index]);
		}

		/// <summary>Removes the <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name from the collection.</summary>
		/// <param name="sourceColumn">The case-sensitive source column name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">There is no <see cref="T:System.Data.Common.DataColumnMapping" /> object with the specified source column name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000726 RID: 1830 RVA: 0x00023FBC File Offset: 0x000221BC
		public void RemoveAt(string sourceColumn)
		{
			this.RemoveAt(this.list.IndexOf(this.sourceColumns[sourceColumn]));
		}

		// Token: 0x040002E3 RID: 739
		private readonly ArrayList list;

		// Token: 0x040002E4 RID: 740
		private readonly Hashtable sourceColumns;

		// Token: 0x040002E5 RID: 741
		private readonly Hashtable dataSetColumns;
	}
}
