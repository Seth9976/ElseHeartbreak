using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML ENTITY attribute.</summary>
	// Token: 0x020004C8 RID: 1224
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapEntity : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> class.</summary>
		// Token: 0x06003163 RID: 12643 RVA: 0x000A2140 File Offset: 0x000A0340
		public SoapEntity()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> class with an XML ENTITY attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML ENTITY attribute. </param>
		// Token: 0x06003164 RID: 12644 RVA: 0x000A2148 File Offset: 0x000A0348
		public SoapEntity(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML ENTITY attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML ENTITY attribute.</returns>
		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x000A215C File Offset: 0x000A035C
		// (set) Token: 0x06003166 RID: 12646 RVA: 0x000A2164 File Offset: 0x000A0364
		public string Value
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

		/// <summary>Gets the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000A2170 File Offset: 0x000A0370
		public static string XsdType
		{
			get
			{
				return "ENTITY";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003168 RID: 12648 RVA: 0x000A2178 File Offset: 0x000A0378
		public string GetXsdType()
		{
			return SoapEntity.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntities" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06003169 RID: 12649 RVA: 0x000A2180 File Offset: 0x000A0380
		public static SoapEntity Parse(string value)
		{
			return new SoapEntity(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapEntity.Value" />.</returns>
		// Token: 0x0600316A RID: 12650 RVA: 0x000A2188 File Offset: 0x000A0388
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040014EF RID: 5359
		private string _value;
	}
}
