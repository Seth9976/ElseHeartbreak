using System;

namespace Boo.Lang
{
	// Token: 0x02000042 RID: 66
	[AttributeUsage(64)]
	[Serializable]
	public class TypeInferenceRuleAttribute : Attribute
	{
		// Token: 0x0600026A RID: 618 RVA: 0x000092E8 File Offset: 0x000074E8
		public TypeInferenceRuleAttribute(TypeInferenceRules rule)
			: this(rule.ToString())
		{
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000092FC File Offset: 0x000074FC
		public TypeInferenceRuleAttribute(string rule)
		{
			this._rule = rule;
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000930C File Offset: 0x0000750C
		public override string ToString()
		{
			return this._rule;
		}

		// Token: 0x04000158 RID: 344
		private readonly string _rule;
	}
}
