using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x02000026 RID: 38
	internal class DefaultTypeMap : SerializationMap
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x00005424 File Offset: 0x00003624
		public DefaultTypeMap(Type type, KnownTypeCollection knownTypes)
			: base(type, KnownTypeCollection.GetContractQName(type, null, null), knownTypes)
		{
			this.Members.AddRange(this.GetDefaultMembers());
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005448 File Offset: 0x00003648
		private List<DataMemberInfo> GetDefaultMembers()
		{
			List<DataMemberInfo> list = new List<DataMemberInfo>();
			foreach (MemberInfo memberInfo in this.RuntimeType.GetMembers())
			{
				FieldInfo fieldInfo = memberInfo as FieldInfo;
				Type type = ((fieldInfo != null) ? fieldInfo.FieldType : null);
				PropertyInfo propertyInfo = memberInfo as PropertyInfo;
				if (propertyInfo != null && propertyInfo.CanRead && propertyInfo.CanWrite && propertyInfo.GetIndexParameters().Length == 0)
				{
					type = propertyInfo.PropertyType;
				}
				if (type != null)
				{
					if (memberInfo.GetCustomAttributes(typeof(IgnoreDataMemberAttribute), false).Length == 0)
					{
						list.Add(new DataMemberInfo(memberInfo, new DataMemberAttribute(), null, null));
					}
				}
			}
			list.Sort(DataMemberInfo.DataMemberInfoComparer.Instance);
			return list;
		}
	}
}
