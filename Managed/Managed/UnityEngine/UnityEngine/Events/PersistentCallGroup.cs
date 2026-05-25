using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	internal class PersistentCallGroup
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00008DF4 File Offset: 0x00006FF4
		public PersistentCallGroup()
		{
			this.m_Calls = new List<PersistentCall>();
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001F4 RID: 500 RVA: 0x00008E08 File Offset: 0x00007008
		public int Count
		{
			get
			{
				return this.m_Calls.Count;
			}
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x00008E18 File Offset: 0x00007018
		public PersistentCall GetListener(int index)
		{
			return this.m_Calls[index];
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00008E28 File Offset: 0x00007028
		public IEnumerable<PersistentCall> GetListeners()
		{
			return this.m_Calls;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00008E30 File Offset: 0x00007030
		public void AddListener()
		{
			this.m_Calls.Add(new PersistentCall());
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x00008E44 File Offset: 0x00007044
		public void AddListener(PersistentCall call)
		{
			this.m_Calls.Add(call);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x00008E54 File Offset: 0x00007054
		public void RemoveListener(int index)
		{
			this.m_Calls.RemoveAt(index);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00008E64 File Offset: 0x00007064
		public void Clear()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00008E74 File Offset: 0x00007074
		public void RegisterEventPersistentListener(int index, Object targetObj, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.EventDefined;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00008E98 File Offset: 0x00007098
		public void RegisterVoidPersistentListener(int index, Object targetObj, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Void;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00008EBC File Offset: 0x000070BC
		public void RegisterObjectPersistentListener(int index, Object targetObj, Object argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Object;
			listener.arguments.unityObjectArgument = argument;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00008EF0 File Offset: 0x000070F0
		public void RegisterIntPersistentListener(int index, Object targetObj, int argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Int;
			listener.arguments.intArgument = argument;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00008F24 File Offset: 0x00007124
		public void RegisterFloatPersistentListener(int index, Object targetObj, float argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Float;
			listener.arguments.floatArgument = argument;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00008F58 File Offset: 0x00007158
		public void RegisterStringPersistentListener(int index, Object targetObj, string argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.String;
			listener.arguments.stringArgument = argument;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00008F8C File Offset: 0x0000718C
		public void RegisterBoolPersistentListener(int index, Object targetObj, bool argument, string methodName)
		{
			PersistentCall listener = this.GetListener(index);
			listener.RegisterPersistentListener(targetObj, methodName);
			listener.mode = PersistentListenerMode.Bool;
			listener.arguments.boolArgument = argument;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00008FC0 File Offset: 0x000071C0
		public void UnregisterPersistentListener(int index)
		{
			PersistentCall listener = this.GetListener(index);
			listener.UnregisterPersistentListener();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00008FDC File Offset: 0x000071DC
		public void RemoveListeners(Object target, string methodName)
		{
			List<PersistentCall> list = new List<PersistentCall>();
			for (int i = 0; i < this.m_Calls.Count; i++)
			{
				if (this.m_Calls[i].target == target && this.m_Calls[i].methodName == methodName)
				{
					list.Add(this.m_Calls[i]);
				}
			}
			this.m_Calls.RemoveAll(new Predicate<PersistentCall>(list.Contains));
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00009070 File Offset: 0x00007270
		public void Initialize(InvokableCallList invokableList, UnityEventBase unityEventBase)
		{
			foreach (PersistentCall persistentCall in this.m_Calls)
			{
				if (persistentCall.IsValid())
				{
					BaseInvokableCall runtimeCall = persistentCall.GetRuntimeCall(unityEventBase);
					if (runtimeCall != null)
					{
						invokableList.AddPersistentInvokableCall(runtimeCall);
					}
				}
			}
		}

		// Token: 0x04000189 RID: 393
		[FormerlySerializedAs("m_Listeners")]
		[SerializeField]
		private List<PersistentCall> m_Calls;
	}
}
