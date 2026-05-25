using System;
using System.Collections;

namespace System.Text.RegularExpressions
{
	// Token: 0x02000474 RID: 1140
	internal class InterpreterFactory : IMachineFactory
	{
		// Token: 0x060028AD RID: 10413 RVA: 0x00085180 File Offset: 0x00083380
		public InterpreterFactory(ushort[] pattern)
		{
			this.pattern = pattern;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x00085190 File Offset: 0x00083390
		public IMachine NewInstance()
		{
			return new Interpreter(this.pattern);
		}

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x060028AF RID: 10415 RVA: 0x000851A0 File Offset: 0x000833A0
		public int GroupCount
		{
			get
			{
				return (int)this.pattern[1];
			}
		}

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x000851AC File Offset: 0x000833AC
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x000851B4 File Offset: 0x000833B4
		public int Gap
		{
			get
			{
				return this.gap;
			}
			set
			{
				this.gap = value;
			}
		}

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000851C0 File Offset: 0x000833C0
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x000851C8 File Offset: 0x000833C8
		public IDictionary Mapping
		{
			get
			{
				return this.mapping;
			}
			set
			{
				this.mapping = value;
			}
		}

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x000851D4 File Offset: 0x000833D4
		// (set) Token: 0x060028B5 RID: 10421 RVA: 0x000851DC File Offset: 0x000833DC
		public string[] NamesMapping
		{
			get
			{
				return this.namesMapping;
			}
			set
			{
				this.namesMapping = value;
			}
		}

		// Token: 0x040019BF RID: 6591
		private IDictionary mapping;

		// Token: 0x040019C0 RID: 6592
		private ushort[] pattern;

		// Token: 0x040019C1 RID: 6593
		private string[] namesMapping;

		// Token: 0x040019C2 RID: 6594
		private int gap;
	}
}
