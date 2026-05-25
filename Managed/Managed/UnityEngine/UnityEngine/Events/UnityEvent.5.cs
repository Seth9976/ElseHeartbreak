using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public abstract class UnityEvent<T0, T1, T2, T3> : UnityEventBase
	{
		// Token: 0x0600023E RID: 574 RVA: 0x000098AC File Offset: 0x00007AAC
		public void AddListener(UnityAction<T0, T1, T2, T3> call)
		{
			base.AddCall(UnityEvent<T0, T1, T2, T3>.GetDelegate(call));
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000098BC File Offset: 0x00007ABC
		public void RemoveListener(UnityAction<T0, T1, T2, T3> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000098D0 File Offset: 0x00007AD0
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2),
				typeof(T3)
			});
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00009920 File Offset: 0x00007B20
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2, T3>(target, theFunction);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000992C File Offset: 0x00007B2C
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2, T3> action)
		{
			return new InvokableCall<T0, T1, T2, T3>(action);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x00009934 File Offset: 0x00007B34
		public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			this.m_InvokeArray[0] = arg0;
			this.m_InvokeArray[1] = arg1;
			this.m_InvokeArray[2] = arg2;
			this.m_InvokeArray[3] = arg3;
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x04000195 RID: 405
		private readonly object[] m_InvokeArray = new object[4];
	}
}
