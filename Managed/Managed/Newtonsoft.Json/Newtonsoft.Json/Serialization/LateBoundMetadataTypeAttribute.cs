using System;
using System.Reflection;
using Newtonsoft.Json.Utilities;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x02000098 RID: 152
	internal class LateBoundMetadataTypeAttribute : IMetadataTypeAttribute
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x0001A95F File Offset: 0x00018B5F
		public LateBoundMetadataTypeAttribute(object attribute)
		{
			this._attribute = attribute;
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0001A96E File Offset: 0x00018B6E
		public Type MetadataClassType
		{
			get
			{
				if (LateBoundMetadataTypeAttribute._metadataClassTypeProperty == null)
				{
					LateBoundMetadataTypeAttribute._metadataClassTypeProperty = this._attribute.GetType().GetProperty("MetadataClassType");
				}
				return (Type)ReflectionUtils.GetMemberValue(LateBoundMetadataTypeAttribute._metadataClassTypeProperty, this._attribute);
			}
		}

		// Token: 0x0400024D RID: 589
		private static PropertyInfo _metadataClassTypeProperty;

		// Token: 0x0400024E RID: 590
		private readonly object _attribute;
	}
}
