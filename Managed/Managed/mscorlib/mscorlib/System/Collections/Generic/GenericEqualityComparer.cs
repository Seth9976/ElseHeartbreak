using System;

namespace System.Collections.Generic
{
	// Token: 0x020006CA RID: 1738
	[Serializable]
	internal sealed class GenericEqualityComparer<T> : EqualityComparer<T> where T : IEquatable<T>
	{
		// Token: 0x06004282 RID: 17026 RVA: 0x000E3DAC File Offset: 0x000E1FAC
		public override int GetHashCode(T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x06004283 RID: 17027 RVA: 0x000E3DC8 File Offset: 0x000E1FC8
		public override bool Equals(T x, T y)
		{
			if (x == null)
			{
				return y == null;
			}
			return x.Equals(y);
		}
	}
}
