using System;

namespace System.ComponentModel
{
	// Token: 0x020001C3 RID: 451
	internal sealed class WeakObjectWrapper
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x00029914 File Offset: 0x00027B14
		public WeakObjectWrapper(object target)
		{
			this.TargetHashCode = target.GetHashCode();
			this.Weak = new WeakReference(target);
		}

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00029940 File Offset: 0x00027B40
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00029948 File Offset: 0x00027B48
		public int TargetHashCode { get; private set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00029954 File Offset: 0x00027B54
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x0002995C File Offset: 0x00027B5C
		public WeakReference Weak { get; private set; }
	}
}
