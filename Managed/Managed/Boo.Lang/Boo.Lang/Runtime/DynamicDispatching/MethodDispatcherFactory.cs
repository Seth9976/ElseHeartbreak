using System;
using System.Collections.Generic;
using System.Reflection;
using Boo.Lang.Runtime.DynamicDispatching.Emitters;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	// Token: 0x02000034 RID: 52
	public class MethodDispatcherFactory : AbstractDispatcherFactory
	{
		// Token: 0x06000140 RID: 320 RVA: 0x00005208 File Offset: 0x00003408
		public MethodDispatcherFactory(ExtensionRegistry extensions, object target, Type type, string methodName, params object[] arguments)
			: base(extensions, target, type, methodName, arguments)
		{
		}

		// Token: 0x06000141 RID: 321 RVA: 0x00005218 File Offset: 0x00003418
		public Dispatcher Create()
		{
			Type[] argumentTypes = base.GetArgumentTypes();
			CandidateMethod candidateMethod = this.ResolveMethod(argumentTypes);
			if (candidateMethod != null)
			{
				return this.EmitMethodDispatcher(candidateMethod, argumentTypes);
			}
			return this.ProduceExtensionDispatcher();
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000524C File Offset: 0x0000344C
		private Dispatcher ProduceExtensionDispatcher()
		{
			CandidateMethod candidateMethod = this.ResolveExtensionMethod();
			if (candidateMethod == null)
			{
				throw new MissingMethodException(this._type.FullName + "." + this._name);
			}
			return base.EmitExtensionDispatcher(candidateMethod);
		}

		// Token: 0x06000143 RID: 323 RVA: 0x00005290 File Offset: 0x00003490
		private CandidateMethod ResolveExtensionMethod()
		{
			return base.ResolveExtension(base.GetExtensionMethods());
		}

		// Token: 0x06000144 RID: 324 RVA: 0x000052A0 File Offset: 0x000034A0
		private CandidateMethod ResolveMethod(Type[] argumentTypes)
		{
			return AbstractDispatcherFactory.ResolveMethod(argumentTypes, this.GetCandidates());
		}

		// Token: 0x06000145 RID: 325 RVA: 0x000052B0 File Offset: 0x000034B0
		private IEnumerable<MethodInfo> GetCandidates()
		{
			foreach (MethodInfo method in this._type.GetMethods(262268))
			{
				if (!(this._name != method.Name))
				{
					yield return method;
				}
			}
			yield break;
		}

		// Token: 0x06000146 RID: 326 RVA: 0x000052D4 File Offset: 0x000034D4
		private Dispatcher EmitMethodDispatcher(CandidateMethod found, Type[] argumentTypes)
		{
			return new MethodDispatcherEmitter(this._type, found, argumentTypes).Emit();
		}
	}
}
