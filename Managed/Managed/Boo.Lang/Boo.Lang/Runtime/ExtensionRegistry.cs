using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Boo.Lang.Runtime
{
	// Token: 0x02000039 RID: 57
	public class ExtensionRegistry
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00005F0C File Offset: 0x0000410C
		public void Register(Type type)
		{
			object classLock = this._classLock;
			lock (classLock)
			{
				this._extensions = ExtensionRegistry.AddExtensionMembers(this.CopyExtensions(), type);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00005F60 File Offset: 0x00004160
		public IEnumerable<MemberInfo> Extensions
		{
			get
			{
				return this._extensions;
			}
		}

		// Token: 0x060001CF RID: 463 RVA: 0x00005F68 File Offset: 0x00004168
		public void UnRegister(Type type)
		{
			object classLock = this._classLock;
			lock (classLock)
			{
				List<MemberInfo> list = this.CopyExtensions();
				list.RemoveAll((MemberInfo member) => member.DeclaringType == type);
				this._extensions = list;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00005FD8 File Offset: 0x000041D8
		private static List<MemberInfo> AddExtensionMembers(List<MemberInfo> extensions, Type type)
		{
			foreach (MemberInfo memberInfo in type.GetMembers(24))
			{
				if (Attribute.IsDefined(memberInfo, typeof(ExtensionAttribute)))
				{
					if (!extensions.Contains(memberInfo))
					{
						extensions.Add(memberInfo);
					}
				}
			}
			return extensions;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000603C File Offset: 0x0000423C
		private List<MemberInfo> CopyExtensions()
		{
			return new List<MemberInfo>(this._extensions);
		}

		// Token: 0x04000143 RID: 323
		private List<MemberInfo> _extensions = new List<MemberInfo>();

		// Token: 0x04000144 RID: 324
		private object _classLock = new object();
	}
}
