using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Enables queuing support for the marked assembly and enables the application to read method calls from Message Queuing queues. This class cannot be inherited.</summary>
	// Token: 0x0200000A RID: 10
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class ApplicationQueuingAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ApplicationQueuingAttribute" /> class, enabling queuing support for the assembly and initializing <see cref="P:System.EnterpriseServices.ApplicationQueuingAttribute.Enabled" />, <see cref="P:System.EnterpriseServices.ApplicationQueuingAttribute.QueueListenerEnabled" />, and <see cref="P:System.EnterpriseServices.ApplicationQueuingAttribute.MaxListenerThreads" />.</summary>
		// Token: 0x06000026 RID: 38 RVA: 0x00002270 File Offset: 0x00000470
		public ApplicationQueuingAttribute()
		{
			this.enabled = true;
			this.queueListenerEnabled = false;
			this.maxListenerThreads = 0;
		}

		/// <summary>Gets or sets a value indicating whether queuing support is enabled.</summary>
		/// <returns>true if queuing support is enabled; otherwise, false. The default value set by the constructor is true.</returns>
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002290 File Offset: 0x00000490
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002298 File Offset: 0x00000498
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				this.enabled = value;
			}
		}

		/// <summary>Gets or sets the number of threads used to extract messages from the queue and activate the corresponding component.</summary>
		/// <returns>The maximum number of threads to use for processing messages arriving in the queue. The default is zero.</returns>
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000022A4 File Offset: 0x000004A4
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000022AC File Offset: 0x000004AC
		public int MaxListenerThreads
		{
			get
			{
				return this.maxListenerThreads;
			}
			set
			{
				this.maxListenerThreads = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the application will accept queued component calls from clients.</summary>
		/// <returns>true if the application accepts queued component calls; otherwise, false. The default is false.</returns>
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000022B8 File Offset: 0x000004B8
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000022C0 File Offset: 0x000004C0
		public bool QueueListenerEnabled
		{
			get
			{
				return this.queueListenerEnabled;
			}
			set
			{
				this.queueListenerEnabled = value;
			}
		}

		// Token: 0x0400002D RID: 45
		private bool enabled;

		// Token: 0x0400002E RID: 46
		private int maxListenerThreads;

		// Token: 0x0400002F RID: 47
		private bool queueListenerEnabled;
	}
}
