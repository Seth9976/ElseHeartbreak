using System;
using System.Globalization;
using System.IO;

namespace Newtonsoft.Json.Linq
{
	// Token: 0x0200002A RID: 42
	public class JRaw : JValue
	{
		// Token: 0x060001E0 RID: 480 RVA: 0x00008569 File Offset: 0x00006769
		public JRaw(JRaw other)
			: base(other)
		{
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00008572 File Offset: 0x00006772
		public JRaw(object rawJson)
			: base(rawJson, JTokenType.Raw)
		{
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00008580 File Offset: 0x00006780
		public static JRaw Create(JsonReader reader)
		{
			JRaw jraw;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				using (JsonTextWriter jsonTextWriter = new JsonTextWriter(stringWriter))
				{
					jsonTextWriter.WriteToken(reader);
					jraw = new JRaw(stringWriter.ToString());
				}
			}
			return jraw;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x000085E8 File Offset: 0x000067E8
		internal override JToken CloneToken()
		{
			return new JRaw(this);
		}
	}
}
