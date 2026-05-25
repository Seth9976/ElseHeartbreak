using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace System.Data.SqlTypes
{
	/// <summary>Represents XML data stored in or retrieved from a server.</summary>
	// Token: 0x02000117 RID: 279
	[XmlSchemaProvider("GetXsdType")]
	[Serializable]
	public sealed class SqlXml : IXmlSerializable, INullable
	{
		/// <summary>Creates a new <see cref="T:System.Data.SqlTypes.SqlXml" /> instance.</summary>
		// Token: 0x06000F9C RID: 3996 RVA: 0x0003D328 File Offset: 0x0003B528
		public SqlXml()
		{
			this.notNull = false;
			this.xmlValue = null;
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlTypes.SqlXml" /> instance, supplying the XML value from the supplied <see cref="T:System.IO.Stream" />-derived instance.</summary>
		/// <param name="value">A <see cref="T:System.IO.Stream" />-derived instance (such as <see cref="T:System.IO.FileStream" />) from which to load the <see cref="T:System.Data.SqlTypes.SqlXml" /> instance's Xml content.</param>
		// Token: 0x06000F9D RID: 3997 RVA: 0x0003D340 File Offset: 0x0003B540
		public SqlXml(Stream value)
		{
			if (value == null)
			{
				this.notNull = false;
				this.xmlValue = null;
			}
			else
			{
				int i = (int)value.Length;
				if (i < 1)
				{
					this.xmlValue = string.Empty;
				}
				else
				{
					int num = 8192;
					StringBuilder stringBuilder = new StringBuilder(i);
					value.Position = 0L;
					if (i < num)
					{
						num = i;
					}
					byte[] array = new byte[num];
					while (i > 0)
					{
						int num2 = value.Read(array, 0, num);
						stringBuilder.Append(Encoding.Unicode.GetString(array, 0, num2));
						if (num2 == 0)
						{
							break;
						}
						i -= num2;
					}
					this.xmlValue = stringBuilder.ToString();
				}
				this.notNull = true;
			}
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlTypes.SqlXml" /> instance and associates it with the content of the supplied <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <param name="value">An <see cref="T:System.Xml.XmlReader" />-derived class instance to be used as the value of the new <see cref="T:System.Data.SqlTypes.SqlXml" /> instance.</param>
		// Token: 0x06000F9E RID: 3998 RVA: 0x0003D404 File Offset: 0x0003B604
		public SqlXml(XmlReader value)
		{
			if (value == null)
			{
				this.notNull = false;
				this.xmlValue = null;
			}
			else
			{
				if (value.Read())
				{
					value.MoveToContent();
					this.xmlValue = value.ReadOuterXml();
				}
				else
				{
					this.xmlValue = string.Empty;
				}
				this.notNull = true;
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0003D468 File Offset: 0x0003B668
		[MonoTODO]
		XmlSchema IXmlSerializable.GetSchema()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0003D470 File Offset: 0x0003B670
		[MonoTODO]
		void IXmlSerializable.ReadXml(XmlReader r)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x0003D478 File Offset: 0x0003B678
		[MonoTODO]
		void IXmlSerializable.WriteXml(XmlWriter writer)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether this instance represents a null <see cref="T:System.Data.SqlTypes.SqlXml" /> value.</summary>
		/// <returns>true if Value is null. Otherwise, false.</returns>
		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000FA2 RID: 4002 RVA: 0x0003D480 File Offset: 0x0003B680
		public bool IsNull
		{
			get
			{
				return !this.notNull;
			}
		}

		/// <summary>Represents a null instance of the <see cref="T:System.Data.SqlTypes.SqlXml" /> type.</summary>
		/// <returns>A null instance of the <see cref="T:System.Data.SqlTypes.SqlXml" /> type.</returns>
		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000FA3 RID: 4003 RVA: 0x0003D48C File Offset: 0x0003B68C
		public static SqlXml Null
		{
			get
			{
				return new SqlXml();
			}
		}

		/// <summary>Gets the string representation of the XML content of this <see cref="T:System.Data.SqlTypes.SqlXml" /> instance.</summary>
		/// <returns>The string representation of the XML content.</returns>
		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000FA4 RID: 4004 RVA: 0x0003D494 File Offset: 0x0003B694
		public string Value
		{
			get
			{
				if (this.notNull)
				{
					return this.xmlValue;
				}
				throw new SqlNullValueException();
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</summary>
		/// <returns>A string that indicates the XSD of the specified <see cref="T:System.Xml.Schema.XmlSchemaSet" />. </returns>
		/// <param name="schemaSet">An <see cref="T:System.Xml.Schema.XmlSchemaSet" />.</param>
		// Token: 0x06000FA5 RID: 4005 RVA: 0x0003D4B0 File Offset: 0x0003B6B0
		public static XmlQualifiedName GetXsdType(XmlSchemaSet schemaSet)
		{
			return new XmlQualifiedName("anyType", "http://www.w3.org/2001/XMLSchema");
		}

		/// <summary>Gets the value of the XML content of this <see cref="T:System.Data.SqlTypes.SqlXml" /> as a <see cref="T:System.Xml.XmlReader" />.</summary>
		/// <returns>A <see cref="T:System.Xml.XmlReader" />-derived instance that contains the XML content. The actual type may vary (for example, the return value might be <see cref="T:System.Xml.XmlTextReader" />) depending on how the information is represented internally, on the server.</returns>
		/// <exception cref="T:System.Data.SqlTypes.SqlNullValueException">Attempt was made to access this property on a null instance of <see cref="T:System.Data.SqlTypes.SqlXml" />.</exception>
		// Token: 0x06000FA6 RID: 4006 RVA: 0x0003D4D0 File Offset: 0x0003B6D0
		public XmlReader CreateReader()
		{
			if (this.notNull)
			{
				XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
				xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
				return XmlReader.Create(new StringReader(this.xmlValue), xmlReaderSettings);
			}
			throw new SqlNullValueException();
		}

		// Token: 0x04000536 RID: 1334
		private bool notNull;

		// Token: 0x04000537 RID: 1335
		private string xmlValue;
	}
}
