using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XML IDREFS attribute.</summary>
	// Token: 0x020004DC RID: 1244
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapIdrefs : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> class.</summary>
		// Token: 0x0600320D RID: 12813 RVA: 0x000A2DB0 File Offset: 0x000A0FB0
		public SoapIdrefs()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> class with an XML IDREFS attribute.</summary>
		/// <param name="value">A <see cref="T:System.String" /> that contains an XML IDREFS attribute. </param>
		// Token: 0x0600320E RID: 12814 RVA: 0x000A2DB8 File Offset: 0x000A0FB8
		public SoapIdrefs(string value)
		{
			this._value = SoapHelper.Normalize(value);
		}

		/// <summary>Gets or sets an XML IDREFS attribute.</summary>
		/// <returns>A <see cref="T:System.String" /> that contains an XML IDREFS attribute.</returns>
		// Token: 0x17000977 RID: 2423
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000A2DCC File Offset: 0x000A0FCC
		// (set) Token: 0x06003210 RID: 12816 RVA: 0x000A2DD4 File Offset: 0x000A0FD4
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
		// Token: 0x17000978 RID: 2424
		// (get) Token: 0x06003211 RID: 12817 RVA: 0x000A2DE0 File Offset: 0x000A0FE0
		public static string XsdType
		{
			get
			{
				return "IDREFS";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003212 RID: 12818 RVA: 0x000A2DE8 File Offset: 0x000A0FE8
		public string GetXsdType()
		{
			return SoapIdrefs.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs" /> object.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		// Token: 0x06003213 RID: 12819 RVA: 0x000A2DF0 File Offset: 0x000A0FF0
		public static SoapIdrefs Parse(string value)
		{
			return new SoapIdrefs(value);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapIdrefs.Value" />.</returns>
		// Token: 0x06003214 RID: 12820 RVA: 0x000A2DF8 File Offset: 0x000A0FF8
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x0400150C RID: 5388
		private string _value;
	}
}
