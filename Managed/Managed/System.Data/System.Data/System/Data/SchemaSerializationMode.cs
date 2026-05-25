using System;

namespace System.Data
{
	/// <summary>Indicates the schema serialization mode for a typed <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x02000071 RID: 113
	public enum SchemaSerializationMode
	{
		/// <summary>Includes schema serialization for a typed <see cref="T:System.Data.DataSet" />. The default.</summary>
		// Token: 0x0400020C RID: 524
		IncludeSchema = 1,
		/// <summary>Skips schema serialization for a typed <see cref="T:System.Data.DataSet" />.</summary>
		// Token: 0x0400020D RID: 525
		ExcludeSchema
	}
}
