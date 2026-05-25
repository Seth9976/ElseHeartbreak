using System;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	// Token: 0x0200002F RID: 47
	internal class GetFieldEmitter : DispatcherEmitter
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00004C08 File Offset: 0x00002E08
		public GetFieldEmitter(FieldInfo field)
			: base(field.DeclaringType, field.Name)
		{
			this._field = field;
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00004C24 File Offset: 0x00002E24
		protected override void EmitMethodBody()
		{
			if (this._field.IsStatic)
			{
				RuntimeHelpers.RunClassConstructor(this._field.DeclaringType.TypeHandle);
				this._il.Emit(OpCodes.Ldsfld, this._field);
			}
			else
			{
				base.EmitLoadTargetObject(this._field.DeclaringType);
				this._il.Emit(OpCodes.Ldfld, this._field);
			}
			base.EmitReturn(this._field.FieldType);
		}

		// Token: 0x0400013A RID: 314
		protected readonly FieldInfo _field;
	}
}
