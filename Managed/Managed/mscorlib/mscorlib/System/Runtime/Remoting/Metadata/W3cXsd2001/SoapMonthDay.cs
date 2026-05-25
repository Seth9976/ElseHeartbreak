using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD gMonthDay type.</summary>
	// Token: 0x020004CE RID: 1230
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapMonthDay : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> class.</summary>
		// Token: 0x06003197 RID: 12695 RVA: 0x000A253C File Offset: 0x000A073C
		public SoapMonthDay()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x06003198 RID: 12696 RVA: 0x000A2544 File Offset: 0x000A0744
		public SoapMonthDay(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x0600319A RID: 12698 RVA: 0x000A2574 File Offset: 0x000A0774
		// (set) Token: 0x0600319B RID: 12699 RVA: 0x000A257C File Offset: 0x000A077C
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
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x0600319C RID: 12700 RVA: 0x000A2588 File Offset: 0x000A0788
		public static string XsdType
		{
			get
			{
				return "gMonthDay";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600319D RID: 12701 RVA: 0x000A2590 File Offset: 0x000A0790
		public string GetXsdType()
		{
			return SoapMonthDay.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x0600319E RID: 12702 RVA: 0x000A2598 File Offset: 0x000A0798
		public static SoapMonthDay Parse(string value)
		{
			DateTime dateTime = DateTime.ParseExact(value, SoapMonthDay._datetimeFormats, null, DateTimeStyles.None);
			return new SoapMonthDay(dateTime);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> that is obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapMonthDay.Value" /> in the format "'--'MM'-'dd".</returns>
		// Token: 0x0600319F RID: 12703 RVA: 0x000A25BC File Offset: 0x000A07BC
		public override string ToString()
		{
			return this._value.ToString("--MM-dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x040014F7 RID: 5367
		private static readonly string[] _datetimeFormats = new string[] { "--MM-dd", "--MM-ddzzz" };

		// Token: 0x040014F8 RID: 5368
		private DateTime _value;
	}
}
