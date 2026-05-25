using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000057 RID: 87
	internal class InvokableCall : BaseInvokableCall
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x000085A0 File Offset: 0x000067A0
		public InvokableCall(object target, MethodInfo theFunction)
			: base(target, theFunction)
		{
			this.Delegate = (UnityAction)global::System.Delegate.Combine(this.Delegate, global::System.Delegate.CreateDelegate(typeof(UnityAction), target, theFunction) as UnityAction);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000085E4 File Offset: 0x000067E4
		public InvokableCall(UnityAction action)
		{
			this.Delegate = (UnityAction)global::System.Delegate.Combine(this.Delegate, action);
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060001C8 RID: 456 RVA: 0x00008604 File Offset: 0x00006804
		// (remove) Token: 0x060001C9 RID: 457 RVA: 0x00008620 File Offset: 0x00006820
		private event UnityAction Delegate;

		// Token: 0x060001CA RID: 458 RVA: 0x0000863C File Offset: 0x0000683C
		public override void Invoke(object[] args)
		{
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate();
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000865C File Offset: 0x0000685C
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.Method == method;
		}
	}
}
