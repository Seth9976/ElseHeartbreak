using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200001F RID: 31
	public class Error : Exception
	{
		// Token: 0x06000115 RID: 277 RVA: 0x00008EB4 File Offset: 0x000070B4
		public Error(string pMessage)
			: base(pMessage)
		{
			this.m_type = Error.ErrorType.UNDEFINED;
			this.m_lineNr = -1;
			this.m_linePosition = -1;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00008ED4 File Offset: 0x000070D4
		public Error(string pMessage, Error.ErrorType type, int lineNr, int linePosition)
			: base(pMessage)
		{
			this.m_type = type;
			this.m_lineNr = lineNr;
			this.m_linePosition = linePosition;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00008EF4 File Offset: 0x000070F4
		public string getMessage()
		{
			return this.Message;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00008EFC File Offset: 0x000070FC
		public int getLineNr()
		{
			return this.m_lineNr;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00008F04 File Offset: 0x00007104
		public int getLinePosition()
		{
			return this.m_linePosition;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00008F0C File Offset: 0x0000710C
		public Error.ErrorType getErrorType()
		{
			return this.m_type;
		}

		// Token: 0x04000099 RID: 153
		private int m_lineNr;

		// Token: 0x0400009A RID: 154
		private int m_linePosition;

		// Token: 0x0400009B RID: 155
		private Error.ErrorType m_type;

		// Token: 0x02000020 RID: 32
		public enum ErrorType
		{
			// Token: 0x0400009D RID: 157
			UNDEFINED,
			// Token: 0x0400009E RID: 158
			SYNTAX,
			// Token: 0x0400009F RID: 159
			LOGIC,
			// Token: 0x040000A0 RID: 160
			SCOPE,
			// Token: 0x040000A1 RID: 161
			RUNTIME
		}
	}
}
