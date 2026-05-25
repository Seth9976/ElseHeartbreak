using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000058 RID: 88
	internal class InvokableCall<T1> : BaseInvokableCall
	{
		// Token: 0x060001CC RID: 460 RVA: 0x0000868C File Offset: 0x0000688C
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1>)global::System.Delegate.Combine(this.Delegate, global::System.Delegate.CreateDelegate(typeof(UnityAction<T1>), target, theFunction) as UnityAction<T1>);
		}

		// Token: 0x060001CD RID: 461 RVA: 0x000086D0 File Offset: 0x000068D0
		public InvokableCall(UnityAction<T1> callback)
		{
			this.Delegate = (UnityAction<T1>)global::System.Delegate.Combine(this.Delegate, callback);
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060001CE RID: 462 RVA: 0x000086F0 File Offset: 0x000068F0
		// (remove) Token: 0x060001CF RID: 463 RVA: 0x0000870C File Offset: 0x0000690C
		protected event UnityAction<T1> Delegate;

		// Token: 0x060001D0 RID: 464 RVA: 0x00008728 File Offset: 0x00006928
		public override void Invoke(object[] args)
		{
			if (args.Length != 1)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)((object)args[0]));
			}
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008774 File Offset: 0x00006974
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method == method;
		}
	}
}
