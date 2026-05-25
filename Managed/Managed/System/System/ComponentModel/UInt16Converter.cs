using System;
using System.Globalization;

namespace System.ComponentModel
{
	/// <summary>Provides a type converter to convert 16-bit unsigned integer objects to and from other representations.</summary>
	// Token: 0x020001BF RID: 447
	public class UInt16Converter : BaseNumberConverter
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.UInt16Converter" /> class. </summary>
		// Token: 0x06000FBC RID: 4028 RVA: 0x000296D0 File Offset: 0x000278D0
		public UInt16Converter()
		{
			this.InnerType = typeof(ushort);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x000296E8 File Offset: 0x000278E8
		internal override bool SupportHex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000296EC File Offset: 0x000278EC
		internal override string ConvertToString(object value, NumberFormatInfo format)
		{
			return ((ushort)value).ToString("G", format);
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x00029710 File Offset: 0x00027910
		internal override object ConvertFromString(string value, NumberFormatInfo format)
		{
			return ushort.Parse(value, NumberStyles.Integer, format);
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x00029720 File Offset: 0x00027920
		internal override object ConvertFromString(string value, int fromBase)
		{
			return Convert.ToUInt16(value, fromBase);
		}
	}
}
