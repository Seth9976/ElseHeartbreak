using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 8-bit unsigned integer objects to and from a string.</summary>
	// Token: 0x020001A5 RID: 421
	public class SByteConverter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.SByteConverter" /> class. </summary>
		// Token: 0x06000EC7 RID: 3783 RVA: 0x00026570 File Offset: 0x00024770
		public SByteConverter()
		{
			this.InnerType = typeof(sbyte);
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000EC8 RID: 3784 RVA: 0x00026588 File Offset: 0x00024788
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0002658C File Offset: 0x0002478C
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((sbyte)value).ToString("G", format);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x000265B0 File Offset: 0x000247B0
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return sbyte.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x000265C0 File Offset: 0x000247C0
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToSByte(value, fromBase);
		}
	}
}
