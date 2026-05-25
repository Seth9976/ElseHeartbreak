using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	// Token: 0x02000030 RID: 48
	internal class ImplicitConversionEmitter : DispatcherEmitter
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00004CAC File Offset: 0x00002EAC
		public ImplicitConversionEmitter(MethodInfo conversion)
			: base(conversion.DeclaringType, conversion.Name)
		{
			this._conversion = conversion;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004CC8 File Offset: 0x00002EC8
		protected override void EmitMethodBody()
		{
			this._il.Emit(OpCodes.Ldarg_0);
			base.EmitCastOrUnbox(this._conversion.GetParameters()[0].ParameterType);
			this._il.Emit(OpCodes.Call, this._conversion);
			base.EmitReturn(this._conversion.ReturnType);
		}

		// Token: 0x0400013B RID: 315
		private MethodInfo _conversion;
	}
}
