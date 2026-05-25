using System;

namespace Boo.Lang
{
	// Token: 0x0200000B RID: 11
	[AttributeUsage(4)]
	[Serializable]
	public class EnumeratorItemTypeAttribute : Attribute
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002A7C File Offset: 0x00000C7C
		public EnumeratorItemTypeAttribute(Type itemType)
		{
			if (itemType == null)
			{
				throw new ArgumentNullException("itemType");
			}
			this._itemType = itemType;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002A9C File Offset: 0x00000C9C
		public Type ItemType
		{
			get
			{
				return this._itemType;
			}
		}

		// Token: 0x04000007 RID: 7
		protected Type _itemType;
	}
}
