using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Text;
using Mono.Data.Tds.Protocol;

namespace System.Data.SqlClient
{
	/// <summary>Provides a way of reading a forward-only stream of rows from a SQL Server database. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000168 RID: 360
	public class SqlDataReader : DbDataReader, IDisposable, IDataReader, IDataRecord
	{
		// Token: 0x0600131D RID: 4893 RVA: 0x0004F1EC File Offset: 0x0004D3EC
		internal SqlDataReader(SqlCommand command)
		{
			this.command = command;
			command.Tds.RecordsAffected = -1;
			this.NextResult();
		}

		/// <summary>Gets a value that indicates the depth of nesting for the current row.</summary>
		/// <returns>The depth of nesting for the current row.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700037C RID: 892
		// (get) Token: 0x0600131E RID: 4894 RVA: 0x0004F21C File Offset: 0x0004D41C
		public override int Depth
		{
			get
			{
				return 0;
			}
		}

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid recordset, 0; otherwise the number of columns in the current row. The default is -1.</returns>
		/// <exception cref="T:System.NotSupportedException">There is no current connection to an instance of SQL Server. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700037D RID: 893
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x0004F220 File Offset: 0x0004D420
		public override int FieldCount
		{
			get
			{
				this.ValidateState();
				return this.command.Tds.Columns.Count;
			}
		}

