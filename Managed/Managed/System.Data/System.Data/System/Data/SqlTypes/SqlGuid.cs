using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents a GUID to be stored in or retrieved from a database.</summary>
	// Token: 0x0200010A RID: 266
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public struct SqlGuid : IXmlSerializable, IComparable, INullable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure using the supplied byte array parameter.</summary>
		/// <param name="value">A byte array. </param>
		// Token: 0x06000DDB RID: 3547 RVA: 0x00039C58 File Offset: 0x00037E58
		public SqlGuid(byte[] value)
		{
			this.value = new Guid(value);
			this.notNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure using the specified <see cref="T:System.Guid" /> parameter.</summary>
		/// <param name="g">A <see cref="T:System.Guid" /></param>
		// Token: 0x06000DDC RID: 3548 RVA: 0x00039C70 File Offset: 0x00037E70
		public SqlGuid(Guid g)
		{
			this.value = g;
			this.notNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure using the specified <see cref="T:System.String" /> parameter.</summary>
		/// <param name="s">A <see cref="T:System.String" /> object. </param>
		// Token: 0x06000DDD RID: 3549 RVA: 0x00039C80 File Offset: 0x00037E80
		public SqlGuid(string s)
		{
			this.value = new Guid(s);
			this.notNull = true;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure using the specified values.</summary>
		/// <param name="a">The first four bytes of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="b">The next two bytes of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="c">The next two bytes of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="d">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="e">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="f">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="g">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="h">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="i">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="j">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		/// <param name="k">The next byte of the <see cref="T:System.Data.SqlTypes.SqlGuid" />. </param>
		// Token: 0x06000DDE RID: 3550 RVA: 0x00039C98 File Offset: 0x00037E98
		public SqlGuid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this.value = new Guid(a, b, c, d, e, f, g, h, i, j, k);
			this.notNull = true;
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <returns>An XmlSchema.</returns>
		// Token: 0x06000DDF RID: 3551 RVA: 0x00039CCC File Offset: 0x00037ECC
		[MonoTODO]
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="reader">XmlReader </param>
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00039CD4 File Offset: 0x00037ED4
		[MonoTODO]
		void IXmlSerializable.ReadXml(XmlReader reader)
		{
			throw new NotImplementedException();
		}

		/// <summary>This member supports the .NET Framework infrastructure and is not intended to be used directly from your code.</summary>
		/// <param name="writer">XmlWriter </param>
		// Token: 0x06000DE1 RID: 3553 RVA: 0x00039CDC File Offset: 0x00037EDC
		[MonoTODO]
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a Boolean value that indicates whether this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure is null.</summary>
		/// <returns>true if null. Otherwise, false.</returns>
		// Token: 0x1700027D RID: 637
		// (get) Token: 0x06000DE2 RID: 3554 RVA: 0x00039CE4 File Offset: 0x00037EE4
		public bool IsNull
		{
			get
			{
				return !this.notNull;
			}
		}

		/// <summary>Gets the value of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. This property is read-only.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure.</returns>
		// Token: 0x1700027E RID: 638
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00039CF0 File Offset: 0x00037EF0
		public Guid Value
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

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to the supplied object and returns an indication of their relative values. Compares more than the last 6 bytes, but treats the last 6 bytes as the most significant ones in comparisons.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than object. Zero This instance is the same as object. Greater than zero This instance is greater than object -or- object is a null reference (Nothing) </returns>
		/// <param name="value">The <see cref="T:System.Object" /> to be compared. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000DE4 RID: 3556 RVA: 0x00039D10 File Offset: 0x00037F10
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is SqlGuid))
			{
				throw new ArgumentException(Locale.GetText("Value is not a System.Data.SqlTypes.SqlGuid"));
			}
			return this.CompareTo((SqlGuid)value);
		}

		/// <summary>Compares this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to the supplied <see cref="T:System.Data.SqlTypes.SqlGuid" /> and returns an indication of their relative values. Compares more than the last 6 bytes, but treats the last 6 bytes as the most significant ones in comparisons.</summary>
		/// <returns>A signed number that indicates the relative values of the instance and the object.Return Value Condition Less than zero This instance is less than object. Zero This instance is the same as object. Greater than zero This instance is greater than object -or- object is a null reference (Nothing). </returns>
		/// <param name="value">The <see cref="T:System.Data.SqlTypes.SqlGuid" /> to be compared.</param>
		// Token: 0x06000DE5 RID: 3557 RVA: 0x00039D44 File Offset: 0x00037F44
		public int CompareTo(SqlGuid value)
		{
			if (value.IsNull)
			{
				return 1;
			}
			return this.value.CompareTo(value.Value);
		}

		/// <summary>Compares the supplied object parameter to the <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> property of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> object.</summary>
		/// <returns>true if object is an instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> and the two are equal; otherwise false.</returns>
		/// <param name="value">The object to be compared. </param>
		// Token: 0x06000DE6 RID: 3558 RVA: 0x00039D74 File Offset: 0x00037F74
		public override bool Equals(object value)
		{
			if (!(value is SqlGuid))
			{
				return false;
			}
			if (this.IsNull)
			{
				return ((SqlGuid)value).IsNull;
			}
			return !((SqlGuid)value).IsNull && (bool)(this == (SqlGuid)value);
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlGuid" /> structures to determine whether they are equal.</summary>
		/// <returns>true if the two values are equal. Otherwise, false. If either instance is null, then the SqlGuid will be null.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DE7 RID: 3559 RVA: 0x00039DD4 File Offset: 0x00037FD4
		public static SqlBoolean Equals(SqlGuid x, SqlGuid y)
		{
			return x == y;
		}

		/// <summary>Returns the hash code of this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000DE8 RID: 3560 RVA: 0x00039DE0 File Offset: 0x00037FE0
		public override int GetHashCode()
		{
			byte[] array = this.ToByteArray();
			int num = 10;
			foreach (byte b in array)
			{
				num = 91 * num + b.GetHashCode();
			}
			return num;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DE9 RID: 3561 RVA: 0x00039E24 File Offset: 0x00038024
		public static SqlBoolean GreaterThan(SqlGuid x, SqlGuid y)
		{
			return x > y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DEA RID: 3562 RVA: 0x00039E30 File Offset: 0x00038030
		public static SqlBoolean GreaterThanOrEqual(SqlGuid x, SqlGuid y)
		{
			return x >= y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DEB RID: 3563 RVA: 0x00039E3C File Offset: 0x0003803C
		public static SqlBoolean LessThan(SqlGuid x, SqlGuid y)
		{
			return x < y;
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DEC RID: 3564 RVA: 0x00039E48 File Offset: 0x00038048
		public static SqlBoolean LessThanOrEqual(SqlGuid x, SqlGuid y)
		{
			return x <= y;
		}

		/// <summary>Performs a logical comparison on two <see cref="T:System.Data.SqlTypes.SqlGuid" /> structures to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DED RID: 3565 RVA: 0x00039E54 File Offset: 0x00038054
		public static SqlBoolean NotEquals(SqlGuid x, SqlGuid y)
		{
			return x != y;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> structure to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlGuid" /> equivalent to the value that is contained in the specified <see cref="T:System.String" />.</returns>
		/// <param name="s">The String to be parsed. </param>
		// Token: 0x06000DEE RID: 3566 RVA: 0x00039E60 File Offset: 0x00038060
		public static SqlGuid Parse(string s)
		{
			return new SqlGuid(s);
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to a byte array.</summary>
		/// <returns>An array of bytes representing the <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> of this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</returns>
		// Token: 0x06000DEF RID: 3567 RVA: 0x00039E68 File Offset: 0x00038068
		public byte[] ToByteArray()
		{
			return this.value.ToByteArray();
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to <see cref="T:System.Data.SqlTypes.SqlBinary" />.</summary>
		/// <returns>A SqlBinary structure that contains the bytes in the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</returns>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x00039E78 File Offset: 0x00038078
		public SqlBinary ToSqlBinary()
		{
			return (SqlBinary)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to <see cref="T:System.Data.SqlTypes.SqlString" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlString" /> structure that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000DF1 RID: 3569 RVA: 0x00039E88 File Offset: 0x00038088
		public SqlString ToSqlString()
		{
			return (SqlString)this;
		}

		/// <summary>Converts this <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the string representation of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000DF2 RID: 3570 RVA: 0x00039E98 File Offset: 0x00038098
		public override string ToString()
		{
			if (!this.notNull)
			{
				return "Null";
			}
			return this.value.ToString();
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string value that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</returns>
		/// <param name="schemaSet">A <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000DF3 RID: 3571 RVA: 0x00039EB8 File Offset: 0x000380B8
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("string", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Performs a logical comparison of two <see cref="T:System.Data.SqlTypes.SqlGuid" /> structures to determine whether they are equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are not equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x00039ED8 File Offset: 0x000380D8
		public static SqlBoolean operator ==(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(x.Value == y.Value);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is greater than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DF5 RID: 3573 RVA: 0x00039F1C File Offset: 0x0003811C
		public static SqlBoolean operator >(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.Value.CompareTo(y.Value) > 0)
			{
				return new SqlBoolean(true);
			}
			return new SqlBoolean(false);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is greater than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is greater than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DF6 RID: 3574 RVA: 0x00039F70 File Offset: 0x00038170
		public static SqlBoolean operator >=(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.Value.CompareTo(y.Value) >= 0)
			{
				return new SqlBoolean(true);
			}
			return new SqlBoolean(false);
		}

		/// <summary>Performs a logical comparison on two <see cref="T:System.Data.SqlTypes.SqlGuid" /> structures to determine whether they are not equal.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the two instances are not equal or <see cref="F:System.Data.SqlTypes.SqlBoolean.False" /> if the two instances are equal. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x00039FC4 File Offset: 0x000381C4
		public static SqlBoolean operator !=(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			return new SqlBoolean(!(x.Value == y.Value));
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is less than the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003A00C File Offset: 0x0003820C
		public static SqlBoolean operator <(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.Value.CompareTo(y.Value) < 0)
			{
				return new SqlBoolean(true);
			}
			return new SqlBoolean(false);
		}

		/// <summary>Compares two instances of <see cref="T:System.Data.SqlTypes.SqlGuid" /> to determine whether the first is less than or equal to the second.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlBoolean" /> that is <see cref="F:System.Data.SqlTypes.SqlBoolean.True" /> if the first instance is less than or equal to the second instance. Otherwise, <see cref="F:System.Data.SqlTypes.SqlBoolean.False" />. If either instance of <see cref="T:System.Data.SqlTypes.SqlGuid" /> is null, the <see cref="P:System.Data.SqlTypes.SqlBoolean.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBoolean" /> will be <see cref="F:System.Data.SqlTypes.SqlBoolean.Null" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		/// <param name="y">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003A060 File Offset: 0x00038260
		public static SqlBoolean operator <=(SqlGuid x, SqlGuid y)
		{
			if (x.IsNull || y.IsNull)
			{
				return SqlBoolean.Null;
			}
			if (x.Value.CompareTo(y.Value) <= 0)
			{
				return new SqlBoolean(true);
			}
			return new SqlBoolean(false);
		}

		/// <summary>Converts the <see cref="T:System.Data.SqlTypes.SqlBinary" /> parameter to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlGuid" /> whose <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> is equal to the <see cref="P:System.Data.SqlTypes.SqlBinary.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlBinary" /> parameter.</returns>
		/// <param name="x">A SqlBinary object. </param>
		// Token: 0x06000DFA RID: 3578 RVA: 0x0003A0B4 File Offset: 0x000382B4
		public static explicit operator SqlGuid(SqlBinary x)
		{
			return new SqlGuid(x.Value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Data.SqlTypes.SqlGuid" /> parameter to <see cref="T:System.Guid" />.</summary>
		/// <returns>A new <see cref="T:System.Guid" /> equal to the <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> of the <see cref="T:System.Data.SqlTypes.SqlGuid" />.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure. </param>
		// Token: 0x06000DFB RID: 3579 RVA: 0x0003A0C4 File Offset: 0x000382C4
		public static explicit operator Guid(SqlGuid x)
		{
			return x.Value;
		}

		/// <summary>Converts the specified <see cref="T:System.Data.SqlTypes.SqlString" /> structure to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlTypes.SqlGuid" /> whose <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> equals the value represented by the <see cref="T:System.Data.SqlTypes.SqlString" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Data.SqlTypes.SqlString" /> object. </param>
		// Token: 0x06000DFC RID: 3580 RVA: 0x0003A0D0 File Offset: 0x000382D0
		public static explicit operator SqlGuid(SqlString x)
		{
			return new SqlGuid(x.Value);
		}

		/// <summary>Converts the supplied <see cref="T:System.Guid" /> parameter to <see cref="T:System.Data.SqlTypes.SqlGuid" />.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlTypes.SqlGuid" /> whose <see cref="P:System.Data.SqlTypes.SqlGuid.Value" /> is equal to the <see cref="T:System.Guid" /> parameter.</returns>
		/// <param name="x">A <see cref="T:System.Guid" />. </param>
		// Token: 0x06000DFD RID: 3581 RVA: 0x0003A0E0 File Offset: 0x000382E0
		public static implicit operator SqlGuid(Guid x)
		{
			return new SqlGuid(x);
		}

		// Token: 0x04000505 RID: 1285
		private Guid value;

		// Token: 0x04000506 RID: 1286
		private bool notNull;

		/// <summary>Represents a <see cref="T:System.DBNull" />  that can be assigned to this instance of the <see cref="T:System.Data.SqlTypes.SqlGuid" /> structure.</summary>
		// Token: 0x04000507 RID: 1287
		public static readonly SqlGuid Null;
	}
}
