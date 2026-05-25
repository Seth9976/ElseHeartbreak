using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000015 RID: 21
	public class TokenWithValue : Token
	{
		// Token: 0x060000B6 RID: 182 RVA: 0x00006C8C File Offset: 0x00004E8C
		public TokenWithValue(Token.TokenType tokenType, string tokenString, int lineNr, int linePosition, object pValue)
			: base(tokenType, tokenString, lineNr, linePosition)
		{
			this.m_value = pValue;
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x00006CA4 File Offset: 0x00004EA4
		public TokenWithValue(Token.TokenType pTokenType, string pTokenString, object pValue)
			: base(pTokenType, pTokenString)
		{
			this.m_value = pValue;
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00006CB8 File Offset: 0x00004EB8
		public object getValue()
		{
			return this.m_value;
		}

		// Token: 0x04000070 RID: 112
		private object m_value;
	}
}
