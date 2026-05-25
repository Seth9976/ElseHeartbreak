using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000053 RID: 83
	[StructLayout(LayoutKind.Sequential)]
	public class TrackedReference
	{
		// Token: 0x060001AE RID: 430 RVA: 0x000083B0 File Offset: 0x000065B0
		protected TrackedReference()
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000083B8 File Offset: 0x000065B8
		public override bool Equals(object o)
		{
			return o as TrackedReference == this;
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000083C8 File Offset: 0x000065C8
		public override int GetHashCode()
		{
			return (int)this.m_Ptr;
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000083D8 File Offset: 0x000065D8
		public static bool operator ==(TrackedReference x, TrackedReference y)
		{
			if (y == null && x == null)
			{
				return true;
			}
			if (y == null)
			{
				return x.m_Ptr == IntPtr.Zero;
			}
			if (x == null)
			{
				return y.m_Ptr == IntPtr.Zero;
			}
			return x.m_Ptr == y.m_Ptr;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00008438 File Offset: 0x00006638
		public static bool operator !=(TrackedReference x, TrackedReference y)
		{
			return !(x == y);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00008444 File Offset: 0x00006644
		public static implicit operator bool(TrackedReference exists)
		{
			return exists != null;
		}

		// Token: 0x0400016B RID: 363
		[NotRenamed]
		internal IntPtr m_Ptr;
	}
}
