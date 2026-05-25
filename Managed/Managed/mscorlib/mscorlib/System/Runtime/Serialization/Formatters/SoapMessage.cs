using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Holds the names and types of parameters required during serialization of a SOAP RPC (Remote Procedure Call).</summary>
	// Token: 0x02000518 RID: 1304
	[ComVisible(true)]
	[Serializable]
	public class SoapMessage : ISoapMessage
	{
		/// <summary>Gets or sets the out-of-band data of the called method.</summary>
		/// <returns>The out-of-band data of the called method.</returns>
		// Token: 0x170009BD RID: 2493
		// (get) Token: 0x060033BD RID: 13245 RVA: 0x000A6F00 File Offset: 0x000A5100
		// (set) Token: 0x060033BE RID: 13246 RVA: 0x000A6F08 File Offset: 0x000A5108
		public Header[] Headers
		{
			get
			{
				return this.headers;
			}
			set
			{
				this.headers = value;
			}
		}

		/// <summary>Gets or sets the name of the called method.</summary>
		/// <returns>The name of the called method.</returns>
		// Token: 0x170009BE RID: 2494
		// (get) Token: 0x060033BF RID: 13247 RVA: 0x000A6F14 File Offset: 0x000A5114
		// (set) Token: 0x060033C0 RID: 13248 RVA: 0x000A6F1C File Offset: 0x000A511C
		public string MethodName
		{
			get
			{
				return this.methodName;
			}
			set
			{
				this.methodName = value;
			}
		}

		/// <summary>Gets or sets the parameter names for the called method.</summary>
		/// <returns>The parameter names for the called method.</returns>
		// Token: 0x170009BF RID: 2495
		// (get) Token: 0x060033C1 RID: 13249 RVA: 0x000A6F28 File Offset: 0x000A5128
		// (set) Token: 0x060033C2 RID: 13250 RVA: 0x000A6F30 File Offset: 0x000A5130
		public string[] ParamNames
		{
			get
			{
				return this.paramNames;
			}
			set
			{
				this.paramNames = value;
			}
		}

		/// <summary>This property is reserved. Use the <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamNames" /> and/or <see cref="P:System.Runtime.Serialization.Formatters.SoapMessage.ParamValues" /> properties instead.</summary>
		/// <returns>Parameter types for the called method.</returns>
		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x060033C3 RID: 13251 RVA: 0x000A6F3C File Offset: 0x000A513C
		// (set) Token: 0x060033C4 RID: 13252 RVA: 0x000A6F44 File Offset: 0x000A5144
		public Type[] ParamTypes
		{
			get
			{
				return this.paramTypes;
			}
			set
			{
				this.paramTypes = value;
			}
		}

		/// <summary>Gets or sets the parameter values for the called method.</summary>
		/// <returns>Parameter values for the called method.</returns>
		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000A6F50 File Offset: 0x000A5150
		// (set) Token: 0x060033C6 RID: 13254 RVA: 0x000A6F58 File Offset: 0x000A5158
		public object[] ParamValues
		{
			get
			{
				return this.paramValues;
			}
			set
			{
				this.paramValues = value;
			}
		}

		/// <summary>Gets or sets the XML namespace name where the object that contains the called method is located.</summary>
		/// <returns>The XML namespace name where the object that contains the called method is located.</returns>
		// Token: 0x170009C2 RID: 2498
		// (get) Token: 0x060033C7 RID: 13255 RVA: 0x000A6F64 File Offset: 0x000A5164
		// (set) Token: 0x060033C8 RID: 13256 RVA: 0x000A6F6C File Offset: 0x000A516C
		public string XmlNameSpace
		{
			get
			{
				return this.xmlNameSpace;
			}
			set
			{
				this.xmlNameSpace = value;
			}
		}

		// Token: 0x04001584 RID: 5508
		private Header[] headers;

		// Token: 0x04001585 RID: 5509
		private string methodName;

		// Token: 0x04001586 RID: 5510
		private string[] paramNames;

		// Token: 0x04001587 RID: 5511
		private Type[] paramTypes;

		// Token: 0x04001588 RID: 5512
		private object[] paramValues;

		// Token: 0x04001589 RID: 5513
		private string xmlNameSpace;
	}
}
