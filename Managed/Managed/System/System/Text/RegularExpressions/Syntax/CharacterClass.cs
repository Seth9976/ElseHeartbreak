using System;
using System.Collections;

namespace System.Text.RegularExpressions.Syntax
{
	// Token: 0x020004A8 RID: 1192
	internal class CharacterClass : Expression
	{
		// Token: 0x06002AC5 RID: 10949 RVA: 0x00093140 File Offset: 0x00091340
		public CharacterClass(bool negate, bool ignore)
		{
			this.negate = negate;
			this.ignore = ignore;
			this.intervals = new IntervalCollection();
			int num = 144;
			this.pos_cats = new BitArray(num);
			this.neg_cats = new BitArray(num);
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x0009318C File Offset: 0x0009138C
		public CharacterClass(Category cat, bool negate)
			: this(false, false)
		{
			this.AddCategory(cat, negate);
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x000931B0 File Offset: 0x000913B0
		// (set) Token: 0x06002AC9 RID: 10953 RVA: 0x000931B8 File Offset: 0x000913B8
		public bool Negate
		{
			get
			{
				return this.negate;
			}
			set
			{
				this.negate = value;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x000931C4 File Offset: 0x000913C4
		// (set) Token: 0x06002ACB RID: 10955 RVA: 0x000931CC File Offset: 0x000913CC
		public bool IgnoreCase
		{
			get
			{
				return this.ignore;
			}
			set
			{
				this.ignore = value;
			}
		}

		// Token: 0x06002ACC RID: 10956 RVA: 0x000931D8 File Offset: 0x000913D8
		public void AddCategory(Category cat, bool negate)
		{
			if (negate)
			{
				this.neg_cats[(int)cat] = true;
			}
			else
			{
				this.pos_cats[(int)cat] = true;
			}
		}

		// Token: 0x06002ACD RID: 10957 RVA: 0x0009320C File Offset: 0x0009140C
		public void AddCharacter(char c)
		{
			this.AddRange(c, c);
		}

		// Token: 0x06002ACE RID: 10958 RVA: 0x00093218 File Offset: 0x00091418
		public void AddRange(char lo, char hi)
		{
			Interval interval = new Interval((int)lo, (int)hi);
			if (this.ignore)
			{
				if (CharacterClass.upper_case_characters.Intersects(interval))
				{
					Interval interval2;
					if (interval.low < CharacterClass.upper_case_characters.low)
					{
						interval2 = new Interval(CharacterClass.upper_case_characters.low + 32, interval.high + 32);
						interval.high = CharacterClass.upper_case_characters.low - 1;
					}
					else
					{
						interval2 = new Interval(interval.low + 32, CharacterClass.upper_case_characters.high + 32);
						interval.low = CharacterClass.upper_case_characters.high + 1;
					}
					this.intervals.Add(interval2);
				}
				else if (CharacterClass.upper_case_characters.Contains(interval))
				{
					interval.high += 32;
					interval.low += 32;
				}
			}
			this.intervals.Add(interval);
		}

		// Token: 0x06002ACF RID: 10959 RVA: 0x00093314 File Offset: 0x00091514
		public override void Compile(ICompiler cmp, bool reverse)
		{
			IntervalCollection metaCollection = this.intervals.GetMetaCollection(new IntervalCollection.CostDelegate(CharacterClass.GetIntervalCost));
			int num = metaCollection.Count;
			for (int i = 0; i < this.pos_cats.Length; i++)
			{
				if (this.pos_cats[i] || this.neg_cats[i])
				{
					num++;
				}
			}
			if (num == 0)
			{
				return;
			}
			LinkRef linkRef = cmp.NewLink();
			if (num > 1)
			{
				cmp.EmitIn(linkRef);
			}
			foreach (object obj in metaCollection)
			{
				Interval interval = (Interval)obj;
				if (interval.IsDiscontiguous)
				{
					BitArray bitArray = new BitArray(interval.Size);
					foreach (object obj2 in this.intervals)
					{
						Interval interval2 = (Interval)obj2;
						if (interval.Contains(interval2))
						{
							for (int j = interval2.low; j <= interval2.high; j++)
							{
								bitArray[j - interval.low] = true;
							}
						}
					}
					cmp.EmitSet((char)interval.low, bitArray, this.negate, this.ignore, reverse);
				}
				else if (interval.IsSingleton)
				{
					cmp.EmitCharacter((char)interval.low, this.negate, this.ignore, reverse);
				}
				else
				{
					cmp.EmitRange((char)interval.low, (char)interval.high, this.negate, this.ignore, reverse);
				}
			}
			for (int k = 0; k < this.pos_cats.Length; k++)
			{
				if (this.pos_cats[k])
				{
					if (this.neg_cats[k])
					{
						cmp.EmitCategory(Category.AnySingleline, this.negate, reverse);
					}
					else
					{
						cmp.EmitCategory((Category)k, this.negate, reverse);
					}
				}
				else if (this.neg_cats[k])
				{
					cmp.EmitNotCategory((Category)k, this.negate, reverse);
				}
			}
			if (num > 1)
			{
				if (this.negate)
				{
					cmp.EmitTrue();
				}
				else
				{
					cmp.EmitFalse();
				}
				cmp.ResolveLink(linkRef);
			}
		}

		// Token: 0x06002AD0 RID: 10960 RVA: 0x000935E0 File Offset: 0x000917E0
		public override void GetWidth(out int min, out int max)
		{
			min = (max = 1);
		}

		// Token: 0x06002AD1 RID: 10961 RVA: 0x000935F8 File Offset: 0x000917F8
		public override bool IsComplex()
		{
			return false;
		}

		// Token: 0x06002AD2 RID: 10962 RVA: 0x000935FC File Offset: 0x000917FC
		private static double GetIntervalCost(Interval i)
		{
			if (i.IsDiscontiguous)
			{
				return (double)(3 + (i.Size + 15 >> 4));
			}
			if (i.IsSingleton)
			{
				return 2.0;
			}
			return 3.0;
		}

		// Token: 0x04001B10 RID: 6928
		private const int distance_between_upper_and_lower_case = 32;

		// Token: 0x04001B11 RID: 6929
		private static Interval upper_case_characters = new Interval(65, 90);

		// Token: 0x04001B12 RID: 6930
		private bool negate;

		// Token: 0x04001B13 RID: 6931
		private bool ignore;

		// Token: 0x04001B14 RID: 6932
		private BitArray pos_cats;

		// Token: 0x04001B15 RID: 6933
		private BitArray neg_cats;

		// Token: 0x04001B16 RID: 6934
		private IntervalCollection intervals;
	}
}
