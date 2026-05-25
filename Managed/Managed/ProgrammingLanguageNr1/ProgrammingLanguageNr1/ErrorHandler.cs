using System;
using System.Collections.Generic;

namespace ProgrammingLanguageNr1
{
	// Token: 0x0200001E RID: 30
	public class ErrorHandler
	{
		// Token: 0x0600010F RID: 271 RVA: 0x00008D74 File Offset: 0x00006F74
		public void errorOccured(Error e)
		{
			this.m_errors.Add(e);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00008D84 File Offset: 0x00006F84
		public void errorOccured(string message, Error.ErrorType errorType, int lineNr, int linePosition)
		{
			this.m_errors.Add(new Error(message, errorType, lineNr, linePosition));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00008D9C File Offset: 0x00006F9C
		public void errorOccured(string message, Error.ErrorType errorType)
		{
			this.m_errors.Add(new Error(message, errorType, 0, 0));
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00008DB4 File Offset: 0x00006FB4
		public void printErrorsToConsole()
		{
			if (this.getErrors().Count == 0)
			{
				Console.WriteLine("NO ERRORS");
			}
			else
			{
				Console.WriteLine("ERROR MESSAGES: ");
				foreach (Error error in this.getErrors())
				{
					Console.WriteLine(string.Concat(new object[]
					{
						error.getErrorType(),
						" ERROR: ",
						error.getMessage(),
						" at line ",
						error.getLineNr(),
						" and position ",
						error.getLinePosition()
					}));
				}
			}
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00008E9C File Offset: 0x0000709C
		public void Reset()
		{
			this.m_errors.Clear();
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00008EAC File Offset: 0x000070AC
		public List<Error> getErrors()
		{
			return this.m_errors;
		}

		// Token: 0x04000098 RID: 152
		private List<Error> m_errors = new List<Error>();
	}
}
