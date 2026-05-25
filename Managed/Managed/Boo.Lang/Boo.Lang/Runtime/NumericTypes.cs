using System;

namespace Boo.Lang.Runtime
{
	// Token: 0x0200003C RID: 60
	public class NumericTypes
	{
		// Token: 0x060001E9 RID: 489 RVA: 0x00006580 File Offset: 0x00004780
		public static bool IsWideningPromotion(Type paramType, Type argType)
		{
			return NumericTypes.NumericRangeOrder(paramType) > NumericTypes.NumericRangeOrder(argType);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00006590 File Offset: 0x00004790
		public static int NumericRangeOrder(Type type)
		{
			switch (Type.GetTypeCode(type))
			{
			case 3:
				return 1;
			case 4:
			case 7:
			case 8:
				return 3;
			case 5:
			case 6:
				return 2;
			case 9:
			case 10:
				return 4;
			case 11:
			case 12:
				return 5;
			case 13:
				return 7;
			case 14:
				return 8;
			case 15:
				return 6;
			default:
				throw new ArgumentException(type.ToString());
			}
		}
	}
}
