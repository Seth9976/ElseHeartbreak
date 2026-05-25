using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000061 RID: 97
	[Serializable]
	public abstract class UnityEventBase : ISerializationCallbackReceiver
	{
		// Token: 0x0600020D RID: 525 RVA: 0x00009258 File Offset: 0x00007458
		protected UnityEventBase()
		{
			this.m_Calls = new InvokableCallList();
			this.m_PersistentCalls = new PersistentCallGroup();
			this.m_TypeName = base.GetType().AssemblyQualifiedName;
		}

		// Token: 0x0600020E RID: 526 RVA: 0x0000929C File Offset: 0x0000749C
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000092A0 File Offset: 0x000074A0
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.DirtyPersistentCalls();
			this.m_TypeName = base.GetType().AssemblyQualifiedName;
		}

		// Token: 0x06000210 RID: 528
		protected abstract MethodInfo FindMethod_Impl(string name, object targetObj);

		// Token: 0x06000211 RID: 529
		internal abstract BaseInvokableCall GetDelegate(object target, MethodInfo theFunction);

		// Token: 0x06000212 RID: 530 RVA: 0x000092BC File Offset: 0x000074BC
		internal MethodInfo FindMethod(PersistentCall call)
		{
			Type type = typeof(Object);
			if (!string.IsNullOrEmpty(call.arguments.unityObjectArgumentAssemblyTypeName))
			{
				type = Type.GetType(call.arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object);
			}
			return this.FindMethod(call.methodName, call.target, call.mode, type);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009328 File Offset: 0x00007528
		internal MethodInfo FindMethod(string name, object listener, PersistentListenerMode mode, Type argumentType)
		{
			switch (mode)
			{
			case PersistentListenerMode.EventDefined:
				return this.FindMethod_Impl(name, listener);
			case PersistentListenerMode.Void:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[0]);
			case PersistentListenerMode.Object:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[] { argumentType ?? typeof(Object) });
			case PersistentListenerMode.Int:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[] { typeof(int) });
			case PersistentListenerMode.Float:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[] { typeof(float) });
			case PersistentListenerMode.String:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[] { typeof(string) });
			case PersistentListenerMode.Bool:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[] { typeof(bool) });
			default:
				return null;
			}
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009408 File Offset: 0x00007608
		public int GetPersistentEventCount()
		{
			return this.m_PersistentCalls.Count;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00009418 File Offset: 0x00007618
		public Object GetPersistentTarget(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener == null) ? null : listener.target;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00009444 File Offset: 0x00007644
		public string GetPersistentMethodName(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener == null) ? string.Empty : listener.methodName;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00009474 File Offset: 0x00007674
		private void DirtyPersistentCalls()
		{
			this.m_Calls.ClearPersistent();
			this.m_CallsDirty = true;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00009488 File Offset: 0x00007688
		private void RebuildPersistentCallsIfNeeded()
		{
			if (this.m_CallsDirty)
			{
				this.m_PersistentCalls.Initialize(this.m_Calls, this);
				this.m_CallsDirty = false;
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000094BC File Offset: 0x000076BC
		public void SetPersistentListenerState(int index, UnityEventCallState state)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			if (listener != null)
			{
				listener.callState = state;
			}
			this.DirtyPersistentCalls();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000094EC File Offset: 0x000076EC
		protected void AddListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.AddListener(this.GetDelegate(targetObj, method));
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009504 File Offset: 0x00007704
		internal void AddCall(BaseInvokableCall call)
		{
			this.m_Calls.AddListener(call);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00009514 File Offset: 0x00007714
		protected void RemoveListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.RemoveListener(targetObj, method);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009524 File Offset: 0x00007724
		public void RemoveAllListeners()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00009534 File Offset: 0x00007734
		protected void Invoke(object[] parameters)
		{
			this.RebuildPersistentCallsIfNeeded();
			this.m_Calls.Invoke(parameters);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00009548 File Offset: 0x00007748
		public override string ToString()
		{
			return base.ToString() + " " + base.GetType().FullName;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00009570 File Offset: 0x00007770
		public static MethodInfo GetValidMethodInfo(object obj, string functionName, Type[] argumentTypes)
		{
			Type type = obj.GetType();
			while (type != typeof(object) && type != null)
			{
				MethodInfo method = type.GetMethod(functionName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, argumentTypes, null);
				if (method != null)
				{
					ParameterInfo[] parameters = method.GetParameters();
					bool flag = true;
					int num = 0;
					foreach (ParameterInfo parameterInfo in parameters)
					{
						Type type2 = argumentTypes[num];
						Type parameterType = parameterInfo.ParameterType;
						flag = type2.IsPrimitive == parameterType.IsPrimitive;
						if (!flag)
						{
							break;
						}
						num++;
					}
					if (flag)
					{
						return method;
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x0400018D RID: 397
		private InvokableCallList m_Calls;

		// Token: 0x0400018E RID: 398
		[SerializeField]
		[FormerlySerializedAs("m_PersistentListeners")]
		private PersistentCallGroup m_PersistentCalls;

		// Token: 0x0400018F RID: 399
		[SerializeField]
		private string m_TypeName;

		// Token: 0x04000190 RID: 400
		private bool m_CallsDirty = true;
	}
}
