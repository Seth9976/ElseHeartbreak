using System;

namespace GrimmLib
{
	// Token: 0x0200000B RID: 11
	public class Token
	{
		// Token: 0x06000068 RID: 104 RVA: 0x00003A54 File Offset: 0x00001C54
		public Token(Token.TokenType pTokenType, string pTokenString)
		{
			this._tokenType = pTokenType;
			this._tokenString = pTokenString;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00003A84 File Offset: 0x00001C84
		public Token(Token.TokenType pTokenType, string pTokenString, int pLineNr, int pLinePosition)
		{
			this._tokenType = pTokenType;
			this._tokenString = pTokenString;
			this._lineNr = pLineNr;
			this._linePosition = pLinePosition;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00003AB8 File Offset: 0x00001CB8
		public Token.TokenType getTokenType()
		{
			return this._tokenType;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00003AC0 File Offset: 0x00001CC0
		public string getTokenString()
		{
			return this._tokenString;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600006D RID: 109 RVA: 0x00003AD4 File Offset: 0x00001CD4
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public int LineNr
		{
			get
			{
				return this._lineNr;
			}
			set
			{
				this._lineNr = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003AE8 File Offset: 0x00001CE8
		// (set) Token: 0x0600006E RID: 110 RVA: 0x00003ADC File Offset: 0x00001CDC
		public int LinePosition
		{
			get
			{
				return this._linePosition;
			}
			set
			{
				this._linePosition = value;
			}
		}

		// Token: 0x04000019 RID: 25
		private Token.TokenType _tokenType;

		// Token: 0x0400001A RID: 26
		private string _tokenString;

		// Token: 0x0400001B RID: 27
		private int _lineNr = -1;

		// Token: 0x0400001C RID: 28
		private int _linePosition = -1;

		// Token: 0x0200000C RID: 12
		public enum TokenType
		{
			// Token: 0x0400001E RID: 30
			NO_TOKEN_TYPE,
			// Token: 0x0400001F RID: 31
			EOF,
			// Token: 0x04000020 RID: 32
			NEW_LINE,
			// Token: 0x04000021 RID: 33
			COMMA,
			// Token: 0x04000022 RID: 34
			NAME,
			// Token: 0x04000023 RID: 35
			NUMBER,
			// Token: 0x04000024 RID: 36
			QUOTED_STRING,
			// Token: 0x04000025 RID: 37
			SWITCH,
			// Token: 0x04000026 RID: 38
			COLON,
			// Token: 0x04000027 RID: 39
			END,
			// Token: 0x04000028 RID: 40
			GOTO,
			// Token: 0x04000029 RID: 41
			PARANTHESIS_LEFT,
			// Token: 0x0400002A RID: 42
			PARANTHESIS_RIGHT,
			// Token: 0x0400002B RID: 43
			BLOCK_BEGIN,
			// Token: 0x0400002C RID: 44
			BLOCK_END,
			// Token: 0x0400002D RID: 45
			BRACKET_LEFT,
			// Token: 0x0400002E RID: 46
			BRACKET_RIGHT,
			// Token: 0x0400002F RID: 47
			DOT,
			// Token: 0x04000030 RID: 48
			IF,
			// Token: 0x04000031 RID: 49
			ELSE,
			// Token: 0x04000032 RID: 50
			ELIF,
			// Token: 0x04000033 RID: 51
			CHOICE,
			// Token: 0x04000034 RID: 52
			LANGUAGE,
			// Token: 0x04000035 RID: 53
			START,
			// Token: 0x04000036 RID: 54
			INTERRUPT,
			// Token: 0x04000037 RID: 55
			WAIT,
			// Token: 0x04000038 RID: 56
			ASSERT,
			// Token: 0x04000039 RID: 57
			LOOP,
			// Token: 0x0400003A RID: 58
			BREAK,
			// Token: 0x0400003B RID: 59
			STOP,
			// Token: 0x0400003C RID: 60
			LISTEN,
			// Token: 0x0400003D RID: 61
			BROADCAST,
			// Token: 0x0400003E RID: 62
			CANCEL,
			// Token: 0x0400003F RID: 63
			FOCUS,
			// Token: 0x04000040 RID: 64
			DEFOCUS,
			// Token: 0x04000041 RID: 65
			AND,
			// Token: 0x04000042 RID: 66
			ETERNAL
		}
	}
}
