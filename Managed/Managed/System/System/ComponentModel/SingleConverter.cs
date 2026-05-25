using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert single-precision, floating point number objects to and from various other representations.</summary>
	// Token: 0x020001A7 RID: 423
	public class SingleConverter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SingleConverter" /> class. </summary>
		// Token: 0x06000ED1 RID: 3793 RVA: 0x00026640 File Offset: 0x00024840
		public SingleConverter()
		{
			this.InnerType = typeof(float);
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000ED2 RID: 3794 RVA: 0x00026658 File Offset: 0x00024858
		internal override bool SupportHex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0002665C File Offset: 0x0002485C
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((float)value).ToString("R", format);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00026680 File Offset: 0x00024880
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return float.Parse(value, NumberStyles.Float, format);
		}
	}
}
