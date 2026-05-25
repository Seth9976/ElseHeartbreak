using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Reads a forward-only stream of rows from a data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000C0 RID: 192
	public abstract class DbDataReader : MarshalByRefObject, IDisposable, IDataReader, IDataRecord, IEnumerable
	{
		/// <summary>For a description of this member, see <see cref="M:System.Data.IDataRecord.GetData(System.Int32)" />.</summary>
		/// <returns>An instance of <see cref="T:System.Data.IDataReader" /> to be used when the field points to more remote structured data.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		// Token: 0x06000931 RID: 2353 RVA: 0x0002E08C File Offset: 0x0002C28C
		IDataReader IDataRecord.GetData(int i)
		{
			return ((IDataRecord)this).GetData(i);
		}

		/// <summary>Gets a value indicating the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000932 RID: 2354
		public abstract int Depth { get; }

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>The number of columns in the current row.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to an instance of SQL Server. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000933 RID: 2355
		public abstract int FieldCount { get; }

		/// <summary>Gets a value that indicates whether this <see cref="T:System.Data.Common.DbDataReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbDataReader" /> contains one or more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000934 RID: 2356
		public abstract bool HasRows { get; }

		/// <summary>Gets a value indicating whether the <see cref="T:System.Data.Common.DbDataReader" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Data.Common.DbDataReader" /> is closed; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000935 RID: 2357
		public abstract bool IsClosed { get; }

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AA RID: 426
		public abstract object this[int index] { get; }

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="name">The name of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AB RID: 427
		public abstract object this[string name] { get; }

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement. </summary>
		/// <returns>The number of rows changed, inserted, or deleted. -1 for SELECT statements; 0 if no rows were affected or the statement failed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000938 RID: 2360
		public abstract int RecordsAffected { get; }

		/// <summary>Gets the number of fields in the <see cref="T:System.Data.Common.DbDataReader" /> that are not hidden.</summary>
		/// <returns>The number of fields that are not hidden.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x0002E098 File Offset: 0x0002C298
		public virtual int VisibleFieldCount
		{
			get
			{
				return this.FieldCount;
			}
		}

		/// <summary>Closes the <see cref="T:System.Data.Common.DbDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093A RID: 2362
		public abstract void Close();

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093B RID: 2363
		public abstract bool GetBoolean(int i);

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093C RID: 2364
		public abstract byte GetByte(int i);

		/// <summary>Reads a stream of bytes from the specified column, starting at location indicated by <paramref name="dataOffset" />, into the buffer, starting at the location indicated by <paramref name="bufferOffset" />.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <param name="dataOffset">The index within the row from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to copy the data.</param>
		/// <param name="bufferOffset">The index with the buffer to which the data will be copied.</param>
		/// <param name="length">The maximum number of characters to read.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093D RID: 2365
		public abstract long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length);

		/// <summary>Gets the value of the specified column as a single character.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093E RID: 2366
		public abstract char GetChar(int i);

		/// <summary>Reads a stream of characters from the specified column, starting at location indicated by <paramref name="dataIndex" />, into the buffer, starting at the location indicated by <paramref name="bufferIndex" />.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <param name="dataOffset">The index within the row from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to copy the data.</param>
		/// <param name="bufferOffset">The index with the buffer to which the data will be copied.</param>
		/// <param name="length">The maximum number of characters to read.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600093F RID: 2367
		public abstract long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length);

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Data.Common.DbDataReader" /> class.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000940 RID: 2368 RVA: 0x0002E0A0 File Offset: 0x0002C2A0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public void Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Releases the managed resources used by the <see cref="T:System.Data.Common.DbDataReader" /> and optionally releases the unmanaged resources.</summary>
		/// <param name="disposing">true to release managed and unmanaged resources; false to release only unmanaged resources.</param>
		// Token: 0x06000941 RID: 2369 RVA: 0x0002E0AC File Offset: 0x0002C2AC
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		/// <summary>Returns a <see cref="T:System.Data.Common.DbDataReader" /> object for the requested column ordinal.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000942 RID: 2370 RVA: 0x0002E0BC File Offset: 0x0002C2BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public DbDataReader GetData(int i)
		{
			return (DbDataReader)this[i];
		}

		/// <summary>Gets name of the data type of the specified column.</summary>
		/// <returns>A string representing the name of the data type.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000943 RID: 2371
		public abstract string GetDataTypeName(int i);

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000944 RID: 2372
		public abstract DateTime GetDateTime(int i);

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000945 RID: 2373
		public abstract decimal GetDecimal(int i);

		/// <summary>Gets the value of the specified column as a double-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000946 RID: 2374
		public abstract double GetDouble(int i);

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000947 RID: 2375
		[EditorBrowsable(EditorBrowsableState.Never)]
		public abstract IEnumerator GetEnumerator();

		/// <summary>Gets the data type of the specified column.</summary>
		/// <returns>The data type of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000948 RID: 2376
		public abstract Type GetFieldType(int i);

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000949 RID: 2377
		public abstract float GetFloat(int i);

		/// <summary>Gets the value of the specified column as a globally-unique identifier (GUID).</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600094A RID: 2378
		public abstract Guid GetGuid(int i);

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600094B RID: 2379
		public abstract short GetInt16(int i);

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600094C RID: 2380
		public abstract int GetInt32(int i);

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600094D RID: 2381
		public abstract long GetInt64(int i);

		/// <summary>Gets the name of the column, given the zero-based column ordinal.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600094E RID: 2382
		public abstract string GetName(int i);

		/// <summary>Gets the column ordinal given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="name">The name of the column.</param>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified is not a valid column name.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600094F RID: 2383
		public abstract int GetOrdinal(string name);

		/// <summary>Returns the provider-specific field type of the specified column.</summary>
		/// <returns>The <see cref="T:System.Type" /> object that describes the data type of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000950 RID: 2384 RVA: 0x0002E0CC File Offset: 0x0002C2CC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual Type GetProviderSpecificFieldType(int i)
		{
			return this.GetFieldType(i);
		}

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000951 RID: 2385 RVA: 0x0002E0D8 File Offset: 0x0002C2D8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual object GetProviderSpecificValue(int i)
		{
			return this.GetValue(i);
		}

		/// <summary>Gets all provider-specific attribute columns in the collection for the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000952 RID: 2386 RVA: 0x0002E0E4 File Offset: 0x0002C2E4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual int GetProviderSpecificValues(object[] values)
		{
			return this.GetValues(values);
		}

		/// <summary>Returns a <see cref="T:System.Data.Common.DbDataReader" /> object for the requested column ordinal that can be overridden with a provider-specific implementation.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		// Token: 0x06000953 RID: 2387 RVA: 0x0002E0F0 File Offset: 0x0002C2F0
		protected virtual DbDataReader GetDbDataReader(int ordinal)
		{
			return (DbDataReader)this[ordinal];
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.Common.DbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000954 RID: 2388
		public abstract DataTable GetSchemaTable();

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.String" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000955 RID: 2389
		public abstract string GetString(int i);

		/// <summary>Gets the value of the specified column as an instance of <see cref="T:System.Object" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000956 RID: 2390
		public abstract object GetValue(int i);

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000957 RID: 2391
		public abstract int GetValues(object[] values);

		/// <summary>Gets a value that indicates whether the column contains nonexistent or missing values.</summary>
		/// <returns>true if the specified column is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000958 RID: 2392
		public abstract bool IsDBNull(int i);

		/// <summary>Advances the reader to the next result when reading the results of a batch of statements.</summary>
		/// <returns>true if there are more result sets; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000959 RID: 2393
		public abstract bool NextResult();

		/// <summary>Advances the reader to the next record in a result set.</summary>
		/// <returns>true if there are more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600095A RID: 2394
		public abstract bool Read();

		// Token: 0x0600095B RID: 2395 RVA: 0x0002E100 File Offset: 0x0002C300
		internal static DataTable GetSchemaTableTemplate()
		{
			Type typeFromHandle = typeof(bool);
			Type typeFromHandle2 = typeof(string);
			Type typeFromHandle3 = typeof(int);
			Type typeFromHandle4 = typeof(Type);
			Type typeFromHandle5 = typeof(short);
			return new DataTable("SchemaTable")
			{
				Columns = 
				{
					{ "ColumnName", typeFromHandle2 },
					{ "ColumnOrdinal", typeFromHandle3 },
					{ "ColumnSize", typeFromHandle3 },
					{ "NumericPrecision", typeFromHandle5 },
					{ "NumericScale", typeFromHandle5 },
					{ "IsUnique", typeFromHandle },
					{ "IsKey", typeFromHandle },
					{ "BaseServerName", typeFromHandle2 },
					{ "BaseCatalogName", typeFromHandle2 },
					{ "BaseColumnName", typeFromHandle2 },
					{ "BaseSchemaName", typeFromHandle2 },
					{ "BaseTableName", typeFromHandle2 },
					{ "DataType", typeFromHandle4 },
					{ "AllowDBNull", typeFromHandle },
					{ "ProviderType", typeFromHandle3 },
					{ "IsAliased", typeFromHandle },
					{ "IsExpression", typeFromHandle },
					{ "IsIdentity", typeFromHandle },
					{ "IsAutoIncrement", typeFromHandle },
					{ "IsRowVersion", typeFromHandle },
					{ "IsHidden", typeFromHandle },
					{ "IsLong", typeFromHandle },
					{ "IsReadOnly", typeFromHandle }
				}
			};
		}
	}
}
