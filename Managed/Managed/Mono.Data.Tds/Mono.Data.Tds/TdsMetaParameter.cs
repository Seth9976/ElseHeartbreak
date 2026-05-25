using System;
using System.Collections.Generic;
using System.Text;
using Mono.Data.Tds.Protocol;

namespace Mono.Data.Tds
{
	// Token: 0x02000007 RID: 7
	public class TdsMetaParameter
	{
		// Token: 0x0600000E RID: 14 RVA: 0x00003940 File Offset: 0x00001B40
		public TdsMetaParameter(string name, object value)
			: this(name, string.Empty, value)
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00003950 File Offset: 0x00001B50
		public TdsMetaParameter(string name, FrameworkValueGetter valueGetter)
			: this(name, string.Empty, null)
		{
			this.frameworkValueGetter = valueGetter;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00003968 File Offset: 0x00001B68
		public TdsMetaParameter(string name, string typeName, object value)
		{
			this.ParameterName = name;
			this.Value = value;
			this.TypeName = typeName;
			this.IsNullable = false;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00003998 File Offset: 0x00001B98
		public TdsMetaParameter(string name, int size, bool isNullable, byte precision, byte scale, object value)
		{
			this.ParameterName = name;
			this.Size = size;
			this.IsNullable = isNullable;
			this.Precision = precision;
			this.Scale = scale;
			this.Value = value;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000039D8 File Offset: 0x00001BD8
		public TdsMetaParameter(string name, int size, bool isNullable, byte precision, byte scale, FrameworkValueGetter valueGetter)
		{
			this.ParameterName = name;
			this.Size = size;
			this.IsNullable = isNullable;
			this.Precision = precision;
			this.Scale = scale;
			this.frameworkValueGetter = valueGetter;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00003A18 File Offset: 0x00001C18
		// (set) Token: 0x06000014 RID: 20 RVA: 0x00003A20 File Offset: 0x00001C20
		public TdsParameterDirection Direction
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

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000015 RID: 21 RVA: 0x00003A2C File Offset: 0x00001C2C
		// (set) Token: 0x06000016 RID: 22 RVA: 0x00003A34 File Offset: 0x00001C34
		public string TypeName
		{
			get
			{
				return this.typeName;
			}
			set
			{
				this.typeName = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00003A40 File Offset: 0x00001C40
		// (set) Token: 0x06000018 RID: 24 RVA: 0x00003A48 File Offset: 0x00001C48
		public string ParameterName
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

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00003A54 File Offset: 0x00001C54
		// (set) Token: 0x0600001A RID: 26 RVA: 0x00003A5C File Offset: 0x00001C5C
		public bool IsNullable
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

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00003A68 File Offset: 0x00001C68
		// (set) Token: 0x0600001C RID: 28 RVA: 0x00003AD4 File Offset: 0x00001CD4
		public object Value
		{
			get
			{
				if (this.frameworkValueGetter != null)
				{
					object obj = this.frameworkValueGetter(this.rawValue, ref this.isUpdated);
					if (this.isUpdated)
					{
						this.value = obj;
					}
				}
				if (this.isUpdated)
				{
					this.value = this.ResizeValue(this.value);
					this.isUpdated = false;
				}
				return this.value;
			}
			set
			{
				this.value = value;
				this.rawValue = value;
				this.isUpdated = true;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00003AF8 File Offset: 0x00001CF8
		// (set) Token: 0x0600001E RID: 30 RVA: 0x00003B00 File Offset: 0x00001D00
		public object RawValue
		{
			get
			{
				return this.rawValue;
			}
			set
			{
				this.Value = value;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00003B0C File Offset: 0x00001D0C
		// (set) Token: 0x06000020 RID: 32 RVA: 0x00003B14 File Offset: 0x00001D14
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

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00003B20 File Offset: 0x00001D20
		// (set) Token: 0x06000022 RID: 34 RVA: 0x00003B9C File Offset: 0x00001D9C
		public byte Scale
		{
			get
			{
				if ((this.TypeName == "decimal" || this.TypeName == "numeric") && this.scale == 0 && !Convert.IsDBNull(this.Value))
				{
					int[] bits = decimal.GetBits(Convert.ToDecimal(this.Value));
					this.scale = (byte)((bits[3] >> 16) & 255);
				}
				return this.scale;
			}
			set
			{
				this.scale = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00003BA8 File Offset: 0x00001DA8
		// (set) Token: 0x06000024 RID: 36 RVA: 0x00003BB0 File Offset: 0x00001DB0
		public int Size
		{
			get
			{
				return this.GetSize();
			}
			set
			{
				this.size = value;
				this.isUpdated = true;
				this.isSizeSet = true;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00003BC8 File Offset: 0x00001DC8
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public bool IsVariableSizeType
		{
			get
			{
				return this.isVariableSizeType;
			}
			set
			{
				this.isVariableSizeType = value;
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003BDC File Offset: 0x00001DDC
		private object ResizeValue(object newValue)
		{
			if (newValue == DBNull.Value || newValue == null)
			{
				return newValue;
			}
			if (!this.isSizeSet || this.size <= 0)
			{
				return newValue;
			}
			string text = newValue as string;
			if (text != null)
			{
				if ((this.TypeName == "nvarchar" || this.TypeName == "nchar" || this.TypeName == "xml") && text.Length > this.size)
				{
					return text.Substring(0, this.size);
				}
			}
			else if (newValue.GetType() == typeof(byte[]))
			{
				byte[] array = (byte[])newValue;
				if (array.Length > this.size)
				{
					byte[] array2 = new byte[this.size];
					Array.Copy(array, array2, this.size);
					return array2;
				}
			}
			return newValue;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003CCC File Offset: 0x00001ECC
		internal string Prepare()
		{
			string text = this.TypeName;
			if (text == "varbinary")
			{
				int actualSize = this.Size;
				if (actualSize <= 0)
				{
					actualSize = this.GetActualSize();
				}
				if (actualSize > 8000)
				{
					text = "image";
				}
			}
			string text2 = "@";
			if (this.ParameterName[0] == '@')
			{
				text2 = string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder(string.Format("{0}{1} {2}", text2, this.ParameterName, text));
			string text3 = text;
			switch (text3)
			{
			case "decimal":
			case "numeric":
				stringBuilder.Append(string.Format("({0},{1})", (this.Precision != 0) ? this.Precision : 29, this.Scale));
				break;
			case "varchar":
			case "varbinary":
			{
				int num2 = this.Size;
				if (num2 <= 0)
				{
					num2 = this.GetActualSize();
					if (num2 <= 0)
					{
						num2 = 1;
					}
				}
				stringBuilder.Append((num2 <= 8000) ? string.Format("({0})", num2) : "(max)");
				break;
			}
			case "nvarchar":
			case "xml":
				stringBuilder.Append((this.Size <= 0) ? "(4000)" : ((this.Size <= 8000) ? string.Format("({0})", this.Size) : "(max)"));
				break;
			case "char":
			case "nchar":
			case "binary":
				if (this.isSizeSet && this.Size > 0)
				{
					stringBuilder.Append(string.Format("({0})", this.Size));
				}
				break;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003F38 File Offset: 0x00002138
		internal int GetActualSize()
		{
			if (this.Value == DBNull.Value || this.Value == null)
			{
				return 0;
			}
			string text = this.Value.GetType().ToString();
			if (text != null)
			{
				if (TdsMetaParameter.<>f__switch$map1 == null)
				{
					TdsMetaParameter.<>f__switch$map1 = new Dictionary<string, int>(2)
					{
						{ "System.String", 0 },
						{ "System.Byte[]", 1 }
					};
				}
				int num;
				if (TdsMetaParameter.<>f__switch$map1.TryGetValue(text, out num))
				{
					if (num == 0)
					{
						int num2 = ((string)this.value).Length;
						if (this.TypeName == "nvarchar" || this.TypeName == "nchar" || this.TypeName == "ntext" || this.TypeName == "xml")
						{
							num2 *= 2;
						}
						return num2;
					}
					if (num == 1)
					{
						return ((byte[])this.value).Length;
					}
				}
			}
			return this.GetSize();
		}

		// Token: 0x0600002A RID: 42 RVA: 0x0000404C File Offset: 0x0000224C
		private int GetSize()
		{
			string text = this.TypeName;
			switch (text)
			{
			case "decimal":
				return 17;
			case "uniqueidentifier":
				return 16;
			case "bigint":
			case "datetime":
			case "float":
			case "money":
				return 8;
			case "int":
			case "real":
			case "smalldatetime":
			case "smallmoney":
				return 4;
			case "smallint":
				return 2;
			case "tinyint":
			case "bit":
				return 1;
			case "nchar":
			case "ntext":
				return this.size * 2;
			}
			return this.size;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00004188 File Offset: 0x00002388
		internal byte[] GetBytes()
		{
			byte[] array = new byte[0];
			if (this.Value == DBNull.Value || this.Value == null)
			{
				return array;
			}
			string text = this.TypeName;
			if (text != null)
			{
				if (TdsMetaParameter.<>f__switch$map3 == null)
				{
					TdsMetaParameter.<>f__switch$map3 = new Dictionary<string, int>(7)
					{
						{ "nvarchar", 0 },
						{ "nchar", 0 },
						{ "ntext", 0 },
						{ "xml", 0 },
						{ "varchar", 1 },
						{ "char", 1 },
						{ "text", 1 }
					};
				}
				int num;
				if (TdsMetaParameter.<>f__switch$map3.TryGetValue(text, out num))
				{
					if (num == 0)
					{
						return Encoding.Unicode.GetBytes((string)this.Value);
					}
					if (num == 1)
					{
						return Encoding.Default.GetBytes((string)this.Value);
					}
				}
			}
			return (byte[])this.Value;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x0000428C File Offset: 0x0000248C
		internal TdsColumnType GetMetaType()
		{
			string text = this.TypeName;
			switch (text)
			{
			case "binary":
				return TdsColumnType.BigBinary;
			case "bit":
				if (this.IsNullable)
				{
					return TdsColumnType.BitN;
				}
				return TdsColumnType.Bit;
			case "bigint":
				if (this.IsNullable)
				{
					return TdsColumnType.IntN;
				}
				return TdsColumnType.BigInt;
			case "char":
				return TdsColumnType.Char;
			case "money":
				if (this.IsNullable)
				{
					return TdsColumnType.MoneyN;
				}
				return TdsColumnType.Money;
			case "smallmoney":
				if (this.IsNullable)
				{
					return TdsColumnType.MoneyN;
				}
				return TdsColumnType.Money4;
			case "decimal":
				return TdsColumnType.Decimal;
			case "datetime":
				if (this.IsNullable)
				{
					return TdsColumnType.DateTimeN;
				}
				return TdsColumnType.DateTime;
			case "smalldatetime":
				if (this.IsNullable)
				{
					return TdsColumnType.DateTimeN;
				}
				return TdsColumnType.DateTime4;
			case "float":
				if (this.IsNullable)
				{
					return TdsColumnType.FloatN;
				}
				return TdsColumnType.Float8;
			case "image":
				return TdsColumnType.Image;
			case "int":
				if (this.IsNullable)
				{
					return TdsColumnType.IntN;
				}
				return TdsColumnType.Int4;
			case "numeric":
				return TdsColumnType.Numeric;
			case "nchar":
				return TdsColumnType.NChar;
			case "ntext":
				return TdsColumnType.NText;
			case "xml":
			case "nvarchar":
				return TdsColumnType.BigNVarChar;
			case "real":
				if (this.IsNullable)
				{
					return TdsColumnType.FloatN;
				}
				return TdsColumnType.Real;
			case "smallint":
				if (this.IsNullable)
				{
					return TdsColumnType.IntN;
				}
				return TdsColumnType.Int2;
			case "text":
				return TdsColumnType.Text;
			case "tinyint":
				if (this.IsNullable)
				{
					return TdsColumnType.IntN;
				}
				return TdsColumnType.Int1;
			case "uniqueidentifier":
				return TdsColumnType.UniqueIdentifier;
			case "varbinary":
				return TdsColumnType.BigVarBinary;
			case "varchar":
				return TdsColumnType.BigVarChar;
			}
			throw new NotSupportedException("Unknown Type : " + this.TypeName);
		}

		// Token: 0x0600002D RID: 45 RVA: 0x0000456C File Offset: 0x0000276C
		public void Validate(int index)
		{
			if ((this.direction == TdsParameterDirection.InputOutput || this.direction == TdsParameterDirection.Output) && this.isVariableSizeType && (this.Value == DBNull.Value || this.Value == null) && this.Size == 0)
			{
				throw new InvalidOperationException(string.Format("{0}[{1}]: the Size property should not be of size 0", this.typeName, index));
			}
		}

		// Token: 0x04000030 RID: 48
		private TdsParameterDirection direction;

		// Token: 0x04000031 RID: 49
		private byte precision;

		// Token: 0x04000032 RID: 50
		private byte scale;

		// Token: 0x04000033 RID: 51
		private int size;

		// Token: 0x04000034 RID: 52
		private string typeName;

		// Token: 0x04000035 RID: 53
		private string name;

		// Token: 0x04000036 RID: 54
		private bool isSizeSet;

		// Token: 0x04000037 RID: 55
		private bool isNullable;

		// Token: 0x04000038 RID: 56
		private object value;

		// Token: 0x04000039 RID: 57
		private bool isVariableSizeType;

		// Token: 0x0400003A RID: 58
		private FrameworkValueGetter frameworkValueGetter;

		// Token: 0x0400003B RID: 59
		private object rawValue;

		// Token: 0x0400003C RID: 60
		private bool isUpdated;
	}
}
