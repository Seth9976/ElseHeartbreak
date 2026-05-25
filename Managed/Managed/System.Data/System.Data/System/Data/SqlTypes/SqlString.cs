using System;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a variable-length stream of characters to be stored in or retrieved from the database. <see cref="T:System.Data.SqlTypes.SqlString" /> has a different underlying data structure from its corresponding .NET Framework <see cref="T:System.String" /> data type.</summary>
	// Token: 0x02000111 RID: 273
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlString : IXmlSerializable, IComparable, INullable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure using the specified string.</summary>
		/// <param name="data">The string to store. </param>
		// Token: 0x06000F46 RID: 3910 RVA: 0x0003C6F0 File Offset: 0x0003A8F0
		public SqlString(string data)
		{
			this.value = data;
			this.lcid = CultureInfo.CurrentCulture.LCID;
			if (this.value != null)
			{
				this.notNull = true;
			}
			else
			{
				this.notNull = false;
			}
			this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure using the specified string and locale id values.</summary>
		/// <param name="data">The string to store. </param>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		// Token: 0x06000F47 RID: 3911 RVA: 0x0003C730 File Offset: 0x0003A930
		public SqlString(string data, int lcid)
		{
			this.value = data;
			this.lcid = lcid;
			if (this.value != null)
			{
				this.notNull = true;
			}
			else
			{
				this.notNull = false;
			}
			this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure using the specified locale id, compare options, and data.</summary>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="data">The data array to store. </param>
		// Token: 0x06000F48 RID: 3912 RVA: 0x0003C774 File Offset: 0x0003A974
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data)
		{
			this = new SqlString(lcid, compareOptions, data, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure using the specified string, locale id, and compare option values.</summary>
		/// <param name="data">The string to store. </param>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		// Token: 0x06000F49 RID: 3913 RVA: 0x0003C780 File Offset: 0x0003A980
		public SqlString(string data, int lcid, SqlCompareOptions compareOptions)
		{
			this.value = data;
			this.lcid = lcid;
			this.compareOptions = compareOptions;
			if (this.value != null)
			{
				this.notNull = true;
			}
			else
			{
				this.notNull = false;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> class.</summary>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="data">The data array to store. </param>
		/// <param name="fUnicode">true if Unicode encoded. Otherwise, false. </param>
		// Token: 0x06000F4A RID: 3914 RVA: 0x0003C7B8 File Offset: 0x0003A9B8
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, bool fUnicode)
		{
			Encoding encoding = ((!fUnicode) ? Encoding.ASCII : Encoding.Unicode);
			this.value = encoding.GetString(data);
			this.lcid = lcid;
			this.compareOptions = compareOptions;
			if (this.value != null)
			{
				this.notNull = true;
			}
			else
			{
				this.notNull = false;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> class.</summary>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="data">The data array to store. </param>
		/// <param name="index">The starting index within the array. </param>
		/// <param name="count">The number of characters from index to copy. </param>
		// Token: 0x06000F4B RID: 3915 RVA: 0x0003C818 File Offset: 0x0003AA18
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, int index, int count)
		{
			this = new SqlString(lcid, compareOptions, data, index, count, true);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> class.</summary>
		/// <param name="lcid">Specifies the geographical locale and language for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="compareOptions">Specifies the compare options for the new <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <param name="data">The data array to store. </param>
		/// <param name="index">The starting index within the array. </param>
		/// <param name="count">The number of characters from index to copy. </param>
		/// <param name="fUnicode">true if Unicode encoded. Otherwise, false. </param>
		// Token: 0x06000F4C RID: 3916 RVA: 0x0003C828 File Offset: 0x0003AA28
		public SqlString(int lcid, SqlCompareOptions compareOptions, byte[] data, int index, int count, bool fUnicode)
		{
			Encoding encoding = ((!fUnicode) ? Encoding.ASCII : Encoding.Unicode);
			this.value = encoding.GetString(data, index, count);
			this.lcid = lcid;
			this.compareOptions = compareOptions;
			if (this.value != null)
			{
				this.notNull = true;
			}
			else
			{
				this.notNull = false;
			}
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x0003C88C File Offset: 0x0003AA8C
		static SqlString()
		{
			SqlString.DecimalFormat.NumberDecimalDigits = 13;
			SqlString.DecimalFormat.NumberGroupSeparator = string.Empty;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06000F4E RID: 3918 RVA: 0x0003C8EC File Offset: 0x0003AAEC
		XmlSchema IXmlSerializable.GetSchema()
		{
			return null;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader</param>
		// Token: 0x06000F4F RID: 3919 RVA: 0x0003C8F0 File Offset: 0x0003AAF0
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			if (reader == null)
			{
				return;
			}
			switch (reader.ReadState)
			{
			case ReadState.Error:
			case ReadState.EndOfFile:
			case ReadState.Closed:
				return;
			default:
				reader.MoveToContent();
				if (reader.EOF)
				{
					return;
				}
				reader.Read();
				if (reader.NodeType == XmlNodeType.EndElement)
				{
					return;
				}
				if (reader.Value.Length > 0)
				{
					if (string.Compare("Null", reader.Value) == 0)
					{
						this.notNull = false;
						return;
					}
					this.value = reader.Value;
					this.notNull = true;
					this.compareOptions = SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreWidth;
				}
				return;
			}
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter</param>
		// Token: 0x06000F50 RID: 3920 RVA: 0x0003C998 File Offset: 0x0003AB98
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			writer.WriteString(this.ToString());
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CompareInfo" /> object that defines how string comparisons should be performed for this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		/// <returns>A CompareInfo object that defines string comparison for this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000F51 RID: 3921 RVA: 0x0003C9A8 File Offset: 0x0003ABA8
		public CompareInfo CompareInfo
		{
			get
			{
				return new CultureInfo(this.lcid).CompareInfo;
			}
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CultureInfo" /> structure that represents information about the culture of this <see cref="T:System.Data.SqlTypes.SqlString" /> object.</summary>
		/// <returns>A <see cref="T:System.Globalization.CultureInfo" /> structure that describes information about the culture of this SqlString structure including the names of the culture, the writing system, and the calendar used, and also access to culture-specific objects that provide methods for common operations, such as formatting dates and sorting strings.</returns>
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000F52 RID: 3922 RVA: 0x0003C9BC File Offset: 0x0003ABBC
		public CultureInfo CultureInfo
		{
			get
			{
				return new CultureInfo(this.lcid);
			}
		}

		/// <summary>Indicates whether this <see cref="T:System.Data.SqlTypes.SqlString" /> structure is null.</summary>
		/// <returns>true if <see cref="P:System.Data.SqlTypes.SqlString.Value" /> is <see cref="F:System.Data.SqlTypes.SqlString.Null" />. Otherwise, false.</returns>
		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000F53 RID: 3923 RVA: 0x0003C9CC File Offset: 0x0003ABCC
		public bool IsNull
		{
			get
			{
				return !this.notNull;
			}
		}

		/// <summary>Specifies the geographical locale and language for the <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		/// <returns>The locale id for the string stored in the <see cref="P:System.Data.SqlTypes.SqlString.Value" /> property.</returns>
		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000F54 RID: 3924 RVA: 0x0003C9D8 File Offset: 0x0003ABD8
		public int LCID
		{
			get
			{
				return this.lcid;
			}
		}

		/// <summary>A combination of one or more of the <see cref="T:System.Data.SqlTypes.SqlCompareOptions" /> enumeration values that represent the way in which this <see cref="T:System.Data.SqlTypes.SqlString" /> should be compared to other <see cref="T:System.Data.SqlTypes.SqlString" /> structures.</summary>
		/// <returns>A value specifying how this <see cref="T:System.Data.SqlTypes.SqlString" /> should be compared to other <see cref="T:System.Data.SqlTypes.SqlString" /> structures.</returns>
		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000F55 RID: 3925 RVA: 0x0003C9E0 File Offset: 0x0003ABE0
		public SqlCompareOptions SqlCompareOptions
		{
			get
			{
				return this.compareOptions;
			}
		}

		/// <summary>Gets the string that is stored in this <see cref="T:System.Data.SqlTypes.SqlString" /> structure. This property is read-only.</summary>
		/// <returns>The string that is stored.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">The value of the string is <see cref="F:System.Data.SqlTypes.SqlString.Null" />. </exception>
		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000F56 RID: 3926 RVA: 0x0003C9E8 File Offset: 0x0003ABE8
		public string Value
		{
			get
			{
				if (this.IsNull)
				{
					throw new SqlNullValueException(Locale.GetText("The property contains Null."));
				}
				return this.value;
			}
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0003CA0C File Offset: 0x0003AC0C
		private CompareOptions CompareOptions
		{
			get
			{
				return (CompareOptions)(((this.compareOptions & SqlCompareOptions.BinarySort) == SqlCompareOptions.None) ? (this.compareOptions & (SqlCompareOptions.IgnoreCase | SqlCompareOptions.IgnoreKanaType | SqlCompareOptions.IgnoreNonSpace | SqlCompareOptions.IgnoreWidth)) : ((SqlCompareOptions)1073741824));
			}
		}

		/// <summary>Creates a copy of this <see cref="T:System.Data.SqlTypes.SqlString" /> object.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> object in which all property values are the same as the original.</returns>
		// Token: 0x06000F58 RID: 3928 RVA: 0x0003CA40 File Offset: 0x0003AC40
		public SqlString Clone()
		{
			return new SqlString(this.value, this.lcid, this.compareOptions);
		}

		/// <summary>Gets the <see cref="T:System.Globalization.CompareOptions" /> enumeration equilvalent of the specified <see cref="T:System.Data.SqlTypes.SqlCompareOptions" /> value.</summary>
		/// <returns>A CompareOptions value that corresponds to the SqlCompareOptions for this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		/// <param name="compareOptions">A <see cref="T:System.Data.SqlTypes.SqlCompareOptions" /> value that describes the comparison options for this <see cref="T:System.Data.SqlTypes.SqlString" /> structure. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F59 RID: 3929 RVA: 0x0003CA5C File Offset: 0x0003AC5C
		public static CompareOptions CompareOptionsFromSqlCompareOptions(SqlCompareOptions compareOptions)
		{
			CompareOptions compareOptions2 = CompareOptions.None;
			if ((compareOptions & SqlCompareOptions.IgnoreCase) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreCase;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreKanaType) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreKanaType;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreNonSpace) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreNonSpace;
			}
			if ((compareOptions & SqlCompareOptions.IgnoreWidth) != SqlCompareOptions.None)
			{
				compareOptions2 |= CompareOptions.IgnoreWidth;
			}
			if ((compareOptions & SqlCompareOptions.BinarySort) != SqlCompareOptions.None)
			{
				throw new ArgumentOutOfRangeException();
			}
			return compareOptions2;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlString" /> object to the supplied <see cref="T:System.Object" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F5A RID: 3930 RVA: 0x0003CAB0 File Offset: 0x0003ACB0
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is SqlString))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Data.SqlTypes.SqlString"));
			}
			return this.CompareSqlString((SqlString)value);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0003CAE4 File Offset: 0x0003ACE4
		private int CompareSqlString(SqlString value)
		{
			if (value.IsNull)
			{
				return 1;
			}
			if (value.CompareOptions != this.CompareOptions)
			{
				throw new SqlTypeException(Locale.GetText("Two strings to be compared have different collation"));
			}
			return this.CultureInfo.CompareInfo.Compare(this.value, value.Value, this.CompareOptions);
		}

		/// <summary>Concatenates the two specified <see cref="T:System.Data.SqlTypes.SqlString" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the newly concatenated value representing the contents of the two <see cref="T:System.Data.SqlTypes.SqlString" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		// Token: 0x06000F5C RID: 3932 RVA: 0x0003CB44 File Offset: 0x0003AD44
		public static SqlString Concat(SqlString x, SqlString y)
		{
			return x + y;
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlString.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlString" /> object.</summary>
		/// <returns>Equals will return true if the object is an instance of <see cref="T:System.Data.SqlTypes.SqlString" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F5D RID: 3933 RVA: 0x0003CB50 File Offset: 0x0003AD50
		public override bool Equals(object value)
		{
			if (!(value is SqlString))
			{
				return false;
			}
			if (this.IsNull)
			{
				return ((SqlString)value).IsNull;
			}
			return !((SqlString)value).IsNull && (bool)(this == (SqlString)value);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlString will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F5E RID: 3934 RVA: 0x0003CBB0 File Offset: 0x0003ADB0
		public static SqlBoolean Equals(SqlString x, SqlString y)
		{
			return x == y;
		}

		/// <summary>Gets the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F5F RID: 3935 RVA: 0x0003CBBC File Offset: 0x0003ADBC
		public override int GetHashCode()
		{
			int num = 10;
			for (int i = 0; i < this.value.Length; i++)
			{
				num = 91 * num + (int)(this.value[i] ^ this.value[i]);
			}
			num = 91 * num + this.lcid.GetHashCode();
			return (int)(91 * num + this.compareOptions);
		}

		/// <summary>Gets an array of bytes, that contains the contents of the <see cref="T:System.Data.SqlTypes.SqlString" /> in ANSI format.</summary>
		/// <returns>An byte array, that contains the contents of the <see cref="T:System.Data.SqlTypes.SqlString" /> in ANSI format.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F60 RID: 3936 RVA: 0x0003CC28 File Offset: 0x0003AE28
		public byte[] GetNonUnicodeBytes()
		{
			return Encoding.ASCII.GetBytes(this.value);
		}

		/// <summary>Gets an array of bytes, that contains the contents of the <see cref="T:System.Data.SqlTypes.SqlString" /> in Unicode format.</summary>
		/// <returns>An byte array, that contains the contents of the <see cref="T:System.Data.SqlTypes.SqlString" /> in Unicode format.</returns>
		// Token: 0x06000F61 RID: 3937 RVA: 0x0003CC3C File Offset: 0x0003AE3C
		public byte[] GetUnicodeBytes()
		{
			return Encoding.Unicode.GetBytes(this.value);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F62 RID: 3938 RVA: 0x0003CC50 File Offset: 0x0003AE50
		public static SqlBoolean GreaterThan(SqlString x, SqlString y)
		{
			return x > y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F63 RID: 3939 RVA: 0x0003CC5C File Offset: 0x0003AE5C
		public static SqlBoolean GreaterThanOrEqual(SqlString x, SqlString y)
		{
			return x >= y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F64 RID: 3940 RVA: 0x0003CC68 File Offset: 0x0003AE68
		public static SqlBoolean LessThan(SqlString x, SqlString y)
		{
			return x < y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F65 RID: 3941 RVA: 0x0003CC74 File Offset: 0x0003AE74
		public static SqlBoolean LessThanOrEqual(SqlString x, SqlString y)
		{
			return x <= y;
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F66 RID: 3942 RVA: 0x0003CC80 File Offset: 0x0003AE80
		public static SqlBoolean NotEquals(SqlString x, SqlString y)
		{
			return x != y;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlBoolean" />.</summary>
		/// <returns>true if the <see cref="P:System.Data.SqlTypes.SqlString.Value" /> is non-zero; false if zero; otherwise Null.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F67 RID: 3943 RVA: 0x0003CC8C File Offset: 0x0003AE8C
		public SqlBoolean ToSqlBoolean()
		{
			return (SqlBoolean)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlByte" />.</summary>
		/// <returns>A new SqlByte structure whose <see cref="P:System.Data.SqlTypes.SqlByte.Value" /> equals the number represented by this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F68 RID: 3944 RVA: 0x0003CC9C File Offset: 0x0003AE9C
		public SqlByte ToSqlByte()
		{
			return (SqlByte)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlDateTime" />.</summary>
		/// <returns>A new SqlDateTime structure that contains the date value represented by this <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F69 RID: 3945 RVA: 0x0003CCAC File Offset: 0x0003AEAC
		public SqlDateTime ToSqlDateTime()
		{
			return (SqlDateTime)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlDecimal" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDecimal" /> that contains the value of this <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		// Token: 0x06000F6A RID: 3946 RVA: 0x0003CCBC File Offset: 0x0003AEBC
		public SqlDecimal ToSqlDecimal()
		{
			return (SqlDecimal)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlDouble" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlDouble" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F6B RID: 3947 RVA: 0x0003CCCC File Offset: 0x0003AECC
		public SqlDouble ToSqlDouble()
		{
			return (SqlDouble)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure whose <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> is the Guid represented by this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F6C RID: 3948 RVA: 0x0003CCDC File Offset: 0x0003AEDC
		public SqlGuid ToSqlGuid()
		{
			return (SqlGuid)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt16" />. </summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt16" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />. </returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F6D RID: 3949 RVA: 0x0003CCEC File Offset: 0x0003AEEC
		public SqlInt16 ToSqlInt16()
		{
			return (SqlInt16)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt32" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt32" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />. </returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F6E RID: 3950 RVA: 0x0003CCFC File Offset: 0x0003AEFC
		public SqlInt32 ToSqlInt32()
		{
			return (SqlInt32)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlInt64" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlInt64" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F6F RID: 3951 RVA: 0x0003CD0C File Offset: 0x0003AF0C
		public SqlInt64 ToSqlInt64()
		{
			return (SqlInt64)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlMoney" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlMoney" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F70 RID: 3952 RVA: 0x0003CD1C File Offset: 0x0003AF1C
		public SqlMoney ToSqlMoney()
		{
			return (SqlMoney)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlSingle" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlSingle" /> that is equal to the numeric value of this <see cref="T:System.Data.SqlTypes.SqlString" />..</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000F71 RID: 3953 RVA: 0x0003CD2C File Offset: 0x0003AF2C
		public SqlSingle ToSqlSingle()
		{
			return (SqlSingle)this;
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlString" /> object to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> with the same value as this <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</returns>
		// Token: 0x06000F72 RID: 3954 RVA: 0x0003CD3C File Offset: 0x0003AF3C
		public override string ToString()
		{
			if (!this.notNull)
			{
				return "Null";
			}
			return (string)this;
		}

		/// <summary>Concatenates two specified <see cref="T:System.Data.SqlTypes.SqlString" /> values to create a new <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> that is the concatenated value of <paramref name="x" /> and <paramref name="y" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		// Token: 0x06000F73 RID: 3955 RVA: 0x0003CD5C File Offset: 0x0003AF5C
		public static SqlString Add(SqlString x, SqlString y)
		{
			return x + y;
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlString" /> instance to the supplied <see cref="T:System.Data.SqlTypes.SqlString" /> and returns an indication of their relative values.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return value Condition Less than zero This instance is less than the object. Zero This instance is the same as the object. Greater than zero This instance is greater than the object -or- The object is a null reference (Nothing in Visual Basic). </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlString" /> to be compared.</param>
		// Token: 0x06000F74 RID: 3956 RVA: 0x0003CD68 File Offset: 0x0003AF68
		public int CompareTo(SqlString value)
		{
			return this.CompareSqlString(value);
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000F75 RID: 3957 RVA: 0x0003CD74 File Offset: 0x0003AF74
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			if (schemaSet != null && schemaSet.Count == 0)
			{
				XmlSchema xmlSchema = new XmlSchema();
				XmlSchemaComplexType xmlSchemaComplexType = new XmlSchemaComplexType();
				xmlSchemaComplexType.Name = "string";
				xmlSchema.Items.Add(xmlSchemaComplexType);
				schemaSet.Add(xmlSchema);
			}
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Concatenates the two specified <see cref="T:System.Data.SqlTypes.SqlString" /> structures.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the newly concatenated value representing the contents of the two <see cref="T:System.Data.SqlTypes.SqlString" /> parameters.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		// Token: 0x06000F76 RID: 3958 RVA: 0x0003CDD0 File Offset: 0x0003AFD0
		public static SqlString operator +(SqlString x, SqlString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value + y.Value);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F77 RID: 3959 RVA: 0x0003CE14 File Offset: 0x0003B014
		public static SqlBoolean operator ==(SqlString x, SqlString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value == y.Value);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F78 RID: 3960 RVA: 0x0003CE58 File Offset: 0x0003B058
		public static SqlBoolean operator >(SqlString x, SqlString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.CompareTo(y) > 0);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F79 RID: 3961 RVA: 0x0003CE94 File Offset: 0x0003B094
		public static SqlBoolean operator >=(SqlString x, SqlString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.CompareTo(y) >= 0);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F7A RID: 3962 RVA: 0x0003CED4 File Offset: 0x0003B0D4
		public static SqlBoolean operator !=(SqlString x, SqlString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value != y.Value);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F7B RID: 3963 RVA: 0x0003CF18 File Offset: 0x0003B118
		public static SqlBoolean operator <(SqlString x, SqlString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.CompareTo(y) < 0);
		}

		/// <summary>Performs a logical comparison of the two <see cref="T:System.Data.SqlTypes.SqlString" /> operands to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlString" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlString" />. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000F7C RID: 3964 RVA: 0x0003CF54 File Offset: 0x0003B154
		public static SqlBoolean operator <=(SqlString x, SqlString y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.CompareTo(y) <= 0);
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlBoolean" /> structure to be converted. </param>
		// Token: 0x06000F7D RID: 3965 RVA: 0x0003CF94 File Offset: 0x0003B194
		public static explicit operator SqlString(SqlBoolean x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value.ToString());
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlByte" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlByte" /> structure to be converted. </param>
		// Token: 0x06000F7E RID: 3966 RVA: 0x0003CFC8 File Offset: 0x0003B1C8
		public static explicit operator SqlString(SqlByte x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value.ToString());
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlDateTime" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlDateTime" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDateTime" /> structure to be converted. </param>
		// Token: 0x06000F7F RID: 3967 RVA: 0x0003CFFC File Offset: 0x0003B1FC
		public static explicit operator SqlString(SqlDateTime x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value.ToString());
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlDecimal" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the SqlDecimal parameter.</returns>
		/// <param name="x">The SqlDecimal structure to be converted. </param>
		// Token: 0x06000F80 RID: 3968 RVA: 0x0003D030 File Offset: 0x0003B230
		public static explicit operator SqlString(SqlDecimal x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value.ToString());
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlDouble" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlDouble" /> structure to be converted. </param>
		// Token: 0x06000F81 RID: 3969 RVA: 0x0003D064 File Offset: 0x0003B264
		public static explicit operator SqlString(SqlDouble x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value.ToString());
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlGuid" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> whose value is the string representation of the specified <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to be converted. </param>
		// Token: 0x06000F82 RID: 3970 RVA: 0x0003D098 File Offset: 0x0003B298
		public static explicit operator SqlString(SqlGuid x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value.ToString());
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlInt16" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt16" /> structure to be converted. </param>
		// Token: 0x06000F83 RID: 3971 RVA: 0x0003D0CC File Offset: 0x0003B2CC
		public static explicit operator SqlString(SqlInt16 x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value.ToString());
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlInt32" /> parameter.</returns>
		/// <param name="x">The SqlInt32 structure to be converted. </param>
		// Token: 0x06000F84 RID: 3972 RVA: 0x0003D100 File Offset: 0x0003B300
		public static explicit operator SqlString(SqlInt32 x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value.ToString());
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> object that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlInt64" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlInt64" /> structure to be converted. </param>
		// Token: 0x06000F85 RID: 3973 RVA: 0x0003D134 File Offset: 0x0003B334
		public static explicit operator SqlString(SqlInt64 x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value.ToString());
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlMoney" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlMoney" /> structure to be converted. </param>
		// Token: 0x06000F86 RID: 3974 RVA: 0x0003D168 File Offset: 0x0003B368
		public static explicit operator SqlString(SqlMoney x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.ToString());
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlSingle" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlSingle" /> structure to be converted. </param>
		// Token: 0x06000F87 RID: 3975 RVA: 0x0003D188 File Offset: 0x0003B388
		public static explicit operator SqlString(SqlSingle x)
		{
			if (x.IsNull)
			{
				return SqlString.Null;
			}
			return new SqlString(x.Value.ToString());
		}

		/// <summary>Converts a <see cref="T:System.Data.SqlTypes.SqlString" /> to a <see cref="T:System.String" /></summary>
		/// <returns>A String, whose contents are the same as the <see cref="P:System.Data.SqlTypes.SqlString.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		/// <param name="x">The <see cref="T:System.Data.SqlTypes.SqlString" /> to be converted. </param>
		// Token: 0x06000F88 RID: 3976 RVA: 0x0003D1BC File Offset: 0x0003B3BC
		public static explicit operator string(SqlString x)
		{
			return x.Value;
		}

		/// <summary>Converts the <see cref="T:System.String" /> parameter to a <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> that contains the value of the specified String.</returns>
		/// <param name="x">The <see cref="T:System.String" /> to be converted. </param>
		// Token: 0x06000F89 RID: 3977 RVA: 0x0003D1C8 File Offset: 0x0003B3C8
		public static implicit operator SqlString(string x)
		{
			return new SqlString(x);
		}

		// Token: 0x04000527 RID: 1319
		private string value;

		// Token: 0x04000528 RID: 1320
		private bool notNull;

		// Token: 0x04000529 RID: 1321
		private int lcid;

		// Token: 0x0400052A RID: 1322
		private SqlCompareOptions compareOptions;

		/// <summary>Specifies that sorts should be based on a characters numeric value instead of its alphabetical value.</summary>
		// Token: 0x0400052B RID: 1323
		public static readonly int BinarySort = 32768;

		/// <summary>Specifies that <see cref="T:System.Data.SqlTypes.SqlString" /> comparisons should ignore case.</summary>
		// Token: 0x0400052C RID: 1324
		public static readonly int IgnoreCase = 1;

		/// <summary>Specifies that the string comparison must ignore the Kana type. </summary>
		// Token: 0x0400052D RID: 1325
		public static readonly int IgnoreKanaType = 8;

		/// <summary>Specifies that the string comparison must ignore non-space combining characters, such as diacritics. </summary>
		// Token: 0x0400052E RID: 1326
		public static readonly int IgnoreNonSpace = 2;

		/// <summary>Specifies that the string comparison must ignore the character width. </summary>
		// Token: 0x0400052F RID: 1327
		public static readonly int IgnoreWidth = 16;

		/// <summary>Represents a <see cref="T:System.DBNull" /> that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlString" /> structure.</summary>
		// Token: 0x04000530 RID: 1328
		public static readonly SqlString Null;

		// Token: 0x04000531 RID: 1329
		internal static NumberFormatInfo DecimalFormat = (NumberFormatInfo)NumberFormatInfo.InvariantInfo.Clone();
	}
}
