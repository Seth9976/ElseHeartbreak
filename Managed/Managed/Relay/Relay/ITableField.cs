using System;

namespace RelayLib
{
	// Token: 0x0200000A RID: 10
	public interface ITableField
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002F RID: 47
		string name { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000030 RID: 48
		Type type { get; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000031 RID: 49
		// (set) Token: 0x06000032 RID: 50
		int rowCount { get; set; }

		// Token: 0x06000033 RID: 51
		void ClearEntryAtRow(int pIndex);

		// Token: 0x06000034 RID: 52
		string GetValueAsString(int pRow);

		// Token: 0x06000035 RID: 53
		void SetValueFromString(int pRow, string pValue);

		// Token: 0x06000036 RID: 54
		ITableField GetEmptyCopy();
	}
}
