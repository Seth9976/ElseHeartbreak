using System;
using System.Runtime.Serialization;

namespace System.Data
{
	/// <summary>This exception is thrown when an ongoing operation is aborted by the user.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000067 RID: 103
	[Serializable]
	public sealed class OperationAbortedException : SystemException
	{
		// Token: 0x06000639 RID: 1593 RVA: 0x0001F23C File Offset: 0x0001D43C
		internal OperationAbortedException()
			: base(Locale.GetText("An OperationAbortedException has occurred."))
		{
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0001F250 File Offset: 0x0001D450
		internal OperationAbortedException(string s)
			: base(s)
		{
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0001F25C File Offset: 0x0001D45C
		internal OperationAbortedException(string s, Exception innerException)
			: base(s, innerException)
		{
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0001F268 File Offset: 0x0001D468
		internal OperationAbortedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
