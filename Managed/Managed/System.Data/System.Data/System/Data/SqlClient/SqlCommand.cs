using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Sql;
using System.Text;
using System.Xml;
using Mono.Data.Tds;
using Mono.Data.Tds.Protocol;

namespace System.Data.SqlClient
{
	/// <summary>Represents a Transact-SQL statement or stored procedure to execute against a SQL Server database. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200015C RID: 348
	[Designer("Microsoft.VSDesigner.Data.VS.SqlCommandDesigner, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.ComponentModel.Design.IDesigner")]
	[ToolboxItem("System.Drawing.Design.ToolboxItem, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("RecordsAffected")]
	public sealed class SqlCommand : DbCommand, IDisposable, IDbCommand, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommand" /> class.</summary>
		// Token: 0x06001200 RID: 4608 RVA: 0x00046154 File Offset: 0x00044354
		public SqlCommand()
			: this(string.Empty, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommand" /> class with the text of the query.</summary>
		/// <param name="cmdText">The text of the query. </param>
		// Token: 0x06001201 RID: 4609 RVA: 0x00046164 File Offset: 0x00044364
		public SqlCommand(string cmdText)
			: this(cmdText, null, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommand" /> class with the text of the query and a <see cref="T:System.Data.SqlClient.SqlConnection" />.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">A <see cref="T:System.Data.SqlClient.SqlConnection" /> that represents the connection to an instance of SQL Server. </param>
		// Token: 0x06001202 RID: 4610 RVA: 0x00046170 File Offset: 0x00044370
		public SqlCommand(string cmdText, SqlConnection connection)
			: this(cmdText, connection, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlCommand" /> class with the text of the query, a <see cref="T:System.Data.SqlClient.SqlConnection" />, and the <see cref="T:System.Data.SqlClient.SqlTransaction" />.</summary>
		/// <param name="cmdText">The text of the query. </param>
		/// <param name="connection">A <see cref="T:System.Data.SqlClient.SqlConnection" /> that represents the connection to an instance of SQL Server. </param>
		/// <param name="transaction">The <see cref="T:System.Data.SqlClient.SqlTransaction" /> in which the <see cref="T:System.Data.SqlClient.SqlCommand" /> executes. </param>
		// Token: 0x06001203 RID: 4611 RVA: 0x0004617C File Offset: 0x0004437C
		public SqlCommand(string cmdText, SqlConnection connection, SqlTransaction transaction)
		{
			this.commandText = cmdText;
			this.connection = connection;
			this.transaction = transaction;
			this.commandType = CommandType.Text;
			this.updatedRowSource = UpdateRowSource.Both;
			this.commandTimeout = 30;
			this.notificationAutoEnlist = true;
			this.designTimeVisible = true;
			this.parameters = new SqlParameterCollection(this);
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x000461D4 File Offset: 0x000443D4
		private SqlCommand(string commandText, SqlConnection connection, SqlTransaction transaction, CommandType commandType, UpdateRowSource updatedRowSource, bool designTimeVisible, int commandTimeout, SqlParameterCollection parameters)
		{
			this.commandText = commandText;
			this.connection = connection;
			this.transaction = transaction;
			this.commandType = commandType;
			this.updatedRowSource = updatedRowSource;
			this.designTimeVisible = designTimeVisible;
			this.commandTimeout = commandTimeout;
			this.parameters = new SqlParameterCollection(this);
			for (int i = 0; i < parameters.Count; i++)
			{
				this.parameters.Add(((ICloneable)parameters[i]).Clone());
			}
		}

		/// <summary>Occurs when the execution of a Transact-SQL statement completes.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x14000022 RID: 34
		// (add) Token: 0x06001205 RID: 4613 RVA: 0x0004625C File Offset: 0x0004445C
		// (remove) Token: 0x06001206 RID: 4614 RVA: 0x00046278 File Offset: 0x00044478
		public event StatementCompletedEventHandler StatementCompleted;

		// Token: 0x06001207 RID: 4615 RVA: 0x00046294 File Offset: 0x00044494
		object ICloneable.Clone()
		{
			return new SqlCommand(this.commandText, this.connection, this.transaction, this.commandType, this.updatedRowSource, this.designTimeVisible, this.commandTimeout, this.parameters);
		}

		// Token: 0x1700032B RID: 811
		// (get) Token: 0x06001208 RID: 4616 RVA: 0x000462D8 File Offset: 0x000444D8
		internal CommandBehavior CommandBehavior
		{
			get
			{
				return this.behavior;
			}
		}

		/// <summary>Gets or sets the Transact-SQL statement, table name or stored procedure to execute at the data source.</summary>
		/// <returns>The Transact-SQL statement or stored procedure to execute. The default is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x000462E0 File Offset: 0x000444E0
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x000462FC File Offset: 0x000444FC
		[DefaultValue("")]
		[Editor("Microsoft.VSDesigner.Data.SQL.Design.SqlCommandTextEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RefreshProperties(RefreshProperties.All)]
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
				if (value != this.commandText && this.preparedStatement != null)
				{
					this.Unprepare();
				}
				this.commandText = value;
			}
		}

		/// <summary>Gets or sets the wait time before terminating the attempt to execute a command and generating an error.</summary>
		/// <returns>The time in seconds to wait for the command to execute. The default is 30 seconds.</returns>
		// Token: 0x1700032D RID: 813
		// (get) Token: 0x0600120B RID: 4619 RVA: 0x00046328 File Offset: 0x00044528
		// (set) Token: 0x0600120C RID: 4620 RVA: 0x00046330 File Offset: 0x00044530
		public override int CommandTimeout
		{
			get
			{
				return this.commandTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("The property value assigned is less than 0.", "CommandTimeout");
				}
				this.commandTimeout = value;
			}
		}

		/// <summary>Gets or sets a value indicating how the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> property is to be interpreted.</summary>
		/// <returns>One of the <see cref="T:System.Data.CommandType" /> values. The default is Text.</returns>
		/// <exception cref="T:System.ArgumentException">The value was not a valid <see cref="T:System.Data.CommandType" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600120D RID: 4621 RVA: 0x00046350 File Offset: 0x00044550
		// (set) Token: 0x0600120E RID: 4622 RVA: 0x00046358 File Offset: 0x00044558
		[DefaultValue(CommandType.Text)]
		[RefreshProperties(RefreshProperties.All)]
		public override CommandType CommandType
		{
			get
			{
				return this.commandType;
			}
			set
			{
				if (value == CommandType.TableDirect)
				{
					throw new ArgumentOutOfRangeException("CommandType.TableDirect is not supported by the Mono SqlClient Data Provider.");
				}
				ExceptionHelper.CheckEnumValue(typeof(CommandType), value);
				this.commandType = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlConnection" /> used by this instance of the <see cref="T:System.Data.SqlClient.SqlCommand" />.</summary>
		/// <returns>The connection to a data source. The default value is null.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> property was changed while the command was enlisted in a transaction.. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x00046398 File Offset: 0x00044598
		// (set) Token: 0x06001210 RID: 4624 RVA: 0x000463A0 File Offset: 0x000445A0
		[DefaultValue(null)]
		[Editor("Microsoft.VSDesigner.Data.Design.DbConnectionEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public new SqlConnection Connection
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

		/// <summary>Gets or sets a value indicating whether the command object should be visible in a Windows Form Designer control.</summary>
		/// <returns>A value indicating whether the command object should be visible in a control. The default is true.</returns>
		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06001211 RID: 4625 RVA: 0x000463AC File Offset: 0x000445AC
		// (set) Token: 0x06001212 RID: 4626 RVA: 0x000463B4 File Offset: 0x000445B4
		[DefaultValue(true)]
		[Browsable(false)]
		[DesignOnly(true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		/// <summary>Gets the <see cref="T:System.Data.SqlClient.SqlParameterCollection" />.</summary>
		/// <returns>The parameters of the Transact-SQL statement or stored procedure. The default is an empty collection.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x000463C0 File Offset: 0x000445C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public new SqlParameterCollection Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x000463C8 File Offset: 0x000445C8
		internal Tds Tds
		{
			get
			{
				return this.Connection.Tds;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Data.SqlClient.SqlTransaction" /> within which the <see cref="T:System.Data.SqlClient.SqlCommand" /> executes.</summary>
		/// <returns>The <see cref="T:System.Data.SqlClient.SqlTransaction" />. The default value is null.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000333 RID: 819
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x000463D8 File Offset: 0x000445D8
		// (set) Token: 0x06001216 RID: 4630 RVA: 0x00046410 File Offset: 0x00044610
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public new SqlTransaction Transaction
		{
			get
			{
				if (this.transaction != null && !this.transaction.IsOpen)
				{
					this.transaction = null;
				}
				return this.transaction;
			}
			set
			{
				this.transaction = value;
			}
		}

		/// <summary>Gets or sets how command results are applied to the <see cref="T:System.Data.DataRow" /> when used by the Update method of the <see cref="T:System.Data.Common.DbDataAdapter" />.</summary>
		/// <returns>One of the <see cref="T:System.Data.UpdateRowSource" /> values.</returns>
		// Token: 0x17000334 RID: 820
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0004641C File Offset: 0x0004461C
		// (set) Token: 0x06001218 RID: 4632 RVA: 0x00046424 File Offset: 0x00044624
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

		/// <summary>Gets or sets a value that specifies the <see cref="T:System.Data.Sql.SqlNotificationRequest" /> object bound to this command.</summary>
		/// <returns>When set to null (default), no notification should be requested.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000335 RID: 821
		// (get) Token: 0x06001219 RID: 4633 RVA: 0x00046444 File Offset: 0x00044644
		// (set) Token: 0x0600121A RID: 4634 RVA: 0x0004644C File Offset: 0x0004464C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SqlNotificationRequest Notification
		{
			get
			{
				return this.notification;
			}
			set
			{
				this.notification = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the application should automatically receive query notifications from a common <see cref="T:System.Data.SqlClient.SqlDependency" /> object.</summary>
		/// <returns>true if the application should automatically receive query notifications; otherwise false. The default value is true.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000336 RID: 822
		// (get) Token: 0x0600121B RID: 4635 RVA: 0x00046458 File Offset: 0x00044658
		// (set) Token: 0x0600121C RID: 4636 RVA: 0x00046460 File Offset: 0x00044660
		[DefaultValue(true)]
		public bool NotificationAutoEnlist
		{
			get
			{
				return this.notificationAutoEnlist;
			}
			set
			{
				this.notificationAutoEnlist = value;
			}
		}

		/// <summary>Tries to cancel the execution of a <see cref="T:System.Data.SqlClient.SqlCommand" />.</summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600121D RID: 4637 RVA: 0x0004646C File Offset: 0x0004466C
		public override void Cancel()
		{
			if (this.Connection == null || this.Connection.Tds == null)
			{
				return;
			}
			this.Connection.Tds.Cancel();
		}

		/// <summary>Creates a new <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is a copy of the current instance.</summary>
		/// <returns>A new <see cref="T:System.Data.SqlClient.SqlCommand" /> object that is a copy of this instance.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x0600121E RID: 4638 RVA: 0x000464A8 File Offset: 0x000446A8
		public SqlCommand Clone()
		{
			return new SqlCommand(this.commandText, this.connection, this.transaction, this.commandType, this.updatedRowSource, this.designTimeVisible, this.commandTimeout, this.parameters);
		}

		// Token: 0x0600121F RID: 4639 RVA: 0x000464EC File Offset: 0x000446EC
		internal void CloseDataReader()
		{
			if (this.Connection != null)
			{
				this.Connection.DataReader = null;
				if ((this.behavior & CommandBehavior.CloseConnection) != CommandBehavior.Default)
				{
					this.Connection.Close();
				}
				if (this.Tds != null)
				{
					this.Tds.SequentialAccess = false;
				}
			}
			this.behavior = CommandBehavior.Default;
		}

		/// <summary>Creates a new instance of a <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001220 RID: 4640 RVA: 0x00046548 File Offset: 0x00044748
		public new SqlParameter CreateParameter()
		{
			return new SqlParameter();
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00046550 File Offset: 0x00044750
		private string EscapeProcName(string name, bool schema)
		{
			string text = name.Trim();
			int length = text.Length;
			char[] array = new char[] { '[', ']' };
			int num = 0;
			int num2 = length;
			string text2;
			if (length > 1)
			{
				int num3;
				bool flag = (num3 = text.IndexOf('[')) <= 0;
				if (flag && num3 > -1)
				{
					int num4 = text.IndexOf(']');
					if (num3 > num4 && num4 != -1)
					{
						flag = false;
					}
					else if (num4 == length - 1)
					{
						if (text.IndexOfAny(array, 1, length - 2) != -1)
						{
							flag = false;
						}
						else
						{
							num = 1;
							num2 = length - 2;
						}
					}
					else
					{
						flag = num4 == -1 && schema;
					}
				}
				if (!flag)
				{
					throw new ArgumentException(string.Format("SqlCommand.CommandText property value is an invalid multipart name {0}, incorrect usage of quotes", this.CommandText));
				}
				text2 = text.Substring(num, num2);
			}
			else
			{
				text2 = text;
			}
			return text2;
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00046660 File Offset: 0x00044860
		internal void DeriveParameters()
		{
			if (this.commandType != CommandType.StoredProcedure)
			{
				throw new InvalidOperationException(string.Format("SqlCommand DeriveParameters only supports CommandType.StoredProcedure, not CommandType.{0}", this.commandType));
			}
			this.ValidateCommand("DeriveParameters", false);
			string text = this.CommandText;
			string text2 = string.Empty;
			int num = text.IndexOf('.');
			if (num >= 0)
			{
				text2 = text.Substring(0, num);
				text = text.Substring(num + 1);
			}
			text = this.EscapeProcName(text, false);
			text2 = this.EscapeProcName(text2, true);
			SqlParameterCollection sqlParameterCollection = new SqlParameterCollection(this);
			sqlParameterCollection.Add("@procedure_name", SqlDbType.NVarChar, text.Length).Value = text;
			if (text2.Length > 0)
			{
				sqlParameterCollection.Add("@procedure_schema", SqlDbType.NVarChar, text2.Length).Value = text2;
			}
			string text3 = "sp_procedure_params_rowset";
			try
			{
				this.Connection.Tds.ExecProc(text3, sqlParameterCollection.MetaParameters, 0, true);
			}
			catch (TdsTimeoutException ex)
			{
				this.Connection.Tds.Reset();
				throw SqlException.FromTdsInternalException(ex);
			}
			catch (TdsInternalException ex2)
			{
				this.Connection.Close();
				throw SqlException.FromTdsInternalException(ex2);
			}
			SqlDataReader sqlDataReader = new SqlDataReader(this);
			this.parameters.Clear();
			object[] array = new object[sqlDataReader.FieldCount];
			while (sqlDataReader.Read())
			{
				sqlDataReader.GetValues(array);
				this.parameters.Add(new SqlParameter(array));
			}
			sqlDataReader.Close();
			if (this.parameters.Count == 0)
			{
				throw new InvalidOperationException("Stored procedure '" + text + "' does not exist.");
			}
		}

		// Token: 0x06001223 RID: 4643 RVA: 0x00046838 File Offset: 0x00044A38
		private void Execute(bool wantResults)
		{
			int num = 0;
			this.Connection.Tds.RecordsAffected = -1;
			TdsMetaParameterCollection metaParameters = this.Parameters.MetaParameters;
			foreach (object obj in ((IEnumerable)metaParameters))
			{
				TdsMetaParameter tdsMetaParameter = (TdsMetaParameter)obj;
				tdsMetaParameter.Validate(num++);
			}
			if (this.preparedStatement == null)
			{
				bool flag = (this.behavior & CommandBehavior.SchemaOnly) > CommandBehavior.Default;
				bool flag2 = (this.behavior & CommandBehavior.KeyInfo) > CommandBehavior.Default;
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				if (flag || flag2)
				{
					stringBuilder.Append("SET FMTONLY OFF;");
				}
				if (flag2)
				{
					stringBuilder.Append("SET NO_BROWSETABLE ON;");
					stringBuilder2.Append("SET NO_BROWSETABLE OFF;");
				}
				if (flag)
				{
					stringBuilder.Append("SET FMTONLY ON;");
					stringBuilder2.Append("SET FMTONLY OFF;");
				}
				switch (this.CommandType)
				{
				case CommandType.Text:
				{
					string text;
					if (stringBuilder2.Length > 0)
					{
						text = string.Format("{0}{1};{2}", stringBuilder.ToString(), this.CommandText, stringBuilder2.ToString());
					}
					else
					{
						text = string.Format("{0}{1}", stringBuilder.ToString(), this.CommandText);
					}
					try
					{
						this.Connection.Tds.Execute(text, metaParameters, this.CommandTimeout, wantResults);
					}
					catch (TdsTimeoutException ex)
					{
						this.Connection.Tds.Reset();
						throw SqlException.FromTdsInternalException(ex);
					}
					catch (TdsInternalException ex2)
					{
						this.Connection.Close();
						throw SqlException.FromTdsInternalException(ex2);
					}
					break;
				}
				case CommandType.StoredProcedure:
					try
					{
						if (flag2 || flag)
						{
							this.Connection.Tds.Execute(stringBuilder.ToString());
						}
						this.Connection.Tds.ExecProc(this.CommandText, metaParameters, this.CommandTimeout, wantResults);
						if (flag2 || flag)
						{
							this.Connection.Tds.Execute(stringBuilder2.ToString());
						}
					}
					catch (TdsTimeoutException ex3)
					{
						this.Connection.Tds.Reset();
						throw SqlException.FromTdsInternalException(ex3);
					}
					catch (TdsInternalException ex4)
					{
						this.Connection.Close();
						throw SqlException.FromTdsInternalException(ex4);
					}
					break;
				}
			}
			else
			{
				try
				{
					this.Connection.Tds.ExecPrepared(this.preparedStatement, metaParameters, this.CommandTimeout, wantResults);
				}
				catch (TdsTimeoutException ex5)
				{
					this.Connection.Tds.Reset();
					throw SqlException.FromTdsInternalException(ex5);
				}
				catch (TdsInternalException ex6)
				{
					this.Connection.Close();
					throw SqlException.FromTdsInternalException(ex6);
				}
			}
		}

		/// <summary>Executes a Transact-SQL statement against the connection and returns the number of rows affected.</summary>
		/// <returns>The number of rows affected.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An exception occurred while executing the command against a locked row. This exception is not generated when you are using Microsoft .NET Framework version 1.0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001224 RID: 4644 RVA: 0x00046BC8 File Offset: 0x00044DC8
		public override int ExecuteNonQuery()
		{
			this.ValidateCommand("ExecuteNonQuery", false);
			int num = 0;
			this.behavior = CommandBehavior.Default;
			try
			{
				this.Execute(false);
				num = this.Connection.Tds.RecordsAffected;
			}
			catch (TdsTimeoutException ex)
			{
				this.Connection.Tds.Reset();
				throw SqlException.FromTdsInternalException(ex);
			}
			this.GetOutputParameters();
			return num;
		}

		/// <summary>Sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> and builds a <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlDataReader" /> object.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An exception occurred while executing the command against a locked row. This exception is not generated when you are using Microsoft .NET Framework version 1.0. </exception>
		/// <exception cref="T:System.InvalidOperationException">The current state of the connection is closed. <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteReader" /> requires an open <see cref="T:System.Data.SqlClient.SqlConnection" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001225 RID: 4645 RVA: 0x00046C48 File Offset: 0x00044E48
		public new SqlDataReader ExecuteReader()
		{
			return this.ExecuteReader(CommandBehavior.Default);
		}

		/// <summary>Sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" />, and builds a <see cref="T:System.Data.SqlClient.SqlDataReader" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlDataReader" /> object.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001226 RID: 4646 RVA: 0x00046C54 File Offset: 0x00044E54
		public new SqlDataReader ExecuteReader(CommandBehavior behavior)
		{
			this.ValidateCommand("ExecuteReader", false);
			if ((behavior & CommandBehavior.SingleRow) != CommandBehavior.Default)
			{
				behavior |= CommandBehavior.SingleResult;
			}
			this.behavior = behavior;
			if ((behavior & CommandBehavior.SequentialAccess) != CommandBehavior.Default)
			{
				this.Tds.SequentialAccess = true;
			}
			SqlDataReader dataReader;
			try
			{
				this.Execute(true);
				this.Connection.DataReader = new SqlDataReader(this);
				dataReader = this.Connection.DataReader;
			}
			catch
			{
				if ((behavior & CommandBehavior.CloseConnection) != CommandBehavior.Default)
				{
					this.Connection.Close();
				}
				throw;
			}
			return dataReader;
		}

		/// <summary>Executes the query, and returns the first column of the first row in the result set returned by the query. Additional columns or rows are ignored.</summary>
		/// <returns>The first column of the first row in the result set, or a null reference (Nothing in Visual Basic) if the result set is empty. Returns a maximum of 2033 characters.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An exception occurred while executing the command against a locked row. This exception is not generated when you are using Microsoft .NET Framework version 1.0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001227 RID: 4647 RVA: 0x00046D00 File Offset: 0x00044F00
		public override object ExecuteScalar()
		{
			object obj2;
			try
			{
				object obj = null;
				this.ValidateCommand("ExecuteScalar", false);
				this.behavior = CommandBehavior.Default;
				this.Execute(true);
				try
				{
					if (this.Connection.Tds.NextResult() && this.Connection.Tds.NextRow())
					{
						obj = this.Connection.Tds.ColumnValues[0];
					}
					if (this.commandType == CommandType.StoredProcedure)
					{
						this.Connection.Tds.SkipToEnd();
						this.GetOutputParameters();
					}
				}
				catch (TdsTimeoutException ex)
				{
					this.Connection.Tds.Reset();
					throw SqlException.FromTdsInternalException(ex);
				}
				catch (TdsInternalException ex2)
				{
					this.Connection.Close();
					throw SqlException.FromTdsInternalException(ex2);
				}
				obj2 = obj;
			}
			finally
			{
				this.CloseDataReader();
			}
			return obj2;
		}

		/// <summary>Sends the <see cref="P:System.Data.SqlClient.SqlCommand.CommandText" /> to the <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> and builds an <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">An exception occurred while executing the command against a locked row. This exception is not generated when you are using Microsoft .NET Framework version 1.0. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001228 RID: 4648 RVA: 0x00046E28 File Offset: 0x00045028
		public XmlReader ExecuteXmlReader()
		{
			this.ValidateCommand("ExecuteXmlReader", false);
			this.behavior = CommandBehavior.Default;
			try
			{
				this.Execute(true);
			}
			catch (TdsTimeoutException ex)
			{
				this.Connection.Tds.Reset();
				throw SqlException.FromTdsInternalException(ex);
			}
			SqlDataReader sqlDataReader = new SqlDataReader(this);
			SqlXmlTextReader sqlXmlTextReader = new SqlXmlTextReader(sqlDataReader);
			return new XmlTextReader(sqlXmlTextReader);
		}

		// Token: 0x06001229 RID: 4649 RVA: 0x00046EA4 File Offset: 0x000450A4
		internal void GetOutputParameters()
		{
			IList outputParameters = this.Connection.Tds.OutputParameters;
			if (outputParameters != null && outputParameters.Count > 0)
			{
				int num = 0;
				foreach (object obj in this.parameters)
				{
					SqlParameter sqlParameter = (SqlParameter)obj;
					if (sqlParameter.Direction != ParameterDirection.Input && sqlParameter.Direction != ParameterDirection.ReturnValue)
					{
						sqlParameter.Value = outputParameters[num];
						num++;
					}
					if (num >= outputParameters.Count)
					{
						break;
					}
				}
			}
		}

		// Token: 0x0600122A RID: 4650 RVA: 0x00046F70 File Offset: 0x00045170
		protected override void Dispose(bool disposing)
		{
			if (this.disposed)
			{
				return;
			}
			if (disposing)
			{
				this.parameters.Clear();
			}
			base.Dispose(disposing);
			this.disposed = true;
		}

		/// <summary>Creates a prepared version of the command on an instance of SQL Server.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> is not set.-or- The <see cref="P:System.Data.SqlClient.SqlCommand.Connection" /> is not <see cref="M:System.Data.SqlClient.SqlConnection.Open" />. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600122B RID: 4651 RVA: 0x00046FA0 File Offset: 0x000451A0
		public override void Prepare()
		{
			if (this.Connection == null)
			{
				throw new NullReferenceException();
			}
			if (this.CommandType == CommandType.StoredProcedure || (this.CommandType == CommandType.Text && this.Parameters.Count == 0))
			{
				return;
			}
			this.ValidateCommand("Prepare", false);
			try
			{
				foreach (object obj in this.Parameters)
				{
					SqlParameter sqlParameter = (SqlParameter)obj;
					sqlParameter.CheckIfInitialized();
				}
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException("SqlCommand.Prepare requires " + ex.Message);
			}
			this.preparedStatement = this.Connection.Tds.Prepare(this.CommandText, this.Parameters.MetaParameters);
		}

		/// <summary>Resets the <see cref="P:System.Data.SqlClient.SqlCommand.CommandTimeout" /> property to its default value.</summary>
		// Token: 0x0600122C RID: 4652 RVA: 0x000470B4 File Offset: 0x000452B4
		public void ResetCommandTimeout()
		{
			this.commandTimeout = 30;
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x000470C0 File Offset: 0x000452C0
		private void Unprepare()
		{
			this.Connection.Tds.Unprepare(this.preparedStatement);
			this.preparedStatement = null;
		}

		// Token: 0x0600122E RID: 4654 RVA: 0x000470E0 File Offset: 0x000452E0
		private void ValidateCommand(string method, bool async)
		{
			if (this.Connection == null)
			{
				throw new InvalidOperationException(string.Format("{0}: A Connection object is required to continue.", method));
			}
			if (this.Transaction == null && this.Connection.Transaction != null)
			{
				throw new InvalidOperationException(string.Format("{0} requires a transaction if the command's connection is in a pending transaction.", method));
			}
			if (this.Transaction != null && this.Transaction.Connection != this.Connection)
			{
				throw new InvalidOperationException("The connection does not have the same transaction as the command.");
			}
			if (this.Connection.State != ConnectionState.Open)
			{
				throw new InvalidOperationException(string.Format("{0} requires an open connection to continue. This connection is closed.", method));
			}
			if (this.CommandText.Length == 0)
			{
				throw new InvalidOperationException(string.Format("{0}: CommandText has not been set for this Command.", method));
			}
			if (this.Connection.DataReader != null)
			{
				throw new InvalidOperationException("There is already an open DataReader associated with this Connection which must be closed first.");
			}
			if (this.Connection.XmlReader != null)
			{
				throw new InvalidOperationException("There is already an open XmlReader associated with this Connection which must be closed first.");
			}
			if (async && !this.Connection.AsyncProcessing)
			{
				throw new InvalidOperationException("This Connection object is not in Asynchronous mode. Use 'Asynchronous Processing = true' to set it.");
			}
		}

		// Token: 0x0600122F RID: 4655 RVA: 0x000471FC File Offset: 0x000453FC
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00047204 File Offset: 0x00045404
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			return this.ExecuteReader(behavior);
		}

		// Token: 0x17000337 RID: 823
		// (get) Token: 0x06001231 RID: 4657 RVA: 0x00047210 File Offset: 0x00045410
		// (set) Token: 0x06001232 RID: 4658 RVA: 0x00047218 File Offset: 0x00045418
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (SqlConnection)value;
			}
		}

		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06001233 RID: 4659 RVA: 0x00047228 File Offset: 0x00045428
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x17000339 RID: 825
		// (get) Token: 0x06001234 RID: 4660 RVA: 0x00047230 File Offset: 0x00045430
		// (set) Token: 0x06001235 RID: 4661 RVA: 0x00047238 File Offset: 0x00045438
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (SqlTransaction)value;
			}
		}

		// Token: 0x06001236 RID: 4662 RVA: 0x00047248 File Offset: 0x00045448
		internal IAsyncResult BeginExecuteInternal(CommandBehavior behavior, bool wantResults, AsyncCallback callback, object state)
		{
			IAsyncResult asyncResult = null;
			this.Connection.Tds.RecordsAffected = -1;
			TdsMetaParameterCollection metaParameters = this.Parameters.MetaParameters;
			if (this.preparedStatement == null)
			{
				bool flag = (behavior & CommandBehavior.SchemaOnly) > CommandBehavior.Default;
				bool flag2 = (behavior & CommandBehavior.KeyInfo) > CommandBehavior.Default;
				StringBuilder stringBuilder = new StringBuilder();
				StringBuilder stringBuilder2 = new StringBuilder();
				if (flag || flag2)
				{
					stringBuilder.Append("SET FMTONLY OFF;");
				}
				if (flag2)
				{
					stringBuilder.Append("SET NO_BROWSETABLE ON;");
					stringBuilder2.Append("SET NO_BROWSETABLE OFF;");
				}
				if (flag)
				{
					stringBuilder.Append("SET FMTONLY ON;");
					stringBuilder2.Append("SET FMTONLY OFF;");
				}
				switch (this.CommandType)
				{
				case CommandType.Text:
				{
					string text = string.Format("{0}{1};{2}", stringBuilder.ToString(), this.CommandText, stringBuilder2.ToString());
					try
					{
						if (wantResults)
						{
							asyncResult = this.Connection.Tds.BeginExecuteQuery(text, metaParameters, callback, state);
						}
						else
						{
							asyncResult = this.Connection.Tds.BeginExecuteNonQuery(text, metaParameters, callback, state);
						}
					}
					catch (TdsTimeoutException ex)
					{
						this.Connection.Tds.Reset();
						throw SqlException.FromTdsInternalException(ex);
					}
					catch (TdsInternalException ex2)
					{
						this.Connection.Close();
						throw SqlException.FromTdsInternalException(ex2);
					}
					break;
				}
				case CommandType.StoredProcedure:
				{
					string text2 = string.Empty;
					string text3 = string.Empty;
					if (flag2 || flag)
					{
						text2 = stringBuilder.ToString();
					}
					if (flag2 || flag)
					{
						text3 = stringBuilder2.ToString();
					}
					try
					{
						this.Connection.Tds.BeginExecuteProcedure(text2, text3, this.CommandText, !wantResults, metaParameters, callback, state);
					}
					catch (TdsTimeoutException ex3)
					{
						this.Connection.Tds.Reset();
						throw SqlException.FromTdsInternalException(ex3);
					}
					catch (TdsInternalException ex4)
					{
						this.Connection.Close();
						throw SqlException.FromTdsInternalException(ex4);
					}
					break;
				}
				}
			}
			else
			{
				try
				{
					this.Connection.Tds.ExecPrepared(this.preparedStatement, metaParameters, this.CommandTimeout, wantResults);
				}
				catch (TdsTimeoutException ex5)
				{
					this.Connection.Tds.Reset();
					throw SqlException.FromTdsInternalException(ex5);
				}
				catch (TdsInternalException ex6)
				{
					this.Connection.Close();
					throw SqlException.FromTdsInternalException(ex6);
				}
			}
			return asyncResult;
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x00047550 File Offset: 0x00045750
		internal void EndExecuteInternal(IAsyncResult ar)
		{
			SqlAsyncResult sqlAsyncResult = (SqlAsyncResult)ar;
			this.Connection.Tds.WaitFor(sqlAsyncResult.InternalResult);
			this.Connection.Tds.CheckAndThrowException(sqlAsyncResult.InternalResult);
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" />.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll or wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)" />, which returns the number of affected rows.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001238 RID: 4664 RVA: 0x00047590 File Offset: 0x00045790
		public IAsyncResult BeginExecuteNonQuery()
		{
			return this.BeginExecuteNonQuery(null, null);
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" />, given a callback procedure and state information.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll or wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)" />, which returns the number of affected rows.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that is invoked when the command's execution has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate that no callback is required.</param>
		/// <param name="stateObject">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback procedure using the <see cref="P:System.IAsyncResult.AsyncState" /> property.</param>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001239 RID: 4665 RVA: 0x0004759C File Offset: 0x0004579C
		public IAsyncResult BeginExecuteNonQuery(AsyncCallback callback, object stateObject)
		{
			this.ValidateCommand("BeginExecuteNonQuery", true);
			SqlAsyncResult sqlAsyncResult = new SqlAsyncResult(callback, stateObject);
			sqlAsyncResult.EndMethod = "EndExecuteNonQuery";
			sqlAsyncResult.InternalResult = this.BeginExecuteInternal(CommandBehavior.Default, false, sqlAsyncResult.BubbleCallback, sqlAsyncResult);
			return sqlAsyncResult;
		}

		/// <summary>Finishes asynchronous execution of a Transact-SQL statement.</summary>
		/// <returns>The number of rows affected (the same behavior as <see cref="M:System.Data.SqlClient.SqlCommand.ExecuteNonQuery" />).</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned by the call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> parameter is null (Nothing in Microsoft Visual Basic)</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)" /> was called more than once for a single command execution, or the method was mismatched against its execution method (for example, the code called <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteNonQuery(System.IAsyncResult)" /> to complete execution of a call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600123A RID: 4666 RVA: 0x000475E0 File Offset: 0x000457E0
		public int EndExecuteNonQuery(IAsyncResult asyncResult)
		{
			this.ValidateAsyncResult(asyncResult, "EndExecuteNonQuery");
			this.EndExecuteInternal(asyncResult);
			int recordsAffected = this.Connection.Tds.RecordsAffected;
			this.GetOutputParameters();
			((SqlAsyncResult)asyncResult).Ended = true;
			return recordsAffected;
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" />, and retrieves one or more result sets from the server.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll or wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" />, which returns a <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance that can be used to retrieve the returned rows.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600123B RID: 4667 RVA: 0x00047624 File Offset: 0x00045824
		public IAsyncResult BeginExecuteReader()
		{
			return this.BeginExecuteReader(null, null, CommandBehavior.Default);
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" /> using one of the <see cref="T:System.Data.CommandBehavior" /> values.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll, wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" />, which returns a <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance that can be used to retrieve the returned rows.</returns>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values, indicating options for statement execution and data retrieval.</param>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600123C RID: 4668 RVA: 0x00047630 File Offset: 0x00045830
		public IAsyncResult BeginExecuteReader(CommandBehavior behavior)
		{
			return this.BeginExecuteReader(null, null, behavior);
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" /> and retrieves one or more result sets from the server, given a callback procedure and state information.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll, wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" />, which returns a <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance which can be used to retrieve the returned rows.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that is invoked when the command's execution has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate that no callback is required.</param>
		/// <param name="stateObject">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback procedure using the <see cref="P:System.IAsyncResult.AsyncState" /> property.</param>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600123D RID: 4669 RVA: 0x0004763C File Offset: 0x0004583C
		public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject)
		{
			return this.BeginExecuteReader(callback, stateObject, CommandBehavior.Default);
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" />, using one of the CommandBehavior values, and retrieving one or more result sets from the server, given a callback procedure and state information. </summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll or wait for results, or both; this value is also needed when invoking <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" />, which returns a <see cref="T:System.Data.SqlClient.SqlDataReader" /> instance which can be used to retrieve the returned rows.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that is invoked when the command's execution has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate that no callback is required.</param>
		/// <param name="stateObject">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback procedure using the <see cref="P:System.IAsyncResult.AsyncState" /> property.</param>
		/// <param name="behavior">One of the <see cref="T:System.Data.CommandBehavior" /> values, indicating options for statement execution and data retrieval.</param>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600123E RID: 4670 RVA: 0x00047648 File Offset: 0x00045848
		public IAsyncResult BeginExecuteReader(AsyncCallback callback, object stateObject, CommandBehavior behavior)
		{
			this.ValidateCommand("BeginExecuteReader", true);
			this.behavior = behavior;
			SqlAsyncResult sqlAsyncResult = new SqlAsyncResult(callback, stateObject);
			sqlAsyncResult.EndMethod = "EndExecuteReader";
			IAsyncResult asyncResult = this.BeginExecuteInternal(behavior, true, sqlAsyncResult.BubbleCallback, stateObject);
			sqlAsyncResult.InternalResult = asyncResult;
			return sqlAsyncResult;
		}

		/// <summary>Finishes asynchronous execution of a Transact-SQL statement, returning the requested <see cref="T:System.Data.SqlClient.SqlDataReader" />.</summary>
		/// <returns>A <see cref="T:System.Data.SqlClient.SqlDataReader" /> object that can be used to retrieve the requested rows.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned by the call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteReader" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> parameter is null (Nothing in Microsoft Visual Basic)</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" /> was called more than once for a single command execution, or the method was mismatched against its execution method (for example, the code called <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteReader(System.IAsyncResult)" /> to complete execution of a call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader" />.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600123F RID: 4671 RVA: 0x00047694 File Offset: 0x00045894
		public SqlDataReader EndExecuteReader(IAsyncResult asyncResult)
		{
			this.ValidateAsyncResult(asyncResult, "EndExecuteReader");
			this.EndExecuteInternal(asyncResult);
			SqlDataReader sqlDataReader = null;
			try
			{
				sqlDataReader = new SqlDataReader(this);
			}
			catch (TdsTimeoutException ex)
			{
				throw SqlException.FromTdsInternalException(ex);
			}
			catch (TdsInternalException ex2)
			{
				if ((this.behavior & CommandBehavior.CloseConnection) != CommandBehavior.Default)
				{
					this.Connection.Close();
				}
				throw SqlException.FromTdsInternalException(ex2);
			}
			((SqlAsyncResult)asyncResult).Ended = true;
			return sqlDataReader;
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" /> and returns results as an <see cref="T:System.Xml.XmlReader" /> object, using a callback procedure.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll, wait for results, or both; this value is also needed when the <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteXmlReader(System.IAsyncResult)" /> is called, which returns the results of the command as XML.</returns>
		/// <param name="callback">An <see cref="T:System.AsyncCallback" /> delegate that is invoked when the command's execution has completed. Pass null (Nothing in Microsoft Visual Basic) to indicate that no callback is required.</param>
		/// <param name="stateObject">A user-defined state object that is passed to the callback procedure. Retrieve this object from within the callback procedure using the <see cref="P:System.IAsyncResult.AsyncState" /> property.</param>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001240 RID: 4672 RVA: 0x00047738 File Offset: 0x00045938
		public IAsyncResult BeginExecuteXmlReader(AsyncCallback callback, object stateObject)
		{
			this.ValidateCommand("BeginExecuteXmlReader", true);
			SqlAsyncResult sqlAsyncResult = new SqlAsyncResult(callback, stateObject);
			sqlAsyncResult.EndMethod = "EndExecuteXmlReader";
			sqlAsyncResult.InternalResult = this.BeginExecuteInternal(this.behavior, true, sqlAsyncResult.BubbleCallback, stateObject);
			return sqlAsyncResult;
		}

		/// <summary>Initiates the asynchronous execution of the Transact-SQL statement or stored procedure that is described by this <see cref="T:System.Data.SqlClient.SqlCommand" /> and returns results as an <see cref="T:System.Xml.XmlReader" /> object.</summary>
		/// <returns>An <see cref="T:System.IAsyncResult" /> that can be used to poll or wait for results, or both; this value is also needed when invoking EndExecuteXmlReader, which returns a single XML value.</returns>
		/// <exception cref="T:System.Data.SqlClient.SqlException">Any error that occurred while executing the command text.</exception>
		/// <exception cref="T:System.InvalidOperationException">The name/value pair "Asynchronous Processing=true" was not included within the connection string defining the connection for this <see cref="T:System.Data.SqlClient.SqlCommand" />.</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy, ControlAppDomain" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Data.SqlClient.SqlClientPermission, System.Data, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06001241 RID: 4673 RVA: 0x00047780 File Offset: 0x00045980
		public IAsyncResult BeginExecuteXmlReader()
		{
			return this.BeginExecuteXmlReader(null, null);
		}

		/// <summary>Finishes asynchronous execution of a Transact-SQL statement, returning the requested data as XML.</summary>
		/// <returns>An <see cref="T:System.Xml.XmlReader" /> object that can be used to fetch the resulting XML data.</returns>
		/// <param name="asyncResult">The <see cref="T:System.IAsyncResult" /> returned by the call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteXmlReader" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="asyncResult" /> parameter is null (Nothing in Microsoft Visual Basic)</exception>
		/// <exception cref="T:System.InvalidOperationException">
		///   <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteXmlReader(System.IAsyncResult)" /> was called more than once for a single command execution, or the method was mismatched against its execution method (for example, the code called <see cref="M:System.Data.SqlClient.SqlCommand.EndExecuteXmlReader(System.IAsyncResult)" /> to complete execution of a call to <see cref="M:System.Data.SqlClient.SqlCommand.BeginExecuteNonQuery" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001242 RID: 4674 RVA: 0x0004778C File Offset: 0x0004598C
		public XmlReader EndExecuteXmlReader(IAsyncResult asyncResult)
		{
			this.ValidateAsyncResult(asyncResult, "EndExecuteXmlReader");
			this.EndExecuteInternal(asyncResult);
			SqlDataReader sqlDataReader = new SqlDataReader(this);
			SqlXmlTextReader sqlXmlTextReader = new SqlXmlTextReader(sqlDataReader);
			XmlReader xmlReader = new XmlTextReader(sqlXmlTextReader);
			((SqlAsyncResult)asyncResult).Ended = true;
			return xmlReader;
		}

		// Token: 0x06001243 RID: 4675 RVA: 0x000477D0 File Offset: 0x000459D0
		internal void ValidateAsyncResult(IAsyncResult ar, string endMethod)
		{
			if (ar == null)
			{
				throw new ArgumentException("result passed is null!");
			}
			if (!(ar is SqlAsyncResult))
			{
				throw new ArgumentException(string.Format("cannot test validity of types {0}", ar.GetType()));
			}
			SqlAsyncResult sqlAsyncResult = (SqlAsyncResult)ar;
			if (sqlAsyncResult.EndMethod != endMethod)
			{
				throw new InvalidOperationException(string.Format("Mismatched {0} called for AsyncResult. Expected call to {1} but {0} is called instead.", endMethod, sqlAsyncResult.EndMethod));
			}
			if (sqlAsyncResult.Ended)
			{
				throw new InvalidOperationException(string.Format("The method {0} cannot be called more than once for the same AsyncResult.", endMethod));
			}
		}

		// Token: 0x04000732 RID: 1842
		private const int DEFAULT_COMMAND_TIMEOUT = 30;

		// Token: 0x04000733 RID: 1843
		private int commandTimeout;

		// Token: 0x04000734 RID: 1844
		private bool designTimeVisible;

		// Token: 0x04000735 RID: 1845
		private string commandText;

		// Token: 0x04000736 RID: 1846
		private CommandType commandType;

		// Token: 0x04000737 RID: 1847
		private SqlConnection connection;

		// Token: 0x04000738 RID: 1848
		private SqlTransaction transaction;

		// Token: 0x04000739 RID: 1849
		private UpdateRowSource updatedRowSource;

		// Token: 0x0400073A RID: 1850
		private CommandBehavior behavior;

		// Token: 0x0400073B RID: 1851
		private SqlParameterCollection parameters;

		// Token: 0x0400073C RID: 1852
		private string preparedStatement;

		// Token: 0x0400073D RID: 1853
		private bool disposed;

		// Token: 0x0400073E RID: 1854
		private SqlNotificationRequest notification;

		// Token: 0x0400073F RID: 1855
		private bool notificationAutoEnlist;
	}
}
