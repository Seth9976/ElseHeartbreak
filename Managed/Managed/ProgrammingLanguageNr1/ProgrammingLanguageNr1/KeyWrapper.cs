using System;

namespace ProgrammingLanguageNr1
{
	// Token: 0x02000028 RID: 40
	public struct KeyWrapper : IComparable<KeyWrapper>
	{
		// Token: 0x06000162 RID: 354 RVA: 0x0000AB84 File Offset: 0x00008D84
		public KeyWrapper(object o)
		{
			this.value = o;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000AB90 File Offset: 0x00008D90
		public override int GetHashCode()
		{
			if (this.value.GetType() == typeof(int))
			{
				return (int)this.value;
			}
			if (this.value.GetType() == typeof(float))
			{
				return (int)((float)this.value);
			}
			if (this.value.GetType() == typeof(bool))
			{
				if ((bool)this.value)
				{
					return 9998;
				}
				return 9999;
			}
			else
			{
				if (this.value == typeof(string))
				{
					return 10000 + ((string)this.value).GetHashCode() % 10000;
				}
				return 20000 + base.GetHashCode() % 10000;
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000AC70 File Offset: 0x00008E70
		public int CompareTo(KeyWrapper pOther)
		{
			return this.GetHashCode() - pOther.GetHashCode();
		}

		// Token: 0x040000B9 RID: 185
		public object value;
	}
}
