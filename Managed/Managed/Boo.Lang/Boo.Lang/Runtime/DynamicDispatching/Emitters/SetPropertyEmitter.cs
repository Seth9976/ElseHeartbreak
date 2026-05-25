using System;
using System.Reflection;
using System.Reflection.Emit;

namespace Boo.Lang.Runtime.DynamicDispatching.Emitters
{
	// Token: 0x02000033 RID: 51
	internal class SetPropertyEmitter : MethodDispatcherEmitter
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00005188 File Offset: 0x00003388
		public SetPropertyEmitter(Type type, CandidateMethod found, Type[] argumentTypes)
			: base(type, found, argumentTypes)
		{
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00005194 File Offset: 0x00003394
		protected override void EmitMethodBody()
		{
			Type valueType = this.GetValueType();
			LocalBuilder localBuilder = base.DeclareLocal(valueType);
			this.EmitLoadTargetObject();
			base.EmitMethodArguments();
			base.Dup();
			base.StoreLocal(localBuilder);
			base.EmitMethodCall();
			base.LoadLocal(localBuilder);
			base.EmitReturn(valueType);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x000051E0 File Offset: 0x000033E0
		private Type GetValueType()
		{
			ParameterInfo[] parameters = this._found.Parameters;
			return parameters[parameters.Length - 1].ParameterType;
		}
	}
}
