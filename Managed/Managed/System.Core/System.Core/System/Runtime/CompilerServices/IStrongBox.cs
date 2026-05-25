using System;

namespace System.Runtime.CompilerServices
{
	/// <summary>Defines a property for accessing the value that an object references.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000018 RID: 24
	public interface IStrongBox
	{
		/// <summary>Gets or sets the value that an object references.</summary>
		/// <returns>The value that the object references.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000162 RID: 354
		// (set) Token: 0x06000163 RID: 355
		object Value { get; set; }
	}
}
