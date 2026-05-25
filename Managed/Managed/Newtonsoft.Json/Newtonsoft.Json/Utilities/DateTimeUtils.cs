using System;
using System.Globalization;
using System.Xml;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000AE RID: 174
	internal static class DateTimeUtils
	{
		// Token: 0x060007F1 RID: 2033 RVA: 0x0001CCC8 File Offset: 0x0001AEC8
		public static string GetUtcOffsetText(this DateTime d)
		{
			TimeSpan utcOffset = d.GetUtcOffset();
			return utcOffset.Hours.ToString("+00;-00", CultureInfo.InvariantCulture) + ":" + utcOffset.Minutes.ToString("00;00", CultureInfo.InvariantCulture);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0001CD18 File Offset: 0x0001AF18
		public static TimeSpan GetUtcOffset(this DateTime d)
		{
			return TimeZoneInfo.Local.GetUtcOffset(d);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0001CD28 File Offset: 0x0001AF28
		public static XmlDateTimeSerializationMode ToSerializationMode(DateTimeKind kind)
		{
			switch (kind)
			{
			case DateTimeKind.Unspecified:
				return XmlDateTimeSerializationMode.Unspecified;
			case DateTimeKind.Utc:
				return XmlDateTimeSerializationMode.Utc;
			case DateTimeKind.Local:
				return XmlDateTimeSerializationMode.Local;
			default:
				throw MiscellaneousUtils.CreateArgumentOutOfRangeException("kind", kind, "Unexpected DateTimeKind value.");
			}
		}
	}
}
