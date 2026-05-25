using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000060 RID: 96
	internal class InvokableCallList
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000206 RID: 518 RVA: 0x00009120 File Offset: 0x00007320
		public int Count
		{
			get
			{
				return this.m_PersistentCalls.Count + this.m_RuntimeCalls.Count;
			}
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000913C File Offset: 0x0000733C
		public void AddPersistentInvokableCall(BaseInvokableCall call)
		{
			this.m_PersistentCalls.Add(call);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x0000914C File Offset: 0x0000734C
		public void AddListener(BaseInvokableCall call)
		{
			this.m_RuntimeCalls.Add(call);
		}

		// Token: 0x06000209 RID: 521 RVA: 0x0000915C File Offset: 0x0000735C
		public void RemoveListener(object targetObj, MethodInfo method)
		{
			List<BaseInvokableCall> list = new List<BaseInvokableCall>();
			for (int i = 0; i < this.m_RuntimeCalls.Count; i++)
			{
				if (this.m_RuntimeCalls[i].Find(targetObj, method))
				{
					list.Add(this.m_RuntimeCalls[i]);
				}
			}
			this.m_RuntimeCalls.RemoveAll(new Predicate<BaseInvokableCall>(list.Contains));
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000091D0 File Offset: 0x000073D0
		public void Clear()
		{
			this.m_RuntimeCalls.Clear();
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000091E0 File Offset: 0x000073E0
		public void ClearPersistent()
		{
			this.m_PersistentCalls.Clear();
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000091F0 File Offset: 0x000073F0
		public void Invoke(object[] parameters)
		{
			this.m_ExecutingCalls.AddRange(this.m_PersistentCalls);
			this.m_ExecutingCalls.AddRange(this.m_RuntimeCalls);
			for (int i = 0; i < this.m_ExecutingCalls.Count; i++)
			{
				this.m_ExecutingCalls[i].Invoke(parameters);
			}
			this.m_ExecutingCalls.Clear();
		}

		// Token: 0x0400018A RID: 394
		private readonly List<BaseInvokableCall> m_PersistentCalls = new List<BaseInvokableCall>();

		// Token: 0x0400018B RID: 395
		private readonly List<BaseInvokableCall> m_RuntimeCalls = new List<BaseInvokableCall>();

		// Token: 0x0400018C RID: 396
		private readonly List<BaseInvokableCall> m_ExecutingCalls = new List<BaseInvokableCall>();
	}
}
