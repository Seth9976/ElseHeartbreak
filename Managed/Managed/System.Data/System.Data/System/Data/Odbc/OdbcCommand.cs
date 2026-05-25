using System;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.Odbc
{
	/// <summary>Represents an SQL statement or stored procedure to execute against a data source. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200011E RID: 286
	[DefaultEvent("RecordsAffected")]
	[Designer("Microsoft.VSDesigner.Data.VS.OdbcCommandDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxItem("System.Drawing.Design.ToolboxItem, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class OdbcCommand : DbCommand, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommand" /> class.</summary>
		// Token: 0x06000FCE RID: 4046 RVA: 0x0003DA94 File Offset: 0x0003BC94
		public OdbcCommand()
		{
			this.timeout = 30;
			this.commandType = CommandType.Text;
			this._parameters = new OdbcParameterCollection();
			this.designTimeVisible = true;
			this.updateRowSource = UpdateRowSource.Both;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommand" /> class with the text of the query.</summary>
		/// <param name="cmdText">The text of the query. </param>
		// Token: 0x06000FCF RID: 4047 RVA: 0x0003DAD0 File Offset: 0x0003BCD0
		public OdbcCommand(string cmdText)
			: this()
		{
			this.commandText = cmdText;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommand" /> class with the text of the query and an <see cref="T:System.Data.Odbc.OdbcConnection" /> object.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">An <see cref="T:System.Data.Odbc.OdbcConnection" /> object that represents the connection to a data source. </param>
		// Token: 0x06000FD0 RID: 4048 RVA: 0x0003DAE0 File Offset: 0x0003BCE0
		public OdbcCommand(string cmdText, OdbcConnection connection)
			: this(cmdText)
		{
			this.Connection = connection;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcCommand" /> class with the text of the query, an <see cref="T:System.Data.Odbc.OdbcConnection" /> object, and the <see cref="P:System.Data.Odbc.OdbcCommand.Transaction" />.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">An <see cref="T:System.Data.Odbc.OdbcConnection" /> object that represents the connection to a data source. </param>
		/// <param name="transaction">The transaction in which the <see cref="T:System.Data.Odbc.OdbcCommand" /> executes. </param>
		// Token: 0x06000FD1 RID: 4049 RVA: 0x0003DAF0 File Offset: 0x0003BCF0
		public OdbcCommand(string cmdText, OdbcConnection connection, OdbcTransaction transaction)
			: this(cmdText, connection)
		{
			this.Transaction = transaction;
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		// Token: 0x06000FD2 RID: 4050 RVA: 0x0003DB04 File Offset: 0x0003BD04
		object ICloneable.Clone()
		{
			OdbcCommand odbcCommand = new OdbcCommand();
			odbcCommand.CommandText = this.CommandText;
			odbcCommand.CommandTimeout = this.CommandTimeout;
			odbcCommand.CommandType = this.CommandType;
			odbcCommand.Connection = this.Connection;
			odbcCommand.DesignTimeVisible = this.DesignTimeVisible;
			foreach (object obj in this.Parameters)
			{
				OdbcParameter odbcParameter = (OdbcParameter)obj;
				odbcCommand.Parameters.Add(odbcParameter);
			}
			odbcCommand.Transaction = this.Transaction;
			return odbcCommand;
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x0003DBC8 File Offset: 0x0003BDC8
		internal IntPtr hStmt
		{
			get
			{
				return this.hstmt;
			}
		}

		/// <summary>Gets or sets the SQL statement or stored procedure to execute against the data source.</summary>
		/// <returns>The SQL statement or stored procedure to execute. The default value is an empty string ("").</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0003DBD0 File Offset: 0x0003BDD0
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x0003DBEC File Offset: 0x0003BDEC
		[DefaultValue("")]
		[OdbcDescription("Command text to execute")]
		[RefreshProperties(RefreshProperties.All)]
		[Editor("Microsoft.VSDesigner.Data.Odbc.Design.OdbcCommandTextEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[OdbcCategory("Data")]
		public override string CommandText
		{
			get
			{
				if (this.commandText == null)
				{
					return string.Empty;
				}
				return this.commandText;
			}
			set
			{
				this.prepared = false;
				this.commandText = value;
			}
		}

		/// <summary>Gets or sets the wait time before terminating an attempt to execute a command and generating an error.</summary>
		/// <returns>The time in seconds to wait for the command to execute. The default is 30 seconds.</returns>
		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0003DBFC File Offset: 0x0003BDFC
		// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x0003DC04 File Offset: 0x0003BE04
		[OdbcDescription("Time to wait for command to execute")]
		public override int CommandTimeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("The property value assigned is less than 0.", "CommandTimeout");
				}
				this.timeout = value;
			}
		}

		/// <summary>Gets or sets a value that indicates how the <see cref="P:System.Data.Odbc.OdbcCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <exception cref="T:System.ArgumentException">The value was not a valid <see cref="T:System.Data.CommandType" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0003DC24 File Offset: 0x0003BE24
		// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x0003DC2C File Offset: 0x0003BE2C
		[OdbcCategory("Data")]
		[RefreshProperties(RefreshProperties.All)]
		[OdbcDescription("How to interpret the CommandText")]
		[DefaultValue("Text")]
		public override CommandType CommandType
		{
			get
			{
				return this.commandType;
			}
			set
			{
				ExceptionHelper.CheckEnumValue(typeof(CommandType), value);
				this.commandType = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcConnection" /> used by this instance of the <see cref="T:System.Data.Odbc.OdbcCommand" />.</summary>
		/// <returns>The connection to a data source. The default is a null value.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.Odbc.OdbcCommand.Connection" /> property was changed while a transaction was in progress. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0003DC4C File Offset: 0x0003BE4C
		// (set) Token: 0x06000FDB RID: 4059 RVA: 0x0003DC5C File Offset: 0x0003BE5C
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DefaultValue(null)]
		public new OdbcConnection Connection
		{
			get
			{
				return this.DbConnection as OdbcConnection;
			}
			set
			{
				this.DbConnection = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the command object should be visible in a customized interface control.</summary>
		/// <returns>true, if the command object should be visible in a control; otherwise false. The default is true.</returns>
		// Token: 0x170002A2 RID: 674
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x0003DC68 File Offset: 0x0003BE68
		// (set) Token: 0x06000FDD RID: 4061 RVA: 0x0003DC70 File Offset: 0x0003BE70
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DefaultValue(true)]
		[DesignOnly(true)]
		public override bool DesignTimeVisible
		{
			get
			{
				return this.designTimeVisible;
			}
			set
			{
				this.designTimeVisible = value;
			}
		}

		/// <summary>Gets the <see cref="T:System.Data.Odbc.OdbcParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure. The default is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x0003DC7C File Offset: 0x0003BE7C
		[OdbcCategory("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[OdbcDescription("The parameters collection")]
		public new OdbcParameterCollection Parameters
		{
			get
			{
				return base.Parameters as OdbcParameterCollection;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.Odbc.OdbcTransaction" /> within which the <see cref="T:System.Data.Odbc.OdbcCommand" /> executes.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcTransaction" />. The default is a null value.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0003DC8C File Offset: 0x0003BE8C
		// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x0003DC94 File Offset: 0x0003BE94
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[OdbcDescription("The transaction used by the command")]
		[Browsable(false)]
		public new OdbcTransaction Transaction
		{
			get
			{
				return this.transaction;
			}
			set
			{
				this.transaction = value;
			}
		}

		/// <summary>Gets or sets a value that specifies how the Update method should apply command results to the DataRow.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values.</returns>
		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0003DCA0 File Offset: 0x0003BEA0
		// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x0003DCA8 File Offset: 0x0003BEA8
		[DefaultValue(UpdateRowSource.Both)]
		[OdbcDescription("When used by a DataAdapter.Update, how command results are applied to the current DataRow")]
		[OdbcCategory("Behavior")]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this.updateRowSource;
			}
			set
			{
				ExceptionHelper.CheckEnumValue(typeof(UpdateRowSource), value);
				this.updateRowSource = value;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000FE3 RID: 4067 RVA: 0x0003DCC8 File Offset: 0x0003BEC8
		// (set) Token: 0x06000FE4 RID: 4068 RVA: 0x0003DCD0 File Offset: 0x0003BED0
		protected override DbConnection DbConnection
		{
			get
			{
				return this.connection;
			}
			set
			{
				this.connection = (OdbcConnection)value;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000FE5 RID: 4069 RVA: 0x0003DCE0 File Offset: 0x0003BEE0
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000FE6 RID: 4070 RVA: 0x0003DCE8 File Offset: 0x0003BEE8
		// (set) Token: 0x06000FE7 RID: 4071 RVA: 0x0003DCF0 File Offset: 0x0003BEF0
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.transaction;
			}
			set
			{
				this.transaction = (OdbcTransaction)value;
			}
		}

		/// <summary>Tries to cancel the execution of an <see cref="T:System.Data.Odbc.OdbcCommand" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FE8 RID: 4072 RVA: 0x0003DD00 File Offset: 0x0003BF00
		public override void Cancel()
		{
			if (!(this.hstmt != IntPtr.Zero))
			{
				throw new InvalidOperationException();
			}
			OdbcReturn odbcReturn = libodbc.SQLCancel(this.hstmt);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
			}
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0003DD5C File Offset: 0x0003BF5C
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		/// <summary>Creates a new instance of an <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcParameter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000FEA RID: 4074 RVA: 0x0003DD64 File Offset: 0x0003BF64
		public new OdbcParameter CreateParameter()
		{
			return new OdbcParameter();
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0003DD6C File Offset: 0x0003BF6C
		internal void Unlink()
		{
			if (this.disposed)
			{
				return;
			}
			this.FreeStatement(false);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x0003DD84 File Offset: 0x0003BF84
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.FreeStatement();
			this.CommandText = null;
			this.Connection = null;
			this.Transaction = null;
			this.Parameters.Clear();
			this.disposed = true;
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x0003DDCC File Offset: 0x0003BFCC
		private IntPtr ReAllocStatment()
		{
			if (this.hstmt != IntPtr.Zero)
			{
				this.FreeStatement();
			}
			else
			{
				this.Connection.Link(this);
			}
			OdbcReturn odbcReturn = libodbc.SQLAllocHandle(OdbcHandleType.Stmt, this.Connection.hDbc, ref this.hstmt);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.connection.CreateOdbcException(OdbcHandleType.Dbc, this.Connection.hDbc);
			}
			this.disposed = false;
			return this.hstmt;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x0003DE50 File Offset: 0x0003C050
		private void FreeStatement()
		{
			this.FreeStatement(true);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x0003DE5C File Offset: 0x0003C05C
		private void FreeStatement(bool unlink)
		{
			this.prepared = false;
			if (this.hstmt == IntPtr.Zero)
			{
				return;
			}
			if (unlink)
			{
				this.Connection.Unlink(this);
			}
			OdbcReturn odbcReturn = libodbc.SQLFreeStmt(this.hstmt, libodbc.SQLFreeStmtOptions.Close);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
			}
			odbcReturn = libodbc.SQLFreeHandle(3, this.hstmt);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
			}
			this.hstmt = IntPtr.Zero;
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003DF00 File Offset: 0x0003C100
		private void ExecSQL(CommandBehavior behavior, bool createReader, string sql)
		{
			if (!this.prepared && this.Parameters.Count == 0)
			{
				this.ReAllocStatment();
				OdbcReturn odbcReturn = libodbc.SQLExecDirect(this.hstmt, sql, -3);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo && odbcReturn != OdbcReturn.NoData)
				{
					throw this.connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
				}
				return;
			}
			else
			{
				if (!this.prepared)
				{
					this.Prepare();
				}
				this.BindParameters();
				OdbcReturn odbcReturn = libodbc.SQLExecute(this.hstmt);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
				}
				return;
			}
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003DFAC File Offset: 0x0003C1AC
		internal void FreeIfNotPrepared()
		{
			if (!this.prepared)
			{
				this.FreeStatement();
			}
		}

		/// <summary>Executes an SQL statement against the <see cref="P:System.Data.Odbc.OdbcCommand.Connection" /> and returns the number of rows affected.</summary>
		/// <returns>For UPDATE, INSERT, and DELETE statements, the return value is the number of rows affected by the command. For all other types of statements, the return value is -1.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection does not exist.-or- The connection is not open. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FF2 RID: 4082 RVA: 0x0003DFC0 File Offset: 0x0003C1C0
		public override int ExecuteNonQuery()
		{
			return this.ExecuteNonQuery("ExecuteNonQuery", CommandBehavior.Default, false);
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x0003DFD0 File Offset: 0x0003C1D0
		private int ExecuteNonQuery(string method, CommandBehavior behavior, bool createReader)
		{
			if (this.Connection == null)
			{
				throw new InvalidOperationException(string.Format("{0}: Connection is not set.", method));
			}
			if (this.Connection.State == ConnectionState.Closed)
			{
				throw new InvalidOperationException(string.Format("{0}: Connection state is closed", method));
			}
			if (this.CommandText.Length == 0)
			{
				throw new InvalidOperationException(string.Format("{0}: CommandText is not set.", method));
			}
			this.ExecSQL(behavior, createReader, this.CommandText);
			int num2;
			if (this.CommandText.ToUpper().IndexOf("UPDATE") != -1 || this.CommandText.ToUpper().IndexOf("INSERT") != -1 || this.CommandText.ToUpper().IndexOf("DELETE") != -1)
			{
				int num = 0;
				OdbcReturn odbcReturn = libodbc.SQLRowCount(this.hstmt, ref num);
				num2 = num;
			}
			else
			{
				num2 = -1;
			}
			if (!createReader && !this.prepared)
			{
				this.FreeStatement();
			}
			return num2;
		}

		/// <summary>Creates a prepared or compiled version of the command at the data source.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.Odbc.OdbcCommand.Connection" /> is not set.-or- The <see cref="P:System.Data.Odbc.OdbcCommand.Connection" /> is not <see cref="!:System.Data.Odbc.OdbcConnection.Open" />. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FF4 RID: 4084 RVA: 0x0003E0D0 File Offset: 0x0003C2D0
		public override void Prepare()
		{
			this.ReAllocStatment();
			OdbcReturn odbcReturn = libodbc.SQLPrepare(this.hstmt, this.CommandText, this.CommandText.Length);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.connection.CreateOdbcException(OdbcHandleType.Stmt, this.hstmt);
			}
			this.prepared = true;
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0003E128 File Offset: 0x0003C328
		private void BindParameters()
		{
			int num = 1;
			foreach (object obj in this.Parameters)
			{
				OdbcParameter odbcParameter = (OdbcParameter)obj;
				odbcParameter.Bind(this, this.hstmt, num);
				odbcParameter.CopyValue();
				num++;
			}
		}

		/// <summary>Sends the <see cref="P:System.Data.Odbc.OdbcCommand.CommandText" /> to the <see cref="P:System.Data.Odbc.OdbcCommand.Connection" /> and builds an <see cref="T:System.Data.Odbc.OdbcDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcDataReader" /> object.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FF6 RID: 4086 RVA: 0x0003E1AC File Offset: 0x0003C3AC
		public new OdbcDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0003E1B8 File Offset: 0x0003C3B8
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		/// <summary>Sends the <see cref="P:System.Data.Odbc.OdbcCommand.CommandText" /> to the <see cref="P:System.Data.Odbc.OdbcCommand.Connection" />, and builds an <see cref="T:System.Data.Odbc.OdbcDataReader" /> using one of the CommandBehavior values.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcDataReader" /> object.</returns>
		/// <param name="behavior">One of the System.Data.CommandBehavior values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FF8 RID: 4088 RVA: 0x0003E1C4 File Offset: 0x0003C3C4
		public new OdbcDataReader ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteReader("ExecuteReader", behavior);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0003E1D4 File Offset: 0x0003C3D4
		private OdbcDataReader ExecuteReader(string method, CommandBehavior behavior)
		{
			int num = this.ExecuteNonQuery(method, behavior, true);
			return new OdbcDataReader(this, behavior, num);
		}

		/// <summary>Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000FFA RID: 4090 RVA: 0x0003E1F8 File Offset: 0x0003C3F8
		public override object ExecuteScalar()
		{
			object obj = null;
			OdbcDataReader odbcDataReader = this.ExecuteReader("ExecuteScalar", CommandBehavior.Default);
			try
			{
				if (odbcDataReader.Read())
				{
					obj = odbcDataReader[0];
				}
			}
			finally
			{
				odbcDataReader.Close();
			}
			return obj;
		}

		/// <summary>Resets the <see cref="P:System.Data.Odbc.OdbcCommand.CommandTimeout" /> property to the default value.</summary>
		// Token: 0x06000FFB RID: 4091 RVA: 0x0003E250 File Offset: 0x0003C450
		public void ResetCommandTimeout()
		{
			this.CommandTimeout = 30;
		}

		// Token: 0x04000547 RID: 1351
		private const int DEFAULT_COMMAND_TIMEOUT = 30;

		// Token: 0x04000548 RID: 1352
		private string commandText;

		// Token: 0x04000549 RID: 1353
		private int timeout;

		// Token: 0x0400054A RID: 1354
		private CommandType commandType;

		// Token: 0x0400054B RID: 1355
		private UpdateRowSource updateRowSource;

		// Token: 0x0400054C RID: 1356
		private OdbcConnection connection;

		// Token: 0x0400054D RID: 1357
		private OdbcTransaction transaction;

		// Token: 0x0400054E RID: 1358
		private OdbcParameterCollection _parameters;

		// Token: 0x0400054F RID: 1359
		private bool designTimeVisible;

		// Token: 0x04000550 RID: 1360
		private bool prepared;

		// Token: 0x04000551 RID: 1361
		private IntPtr hstmt = IntPtr.Zero;

		// Token: 0x04000552 RID: 1362
		private bool disposed;
	}
}
