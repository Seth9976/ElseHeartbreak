using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000059 RID: 89
	internal class InvokableCall<T1, T2> : BaseInvokableCall
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x000087A4 File Offset: 0x000069A4
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = global::System.Delegate.CreateDelegate(typeof(UnityAction<T1, T2>), target, theFunction) as UnityAction<T1, T2>;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x000087D8 File Offset: 0x000069D8
		public InvokableCall(UnityAction<T1, T2> action)
		{
			this.Delegate = (UnityAction<T1, T2>)global::System.Delegate.Combine(this.Delegate, action);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060001D4 RID: 468 RVA: 0x000087F8 File Offset: 0x000069F8
		// (remove) Token: 0x060001D5 RID: 469 RVA: 0x00008814 File Offset: 0x00006A14
		protected event UnityAction<T1, T2> Delegate;

		// Token: 0x060001D6 RID: 470 RVA: 0x00008830 File Offset: 0x00006A30
		public override void Invoke(object[] args)
		{
			if (args.Length != 2)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]));
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000888C File Offset: 0x00006A8C
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method == method;
		}
	}
}
