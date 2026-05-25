using System;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	// Token: 0x02000031 RID: 49
	public class MethodDispatcherEmitter : DispatcherEmitter
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00004D24 File Offset: 0x00002F24
		public MethodDispatcherEmitter(CandidateMethod found, params Type[] argumentTypes)
			: this(found.Method.DeclaringType, found, argumentTypes)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004D44 File Offset: 0x00002F44
		public MethodDispatcherEmitter(Type owner, CandidateMethod found, Type[] argumentTypes)
			: base(owner, found.Method.Name + "$" + Builtins.join(argumentTypes, "$"))
		{
			this._found = found;
			this._argumentTypes = argumentTypes;
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004D88 File Offset: 0x00002F88
		protected override void EmitMethodBody()
		{
			this.EmitInvocation();
			this.EmitMethodReturn();
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00004D98 File Offset: 0x00002F98
		protected void EmitInvocation()
		{
			this.EmitLoadTargetObject();
			this.EmitMethodArguments();
			this.EmitMethodCall();
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00004DAC File Offset: 0x00002FAC
		protected void EmitMethodCall()
		{
			this._il.Emit((!this._found.Method.IsStatic) ? OpCodes.Callvirt : OpCodes.Call, this._found.Method);
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00004DF4 File Offset: 0x00002FF4
		protected void EmitMethodArguments()
		{
			this.EmitFixedMethodArguments();
			if (this._found.VarArgs)
			{
				this.EmitVarArgsMethodArguments();
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00004E14 File Offset: 0x00003014
		private void EmitFixedMethodArguments()
		{
			int fixedArgumentOffset = this.FixedArgumentOffset;
			int num = this.MinimumArgumentCount();
			for (int i = 0; i < num; i++)
			{
				Type parameterType = this._found.GetParameterType(i + fixedArgumentOffset);
				this.EmitMethodArgument(i, parameterType);
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000132 RID: 306 RVA: 0x00004E58 File Offset: 0x00003058
		protected virtual int FixedArgumentOffset
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00004E5C File Offset: 0x0000305C
		private void EmitVarArgsMethodArguments()
		{
			int num = this._argumentTypes.Length - this.MinimumArgumentCount();
			Type varArgsParameterType = this._found.VarArgsParameterType;
			OpCode storeElementOpCode = MethodDispatcherEmitter.GetStoreElementOpCode(varArgsParameterType);
			this._il.Emit(OpCodes.Ldc_I4, num);
			this._il.Emit(OpCodes.Newarr, varArgsParameterType);
			for (int i = 0; i < num; i++)
			{
				base.Dup();
				this._il.Emit(OpCodes.Ldc_I4, i);
				if (base.IsStobj(storeElementOpCode))
				{
					this._il.Emit(OpCodes.Ldelema, varArgsParameterType);
					this.EmitMethodArgument(this.MinimumArgumentCount() + i, varArgsParameterType);
					this._il.Emit(storeElementOpCode, varArgsParameterType);
				}
				else
				{
					this.EmitMethodArgument(this.MinimumArgumentCount() + i, varArgsParameterType);
					this._il.Emit(storeElementOpCode);
				}
			}
		}

		// Token: 0x06000134 RID: 308 RVA: 0x00004F30 File Offset: 0x00003130
		private int MinimumArgumentCount()
		{
			return this._found.MinimumArgumentCount - this.FixedArgumentOffset;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00004F44 File Offset: 0x00003144
		private static OpCode GetStoreElementOpCode(Type type)
		{
			if (!type.IsValueType)
			{
				return OpCodes.Stelem_Ref;
			}
			if (type.IsEnum)
			{
				return OpCodes.Stelem_I4;
			}
			switch (Type.GetTypeCode(type))
			{
			case 6:
				return OpCodes.Stelem_I1;
			case 7:
				return OpCodes.Stelem_I2;
			case 9:
				return OpCodes.Stelem_I4;
			case 11:
				return OpCodes.Stelem_I8;
			case 13:
				return OpCodes.Stelem_R4;
			case 14:
				return OpCodes.Stelem_R8;
			}
			return OpCodes.Stobj;
		}

		// Token: 0x06000136 RID: 310 RVA: 0x00004FD4 File Offset: 0x000031D4
		protected void EmitMethodArgument(int argumentIndex, Type expectedType)
		{
			base.EmitArgArrayElement(argumentIndex);
			this.EmitCoercion(argumentIndex, expectedType, this._found.ArgumentScores[argumentIndex]);
		}

		// Token: 0x06000137 RID: 311 RVA: 0x00005000 File Offset: 0x00003200
		private void EmitCoercion(int argumentIndex, Type expectedType, int score)
		{
			base.EmitCoercion(this._argumentTypes[argumentIndex], expectedType, score);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x00005014 File Offset: 0x00003214
		protected virtual void EmitLoadTargetObject()
		{
			if (this._found.Method.IsStatic)
			{
				return;
			}
			base.EmitLoadTargetObject(this._found.Method.DeclaringType);
		}

		// Token: 0x06000139 RID: 313 RVA: 0x00005050 File Offset: 0x00003250
		private void EmitMethodReturn()
		{
			base.EmitReturn(this._found.Method.ReturnType);
		}

		// Token: 0x0400013C RID: 316
		protected readonly CandidateMethod _found;

		// Token: 0x0400013D RID: 317
		protected readonly Type[] _argumentTypes;
	}
}
