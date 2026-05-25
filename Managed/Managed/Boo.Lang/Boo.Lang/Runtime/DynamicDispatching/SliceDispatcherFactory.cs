using System;
using System.Collections.Generic;
using System.Reflection;
using Boo.Lang.Runtime.DynamicDispatching.Emitters;

namespace Boo.Lang.Runtime.DynamicDispatching
{
	// Token: 0x02000038 RID: 56
	internal class SliceDispatcherFactory : AbstractDispatcherFactory
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x00005C20 File Offset: 0x00003E20
		public SliceDispatcherFactory(ExtensionRegistry extensions, object target, Type type, string name, params object[] arguments)
			: base(extensions, target, type, (name.Length != 0) ? name : RuntimeServices.GetDefaultMemberName(type), arguments)
		{
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005C54 File Offset: 0x00003E54
		public Dispatcher CreateGetter()
		{
			MemberInfo[] array = this.ResolveMember();
			if (array.Length == 1)
			{
				return this.CreateGetter(array[0]);
			}
			return this.EmitMethodDispatcher(this.Getters(array));
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005C88 File Offset: 0x00003E88
		private IEnumerable<MethodInfo> Getters(MemberInfo[] candidates)
		{
			foreach (MemberInfo info in candidates)
			{
				PropertyInfo p = info as PropertyInfo;
				if (p != null)
				{
					MethodInfo getter = p.GetGetMethod(true);
					if (getter != null)
					{
						yield return getter;
					}
				}
			}
			yield break;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00005CB4 File Offset: 0x00003EB4
		private Dispatcher CreateGetter(MemberInfo member)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType == 4)
			{
				FieldInfo field = (FieldInfo)member;
				return (object o, object[] arguments) => RuntimeServices.GetSlice(field.GetValue(o), string.Empty, arguments);
			}
			if (memberType != 16)
			{
				throw base.MissingField();
			}
			MethodInfo getter = ((PropertyInfo)member).GetGetMethod(true);
			if (getter == null)
			{
				throw base.MissingField();
			}
			if (getter.GetParameters().Length > 0)
			{
				return this.EmitMethodDispatcher(getter);
			}
			return (object o, object[] arguments) => RuntimeServices.GetSlice(getter.Invoke(o, null), string.Empty, arguments);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00005D58 File Offset: 0x00003F58
		private Dispatcher EmitMethodDispatcher(MethodInfo candidate)
		{
			return this.EmitMethodDispatcher(new MethodInfo[] { candidate });
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00005D6C File Offset: 0x00003F6C
		private Dispatcher EmitMethodDispatcher(IEnumerable<MethodInfo> candidates)
		{
			CandidateMethod candidateMethod = AbstractDispatcherFactory.ResolveMethod(base.GetArgumentTypes(), candidates);
			if (candidateMethod == null)
			{
				throw base.MissingField();
			}
			return new MethodDispatcherEmitter(this._type, candidateMethod, base.GetArgumentTypes()).Emit();
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00005DAC File Offset: 0x00003FAC
		private MemberInfo[] ResolveMember()
		{
			MemberInfo[] member = this._type.GetMember(this._name, 20, 262268);
			if (member.Length == 0)
			{
				throw base.MissingField();
			}
			return member;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00005DE4 File Offset: 0x00003FE4
		public Dispatcher CreateSetter()
		{
			MemberInfo[] array = this.ResolveMember();
			if (array.Length == 1)
			{
				return this.CreateSetter(array[0]);
			}
			return this.EmitMethodDispatcher(this.Setters(array));
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00005E18 File Offset: 0x00004018
		private IEnumerable<MethodInfo> Setters(MemberInfo[] candidates)
		{
			foreach (MemberInfo info in candidates)
			{
				PropertyInfo p = info as PropertyInfo;
				if (p != null)
				{
					MethodInfo setter = p.GetSetMethod(true);
					if (setter != null)
					{
						yield return setter;
					}
				}
			}
			yield break;
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00005E44 File Offset: 0x00004044
		private Dispatcher CreateSetter(MemberInfo member)
		{
			MemberTypes memberType = member.MemberType;
			if (memberType == 4)
			{
				FieldInfo field = (FieldInfo)member;
				return (object o, object[] arguments) => RuntimeServices.SetSlice(field.GetValue(o), string.Empty, arguments);
			}
			if (memberType != 16)
			{
				throw base.MissingField();
			}
			PropertyInfo propertyInfo = (PropertyInfo)member;
			if (propertyInfo.GetIndexParameters().Length <= 0)
			{
				return (object o, object[] arguments) => RuntimeServices.SetSlice(RuntimeServices.GetProperty(o, this._name), string.Empty, arguments);
			}
			MethodInfo setMethod = propertyInfo.GetSetMethod(true);
			if (setMethod == null)
			{
				throw base.MissingField();
			}
			return this.EmitMethodDispatcher(setMethod);
		}
	}
}
