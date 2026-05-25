using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000171 RID: 369
	public class Component : Object
	{
		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x06000FFB RID: 4091 RVA: 0x0001F4B8 File Offset: 0x0001D6B8
		public Transform transform
		{
			get
			{
				return this.InternalGetTransform();
			}
		}

		// Token: 0x06000FFC RID: 4092
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern Transform InternalGetTransform();

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06000FFD RID: 4093
		public extern Rigidbody rigidbody
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003C3 RID: 963
		// (get) Token: 0x06000FFE RID: 4094
		public extern Rigidbody2D rigidbody2D
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003C4 RID: 964
		// (get) Token: 0x06000FFF RID: 4095
		public extern Camera camera
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x06001000 RID: 4096
		public extern Light light
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003C6 RID: 966
		// (get) Token: 0x06001001 RID: 4097
		public extern Animation animation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06001002 RID: 4098
		public extern ConstantForce constantForce
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003C8 RID: 968
		// (get) Token: 0x06001003 RID: 4099
		public extern Renderer renderer
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003C9 RID: 969
		// (get) Token: 0x06001004 RID: 4100
		public extern AudioSource audio
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003CA RID: 970
		// (get) Token: 0x06001005 RID: 4101
		public extern GUIText guiText
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003CB RID: 971
		// (get) Token: 0x06001006 RID: 4102
		public extern NetworkView networkView
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06001007 RID: 4103
		[Obsolete("Please use guiTexture instead")]
		public extern GUIElement guiElement
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003CD RID: 973
		// (get) Token: 0x06001008 RID: 4104
		public extern GUITexture guiTexture
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003CE RID: 974
		// (get) Token: 0x06001009 RID: 4105
		public extern Collider collider
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x0600100A RID: 4106
		public extern Collider2D collider2D
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x0600100B RID: 4107
		public extern HingeJoint hingeJoint
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003D1 RID: 977
		// (get) Token: 0x0600100C RID: 4108
		public extern ParticleEmitter particleEmitter
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003D2 RID: 978
		// (get) Token: 0x0600100D RID: 4109
		public extern ParticleSystem particleSystem
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600100E RID: 4110 RVA: 0x0001F4C0 File Offset: 0x0001D6C0
		public GameObject gameObject
		{
			get
			{
				return this.InternalGetGameObject();
			}
		}

		// Token: 0x0600100F RID: 4111
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern GameObject InternalGetGameObject();

		// Token: 0x06001010 RID: 4112
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponent(Type type);

		// Token: 0x06001011 RID: 4113 RVA: 0x0001F4C8 File Offset: 0x0001D6C8
		public T GetComponent<T>() where T : Component
		{
			return this.GetComponent(typeof(T)) as T;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0001F4E4 File Offset: 0x0001D6E4
		public Component GetComponent(string type)
		{
			return this.gameObject.GetComponent(type);
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0001F4F4 File Offset: 0x0001D6F4
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type t)
		{
			return this.gameObject.GetComponentInChildren(t);
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0001F504 File Offset: 0x0001D704
		public T GetComponentInChildren<T>() where T : Component
		{
			return (T)((object)this.GetComponentInChildren(typeof(T)));
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0001F51C File Offset: 0x0001D71C
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type t)
		{
			bool flag = false;
			return this.GetComponentsInChildren(t, flag);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0001F534 File Offset: 0x0001D734
		public Component[] GetComponentsInChildren(Type t, [DefaultValue("false")] bool includeInactive)
		{
			return this.gameObject.GetComponentsInChildren(t, includeInactive);
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0001F544 File Offset: 0x0001D744
		public T[] GetComponentsInChildren<T>(bool includeInactive) where T : Component
		{
			return this.gameObject.GetComponentsInChildren<T>(includeInactive);
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0001F554 File Offset: 0x0001D754
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> result) where T : Component
		{
			this.gameObject.GetComponentsInChildren<T>(includeInactive, result);
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0001F564 File Offset: 0x0001D764
		public T[] GetComponentsInChildren<T>() where T : Component
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0001F570 File Offset: 0x0001D770
		public void GetComponentsInChildren<T>(List<T> results) where T : Component
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0001F57C File Offset: 0x0001D77C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type t)
		{
			return this.gameObject.GetComponentInParent(t);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0001F58C File Offset: 0x0001D78C
		public T GetComponentInParent<T>() where T : Component
		{
			return (T)((object)this.GetComponentInParent(typeof(T)));
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0001F5A4 File Offset: 0x0001D7A4
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type t)
		{
			bool flag = false;
			return this.GetComponentsInParent(t, flag);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0001F5BC File Offset: 0x0001D7BC
		public Component[] GetComponentsInParent(Type t, [DefaultValue("false")] bool includeInactive)
		{
			return this.gameObject.GetComponentsInParent(t, includeInactive);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0001F5CC File Offset: 0x0001D7CC
		public T[] GetComponentsInParent<T>(bool includeInactive) where T : Component
		{
			return this.gameObject.GetComponentsInParent<T>(includeInactive);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0001F5DC File Offset: 0x0001D7DC
		public T[] GetComponentsInParent<T>() where T : Component
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x06001021 RID: 4129
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component[] GetComponents(Type type);

		// Token: 0x06001022 RID: 4130
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Component[] GetComponentsWithCorrectReturnType(Type type);

		// Token: 0x06001023 RID: 4131
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetComponentsForListInternal(Type searchType, Type listElementType, bool recursive, bool includeInactive, object resultList);

		// Token: 0x06001024 RID: 4132 RVA: 0x0001F5E8 File Offset: 0x0001D7E8
		public T[] GetComponents<T>() where T : Component
		{
			return (T[])this.GetComponentsWithCorrectReturnType(typeof(T));
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0001F600 File Offset: 0x0001D800
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsForListInternal(type, typeof(Component), false, true, results);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0001F618 File Offset: 0x0001D818
		public void GetComponents<T>(List<T> results) where T : Component
		{
			this.GetComponentsForListInternal(typeof(T), typeof(T), false, true, results);
		}

		// Token: 0x170003D4 RID: 980
		// (get) Token: 0x06001027 RID: 4135
		// (set) Token: 0x06001028 RID: 4136
		[Obsolete("the active property is deprecated on components. Please use gameObject.active instead. If you meant to enable / disable a single component use enabled instead.")]
		public extern bool active
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001029 RID: 4137
		// (set) Token: 0x0600102A RID: 4138
		public extern string tag
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600102B RID: 4139
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CompareTag(string tag);

		// Token: 0x0600102C RID: 4140
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x0600102D RID: 4141 RVA: 0x0001F644 File Offset: 0x0001D844
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, sendMessageOptions);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0001F65C File Offset: 0x0001D85C
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessageUpwards(methodName, obj, sendMessageOptions);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0001F678 File Offset: 0x0001D878
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x06001030 RID: 4144
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06001031 RID: 4145 RVA: 0x0001F684 File Offset: 0x0001D884
		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, sendMessageOptions);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0001F69C File Offset: 0x0001D89C
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0001F6B8 File Offset: 0x0001D8B8
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x06001034 RID: 4148
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x06001035 RID: 4149 RVA: 0x0001F6C4 File Offset: 0x0001D8C4
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, sendMessageOptions);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0001F6DC File Offset: 0x0001D8DC
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.BroadcastMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0001F6F8 File Offset: 0x0001D8F8
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}
	}
}
