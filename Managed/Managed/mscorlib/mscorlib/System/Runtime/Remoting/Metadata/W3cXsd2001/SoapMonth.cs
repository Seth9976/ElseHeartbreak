using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD gMonth type.</summary>
	// Token: 0x020004C9 RID: 1225
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonth : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth" /> class.</summary>
		// Token: 0x0600316B RID: 12651 RVA: 0x000A2190 File Offset: 0x000A0390
		public SoapMonth()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x0600316C RID: 12652 RVA: 0x000A2198 File Offset: 0x000A0398
		public SoapMonth(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x0600316E RID: 12654 RVA: 0x000A21C8 File Offset: 0x000A03C8
		// (set) Token: 0x0600316F RID: 12655 RVA: 0x000A21D0 File Offset: 0x000A03D0
		public DateTime Value
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
		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000A21DC File Offset: 0x000A03DC
		public static string XsdType
		{
			get
			{
				return "gMonth";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x06003171 RID: 12657 RVA: 0x000A21E4 File Offset: 0x000A03E4
		public string GetXsdType()
		{
			return SoapMonth.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The String to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x06003172 RID: 12658 RVA: 0x000A21EC File Offset: 0x000A03EC
		public static SoapMonth Parse(string value)
		{
			DateTime dateTime = DateTime.ParseExact(value, SoapMonth._datetimeFormats, null, DateTimeStyles.None);
			return new SoapMonth(dateTime);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonth.Value" /> in the format "--MM--".</returns>
		// Token: 0x06003173 RID: 12659 RVA: 0x000A2210 File Offset: 0x000A0410
		public override string ToString()
		{
			return this._value.ToString("--MM--", CultureInfo.InvariantCulture);
		}

		// Token: 0x040014F0 RID: 5360
		private static readonly string[] _datetimeFormats = new string[] { "--MM--", "--MM--zzz" };

		// Token: 0x040014F1 RID: 5361
		private DateTime _value;
	}
}
