using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Sets the queuing exception class for the queued class. This class cannot be inherited.</summary>
	// Token: 0x02000017 RID: 23
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ExceptionClassAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ExceptionClassAttribute" /> class.</summary>
		/// <param name="name">The name of the exception class for the player to activate and play back before the message is routed to the dead letter queue. </param>
		// Token: 0x06000061 RID: 97 RVA: 0x00002500 File Offset: 0x00000700
		public ExceptionClassAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Gets the name of the exception class for the player to activate and play back before the message is routed to the dead letter queue.</summary>
		/// <returns>The name of the exception class for the player to activate and play back before the message is routed to the dead letter queue.</returns>
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002510 File Offset: 0x00000710
		public string Value
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000047 RID: 71
		private string name;
	}
}
