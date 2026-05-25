using System;

namespace System.Data.OleDb
{
	/// <summary>Specifies the data type of a field, a property, for use in an <see cref="T:System.Data.OleDb.OleDbParameter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000FF RID: 255
	public enum OleDbType
	{
		/// <summary>A 64-bit signed integer (DBTYPE_I8). This maps to <see cref="T:System.Int64" />.</summary>
		// Token: 0x040004A1 RID: 1185
		BigInt = 20,
		/// <summary>A stream of binary data (DBTYPE_BYTES). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x040004A2 RID: 1186
		Binary = 128,
		/// <summary>A Boolean value (DBTYPE_BOOL). This maps to <see cref="T:System.Boolean" />.</summary>
		// Token: 0x040004A3 RID: 1187
		Boolean = 11,
		/// <summary>A null-terminated character string of Unicode characters (DBTYPE_BSTR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040004A4 RID: 1188
		BSTR = 8,
		/// <summary>A character string (DBTYPE_STR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040004A5 RID: 1189
		Char = 129,
		/// <summary>A currency value ranging from -2 63 (or -922,337,203,685,477.5808) to 2 63 -1 (or +922,337,203,685,477.5807) with an accuracy to a ten-thousandth of a currency unit (DBTYPE_CY). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x040004A6 RID: 1190
		Currency = 6,
		/// <summary>Date data, stored as a double (DBTYPE_DATE). The whole portion is the number of days since December 30, 1899, and the fractional portion is a fraction of a day. This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x040004A7 RID: 1191
		Date,
		/// <summary>Date data in the format yyyymmdd (DBTYPE_DBDATE). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x040004A8 RID: 1192
		DBDate = 133,
		/// <summary>Time data in the format hhmmss (DBTYPE_DBTIME). This maps to <see cref="T:System.TimeSpan" />.</summary>
		// Token: 0x040004A9 RID: 1193
		DBTime,
		/// <summary>Data and time data in the format yyyymmddhhmmss (DBTYPE_DBTIMESTAMP). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x040004AA RID: 1194
		DBTimeStamp,
		/// <summary>A fixed precision and scale numeric value between -10 38 -1 and 10 38 -1 (DBTYPE_DECIMAL). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x040004AB RID: 1195
		Decimal = 14,
		/// <summary>A floating-point number within the range of -1.79E +308 through 1.79E +308 (DBTYPE_R8). This maps to <see cref="T:System.Double" />.</summary>
		// Token: 0x040004AC RID: 1196
		Double = 5,
		/// <summary>No value (DBTYPE_EMPTY).</summary>
		// Token: 0x040004AD RID: 1197
		Empty = 0,
		/// <summary>A 32-bit error code (DBTYPE_ERROR). This maps to <see cref="T:System.Exception" />.</summary>
		// Token: 0x040004AE RID: 1198
		Error = 10,
		/// <summary>A 64-bit unsigned integer representing the number of 100-nanosecond intervals since January 1, 1601 (DBTYPE_FILETIME). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x040004AF RID: 1199
		Filetime = 64,
		/// <summary>A globally unique identifier (or GUID) (DBTYPE_GUID). This maps to <see cref="T:System.Guid" />.</summary>
		// Token: 0x040004B0 RID: 1200
		Guid = 72,
		/// <summary>A pointer to an IDispatch interface (DBTYPE_IDISPATCH). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x040004B1 RID: 1201
		IDispatch = 9,
		/// <summary>A 32-bit signed integer (DBTYPE_I4). This maps to <see cref="T:System.Int32" />.</summary>
		// Token: 0x040004B2 RID: 1202
		Integer = 3,
		/// <summary>A pointer to an IUnknown interface (DBTYPE_UNKNOWN). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x040004B3 RID: 1203
		IUnknown = 13,
		/// <summary>A long binary value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x040004B4 RID: 1204
		LongVarBinary = 205,
		/// <summary>A long string value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040004B5 RID: 1205
		LongVarChar = 201,
		/// <summary>A long null-terminated Unicode string value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040004B6 RID: 1206
		LongVarWChar = 203,
		/// <summary>An exact numeric value with a fixed precision and scale (DBTYPE_NUMERIC). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x040004B7 RID: 1207
		Numeric = 131,
		/// <summary>An automation PROPVARIANT (DBTYPE_PROP_VARIANT). This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x040004B8 RID: 1208
		PropVariant = 138,
		/// <summary>A floating-point number within the range of -3.40E +38 through 3.40E +38 (DBTYPE_R4). This maps to <see cref="T:System.Single" />.</summary>
		// Token: 0x040004B9 RID: 1209
		Single = 4,
		/// <summary>A 16-bit signed integer (DBTYPE_I2). This maps to <see cref="T:System.Int16" />.</summary>
		// Token: 0x040004BA RID: 1210
		SmallInt = 2,
		/// <summary>A 8-bit signed integer (DBTYPE_I1). This maps to <see cref="T:System.SByte" />.</summary>
		// Token: 0x040004BB RID: 1211
		TinyInt = 16,
		/// <summary>A 64-bit unsigned integer (DBTYPE_UI8). This maps to <see cref="T:System.UInt64" />.</summary>
		// Token: 0x040004BC RID: 1212
		UnsignedBigInt = 21,
		/// <summary>A 32-bit unsigned integer (DBTYPE_UI4). This maps to <see cref="T:System.UInt32" />.</summary>
		// Token: 0x040004BD RID: 1213
		UnsignedInt = 19,
		/// <summary>A 16-bit unsigned integer (DBTYPE_UI2). This maps to <see cref="T:System.UInt16" />.</summary>
		// Token: 0x040004BE RID: 1214
		UnsignedSmallInt = 18,
		/// <summary>A 8-bit unsigned integer (DBTYPE_UI1). This maps to <see cref="T:System.Byte" />.</summary>
		// Token: 0x040004BF RID: 1215
		UnsignedTinyInt = 17,
		/// <summary>A variable-length stream of binary data (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x040004C0 RID: 1216
		VarBinary = 204,
		/// <summary>A variable-length stream of non-Unicode characters (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040004C1 RID: 1217
		VarChar = 200,
		/// <summary>A special data type that can contain numeric, string, binary, or date data, and also the special values Empty and Null (DBTYPE_VARIANT). This type is assumed if no other is specified. This maps to <see cref="T:System.Object" />.</summary>
		// Token: 0x040004C2 RID: 1218
		Variant = 12,
		/// <summary>A variable-length numeric value (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x040004C3 RID: 1219
		VarNumeric = 139,
		/// <summary>A variable-length, null-terminated stream of Unicode characters (<see cref="T:System.Data.OleDb.OleDbParameter" /> only). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040004C4 RID: 1220
		VarWChar = 202,
		/// <summary>A null-terminated stream of Unicode characters (DBTYPE_WSTR). This maps to <see cref="T:System.String" />. </summary>
		// Token: 0x040004C5 RID: 1221
		WChar = 130
	}
}
