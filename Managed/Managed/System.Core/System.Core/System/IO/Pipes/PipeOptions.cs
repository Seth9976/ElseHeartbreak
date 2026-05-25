using System;

namespace System.IO.Pipes
{
	/// <summary>Provides options for creating a <see cref="T:System.IO.Pipes.PipeStream" /> object. This enumeration has a <see cref="T:System.FlagsAttribute" /> attribute that allows a bitwise combination of its member values.</summary>
	// Token: 0x02000075 RID: 117
	[Flags]
	[Serializable]
	public enum PipeOptions
	{
		/// <summary>Indicates that there are no additional parameters.</summary>
		// Token: 0x04000195 RID: 405
		None = 0,
		/// <summary>Indicates that the system should write through any intermediate cache and go directly to the pipe.</summary>
		// Token: 0x04000196 RID: 406
		WriteThrough = 1,
		/// <summary>Indicates that the pipe can be used for asynchronous reading and writing.</summary>
		// Token: 0x04000197 RID: 407
		Asynchronous = 2
	}
}
