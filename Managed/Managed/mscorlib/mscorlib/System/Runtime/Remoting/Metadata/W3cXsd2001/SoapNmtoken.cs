using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML NMTOKEN attribute.</summary>
	// Token: 0x020004E1 RID: 1249
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNmtoken : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> class.</summary>
		// Token: 0x06003231 RID: 12849 RVA: 0x000A33AC File Offset: 0x000A15AC
		public SoapNmtoken()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> class with an XML NMTOKEN attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> containing an XML NMTOKEN attribute. </param>
		// Token: 0x06003232 RID: 12850 RVA: 0x000A33B4 File Offset: 0x000A15B4
		public SoapNmtoken(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML NMTOKEN attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML NMTOKEN attribute.</returns>
		// Token: 0x17000980 RID: 2432
		// (get) Token: 0x06003233 RID: 12851 RVA: 0x000A33C8 File Offset: 0x000A15C8
		// (set) Token: 0x06003234 RID: 12852 RVA: 0x000A33D0 File Offset: 0x000A15D0
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
		// Token: 0x17000981 RID: 2433
		// (get) Token: 0x06003235 RID: 12853 RVA: 0x000A33DC File Offset: 0x000A15DC
		public static string XsdType
		{
			get
			{
				return "NMTOKEN";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003236 RID: 12854 RVA: 0x000A33E4 File Offset: 0x000A15E4
		public string GetXsdType()
		{
			return SoapNmtoken.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06003237 RID: 12855 RVA: 0x000A33EC File Offset: 0x000A15EC
		public static SoapNmtoken Parse(string value)
		{
			return new SoapNmtoken(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNmtoken.Value" />.</returns>
		// Token: 0x06003238 RID: 12856 RVA: 0x000A33F4 File Offset: 0x000A15F4
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04001510 RID: 5392
		private string _value;
	}
}
