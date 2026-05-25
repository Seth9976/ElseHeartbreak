using System;
using System.Data;
using System.Data.SqlTypes;
using System.Threading;

namespace Microsoft.SqlServer.Server
{
	/// <summary>Specifies and retrieves metadata information from parameters and columns of <see cref="T:Microsoft.SqlServer.Server.SqlDataRecord" /> objects. This class cannot be inherited.</summary>
	// Token: 0x02000149 RID: 329
	public sealed class SqlMetaData
	{
		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name and type.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x0600117B RID: 4475 RVA: 0x000445B8 File Offset: 0x000427B8
		public SqlMetaData(string name, SqlDbType sqlDbType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name can not be null");
			}
			switch (sqlDbType)
			{
			case SqlDbType.BigInt:
				this.maxLength = 8L;
				this.precision = 19;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Int64;
				this.type = typeof(long);
				goto IL_045E;
			case SqlDbType.Bit:
				this.maxLength = 1L;
				this.precision = 1;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Boolean;
				this.type = typeof(bool);
				goto IL_045E;
			case SqlDbType.DateTime:
				this.maxLength = 8L;
				this.precision = 23;
				this.scale = 3;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.DateTime;
				this.type = typeof(DateTime);
				goto IL_045E;
			case SqlDbType.Decimal:
				this.maxLength = 9L;
				this.precision = 18;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Decimal;
				this.type = typeof(decimal);
				goto IL_045E;
			case SqlDbType.Float:
				this.maxLength = 8L;
				this.precision = 53;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Double;
				this.type = typeof(float);
				goto IL_045E;
			case SqlDbType.Int:
				this.maxLength = 4L;
				this.precision = 10;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Int32;
				this.type = typeof(int);
				goto IL_045E;
			case SqlDbType.Money:
				this.maxLength = 8L;
				this.precision = 19;
				this.scale = 4;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Currency;
				this.type = typeof(double);
				goto IL_045E;
			case SqlDbType.UniqueIdentifier:
				this.maxLength = 16L;
				this.precision = 0;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Guid;
				this.type = typeof(Guid);
				goto IL_045E;
			case SqlDbType.SmallDateTime:
				this.maxLength = 4L;
				this.precision = 16;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.DateTime;
				this.type = typeof(DateTime);
				goto IL_045E;
			case SqlDbType.SmallInt:
				this.maxLength = 2L;
				this.precision = 5;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Int16;
				this.type = typeof(short);
				goto IL_045E;
			case SqlDbType.SmallMoney:
				this.maxLength = 4L;
				this.precision = 10;
				this.scale = 4;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Currency;
				this.type = typeof(double);
				goto IL_045E;
			case SqlDbType.Timestamp:
				this.maxLength = 8L;
				this.precision = 0;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.DateTime;
				this.type = typeof(DateTime);
				goto IL_045E;
			case SqlDbType.TinyInt:
				this.maxLength = 1L;
				this.precision = 3;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Int16;
				this.type = typeof(short);
				goto IL_045E;
			case SqlDbType.Xml:
				this.maxLength = -1L;
				this.precision = 0;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
				this.dbType = DbType.Xml;
				this.type = typeof(string);
				goto IL_045E;
			}
			throw new ArgumentException("SqlDbType not supported");
			IL_045E:
			this.name = name;
			this.sqlDbType = sqlDbType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, and maximum length.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x0600117C RID: 4476 RVA: 0x00044A34 File Offset: 0x00042C34
		public SqlMetaData(string name, SqlDbType sqlDbType, long maxLength)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name can not be null");
			}
			switch (sqlDbType)
			{
			case SqlDbType.Binary:
				this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
				this.dbType = DbType.Binary;
				this.type = typeof(byte[]);
				break;
			default:
				switch (sqlDbType)
				{
				case SqlDbType.Text:
					maxLength = -1L;
					this.precision = 0;
					this.scale = 0;
					this.localeId = (long)Thread.CurrentThread.CurrentCulture.LCID;
					this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
					this.dbType = DbType.String;
					this.type = typeof(char[]);
					goto IL_02BD;
				case SqlDbType.VarBinary:
					maxLength = -1L;
					this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
					this.dbType = DbType.Binary;
					this.type = typeof(byte[]);
					goto IL_02BD;
				case SqlDbType.VarChar:
					maxLength = -1L;
					this.localeId = (long)Thread.CurrentThread.CurrentCulture.LCID;
					this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
					this.dbType = DbType.String;
					this.type = typeof(char[]);
					goto IL_02BD;
				}
				throw new ArgumentException("SqlDbType not supported");
			case SqlDbType.Char:
				this.localeId = (long)Thread.CurrentThread.CurrentCulture.LCID;
				this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
				this.dbType = DbType.AnsiStringFixedLength;
				this.type = typeof(string);
				break;
			case SqlDbType.Image:
				maxLength = -1L;
				this.precision = 0;
				this.scale = 0;
				this.localeId = 0L;
				this.compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Binary;
				this.type = typeof(byte[]);
				break;
			case SqlDbType.NChar:
				this.localeId = (long)Thread.CurrentThread.CurrentCulture.LCID;
				this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
				this.dbType = DbType.String;
				this.type = typeof(string);
				break;
			case SqlDbType.NText:
				maxLength = -1L;
				this.precision = 0;
				this.scale = 0;
				this.localeId = (long)Thread.CurrentThread.CurrentCulture.LCID;
				this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
				this.dbType = DbType.String;
				this.type = typeof(string);
				break;
			case SqlDbType.NVarChar:
				maxLength = -1L;
				this.localeId = (long)Thread.CurrentThread.CurrentCulture.LCID;
				this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
				this.dbType = DbType.String;
				this.type = typeof(string);
				break;
			}
			IL_02BD:
			this.maxLength = maxLength;
			this.name = name;
			this.sqlDbType = sqlDbType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, and user-defined type (UDT).</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="userDefinedType" /> points to a type that does not have <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> declared. </exception>
		// Token: 0x0600117D RID: 4477 RVA: 0x00044D14 File Offset: 0x00042F14
		[MonoTODO]
		public SqlMetaData(string name, SqlDbType sqlDbType, Type userDefinedType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name can not be null");
			}
			if (sqlDbType != SqlDbType.Udt)
			{
				throw new ArgumentException("SqlDbType not supported");
			}
			this.maxLength = -1L;
			this.precision = 0;
			this.scale = 0;
			this.localeId = 0L;
			this.compareOptions = SqlCompareOptions.None;
			this.dbType = DbType.Guid;
			this.type = typeof(Guid);
			this.name = name;
			throw new NotImplementedException();
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, precision, and scale.</summary>
		/// <param name="name">The name of the parameter or column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="scale" /> was greater than <paramref name="precision" />. </exception>
		// Token: 0x0600117E RID: 4478 RVA: 0x00044DC8 File Offset: 0x00042FC8
		public SqlMetaData(string name, SqlDbType sqlDbType, byte precision, byte scale)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name can not be null");
			}
			if (sqlDbType != SqlDbType.Decimal)
			{
				throw new ArgumentException("SqlDbType not supported");
			}
			this.maxLength = 9L;
			this.precision = precision;
			this.scale = scale;
			this.localeId = 0L;
			this.compareOptions = SqlCompareOptions.None;
			this.dbType = DbType.Decimal;
			this.type = typeof(decimal);
			this.name = name;
			this.sqlDbType = sqlDbType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, locale, and compare options.</summary>
		/// <param name="name">The name of the parameter or column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type. </param>
		/// <param name="locale">The locale ID of the parameter or column.</param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x0600117F RID: 4479 RVA: 0x00044E80 File Offset: 0x00043080
		public SqlMetaData(string name, SqlDbType sqlDbType, long maxLength, long locale, SqlCompareOptions compareOptions)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name can not be null");
			}
			switch (sqlDbType)
			{
			case SqlDbType.NChar:
				this.dbType = DbType.StringFixedLength;
				this.type = typeof(char[]);
				break;
			case SqlDbType.NText:
			case SqlDbType.NVarChar:
				this.dbType = DbType.String;
				this.type = typeof(string);
				break;
			default:
				if (sqlDbType != SqlDbType.Char)
				{
					if (sqlDbType != SqlDbType.Text && sqlDbType != SqlDbType.VarChar)
					{
						throw new ArgumentException("SqlDbType not supported");
					}
					this.dbType = DbType.AnsiString;
					this.type = typeof(char[]);
				}
				else
				{
					this.dbType = DbType.AnsiStringFixedLength;
					this.type = typeof(char[]);
				}
				break;
			}
			this.compareOptions = compareOptions;
			this.localeId = locale;
			this.maxLength = maxLength;
			this.name = name;
			this.sqlDbType = sqlDbType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, database name, owning schema, and object name.</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="database">The database name of the XML schema collection of a typed XML instance.</param>
		/// <param name="owningSchema">The relational schema name of the XML schema collection of a typed XML instance.</param>
		/// <param name="objectName">The name of the XML schema collection of a typed XML instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null, or <paramref name="objectName" /> is null when <paramref name="database" /> and <paramref name="owningSchema" /> are non-null.</exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />.</exception>
		// Token: 0x06001180 RID: 4480 RVA: 0x00044FA4 File Offset: 0x000431A4
		public SqlMetaData(string name, SqlDbType sqlDbType, string database, string owningSchema, string objectName)
		{
			if ((name == null || objectName == null) && database != null && owningSchema != null)
			{
				throw new ArgumentNullException("name can not be null");
			}
			if (sqlDbType != SqlDbType.Xml)
			{
				throw new ArgumentException("SqlDbType not supported");
			}
			this.maxLength = -1L;
			this.precision = 0;
			this.scale = 0;
			this.localeId = 0L;
			this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
			this.dbType = DbType.String;
			this.type = typeof(string);
			this.name = name;
			this.sqlDbType = sqlDbType;
			this.databaseName = database;
			this.owningSchema = owningSchema;
			this.objectName = objectName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> class with the specified column name, type, maximum length, precision, scale, locale ID, compare options, and user-defined type (UDT).</summary>
		/// <param name="name">The name of the column.</param>
		/// <param name="dbType">The SQL Server type of the parameter or column.</param>
		/// <param name="maxLength">The maximum length of the specified type.</param>
		/// <param name="precision">The precision of the parameter or column.</param>
		/// <param name="scale">The scale of the parameter or column.</param>
		/// <param name="localeId"></param>
		/// <param name="compareOptions">The comparison rules of the parameter or column.</param>
		/// <param name="userDefinedType">A <see cref="T:System.Type" /> instance that points to the UDT.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="Name" /> is null. </exception>
		/// <exception cref="T:System.ArgumentException">A SqlDbType that is not allowed was passed to the constructor as <paramref name="dbType" />, or <paramref name="userDefinedType" /> points to a type that does not have <see cref="T:Microsoft.SqlServer.Server.SqlUserDefinedTypeAttribute" /> declared.</exception>
		// Token: 0x06001181 RID: 4481 RVA: 0x00045088 File Offset: 0x00043288
		public SqlMetaData(string name, SqlDbType sqlDbType, long maxLength, byte precision, byte scale, long localeId, SqlCompareOptions compareOptions, Type userDefinedType)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name can not be null");
			}
			this.compareOptions = compareOptions;
			this.localeId = localeId;
			this.maxLength = maxLength;
			this.precision = precision;
			this.scale = scale;
			switch (sqlDbType)
			{
			case SqlDbType.BigInt:
				maxLength = 8L;
				precision = 19;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Int64;
				this.type = typeof(long);
				goto IL_04B2;
			case SqlDbType.Bit:
				maxLength = 1L;
				precision = 1;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Boolean;
				this.type = typeof(bool);
				goto IL_04B2;
			case SqlDbType.DateTime:
				maxLength = 8L;
				precision = 23;
				scale = 3;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.DateTime;
				this.type = typeof(DateTime);
				goto IL_04B2;
			case SqlDbType.Decimal:
				maxLength = 9L;
				precision = 18;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Decimal;
				this.type = typeof(decimal);
				goto IL_04B2;
			case SqlDbType.Float:
				maxLength = 8L;
				precision = 53;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Decimal;
				this.type = typeof(float);
				goto IL_04B2;
			case SqlDbType.Image:
				maxLength = -1L;
				precision = 0;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Binary;
				this.type = typeof(byte[]);
				goto IL_04B2;
			case SqlDbType.Int:
				maxLength = 4L;
				precision = 10;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Int32;
				this.type = typeof(int);
				goto IL_04B2;
			case SqlDbType.Money:
				maxLength = 8L;
				precision = 19;
				scale = 4;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Currency;
				this.type = typeof(decimal);
				goto IL_04B2;
			case SqlDbType.NText:
				maxLength = -1L;
				precision = 0;
				scale = 0;
				localeId = (long)Thread.CurrentThread.CurrentCulture.LCID;
				compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
				this.dbType = DbType.String;
				this.type = typeof(string);
				goto IL_04B2;
			case SqlDbType.Real:
				maxLength = 4L;
				precision = 24;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Single;
				this.type = typeof(float);
				goto IL_04B2;
			case SqlDbType.UniqueIdentifier:
				maxLength = 16L;
				precision = 0;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Guid;
				this.type = typeof(Guid);
				goto IL_04B2;
			case SqlDbType.SmallDateTime:
				maxLength = 4L;
				precision = 16;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.DateTime;
				this.type = typeof(DateTime);
				goto IL_04B2;
			case SqlDbType.SmallInt:
				maxLength = 2L;
				precision = 5;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Int16;
				this.type = typeof(short);
				goto IL_04B2;
			case SqlDbType.SmallMoney:
				maxLength = 4L;
				precision = 10;
				scale = 4;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Currency;
				this.type = typeof(decimal);
				goto IL_04B2;
			case SqlDbType.Text:
				maxLength = -1L;
				precision = 0;
				scale = 0;
				localeId = (long)Thread.CurrentThread.CurrentCulture.LCID;
				compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
				this.dbType = DbType.AnsiString;
				this.type = typeof(char[]);
				goto IL_04B2;
			case SqlDbType.Timestamp:
				maxLength = 8L;
				precision = 0;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Byte;
				this.type = typeof(byte[]);
				goto IL_04B2;
			case SqlDbType.TinyInt:
				maxLength = 1L;
				precision = 3;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Int16;
				this.type = typeof(short);
				goto IL_04B2;
			case SqlDbType.Variant:
				maxLength = 8016L;
				precision = 0;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Object;
				this.type = typeof(object);
				goto IL_04B2;
			case SqlDbType.Xml:
				maxLength = -1L;
				precision = 0;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
				this.dbType = DbType.Xml;
				this.type = typeof(string);
				goto IL_04B2;
			case SqlDbType.Udt:
				maxLength = -1L;
				precision = 0;
				scale = 0;
				localeId = 0L;
				compareOptions = SqlCompareOptions.None;
				this.dbType = DbType.Object;
				this.type = typeof(object);
				goto IL_04B2;
			}
			throw new ArgumentException("SqlDbType not supported");
			IL_04B2:
			this.name = name;
			this.sqlDbType = sqlDbType;
		}

		/// <summary>Gets the comparison rules used for the column or parameter.</summary>
		/// <returns>The comparison rules used for the column or parameter as a <see cref="T:System.Data.SqlTypes.SqlCompareOptions" />.</returns>
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06001182 RID: 4482 RVA: 0x00045558 File Offset: 0x00043758
		public SqlCompareOptions CompareOptions
		{
			get
			{
				return this.compareOptions;
			}
		}

		/// <summary>Gets the data type of the column or parameter.</summary>
		/// <returns>The data type of the column or parameter as a <see cref="T:System.Data.DbType" />.</returns>
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x00045560 File Offset: 0x00043760
		public DbType DbType
		{
			get
			{
				return this.dbType;
			}
		}

		/// <summary>Gets the locale ID of the column or parameter.</summary>
		/// <returns>The locale ID of the column or parameter as a <see cref="T:System.Int64" />.</returns>
		// Token: 0x170002FD RID: 765
		// (get) Token: 0x06001184 RID: 4484 RVA: 0x00045568 File Offset: 0x00043768
		public long LocaleId
		{
			get
			{
				return this.localeId;
			}
		}

		/// <summary>Gets the length of text, ntext, and image data types. </summary>
		/// <returns>The length of text, ntext, and image data types.</returns>
		// Token: 0x170002FE RID: 766
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x00045570 File Offset: 0x00043770
		public static long Max
		{
			get
			{
				return -1L;
			}
		}

		/// <summary>Gets the maximum length of the column or parameter.</summary>
		/// <returns>The maximum length of the column or parameter as a <see cref="T:System.Int64" />.</returns>
		// Token: 0x170002FF RID: 767
		// (get) Token: 0x06001186 RID: 4486 RVA: 0x00045574 File Offset: 0x00043774
		public long MaxLength
		{
			get
			{
				return this.maxLength;
			}
		}

		/// <summary>Gets the name of the column or parameter.</summary>
		/// <returns>The name of the column or parameter as a <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="Name" /> specified in the constructor is longer than 128 characters. </exception>
		// Token: 0x17000300 RID: 768
		// (get) Token: 0x06001187 RID: 4487 RVA: 0x0004557C File Offset: 0x0004377C
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the precision of the column or parameter.</summary>
		/// <returns>The precision of the column or parameter as a <see cref="T:System.Byte" />.</returns>
		// Token: 0x17000301 RID: 769
		// (get) Token: 0x06001188 RID: 4488 RVA: 0x00045584 File Offset: 0x00043784
		public byte Precision
		{
			get
			{
				return this.precision;
			}
		}

		/// <summary>Gets the scale of the column or parameter.</summary>
		/// <returns>The scale of the column or parameter.</returns>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06001189 RID: 4489 RVA: 0x0004558C File Offset: 0x0004378C
		public byte Scale
		{
			get
			{
				return this.scale;
			}
		}

		/// <summary>Gets the data type of the column or parameter.</summary>
		/// <returns>The data type of the column or parameter as a <see cref="T:System.Data.DbType" />.</returns>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x0600118A RID: 4490 RVA: 0x00045594 File Offset: 0x00043794
		public SqlDbType SqlDbType
		{
			get
			{
				return this.sqlDbType;
			}
		}

		/// <summary>Gets the name of the database where the schema collection for this XML instance is located.</summary>
		/// <returns>The name of the database where the schema collection for this XML instance is located as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x0600118B RID: 4491 RVA: 0x0004559C File Offset: 0x0004379C
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				return this.databaseName;
			}
		}

		/// <summary>Gets the name of the schema collection for this XML instance.</summary>
		/// <returns>The name of the schema collection for this XML instance as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x0600118C RID: 4492 RVA: 0x000455A4 File Offset: 0x000437A4
		public string XmlSchemaCollectionName
		{
			get
			{
				return this.objectName;
			}
		}

		/// <summary>Gets the owning relational schema where the schema collection for this XML instance is located.</summary>
		/// <returns>The owning relational schema where the schema collection for this XML instance is located as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x0600118D RID: 4493 RVA: 0x000455AC File Offset: 0x000437AC
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				return this.owningSchema;
			}
		}

		/// <summary>Gets the three-part name of the user-defined type (UDT) or the SQL Server type represented by the instance.</summary>
		/// <returns>The name of the UDT or SQL Server type as a <see cref="T:System.String" />.</returns>
		// Token: 0x17000307 RID: 775
		// (get) Token: 0x0600118E RID: 4494 RVA: 0x000455B4 File Offset: 0x000437B4
		[MonoTODO]
		public string TypeName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Validates the specified <see cref="T:System.Boolean" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Boolean" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x0600118F RID: 4495 RVA: 0x000455BC File Offset: 0x000437BC
		public bool Adjust(bool value)
		{
			if (this.type != typeof(bool))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Byte" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Byte" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06001190 RID: 4496 RVA: 0x000455E0 File Offset: 0x000437E0
		public byte Adjust(byte value)
		{
			if (this.type != typeof(byte))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified array of <see cref="T:System.Byte" /> values against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as an array of <see cref="T:System.Byte" /> values.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06001191 RID: 4497 RVA: 0x00045604 File Offset: 0x00043804
		public byte[] Adjust(byte[] value)
		{
			if (this.type != typeof(byte[]))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Char" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Char" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06001192 RID: 4498 RVA: 0x00045628 File Offset: 0x00043828
		public char Adjust(char value)
		{
			if (this.type != typeof(char))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified array of <see cref="T:System.Char" /> values against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as an array <see cref="T:System.Char" /> values.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06001193 RID: 4499 RVA: 0x0004564C File Offset: 0x0004384C
		public char[] Adjust(char[] value)
		{
			if (this.type != typeof(char[]))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.DateTime" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.DateTime" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06001194 RID: 4500 RVA: 0x00045670 File Offset: 0x00043870
		public DateTime Adjust(DateTime value)
		{
			if (this.type != typeof(DateTime))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Decimal" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Decimal" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06001195 RID: 4501 RVA: 0x00045694 File Offset: 0x00043894
		public decimal Adjust(decimal value)
		{
			if (this.type != typeof(decimal))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Double" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Double" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06001196 RID: 4502 RVA: 0x000456B8 File Offset: 0x000438B8
		public double Adjust(double value)
		{
			if (this.type != typeof(double))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Guid" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Guid" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06001197 RID: 4503 RVA: 0x000456DC File Offset: 0x000438DC
		public Guid Adjust(Guid value)
		{
			if (this.type != typeof(Guid))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Int16" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Int16" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06001198 RID: 4504 RVA: 0x00045700 File Offset: 0x00043900
		public short Adjust(short value)
		{
			if (this.type != typeof(short))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Int32" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Int32" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x06001199 RID: 4505 RVA: 0x00045724 File Offset: 0x00043924
		public int Adjust(int value)
		{
			if (this.type != typeof(int))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Int64" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Int64" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x0600119A RID: 4506 RVA: 0x00045748 File Offset: 0x00043948
		public long Adjust(long value)
		{
			if (this.type != typeof(long))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Object" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Object" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x0600119B RID: 4507 RVA: 0x0004576C File Offset: 0x0004396C
		public object Adjust(object value)
		{
			if (this.type != typeof(object))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Single" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Single" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x0600119C RID: 4508 RVA: 0x00045790 File Offset: 0x00043990
		public float Adjust(float value)
		{
			if (this.type != typeof(float))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBinary" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x0600119D RID: 4509 RVA: 0x000457B4 File Offset: 0x000439B4
		public SqlBinary Adjust(SqlBinary value)
		{
			if (this.type != typeof(byte[]))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x0600119E RID: 4510 RVA: 0x000457D8 File Offset: 0x000439D8
		public SqlBoolean Adjust(SqlBoolean value)
		{
			if (this.type != typeof(bool))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlByte" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlByte" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x0600119F RID: 4511 RVA: 0x000457FC File Offset: 0x000439FC
		public SqlByte Adjust(SqlByte value)
		{
			if (this.type != typeof(byte))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlBytes" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlBytes" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011A0 RID: 4512 RVA: 0x00045820 File Offset: 0x00043A20
		public SqlBytes Adjust(SqlBytes value)
		{
			if (this.type != typeof(byte[]))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlChars" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlChars" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011A1 RID: 4513 RVA: 0x00045844 File Offset: 0x00043A44
		public SqlChars Adjust(SqlChars value)
		{
			if (this.type != typeof(char[]))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDateTime" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011A2 RID: 4514 RVA: 0x00045868 File Offset: 0x00043A68
		public SqlDateTime Adjust(SqlDateTime value)
		{
			if (this.type != typeof(DateTime))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011A3 RID: 4515 RVA: 0x0004588C File Offset: 0x00043A8C
		public SqlDecimal Adjust(SqlDecimal value)
		{
			if (this.type != typeof(decimal))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlDouble" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011A4 RID: 4516 RVA: 0x000458B0 File Offset: 0x00043AB0
		public SqlDouble Adjust(SqlDouble value)
		{
			if (this.type != typeof(double))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlGuid" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011A5 RID: 4517 RVA: 0x000458D4 File Offset: 0x00043AD4
		public SqlGuid Adjust(SqlGuid value)
		{
			if (this.type != typeof(Guid))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt16" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011A6 RID: 4518 RVA: 0x000458F8 File Offset: 0x00043AF8
		public SqlInt16 Adjust(SqlInt16 value)
		{
			if (this.type != typeof(short))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt32" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011A7 RID: 4519 RVA: 0x0004591C File Offset: 0x00043B1C
		public SqlInt32 Adjust(SqlInt32 value)
		{
			if (this.type != typeof(int))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlInt64" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlInt64" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011A8 RID: 4520 RVA: 0x00045940 File Offset: 0x00043B40
		public SqlInt64 Adjust(SqlInt64 value)
		{
			if (this.type != typeof(long))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlMoney" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlMoney" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011A9 RID: 4521 RVA: 0x00045964 File Offset: 0x00043B64
		public SqlMoney Adjust(SqlMoney value)
		{
			if (this.type != typeof(decimal))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlSingle" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011AA RID: 4522 RVA: 0x00045988 File Offset: 0x00043B88
		public SqlSingle Adjust(SqlSingle value)
		{
			if (this.type != typeof(float))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.Data.SqlTypes.SqlString" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011AB RID: 4523 RVA: 0x000459AC File Offset: 0x00043BAC
		public SqlString Adjust(SqlString value)
		{
			if (this.type != typeof(string))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Validates the specified <see cref="T:System.String" /> value against the metadata, and adjusts the value if necessary.</summary>
		/// <returns>The adjusted value as a <see cref="T:System.String" />.</returns>
		/// <param name="value">The value to validate against the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="Value" /> does not match the <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> type, or <paramref name="value" /> could not be adjusted. </exception>
		// Token: 0x060011AC RID: 4524 RVA: 0x000459D0 File Offset: 0x00043BD0
		public string Adjust(string value)
		{
			if (this.type != typeof(string))
			{
				throw new ArgumentException("Value does not match the SqlMetaData type");
			}
			return value;
		}

		/// <summary>Infers the metadata from the specified object and returns it as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</summary>
		/// <returns>The inferred metadata as a <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</returns>
		/// <param name="value">The object used from which the metadata is inferred.</param>
		/// <param name="name">The name assigned to the returned <see cref="T:Microsoft.SqlServer.Server.SqlMetaData" /> instance.</param>
		/// <exception cref="T:System.ArgumentNullException">The v<paramref name="alue" /> is null. </exception>
		// Token: 0x060011AD RID: 4525 RVA: 0x000459F4 File Offset: 0x00043BF4
		public static SqlMetaData InferFromValue(object value, string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name can not be null");
			}
			if (value == null)
			{
				throw new ArgumentException("value can not be null");
			}
			string text = value.GetType().ToString();
			switch (text)
			{
			case "System.Boolean":
				return new SqlMetaData(name, SqlDbType.Bit);
			case "System.Byte":
				return new SqlMetaData(name, SqlDbType.Binary);
			case "System.Byte[]":
				return new SqlMetaData(name, SqlDbType.VarBinary);
			case "System.Char":
				return new SqlMetaData(name, SqlDbType.Char);
			case "System.Char[]":
				return new SqlMetaData(name, SqlDbType.VarChar);
			case "System.DateTime":
				return new SqlMetaData(name, SqlDbType.DateTime);
			case "System.Decimal":
				return new SqlMetaData(name, SqlDbType.Decimal);
			case "System.Double":
				return new SqlMetaData(name, SqlDbType.Float);
			case "System.Guid":
				return new SqlMetaData(name, SqlDbType.UniqueIdentifier);
			case "System.Int16":
				return new SqlMetaData(name, SqlDbType.SmallInt);
			case "System.Int32":
				return new SqlMetaData(name, SqlDbType.Int);
			case "System.Int64":
				return new SqlMetaData(name, SqlDbType.BigInt);
			case "System.Single":
				return new SqlMetaData(name, SqlDbType.Real);
			case "System.String":
				return new SqlMetaData(name, SqlDbType.NVarChar);
			}
			return new SqlMetaData(name, SqlDbType.Variant);
		}

		// Token: 0x0400067E RID: 1662
		private SqlCompareOptions compareOptions;

		// Token: 0x0400067F RID: 1663
		private string databaseName;

		// Token: 0x04000680 RID: 1664
		private long localeId;

		// Token: 0x04000681 RID: 1665
		private long maxLength;

		// Token: 0x04000682 RID: 1666
		private string name;

		// Token: 0x04000683 RID: 1667
		private byte precision = 10;

		// Token: 0x04000684 RID: 1668
		private byte scale;

		// Token: 0x04000685 RID: 1669
		private string owningSchema;

		// Token: 0x04000686 RID: 1670
		private string objectName;

		// Token: 0x04000687 RID: 1671
		private SqlDbType sqlDbType = SqlDbType.NVarChar;

		// Token: 0x04000688 RID: 1672
		private DbType dbType = DbType.String;

		// Token: 0x04000689 RID: 1673
		private Type type = typeof(string);
	}
}
