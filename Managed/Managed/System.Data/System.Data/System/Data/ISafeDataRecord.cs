using System;

namespace System.Data
{
	// Token: 0x02000059 RID: 89
	internal interface ISafeDataRecord : IDataRecord
	{
		// Token: 0x060005F9 RID: 1529
		bool GetBooleanSafe(int i);

		// Token: 0x060005FA RID: 1530
		byte GetByteSafe(int i);

		// Token: 0x060005FB RID: 1531
		char GetCharSafe(int i);

		// Token: 0x060005FC RID: 1532
		DateTime GetDateTimeSafe(int i);

		// Token: 0x060005FD RID: 1533
		decimal GetDecimalSafe(int i);

		// Token: 0x060005FE RID: 1534
		double GetDoubleSafe(int i);

		// Token: 0x060005FF RID: 1535
		float GetFloatSafe(int i);

		// Token: 0x06000600 RID: 1536
		short GetInt16Safe(int i);

		// Token: 0x06000601 RID: 1537
		int GetInt32Safe(int i);

		// Token: 0x06000602 RID: 1538
		long GetInt64Safe(int i);

		// Token: 0x06000603 RID: 1539
		string GetStringSafe(int i);
	}
}
