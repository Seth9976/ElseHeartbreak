using System;

namespace System.Data.Common
{
	// Token: 0x020000AC RID: 172
	internal sealed class DecimalDataContainer : ObjectDataContainer
	{
		// Token: 0x060007C5 RID: 1989 RVA: 0x00025350 File Offset: 0x00023550
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			base.SetValue(index, record.GetDecimalSafe(field));
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00025368 File Offset: 0x00023568
		protected override void SetValue(int index, object value)
		{
			base.SetValue(index, base.GetContainerData(value));
		}
	}
}
