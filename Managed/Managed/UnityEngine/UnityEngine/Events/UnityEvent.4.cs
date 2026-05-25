using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000065 RID: 101
	[Serializable]
	public abstract class UnityEvent<T0, T1, T2> : UnityEventBase
	{
		// Token: 0x06000237 RID: 567 RVA: 0x000097D8 File Offset: 0x000079D8
		public void AddListener(UnityAction<T0, T1, T2> call)
		{
			base.AddCall(UnityEvent<T0, T1, T2>.GetDelegate(call));
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000097E8 File Offset: 0x000079E8
		public void RemoveListener(UnityAction<T0, T1, T2> call)
		{
			base.RemoveListener(call.Target, call.Method);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000097FC File Offset: 0x000079FC
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2)
			});
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00009840 File Offset: 0x00007A40
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2>(target, theFunction);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000984C File Offset: 0x00007A4C
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2> action)
		{
			return new InvokableCall<T0, T1, T2>(action);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x00009854 File Offset: 0x00007A54
		public void Invoke(T0 arg0, T1 arg1, T2 arg2)
		{
			this.m_InvokeArray[0] = arg0;
			this.m_InvokeArray[1] = arg1;
			this.m_InvokeArray[2] = arg2;
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x04000194 RID: 404
		private readonly object[] m_InvokeArray = new object[3];
	}
}
