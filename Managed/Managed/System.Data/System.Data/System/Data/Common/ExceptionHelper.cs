using System;
using System.Globalization;

namespace System.Data.Common
{
	// Token: 0x020000D1 RID: 209
	internal sealed class ExceptionHelper
	{
		// Token: 0x06000A0F RID: 2575 RVA: 0x0002F200 File Offset: 0x0002D400
		internal static ArgumentException InvalidSizeValue(int value)
		{
			string[] array = new string[] { value.ToString() };
			return new ArgumentException(ExceptionHelper.GetExceptionMessage("Invalid parameter Size value '{0}'. The value must be greater than or equal to 0.", array));
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002F230 File Offset: 0x0002D430
		internal static void CheckEnumValue(Type enumType, object value)
		{
			if (!Enum.IsDefined(enumType, value))
			{
				throw ExceptionHelper.InvalidEnumValueException(enumType.Name, value);
			}
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x0002F24C File Offset: 0x0002D44C
		internal static ArgumentException InvalidEnumValueException(string enumeration, object value)
		{
			string text = string.Format(CultureInfo.InvariantCulture, "The {0} enumeration value, {1}, is invalid.", new object[] { enumeration, value });
			return new ArgumentOutOfRangeException(enumeration, text);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x0002F280 File Offset: 0x0002D480
		internal static ArgumentOutOfRangeException InvalidDataRowVersion(DataRowVersion value)
		{
			object[] array = new object[]
			{
				"DataRowVersion",
				value.ToString()
			};
			return new ArgumentOutOfRangeException(ExceptionHelper.GetExceptionMessage("{0}: Invalid DataRow Version enumeration value: {1}", array));
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x0002F2BC File Offset: 0x0002D4BC
		internal static ArgumentOutOfRangeException InvalidParameterDirection(ParameterDirection value)
		{
			object[] array = new object[]
			{
				"ParameterDirection",
				value.ToString()
			};
			return new ArgumentOutOfRangeException(ExceptionHelper.GetExceptionMessage("Invalid direction '{0}' for '{1}' parameter.", array));
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x0002F2F8 File Offset: 0x0002D4F8
		internal static InvalidOperationException NoStoredProcedureExists(string procedureName)
		{
			object[] array = new object[] { procedureName };
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("The stored procedure '{0}' doesn't exist.", array));
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x0002F320 File Offset: 0x0002D520
		internal static ArgumentNullException ArgumentNull(string parameter)
		{
			return new ArgumentNullException(parameter);
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x0002F328 File Offset: 0x0002D528
		internal static InvalidOperationException TransactionRequired()
		{
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("Execute requires the command to have a transaction object when the connection assigned to the command is in a pending local transaction.  The Transaction property of the command has not been initialized."));
		}

		// Token: 0x06000A17 RID: 2583 RVA: 0x0002F33C File Offset: 0x0002D53C
		internal static ArgumentOutOfRangeException InvalidOleDbType(int value)
		{
			string[] array = new string[] { value.ToString() };
			return new ArgumentOutOfRangeException(ExceptionHelper.GetExceptionMessage("Invalid OleDbType enumeration value: {0}", array));
		}

		// Token: 0x06000A18 RID: 2584 RVA: 0x0002F36C File Offset: 0x0002D56C
		internal static ArgumentException InvalidDbType(int value)
		{
			string[] array = new string[] { value.ToString() };
			return new ArgumentException(ExceptionHelper.GetExceptionMessage("No mapping exists from DbType {0} to a known {1}.", array));
		}

		// Token: 0x06000A19 RID: 2585 RVA: 0x0002F39C File Offset: 0x0002D59C
		internal static InvalidOperationException DeriveParametersNotSupported(Type type, CommandType commandType)
		{
			string[] array = new string[]
			{
				type.ToString(),
				commandType.ToString()
			};
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("{0} DeriveParameters only supports CommandType.StoredProcedure, not CommandType.{1}.", array));
		}

		// Token: 0x06000A1A RID: 2586 RVA: 0x0002F3D8 File Offset: 0x0002D5D8
		internal static InvalidOperationException ReaderClosed(string mehodName)
		{
			string[] array = new string[] { mehodName };
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("Invalid attempt to {0} when reader is closed.", array));
		}

		// Token: 0x06000A1B RID: 2587 RVA: 0x0002F400 File Offset: 0x0002D600
		internal static ArgumentOutOfRangeException InvalidSqlDbType(int value)
		{
			string[] array = new string[] { value.ToString() };
			return new ArgumentOutOfRangeException(ExceptionHelper.GetExceptionMessage("{0}: Invalid SqlDbType enumeration value: {1}.", array));
		}

		// Token: 0x06000A1C RID: 2588 RVA: 0x0002F430 File Offset: 0x0002D630
		internal static ArgumentException UnknownDataType(string type1, string type2)
		{
			string[] array = new string[] { type1, type2 };
			return new ArgumentException(ExceptionHelper.GetExceptionMessage("No mapping exists from DbType {0} to a known {1}.", array));
		}

		// Token: 0x06000A1D RID: 2589 RVA: 0x0002F45C File Offset: 0x0002D65C
		internal static InvalidOperationException TransactionNotInitialized()
		{
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("Execute requires the command to have a transaction object when the connection assigned to the command is in a pending local transaction.  The Transaction property of the command has not been initialized."));
		}

		// Token: 0x06000A1E RID: 2590 RVA: 0x0002F470 File Offset: 0x0002D670
		internal static InvalidOperationException TransactionNotUsable(Type type)
		{
			return new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, "This {0} has completed; it is no longer usable.", new object[] { type.Name }));
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002F4A0 File Offset: 0x0002D6A0
		internal static InvalidOperationException ParametersNotInitialized(int parameterPosition, string parameterName, string parameterType)
		{
			object[] array = new object[] { parameterPosition, parameterName, parameterType };
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("Parameter {0}: '{1}', the property DbType is uninitialized: OleDbType.{2}.", array));
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x0002F4D8 File Offset: 0x0002D6D8
		internal static InvalidOperationException WrongParameterSize(string provider)
		{
			string[] array = new string[] { provider };
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("{0}.Prepare method requires all variable length parameters to have an explicitly set non-zero Size.", array));
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x0002F500 File Offset: 0x0002D700
		internal static InvalidOperationException ConnectionNotOpened(string operationName, string connectionState)
		{
			object[] array = new object[] { operationName, connectionState };
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("{0} requires an open and available Connection. The connection's current state is {1}.", array));
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x0002F52C File Offset: 0x0002D72C
		internal static InvalidOperationException ConnectionNotInitialized(string methodName)
		{
			object[] array = new object[] { methodName };
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("{0}: Connection property has not been initialized.", array));
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x0002F554 File Offset: 0x0002D754
		internal static InvalidOperationException OpenConnectionRequired(string methodName, object connectionState)
		{
			object[] array = new object[] { methodName, connectionState };
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("{0} requires an open and available Connection. The connection's current state is {1}.", array));
		}

