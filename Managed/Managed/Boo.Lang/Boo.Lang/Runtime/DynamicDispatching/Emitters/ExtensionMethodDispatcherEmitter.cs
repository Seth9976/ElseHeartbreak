using System;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	// Token: 0x0200002E RID: 46
	internal class ExtensionMethodDispatcherEmitter : MethodDispatcherEmitter
	{
		// Token: 0x06000124 RID: 292 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public ExtensionMethodDispatcherEmitter(CandidateMethod found, Type[] argumentTypes)
			: base(found, argumentTypes)
		{
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00004BD4 File Offset: 0x00002DD4
		protected override void EmitLoadTargetObject()
		{
			this._il.Emit(OpCodes.Ldarg_0);
			base.EmitCastOrUnbox(this._found.GetParameterType(0));
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000126 RID: 294 RVA: 0x00004C04 File Offset: 0x00002E04
		protected override int FixedArgumentOffset
		{
			get
			{
				return 1;
			}
		}
	}
}
