using System;

namespace System.Data
{
	/// <summary>Provides access to the column values within each row for a DataReader, and is implemented by .NET Framework data providers that access relational databases.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200004F RID: 79
	public interface IDataRecord
	{
		/// <summary>Gets the value of the specified column as a Boolean.</summary>
		/// <returns>The value of the column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600059A RID: 1434
		bool GetBoolean(int i);

		/// <summary>Gets the 8-bit unsigned integer value of the specified column.</summary>
		/// <returns>The 8-bit unsigned integer value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600059B RID: 1435
		byte GetByte(int i);

		/// <summary>Reads a stream of bytes from the specified column offset into the buffer as an array, starting at the given buffer offset.</summary>
		/// <returns>The actual number of bytes read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="fieldOffset">The index within the field from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferoffset">The index for <paramref name="buffer" /> to start the read operation. </param>
		/// <param name="length">The number of bytes to read. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600059C RID: 1436
		long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length);

		/// <summary>Gets the character value of the specified column.</summary>
		/// <returns>The character value of the specified column.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600059D RID: 1437
		char GetChar(int i);

		/// <summary>Reads a stream of characters from the specified column offset into the buffer as an array, starting at the given buffer offset.</summary>
		/// <returns>The actual number of characters read.</returns>
		/// <param name="i">The zero-based column ordinal. </param>
		/// <param name="fieldoffset">The index within the row from which to start the read operation. </param>
		/// <param name="buffer">The buffer into which to read the stream of bytes. </param>
		/// <param name="bufferoffset">The index for <paramref name="buffer" /> to start the read operation. </param>
		/// <param name="length">The number of bytes to read. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600059E RID: 1438
		long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length);

		/// <summary>Returns an <see cref="T:System.Data.IDataReader" /> for the specified column ordinal.</summary>
		/// <returns>An <see cref="T:System.Data.IDataReader" />.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600059F RID: 1439
		IDataReader GetData(int i);

		/// <summary>Gets the data type information for the specified field.</summary>
		/// <returns>The data type information for the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A0 RID: 1440
		string GetDataTypeName(int i);

		/// <summary>Gets the date and time data value of the specified field.</summary>
		/// <returns>The date and time data value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A1 RID: 1441
		DateTime GetDateTime(int i);

		/// <summary>Gets the fixed-position numeric value of the specified field.</summary>
		/// <returns>The fixed-position numeric value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A2 RID: 1442
		decimal GetDecimal(int i);

		/// <summary>Gets the double-precision floating point number of the specified field.</summary>
		/// <returns>The double-precision floating point number of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A3 RID: 1443
		double GetDouble(int i);

		/// <summary>Gets the <see cref="T:System.Type" /> information corresponding to the type of <see cref="T:System.Object" /> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)" />.</summary>
		/// <returns>The <see cref="T:System.Type" /> information corresponding to the type of <see cref="T:System.Object" /> that would be returned from <see cref="M:System.Data.IDataRecord.GetValue(System.Int32)" />.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A4 RID: 1444
		Type GetFieldType(int i);

		/// <summary>Gets the single-precision floating point number of the specified field.</summary>
		/// <returns>The single-precision floating point number of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A5 RID: 1445
		float GetFloat(int i);

		/// <summary>Returns the GUID value of the specified field.</summary>
		/// <returns>The GUID value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A6 RID: 1446
		Guid GetGuid(int i);

		/// <summary>Gets the 16-bit signed integer value of the specified field.</summary>
		/// <returns>The 16-bit signed integer value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A7 RID: 1447
		short GetInt16(int i);

		/// <summary>Gets the 32-bit signed integer value of the specified field.</summary>
		/// <returns>The 32-bit signed integer value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A8 RID: 1448
		int GetInt32(int i);

		/// <summary>Gets the 64-bit signed integer value of the specified field.</summary>
		/// <returns>The 64-bit signed integer value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005A9 RID: 1449
		long GetInt64(int i);

		/// <summary>Gets the name for the field to find.</summary>
		/// <returns>The name of the field or the empty string (""), if there is no value to return.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005AA RID: 1450
		string GetName(int i);

		/// <summary>Return the index of the named field.</summary>
		/// <returns>The index of the named field.</returns>
		/// <param name="name">The name of the field to find. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005AB RID: 1451
		int GetOrdinal(string name);

		/// <summary>Gets the string value of the specified field.</summary>
		/// <returns>The string value of the specified field.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005AC RID: 1452
		string GetString(int i);

		/// <summary>Return the value of the specified field.</summary>
		/// <returns>The <see cref="T:System.Object" /> which will contain the field value upon return.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005AD RID: 1453
		object GetValue(int i);

		/// <summary>Populates an array of objects with the column values of the current record.</summary>
		/// <returns>The number of instances of <see cref="T:System.Object" /> in the array.</returns>
		/// <param name="values">An array of <see cref="T:System.Object" /> to copy the attribute fields into. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005AE RID: 1454
		int GetValues(object[] values);

		/// <summary>Return whether the specified field is set to null.</summary>
		/// <returns>true if the specified field is set to null; otherwise, false.</returns>
		/// <param name="i">The index of the field to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060005AF RID: 1455
		bool IsDBNull(int i);

		/// <summary>Gets the number of columns in the current row.</summary>
		/// <returns>When not positioned in a valid recordset, 0; otherwise, the number of columns in the current record. The default is -1.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060005B0 RID: 1456
		int FieldCount { get; }

		/// <summary>Gets the column with the specified name.</summary>
		/// <returns>The column with the specified name as an <see cref="T:System.Object" />.</returns>
		/// <param name="name">The name of the column to find. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">No column with the specified name was found. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700010D RID: 269
		object this[string name] { get; }

		/// <summary>Gets the column located at the specified index.</summary>
		/// <returns>The column located at the specified index as an <see cref="T:System.Object" />.</returns>
		/// <param name="i">The zero-based index of the column to get. </param>
		/// <exception cref="T:System.IndexOutOfRangeException">The index passed was outside the range of 0 through <see cref="P:System.Data.IDataRecord.FieldCount" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700010E RID: 270
		object this[int i] { get; }
	}
}
