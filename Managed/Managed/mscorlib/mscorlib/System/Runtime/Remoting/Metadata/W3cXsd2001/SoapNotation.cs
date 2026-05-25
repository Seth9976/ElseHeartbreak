using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML NOTATION attribute type.</summary>
	// Token: 0x020004DA RID: 1242
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapNotation : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> class.</summary>
		// Token: 0x060031FC RID: 12796 RVA: 0x000A2CCC File Offset: 0x000A0ECC
		public SoapNotation()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> class with an XML NOTATION attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML NOTATION attribute. </param>
		// Token: 0x060031FD RID: 12797 RVA: 0x000A2CD4 File Offset: 0x000A0ED4
		public SoapNotation(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets an XML NOTATION attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML NOTATION attribute.</returns>
		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x060031FE RID: 12798 RVA: 0x000A2CE4 File Offset: 0x000A0EE4
		// (set) Token: 0x060031FF RID: 12799 RVA: 0x000A2CEC File Offset: 0x000A0EEC
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
		// Token: 0x17000974 RID: 2420
		// (get) Token: 0x06003200 RID: 12800 RVA: 0x000A2CF8 File Offset: 0x000A0EF8
		public static string XsdType
		{
			get
			{
				return "NOTATION";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003201 RID: 12801 RVA: 0x000A2D00 File Offset: 0x000A0F00
		public string GetXsdType()
		{
			return SoapNotation.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06003202 RID: 12802 RVA: 0x000A2D08 File Offset: 0x000A0F08
		public static SoapNotation Parse(string value)
		{
			return new SoapNotation(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapNotation.Value" />.</returns>
		// Token: 0x06003203 RID: 12803 RVA: 0x000A2D10 File Offset: 0x000A0F10
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x04001509 RID: 5385
		private string _value;
	}
}
