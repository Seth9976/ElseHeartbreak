using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace System.Runtime.Serialization
{
	// Token: 0x0200002E RID: 46
	internal class DataMemberInfo
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000066C8 File Offset: 0x000048C8
		public DataMemberInfo(MemberInfo member, DataMemberAttribute dma, string rootNamespce, string ns)
		{
			if (dma == null)
			{
				throw new ArgumentNullException("dma");
			}
			this.Order = dma.Order;
			this.Member = member;
			this.IsRequired = dma.IsRequired;
			this.XmlName = ((dma.Name == null) ? member.Name : dma.Name);
			this.XmlNamespace = ns;
			this.XmlRootNamespace = rootNamespce;
			if (this.Member is FieldInfo)
			{
				this.MemberType = ((FieldInfo)this.Member).FieldType;
			}
			else
			{
				this.MemberType = ((PropertyInfo)this.Member).PropertyType;
			}
		}

		// Token: 0x0400008D RID: 141
		public readonly int Order;

		// Token: 0x0400008E RID: 142
		public readonly bool IsRequired;

		// Token: 0x0400008F RID: 143
		public readonly string XmlName;

		// Token: 0x04000090 RID: 144
		public readonly MemberInfo Member;

		// Token: 0x04000091 RID: 145
		public readonly string XmlNamespace;

		// Token: 0x04000092 RID: 146
		public readonly string XmlRootNamespace;

		// Token: 0x04000093 RID: 147
		public readonly Type MemberType;

		// Token: 0x0200002F RID: 47
		public class DataMemberInfoComparer : IComparer<DataMemberInfo>, IComparer
		{
			// Token: 0x0600010B RID: 267 RVA: 0x00006780 File Offset: 0x00004980
			private DataMemberInfoComparer()
			{
			}

			// Token: 0x0600010D RID: 269 RVA: 0x00006794 File Offset: 0x00004994
			public int Compare(object o1, object o2)
			{
				return this.Compare((DataMemberInfo)o1, (DataMemberInfo)o2);
			}

			// Token: 0x0600010E RID: 270 RVA: 0x000067A8 File Offset: 0x000049A8
			public int Compare(DataMemberInfo d1, DataMemberInfo d2)
			{
				if (d1.Order == d2.Order)
				{
					return string.CompareOrdinal(d1.XmlName, d2.XmlName);
				}
				return d1.Order - d2.Order;
			}

			// Token: 0x04000094 RID: 148
			public static readonly DataMemberInfo.DataMemberInfoComparer Instance = new DataMemberInfo.DataMemberInfoComparer();
		}
	}
}
