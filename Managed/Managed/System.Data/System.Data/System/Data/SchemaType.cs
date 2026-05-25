using System;

namespace System.Data
{
	/// <summary>Specifies how to handle existing schema mappings when performing a <see cref="M:System.Data.Common.DataAdapter.FillSchema(System.Data.DataSet,System.Data.SchemaType)" /> operation.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200006F RID: 111
	public enum SchemaType
	{
		/// <summary>Ignore any table mappings on the DataAdapter. Configure the <see cref="T:System.Data.DataSet" /> using the incoming schema without applying any transformations.</summary>
		// Token: 0x04000206 RID: 518
		Source = 1,
		/// <summary>Apply any existing table mappings to the incoming schema. Configure the <see cref="T:System.Data.DataSet" /> with the transformed schema.</summary>
		// Token: 0x04000207 RID: 519
		Mapped
	}
}
