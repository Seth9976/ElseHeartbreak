using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD anyURI type.</summary>
	// Token: 0x020004C7 RID: 1223
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapAnyUri : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> class.</summary>
		// Token: 0x0600315B RID: 12635 RVA: 0x000A20F4 File Offset: 0x000A02F4
		public SoapAnyUri()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> class with the specified URI.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains a URI. </param>
		// Token: 0x0600315C RID: 12636 RVA: 0x000A20FC File Offset: 0x000A02FC
		public SoapAnyUri(string value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets a URI.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains a URI.</returns>
		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x000A210C File Offset: 0x000A030C
		// (set) Token: 0x0600315E RID: 12638 RVA: 0x000A2114 File Offset: 0x000A0314
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
		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000A2120 File Offset: 0x000A0320
		public static string XsdType
		{
			get
			{
				return "anyUri";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003160 RID: 12640 RVA: 0x000A2128 File Offset: 0x000A0328
		public string GetXsdType()
		{
			return SoapAnyUri.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		// Token: 0x06003161 RID: 12641 RVA: 0x000A2130 File Offset: 0x000A0330
		public static SoapAnyUri Parse(string value)
		{
			return new SoapAnyUri(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapAnyUri.Value" />.</returns>
		// Token: 0x06003162 RID: 12642 RVA: 0x000A2138 File Offset: 0x000A0338
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040014EE RID: 5358
		private string _value;
	}
}
