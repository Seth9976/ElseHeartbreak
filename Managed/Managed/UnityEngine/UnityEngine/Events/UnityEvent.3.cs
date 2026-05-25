using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000064 RID: 100
	[Serializable]
	public abstract class UnityEvent<T0, T1> : UnityEventBase
	{
		// Token: 0x06000230 RID: 560 RVA: 0x00009728 File Offset: 0x00007928
		public void AddListener(UnityAction<T0, T1> call)
		{
			base.AddCall(UnityEvent<T0, T1>.GetDelegate(call));
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00009738 File Offset: 0x00007938
		public void RemoveListener(UnityAction<T0, T1> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000974C File Offset: 0x0000794C
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[]
			{
				typeof(T0),
				typeof(T1)
			});
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00009778 File Offset: 0x00007978
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1>(target, theFunction);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00009784 File Offset: 0x00007984
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1> action)
		{
			return new InvokableCall<T0, T1>(action);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000978C File Offset: 0x0000798C
		public void Invoke(T0 arg0, T1 arg1)
		{
			this.m_InvokeArray[0] = arg0;
			this.m_InvokeArray[1] = arg1;
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x04000193 RID: 403
		private readonly object[] m_InvokeArray = new object[2];
	}
}
