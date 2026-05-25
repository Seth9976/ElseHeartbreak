using System;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Data.Odbc
{
	/// <summary>Represents a parameter to an <see cref="T:System.Data.Odbc.OdbcCommand" /> and optionally, its mapping to a <see cref="T:System.Data.DataColumn" />. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000127 RID: 295
	[TypeConverter("System.Data.Odbc.OdbcParameter+OdbcParameterConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public sealed class OdbcParameter : DbParameter, IDataParameter, IDbDataParameter, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class.</summary>
		// Token: 0x0600107F RID: 4223 RVA: 0x00040ED0 File Offset: 0x0003F0D0
		public OdbcParameter()
		{
			this._cbLengthInd = new NativeBuffer();
			this.ParameterName = string.Empty;
			this.IsNullable = false;
			this.SourceColumn = string.Empty;
			this.Direction = ParameterDirection.Input;
			this._typeMap = OdbcTypeConverter.GetTypeMap(OdbcType.NVarChar);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name and an <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="value">An <see cref="T:System.Data.Odbc.OdbcParameter" /> object. </param>
		// Token: 0x06001080 RID: 4224 RVA: 0x00040F2C File Offset: 0x0003F12C
		public OdbcParameter(string name, object value)
			: this()
		{
			this.ParameterName = name;
			this.Value = value;
			this._typeMap = OdbcTypeConverter.InferFromValue(value);
			if (value != null && !value.GetType().IsValueType)
			{
				Type type = value.GetType();
				if (type.IsArray)
				{
					this.Size = ((type.GetElementType() != typeof(byte)) ? 0 : ((Array)value).Length);
				}
				else
				{
					this.Size = value.ToString().Length;
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name and data type.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001081 RID: 4225 RVA: 0x00040FC4 File Offset: 0x0003F1C4
		public OdbcParameter(string name, OdbcType type)
			: this()
		{
			this.ParameterName = name;
			this._typeMap = OdbcTypeConverter.GetTypeMap(type);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, and length.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001082 RID: 4226 RVA: 0x00040FE0 File Offset: 0x0003F1E0
		public OdbcParameter(string name, OdbcType type, int size)
			: this(name, type)
		{
			this.Size = size;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, and source column name.</summary>
		/// <param name="name">The name of the parameter. </param>
		/// <param name="type">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="sourcecolumn">The name of the source column. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001083 RID: 4227 RVA: 0x00040FF4 File Offset: 0x0003F1F4
		public OdbcParameter(string name, OdbcType type, int size, string sourcecolumn)
			: this(name, type, size)
		{
			this.SourceColumn = sourcecolumn;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcParameter" /> class that uses the parameter name, data type, length, source column name, parameter direction, numeric precision, and other properties.</summary>
		/// <param name="parameterName">The name of the parameter. </param>
		/// <param name="odbcType">One of the <see cref="T:System.Data.Odbc.OdbcType" /> values. </param>
		/// <param name="size">The length of the parameter. </param>
		/// <param name="parameterDirection">One of the <see cref="T:System.Data.ParameterDirection" /> values. </param>
		/// <param name="isNullable">true if the value of the field can be null; otherwise false. </param>
		/// <param name="precision">The total number of digits to the left and right of the decimal point to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. </param>
		/// <param name="scale">The total number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. </param>
		/// <param name="srcColumn">The name of the source column. </param>
		/// <param name="srcVersion">One of the <see cref="T:System.Data.DataRowVersion" /> values. </param>
		/// <param name="value">An <see cref="T:System.Object" /> that is the value of the <see cref="T:System.Data.Odbc.OdbcParameter" />. </param>
		/// <exception cref="T:System.ArgumentException">The value supplied in the <paramref name="type" /> parameter is an invalid back-end data type. </exception>
		// Token: 0x06001084 RID: 4228 RVA: 0x00041008 File Offset: 0x0003F208
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public OdbcParameter(string parameterName, OdbcType odbcType, int size, ParameterDirection parameterDirection, bool isNullable, byte precision, byte scale, string srcColumn, DataRowVersion srcVersion, object value)
			: this(parameterName, odbcType, size, srcColumn)
		{
			this.Direction = parameterDirection;
			this.IsNullable = isNullable;
			this.SourceVersion = srcVersion;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		// Token: 0x06001085 RID: 4229 RVA: 0x00041038 File Offset: 0x0003F238
		[MonoTODO]
		object ICloneable.Clone()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170002C9 RID: 713
		// (get) Token: 0x06001086 RID: 4230 RVA: 0x00041040 File Offset: 0x0003F240
		// (set) Token: 0x06001087 RID: 4231 RVA: 0x00041048 File Offset: 0x0003F248
		internal OdbcParameterCollection Container
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

		/// <summary>Gets or sets the <see cref="T:System.Data.DbType" /> of the parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.DbType" /> values. The default is <see cref="T:System.String" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The property was not set to a valid <see cref="T:System.Data.DbType" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002CA RID: 714
		// (get) Token: 0x06001088 RID: 4232 RVA: 0x00041054 File Offset: 0x0003F254
		// (set) Token: 0x06001089 RID: 4233 RVA: 0x00041064 File Offset: 0x0003F264
		[OdbcDescription("The parameter generic type")]
		[OdbcCategory("Data")]
		public override DbType DbType
		{
			get
			{
				return this._typeMap.DbType;
			}
			set
			{
				if (value == this._typeMap.DbType)
				{
					return;
				}
				this._typeMap = OdbcTypeConverter.GetTypeMap(value);
			}
		}

		/// <summary>Gets or sets a value that indicates whether the parameter is input-only, output-only, bidirectional, or a stored procedure return value parameter.</summary>
		/// <returns>One of the <see cref="T:System.Data.ParameterDirection" /> values. The default is Input.</returns>
		/// <exception cref="T:System.ArgumentException">The property was not set to one of the valid <see cref="T:System.Data.ParameterDirection" /> values.</exception>
		// Token: 0x170002CB RID: 715
		// (get) Token: 0x0600108A RID: 4234 RVA: 0x00041084 File Offset: 0x0003F284
		// (set) Token: 0x0600108B RID: 4235 RVA: 0x0004108C File Offset: 0x0003F28C
		[OdbcDescription("Input, output, or bidirectional parameter")]
		[RefreshProperties(RefreshProperties.All)]
		[OdbcCategory("Data")]
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
		// Token: 0x170002CC RID: 716
		// (get) Token: 0x0600108C RID: 4236 RVA: 0x00041098 File Offset: 0x0003F298
		// (set) Token: 0x0600108D RID: 4237 RVA: 0x000410A0 File Offset: 0x0003F2A0
		[OdbcDescription("A design-time property used for strongly typed code generation")]
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

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcType" /> of the parameter.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcType" /> value that is the <see cref="T:System.Data.Odbc.OdbcType" /> of the parameter. The default is Nchar.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002CD RID: 717
		// (get) Token: 0x0600108E RID: 4238 RVA: 0x000410AC File Offset: 0x0003F2AC
		// (set) Token: 0x0600108F RID: 4239 RVA: 0x000410BC File Offset: 0x0003F2BC
		[OdbcDescription("The parameter native type")]
		[DbProviderSpecificTypeProperty(true)]
		[OdbcCategory("Data")]
		[DefaultValue(OdbcType.NChar)]
		[RefreshProperties(RefreshProperties.All)]
		public OdbcType OdbcType
		{
			get
			{
				return this._typeMap.OdbcType;
			}
			set
			{
				if (value == this._typeMap.OdbcType)
				{
					return;
				}
				this._typeMap = OdbcTypeConverter.GetTypeMap(value);
			}
		}

		/// <summary>Gets or sets the name of the <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		/// <returns>The name of the <see cref="T:System.Data.Odbc.OdbcParameter" />. The default is an empty string ("").</returns>
		// Token: 0x170002CE RID: 718
		// (get) Token: 0x06001090 RID: 4240 RVA: 0x000410DC File Offset: 0x0003F2DC
		// (set) Token: 0x06001091 RID: 4241 RVA: 0x000410E4 File Offset: 0x0003F2E4
		[OdbcDescription("DataParameter_ParameterName")]
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

		/// <summary>Gets or sets the number of digits used to represent the <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> property.</summary>
		/// <returns>The maximum number of digits used to represent the <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> property. The default value is 0, which indicates that the data provider sets the precision for <see cref="P:System.Data.Odbc.OdbcParameter.Value" />. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06001092 RID: 4242 RVA: 0x000410F0 File Offset: 0x0003F2F0
		// (set) Token: 0x06001093 RID: 4243 RVA: 0x000410F8 File Offset: 0x0003F2F8
		[OdbcCategory("DataCategory_Data")]
		[OdbcDescription("DbDataParameter_Precision")]
		[DefaultValue(0)]
		public byte Precision
		{
			get
			{
				return this._precision;
			}
			set
			{
				this._precision = value;
			}
		}

		/// <summary>Gets or sets the number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved.</summary>
		/// <returns>The number of decimal places to which <see cref="P:System.Data.Odbc.OdbcParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002D0 RID: 720
		// (get) Token: 0x06001094 RID: 4244 RVA: 0x00041104 File Offset: 0x0003F304
		// (set) Token: 0x06001095 RID: 4245 RVA: 0x0004110C File Offset: 0x0003F30C
		[DefaultValue(0)]
		[OdbcDescription("DbDataParameter_Scale")]
		[OdbcCategory("DataCategory_Data")]
		public byte Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				this._scale = value;
			}
		}

		/// <summary>Gets or sets the maximum size of the data within the column.</summary>
		/// <returns>The maximum size of the data within the column. The default value is inferred from the parameter value.</returns>
		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06001096 RID: 4246 RVA: 0x00041118 File Offset: 0x0003F318
		// (set) Token: 0x06001097 RID: 4247 RVA: 0x00041120 File Offset: 0x0003F320
		[OdbcCategory("DataCategory_Data")]
		[OdbcDescription("DbDataParameter_Size")]
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

		/// <summary>Gets or sets the name of the source column mapped to the <see cref="T:System.Data.DataSet" /> and used for loading or returning the <see cref="P:System.Data.Odbc.OdbcParameter.Value" />.</summary>
		/// <returns>The name of the source column that will be used to set the value of this parameter. The default is an empty string ("").</returns>
		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06001098 RID: 4248 RVA: 0x0004112C File Offset: 0x0003F32C
		// (set) Token: 0x06001099 RID: 4249 RVA: 0x00041134 File Offset: 0x0003F334
		[OdbcCategory("DataCategory_Data")]
		[OdbcDescription("DataParameter_SourceColumn")]
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

		/// <summary>Gets or sets the <see cref="T:System.Data.DataRowVersion" /> to use when you load <see cref="P:System.Data.Odbc.OdbcParameter.Value" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.DataRowVersion" /> values. The default is Current.</returns>
		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x0600109A RID: 4250 RVA: 0x00041140 File Offset: 0x0003F340
		// (set) Token: 0x0600109B RID: 4251 RVA: 0x00041148 File Offset: 0x0003F348
		[OdbcCategory("DataCategory_Data")]
		[OdbcDescription("DataParameter_SourceVersion")]
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
		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x0600109C RID: 4252 RVA: 0x00041154 File Offset: 0x0003F354
		// (set) Token: 0x0600109D RID: 4253 RVA: 0x0004115C File Offset: 0x0003F35C
		[OdbcDescription("DataParameter_Value")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter(typeof(StringConverter))]
		[OdbcCategory("DataCategory_Data")]
		public override object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00041168 File Offset: 0x0003F368
		internal void Bind(OdbcCommand command, IntPtr hstmt, int ParamNum)
		{
			OdbcInputOutputDirection odbcInputOutputDirection = libodbc.ConvertParameterDirection(this.Direction);
			this._cbLengthInd.EnsureAlloc(Marshal.SizeOf(typeof(int)));
			int num;
			if (this.Value is DBNull)
			{
				num = -1;
			}
			else
			{
				num = this.GetNativeSize();
				this.AllocateBuffer();
			}
			Marshal.WriteInt32(this._cbLengthInd, num);
			OdbcReturn odbcReturn = libodbc.SQLBindParameter(hstmt, (ushort)ParamNum, (short)odbcInputOutputDirection, this._typeMap.NativeType, this._typeMap.SqlType, Convert.ToUInt32(this.Size), 0, this._nativeBuffer, 0, this._cbLengthInd);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw command.Connection.CreateOdbcException(OdbcHandleType.Stmt, hstmt);
			}
		}

		/// <summary>Gets a string that contains the <see cref="P:System.Data.Odbc.OdbcParameter.ParameterName" />.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.Odbc.OdbcParameter.ParameterName" />.</returns>
		// Token: 0x0600109F RID: 4255 RVA: 0x00041230 File Offset: 0x0003F430
		public override string ToString()
		{
			return this.ParameterName;
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x00041238 File Offset: 0x0003F438
		private int GetNativeSize()
		{
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			Encoding encoding = Encoding.GetEncoding(textInfo.ANSICodePage);
			switch (this._typeMap.OdbcType)
			{
			case OdbcType.BigInt:
				return Marshal.SizeOf(typeof(long));
			case OdbcType.Binary:
				if (this.Value.GetType().IsArray && this.Value.GetType().GetElementType() == typeof(byte))
				{
					return ((Array)this.Value).Length;
				}
				return this.Value.ToString().Length;
			case OdbcType.Bit:
				return Marshal.SizeOf(typeof(byte));
			case OdbcType.Char:
			case OdbcType.Text:
			case OdbcType.VarChar:
				return encoding.GetByteCount(Convert.ToString(this.Value)) + 1;
			case OdbcType.DateTime:
			case OdbcType.SmallDateTime:
			case OdbcType.Timestamp:
			case OdbcType.Date:
			case OdbcType.Time:
				return 18;
			case OdbcType.Decimal:
			case OdbcType.Numeric:
				return 19;
			case OdbcType.Double:
				return Marshal.SizeOf(typeof(double));
			case OdbcType.Image:
			case OdbcType.VarBinary:
				if (this.Value.GetType().IsArray && this.Value.GetType().GetElementType() == typeof(byte))
				{
					return ((Array)this.Value).Length;
				}
				throw new ArgumentException("Unsupported Native Type!");
			case OdbcType.Int:
				return Marshal.SizeOf(typeof(int));
			case OdbcType.NChar:
			case OdbcType.NText:
			case OdbcType.NVarChar:
				return encoding.GetByteCount(Convert.ToString(this.Value)) + 1;
			case OdbcType.Real:
				return Marshal.SizeOf(typeof(float));
			case OdbcType.UniqueIdentifier:
				return Marshal.SizeOf(typeof(Guid));
			case OdbcType.SmallInt:
				return Marshal.SizeOf(typeof(short));
			case OdbcType.TinyInt:
				return Marshal.SizeOf(typeof(byte));
			default:
				if (this.Value.GetType().IsArray && this.Value.GetType().GetElementType() == typeof(byte))
				{
					return ((Array)this.Value).Length;
				}
				return this.Value.ToString().Length;
			}
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x00041480 File Offset: 0x0003F680
		private void AllocateBuffer()
		{
			int nativeSize = this.GetNativeSize();
			if (this._nativeBuffer.Size == nativeSize)
			{
				return;
			}
			this._nativeBuffer.AllocBuffer(nativeSize);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000414B4 File Offset: 0x0003F6B4
		internal void CopyValue()
		{
			if (this._nativeBuffer.Handle == IntPtr.Zero)
			{
				return;
			}
			if (this.Value is DBNull)
			{
				return;
			}
			TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
			Encoding encoding = Encoding.GetEncoding(textInfo.ANSICodePage);
			switch (this._typeMap.OdbcType)
			{
			case OdbcType.BigInt:
				Marshal.WriteInt64(this._nativeBuffer, Convert.ToInt64(this.Value));
				return;
			case OdbcType.Binary:
			case OdbcType.Image:
			case OdbcType.VarBinary:
				if (this.Value.GetType().IsArray && this.Value.GetType().GetElementType() == typeof(byte))
				{
					Marshal.Copy((byte[])this.Value, 0, this._nativeBuffer, ((byte[])this.Value).Length);
					return;
				}
				throw new ArgumentException("Unsupported Native Type!");
			case OdbcType.Bit:
				Marshal.WriteByte(this._nativeBuffer, Convert.ToByte(this.Value));
				return;
			case OdbcType.Char:
			case OdbcType.Text:
			case OdbcType.VarChar:
			{
				byte[] array = new byte[this.GetNativeSize()];
				byte[] array2 = encoding.GetBytes(Convert.ToString(this.Value));
				Array.Copy(array2, 0, array, 0, array2.Length);
				array[array.Length - 1] = 0;
				Marshal.Copy(array, 0, this._nativeBuffer, array.Length);
				Marshal.WriteInt32(this._cbLengthInd, -3);
				return;
			}
			case OdbcType.DateTime:
			case OdbcType.SmallDateTime:
			case OdbcType.Timestamp:
			{
				DateTime dateTime = (DateTime)this.Value;
				Marshal.WriteInt16(this._nativeBuffer, 0, (short)dateTime.Year);
				Marshal.WriteInt16(this._nativeBuffer, 2, (short)dateTime.Month);
				Marshal.WriteInt16(this._nativeBuffer, 4, (short)dateTime.Day);
				Marshal.WriteInt16(this._nativeBuffer, 6, (short)dateTime.Hour);
				Marshal.WriteInt16(this._nativeBuffer, 8, (short)dateTime.Minute);
				Marshal.WriteInt16(this._nativeBuffer, 10, (short)dateTime.Second);
				Marshal.WriteInt32(this._nativeBuffer, 12, (int)(dateTime.Ticks % 10000000L) * 100);
				return;
			}
			case OdbcType.Decimal:
			case OdbcType.Numeric:
			{
				int[] bits = decimal.GetBits(Convert.ToDecimal(this.Value));
				byte[] array = new byte[19];
				array[0] = this.Precision;
				array[1] = (byte)((bits[3] & 16711680) >> 16);
				array[2] = ((((long)bits[3] & (long)((ulong)int.MinValue)) <= 0L) ? 1 : 2);
				Buffer.BlockCopy(bits, 0, array, 3, 12);
				for (int i = 16; i < 19; i++)
				{
					array[i] = 0;
				}
				Marshal.Copy(array, 0, this._nativeBuffer, 19);
				return;
			}
			case OdbcType.Double:
				Marshal.StructureToPtr(Convert.ToDouble(this.Value), this._nativeBuffer, false);
				return;
			case OdbcType.Int:
				Marshal.WriteInt32(this._nativeBuffer, Convert.ToInt32(this.Value));
				return;
			case OdbcType.NChar:
			case OdbcType.NText:
			case OdbcType.NVarChar:
			{
				byte[] array = new byte[this.GetNativeSize()];
				byte[] array2 = encoding.GetBytes(Convert.ToString(this.Value));
				Array.Copy(array2, 0, array, 0, array2.Length);
				array[array.Length - 1] = 0;
				Marshal.Copy(array, 0, this._nativeBuffer, array.Length);
				Marshal.WriteInt32(this._cbLengthInd, -3);
				return;
			}
			case OdbcType.Real:
				Marshal.StructureToPtr(Convert.ToSingle(this.Value), this._nativeBuffer, false);
				return;
			case OdbcType.UniqueIdentifier:
				throw new NotImplementedException();
			case OdbcType.SmallInt:
				Marshal.WriteInt16(this._nativeBuffer, Convert.ToInt16(this.Value));
				return;
			case OdbcType.TinyInt:
				Marshal.WriteByte(this._nativeBuffer, Convert.ToByte(this.Value));
				return;
			case OdbcType.Date:
			{
				DateTime dateTime = (DateTime)this.Value;
				Marshal.WriteInt16(this._nativeBuffer, 0, (short)dateTime.Year);
				Marshal.WriteInt16(this._nativeBuffer, 2, (short)dateTime.Month);
				Marshal.WriteInt16(this._nativeBuffer, 4, (short)dateTime.Day);
				return;
			}
			case OdbcType.Time:
			{
				DateTime dateTime = (DateTime)this.Value;
				Marshal.WriteInt16(this._nativeBuffer, 0, (short)dateTime.Hour);
				Marshal.WriteInt16(this._nativeBuffer, 2, (short)dateTime.Minute);
				Marshal.WriteInt16(this._nativeBuffer, 4, (short)dateTime.Second);
				return;
			}
			default:
				if (this.Value.GetType().IsArray && this.Value.GetType().GetElementType() == typeof(byte))
				{
					Marshal.Copy((byte[])this.Value, 0, this._nativeBuffer, ((byte[])this.Value).Length);
					return;
				}
				throw new ArgumentException("Unsupported Native Type!");
			}
		}

		/// <summary>Sets or gets a value which indicates whether the source column is nullable. This lets <see cref="T:System.Data.Common.DbCommandBuilder" /> correctly generate Update statements for nullable columns.</summary>
		/// <returns>true if the source column is nullable; false if it is not.</returns>
		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x00041A00 File Offset: 0x0003FC00
		// (set) Token: 0x060010A4 RID: 4260 RVA: 0x00041A04 File Offset: 0x0003FC04
		public override bool SourceColumnNullMapping
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010A5 RID: 4261 RVA: 0x00041A08 File Offset: 0x0003FC08
		public override void ResetDbType()
		{
			this._typeMap = OdbcTypeConverter.GetTypeMap(OdbcType.NVarChar);
		}

		/// <summary>Resets the type associated with this <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010A6 RID: 4262 RVA: 0x00041A18 File Offset: 0x0003FC18
		public void ResetOdbcType()
		{
			this._typeMap = OdbcTypeConverter.GetTypeMap(OdbcType.NVarChar);
		}

		// Token: 0x04000577 RID: 1399
		private string name;

		// Token: 0x04000578 RID: 1400
		private ParameterDirection direction;

		// Token: 0x04000579 RID: 1401
		private bool isNullable;

		// Token: 0x0400057A RID: 1402
		private int size;

		// Token: 0x0400057B RID: 1403
		private DataRowVersion sourceVersion;

		// Token: 0x0400057C RID: 1404
		private string sourceColumn;

		// Token: 0x0400057D RID: 1405
		private byte _precision;

		// Token: 0x0400057E RID: 1406
		private byte _scale;

		// Token: 0x0400057F RID: 1407
		private object _value;

		// Token: 0x04000580 RID: 1408
		private OdbcTypeMap _typeMap;

		// Token: 0x04000581 RID: 1409
		private NativeBuffer _nativeBuffer = new NativeBuffer();

		// Token: 0x04000582 RID: 1410
		private NativeBuffer _cbLengthInd;

		// Token: 0x04000583 RID: 1411
		private OdbcParameterCollection container;
	}
}
