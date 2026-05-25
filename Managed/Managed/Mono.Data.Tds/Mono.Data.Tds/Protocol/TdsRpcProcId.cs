using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000006 RID: 6
	public enum TdsRpcProcId
	{
		// Token: 0x04000021 RID: 33
		Cursor = 1,
		// Token: 0x04000022 RID: 34
		CursorOpen,
		// Token: 0x04000023 RID: 35
		CursorPrepare,
		// Token: 0x04000024 RID: 36
		CursorExecute,
		// Token: 0x04000025 RID: 37
		CursorPrepExec,
		// Token: 0x04000026 RID: 38
		CursorUnprepare,
		// Token: 0x04000027 RID: 39
		CursorFetch,
		// Token: 0x04000028 RID: 40
		CursorOption,
		// Token: 0x04000029 RID: 41
		CursorClose,
		// Token: 0x0400002A RID: 42
		ExecuteSql,
		// Token: 0x0400002B RID: 43
		Prepare,
		// Token: 0x0400002C RID: 44
		Execute,
		// Token: 0x0400002D RID: 45
		PrepExec,
		// Token: 0x0400002E RID: 46
		PrepExecRpc,
		// Token: 0x0400002F RID: 47
		Unprepare
	}
}
