using System;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Bson
{
	// Token: 0x02000014 RID: 20
	public class BsonObjectId
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000512B File Offset: 0x0000332B
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00005133 File Offset: 0x00003333
		public byte[] Value { get; private set; }

		// Token: 0x060000EF RID: 239 RVA: 0x0000513C File Offset: 0x0000333C
		public BsonObjectId(byte[] value)
		{
			ValidationUtils.ArgumentNotNull(value, "value");
			if (value.Length != 12)
			{
				throw new Exception("An ObjectId must be 12 bytes");
			}
			this.Value = value;
		}
	}
}