		// Token: 0x06000A24 RID: 2596 RVA: 0x0002F580 File Offset: 0x0002D780
		internal static InvalidOperationException OpenedReaderExists()
		{
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("There is already an open DataReader associated with this Connection which must be closed first."));
		}

		// Token: 0x06000A25 RID: 2597 RVA: 0x0002F594 File Offset: 0x0002D794
		internal static InvalidOperationException ConnectionAlreadyOpen(object connectionState)
		{
			object[] array = new object[] { connectionState };
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("The connection is already Open (state={0}).", array));
		}

		// Token: 0x06000A26 RID: 2598 RVA: 0x0002F5BC File Offset: 0x0002D7BC
		internal static InvalidOperationException ConnectionClosed()
		{
			return new InvalidOperationException("Invalid operation. The Connection is closed.");
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0002F5C8 File Offset: 0x0002D7C8
		internal static InvalidOperationException ConnectionStringNotInitialized()
		{
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("The ConnectionString property has not been initialized."));
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0002F5DC File Offset: 0x0002D7DC
		internal static InvalidOperationException ConnectionIsBusy(object commandType, object connectionState)
		{
			object[] array = new object[]
			{
				commandType.ToString(),
				connectionState.ToString()
			};
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("The {0} is currently busy {1}.", array));
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0002F614 File Offset: 0x0002D814
		internal static InvalidOperationException NotAllowedWhileConnectionOpen(string propertyName, object connectionState)
		{
			object[] array = new object[] { propertyName, connectionState };
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("Not allowed to change the '{0}' property while the connection (state={1}).", array));
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0002F640 File Offset: 0x0002D840
		internal static ArgumentException OleDbNoProviderSpecified()
		{
			return new ArgumentException(ExceptionHelper.GetExceptionMessage("An OLE DB Provider was not specified in the ConnectionString.  An example would be, 'Provider=SQLOLEDB;'."));
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0002F654 File Offset: 0x0002D854
		internal static ArgumentException InvalidValueForKey(string key)
		{
			string[] array = new string[] { key };
			return new ArgumentException(string.Format("Invalid value for key {0}", array));
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0002F67C File Offset: 0x0002D87C
		internal static InvalidOperationException ParameterSizeNotInitialized(int parameterIndex, string parameterName, string parameterType, int parameterSize)
		{
			object[] array = new object[]
			{
				parameterIndex.ToString(),
				parameterName,
				parameterType,
				parameterSize.ToString()
			};
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("Parameter {0}: '{1}' of type: {2}, the property Size has an invalid size: {3}", array));
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x0002F6BC File Offset: 0x0002D8BC
		internal static ArgumentException InvalidUpdateStatus(UpdateStatus status)
		{
			object[] array = new object[] { status };
			return new ArgumentException(ExceptionHelper.GetExceptionMessage("Invalid UpdateStatus: {0}", array));
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x0002F6EC File Offset: 0x0002D8EC
		internal static InvalidOperationException UpdateRequiresCommand(string command)
		{
			object[] array = new object[] { command };
			return new InvalidOperationException(ExceptionHelper.GetExceptionMessage("Auto SQL generation during {0} requires a valid SelectCommand.", array));
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x0002F714 File Offset: 0x0002D914
		internal static DataException RowUpdatedError()
		{
			return new DataException(ExceptionHelper.GetExceptionMessage("RowUpdatedEvent: Errors occurred; no additional is information available."));
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x0002F728 File Offset: 0x0002D928
		internal static ArgumentNullException CollectionNoNullsAllowed(object collection, object objectsType)
		{
			object[] array = new object[]
			{
				collection.GetType().ToString(),
				objectsType.ToString()
			};
			return new ArgumentNullException(ExceptionHelper.GetExceptionMessage("The {0} only accepts non-null {1} type objects.", array));
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x0002F764 File Offset: 0x0002D964
		internal static ArgumentException CollectionAlreadyContains(object objectType, string propertyName, object propertyValue, object collection)
		{
			object[] array = new object[]
			{
				objectType.ToString(),
				propertyName,
				propertyValue,
				collection.GetType().ToString()
			};
			return new ArgumentException(ExceptionHelper.GetExceptionMessage("The {0} with {1} '{2}' is already contained by this {3}.", array));
		}

		// Token: 0x06000A32 RID: 2610 RVA: 0x0002F7A8 File Offset: 0x0002D9A8
		internal static string GetExceptionMessage(string exceptionMessage, object[] args)
		{
			if (args == null || args.Length == 0)
			{
				return exceptionMessage;
			}
			return string.Format(exceptionMessage, args);
		}

		// Token: 0x06000A33 RID: 2611 RVA: 0x0002F7C4 File Offset: 0x0002D9C4
		internal static string GetExceptionMessage(string exceptionMessage)
		{
			return ExceptionHelper.GetExceptionMessage(exceptionMessage, null);
		}
	}
}
