using System;
using System.Collections;

namespace Boo.Lang
{
	// Token: 0x02000002 RID: 2
	public abstract class AbstractGenerator : IEnumerable
	{
		// Token: 0x06000002 RID: 2
		public abstract IEnumerator GetEnumerator();

		// Token: 0x06000003 RID: 3 RVA: 0x000020F4 File Offset: 0x000002F4
		public override string ToString()
		{
			EnumeratorItemTypeAttribute enumeratorItemTypeAttribute = (EnumeratorItemTypeAttribute)Attribute.GetCustomAttribute(base.GetType(), typeof(EnumeratorItemTypeAttribute));
			return string.Format("generator({0})", enumeratorItemTypeAttribute.ItemType);
		}
	}
}
