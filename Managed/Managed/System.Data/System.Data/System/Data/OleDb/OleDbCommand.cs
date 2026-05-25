using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Runtime.InteropServices;

namespace System.Data.OleDb
{
	/// <summary>Represents an SQL statement or stored procedure to execute against a data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000EA RID: 234
	[Designer("Microsoft.VSDesigner.Data.VS.OleDbCommandDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[DefaultEvent("RecordsAffected")]
	[ToolboxItem("System.Drawing.Design.ToolboxItem, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public sealed class OleDbCommand : DbCommand, IDisposable, IDbCommand, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class.</summary>
		// Token: 0x06000B03 RID: 2819 RVA: 0x000316B4 File Offset: 0x0002F8B4
		public OleDbCommand()
		{
			this.timeout = 30;
			this.commandType = CommandType.Text;
			this.parameters = new OleDbParameterCollection();
			this.behavior = CommandBehavior.Default;
			this.gdaCommand = IntPtr.Zero;
			this.designTimeVisible = true;
			this.updatedRowSource = UpdateRowSource.Both;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class with the text of the query.</summary>
		/// <param name="cmdText">The text of the query. </param>
		// Token: 0x06000B04 RID: 2820 RVA: 0x00031704 File Offset: 0x0002F904
		public OleDbCommand(string cmdText)
			: this()
		{
			this.CommandText = cmdText;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class with the text of the query and an <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">An <see cref="T:System.Data.OleDb.OleDbConnection" /> that represents the connection to a data source. </param>
		// Token: 0x06000B05 RID: 2821 RVA: 0x00031714 File Offset: 0x0002F914
		public OleDbCommand(string cmdText, OleDbConnection connection)
			: this(cmdText)
		{
			this.Connection = connection;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbCommand" /> class with the text of the query, an <see cref="T:System.Data.OleDb.OleDbConnection" />, and the <see cref="P:System.Data.OleDb.OleDbCommand.Transaction" />.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">An <see cref="T:System.Data.OleDb.OleDbConnection" /> that represents the connection to a data source. </param>
		/// <param name="transaction">The transaction in which the <see cref="T:System.Data.OleDb.OleDbCommand" /> executes. </param>
		// Token: 0x06000B06 RID: 2822 RVA: 0x00031724 File Offset: 0x0002F924
		public OleDbCommand(string cmdText, OleDbConnection connection, OleDbTransaction transaction)
			: this(cmdText, connection)
		{
			this.transaction = transaction;
		}

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000B07 RID: 2823 RVA: 0x00031738 File Offset: 0x0002F938
		// (set) Token: 0x06000B08 RID: 2824 RVA: 0x00031740 File Offset: 0x0002F940
		IDbConnection IDbCommand.Connection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (OleDbConnection)value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000B09 RID: 2825 RVA: 0x00031750 File Offset: 0x0002F950
		IDataParameterCollection IDbCommand.Parameters
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000B0A RID: 2826 RVA: 0x00031758 File Offset: 0x0002F958
		// (set) Token: 0x06000B0B RID: 2827 RVA: 0x00031760 File Offset: 0x0002F960
		IDbTransaction IDbCommand.Transaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (OleDbTransaction)value;
			}
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbCommand.ExecuteReader" />.</summary>
		// Token: 0x06000B0C RID: 2828 RVA: 0x00031770 File Offset: 0x0002F970
		IDataReader IDbCommand.ExecuteReader()
		{
			return this.ExecuteReader();
		}

		/// <summary>For a description of this member, see <see cref="M:System.Data.IDbCommand.ExecuteReader" />.</summary>
		// Token: 0x06000B0D RID: 2829 RVA: 0x00031778 File Offset: 0x0002F978
		IDataReader IDbCommand.ExecuteReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		// Token: 0x06000B0E RID: 2830 RVA: 0x00031784 File Offset: 0x0002F984
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		/// <summary>Gets or sets the SQL statement or stored procedure to execute at the data source.</summary>
		/// <returns>The SQL statement or stored procedure to execute. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000B0F RID: 2831 RVA: 0x0003178C File Offset: 0x0002F98C
		// (set) Token: 0x06000B10 RID: 2832 RVA: 0x000317A8 File Offset: 0x0002F9A8
		[DataCategory("Data")]
		[RefreshProperties(RefreshProperties.All)]
		[DefaultValue("")]
		[Editor("Microsoft.VSDesigner.Data.ADO.Design.OleDbCommandTextEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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
				this.commandText = value;
			}
		}

		/// <summary>Gets or sets the wait time before terminating an attempt to execute a command and generating an error.</summary>
		/// <returns>The time (in seconds) to wait for the command to execute. The default is 30 seconds.</returns>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000B11 RID: 2833 RVA: 0x000317B4 File Offset: 0x0002F9B4
		// (set) Token: 0x06000B12 RID: 2834 RVA: 0x000317BC File Offset: 0x0002F9BC
		public override int CommandTimeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		/// <summary>Gets or sets a value that indicates how the <see cref="P:System.Data.OleDb.OleDbCommand.CommandText" /> property is interpreted.</summary>
		/// <returns>One of the <see cref="P:System.Data.OleDb.OleDbCommand.CommandType" /> values. The default is Text.</returns>
		/// <exception cref="T:System.ArgumentException">The value was not a valid <see cref="P:System.Data.OleDb.OleDbCommand.CommandType" />.</exception>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000B13 RID: 2835 RVA: 0x000317C8 File Offset: 0x0002F9C8
		// (set) Token: 0x06000B14 RID: 2836 RVA: 0x000317D0 File Offset: 0x0002F9D0
		[DefaultValue("Text")]
		[DataCategory("Data")]
		[RefreshProperties(RefreshProperties.All)]
		public override CommandType CommandType
		{
			get
			{
				return this.commandType;
			}
			set
			{
				this.commandType = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbConnection" /> used by this instance of the <see cref="T:System.Data.OleDb.OleDbCommand" />.</summary>
		/// <returns>The connection to a data source. The default value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> property was changed while a transaction was in progress. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000B15 RID: 2837 RVA: 0x000317DC File Offset: 0x0002F9DC
		// (set) Token: 0x06000B16 RID: 2838 RVA: 0x000317E4 File Offset: 0x0002F9E4
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[DataCategory("Behavior")]
		[DefaultValue(null)]
		public new OleDbConnection Connection
		{
			get
			{
				return this.connection;
			}
			set
			{
				this.connection = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the command object should be visible in a customized Windows Forms Designer control.</summary>
		/// <returns>A value that indicates whether the command object should be visible in a control. The default is true.</returns>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000B17 RID: 2839 RVA: 0x000317F0 File Offset: 0x0002F9F0
		// (set) Token: 0x06000B18 RID: 2840 RVA: 0x000317F8 File Offset: 0x0002F9F8
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Browsable(false)]
		[DesignOnly(true)]
		[DefaultValue(true)]
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

		/// <summary>Gets the <see cref="T:System.Data.OleDb.OleDbParameterCollection" />.</summary>
		/// <returns>The parameters of the SQL statement or stored procedure. The default is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000B19 RID: 2841 RVA: 0x00031804 File Offset: 0x0002FA04
		// (set) Token: 0x06000B1A RID: 2842 RVA: 0x0003180C File Offset: 0x0002FA0C
		[DataCategory("Data")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new OleDbParameterCollection Parameters
		{
			get
			{
				return this.parameters;
			}
			internal set
			{
				this.parameters = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.OleDb.OleDbTransaction" /> within which the <see cref="T:System.Data.OleDb.OleDbCommand" /> executes.</summary>
		/// <returns>The <see cref="T:System.Data.OleDb.OleDbTransaction" />. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000B1B RID: 2843 RVA: 0x00031818 File Offset: 0x0002FA18
		// (set) Token: 0x06000B1C RID: 2844 RVA: 0x00031820 File Offset: 0x0002FA20
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new OleDbTransaction Transaction
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

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the Update method of the <see cref="T:System.Data.OleDb.OleDbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values.</returns>
		/// <exception cref="T:System.ArgumentException">The value entered was not one of the <see cref="T:System.Data.UpdateRowSource" /> values.</exception>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x0003182C File Offset: 0x0002FA2C
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00031834 File Offset: 0x0002FA34
		[DataCategory("Behavior")]
		[MonoTODO]
		[DefaultValue(UpdateRowSource.Both)]
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this.updatedRowSource;
			}
			set
			{
				ExceptionHelper.CheckEnumValue(typeof(UpdateRowSource), value);
				this.updatedRowSource = value;
			}
		}

		/// <summary>Tries to cancel the execution of an <see cref="T:System.Data.OleDb.OleDbCommand" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B1F RID: 2847 RVA: 0x00031854 File Offset: 0x0002FA54
		[MonoTODO]
		public override void Cancel()
		{
			throw new NotImplementedException();
		}

		/// <summary>Creates a new instance of an <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbParameter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B20 RID: 2848 RVA: 0x0003185C File Offset: 0x0002FA5C
		public new OleDbParameter CreateParameter()
		{
			return new OleDbParameter();
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x00031864 File Offset: 0x0002FA64
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			this.Connection = null;
			this.Transaction = null;
			this.disposed = true;
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x00031888 File Offset: 0x0002FA88
		private void SetupGdaCommand()
		{
			CommandType commandType = this.commandType;
			GdaCommandType gdaCommandType;
			switch (commandType)
			{
			case CommandType.Text:
				break;
			default:
				if (commandType == CommandType.TableDirect)
				{
					gdaCommandType = GdaCommandType.Table;
					goto IL_0044;
				}
				break;
			case CommandType.StoredProcedure:
				gdaCommandType = GdaCommandType.Procedure;
				goto IL_0044;
			}
			gdaCommandType = GdaCommandType.Sql;
			IL_0044:
			if (this.gdaCommand != IntPtr.Zero)
			{
				libgda.gda_command_set_text(this.gdaCommand, this.CommandText);
				libgda.gda_command_set_command_type(this.gdaCommand, gdaCommandType);
			}
			else
			{
				this.gdaCommand = libgda.gda_command_new(this.CommandText, gdaCommandType, (GdaCommandOptions)0);
			}
		}

		/// <summary>Executes an SQL statement against the <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> and returns the number of rows affected.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection does not exist.-or- The connection is not open.-or- Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeSubWindows" />
		///   <IPermission class="System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B23 RID: 2851 RVA: 0x00031924 File Offset: 0x0002FB24
		public override int ExecuteNonQuery()
		{
			if (this.connection == null)
			{
				throw new InvalidOperationException("connection == null");
			}
			if (this.connection.State == ConnectionState.Closed)
			{
				throw new InvalidOperationException("State == Closed");
			}
			IntPtr gdaConnection = this.connection.GdaConnection;
			IntPtr gdaParameterList = this.parameters.GdaParameterList;
			this.SetupGdaCommand();
			return libgda.gda_connection_execute_non_query(gdaConnection, this.gdaCommand, gdaParameterList);
		}

		/// <summary>Sends the <see cref="P:System.Data.OleDb.OleDbCommand.CommandText" /> to the <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> and builds an <see cref="T:System.Data.OleDb.OleDbDataReader" />.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataReader" /> object.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeSubWindows" />
		///   <IPermission class="System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B24 RID: 2852 RVA: 0x00031990 File Offset: 0x0002FB90
		public new OleDbDataReader ExecuteReader()
		{
			return this.ExecuteReader(this.behavior);
		}

		/// <summary>Sends the <see cref="P:System.Data.OleDb.OleDbCommand.CommandText" /> to the <see cref="P:System.Data.OleDb.OleDbCommand.Connection" />, and builds an <see cref="T:System.Data.OleDb.OleDbDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		/// <exception cref="T:System.InvalidOperationException">Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeSubWindows" />
		///   <IPermission class="System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B25 RID: 2853 RVA: 0x000319A0 File Offset: 0x0002FBA0
		public new OleDbDataReader ExecuteReader(CommandBehavior behavior)
		{
			ArrayList arrayList = new ArrayList();
			if (this.connection.State != ConnectionState.Open)
			{
				throw new InvalidOperationException("State != Open");
			}
			this.behavior = behavior;
			IntPtr gdaConnection = this.connection.GdaConnection;
			IntPtr gdaParameterList = this.parameters.GdaParameterList;
			this.SetupGdaCommand();
			IntPtr intPtr = libgda.gda_connection_execute_command(gdaConnection, this.gdaCommand, gdaParameterList);
			if (intPtr != IntPtr.Zero)
			{
				for (GdaList gdaList = (GdaList)Marshal.PtrToStructure(intPtr, typeof(GdaList)); gdaList != null; gdaList = (GdaList)Marshal.PtrToStructure(gdaList.next, typeof(GdaList)))
				{
					arrayList.Add(gdaList.data);
					if (gdaList.next == IntPtr.Zero)
					{
						break;
					}
				}
				this.dataReader = new OleDbDataReader(this, arrayList);
				this.dataReader.NextResult();
			}
			return this.dataReader;
		}

		/// <summary>Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set, or a null reference if the result set is empty.</returns>
		/// <exception cref="T:System.InvalidOperationException">Cannot execute a command within a transaction context that differs from the context in which the connection was originally enlisted. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeSubWindows" />
		///   <IPermission class="System.Data.OleDb.OleDbPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B26 RID: 2854 RVA: 0x00031A9C File Offset: 0x0002FC9C
		public override object ExecuteScalar()
		{
			this.SetupGdaCommand();
			OleDbDataReader oleDbDataReader = this.ExecuteReader();
			if (oleDbDataReader == null)
			{
				return null;
			}
			if (!oleDbDataReader.Read())
			{
				oleDbDataReader.Close();
				return null;
			}
			object value = oleDbDataReader.GetValue(0);
			oleDbDataReader.Close();
			return value;
		}

		/// <summary>Creates a new <see cref="T:System.Data.OleDb.OleDbCommand" /> object that is a copy of the current instance.</summary>
		/// <returns>A new <see cref="T:System.Data.OleDb.OleDbCommand" /> object that is a copy of this instance.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06000B27 RID: 2855 RVA: 0x00031AE0 File Offset: 0x0002FCE0
		public OleDbCommand Clone()
		{
			return new OleDbCommand
			{
				CommandText = this.CommandText,
				CommandTimeout = this.CommandTimeout,
				CommandType = this.CommandType,
				Connection = this.Connection,
				DesignTimeVisible = this.DesignTimeVisible,
				Parameters = this.Parameters,
				Transaction = this.Transaction
			};
		}

		/// <summary>Creates a prepared (or compiled) version of the command on the data source.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not set.-or- The <see cref="P:System.Data.OleDb.OleDbCommand.Connection" /> is not open. </exception>
		/// <exception cref="T:System.Data.OleDb.OleDbException">
		///   <see cref="M:System.Data.OleDb.OleDbCommand.Prepare" /> is called before initializing the <see cref="P:System.Data.OleDb.OleDbCommand.CommandText" /> property.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B28 RID: 2856 RVA: 0x00031B48 File Offset: 0x0002FD48
		[MonoTODO]
		public override void Prepare()
		{
			throw new NotImplementedException();
		}

		/// <summary>Resets the <see cref="P:System.Data.OleDb.OleDbCommand.CommandTimeout" /> property to the default value.</summary>
		// Token: 0x06000B29 RID: 2857 RVA: 0x00031B50 File Offset: 0x0002FD50
		public void ResetCommandTimeout()
		{
			this.timeout = 30;
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x00031B5C File Offset: 0x0002FD5C
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x00031B64 File Offset: 0x0002FD64
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x00031B70 File Offset: 0x0002FD70
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x00031B78 File Offset: 0x0002FD78
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (OleDbConnection)value;
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x00031B88 File Offset: 0x0002FD88
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000B2F RID: 2863 RVA: 0x00031B90 File Offset: 0x0002FD90
		// (set) Token: 0x06000B30 RID: 2864 RVA: 0x00031B98 File Offset: 0x0002FD98
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (OleDbTransaction)value;
			}
		}

		// Token: 0x04000419 RID: 1049
		private const int DEFAULT_COMMAND_TIMEOUT = 30;

		// Token: 0x0400041A RID: 1050
		private string commandText;

		// Token: 0x0400041B RID: 1051
		private int timeout;

		// Token: 0x0400041C RID: 1052
		private CommandType commandType;

		// Token: 0x0400041D RID: 1053
		private OleDbConnection connection;

		// Token: 0x0400041E RID: 1054
		private OleDbParameterCollection parameters;

		// Token: 0x0400041F RID: 1055
		private OleDbTransaction transaction;

		// Token: 0x04000420 RID: 1056
		private bool designTimeVisible;

		// Token: 0x04000421 RID: 1057
		private OleDbDataReader dataReader;

		// Token: 0x04000422 RID: 1058
		private CommandBehavior behavior;

		// Token: 0x04000423 RID: 1059
		private IntPtr gdaCommand;

		// Token: 0x04000424 RID: 1060
		private UpdateRowSource updatedRowSource;

		// Token: 0x04000425 RID: 1061
		private bool disposed;
	}
}
