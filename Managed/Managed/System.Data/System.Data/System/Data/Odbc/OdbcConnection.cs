using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.EnterpriseServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Transactions;

namespace System.Data.Odbc
{
	/// <summary>Represents an open connection to a data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200011F RID: 287
	[DefaultEvent("InfoMessage")]
	public sealed class OdbcConnection : DbConnection, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnection" /> class.</summary>
		// Token: 0x06000FFC RID: 4092 RVA: 0x0003E25C File Offset: 0x0003C45C
		public OdbcConnection()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.Odbc.OdbcConnection" /> class with the specified connection string.</summary>
		/// <param name="connectionString">The connection used to open the data source. </param>
		// Token: 0x06000FFD RID: 4093 RVA: 0x0003E26C File Offset: 0x0003C46C
		public OdbcConnection(string connectionString)
		{
			this.connectionTimeout = 15;
			this.ConnectionString = connectionString;
		}

		/// <summary>Occurs when the ODBC driver sends a warning or an informational message.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06000FFE RID: 4094 RVA: 0x0003E29C File Offset: 0x0003C49C
		// (remove) Token: 0x06000FFF RID: 4095 RVA: 0x0003E2B8 File Offset: 0x0003C4B8
		[OdbcCategory("DataCategory_InfoMessage")]
		[OdbcDescription("DbConnection_InfoMessage")]
		public event OdbcInfoMessageEventHandler InfoMessage;

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		// Token: 0x06001000 RID: 4096 RVA: 0x0003E2D4 File Offset: 0x0003C4D4
		[MonoTODO]
		object ICloneable.Clone()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06001001 RID: 4097 RVA: 0x0003E2DC File Offset: 0x0003C4DC
		internal IntPtr hDbc
		{
			get
			{
				return this.hdbc;
			}
		}

