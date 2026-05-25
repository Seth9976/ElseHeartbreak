using System;

namespace Newtonsoft.Json.Converters
{
	// Token: 0x0200001B RID: 27
	public abstract class DateTimeConverterBase : JsonConverter
	{
		// Token: 0x0600010D RID: 269 RVA: 0x0000580D File Offset: 0x00003A0D
		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(DateTime) || objectType == typeof(DateTime?) || (objectType == typeof(DateTimeOffset) || objectType == typeof(DateTimeOffset?));
		}
	}
}
