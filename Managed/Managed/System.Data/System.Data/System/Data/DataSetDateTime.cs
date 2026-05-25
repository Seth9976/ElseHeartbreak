using System;

namespace System.Data
{
	/// <summary>Describes the serialization format for <see cref="T:System.DateTime" /> columns in a <see cref="T:System.Data.DataSet" />.</summary>
	// Token: 0x02000032 RID: 50
	public enum DataSetDateTime
	{
		/// <summary>DateTime is always stored in Local. If <see cref="F:System.Data.DataSetDateTime.Utc" /> or <see cref="F:System.Data.DataSetDateTime.Unspecified" /> is assigned to a column in this mode, it is first converted into Local. Serialization in this mode is always performed in Local. There is an offset during serialization.</summary>
		// Token: 0x04000128 RID: 296
		Local = 1,
		/// <summary>DateTime is always stored in Unspecified. If <see cref="F:System.Data.DataSetDateTime.Local" /> or <see cref="F:System.Data.DataSetDateTime.Utc" /> is assigned to a column in this mode, it is first converted into <see cref="F:System.Data.DataSetDateTime.Unspecified" />. Serialization in this mode does not cause an offset.</summary>
		// Token: 0x04000129 RID: 297
		Unspecified,
		/// <summary>DateTime is stored in Unspecified. If <see cref="F:System.Data.DataSetDateTime.Local" /> or <see cref="F:System.Data.DataSetDateTime.Utc" /> is assigned to a column in this mode, it is first converted into <see cref="F:System.Data.DataSetDateTime.Unspecified" />. Serialization in this mode causes offset. This is the default behavior and is backward compatible. This option should be thought of as being Unspecified in storage but applying an offset that is similar to <see cref="F:System.Data.DataSetDateTime.Local" /> during serialization.</summary>
		// Token: 0x0400012A RID: 298
		UnspecifiedLocal,
		/// <summary>DateTime is stored in Universal Coordinated Time (UTC). If <see cref="F:System.Data.DataSetDateTime.Local" /> or <see cref="F:System.Data.DataSetDateTime.Unspecified" /> is assigned to a column in this mode, it is first converted into Utc format. Serialization in this mode is always performed in Utc. There is no offset during serialization.</summary>
		// Token: 0x0400012B RID: 299
		Utc
	}
}
