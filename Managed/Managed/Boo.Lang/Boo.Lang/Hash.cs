using System;
using System.Collections;
using System.Runtime.Serialization;
using Boo.Lang.Runtime;

namespace Boo.Lang
{
	// Token: 0x0200001B RID: 27
	[EnumeratorItemType(typeof(DictionaryEntry))]
	[Serializable]
	public class Hash : Hashtable, IEquatable<Hash>
	{
		// Token: 0x060000B9 RID: 185 RVA: 0x00003C34 File Offset: 0x00001E34
		public Hash()
			: base(BooHashCodeProvider.Default)
		{
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003C44 File Offset: 0x00001E44
		public Hash(IDictionary other)
			: this()
		{
			if (other == null)
			{
				throw new ArgumentNullException("other");
			}
			foreach (object obj in other)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				this.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00003CD4 File Offset: 0x00001ED4
		public Hash(IEnumerable enumerable)
			: this()
		{
			if (enumerable == null)
			{
				throw new ArgumentNullException("enumerable");
			}
			foreach (object obj in enumerable)
			{
				Array array = (Array)obj;
				this.Add(array.GetValue(0), array.GetValue(1));
			}
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003D64 File Offset: 0x00001F64
		public Hash(bool caseInsensitive)
			: base(StringComparer.InvariantCultureIgnoreCase)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00003D74 File Offset: 0x00001F74
		public Hash(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003D80 File Offset: 0x00001F80
		public override object Clone()
		{
			return new Hash(this);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003D88 File Offset: 0x00001F88
		public override bool Equals(object other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			Hash hash = other as Hash;
			return this.Equals(hash);
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00003DB4 File Offset: 0x00001FB4
		public bool Equals(Hash other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (this.Count != other.Count)
			{
				return false;
			}
			foreach (object obj in other)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				if (!this.ContainsKey(dictionaryEntry.Key))
				{
					return false;
				}
				if (!RuntimeServices.EqualityOperator(dictionaryEntry.Value, this[dictionaryEntry.Key]))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003E7C File Offset: 0x0000207C
		public override int GetHashCode()
		{
			int num = 0;
			foreach (object obj in this)
			{
				num ^= this.GetHash(obj);
			}
			return num;
		}
	}
}
