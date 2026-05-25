using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Globalization;
using System.Xml;
using Mono.Data.Tds;
using Mono.Data.Tds.Protocol;

namespace System.Data.SqlClient
{
	/// <summary>Represents a parameter to a <see cref="T:System.Data.SqlClient.SqlCommand" /> and optionally its mapping to <see cref="T:System.Data.DataSet" /> columns. This class cannot be inherited. For more information on parameters, see Configuring Parameters and Parameter Data Types (ADO.NET).</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000171 RID: 369
	[TypeConverter("System.Data.SqlClient.SqlParameter+SqlParameterConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public sealed class SqlParameter : DbParameter, IDataParameter, IDbDataParameter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class.</summary>
		// Token: 0x060013A2 RID: 5026 RVA: 0x00051CD8 File Offset: 0x0004FED8
		public SqlParameter()
			: this(string.Empty, SqlDbType.NVarChar, 0, ParameterDirection.Input, false, 0, 0, string.Empty, DataRowVersion.Current, null)
		{
			this.isTypeSet = false;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name and a value of the new <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		/// <param name="parameterName">The name of the parameter to map. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.SqlClient.SqlParameter" />. </param>
		// Token: 0x060013A3 RID: 5027 RVA: 0x00051D0C File Offset: 0x0004FF0C
		public SqlParameter(string parameterName, object value)
		{
			this.direction = ParameterDirection.Input;
			this.xmlSchemaCollectionDatabase = string.Empty;
			this.xmlSchemaCollectionOwningSchema = string.Empty;
			this.xmlSchemaCollectionName = string.Empty;
			base..ctor();
			if (parameterName == null)
			{
				parameterName = string.Empty;
			}
			this.metaParameter = new TdsMetaParameter(parameterName, new FrameworkValueGetter(this.GetFrameworkValue));
			this.metaParameter.RawValue = value;
			this.InferSqlType(value);
			this.sourceVersion = DataRowVersion.Current;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name and the data type.</summary>
		/// <param name="parameterName">The name of the parameter to map. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x060013A4 RID: 5028 RVA: 0x00051D8C File Offset: 0x0004FF8C
		public SqlParameter(string parameterName, SqlDbType dbType)
			: this(parameterName, dbType, 0, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the <see cref="T:System.Data.SqlDbType" />, and the size.</summary>
		/// <param name="parameterName">The name of the parameter to map. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x060013A5 RID: 5029 RVA: 0x00051DB0 File Offset: 0x0004FFB0
		public SqlParameter(string parameterName, SqlDbType dbType, int size)
			: this(parameterName, dbType, size, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the <see cref="T:System.Data.SqlDbType" />, the size, and the source column name.</summary>
		/// <param name="parameterName">The name of the parameter to map. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="sourceColumn">The name of the source column. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x060013A6 RID: 5030 RVA: 0x00051DD4 File Offset: 0x0004FFD4
		public SqlParameter(string parameterName, SqlDbType dbType, int size, string sourceColumn)
			: this(parameterName, dbType, size, ParameterDirection.Input, false, 0, 0, sourceColumn, DataRowVersion.Current, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the type of the parameter, the size of the parameter, a <see cref="T:System.Data.ParameterDirection" />, the precision of the parameter, the scale of the parameter, the source column, a <see cref="T:System.Data.DataRowVersion" /> to use, and the value of the parameter.</summary>
		/// <param name="parameterName">The name of the parameter to map. </param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="isNullable">true if the value of the field can be null; otherwise false. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved. </param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved. </param>
		/// <param name="sourceColumn">The name of the source column. </param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.SqlClient.SqlParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="dbType" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x060013A7 RID: 5031 RVA: 0x00051DF8 File Offset: 0x0004FFF8
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, bool isNullable, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
		{
			this.direction = ParameterDirection.Input;
			this.xmlSchemaCollectionDatabase = string.Empty;
			this.xmlSchemaCollectionOwningSchema = string.Empty;
			this.xmlSchemaCollectionName = string.Empty;
			base..ctor();
			if (parameterName == null)
			{
				parameterName = string.Empty;
			}
			this.metaParameter = new TdsMetaParameter(parameterName, size, isNullable, precision, scale, new FrameworkValueGetter(this.GetFrameworkValue));
			this.metaParameter.RawValue = value;
			if (dbType != SqlDbType.Variant)
			{
				this.SqlDbType = dbType;
			}
			this.Direction = direction;
			this.SourceColumn = sourceColumn;
			this.SourceVersion = sourceVersion;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlParameter" /> class that uses the parameter name, the type of the parameter, the length of the parameter the direction, the precision, the scale, the name of the source column, one of the <see cref="T:System.Data.DataRowVersion" /> values, a Boolean for source column mapping, the value of the SqlParameter, the name of the database where the schema collection for this XML instance is located, the owning relational schema where the schema collection for this XML instance is located, and the name of the schema collection for this parameter.</summary>
		/// <param name="parameterName">The name of the parameter to map.</param>
		/// <param name="dbType">One of the <see cref="T:System.Data.SqlDbType" /> values.</param>
		/// <param name="size">The length of the parameter.</param>
		/// <param name="direction">One of the <see cref="T:System.Data.ParameterDirection" /> values.</param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</param>
		/// <param name="sourceColumn">The name of the source column. </param>
		/// <param name="sourceVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="sourceColumnNullMapping">true if the source column is nullable; false if it is not.</param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.SqlClient.SqlParameter" />.</param>
		/// <param name="xmlSchemaCollectionDatabase">The name of the database where the schema collection for this XML instance is located.</param>
		/// <param name="xmlSchemaCollectionOwningSchema">The owning relational schema where the schema collection for this XML instance is located.</param>
		/// <param name="xmlSchemaCollectionName">The name of the schema collection for this parameter.</param>
		// Token: 0x060013A8 RID: 5032 RVA: 0x00051E94 File Offset: 0x00050094
		public SqlParameter(string parameterName, SqlDbType dbType, int size, ParameterDirection direction, byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, bool sourceColumnNullMapping, object value, string xmlSchemaCollectionDatabase, string xmlSchemaCollectionOwningSchema, string xmlSchemaCollectionName)
			: this(parameterName, dbType, size, direction, false, precision, scale, sourceColumn, sourceVersion, value)
		{
			this.XmlSchemaCollectionDatabase = xmlSchemaCollectionDatabase;
			this.XmlSchemaCollectionOwningSchema = xmlSchemaCollectionOwningSchema;
			this.XmlSchemaCollectionName = xmlSchemaCollectionName;
			this.SourceColumnNullMapping = sourceColumnNullMapping;
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x00051ED8 File Offset: 0x000500D8
		internal SqlParameter(object[] dbValues)
			: this(dbValues[3].ToString(), null)
		{
			this.ParameterName = (string)dbValues[3];
			switch ((short)dbValues[5])
			{
			case 1:
				this.Direction = ParameterDirection.Input;
				break;
			case 2:
				this.Direction = ParameterDirection.InputOutput;
				break;
			case 3:
				this.Direction = ParameterDirection.Output;
				break;
			case 4:
				this.Direction = ParameterDirection.ReturnValue;
				break;
			default:
				this.Direction = ParameterDirection.Input;
				break;
			}
			this.SqlDbType = this.FrameworkDbTypeFromName((string)dbValues[16]);
			if (this.MetaParameter.IsVariableSizeType && dbValues[10] != DBNull.Value)
			{
				this.Size = (int)dbValues[10];
			}
			if (this.SqlDbType == SqlDbType.Decimal)
			{
				if (dbValues[12] != null && dbValues[12] != DBNull.Value)
				{
					this.Precision = (byte)((short)dbValues[12]);
				}
				if (dbValues[13] != null && dbValues[13] != DBNull.Value)
				{
					this.Scale = (byte)((short)dbValues[13]);
				}
			}
		}

		// Token: 0x060013AA RID: 5034 RVA: 0x00052000 File Offset: 0x00050200
		static SqlParameter()
		{
			if (DbParameter.DbTypeMapping == null)
			{
				DbParameter.DbTypeMapping = new Hashtable();
			}
			DbParameter.DbTypeMapping.Add(SqlDbType.BigInt, typeof(long));
			DbParameter.DbTypeMapping.Add(SqlDbType.Bit, typeof(bool));
			DbParameter.DbTypeMapping.Add(SqlDbType.Char, typeof(string));
			DbParameter.DbTypeMapping.Add(SqlDbType.NChar, typeof(string));
			DbParameter.DbTypeMapping.Add(SqlDbType.Text, typeof(string));
			DbParameter.DbTypeMapping.Add(SqlDbType.NText, typeof(string));
			DbParameter.DbTypeMapping.Add(SqlDbType.VarChar, typeof(string));
			DbParameter.DbTypeMapping.Add(SqlDbType.NVarChar, typeof(string));
			DbParameter.DbTypeMapping.Add(SqlDbType.SmallDateTime, typeof(DateTime));
			DbParameter.DbTypeMapping.Add(SqlDbType.DateTime, typeof(DateTime));
			DbParameter.DbTypeMapping.Add(SqlDbType.Decimal, typeof(decimal));
			DbParameter.DbTypeMapping.Add(SqlDbType.Float, typeof(double));
			DbParameter.DbTypeMapping.Add(SqlDbType.Binary, typeof(byte[]));
			DbParameter.DbTypeMapping.Add(SqlDbType.Image, typeof(byte[]));
			DbParameter.DbTypeMapping.Add(SqlDbType.Money, typeof(decimal));
			DbParameter.DbTypeMapping.Add(SqlDbType.SmallMoney, typeof(decimal));
			DbParameter.DbTypeMapping.Add(SqlDbType.VarBinary, typeof(byte[]));
			DbParameter.DbTypeMapping.Add(SqlDbType.TinyInt, typeof(byte));
			DbParameter.DbTypeMapping.Add(SqlDbType.Int, typeof(int));
			DbParameter.DbTypeMapping.Add(SqlDbType.Real, typeof(float));
			DbParameter.DbTypeMapping.Add(SqlDbType.SmallInt, typeof(short));
			DbParameter.DbTypeMapping.Add(SqlDbType.UniqueIdentifier, typeof(Guid));
			DbParameter.DbTypeMapping.Add(SqlDbType.Variant, typeof(object));
			DbParameter.DbTypeMapping.Add(SqlDbType.Xml, typeof(string));
			SqlParameter.type_mapping = new Hashtable();
			SqlParameter.type_mapping.Add(typeof(long), SqlDbType.BigInt);
			SqlParameter.type_mapping.Add(typeof(SqlInt64), SqlDbType.BigInt);
			SqlParameter.type_mapping.Add(typeof(bool), SqlDbType.Bit);
			SqlParameter.type_mapping.Add(typeof(SqlBoolean), SqlDbType.Bit);
			SqlParameter.type_mapping.Add(typeof(char), SqlDbType.NVarChar);
			SqlParameter.type_mapping.Add(typeof(char[]), SqlDbType.NVarChar);
			SqlParameter.type_mapping.Add(typeof(SqlChars), SqlDbType.NVarChar);
			SqlParameter.type_mapping.Add(typeof(string), SqlDbType.NVarChar);
			SqlParameter.type_mapping.Add(typeof(SqlString), SqlDbType.NVarChar);
			SqlParameter.type_mapping.Add(typeof(DateTime), SqlDbType.DateTime);
			SqlParameter.type_mapping.Add(typeof(SqlDateTime), SqlDbType.DateTime);
			SqlParameter.type_mapping.Add(typeof(decimal), SqlDbType.Decimal);
			SqlParameter.type_mapping.Add(typeof(SqlDecimal), SqlDbType.Decimal);
			SqlParameter.type_mapping.Add(typeof(double), SqlDbType.Float);
			SqlParameter.type_mapping.Add(typeof(SqlDouble), SqlDbType.Float);
			SqlParameter.type_mapping.Add(typeof(byte[]), SqlDbType.VarBinary);
			SqlParameter.type_mapping.Add(typeof(SqlBinary), SqlDbType.VarBinary);
			SqlParameter.type_mapping.Add(typeof(SqlBytes), SqlDbType.VarBinary);
			SqlParameter.type_mapping.Add(typeof(byte), SqlDbType.TinyInt);
			SqlParameter.type_mapping.Add(typeof(SqlByte), SqlDbType.TinyInt);
			SqlParameter.type_mapping.Add(typeof(int), SqlDbType.Int);
			SqlParameter.type_mapping.Add(typeof(SqlInt32), SqlDbType.Int);
			SqlParameter.type_mapping.Add(typeof(float), SqlDbType.Real);
			SqlParameter.type_mapping.Add(typeof(SqlSingle), SqlDbType.Real);
			SqlParameter.type_mapping.Add(typeof(short), SqlDbType.SmallInt);
			SqlParameter.type_mapping.Add(typeof(SqlInt16), SqlDbType.SmallInt);
			SqlParameter.type_mapping.Add(typeof(Guid), SqlDbType.UniqueIdentifier);
			SqlParameter.type_mapping.Add(typeof(SqlGuid), SqlDbType.UniqueIdentifier);
			SqlParameter.type_mapping.Add(typeof(SqlMoney), SqlDbType.Money);
			SqlParameter.type_mapping.Add(typeof(XmlReader), SqlDbType.Xml);
			SqlParameter.type_mapping.Add(typeof(SqlXml), SqlDbType.Xml);
			SqlParameter.type_mapping.Add(typeof(object), SqlDbType.Variant);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		// Token: 0x060013AB RID: 5035 RVA: 0x00052600 File Offset: 0x00050800
		object ICloneable.Clone()
		{
			return new SqlParameter(this.ParameterName, this.SqlDbType, this.Size, this.Direction, this.IsNullable, this.Precision, this.Scale, this.SourceColumn, this.SourceVersion, this.Value);
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x060013AC RID: 5036 RVA: 0x00052650 File Offset: 0x00050850
		// (set) Token: 0x060013AD RID: 5037 RVA: 0x00052658 File Offset: 0x00050858
		internal SqlParameterCollection Container
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

		// Token: 0x060013AE RID: 5038 RVA: 0x00052664 File Offset: 0x00050864
		internal void CheckIfInitialized()
		{
			if (!this.isTypeSet)
			{
				throw new Exception("all parameters to have an explicity set type");
			}
			if (this.MetaParameter.IsVariableSizeType)
			{
				if (this.SqlDbType == SqlDbType.Decimal && this.Precision == 0)
				{
					throw new Exception("Parameter of type 'Decimal' have an explicitly set Precision and Scale");
				}
				if (this.Size == 0)
				{
					throw new Exception("all variable length parameters to have an explicitly set non-zero Size");
				}
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlDbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.SqlDbType" /> values. The default is NVarChar.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x060013AF RID: 5039 RVA: 0x000526D0 File Offset: 0x000508D0
		// (set) Token: 0x060013B0 RID: 5040 RVA: 0x000526D8 File Offset: 0x000508D8
		public override DbType DbType
		{
			get
			{
				return this.dbType;
			}
			set
			{
				this.SetDbType(value);
				this.typeChanged = true;
				this.isTypeSet = true;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x060013B1 RID: 5041 RVA: 0x000526F0 File Offset: 0x000508F0
		// (set) Token: 0x060013B2 RID: 5042 RVA: 0x000526F8 File Offset: 0x000508F8
		[RefreshProperties(RefreshProperties.All)]
		public override ParameterDirection Direction
		{
			get
			{
				return this.direction;
			}
			set
			{
				this.direction = value;
				switch (this.direction)
				{
				case ParameterDirection.Output:
					this.MetaParameter.Direction = TdsParameterDirection.Output;
					break;
				case ParameterDirection.InputOutput:
					this.MetaParameter.Direction = TdsParameterDirection.InputOutput;
					break;
				case ParameterDirection.ReturnValue:
					this.MetaParameter.Direction = TdsParameterDirection.ReturnValue;
					break;
				}
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x060013B3 RID: 5043 RVA: 0x00052768 File Offset: 0x00050968
		internal TdsMetaParameter MetaParameter
		{
			get
			{
				return this.metaParameter;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter accepts null values.</summary>
		/// <returns>true if null values are accepted; otherwise false. The default is false.</returns>
		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x060013B4 RID: 5044 RVA: 0x00052770 File Offset: 0x00050970
		// (set) Token: 0x060013B5 RID: 5045 RVA: 0x00052780 File Offset: 0x00050980
		public override bool IsNullable
		{
			get
			{
				return this.metaParameter.IsNullable;
			}
			set
			{
				this.metaParameter.IsNullable = value;
			}
		}

		/// <summary>Gets or sets the offset to the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> property.</summary>
		/// <returns>The offset to the <see cref="P:System.Data.SqlClient.SqlParameter.Value" />. The default is 0.</returns>
		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x060013B6 RID: 5046 RVA: 0x00052790 File Offset: 0x00050990
		// (set) Token: 0x060013B7 RID: 5047 RVA: 0x00052798 File Offset: 0x00050998
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public int Offset
		{
			get
			{
				return this.offset;
			}
			set
			{
				this.offset = value;
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.SqlClient.SqlParameter" />. The default is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x060013B8 RID: 5048 RVA: 0x000527A4 File Offset: 0x000509A4
		// (set) Token: 0x060013B9 RID: 5049 RVA: 0x000527B4 File Offset: 0x000509B4
		public override string ParameterName
		{
			get
			{
				return this.metaParameter.ParameterName;
			}
			set
			{
				if (value == null)
				{
					value = string.Empty;
				}
				this.metaParameter.ParameterName = value;
			}
		}

		/// <summary>Gets or sets the maximum number of digits used to represent the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> property. The default value is 0. This indicates that the data provider sets the precision for <see cref="P:System.Data.SqlClient.SqlParameter.Value" />.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x060013BA RID: 5050 RVA: 0x000527D0 File Offset: 0x000509D0
		// (set) Token: 0x060013BB RID: 5051 RVA: 0x000527E0 File Offset: 0x000509E0
		[DefaultValue(0)]
		public byte Precision
		{
			get
			{
				return this.metaParameter.Precision;
			}
			set
			{
				this.metaParameter.Precision = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.SqlClient.SqlParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x060013BC RID: 5052 RVA: 0x000527F0 File Offset: 0x000509F0
		// (set) Token: 0x060013BD RID: 5053 RVA: 0x00052800 File Offset: 0x00050A00
		[DefaultValue(0)]
		public byte Scale
		{
			get
			{
				return this.metaParameter.Scale;
			}
			set
			{
				this.metaParameter.Scale = value;
			}
		}

		/// <summary>Gets or sets the maximum size, in bytes, of the data within the column.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x060013BE RID: 5054 RVA: 0x00052810 File Offset: 0x00050A10
		// (set) Token: 0x060013BF RID: 5055 RVA: 0x00052820 File Offset: 0x00050A20
		public override int Size
		{
			get
			{
				return this.metaParameter.Size;
			}
			set
			{
				this.metaParameter.Size = value;
			}
		}

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.SqlClient.SqlParameter.Value" /></summary>
		/// <returns>The name of the source column mapped to the <see cref="T:System.Data.DataSet" />. The default is an empty string.</returns>
		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x060013C0 RID: 5056 RVA: 0x00052830 File Offset: 0x00050A30
		// (set) Token: 0x060013C1 RID: 5057 RVA: 0x0005284C File Offset: 0x00050A4C
		public override string SourceColumn
		{
			get
			{
				if (this.sourceColumn == null)
				{
					return string.Empty;
				}
				return this.sourceColumn;
			}
			set
			{
				this.sourceColumn = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.SqlClient.SqlParameter.Value" /></summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		// Token: 0x170003AA RID: 938
		// (get) Token: 0x060013C2 RID: 5058 RVA: 0x00052858 File Offset: 0x00050A58
		// (set) Token: 0x060013C3 RID: 5059 RVA: 0x00052860 File Offset: 0x00050A60
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

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlDbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.SqlDbType" /> values. The default is NVarChar.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003AB RID: 939
		// (get) Token: 0x060013C4 RID: 5060 RVA: 0x0005286C File Offset: 0x00050A6C
		// (set) Token: 0x060013C5 RID: 5061 RVA: 0x00052874 File Offset: 0x00050A74
		[DbProviderSpecificTypeProperty(true)]
		[RefreshProperties(RefreshProperties.All)]
		public SqlDbType SqlDbType
		{
			get
			{
				return this.sqlDbType;
			}
			set
			{
				this.SetSqlDbType(value);
				this.typeChanged = true;
				this.isTypeSet = true;
			}
		}

		/// <summary>Gets or sets the value of the parameter.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter. The default value is null.</returns>
		// Token: 0x170003AC RID: 940
		// (get) Token: 0x060013C6 RID: 5062 RVA: 0x0005288C File Offset: 0x00050A8C
		// (set) Token: 0x060013C7 RID: 5063 RVA: 0x000528C4 File Offset: 0x00050AC4
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(StringConverter))]
		public override object Value
		{
			get
			{
				if (this.sqlType != null)
				{
					return this.GetSqlValue(this.metaParameter.RawValue);
				}
				return this.metaParameter.RawValue;
			}
			set
			{
				if (!this.isTypeSet)
				{
					this.InferSqlType(value);
				}
				if (value is INullable)
				{
					this.sqlType = value.GetType();
					value = this.SqlTypeToFrameworkType(value);
				}
				this.metaParameter.RawValue = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Globalization.CompareInfo" /> object that defines how string comparisons should be performed for this parameter.</summary>
		/// <returns>A <see cref="T:System.Globalization.CompareInfo" /> object that defines string comparison for this parameter.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003AD RID: 941
		// (get) Token: 0x060013C8 RID: 5064 RVA: 0x00052910 File Offset: 0x00050B10
		// (set) Token: 0x060013C9 RID: 5065 RVA: 0x00052918 File Offset: 0x00050B18
		[Browsable(false)]
		public SqlCompareOptions CompareInfo
		{
			get
			{
				return this.compareInfo;
			}
			set
			{
				this.compareInfo = value;
			}
		}

		/// <summary>Gets or sets the locale identifier that determines conventions and language for a particular region.</summary>
		/// <returns>Returns the locale identifier associated with the parameter.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x060013CA RID: 5066 RVA: 0x00052924 File Offset: 0x00050B24
		// (set) Token: 0x060013CB RID: 5067 RVA: 0x0005292C File Offset: 0x00050B2C
		[Browsable(false)]
		public int LocaleId
		{
			get
			{
				return this.localeId;
			}
			set
			{
				this.localeId = value;
			}
		}

		/// <summary>Gets or sets the value of the parameter as an SQL type.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is the value of the parameter, using SQL types. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003AF RID: 943
		// (get) Token: 0x060013CC RID: 5068 RVA: 0x00052938 File Offset: 0x00050B38
		// (set) Token: 0x060013CD RID: 5069 RVA: 0x0005294C File Offset: 0x00050B4C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public object SqlValue
		{
			get
			{
				return this.GetSqlValue(this.metaParameter.RawValue);
			}
			set
			{
				this.Value = value;
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This allows <see cref="T:System.Data.SqlClient.SqlCommandBuilder" /> to correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060013CE RID: 5070 RVA: 0x00052958 File Offset: 0x00050B58
		// (set) Token: 0x060013CF RID: 5071 RVA: 0x00052960 File Offset: 0x00050B60
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

		/// <summary>Gets the name of the database where the schema collection for this XML instance is located.</summary>
		/// <returns>The name of the database where the schema collection for this XML instance is located.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060013D0 RID: 5072 RVA: 0x0005296C File Offset: 0x00050B6C
		// (set) Token: 0x060013D1 RID: 5073 RVA: 0x00052974 File Offset: 0x00050B74
		public string XmlSchemaCollectionDatabase
		{
			get
			{
				return this.xmlSchemaCollectionDatabase;
			}
			set
			{
				this.xmlSchemaCollectionDatabase = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>Gets the name of the schema collection for this XML instance.</summary>
		/// <returns>The name of the schema collection for this XML instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x060013D2 RID: 5074 RVA: 0x00052990 File Offset: 0x00050B90
		// (set) Token: 0x060013D3 RID: 5075 RVA: 0x00052998 File Offset: 0x00050B98
		public string XmlSchemaCollectionName
		{
			get
			{
				return this.xmlSchemaCollectionName;
			}
			set
			{
				this.xmlSchemaCollectionName = ((value != null) ? value : string.Empty);
			}
		}

		/// <summary>The owning relational schema where the schema collection for this XML instance is located.</summary>
		/// <returns>An <see cref="P:System.Data.SqlClient.SqlParameter.XmlSchemaCollectionOwningSchema" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x000529B4 File Offset: 0x00050BB4
		// (set) Token: 0x060013D5 RID: 5077 RVA: 0x000529BC File Offset: 0x00050BBC
		public string XmlSchemaCollectionOwningSchema
		{
			get
			{
				return this.xmlSchemaCollectionOwningSchema;
			}
			set
			{
				this.xmlSchemaCollectionOwningSchema = ((value != null) ? value : string.Empty);
			}
		}

		// Token: 0x060013D6 RID: 5078 RVA: 0x000529D8 File Offset: 0x00050BD8
		private void InferSqlType(object value)
		{
			if (value == null || value == DBNull.Value)
			{
				this.SetSqlDbType(SqlDbType.NVarChar);
				return;
			}
			Type type = value.GetType();
			if (type.IsEnum)
			{
				type = Enum.GetUnderlyingType(type);
			}
			object obj = SqlParameter.type_mapping[type];
			if (obj == null)
			{
				throw new ArgumentException(string.Format("The parameter data type of {0} is invalid.", type.FullName));
			}
			this.SetSqlDbType((SqlDbType)((int)obj));
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x00052A4C File Offset: 0x00050C4C
		internal override Type SystemType
		{
			get
			{
				return (Type)DbParameter.DbTypeMapping[this.sqlDbType];
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x060013D8 RID: 5080 RVA: 0x00052A68 File Offset: 0x00050C68
		// (set) Token: 0x060013D9 RID: 5081 RVA: 0x00052A78 File Offset: 0x00050C78
		internal override object FrameworkDbType
		{
			get
			{
				return this.sqlDbType;
			}
			set
			{
				try
				{
					object obj = this.DbTypeFromName((string)value);
					this.SetDbType((DbType)((int)obj));
				}
				catch (ArgumentException)
				{
					object obj = this.FrameworkDbTypeFromName((string)value);
					this.SetSqlDbType((SqlDbType)((int)obj));
				}
			}
		}

		// Token: 0x060013DA RID: 5082 RVA: 0x00052AE8 File Offset: 0x00050CE8
		private DbType DbTypeFromName(string name)
		{
			string text = name.ToLower();
			switch (text)
			{
			case "ansistring":
				return DbType.AnsiString;
			case "ansistringfixedlength":
				return DbType.AnsiStringFixedLength;
			case "binary":
				return DbType.Binary;
			case "boolean":
				return DbType.Boolean;
			case "byte":
				return DbType.Byte;
			case "currency":
				return DbType.Currency;
			case "date":
				return DbType.Date;
			case "datetime":
				return DbType.DateTime;
			case "decimal":
				return DbType.Decimal;
			case "double":
				return DbType.Double;
			case "guid":
				return DbType.Guid;
			case "int16":
				return DbType.Int16;
			case "int32":
				return DbType.Int32;
			case "int64":
				return DbType.Int64;
			case "object":
				return DbType.Object;
			case "single":
				return DbType.Single;
			case "string":
				return DbType.String;
			case "stringfixedlength":
				return DbType.StringFixedLength;
			case "time":
				return DbType.Time;
			case "xml":
				return DbType.Xml;
			}
			string text2 = string.Format("No mapping exists from {0} to a known DbType.", name);
			throw new ArgumentException(text2);
		}

		// Token: 0x060013DB RID: 5083 RVA: 0x00052CC8 File Offset: 0x00050EC8
		private void SetDbType(DbType type)
		{
			switch (type)
			{
			case DbType.AnsiString:
				this.MetaParameter.TypeName = "varchar";
				this.sqlDbType = SqlDbType.VarChar;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_02F4;
			case DbType.Binary:
				this.MetaParameter.TypeName = "varbinary";
				this.sqlDbType = SqlDbType.VarBinary;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_02F4;
			case DbType.Byte:
				this.MetaParameter.TypeName = "tinyint";
				this.sqlDbType = SqlDbType.TinyInt;
				goto IL_02F4;
			case DbType.Boolean:
				this.MetaParameter.TypeName = "bit";
				this.sqlDbType = SqlDbType.Bit;
				goto IL_02F4;
			case DbType.Currency:
				this.sqlDbType = SqlDbType.Money;
				this.MetaParameter.TypeName = "money";
				goto IL_02F4;
			case DbType.Date:
			case DbType.DateTime:
				this.MetaParameter.TypeName = "datetime";
				this.sqlDbType = SqlDbType.DateTime;
				goto IL_02F4;
			case DbType.Decimal:
				this.MetaParameter.TypeName = "decimal";
				this.sqlDbType = SqlDbType.Decimal;
				goto IL_02F4;
			case DbType.Double:
				this.MetaParameter.TypeName = "float";
				this.sqlDbType = SqlDbType.Float;
				goto IL_02F4;
			case DbType.Guid:
				this.MetaParameter.TypeName = "uniqueidentifier";
				this.sqlDbType = SqlDbType.UniqueIdentifier;
				goto IL_02F4;
			case DbType.Int16:
				this.MetaParameter.TypeName = "smallint";
				this.sqlDbType = SqlDbType.SmallInt;
				goto IL_02F4;
			case DbType.Int32:
				this.MetaParameter.TypeName = "int";
				this.sqlDbType = SqlDbType.Int;
				goto IL_02F4;
			case DbType.Int64:
				this.MetaParameter.TypeName = "bigint";
				this.sqlDbType = SqlDbType.BigInt;
				goto IL_02F4;
			case DbType.Object:
				this.MetaParameter.TypeName = "sql_variant";
				this.sqlDbType = SqlDbType.Variant;
				goto IL_02F4;
			case DbType.Single:
				this.MetaParameter.TypeName = "real";
				this.sqlDbType = SqlDbType.Real;
				goto IL_02F4;
			case DbType.String:
				this.MetaParameter.TypeName = "nvarchar";
				this.sqlDbType = SqlDbType.NVarChar;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_02F4;
			case DbType.Time:
				this.MetaParameter.TypeName = "datetime";
				this.sqlDbType = SqlDbType.DateTime;
				goto IL_02F4;
			case DbType.AnsiStringFixedLength:
				this.MetaParameter.TypeName = "char";
				this.sqlDbType = SqlDbType.Char;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_02F4;
			case DbType.StringFixedLength:
				this.MetaParameter.TypeName = "nchar";
				this.sqlDbType = SqlDbType.NChar;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_02F4;
			case DbType.Xml:
				this.MetaParameter.TypeName = "xml";
				this.sqlDbType = SqlDbType.Xml;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_02F4;
			}
			string text = string.Format("No mapping exists from DbType {0} to a known SqlDbType.", type);
			throw new ArgumentException(text);
			IL_02F4:
			this.dbType = type;
		}

		// Token: 0x060013DC RID: 5084 RVA: 0x00052FD0 File Offset: 0x000511D0
		private SqlDbType FrameworkDbTypeFromName(string dbTypeName)
		{
			string text = dbTypeName.ToLower();
			switch (text)
			{
			case "bigint":
				return SqlDbType.BigInt;
			case "binary":
				return SqlDbType.Binary;
			case "bit":
				return SqlDbType.Bit;
			case "char":
				return SqlDbType.Char;
			case "datetime":
				return SqlDbType.DateTime;
			case "decimal":
				return SqlDbType.Decimal;
			case "float":
				return SqlDbType.Float;
			case "image":
				return SqlDbType.Image;
			case "int":
				return SqlDbType.Int;
			case "money":
				return SqlDbType.Money;
			case "nchar":
				return SqlDbType.NChar;
			case "ntext":
				return SqlDbType.NText;
			case "nvarchar":
				return SqlDbType.NVarChar;
			case "real":
				return SqlDbType.Real;
			case "smalldatetime":
				return SqlDbType.SmallDateTime;
			case "smallint":
				return SqlDbType.SmallInt;
			case "smallmoney":
				return SqlDbType.SmallMoney;
			case "text":
				return SqlDbType.Text;
			case "timestamp":
				return SqlDbType.Timestamp;
			case "tinyint":
				return SqlDbType.TinyInt;
			case "uniqueidentifier":
				return SqlDbType.UniqueIdentifier;
			case "varbinary":
				return SqlDbType.VarBinary;
			case "varchar":
				return SqlDbType.VarChar;
			case "sql_variant":
				return SqlDbType.Variant;
			case "xml":
				return SqlDbType.Xml;
			}
			return SqlDbType.Variant;
		}

		// Token: 0x060013DD RID: 5085 RVA: 0x00053204 File Offset: 0x00051404
		internal void SetSqlDbType(SqlDbType type)
		{
			switch (type)
			{
			case SqlDbType.BigInt:
				this.MetaParameter.TypeName = "bigint";
				this.dbType = DbType.Int64;
				goto IL_03D1;
			case SqlDbType.Binary:
				this.MetaParameter.TypeName = "binary";
				this.dbType = DbType.Binary;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_03D1;
			case SqlDbType.Bit:
				this.MetaParameter.TypeName = "bit";
				this.dbType = DbType.Boolean;
				goto IL_03D1;
			case SqlDbType.Char:
				this.MetaParameter.TypeName = "char";
				this.dbType = DbType.AnsiStringFixedLength;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_03D1;
			case SqlDbType.DateTime:
				this.MetaParameter.TypeName = "datetime";
				this.dbType = DbType.DateTime;
				goto IL_03D1;
			case SqlDbType.Decimal:
				this.MetaParameter.TypeName = "decimal";
				this.dbType = DbType.Decimal;
				goto IL_03D1;
			case SqlDbType.Float:
				this.MetaParameter.TypeName = "float";
				this.dbType = DbType.Double;
				goto IL_03D1;
			case SqlDbType.Image:
				this.MetaParameter.TypeName = "image";
				this.dbType = DbType.Binary;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_03D1;
			case SqlDbType.Int:
				this.MetaParameter.TypeName = "int";
				this.dbType = DbType.Int32;
				goto IL_03D1;
			case SqlDbType.Money:
				this.MetaParameter.TypeName = "money";
				this.dbType = DbType.Currency;
				goto IL_03D1;
			case SqlDbType.NChar:
				this.MetaParameter.TypeName = "nchar";
				this.dbType = DbType.StringFixedLength;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_03D1;
			case SqlDbType.NText:
				this.MetaParameter.TypeName = "ntext";
				this.dbType = DbType.String;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_03D1;
			case SqlDbType.NVarChar:
				this.MetaParameter.TypeName = "nvarchar";
				this.dbType = DbType.String;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_03D1;
			case SqlDbType.Real:
				this.MetaParameter.TypeName = "real";
				this.dbType = DbType.Single;
				goto IL_03D1;
			case SqlDbType.UniqueIdentifier:
				this.MetaParameter.TypeName = "uniqueidentifier";
				this.dbType = DbType.Guid;
				goto IL_03D1;
			case SqlDbType.SmallDateTime:
				this.MetaParameter.TypeName = "smalldatetime";
				this.dbType = DbType.DateTime;
				goto IL_03D1;
			case SqlDbType.SmallInt:
				this.MetaParameter.TypeName = "smallint";
				this.dbType = DbType.Int16;
				goto IL_03D1;
			case SqlDbType.SmallMoney:
				this.MetaParameter.TypeName = "smallmoney";
				this.dbType = DbType.Currency;
				goto IL_03D1;
			case SqlDbType.Text:
				this.MetaParameter.TypeName = "text";
				this.dbType = DbType.AnsiString;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_03D1;
			case SqlDbType.Timestamp:
				this.MetaParameter.TypeName = "timestamp";
				this.dbType = DbType.Binary;
				goto IL_03D1;
			case SqlDbType.TinyInt:
				this.MetaParameter.TypeName = "tinyint";
				this.dbType = DbType.Byte;
				goto IL_03D1;
			case SqlDbType.VarBinary:
				this.MetaParameter.TypeName = "varbinary";
				this.dbType = DbType.Binary;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_03D1;
			case SqlDbType.VarChar:
				this.MetaParameter.TypeName = "varchar";
				this.dbType = DbType.AnsiString;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_03D1;
			case SqlDbType.Variant:
				this.MetaParameter.TypeName = "sql_variant";
				this.dbType = DbType.Object;
				goto IL_03D1;
			case SqlDbType.Xml:
				this.MetaParameter.TypeName = "xml";
				this.dbType = DbType.Xml;
				this.MetaParameter.IsVariableSizeType = true;
				goto IL_03D1;
			}
			string text = string.Format("No mapping exists from SqlDbType {0} to a known DbType.", type);
			throw new ArgumentOutOfRangeException("SqlDbType", text);
			IL_03D1:
			this.sqlDbType = type;
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.SqlClient.SqlParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.SqlClient.SqlParameter.ParameterName" />.</returns>
		// Token: 0x060013DE RID: 5086 RVA: 0x000535EC File Offset: 0x000517EC
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x060013DF RID: 5087 RVA: 0x000535F4 File Offset: 0x000517F4
		private object GetFrameworkValue(object rawValue, ref bool updated)
		{
			updated = this.typeChanged || updated;
			object obj;
			if (updated)
			{
				obj = this.SqlTypeToFrameworkType(rawValue);
				this.typeChanged = false;
			}
			else
			{
				obj = null;
			}
			return obj;
		}

		// Token: 0x060013E0 RID: 5088 RVA: 0x00053634 File Offset: 0x00051834
		private object GetSqlValue(object value)
		{
			if (value == null)
			{
				return value;
			}
			switch (this.sqlDbType)
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
			{
				if (value == DBNull.Value)
				{
					return SqlString.Null;
				}
				Type type = value.GetType();
				string text;
				if (type == typeof(char))
				{
					text = value.ToString();
				}
				else if (type == typeof(char[]))
				{
					text = new string((char[])value);
				}
				else
				{
					text = (string)value;
				}
				return text;
			}
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
					return SqlXml.Null;
				}
				return (SqlXml)value;
			}
			throw new NotImplementedException("Type '" + this.sqlDbType + "' not implemented.");
		}

		// Token: 0x060013E1 RID: 5089 RVA: 0x00053958 File Offset: 0x00051B58
		private object SqlTypeToFrameworkType(object value)
		{
			INullable nullable = value as INullable;
			if (nullable == null)
			{
				return this.ConvertToFrameworkType(value);
			}
			if (nullable.IsNull)
			{
				return DBNull.Value;
			}
			Type type = value.GetType();
			if (typeof(SqlString) == type)
			{
				return ((SqlString)value).Value;
			}
			if (typeof(SqlInt16) == type)
			{
				return ((SqlInt16)value).Value;
			}
			if (typeof(SqlInt32) == type)
			{
				return ((SqlInt32)value).Value;
			}
			if (typeof(SqlDateTime) == type)
			{
				return ((SqlDateTime)value).Value;
			}
			if (typeof(SqlInt64) == type)
			{
				return ((SqlInt64)value).Value;
			}
			if (typeof(SqlBinary) == type)
			{
				return ((SqlBinary)value).Value;
			}
			if (typeof(SqlBytes) == type)
			{
				return ((SqlBytes)value).Value;
			}
			if (typeof(SqlChars) == type)
			{
				return ((SqlChars)value).Value;
			}
			if (typeof(SqlBoolean) == type)
			{
				return ((SqlBoolean)value).Value;
			}
			if (typeof(SqlByte) == type)
			{
				return ((SqlByte)value).Value;
			}
			if (typeof(SqlDecimal) == type)
			{
				return ((SqlDecimal)value).Value;
			}
			if (typeof(SqlDouble) == type)
			{
				return ((SqlDouble)value).Value;
			}
			if (typeof(SqlGuid) == type)
			{
				return ((SqlGuid)value).Value;
			}
			if (typeof(SqlMoney) == type)
			{
				return ((SqlMoney)value).Value;
			}
			if (typeof(SqlSingle) == type)
			{
				return ((SqlSingle)value).Value;
			}
			return value;
		}

		// Token: 0x060013E2 RID: 5090 RVA: 0x00053BA0 File Offset: 0x00051DA0
		internal object ConvertToFrameworkType(object value)
		{
			if (value == null || value == DBNull.Value)
			{
				return value;
			}
			if (this.sqlDbType == SqlDbType.Variant)
			{
				return this.metaParameter.Value;
			}
			Type systemType = this.SystemType;
			if (systemType == null)
			{
				throw new NotImplementedException("Type Not Supported : " + this.sqlDbType.ToString());
			}
			Type type = value.GetType();
			if (type == systemType)
			{
				return value;
			}
			object obj = null;
			try
			{
				obj = this.ConvertToFrameworkType(value, systemType);
			}
			catch (FormatException ex)
			{
				throw new FormatException(string.Format(CultureInfo.InvariantCulture, "Parameter value could not be converted from {0} to {1}.", new object[] { type.Name, systemType.Name }), ex);
			}
			return obj;
		}

		// Token: 0x060013E3 RID: 5091 RVA: 0x00053C78 File Offset: 0x00051E78
		private object ConvertToFrameworkType(object value, Type frameworkType)
		{
			object obj = Convert.ChangeType(value, frameworkType);
			SqlDbType sqlDbType = this.sqlDbType;
			if (sqlDbType == SqlDbType.Money || sqlDbType == SqlDbType.SmallMoney)
			{
				obj = decimal.Round((decimal)obj, 4);
			}
			return obj;
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013E4 RID: 5092 RVA: 0x00053CC4 File Offset: 0x00051EC4
		public override void ResetDbType()
		{
			this.InferSqlType(this.Value);
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.SqlClient.SqlParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060013E5 RID: 5093 RVA: 0x00053CD4 File Offset: 0x00051ED4
		public void ResetSqlDbType()
		{
			this.InferSqlType(this.Value);
		}

		// Token: 0x040007EE RID: 2030
		private TdsMetaParameter metaParameter;

		// Token: 0x040007EF RID: 2031
		private SqlParameterCollection container;

		// Token: 0x040007F0 RID: 2032
		private DbType dbType;

		// Token: 0x040007F1 RID: 2033
		private ParameterDirection direction;

		// Token: 0x040007F2 RID: 2034
		private bool isTypeSet;

		// Token: 0x040007F3 RID: 2035
		private int offset;

		// Token: 0x040007F4 RID: 2036
		private SqlDbType sqlDbType;

		// Token: 0x040007F5 RID: 2037
		private string sourceColumn;

		// Token: 0x040007F6 RID: 2038
		private DataRowVersion sourceVersion;

		// Token: 0x040007F7 RID: 2039
		private SqlCompareOptions compareInfo;

		// Token: 0x040007F8 RID: 2040
		private int localeId;

		// Token: 0x040007F9 RID: 2041
		private Type sqlType;

		// Token: 0x040007FA RID: 2042
		private bool typeChanged;

		// Token: 0x040007FB RID: 2043
		private bool sourceColumnNullMapping;

		// Token: 0x040007FC RID: 2044
		private string xmlSchemaCollectionDatabase;

		// Token: 0x040007FD RID: 2045
		private string xmlSchemaCollectionOwningSchema;

		// Token: 0x040007FE RID: 2046
		private string xmlSchemaCollectionName;

		// Token: 0x040007FF RID: 2047
		private static Hashtable type_mapping;
	}
}
