using System;

namespace UnityEngine.Serialization
{
	// Token: 0x0200006C RID: 108
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	public class FormerlySerializedAsAttribute : Attribute
	{
		// Token: 0x06000284 RID: 644 RVA: 0x0000A668 File Offset: 0x00008868
		public FormerlySerializedAsAttribute(string oldName)
		{
			this.m_oldName = oldName;
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000285 RID: 645 RVA: 0x0000A678 File Offset: 0x00008878
		public string oldName
		{
			get
			{
				return this.m_oldName;
			}
		}

		// Token: 0x0400019C RID: 412
		private string m_oldName;
	}
}
