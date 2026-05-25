using System;

namespace UnityEngineInternal
{
	// Token: 0x02000076 RID: 118
	[AttributeUsage(AttributeTargets.Method)]
	[Serializable]
	public class TypeInferenceRuleAttribute : Attribute
	{
		// Token: 0x0600029D RID: 669 RVA: 0x0000AED8 File Offset: 0x000090D8
		public TypeInferenceRuleAttribute(TypeInferenceRules rule)
			: this(rule.ToString())
		{
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000AEEC File Offset: 0x000090EC
		public TypeInferenceRuleAttribute(string rule)
		{
			this._rule = rule;
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000AEFC File Offset: 0x000090FC
		public override string ToString()
		{
			return this._rule;
		}

		// Token: 0x040001AD RID: 429
		private readonly string _rule;
	}
}
