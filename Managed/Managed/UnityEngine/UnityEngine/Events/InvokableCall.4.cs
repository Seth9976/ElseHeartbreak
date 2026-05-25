using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x0200005A RID: 90
	internal class InvokableCall<T1, T2, T3> : BaseInvokableCall
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x000088BC File Offset: 0x00006ABC
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)global::System.Delegate.Combine(this.Delegate, global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2, T3>), target, theFunction) as UnityAction<T1, T2, T3>);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x00008900 File Offset: 0x00006B00
		public InvokableCall(UnityAction<T1, T2, T3> action)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)global::System.Delegate.Combine(this.Delegate, action);
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001DA RID: 474 RVA: 0x00008920 File Offset: 0x00006B20
		// (remove) Token: 0x060001DB RID: 475 RVA: 0x0000893C File Offset: 0x00006B3C
		protected event UnityAction<T1, T2, T3> Delegate;

		// Token: 0x060001DC RID: 476 RVA: 0x00008958 File Offset: 0x00006B58
		public override void Invoke(object[] args)
		{
			if (args.Length != 3)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]));
			}
		}

		// Token: 0x060001DD RID: 477 RVA: 0x000089C4 File Offset: 0x00006BC4
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method == method;
		}
	}
}
