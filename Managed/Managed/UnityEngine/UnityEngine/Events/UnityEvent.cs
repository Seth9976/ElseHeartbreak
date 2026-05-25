using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000062 RID: 98
	[Serializable]
	public class UnityEvent : UnityEventBase
	{
		// Token: 0x06000222 RID: 546 RVA: 0x00009638 File Offset: 0x00007838
		public void AddListener(UnityAction call)
		{
			base.AddCall(UnityEvent.GetDelegate(call));
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00009648 File Offset: 0x00007848
		public void RemoveListener(UnityAction call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000965C File Offset: 0x0000785C
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[0]);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000966C File Offset: 0x0000786C
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall(target, theFunction);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x00009678 File Offset: 0x00007878
		private static BaseInvokableCall GetDelegate(UnityAction action)
		{
			return new InvokableCall(action);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x00009680 File Offset: 0x00007880
		public void Invoke()
		{
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x04000191 RID: 401
		private readonly object[] m_InvokeArray = new object[0];
	}
}
