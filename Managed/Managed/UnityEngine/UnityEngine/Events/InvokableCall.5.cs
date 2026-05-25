using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x0200005B RID: 91
	internal class InvokableCall<T1, T2, T3, T4> : BaseInvokableCall
	{
		// Token: 0x060001DE RID: 478 RVA: 0x000089F4 File Offset: 0x00006BF4
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)global::System.Delegate.Combine(this.Delegate, global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3, T4>), target, theFunction) as UnityAction<T1, T2, T3, T4>);
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00008A38 File Offset: 0x00006C38
		public InvokableCall(UnityAction<T1, T2, T3, T4> action)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)global::System.Delegate.Combine(this.Delegate, action);
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001E0 RID: 480 RVA: 0x00008A58 File Offset: 0x00006C58
		// (remove) Token: 0x060001E1 RID: 481 RVA: 0x00008A74 File Offset: 0x00006C74
		protected event UnityAction<T1, T2, T3, T4> Delegate;

		// Token: 0x060001E2 RID: 482 RVA: 0x00008A90 File Offset: 0x00006C90
		public override void Invoke(object[] args)
		{
			if (args.Length != 4)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			BaseInvokableCall.ThrowOnInvalidArg<T4>(args[3]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]), (T4)((object)args[3]));
			}
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00008B0C File Offset: 0x00006D0C
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method == method;
		}
	}
}
