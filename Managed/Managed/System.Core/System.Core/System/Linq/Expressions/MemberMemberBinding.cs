using System;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Reflection.Emit;

namespace System.Linq.Expressions
{
	/// <summary>Represents initializing members of a member of a newly created object.</summary>
	// Token: 0x0200004A RID: 74
	public sealed class MemberMemberBinding : MemberBinding
	{
		// Token: 0x06000473 RID: 1139 RVA: 0x00013D00 File Offset: 0x00011F00
		internal MemberMemberBinding(MemberInfo member, ReadOnlyCollection<MemberBinding> bindings)
			: base(MemberBindingType.MemberBinding, member)
		{
			this.bindings = bindings;
		}

		/// <summary>Gets the bindings that describe how to initialize the members of a member.</summary>
		/// <returns>A <see cref="T:System.Collections.ObjectModel.ReadOnlyCollection`1" /> of <see cref="T:System.Linq.Expressions.MemberBinding" /> objects that describe how to initialize the members of the member.</returns>
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x00013D14 File Offset: 0x00011F14
		public ReadOnlyCollection<MemberBinding> Bindings
		{
			get
			{
				return this.bindings;
			}
		}

		// Token: 0x06000475 RID: 1141 RVA: 0x00013D1C File Offset: 0x00011F1C
		internal override void Emit(EmitContext ec, LocalBuilder local)
		{
			LocalBuilder localBuilder = base.EmitLoadMember(ec, local);
			foreach (MemberBinding memberBinding in this.bindings)
			{
				memberBinding.Emit(ec, localBuilder);
			}
		}

		// Token: 0x04000110 RID: 272
		private ReadOnlyCollection<MemberBinding> bindings;
	}
}
