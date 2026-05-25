using System;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata.W3cXsd2001
{
	/// <summary>Wraps an XSD gDay type. </summary>
	// Token: 0x020004DB RID: 1243
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapDay : ISoapXsd
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> class.</summary>
		// Token: 0x06003204 RID: 12804 RVA: 0x000A2D18 File Offset: 0x000A0F18
		public SoapDay()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> class with a specified <see cref="T:System.DateTime" /> object.</summary>
		/// <param name="value">A <see cref="T:System.DateTime" /> object to initialize the current instance. </param>
		// Token: 0x06003205 RID: 12805 RVA: 0x000A2D20 File Offset: 0x000A0F20
		public SoapDay(DateTime value)
		{
			this._value = value;
		}

		/// <summary>Gets or sets the date and time of the current instance.</summary>
		/// <returns>The <see cref="T:System.DateTime" /> object that contains the date and time of the current instance.</returns>
		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000A2D50 File Offset: 0x000A0F50
		// (set) Token: 0x06003208 RID: 12808 RVA: 0x000A2D58 File Offset: 0x000A0F58
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
		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000A2D64 File Offset: 0x000A0F64
		public static string XsdType
		{
			get
			{
				return "gDay";
			}
		}

		/// <summary>Returns the XML Schema definition language (XSD) of the current SOAP type.</summary>
		/// <returns>A <see cref="T:System.String" /> that indicates the XSD of the current SOAP type.</returns>
		// Token: 0x0600320A RID: 12810 RVA: 0x000A2D6C File Offset: 0x000A0F6C
		public string GetXsdType()
		{
			return SoapDay.XsdType;
		}

		/// <summary>Converts the specified <see cref="T:System.String" /> into a <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> object.</summary>
		/// <returns>A <see cref="T:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay" /> object that is obtained from <paramref name="value" />.</returns>
		/// <param name="value">The <see cref="T:System.String" /> to convert. </param>
		/// <exception cref="T:System.Runtime.Remoting.RemotingException">
		///   <paramref name="value" /> does not contain a date and time that corresponds to any of the recognized format patterns. </exception>
		// Token: 0x0600320B RID: 12811 RVA: 0x000A2D74 File Offset: 0x000A0F74
		public static SoapDay Parse(string value)
		{
			DateTime dateTime = DateTime.ParseExact(value, SoapDay._datetimeFormats, null, DateTimeStyles.None);
			return new SoapDay(dateTime);
		}

		/// <summary>Returns <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay.Value" /> as a <see cref="T:System.String" />.</summary>
		/// <returns>A <see cref="T:System.String" /> obtained from <see cref="P:System.Runtime.Remoting.Metadata.W3cXsd2001.SoapDay.Value" /> in the format "---dd".</returns>
		// Token: 0x0600320C RID: 12812 RVA: 0x000A2D98 File Offset: 0x000A0F98
		public override string ToString()
		{
			return this._value.ToString("---dd", CultureInfo.InvariantCulture);
		}

		// Token: 0x0400150A RID: 5386
		private static readonly string[] _datetimeFormats = new string[] { "---dd", "---ddzzz" };

		// Token: 0x0400150B RID: 5387
		private DateTime _value;
	}
}
