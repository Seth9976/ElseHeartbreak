using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.OleDb
{
	/// <summary>Automatically generates single-table commands that are used to reconcile changes made to a <see cref="T:System.Data.DataSet" /> with the associated database. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000EB RID: 235
	public sealed class OleDbCommandBuilder : DbCommandBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommandBuilder" /> class.</summary>
		// Token: 0x06000B31 RID: 2865 RVA: 0x00031BA8 File Offset: 0x0002FDA8
		public OleDbCommandBuilder()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommandBuilder" /> class with the associated <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object.</summary>
		/// <param name="adapter">An <see cref="T:System.Data.OleDb.OleDbDataAdapter" />. </param>
		// Token: 0x06000B32 RID: 2866 RVA: 0x00031BB0 File Offset: 0x0002FDB0
		public OleDbCommandBuilder(OleDbDataAdapter adapter)
			: this()
		{
			this.adapter = adapter;
		}

		/// <summary>Gets or sets an <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object for which SQL statements are automatically generated.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataAdapter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000B33 RID: 2867 RVA: 0x00031BC0 File Offset: 0x0002FDC0
		// (set) Token: 0x06000B34 RID: 2868 RVA: 0x00031BC8 File Offset: 0x0002FDC8
		[DefaultValue(null)]
		public new OleDbDataAdapter DataAdapter
		{
			get
			{
				return this.adapter;
			}
			set
			{
				this.adapter = value;
			}
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x00031BD4 File Offset: 0x0002FDD4
		protected override void ApplyParameterInfo(DbParameter dbParameter, DataRow row, StatementType statementType, bool whereClause)
		{
			OleDbParameter oleDbParameter = (OleDbParameter)dbParameter;
			oleDbParameter.Size = int.Parse(row["ColumnSize"].ToString());
			if (row["NumericPrecision"] != DBNull.Value)
			{
				oleDbParameter.Precision = byte.Parse(row["NumericPrecision"].ToString());
			}
			if (row["NumericScale"] != DBNull.Value)
			{
				oleDbParameter.Scale = byte.Parse(row["NumericScale"].ToString());
			}
			oleDbParameter.DbType = (DbType)((int)row["ProviderType"]);
		}

		/// <summary>Retrieves parameter information from the stored procedure specified in the <see cref="T:System.Data.OleDb.OleDbCommand" /> and populates the <see cref="P:System.Data.OleDb.OleDbCommand.Parameters" /> collection of the specified <see cref="T:System.Data.OleDb.OleDbCommand" /> object.</summary>
		/// <param name="command">The <see cref="T:System.Data.OleDb.OleDbCommand" /> referencing the stored procedure from which the parameter information is to be derived. The derived parameters are added to the <see cref="P:System.Data.OleDb.OleDbCommand.Parameters" /> collection of the <see cref="T:System.Data.OleDb.OleDbCommand" />. </param>
		/// <exception cref="T:System.InvalidOperationException">The underlying OLE DB provider does not support returning stored procedure parameter information, the command text is not a valid stored procedure name, or the <see cref="P:System.Data.OleDb.OleDbCommand.CommandType" /> specified was not StoredProcedure. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B36 RID: 2870 RVA: 0x00031C7C File Offset: 0x0002FE7C
		[MonoTODO]
		public static void DeriveParameters(OleDbCommand command)
		{
			if (command.CommandType != CommandType.StoredProcedure)
			{
				throw new InvalidOperationException("You can perform this operation only on CommandTye StoredProcedure");
			}
			throw new NotImplementedException();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000B37 RID: 2871 RVA: 0x00031C9C File Offset: 0x0002FE9C
		[MonoTODO]
		public new OleDbCommand GetDeleteCommand()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform deletions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B38 RID: 2872 RVA: 0x00031CA4 File Offset: 0x0002FEA4
		[MonoTODO]
		public new OleDbCommand GetDeleteCommand(bool useColumnsForParameterNames)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000B39 RID: 2873 RVA: 0x00031CAC File Offset: 0x0002FEAC
		[MonoTODO]
		public new OleDbCommand GetInsertCommand()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform insertions.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B3A RID: 2874 RVA: 0x00031CB4 File Offset: 0x0002FEB4
		[MonoTODO]
		public new OleDbCommand GetInsertCommand(bool useColumnsForParameterNames)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00031CBC File Offset: 0x0002FEBC
		protected override string GetParameterName(int position)
		{
			return string.Format("@p{0}", position);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x00031CD0 File Offset: 0x0002FED0
		protected override string GetParameterName(string parameterName)
		{
			return string.Format("@{0}", parameterName);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x00031CE0 File Offset: 0x0002FEE0
		protected override string GetParameterPlaceholder(int position)
		{
			return this.GetParameterName(position);
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates at the data source.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000B3E RID: 2878 RVA: 0x00031CEC File Offset: 0x0002FEEC
		[MonoTODO]
		public new OleDbCommand GetUpdateCommand()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates at the data source, optionally using columns for parameter names.</summary>
		/// <returns>The automatically generated <see cref="T:System.Data.OleDb.OleDbCommand" /> object required to perform updates.</returns>
		/// <param name="useColumnsForParameterNames">If true, generate parameter names matching column names, if it is possible. If false, generate @p1, @p2, and so on.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000B3F RID: 2879 RVA: 0x00031CF4 File Offset: 0x0002FEF4
		[MonoTODO]
		public new OleDbCommand GetUpdateCommand(bool useColumnsForParameterNames)
		{
			throw new NotImplementedException();
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The original unquoted identifier.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000B40 RID: 2880 RVA: 0x00031CFC File Offset: 0x0002FEFC
		[MonoTODO]
		public override string QuoteIdentifier(string unquotedIdentifier)
		{
			return base.QuoteIdentifier(unquotedIdentifier);
		}

		/// <summary>Given an unquoted identifier in the correct catalog case, returns the correct quoted form of that identifier. This includes correctly escaping any embedded quotes in the identifier.</summary>
		/// <returns>The quoted version of the identifier. Embedded quotes within the identifier are correctly escaped.</returns>
		/// <param name="unquotedIdentifier">The unquoted identifier to be returned in quoted format.</param>
		/// <param name="connection">The <see cref="T:System.Data.OleDb.OleDbConnection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000B41 RID: 2881 RVA: 0x00031D08 File Offset: 0x0002FF08
		[MonoTODO]
		public string QuoteIdentifier(string unquotedIdentifier, OleDbConnection connection)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x00031D10 File Offset: 0x0002FF10
		[MonoTODO]
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			throw new NotImplementedException();
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly un-escaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes correctly un-escaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000B43 RID: 2883 RVA: 0x00031D18 File Offset: 0x0002FF18
		[MonoTODO]
		public override string UnquoteIdentifier(string quotedIdentifier)
		{
			return base.UnquoteIdentifier(quotedIdentifier);
		}

		/// <summary>Given a quoted identifier, returns the correct unquoted form of that identifier. This includes correctly un-escaping any embedded quotes in the identifier.</summary>
		/// <returns>The unquoted identifier, with embedded quotes correctly un-escaped.</returns>
		/// <param name="quotedIdentifier">The identifier that will have its embedded quotes removed.</param>
		/// <param name="connection">The <see cref="T:System.Data.OleDb.OleDbConnection" />.</param>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000B44 RID: 2884 RVA: 0x00031D24 File Offset: 0x0002FF24
		[MonoTODO]
		public string UnquoteIdentifier(string quotedIdentifier, OleDbConnection connection)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000426 RID: 1062
		private OleDbDataAdapter adapter;
	}
}
