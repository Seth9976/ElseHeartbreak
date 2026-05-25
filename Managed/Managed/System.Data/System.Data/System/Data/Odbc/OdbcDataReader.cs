using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Text;

namespace System.Data.Odbc
{
	/// <summary>Provides a way of reading a forward-only stream of data rows from a data source. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000121 RID: 289
	public sealed class OdbcDataReader : DbDataReader
	{
		// Token: 0x06001033 RID: 4147 RVA: 0x0003F0F4 File Offset: 0x0003D2F4
		internal OdbcDataReader(OdbcCommand command, CommandBehavior behavior)
		{
			this.command = command;
			this.CommandBehavior = behavior;
			this.open = true;
			this.currentRow = -1;
			this.hstmt = command.hStmt;
			short num = 0;
			libodbc.SQLNumResultCols(this.hstmt, ref num);
			this.cols = new OdbcColumn[(int)num];
			this.GetColumns();
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0003F158 File Offset: 0x0003D358
		internal OdbcDataReader(OdbcCommand command, CommandBehavior behavior, int recordAffected)
			: this(command, behavior)
		{
			this._recordsAffected = recordAffected;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06001035 RID: 4149 RVA: 0x0003F16C File Offset: 0x0003D36C
		// (set) Token: 0x06001036 RID: 4150 RVA: 0x0003F174 File Offset: 0x0003D374
		private CommandBehavior CommandBehavior
		{
			get
			{
				return this.behavior;
			}
			set
			{
				this.behavior = value;
			}
		}

		/// <summary>Gets a value that indicates the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002B7 RID: 695
		// (get) Token: 0x06001037 RID: 4151 RVA: 0x0003F180 File Offset: 0x0003D380
		public override int Depth
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid record set, 0; otherwise the number of columns in the current record. The default is -1.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to a data source. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06001038 RID: 4152 RVA: 0x0003F184 File Offset: 0x0003D384
		public override int FieldCount
		{
			get
			{
				if (this.IsClosed)
				{
					throw new InvalidOperationException("The reader is closed.");
				}
				return this.cols.Length;
			}
		}

		/// <summary>Indicates whether the <see cref="T:System.Data.Odbc.OdbcDataReader" /> is closed.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcDataReader" /> is closed; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06001039 RID: 4153 RVA: 0x0003F1A4 File Offset: 0x0003D3A4
		public override bool IsClosed
		{
			get
			{
				return !this.open;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="value">The column name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002BA RID: 698
		public override object this[string value]
		{
			get
			{
				int ordinal = this.GetOrdinal(value);
				return this[ordinal];
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="i">The column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002BB RID: 699
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted. -1 for SELECT statements; 0 if no rows were affected, or the statement failed.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x0600103C RID: 4156 RVA: 0x0003F1D8 File Offset: 0x0003D3D8
		public override int RecordsAffected
		{
			get
			{
				return this._recordsAffected;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.Odbc.OdbcDataReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.Odbc.OdbcDataReader" /> contains one or more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002BD RID: 701
		// (get) Token: 0x0600103D RID: 4157 RVA: 0x0003F1E0 File Offset: 0x0003D3E0
		[MonoTODO]
		public override bool HasRows
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x0600103E RID: 4158 RVA: 0x0003F1E8 File Offset: 0x0003D3E8
		private OdbcConnection Connection
		{
			get
			{
				if (this.command != null)
				{
					return this.command.Connection;
				}
				return null;
			}
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0003F204 File Offset: 0x0003D404
		private int ColIndex(string colname)
		{
			int num = 0;
			foreach (OdbcColumn odbcColumn in this.cols)
			{
				if (odbcColumn != null)
				{
					if (odbcColumn.ColumnName == colname)
					{
						return num;
					}
					if (string.Compare(odbcColumn.ColumnName, colname, true) == 0)
					{
						return num;
					}
				}
				num++;
			}
			return -1;
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0003F264 File Offset: 0x0003D464
		private OdbcColumn GetColumn(int ordinal)
		{
			if (this.cols[ordinal] == null)
			{
				short num = 255;
				byte[] array = new byte[(int)num];
				short num2 = 0;
				uint num3 = 0U;
				short num4 = 0;
				short num5 = 0;
				short num6 = 0;
				OdbcReturn odbcReturn = libodbc.SQLDescribeCol(this.hstmt, Convert.ToUInt16(ordinal + 1), array, num, ref num2, ref num6, ref num3, ref num4, ref num5);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
				}
				string text = OdbcDataReader.RemoveTrailingNullChar(Encoding.Unicode.GetString(array));
				OdbcColumn odbcColumn = new OdbcColumn(text, (SQL_TYPE)num6);
				odbcColumn.AllowDBNull = num5 != 0;
				odbcColumn.Digits = (int)num4;
				if (odbcColumn.IsVariableSizeType)
				{
					odbcColumn.MaxLength = (int)num3;
				}
				this.cols[ordinal] = odbcColumn;
			}
			return this.cols[ordinal];
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0003F338 File Offset: 0x0003D538
		private void GetColumns()
		{
			for (int i = 0; i < this.cols.Length; i++)
			{
				this.GetColumn(i);
			}
		}

		/// <summary>Closes the <see cref="T:System.Data.Odbc.OdbcDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001042 RID: 4162 RVA: 0x0003F368 File Offset: 0x0003D568
		public override void Close()
		{
			this.open = false;
			this.currentRow = -1;
			this.command.FreeIfNotPrepared();
			if ((this.CommandBehavior & CommandBehavior.CloseConnection) == CommandBehavior.CloseConnection)
			{
				this.command.Connection.Close();
			}
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>A Boolean that is the value of the column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001043 RID: 4163 RVA: 0x0003F3A4 File Offset: 0x0003D5A4
		public override bool GetBoolean(int i)
		{
			return (bool)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column as a byte.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001044 RID: 4164 RVA: 0x0003F3B4 File Offset: 0x0003D5B4
		public override byte GetByte(int i)
		{
			return Convert.ToByte(this.GetValue(i));
		}

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the particular buffer offset.</summary>
		/// <returns>The number of bytes read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field where the read operation is to start. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The number of bytes to read. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001045 RID: 4165 RVA: 0x0003F3C4 File Offset: 0x0003D5C4
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("Reader is not open.");
			}
			if (this.currentRow == -1)
			{
				throw new InvalidOperationException("No data available.");
			}
			int num = 0;
			int num2 = 0;
			byte[] array = new byte[length + 1];
			if (buffer == null)
			{
				length = 0;
			}
			OdbcReturn odbcReturn = libodbc.SQLGetData(this.hstmt, (ushort)(i + 1), SQL_C_TYPE.BINARY, array, length, ref num2);
			if (odbcReturn == OdbcReturn.NoData)
			{
				return 0L;
			}
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
			}
			OdbcException ex = null;
			if (odbcReturn == OdbcReturn.SuccessWithInfo)
			{
				ex = this.Connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
			}
			if (buffer == null)
			{
				return (long)num2;
			}
			bool flag;
			if (odbcReturn == OdbcReturn.SuccessWithInfo)
			{
				if (num2 == -4)
				{
					flag = true;
				}
				else if (num2 == -1)
				{
					flag = false;
					num = -1;
				}
				else
				{
					string sqlstate = ex.Errors[0].SQLState;
					if (sqlstate != "01004")
					{
						throw ex;
					}
					flag = true;
				}
			}
			else
			{
				flag = num2 != -1;
				num = num2;
			}
			if (flag)
			{
				if (num2 == -4)
				{
					int num3 = 0;
					while (array[num3] != 0)
					{
						buffer[bufferIndex + num3] = array[num3];
						num3++;
					}
					num = num3;
				}
				else
				{
					int num4 = Math.Min(num2, length);
					for (int j = 0; j < num4; j++)
					{
						buffer[bufferIndex + j] = array[j];
					}
					num = num4;
				}
			}
			return (long)num;
		}

		/// <summary>Gets the value of the specified column as a character.</summary>
		/// <returns>The value of the specified column as a character.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001046 RID: 4166 RVA: 0x0003F558 File Offset: 0x0003D758
		[MonoTODO]
		public override char GetChar(int i)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array, starting at the particular buffer offset.</summary>
		/// <returns>The number of characters read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the row where the read operation is to start. </param>
		/// <param name="buffer">The buffer into which to copy data. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The number of characters to read. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001047 RID: 4167 RVA: 0x0003F560 File Offset: 0x0003D760
		[MonoTODO]
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("The reader is closed.");
			}
			if (this.currentRow == -1)
			{
				throw new InvalidOperationException("No data available.");
			}
			if (i < 0 || i >= this.FieldCount)
			{
				throw new IndexOutOfRangeException();
			}
			throw new NotImplementedException();
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0003F5B8 File Offset: 0x0003D7B8
		[MonoTODO]
		[EditorBrowsable(EditorBrowsableState.Never)]
		private new IDataReader GetData(int i)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the name of the source data type.</summary>
		/// <returns>The name of the source data type.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001049 RID: 4169 RVA: 0x0003F5C0 File Offset: 0x0003D7C0
		public override string GetDataTypeName(int i)
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("The reader is closed.");
			}
			if (i < 0 || i >= this.FieldCount)
			{
				throw new IndexOutOfRangeException();
			}
			return this.GetColumnAttributeStr(i + 1, FieldIdentifier.TypeName);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600104A RID: 4170 RVA: 0x0003F608 File Offset: 0x0003D808
		public DateTime GetDate(int i)
		{
			return this.GetDateTime(i);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.DateTime" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600104B RID: 4171 RVA: 0x0003F614 File Offset: 0x0003D814
		public override DateTime GetDateTime(int i)
		{
			return (DateTime)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.Decimal" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600104C RID: 4172 RVA: 0x0003F624 File Offset: 0x0003D824
		public override decimal GetDecimal(int i)
		{
			return (decimal)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a double-precision floating-point number.</summary>
		/// <returns>The value of the specified column as a double-precision floating-point number.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600104D RID: 4173 RVA: 0x0003F634 File Offset: 0x0003D834
		public override double GetDouble(int i)
		{
			return (double)this.GetValue(i);
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600104E RID: 4174 RVA: 0x0003F644 File Offset: 0x0003D844
		public override Type GetFieldType(int i)
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("The reader is closed.");
			}
			return this.GetColumn(i).DataType;
		}

		/// <summary>Gets the value of the specified column as a single-precision floating-point number.</summary>
		/// <returns>The value of the specified column as a single-precision floating-point number.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600104F RID: 4175 RVA: 0x0003F674 File Offset: 0x0003D874
		public override float GetFloat(int i)
		{
			return (float)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a globally unique identifier (GUID).</summary>
		/// <returns>The value of the specified column as a GUID.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001050 RID: 4176 RVA: 0x0003F684 File Offset: 0x0003D884
		[MonoTODO]
		public override Guid GetGuid(int i)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column as a 16-bit signed integer.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001051 RID: 4177 RVA: 0x0003F68C File Offset: 0x0003D88C
		public override short GetInt16(int i)
		{
			return (short)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column as a 32-bit signed integer.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001052 RID: 4178 RVA: 0x0003F69C File Offset: 0x0003D89C
		public override int GetInt32(int i)
		{
			return (int)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column as a 64-bit signed integer.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001053 RID: 4179 RVA: 0x0003F6AC File Offset: 0x0003D8AC
		public override long GetInt64(int i)
		{
			return (long)this.GetValue(i);
		}

		/// <summary>Gets the name of the specified column.</summary>
		/// <returns>A string that is the name of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001054 RID: 4180 RVA: 0x0003F6BC File Offset: 0x0003D8BC
		public override string GetName(int i)
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("The reader is closed.");
			}
			return this.GetColumn(i).ColumnName;
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="value">The name of the column. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001055 RID: 4181 RVA: 0x0003F6EC File Offset: 0x0003D8EC
		public override int GetOrdinal(string value)
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("The reader is closed.");
			}
			if (value == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			int num = this.ColIndex(value);
			if (num == -1)
			{
				throw new IndexOutOfRangeException();
			}
			return num;
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.Odbc.OdbcDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.Odbc.OdbcDataReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06001056 RID: 4182 RVA: 0x0003F738 File Offset: 0x0003D938
		[MonoTODO]
		public override DataTable GetSchemaTable()
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("The reader is closed.");
			}
			if (this._dataTableSchema != null)
			{
				return this._dataTableSchema;
			}
			DataTable dataTable = null;
			if (this.cols.Length > 0)
			{
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
				for (int i = 0; i < this.cols.Length; i++)
				{
					OdbcColumn column = this.GetColumn(i);
					DataRow dataRow = dataTable.NewRow();
					dataTable.Rows.Add(dataRow);
					dataRow["ColumnName"] = column.ColumnName;
					dataRow["ColumnOrdinal"] = i;
					dataRow["ColumnSize"] = column.MaxLength;
					dataRow["NumericPrecision"] = this.GetColumnAttribute(i + 1, FieldIdentifier.Precision);
					dataRow["NumericScale"] = this.GetColumnAttribute(i + 1, FieldIdentifier.Scale);
					dataRow["BaseTableName"] = this.GetColumnAttributeStr(i + 1, FieldIdentifier.TableName);
					dataRow["BaseSchemaName"] = this.GetColumnAttributeStr(i + 1, FieldIdentifier.SchemaName);
					dataRow["BaseCatalogName"] = this.GetColumnAttributeStr(i + 1, FieldIdentifier.CatelogName);
					dataRow["BaseColumnName"] = this.GetColumnAttributeStr(i + 1, FieldIdentifier.BaseColumnName);
					dataRow["DataType"] = column.DataType;
					dataRow["IsUnique"] = false;
					dataRow["IsKey"] = DBNull.Value;
					dataRow["AllowDBNull"] = this.GetColumnAttribute(i + 1, FieldIdentifier.Nullable) != 0;
					dataRow["ProviderType"] = (int)column.OdbcType;
					dataRow["IsAutoIncrement"] = this.GetColumnAttribute(i + 1, FieldIdentifier.AutoUniqueValue) == 1;
					dataRow["IsExpression"] = dataRow.IsNull("BaseTableName") || (string)dataRow["BaseTableName"] == string.Empty;
					dataRow["IsAliased"] = (string)dataRow["BaseColumnName"] != (string)dataRow["ColumnName"];
					dataRow["IsReadOnly"] = (bool)dataRow["IsExpression"] || this.GetColumnAttribute(i + 1, FieldIdentifier.Updatable) == 0;
					dataRow["IsIdentity"] = false;
					dataRow["IsRowVersion"] = false;
					dataRow["IsHidden"] = false;
					dataRow["IsLong"] = false;
				}
				DataRow[] array = dataTable.Select("BaseTableName <> ''", "BaseCatalogName, BaseSchemaName, BaseTableName ASC");
				string text = string.Empty;
				string text2 = string.Empty;
				string text3 = string.Empty;
				string[] array2 = null;
				foreach (DataRow dataRow2 in array)
				{
					string text4 = (string)dataRow2["BaseTableName"];
					string text5 = (string)dataRow2["BaseSchemaName"];
					string text6 = (string)dataRow2["BaseCatalogName"];
					if (text4 != text || text5 != text2 || text6 != text3)
					{
						array2 = this.GetPrimaryKeys(text6, text5, text4);
					}
					if (array2 != null && Array.BinarySearch<string>(array2, (string)dataRow2["BaseColumnName"]) >= 0)
					{
						dataRow2["IsKey"] = true;
						dataRow2["IsUnique"] = true;
						dataRow2["AllowDBNull"] = false;
						this.GetColumn(this.ColIndex((string)dataRow2["ColumnName"])).AllowDBNull = false;
					}
					text = text4;
					text2 = text5;
					text3 = text6;
				}
				dataTable.AcceptChanges();
			}
			return this._dataTableSchema = dataTable;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.String" />.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.String" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001057 RID: 4183 RVA: 0x0003FDC4 File Offset: 0x0003DFC4
		public override string GetString(int i)
		{
			object value = this.GetValue(i);
			if (value != null && value.GetType() != typeof(string))
			{
				return Convert.ToString(value);
			}
			return (string)this.GetValue(i);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.TimeSpan" /> object.</summary>
		/// <returns>The value of the specified column as a <see cref="T:System.TimeSpan" /> object.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001058 RID: 4184 RVA: 0x0003FE08 File Offset: 0x0003E008
		[MonoTODO]
		public TimeSpan GetTime(int i)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the value of the column at the specified ordinal in its native format.</summary>
		/// <returns>The value to return.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001059 RID: 4185 RVA: 0x0003FE10 File Offset: 0x0003E010
		public override object GetValue(int i)
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("The reader is closed.");
			}
			if (this.currentRow == -1)
			{
				throw new InvalidOperationException("No data available.");
			}
			if (i > this.cols.Length - 1 || i < 0)
			{
				throw new IndexOutOfRangeException();
			}
			int num = 0;
			OdbcColumn column = this.GetColumn(i);
			object obj = null;
			ushort num2 = Convert.ToUInt16(i + 1);
			if (column.Value == null)
			{
				OdbcReturn odbcReturn;
				int num4;
				byte[] array;
				switch (column.OdbcType)
				{
				case OdbcType.BigInt:
				{
					long num3 = 0L;
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, column.SqlCType, ref num3, 0, ref num);
					obj = num3;
					goto IL_0698;
				}
				case OdbcType.Binary:
				{
					num4 = column.MaxLength;
					array = new byte[num4];
					long bytes = this.GetBytes(i, 0L, array, 0, num4);
					odbcReturn = OdbcReturn.Success;
					obj = array;
					goto IL_0698;
				}
				case OdbcType.Bit:
				{
					short num5 = 0;
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, column.SqlCType, ref num5, 0, ref num);
					if (num != -1)
					{
						obj = ((num5 != 0) ? "True" : "False");
					}
					goto IL_0698;
				}
				case OdbcType.DateTime:
				case OdbcType.Timestamp:
				case OdbcType.Date:
				case OdbcType.Time:
				{
					OdbcTimestamp odbcTimestamp = default(OdbcTimestamp);
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, column.SqlCType, ref odbcTimestamp, 0, ref num);
					if (num != -1)
					{
						if (column.OdbcType == OdbcType.Time)
						{
							obj = new TimeSpan((int)odbcTimestamp.year, (int)odbcTimestamp.month, (int)odbcTimestamp.day);
						}
						else
						{
							obj = new DateTime((int)odbcTimestamp.year, (int)odbcTimestamp.month, (int)odbcTimestamp.day, (int)odbcTimestamp.hour, (int)odbcTimestamp.minute, (int)odbcTimestamp.second);
							if (odbcTimestamp.fraction != 0UL)
							{
								obj = ((DateTime)obj).AddTicks((long)(odbcTimestamp.fraction / 100UL));
							}
						}
					}
					goto IL_0698;
				}
				case OdbcType.Decimal:
				case OdbcType.Numeric:
					num4 = 50;
					array = new byte[num4];
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, SQL_C_TYPE.CHAR, array, num4, ref num);
					if (num != -1)
					{
						byte[] array2 = new byte[num];
						for (int j = 0; j < num; j++)
						{
							array2[j] = array[j];
						}
						obj = decimal.Parse(Encoding.Default.GetString(array2), CultureInfo.InvariantCulture);
					}
					goto IL_0698;
				case OdbcType.Double:
				{
					double num6 = 0.0;
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, column.SqlCType, ref num6, 0, ref num);
					obj = num6;
					goto IL_0698;
				}
				case OdbcType.Image:
				case OdbcType.VarBinary:
				{
					num4 = ((column.MaxLength >= 255 || column.MaxLength <= 0) ? 255 : column.MaxLength);
					array = new byte[num4];
					ArrayList arrayList = new ArrayList();
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, SQL_C_TYPE.BINARY, array, 0, ref num);
					if (num != -1)
					{
						do
						{
							odbcReturn = libodbc.SQLGetData(this.hstmt, num2, SQL_C_TYPE.BINARY, array, num4, ref num);
							if (odbcReturn == OdbcReturn.Error)
							{
								break;
							}
							if (odbcReturn == OdbcReturn.NoData || num == -1)
							{
								break;
							}
							if (num < num4)
							{
								byte[] array3 = new byte[num];
								Array.Copy(array, 0, array3, 0, num);
								arrayList.AddRange(array3);
							}
							else
							{
								arrayList.AddRange(array);
							}
						}
						while (odbcReturn != OdbcReturn.NoData);
					}
					obj = arrayList.ToArray(typeof(byte));
					goto IL_0698;
				}
				case OdbcType.Int:
				{
					int num7 = 0;
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, column.SqlCType, ref num7, 0, ref num);
					obj = num7;
					goto IL_0698;
				}
				case OdbcType.NChar:
					num4 = 255;
					array = new byte[num4];
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, SQL_C_TYPE.WCHAR, array, num4, ref num);
					if (num != -1 && (odbcReturn != OdbcReturn.SuccessWithInfo || num != -4))
					{
						obj = Encoding.Unicode.GetString(array, 0, num);
					}
					goto IL_0698;
				case OdbcType.NText:
				case OdbcType.NVarChar:
				{
					num4 = ((column.MaxLength >= 127) ? 255 : (column.MaxLength * 2 + 1));
					array = new byte[num4];
					StringBuilder stringBuilder = new StringBuilder();
					do
					{
						odbcReturn = libodbc.SQLGetData(this.hstmt, num2, column.SqlCType, array, num4, ref num);
						if (odbcReturn == OdbcReturn.Error)
						{
							break;
						}
						if (odbcReturn == OdbcReturn.Success && num == -1)
						{
							odbcReturn = OdbcReturn.NoData;
						}
						if (odbcReturn != OdbcReturn.NoData && num > 0)
						{
							string text;
							if (num < num4)
							{
								text = Encoding.Unicode.GetString(array, 0, num);
							}
							else
							{
								text = Encoding.Unicode.GetString(array, 0, num4);
							}
							stringBuilder.Append(OdbcDataReader.RemoveTrailingNullChar(text));
						}
					}
					while (odbcReturn != OdbcReturn.NoData);
					obj = stringBuilder.ToString();
					goto IL_0698;
				}
				case OdbcType.Real:
				{
					float num8 = 0f;
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, column.SqlCType, ref num8, 0, ref num);
					obj = num8;
					goto IL_0698;
				}
				case OdbcType.SmallInt:
				{
					short num9 = 0;
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, column.SqlCType, ref num9, 0, ref num);
					obj = num9;
					goto IL_0698;
				}
				case OdbcType.Text:
				case OdbcType.VarChar:
				{
					num4 = ((column.MaxLength >= 255) ? 255 : (column.MaxLength + 1));
					array = new byte[num4];
					StringBuilder stringBuilder2 = new StringBuilder();
					do
					{
						odbcReturn = libodbc.SQLGetData(this.hstmt, num2, column.SqlCType, array, num4, ref num);
						if (odbcReturn == OdbcReturn.Error)
						{
							break;
						}
						if (odbcReturn == OdbcReturn.Success && num == -1)
						{
							odbcReturn = OdbcReturn.NoData;
						}
						if (odbcReturn != OdbcReturn.NoData && num > 0)
						{
							if (num < num4)
							{
								stringBuilder2.Append(Encoding.Default.GetString(array, 0, num));
							}
							else
							{
								stringBuilder2.Append(Encoding.Default.GetString(array, 0, num4 - 1));
							}
						}
					}
					while (odbcReturn != OdbcReturn.NoData);
					obj = stringBuilder2.ToString();
					goto IL_0698;
				}
				case OdbcType.TinyInt:
				{
					short num10 = 0;
					odbcReturn = libodbc.SQLGetData(this.hstmt, num2, column.SqlCType, ref num10, 0, ref num);
					obj = Convert.ToByte(num10);
					goto IL_0698;
				}
				}
				num4 = 255;
				array = new byte[num4];
				odbcReturn = libodbc.SQLGetData(this.hstmt, num2, SQL_C_TYPE.CHAR, array, num4, ref num);
				if (num != -1 && (odbcReturn != OdbcReturn.SuccessWithInfo || num != -4))
				{
					obj = Encoding.Default.GetString(array, 0, num);
				}
				IL_0698:
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo && odbcReturn != OdbcReturn.NoData)
				{
					throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
				}
				if (num == -1)
				{
					column.Value = DBNull.Value;
				}
				else
				{
					column.Value = obj;
				}
			}
			return column.Value;
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of type <see cref="T:System.Object" /> into which to copy the attribute columns. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600105A RID: 4186 RVA: 0x00040508 File Offset: 0x0003E708
		public override int GetValues(object[] values)
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("The reader is closed.");
			}
			if (this.currentRow == -1)
			{
				throw new InvalidOperationException("No data available.");
			}
			for (int i = 0; i < values.Length; i++)
			{
				if (i < this.FieldCount)
				{
					values[i] = this.GetValue(i);
				}
				else
				{
					values[i] = null;
				}
			}
			int num;
			if (values.Length < this.FieldCount)
			{
				num = values.Length;
			}
			else if (values.Length == this.FieldCount)
			{
				num = this.FieldCount;
			}
			else
			{
				num = this.FieldCount;
			}
			return num;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> that can be used to iterate through the rows in the data reader.</returns>
		// Token: 0x0600105B RID: 4187 RVA: 0x000405B0 File Offset: 0x0003E7B0
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this);
		}

		// Token: 0x0600105C RID: 4188 RVA: 0x000405B8 File Offset: 0x0003E7B8
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				this.Close();
			}
			this.command = null;
			this.cols = null;
			this._dataTableSchema = null;
			this.disposed = true;
		}

		/// <summary>Gets a value that indicates whether the column contains nonexistent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600105D RID: 4189 RVA: 0x000405FC File Offset: 0x0003E7FC
		public override bool IsDBNull(int i)
		{
			return this.GetValue(i) is DBNull;
		}

		/// <summary>Advances the <see cref="T:System.Data.Odbc.OdbcDataReader" /> to the next result when reading the results of batch SQL statements.</summary>
		/// <returns>true if there are more result sets; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600105E RID: 4190 RVA: 0x00040610 File Offset: 0x0003E810
		public override bool NextResult()
		{
			OdbcReturn odbcReturn = libodbc.SQLMoreResults(this.hstmt);
			if (odbcReturn == OdbcReturn.Success)
			{
				short num = 0;
				libodbc.SQLNumResultCols(this.hstmt, ref num);
				this.cols = new OdbcColumn[(int)num];
				this._dataTableSchema = null;
				this.GetColumns();
			}
			return odbcReturn == OdbcReturn.Success;
		}

		// Token: 0x0600105F RID: 4191 RVA: 0x00040660 File Offset: 0x0003E860
		private bool NextRow()
		{
			OdbcReturn odbcReturn = libodbc.SQLFetch(this.hstmt);
			if (odbcReturn != OdbcReturn.Success)
			{
				this.currentRow = -1;
			}
			else
			{
				this.currentRow++;
			}
			foreach (OdbcColumn odbcColumn in this.cols)
			{
				if (odbcColumn != null)
				{
					odbcColumn.Value = null;
				}
			}
			return odbcReturn == OdbcReturn.Success;
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000406CC File Offset: 0x0003E8CC
		private int GetColumnAttribute(int column, FieldIdentifier fieldId)
		{
			byte[] array = new byte[255];
			short num = 0;
			int num2 = 0;
			OdbcReturn odbcReturn = libodbc.SQLColAttribute(this.hstmt, (short)column, fieldId, array, (short)array.Length, ref num, ref num2);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
			}
			return num2;
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x00040724 File Offset: 0x0003E924
		private string GetColumnAttributeStr(int column, FieldIdentifier fieldId)
		{
			byte[] array = new byte[255];
			short num = 0;
			int num2 = 0;
			OdbcReturn odbcReturn = libodbc.SQLColAttribute(this.hstmt, (short)column, fieldId, array, (short)array.Length, ref num, ref num2);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
			}
			string text = string.Empty;
			if (num > 0)
			{
				text = Encoding.Unicode.GetString(array, 0, (int)num);
			}
			return text;
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x0004079C File Offset: 0x0003E99C
		private string[] GetPrimaryKeys(string catalog, string schema, string table)
		{
			if (this.cols.Length <= 0)
			{
				return new string[0];
			}
			ArrayList arrayList = null;
			try
			{
				arrayList = this.GetPrimaryKeysBySQLPrimaryKey(catalog, schema, table);
			}
			catch (OdbcException)
			{
				try
				{
					arrayList = this.GetPrimaryKeysBySQLStatistics(catalog, schema, table);
				}
				catch (OdbcException)
				{
				}
			}
			if (arrayList == null)
			{
				return null;
			}
			arrayList.Sort();
			return (string[])arrayList.ToArray(typeof(string));
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x00040844 File Offset: 0x0003EA44
		private ArrayList GetPrimaryKeysBySQLPrimaryKey(string catalog, string schema, string table)
		{
			ArrayList arrayList = new ArrayList();
			IntPtr zero = IntPtr.Zero;
			try
			{
				OdbcReturn odbcReturn = libodbc.SQLAllocHandle(OdbcHandleType.Stmt, this.command.Connection.hDbc, ref zero);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.Connection.CreateOdbcException(OdbcHandleType.Dbc, this.Connection.hDbc);
				}
				odbcReturn = libodbc.SQLPrimaryKeys(zero, catalog, -3, schema, -3, table, -3);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, zero);
				}
				int num = 0;
				byte[] array = new byte[255];
				odbcReturn = libodbc.SQLBindCol(zero, 4, SQL_C_TYPE.CHAR, array, array.Length, ref num);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, zero);
				}
				for (;;)
				{
					odbcReturn = libodbc.SQLFetch(zero);
					if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
					{
						break;
					}
					string @string = Encoding.Default.GetString(array, 0, num);
					arrayList.Add(@string);
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					OdbcReturn odbcReturn = libodbc.SQLFreeStmt(zero, libodbc.SQLFreeStmtOptions.Close);
					if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
					{
						throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, zero);
					}
					odbcReturn = libodbc.SQLFreeHandle(3, zero);
					if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
					{
						throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, zero);
					}
				}
			}
			return arrayList;
		}

		// Token: 0x06001064 RID: 4196 RVA: 0x000409B0 File Offset: 0x0003EBB0
		private ArrayList GetPrimaryKeysBySQLStatistics(string catalog, string schema, string table)
		{
			ArrayList arrayList = new ArrayList();
			IntPtr zero = IntPtr.Zero;
			try
			{
				OdbcReturn odbcReturn = libodbc.SQLAllocHandle(OdbcHandleType.Stmt, this.command.Connection.hDbc, ref zero);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.Connection.CreateOdbcException(OdbcHandleType.Dbc, this.Connection.hDbc);
				}
				odbcReturn = libodbc.SQLStatistics(zero, catalog, -3, schema, -3, table, -3, 0, 0);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, zero);
				}
				int num = 0;
				short num2 = 0;
				odbcReturn = libodbc.SQLBindCol(zero, 4, SQL_C_TYPE.SHORT, ref num2, 2, ref num);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, zero);
				}
				int num3 = 0;
				byte[] array = new byte[255];
				odbcReturn = libodbc.SQLBindCol(zero, 9, SQL_C_TYPE.CHAR, array, array.Length, ref num3);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, zero);
				}
				for (;;)
				{
					odbcReturn = libodbc.SQLFetch(zero);
					if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
					{
						break;
					}
					if (num2 == 1)
					{
						goto Block_13;
					}
				}
				goto IL_0126;
				Block_13:
				string @string = Encoding.Default.GetString(array, 0, num3);
				arrayList.Add(@string);
				IL_0126:;
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					OdbcReturn odbcReturn = libodbc.SQLFreeStmt(zero, libodbc.SQLFreeStmtOptions.Close);
					if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
					{
						throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, zero);
					}
					odbcReturn = libodbc.SQLFreeHandle(3, zero);
					if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
					{
						throw this.Connection.CreateOdbcException(OdbcHandleType.Stmt, zero);
					}
				}
			}
			return arrayList;
		}

		/// <summary>Advances the <see cref="T:System.Data.Odbc.OdbcDataReader" /> to the next record.</summary>
		/// <returns>true if there are more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001065 RID: 4197 RVA: 0x00040B5C File Offset: 0x0003ED5C
		public override bool Read()
		{
			return this.NextRow();
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x00040B64 File Offset: 0x0003ED64
		private static string RemoveTrailingNullChar(string value)
		{
			return value.TrimEnd(new char[1]);
		}

		// Token: 0x0400055E RID: 1374
		private OdbcCommand command;

		// Token: 0x0400055F RID: 1375
		private bool open;

		// Token: 0x04000560 RID: 1376
		private int currentRow;

		// Token: 0x04000561 RID: 1377
		private OdbcColumn[] cols;

		// Token: 0x04000562 RID: 1378
		private IntPtr hstmt;

		// Token: 0x04000563 RID: 1379
		private int _recordsAffected = -1;

		// Token: 0x04000564 RID: 1380
		private bool disposed;

		// Token: 0x04000565 RID: 1381
		private DataTable _dataTableSchema;

		// Token: 0x04000566 RID: 1382
		private CommandBehavior behavior;
	}
}
