using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Common;

namespace System.Data.SqlClient
{
	/// <summary>Provides a simple way to create and manage the contents of connection strings used by the <see cref="T:System.Data.SqlClient.SqlConnection" /> class. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000166 RID: 358
	[DefaultProperty("DataSource")]
	[TypeConverter("System.Data.SqlClient.SqlConnectionStringBuilder+SqlConnectionStringBuilderConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public sealed class SqlConnectionStringBuilder : DbConnectionStringBuilder
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> class.</summary>
		// Token: 0x060012B0 RID: 4784 RVA: 0x0004D93C File Offset: 0x0004BB3C
		public SqlConnectionStringBuilder()
			: this(string.Empty)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> class. The provided connection string provides the data for the instance's internal connection information.</summary>
		/// <param name="connectionString">The basis for the object's internal connection information. Parsed into name/value pairs. Invalid key names raise <see cref="T:System.Collections.Generic.KeyNotFoundException" />.</param>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Invalid key name within the connection string.</exception>
		/// <exception cref="T:System.FormatException">Invalid value within the connection string (specifically, when a Boolean or numeric value was expected but not supplied).</exception>
		/// <exception cref="T:System.ArgumentException">The supplied <paramref name="connectionString" /> is not valid.</exception>
		// Token: 0x060012B1 RID: 4785 RVA: 0x0004D94C File Offset: 0x0004BB4C
		public SqlConnectionStringBuilder(string connectionString)
		{
			this.Init();
			base.ConnectionString = connectionString;
		}

		// Token: 0x060012B2 RID: 4786 RVA: 0x0004D964 File Offset: 0x0004BB64
		static SqlConnectionStringBuilder()
		{
			SqlConnectionStringBuilder._keywords["APP"] = "Application Name";
			SqlConnectionStringBuilder._keywords["APPLICATION NAME"] = "Application Name";
			SqlConnectionStringBuilder._keywords["ATTACHDBFILENAME"] = "AttachDbFilename";
			SqlConnectionStringBuilder._keywords["EXTENDED PROPERTIES"] = "Extended Properties";
			SqlConnectionStringBuilder._keywords["INITIAL FILE NAME"] = "Initial File Name";
			SqlConnectionStringBuilder._keywords["TIMEOUT"] = "Connect Timeout";
			SqlConnectionStringBuilder._keywords["CONNECT TIMEOUT"] = "Connect Timeout";
			SqlConnectionStringBuilder._keywords["CONNECTION TIMEOUT"] = "Connect Timeout";
			SqlConnectionStringBuilder._keywords["CONNECTION RESET"] = "Connection Reset";
			SqlConnectionStringBuilder._keywords["LANGUAGE"] = "Current Language";
			SqlConnectionStringBuilder._keywords["CURRENT LANGUAGE"] = "Current Language";
			SqlConnectionStringBuilder._keywords["DATA SOURCE"] = "Data Source";
			SqlConnectionStringBuilder._keywords["SERVER"] = "Data Source";
			SqlConnectionStringBuilder._keywords["ADDRESS"] = "Data Source";
			SqlConnectionStringBuilder._keywords["ADDR"] = "Data Source";
			SqlConnectionStringBuilder._keywords["NETWORK ADDRESS"] = "Data Source";
			SqlConnectionStringBuilder._keywords["ENCRYPT"] = "Encrypt";
			SqlConnectionStringBuilder._keywords["ENLIST"] = "Enlist";
			SqlConnectionStringBuilder._keywords["INITIAL CATALOG"] = "Initial Catalog";
			SqlConnectionStringBuilder._keywords["DATABASE"] = "Initial Catalog";
			SqlConnectionStringBuilder._keywords["INTEGRATED SECURITY"] = "Integrated Security";
			SqlConnectionStringBuilder._keywords["TRUSTED_CONNECTION"] = "Integrated Security";
			SqlConnectionStringBuilder._keywords["MAX POOL SIZE"] = "Max Pool Size";
			SqlConnectionStringBuilder._keywords["MIN POOL SIZE"] = "Min Pool Size";
			SqlConnectionStringBuilder._keywords["MULTIPLEACTIVERESULTSETS"] = "MultipleActiveResultSets";
			SqlConnectionStringBuilder._keywords["ASYNCHRONOUS PROCESSING"] = "Asynchronous Processing";
			SqlConnectionStringBuilder._keywords["ASYNC"] = "Async";
			SqlConnectionStringBuilder._keywords["NET"] = "Network Library";
			SqlConnectionStringBuilder._keywords["NETWORK"] = "Network Library";
			SqlConnectionStringBuilder._keywords["NETWORK LIBRARY"] = "Network Library";
			SqlConnectionStringBuilder._keywords["PACKET SIZE"] = "Packet Size";
			SqlConnectionStringBuilder._keywords["PASSWORD"] = "Password";
			SqlConnectionStringBuilder._keywords["PWD"] = "Password";
			SqlConnectionStringBuilder._keywords["PERSISTSECURITYINFO"] = "Persist Security Info";
			SqlConnectionStringBuilder._keywords["PERSIST SECURITY INFO"] = "Persist Security Info";
			SqlConnectionStringBuilder._keywords["POOLING"] = "Pooling";
			SqlConnectionStringBuilder._keywords["UID"] = "User ID";
			SqlConnectionStringBuilder._keywords["USER"] = "User ID";
			SqlConnectionStringBuilder._keywords["USER ID"] = "User ID";
			SqlConnectionStringBuilder._keywords["WSID"] = "Workstation ID";
			SqlConnectionStringBuilder._keywords["WORKSTATION ID"] = "Workstation ID";
			SqlConnectionStringBuilder._keywords["USER INSTANCE"] = "User Instance";
			SqlConnectionStringBuilder._keywords["CONTEXT CONNECTION"] = "Context Connection";
			SqlConnectionStringBuilder._keywords["TRANSACTION BINDING"] = "Transaction Binding";
			SqlConnectionStringBuilder._keywords["FAILOVER PARTNER"] = "Failover Partner";
			SqlConnectionStringBuilder._keywords["REPLICATION"] = "Replication";
			SqlConnectionStringBuilder._keywords["TRUSTSERVERCERTIFICATE"] = "TrustServerCertificate";
			SqlConnectionStringBuilder._keywords["LOAD BALANCE TIMEOUT"] = "Load Balance Timeout";
			SqlConnectionStringBuilder._keywords["TYPE SYSTEM VERSION"] = "Type System Version";
			SqlConnectionStringBuilder._defaults = new Dictionary<string, object>();
			SqlConnectionStringBuilder._defaults.Add("Data Source", string.Empty);
			SqlConnectionStringBuilder._defaults.Add("Failover Partner", string.Empty);
			SqlConnectionStringBuilder._defaults.Add("AttachDbFilename", string.Empty);
			SqlConnectionStringBuilder._defaults.Add("Initial Catalog", string.Empty);
			SqlConnectionStringBuilder._defaults.Add("Integrated Security", false);
			SqlConnectionStringBuilder._defaults.Add("Persist Security Info", false);
			SqlConnectionStringBuilder._defaults.Add("User ID", string.Empty);
			SqlConnectionStringBuilder._defaults.Add("Password", string.Empty);
			SqlConnectionStringBuilder._defaults.Add("Enlist", false);
			SqlConnectionStringBuilder._defaults.Add("Pooling", true);
			SqlConnectionStringBuilder._defaults.Add("Min Pool Size", 0);
			SqlConnectionStringBuilder._defaults.Add("Max Pool Size", 100);
			SqlConnectionStringBuilder._defaults.Add("Asynchronous Processing", false);
			SqlConnectionStringBuilder._defaults.Add("Connection Reset", true);
			SqlConnectionStringBuilder._defaults.Add("MultipleActiveResultSets", false);
			SqlConnectionStringBuilder._defaults.Add("Replication", false);
			SqlConnectionStringBuilder._defaults.Add("Connect Timeout", 15);
			SqlConnectionStringBuilder._defaults.Add("Encrypt", false);
			SqlConnectionStringBuilder._defaults.Add("TrustServerCertificate", false);
			SqlConnectionStringBuilder._defaults.Add("Load Balance Timeout", 0);
			SqlConnectionStringBuilder._defaults.Add("Network Library", string.Empty);
			SqlConnectionStringBuilder._defaults.Add("Packet Size", 8000);
			SqlConnectionStringBuilder._defaults.Add("Type System Version", "Latest");
			SqlConnectionStringBuilder._defaults.Add("Application Name", ".NET SqlClient Data Provider");
			SqlConnectionStringBuilder._defaults.Add("Current Language", string.Empty);
			SqlConnectionStringBuilder._defaults.Add("Workstation ID", string.Empty);
			SqlConnectionStringBuilder._defaults.Add("User Instance", false);
			SqlConnectionStringBuilder._defaults.Add("Context Connection", false);
			SqlConnectionStringBuilder._defaults.Add("Transaction Binding", "Implicit Unbind");
		}

		/// <summary>Gets or sets the name of the application associated with the connection string.</summary>
		/// <returns>The name of the application, or ".NET SqlClient Data Provider" if no name has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000353 RID: 851
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x0004DFB4 File Offset: 0x0004C1B4
		// (set) Token: 0x060012B4 RID: 4788 RVA: 0x0004DFBC File Offset: 0x0004C1BC
		[DisplayName("Application Name")]
		[RefreshProperties(RefreshProperties.All)]
		public string ApplicationName
		{
			get
			{
				return this._applicationName;
			}
			set
			{
				base["Application Name"] = value;
				this._applicationName = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether asynchronous processing is allowed by the connection created by using this connection string.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.AsynchronousProcessing" /> property, or false if no value has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000354 RID: 852
		// (get) Token: 0x060012B5 RID: 4789 RVA: 0x0004DFD4 File Offset: 0x0004C1D4
		// (set) Token: 0x060012B6 RID: 4790 RVA: 0x0004DFDC File Offset: 0x0004C1DC
		[DisplayName("Asynchronous Processing")]
		[RefreshProperties(RefreshProperties.All)]
		public bool AsynchronousProcessing
		{
			get
			{
				return this._asynchronousProcessing;
			}
			set
			{
				base["Asynchronous Processing"] = value;
				this._asynchronousProcessing = value;
			}
		}

		/// <summary>Gets or sets a string that contains the name of the primary data file. This includes the full path name of an attachable database.</summary>
		/// <returns>The value of the AttachDBFilename property, or String.Empty if no value has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000355 RID: 853
		// (get) Token: 0x060012B7 RID: 4791 RVA: 0x0004DFF8 File Offset: 0x0004C1F8
		// (set) Token: 0x060012B8 RID: 4792 RVA: 0x0004E000 File Offset: 0x0004C200
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("AttachDbFilename")]
		[Editor("System.Windows.Forms.Design.FileNameEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor, System.Drawing, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string AttachDBFilename
		{
			get
			{
				return this._attachDBFilename;
			}
			set
			{
				base["AttachDbFilename"] = value;
				this._attachDBFilename = value;
			}
		}

		/// <summary>Obsolete. Gets or sets a Boolean value that indicates whether the connection is reset when drawn from the connection pool.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.ConnectionReset" /> property, or true if no value has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000356 RID: 854
		// (get) Token: 0x060012B9 RID: 4793 RVA: 0x0004E018 File Offset: 0x0004C218
		// (set) Token: 0x060012BA RID: 4794 RVA: 0x0004E020 File Offset: 0x0004C220
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Connection Reset")]
		public bool ConnectionReset
		{
			get
			{
				return this._connectionReset;
			}
			set
			{
				base["Connection Reset"] = value;
				this._connectionReset = value;
			}
		}

		/// <summary>Gets or sets the length of time (in seconds) to wait for a connection to the server before terminating the attempt and generating an error.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.ConnectTimeout" /> property, or 15 seconds if no value has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000357 RID: 855
		// (get) Token: 0x060012BB RID: 4795 RVA: 0x0004E03C File Offset: 0x0004C23C
		// (set) Token: 0x060012BC RID: 4796 RVA: 0x0004E044 File Offset: 0x0004C244
		[DisplayName("Connect Timeout")]
		[RefreshProperties(RefreshProperties.All)]
		public int ConnectTimeout
		{
			get
			{
				return this._connectTimeout;
			}
			set
			{
				base["Connect Timeout"] = value;
				this._connectTimeout = value;
			}
		}

		/// <summary>Gets or sets the SQL Server Language record name.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.CurrentLanguage" /> property, or String.Empty if no value has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000358 RID: 856
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0004E060 File Offset: 0x0004C260
		// (set) Token: 0x060012BE RID: 4798 RVA: 0x0004E068 File Offset: 0x0004C268
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Current Language")]
		public string CurrentLanguage
		{
			get
			{
				return this._currentLanguage;
			}
			set
			{
				base["Current Language"] = value;
				this._currentLanguage = value;
			}
		}

		/// <summary>Gets or sets the name or network address of the instance of SQL Server to connect to.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.DataSource" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000359 RID: 857
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0004E080 File Offset: 0x0004C280
		// (set) Token: 0x060012C0 RID: 4800 RVA: 0x0004E088 File Offset: 0x0004C288
		[DisplayName("Data Source")]
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter("System.Data.SqlClient.SqlConnectionStringBuilder+SqlDataSourceConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		public string DataSource
		{
			get
			{
				return this._dataSource;
			}
			set
			{
				base["Data Source"] = value;
				this._dataSource = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether SQL Server uses SSL encryption for all data sent between the client and server if the server has a certificate installed.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.Encrypt" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0004E0A0 File Offset: 0x0004C2A0
		// (set) Token: 0x060012C2 RID: 4802 RVA: 0x0004E0A8 File Offset: 0x0004C2A8
		[DisplayName("Encrypt")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Encrypt
		{
			get
			{
				return this._encrypt;
			}
			set
			{
				base["Encrypt"] = value;
				this._encrypt = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the SQL Server connection pooler automatically enlists the connection in the creation thread's current transaction context.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.Enlist" /> property, or true if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0004E0C4 File Offset: 0x0004C2C4
		// (set) Token: 0x060012C4 RID: 4804 RVA: 0x0004E0CC File Offset: 0x0004C2CC
		[DisplayName("Enlist")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Enlist
		{
			get
			{
				return this._enlist;
			}
			set
			{
				base["Enlist"] = value;
				this._enlist = value;
			}
		}

		/// <summary>Gets or sets the name or address of the partner server to connect to if the primary server is down.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.FailoverPartner" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x060012C5 RID: 4805 RVA: 0x0004E0E8 File Offset: 0x0004C2E8
		// (set) Token: 0x060012C6 RID: 4806 RVA: 0x0004E0F0 File Offset: 0x0004C2F0
		[TypeConverter("System.Data.SqlClient.SqlConnectionStringBuilder+SqlDataSourceConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[DisplayName("Failover Partner")]
		[RefreshProperties(RefreshProperties.All)]
		public string FailoverPartner
		{
			get
			{
				return this._failoverPartner;
			}
			set
			{
				base["Failover Partner"] = value;
				this._failoverPartner = value;
			}
		}

		/// <summary>Gets or sets the name of the database associated with the connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.InitialCatalog" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x060012C7 RID: 4807 RVA: 0x0004E108 File Offset: 0x0004C308
		// (set) Token: 0x060012C8 RID: 4808 RVA: 0x0004E110 File Offset: 0x0004C310
		[RefreshProperties(RefreshProperties.All)]
		[TypeConverter("System.Data.SqlClient.SqlConnectionStringBuilder+SqlInitialCatalogConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[DisplayName("Initial Catalog")]
		public string InitialCatalog
		{
			get
			{
				return this._initialCatalog;
			}
			set
			{
				base["Initial Catalog"] = value;
				this._initialCatalog = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether User ID and Password are specified in the connection (when false) or whether the current Windows account credentials are used for authentication (when true).</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.IntegratedSecurity" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700035E RID: 862
		// (get) Token: 0x060012C9 RID: 4809 RVA: 0x0004E128 File Offset: 0x0004C328
		// (set) Token: 0x060012CA RID: 4810 RVA: 0x0004E130 File Offset: 0x0004C330
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Integrated Security")]
		public bool IntegratedSecurity
		{
			get
			{
				return this._integratedSecurity;
			}
			set
			{
				base["Integrated Security"] = value;
				this._integratedSecurity = value;
			}
		}

		/// <summary>Gets a value that indicates whether the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> has a fixed size.</summary>
		/// <returns>true in every case, because the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> supplies a fixed-size collection of key/value pairs.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700035F RID: 863
		// (get) Token: 0x060012CB RID: 4811 RVA: 0x0004E14C File Offset: 0x0004C34C
		public override bool IsFixedSize
		{
			get
			{
				return true;
			}
		}

		/// <summary>Gets or sets the value associated with the specified key. In C#, this property is the indexer.</summary>
		/// <returns>The value associated with the specified key. </returns>
		/// <param name="keyword">The key of the item to get or set.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is a null reference (Nothing in Visual Basic).</exception>
		/// <exception cref="T:System.Collections.Generic.KeyNotFoundException">Tried to add a key that does not exist within the available keys.</exception>
		/// <exception cref="T:System.FormatException">Invalid value within the connection string (specifically, a Boolean or numeric value was expected but not supplied).</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x17000360 RID: 864
		public override object this[string keyword]
		{
			get
			{
				string text = this.MapKeyword(keyword);
				if (base.ContainsKey(text))
				{
					return base[text];
				}
				return SqlConnectionStringBuilder._defaults[text];
			}
			set
			{
				this.SetValue(keyword, value);
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the keys in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000361 RID: 865
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x0004E190 File Offset: 0x0004C390
		public override ICollection Keys
		{
			get
			{
				return new ReadOnlyCollection<string>(new List<string>
				{
					"Data Source", "Failover Partner", "AttachDbFilename", "Initial Catalog", "Integrated Security", "Persist Security Info", "User ID", "Password", "Enlist", "Pooling",
					"Min Pool Size", "Max Pool Size", "Asynchronous Processing", "Connection Reset", "MultipleActiveResultSets", "Replication", "Connect Timeout", "Encrypt", "TrustServerCertificate", "Load Balance Timeout",
					"Network Library", "Packet Size", "Type System Version", "Application Name", "Current Language", "Workstation ID", "User Instance", "Context Connection", "Transaction Binding"
				});
			}
		}

		/// <summary>Gets or sets the minimum time, in seconds, for the connection to live in the connection pool before being destroyed.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.LoadBalanceTimeout" /> property, or 0 if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0004E2EC File Offset: 0x0004C4EC
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x0004E2F4 File Offset: 0x0004C4F4
		[DisplayName("Load Balance Timeout")]
		[RefreshProperties(RefreshProperties.All)]
		public int LoadBalanceTimeout
		{
			get
			{
				return this._loadBalanceTimeout;
			}
			set
			{
				base["Load Balance Timeout"] = value;
				this._loadBalanceTimeout = value;
			}
		}

		/// <summary>Gets or sets the maximum number of connections allowed in the connection pool for this specific connection string.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.MaxPoolSize" /> property, or 100 if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0004E310 File Offset: 0x0004C510
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x0004E318 File Offset: 0x0004C518
		[DisplayName("Max Pool Size")]
		[RefreshProperties(RefreshProperties.All)]
		public int MaxPoolSize
		{
			get
			{
				return this._maxPoolSize;
			}
			set
			{
				base["Max Pool Size"] = value;
				this._maxPoolSize = value;
			}
		}

		/// <summary>Gets or sets the minimum number of connections allowed in the connection pool for this specific connection string.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.MinPoolSize" /> property, or 0 if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0004E334 File Offset: 0x0004C534
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x0004E33C File Offset: 0x0004C53C
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Min Pool Size")]
		public int MinPoolSize
		{
			get
			{
				return this._minPoolSize;
			}
			set
			{
				base["Min Pool Size"] = value;
				this._minPoolSize = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether multiple active result sets can be associated with the associated connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.MultipleActiveResultSets" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0004E358 File Offset: 0x0004C558
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x0004E360 File Offset: 0x0004C560
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("MultipleActiveResultSets")]
		public bool MultipleActiveResultSets
		{
			get
			{
				return this._multipleActiveResultSets;
			}
			set
			{
				base["Multiple Active Resultsets"] = value;
				this._multipleActiveResultSets = value;
			}
		}

		/// <summary>Gets or sets a string that contains the name of the network library used to establish a connection to the SQL Server.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.NetworkLibrary" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0004E37C File Offset: 0x0004C57C
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0004E384 File Offset: 0x0004C584
		[TypeConverter("System.Data.SqlClient.SqlConnectionStringBuilder+NetworkLibraryConverter, System.Data, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
		[DisplayName("Network Library")]
		[RefreshProperties(RefreshProperties.All)]
		public string NetworkLibrary
		{
			get
			{
				return this._networkLibrary;
			}
			set
			{
				base["Network Library"] = value;
				this._networkLibrary = value;
			}
		}

		/// <summary>Gets or sets the size in bytes of the network packets used to communicate with an instance of SQL Server.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.PacketSize" /> property, or 8000 if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0004E39C File Offset: 0x0004C59C
		// (set) Token: 0x060012DA RID: 4826 RVA: 0x0004E3A4 File Offset: 0x0004C5A4
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Packet Size")]
		public int PacketSize
		{
			get
			{
				return this._packetSize;
			}
			set
			{
				base["Packet Size"] = value;
				this._packetSize = value;
			}
		}

		/// <summary>Gets or sets the password for the SQL Server account.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.Password" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x060012DB RID: 4827 RVA: 0x0004E3C0 File Offset: 0x0004C5C0
		// (set) Token: 0x060012DC RID: 4828 RVA: 0x0004E3C8 File Offset: 0x0004C5C8
		[DisplayName("Password")]
		[PasswordPropertyText(true)]
		[RefreshProperties(RefreshProperties.All)]
		public string Password
		{
			get
			{
				return this._password;
			}
			set
			{
				base["Password"] = value;
				this._password = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates if security-sensitive information, such as the password, is not returned as part of the connection if the connection is open or has ever been in an open state.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.PersistSecurityInfo" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x060012DD RID: 4829 RVA: 0x0004E3E0 File Offset: 0x0004C5E0
		// (set) Token: 0x060012DE RID: 4830 RVA: 0x0004E3E8 File Offset: 0x0004C5E8
		[DisplayName("Persist Security Info")]
		[RefreshProperties(RefreshProperties.All)]
		public bool PersistSecurityInfo
		{
			get
			{
				return this._persistSecurityInfo;
			}
			set
			{
				base["Persist Security Info"] = value;
				this._persistSecurityInfo = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether the connection will be pooled or explicitly opened every time that the connection is requested.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.Pooling" /> property, or true if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x060012DF RID: 4831 RVA: 0x0004E404 File Offset: 0x0004C604
		// (set) Token: 0x060012E0 RID: 4832 RVA: 0x0004E40C File Offset: 0x0004C60C
		[DisplayName("Pooling")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Pooling
		{
			get
			{
				return this._pooling;
			}
			set
			{
				base["Pooling"] = value;
				this._pooling = value;
			}
		}

		/// <summary>Gets or sets a Boolean value that indicates whether replication is supported using the connection.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.Replication" /> property, or false if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x0004E428 File Offset: 0x0004C628
		// (set) Token: 0x060012E2 RID: 4834 RVA: 0x0004E430 File Offset: 0x0004C630
		[DisplayName("Replication")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Replication
		{
			get
			{
				return this._replication;
			}
			set
			{
				base["Replication"] = value;
				this._replication = value;
			}
		}

		/// <summary>Gets or sets the user ID to be used when connecting to SQL Server.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.UserID" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x0004E44C File Offset: 0x0004C64C
		// (set) Token: 0x060012E4 RID: 4836 RVA: 0x0004E454 File Offset: 0x0004C654
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("User ID")]
		public string UserID
		{
			get
			{
				return this._userID;
			}
			set
			{
				base["User Id"] = value;
				this._userID = value;
			}
		}

		/// <summary>Gets an <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</summary>
		/// <returns>An <see cref="T:System.Collections.ICollection" /> that contains the values in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x1700036D RID: 877
		// (get) Token: 0x060012E5 RID: 4837 RVA: 0x0004E46C File Offset: 0x0004C66C
		public override ICollection Values
		{
			get
			{
				return new ReadOnlyCollection<object>(new List<object>
				{
					this._dataSource, this._failoverPartner, this._attachDBFilename, this._initialCatalog, this._integratedSecurity, this._persistSecurityInfo, this._userID, this._password, this._enlist, this._pooling,
					this._minPoolSize, this._maxPoolSize, this._asynchronousProcessing, this._connectionReset, this._multipleActiveResultSets, this._replication, this._connectTimeout, this._encrypt, this._trustServerCertificate, this._loadBalanceTimeout,
					this._networkLibrary, this._packetSize, this._typeSystemVersion, this._applicationName, this._currentLanguage, this._workstationID, this._userInstance, this._contextConnection, this._transactionBinding
				});
			}
		}

		/// <summary>Gets or sets the name of the workstation connecting to SQL Server.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.WorkstationID" /> property, or String.Empty if none has been supplied.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x1700036E RID: 878
		// (get) Token: 0x060012E6 RID: 4838 RVA: 0x0004E638 File Offset: 0x0004C838
		// (set) Token: 0x060012E7 RID: 4839 RVA: 0x0004E640 File Offset: 0x0004C840
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Workstation ID")]
		public string WorkstationID
		{
			get
			{
				return this._workstationID;
			}
			set
			{
				base["Workstation Id"] = value;
				this._workstationID = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether the channel will be encrypted while bypassing walking the certificate chain to validate trust.</summary>
		/// <returns>A Boolean. Recognized values are true, false, yes, and no. </returns>
		// Token: 0x1700036F RID: 879
		// (get) Token: 0x060012E8 RID: 4840 RVA: 0x0004E658 File Offset: 0x0004C858
		// (set) Token: 0x060012E9 RID: 4841 RVA: 0x0004E660 File Offset: 0x0004C860
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("TrustServerCertificate")]
		public bool TrustServerCertificate
		{
			get
			{
				return this._trustServerCertificate;
			}
			set
			{
				base["Trust Server Certificate"] = value;
				this._trustServerCertificate = value;
			}
		}

		/// <summary>Gets or sets a string value that indicates the type system the application expects.</summary>
		/// <returns>The following table shows the possible values for the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.TypeSystemVersion" /> property:ValueDescriptionSQL Server 2000Uses the SQL Server 2000 type system. The following comparisons will be performed when connecting to a SQL Server 2005 instance:XML to NTEXTUDT to VARBINARYVARCHAR(MAX), NVARCHAR(MAX) and VARBINARY(MAX) to TEXT, NEXT and IMAGE respectively.SQL Server 2005Uses the SQL Server 2005 type system. No conversions are made for the current version of ADO.NET.SQL Server 2008Uses the SQL Server 2008 type system.LatestUse the latest version than this client-server pair can handle. This will automatically move forward as the client and server components are upgraded.</returns>
		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060012EA RID: 4842 RVA: 0x0004E67C File Offset: 0x0004C87C
		// (set) Token: 0x060012EB RID: 4843 RVA: 0x0004E684 File Offset: 0x0004C884
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Type System Version")]
		public string TypeSystemVersion
		{
			get
			{
				return this._typeSystemVersion;
			}
			set
			{
				base["Type System Version"] = value;
				this._typeSystemVersion = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether to redirect the connection from the default SQL Server Express instance to a runtime-initiated instance running under the account of the caller.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.UserInstance" /> property, or False if none has been supplied.</returns>
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060012EC RID: 4844 RVA: 0x0004E69C File Offset: 0x0004C89C
		// (set) Token: 0x060012ED RID: 4845 RVA: 0x0004E6A4 File Offset: 0x0004C8A4
		[DisplayName("User Instance")]
		[RefreshProperties(RefreshProperties.All)]
		public bool UserInstance
		{
			get
			{
				return this._userInstance;
			}
			set
			{
				base["User Instance"] = value;
				this._userInstance = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether a client/server or in-process connection to SQL Server should be made.</summary>
		/// <returns>The value of the <see cref="P:System.Data.SqlClient.SqlConnectionStringBuilder.ContextConnection" /> property, or False if none has been supplied.</returns>
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x0004E6C0 File Offset: 0x0004C8C0
		// (set) Token: 0x060012EF RID: 4847 RVA: 0x0004E6C8 File Offset: 0x0004C8C8
		[RefreshProperties(RefreshProperties.All)]
		[DisplayName("Context Connection")]
		public bool ContextConnection
		{
			get
			{
				return this._contextConnection;
			}
			set
			{
				base["Context Connection"] = value;
				this._contextConnection = value;
			}
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x0004E6E4 File Offset: 0x0004C8E4
		private void Init()
		{
			this._applicationName = ".NET SqlClient Data Provider";
			this._asynchronousProcessing = false;
			this._attachDBFilename = string.Empty;
			this._connectionReset = true;
			this._connectTimeout = 15;
			this._currentLanguage = string.Empty;
			this._dataSource = string.Empty;
			this._encrypt = false;
			this._enlist = false;
			this._failoverPartner = string.Empty;
			this._initialCatalog = string.Empty;
			this._integratedSecurity = false;
			this._loadBalanceTimeout = 0;
			this._maxPoolSize = 100;
			this._minPoolSize = 0;
			this._multipleActiveResultSets = false;
			this._networkLibrary = string.Empty;
			this._packetSize = 8000;
			this._password = string.Empty;
			this._persistSecurityInfo = false;
			this._pooling = true;
			this._replication = false;
			this._userID = string.Empty;
			this._workstationID = string.Empty;
			this._trustServerCertificate = false;
			this._typeSystemVersion = "Latest";
			this._userInstance = false;
			this._contextConnection = false;
			this._transactionBinding = "Implicit Unbind";
		}

		/// <summary>Clears the contents of the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> instance.</summary>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060012F1 RID: 4849 RVA: 0x0004E7F4 File Offset: 0x0004C9F4
		public override void Clear()
		{
			base.Clear();
			this.Init();
		}

		/// <summary>Determines whether the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> contains a specific key.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> contains an element that has the specified key; otherwise, false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic)</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012F2 RID: 4850 RVA: 0x0004E804 File Offset: 0x0004CA04
		public override bool ContainsKey(string keyword)
		{
			keyword = keyword.ToUpper().Trim();
			return SqlConnectionStringBuilder._keywords.ContainsKey(keyword) && base.ContainsKey(SqlConnectionStringBuilder._keywords[keyword]);
		}

		/// <summary>Removes the entry with the specified key from the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the key existed within the connection string and was removed; false if the key did not exist.</returns>
		/// <param name="keyword">The key of the key/value pair to be removed from the connection string in this <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> is null (Nothing in Visual Basic)</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012F3 RID: 4851 RVA: 0x0004E844 File Offset: 0x0004CA44
		public override bool Remove(string keyword)
		{
			if (!this.ContainsKey(keyword))
			{
				return false;
			}
			this[keyword] = null;
			return true;
		}

		/// <summary>Indicates whether the specified key exists in this <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> instance.</summary>
		/// <returns>true if the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" /> contains an entry with the specified key; otherwise, false.</returns>
		/// <param name="keyword">The key to locate in the <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060012F4 RID: 4852 RVA: 0x0004E860 File Offset: 0x0004CA60
		[MonoNotSupported("")]
		public override bool ShouldSerialize(string keyword)
		{
			if (!this.ContainsKey(keyword))
			{
				return false;
			}
			keyword = keyword.ToUpper().Trim();
			return !(SqlConnectionStringBuilder._keywords[keyword] == "Password") && base.ShouldSerialize(SqlConnectionStringBuilder._keywords[keyword]);
		}

		/// <summary>Retrieves a value corresponding to the supplied key from this <see cref="T:System.Data.SqlClient.SqlConnectionStringBuilder" />.</summary>
		/// <returns>true if <paramref name="keyword" /> was found within the connection string; otherwise, false.</returns>
		/// <param name="keyword">The key of the item to retrieve.</param>
		/// <param name="value">The value corresponding to <paramref name="keyword." /></param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="keyword" /> contains a null value (Nothing in Visual Basic).</exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060012F5 RID: 4853 RVA: 0x0004E8B8 File Offset: 0x0004CAB8
		public override bool TryGetValue(string keyword, out object value)
		{
			if (!this.ContainsKey(keyword))
			{
				value = string.Empty;
				return false;
			}
			return base.TryGetValue(SqlConnectionStringBuilder._keywords[keyword.ToUpper().Trim()], out value);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x0004E8F8 File Offset: 0x0004CAF8
		private string MapKeyword(string keyword)
		{
			keyword = keyword.ToUpper().Trim();
			if (!SqlConnectionStringBuilder._keywords.ContainsKey(keyword))
			{
				throw new ArgumentException("Keyword not supported :" + keyword);
			}
			return SqlConnectionStringBuilder._keywords[keyword];
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x0004E940 File Offset: 0x0004CB40
		private void SetValue(string key, object value)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key cannot be null!");
			}
			string text = this.MapKeyword(key);
			string text2 = text.ToUpper().Trim();
			if (text2 != null)
			{
				if (SqlConnectionStringBuilder.<>f__switch$map8 == null)
				{
					SqlConnectionStringBuilder.<>f__switch$map8 = new Dictionary<string, int>(26)
					{
						{ "APPLICATION NAME", 0 },
						{ "ATTACHDBFILENAME", 1 },
						{ "CONNECT TIMEOUT", 2 },
						{ "CONNECTION LIFETIME", 3 },
						{ "CONNECTION RESET", 4 },
						{ "CURRENT LANGUAGE", 5 },
						{ "CONTEXT CONNECTION", 6 },
						{ "DATA SOURCE", 7 },
						{ "ENCRYPT", 8 },
						{ "ENLIST", 9 },
						{ "INITIAL CATALOG", 10 },
						{ "INTEGRATED SECURITY", 11 },
						{ "MAX POOL SIZE", 12 },
						{ "MIN POOL SIZE", 13 },
						{ "MULTIPLEACTIVERESULTSETS", 14 },
						{ "ASYNCHRONOUS PROCESSING", 15 },
						{ "NETWORK LIBRARY", 16 },
						{ "LOAD BALANCE TIMEOUT", 17 },
						{ "PACKET SIZE", 18 },
						{ "PASSWORD", 19 },
						{ "PERSIST SECURITY INFO", 20 },
						{ "POOLING", 21 },
						{ "USER ID", 22 },
						{ "USER INSTANCE", 23 },
						{ "WORKSTATION ID", 24 },
						{ "TRANSACTION BINDING", 25 }
					};
				}
				int num;
				if (SqlConnectionStringBuilder.<>f__switch$map8.TryGetValue(text2, out num))
				{
					switch (num)
					{
					case 0:
						if (value == null)
						{
							this._applicationName = ".NET SqlClient Data Provider";
							base.Remove(text);
						}
						else
						{
							this.ApplicationName = value.ToString();
						}
						break;
					case 1:
						throw new NotImplementedException("Attachable database support is not implemented.");
					case 2:
						if (value == null)
						{
							this._connectTimeout = 15;
							base.Remove(text);
						}
						else
						{
							this.ConnectTimeout = DbConnectionStringBuilderHelper.ConvertToInt32(value);
						}
						break;
					case 3:
						break;
					case 4:
						if (value == null)
						{
							this._connectionReset = true;
							base.Remove(text);
						}
						else
						{
							this.ConnectionReset = DbConnectionStringBuilderHelper.ConvertToBoolean(value);
						}
						break;
					case 5:
						if (value == null)
						{
							this._currentLanguage = string.Empty;
							base.Remove(text);
						}
						else
						{
							this.CurrentLanguage = value.ToString();
						}
						break;
					case 6:
						if (value == null)
						{
							this._contextConnection = false;
							base.Remove(text);
						}
						else
						{
							this.ContextConnection = DbConnectionStringBuilderHelper.ConvertToBoolean(value);
						}
						break;
					case 7:
						if (value == null)
						{
							this._dataSource = string.Empty;
							base.Remove(text);
						}
						else
						{
							this.DataSource = value.ToString();
						}
						break;
					case 8:
						if (value == null)
						{
							this._encrypt = false;
							base.Remove(text);
						}
						else if (DbConnectionStringBuilderHelper.ConvertToBoolean(value))
						{
							throw new NotImplementedException("SSL encryption for data sent between client and server is not implemented.");
						}
						break;
					case 9:
						if (value == null)
						{
							this._enlist = false;
							base.Remove(text);
						}
						else if (!DbConnectionStringBuilderHelper.ConvertToBoolean(value))
						{
							throw new NotImplementedException("Disabling the automatic enlistment of connections in the thread's current transaction context is not implemented.");
						}
						break;
					case 10:
						if (value == null)
						{
							this._initialCatalog = string.Empty;
							base.Remove(text);
						}
						else
						{
							this.InitialCatalog = value.ToString();
						}
						break;
					case 11:
						if (value == null)
						{
							this._integratedSecurity = false;
							base.Remove(text);
						}
						else
						{
							this.IntegratedSecurity = DbConnectionStringBuilderHelper.ConvertToBoolean(value);
						}
						break;
					case 12:
						if (value == null)
						{
							this._maxPoolSize = 100;
							base.Remove(text);
						}
						else
						{
							this.MaxPoolSize = DbConnectionStringBuilderHelper.ConvertToInt32(value);
						}
						break;
					case 13:
						if (value == null)
						{
							this._minPoolSize = 0;
							base.Remove(text);
						}
						else
						{
							this.MinPoolSize = DbConnectionStringBuilderHelper.ConvertToInt32(value);
						}
						break;
					case 14:
						if (value == null)
						{
							this._multipleActiveResultSets = false;
							base.Remove(text);
						}
						else if (DbConnectionStringBuilderHelper.ConvertToBoolean(value))
						{
							throw new NotImplementedException("MARS is not yet implemented!");
						}
						break;
					case 15:
						if (value == null)
						{
							this._asynchronousProcessing = false;
							base.Remove(text);
						}
						else
						{
							this.AsynchronousProcessing = DbConnectionStringBuilderHelper.ConvertToBoolean(value);
						}
						break;
					case 16:
						if (value == null)
						{
							this._networkLibrary = string.Empty;
							base.Remove(text);
						}
						else
						{
							if (!value.ToString().ToUpper().Equals("DBMSSOCN"))
							{
								throw new ArgumentException("Unsupported network library.");
							}
							this.NetworkLibrary = value.ToString().ToLower();
						}
						break;
					case 17:
						break;
					case 18:
						if (value == null)
						{
							this._packetSize = 8000;
							base.Remove(text);
						}
						else
						{
							this.PacketSize = DbConnectionStringBuilderHelper.ConvertToInt32(value);
						}
						break;
					case 19:
						if (value == null)
						{
							this._password = string.Empty;
							base.Remove(text);
						}
						else
						{
							this.Password = value.ToString();
						}
						break;
					case 20:
						if (value == null)
						{
							this._persistSecurityInfo = false;
							base.Remove(text);
						}
						else if (DbConnectionStringBuilderHelper.ConvertToBoolean(value))
						{
							throw new NotImplementedException("Persisting security info is not yet implemented");
						}
						break;
					case 21:
						if (value == null)
						{
							this._pooling = true;
							base.Remove(text);
						}
						else
						{
							this.Pooling = DbConnectionStringBuilderHelper.ConvertToBoolean(value);
						}
						break;
					case 22:
						if (value == null)
						{
							this._userID = string.Empty;
							base.Remove(text);
						}
						else
						{
							this.UserID = value.ToString();
						}
						break;
					case 23:
						if (value == null)
						{
							this._userInstance = false;
							base.Remove(text);
						}
						else
						{
							this.UserInstance = DbConnectionStringBuilderHelper.ConvertToBoolean(value);
						}
						break;
					case 24:
						if (value == null)
						{
							this._workstationID = string.Empty;
							base.Remove(text);
						}
						else
						{
							this.WorkstationID = value.ToString();
						}
						break;
					case 25:
						break;
					default:
						goto IL_0655;
					}
					return;
				}
			}
			IL_0655:
			throw new ArgumentException("Keyword not supported :" + key);
		}

		// Token: 0x04000776 RID: 1910
		private const string DEF_APPLICATIONNAME = ".NET SqlClient Data Provider";

		// Token: 0x04000777 RID: 1911
		private const bool DEF_ASYNCHRONOUSPROCESSING = false;

		// Token: 0x04000778 RID: 1912
		private const string DEF_ATTACHDBFILENAME = "";

		// Token: 0x04000779 RID: 1913
		private const bool DEF_CONNECTIONRESET = true;

		// Token: 0x0400077A RID: 1914
		private const int DEF_CONNECTTIMEOUT = 15;

		// Token: 0x0400077B RID: 1915
		private const string DEF_CURRENTLANGUAGE = "";

		// Token: 0x0400077C RID: 1916
		private const string DEF_DATASOURCE = "";

		// Token: 0x0400077D RID: 1917
		private const bool DEF_ENCRYPT = false;

		// Token: 0x0400077E RID: 1918
		private const bool DEF_ENLIST = false;

		// Token: 0x0400077F RID: 1919
		private const string DEF_FAILOVERPARTNER = "";

		// Token: 0x04000780 RID: 1920
		private const string DEF_INITIALCATALOG = "";

		// Token: 0x04000781 RID: 1921
		private const bool DEF_INTEGRATEDSECURITY = false;

		// Token: 0x04000782 RID: 1922
		private const int DEF_LOADBALANCETIMEOUT = 0;

		// Token: 0x04000783 RID: 1923
		private const int DEF_MAXPOOLSIZE = 100;

		// Token: 0x04000784 RID: 1924
		private const int DEF_MINPOOLSIZE = 0;

		// Token: 0x04000785 RID: 1925
		private const bool DEF_MULTIPLEACTIVERESULTSETS = false;

		// Token: 0x04000786 RID: 1926
		private const string DEF_NETWORKLIBRARY = "";

		// Token: 0x04000787 RID: 1927
		private const int DEF_PACKETSIZE = 8000;

		// Token: 0x04000788 RID: 1928
		private const string DEF_PASSWORD = "";

		// Token: 0x04000789 RID: 1929
		private const bool DEF_PERSISTSECURITYINFO = false;

		// Token: 0x0400078A RID: 1930
		private const bool DEF_POOLING = true;

		// Token: 0x0400078B RID: 1931
		private const bool DEF_REPLICATION = false;

		// Token: 0x0400078C RID: 1932
		private const string DEF_USERID = "";

		// Token: 0x0400078D RID: 1933
		private const string DEF_WORKSTATIONID = "";

		// Token: 0x0400078E RID: 1934
		private const string DEF_TYPESYSTEMVERSION = "Latest";

		// Token: 0x0400078F RID: 1935
		private const bool DEF_TRUSTSERVERCERTIFICATE = false;

		// Token: 0x04000790 RID: 1936
		private const bool DEF_USERINSTANCE = false;

		// Token: 0x04000791 RID: 1937
		private const bool DEF_CONTEXTCONNECTION = false;

		// Token: 0x04000792 RID: 1938
		private const string DEF_TRANSACTIONBINDING = "Implicit Unbind";

		// Token: 0x04000793 RID: 1939
		private string _applicationName;

		// Token: 0x04000794 RID: 1940
		private bool _asynchronousProcessing;

		// Token: 0x04000795 RID: 1941
		private string _attachDBFilename;

		// Token: 0x04000796 RID: 1942
		private bool _connectionReset;

		// Token: 0x04000797 RID: 1943
		private int _connectTimeout;

		// Token: 0x04000798 RID: 1944
		private string _currentLanguage;

		// Token: 0x04000799 RID: 1945
		private string _dataSource;

		// Token: 0x0400079A RID: 1946
		private bool _encrypt;

		// Token: 0x0400079B RID: 1947
		private bool _enlist;

		// Token: 0x0400079C RID: 1948
		private string _failoverPartner;

		// Token: 0x0400079D RID: 1949
		private string _initialCatalog;

		// Token: 0x0400079E RID: 1950
		private bool _integratedSecurity;

		// Token: 0x0400079F RID: 1951
		private int _loadBalanceTimeout;

		// Token: 0x040007A0 RID: 1952
		private int _maxPoolSize;

		// Token: 0x040007A1 RID: 1953
		private int _minPoolSize;

		// Token: 0x040007A2 RID: 1954
		private bool _multipleActiveResultSets;

		// Token: 0x040007A3 RID: 1955
		private string _networkLibrary;

		// Token: 0x040007A4 RID: 1956
		private int _packetSize;

		// Token: 0x040007A5 RID: 1957
		private string _password;

		// Token: 0x040007A6 RID: 1958
		private bool _persistSecurityInfo;

		// Token: 0x040007A7 RID: 1959
		private bool _pooling;

		// Token: 0x040007A8 RID: 1960
		private bool _replication;

		// Token: 0x040007A9 RID: 1961
		private string _userID;

		// Token: 0x040007AA RID: 1962
		private string _workstationID;

		// Token: 0x040007AB RID: 1963
		private bool _trustServerCertificate;

		// Token: 0x040007AC RID: 1964
		private string _typeSystemVersion;

		// Token: 0x040007AD RID: 1965
		private bool _userInstance;

		// Token: 0x040007AE RID: 1966
		private bool _contextConnection;

		// Token: 0x040007AF RID: 1967
		private string _transactionBinding;

		// Token: 0x040007B0 RID: 1968
		private static Dictionary<string, string> _keywords = new Dictionary<string, string>();

		// Token: 0x040007B1 RID: 1969
		private static Dictionary<string, object> _defaults;
	}
}
