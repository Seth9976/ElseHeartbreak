using System;
using System.Collections.Generic;
using System.Text;

namespace System.Xml.Linq
{
	/// <summary>Represents an XML attribute.</summary>
	// Token: 0x0200000D RID: 13
	public class XAttribute : XObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XAttribute" /> class from another <see cref="T:System.Xml.Linq.XAttribute" /> object. </summary>
		/// <param name="other">An <see cref="T:System.Xml.Linq.XAttribute" /> object to copy from.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="other" /> parameter is null.</exception>
		// Token: 0x0600001E RID: 30 RVA: 0x0000252C File Offset: 0x0000072C
		public XAttribute(XAttribute other)
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			this.name = other.name;
			this.value = other.value;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Xml.Linq.XAttribute" /> class from the specified name and value. </summary>
		/// <param name="name">The <see cref="T:System.Xml.Linq.XName" /> of the attribute.</param>
		/// <param name="value">An <see cref="T:System.Object" /> containing the value of the attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> or <paramref name="value" /> parameter is null.</exception>
		// Token: 0x0600001F RID: 31 RVA: 0x00002560 File Offset: 0x00000760
		public XAttribute(XName name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.name = name;
			this.SetValue(value);
		}

		/// <summary>Gets an empty collection of attributes.</summary>
		/// <returns>An <see cref="T:System.Collections.Generic.IEnumerable`1" /> of <see cref="T:System.Xml.Linq.XAttribute" /> containing an empty collection.</returns>
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000025B4 File Offset: 0x000007B4
		public static IEnumerable<XAttribute> EmptySequence
		{
			get
			{
				return XAttribute.empty_array;
			}
		}

		/// <summary>Determines if this attribute is a namespace declaration.</summary>
		/// <returns>true if this attribute is a namespace declaration; otherwise false.</returns>
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000022 RID: 34 RVA: 0x000025BC File Offset: 0x000007BC
		public bool IsNamespaceDeclaration
		{
			get
			{
				return this.name.Namespace == XNamespace.Xmlns || (this.name.LocalName == "xmlns" && this.name.Namespace == XNamespace.None);
			}
		}

		/// <summary>Gets the expanded name of this attribute.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XName" /> containing the name of this attribute.</returns>
		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002618 File Offset: 0x00000818
		public XName Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Gets the next attribute of the parent element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> containing the next attribute of the parent element.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002620 File Offset: 0x00000820
		// (set) Token: 0x06000025 RID: 37 RVA: 0x00002628 File Offset: 0x00000828
		public XAttribute NextAttribute
		{
			get
			{
				return this.next;
			}
			internal set
			{
				this.next = value;
			}
		}

		/// <summary>Gets the node type for this node.</summary>
		/// <returns>The node type. For <see cref="T:System.Xml.Linq.XAttribute" /> objects, this value is <see cref="F:System.Xml.XmlNodeType.Attribute" />.</returns>
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002634 File Offset: 0x00000834
		public override XmlNodeType NodeType
		{
			get
			{
				return XmlNodeType.Attribute;
			}
		}

		/// <summary>Gets the previous attribute of the parent element.</summary>
		/// <returns>An <see cref="T:System.Xml.Linq.XAttribute" /> containing the previous attribute of the parent element.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002638 File Offset: 0x00000838
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002640 File Offset: 0x00000840
		public XAttribute PreviousAttribute
		{
			get
			{
				return this.previous;
			}
			internal set
			{
				this.previous = value;
			}
		}

		/// <summary>Gets or sets the value of this attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the value of this attribute.</returns>
		/// <exception cref="T:System.ArgumentNullException">When setting, the <paramref name="value" /> is null.</exception>
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000029 RID: 41 RVA: 0x0000264C File Offset: 0x0000084C
		// (set) Token: 0x0600002A RID: 42 RVA: 0x0000265C File Offset: 0x0000085C
		public string Value
		{
			get
			{
				return XUtil.ToString(this.value);
			}
			set
			{
				this.value = value;
			}
		}

		/// <summary>Removes this attribute from its parent element.</summary>
		/// <exception cref="T:System.InvalidOperationException">The parent element is null.</exception>
		// Token: 0x0600002B RID: 43 RVA: 0x00002668 File Offset: 0x00000868
		public void Remove()
		{
			if (base.Parent != null)
			{
				if (this.next != null)
				{
					this.next.previous = this.previous;
				}
				if (this.previous != null)
				{
					this.previous.next = this.next;
				}
				if (base.Parent.FirstAttribute == this)
				{
					base.Parent.FirstAttribute = this.next;
				}
				if (base.Parent.LastAttribute == this)
				{
					base.Parent.LastAttribute = this.previous;
				}
				base.SetOwner(null);
			}
			this.next = null;
			this.previous = null;
		}

