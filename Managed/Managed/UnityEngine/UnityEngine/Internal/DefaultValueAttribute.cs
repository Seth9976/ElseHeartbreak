using System;

namespace UnityEngine.Internal
{
	// Token: 0x02000069 RID: 105
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.GenericParameter)]
	[Serializable]
	public class DefaultValueAttribute : Attribute
	{
		// Token: 0x0600027E RID: 638 RVA: 0x0000A5DC File Offset: 0x000087DC
		public DefaultValueAttribute(string value)
		{
			this.DefaultValue = value;
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600027F RID: 639 RVA: 0x0000A5EC File Offset: 0x000087EC
		public object Value
		{
			get
			{
				return this.DefaultValue;
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000A5F4 File Offset: 0x000087F4
		public override bool Equals(object obj)
		{
			DefaultValueAttribute defaultValueAttribute = obj as DefaultValueAttribute;
			if (defaultValueAttribute == null)
			{
				return false;
			}
			if (this.DefaultValue == null)
			{
				return defaultValueAttribute.Value == null;
			}
			return this.DefaultValue.Equals(defaultValueAttribute.Value);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000A638 File Offset: 0x00008838
		public override int GetHashCode()
		{
			if (this.DefaultValue == null)
			{
				return base.GetHashCode();
			}
			return this.DefaultValue.GetHashCode();
		}

		// Token: 0x0400019B RID: 411
		private object DefaultValue;
	}
}
