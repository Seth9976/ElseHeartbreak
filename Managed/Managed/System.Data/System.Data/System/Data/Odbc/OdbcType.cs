using System;

namespace System.Data.Odbc
{
	/// <summary>Specifies the data type of a field, property, for use in an <see cref="T:System.Data.Odbc.OdbcParameter" />.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200012A RID: 298
	public enum OdbcType
	{
		/// <summary>Exact numeric value with precision 19 (if signed) or 20 (if unsigned) and scale 0 (signed: –2[63] &lt;= n &lt;= 2[63] – 1, unsigned:0 &lt;= n &lt;= 2[64] – 1) (SQL_BIGINT). This maps to <see cref="T:System.Int64" />.</summary>
		// Token: 0x0400058B RID: 1419
		BigInt = 1,
		/// <summary>A stream of binary data (SQL_BINARY). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x0400058C RID: 1420
		Binary,
		/// <summary>Single bit binary data (SQL_BIT). This maps to <see cref="T:System.Boolean" />.</summary>
		// Token: 0x0400058D RID: 1421
		Bit,
		/// <summary>A fixed-length character string (SQL_CHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x0400058E RID: 1422
		Char,
		/// <summary>Date data in the format yyyymmdd (SQL_TYPE_DATE). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x0400058F RID: 1423
		Date = 23,
		/// <summary>Date data in the format yyyymmddhhmmss (SQL_TYPE_TIMESTAMP). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x04000590 RID: 1424
		DateTime = 5,
		/// <summary>Signed, exact, numeric value with a precision of at least p and scale s, where 1 &lt;= p &lt;= 15 and s &lt;= p. The maximum precision is driver-specific (SQL_DECIMAL). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x04000591 RID: 1425
		Decimal,
		/// <summary>Signed, approximate, numeric value with a binary precision 53 (zero or absolute value 10[–308] to 10[308]) (SQL_DOUBLE). This maps to <see cref="T:System.Double" />.</summary>
		// Token: 0x04000592 RID: 1426
		Double = 8,
		/// <summary>Variable length binary data. Maximum length is data source–dependent (SQL_LONGVARBINARY). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x04000593 RID: 1427
		Image,
		/// <summary>Exact numeric value with precision 10 and scale 0 (signed: –2[31] &lt;= n &lt;= 2[31] – 1, unsigned:0 &lt;= n &lt;= 2[32] – 1) (SQL_INTEGER). This maps to <see cref="T:System.Int32" />.</summary>
		// Token: 0x04000594 RID: 1428
		Int,
		/// <summary>Unicode character string of fixed string length (SQL_WCHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000595 RID: 1429
		NChar,
		/// <summary>Unicode variable-length character data. Maximum length is data source–dependent. (SQL_WLONGVARCHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000596 RID: 1430
		NText,
		/// <summary>Signed, exact, numeric value with a precision p and scale s, where 1 &lt;= p &lt;= 15, and s &lt;= p (SQL_NUMERIC). This maps to <see cref="T:System.Decimal" />.</summary>
		// Token: 0x04000597 RID: 1431
		Numeric = 7,
		/// <summary>A variable-length stream of Unicode characters (SQL_WVARCHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x04000598 RID: 1432
		NVarChar = 13,
		/// <summary>Signed, approximate, numeric value with a binary precision 24 (zero or absolute value 10[–38] to 10[38]).(SQL_REAL). This maps to <see cref="T:System.Single" />.</summary>
		// Token: 0x04000599 RID: 1433
		Real,
		/// <summary>Data and time data in the format yyyymmddhhmmss (SQL_TYPE_TIMESTAMP). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x0400059A RID: 1434
		SmallDateTime = 16,
		/// <summary>Exact numeric value with precision 5 and scale 0 (signed: –32,768 &lt;= n &lt;= 32,767, unsigned: 0 &lt;= n &lt;= 65,535) (SQL_SMALLINT). This maps to <see cref="T:System.Int16" />.</summary>
		// Token: 0x0400059B RID: 1435
		SmallInt,
		/// <summary>Variable length character data. Maximum length is data source–dependent (SQL_LONGVARCHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x0400059C RID: 1436
		Text,
		/// <summary>Date data in the format hhmmss (SQL_TYPE_TIMES). This maps to <see cref="T:System.DateTime" />.</summary>
		// Token: 0x0400059D RID: 1437
		Time = 24,
		/// <summary>A stream of binary data (SQL_BINARY). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x0400059E RID: 1438
		Timestamp = 19,
		/// <summary>Exact numeric value with precision 3 and scale 0 (signed: –128 &lt;= n &lt;= 127, unsigned:0 &lt;= n &lt;= 255)(SQL_TINYINT). This maps to <see cref="T:System.Byte" />.</summary>
		// Token: 0x0400059F RID: 1439
		TinyInt,
		/// <summary>A fixed-length GUID (SQL_GUID). This maps to <see cref="T:System.Guid" />.</summary>
		// Token: 0x040005A0 RID: 1440
		UniqueIdentifier = 15,
		/// <summary>Variable length binary. The maximum is set by the user (SQL_VARBINARY). This maps to an <see cref="T:System.Array" /> of type <see cref="T:System.Byte" />.</summary>
		// Token: 0x040005A1 RID: 1441
		VarBinary = 21,
		/// <summary>A variable-length stream character string (SQL_CHAR). This maps to <see cref="T:System.String" />.</summary>
		// Token: 0x040005A2 RID: 1442
		VarChar
	}
}
