using System;

namespace System.Data.Odbc
{
	// Token: 0x0200011D RID: 285
	internal class OdbcColumn
	{
		// Token: 0x06000FC3 RID: 4035 RVA: 0x0003D730 File Offset: 0x0003B930
		internal OdbcColumn(string Name, OdbcType Type)
		{
			this.ColumnName = Name;
			this.OdbcType = Type;
			this.AllowDBNull = false;
			this.MaxLength = 0;
			this.Digits = 0;
			this.Value = null;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x0003D784 File Offset: 0x0003B984
		internal OdbcColumn(string Name, SQL_TYPE type)
		{
			this.ColumnName = Name;
			this.AllowDBNull = false;
			this.MaxLength = 0;
			this.Digits = 0;
			this.Value = null;
			this.UpdateTypes(type);
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x0003D7D8 File Offset: 0x0003B9D8
		internal Type DataType
		{
			get
			{
				switch (this.OdbcType)
				{
				case OdbcType.BigInt:
					return typeof(long);
				case OdbcType.Binary:
				case OdbcType.Image:
				case OdbcType.VarBinary:
					return typeof(byte[]);
				case OdbcType.Bit:
					return typeof(bool);
				case OdbcType.Char:
				case OdbcType.NChar:
					return typeof(string);
				case OdbcType.DateTime:
				case OdbcType.SmallDateTime:
				case OdbcType.Timestamp:
				case OdbcType.Date:
					return typeof(DateTime);
				case OdbcType.Decimal:
				case OdbcType.Numeric:
					return typeof(decimal);
				case OdbcType.Double:
					return typeof(double);
				case OdbcType.Int:
					return typeof(int);
				case OdbcType.NText:
				case OdbcType.NVarChar:
				case OdbcType.Text:
				case OdbcType.VarChar:
					return typeof(string);
				case OdbcType.Real:
					return typeof(float);
				case OdbcType.UniqueIdentifier:
					return typeof(Guid);
				case OdbcType.SmallInt:
					return typeof(short);
				case OdbcType.TinyInt:
					return typeof(byte);
				case OdbcType.Time:
					return typeof(TimeSpan);
				default:
					throw new InvalidCastException();
				}
			}
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x0003D8F8 File Offset: 0x0003BAF8
		internal bool IsDateType
		{
			get
			{
				OdbcType odbcType = this.OdbcType;
				switch (odbcType)
				{
				case OdbcType.SmallDateTime:
				case OdbcType.Timestamp:
					break;
				default:
					if (odbcType != OdbcType.Date && odbcType != OdbcType.Time && odbcType != OdbcType.DateTime)
					{
						return false;
					}
					break;
				}
				return true;
			}
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x0003D944 File Offset: 0x0003BB44
		internal bool IsStringType
		{
			get
			{
				OdbcType odbcType = this.OdbcType;
				return odbcType == OdbcType.NText || odbcType == OdbcType.NVarChar || odbcType == OdbcType.Char || odbcType == OdbcType.Text || odbcType == OdbcType.VarChar;
			}
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0003D988 File Offset: 0x0003BB88
		internal bool IsVariableSizeType
		{
			get
			{
				if (this.IsStringType)
				{
					return true;
				}
				OdbcType odbcType = this.OdbcType;
				return odbcType == OdbcType.Binary || odbcType == OdbcType.Image || odbcType == OdbcType.VarBinary;
			}
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x0003D9C8 File Offset: 0x0003BBC8
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x0003DA04 File Offset: 0x0003BC04
		internal SQL_TYPE SqlType
		{
			get
			{
				if (this._sqlType == SQL_TYPE.UNASSIGNED)
				{
					this._sqlType = OdbcTypeConverter.GetTypeMap(this.OdbcType).SqlType;
				}
				return this._sqlType;
			}
			set
			{
				this._sqlType = value;
			}
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0003DA10 File Offset: 0x0003BC10
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x0003DA4C File Offset: 0x0003BC4C
		internal SQL_C_TYPE SqlCType
		{
			get
			{
				if (this._sqlCType == SQL_C_TYPE.UNASSIGNED)
				{
					this._sqlCType = OdbcTypeConverter.GetTypeMap(this.OdbcType).NativeType;
				}
				return this._sqlCType;
			}
			set
			{
				this._sqlCType = value;
			}
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x0003DA58 File Offset: 0x0003BC58
		internal void UpdateTypes(SQL_TYPE sqlType)
		{
			this.SqlType = sqlType;
			OdbcTypeMap typeMap = OdbcTypeConverter.GetTypeMap(this.SqlType);
			this.OdbcType = typeMap.OdbcType;
			this.SqlCType = typeMap.NativeType;
		}

		// Token: 0x0400053F RID: 1343
		internal string ColumnName;

		// Token: 0x04000540 RID: 1344
		internal OdbcType OdbcType;

		// Token: 0x04000541 RID: 1345
		private SQL_TYPE _sqlType = SQL_TYPE.UNASSIGNED;

		// Token: 0x04000542 RID: 1346
		private SQL_C_TYPE _sqlCType = SQL_C_TYPE.UNASSIGNED;

		// Token: 0x04000543 RID: 1347
		internal bool AllowDBNull;

		// Token: 0x04000544 RID: 1348
		internal int MaxLength;

		// Token: 0x04000545 RID: 1349
		internal int Digits;

		// Token: 0x04000546 RID: 1350
		internal object Value;
	}
}
