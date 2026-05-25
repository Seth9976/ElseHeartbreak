using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD base64Binary type. </summary>
	// Token: 0x020004CC RID: 1228
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapBase64Binary : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> class.</summary>
		// Token: 0x06003184 RID: 12676 RVA: 0x000A22FC File Offset: 0x000A04FC
		public SoapBase64Binary()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> class with the binary representation of a 64-bit number.</summary>
		/// <param name="value">A <see cref="T:System.Byte" /> array that contains a 64-bit number. </param>
		// Token: 0x06003185 RID: 12677 RVA: 0x000A2304 File Offset: 0x000A0504
		public SoapBase64Binary(byte[] value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the binary representation of a 64-bit number.</summary>
		/// <returns>A <see cref="T:System.Byte" /> array that contains the binary representation of a 64-bit number.</returns>
		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x000A2314 File Offset: 0x000A0514
		// (set) Token: 0x06003187 RID: 12679 RVA: 0x000A231C File Offset: 0x000A051C
		public byte[] Value
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
		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000A2328 File Offset: 0x000A0528
		public static string XsdType
		{
			get
			{
				return "base64Binary";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003189 RID: 12681 RVA: 0x000A2330 File Offset: 0x000A0530
		public string GetXsdType()
		{
			return SoapBase64Binary.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">One of the following:<paramref name="value" /> is null. The length of <paramref name="value" /> is less than 4.The length of <paramref name="value" /> is not a multiple of 4. </exception>
		// Token: 0x0600318A RID: 12682 RVA: 0x000A2338 File Offset: 0x000A0538
		public static SoapBase64Binary Parse(string value)
		{
			return new SoapBase64Binary(Convert.FromBase64String(value));
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapBase64Binary.Value" />.</returns>
		// Token: 0x0600318B RID: 12683 RVA: 0x000A2348 File Offset: 0x000A0548
		public override string ToString()
		{
			return Convert.ToBase64String(this._value);
		}

		// Token: 0x040014F4 RID: 5364
		private byte[] _value;
	}
}
