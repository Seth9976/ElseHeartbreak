using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	// Token: 0x0200002D RID: 45
	public abstract class DispatcherEmitter
	{
		// Token: 0x06000112 RID: 274 RVA: 0x0000479C File Offset: 0x0000299C
		public DispatcherEmitter(Type owner, string dynamicMethodName)
		{
			this._dynamicMethod = new DynamicMethod(owner.Name + "$" + dynamicMethodName, typeof(object), new Type[]
			{
				typeof(object),
				typeof(object[])
			}, owner);
			this._il = this._dynamicMethod.GetILGenerator();
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00004808 File Offset: 0x00002A08
		public Dispatcher Emit()
		{
			this.EmitMethodBody();
			return this.CreateMethodDispatcher();
		}

		// Token: 0x06000114 RID: 276
		protected abstract void EmitMethodBody();

		// Token: 0x06000115 RID: 277 RVA: 0x00004818 File Offset: 0x00002A18
		protected Dispatcher CreateMethodDispatcher()
		{
			return (Dispatcher)this._dynamicMethod.CreateDelegate(typeof(Dispatcher));
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00004834 File Offset: 0x00002A34
		protected bool IsStobj(OpCode code)
		{
			OpCode stobj = OpCodes.Stobj;
			return stobj.Value == code.Value;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00004858 File Offset: 0x00002A58
		protected void EmitCastOrUnbox(Type type)
		{
			if (type.IsValueType)
			{
				this._il.Emit(OpCodes.Unbox, type);
				this._il.Emit(OpCodes.Ldobj, type);
			}
			else
			{
				this._il.Emit(OpCodes.Castclass, type);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x000048A8 File Offset: 0x00002AA8
		protected void BoxIfNeeded(Type returnType)
		{
			if (returnType.IsValueType)
			{
				this._il.Emit(OpCodes.Box, returnType);
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000048C8 File Offset: 0x00002AC8
		protected void EmitLoadTargetObject(Type expectedType)
		{
			this._il.Emit(OpCodes.Ldarg_0);
			if (expectedType.IsValueType)
			{
				this._il.Emit(OpCodes.Unbox, expectedType);
			}
			else
			{
				this._il.Emit(OpCodes.Castclass, expectedType);
			}
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004918 File Offset: 0x00002B18
		protected void EmitReturn(Type typeOnStack)
		{
			if (typeOnStack == typeof(void))
			{
				this._il.Emit(OpCodes.Ldnull);
			}
			else
			{
				this.BoxIfNeeded(typeOnStack);
			}
			this._il.Emit(OpCodes.Ret);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00004964 File Offset: 0x00002B64
		protected void EmitPromotion(Type expectedType, Type actualType)
		{
			this._il.Emit(OpCodes.Unbox_Any, actualType);
			this._il.Emit(DispatcherEmitter.NumericPromotionOpcodeFor(Type.GetTypeCode(expectedType), true));
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000499C File Offset: 0x00002B9C
		private static OpCode NumericPromotionOpcodeFor(TypeCode typeCode, bool @checked)
		{
			switch (typeCode)
			{
			case 4:
			case 8:
				return (!@checked) ? OpCodes.Conv_U2 : OpCodes.Conv_Ovf_U2;
			case 5:
				return (!@checked) ? OpCodes.Conv_I1 : OpCodes.Conv_Ovf_I1;
			case 6:
				return (!@checked) ? OpCodes.Conv_U1 : OpCodes.Conv_Ovf_U1;
			case 7:
				return (!@checked) ? OpCodes.Conv_I2 : OpCodes.Conv_Ovf_I2;
			case 9:
				return (!@checked) ? OpCodes.Conv_I4 : OpCodes.Conv_Ovf_I4;
			case 10:
				return (!@checked) ? OpCodes.Conv_U4 : OpCodes.Conv_Ovf_U4;
			case 11:
				return (!@checked) ? OpCodes.Conv_I8 : OpCodes.Conv_Ovf_I8;
			case 12:
				return (!@checked) ? OpCodes.Conv_U8 : OpCodes.Conv_Ovf_U8;
			case 13:
				return OpCodes.Conv_R4;
			case 14:
				return OpCodes.Conv_R8;
			default:
				throw new ArgumentException(typeCode.ToString());
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00004AB0 File Offset: 0x00002CB0
		protected void EmitArgArrayElement(int argumentIndex)
		{
			this._il.Emit(OpCodes.Ldarg_1);
			this._il.Emit(OpCodes.Ldc_I4, argumentIndex);
			this._il.Emit(OpCodes.Ldelem_Ref);
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00004AE4 File Offset: 0x00002CE4
		private MethodInfo GetPromotionMethod(Type type)
		{
			return typeof(IConvertible).GetMethod("To" + Type.GetTypeCode(type));
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004B18 File Offset: 0x00002D18
		protected void Dup()
		{
			this._il.Emit(OpCodes.Dup);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004B2C File Offset: 0x00002D2C
		protected void EmitCoercion(Type actualType, Type expectedType, int score)
		{
			switch (score)
			{
			case 3:
			case 5:
				this.EmitPromotion(expectedType, actualType);
				break;
			case 4:
				this.EmitCastOrUnbox(actualType);
				this._il.Emit(OpCodes.Call, RuntimeServices.FindImplicitConversionOperator(actualType, expectedType));
				break;
			default:
				this.EmitCastOrUnbox(expectedType);
				break;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004B90 File Offset: 0x00002D90
		protected void LoadLocal(LocalBuilder value)
		{
			this._il.Emit(OpCodes.Ldloc, value);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00004BA4 File Offset: 0x00002DA4
		protected void StoreLocal(LocalBuilder value)
		{
			this._il.Emit(OpCodes.Stloc, value);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00004BB8 File Offset: 0x00002DB8
		protected LocalBuilder DeclareLocal(Type type)
		{
			return this._il.DeclareLocal(type);
		}

		// Token: 0x04000138 RID: 312
		private DynamicMethod _dynamicMethod;

		// Token: 0x04000139 RID: 313
		protected readonly ILGenerator _il;
	}
}
