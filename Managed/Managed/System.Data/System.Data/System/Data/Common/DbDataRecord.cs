using System;
using System.ComponentModel;

namespace System.Data.Common
{
	/// <summary>Implements <see cref="T:System.Data.IDataRecord" /> and <see cref="T:System.ComponentModel.ICustomTypeDescriptor" />, and provides data binding support for <see cref="T:System.Data.Common.DbEnumerator" />.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000C1 RID: 193
	public abstract class DbDataRecord : IDataRecord, ICustomTypeDescriptor
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x0002E314 File Offset: 0x0002C514
		[MonoTODO]
		AttributeCollection ICustomTypeDescriptor.GetAttributes()
		{
			return new AttributeCollection(null);
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x0002E31C File Offset: 0x0002C51C
		[MonoTODO]
		string ICustomTypeDescriptor.GetClassName()
		{
			return string.Empty;
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x0002E324 File Offset: 0x0002C524
		[MonoTODO]
		string ICustomTypeDescriptor.GetComponentName()
		{
			return null;
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002E328 File Offset: 0x0002C528
		[MonoTODO]
		TypeConverter ICustomTypeDescriptor.GetConverter()
		{
			return null;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002E32C File Offset: 0x0002C52C
		[MonoTODO]
		EventDescriptor ICustomTypeDescriptor.GetDefaultEvent()
		{
			return null;
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002E330 File Offset: 0x0002C530
		[MonoTODO]
		PropertyDescriptor ICustomTypeDescriptor.GetDefaultProperty()
		{
			return null;
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0002E334 File Offset: 0x0002C534
		[MonoTODO]
		object ICustomTypeDescriptor.GetEditor(Type editorBaseType)
		{
			return null;
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0002E338 File Offset: 0x0002C538
		[MonoTODO]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents()
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0002E340 File Offset: 0x0002C540
		[MonoTODO]
		EventDescriptorCollection ICustomTypeDescriptor.GetEvents(Attribute[] attributes)
		{
			return new EventDescriptorCollection(null);
		}

		// Token: 0x06000966 RID: 2406 RVA: 0x0002E348 File Offset: 0x0002C548
		[MonoTODO]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties()
		{
			DataColumnPropertyDescriptor[] array = new DataColumnPropertyDescriptor[this.FieldCount];
			for (int i = 0; i < this.FieldCount; i++)
			{
				DataColumnPropertyDescriptor dataColumnPropertyDescriptor = new DataColumnPropertyDescriptor(this.GetName(i), i, null);
				dataColumnPropertyDescriptor.SetComponentType(typeof(DbDataRecord));
				dataColumnPropertyDescriptor.SetPropertyType(this.GetFieldType(i));
				array[i] = dataColumnPropertyDescriptor;
			}
			return new PropertyDescriptorCollection(array);
		}

		// Token: 0x06000967 RID: 2407 RVA: 0x0002E3B0 File Offset: 0x0002C5B0
		[MonoTODO]
		PropertyDescriptorCollection ICustomTypeDescriptor.GetProperties(Attribute[] attributes)
		{
			return ((ICustomTypeDescriptor)this).GetProperties();
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
		[MonoTODO]
		object ICustomTypeDescriptor.GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		/// <summary>Indicates the number of fields within the current record. This property is read-only.</summary>
		/// <returns>The number of fields within the current record.</returns>
		/// <exception cref="T:System.NotSupportedException">Not connected to a data source to read from. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000969 RID: 2409
		public abstract int FieldCount { get; }

		/// <summary>Indicates the value at the specified column in its native format given the column name. This property is read-only.</summary>
		/// <returns>The value at the specified column in its native format.</returns>
		/// <param name="name">The column name. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001AF RID: 431
		public abstract object this[string name] { get; }

		/// <summary>Indicates the value at the specified column in its native format given the column ordinal. This property is read-only.</summary>
		/// <returns>The value at the specified column in its native format.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170001B0 RID: 432
		public abstract object this[int i] { get; }

		/// <summary>Returns the value of the specified column as a Boolean.</summary>
		/// <returns>true if the Boolean is true; otherwise false.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600096C RID: 2412
		public abstract bool GetBoolean(int i);

		/// <summary>Returns the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600096D RID: 2413
		public abstract byte GetByte(int i);

		/// <summary>Returns the value of the specified column as a byte array.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <param name="dataIndex">The index within the field from which to start the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes.</param>
		/// <param name="bufferIndex">The index for <paramref name="buffer" /> to start the read operation.</param>
		/// <param name="length">The number of bytes to read.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600096E RID: 2414
		public abstract long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length);

		/// <summary>Returns the value of the specified column as a character.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600096F RID: 2415
		public abstract char GetChar(int i);

		/// <summary>Returns the value of the specified column as a character array.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">Column ordinal. </param>
		/// <param name="dataIndex">Buffer to copy data into. </param>
		/// <param name="buffer">Maximum length to copy into the buffer. </param>
		/// <param name="bufferIndex">Point to start from within the buffer. </param>
		/// <param name="length">Point to start from within the source data. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000970 RID: 2416
		public abstract long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length);

		/// <summary>Returns the name of the back-end data type.</summary>
		/// <returns>The name of the back-end data type.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000971 RID: 2417
		public abstract string GetDataTypeName(int i);

		/// <summary>Returns a <see cref="T:System.Data.Common.DbDataReader" /> object for the requested column ordinal that can be overridden with a provider-specific implementation.</summary>
		/// <returns>A <see cref="T:System.Data.Common.DbDataReader" /> object.</returns>
		/// <param name="i">The zero-based column ordinal.</param>
		// Token: 0x06000972 RID: 2418
		protected abstract DbDataReader GetDbDataReader(int i);

		/// <summary>Returns the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000973 RID: 2419
		public abstract DateTime GetDateTime(int i);

		/// <summary>Returns the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000974 RID: 2420
		public abstract decimal GetDecimal(int i);

		/// <summary>Returns the value of the specified column as a double-precision floating-point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000975 RID: 2421
		public abstract double GetDouble(int i);

		/// <summary>Returns the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000976 RID: 2422
		public abstract Type GetFieldType(int i);

		/// <summary>Returns the value of the specified column as a single-precision floating-point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000977 RID: 2423
		public abstract float GetFloat(int i);

		/// <summary>Returns the GUID value of the specified field.</summary>
		/// <returns>The GUID value of the specified field.</returns>
		/// <param name="i">The index of the field to return. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000978 RID: 2424
		public abstract Guid GetGuid(int i);

		/// <summary>Returns the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000979 RID: 2425
		public abstract short GetInt16(int i);

		/// <summary>Returns the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600097A RID: 2426
		public abstract int GetInt32(int i);

		/// <summary>Returns the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600097B RID: 2427
		public abstract long GetInt64(int i);

		/// <summary>Returns the name of the specified column.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600097C RID: 2428
		public abstract string GetName(int i);

		/// <summary>Returns the column ordinal, given the name of the column.</summary>
		/// <returns>The column ordinal.</returns>
		/// <param name="name">The name of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600097D RID: 2429
		public abstract int GetOrdinal(string name);

		/// <summary>Returns the value of the specified column as a string.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600097E RID: 2430
		public abstract string GetString(int i);

		/// <summary>Returns the value at the specified column in its native format.</summary>
		/// <returns>The value to return.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600097F RID: 2431
		public abstract object GetValue(int i);

		/// <summary>Populates an array of objects with the column values of the current record.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> to copy the attribute fields into. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000980 RID: 2432
		public abstract int GetValues(object[] values);

		/// <summary>Used to indicate nonexistent values.</summary>
		/// <returns>true if the specified column is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000981 RID: 2433
		public abstract bool IsDBNull(int i);

		/// <summary>Not currently supported.</summary>
		/// <param name="i">
		///   <see cref="T:System.Int32" />
		/// </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000982 RID: 2434 RVA: 0x0002E3CC File Offset: 0x0002C5CC
		public IDataReader GetData(int i)
		{
			return (IDataReader)this.GetValue(i);
		}
	}
}
