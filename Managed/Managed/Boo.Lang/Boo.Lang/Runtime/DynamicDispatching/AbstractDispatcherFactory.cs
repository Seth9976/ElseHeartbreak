using System;
using System.Collections.Generic;
using System.Reflection;
using Boo.Lang.Runtime.DynamicDispatching.Emitters;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	// Token: 0x02000029 RID: 41
	public abstract class AbstractDispatcherFactory
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00004458 File Offset: 0x00002658
		public AbstractDispatcherFactory(ExtensionRegistry extensions, object target, Type type, string name, params object[] arguments)
		{
			this._extensions = extensions;
			this._target = target;
			this._type = type;
			this._name = name;
			this._arguments = arguments;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00004488 File Offset: 0x00002688
		protected IEnumerable<MemberInfo> Extensions
		{
			get
			{
				return this._extensions.Extensions;
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004498 File Offset: 0x00002698
		protected object[] GetExtensionArgs()
		{
			return AbstractDispatcherFactory.AdjustExtensionArgs(this._target, this._arguments);
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000044AC File Offset: 0x000026AC
		private static object[] AdjustExtensionArgs(object target, object[] originalArguments)
		{
			if (originalArguments == null)
			{
				return new object[] { target };
			}
			object[] array = new object[originalArguments.Length + 1];
			array[0] = target;
			Array.Copy(originalArguments, 0, array, 1, originalArguments.Length);
			return array;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000044E8 File Offset: 0x000026E8
		protected Type[] GetArgumentTypes()
		{
			return MethodResolver.GetArgumentTypes(this._arguments);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x000044F8 File Offset: 0x000026F8
		protected Type[] GetExtensionArgumentTypes()
		{
			return MethodResolver.GetArgumentTypes(this.GetExtensionArgs());
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004508 File Offset: 0x00002708
		protected Dispatcher EmitExtensionDispatcher(CandidateMethod found)
		{
			return new ExtensionMethodDispatcherEmitter(found, this.GetArgumentTypes()).Emit();
		}

		// Token: 0x06000102 RID: 258 RVA: 0x0000451C File Offset: 0x0000271C
		protected CandidateMethod ResolveExtension(IEnumerable<MethodInfo> candidates)
		{
			MethodResolver methodResolver = new MethodResolver(this.GetExtensionArgumentTypes());
			return methodResolver.ResolveMethod(candidates);
		}

		// Token: 0x06000103 RID: 259 RVA: 0x0000453C File Offset: 0x0000273C
		protected IEnumerable<MethodInfo> GetExtensionMethods()
		{
			return this.GetExtensions<MethodInfo>(8);
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004548 File Offset: 0x00002748
		protected IEnumerable<T> GetExtensions<T>(MemberTypes memberTypes) where T : MemberInfo
		{
			foreach (MemberInfo i in this.Extensions)
			{
				if (i.MemberType == memberTypes)
				{
					if (!(i.Name != this._name))
					{
						yield return (T)((object)i);
					}
				}
			}
			yield break;
		}

		// Token: 0x06000105 RID: 261 RVA: 0x0000457C File Offset: 0x0000277C
		protected static CandidateMethod ResolveMethod(Type[] argumentTypes, IEnumerable<MethodInfo> candidates)
		{
			return new MethodResolver(argumentTypes).ResolveMethod(candidates);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x0000458C File Offset: 0x0000278C
		protected MissingFieldException MissingField()
		{
			return new MissingFieldException(this._type.FullName + "." + this._name);
		}

		// Token: 0x0400012E RID: 302
		private readonly ExtensionRegistry _extensions;

		// Token: 0x0400012F RID: 303
		private readonly object _target;

		// Token: 0x04000130 RID: 304
		protected readonly Type _type;

		// Token: 0x04000131 RID: 305
		protected readonly string _name;

		// Token: 0x04000132 RID: 306
		private readonly object[] _arguments;
	}
}
