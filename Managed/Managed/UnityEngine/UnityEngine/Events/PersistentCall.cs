using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200005E RID: 94
	[Serializable]
	internal class PersistentCall
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x00008B98 File Offset: 0x00006D98
		public Object target
		{
			get
			{
				return this.m_Target;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x00008BA0 File Offset: 0x00006DA0
		public string methodName
		{
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00008BA8 File Offset: 0x00006DA8
		// (set) Token: 0x060001EA RID: 490 RVA: 0x00008BB0 File Offset: 0x00006DB0
		public PersistentListenerMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001EB RID: 491 RVA: 0x00008BBC File Offset: 0x00006DBC
		public ArgumentCache arguments
		{
			get
			{
				return this.m_Arguments;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001EC RID: 492 RVA: 0x00008BC4 File Offset: 0x00006DC4
		// (set) Token: 0x060001ED RID: 493 RVA: 0x00008BCC File Offset: 0x00006DCC
		public UnityEventCallState callState
		{
			get
			{
				return this.m_CallState;
			}
			set
			{
				this.m_CallState = value;
			}
		}

		// Token: 0x060001EE RID: 494 RVA: 0x00008BD8 File Offset: 0x00006DD8
		public bool IsValid()
		{
			return this.target != null && !string.IsNullOrEmpty(this.methodName);
		}

		// Token: 0x060001EF RID: 495 RVA: 0x00008C08 File Offset: 0x00006E08
		public BaseInvokableCall GetRuntimeCall(UnityEventBase theEvent)
		{
			if (this.m_CallState == UnityEventCallState.Off || theEvent == null)
			{
				return null;
			}
			MethodInfo methodInfo = theEvent.FindMethod(this);
			if (methodInfo == null)
			{
				return null;
			}
			switch (this.m_Mode)
			{
			case PersistentListenerMode.EventDefined:
				return theEvent.GetDelegate(this.target, methodInfo);
			case PersistentListenerMode.Void:
				return new InvokableCall(this.target, methodInfo);
			case PersistentListenerMode.Object:
				return PersistentCall.GetObjectCall(this.target, methodInfo, this.m_Arguments);
			case PersistentListenerMode.Int:
				return new CachedInvokableCall<int>(this.target, methodInfo, this.m_Arguments.intArgument);
			case PersistentListenerMode.Float:
				return new CachedInvokableCall<float>(this.target, methodInfo, this.m_Arguments.floatArgument);
			case PersistentListenerMode.String:
				return new CachedInvokableCall<string>(this.target, methodInfo, this.m_Arguments.stringArgument);
			case PersistentListenerMode.Bool:
				return new CachedInvokableCall<bool>(this.target, methodInfo, this.m_Arguments.boolArgument);
			default:
				return null;
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00008CF8 File Offset: 0x00006EF8
		private static BaseInvokableCall GetObjectCall(Object target, MethodInfo method, ArgumentCache arguments)
		{
			Type type = typeof(Object);
			if (!string.IsNullOrEmpty(arguments.unityObjectArgumentAssemblyTypeName))
			{
				type = Type.GetType(arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object);
			}
			Type typeFromHandle = typeof(CachedInvokableCall<>);
			Type type2 = typeFromHandle.MakeGenericType(new Type[] { type });
			ConstructorInfo constructor = type2.GetConstructor(new Type[]
			{
				typeof(Object),
				typeof(MethodInfo),
				type
			});
			Object @object = arguments.unityObjectArgument;
			if (@object != null && !type.IsAssignableFrom(@object.GetType()))
			{
				@object = null;
			}
			return constructor.Invoke(new object[] { target, method, @object }) as BaseInvokableCall;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x00008DD0 File Offset: 0x00006FD0
		public void RegisterPersistentListener(Object ttarget, string mmethodName)
		{
			this.m_Target = ttarget;
			this.m_MethodName = mmethodName;
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00008DE0 File Offset: 0x00006FE0
		public void UnregisterPersistentListener()
		{
			this.m_MethodName = string.Empty;
			this.m_Target = null;
		}

		// Token: 0x04000184 RID: 388
		[SerializeField]
		[FormerlySerializedAs("instance")]
		private Object m_Target;

		// Token: 0x04000185 RID: 389
		[SerializeField]
		[FormerlySerializedAs("methodName")]
		private string m_MethodName;

		// Token: 0x04000186 RID: 390
		[SerializeField]
		[FormerlySerializedAs("mode")]
		private PersistentListenerMode m_Mode;

		// Token: 0x04000187 RID: 391
		[FormerlySerializedAs("arguments")]
		[SerializeField]
		private ArgumentCache m_Arguments = new ArgumentCache();

		// Token: 0x04000188 RID: 392
		[SerializeField]
		[FormerlySerializedAs("m_Enabled")]
		[FormerlySerializedAs("enabled")]
		private UnityEventCallState m_CallState = UnityEventCallState.RuntimeOnly;
	}
}
