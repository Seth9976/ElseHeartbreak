using System;
using Newtonsoft.Json.Schema;

namespace Newtonsoft.Json
{
	// Token: 0x02000016 RID: 22
	public abstract class JsonConverter
	{
		// Token: 0x060000F1 RID: 241
		public abstract void WriteJson(JsonWriter writer, object value, JsonSerializer serializer);

		// Token: 0x060000F2 RID: 242
		public abstract object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer);

		// Token: 0x060000F3 RID: 243
		public abstract bool CanConvert(Type objectType);

		// Token: 0x060000F4 RID: 244 RVA: 0x00005168 File Offset: 0x00003368
		public virtual JsonSchema GetSchema()
		{
			return null;
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000516B File Offset: 0x0000336B
		public virtual bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x0000516E File Offset: 0x0000336E
		public virtual bool CanWrite
		{
			get
			{
				return true;
			}
		}
	}
}
