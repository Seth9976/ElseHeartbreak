using System;

namespace System.Data.Common
{
	// Token: 0x020000AB RID: 171
	internal sealed class DateTimeDataContainer : ObjectDataContainer
	{
		// Token: 0x060007C2 RID: 1986 RVA: 0x00025320 File Offset: 0x00023520
		protected override void SetValueFromSafeDataRecord(int index, ISafeDataRecord record, int field)
		{
			base.SetValue(index, record.GetDateTimeSafe(field));
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00025338 File Offset: 0x00023538
		protected override void SetValue(int index, object value)
		{
			base.SetValue(index, base.GetContainerData(value));
		}
	}
}