		/// <summary>Sets the value of this attribute.</summary>
		/// <param name="value">The value to assign to this attribute.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="value" /> parameter is null.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="value" /> is an <see cref="T:System.Xml.Linq.XObject" />.</exception>
		// Token: 0x0600002C RID: 44 RVA: 0x00002714 File Offset: 0x00000914
		public void SetValue(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			this.value = XUtil.ToString(value);
		}

		/// <summary>Converts the current <see cref="T:System.Xml.Linq.XAttribute" /> object to a string representation.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the XML text representation of an attribute and its value.</returns>
		// Token: 0x0600002D RID: 45 RVA: 0x00002734 File Offset: 0x00000934
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.name.ToString());
			stringBuilder.Append("=\"");
			int num = 0;
			for (;;)
			{
				int num2 = this.value.IndexOfAny(XAttribute.escapeChars, num);
				if (num2 < 0)
				{
					break;
				}
				stringBuilder.Append(this.value, num, num2 - num);
				char c = this.value[num2];
				switch (c)
				{
				case '\t':
					stringBuilder.Append("&#x9;");
					break;
				case '\n':
					stringBuilder.Append("&#xA;");
					break;
				default:
					switch (c)
					{
					case '<':
						stringBuilder.Append("&lt;");
						break;
					default:
						if (c != '"')
						{
							if (c == '&')
							{
								stringBuilder.Append("&amp;");
							}
						}
						else
						{
							stringBuilder.Append("&quot;");
						}
						break;
					case '>':
						stringBuilder.Append("&gt;");
						break;
					}
					break;
				case '\r':
					stringBuilder.Append("&#xD;");
					break;
				}
				num = num2 + 1;
			}
			if (num > 0)
			{
				stringBuilder.Append(this.value, num, this.value.Length - num);
			}
			else
			{
				stringBuilder.Append(this.value);
			}
			stringBuilder.Append("\"");
			return stringBuilder.ToString();
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Boolean" />.</summary>
		/// <returns>A <see cref="T:System.Boolean" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Boolean" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Boolean" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x0600002E RID: 46 RVA: 0x000028AC File Offset: 0x00000AAC
		public static explicit operator bool(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XUtil.ConvertToBoolean(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Boolean" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Boolean" /> value.</exception>
		// Token: 0x0600002F RID: 47 RVA: 0x000028CC File Offset: 0x00000ACC
		public static explicit operator bool?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new bool?(XUtil.ConvertToBoolean(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.DateTime" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTime" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x06000030 RID: 48 RVA: 0x00002914 File Offset: 0x00000B14
		public static explicit operator DateTime(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XUtil.ToDateTime(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTime" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTime" /> value.</exception>
		// Token: 0x06000031 RID: 49 RVA: 0x00002934 File Offset: 0x00000B34
		public static explicit operator DateTime?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new DateTime?(XUtil.ToDateTime(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.DateTimeOffset" />.</summary>
		/// <returns>A <see cref="T:System.DateTimeOffset" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.DateTimeOffset" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTimeOffset" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x06000032 RID: 50 RVA: 0x0000297C File Offset: 0x00000B7C
		public static explicit operator DateTimeOffset(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDateTimeOffset(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.DateTimeOffset" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.DateTimeOffset" /> value.</exception>
		// Token: 0x06000033 RID: 51 RVA: 0x0000299C File Offset: 0x00000B9C
		public static explicit operator DateTimeOffset?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new DateTimeOffset?(XmlConvert.ToDateTimeOffset(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Decimal" />.</summary>
		/// <returns>A <see cref="T:System.Decimal" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Decimal" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Decimal" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x06000034 RID: 52 RVA: 0x000029E4 File Offset: 0x00000BE4
		public static explicit operator decimal(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDecimal(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Decimal" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Decimal" /> value.</exception>
		// Token: 0x06000035 RID: 53 RVA: 0x00002A04 File Offset: 0x00000C04
		public static explicit operator decimal?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new decimal?(XmlConvert.ToDecimal(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Double" />.</summary>
		/// <returns>A <see cref="T:System.Double" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Double" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Double" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x06000036 RID: 54 RVA: 0x00002A4C File Offset: 0x00000C4C
		public static explicit operator double(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToDouble(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Double" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Double" /> value.</exception>
		// Token: 0x06000037 RID: 55 RVA: 0x00002A6C File Offset: 0x00000C6C
		public static explicit operator double?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new double?(XmlConvert.ToDouble(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Single" />.</summary>
		/// <returns>A <see cref="T:System.Single" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Single" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Single" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x06000038 RID: 56 RVA: 0x00002AB4 File Offset: 0x00000CB4
		public static explicit operator float(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToSingle(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Nullable`1" /> of <see cref="T:System.Single" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Single" /> value.</exception>
		// Token: 0x06000039 RID: 57 RVA: 0x00002AD4 File Offset: 0x00000CD4
		public static explicit operator float?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new float?(XmlConvert.ToSingle(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Guid" />.</summary>
		/// <returns>A <see cref="T:System.Guid" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Guid" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Guid" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x0600003A RID: 58 RVA: 0x00002B1C File Offset: 0x00000D1C
		public static explicit operator Guid(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToGuid(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Guid" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Guid" /> value.</exception>
		// Token: 0x0600003B RID: 59 RVA: 0x00002B3C File Offset: 0x00000D3C
		public static explicit operator Guid?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new Guid?(XmlConvert.ToGuid(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to an <see cref="T:System.Int32" />.</summary>
		/// <returns>A <see cref="T:System.Int32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Int32" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Int32" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x0600003C RID: 60 RVA: 0x00002B84 File Offset: 0x00000D84
		public static explicit operator int(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToInt32(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int32" />.</param>
		// Token: 0x0600003D RID: 61 RVA: 0x00002BA4 File Offset: 0x00000DA4
		public static explicit operator int?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new int?(XmlConvert.ToInt32(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to an <see cref="T:System.Int64" />.</summary>
		/// <returns>A <see cref="T:System.Int64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.Int64" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Int64" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x0600003E RID: 62 RVA: 0x00002BEC File Offset: 0x00000DEC
		public static explicit operator long(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToInt64(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.Int64" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.Int64" /> value.</exception>
		// Token: 0x0600003F RID: 63 RVA: 0x00002C0C File Offset: 0x00000E0C
		public static explicit operator long?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new long?(XmlConvert.ToInt64(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.UInt32" />.</summary>
		/// <returns>A <see cref="T:System.UInt32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.UInt32" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt32" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x06000040 RID: 64 RVA: 0x00002C54 File Offset: 0x00000E54
		[CLSCompliant(false)]
		public static explicit operator uint(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToUInt32(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt32" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt32" /> value.</exception>
		// Token: 0x06000041 RID: 65 RVA: 0x00002C74 File Offset: 0x00000E74
		[CLSCompliant(false)]
		public static explicit operator uint?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new uint?(XmlConvert.ToUInt32(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.UInt64" />.</summary>
		/// <returns>A <see cref="T:System.UInt64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.UInt64" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt64" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x06000042 RID: 66 RVA: 0x00002CBC File Offset: 0x00000EBC
		[CLSCompliant(false)]
		public static explicit operator ulong(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToUInt64(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.UInt64" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.UInt64" /> value.</exception>
		// Token: 0x06000043 RID: 67 RVA: 0x00002CDC File Offset: 0x00000EDC
		[CLSCompliant(false)]
		public static explicit operator ulong?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new ulong?(XmlConvert.ToUInt64(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.TimeSpan" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.TimeSpan" /> value.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="attribute" /> parameter is null.</exception>
		// Token: 0x06000044 RID: 68 RVA: 0x00002D24 File Offset: 0x00000F24
		public static explicit operator TimeSpan(XAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			return XmlConvert.ToTimeSpan(attribute.value);
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" />.</summary>
		/// <returns>A <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to a <see cref="T:System.Nullable`1" /> of <see cref="T:System.TimeSpan" />.</param>
		/// <exception cref="T:System.FormatException">The attribute does not contain a valid <see cref="T:System.TimeSpan" /> value.</exception>
		// Token: 0x06000045 RID: 69 RVA: 0x00002D44 File Offset: 0x00000F44
		public static explicit operator TimeSpan?(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return (attribute.value != null) ? new TimeSpan?(XmlConvert.ToTimeSpan(attribute.value)) : null;
		}

		/// <summary>Cast the value of this <see cref="T:System.Xml.Linq.XAttribute" /> to a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains the content of this <see cref="T:System.Xml.Linq.XAttribute" />.</returns>
		/// <param name="attribute">The <see cref="T:System.Xml.Linq.XAttribute" /> to cast to <see cref="T:System.String" />.</param>
		// Token: 0x06000046 RID: 70 RVA: 0x00002D8C File Offset: 0x00000F8C
		public static explicit operator string(XAttribute attribute)
		{
			if (attribute == null)
			{
				return null;
			}
			return attribute.value;
		}

		// Token: 0x04000026 RID: 38
		private static readonly XAttribute[] empty_array = new XAttribute[0];

		// Token: 0x04000027 RID: 39
		private XName name;

		// Token: 0x04000028 RID: 40
		private string value;

		// Token: 0x04000029 RID: 41
		private XAttribute next;

		// Token: 0x0400002A RID: 42
		private XAttribute previous;

		// Token: 0x0400002B RID: 43
		private static readonly char[] escapeChars = new char[] { '<', '>', '&', '"', '\r', '\n', '\t' };
	}
}
