using System;
using System.Collections.Generic;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	// Token: 0x0200002B RID: 43
	public class DispatcherKey
	{
		// Token: 0x0600010B RID: 267 RVA: 0x000046A0 File Offset: 0x000028A0
		public DispatcherKey(Type type, string name)
			: this(type, name, Type.EmptyTypes)
		{
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000046B0 File Offset: 0x000028B0
		public DispatcherKey(Type type, string name, Type[] arguments)
		{
			this._type = type;
			this._name = name;
			this._arguments = arguments;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000046DC File Offset: 0x000028DC
		public Type[] Arguments
		{
			get
			{
				return this._arguments;
			}
		}

		// Token: 0x04000134 RID: 308
		public static readonly IEqualityComparer<DispatcherKey> EqualityComparer = new DispatcherKey._EqualityComparer();

		// Token: 0x04000135 RID: 309
		private readonly Type _type;

		// Token: 0x04000136 RID: 310
		private readonly string _name;

		// Token: 0x04000137 RID: 311
		private readonly Type[] _arguments;

		// Token: 0x0200002C RID: 44
		private sealed class _EqualityComparer : IEqualityComparer<DispatcherKey>
		{
			// Token: 0x06000110 RID: 272 RVA: 0x000046EC File Offset: 0x000028EC
			public int GetHashCode(DispatcherKey key)
			{
				return key._type.GetHashCode() ^ key._name.GetHashCode() ^ key._arguments.Length;
			}

			// Token: 0x06000111 RID: 273 RVA: 0x0000471C File Offset: 0x0000291C
			public bool Equals(DispatcherKey x, DispatcherKey y)
			{
				if (x._type != y._type)
				{
					return false;
				}
				if (x._arguments.Length != y._arguments.Length)
				{
					return false;
				}
				if (x._name != y._name)
				{
					return false;
				}
				for (int i = 0; i < x._arguments.Length; i++)
				{
					if (x._arguments[i] != y._arguments[i])
					{
						return false;
					}
				}
				return true;
			}
		}
	}
}
