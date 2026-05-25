using System;
using System.Collections;
using System.Collections.Generic;

namespace Boo.Lang
{
	// Token: 0x02000005 RID: 5
	[Serializable]
	public class BooHashCodeProvider : IEqualityComparer, IEqualityComparer<object>
	{
		// Token: 0x0600000D RID: 13 RVA: 0x00002264 File Offset: 0x00000464
		private BooHashCodeProvider()
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002278 File Offset: 0x00000478
		public int GetHashCode(object o)
		{
			if (o == null)
			{
				return 0;
			}
			Array array = o as Array;
			return (array == null) ? o.GetHashCode() : this.GetArrayHashCode(array);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022AC File Offset: 0x000004AC
		public bool Equals(object lhs, object rhs)
		{
			return BooComparer.Default.Compare(lhs, rhs) == 0;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000022C0 File Offset: 0x000004C0
		public int GetArrayHashCode(Array array)
		{
			int num = 1;
			int num2 = 0;
			foreach (object obj in array)
			{
				num ^= this.GetHashCode(obj) * ++num2;
			}
			return num;
		}

		// Token: 0x04000004 RID: 4
		public static readonly IEqualityComparer Default = new BooHashCodeProvider();
	}
}
