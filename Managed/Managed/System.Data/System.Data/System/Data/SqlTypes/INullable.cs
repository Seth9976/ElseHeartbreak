using System;

namespace System.Data.SqlTypes
{
	/// <summary>All the <see cref="N:System.Data.SqlTypes" /> objects and structures implement the INullable interface. </summary>
	// Token: 0x02000100 RID: 256
	public interface INullable
	{
		/// <summary>Indicates whether a structure is null. This property is read-only.</summary>
		/// <returns>
		///   <see cref="T:System.Data.SqlTypes.SqlBoolean" />true if the value of this object is null. Otherwise, false.</returns>
		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000C44 RID: 3140
		bool IsNull { get; }
	}
}
