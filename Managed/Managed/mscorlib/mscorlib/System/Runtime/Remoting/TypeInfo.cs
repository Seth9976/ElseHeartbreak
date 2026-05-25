using System;

namespace System.Runtime.Remoting
{
	// Token: 0x02000435 RID: 1077
	[Serializable]
	internal class TypeInfo : IRemotingTypeInfo
	{
		// Token: 0x06002DCC RID: 11724 RVA: 0x00098970 File Offset: 0x00096B70
		public TypeInfo(Type type)
		{
			if (type.IsInterface)
			{
				this.serverType = typeof(MarshalByRefObject).AssemblyQualifiedName;
				this.serverHierarchy = new string[0];
				this.interfacesImplemented = new string[] { type.AssemblyQualifiedName };
			}
			else
			{
				this.serverType = type.AssemblyQualifiedName;
				int num = 0;
				Type type2 = type.BaseType;
				while (type2 != typeof(MarshalByRefObject) && type2 != typeof(object))
				{
					type2 = type2.BaseType;
					num++;
				}
				this.serverHierarchy = new string[num];
				type2 = type.BaseType;
				for (int i = 0; i < num; i++)
				{
					this.serverHierarchy[i] = type2.AssemblyQualifiedName;
					type2 = type2.BaseType;
				}
				Type[] interfaces = type.GetInterfaces();
				this.interfacesImplemented = new string[interfaces.Length];
				for (int j = 0; j < interfaces.Length; j++)
				{
					this.interfacesImplemented[j] = interfaces[j].AssemblyQualifiedName;
				}
			}
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002DCD RID: 11725 RVA: 0x00098A88 File Offset: 0x00096C88
		// (set) Token: 0x06002DCE RID: 11726 RVA: 0x00098A90 File Offset: 0x00096C90
		public string TypeName
		{
			get
			{
				return this.serverType;
			}
			set
			{
				this.serverType = value;
			}
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x00098A9C File Offset: 0x00096C9C
		public bool CanCastTo(Type fromType, object o)
		{
			if (fromType == typeof(object))
			{
				return true;
			}
			if (fromType == typeof(MarshalByRefObject))
			{
				return true;
			}
			string text = fromType.AssemblyQualifiedName;
			int num = text.IndexOf(',');
			if (num != -1)
			{
				num = text.IndexOf(',', num + 1);
			}
			if (num != -1)
			{
				text = text.Substring(0, num + 1);
			}
			else
			{
				text += ",";
			}
			if ((this.serverType + ",").StartsWith(text))
			{
				return true;
			}
			if (this.serverHierarchy != null)
			{
				foreach (string text2 in this.serverHierarchy)
				{
					if ((text2 + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			if (this.interfacesImplemented != null)
			{
				foreach (string text3 in this.interfacesImplemented)
				{
					if ((text3 + ",").StartsWith(text))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x040013AD RID: 5037
		private string serverType;

		// Token: 0x040013AE RID: 5038
		private string[] serverHierarchy;

		// Token: 0x040013AF RID: 5039
		private string[] interfacesImplemented;
	}
}
