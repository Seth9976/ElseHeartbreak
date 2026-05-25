using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Marks the attributed class as an event class. This class cannot be inherited.</summary>
	// Token: 0x02000015 RID: 21
	[AttributeUsage(AttributeTargets.Class)]
	[ComVisible(false)]
	public sealed class EventClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.EventClassAttribute" /> class.</summary>
		// Token: 0x06000057 RID: 87 RVA: 0x0000247C File Offset: 0x0000067C
		public EventClassAttribute()
		{
			this.allowInProcSubscribers = true;
			this.fireInParallel = false;
			this.publisherFilter = null;
		}

		/// <summary>Gets or sets a value that indicates whether subscribers can be activated in the publisher's process.</summary>
		/// <returns>true if subscribers can be activated in the publisher's process; otherwise, false.</returns>
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000058 RID: 88 RVA: 0x0000249C File Offset: 0x0000069C
		// (set) Token: 0x06000059 RID: 89 RVA: 0x000024A4 File Offset: 0x000006A4
		public bool AllowInprocSubscribers
		{
			get
			{
				return this.allowInProcSubscribers;
			}
			set
			{
				this.allowInProcSubscribers = value;
			}
		}

		/// <summary>Gets or sets a value that indicates whether events are to be delivered to subscribers in parallel.</summary>
		/// <returns>true if events are to be delivered to subscribers in parallel; otherwise, false.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000024B0 File Offset: 0x000006B0
		// (set) Token: 0x0600005B RID: 91 RVA: 0x000024B8 File Offset: 0x000006B8
		public bool FireInParallel
		{
			get
			{
				return this.fireInParallel;
			}
			set
			{
				this.fireInParallel = value;
			}
		}

		/// <summary>Gets or sets a publisher filter for an event method.</summary>
		/// <returns>The publisher filter.</returns>
		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000024C4 File Offset: 0x000006C4
		// (set) Token: 0x0600005D RID: 93 RVA: 0x000024CC File Offset: 0x000006CC
		public string PublisherFilter
		{
			get
			{
				return this.publisherFilter;
			}
			set
			{
				this.publisherFilter = value;
			}
		}

		// Token: 0x04000043 RID: 67
		private bool allowInProcSubscribers;

		// Token: 0x04000044 RID: 68
		private bool fireInParallel;

		// Token: 0x04000045 RID: 69
		private string publisherFilter;
	}
}
