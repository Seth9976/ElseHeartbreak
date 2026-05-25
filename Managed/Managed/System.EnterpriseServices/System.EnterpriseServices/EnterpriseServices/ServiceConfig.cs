using System;
using System.Runtime.InteropServices;
using System.Transactions;

namespace System.EnterpriseServices
{
	/// <summary>Specifies and configures the services that are to be active in the domain which is entered when calling <see cref="M:System.EnterpriseServices.ServiceDomain.Enter(System.EnterpriseServices.ServiceConfig)" /> or creating an <see cref="T:System.EnterpriseServices.Activity" />. This class cannot be inherited.</summary>
	// Token: 0x02000041 RID: 65
	[ComVisible(false)]
	[MonoTODO]
	public sealed class ServiceConfig
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ServiceConfig" /> class, setting the properties to configure the desired services.</summary>
		/// <exception cref="T:System.PlatformNotSupportedException">
		///   <see cref="T:System.EnterpriseServices.ServiceConfig" /> is not supported on the current platform. </exception>
		// Token: 0x06000102 RID: 258 RVA: 0x00002A08 File Offset: 0x00000C08
		[MonoTODO]
		public ServiceConfig()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets the binding option, which indicates whether all work submitted by the activity is to be bound to only one single-threaded apartment (STA).</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.BindingOption" /> values. The default is <see cref="F:System.EnterpriseServices.BindingOption.NoBinding" />.</returns>
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000103 RID: 259 RVA: 0x00002A18 File Offset: 0x00000C18
		// (set) Token: 0x06000104 RID: 260 RVA: 0x00002A20 File Offset: 0x00000C20
		[MonoTODO]
		public BindingOption Binding
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Transactions.Transaction" /> that represents an existing transaction that supplies the settings used to run the transaction identified by <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
		/// <returns>A <see cref="T:System.Transactions.Transaction" />. The default is null.</returns>
		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000105 RID: 261 RVA: 0x00002A28 File Offset: 0x00000C28
		// (set) Token: 0x06000106 RID: 262 RVA: 0x00002A30 File Offset: 0x00000C30
		[MonoTODO]
		public Transaction BringYourOwnSystemTransaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.EnterpriseServices.ITransaction" /> that represents an existing transaction that supplies the settings used to run the transaction identified by <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
		/// <returns>An <see cref="T:System.EnterpriseServices.ITransaction" />. The default is null.</returns>
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00002A38 File Offset: 0x00000C38
		// (set) Token: 0x06000108 RID: 264 RVA: 0x00002A40 File Offset: 0x00000C40
		[MonoTODO]
		public ITransaction BringYourOwnTransaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether COM Transaction Integrator (COMTI) intrinsics are enabled.</summary>
		/// <returns>true if COMTI intrinsics are enabled; otherwise, false. The default is false.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00002A48 File Offset: 0x00000C48
		// (set) Token: 0x0600010A RID: 266 RVA: 0x00002A50 File Offset: 0x00000C50
		[MonoTODO]
		public bool COMTIIntrinsicsEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether Internet Information Services (IIS) intrinsics are enabled.</summary>
		/// <returns>true if IIS intrinsics are enabled; otherwise, false. The default is false.</returns>
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00002A58 File Offset: 0x00000C58
		// (set) Token: 0x0600010C RID: 268 RVA: 0x00002A60 File Offset: 0x00000C60
		[MonoTODO]
		public bool IISIntrinsicsEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether to construct a new context based on the current context or to create a new context based solely on the information in <see cref="T:System.EnterpriseServices.ServiceConfig" />.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.InheritanceOption" /> values. The default is <see cref="F:System.EnterpriseServices.InheritanceOption.Inherit" />.</returns>
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600010D RID: 269 RVA: 0x00002A68 File Offset: 0x00000C68
		// (set) Token: 0x0600010E RID: 270 RVA: 0x00002A70 File Offset: 0x00000C70
		[MonoTODO]
		public InheritanceOption Inheritance
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the isolation level of the transaction.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionIsolationLevel" /> values. The default is <see cref="F:System.EnterpriseServices.TransactionIsolationLevel.Any" />.</returns>
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600010F RID: 271 RVA: 0x00002A78 File Offset: 0x00000C78
		// (set) Token: 0x06000110 RID: 272 RVA: 0x00002A80 File Offset: 0x00000C80
		[MonoTODO]
		public TransactionIsolationLevel IsolationLevel
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the GUID for the COM+ partition that is to be used.</summary>
		/// <returns>The GUID for the partition to be used. The default is a zero GUID.</returns>
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00002A88 File Offset: 0x00000C88
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00002A90 File Offset: 0x00000C90
		[MonoTODO]
		public Guid PartitionId
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates how partitions are used for the enclosed work.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.PartitionOption" /> values. The default is <see cref="F:System.EnterpriseServices.PartitionOption.Ignore" />.</returns>
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00002A98 File Offset: 0x00000C98
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00002AA0 File Offset: 0x00000CA0
		[MonoTODO]
		public PartitionOption PartitionOption
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the directory for the side-by-side assembly for the enclosed work.</summary>
		/// <returns>The name of the directory to be used for the side-by-side assembly. The default value is null.</returns>
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000115 RID: 277 RVA: 0x00002AA8 File Offset: 0x00000CA8
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00002AB0 File Offset: 0x00000CB0
		[MonoTODO]
		public string SxsDirectory
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the file name of the side-by-side assembly for the enclosed work.</summary>
		/// <returns>The file name of the side-by-side assembly. The default value is null.</returns>
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000117 RID: 279 RVA: 0x00002AB8 File Offset: 0x00000CB8
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00002AC0 File Offset: 0x00000CC0
		[MonoTODO]
		public string SxsName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates how to configure the side-by-side assembly.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.SxsOption" /> values. The default is <see cref="F:System.EnterpriseServices.SxsOption.Ignore" />.</returns>
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00002AC8 File Offset: 0x00000CC8
		// (set) Token: 0x0600011A RID: 282 RVA: 0x00002AD0 File Offset: 0x00000CD0
		[MonoTODO]
		public SxsOption SxsOption
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value in that indicates the type of automatic synchronization requested by the component.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.SynchronizationOption" /> values. The default is <see cref="F:System.EnterpriseServices.SynchronizationOption.Disabled" />.</returns>
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00002AD8 File Offset: 0x00000CD8
		// (set) Token: 0x0600011C RID: 284 RVA: 0x00002AE0 File Offset: 0x00000CE0
		[MonoTODO]
		public SynchronizationOption Synchronization
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates the thread pool which runs the work submitted by the activity.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.ThreadPoolOption" /> values. The default is <see cref="F:System.EnterpriseServices.ThreadPoolOption.None" />.</returns>
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600011D RID: 285 RVA: 0x00002AE8 File Offset: 0x00000CE8
		// (set) Token: 0x0600011E RID: 286 RVA: 0x00002AF0 File Offset: 0x00000CF0
		[MonoTODO]
		public ThreadPoolOption ThreadPool
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the Transaction Internet Protocol (TIP) URL that allows the enclosed code to run in an existing transaction.</summary>
		/// <returns>A TIP URL. The default value is null.</returns>
		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00002AF8 File Offset: 0x00000CF8
		// (set) Token: 0x06000120 RID: 288 RVA: 0x00002B00 File Offset: 0x00000D00
		[MonoTODO]
		public string TipUrl
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a text string that corresponds to the application ID under which tracker information is reported.</summary>
		/// <returns>The application ID under which tracker information is reported. The default value is null.</returns>
		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000121 RID: 289 RVA: 0x00002B08 File Offset: 0x00000D08
		// (set) Token: 0x06000122 RID: 290 RVA: 0x00002B10 File Offset: 0x00000D10
		[MonoTODO]
		public string TrackingAppName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a text string that corresponds to the context name under which tracker information is reported.</summary>
		/// <returns>The context name under which tracker information is reported. The default value is null.</returns>
		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000123 RID: 291 RVA: 0x00002B18 File Offset: 0x00000D18
		// (set) Token: 0x06000124 RID: 292 RVA: 0x00002B20 File Offset: 0x00000D20
		[MonoTODO]
		public string TrackingComponentName
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates whether tracking is enabled.</summary>
		/// <returns>true if tracking is enabled; otherwise, false. The default is false.</returns>
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000125 RID: 293 RVA: 0x00002B28 File Offset: 0x00000D28
		// (set) Token: 0x06000126 RID: 294 RVA: 0x00002B30 File Offset: 0x00000D30
		[MonoTODO]
		public bool TrackingEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets a value that indicates how transactions are used in the enclosed work.</summary>
		/// <returns>One of the <see cref="T:System.EnterpriseServices.TransactionOption" /> values. The default is <see cref="F:System.EnterpriseServices.TransactionOption.Disabled" />.</returns>
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000127 RID: 295 RVA: 0x00002B38 File Offset: 0x00000D38
		// (set) Token: 0x06000128 RID: 296 RVA: 0x00002B40 File Offset: 0x00000D40
		[MonoTODO]
		public TransactionOption Transaction
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the name that is used when transaction statistics are displayed.</summary>
		/// <returns>The name used when transaction statistics are displayed. The default value is null.</returns>
		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000129 RID: 297 RVA: 0x00002B48 File Offset: 0x00000D48
		// (set) Token: 0x0600012A RID: 298 RVA: 0x00002B50 File Offset: 0x00000D50
		[MonoTODO]
		public string TransactionDescription
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the transaction time-out for a new transaction.</summary>
		/// <returns>The transaction time-out, in seconds.</returns>
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600012B RID: 299 RVA: 0x00002B58 File Offset: 0x00000D58
		// (set) Token: 0x0600012C RID: 300 RVA: 0x00002B60 File Offset: 0x00000D60
		[MonoTODO]
		public int TransactionTimeout
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}
	}
}
