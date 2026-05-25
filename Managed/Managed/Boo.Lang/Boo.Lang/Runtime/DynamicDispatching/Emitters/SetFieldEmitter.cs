using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	// Token: 0x02000032 RID: 50
	internal class SetFieldEmitter : DispatcherEmitter
	{
		// Token: 0x0600013A RID: 314 RVA: 0x00005068 File Offset: 0x00003268
		public SetFieldEmitter(FieldInfo field, Type argumentType)
			: base(field.DeclaringType, field.Name + "=")
		{
			this._field = field;
			this._argumentType = argumentType;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x000050A0 File Offset: 0x000032A0
		protected override void EmitMethodBody()
		{
			LocalBuilder localBuilder = base.DeclareLocal(this._field.FieldType);
			this.EmitLoadValue();
			base.StoreLocal(localBuilder);
			if (this._field.IsStatic)
			{
				base.LoadLocal(localBuilder);
				this._il.Emit(OpCodes.Stsfld, this._field);
			}
			else
			{
				base.EmitLoadTargetObject(this._field.DeclaringType);
				base.LoadLocal(localBuilder);
				this._il.Emit(OpCodes.Stfld, this._field);
			}
			base.LoadLocal(localBuilder);
			base.EmitReturn(this._field.FieldType);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x00005144 File Offset: 0x00003344
		private void EmitLoadValue()
		{
			base.EmitArgArrayElement(0);
			base.EmitCoercion(this._argumentType, this._field.FieldType, CandidateMethod.CalculateArgumentScore(this._field.FieldType, this._argumentType));
		}

		// Token: 0x0400013E RID: 318
		private readonly FieldInfo _field;

		// Token: 0x0400013F RID: 319
		private Type _argumentType;
	}
}
