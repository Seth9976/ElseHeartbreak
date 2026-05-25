using System;

namespace UnityEngine.UI
{
	// Token: 0x02000063 RID: 99
	internal static class SetPropertyUtility
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0000F7A8 File Offset: 0x0000D9A8
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000F808 File Offset: 0x0000DA08
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000F82C File Offset: 0x0000DA2C
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
