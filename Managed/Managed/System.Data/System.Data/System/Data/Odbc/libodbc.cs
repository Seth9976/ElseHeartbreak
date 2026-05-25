using System;
using System.Runtime.InteropServices;

namespace System.Data.Odbc
{
	// Token: 0x02000139 RID: 313
	internal class libodbc
	{
		// Token: 0x060010E2 RID: 4322 RVA: 0x0004315C File Offset: 0x0004135C
		internal static OdbcInputOutputDirection ConvertParameterDirection(ParameterDirection dir)
		{
			switch (dir)
			{
			case ParameterDirection.Input:
				return OdbcInputOutputDirection.Input;
			case ParameterDirection.Output:
				return OdbcInputOutputDirection.Output;
			case ParameterDirection.InputOutput:
				return OdbcInputOutputDirection.InputOutput;
			case ParameterDirection.ReturnValue:
				return OdbcInputOutputDirection.ReturnValue;
			}
			return OdbcInputOutputDirection.Input;
		}

		// Token: 0x060010E3 RID: 4323
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLAllocHandle(OdbcHandleType HandleType, IntPtr InputHandle, ref IntPtr OutputHandlePtr);

		// Token: 0x060010E4 RID: 4324
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLSetEnvAttr(IntPtr EnvHandle, OdbcEnv Attribute, IntPtr Value, int StringLength);

		// Token: 0x060010E5 RID: 4325
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLConnect(IntPtr ConnectionHandle, string ServerName, short NameLength1, string UserName, short NameLength2, string Authentication, short NameLength3);

		// Token: 0x060010E6 RID: 4326
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLDriverConnect(IntPtr ConnectionHandle, IntPtr WindowHandle, string InConnectionString, short StringLength1, string OutConnectionString, short BufferLength, ref short StringLength2Ptr, ushort DriverCompletion);

		// Token: 0x060010E7 RID: 4327
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLExecDirect(IntPtr StatementHandle, string StatementText, int TextLength);

		// Token: 0x060010E8 RID: 4328
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLRowCount(IntPtr StatementHandle, ref int RowCount);

		// Token: 0x060010E9 RID: 4329
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLNumResultCols(IntPtr StatementHandle, ref short ColumnCount);

		// Token: 0x060010EA RID: 4330
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLFetch(IntPtr StatementHandle);

		// Token: 0x060010EB RID: 4331
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetData(IntPtr StatementHandle, ushort ColumnNumber, SQL_C_TYPE TargetType, ref bool TargetPtr, int BufferLen, ref int Len);

		// Token: 0x060010EC RID: 4332
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetData(IntPtr StatementHandle, ushort ColumnNumber, SQL_C_TYPE TargetType, ref double TargetPtr, int BufferLen, ref int Len);

		// Token: 0x060010ED RID: 4333
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetData(IntPtr StatementHandle, ushort ColumnNumber, SQL_C_TYPE TargetType, ref long TargetPtr, int BufferLen, ref int Len);

		// Token: 0x060010EE RID: 4334
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetData(IntPtr StatementHandle, ushort ColumnNumber, SQL_C_TYPE TargetType, ref short TargetPtr, int BufferLen, ref int Len);

		// Token: 0x060010EF RID: 4335
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetData(IntPtr StatementHandle, ushort ColumnNumber, SQL_C_TYPE TargetType, ref float TargetPtr, int BufferLen, ref int Len);

		// Token: 0x060010F0 RID: 4336
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetData(IntPtr StatementHandle, ushort ColumnNumber, SQL_C_TYPE TargetType, ref OdbcTimestamp TargetPtr, int BufferLen, ref int Len);

		// Token: 0x060010F1 RID: 4337
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetData(IntPtr StatementHandle, ushort ColumnNumber, SQL_C_TYPE TargetType, ref int TargetPtr, int BufferLen, ref int Len);

		// Token: 0x060010F2 RID: 4338
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetData(IntPtr StatementHandle, ushort ColumnNumber, SQL_C_TYPE TargetType, byte[] TargetPtr, int BufferLen, ref int Len);

		// Token: 0x060010F3 RID: 4339
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLDescribeCol(IntPtr StatementHandle, ushort ColumnNumber, byte[] ColumnName, short BufferLength, ref short NameLength, ref short DataType, ref uint ColumnSize, ref short DecimalDigits, ref short Nullable);

		// Token: 0x060010F4 RID: 4340
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLFreeHandle(ushort HandleType, IntPtr SqlHandle);

		// Token: 0x060010F5 RID: 4341
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLDisconnect(IntPtr ConnectionHandle);

		// Token: 0x060010F6 RID: 4342
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLPrepare(IntPtr StatementHandle, string Statement, int TextLength);

		// Token: 0x060010F7 RID: 4343
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLExecute(IntPtr StatementHandle);

		// Token: 0x060010F8 RID: 4344
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetConnectAttr(IntPtr ConnectionHandle, OdbcConnectionAttribute Attribute, out int value, int BufferLength, out int StringLength);

		// Token: 0x060010F9 RID: 4345
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLSetConnectAttr(IntPtr ConnectionHandle, OdbcConnectionAttribute Attribute, IntPtr Value, int Length);

