using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200002E RID: 46
	public class JsonISerializableContract : JsonContract
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000893C File Offset: 0x00006B3C
		// (set) Token: 0x06000216 RID: 534 RVA: 0x00008944 File Offset: 0x00006B44
		public ObjectConstructor<object> ISerializableCreator { get; set; }

		// Token: 0x06000217 RID: 535 RVA: 0x0000894D File Offset: 0x00006B4D
		public JsonISerializableContract(Type underlyingType)
			: base(underlyingType)
		{
		}
	}
}
