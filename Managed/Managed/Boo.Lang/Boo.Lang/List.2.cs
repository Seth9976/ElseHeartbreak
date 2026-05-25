using System;
using System.Collections;

namespace Boo.Lang
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class List : List<object>
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00003EE8 File Offset: 0x000020E8
		public List()
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003EF0 File Offset: 0x000020F0
		public List(IEnumerable enumerable)
			: base(enumerable)
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003EFC File Offset: 0x000020FC
		public List(int initialCapacity)
			: base(initialCapacity)
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003F08 File Offset: 0x00002108
		public List(object[] items, bool takeOwnership)
			: base(items, takeOwnership)
		{
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00003F14 File Offset: 0x00002114
		public object Find(Predicate<object> predicate)
		{
			object obj;
			return (!base.Find(predicate, out obj)) ? null : obj;
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00003F38 File Offset: 0x00002138
		protected override List<object> NewConcreteList(object[] items, bool takeOwnership)
		{
			return new List(items, takeOwnership);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00003F44 File Offset: 0x00002144
		public Array ToArray(Type targetType)
		{
			Array array = Array.CreateInstance(targetType, this._count);
			Array.Copy(this._items, 0, array, 0, this._count);
			return array;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003F74 File Offset: 0x00002174
		public static string operator %(string format, List rhs)
		{
			return string.Format(format, rhs.ToArray());
		}
	}
}
