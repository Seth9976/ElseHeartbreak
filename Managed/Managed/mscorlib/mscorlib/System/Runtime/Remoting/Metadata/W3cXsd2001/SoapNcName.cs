using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML NcName type.</summary>
	// Token: 0x020004D9 RID: 1241
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNcName : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> class.</summary>
		// Token: 0x060031F4 RID: 12788 RVA: 0x000A2C7C File Offset: 0x000A0E7C
		public SoapNcName()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> class with an XML NcName type.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML NcName type. </param>
		// Token: 0x060031F5 RID: 12789 RVA: 0x000A2C84 File Offset: 0x000A0E84
		public SoapNcName(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML NcName type.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML NcName type.</returns>
		// Token: 0x17000971 RID: 2417
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x000A2C98 File Offset: 0x000A0E98
		// (set) Token: 0x060031F7 RID: 12791 RVA: 0x000A2CA0 File Offset: 0x000A0EA0
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
		// Token: 0x17000972 RID: 2418
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x000A2CAC File Offset: 0x000A0EAC
		public static string XsdType
		{
			get
			{
				return "NCName";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x060031F9 RID: 12793 RVA: 0x000A2CB4 File Offset: 0x000A0EB4
		public string GetXsdType()
		{
			return SoapNcName.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x060031FA RID: 12794 RVA: 0x000A2CBC File Offset: 0x000A0EBC
		public static SoapNcName Parse(string value)
		{
			return new SoapNcName(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNcName.Value" />.</returns>
		// Token: 0x060031FB RID: 12795 RVA: 0x000A2CC4 File Offset: 0x000A0EC4
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04001508 RID: 5384
		private string _value;
	}
}
