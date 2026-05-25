using System;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000021 RID: 33
	public enum TdsPacketSubType
	{
		// Token: 0x0400010D RID: 269
		Capability = 226,
		// Token: 0x0400010E RID: 270
		Dynamic = 231,
		// Token: 0x0400010F RID: 271
		Dynamic2 = 163,
		// Token: 0x04000110 RID: 272
		EnvironmentChange = 227,
		// Token: 0x04000111 RID: 273
		Error = 170,
		// Token: 0x04000112 RID: 274
		Info,
		// Token: 0x04000113 RID: 275
		EED = 229,
		// Token: 0x04000114 RID: 276
		Param = 172,
		// Token: 0x04000115 RID: 277
		Authentication = 237,
		// Token: 0x04000116 RID: 278
		LoginAck = 173,
		// Token: 0x04000117 RID: 279
		ReturnStatus = 121,
		// Token: 0x04000118 RID: 280
		ProcId = 124,
		// Token: 0x04000119 RID: 281
		Done = 253,
		// Token: 0x0400011A RID: 282
		DoneProc,
		// Token: 0x0400011B RID: 283
		DoneInProc,
		// Token: 0x0400011C RID: 284
		ColumnName = 160,
		// Token: 0x0400011D RID: 285
		ColumnInfo,
		// Token: 0x0400011E RID: 286
		ColumnDetail = 165,
		// Token: 0x0400011F RID: 287
		AltName = 167,
		// Token: 0x04000120 RID: 288
		AltFormat,
		// Token: 0x04000121 RID: 289
		TableName = 164,
		// Token: 0x04000122 RID: 290
		ColumnOrder = 169,
		// Token: 0x04000123 RID: 291
		Control = 174,
		// Token: 0x04000124 RID: 292
		Row = 209,
		// Token: 0x04000125 RID: 293
		ColumnMetadata = 129,
		// Token: 0x04000126 RID: 294
		RowFormat = 238,
		// Token: 0x04000127 RID: 295
		ParamFormat = 236,
		// Token: 0x04000128 RID: 296
		Parameters = 215
	}
}
