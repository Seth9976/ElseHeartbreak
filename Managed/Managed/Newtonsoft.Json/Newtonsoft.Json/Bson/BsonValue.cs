using System;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x0200000C RID: 12
	internal class BsonValue : BsonToken
	{
		// Token: 0x0600005F RID: 95 RVA: 0x00003BCB File Offset: 0x00001DCB
		public BsonValue(object value, BsonType type)
		{
			this._value = value;
			this._type = type;
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000060 RID: 96 RVA: 0x00003BE1 File Offset: 0x00001DE1
		public object Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003BE9 File Offset: 0x00001DE9
		public override BsonType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000044 RID: 68
		private object _value;

		// Token: 0x04000045 RID: 69
		private BsonType _type;
	}
}