		/// <summary>Retrieves a Boolean value that indicates whether the specified <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance has been closed. </summary>
		/// <returns>true if the specified <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance is closed; otherwise false. </returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037E RID: 894
		// (get) Token: 0x06001320 RID: 4896 RVA: 0x0004F248 File Offset: 0x0004D448
		public override bool IsClosed
		{
			get
			{
				return this.isClosed;
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column ordinal.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700037F RID: 895
		public override object this[int i]
		{
			get
			{
				return this.GetValue(i);
			}
		}

		/// <summary>Gets the value of the specified column in its native format given the column name.</summary>
		/// <returns>The value of the specified column in its native format.</returns>
		/// <param name="name">The column name. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000380 RID: 896
		public override object this[string name]
		{
			get
			{
				return this.GetValue(this.GetOrdinal(name));
			}
		}

		/// <summary>Gets the number of rows changed, inserted, or deleted by execution of the Transact-SQL statement.</summary>
		/// <returns>The number of rows changed, inserted, or deleted; 0 if no rows were affected or the statement failed; and -1 for SELECT statements.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06001323 RID: 4899 RVA: 0x0004F26C File Offset: 0x0004D46C
		public override int RecordsAffected
		{
			get
			{
				return this.command.Tds.RecordsAffected;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlDataReader" /> contains one or more rows.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlDataReader" /> contains one or more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06001324 RID: 4900 RVA: 0x0004F280 File Offset: 0x0004D480
		public override bool HasRows
		{
			get
			{
				this.ValidateState();
				if (this.rowsRead > 0)
				{
					return true;
				}
				if (!this.haveRead)
				{
					this.readResult = this.ReadRecord();
				}
				return this.readResult;
			}
		}

		/// <summary>Gets the number of fields in the <see cref="T:System.Data.SqlClient.SqlDataReader" /> that are not hidden. </summary>
		/// <returns>The number of fields that are not hidden.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000383 RID: 899
		// (get) Token: 0x06001325 RID: 4901 RVA: 0x0004F2B4 File Offset: 0x0004D4B4
		public override int VisibleFieldCount
		{
			get
			{
				return this.visibleFieldCount;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlConnection" /> associated with the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlConnection" /> associated with the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</returns>
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06001326 RID: 4902 RVA: 0x0004F2BC File Offset: 0x0004D4BC
		protected SqlConnection Connection
		{
			get
			{
				return this.command.Connection;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Data.CommandBehavior" /> matches that of the <see cref="T:System.Data.SqlClient.SqlDataReader" /> .</summary>
		/// <returns>true if the specified <see cref="T:System.Data.CommandBehavior" /> is true, false otherwise.</returns>
		/// <param name="condition">A <see cref="T:System.Data.CommandBehavior" /> enumeration.</param>
		// Token: 0x06001327 RID: 4903 RVA: 0x0004F2CC File Offset: 0x0004D4CC
		protected bool IsCommandBehavior(CommandBehavior condition)
		{
			return condition == this.command.CommandBehavior;
		}

		/// <summary>Closes the <see cref="T:System.Data.SqlClient.SqlDataReader" /> object.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001328 RID: 4904 RVA: 0x0004F2DC File Offset: 0x0004D4DC
		public override void Close()
		{
			if (this.IsClosed)
			{
				return;
			}
			while (this.NextResult())
			{
			}
			this.isClosed = true;
			this.command.CloseDataReader();
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0004F318 File Offset: 0x0004D518
		private static DataTable ConstructSchemaTable()
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
					{ "IsReadOnly", typeFromHandle },
					{ "ProviderSpecificDataType", typeFromHandle4 },
					{ "DataTypeName", typeFromHandle2 },
					{ "XmlSchemaCollectionDatabase", typeFromHandle2 },
					{ "XmlSchemaCollectionOwningSchema", typeFromHandle2 },
					{ "XmlSchemaCollectionName", typeFromHandle2 },
					{ "UdtAssemblyQualifiedName", typeFromHandle2 },
					{ "NonVersionedProviderType", typeFromHandle3 },
					{ "IsColumnSet", typeFromHandle }
				}
			};
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0004F5BC File Offset: 0x0004D7BC
		private string GetSchemaRowTypeName(TdsColumnType ctype, int csize, short precision, short scale)
		{
			int num;
			Type type;
			bool flag;
			string text;
			this.GetSchemaRowType(ctype, csize, precision, scale, out num, out type, out flag, out text);
			return text;
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x0004F5E0 File Offset: 0x0004D7E0
		private Type GetSchemaRowFieldType(TdsColumnType ctype, int csize, short precision, short scale)
		{
			int num;
			Type type;
			bool flag;
			string text;
			this.GetSchemaRowType(ctype, csize, precision, scale, out num, out type, out flag, out text);
			return type;
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x0004F604 File Offset: 0x0004D804
		private SqlDbType GetSchemaRowDbType(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this.command.Tds.Columns.Count)
			{
				throw new IndexOutOfRangeException();
			}
			TdsDataColumn tdsDataColumn = this.command.Tds.Columns[ordinal];
			TdsColumnType value = tdsDataColumn.ColumnType.Value;
			int value2 = tdsDataColumn.ColumnSize.Value;
			short? numericPrecision = tdsDataColumn.NumericPrecision;
			short num = ((numericPrecision == null) ? 0 : numericPrecision.Value);
			short? numericScale = tdsDataColumn.NumericScale;
			short num2 = ((numericScale == null) ? 0 : numericScale.Value);
			return this.GetSchemaRowDbType(value, value2, num, num2);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x0004F6C4 File Offset: 0x0004D8C4
		private SqlDbType GetSchemaRowDbType(TdsColumnType ctype, int csize, short precision, short scale)
		{
			int num;
			Type type;
			bool flag;
			string text;
			this.GetSchemaRowType(ctype, csize, precision, scale, out num, out type, out flag, out text);
			return (SqlDbType)num;
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x0004F6E8 File Offset: 0x0004D8E8
		private void GetSchemaRowType(TdsColumnType ctype, int csize, short precision, short scale, out int dbType, out Type fieldType, out bool isLong, out string typeName)
		{
			dbType = -1;
			typeName = string.Empty;
			isLong = false;
			fieldType = typeof(Type);
			switch (ctype)
			{
			case TdsColumnType.Image:
				typeName = "image";
				dbType = 7;
				fieldType = typeof(byte[]);
				isLong = true;
				return;
			case TdsColumnType.Text:
				typeName = "text";
				dbType = 18;
				fieldType = typeof(string);
				isLong = true;
				return;
			case TdsColumnType.UniqueIdentifier:
				typeName = "uniqueidentifier";
				dbType = 14;
				fieldType = typeof(Guid);
				isLong = false;
				return;
			case TdsColumnType.VarBinary:
				goto IL_02B2;
			case TdsColumnType.IntN:
			case TdsColumnType.Int1:
			case TdsColumnType.Int2:
			case TdsColumnType.Int4:
				break;
			case TdsColumnType.VarChar:
				goto IL_02D5;
			default:
				switch (ctype)
				{
				case TdsColumnType.NText:
					typeName = "ntext";
					dbType = 11;
					fieldType = typeof(string);
					isLong = true;
					return;
				default:
					switch (ctype)
					{
					case TdsColumnType.BigVarBinary:
						goto IL_02B2;
					default:
						switch (ctype)
						{
						case TdsColumnType.BigBinary:
							goto IL_02F8;
						default:
							if (ctype == TdsColumnType.SmallMoney)
							{
								typeName = "smallmoney";
								dbType = 17;
								fieldType = typeof(decimal);
								isLong = false;
								return;
							}
							if (ctype != TdsColumnType.BigInt)
							{
								if (ctype != TdsColumnType.NChar)
								{
									typeName = "variant";
									dbType = 23;
									fieldType = typeof(object);
									isLong = false;
									return;
								}
								typeName = "nchar";
								dbType = 10;
								fieldType = typeof(string);
								isLong = false;
								return;
							}
							break;
						case TdsColumnType.BigChar:
							goto IL_031A;
						}
						break;
					case TdsColumnType.BigVarChar:
						goto IL_02D5;
					}
					break;
				case TdsColumnType.NVarChar:
					typeName = "nvarchar";
					dbType = 12;
					fieldType = typeof(string);
					isLong = false;
					return;
				case TdsColumnType.BitN:
					goto IL_033C;
				case TdsColumnType.Decimal:
				case TdsColumnType.Numeric:
					if (precision == 19 && scale == 0)
					{
						typeName = "bigint";
						dbType = 0;
						fieldType = typeof(long);
					}
					else
					{
						typeName = "decimal";
						dbType = 5;
						fieldType = typeof(decimal);
					}
					isLong = false;
					return;
				case TdsColumnType.FloatN:
					goto IL_01EB;
				case TdsColumnType.MoneyN:
				case TdsColumnType.Money4:
					goto IL_03BD;
				case TdsColumnType.DateTimeN:
					goto IL_035E;
				}
				break;
			case TdsColumnType.Binary:
				goto IL_02F8;
			case TdsColumnType.Char:
				goto IL_031A;
			case TdsColumnType.Bit:
				goto IL_033C;
			case TdsColumnType.DateTime4:
			case TdsColumnType.DateTime:
				goto IL_035E;
			case TdsColumnType.Real:
			case TdsColumnType.Float8:
				goto IL_01EB;
			case TdsColumnType.Money:
				goto IL_03BD;
			}
			switch (csize)
			{
			case 1:
				typeName = "tinyint";
				dbType = 20;
				fieldType = typeof(byte);
				isLong = false;
				break;
			case 2:
				typeName = "smallint";
				dbType = 16;
				fieldType = typeof(short);
				isLong = false;
				break;
			case 4:
				typeName = "int";
				dbType = 8;
				fieldType = typeof(int);
				isLong = false;
				break;
			case 8:
				typeName = "bigint";
				dbType = 0;
				fieldType = typeof(long);
				isLong = false;
				break;
			}
			return;
			IL_01EB:
			if (csize != 4)
			{
				if (csize == 8)
				{
					typeName = "float";
					dbType = 6;
					fieldType = typeof(double);
					isLong = false;
				}
			}
			else
			{
				typeName = "real";
				dbType = 13;
				fieldType = typeof(float);
				isLong = false;
			}
			return;
			IL_02B2:
			typeName = "varbinary";
			dbType = 21;
			fieldType = typeof(byte[]);
			isLong = false;
			return;
			IL_02D5:
			typeName = "varchar";
			dbType = 22;
			fieldType = typeof(string);
			isLong = false;
			return;
			IL_02F8:
			typeName = "binary";
			dbType = 1;
			fieldType = typeof(byte[]);
			isLong = false;
			return;
			IL_031A:
			typeName = "char";
			dbType = 3;
			fieldType = typeof(string);
			isLong = false;
			return;
			IL_033C:
			typeName = "bit";
			dbType = 2;
			fieldType = typeof(bool);
			isLong = false;
			return;
			IL_035E:
			if (csize != 4)
			{
				if (csize == 8)
				{
					typeName = "datetime";
					dbType = 4;
					fieldType = typeof(DateTime);
					isLong = false;
				}
			}
			else
			{
				typeName = "smalldatetime";
				dbType = 15;
				fieldType = typeof(DateTime);
				isLong = false;
			}
			return;
			IL_03BD:
			if (csize != 4)
			{
				if (csize == 8)
				{
					typeName = "money";
					dbType = 9;
					fieldType = typeof(decimal);
					isLong = false;
				}
			}
			else
			{
				typeName = "smallmoney";
				dbType = 17;
				fieldType = typeof(decimal);
				isLong = false;
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004FC10 File Offset: 0x0004DE10
		private new void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					if (this.schemaTable != null)
					{
						this.schemaTable.Dispose();
					}
					this.Close();
					this.command = null;
				}
				this.disposed = true;
			}
		}

		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001330 RID: 4912 RVA: 0x0004FC50 File Offset: 0x0004DE50
		public override bool GetBoolean(int i)
		{
			object value = this.GetValue(i);
			if (value is bool)
			{
				return (bool)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Gets the value of the specified column as a byte.</summary>
		/// <returns>The value of the specified column as a byte.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001331 RID: 4913 RVA: 0x0004FCA4 File Offset: 0x0004DEA4
		public override byte GetByte(int i)
		{
			object value = this.GetValue(i);
			if (value is byte)
			{
				return (byte)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer an array starting at the given buffer offset.</summary>
		/// <returns>The number of bytes read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to begin the read operation.</param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferIndex">The index within the <paramref name="buffer" /> where the write operation is to start. </param>
		/// <param name="length">The maximum length to copy into the buffer. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001332 RID: 4914 RVA: 0x0004FCF8 File Offset: 0x0004DEF8
		public override long GetBytes(int i, long dataIndex, byte[] buffer, int bufferIndex, int length)
		{
			if ((this.command.CommandBehavior & CommandBehavior.SequentialAccess) != CommandBehavior.Default)
			{
				this.ValidateState();
				this.EnsureDataAvailable();
				try
				{
					long sequentialColumnValue = this.command.Tds.GetSequentialColumnValue(i, dataIndex, buffer, bufferIndex, length);
					if (sequentialColumnValue == -1L)
					{
						throw this.CreateGetBytesOnInvalidColumnTypeException(i);
					}
					if (sequentialColumnValue == -2L)
					{
						throw new SqlNullValueException();
					}
					return sequentialColumnValue;
				}
				catch (TdsInternalException ex)
				{
					this.command.Connection.Close();
					throw SqlException.FromTdsInternalException(ex);
				}
			}
			object obj = this.GetValue(i);
			if (!(obj is byte[]))
			{
				SqlDbType schemaRowDbType = this.GetSchemaRowDbType(i);
				SqlDbType sqlDbType = schemaRowDbType;
				if (sqlDbType != SqlDbType.Image)
				{
					if (sqlDbType != SqlDbType.NText)
					{
						if (sqlDbType != SqlDbType.Text)
						{
							throw this.CreateGetBytesOnInvalidColumnTypeException(i);
						}
						string text = obj as string;
						if (text != null)
						{
							obj = Encoding.Default.GetBytes(text);
						}
						else
						{
							obj = null;
						}
					}
					else
					{
						string text2 = obj as string;
						if (text2 != null)
						{
							obj = Encoding.Unicode.GetBytes(text2);
						}
						else
						{
							obj = null;
						}
					}
				}
				else if (obj is DBNull)
				{
					throw new SqlNullValueException();
				}
			}
			if (buffer == null)
			{
				return (long)((byte[])obj).Length;
			}
			int num = (int)((long)((byte[])obj).Length - dataIndex);
			if (num < length)
			{
				length = num;
			}
			if (dataIndex < 0L)
			{
				return 0L;
			}
			Array.Copy((byte[])obj, (int)dataIndex, buffer, bufferIndex, length);
			return (long)length;
		}

		/// <summary>Gets the value of the specified column as a single character.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001333 RID: 4915 RVA: 0x0004FE9C File Offset: 0x0004E09C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override char GetChar(int i)
		{
			throw new NotSupportedException();
		}

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array starting at the given buffer offset.</summary>
		/// <returns>The number of characters read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="dataIndex">The index within the field from which to begin the read operation.</param>
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
		// Token: 0x06001334 RID: 4916 RVA: 0x0004FEA4 File Offset: 0x0004E0A4
		public override long GetChars(int i, long dataIndex, char[] buffer, int bufferIndex, int length)
		{
			if ((this.command.CommandBehavior & CommandBehavior.SequentialAccess) != CommandBehavior.Default)
			{
				this.ValidateState();
				this.EnsureDataAvailable();
				if (i < 0 || i >= this.command.Tds.Columns.Count)
				{
					throw new IndexOutOfRangeException();
				}
				byte b = 1;
				TdsColumnType tdsColumnType = (TdsColumnType)((int)this.command.Tds.Columns[i]["ColumnType"]);
				TdsColumnType tdsColumnType2 = tdsColumnType;
				Encoding encoding;
				if (tdsColumnType2 != TdsColumnType.Text && tdsColumnType2 != TdsColumnType.VarChar && tdsColumnType2 != TdsColumnType.Char)
				{
					if (tdsColumnType2 != TdsColumnType.NText && tdsColumnType2 != TdsColumnType.NVarChar)
					{
						if (tdsColumnType2 == TdsColumnType.BigVarChar)
						{
							goto IL_00BE;
						}
						if (tdsColumnType2 != TdsColumnType.NChar)
						{
							return -1L;
						}
					}
					encoding = Encoding.Unicode;
					b = 2;
					goto IL_00D9;
				}
				IL_00BE:
				encoding = Encoding.ASCII;
				IL_00D9:
				long num;
				if (buffer == null)
				{
					num = this.GetBytes(i, 0L, null, 0, 0);
					return num / (long)b;
				}
				length *= (int)b;
				byte[] array = new byte[length];
				num = this.GetBytes(i, dataIndex, array, 0, length);
				if (num == -1L)
				{
					throw new InvalidCastException("Specified cast is not valid");
				}
				char[] chars = encoding.GetChars(array, 0, (int)num);
				chars.CopyTo(buffer, bufferIndex);
				return (long)chars.Length;
			}
			else
			{
				object value = this.GetValue(i);
				char[] array2;
				if (value is char[])
				{
					array2 = (char[])value;
				}
				else if (value is string)
				{
					array2 = ((string)value).ToCharArray();
				}
				else
				{
					if (value is DBNull)
					{
						throw new SqlNullValueException();
					}
					throw new InvalidCastException("Type is " + value.GetType().ToString());
				}
				if (buffer == null)
				{
					return (long)array2.Length;
				}
				Array.Copy(array2, (int)dataIndex, buffer, bufferIndex, length);
				return (long)array2.Length - dataIndex;
			}
		}

		/// <summary>Gets a string representing the data type of the specified column.</summary>
		/// <param name="i">The zero-based ordinal position of the column to find.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001335 RID: 4917 RVA: 0x00050084 File Offset: 0x0004E284
		public override string GetDataTypeName(int i)
		{
			this.ValidateState();
			if (i < 0 || i >= this.command.Tds.Columns.Count)
			{
				throw new IndexOutOfRangeException();
			}
			TdsDataColumn tdsDataColumn = this.command.Tds.Columns[i];
			TdsColumnType value = tdsDataColumn.ColumnType.Value;
			int value2 = tdsDataColumn.ColumnSize.Value;
			short? numericPrecision = tdsDataColumn.NumericPrecision;
			short num = ((numericPrecision == null) ? 0 : numericPrecision.Value);
			short? numericScale = tdsDataColumn.NumericScale;
			short num2 = ((numericScale == null) ? 0 : numericScale.Value);
			return this.GetSchemaRowTypeName(value, value2, num, num2);
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.DateTime" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001336 RID: 4918 RVA: 0x00050148 File Offset: 0x0004E348
		public override DateTime GetDateTime(int i)
		{
			object value = this.GetValue(i);
			if (value is DateTime)
			{
				return (DateTime)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Decimal" /> object.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001337 RID: 4919 RVA: 0x0005019C File Offset: 0x0004E39C
		public override decimal GetDecimal(int i)
		{
			object value = this.GetValue(i);
			if (value is decimal)
			{
				return (decimal)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Gets the value of the specified column as a double-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001338 RID: 4920 RVA: 0x000501F0 File Offset: 0x0004E3F0
		public override double GetDouble(int i)
		{
			object value = this.GetValue(i);
			if (value is double)
			{
				return (double)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Gets the <see cref="T:System.Type" /> that is the data type of the object.</summary>
		/// <returns>The <see cref="T:System.Type" /> that is the data type of the object. If the type does not exist on the client, in the case of a User-Defined Type (UDT) returned from the database, GetFieldType returns null.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001339 RID: 4921 RVA: 0x00050244 File Offset: 0x0004E444
		public override Type GetFieldType(int i)
		{
			this.ValidateState();
			if (i < 0 || i >= this.command.Tds.Columns.Count)
			{
				throw new IndexOutOfRangeException();
			}
			TdsDataColumn tdsDataColumn = this.command.Tds.Columns[i];
			TdsColumnType value = tdsDataColumn.ColumnType.Value;
			int value2 = tdsDataColumn.ColumnSize.Value;
			short? numericPrecision = tdsDataColumn.NumericPrecision;
			short num = ((numericPrecision == null) ? 0 : numericPrecision.Value);
			short? numericScale = tdsDataColumn.NumericScale;
			short num2 = ((numericScale == null) ? 0 : numericScale.Value);
			return this.GetSchemaRowFieldType(value, value2, num, num2);
		}

		/// <summary>Gets the value of the specified column as a single-precision floating point number.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600133A RID: 4922 RVA: 0x00050308 File Offset: 0x0004E508
		public override float GetFloat(int i)
		{
			object value = this.GetValue(i);
			if (value is float)
			{
				return (float)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Gets the value of the specified column as a globally unique identifier (GUID).</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600133B RID: 4923 RVA: 0x0005035C File Offset: 0x0004E55C
		public override Guid GetGuid(int i)
		{
			object value = this.GetValue(i);
			if (value is Guid)
			{
				return (Guid)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Gets the value of the specified column as a 16-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600133C RID: 4924 RVA: 0x000503B0 File Offset: 0x0004E5B0
		public override short GetInt16(int i)
		{
			object value = this.GetValue(i);
			if (value is short)
			{
				return (short)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Gets the value of the specified column as a 32-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600133D RID: 4925 RVA: 0x00050404 File Offset: 0x0004E604
		public override int GetInt32(int i)
		{
			object value = this.GetValue(i);
			if (value is int)
			{
				return (int)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Gets the value of the specified column as a 64-bit signed integer.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600133E RID: 4926 RVA: 0x00050458 File Offset: 0x0004E658
		public override long GetInt64(int i)
		{
			object value = this.GetValue(i);
			if (value is long)
			{
				return (long)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Gets the name of the specified column.</summary>
		/// <returns>The name of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600133F RID: 4927 RVA: 0x000504AC File Offset: 0x0004E6AC
		public override string GetName(int i)
		{
			this.ValidateState();
			if (i < 0 || i >= this.command.Tds.Columns.Count)
			{
				throw new IndexOutOfRangeException();
			}
			return this.command.Tds.Columns[i].ColumnName;
		}

		/// <summary>Gets the column ordinal, given the name of the column.</summary>
		/// <returns>The zero-based column ordinal.</returns>
		/// <param name="name">The name of the column. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The name specified is not a valid column name. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001340 RID: 4928 RVA: 0x00050504 File Offset: 0x0004E704
		public override int GetOrdinal(string name)
		{
			this.ValidateState();
			if (name == null)
			{
				throw new ArgumentNullException("fieldName");
			}
			foreach (object obj in this.command.Tds.Columns)
			{
				TdsDataColumn tdsDataColumn = (TdsDataColumn)obj;
				string columnName = tdsDataColumn.ColumnName;
				if (columnName.Equals(name) || string.Compare(columnName, name, true) == 0)
				{
					return tdsDataColumn.ColumnOrdinal.Value;
				}
			}
			throw new IndexOutOfRangeException();
		}

		/// <summary>Returns a <see cref="T:System.Data.DataTable" /> that describes the column metadata of the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that describes the column metadata.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.SqlClient.SqlDataReader" /> is closed. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001341 RID: 4929 RVA: 0x000505D0 File Offset: 0x0004E7D0
		public override DataTable GetSchemaTable()
		{
			this.ValidateState();
			if (this.schemaTable == null)
			{
				this.schemaTable = SqlDataReader.ConstructSchemaTable();
			}
			if (this.schemaTable.Rows != null && this.schemaTable.Rows.Count > 0)
			{
				return this.schemaTable;
			}
			if (!this.moreResults)
			{
				return null;
			}
			foreach (object obj in this.command.Tds.Columns)
			{
				TdsDataColumn tdsDataColumn = (TdsDataColumn)obj;
				DataRow dataRow = this.schemaTable.NewRow();
				dataRow[0] = SqlDataReader.GetSchemaValue(tdsDataColumn.ColumnName);
				dataRow[1] = SqlDataReader.GetSchemaValue(tdsDataColumn.ColumnOrdinal);
				dataRow[5] = SqlDataReader.GetSchemaValue(tdsDataColumn.IsUnique);
				dataRow[18] = SqlDataReader.GetSchemaValue(tdsDataColumn.IsAutoIncrement);
				dataRow[19] = SqlDataReader.GetSchemaValue(tdsDataColumn.IsRowVersion);
				dataRow[20] = SqlDataReader.GetSchemaValue(tdsDataColumn.IsHidden);
				dataRow[17] = SqlDataReader.GetSchemaValue(tdsDataColumn.IsIdentity);
				dataRow[3] = SqlDataReader.GetSchemaValue(tdsDataColumn.NumericPrecision);
				dataRow[6] = SqlDataReader.GetSchemaValue(tdsDataColumn.IsKey);
				dataRow[15] = SqlDataReader.GetSchemaValue(tdsDataColumn.IsAliased);
				dataRow[16] = SqlDataReader.GetSchemaValue(tdsDataColumn.IsExpression);
				dataRow[22] = SqlDataReader.GetSchemaValue(tdsDataColumn.IsReadOnly);
				dataRow[7] = SqlDataReader.GetSchemaValue(tdsDataColumn.BaseServerName);
				dataRow[8] = SqlDataReader.GetSchemaValue(tdsDataColumn.BaseCatalogName);
				dataRow[9] = SqlDataReader.GetSchemaValue(tdsDataColumn.BaseColumnName);
				dataRow[10] = SqlDataReader.GetSchemaValue(tdsDataColumn.BaseSchemaName);
				dataRow[11] = SqlDataReader.GetSchemaValue(tdsDataColumn.BaseTableName);
				dataRow[13] = SqlDataReader.GetSchemaValue(tdsDataColumn.AllowDBNull);
				dataRow[23] = DBNull.Value;
				dataRow[24] = SqlDataReader.GetSchemaValue(tdsDataColumn.DataTypeName);
				dataRow[25] = DBNull.Value;
				dataRow[26] = DBNull.Value;
				dataRow[27] = DBNull.Value;
				dataRow[28] = DBNull.Value;
				dataRow[29] = DBNull.Value;
				dataRow[30] = DBNull.Value;
				if (dataRow[9] == DBNull.Value)
				{
					dataRow[9] = dataRow[0];
				}
				TdsColumnType value = tdsDataColumn.ColumnType.Value;
				int value2 = tdsDataColumn.ColumnSize.Value;
				short num = (short)SqlDataReader.GetSchemaValue(tdsDataColumn.NumericPrecision);
				short num2 = (short)SqlDataReader.GetSchemaValue(tdsDataColumn.NumericScale);
				int num3;
				Type type;
				bool flag;
				string text;
				this.GetSchemaRowType(value, value2, num, num2, out num3, out type, out flag, out text);
				dataRow[2] = value2;
				dataRow[3] = num;
				dataRow[4] = num2;
				dataRow[14] = num3;
				dataRow[12] = type;
				dataRow[21] = flag;
				if (!(bool)dataRow[20])
				{
					this.visibleFieldCount++;
				}
				this.schemaTable.Rows.Add(dataRow);
			}
			return this.schemaTable;
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x000509B0 File Offset: 0x0004EBB0
		private static object GetSchemaValue(TdsDataColumn schema, string key)
		{
			object obj = schema[key];
			if (obj != null)
			{
				return obj;
			}
			return DBNull.Value;
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x000509D4 File Offset: 0x0004EBD4
		private static object GetSchemaValue(object value)
		{
			if (value == null)
			{
				return DBNull.Value;
			}
			return value;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001344 RID: 4932 RVA: 0x000509E4 File Offset: 0x0004EBE4
		public virtual SqlBinary GetSqlBinary(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlBinary))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlBinary)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001345 RID: 4933 RVA: 0x00050A28 File Offset: 0x0004EC28
		public virtual SqlBoolean GetSqlBoolean(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlBoolean))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlBoolean)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001346 RID: 4934 RVA: 0x00050A6C File Offset: 0x0004EC6C
		public virtual SqlByte GetSqlByte(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlByte))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlByte)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001347 RID: 4935 RVA: 0x00050AB0 File Offset: 0x0004ECB0
		public virtual SqlDateTime GetSqlDateTime(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlDateTime))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlDateTime)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001348 RID: 4936 RVA: 0x00050AF4 File Offset: 0x0004ECF4
		public virtual SqlDecimal GetSqlDecimal(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlDecimal))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlDecimal)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001349 RID: 4937 RVA: 0x00050B38 File Offset: 0x0004ED38
		public virtual SqlDouble GetSqlDouble(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlDouble))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlDouble)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600134A RID: 4938 RVA: 0x00050B7C File Offset: 0x0004ED7C
		public virtual SqlGuid GetSqlGuid(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlGuid))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlGuid)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134B RID: 4939 RVA: 0x00050BC0 File Offset: 0x0004EDC0
		public virtual SqlInt16 GetSqlInt16(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlInt16))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlInt16)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134C RID: 4940 RVA: 0x00050C04 File Offset: 0x0004EE04
		public virtual SqlInt32 GetSqlInt32(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlInt32))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlInt32)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134D RID: 4941 RVA: 0x00050C48 File Offset: 0x0004EE48
		public virtual SqlInt64 GetSqlInt64(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlInt64))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlInt64)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134E RID: 4942 RVA: 0x00050C8C File Offset: 0x0004EE8C
		public virtual SqlMoney GetSqlMoney(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlMoney))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlMoney)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600134F RID: 4943 RVA: 0x00050CD0 File Offset: 0x0004EED0
		public virtual SqlSingle GetSqlSingle(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlSingle))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlSingle)sqlValue;
		}

		/// <summary>Gets the value of the specified column as a <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001350 RID: 4944 RVA: 0x00050D14 File Offset: 0x0004EF14
		public virtual SqlString GetSqlString(int i)
		{
			object sqlValue = this.GetSqlValue(i);
			if (!(sqlValue is SqlString))
			{
				throw new InvalidCastException("Type is " + sqlValue.GetType().ToString());
			}
			return (SqlString)sqlValue;
		}

		/// <summary>Gets the value of the specified column as an XML value.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlXml" /> value that contains the XML stored within the corresponding field. </returns>
		/// <param name="i">The zero-based column ordinal.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The index passed was outside the range of 0 to <see cref="P:System.Data.DataTableReader.FieldCount" /> - 1</exception>
		/// <exception cref="T:System.InvalidOperationException">An attempt was made to read or access columns in a closed <see cref="T:System.Data.SqlClient.SqlDataReader" />.</exception>
		/// <exception cref="T:System.InvalidCastException">The retrieved data is not compatible with the <see cref="T:System.Data.SqlTypes.SqlXml" /> type.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001351 RID: 4945 RVA: 0x00050D58 File Offset: 0x0004EF58
		public virtual SqlXml GetSqlXml(int i)
		{
			object obj = this.GetSqlValue(i);
			if (!(obj is SqlXml))
			{
				if (obj is DBNull)
				{
					throw new SqlNullValueException();
				}
				if (this.command.Tds.TdsVersion > TdsVersion.tds80 || !(obj is SqlString))
				{
					throw new InvalidCastException("Type is " + obj.GetType().ToString());
				}
				MemoryStream memoryStream = null;
				if (!((SqlString)obj).IsNull)
				{
					memoryStream = new MemoryStream(Encoding.Unicode.GetBytes(obj.ToString()));
				}
				obj = new SqlXml(memoryStream);
			}
			return (SqlXml)obj;
		}

		/// <summary>Returns the data value in the specified column as a SQL Server type. </summary>
		/// <returns>The value of the column expressed as a <see cref="T:System.Data.SqlDbType" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001352 RID: 4946 RVA: 0x00050E04 File Offset: 0x0004F004
		public virtual object GetSqlValue(int i)
		{
			object value = this.GetValue(i);
			switch (this.GetSchemaRowDbType(i))
			{
			case SqlDbType.BigInt:
				if (value == DBNull.Value)
				{
					return SqlInt64.Null;
				}
				return (long)value;
			case SqlDbType.Binary:
			case SqlDbType.Image:
			case SqlDbType.Timestamp:
			case SqlDbType.VarBinary:
				if (value == DBNull.Value)
				{
					return SqlBinary.Null;
				}
				return (byte[])value;
			case SqlDbType.Bit:
				if (value == DBNull.Value)
				{
					return SqlBoolean.Null;
				}
				return (bool)value;
			case SqlDbType.Char:
			case SqlDbType.NChar:
			case SqlDbType.NText:
			case SqlDbType.NVarChar:
			case SqlDbType.Text:
			case SqlDbType.VarChar:
				if (value == DBNull.Value)
				{
					return SqlString.Null;
				}
				return (string)value;
			case SqlDbType.DateTime:
			case SqlDbType.SmallDateTime:
				if (value == DBNull.Value)
				{
					return SqlDateTime.Null;
				}
				return (DateTime)value;
			case SqlDbType.Decimal:
				if (value == DBNull.Value)
				{
					return SqlDecimal.Null;
				}
				if (value is TdsBigDecimal)
				{
					return SqlDecimal.FromTdsBigDecimal((TdsBigDecimal)value);
				}
				return (decimal)value;
			case SqlDbType.Float:
				if (value == DBNull.Value)
				{
					return SqlDouble.Null;
				}
				return (double)value;
			case SqlDbType.Int:
				if (value == DBNull.Value)
				{
					return SqlInt32.Null;
				}
				return (int)value;
			case SqlDbType.Money:
			case SqlDbType.SmallMoney:
				if (value == DBNull.Value)
				{
					return SqlMoney.Null;
				}
				return (decimal)value;
			case SqlDbType.Real:
				if (value == DBNull.Value)
				{
					return SqlSingle.Null;
				}
				return (float)value;
			case SqlDbType.UniqueIdentifier:
				if (value == DBNull.Value)
				{
					return SqlGuid.Null;
				}
				return (Guid)value;
			case SqlDbType.SmallInt:
				if (value == DBNull.Value)
				{
					return SqlInt16.Null;
				}
				return (short)value;
			case SqlDbType.TinyInt:
				if (value == DBNull.Value)
				{
					return SqlByte.Null;
				}
				return (byte)value;
			case SqlDbType.Xml:
				if (value == DBNull.Value)
				{
					return SqlByte.Null;
				}
				return (SqlXml)value;
			}
			throw new InvalidOperationException("The type of this column is unknown.");
		}

		/// <summary>Fills an array of <see cref="T:System.Object" /> that contains the values for all the columns in the record, expressed as SQL Server types.</summary>
		/// <returns>An integer indicating the number of columns copied.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the values. The column values are expressed as SQL Server types.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="values" /> is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001353 RID: 4947 RVA: 0x000510F8 File Offset: 0x0004F2F8
		public virtual int GetSqlValues(object[] values)
		{
			this.ValidateState();
			this.EnsureDataAvailable();
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int count = this.command.Tds.Columns.Count;
			int num = values.Length;
			int num2;
			if (num > count)
			{
				num2 = count;
			}
			else
			{
				num2 = num;
			}
			for (int i = 0; i < num2; i++)
			{
				values[i] = this.GetSqlValue(i);
			}
			return num2;
		}

		/// <summary>Gets the value of the specified column as a string.</summary>
		/// <returns>The value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.InvalidCastException">The specified cast is not valid. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001354 RID: 4948 RVA: 0x0005116C File Offset: 0x0004F36C
		public override string GetString(int i)
		{
			object value = this.GetValue(i);
			if (value is string)
			{
				return (string)value;
			}
			if (value is DBNull)
			{
				throw new SqlNullValueException();
			}
			throw new InvalidCastException("Type is " + value.GetType().ToString());
		}

		/// <summary>Gets the value of the specified column in its native format.</summary>
		/// <returns>This method returns <see cref="T:System.DBNull" /> for null database columns.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001355 RID: 4949 RVA: 0x000511C0 File Offset: 0x0004F3C0
		public override object GetValue(int i)
		{
			this.ValidateState();
			this.EnsureDataAvailable();
			if (i < 0 || i >= this.command.Tds.Columns.Count)
			{
				throw new IndexOutOfRangeException();
			}
			try
			{
				if ((this.command.CommandBehavior & CommandBehavior.SequentialAccess) != CommandBehavior.Default)
				{
					return this.command.Tds.GetSequentialColumnValue(i);
				}
			}
			catch (TdsInternalException ex)
			{
				this.command.Connection.Close();
				throw SqlException.FromTdsInternalException(ex);
			}
			return this.command.Tds.ColumnValues[i];
		}

		/// <summary>Populates an array of objects with the column values of the current row.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the attribute columns. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001356 RID: 4950 RVA: 0x00051284 File Offset: 0x0004F484
		public override int GetValues(object[] values)
		{
			this.ValidateState();
			this.EnsureDataAvailable();
			if (values == null)
			{
				throw new ArgumentNullException("values");
			}
			int num = values.Length;
			int bigDecimalIndex = this.command.Tds.ColumnValues.BigDecimalIndex;
			if (bigDecimalIndex >= 0 && bigDecimalIndex < num)
			{
				throw new OverflowException();
			}
			try
			{
				this.command.Tds.ColumnValues.CopyTo(0, values, 0, (num <= this.command.Tds.ColumnValues.Count) ? num : this.command.Tds.ColumnValues.Count);
			}
			catch (TdsInternalException ex)
			{
				this.command.Connection.Close();
				throw SqlException.FromTdsInternalException(ex);
			}
			return (num >= this.FieldCount) ? this.FieldCount : num;
		}

		/// <summary>Returns an <see cref="T:System.Collections.IEnumerator" /> that iterates through the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for the <see cref="T:System.Data.SqlClient.SqlDataReader" />.</returns>
		// Token: 0x06001357 RID: 4951 RVA: 0x00051380 File Offset: 0x0004F580
		public override IEnumerator GetEnumerator()
		{
			return new DbEnumerator(this);
		}

		/// <summary>Gets a value that indicates whether the column contains non-existent or missing values.</summary>
		/// <returns>true if the specified column value is equivalent to <see cref="T:System.DBNull" />; otherwise false.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001358 RID: 4952 RVA: 0x00051388 File Offset: 0x0004F588
		public override bool IsDBNull(int i)
		{
			return this.GetValue(i) == DBNull.Value;
		}

		/// <summary>Advances the data reader to the next result, when reading the results of batch Transact-SQL statements.</summary>
		/// <returns>true if there are more result sets; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001359 RID: 4953 RVA: 0x00051398 File Offset: 0x0004F598
		public override bool NextResult()
		{
			this.ValidateState();
			if ((this.command.CommandBehavior & CommandBehavior.SingleResult) != CommandBehavior.Default && this.resultsRead > 0)
			{
				this.moreResults = false;
				this.rowsRead = 0;
				this.haveRead = false;
				return false;
			}
			try
			{
				this.moreResults = this.command.Tds.NextResult();
			}
			catch (TdsInternalException ex)
			{
				this.command.Connection.Close();
				throw SqlException.FromTdsInternalException(ex);
			}
			if (!this.moreResults)
			{
				this.command.GetOutputParameters();
			}
			else
			{
				this.schemaTable = null;
			}
			this.rowsRead = 0;
			this.haveRead = false;
			this.resultsRead++;
			return this.moreResults;
		}

		/// <summary>Advances the <see cref="T:System.Data.SqlClient.SqlDataReader" /> to the next record.</summary>
		/// <returns>true if there are more rows; otherwise false.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600135A RID: 4954 RVA: 0x00051478 File Offset: 0x0004F678
		public override bool Read()
		{
			this.ValidateState();
			if (!this.haveRead || this.readResultUsed)
			{
				this.readResult = this.ReadRecord();
			}
			this.readResultUsed = true;
			return this.readResult;
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000514B0 File Offset: 0x0004F6B0
		internal bool ReadRecord()
		{
			this.readResultUsed = false;
			if ((this.command.CommandBehavior & CommandBehavior.SingleRow) != CommandBehavior.Default && this.haveRead)
			{
				return false;
			}
			if ((this.command.CommandBehavior & CommandBehavior.SchemaOnly) != CommandBehavior.Default)
			{
				return false;
			}
			if (!this.moreResults)
			{
				return false;
			}
			bool flag2;
			try
			{
				bool flag = this.command.Tds.NextRow();
				if (flag)
				{
					this.rowsRead++;
				}
				this.haveRead = true;
				flag2 = flag;
			}
			catch (TdsInternalException ex)
			{
				this.command.Connection.Close();
				throw SqlException.FromTdsInternalException(ex);
			}
			return flag2;
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x00051578 File Offset: 0x0004F778
		private void ValidateState()
		{
			if (this.IsClosed)
			{
				throw new InvalidOperationException("Invalid attempt to read data when reader is closed");
			}
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x00051590 File Offset: 0x0004F790
		private void EnsureDataAvailable()
		{
			if (!this.readResult || !this.haveRead || !this.readResultUsed)
			{
				throw new InvalidOperationException("No data available.");
			}
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000515CC File Offset: 0x0004F7CC
		private InvalidCastException CreateGetBytesOnInvalidColumnTypeException(int ordinal)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "Invalid attempt to GetBytes on column '{0}'.The GetBytes function can only be used on columns of type Text, NText, or Image.", new object[] { this.GetName(ordinal) });
			return new InvalidCastException(text);
		}

		/// <summary>Gets an Object that is a representation of the underlying provider-specific field type.</summary>
		/// <returns>Gets an <see cref="T:System.Object" /> that is a representation of the underlying provider-specific field type.</returns>
		/// <param name="i">An <see cref="T:System.Int32" /> representing the column ordinal. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600135F RID: 4959 RVA: 0x00051600 File Offset: 0x0004F800
		public override Type GetProviderSpecificFieldType(int i)
		{
			return this.GetSqlValue(i).GetType();
		}

		/// <summary>Gets an Object that is a representation of the underlying provider specific value.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a representation of the underlying provider specific value.</returns>
		/// <param name="i">An <see cref="T:System.Int32" /> representing the column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001360 RID: 4960 RVA: 0x00051610 File Offset: 0x0004F810
		public override object GetProviderSpecificValue(int i)
		{
			return this.GetSqlValue(i);
		}

		/// <summary>Gets an array of objects that are a representation of the underlying provider specific values.</summary>
		/// <returns>The array of objects that are a representation of the underlying provider specific values.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> into which to copy the column values.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001361 RID: 4961 RVA: 0x0005161C File Offset: 0x0004F81C
		public override int GetProviderSpecificValues(object[] values)
		{
			return this.GetSqlValues(values);
		}

		/// <summary>Gets the value of the specified column as <see cref="T:System.Data.SqlTypes.SqlBytes" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001362 RID: 4962 RVA: 0x00051628 File Offset: 0x0004F828
		public virtual SqlBytes GetSqlBytes(int i)
		{
			byte[] array = (byte[])this.GetValue(i);
			return new SqlBytes(array);
		}

		// Token: 0x040007B6 RID: 1974
		private const int COLUMN_NAME_IDX = 0;

		// Token: 0x040007B7 RID: 1975
		private const int COLUMN_ORDINAL_IDX = 1;

		// Token: 0x040007B8 RID: 1976
		private const int COLUMN_SIZE_IDX = 2;

		// Token: 0x040007B9 RID: 1977
		private const int NUMERIC_PRECISION_IDX = 3;

		// Token: 0x040007BA RID: 1978
		private const int NUMERIC_SCALE_IDX = 4;

		// Token: 0x040007BB RID: 1979
		private const int IS_UNIQUE_IDX = 5;

		// Token: 0x040007BC RID: 1980
		private const int IS_KEY_IDX = 6;

		// Token: 0x040007BD RID: 1981
		private const int BASE_SERVER_NAME_IDX = 7;

		// Token: 0x040007BE RID: 1982
		private const int BASE_CATALOG_NAME_IDX = 8;

		// Token: 0x040007BF RID: 1983
		private const int BASE_COLUMN_NAME_IDX = 9;

		// Token: 0x040007C0 RID: 1984
		private const int BASE_SCHEMA_NAME_IDX = 10;

		// Token: 0x040007C1 RID: 1985
		private const int BASE_TABLE_NAME_IDX = 11;

		// Token: 0x040007C2 RID: 1986
		private const int DATA_TYPE_IDX = 12;

		// Token: 0x040007C3 RID: 1987
		private const int ALLOW_DBNULL_IDX = 13;

		// Token: 0x040007C4 RID: 1988
		private const int PROVIDER_TYPE_IDX = 14;

		// Token: 0x040007C5 RID: 1989
		private const int IS_ALIASED_IDX = 15;

		// Token: 0x040007C6 RID: 1990
		private const int IS_EXPRESSION_IDX = 16;

		// Token: 0x040007C7 RID: 1991
		private const int IS_IDENTITY_IDX = 17;

		// Token: 0x040007C8 RID: 1992
		private const int IS_AUTO_INCREMENT_IDX = 18;

		// Token: 0x040007C9 RID: 1993
		private const int IS_ROW_VERSION_IDX = 19;

		// Token: 0x040007CA RID: 1994
		private const int IS_HIDDEN_IDX = 20;

		// Token: 0x040007CB RID: 1995
		private const int IS_LONG_IDX = 21;

		// Token: 0x040007CC RID: 1996
		private const int IS_READ_ONLY_IDX = 22;

		// Token: 0x040007CD RID: 1997
		private const int PROVIDER_SPECIFIC_TYPE_IDX = 23;

		// Token: 0x040007CE RID: 1998
		private const int DATA_TYPE_NAME_IDX = 24;

		// Token: 0x040007CF RID: 1999
		private const int XML_SCHEMA_COLLCTN_DB_IDX = 25;

		// Token: 0x040007D0 RID: 2000
		private const int XML_SCHEMA_COLLCTN_OWN_SCHEMA_IDX = 26;

		// Token: 0x040007D1 RID: 2001
		private const int XML_SCHEMA_COLLCTN_NAME_IDX = 27;

		// Token: 0x040007D2 RID: 2002
		private const int UDT_ASMBLY_QUALIFIED_NAME_IDX = 28;

		// Token: 0x040007D3 RID: 2003
		private const int NON_VER_PROVIDER_TYPE_IDX = 29;

		// Token: 0x040007D4 RID: 2004
		private const int IS_COLUMN_SET = 30;

		// Token: 0x040007D5 RID: 2005
		private SqlCommand command;

		// Token: 0x040007D6 RID: 2006
		private bool disposed;

		// Token: 0x040007D7 RID: 2007
		private bool isClosed;

		// Token: 0x040007D8 RID: 2008
		private bool moreResults;

		// Token: 0x040007D9 RID: 2009
		private int resultsRead;

		// Token: 0x040007DA RID: 2010
		private int rowsRead;

		// Token: 0x040007DB RID: 2011
		private DataTable schemaTable;

		// Token: 0x040007DC RID: 2012
		private bool haveRead;

		// Token: 0x040007DD RID: 2013
		private bool readResult;

		// Token: 0x040007DE RID: 2014
		private bool readResultUsed;

		// Token: 0x040007DF RID: 2015
		private int visibleFieldCount;
	}
}
