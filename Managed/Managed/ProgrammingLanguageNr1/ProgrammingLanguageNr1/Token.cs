using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000013 RID: 19
	public class Token
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00006B70 File Offset: 0x00004D70
		public Token(Token.TokenType tokenType, string tokenString)
		{
			this.m_tokenType = tokenType;
			this.m_tokenString = tokenString;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00006BA0 File Offset: 0x00004DA0
		public Token(Token.TokenType tokenType, string tokenString, int lineNr, int linePosition)
		{
			this.m_tokenType = tokenType;
			this.m_tokenString = tokenString;
			this.m_lineNr = lineNr;
			this.m_linePosition = linePosition;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00006BD4 File Offset: 0x00004DD4
		public Token.TokenType getTokenType()
		{
			return this.m_tokenType;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00006BDC File Offset: 0x00004DDC
		public string getTokenString()
		{
			return this.m_tokenString;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00006BF0 File Offset: 0x00004DF0
		// (set) Token: 0x060000AF RID: 175 RVA: 0x00006BE4 File Offset: 0x00004DE4
		public int LineNr
		{
			get
			{
				return this.m_lineNr;
			}
			set
			{
				this.m_lineNr = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00006C04 File Offset: 0x00004E04
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00006BF8 File Offset: 0x00004DF8
		public int LinePosition
		{
			get
			{
				return this.m_linePosition;
			}
			set
			{
				this.m_linePosition = value;
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00006C0C File Offset: 0x00004E0C
		public override string ToString()
		{
			return this.getTokenType() + " " + this.getTokenString();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00006C2C File Offset: 0x00004E2C
		public override bool Equals(object obj)
		{
			if (!(obj is Token))
			{
				return false;
			}
			Token token = obj as Token;
			if (this.getTokenString() == "")
			{
				return this.m_tokenType == token.getTokenType();
			}
			return this.m_tokenString == token.getTokenString();
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00006C84 File Offset: 0x00004E84
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04000040 RID: 64
		protected Token.TokenType m_tokenType;

		// Token: 0x04000041 RID: 65
		private string m_tokenString;

		// Token: 0x04000042 RID: 66
		private int m_lineNr = -1;

		// Token: 0x04000043 RID: 67
		private int m_linePosition = -1;

		// Token: 0x02000014 RID: 20
		public enum TokenType
		{
			// Token: 0x04000045 RID: 69
			NO_TOKEN_TYPE,
			// Token: 0x04000046 RID: 70
			EOF,
			// Token: 0x04000047 RID: 71
			NEW_LINE,
			// Token: 0x04000048 RID: 72
			COMMA,
			// Token: 0x04000049 RID: 73
			NAME,
			// Token: 0x0400004A RID: 74
			ARRAY_LOOKUP,
			// Token: 0x0400004B RID: 75
			OPERATOR,
			// Token: 0x0400004C RID: 76
			NUMBER,
			// Token: 0x0400004D RID: 77
			QUOTED_STRING,
			// Token: 0x0400004E RID: 78
			BOOLEAN_VALUE,
			// Token: 0x0400004F RID: 79
			ARRAY,
			// Token: 0x04000050 RID: 80
			DOT,
			// Token: 0x04000051 RID: 81
			ARRAY_END_SIGNAL,
			// Token: 0x04000052 RID: 82
			BUILT_IN_TYPE_NAME,
			// Token: 0x04000053 RID: 83
			ELSE,
			// Token: 0x04000054 RID: 84
			PARANTHESIS_LEFT,
			// Token: 0x04000055 RID: 85
			PARANTHESIS_RIGHT,
			// Token: 0x04000056 RID: 86
			BRACKET_LEFT,
			// Token: 0x04000057 RID: 87
			BRACKET_RIGHT,
			// Token: 0x04000058 RID: 88
			BLOCK_END,
			// Token: 0x04000059 RID: 89
			STATEMENT_LIST,
			// Token: 0x0400005A RID: 90
			VAR_DECLARATION,
			// Token: 0x0400005B RID: 91
			FUNC_DECLARATION,
			// Token: 0x0400005C RID: 92
			NODE_GROUP,
			// Token: 0x0400005D RID: 93
			PARAMETER,
			// Token: 0x0400005E RID: 94
			FUNCTION_CALL,
			// Token: 0x0400005F RID: 95
			ASSIGNMENT,
			// Token: 0x04000060 RID: 96
			ASSIGNMENT_TO_ARRAY,
			// Token: 0x04000061 RID: 97
			IF,
			// Token: 0x04000062 RID: 98
			LOOP,
			// Token: 0x04000063 RID: 99
			IN,
			// Token: 0x04000064 RID: 100
			LOOP_BLOCK,
			// Token: 0x04000065 RID: 101
			LOOP_INCREMENT,
			// Token: 0x04000066 RID: 102
			GOTO_BEGINNING_OF_LOOP,
			// Token: 0x04000067 RID: 103
			BREAK,
			// Token: 0x04000068 RID: 104
			RETURN,
			// Token: 0x04000069 RID: 105
			PROGRAM_ROOT,
			// Token: 0x0400006A RID: 106
			COMMENT,
			// Token: 0x0400006B RID: 107
			FROM,
			// Token: 0x0400006C RID: 108
			TO,
			// Token: 0x0400006D RID: 109
			NOT,
			// Token: 0x0400006E RID: 110
			AND,
			// Token: 0x0400006F RID: 111
			OR
		}
	}
}
