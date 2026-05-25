using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a variable-length stream of binary data to be stored in or retrieved from a database. </summary>
	// Token: 0x02000101 RID: 257
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlBinary : IXmlSerializable, IComparable, INullable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure, setting the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property to the contents of the supplied byte array.</summary>
		/// <param name="value">The byte array to be stored or retrieved. </param>
		// Token: 0x06000C45 RID: 3141 RVA: 0x000348B4 File Offset: 0x00032AB4
		public SqlBinary(byte[] value)
		{
			this.value = value;
			this.notNull = true;
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000348C4 File Offset: 0x00032AC4
		[MonoTODO]
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000348CC File Offset: 0x00032ACC
		[MonoTODO]
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000348D4 File Offset: 0x00032AD4
		[MonoTODO]
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure is null. This property is read-only.</summary>
		/// <returns>true if null. Otherwise false.</returns>
		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000348DC File Offset: 0x00032ADC
		public bool IsNull
		{
			get
			{
				return !this.notNull;
			}
		}

		/// <summary>Gets the single byte from the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property located at the position indicated by the integer parameter, <paramref name="index" />. If <paramref name="index" /> indicates a position beyond the end of the byte array, a <see cref="T:System.Data.SqlTypes.SqlNullValueException" /> will be raised. This property is read-only.</summary>
		/// <returns>The byte located at the position indicated by the integer parameter.</returns>
		/// <param name="index">The position of the byte to be retrieved. </param>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The property is read when the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property contains <see cref="F:System.Data.SqlTypes.SqlBinary.Null" />- or - The <paramref name="index" /> parameter indicates a position byond the length of the byte array as indicated by the <see cref="P:System.Data.SqlTypes.SqlBinary.Length" /> property. </exception>
		// Token: 0x17000255 RID: 597
		public byte this[int index]
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException("The property contains Null.");
				}
				if (index >= this.Length)
				{
					throw new IndexOutOfRangeException("The index parameter indicates a position beyond the length of the byte array.");
				}
				return this.value[index];
			}
		}

		/// <summary>Gets the length in bytes of the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property. This property is read-only.</summary>
		/// <returns>The length of the binary data in the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The <see cref="P:System.Data.SqlTypes.SqlBinary.Length" /> property is read when the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property contains <see cref="F:System.Data.SqlTypes.SqlBinary.Null" />. </exception>
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x00034920 File Offset: 0x00032B20
		public int Length
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException("The property contains Null.");
				}
				return this.value.Length;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. This property is read-only.</summary>
		/// <returns>The value of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property is read when the property contains <see cref="F:System.Data.SqlTypes.SqlBinary.Null" />. </exception>
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000C4C RID: 3148 RVA: 0x00034940 File Offset: 0x00032B40
		public byte[] Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException("The property contains Null.");
				}
				return this.value;
			}
		}

		/// <summary>Concatenates two specified <see cref="T:System.Data.SqlTypes.SqlBinary" /> values to create a new <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBinary" /> that is the concatenated value of x and y.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" />. </param>
		// Token: 0x06000C4D RID: 3149 RVA: 0x00034960 File Offset: 0x00032B60
		public static SqlBinary Add(SqlBinary x, SqlBinary y)
		{
			return x + y;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBinary" /> object to the supplied object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure and the object.Return value Condition Less than zero The value of this <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is less than the object. Zero This <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is the same as object. Greater than zero This <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is greater than object.-or- The object is a null reference. </returns>
		/// <param name="value">The object to be compared to this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000C4E RID: 3150 RVA: 0x0003496C File Offset: 0x00032B6C
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is SqlBinary))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Data.SqlTypes.SqlBinary"));
			}
			return this.CompareTo((SqlBinary)value);
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlBinary" /> object to the supplied <see cref="T:System.Data.SqlTypes.SqlBinary" /> object and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure and the object.Return value Condition Less than zero The value of this <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is less than the object. Zero This <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is the same as object. Greater than zero This <see cref="T:System.Data.SqlTypes.SqlBinary" /> object is greater than object.-or- The object is a null reference. </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlBinary" /> object to be compared to this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		// Token: 0x06000C4F RID: 3151 RVA: 0x000349A0 File Offset: 0x00032BA0
		public int CompareTo(SqlBinary value)
		{
			if (value.IsNull)
			{
				return 1;
			}
			return SqlBinary.Compare(this, value);
		}

		/// <summary>Concatenates two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to create a new <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <returns>The concatenated values of the <paramref name="x" /> and <paramref name="y" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		// Token: 0x06000C50 RID: 3152 RVA: 0x000349BC File Offset: 0x00032BBC
		public static SqlBinary Concat(SqlBinary x, SqlBinary y)
		{
			return x + y;
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000C51 RID: 3153 RVA: 0x000349C8 File Offset: 0x00032BC8
		public override bool Equals(object value)
		{
			if (!(value is SqlBinary))
			{
				return false;
			}
			if (this.IsNull)
			{
				return ((SqlBinary)value).IsNull;
			}
			return !((SqlBinary)value).IsNull && (bool)(this == (SqlBinary)value);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlBinary will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		// Token: 0x06000C52 RID: 3154 RVA: 0x00034A28 File Offset: 0x00032C28
		public static SqlBoolean Equals(SqlBinary x, SqlBinary y)
		{
			return x == y;
		}

		/// <summary>Returns the hash code for this <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000C53 RID: 3155 RVA: 0x00034A34 File Offset: 0x00032C34
		public override int GetHashCode()
		{
			int num = 10;
			for (int i = 0; i < this.value.Length; i++)
			{
				num = 91 * num + (int)this.value[i];
			}
			return num;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		// Token: 0x06000C54 RID: 3156 RVA: 0x00034A6C File Offset: 0x00032C6C
		public static SqlBoolean GreaterThan(SqlBinary x, SqlBinary y)
		{
			return x > y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		// Token: 0x06000C55 RID: 3157 RVA: 0x00034A78 File Offset: 0x00032C78
		public static SqlBoolean GreaterThanOrEqual(SqlBinary x, SqlBinary y)
		{
			return x >= y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		// Token: 0x06000C56 RID: 3158 RVA: 0x00034A84 File Offset: 0x00032C84
		public static SqlBoolean LessThan(SqlBinary x, SqlBinary y)
		{
			return x < y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		// Token: 0x06000C57 RID: 3159 RVA: 0x00034A90 File Offset: 0x00032C90
		public static SqlBoolean LessThanOrEqual(SqlBinary x, SqlBinary y)
		{
			return x <= y;
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure. </param>
		// Token: 0x06000C58 RID: 3160 RVA: 0x00034A9C File Offset: 0x00032C9C
		public static SqlBoolean NotEquals(SqlBinary x, SqlBinary y)
		{
			return x != y;
		}

		/// <summary>Converts this instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</returns>
		// Token: 0x06000C59 RID: 3161 RVA: 0x00034AA8 File Offset: 0x00032CA8
		public SqlGuid ToSqlGuid()
		{
			return (SqlGuid)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlBinary" /> object to a string.</summary>
		/// <returns>A string that contains the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBinary" />. If the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> is null the string will contain "null".</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000C5A RID: 3162 RVA: 0x00034AB8 File Offset: 0x00032CB8
		public override string ToString()
		{
			if (!this.notNull)
			{
				return "Null";
			}
			return "SqlBinary(" + this.value.Length + ")";
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00034AE8 File Offset: 0x00032CE8
		private static int Compare(SqlBinary x, SqlBinary y)
		{
			int num = 0;
			if (x.Value.Length != y.Value.Length)
			{
				num = x.Value.Length - y.Value.Length;
				if (num > 0)
				{
					for (int i = x.Value.Length - 1; i > x.Value.Length - num; i--)
					{
						if (x.Value[i] != 0)
						{
							return 1;
						}
					}
				}
				else
				{
					for (int j = y.Value.Length - 1; j > y.Value.Length - num; j--)
					{
						if (y.Value[j] != 0)
						{
							return -1;
						}
					}
				}
			}
			int num2 = ((num <= 0) ? x.Value.Length : y.Value.Length);
			for (int k = num2 - 1; k > 0; k--)
			{
				byte b = x.Value[k];
				byte b2 = y.Value[k];
				if (b > b2)
				{
					return 1;
				}
				if (b < b2)
				{
					return -1;
				}
			}
			return 0;
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />. </summary>
		/// <returns>A string that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000C5C RID: 3164 RVA: 0x00034C00 File Offset: 0x00032E00
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("base64Binary", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Concatenates the two <see cref="T:System.Data.SqlTypes.SqlBinary" /> parameters to create a new <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <returns>The concatenated values of the <paramref name="x" /> and <paramref name="y" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		// Token: 0x06000C5D RID: 3165 RVA: 0x00034C20 File Offset: 0x00032E20
		public static SqlBinary operator +(SqlBinary x, SqlBinary y)
		{
			byte[] array = new byte[x.Value.Length + y.Value.Length];
			int num = 0;
			int i;
			for (i = 0; i < x.Value.Length; i++)
			{
				array[i] = x.Value[i];
			}
			while (i < x.Value.Length + y.Value.Length)
			{
				array[i] = y.Value[num];
				num++;
				i++;
			}
			return new SqlBinary(array);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		// Token: 0x06000C5E RID: 3166 RVA: 0x00034CA8 File Offset: 0x00032EA8
		public static SqlBoolean operator ==(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.Compare(x, y) == 0);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		// Token: 0x06000C5F RID: 3167 RVA: 0x00034CE4 File Offset: 0x00032EE4
		public static SqlBoolean operator >(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.Compare(x, y) > 0);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structues to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		// Token: 0x06000C60 RID: 3168 RVA: 0x00034D20 File Offset: 0x00032F20
		public static SqlBoolean operator >=(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.Compare(x, y) >= 0);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		// Token: 0x06000C61 RID: 3169 RVA: 0x00034D60 File Offset: 0x00032F60
		public static SqlBoolean operator !=(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.Compare(x, y) != 0);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		// Token: 0x06000C62 RID: 3170 RVA: 0x00034DA0 File Offset: 0x00032FA0
		public static SqlBoolean operator <(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.Compare(x, y) < 0);
		}

		/// <summary>Compares two <see cref="T:System.Data.SqlTypes.SqlBinary" /> structures to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlBinary" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlBinary" /> object. </param>
		// Token: 0x06000C63 RID: 3171 RVA: 0x00034DDC File Offset: 0x00032FDC
		public static SqlBoolean operator <=(SqlBinary x, SqlBinary y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(SqlBinary.Compare(x, y) <= 0);
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure to a <see cref="T:System.Byte" /> array.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure to be converted. </param>
		// Token: 0x06000C64 RID: 3172 RVA: 0x00034E1C File Offset: 0x0003301C
		public static explicit operator byte[](SqlBinary x)
		{
			return x.Value;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to a <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <returns>The <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to be converted. </returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to be converted. </param>
		// Token: 0x06000C65 RID: 3173 RVA: 0x00034E28 File Offset: 0x00033028
		public static explicit operator SqlBinary(SqlGuid x)
		{
			return new SqlBinary(x.ToByteArray());
		}

		/// <summary>Converts an array of bytes to a <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure that represents the converted array of bytes.</returns>
		/// <param name="x">The array of bytes to be converted. </param>
		// Token: 0x06000C66 RID: 3174 RVA: 0x00034E38 File Offset: 0x00033038
		public static implicit operator SqlBinary(byte[] x)
		{
			return new SqlBinary(x);
		}

		// Token: 0x040004C6 RID: 1222
		private byte[] value;

		// Token: 0x040004C7 RID: 1223
		private bool notNull;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> structure.</summary>
		// Token: 0x040004C8 RID: 1224
		public static readonly SqlBinary Null;
	}
}