		/// <summary>Gets or sets the string used to open a data source.</summary>
		/// <returns>The ODBC driver connection string that includes settings, such as the data source name, needed to establish the initial connection. The default value is an empty string (""). The maximum length is 1024 characters.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AA RID: 682
		// (get) Token: 0x06001002 RID: 4098 RVA: 0x0003E2E4 File Offset: 0x0003C4E4
		// (set) Token: 0x06001003 RID: 4099 RVA: 0x0003E300 File Offset: 0x0003C500
		[RecommendedAsConfigurable(true)]
		[Editor("Microsoft.VSDesigner.Data.Odbc.Design.OdbcConnectionStringEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[RefreshProperties(RefreshProperties.All)]
		[OdbcDescription("Information used to connect to a Data Source")]
		[DefaultValue("")]
		[OdbcCategory("DataCategory_Data")]
		public override string ConnectionString
		{
			get
			{
				if (this.connectionString == null)
				{
					return string.Empty;
				}
				return this.connectionString;
			}
			set
			{
				this.connectionString = value;
			}
		}

		/// <summary>Gets or sets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time in seconds to wait for a connection to open. The default value is 15 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002AB RID: 683
		// (get) Token: 0x06001004 RID: 4100 RVA: 0x0003E30C File Offset: 0x0003C50C
		// (set) Token: 0x06001005 RID: 4101 RVA: 0x0003E314 File Offset: 0x0003C514
		[OdbcDescription("Current connection timeout value, not settable  in the ConnectionString")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[DefaultValue(15)]
		public new int ConnectionTimeout
		{
			get
			{
				return this.connectionTimeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException("Timout should not be less than zero.");
				}
				this.connectionTimeout = value;
			}
		}

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database. The default value is an empty string ("") until the connection is opened.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x170002AC RID: 684
		// (get) Token: 0x06001006 RID: 4102 RVA: 0x0003E330 File Offset: 0x0003C530
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[OdbcDescription("Current data source Catlog value, 'Database=X' in the ConnectionString")]
		public override string Database
		{
			get
			{
				if (this.State == ConnectionState.Closed)
				{
					return string.Empty;
				}
				return this.GetInfo(OdbcInfo.DatabaseName);
			}
		}

		/// <summary>Gets the current state of the connection.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Data.ConnectionState" /> values. The default is Closed.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x170002AD RID: 685
		// (get) Token: 0x06001007 RID: 4103 RVA: 0x0003E34C File Offset: 0x0003C54C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[OdbcDescription("The ConnectionState indicating whether the connection is open or closed")]
		[Browsable(false)]
		public override ConnectionState State
		{
			get
			{
				if (this.hdbc != IntPtr.Zero)
				{
					return ConnectionState.Open;
				}
				return ConnectionState.Closed;
			}
		}

		/// <summary>Gets the server name or file name of the data source.</summary>
		/// <returns>The server name or file name of the data source. The default value is an empty string ("") until the connection is opened.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x170002AE RID: 686
		// (get) Token: 0x06001008 RID: 4104 RVA: 0x0003E368 File Offset: 0x0003C568
		[OdbcDescription("Current data source, 'Server=X' in the ConnectionString")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public override string DataSource
		{
			get
			{
				if (this.State == ConnectionState.Closed)
				{
					return string.Empty;
				}
				return this.GetInfo(OdbcInfo.DataSourceName);
			}
		}

		/// <summary>Gets the name of the ODBC driver specified for the current connection.</summary>
		/// <returns>The name of the ODBC driver. This typically is the DLL name (for example, Sqlsrv32.dll). The default value is an empty string ("") until the connection is opened.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002AF RID: 687
		// (get) Token: 0x06001009 RID: 4105 RVA: 0x0003E384 File Offset: 0x0003C584
		[Browsable(false)]
		[OdbcDescription("Current ODBC Driver")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public string Driver
		{
			get
			{
				if (this.State == ConnectionState.Closed)
				{
					return string.Empty;
				}
				return this.GetInfo(OdbcInfo.DriverName);
			}
		}

		/// <summary>Gets a string that contains the version of the server to which the client is connected.</summary>
		/// <returns>The version of the connected server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600100A RID: 4106 RVA: 0x0003E3A0 File Offset: 0x0003C5A0
		[OdbcDescription("Version of the product accessed by the ODBC Driver")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string ServerVersion
		{
			get
			{
				return this.GetInfo(OdbcInfo.DbmsVersion);
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x0600100B RID: 4107 RVA: 0x0003E3AC File Offset: 0x0003C5AC
		internal string SafeDriver
		{
			get
			{
				string safeInfo = this.GetSafeInfo(OdbcInfo.DriverName);
				if (safeInfo == null)
				{
					return string.Empty;
				}
				return safeInfo;
			}
		}

		/// <summary>Starts a transaction at the data source.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">A transaction is currently active. Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600100C RID: 4108 RVA: 0x0003E3D0 File Offset: 0x0003C5D0
		public new OdbcTransaction BeginTransaction()
		{
			return this.BeginTransaction(IsolationLevel.Unspecified);
		}

		// Token: 0x0600100D RID: 4109 RVA: 0x0003E3DC File Offset: 0x0003C5DC
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginTransaction(isolationLevel);
		}

		/// <summary>Starts a transaction at the data source with the specified <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="isolevel">The transaction isolation level for this connection. If you do not specify an isolation level, the default isolation level for the driver is used. </param>
		/// <exception cref="T:System.InvalidOperationException">A transaction is currently active. Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x0600100E RID: 4110 RVA: 0x0003E3E8 File Offset: 0x0003C5E8
		public new OdbcTransaction BeginTransaction(IsolationLevel isolevel)
		{
			if (this.State == ConnectionState.Closed)
			{
				throw ExceptionHelper.ConnectionClosed();
			}
			if (this.transaction == null)
			{
				this.transaction = new OdbcTransaction(this, isolevel);
				return this.transaction;
			}
			throw new InvalidOperationException();
		}

		/// <summary>Closes the connection to the data source. </summary>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x0600100F RID: 4111 RVA: 0x0003E420 File Offset: 0x0003C620
		public override void Close()
		{
			if (this.State == ConnectionState.Open)
			{
				if (this.linkedCommands != null)
				{
					for (int i = 0; i < this.linkedCommands.Count; i++)
					{
						WeakReference weakReference = (WeakReference)this.linkedCommands[i];
						if (weakReference != null)
						{
							OdbcCommand odbcCommand = (OdbcCommand)weakReference.Target;
							if (odbcCommand != null)
							{
								odbcCommand.Unlink();
							}
						}
					}
					this.linkedCommands = null;
				}
				OdbcReturn odbcReturn = libodbc.SQLDisconnect(this.hdbc);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.CreateOdbcException(OdbcHandleType.Dbc, this.hdbc);
				}
				this.FreeHandles();
				this.transaction = null;
				this.RaiseStateChange(ConnectionState.Open, ConnectionState.Closed);
			}
		}

		/// <summary>Creates and returns an <see cref="T:System.Data.Odbc.OdbcCommand" /> object associated with the <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <returns>An <see cref="T:System.Data.Odbc.OdbcCommand" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001010 RID: 4112 RVA: 0x0003E4DC File Offset: 0x0003C6DC
		public new OdbcCommand CreateCommand()
		{
			return new OdbcCommand(string.Empty, this, this.transaction);
		}

		/// <summary>Changes the current database associated with an open <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <param name="value">The database name. </param>
		/// <exception cref="T:System.ArgumentException">The database name is not valid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The connection is not open. </exception>
		/// <exception cref="T:System.Data.Odbc.OdbcException">Cannot change the database. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x06001011 RID: 4113 RVA: 0x0003E4F0 File Offset: 0x0003C6F0
		public override void ChangeDatabase(string value)
		{
			IntPtr intPtr = IntPtr.Zero;
			try
			{
				intPtr = Marshal.StringToHGlobalUni(value);
				OdbcReturn odbcReturn = libodbc.SQLSetConnectAttr(this.hdbc, OdbcConnectionAttribute.CurrentCatalog, intPtr, value.Length * 2);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.CreateOdbcException(OdbcHandleType.Dbc, this.hdbc);
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeCoTaskMem(intPtr);
				}
			}
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0003E578 File Offset: 0x0003C778
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				try
				{
					this.Close();
					this.disposed = true;
				}
				finally
				{
					base.Dispose(disposing);
				}
			}
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0003E5C8 File Offset: 0x0003C7C8
		protected override DbCommand CreateDbCommand()
		{
			return this.CreateCommand();
		}

		/// <summary>Opens a connection to a data source with the property settings specified by the <see cref="P:System.Data.Odbc.OdbcConnection.ConnectionString" />.</summary>
		// Token: 0x06001014 RID: 4116 RVA: 0x0003E5D0 File Offset: 0x0003C7D0
		public override void Open()
		{
			if (this.State == ConnectionState.Open)
			{
				throw new InvalidOperationException();
			}
			try
			{
				OdbcReturn odbcReturn = libodbc.SQLAllocHandle(OdbcHandleType.Env, IntPtr.Zero, ref this.henv);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					OdbcException ex = new OdbcException(new OdbcErrorCollection
					{
						new OdbcError(this)
					});
					this.MessageHandler(ex);
					throw ex;
				}
				odbcReturn = libodbc.SQLSetEnvAttr(this.henv, OdbcEnv.OdbcVersion, (IntPtr)3, 0);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.CreateOdbcException(OdbcHandleType.Env, this.henv);
				}
				odbcReturn = libodbc.SQLAllocHandle(OdbcHandleType.Dbc, this.henv, ref this.hdbc);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.CreateOdbcException(OdbcHandleType.Env, this.henv);
				}
				if (this.ConnectionString.ToLower().IndexOf("dsn=") >= 0)
				{
					string text = string.Empty;
					string text2 = string.Empty;
					string text3 = string.Empty;
					string[] array = this.ConnectionString.Split(new char[] { ';' });
					foreach (string text4 in array)
					{
						string[] array3 = text4.Split(new char[] { '=' });
						string text5 = array3[0].Trim().ToLower();
						switch (text5)
						{
						case "dsn":
							text3 = array3[1].Trim();
							break;
						case "uid":
							text = array3[1].Trim();
							break;
						case "pwd":
							text2 = array3[1].Trim();
							break;
						}
					}
					odbcReturn = libodbc.SQLConnect(this.hdbc, text3, -3, text, -3, text2, -3);
					if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
					{
						throw this.CreateOdbcException(OdbcHandleType.Dbc, this.hdbc);
					}
				}
				else
				{
					string text6 = new string(' ', 1024);
					short num2 = 0;
					odbcReturn = libodbc.SQLDriverConnect(this.hdbc, IntPtr.Zero, this.ConnectionString, -3, text6, (short)text6.Length, ref num2, 0);
					if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
					{
						throw this.CreateOdbcException(OdbcHandleType.Dbc, this.hdbc);
					}
				}
				this.RaiseStateChange(ConnectionState.Closed, ConnectionState.Open);
			}
			catch
			{
				this.FreeHandles();
				throw;
			}
			this.disposed = false;
		}

		/// <summary>Indicates that the ODBC Driver Manager environment handle can be released when the last underlying connection is released.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06001015 RID: 4117 RVA: 0x0003E88C File Offset: 0x0003CA8C
		[MonoTODO]
		public static void ReleaseObjectPool()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0003E894 File Offset: 0x0003CA94
		private void FreeHandles()
		{
			if (this.hdbc != IntPtr.Zero)
			{
				OdbcReturn odbcReturn = libodbc.SQLFreeHandle(2, this.hdbc);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.CreateOdbcException(OdbcHandleType.Dbc, this.hdbc);
				}
			}
			this.hdbc = IntPtr.Zero;
			if (this.henv != IntPtr.Zero)
			{
				OdbcReturn odbcReturn = libodbc.SQLFreeHandle(1, this.henv);
				if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
				{
					throw this.CreateOdbcException(OdbcHandleType.Env, this.henv);
				}
			}
			this.henv = IntPtr.Zero;
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Odbc.OdbcConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x06001017 RID: 4119 RVA: 0x0003E934 File Offset: 0x0003CB34
		public override DataTable GetSchema()
		{
			if (this.State == ConnectionState.Closed)
			{
				throw ExceptionHelper.ConnectionClosed();
			}
			return DbConnection.MetaDataCollections.Instance;
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Odbc.OdbcConnection" /> using the specified name for the schema name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		// Token: 0x06001018 RID: 4120 RVA: 0x0003E94C File Offset: 0x0003CB4C
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.Odbc.OdbcConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		// Token: 0x06001019 RID: 4121 RVA: 0x0003E958 File Offset: 0x0003CB58
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			if (this.State == ConnectionState.Closed)
			{
				throw ExceptionHelper.ConnectionClosed();
			}
			return this.GetSchema(collectionName, null);
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
		// Token: 0x0600101A RID: 4122 RVA: 0x0003E974 File Offset: 0x0003CB74
		[MonoTODO]
		public override void EnlistTransaction(Transaction transaction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.EnterpriseServices.ITransaction" /> in which to enlist.</param>
		// Token: 0x0600101B RID: 4123 RVA: 0x0003E97C File Offset: 0x0003CB7C
		[MonoTODO]
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0003E984 File Offset: 0x0003CB84
		internal string GetInfo(OdbcInfo info)
		{
			if (this.State == ConnectionState.Closed)
			{
				throw new InvalidOperationException("The connection is closed.");
			}
			short num = 512;
			byte[] array = new byte[512];
			short num2 = 0;
			OdbcReturn odbcReturn = libodbc.SQLGetInfo(this.hdbc, info, array, num, ref num2);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				throw this.CreateOdbcException(OdbcHandleType.Dbc, this.hdbc);
			}
			return Encoding.Unicode.GetString(array, 0, (int)num2);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0003E9F8 File Offset: 0x0003CBF8
		private string GetSafeInfo(OdbcInfo info)
		{
			if (this.State == ConnectionState.Closed)
			{
				return null;
			}
			short num = 512;
			byte[] array = new byte[512];
			short num2 = 0;
			OdbcReturn odbcReturn = libodbc.SQLGetInfo(this.hdbc, info, array, num, ref num2);
			if (odbcReturn != OdbcReturn.Success && odbcReturn != OdbcReturn.SuccessWithInfo)
			{
				return null;
			}
			return Encoding.Unicode.GetString(array, 0, (int)num2);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0003EA54 File Offset: 0x0003CC54
		private void RaiseStateChange(ConnectionState from, ConnectionState to)
		{
			base.OnStateChange(new StateChangeEventArgs(from, to));
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0003EA64 File Offset: 0x0003CC64
		private OdbcInfoMessageEventArgs CreateOdbcInfoMessageEvent(OdbcErrorCollection errors)
		{
			return new OdbcInfoMessageEventArgs(errors);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0003EA6C File Offset: 0x0003CC6C
		private void OnOdbcInfoMessage(OdbcInfoMessageEventArgs e)
		{
			if (this.InfoMessage != null)
			{
				this.InfoMessage(this, e);
			}
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0003EA88 File Offset: 0x0003CC88
		internal OdbcException CreateOdbcException(OdbcHandleType HandleType, IntPtr Handle)
		{
			short num = 256;
			short num2 = 0;
			int num3 = 0;
			OdbcReturn odbcReturn = OdbcReturn.Success;
			OdbcErrorCollection odbcErrorCollection = new OdbcErrorCollection();
			for (;;)
			{
				byte[] array = new byte[(int)(num * 2)];
				byte[] array2 = new byte[(int)(num * 2)];
				switch (HandleType)
				{
				case OdbcHandleType.Env:
					odbcReturn = libodbc.SQLError(Handle, IntPtr.Zero, IntPtr.Zero, array2, ref num3, array, num, ref num2);
					break;
				case OdbcHandleType.Dbc:
					odbcReturn = libodbc.SQLError(IntPtr.Zero, Handle, IntPtr.Zero, array2, ref num3, array, num, ref num2);
					break;
				case OdbcHandleType.Stmt:
					odbcReturn = libodbc.SQLError(IntPtr.Zero, IntPtr.Zero, Handle, array2, ref num3, array, num, ref num2);
					break;
				}
				if (odbcReturn != OdbcReturn.Success)
				{
					break;
				}
				string text = OdbcConnection.RemoveTrailingNullChar(Encoding.Unicode.GetString(array2));
				string @string = Encoding.Unicode.GetString(array, 0, (int)(num2 * 2));
				odbcErrorCollection.Add(new OdbcError(@string, text, num3));
			}
			string safeDriver = this.SafeDriver;
			foreach (object obj in odbcErrorCollection)
			{
				OdbcError odbcError = (OdbcError)obj;
				odbcError.SetSource(safeDriver);
			}
			return new OdbcException(odbcErrorCollection);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0003EBF4 File Offset: 0x0003CDF4
		private static string RemoveTrailingNullChar(string value)
		{
			return value.TrimEnd(new char[1]);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0003EC04 File Offset: 0x0003CE04
		internal void Link(OdbcCommand cmd)
		{
			if (this.linkedCommands == null)
			{
				this.linkedCommands = new ArrayList();
			}
			this.linkedCommands.Add(new WeakReference(cmd));
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0003EC3C File Offset: 0x0003CE3C
		internal void Unlink(OdbcCommand cmd)
		{
			if (this.linkedCommands == null)
			{
				return;
			}
			for (int i = 0; i < this.linkedCommands.Count; i++)
			{
				WeakReference weakReference = (WeakReference)this.linkedCommands[i];
				if (weakReference != null)
				{
					OdbcCommand odbcCommand = (OdbcCommand)weakReference.Target;
					if (odbcCommand == cmd)
					{
						this.linkedCommands[i] = null;
						break;
					}
				}
			}
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0003ECB4 File Offset: 0x0003CEB4
		private void MessageHandler(OdbcException e)
		{
			this.OnOdbcInfoMessage(this.CreateOdbcInfoMessageEvent(e.Errors));
		}

		// Token: 0x04000553 RID: 1363
		private string connectionString;

		// Token: 0x04000554 RID: 1364
		private int connectionTimeout;

		// Token: 0x04000555 RID: 1365
		internal OdbcTransaction transaction;

		// Token: 0x04000556 RID: 1366
		private IntPtr henv = IntPtr.Zero;

		// Token: 0x04000557 RID: 1367
		private IntPtr hdbc = IntPtr.Zero;

		// Token: 0x04000558 RID: 1368
		private bool disposed;

		// Token: 0x04000559 RID: 1369
		private ArrayList linkedCommands;
	}
}
