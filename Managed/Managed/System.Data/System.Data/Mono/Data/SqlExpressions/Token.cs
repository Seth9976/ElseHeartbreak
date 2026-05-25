using System;

namespace Mono.Data.SqlExpressions
{
	// Token: 0x02000006 RID: 6
	internal class Token
	{
		// Token: 0x04000015 RID: 21
		public const int PAROPEN = 257;

		// Token: 0x04000016 RID: 22
		public const int PARCLOSE = 258;

		// Token: 0x04000017 RID: 23
		public const int AND = 259;

		// Token: 0x04000018 RID: 24
		public const int OR = 260;

		// Token: 0x04000019 RID: 25
		public const int NOT = 261;

		// Token: 0x0400001A RID: 26
		public const int TRUE = 262;

		// Token: 0x0400001B RID: 27
		public const int FALSE = 263;

		// Token: 0x0400001C RID: 28
		public const int NULL = 264;

		// Token: 0x0400001D RID: 29
		public const int PARENT = 265;

		// Token: 0x0400001E RID: 30
		public const int CHILD = 266;

		// Token: 0x0400001F RID: 31
		public const int EQ = 267;

		// Token: 0x04000020 RID: 32
		public const int LT = 268;

		// Token: 0x04000021 RID: 33
		public const int GT = 269;

		// Token: 0x04000022 RID: 34
		public const int PLUS = 270;

		// Token: 0x04000023 RID: 35
		public const int MINUS = 271;

		// Token: 0x04000024 RID: 36
		public const int MUL = 272;

		// Token: 0x04000025 RID: 37
		public const int DIV = 273;

		// Token: 0x04000026 RID: 38
		public const int MOD = 274;

		// Token: 0x04000027 RID: 39
		public const int DOT = 275;

		// Token: 0x04000028 RID: 40
		public const int COMMA = 276;

		// Token: 0x04000029 RID: 41
		public const int IS = 277;

		// Token: 0x0400002A RID: 42
		public const int IN = 278;

		// Token: 0x0400002B RID: 43
		public const int NOT_IN = 279;

		// Token: 0x0400002C RID: 44
		public const int LIKE = 280;

		// Token: 0x0400002D RID: 45
		public const int NOT_LIKE = 281;

		// Token: 0x0400002E RID: 46
		public const int COUNT = 282;

		// Token: 0x0400002F RID: 47
		public const int SUM = 283;

		// Token: 0x04000030 RID: 48
		public const int AVG = 284;

		// Token: 0x04000031 RID: 49
		public const int MAX = 285;

		// Token: 0x04000032 RID: 50
		public const int MIN = 286;

		// Token: 0x04000033 RID: 51
		public const int STDEV = 287;

		// Token: 0x04000034 RID: 52
		public const int VAR = 288;

		// Token: 0x04000035 RID: 53
		public const int IIF = 289;

		// Token: 0x04000036 RID: 54
		public const int SUBSTRING = 290;

		// Token: 0x04000037 RID: 55
		public const int ISNULL = 291;

		// Token: 0x04000038 RID: 56
		public const int LEN = 292;

		// Token: 0x04000039 RID: 57
		public const int TRIM = 293;

		// Token: 0x0400003A RID: 58
		public const int CONVERT = 294;

		// Token: 0x0400003B RID: 59
		public const int StringLiteral = 295;

		// Token: 0x0400003C RID: 60
		public const int NumberLiteral = 296;

		// Token: 0x0400003D RID: 61
		public const int DateLiteral = 297;

		// Token: 0x0400003E RID: 62
		public const int Identifier = 298;

		// Token: 0x0400003F RID: 63
		public const int FunctionName = 299;

		// Token: 0x04000040 RID: 64
		public const int UMINUS = 300;

		// Token: 0x04000041 RID: 65
		public const int yyErrorCode = 256;
	}
}
