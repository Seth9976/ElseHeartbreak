using System;
using System.Collections.Generic;

namespace System.ComponentModel
{
	// Token: 0x020001C4 RID: 452
	internal sealed class WeakObjectWrapperComparer : EqualityComparer<WeakObjectWrapper>
	{
		// Token: 0x06000FDA RID: 4058 RVA: 0x00029970 File Offset: 0x00027B70
		public override bool Equals(WeakObjectWrapper x, WeakObjectWrapper y)
		{
			if (x == null && y == null)
			{
				return false;
			}
			if (x == null || y == null)
			{
				return false;
			}
			WeakReference weak = x.Weak;
			WeakReference weak2 = y.Weak;
			return (weak.IsAlive || weak2.IsAlive) && weak.Target == weak2.Target;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x000299D0 File Offset: 0x00027BD0
		public override int GetHashCode(WeakObjectWrapper obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.TargetHashCode;
		}
	}
}
