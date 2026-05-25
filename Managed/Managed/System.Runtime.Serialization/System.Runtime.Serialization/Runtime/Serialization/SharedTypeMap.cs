using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace System.Runtime.Serialization
{
	// Token: 0x0200002B RID: 43
	internal class SharedTypeMap : SerializationMap
	{
		// Token: 0x06000100 RID: 256 RVA: 0x000061E8 File Offset: 0x000043E8
		public SharedTypeMap(Type type, XmlQualifiedName qname, KnownTypeCollection knownTypes)
			: base(type, qname, knownTypes)
		{
			this.Members = this.GetMembers(type, base.XmlName, false);
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006214 File Offset: 0x00004414
		private List<DataMemberInfo> GetMembers(Type type, XmlQualifiedName qname, bool declared_only)
		{
			List<DataMemberInfo> list = new List<DataMemberInfo>();
			BindingFlags bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			if (declared_only)
			{
				bindingFlags |= BindingFlags.DeclaredOnly;
			}
			foreach (FieldInfo fieldInfo in type.GetFields(bindingFlags))
			{
				if (fieldInfo.GetCustomAttributes(typeof(NonSerializedAttribute), false).Length <= 0)
				{
					if (fieldInfo.IsInitOnly)
					{
						throw new InvalidDataContractException(string.Format("DataMember field {0} must not be read-only.", fieldInfo));
					}
					DataMemberAttribute dataMemberAttribute = new DataMemberAttribute();
					list.Add(base.CreateDataMemberInfo(dataMemberAttribute, fieldInfo, fieldInfo.FieldType));
				}
			}
			list.Sort(DataMemberInfo.DataMemberInfoComparer.Instance);
			return list;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x000062BC File Offset: 0x000044BC
		public override List<DataMemberInfo> GetMembers()
		{
			return this.Members;
		}
	}
}
