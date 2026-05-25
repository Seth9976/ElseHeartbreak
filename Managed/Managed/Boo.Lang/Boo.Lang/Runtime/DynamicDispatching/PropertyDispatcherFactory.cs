using System;
using System.Collections.Generic;
using System.Reflection;
using Boo.Lang.Runtime.DynamicDispatching.Emitters;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	// Token: 0x02000036 RID: 54
	public class PropertyDispatcherFactory : AbstractDispatcherFactory
	{
		// Token: 0x060001B6 RID: 438 RVA: 0x000059D0 File Offset: 0x00003BD0
		public PropertyDispatcherFactory(ExtensionRegistry extensions, object target, Type type, string name, params object[] arguments)
			: base(extensions, target, type, name, arguments)
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000059E0 File Offset: 0x00003BE0
		public Dispatcher CreateSetter()
		{
			return this.Create(SetOrGet.Set);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000059EC File Offset: 0x00003BEC
		public Dispatcher CreateGetter()
		{
			return this.Create(SetOrGet.Get);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000059F8 File Offset: 0x00003BF8
		private Dispatcher Create(SetOrGet gos)
		{
			MemberInfo[] member = this._type.GetMember(this._name, 20, 262268);
			if (member.Length == 0)
			{
				return this.FindExtension(this.GetCandidateExtensions(gos));
			}
			if (member.Length > 1)
			{
				throw new AmbiguousMatchException(Builtins.join(member, ", "));
			}
			return this.EmitDispatcherFor(member[0], gos);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00005A58 File Offset: 0x00003C58
		private Dispatcher FindExtension(IEnumerable<MethodInfo> candidates)
		{
			CandidateMethod candidateMethod = base.ResolveExtension(candidates);
			if (candidateMethod != null)
			{
				return base.EmitExtensionDispatcher(candidateMethod);
			}
			throw base.MissingField();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00005A84 File Offset: 0x00003C84
		private IEnumerable<MethodInfo> GetCandidateExtensions(SetOrGet gos)
		{
			foreach (PropertyInfo p in base.GetExtensions<PropertyInfo>(16))
			{
				MethodInfo i = PropertyDispatcherFactory.Accessor(p, gos);
				if (i != null)
				{
					yield return i;
				}
			}
			yield break;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00005AB8 File Offset: 0x00003CB8
		private static MethodInfo Accessor(PropertyInfo p, SetOrGet gos)
		{
			return (gos != SetOrGet.Get) ? p.GetSetMethod(true) : p.GetGetMethod(true);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005AD4 File Offset: 0x00003CD4
		private Dispatcher EmitDispatcherFor(MemberInfo info, SetOrGet gos)
		{
			MemberTypes memberType = info.MemberType;
			if (memberType != 16)
			{
				return this.EmitFieldDispatcher((FieldInfo)info, gos);
			}
			return this.EmitPropertyDispatcher((PropertyInfo)info, gos);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00005B10 File Offset: 0x00003D10
		private Dispatcher EmitFieldDispatcher(FieldInfo field, SetOrGet gos)
		{
			if (field.IsLiteral)
			{
				return PropertyDispatcherFactory.ReflectionBasedFieldDispatcherFor(field, gos);
			}
			return (gos != SetOrGet.Get) ? new SetFieldEmitter(field, base.GetArgumentTypes()[0]).Emit() : new GetFieldEmitter(field).Emit();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00005B5C File Offset: 0x00003D5C
		private static Dispatcher ReflectionBasedFieldDispatcherFor(FieldInfo field, SetOrGet gos)
		{
			if (gos == SetOrGet.Set)
			{
				return delegate(object target, object[] args)
				{
					object obj = args[0];
					field.SetValue(target, RuntimeServices.Coerce(obj, field.FieldType));
					return obj;
				};
			}
			if (gos != SetOrGet.Get)
			{
				throw new ArgumentException();
			}
			return (object target, object[] args) => field.GetValue(target);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005BAC File Offset: 0x00003DAC
		private Dispatcher EmitPropertyDispatcher(PropertyInfo property, SetOrGet gos)
		{
			Type[] argumentTypes = base.GetArgumentTypes();
			MethodInfo methodInfo = PropertyDispatcherFactory.Accessor(property, gos);
			if (methodInfo == null)
			{
				throw base.MissingField();
			}
			CandidateMethod candidateMethod = AbstractDispatcherFactory.ResolveMethod(argumentTypes, new MethodInfo[] { methodInfo });
			if (candidateMethod == null)
			{
				throw base.MissingField();
			}
			if (gos == SetOrGet.Get)
			{
				return new MethodDispatcherEmitter(this._type, candidateMethod, argumentTypes).Emit();
			}
			return new SetPropertyEmitter(this._type, candidateMethod, argumentTypes).Emit();
		}
	}
}
