using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML ID attribute.</summary>
	// Token: 0x020004D3 RID: 1235
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapId : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> class.</summary>
		// Token: 0x060031C3 RID: 12739 RVA: 0x000A28EC File Offset: 0x000A0AEC
		public SoapId()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> class with an XML ID attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML ID attribute. </param>
		// Token: 0x060031C4 RID: 12740 RVA: 0x000A28F4 File Offset: 0x000A0AF4
		public SoapId(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML ID attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML ID attribute.</returns>
		// Token: 0x17000965 RID: 2405
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000A2908 File Offset: 0x000A0B08
		// (set) Token: 0x060031C6 RID: 12742 RVA: 0x000A2910 File Offset: 0x000A0B10
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
		// Token: 0x17000966 RID: 2406
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x000A291C File Offset: 0x000A0B1C
		public static string XsdType
		{
			get
			{
				return "ID";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060031C8 RID: 12744 RVA: 0x000A2924 File Offset: 0x000A0B24
		public string GetXsdType()
		{
			return SoapId.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060031C9 RID: 12745 RVA: 0x000A292C File Offset: 0x000A0B2C
		public static SoapId Parse(string value)
		{
			return new SoapId(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapId.Value" />.</returns>
		// Token: 0x060031CA RID: 12746 RVA: 0x000A2934 File Offset: 0x000A0B34
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04001500 RID: 5376
		private string _value;
	}
}
