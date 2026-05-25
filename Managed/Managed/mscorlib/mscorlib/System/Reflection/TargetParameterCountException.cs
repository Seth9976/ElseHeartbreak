using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	/// <summary>The exception that is thrown when the number of parameters for an invocation does not match the number expected. This class cannot be inherited.</summary>
	// Token: 0x020002BD RID: 701
	[ComVisible(true)]
	[Serializable]
	public sealed class TargetParameterCountException : ApplicationException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with an empty message string and the root cause of the exception.</summary>
		// Token: 0x0600234D RID: 9037 RVA: 0x0007E654 File Offset: 0x0007C854
		public TargetParameterCountException()
			: base(Locale.GetText("Number of parameter does not match expected count."))
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with its message string set to the given message and the root cause exception.</summary>
		/// <param name="message">A String describing the reason this exception was thrown. </param>
		// Token: 0x0600234E RID: 9038 RVA: 0x0007E668 File Offset: 0x0007C868
		public TargetParameterCountException(string message)
			: base(message)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.TargetParameterCountException" /> class with a specified error message and a reference to the inner exception that is the cause of this exception.</summary>
		/// <param name="message">The error message that explains the reason for the exception. </param>
		/// <param name="inner">The exception that is the cause of the current exception. If the <paramref name="inner" /> parameter is not null, the current exception is raised in a catch block that handles the inner exception. </param>
		// Token: 0x0600234F RID: 9039 RVA: 0x0007E674 File Offset: 0x0007C874
		public TargetParameterCountException(string message, Exception inner)
			: base(message, inner)
		{
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0007E680 File Offset: 0x0007C880
		internal TargetParameterCountException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
