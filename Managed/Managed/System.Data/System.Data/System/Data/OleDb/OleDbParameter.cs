using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Represents a parameter to an <see cref="T:System.Data.OleDb.OleDbCommand" /> and optionally its mapping to a <see cref="T:System.Data.DataSet" /> column. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000F7 RID: 247
	[TypeConverter("System.Data.OleDb.OleDbParameter+OleDbParameterConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public sealed class OleDbParameter : DbParameter, IDataParameter, IDbDataParameter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class.</summary>
		// Token: 0x06000BD2 RID: 3026 RVA: 0x000337EC File Offset: 0x000319EC
		public OleDbParameter()
		{
			this.name = string.Empty;
			this.isNullable = true;
			this.sourceColumn = string.Empty;
			this.gdaParameter = IntPtr.Zero;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name and the value of the new <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="value">The value of the new <see cref="T:System.Data.OleDb.OleDbParameter" /> object. </param>
		// Token: 0x06000BD3 RID: 3027 RVA: 0x00033828 File Offset: 0x00031A28
		public OleDbParameter(string name, object value)
			: this()
		{
			this.name = name;
			this.value = value;
			this.OleDbType = this.GetOleDbType(value);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name and data type.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000BD4 RID: 3028 RVA: 0x0003384C File Offset: 0x00031A4C
		public OleDbParameter(string name, OleDbType dataType)
			: this()
		{
			this.name = name;
			this.OleDbType = dataType;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, and length.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000BD5 RID: 3029 RVA: 0x00033864 File Offset: 0x00031A64
		public OleDbParameter(string name, OleDbType dataType, int size)
			: this(name, dataType)
		{
			this.size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, and source column name.</summary>
		/// <param name="name">The name of the parameter to map. </param>
		/// <param name="dataType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="srcColumn">The name of the source column. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000BD6 RID: 3030 RVA: 0x00033878 File Offset: 0x00031A78
		public OleDbParameter(string name, OleDbType dataType, int size, string srcColumn)
			: this(name, dataType, size)
		{
			this.sourceColumn = srcColumn;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="isNullable">true if the value of the field can be null; otherwise false. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. </param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. </param>
		/// <param name="srcColumn">The name of the source column. </param>
		/// <param name="srcVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.OleDb.OleDbParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000BD7 RID: 3031 RVA: 0x0003388C File Offset: 0x00031A8C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OleDbParameter(string parameterName, OleDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value)
			: this(parameterName, dbType, size, srcColumn)
		{
			this.direction = direction;
			this.isNullable = isNullable;
			this.precision = precision;
			this.scale = scale;
			this.sourceVersion = srcVersion;
			this.value = value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.OleDb.OleDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</param>
		/// <param name="sourceColumn">The name of the source column.</param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values.</param>
		/// <param name="sourceColumnNullMapping">true if the source column is nullable; false if it is not.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.OleDb.OleDbParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dataType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06000BD8 RID: 3032 RVA: 0x000338CC File Offset: 0x00031ACC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OleDbParameter(string parameterName, OleDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value)
			: this(parameterName, dbType, size, sourceColumn)
		{
			this.direction = direction;
			this.precision = precision;
			this.scale = scale;
			this.sourceVersion = sourceVersion;
			this.sourceColumnNullMapping = sourceColumnNullMapping;
			this.value = value;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		// Token: 0x06000BD9 RID: 3033 RVA: 0x0003390C File Offset: 0x00031B0C
		[MonoTODO]
		object ICloneable.Clone()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="F:System.Data.DbType.String" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x06000BDA RID: 3034 RVA: 0x00033914 File Offset: 0x00031B14
		// (set) Token: 0x06000BDB RID: 3035 RVA: 0x0003391C File Offset: 0x00031B1C
		[DataCategory("DataCategory_Data")]
		public override DbType DbType
		{
			get
			{
				return this.dbType;
			}
			set
			{
				this.dbType = value;
				this.oleDbType = this.DbTypeToOleDbType(value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return-value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000BDC RID: 3036 RVA: 0x00033934 File Offset: 0x00031B34
		// (set) Token: 0x06000BDD RID: 3037 RVA: 0x0003393C File Offset: 0x00031B3C
		[RefreshProperties(RefreshProperties.All)]
		[DataCategory("DataCategory_Data")]
		public override ParameterDirection Direction
		{
			get
			{
				return this.direction;
			}
			set
			{
				this.direction = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x00033948 File Offset: 0x00031B48
		// (set) Token: 0x06000BDF RID: 3039 RVA: 0x00033950 File Offset: 0x00031B50
		public override bool IsNullable
		{
			get
			{
				return this.isNullable;
			}
			set
			{
				this.isNullable = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbType" /> of the parameter.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbType" /> of the parameter. The default is <see cref="F:System.Data.OleDb.OleDbType.VarWChar" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0003395C File Offset: 0x00031B5C
		// (set) Token: 0x06000BE1 RID: 3041 RVA: 0x00033964 File Offset: 0x00031B64
		[RefreshProperties(RefreshProperties.All)]
		[DbProviderSpecificTypeProperty(true)]
		[DataCategory("DataCategory_Data")]
		public OleDbType OleDbType
		{
			get
			{
				return this.oleDbType;
			}
			set
			{
				this.oleDbType = value;
				this.dbType = this.OleDbTypeToDbType(value);
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.OleDb.OleDbParameter" />. The default is an empty string ("").</returns>
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000BE2 RID: 3042 RVA: 0x0003397C File Offset: 0x00031B7C
		// (set) Token: 0x06000BE3 RID: 3043 RVA: 0x00033984 File Offset: 0x00031B84
		public override string ParameterName
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		/// <summary>Gets or sets the maximum number of digits used to represent the <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> property. The default value is 0, which indicates that the data provider sets the precision for <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000BE4 RID: 3044 RVA: 0x00033990 File Offset: 0x00031B90
		// (set) Token: 0x06000BE5 RID: 3045 RVA: 0x00033998 File Offset: 0x00031B98
		[DataCategory("DataCategory_Data")]
		[DefaultValue(0)]
		public byte Precision
		{
			get
			{
				return this.precision;
			}
			set
			{
				this.precision = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000BE6 RID: 3046 RVA: 0x000339A4 File Offset: 0x00031BA4
		// (set) Token: 0x06000BE7 RID: 3047 RVA: 0x000339AC File Offset: 0x00031BAC
		[DefaultValue(0)]
		[DataCategory("DataCategory_Data")]
		public byte Scale
		{
			get
			{
				return this.scale;
			}
			set
			{
				this.scale = value;
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000BE8 RID: 3048 RVA: 0x000339B8 File Offset: 0x00031BB8
		// (set) Token: 0x06000BE9 RID: 3049 RVA: 0x000339C0 File Offset: 0x00031BC0
		[DataCategory("DataCategory_Data")]
		public override int Size
		{
			get
			{
				return this.size;
			}
			set
			{
				this.size = value;
			}
		}

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000BEA RID: 3050 RVA: 0x000339CC File Offset: 0x00031BCC
		// (set) Token: 0x06000BEB RID: 3051 RVA: 0x000339D4 File Offset: 0x00031BD4
		[DataCategory("DataCategory_Data")]
		public override string SourceColumn
		{
			get
			{
				return this.sourceColumn;
			}
			set
			{
				this.sourceColumn = value;
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.Common.DbCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000BEC RID: 3052 RVA: 0x000339E0 File Offset: 0x00031BE0
		// (set) Token: 0x06000BED RID: 3053 RVA: 0x000339E8 File Offset: 0x00031BE8
		public override bool SourceColumnNullMapping
		{
			get
			{
				return this.sourceColumnNullMapping;
			}
			set
			{
				this.sourceColumnNullMapping = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.OleDb.OleDbParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the <see cref="T:System.Data.DataRowVersion" /> values.</exception>
		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000BEE RID: 3054 RVA: 0x000339F4 File Offset: 0x00031BF4
		// (set) Token: 0x06000BEF RID: 3055 RVA: 0x000339FC File Offset: 0x00031BFC
		[DataCategory("DataCategory_Data")]
		public override DataRowVersion SourceVersion
		{
			get
			{
				return this.sourceVersion;
			}
			set
			{
				this.sourceVersion = value;
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x17000240 RID: 576
		// (get) Token: 0x06000BF0 RID: 3056 RVA: 0x00033A08 File Offset: 0x00031C08
		// (set) Token: 0x06000BF1 RID: 3057 RVA: 0x00033A10 File Offset: 0x00031C10
		[DataCategory("DataCategory_Data")]
		[TypeConverter(typeof(StringConverter))]
		[RefreshProperties(RefreshProperties.All)]
		public override object Value
		{
			get
			{
				return this.value;
			}
			set
			{
				this.value = value;
			}
		}

		// Token: 0x17000241 RID: 577
		// (get) Token: 0x06000BF2 RID: 3058 RVA: 0x00033A1C File Offset: 0x00031C1C
		// (set) Token: 0x06000BF3 RID: 3059 RVA: 0x00033A24 File Offset: 0x00031C24
		internal OleDbParameterCollection Container
		{
			get
			{
				return this.container;
			}
			set
			{
				this.container = value;
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x06000BF4 RID: 3060 RVA: 0x00033A30 File Offset: 0x00031C30
		internal IntPtr GdaParameter
		{
			get
			{
				return this.gdaParameter;
			}
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BF5 RID: 3061 RVA: 0x00033A38 File Offset: 0x00031C38
		public override void ResetDbType()
		{
			this.ResetOleDbType();
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BF6 RID: 3062 RVA: 0x00033A40 File Offset: 0x00031C40
		public void ResetOleDbType()
		{
			this.oleDbType = this.GetOleDbType(this.Value);
			this.dbType = this.OleDbTypeToDbType(this.oleDbType);
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.OleDb.OleDbParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.OleDb.OleDbParameter.ParameterName" />.</returns>
		// Token: 0x06000BF7 RID: 3063 RVA: 0x00033A74 File Offset: 0x00031C74
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00033A7C File Offset: 0x00031C7C
		private OleDbType DbTypeToOleDbType(DbType dbType)
		{
			switch (dbType)
			{
			case DbType.AnsiString:
				return OleDbType.VarChar;
			case DbType.Binary:
				return OleDbType.Binary;
			case DbType.Byte:
				return OleDbType.UnsignedTinyInt;
			case DbType.Boolean:
				return OleDbType.Boolean;
			case DbType.Currency:
				return OleDbType.Currency;
			case DbType.Date:
				return OleDbType.Date;
			case DbType.DateTime:
				throw new NotImplementedException();
			case DbType.Decimal:
				return OleDbType.Decimal;
			case DbType.Double:
				return OleDbType.Double;
			case DbType.Guid:
				return OleDbType.Guid;
			case DbType.Int16:
				return OleDbType.SmallInt;
			case DbType.Int32:
				return OleDbType.Integer;
			case DbType.Int64:
				return OleDbType.BigInt;
			case DbType.Object:
				return OleDbType.Variant;
			case DbType.SByte:
				return OleDbType.TinyInt;
			case DbType.Single:
				return OleDbType.Single;
			case DbType.String:
				return OleDbType.WChar;
			case DbType.Time:
				throw new NotImplementedException();
			case DbType.UInt16:
				return OleDbType.UnsignedSmallInt;
			case DbType.UInt32:
				return OleDbType.UnsignedInt;
			case DbType.UInt64:
				return OleDbType.UnsignedBigInt;
			case DbType.VarNumeric:
				return OleDbType.VarNumeric;
			case DbType.AnsiStringFixedLength:
				return OleDbType.Char;
			case DbType.StringFixedLength:
				return OleDbType.VarWChar;
			default:
				return OleDbType.Variant;
			}
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x00033B54 File Offset: 0x00031D54
		private DbType OleDbTypeToDbType(OleDbType oleDbType)
		{
			switch (oleDbType)
			{
			case OleDbType.Empty:
				throw new NotImplementedException();
			default:
				switch (oleDbType)
				{
				case OleDbType.Binary:
					return DbType.Binary;
				case OleDbType.Char:
					return DbType.AnsiStringFixedLength;
				case OleDbType.WChar:
					return DbType.String;
				case OleDbType.Numeric:
					return DbType.Decimal;
				default:
					switch (oleDbType)
					{
					case OleDbType.VarChar:
						return DbType.AnsiString;
					case OleDbType.LongVarChar:
						return DbType.AnsiString;
					case OleDbType.VarWChar:
						return DbType.StringFixedLength;
					case OleDbType.LongVarWChar:
						return DbType.String;
					case OleDbType.VarBinary:
						return DbType.Binary;
					case OleDbType.LongVarBinary:
						return DbType.Binary;
					default:
						if (oleDbType == OleDbType.Filetime)
						{
							return DbType.DateTime;
						}
						if (oleDbType != OleDbType.Guid)
						{
							return DbType.Object;
						}
						return DbType.Guid;
					}
					break;
				case OleDbType.DBDate:
					return DbType.DateTime;
				case OleDbType.DBTime:
					throw new NotImplementedException();
				case OleDbType.DBTimeStamp:
					return DbType.DateTime;
				case OleDbType.PropVariant:
					return DbType.Object;
				case OleDbType.VarNumeric:
					return DbType.VarNumeric;
				}
				break;
			case OleDbType.SmallInt:
				return DbType.Int16;
			case OleDbType.Integer:
				return DbType.Int32;
			case OleDbType.Single:
				return DbType.Single;
			case OleDbType.Double:
				return DbType.Double;
			case OleDbType.Currency:
				return DbType.Currency;
			case OleDbType.Date:
				return DbType.DateTime;
			case OleDbType.BSTR:
				return DbType.AnsiString;
			case OleDbType.IDispatch:
				return DbType.Object;
			case OleDbType.Error:
				throw new NotImplementedException();
			case OleDbType.Boolean:
				return DbType.Boolean;
			case OleDbType.Variant:
				return DbType.Object;
			case OleDbType.IUnknown:
				return DbType.Object;
			case OleDbType.Decimal:
				return DbType.Decimal;
			case OleDbType.TinyInt:
				return DbType.SByte;
			case OleDbType.UnsignedTinyInt:
				return DbType.Byte;
			case OleDbType.UnsignedSmallInt:
				return DbType.UInt16;
			case OleDbType.UnsignedInt:
				return DbType.UInt32;
			case OleDbType.BigInt:
				return DbType.Int64;
			case OleDbType.UnsignedBigInt:
				return DbType.UInt64;
			}
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00033CA0 File Offset: 0x00031EA0
		private OleDbType GetOleDbType(object value)
		{
			if (value is Guid)
			{
				return OleDbType.Guid;
			}
			if (value is TimeSpan)
			{
				return OleDbType.DBTime;
			}
			switch (Type.GetTypeCode(value.GetType()))
			{
			case TypeCode.Empty:
				return OleDbType.Empty;
			case TypeCode.Object:
				return OleDbType.Variant;
			case TypeCode.DBNull:
				return OleDbType.Empty;
			case TypeCode.Boolean:
				return OleDbType.Boolean;
			case TypeCode.Char:
				return OleDbType.Char;
			case TypeCode.SByte:
				return OleDbType.TinyInt;
			case TypeCode.Byte:
				if (value.GetType().IsArray)
				{
					return OleDbType.Binary;
				}
				return OleDbType.UnsignedTinyInt;
			case TypeCode.Int16:
				return OleDbType.SmallInt;
			case TypeCode.UInt16:
				return OleDbType.UnsignedSmallInt;
			case TypeCode.Int32:
				return OleDbType.Integer;
			case TypeCode.UInt32:
				return OleDbType.UnsignedInt;
			case TypeCode.Int64:
				return OleDbType.BigInt;
			case TypeCode.UInt64:
				return OleDbType.UnsignedBigInt;
			case TypeCode.Single:
				return OleDbType.Single;
			case TypeCode.Double:
				return OleDbType.Double;
			case TypeCode.Decimal:
				return OleDbType.Decimal;
			case TypeCode.DateTime:
				return OleDbType.Date;
			case TypeCode.String:
				return OleDbType.VarChar;
			}
			return OleDbType.IUnknown;
		}

		// Token: 0x04000467 RID: 1127
		private string name;

		// Token: 0x04000468 RID: 1128
		private object value;

		// Token: 0x04000469 RID: 1129
		private int size;

		// Token: 0x0400046A RID: 1130
		private bool isNullable;

		// Token: 0x0400046B RID: 1131
		private byte precision;

		// Token: 0x0400046C RID: 1132
		private byte scale;

		// Token: 0x0400046D RID: 1133
		private DataRowVersion sourceVersion;

		// Token: 0x0400046E RID: 1134
		private string sourceColumn;

		// Token: 0x0400046F RID: 1135
		private bool sourceColumnNullMapping;

		// Token: 0x04000470 RID: 1136
		private ParameterDirection direction;

		// Token: 0x04000471 RID: 1137
		private OleDbType oleDbType;

		// Token: 0x04000472 RID: 1138
		private DbType dbType;

		// Token: 0x04000473 RID: 1139
		private OleDbParameterCollection container;

		// Token: 0x04000474 RID: 1140
		private IntPtr gdaParameter;
	}
}
