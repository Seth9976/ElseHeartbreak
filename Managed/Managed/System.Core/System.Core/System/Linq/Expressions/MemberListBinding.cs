using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	/// <summary>Represents initializing the elements of a collection member of a newly created object.</summary>
	// Token: 0x02000049 RID: 73
	public sealed class MemberListBinding : MemberBinding
	{
		// Token: 0x06000470 RID: 1136 RVA: 0x00013C74 File Offset: 0x00011E74
		internal MemberListBinding(MemberInfo member, ReadOnlyCollection<ElementInit> initializers)
			: base(MemberBindingType.ListBinding, member)
		{
			this.initializers = initializers;
		}

		/// <summary>Gets the element initializers for initializing a collection member of a newly created object.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.ElementInit" /> objects to initialize a collection member with.</returns>
		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x00013C88 File Offset: 0x00011E88
		public ReadOnlyCollection<ElementInit> Initializers
		{
			get
			{
				return this.initializers;
			}
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x00013C90 File Offset: 0x00011E90
		internal override void Emit(EmitContext ec, LocalBuilder local)
		{
			LocalBuilder localBuilder = base.EmitLoadMember(ec, local);
			foreach (ElementInit elementInit in this.initializers)
			{
				elementInit.Emit(ec, localBuilder);
			}
		}

		// Token: 0x0400010F RID: 271
		private ReadOnlyCollection<ElementInit> initializers;
	}
}
