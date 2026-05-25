using System;

namespace System.Data.SqlClient
{
	/// <summary>Defines the mapping between a column in a <see cref="T:System.Data.SqlClient.SqlBulkCopy" /> instance's data source and a column in the instance's destination table. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017C RID: 380
	public sealed class SqlBulkCopyColumnMapping
	{
		/// <summary>Default constructor that initializes a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</summary>
		// Token: 0x06001453 RID: 5203 RVA: 0x00055B34 File Offset: 0x00053D34
		public SqlBulkCopyColumnMapping()
		{
		}

		/// <summary>Creates a new column mapping, using column ordinals to refer to source and destination columns.</summary>
		/// <param name="sourceColumnOrdinal">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationOrdinal">The ordinal position of the destination column within the destination table.</param>
		// Token: 0x06001454 RID: 5204 RVA: 0x00055B4C File Offset: 0x00053D4C
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, int destinationOrdinal)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationOrdinal = destinationOrdinal;
		}

		/// <summary>Creates a new column mapping, using a column ordinal to refer to the source column and a column name for the target column.</summary>
		/// <param name="sourceColumnOrdinal">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		// Token: 0x06001455 RID: 5205 RVA: 0x00055B7C File Offset: 0x00053D7C
		public SqlBulkCopyColumnMapping(int sourceColumnOrdinal, string destinationColumn)
		{
			this.SourceOrdinal = sourceColumnOrdinal;
			this.DestinationColumn = destinationColumn;
		}

		/// <summary>Creates a new column mapping, using a column name to refer to the source column and a column ordinal for the target column.</summary>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationOrdinal">The ordinal position of the destination column within the destination table.</param>
		// Token: 0x06001456 RID: 5206 RVA: 0x00055BAC File Offset: 0x00053DAC
		public SqlBulkCopyColumnMapping(string sourceColumn, int destinationOrdinal)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationOrdinal = destinationOrdinal;
		}

		/// <summary>Creates a new column mapping, using column names to refer to source and destination columns.</summary>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		// Token: 0x06001457 RID: 5207 RVA: 0x00055BDC File Offset: 0x00053DDC
		public SqlBulkCopyColumnMapping(string sourceColumn, string destinationColumn)
		{
			this.SourceColumn = sourceColumn;
			this.DestinationColumn = destinationColumn;
		}

		/// <summary>Name of the column being mapped in the destination database table.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.DestinationColumn" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001458 RID: 5208 RVA: 0x00055C0C File Offset: 0x00053E0C
		// (set) Token: 0x06001459 RID: 5209 RVA: 0x00055C28 File Offset: 0x00053E28
		public string DestinationColumn
		{
			get
			{
				if (this.destinationColumn != null)
				{
					return this.destinationColumn;
				}
				return string.Empty;
			}
			set
			{
				this.destinationOrdinal = -1;
				this.destinationColumn = value;
			}
		}

		/// <summary>Name of the column being mapped in the data source.</summary>
		/// <returns>The string value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.SourceColumn" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CC RID: 972
		// (get) Token: 0x0600145A RID: 5210 RVA: 0x00055C38 File Offset: 0x00053E38
		// (set) Token: 0x0600145B RID: 5211 RVA: 0x00055C54 File Offset: 0x00053E54
		public string SourceColumn
		{
			get
			{
				if (this.sourceColumn != null)
				{
					return this.sourceColumn;
				}
				return string.Empty;
			}
			set
			{
				this.sourceOrdinal = -1;
				this.sourceColumn = value;
			}
		}

		/// <summary>Ordinal value of the destination column within the destination table.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.DestinationOrdinal" /> property, or -1 if the property has not been set.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CD RID: 973
		// (get) Token: 0x0600145C RID: 5212 RVA: 0x00055C64 File Offset: 0x00053E64
		// (set) Token: 0x0600145D RID: 5213 RVA: 0x00055C6C File Offset: 0x00053E6C
		public int DestinationOrdinal
		{
			get
			{
				return this.destinationOrdinal;
			}
			set
			{
				if (value < 0)
				{
					throw new IndexOutOfRangeException();
				}
				this.destinationColumn = null;
				this.destinationOrdinal = value;
			}
		}

		/// <summary>The ordinal position of the source column within the data source.</summary>
		/// <returns>The integer value of the <see cref="P:System.Data.SqlClient.SqlBulkCopyColumnMapping.SourceOrdinal" /> property.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CE RID: 974
		// (get) Token: 0x0600145E RID: 5214 RVA: 0x00055C8C File Offset: 0x00053E8C
		// (set) Token: 0x0600145F RID: 5215 RVA: 0x00055C94 File Offset: 0x00053E94
		public int SourceOrdinal
		{
			get
			{
				return this.sourceOrdinal;
			}
			set
			{
				if (value < 0)
				{
					throw new IndexOutOfRangeException();
				}
				this.sourceColumn = null;
				this.sourceOrdinal = value;
			}
		}

		// Token: 0x04000823 RID: 2083
		private int sourceOrdinal = -1;

		// Token: 0x04000824 RID: 2084
		private int destinationOrdinal = -1;

		// Token: 0x04000825 RID: 2085
		private string sourceColumn;

		// Token: 0x04000826 RID: 2086
		private string destinationColumn;
	}
}
