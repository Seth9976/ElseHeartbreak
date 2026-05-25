using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x0200005C RID: 92
	internal class CachedInvokableCall<T> : InvokableCall<T>
	{
		// Token: 0x060001E4 RID: 484 RVA: 0x00008B3C File Offset: 0x00006D3C
		public CachedInvokableCall(Object target, MethodInfo theFunction, T argument)
			: base(target, theFunction)
		{
			this.m_Arg1[0] = argument;
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x00008B6C File Offset: 0x00006D6C
		public override void Invoke(object[] args)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x0400017F RID: 383
		private readonly object[] m_Arg1 = new object[1];
	}
}
