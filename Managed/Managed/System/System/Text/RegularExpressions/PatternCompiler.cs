using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000475 RID: 1141
	internal class PatternCompiler : ICompiler
	{
		// Token: 0x060028B6 RID: 10422 RVA: 0x000851E8 File Offset: 0x000833E8
		public PatternCompiler()
		{
			this.pgm = new ArrayList();
		}

		// Token: 0x060028B7 RID: 10423 RVA: 0x000851FC File Offset: 0x000833FC
		public static ushort EncodeOp(OpCode op, OpFlags flags)
		{
			return (ushort)(op | (OpCode)(flags & (OpFlags)65280));
		}

		// Token: 0x060028B8 RID: 10424 RVA: 0x00085208 File Offset: 0x00083408
		public static void DecodeOp(ushort word, out OpCode op, out OpFlags flags)
		{
			op = (OpCode)(word & 255);
			flags = (OpFlags)(word & 65280);
		}

		// Token: 0x060028B9 RID: 10425 RVA: 0x00085220 File Offset: 0x00083420
		public void Reset()
		{
			this.pgm.Clear();
		}

		// Token: 0x060028BA RID: 10426 RVA: 0x00085230 File Offset: 0x00083430
		public IMachineFactory GetMachineFactory()
		{
			ushort[] array = new ushort[this.pgm.Count];
			this.pgm.CopyTo(array);
			return new InterpreterFactory(array);
		}

		// Token: 0x060028BB RID: 10427 RVA: 0x00085260 File Offset: 0x00083460
		public void EmitFalse()
		{
			this.Emit(OpCode.False);
		}

		// Token: 0x060028BC RID: 10428 RVA: 0x0008526C File Offset: 0x0008346C
		public void EmitTrue()
		{
			this.Emit(OpCode.True);
		}

		// Token: 0x060028BD RID: 10429 RVA: 0x00085278 File Offset: 0x00083478
		private void EmitCount(int count)
		{
			this.Emit((ushort)(count & 65535));
			this.Emit((ushort)((uint)count >> 16));
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x000852A0 File Offset: 0x000834A0
		public void EmitCharacter(char c, bool negate, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Character, PatternCompiler.MakeFlags(negate, ignore, reverse, false));
			if (ignore)
			{
				c = char.ToLower(c);
			}
			this.Emit((ushort)c);
		}

		// Token: 0x060028BF RID: 10431 RVA: 0x000852D4 File Offset: 0x000834D4
		public void EmitCategory(Category cat, bool negate, bool reverse)
		{
			this.Emit(OpCode.Category, PatternCompiler.MakeFlags(negate, false, reverse, false));
			this.Emit((ushort)cat);
		}

		// Token: 0x060028C0 RID: 10432 RVA: 0x000852F0 File Offset: 0x000834F0
		public void EmitNotCategory(Category cat, bool negate, bool reverse)
		{
			this.Emit(OpCode.NotCategory, PatternCompiler.MakeFlags(negate, false, reverse, false));
			this.Emit((ushort)cat);
		}

		// Token: 0x060028C1 RID: 10433 RVA: 0x0008530C File Offset: 0x0008350C
		public void EmitRange(char lo, char hi, bool negate, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Range, PatternCompiler.MakeFlags(negate, ignore, reverse, false));
			this.Emit((ushort)lo);
			this.Emit((ushort)hi);
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x0008533C File Offset: 0x0008353C
		public void EmitSet(char lo, BitArray set, bool negate, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Set, PatternCompiler.MakeFlags(negate, ignore, reverse, false));
			this.Emit((ushort)lo);
			int num = set.Length + 15 >> 4;
			this.Emit((ushort)num);
			int num2 = 0;
			while (num-- != 0)
			{
				ushort num3 = 0;
				for (int i = 0; i < 16; i++)
				{
					if (num2 >= set.Length)
					{
						break;
					}
					if (set[num2++])
					{
						num3 |= (ushort)(1 << i);
					}
				}
				this.Emit(num3);
			}
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x000853D0 File Offset: 0x000835D0
		public void EmitString(string str, bool ignore, bool reverse)
		{
			this.Emit(OpCode.String, PatternCompiler.MakeFlags(false, ignore, reverse, false));
			int length = str.Length;
			this.Emit((ushort)length);
			if (ignore)
			{
				str = str.ToLower();
			}
			for (int i = 0; i < length; i++)
			{
				this.Emit((ushort)str[i]);
			}
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x0008542C File Offset: 0x0008362C
		public void EmitPosition(Position pos)
		{
			this.Emit(OpCode.Position, OpFlags.None);
			this.Emit((ushort)pos);
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x00085440 File Offset: 0x00083640
		public void EmitOpen(int gid)
		{
			this.Emit(OpCode.Open);
			this.Emit((ushort)gid);
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x00085454 File Offset: 0x00083654
		public void EmitClose(int gid)
		{
			this.Emit(OpCode.Close);
			this.Emit((ushort)gid);
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x00085468 File Offset: 0x00083668
		public void EmitBalanceStart(int gid, int balance, bool capture, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.BalanceStart);
			this.Emit((ushort)gid);
			this.Emit((ushort)balance);
			this.Emit((!capture) ? 0 : 1);
			this.EmitLink(tail);
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x000854B4 File Offset: 0x000836B4
		public void EmitBalance()
		{
			this.Emit(OpCode.Balance);
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x000854C0 File Offset: 0x000836C0
		public void EmitReference(int gid, bool ignore, bool reverse)
		{
			this.Emit(OpCode.Reference, PatternCompiler.MakeFlags(false, ignore, reverse, false));
			this.Emit((ushort)gid);
		}

		// Token: 0x060028CA RID: 10442 RVA: 0x000854DC File Offset: 0x000836DC
		public void EmitIfDefined(int gid, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.IfDefined);
			this.EmitLink(tail);
			this.Emit((ushort)gid);
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x00085508 File Offset: 0x00083708
		public void EmitSub(LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.Sub);
			this.EmitLink(tail);
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x00085520 File Offset: 0x00083720
		public void EmitTest(LinkRef yes, LinkRef tail)
		{
			this.BeginLink(yes);
			this.BeginLink(tail);
			this.Emit(OpCode.Test);
			this.EmitLink(yes);
			this.EmitLink(tail);
		}

		// Token: 0x060028CD RID: 10445 RVA: 0x00085554 File Offset: 0x00083754
		public void EmitBranch(LinkRef next)
		{
			this.BeginLink(next);
			this.Emit(OpCode.Branch, OpFlags.None);
			this.EmitLink(next);
		}

		// Token: 0x060028CE RID: 10446 RVA: 0x00085570 File Offset: 0x00083770
		public void EmitJump(LinkRef target)
		{
			this.BeginLink(target);
			this.Emit(OpCode.Jump, OpFlags.None);
			this.EmitLink(target);
		}

		// Token: 0x060028CF RID: 10447 RVA: 0x0008558C File Offset: 0x0008378C
		public void EmitRepeat(int min, int max, bool lazy, LinkRef until)
		{
			this.BeginLink(until);
			this.Emit(OpCode.Repeat, PatternCompiler.MakeFlags(false, false, false, lazy));
			this.EmitLink(until);
			this.EmitCount(min);
			this.EmitCount(max);
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x000855C8 File Offset: 0x000837C8
		public void EmitUntil(LinkRef repeat)
		{
			this.ResolveLink(repeat);
			this.Emit(OpCode.Until);
		}

		// Token: 0x060028D1 RID: 10449 RVA: 0x000855DC File Offset: 0x000837DC
		public void EmitFastRepeat(int min, int max, bool lazy, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.FastRepeat, PatternCompiler.MakeFlags(false, false, false, lazy));
			this.EmitLink(tail);
			this.EmitCount(min);
			this.EmitCount(max);
		}

		// Token: 0x060028D2 RID: 10450 RVA: 0x00085618 File Offset: 0x00083818
		public void EmitIn(LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.In);
			this.EmitLink(tail);
		}

		// Token: 0x060028D3 RID: 10451 RVA: 0x00085630 File Offset: 0x00083830
		public void EmitAnchor(bool reverse, int offset, LinkRef tail)
		{
			this.BeginLink(tail);
			this.Emit(OpCode.Anchor, PatternCompiler.MakeFlags(false, false, reverse, false));
			this.EmitLink(tail);
			this.Emit((ushort)offset);
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x00085664 File Offset: 0x00083864
		public void EmitInfo(int count, int min, int max)
		{
			this.Emit(OpCode.Info);
			this.EmitCount(count);
			this.EmitCount(min);
			this.EmitCount(max);
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x00085690 File Offset: 0x00083890
		public LinkRef NewLink()
		{
			return new PatternCompiler.PatternLinkStack();
		}

		// Token: 0x060028D6 RID: 10454 RVA: 0x00085698 File Offset: 0x00083898
		public void ResolveLink(LinkRef lref)
		{
			PatternCompiler.PatternLinkStack patternLinkStack = (PatternCompiler.PatternLinkStack)lref;
			while (patternLinkStack.Pop())
			{
				this.pgm[patternLinkStack.OffsetAddress] = (ushort)patternLinkStack.GetOffset(this.CurrentAddress);
			}
		}

		// Token: 0x060028D7 RID: 10455 RVA: 0x000856E0 File Offset: 0x000838E0
		public void EmitBranchEnd()
		{
		}

		// Token: 0x060028D8 RID: 10456 RVA: 0x000856E4 File Offset: 0x000838E4
		public void EmitAlternationEnd()
		{
		}

		// Token: 0x060028D9 RID: 10457 RVA: 0x000856E8 File Offset: 0x000838E8
		private static OpFlags MakeFlags(bool negate, bool ignore, bool reverse, bool lazy)
		{
			OpFlags opFlags = OpFlags.None;
			if (negate)
			{
				opFlags |= OpFlags.Negate;
			}
			if (ignore)
			{
				opFlags |= OpFlags.IgnoreCase;
			}
			if (reverse)
			{
				opFlags |= OpFlags.RightToLeft;
			}
			if (lazy)
			{
				opFlags |= OpFlags.Lazy;
			}
			return opFlags;
		}

		// Token: 0x060028DA RID: 10458 RVA: 0x00085734 File Offset: 0x00083934
		private void Emit(OpCode op)
		{
			this.Emit(op, OpFlags.None);
		}

		// Token: 0x060028DB RID: 10459 RVA: 0x00085740 File Offset: 0x00083940
		private void Emit(OpCode op, OpFlags flags)
		{
			this.Emit(PatternCompiler.EncodeOp(op, flags));
		}

		// Token: 0x060028DC RID: 10460 RVA: 0x00085750 File Offset: 0x00083950
		private void Emit(ushort word)
		{
			this.pgm.Add(word);
		}

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x060028DD RID: 10461 RVA: 0x00085764 File Offset: 0x00083964
		private int CurrentAddress
		{
			get
			{
				return this.pgm.Count;
			}
		}

		// Token: 0x060028DE RID: 10462 RVA: 0x00085774 File Offset: 0x00083974
		private void BeginLink(LinkRef lref)
		{
			PatternCompiler.PatternLinkStack patternLinkStack = (PatternCompiler.PatternLinkStack)lref;
			patternLinkStack.BaseAddress = this.CurrentAddress;
		}

		// Token: 0x060028DF RID: 10463 RVA: 0x00085794 File Offset: 0x00083994
		private void EmitLink(LinkRef lref)
		{
			PatternCompiler.PatternLinkStack patternLinkStack = (PatternCompiler.PatternLinkStack)lref;
			patternLinkStack.OffsetAddress = this.CurrentAddress;
			this.Emit(0);
			patternLinkStack.Push();
		}

		// Token: 0x040019C3 RID: 6595
		private ArrayList pgm;

		// Token: 0x02000476 RID: 1142
		private class PatternLinkStack : LinkStack
		{
			// Token: 0x17000B5B RID: 2907
			// (set) Token: 0x060028E1 RID: 10465 RVA: 0x000857CC File Offset: 0x000839CC
			public int BaseAddress
			{
				set
				{
					this.link.base_addr = value;
				}
			}

			// Token: 0x17000B5C RID: 2908
			// (get) Token: 0x060028E2 RID: 10466 RVA: 0x000857DC File Offset: 0x000839DC
			// (set) Token: 0x060028E3 RID: 10467 RVA: 0x000857EC File Offset: 0x000839EC
			public int OffsetAddress
			{
				get
				{
					return this.link.offset_addr;
				}
				set
				{
					this.link.offset_addr = value;
				}
			}

			// Token: 0x060028E4 RID: 10468 RVA: 0x000857FC File Offset: 0x000839FC
			public int GetOffset(int target_addr)
			{
				return target_addr - this.link.base_addr;
			}

			// Token: 0x060028E5 RID: 10469 RVA: 0x0008580C File Offset: 0x00083A0C
			protected override object GetCurrent()
			{
				return this.link;
			}

			// Token: 0x060028E6 RID: 10470 RVA: 0x0008581C File Offset: 0x00083A1C
			protected override void SetCurrent(object l)
			{
				this.link = (PatternCompiler.PatternLinkStack.Link)l;
			}

			// Token: 0x040019C4 RID: 6596
			private PatternCompiler.PatternLinkStack.Link link;

			// Token: 0x02000477 RID: 1143
			private struct Link
			{
				// Token: 0x040019C5 RID: 6597
				public int base_addr;

				// Token: 0x040019C6 RID: 6598
				public int offset_addr;
			}
		}
	}
}
