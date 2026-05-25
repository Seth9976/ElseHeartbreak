using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents an integer value that is either 1 or 0 to be stored in or retrieved from a database.</summary>
	// Token: 0x02000102 RID: 258
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlBoolean : IXmlSerializable, IComparable, INullable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure using the supplied Boolean value.</summary>
		/// <param name="value">The value for the new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure; either true or false. </param>
		// Token: 0x06000C67 RID: 3175 RVA: 0x00034E40 File Offset: 0x00033040
		public SqlBoolean(bool value)
		{
			this.value = ((!value) ? 0 : 1);
			this.notNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure using the specified integer value.</summary>
		/// <param name="value">The integer whose value is to be used for the new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C68 RID: 3176 RVA: 0x00034E60 File Offset: 0x00033060
		public SqlBoolean(int value)
		{
			this.value = ((value == 0) ? 0 : 1);
			this.notNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06000C6A RID: 3178 RVA: 0x00034EBC File Offset: 0x000330BC
		[MonoTODO]
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000C6B RID: 3179 RVA: 0x00034EC4 File Offset: 0x000330C4
		[MonoTODO]
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06000C6C RID: 3180 RVA: 0x00034ECC File Offset: 0x000330CC
		[MonoTODO]
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure as a byte.</summary>
		/// <returns>A byte representing the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</returns>
		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00034ED4 File Offset: 0x000330D4
		public byte ByteValue
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException(Locale.GetText("The property is set to null."));
				}
				return this.value;
			}
		}

		/// <summary>Indicates whether the current <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />.</summary>
		/// <returns>true if Value is False; otherwise, false.</returns>
		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x00034EF8 File Offset: 0x000330F8
		public bool IsFalse
		{
			get
			{
				return !this.IsNull && this.value == 0;
			}
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure is null.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure is null; otherwise false.</returns>
		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C6F RID: 3183 RVA: 0x00034F10 File Offset: 0x00033110
		public bool IsNull
		{
			get
			{
				return !this.notNull;
			}
		}

		/// <summary>Indicates whether the current <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" />.</summary>
		/// <returns>true if Value is True; otherwise, false.</returns>
		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x00034F1C File Offset: 0x0003311C
		public bool IsTrue
		{
			get
			{
				return !this.IsNull && this.value != 0;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value. This property is read-only.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" />; otherwise false.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The property is set to null. </exception>
		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x00034F38 File Offset: 0x00033138
		public bool Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException(Locale.GetText("The property is set to null."));
				}
				return this.IsTrue;
			}
		}

		/// <summary>Computes the bitwise AND operation of two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>The result of the logical AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C72 RID: 3186 RVA: 0x00034F5C File Offset: 0x0003315C
		public static SqlBoolean And(SqlBoolean x, SqlBoolean y)
		{
			return x & y;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to a specified object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and value.Value Description A negative integer This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">An object to compare, or a null reference (Nothing in Visual Basic). </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C73 RID: 3187 RVA: 0x00034F68 File Offset: 0x00033168
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is SqlBoolean))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Data.SqlTypes.SqlBoolean"));
			}
			return this.CompareTo((SqlBoolean)value);
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object to the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and value.Value Description A negative integer This instance is less than <paramref name="value" />. Zero This instance is equal to <paramref name="value" />. A positive integer This instance is greater than <paramref name="value" />.-or- <paramref name="value" /> is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /><see cref="T:System.Data.SqlTypes.SqlBoolean" /> object to compare, or a null reference (Nothing in Visual Basic).  </param>
		// Token: 0x06000C74 RID: 3188 RVA: 0x00034F9C File Offset: 0x0003319C
		public int CompareTo(SqlBoolean value)
		{
			if (value.IsNull)
			{
				return 1;
			}
			return this.value.CompareTo(value.ByteValue);
		}

		/// <summary>Compares the supplied object parameter to the <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> and the two are equal; otherwise, false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000C75 RID: 3189 RVA: 0x00034FCC File Offset: 0x000331CC
		public override bool Equals(object value)
		{
			if (!(value is SqlBoolean))
			{
				return false;
			}
			if (this.IsNull)
			{
				return ((SqlBoolean)value).IsNull;
			}
			return !((SqlBoolean)value).IsNull && (bool)(this == (SqlBoolean)value);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C76 RID: 3190 RVA: 0x0003502C File Offset: 0x0003322C
		public static SqlBoolean Equals(SqlBoolean x, SqlBoolean y)
		{
			return x == y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is greater than the second instance; otherwise false. </returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06000C77 RID: 3191 RVA: 0x00035038 File Offset: 0x00033238
		public static SqlBoolean GreaterThan(SqlBoolean x, SqlBoolean y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is greater than or equal to the second instance; otherwise false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06000C78 RID: 3192 RVA: 0x00035044 File Offset: 0x00033244
		public static SqlBoolean GreaterThanOrEquals(SqlBoolean x, SqlBoolean y)
		{
			return x >= y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is less than the second instance; otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06000C79 RID: 3193 RVA: 0x00035050 File Offset: 0x00033250
		public static SqlBoolean LessThan(SqlBoolean x, SqlBoolean y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is less than or equal to the second instance; otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06000C7A RID: 3194 RVA: 0x0003505C File Offset: 0x0003325C
		public static SqlBoolean LessThanOrEquals(SqlBoolean x, SqlBoolean y)
		{
			return x <= y;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000C7B RID: 3195 RVA: 0x00035068 File Offset: 0x00033268
		public override int GetHashCode()
		{
			int num;
			if (this.IsTrue)
			{
				num = 1;
			}
			else
			{
				num = 0;
			}
			return num;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> for equality.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C7C RID: 3196 RVA: 0x0003508C File Offset: 0x0003328C
		public static SqlBoolean NotEquals(SqlBoolean x, SqlBoolean y)
		{
			return x != y;
		}

		/// <summary>Performs a one's complement operation on the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>The one's complement of the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C7D RID: 3197 RVA: 0x00035098 File Offset: 0x00033298
		public static SqlBoolean OnesComplement(SqlBoolean x)
		{
			return ~x;
		}

		/// <summary>Performs a bitwise OR operation on the two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose Value is the result of the bitwise OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C7E RID: 3198 RVA: 0x000350A0 File Offset: 0x000332A0
		public static SqlBoolean Or(SqlBoolean x, SqlBoolean y)
		{
			return x | y;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> representation of a logical value to its <see cref="T:System.Data.SqlTypes.SqlBoolean" /> equivalent.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure that contains the parsed value.</returns>
		/// <param name="s">The <see cref="T:System.String" /> to be converted. </param>
		// Token: 0x06000C7F RID: 3199 RVA: 0x000350AC File Offset: 0x000332AC
		public static SqlBoolean Parse(string s)
		{
			if (s != null)
			{
				if (SqlBoolean.<>f__switch$map3 == null)
				{
					SqlBoolean.<>f__switch$map3 = new Dictionary<string, int>(2)
					{
						{ "0", 0 },
						{ "1", 1 }
					};
				}
				int num;
				if (SqlBoolean.<>f__switch$map3.TryGetValue(s, out num))
				{
					if (num == 0)
					{
						return new SqlBoolean(false);
					}
					if (num == 1)
					{
						return new SqlBoolean(true);
					}
				}
			}
			return new SqlBoolean(bool.Parse(s));
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's value is 1. Otherwise, the new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure's value is 0.</returns>
		// Token: 0x06000C80 RID: 3200 RVA: 0x00035130 File Offset: 0x00033330
		public SqlByte ToSqlByte()
		{
			return new SqlByte(this.value);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true then the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value is 1. Otherwise, the new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> structure's value is 0.</returns>
		// Token: 0x06000C81 RID: 3201 RVA: 0x00035140 File Offset: 0x00033340
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true then the new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's value is 1. Otherwise, the new <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure's value is 0.</returns>
		// Token: 0x06000C82 RID: 3202 RVA: 0x00035150 File Offset: 0x00033350
		public SqlDouble ToSqlDouble()
		{
			return (SqlDouble)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />.</summary>
		/// <returns>A new SqlInt16 structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true then the new <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure's value is 1. Otherwise, the new SqlInt16 structure's value is 0.</returns>
		// Token: 0x06000C83 RID: 3203 RVA: 0x00035160 File Offset: 0x00033360
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new SqlInt32 structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlInt32" /> structure's value is 1. Otherwise, the new SqlInt32 structure's value is 0.</returns>
		// Token: 0x06000C84 RID: 3204 RVA: 0x00035170 File Offset: 0x00033370
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new SqlInt64 structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlInt64" />  structure's value is 1. Otherwise, the new SqlInt64 structure's value is 0.</returns>
		// Token: 0x06000C85 RID: 3205 RVA: 0x00035180 File Offset: 0x00033380
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlMoney" /> value is 1. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals false, the new <see cref="T:System.Data.SqlTypes.SqlMoney" /> value is 0. If <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value is neither 1 nor 0, the new <see cref="T:System.Data.SqlTypes.SqlMoney" /> value is <see cref="F:System.Data.SqlTypes.SqlMoney.Null" />.</returns>
		// Token: 0x06000C86 RID: 3206 RVA: 0x00035190 File Offset: 0x00033390
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure whose value is 1 or 0.If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true, the new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure's value is 1; otherwise the new <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure's value is 0.</returns>
		// Token: 0x06000C87 RID: 3207 RVA: 0x000351A0 File Offset: 0x000333A0
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> structure whose value is 1 or 0. If the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure's value equals true then <see cref="T:System.Data.SqlTypes.SqlString" /> structure's value is 1. Otherwise the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure's value is 0.</returns>
		// Token: 0x06000C88 RID: 3208 RVA: 0x000351B0 File Offset: 0x000333B0
		public SqlString ToSqlString()
		{
			if (this.IsNull)
			{
				return new SqlString("Null");
			}
			if (this.IsTrue)
			{
				return new SqlString("True");
			}
			return new SqlString("False");
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to a string.</summary>
		/// <returns>A string that contains the value of the <see cref="T:System.Data.SqlTypes.SqlBoolean" />. If the value is null, the string will contain "null".</returns>
		// Token: 0x06000C89 RID: 3209 RVA: 0x000351F4 File Offset: 0x000333F4
		public override string ToString()
		{
			if (this.IsNull)
			{
				return "Null";
			}
			if (this.IsTrue)
			{
				return "True";
			}
			return "False";
		}

		/// <summary>Performs a bitwise exclusive-OR operation on the supplied parameters.</summary>
		/// <returns>The result of the logical XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C8A RID: 3210 RVA: 0x00035220 File Offset: 0x00033420
		public static SqlBoolean Xor(SqlBoolean x, SqlBoolean y)
		{
			return x ^ y;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0003522C File Offset: 0x0003342C
		private static int Compare(SqlBoolean x, SqlBoolean y)
		{
			if (x == y)
			{
				return 0;
			}
			if (x.IsTrue && y.IsFalse)
			{
				return 1;
			}
			if (x.IsFalse && y.IsTrue)
			{
				return -1;
			}
			return 0;
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000C8C RID: 3212 RVA: 0x00035284 File Offset: 0x00033484
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("boolean", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Computes the bitwise AND operation of two specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>The result of the logical AND operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C8D RID: 3213 RVA: 0x000352A4 File Offset: 0x000334A4
		public static SqlBoolean operator &(SqlBoolean x, SqlBoolean y)
		{
			return new SqlBoolean(x.Value & y.Value);
		}

		/// <summary>Computes the bitwise OR of its operands.</summary>
		/// <returns>The results of the logical OR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C8E RID: 3214 RVA: 0x000352BC File Offset: 0x000334BC
		public static SqlBoolean operator |(SqlBoolean x, SqlBoolean y)
		{
			return new SqlBoolean(x.Value | y.Value);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> for equality.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		// Token: 0x06000C8F RID: 3215 RVA: 0x000352D4 File Offset: 0x000334D4
		public static SqlBoolean operator ==(SqlBoolean x, SqlBoolean y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value == y.Value);
		}

		/// <summary>Performs a bitwise exclusive-OR (XOR) operation on the supplied parameters.</summary>
		/// <returns>The result of the logical XOR operation.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C90 RID: 3216 RVA: 0x00035314 File Offset: 0x00033514
		public static SqlBoolean operator ^(SqlBoolean x, SqlBoolean y)
		{
			return new SqlBoolean(x.Value ^ y.Value);
		}

		/// <summary>The false operator can be used to test the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether it is false.</summary>
		/// <returns>Returns true if the supplied parameter is <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is false, false otherwise.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be tested. </param>
		// Token: 0x06000C91 RID: 3217 RVA: 0x0003532C File Offset: 0x0003352C
		public static bool operator false(SqlBoolean x)
		{
			return x.IsFalse;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		// Token: 0x06000C92 RID: 3218 RVA: 0x00035338 File Offset: 0x00033538
		public static SqlBoolean operator !=(SqlBoolean x, SqlBoolean y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value != y.Value);
		}

		/// <summary>Performs a NOT operation on a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> with the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /><see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if argument was true, <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" /> if argument was null, and <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> otherwise.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> on which the NOT operation will be performed. </param>
		// Token: 0x06000C93 RID: 3219 RVA: 0x0003537C File Offset: 0x0003357C
		public static SqlBoolean operator !(SqlBoolean x)
		{
			if (x.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(!x.Value);
		}

		/// <summary>Performs a one's complement operation on the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures.</summary>
		/// <returns>The one's complement of the supplied <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C94 RID: 3220 RVA: 0x000353A0 File Offset: 0x000335A0
		public static SqlBoolean operator ~(SqlBoolean x)
		{
			SqlBoolean sqlBoolean;
			if (x.IsTrue)
			{
				sqlBoolean = new SqlBoolean(false);
			}
			else
			{
				sqlBoolean = new SqlBoolean(true);
			}
			return sqlBoolean;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is greater than the second instance; otherwise, false. </returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> object. </param>
		// Token: 0x06000C95 RID: 3221 RVA: 0x000353D0 File Offset: 0x000335D0
		public static SqlBoolean operator >(SqlBoolean x, SqlBoolean y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBoolean.Compare(x, y) > 0);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is greater than or equal to the second instance; otherwise, false. </returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06000C96 RID: 3222 RVA: 0x0003540C File Offset: 0x0003360C
		public static SqlBoolean operator >=(SqlBoolean x, SqlBoolean y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBoolean.Compare(x, y) >= 0);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is less than the second instance; otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06000C97 RID: 3223 RVA: 0x0003544C File Offset: 0x0003364C
		public static SqlBoolean operator <(SqlBoolean x, SqlBoolean y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBoolean.Compare(x, y) < 0);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is true if the first instance is less than or equal to the second instance; otherwise, false.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</param>
		// Token: 0x06000C98 RID: 3224 RVA: 0x00035488 File Offset: 0x00033688
		public static SqlBoolean operator <=(SqlBoolean x, SqlBoolean y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBoolean.Compare(x, y) <= 0);
		}

		/// <summary>The true operator can be used to test the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to determine whether it is true.</summary>
		/// <returns>Returns true if the supplied parameter is <see cref="T:System.Data.SqlTypes.SqlBoolean" /> is true, false otherwise.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be tested. </param>
		// Token: 0x06000C99 RID: 3225 RVA: 0x000354C8 File Offset: 0x000336C8
		public static bool operator true(SqlBoolean x)
		{
			return x.IsTrue;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to a Boolean.</summary>
		/// <returns>A Boolean set to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> to convert. </param>
		// Token: 0x06000C9A RID: 3226 RVA: 0x000354D4 File Offset: 0x000336D4
		public static explicit operator bool(SqlBoolean x)
		{
			return x.Value;
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlByte" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C9B RID: 3227 RVA: 0x000354E0 File Offset: 0x000336E0
		public static explicit operator SqlBoolean(SqlByte x)
		{
			if (x.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean((int)x.Value);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlDecimal.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDecimal" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C9C RID: 3228 RVA: 0x00035500 File Offset: 0x00033700
		public static explicit operator SqlBoolean(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean((int)x.Value);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlDouble.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlDouble" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C9D RID: 3229 RVA: 0x00035528 File Offset: 0x00033728
		public static explicit operator SqlBoolean(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean((int)x.Value);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlInt16.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt16" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C9E RID: 3230 RVA: 0x0003554C File Offset: 0x0003374C
		public static explicit operator SqlBoolean(SqlInt16 x)
		{
			if (x.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean((int)x.Value);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlInt32.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt32" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000C9F RID: 3231 RVA: 0x0003556C File Offset: 0x0003376C
		public static explicit operator SqlBoolean(SqlInt32 x)
		{
			if (x.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlInt64.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlInt64" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000CA0 RID: 3232 RVA: 0x0003558C File Offset: 0x0003378C
		public static explicit operator SqlBoolean(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(checked((int)x.Value));
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlMoney.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlMoney" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000CA1 RID: 3233 RVA: 0x000355B0 File Offset: 0x000337B0
		public static explicit operator SqlBoolean(SqlMoney x)
		{
			if (x.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean((int)x.Value);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlSingle.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlSingle" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000CA2 RID: 3234 RVA: 0x000355D8 File Offset: 0x000337D8
		public static explicit operator SqlBoolean(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean((int)x.Value);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlByte" /> structure whose value equals the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" /> to be converted to a <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure. </param>
		// Token: 0x06000CA3 RID: 3235 RVA: 0x000355FC File Offset: 0x000337FC
		public static explicit operator SqlBoolean(SqlString x)
		{
			if (x.IsNull)
			{
				return SqlBoolean.Null;
			}
			return SqlBoolean.Parse(x.Value);
		}

		/// <summary>Converts the supplied byte value to a <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> value that contains 0 or 1.</returns>
		/// <param name="x">A byte value to be converted to <see cref="T:System.Data.SqlTypes.SqlBoolean" />. </param>
		// Token: 0x06000CA4 RID: 3236 RVA: 0x0003561C File Offset: 0x0003381C
		public static implicit operator SqlBoolean(bool x)
		{
			return new SqlBoolean(x);
		}

		// Token: 0x040004C9 RID: 1225
		private byte value;

		// Token: 0x040004CA RID: 1226
		private bool notNull;

		/// <summary>Represents a false value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040004CB RID: 1227
		public static readonly SqlBoolean False = new SqlBoolean(false);

		/// <summary>Represents <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040004CC RID: 1228
		public static readonly SqlBoolean Null;

		/// <summary>Represents a one value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040004CD RID: 1229
		public static readonly SqlBoolean One = new SqlBoolean(1);

		/// <summary>Represents a true value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040004CE RID: 1230
		public static readonly SqlBoolean True = new SqlBoolean(true);

		/// <summary>Represents a zero value that can be assigned to the <see cref="P:System.Data.SqlTypes.SqlBoolean.ByteValue" /> property of an instance of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure.</summary>
		// Token: 0x040004CF RID: 1231
		public static readonly SqlBoolean Zero = new SqlBoolean(0);
	}
}
