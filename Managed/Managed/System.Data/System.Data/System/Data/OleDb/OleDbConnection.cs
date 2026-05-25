using System;
using System.ComponentModel;
using System.Data.Common;
using System.EnterpriseServices;
using System.Transactions;

namespace System.Data.OleDb
{
	/// <summary>Represents an open connection to a data source.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020000EC RID: 236
	[DefaultEvent("InfoMessage")]
	public sealed class OleDbConnection : DbConnection, ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbConnection" /> class.</summary>
		// Token: 0x06000B45 RID: 2885 RVA: 0x00031D2C File Offset: 0x0002FF2C
		public OleDbConnection()
		{
			this.gdaConnection = IntPtr.Zero;
			this.connectionTimeout = 15;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.OleDb.OleDbConnection" /> class with the specified connection string.</summary>
		/// <param name="connectionString">The connection used to open the database. </param>
		// Token: 0x06000B46 RID: 2886 RVA: 0x00031D48 File Offset: 0x0002FF48
		public OleDbConnection(string connectionString)
			: this()
		{
			this.connectionString = connectionString;
		}

		/// <summary>Occurs when the provider sends a warning or an informational message.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06000B47 RID: 2887 RVA: 0x00031D58 File Offset: 0x0002FF58
		// (remove) Token: 0x06000B48 RID: 2888 RVA: 0x00031D74 File Offset: 0x0002FF74
		[DataCategory("DataCategory_InfoMessage")]
		public event OleDbInfoMessageEventHandler InfoMessage;

		/// <summary>For a description of this member, see <see cref="M:System.ICloneable.Clone" />.</summary>
		// Token: 0x06000B49 RID: 2889 RVA: 0x00031D90 File Offset: 0x0002FF90
		[MonoTODO]
		object ICloneable.Clone()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the string used to open a database.</summary>
		/// <returns>The OLE DB provider connection string that includes the data source name, and other parameters needed to establish the initial connection. The default value is an empty string.</returns>
		/// <exception cref="T:System.ArgumentException">An invalid connection string argument has been supplied or a required connection string argument has not been supplied. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000B4A RID: 2890 RVA: 0x00031D98 File Offset: 0x0002FF98
		// (set) Token: 0x06000B4B RID: 2891 RVA: 0x00031DB4 File Offset: 0x0002FFB4
		[DataCategory("Data")]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.All)]
		[RecommendedAsConfigurable(true)]
		[Editor("Microsoft.VSDesigner.Data.ADO.Design.OleDbConnectionStringEditor, Microsoft.VSDesigner, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
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

		/// <summary>Gets the time to wait while trying to establish a connection before terminating the attempt and generating an error.</summary>
		/// <returns>The time in seconds to wait for a connection to open. The default value is 15 seconds.</returns>
		/// <exception cref="T:System.ArgumentException">The value set is less than 0. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00031DC0 File Offset: 0x0002FFC0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override int ConnectionTimeout
		{
			get
			{
				return this.connectionTimeout;
			}
		}

		/// <summary>Gets the name of the current database or the database to be used after a connection is opened.</summary>
		/// <returns>The name of the current database or the name of the database to be used after a connection is opened. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000B4D RID: 2893 RVA: 0x00031DC8 File Offset: 0x0002FFC8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Database
		{
			get
			{
				if (this.gdaConnection != IntPtr.Zero && libgda.gda_connection_is_open(this.gdaConnection))
				{
					return libgda.gda_connection_get_database(this.gdaConnection);
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the server name or file name of the data source.</summary>
		/// <returns>The server name or file name of the data source. The default value is an empty string.</returns>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00031E0C File Offset: 0x0003000C
		[Browsable(true)]
		public override string DataSource
		{
			get
			{
				if (this.gdaConnection != IntPtr.Zero && libgda.gda_connection_is_open(this.gdaConnection))
				{
					return libgda.gda_connection_get_dsn(this.gdaConnection);
				}
				return string.Empty;
			}
		}

		/// <summary>Gets the name of the OLE DB provider specified in the "Provider= " clause of the connection string.</summary>
		/// <returns>The name of the provider as specified in the "Provider= " clause of the connection string. The default value is an empty string.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00031E50 File Offset: 0x00030050
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(true)]
		public string Provider
		{
			get
			{
				if (this.gdaConnection != IntPtr.Zero && libgda.gda_connection_is_open(this.gdaConnection))
				{
					return libgda.gda_connection_get_provider(this.gdaConnection);
				}
				return string.Empty;
			}
		}

		/// <summary>Gets a string that contains the version of the server to which the client is connected.</summary>
		/// <returns>The version of the connected server.</returns>
		/// <exception cref="T:System.InvalidOperationException">The connection is closed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000B50 RID: 2896 RVA: 0x00031E94 File Offset: 0x00030094
		public override string ServerVersion
		{
			get
			{
				if (this.State == ConnectionState.Closed)
				{
					throw ExceptionHelper.ConnectionClosed();
				}
				return libgda.gda_connection_get_server_version(this.gdaConnection);
			}
		}

		/// <summary>Gets the current state of the connection.</summary>
		/// <returns>A bitwise combination of the <see cref="T:System.Data.ConnectionState" /> values. The default is Closed.</returns>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000B51 RID: 2897 RVA: 0x00031EB4 File Offset: 0x000300B4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ConnectionState State
		{
			get
			{
				if (this.gdaConnection != IntPtr.Zero && libgda.gda_connection_is_open(this.gdaConnection))
				{
					return ConnectionState.Open;
				}
				return ConnectionState.Closed;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000B52 RID: 2898 RVA: 0x00031EEC File Offset: 0x000300EC
		internal IntPtr GdaConnection
		{
			get
			{
				return this.gdaConnection;
			}
		}

		/// <summary>Starts a database transaction with the current <see cref="T:System.Data.IsolationLevel" /> value.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B53 RID: 2899 RVA: 0x00031EF4 File Offset: 0x000300F4
		public new OleDbTransaction BeginTransaction()
		{
			if (this.State == ConnectionState.Closed)
			{
				throw ExceptionHelper.ConnectionClosed();
			}
			return new OleDbTransaction(this);
		}

		/// <summary>Starts a database transaction with the specified isolation level.</summary>
		/// <returns>An object representing the new transaction.</returns>
		/// <param name="isolationLevel">The isolation level under which the transaction should run.</param>
		/// <exception cref="T:System.InvalidOperationException">Parallel transactions are not supported. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B54 RID: 2900 RVA: 0x00031F10 File Offset: 0x00030110
		public new OleDbTransaction BeginTransaction(IsolationLevel isolationLevel)
		{
			if (this.State == ConnectionState.Closed)
			{
				throw ExceptionHelper.ConnectionClosed();
			}
			return new OleDbTransaction(this, isolationLevel);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00031F2C File Offset: 0x0003012C
		protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
		{
			return this.BeginTransaction(isolationLevel);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x00031F38 File Offset: 0x00030138
		protected override DbCommand CreateDbCommand()
		{
			return this.CreateCommand();
		}

		/// <summary>Changes the current database for an open <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <param name="value">The database name. </param>
		/// <exception cref="T:System.ArgumentException">The database name is not valid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The connection is not open. </exception>
		/// <exception cref="T:System.Data.OleDb.OleDbException">Cannot change the database. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B57 RID: 2903 RVA: 0x00031F40 File Offset: 0x00030140
		public override void ChangeDatabase(string value)
		{
			if (this.State != ConnectionState.Open)
			{
				throw new InvalidOperationException();
			}
			if (!libgda.gda_connection_change_database(this.gdaConnection, value))
			{
				throw new OleDbException(this);
			}
		}

		/// <summary>Closes the connection to the data source.</summary>
		// Token: 0x06000B58 RID: 2904 RVA: 0x00031F78 File Offset: 0x00030178
		public override void Close()
		{
			if (this.State == ConnectionState.Open)
			{
				libgda.gda_connection_close(this.gdaConnection);
				this.gdaConnection = IntPtr.Zero;
			}
		}

		/// <summary>Creates and returns an <see cref="T:System.Data.OleDb.OleDbCommand" /> object associated with the <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <returns>An <see cref="T:System.Data.OleDb.OleDbCommand" /> object.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000B59 RID: 2905 RVA: 0x00031FA0 File Offset: 0x000301A0
		public new OleDbCommand CreateCommand()
		{
			if (this.State == ConnectionState.Open)
			{
				return new OleDbCommand(null, this);
			}
			return null;
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x00031FB8 File Offset: 0x000301B8
		[MonoTODO]
		protected override void Dispose(bool disposing)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns schema information from a data source as indicated by a GUID, and after it applies the specified restrictions.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains the requested schema information.</returns>
		/// <param name="schema">One of the <see cref="T:System.Data.OleDb.OleDbSchemaGuid" /> values that specifies the schema table to return. </param>
		/// <param name="restrictions">An <see cref="T:System.Object" /> array of restriction values. These are applied in the order of the restriction columns. That is, the first restriction value applies to the first restriction column, the second restriction value applies to the second restriction column, and so on. </param>
		/// <exception cref="T:System.Data.OleDb.OleDbException">The specified set of restrictions is invalid. </exception>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Data.OleDb.OleDbConnection" /> is closed. </exception>
		/// <exception cref="T:System.ArgumentException">The specified schema rowset is not supported by the OLE DB provider.-or- The <paramref name="schema" /> parameter contains a value of <see cref="F:System.Data.OleDb.OleDbSchemaGuid.DbInfoLiterals" /> and the <paramref name="restrictions" /> parameter contains one or more restrictions. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B5B RID: 2907 RVA: 0x00031FC0 File Offset: 0x000301C0
		[MonoTODO]
		public DataTable GetOleDbSchemaTable(Guid schema, object[] restrictions)
		{
			throw new NotImplementedException();
		}

		/// <summary>Opens a database connection with the property settings specified by the <see cref="P:System.Data.OleDb.OleDbConnection.ConnectionString" />.</summary>
		/// <exception cref="T:System.InvalidOperationException">The connection is already open.</exception>
		/// <exception cref="T:System.Data.OleDb.OleDbException">A connection-level error occurred while opening the connection.</exception>
		// Token: 0x06000B5C RID: 2908 RVA: 0x00031FC8 File Offset: 0x000301C8
		public override void Open()
		{
			if (this.State == ConnectionState.Open)
			{
				throw new InvalidOperationException();
			}
			libgda.gda_init("System.Data.OleDb", "1.0", 0, new string[0]);
			this.gdaConnection = libgda.gda_client_open_connection(libgda.GdaClient, this.ConnectionString, string.Empty, string.Empty, (GdaConnectionOptions)0);
			if (this.gdaConnection == IntPtr.Zero)
			{
				throw new OleDbException(this);
			}
		}

		/// <summary>Indicates that the <see cref="T:System.Data.OleDb.OleDbConnection" /> object pool can be released when the last underlying connection is released.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B5D RID: 2909 RVA: 0x0003203C File Offset: 0x0003023C
		[MonoTODO]
		public static void ReleaseObjectPool()
		{
			throw new NotImplementedException();
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.EnterpriseServices.ITransaction" /> in which to enlist.</param>
		// Token: 0x06000B5E RID: 2910 RVA: 0x00032044 File Offset: 0x00030244
		[MonoTODO]
		public void EnlistDistributedTransaction(ITransaction transaction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Enlists in the specified transaction as a distributed transaction.</summary>
		/// <param name="transaction">A reference to an existing <see cref="T:System.Transactions.Transaction" /> in which to enlist.</param>
		// Token: 0x06000B5F RID: 2911 RVA: 0x0003204C File Offset: 0x0003024C
		[MonoTODO]
		public override void EnlistTransaction(Transaction transaction)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.OleDb.OleDbConnection" />.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		// Token: 0x06000B60 RID: 2912 RVA: 0x00032054 File Offset: 0x00030254
		[MonoTODO]
		public override DataTable GetSchema()
		{
			if (this.State == ConnectionState.Closed)
			{
				throw ExceptionHelper.ConnectionClosed();
			}
			throw new NotImplementedException();
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.OleDb.OleDbConnection" /> using the specified string for the schema name.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		// Token: 0x06000B61 RID: 2913 RVA: 0x0003206C File Offset: 0x0003026C
		[MonoTODO]
		public override DataTable GetSchema(string collectionName)
		{
			return this.GetSchema(collectionName, null);
		}

		/// <summary>Returns schema information for the data source of this <see cref="T:System.Data.OleDb.OleDbConnection" /> using the specified string for the schema name and the specified string array for the restriction values.</summary>
		/// <returns>A <see cref="T:System.Data.DataTable" /> that contains schema information.</returns>
		/// <param name="collectionName">Specifies the name of the schema to return.</param>
		/// <param name="restrictionValues">Specifies a set of restriction values for the requested schema.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="collectionName" /> is specified as null.</exception>
		// Token: 0x06000B62 RID: 2914 RVA: 0x00032078 File Offset: 0x00030278
		[MonoTODO]
		public override DataTable GetSchema(string collectionName, string[] restrictionValues)
		{
			if (this.State == ConnectionState.Closed)
			{
				throw ExceptionHelper.ConnectionClosed();
			}
			throw new NotImplementedException();
		}

		/// <summary>Updates the <see cref="P:System.Data.OleDb.OleDbConnection.State" /> property of the <see cref="T:System.Data.OleDb.OleDbConnection" /> object.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		///   <IPermission class="System.Diagnostics.PerformanceCounterPermission, System, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		/// </PermissionSet>
		// Token: 0x06000B63 RID: 2915 RVA: 0x00032090 File Offset: 0x00030290
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[MonoTODO]
		public void ResetState()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000427 RID: 1063
		private string connectionString;

		// Token: 0x04000428 RID: 1064
		private int connectionTimeout;

		// Token: 0x04000429 RID: 1065
		private IntPtr gdaConnection;
	}
}
