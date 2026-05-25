using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML IDREFS attribute.</summary>
	// Token: 0x020004D8 RID: 1240
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdref : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> class.</summary>
		// Token: 0x060031EC RID: 12780 RVA: 0x000A2C2C File Offset: 0x000A0E2C
		public SoapIdref()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref" /> class with an XML IDREF attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML IDREF attribute. </param>
		// Token: 0x060031ED RID: 12781 RVA: 0x000A2C34 File Offset: 0x000A0E34
		public SoapIdref(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML IDREF attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML IDREF attribute.</returns>
		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x000A2C48 File Offset: 0x000A0E48
		// (set) Token: 0x060031EF RID: 12783 RVA: 0x000A2C50 File Offset: 0x000A0E50
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
		// Token: 0x17000970 RID: 2416
		// (get) Token: 0x060031F0 RID: 12784 RVA: 0x000A2C5C File Offset: 0x000A0E5C
		public static string XsdType
		{
			get
			{
				return "IDREF";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060031F1 RID: 12785 RVA: 0x000A2C64 File Offset: 0x000A0E64
		public string GetXsdType()
		{
			return SoapIdref.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060031F2 RID: 12786 RVA: 0x000A2C6C File Offset: 0x000A0E6C
		public static SoapIdref Parse(string value)
		{
			return new SoapIdref(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdref.Value" />.</returns>
		// Token: 0x060031F3 RID: 12787 RVA: 0x000A2C74 File Offset: 0x000A0E74
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04001507 RID: 5383
		private string _value;
	}
}
