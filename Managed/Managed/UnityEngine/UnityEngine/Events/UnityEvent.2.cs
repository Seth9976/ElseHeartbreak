using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public abstract class UnityEvent<T0> : UnityEventBase
	{
		// Token: 0x06000229 RID: 553 RVA: 0x000096A4 File Offset: 0x000078A4
		public void AddListener(UnityAction<T0> call)
		{
			base.AddCall(UnityEvent<T0>.GetDelegate(call));
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000096B4 File Offset: 0x000078B4
		public void RemoveListener(UnityAction<T0> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000096C8 File Offset: 0x000078C8
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[] { typeof(T0) });
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000096E4 File Offset: 0x000078E4
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0>(target, theFunction);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000096F0 File Offset: 0x000078F0
		private static BaseInvokableCall GetDelegate(UnityAction<T0> action)
		{
			return new InvokableCall<T0>(action);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000096F8 File Offset: 0x000078F8
		public void Invoke(T0 arg0)
		{
			this.m_InvokeArray[0] = arg0;
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x04000192 RID: 402
		private readonly object[] m_InvokeArray = new object[1];
	}
}
