using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	/// <summary>Provides a way of reading a forward-only stream of data rows from a data source. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000EE RID: 238
	public sealed class OleDbDataReader : DbDataReader, IDisposable
	{
		// Token: 0x06000B83 RID: 2947 RVA: 0x00032264 File Offset: 0x00030464
		internal OleDbDataReader(OleDbCommand command, ArrayList results)
		{
			this.command = command;
			this.open = true;
			if (results != null)
			{
				this.gdaResults = results;
			}
			else
			{
				this.gdaResults = new ArrayList();
			}
			this.currentResult = -1;
			this.currentRow = -1;
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000322B0 File Offset: 0x000304B0
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		/// <summary>Gets a value that indicates the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000B85 RID: 2949 RVA: 0x000322BC File Offset: 0x000304BC
		public override int Depth
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid recordset, 0; otherwise the number of columns in the current record. The default is -1.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to a data source. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000B86 RID: 2950 RVA: 0x000322C0 File Offset: 0x000304C0
		public override int FieldCount
		{
			get
			{
				if (this.currentResult < 0 || this.currentResult >= this.gdaResults.Count)
				{
					return 0;
				}
				return libgda.gda_data_model_get_n_columns((IntPtr)this.gdaResults[this.currentResult]);
			}
		}

		/// <summary>Indicates whether the data reader is closed.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbDataReader" /> is closed; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000B87 RID: 2951 RVA: 0x0003230C File Offset: 0x0003050C
		public override bool IsClosed
		{
			get
			{
				return !this.open;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="name">The column name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000220 RID: 544
		public override object this[string name]
		{
			get
			{
				if (this.currentResult == -1)
				{
					throw new InvalidOperationException();
				}
				int num = libgda.gda_data_model_get_column_position((IntPtr)this.gdaResults[this.currentResult], name);
				if (num == -1)
				{
					throw new IndexOutOfRangeException();
				}
				return this[num];
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="index">The column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x17000221 RID: 545
		public override object this[int index]
		{
			get
			{
				return this.GetValue(index);
			}
		}

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x00032374 File Offset: 0x00030574
		public override int RecordsAffected
		{
			get
			{
				if (this.currentResult < 0 || this.currentResult >= this.gdaResults.Count)
				{
					return 0;
				}
				int num = libgda.gda_data_model_get_n_rows((IntPtr)this.gdaResults[this.currentResult]);
				if (num > 0 && this.FieldCount > 0)
				{
					return -1;
				}
				return (this.FieldCount <= 0) ? num : (-1);
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.OleDb.OleDbDataReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.OleDb.OleDbDataReader" /> contains one or more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x000323EC File Offset: 0x000305EC
		[MonoTODO]
		public override bool HasRows
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of fields in the <see cref="T:System.Data.OleDb.OleDbDataReader" /> that are not hidden.</summary>
		/// <returns>The number of fields that are not hidden.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x000323F4 File Offset: 0x000305F4
		[MonoTODO]
		public override int VisibleFieldCount
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Closes the <see cref="T:System.Data.OleDb.OleDbDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B8D RID: 2957 RVA: 0x000323FC File Offset: 0x000305FC
		public override void Close()
		{
			for (int i = 0; i < this.gdaResults.Count; i++)
			{
				IntPtr intPtr = (IntPtr)this.gdaResults[i];
				libgda.FreeObject(intPtr);
			}
			this.gdaResults.Clear();
			this.gdaResults = null;
			this.open = false;
			this.currentResult = -1;
			this.currentRow = -1;
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B8E RID: 2958 RVA: 0x00032464 File Offset: 0x00030664
		public override bool GetBoolean(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new InvalidCastException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidCastException();
			}
			if (libgda.gda_value_get_type(intPtr) != GdaValueType.Boolean)
			{
				throw new InvalidCastException();
			}
			return libgda.gda_value_get_boolean(intPtr);
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column as a byte.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B8F RID: 2959 RVA: 0x000324D4 File Offset: 0x000306D4
		public override byte GetByte(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new InvalidCastException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidCastException();
			}
			if (libgda.gda_value_get_type(intPtr) != GdaValueType.Tinyint)
			{
				throw new InvalidCastException();
			}
			return libgda.gda_value_get_tinyint(intPtr);
		}

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer as an array starting at the given buffer offset.</summary>
		/// <returns>The number of bytes read.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B90 RID: 2960 RVA: 0x00032548 File Offset: 0x00030748
		[MonoTODO]
		public override long GetBytes(int ordinal, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the value of the specified column as a character.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000B91 RID: 2961 RVA: 0x00032550 File Offset: 0x00030750
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override char GetChar(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new InvalidCastException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidCastException();
			}
			if (libgda.gda_value_get_type(intPtr) != GdaValueType.Tinyint)
			{
				throw new InvalidCastException();
			}
			return (char)libgda.gda_value_get_tinyint(intPtr);
		}

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array starting at the given buffer offset.</summary>
		/// <returns>The number of characters read.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the row from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to copy data. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The number of characters to read. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B92 RID: 2962 RVA: 0x000325C4 File Offset: 0x000307C4
		[MonoTODO]
		public override long GetChars(int ordinal, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an <see cref="T:System.Data.OleDb.OleDbDataReader" /> object for the requested column ordinal.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataReader" /> object.</returns>
		/// <param name="ordinal">The zero-based column ordinal.</param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B93 RID: 2963 RVA: 0x000325CC File Offset: 0x000307CC
		[MonoTODO]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new OleDbDataReader GetData(int ordinal)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000325D4 File Offset: 0x000307D4
		protected override DbDataReader GetDbDataReader(int ordinal)
		{
			return this.GetData(ordinal);
		}

		/// <summary>Gets the name of the source data type.</summary>
		/// <returns>The name of the back-end data type. For more information, see SQL Server data types or Access data types.</returns>
		/// <param name="index">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000B95 RID: 2965 RVA: 0x000325E0 File Offset: 0x000307E0
		public override string GetDataTypeName(int index)
		{
			if (this.currentResult == -1)
			{
				return "unknown";
			}
			IntPtr intPtr = libgda.gda_data_model_describe_column((IntPtr)this.gdaResults[this.currentResult], index);
			if (intPtr == IntPtr.Zero)
			{
				return "unknown";
			}
			GdaValueType gdaValueType = libgda.gda_field_attributes_get_gdatype(intPtr);
			libgda.gda_field_attributes_free(intPtr);
			return libgda.gda_type_to_string(gdaValueType);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B96 RID: 2966 RVA: 0x00032648 File Offset: 0x00030848
		public override DateTime GetDateTime(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new InvalidCastException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidCastException();
			}
			if (libgda.gda_value_get_type(intPtr) == GdaValueType.Date)
			{
				GdaDate gdaDate = (GdaDate)Marshal.PtrToStructure(libgda.gda_value_get_date(intPtr), typeof(GdaDate));
				return new DateTime((int)gdaDate.year, (int)gdaDate.month, (int)gdaDate.day);
			}
			if (libgda.gda_value_get_type(intPtr) == GdaValueType.Time)
			{
				GdaTime gdaTime = (GdaTime)Marshal.PtrToStructure(libgda.gda_value_get_time(intPtr), typeof(GdaTime));
				return new DateTime(0, 0, 0, (int)gdaTime.hour, (int)gdaTime.minute, (int)gdaTime.second, 0);
			}
			if (libgda.gda_value_get_type(intPtr) == GdaValueType.Timestamp)
			{
				GdaTimestamp gdaTimestamp = (GdaTimestamp)Marshal.PtrToStructure(libgda.gda_value_get_timestamp(intPtr), typeof(GdaTimestamp));
				return new DateTime((int)gdaTimestamp.year, (int)gdaTimestamp.month, (int)gdaTimestamp.day, (int)gdaTimestamp.hour, (int)gdaTimestamp.minute, (int)gdaTimestamp.second, (int)gdaTimestamp.fraction);
			}
			throw new InvalidCastException();
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B97 RID: 2967 RVA: 0x00032784 File Offset: 0x00030984
		[MonoTODO]
		public override decimal GetDecimal(int ordinal)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the value of the specified column as a double-precision floating-point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B98 RID: 2968 RVA: 0x0003278C File Offset: 0x0003098C
		public override double GetDouble(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new InvalidCastException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidCastException();
			}
			if (libgda.gda_value_get_type(intPtr) != GdaValueType.Double)
			{
				throw new InvalidCastException();
			}
			return libgda.gda_value_get_double(intPtr);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="index">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000B99 RID: 2969 RVA: 0x000327FC File Offset: 0x000309FC
		public override Type GetFieldType(int index)
		{
			if (this.currentResult == -1)
			{
				throw new IndexOutOfRangeException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], index, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new IndexOutOfRangeException();
			}
			switch (libgda.gda_value_get_type(intPtr))
			{
			case GdaValueType.Bigint:
				return typeof(long);
			case GdaValueType.Boolean:
				return typeof(bool);
			case GdaValueType.Date:
				return typeof(DateTime);
			case GdaValueType.Double:
				return typeof(double);
			case GdaValueType.Integer:
				return typeof(int);
			case GdaValueType.Single:
				return typeof(float);
			case GdaValueType.Smallint:
				return typeof(byte);
			case GdaValueType.String:
				return typeof(string);
			case GdaValueType.Time:
				return typeof(DateTime);
			case GdaValueType.Timestamp:
				return typeof(DateTime);
			case GdaValueType.Tinyint:
				return typeof(byte);
			}
			return typeof(string);
		}

		/// <summary>Gets the value of the specified column as a single-precision floating-point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B9A RID: 2970 RVA: 0x0003292C File Offset: 0x00030B2C
		public override float GetFloat(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new InvalidCastException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidCastException();
			}
			if (libgda.gda_value_get_type(intPtr) != GdaValueType.Single)
			{
				throw new InvalidCastException();
			}
			return libgda.gda_value_get_single(intPtr);
		}

		/// <summary>Gets the value of the specified column as a globally unique identifier (GUID).</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B9B RID: 2971 RVA: 0x000329A0 File Offset: 0x00030BA0
		[MonoTODO]
		public override Guid GetGuid(int ordinal)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B9C RID: 2972 RVA: 0x000329A8 File Offset: 0x00030BA8
		public override short GetInt16(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new InvalidCastException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidCastException();
			}
			if (libgda.gda_value_get_type(intPtr) != GdaValueType.Smallint)
			{
				throw new InvalidCastException();
			}
			return (short)libgda.gda_value_get_smallint(intPtr);
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B9D RID: 2973 RVA: 0x00032A1C File Offset: 0x00030C1C
		public override int GetInt32(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new InvalidCastException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidCastException();
			}
			if (libgda.gda_value_get_type(intPtr) != GdaValueType.Integer)
			{
				throw new InvalidCastException();
			}
			return libgda.gda_value_get_integer(intPtr);
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B9E RID: 2974 RVA: 0x00032A8C File Offset: 0x00030C8C
		public override long GetInt64(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new InvalidCastException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidCastException();
			}
			if (libgda.gda_value_get_type(intPtr) != GdaValueType.Bigint)
			{
				throw new InvalidCastException();
			}
			return libgda.gda_value_get_bigint(intPtr);
		}

		/// <summary>Gets the name of the specified column.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="index">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000B9F RID: 2975 RVA: 0x00032AFC File Offset: 0x00030CFC
		public override string GetName(int index)
		{
			if (this.currentResult == -1)
			{
				return null;
			}
			return libgda.gda_data_model_get_column_title((IntPtr)this.gdaResults[this.currentResult], index);
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="name">The name of the column. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified is not a valid column name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000BA0 RID: 2976 RVA: 0x00032B34 File Offset: 0x00030D34
		public override int GetOrdinal(string name)
		{
			if (this.currentResult == -1)
			{
				throw new IndexOutOfRangeException();
			}
			for (int i = 0; i < this.FieldCount; i++)
			{
				if (this.GetName(i) == name)
				{
					return i;
				}
			}
			throw new IndexOutOfRangeException();
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.OleDb.OleDbDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.OleDb.OleDbDataReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000BA1 RID: 2977 RVA: 0x00032B84 File Offset: 0x00030D84
		public override DataTable GetSchemaTable()
		{
			DataTable dataTable = null;
			if (this.FieldCount > 0)
			{
				if (this.currentResult == -1)
				{
					return null;
				}
				dataTable = new DataTable();
				dataTable.Columns.Add("ColumnName", typeof(string));
				dataTable.Columns.Add("ColumnOrdinal", typeof(int));
				dataTable.Columns.Add("ColumnSize", typeof(int));
				dataTable.Columns.Add("NumericPrecision", typeof(int));
				dataTable.Columns.Add("NumericScale", typeof(int));
				dataTable.Columns.Add("IsUnique", typeof(bool));
				dataTable.Columns.Add("IsKey", typeof(bool));
				DataColumn dataColumn = dataTable.Columns["IsKey"];
				dataColumn.AllowDBNull = true;
				dataTable.Columns.Add("BaseCatalogName", typeof(string));
				dataTable.Columns.Add("BaseColumnName", typeof(string));
				dataTable.Columns.Add("BaseSchemaName", typeof(string));
				dataTable.Columns.Add("BaseTableName", typeof(string));
				dataTable.Columns.Add("DataType", typeof(Type));
				dataTable.Columns.Add("AllowDBNull", typeof(bool));
				dataTable.Columns.Add("ProviderType", typeof(int));
				dataTable.Columns.Add("IsAliased", typeof(bool));
				dataTable.Columns.Add("IsExpression", typeof(bool));
				dataTable.Columns.Add("IsIdentity", typeof(bool));
				dataTable.Columns.Add("IsAutoIncrement", typeof(bool));
				dataTable.Columns.Add("IsRowVersion", typeof(bool));
				dataTable.Columns.Add("IsHidden", typeof(bool));
				dataTable.Columns.Add("IsLong", typeof(bool));
				dataTable.Columns.Add("IsReadOnly", typeof(bool));
				for (int i = 0; i < this.FieldCount; i++)
				{
					DataRow dataRow = dataTable.NewRow();
					IntPtr intPtr = libgda.gda_data_model_describe_column((IntPtr)this.gdaResults[this.currentResult], i);
					if (intPtr == IntPtr.Zero)
					{
						return null;
					}
					GdaValueType gdaValueType = libgda.gda_field_attributes_get_gdatype(intPtr);
					long num = libgda.gda_field_attributes_get_defined_size(intPtr);
					libgda.gda_field_attributes_free(intPtr);
					dataRow["ColumnName"] = this.GetName(i);
					dataRow["ColumnOrdinal"] = i + 1;
					dataRow["ColumnSize"] = (int)num;
					dataRow["NumericPrecision"] = 0;
					dataRow["NumericScale"] = 0;
					dataRow["IsUnique"] = false;
					dataRow["IsKey"] = DBNull.Value;
					dataRow["BaseCatalogName"] = string.Empty;
					dataRow["BaseColumnName"] = this.GetName(i);
					dataRow["BaseSchemaName"] = string.Empty;
					dataRow["BaseTableName"] = string.Empty;
					dataRow["DataType"] = this.GetFieldType(i);
					dataRow["AllowDBNull"] = false;
					dataRow["ProviderType"] = (int)gdaValueType;
					dataRow["IsAliased"] = false;
					dataRow["IsExpression"] = false;
					dataRow["IsIdentity"] = false;
					dataRow["IsAutoIncrement"] = false;
					dataRow["IsRowVersion"] = false;
					dataRow["IsHidden"] = false;
					dataRow["IsLong"] = false;
					dataRow["IsReadOnly"] = false;
					dataRow.AcceptChanges();
					dataTable.Rows.Add(dataRow);
				}
			}
			return dataTable;
		}

		/// <summary>Gets the value of the specified column as a string.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000BA2 RID: 2978 RVA: 0x00033030 File Offset: 0x00031230
		public override string GetString(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new InvalidCastException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new InvalidCastException();
			}
			if (libgda.gda_value_get_type(intPtr) != GdaValueType.String)
			{
				throw new InvalidCastException();
			}
			return libgda.gda_value_get_string(intPtr);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000BA3 RID: 2979 RVA: 0x000330A4 File Offset: 0x000312A4
		[MonoTODO]
		public TimeSpan GetTimeSpan(int ordinal)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the value of the column at the specified ordinal in its native format.</summary>
		/// <returns>The value to return.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000BA4 RID: 2980 RVA: 0x000330AC File Offset: 0x000312AC
		public override object GetValue(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new IndexOutOfRangeException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new IndexOutOfRangeException();
			}
			switch (libgda.gda_value_get_type(intPtr))
			{
			case GdaValueType.Bigint:
				return this.GetInt64(ordinal);
			case GdaValueType.Boolean:
				return this.GetBoolean(ordinal);
			case GdaValueType.Date:
				return this.GetDateTime(ordinal);
			case GdaValueType.Double:
				return this.GetDouble(ordinal);
			case GdaValueType.Integer:
				return this.GetInt32(ordinal);
			case GdaValueType.Single:
				return this.GetFloat(ordinal);
			case GdaValueType.Smallint:
				return this.GetByte(ordinal);
			case GdaValueType.String:
				return this.GetString(ordinal);
			case GdaValueType.Time:
				return this.GetDateTime(ordinal);
			case GdaValueType.Timestamp:
				return this.GetDateTime(ordinal);
			case GdaValueType.Tinyint:
				return this.GetByte(ordinal);
			}
			return libgda.gda_value_stringify(intPtr);
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000BA5 RID: 2981 RVA: 0x000331E8 File Offset: 0x000313E8
		[MonoTODO]
		public override int GetValues(object[] values)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
		// Token: 0x06000BA6 RID: 2982 RVA: 0x000331F0 File Offset: 0x000313F0
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this);
		}

		/// <summary>Gets a value that indicates whether the column contains nonexistent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise, false.</returns>
		/// <param name="ordinal">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000BA7 RID: 2983 RVA: 0x000331F8 File Offset: 0x000313F8
		public override bool IsDBNull(int ordinal)
		{
			if (this.currentResult == -1)
			{
				throw new IndexOutOfRangeException();
			}
			IntPtr intPtr = libgda.gda_data_model_get_value_at((IntPtr)this.gdaResults[this.currentResult], ordinal, this.currentRow);
			if (intPtr == IntPtr.Zero)
			{
				throw new IndexOutOfRangeException();
			}
			return libgda.gda_value_is_null(intPtr);
		}

		/// <summary>Advances the data reader to the next result, when reading the results of batch SQL statements.</summary>
		/// <returns>true if there are more result sets; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000BA8 RID: 2984 RVA: 0x00033258 File Offset: 0x00031458
		public override bool NextResult()
		{
			int num = this.currentResult + 1;
			if (num >= 0 && num < this.gdaResults.Count)
			{
				this.currentResult++;
				return true;
			}
			return false;
		}

		/// <summary>Advances the <see cref="T:System.Data.OleDb.OleDbDataReader" /> to the next record.</summary>
		/// <returns>true if there are more rows; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000BA9 RID: 2985 RVA: 0x00033298 File Offset: 0x00031498
		public override bool Read()
		{
			if (this.currentResult < 0 || this.currentResult >= this.gdaResults.Count)
			{
				return false;
			}
			this.currentRow++;
			return this.currentRow < libgda.gda_data_model_get_n_rows((IntPtr)this.gdaResults[this.currentResult]);
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x00033300 File Offset: 0x00031500
		private new void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.command = null;
					GC.SuppressFinalize(this);
				}
				if (this.gdaResults != null)
				{
					this.gdaResults.Clear();
					this.gdaResults = null;
				}
				if (this.open)
				{
					this.Close();
				}
				this.disposed = true;
			}
		}

		// Token: 0x04000431 RID: 1073
		private OleDbCommand command;

		// Token: 0x04000432 RID: 1074
		private bool open;

		// Token: 0x04000433 RID: 1075
		private ArrayList gdaResults;

		// Token: 0x04000434 RID: 1076
		private int currentResult;

		// Token: 0x04000435 RID: 1077
		private int currentRow;

		// Token: 0x04000436 RID: 1078
		private bool disposed;
	}
}
