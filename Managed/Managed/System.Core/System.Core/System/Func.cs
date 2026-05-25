using System;

namespace System
{
	/// <summary>Encapsulates a method that has no parameters and returns a value of the type specified by the <paramref name="TResult" /> parameter.</summary>
	/// <returns>The return value of the method that this delegate encapsulates.</returns>
	/// <typeparam name="TResult">The type of the return value of the method that this delegate encapsulates.</typeparam>
	// Token: 0x0200008C RID: 140
	// (Invoke) Token: 0x06000658 RID: 1624
	public delegate TResult Func<TResult>();
}
