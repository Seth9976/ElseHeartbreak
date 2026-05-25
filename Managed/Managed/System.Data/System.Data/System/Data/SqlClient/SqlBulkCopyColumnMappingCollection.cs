using System;
using System.Collections;

namespace System.Data.SqlClient
{
	/// <summary>Collection of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> objects that inherits from <see cref="T:System.Collections.CollectionBase" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200017D RID: 381
	public sealed class SqlBulkCopyColumnMappingCollection : CollectionBase
	{
		// Token: 0x06001460 RID: 5216 RVA: 0x00055CB4 File Offset: 0x00053EB4
		internal SqlBulkCopyColumnMappingCollection()
		{
		}

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object at the specified index.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> to find.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170003CF RID: 975
		public SqlBulkCopyColumnMapping this[int index]
		{
			get
			{
				if (index < 0 || index > base.Count)
				{
					throw new ArgumentOutOfRangeException("Index is out of range");
				}
				return (SqlBulkCopyColumnMapping)base.List[index];
			}
		}

		/// <summary>Adds the specified mapping to the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</returns>
		/// <param name="bulkCopyColumnMapping">The <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object that describes the mapping to be added to the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001462 RID: 5218 RVA: 0x00055CF0 File Offset: 0x00053EF0
		public SqlBulkCopyColumnMapping Add(SqlBulkCopyColumnMapping bulkCopyColumnMapping)
		{
			if (bulkCopyColumnMapping == null)
			{
				throw new ArgumentNullException("bulkCopyColumnMapping");
			}
			base.List.Add(bulkCopyColumnMapping);
			return bulkCopyColumnMapping;
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using ordinals to specify both source and destination columns.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" />.</returns>
		/// <param name="sourceColumnIndex">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumnIndex">The ordinal position of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001463 RID: 5219 RVA: 0x00055D14 File Offset: 0x00053F14
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, int destinationColumnIndex)
		{
			SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumnIndex);
			return this.Add(sqlBulkCopyColumnMapping);
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using an ordinal for the source column and a string for the destination column.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" />.</returns>
		/// <param name="sourceColumnIndex">The ordinal position of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001464 RID: 5220 RVA: 0x00055D30 File Offset: 0x00053F30
		public SqlBulkCopyColumnMapping Add(int sourceColumnIndex, string destinationColumn)
		{
			SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumnIndex, destinationColumn);
			return this.Add(sqlBulkCopyColumnMapping);
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using a column name to describe the source column and an ordinal to specify the destination column.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" />.</returns>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumnIndex">The ordinal position of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001465 RID: 5221 RVA: 0x00055D4C File Offset: 0x00053F4C
		public SqlBulkCopyColumnMapping Add(string sourceColumn, int destinationColumnIndex)
		{
			SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumn, destinationColumnIndex);
			return this.Add(sqlBulkCopyColumnMapping);
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> and adds it to the collection, using column names to specify both source and destination columns.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" />.</returns>
		/// <param name="sourceColumn">The name of the source column within the data source.</param>
		/// <param name="destinationColumn">The name of the destination column within the destination table.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001466 RID: 5222 RVA: 0x00055D68 File Offset: 0x00053F68
		public SqlBulkCopyColumnMapping Add(string sourceColumn, string destinationColumn)
		{
			SqlBulkCopyColumnMapping sqlBulkCopyColumnMapping = new SqlBulkCopyColumnMapping(sourceColumn, destinationColumn);
			return this.Add(sqlBulkCopyColumnMapping);
		}

		/// <summary>Clears the contents of the collection.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001467 RID: 5223 RVA: 0x00055D84 File Offset: 0x00053F84
		public new void Clear()
		{
			base.List.Clear();
		}

		/// <summary>Gets a value indicating whether a specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object exists in the collection.</summary>
		/// <returns>true if the specified mapping exists in the collection; otherwise false.</returns>
		/// <param name="value">A valid <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001468 RID: 5224 RVA: 0x00055D94 File Offset: 0x00053F94
		public bool Contains(SqlBulkCopyColumnMapping value)
		{
			return base.List.Contains(value);
		}

		/// <summary>Gets the index of the specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object.</summary>
		/// <returns>The zero-based index of the column mapping, or -1 if the column mapping is not found in the collection.</returns>
		/// <param name="value">The <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object for which to search.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001469 RID: 5225 RVA: 0x00055DA4 File Offset: 0x00053FA4
		public int IndexOf(SqlBulkCopyColumnMapping value)
		{
			return base.List.IndexOf(value);
		}

		/// <summary>Copies the elements of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" /> to an array of <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> items, starting at a particular index.</summary>
		/// <param name="array">The one-dimensional <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> array that is the destination of the elements copied from <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />. The array must have zero-based indexing. </param>
		/// <param name="index">The zero-based index in <paramref name="array" /> at which copying begins. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600146A RID: 5226 RVA: 0x00055DB4 File Offset: 0x00053FB4
		public void CopyTo(SqlBulkCopyColumnMapping[] array, int index)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException("Index is out of range");
			}
			if (array == null)
			{
				throw new ArgumentNullException("array");
			}
			int num = base.Count;
			if (num - index > array.Length)
			{
				num = array.Length;
			}
			int i = index;
			int num2 = 0;
			while (i < base.Count)
			{
				array[num2] = (SqlBulkCopyColumnMapping)base.List[i];
				i++;
				num2++;
			}
		}

		/// <summary>Insert a new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> at the index specified.</summary>
		/// <param name="index">Integer value of the location within the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />  at which to insert the new <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" />.</param>
		/// <param name="value">
		///   <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be inserted in the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600146B RID: 5227 RVA: 0x00055E38 File Offset: 0x00054038
		public void Insert(int index, SqlBulkCopyColumnMapping value)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException("Index is out of range");
			}
			base.List.Insert(index, value);
		}

		/// <summary>Removes the specified <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> element from the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMappingCollection" />.</summary>
		/// <param name="value">
		///   <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be removed from the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600146C RID: 5228 RVA: 0x00055E68 File Offset: 0x00054068
		public void Remove(SqlBulkCopyColumnMapping value)
		{
			base.List.Remove(value);
		}

		/// <summary>Removes the mapping at the specified index from the collection.</summary>
		/// <param name="index">The zero-based index of the <see cref="T:System.Data.SqlClient.SqlBulkCopyColumnMapping" /> object to be removed from the collection.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600146D RID: 5229 RVA: 0x00055E78 File Offset: 0x00054078
		public new void RemoveAt(int index)
		{
			if (index < 0 || index > base.Count)
			{
				throw new ArgumentOutOfRangeException("Index is out of range");
			}
			base.RemoveAt(index);
		}
	}
}
