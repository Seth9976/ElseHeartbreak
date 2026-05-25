using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Metadata;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Carries error and status information within a SOAP message. This class cannot be inherited.</summary>
	// Token: 0x02000517 RID: 1303
	[SoapType]
	[ComVisible(true)]
	[Serializable]
	public sealed class SoapFault : ISerializable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> class with default values.</summary>
		// Token: 0x060033B0 RID: 13232 RVA: 0x000A6DC8 File Offset: 0x000A4FC8
		public SoapFault()
		{
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x000A6DD0 File Offset: 0x000A4FD0
		private SoapFault(SerializationInfo info, StreamingContext context)
		{
			this.code = info.GetString("faultcode");
			this.faultString = info.GetString("faultstring");
			this.detail = info.GetValue("detail", typeof(object));
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> class, setting the properties to specified values.</summary>
		/// <param name="faultCode">The fault code for the new instance of <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />. The fault code identifies the type of the fault that occurred. </param>
		/// <param name="faultString">The fault string for the new instance of <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />. The fault string provides a human readable explanation of the fault. </param>
		/// <param name="faultActor">The URI of the object that generated the fault. </param>
		/// <param name="serverFault">The description of a common language runtime exception. This information is also present in the <see cref="P:System.Runtime.Serialization.Formatters.SoapFault.Detail" /> property. </param>
		// Token: 0x060033B2 RID: 13234 RVA: 0x000A6E20 File Offset: 0x000A5020
		public SoapFault(string faultCode, string faultString, string faultActor, ServerFault serverFault)
		{
			this.code = faultCode;
			this.actor = faultActor;
			this.faultString = faultString;
			this.detail = serverFault;
		}

		/// <summary>Gets or sets additional information required for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>Additional information required for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x170009B9 RID: 2489
		// (get) Token: 0x060033B3 RID: 13235 RVA: 0x000A6E48 File Offset: 0x000A5048
		// (set) Token: 0x060033B4 RID: 13236 RVA: 0x000A6E50 File Offset: 0x000A5050
		public object Detail
		{
			get
			{
				return this.detail;
			}
			set
			{
				this.detail = value;
			}
		}

		/// <summary>Gets or sets the fault actor for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>The fault actor for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x170009BA RID: 2490
		// (get) Token: 0x060033B5 RID: 13237 RVA: 0x000A6E5C File Offset: 0x000A505C
		// (set) Token: 0x060033B6 RID: 13238 RVA: 0x000A6E64 File Offset: 0x000A5064
		public string FaultActor
		{
			get
			{
				return this.actor;
			}
			set
			{
				this.actor = value;
			}
		}

		/// <summary>Gets or sets the fault code for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>The fault code for this <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x170009BB RID: 2491
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x000A6E70 File Offset: 0x000A5070
		// (set) Token: 0x060033B8 RID: 13240 RVA: 0x000A6E78 File Offset: 0x000A5078
		public string FaultCode
		{
			get
			{
				return this.code;
			}
			set
			{
				this.code = value;
			}
		}

		/// <summary>Gets or sets the fault message for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</summary>
		/// <returns>The fault message for the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" />.</returns>
		// Token: 0x170009BC RID: 2492
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000A6E84 File Offset: 0x000A5084
		// (set) Token: 0x060033BA RID: 13242 RVA: 0x000A6E8C File Offset: 0x000A508C
		public string FaultString
		{
			get
			{
				return this.faultString;
			}
			set
			{
				this.faultString = value;
			}
		}

		/// <summary>Populates the specified <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with the data to serialize the <see cref="T:System.Runtime.Serialization.Formatters.SoapFault" /> object.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> to populate with data. </param>
		/// <param name="context">The destination (see <see cref="T:System.Runtime.Serialization.StreamingContext" />) for the current serialization. </param>
		// Token: 0x060033BB RID: 13243 RVA: 0x000A6E98 File Offset: 0x000A5098
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("faultcode", this.code, typeof(string));
			info.AddValue("faultstring", this.faultString, typeof(string));
			info.AddValue("detail", this.detail, typeof(object));
		}

		// Token: 0x04001580 RID: 5504
		private string code;

		// Token: 0x04001581 RID: 5505
		private string actor;

		// Token: 0x04001582 RID: 5506
		private string faultString;

		// Token: 0x04001583 RID: 5507
		private object detail;
	}
}
