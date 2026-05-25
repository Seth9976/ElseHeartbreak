using System;

namespace System.Data.Common
{
	/// <summary>Provides a mechanism for enumerating all available instances of database servers within the local network. This class provides the basis for other strongly typed data source enumerators. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000C3 RID: 195
	public abstract class DbDataSourceEnumerator
	{
		/// <summary>Retrieves a <see cref="T:System.Data.DataTable" /> containing information about all visible instances of the server represented by the strongly typed instance of this class.</summary>
		/// <returns>Returns a <see cref="T:System.Data.DataTable" /> containing information about the visible instances of the associated data source.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600099E RID: 2462
		public abstract DataTable GetDataSources();
	}
}
