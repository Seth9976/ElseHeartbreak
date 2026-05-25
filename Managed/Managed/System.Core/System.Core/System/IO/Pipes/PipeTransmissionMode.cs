using System;

namespace System.IO.Pipes
{
	/// <summary>Specifies the transmission mode of the pipe.</summary>
	// Token: 0x02000078 RID: 120
	[Serializable]
	public enum PipeTransmissionMode
	{
		/// <summary>Indicates that data in the pipe is transmitted and read as a stream of bytes.</summary>
		// Token: 0x040001A6 RID: 422
		Byte,
		/// <summary>Indicates that data in the pipe is transmitted and read as a stream of messages.</summary>
		// Token: 0x040001A7 RID: 423
		Message
	}
}
