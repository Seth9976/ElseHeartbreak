using System;

namespace System.Data
{
	/// <summary>Used by the Visual Basic .NET Data Designers to represent a parameter to a Command object, and optionally, its mapping to <see cref="T:System.Data.DataSet" /> columns.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000053 RID: 83
	public interface IDbDataParameter : IDataParameter
	{
		/// <summary>Indicates the precision of numeric parameters.</summary>
		/// <returns>The maximum number of digits used to represent the Value property of a data provider Parameter object. The default value is 0, which indicates that a data provider sets the precision for Value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060005DA RID: 1498
		// (set) Token: 0x060005DB RID: 1499
		byte Precision { get; set; }

		/// <summary>Indicates the scale of numeric parameters.</summary>
		/// <returns>The number of decimal places to which <see cref="T:System.Data.OleDb.OleDbParameter.Value" /> is resolved. The default is 0.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060005DC RID: 1500
		// (set) Token: 0x060005DD RID: 1501
		byte Scale { get; set; }

		/// <summary>The size of the parameter.</summary>
		/// <returns>The maximum size, in bytes, of the data within the column. The default value is inferred from the the parameter value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000120 RID: 288
		// (get) Token: 0x060005DE RID: 1502
		// (set) Token: 0x060005DF RID: 1503
		int Size { get; set; }
	}
}
