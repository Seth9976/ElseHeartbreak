using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x020000B3 RID: 179
	internal sealed class DbConnectionStringBuilderHelper
	{
		// Token: 0x06000870 RID: 2160 RVA: 0x000280F8 File Offset: 0x000262F8
		public static int ConvertToInt32(object value)
		{
			return int.Parse(value.ToString(), CultureInfo.InvariantCulture);
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0002810C File Offset: 0x0002630C
		public static bool ConvertToBoolean(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("null value cannot be converted to boolean");
			}
			string text = value.ToString().ToUpper().Trim();
			if (text == "YES" || text == "TRUE")
			{
				return true;
			}
			if (text == "NO" || text == "FALSE")
			{
				return false;
			}
			throw new ArgumentException(string.Format("Invalid boolean value: {0}", value.ToString()));
		}
	}
}
