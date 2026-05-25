using System;

namespace System.Collections.Specialized
{
	// Token: 0x020000BF RID: 191
	internal class ProcessStringDictionary : StringDictionary, IEnumerable
	{
		// Token: 0x0600083E RID: 2110 RVA: 0x00019194 File Offset: 0x00017394
		public ProcessStringDictionary()
		{
			IHashCodeProvider hashCodeProvider = null;
			IComparer comparer = null;
			int platform = (int)Environment.OSVersion.Platform;
			if (platform != 4 && platform != 128)
			{
				hashCodeProvider = CaseInsensitiveHashCodeProvider.DefaultInvariant;
				comparer = CaseInsensitiveComparer.DefaultInvariant;
			}
			this.table = new Hashtable(hashCodeProvider, comparer);
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x000191E4 File Offset: 0x000173E4
		public override int Count
		{
			get
			{
				return this.table.Count;
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x06000840 RID: 2112 RVA: 0x000191F4 File Offset: 0x000173F4
		public override bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170001C8 RID: 456
		public override string this[string key]
		{
			get
			{
				return (string)this.table[key];
			}
			set
			{
				this.table[key] = value;
			}
		}

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0001921C File Offset: 0x0001741C
		public override ICollection Keys
		{
			get
			{
				return this.table.Keys;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x06000844 RID: 2116 RVA: 0x0001922C File Offset: 0x0001742C
		public override ICollection Values
		{
			get
			{
				return this.table.Values;
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0001923C File Offset: 0x0001743C
		public override object SyncRoot
		{
			get
			{
				return this.table.SyncRoot;
			}
		}

		// Token: 0x06000846 RID: 2118 RVA: 0x0001924C File Offset: 0x0001744C
		public override void Add(string key, string value)
		{
			this.table.Add(key, value);
		}

		// Token: 0x06000847 RID: 2119 RVA: 0x0001925C File Offset: 0x0001745C
		public override void Clear()
		{
			this.table.Clear();
		}

		// Token: 0x06000848 RID: 2120 RVA: 0x0001926C File Offset: 0x0001746C
		public override bool ContainsKey(string key)
		{
			return this.table.ContainsKey(key);
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001927C File Offset: 0x0001747C
		public override bool ContainsValue(string value)
		{
			return this.table.ContainsValue(value);
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001928C File Offset: 0x0001748C
		public override void CopyTo(Array array, int index)
		{
			this.table.CopyTo(array, index);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001929C File Offset: 0x0001749C
		public override IEnumerator GetEnumerator()
		{
			return this.table.GetEnumerator();
		}

		// Token: 0x0600084C RID: 2124 RVA: 0x000192AC File Offset: 0x000174AC
		public override void Remove(string key)
		{
			this.table.Remove(key);
		}

		// Token: 0x04000232 RID: 562
		private Hashtable table;
	}
}
