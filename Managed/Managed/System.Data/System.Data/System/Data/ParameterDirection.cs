using System;

namespace System.Data
{
	/// <summary>Specifies the type of a parameter within a query relative to the <see cref="T:System.Data.DataSet" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000068 RID: 104
	public enum ParameterDirection
	{
		/// <summary>The parameter is an input parameter.</summary>
		// Token: 0x040001F5 RID: 501
		Input = 1,
		/// <summary>The parameter is an output parameter.</summary>
		// Token: 0x040001F6 RID: 502
		Output,
		/// <summary>The parameter is capable of both input and output.</summary>
		// Token: 0x040001F7 RID: 503
		InputOutput,
		/// <summary>The parameter represents a return value from an operation such as a stored procedure, built-in function, or user-defined function.</summary>
		// Token: 0x040001F8 RID: 504
		ReturnValue = 6
	}
}