		// Token: 0x060010FA RID: 4346
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLEndTran(int HandleType, IntPtr Handle, short CompletionType);

		// Token: 0x060010FB RID: 4347
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLBindParameter(IntPtr StatementHandle, ushort ParamNum, short InputOutputType, SQL_C_TYPE ValueType, SQL_TYPE ParamType, uint ColSize, short DecimalDigits, IntPtr ParamValue, int BufLen, IntPtr StrLen);

		// Token: 0x060010FC RID: 4348
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLCancel(IntPtr StatementHandle);

		// Token: 0x060010FD RID: 4349
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLCloseCursor(IntPtr StatementHandle);

		// Token: 0x060010FE RID: 4350
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLError(IntPtr EnvironmentHandle, IntPtr ConnectionHandle, IntPtr StatementHandle, byte[] Sqlstate, ref int NativeError, byte[] MessageText, short BufferLength, ref short TextLength);

		// Token: 0x060010FF RID: 4351
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetStmtAttr(IntPtr StatementHandle, int Attribute, ref IntPtr Value, int BufLen, int StrLen);

		// Token: 0x06001100 RID: 4352
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLSetDescField(IntPtr DescriptorHandle, short RecNumber, short FieldIdentifier, byte[] Value, int BufLen);

		// Token: 0x06001101 RID: 4353
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetDiagRec(OdbcHandleType HandleType, IntPtr Handle, ushort RecordNumber, byte[] Sqlstate, ref int NativeError, byte[] MessageText, short BufferLength, ref short TextLength);

		// Token: 0x06001102 RID: 4354
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLMoreResults(IntPtr Handle);

		// Token: 0x06001103 RID: 4355
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLFreeStmt(IntPtr Handle, libodbc.SQLFreeStmtOptions option);

		// Token: 0x06001104 RID: 4356
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLGetInfo(IntPtr connHandle, OdbcInfo info, byte[] buffer, short buffLength, ref short remainingStrLen);

		// Token: 0x06001105 RID: 4357
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLColAttribute(IntPtr StmtHandle, short column, FieldIdentifier fieldId, byte[] charAttributePtr, short bufferLength, ref short strLengthPtr, ref int numericAttributePtr);

		// Token: 0x06001106 RID: 4358
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLPrimaryKeys(IntPtr StmtHandle, string catalog, short catalogLength, string schema, short schemaLength, string tableName, short tableLength);

		// Token: 0x06001107 RID: 4359
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLStatistics(IntPtr StmtHandle, string catalog, short catalogLength, string schema, short schemaLength, string tableName, short tableLength, short unique, short Reserved);

		// Token: 0x06001108 RID: 4360
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLBindCol(IntPtr StmtHandle, short column, SQL_C_TYPE targetType, byte[] buffer, int bufferLength, ref int indicator);

		// Token: 0x06001109 RID: 4361
		[DllImport("odbc32.dll", CharSet = CharSet.Unicode)]
		internal static extern OdbcReturn SQLBindCol(IntPtr StmtHandle, short column, SQL_C_TYPE targetType, ref short value, int bufferLength, ref int indicator);

		// Token: 0x0400064B RID: 1611
		internal const int SQL_OV_ODBC2 = 2;

		// Token: 0x0400064C RID: 1612
		internal const int SQL_OV_ODBC3 = 3;

		// Token: 0x0400064D RID: 1613
		internal const string SQLSTATE_RIGHT_TRUNC = "01004";

		// Token: 0x0400064E RID: 1614
		internal const char C_NULL = '\0';

		// Token: 0x0400064F RID: 1615
		internal const int SQL_NTS = -3;

		// Token: 0x04000650 RID: 1616
		internal const short SQL_TRUE = 1;

		// Token: 0x04000651 RID: 1617
		internal const short SQL_FALSE = 0;

		// Token: 0x04000652 RID: 1618
		internal const short SQL_INDEX_UNIQUE = 0;

		// Token: 0x04000653 RID: 1619
		internal const short SQL_INDEX_ALL = 1;

		// Token: 0x04000654 RID: 1620
		internal const short SQL_QUICK = 0;

		// Token: 0x04000655 RID: 1621
		internal const short SQL_ENSURE = 1;

		// Token: 0x04000656 RID: 1622
		internal const short SQL_NO_NULLS = 0;

		// Token: 0x04000657 RID: 1623
		internal const short SQL_NULLABLE = 1;

		// Token: 0x04000658 RID: 1624
		internal const short SQL_NULLABLE_UNKNOWN = 2;

		// Token: 0x04000659 RID: 1625
		internal const short SQL_ATTR_READONLY = 0;

		// Token: 0x0400065A RID: 1626
		internal const short SQL_ATTR_WRITE = 1;

		// Token: 0x0400065B RID: 1627
		internal const short SQL_ATTR_READWRITE_UNKNOWN = 2;

		// Token: 0x0200013A RID: 314
		internal enum SQLFreeStmtOptions : short
		{
			// Token: 0x0400065D RID: 1629
			Close,
			// Token: 0x0400065E RID: 1630
			Drop,
			// Token: 0x0400065F RID: 1631
			Unbind,
			// Token: 0x04000660 RID: 1632
			ResetParams
		}
	}
}
