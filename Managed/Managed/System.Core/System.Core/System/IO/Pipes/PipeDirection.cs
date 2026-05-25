using System;

namespace System.IO.Pipes
{
	/// <summary>Specifies the direction of the pipe.</summary>
	// Token: 0x0200006F RID: 111
	[Serializable]
	public enum PipeDirection
	{
		/// <summary>Specifies that the pipe direction is in.</summary>
		// Token: 0x04000191 RID: 401
		In = 1,
		/// <summary>Specifies that the pipe direction is out.</summary>
		// Token: 0x04000192 RID: 402
		Out,
		/// <summary>Specifies that the pipe direction is two-way.</summary>
		// Token: 0x04000193 RID: 403
		InOut
	}
}
