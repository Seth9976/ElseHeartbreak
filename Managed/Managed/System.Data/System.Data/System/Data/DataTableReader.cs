using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace System.Data
{
	/// <summary>The <see cref="T:System.Data.DataTableReader" /> obtains the contents of one or more <see cref="T:System.Data.DataTable" /> objects in the form of one or more read-only, forward-only result sets. </summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000039 RID: 57
	public sealed class DataTableReader : DbDataReader
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableReader" /> class by using data from the supplied <see cref="T:System.Data.DataTable" />.</summary>
		/// <param name="dataTable">The <see cref="T:System.Data.DataTable" /> from which the new <see cref="T:System.Data.DataTableReader" /> obtains its result set. </param>
		// Token: 0x0600042D RID: 1069 RVA: 0x00019E9C File Offset: 0x0001809C
		public DataTableReader(DataTable dt)
			: this(new DataTable[] { dt })
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.DataTableReader" /> class using the supplied array of <see cref="T:System.Data.DataTable" /> objects.</summary>
		/// <param name="dataTables">The array of <see cref="T:System.Data.DataTable" /> objects that supplies the results for the new <see cref="T:System.Data.DataTableReader" /> object. </param>
		// Token: 0x0600042E RID: 1070 RVA: 0x00019EB0 File Offset: 0x000180B0
		public DataTableReader(DataTable[] dataTables)
		{
			if (dataTables == null || dataTables.Length <= 0)
			{
				throw new ArgumentException("Cannot Create DataTable. Argument Empty!");
			}
			this._tables = new DataTable[dataTables.Length];
			for (int i = 0; i < dataTables.Length; i++)
			{
				this._tables[i] = dataTables[i];
			}
			this._closed = false;
			this._index = 0;
			this._current = -1;
			this._rowRef = null;
			this._tableCleared = false;
			this.SubscribeEvents();
		}

		/// <summary>The depth of nesting for the current row of the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>The depth of nesting for the current row; always zero.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00019F3C File Offset: 0x0001813C
		public override int Depth
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Returns the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid result set, 0; otherwise the number of columns in the current row. </returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to retrieve the field count in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x00019F40 File Offset: 0x00018140
		public override int FieldCount
		{
			get
			{
				return this.CurrentTable.Columns.Count;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataTableReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.DataTableReader" /> contains one or more rows; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to retrieve information about a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x00019F54 File Offset: 0x00018154
		public override bool HasRows
		{
			get
			{
				return this.CurrentTable.Rows.Count > 0;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.DataTableReader" /> is closed.</summary>
		/// <returns>Returns true if the <see cref="T:System.Data.DataTableReader" /> is closed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000432 RID: 1074 RVA: 0x00019F74 File Offset: 0x00018174
		public override bool IsClosed
		{
			get
			{
				return this._closed;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A6 RID: 166
		public override object this[int index]
		{
			get
			{
				this.Validate();
				if (index < 0 || index >= this.FieldCount)
				{
					throw new ArgumentOutOfRangeException("index " + index + " is not in the range");
				}
				DataRow currentRow = this.CurrentRow;
				if (currentRow.RowState == DataRowState.Deleted)
				{
					throw new InvalidOperationException("Deleted Row's information cannot be accessed!");
				}
				return currentRow[index];
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x00019FE4 File Offset: 0x000181E4
		private DataTable CurrentTable
		{
			get
			{
				return this._tables[this._index];
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000435 RID: 1077 RVA: 0x00019FF4 File Offset: 0x000181F4
		private DataRow CurrentRow
		{
			get
			{
				return this.CurrentTable.Rows[this._current];
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="name">The name of the column. </param>
		/// <exception cref="T:System.ArgumentException">The name specified is not a valid column name. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000A9 RID: 169
		public override object this[string name]
		{
			get
			{
				this.Validate();
				DataRow currentRow = this.CurrentRow;
				if (currentRow.RowState == DataRowState.Deleted)
				{
					throw new InvalidOperationException("Deleted Row's information cannot be accessed!");
				}
				return currentRow[name];
			}
		}

		/// <summary>Gets the number of rows inserted, changed, or deleted by execution of the SQL statement.</summary>
		/// <returns>The <see cref="T:System.Data.DataTableReader" /> does not support this property and always returns 0.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0001A044 File Offset: 0x00018244
		public override int RecordsAffected
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x0001A048 File Offset: 0x00018248
		private void SubscribeEvents()
		{
			if (this._subscribed)
			{
				return;
			}
			this.CurrentTable.TableCleared += this.OnTableCleared;
			this.CurrentTable.RowChanged += this.OnRowChanged;
			this.CurrentTable.Columns.CollectionChanged += this.OnColumnCollectionChanged;
			for (int i = 0; i < this.CurrentTable.Columns.Count; i++)
			{
				this.CurrentTable.Columns[i].PropertyChanged += this.OnColumnChanged;
			}
			this._subscribed = true;
			this._schemaChanged = false;
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x0001A0FC File Offset: 0x000182FC
		private void UnsubscribeEvents()
		{
			if (!this._subscribed)
			{
				return;
			}
			this.CurrentTable.TableCleared -= this.OnTableCleared;
			this.CurrentTable.RowChanged -= this.OnRowChanged;
			this.CurrentTable.Columns.CollectionChanged -= this.OnColumnCollectionChanged;
			for (int i = 0; i < this.CurrentTable.Columns.Count; i++)
			{
				this.CurrentTable.Columns[i].PropertyChanged -= this.OnColumnChanged;
			}
			this._subscribed = false;
			this._schemaChanged = false;
		}

		/// <summary>Closes the current <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600043A RID: 1082 RVA: 0x0001A1B0 File Offset: 0x000183B0
		public override void Close()
		{
			if (this.IsClosed)
			{
				return;
			}
			this.UnsubscribeEvents();
			this._closed = true;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Boolean" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a Boolean. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600043B RID: 1083 RVA: 0x0001A1CC File Offset: 0x000183CC
		public override bool GetBoolean(int i)
		{
			return (bool)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a byte. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600043C RID: 1084 RVA: 0x0001A1DC File Offset: 0x000183DC
		public override byte GetByte(int i)
		{
			return (byte)this.GetValue(i);
		}

		/// <summary>Reads a stream of bytes starting at the specified column offset into the buffer as an array starting at the specified buffer offset.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the buffer at which to start placing the data. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a byte array. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600043D RID: 1085 RVA: 0x0001A1EC File Offset: 0x000183EC
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			byte[] array = this[i] as byte[];
			if (array == null)
			{
				this.ThrowInvalidCastException(this[i].GetType(), typeof(byte[]));
			}
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = ((length <= array.Length) ? length : array.Length);
			Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num);
			return (long)num;
		}

		/// <summary>Gets the value of the specified column as a character.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified field does not contain a character. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600043E RID: 1086 RVA: 0x0001A258 File Offset: 0x00018458
		public override char GetChar(int i)
		{
			return (char)this.GetValue(i);
		}

		/// <summary>Returns the value of the specified column as a character array.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of chars. </param>
		/// <param name="bufferIndex">The index within the buffer at which to start placing the data. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a character array. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600043F RID: 1087 RVA: 0x0001A268 File Offset: 0x00018468
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			char[] array = this[i] as char[];
			if (array == null)
			{
				this.ThrowInvalidCastException(this[i].GetType(), typeof(char[]));
			}
			if (buffer == null)
			{
				return (long)array.Length;
			}
			int num = ((length <= array.Length) ? length : array.Length);
			Array.Copy(array, dataIndex, buffer, (long)bufferIndex, (long)num);
			return (long)num;
		}

		/// <summary>Gets a string representing the data type of the specified column.</summary>
		/// <returns>A string representing the column's data type.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000440 RID: 1088 RVA: 0x0001A2D4 File Offset: 0x000184D4
		public override string GetDataTypeName(int i)
		{
			return this.GetFieldType(i).ToString();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a DateTime value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000441 RID: 1089 RVA: 0x0001A2E4 File Offset: 0x000184E4
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" />.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a Decimal value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000442 RID: 1090 RVA: 0x0001A2F4 File Offset: 0x000184F4
		public override decimal GetDecimal(int i)
		{
			return (decimal)this.GetValue(i);
		}

		/// <summary>Gets the value of the column as a double-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based ordinal of the column. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed DataTableReader.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a double-precision floating point number. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000443 RID: 1091 RVA: 0x0001A304 File Offset: 0x00018504
		public override double GetDouble(int i)
		{
			return (double)this.GetValue(i);
		}

		/// <summary>Returns an enumerator that can be used to iterate through the item collection. </summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> object that represents the item collection.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000444 RID: 1092 RVA: 0x0001A314 File Offset: 0x00018514
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this);
		}

		/// <summary>Gets the type of the specified column in provider-specific format.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000445 RID: 1093 RVA: 0x0001A31C File Offset: 0x0001851C
		public override Type GetProviderSpecificFieldType(int i)
		{
			return this.GetFieldType(i);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000446 RID: 1094 RVA: 0x0001A328 File Offset: 0x00018528
		public override Type GetFieldType(int i)
		{
			this.ValidateClosed();
			return this.CurrentTable.Columns[i].DataType;
		}

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a single-precision floating point number. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000447 RID: 1095 RVA: 0x0001A354 File Offset: 0x00018554
		public override float GetFloat(int i)
		{
			return (float)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a globally-unique identifier (GUID).</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a GUID. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000448 RID: 1096 RVA: 0x0001A364 File Offset: 0x00018564
		public override Guid GetGuid(int i)
		{
			return (Guid)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 16-bit signed integer. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000449 RID: 1097 RVA: 0x0001A374 File Offset: 0x00018574
		public override short GetInt16(int i)
		{
			return (short)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 32-bit signed integer value. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600044A RID: 1098 RVA: 0x0001A384 File Offset: 0x00018584
		public override int GetInt32(int i)
		{
			return (int)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a 64-bit signed integer value. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600044B RID: 1099 RVA: 0x0001A394 File Offset: 0x00018594
		public override long GetInt64(int i)
		{
			return (long)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.String" />.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600044C RID: 1100 RVA: 0x0001A3A4 File Offset: 0x000185A4
		public override string GetName(int i)
		{
			this.ValidateClosed();
			return this.CurrentTable.Columns[i].ColumnName;
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="name">The name of the column. </param>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.ArgumentException">The name specified is not a valid column name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600044D RID: 1101 RVA: 0x0001A3D0 File Offset: 0x000185D0
		public override int GetOrdinal(string name)
		{
			this.ValidateClosed();
			int num = this.CurrentTable.Columns.IndexOf(name);
			if (num == -1)
			{
				throw new ArgumentException(string.Format("Column {0} is not found in the schema", name));
			}
			return num;
		}

		/// <summary>Gets the value of the specified column in provider-specific format.</summary>
		/// <returns>The value of the specified column in provider-specific format.</returns>
		/// <param name="ordinal">The zero-based number of the column whose value is retrieved. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /></exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600044E RID: 1102 RVA: 0x0001A410 File Offset: 0x00018610
		public override object GetProviderSpecificValue(int i)
		{
			return this.GetValue(i);
		}

		/// <summary>Fills the supplied array with provider-specific type information for all the columns in the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>The number of column values copied into the array.</returns>
		/// <param name="values">An array of objects to be filled in with type information for the columns in the <see cref="T:System.Data.DataTableReader" />. </param>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600044F RID: 1103 RVA: 0x0001A41C File Offset: 0x0001861C
		public override int GetProviderSpecificValues(object[] values)
		{
			return this.GetValues(values);
		}

		/// <summary>Gets the value of the specified column as a string.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The specified column does not contain a string. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000450 RID: 1104 RVA: 0x0001A428 File Offset: 0x00018628
		public override string GetString(int i)
		{
			return (string)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column in its native format.</summary>
		/// <returns>The value of the specified column. This method returns DBNull for null columns.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access columns in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000451 RID: 1105 RVA: 0x0001A438 File Offset: 0x00018638
		public override object GetValue(int i)
		{
			return this[i];
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of column values copied into the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the column values from the <see cref="T:System.Data.DataTableReader" />.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1. </exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000452 RID: 1106 RVA: 0x0001A444 File Offset: 0x00018644
		public override int GetValues(object[] values)
		{
			this.Validate();
			if (this.CurrentRow.RowState == DataRowState.Deleted)
			{
				throw new DeletedRowInaccessibleException(string.Empty);
			}
			int num = ((this.FieldCount >= values.Length) ? values.Length : this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.CurrentRow[i];
			}
			return num;
		}

		/// <summary>Gets a value that indicates whether the column contains non-existent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise, false.</returns>
		/// <param name="ordinal">The zero-based column ordinal </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1.</exception>
		/// <exception cref="T:System.Data.DeletedRowInaccessibleException">An attempt was made to retrieve data from a deleted row.</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000453 RID: 1107 RVA: 0x0001A4B4 File Offset: 0x000186B4
		public override bool IsDBNull(int i)
		{
			return this.GetValue(i) is DBNull;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.DataTableReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.DataTableReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000454 RID: 1108 RVA: 0x0001A4C8 File Offset: 0x000186C8
		public override DataTable GetSchemaTable()
		{
			this.ValidateClosed();
			this.ValidateSchemaIntact();
			if (this._schemaTable != null)
			{
				return this._schemaTable;
			}
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("ColumnName", typeof(string));
			dataTable.Columns.Add("ColumnOrdinal", typeof(int));
			dataTable.Columns.Add("ColumnSize", typeof(int));
			dataTable.Columns.Add("NumericPrecision", typeof(short));
			dataTable.Columns.Add("NumericScale", typeof(short));
			dataTable.Columns.Add("DataType", typeof(Type));
			dataTable.Columns.Add("ProviderType", typeof(int));
			dataTable.Columns.Add("IsLong", typeof(bool));
			dataTable.Columns.Add("AllowDBNull", typeof(bool));
			dataTable.Columns.Add("IsReadOnly", typeof(bool));
			dataTable.Columns.Add("IsRowVersion", typeof(bool));
			dataTable.Columns.Add("IsUnique", typeof(bool));
			dataTable.Columns.Add("IsKey", typeof(bool));
			dataTable.Columns.Add("IsAutoIncrement", typeof(bool));
			dataTable.Columns.Add("BaseCatalogName", typeof(string));
			dataTable.Columns.Add("BaseSchemaName", typeof(string));
			dataTable.Columns.Add("BaseTableName", typeof(string));
			dataTable.Columns.Add("BaseColumnName", typeof(string));
			dataTable.Columns.Add("AutoIncrementSeed", typeof(long));
			dataTable.Columns.Add("AutoIncrementStep", typeof(long));
			dataTable.Columns.Add("DefaultValue", typeof(object));
			dataTable.Columns.Add("Expression", typeof(string));
			dataTable.Columns.Add("ColumnMapping", typeof(MappingType));
			dataTable.Columns.Add("BaseTableNamespace", typeof(string));
			dataTable.Columns.Add("BaseColumnNamespace", typeof(string));
			for (int i = 0; i < this.CurrentTable.Columns.Count; i++)
			{
				DataRow dataRow = dataTable.NewRow();
				DataColumn dataColumn = this.CurrentTable.Columns[i];
				dataRow["ColumnName"] = dataColumn.ColumnName;
				dataRow["BaseColumnName"] = dataColumn.ColumnName;
				dataRow["ColumnOrdinal"] = dataColumn.Ordinal;
				dataRow["ColumnSize"] = dataColumn.MaxLength;
				dataRow["NumericPrecision"] = DBNull.Value;
				dataRow["NumericScale"] = DBNull.Value;
				dataRow["DataType"] = dataColumn.DataType;
				dataRow["ProviderType"] = DBNull.Value;
				dataRow["IsLong"] = false;
				dataRow["AllowDBNull"] = dataColumn.AllowDBNull;
				dataRow["IsReadOnly"] = dataColumn.ReadOnly;
				dataRow["IsRowVersion"] = false;
				dataRow["IsUnique"] = dataColumn.Unique;
				dataRow["IsKey"] = Array.IndexOf<DataColumn>(this.CurrentTable.PrimaryKey, dataColumn) != -1;
				dataRow["IsAutoIncrement"] = dataColumn.AutoIncrement;
				dataRow["AutoIncrementSeed"] = dataColumn.AutoIncrementSeed;
				dataRow["AutoIncrementStep"] = dataColumn.AutoIncrementStep;
				dataRow["BaseCatalogName"] = ((this.CurrentTable.DataSet == null) ? null : this.CurrentTable.DataSet.DataSetName);
				dataRow["BaseSchemaName"] = DBNull.Value;
				dataRow["BaseTableName"] = this.CurrentTable.TableName;
				dataRow["DefaultValue"] = dataColumn.DefaultValue;
				if (dataColumn.Expression == string.Empty)
				{
					dataRow["Expression"] = dataColumn.Expression;
				}
				else
				{
					Regex regex = new Regex("((Parent|Child)( )*[.(])", RegexOptions.IgnoreCase);
					if (regex.IsMatch(dataColumn.Expression, 0))
					{
						dataRow["Expression"] = DBNull.Value;
					}
					else
					{
						dataRow["Expression"] = dataColumn.Expression;
					}
				}
				dataRow["ColumnMapping"] = dataColumn.ColumnMapping;
				dataRow["BaseTableNamespace"] = this.CurrentTable.Namespace;
				dataRow["BaseColumnNamespace"] = dataColumn.Namespace;
				dataTable.Rows.Add(dataRow);
			}
			return this._schemaTable = dataTable;
		}

		// Token: 0x06000455 RID: 1109 RVA: 0x0001AA60 File Offset: 0x00018C60
		private void Validate()
		{
			this.ValidateClosed();
			if (this._index >= this._tables.Length)
			{
				throw new InvalidOperationException("Invalid attempt to read when no data is present");
			}
			if (this._tableCleared)
			{
				throw new RowNotInTableException("The table is cleared, no rows are accessible");
			}
			if (this._current == -1)
			{
				throw new InvalidOperationException("DataReader is invalid for the DataTable");
			}
			this.ValidateSchemaIntact();
		}

		// Token: 0x06000456 RID: 1110 RVA: 0x0001AAC4 File Offset: 0x00018CC4
		private void ValidateClosed()
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("Invalid attempt to read when the reader is closed");
			}
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x0001AADC File Offset: 0x00018CDC
		private void ValidateSchemaIntact()
		{
			if (this._schemaChanged)
			{
				throw new InvalidOperationException("Schema of current DataTable '" + this.CurrentTable.TableName + "' in DataTableReader has changed, DataTableReader is invalid.");
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x0001AB0C File Offset: 0x00018D0C
		private void ThrowInvalidCastException(Type sourceType, Type destType)
		{
			throw new InvalidCastException(string.Format("Unable to cast object of type '{0}' to type '{1}'.", sourceType, destType));
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x0001AB20 File Offset: 0x00018D20
		private bool MoveNext()
		{
			if (this._index >= this._tables.Length || this._tableCleared)
			{
				return false;
			}
			do
			{
				this._current++;
			}
			while (this._current < this.CurrentTable.Rows.Count && this.CurrentRow.RowState == DataRowState.Deleted);
			this._rowRef = ((this._current >= this.CurrentTable.Rows.Count) ? null : this.CurrentRow);
			return this._current < this.CurrentTable.Rows.Count;
		}

		/// <summary>Advances the <see cref="T:System.Data.DataTableReader" /> to the next result set, if any.</summary>
		/// <returns>true if there was another result set; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to navigate within a closed <see cref="T:System.Data.DataTableReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600045A RID: 1114 RVA: 0x0001ABCC File Offset: 0x00018DCC
		public override bool NextResult()
		{
			if (this._index + 1 >= this._tables.Length)
			{
				this.UnsubscribeEvents();
				this._index = this._tables.Length;
				return false;
			}
			this.UnsubscribeEvents();
			this._index++;
			this._current = -1;
			this._rowRef = null;
			this._schemaTable = null;
			this._tableCleared = false;
			this.SubscribeEvents();
			return true;
		}

		/// <summary>Advances the <see cref="T:System.Data.DataTableReader" /> to the next record.</summary>
		/// <returns>true if there was another row to read; otherwise false.</returns>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access a column in a closed <see cref="T:System.Data.DataTableReader" /> .</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600045B RID: 1115 RVA: 0x0001AC3C File Offset: 0x00018E3C
		public override bool Read()
		{
			this.ValidateClosed();
			return this.MoveNext();
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x0001AC4C File Offset: 0x00018E4C
		private void OnColumnChanged(object sender, PropertyChangedEventArgs args)
		{
			this._schemaChanged = true;
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x0001AC58 File Offset: 0x00018E58
		private void OnColumnCollectionChanged(object sender, CollectionChangeEventArgs args)
		{
			this._schemaChanged = true;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x0001AC64 File Offset: 0x00018E64
		private void OnRowChanged(object src, DataRowChangeEventArgs args)
		{
			DataRowAction action = args.Action;
			DataRow row = args.Row;
			if (action == DataRowAction.Add)
			{
				if (this._tableCleared && this._current != -1)
				{
					return;
				}
				if (this._current == -1 || (this._current >= 0 && row.RowID > this.CurrentRow.RowID))
				{
					this._tableCleared = false;
					return;
				}
				this._current++;
				this._rowRef = this.CurrentRow;
			}
			if (action == DataRowAction.Commit && row.RowState == DataRowState.Detached)
			{
				if (this._rowRef == row)
				{
					this._current--;
					this._rowRef = ((this._current < 0) ? null : this.CurrentRow);
				}
				if (this._current >= this.CurrentTable.Rows.Count)
				{
					this._current--;
					this._rowRef = ((this._current < 0) ? null : this.CurrentRow);
					return;
				}
				if (this._current > 0 && this._rowRef == this.CurrentTable.Rows[this._current - 1])
				{
					this._current--;
					this._rowRef = this.CurrentRow;
					return;
				}
			}
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x0001ADCC File Offset: 0x00018FCC
		private void OnTableCleared(object src, DataTableClearEventArgs args)
		{
			this._tableCleared = true;
		}

		// Token: 0x04000163 RID: 355
		private bool _closed;

		// Token: 0x04000164 RID: 356
		private DataTable[] _tables;

		// Token: 0x04000165 RID: 357
		private int _current = -1;

		// Token: 0x04000166 RID: 358
		private int _index;

		// Token: 0x04000167 RID: 359
		private DataTable _schemaTable;

		// Token: 0x04000168 RID: 360
		private bool _tableCleared;

		// Token: 0x04000169 RID: 361
		private bool _subscribed;

		// Token: 0x0400016A RID: 362
		private DataRow _rowRef;

		// Token: 0x0400016B RID: 363
		private bool _schemaChanged;
	}
}
