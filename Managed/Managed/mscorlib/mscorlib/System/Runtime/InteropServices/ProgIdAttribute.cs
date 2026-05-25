using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Allows the user to specify the ProgID of a class.</summary>
	// Token: 0x020003B6 RID: 950
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	[ComVisible(true)]
	public sealed class ProgIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the ProgIdAttribute with the specified ProgID.</summary>
		/// <param name="progId">The ProgID to be assigned to the class. </param>
		// Token: 0x06002B57 RID: 11095 RVA: 0x0009384C File Offset: 0x00091A4C
		public ProgIdAttribute(string progId)
		{
			this.pid = progId;
		}

		/// <summary>Gets the ProgID of the class.</summary>
		/// <returns>The ProgID of the class.</returns>
		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x06002B58 RID: 11096 RVA: 0x0009385C File Offset: 0x00091A5C
		public string Value
		{
			get
			{
				return this.pid;
			}
		}

		// Token: 0x04001192 RID: 4498
		private string pid;
	}
}
