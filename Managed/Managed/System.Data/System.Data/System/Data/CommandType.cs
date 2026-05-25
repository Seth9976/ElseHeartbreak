using System;

namespace System.Data
{
	/// <summary>Specifies how a command string is interpreted.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200000F RID: 15
	public enum CommandType
	{
		/// <summary>An SQL text command. (Default.) </summary>
		// Token: 0x0400006B RID: 107
		Text = 1,
		/// <summary>The name of a stored procedure.</summary>
		// Token: 0x0400006C RID: 108
		StoredProcedure = 4,
		/// <summary>The name of a table.</summary>
		// Token: 0x0400006D RID: 109
		TableDirect = 512
	}
}
