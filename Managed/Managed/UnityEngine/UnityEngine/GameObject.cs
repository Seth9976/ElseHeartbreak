using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000173 RID: 371
	public sealed class GameObject : Object
	{
		// Token: 0x06001064 RID: 4196 RVA: 0x0001F750 File Offset: 0x0001D950
		public GameObject(string name)
		{
			GameObject.Internal_CreateGameObject(this, name);
		}

		// Token: 0x06001065 RID: 4197 RVA: 0x0001F760 File Offset: 0x0001D960
		public GameObject()
		{
			GameObject.Internal_CreateGameObject(this, null);
		}

		// Token: 0x06001066 RID: 4198 RVA: 0x0001F770 File Offset: 0x0001D970
		public GameObject(string name, params Type[] components)
		{
			GameObject.Internal_CreateGameObject(this, name);
			foreach (Type type in components)
			{
				this.AddComponent(type);
			}
		}

		// Token: 0x06001067 RID: 4199
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject CreatePrimitive(PrimitiveType type);

		// Token: 0x06001068 RID: 4200
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component GetComponent(Type type);

		// Token: 0x06001069 RID: 4201 RVA: 0x0001F7AC File Offset: 0x0001D9AC
		public T GetComponent<T>() where T : Component
		{
			return this.GetComponent(typeof(T)) as T;
		}

		// Token: 0x0600106A RID: 4202 RVA: 0x0001F7C8 File Offset: 0x0001D9C8
		public Component GetComponent(string type)
		{
			return this.GetComponentByName(type);
		}

		// Token: 0x0600106B RID: 4203
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Component GetComponentByName(string type);

		// Token: 0x0600106C RID: 4204 RVA: 0x0001F7D4 File Offset: 0x0001D9D4
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInChildren(Type type)
		{
			if (this.activeInHierarchy)
			{
				Component component = this.GetComponent(type);
				if (component != null)
				{
					return component;
				}
			}
			Transform transform = this.transform;
			if (transform != null)
			{
				foreach (object obj in transform)
				{
					Transform transform2 = (Transform)obj;
					Component componentInChildren = transform2.gameObject.GetComponentInChildren(type);
					if (componentInChildren != null)
					{
						return componentInChildren;
					}
				}
			}
			return null;
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0001F898 File Offset: 0x0001DA98
		public T GetComponentInChildren<T>() where T : Component
		{
			return this.GetComponentInChildren(typeof(T)) as T;
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0001F8B4 File Offset: 0x0001DAB4
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component GetComponentInParent(Type type)
		{
			if (this.activeInHierarchy)
			{
				Component component = this.GetComponent(type);
				if (component != null)
				{
					return component;
				}
			}
			Transform transform = this.transform.parent;
			if (transform != null)
			{
				while (transform != null)
				{
					if (transform.gameObject.activeInHierarchy)
					{
						Component component2 = transform.gameObject.GetComponent(type);
						if (component2 != null)
						{
							return component2;
						}
					}
					transform = transform.parent;
				}
			}
			return null;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0001F940 File Offset: 0x0001DB40
		public T GetComponentInParent<T>() where T : Component
		{
			return this.GetComponentInParent(typeof(T)) as T;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0001F95C File Offset: 0x0001DB5C
		public void GetComponentsInParent<T>(bool includeInactive, List<T> results) where T : Component
		{
			this.GetComponentsForListInternal(typeof(T), typeof(T), true, includeInactive, true, results);
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06001071 RID: 4209
		// (set) Token: 0x06001072 RID: 4210
		public extern bool isStatic
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06001073 RID: 4211
		internal extern bool isStaticBatchable
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0001F988 File Offset: 0x0001DB88
		[CanConvertToFlash]
		public Component[] GetComponents(Type type)
		{
			return this.GetComponentsInternal(type, false, false, true, false);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x0001F998 File Offset: 0x0001DB98
		public T[] GetComponents<T>() where T : Component
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, false, true, false);
		}

		// Token: 0x06001076 RID: 4214 RVA: 0x0001F9B4 File Offset: 0x0001DBB4
		public void GetComponents(Type type, List<Component> results)
		{
			this.GetComponentsForListInternal(type, typeof(Component), false, true, false, results);
		}

		// Token: 0x06001077 RID: 4215 RVA: 0x0001F9D8 File Offset: 0x0001DBD8
		public void GetComponents<T>(List<T> results) where T : Component
		{
			this.GetComponentsForListInternal(typeof(T), typeof(T), false, true, false, results);
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x0001FA04 File Offset: 0x0001DC04
		[ExcludeFromDocs]
		public Component[] GetComponentsInChildren(Type type)
		{
			bool flag = false;
			return this.GetComponentsInChildren(type, flag);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x0001FA1C File Offset: 0x0001DC1C
		public Component[] GetComponentsInChildren(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return this.GetComponentsInternal(type, false, true, includeInactive, false);
		}

		// Token: 0x0600107A RID: 4218 RVA: 0x0001FA2C File Offset: 0x0001DC2C
		public T[] GetComponentsInChildren<T>(bool includeInactive) where T : Component
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, false);
		}

		// Token: 0x0600107B RID: 4219 RVA: 0x0001FA48 File Offset: 0x0001DC48
		public void GetComponentsInChildren<T>(bool includeInactive, List<T> results) where T : Component
		{
			this.GetComponentsForListInternal(typeof(T), typeof(T), true, includeInactive, false, results);
		}

		// Token: 0x0600107C RID: 4220 RVA: 0x0001FA74 File Offset: 0x0001DC74
		public T[] GetComponentsInChildren<T>() where T : Component
		{
			return this.GetComponentsInChildren<T>(false);
		}

		// Token: 0x0600107D RID: 4221 RVA: 0x0001FA80 File Offset: 0x0001DC80
		public void GetComponentsInChildren<T>(List<T> results) where T : Component
		{
			this.GetComponentsInChildren<T>(false, results);
		}

		// Token: 0x0600107E RID: 4222 RVA: 0x0001FA8C File Offset: 0x0001DC8C
		[ExcludeFromDocs]
		public Component[] GetComponentsInParent(Type type)
		{
			bool flag = false;
			return this.GetComponentsInParent(type, flag);
		}

		// Token: 0x0600107F RID: 4223 RVA: 0x0001FAA4 File Offset: 0x0001DCA4
		public Component[] GetComponentsInParent(Type type, [DefaultValue("false")] bool includeInactive)
		{
			return this.GetComponentsInternal(type, false, true, includeInactive, true);
		}

		// Token: 0x06001080 RID: 4224 RVA: 0x0001FAB4 File Offset: 0x0001DCB4
		public T[] GetComponentsInParent<T>(bool includeInactive) where T : Component
		{
			return (T[])this.GetComponentsInternal(typeof(T), true, true, includeInactive, true);
		}

		// Token: 0x06001081 RID: 4225 RVA: 0x0001FAD0 File Offset: 0x0001DCD0
		public T[] GetComponentsInParent<T>() where T : Component
		{
			return this.GetComponentsInParent<T>(false);
		}

		// Token: 0x06001082 RID: 4226
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetComponentsForListInternal(Type searchType, Type listElementType, bool recursive, bool includeInactive, bool reverse, object resultList);

		// Token: 0x06001083 RID: 4227
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Component[] GetComponentsInternal(Type type, bool isGenericTypeArray, bool recursive, bool includeInactive, bool reverse);

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x06001084 RID: 4228
		public extern Transform transform
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001085 RID: 4229
		public extern Rigidbody rigidbody
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001086 RID: 4230
		public extern Rigidbody2D rigidbody2D
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001087 RID: 4231
		public extern Camera camera
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001088 RID: 4232
		public extern Light light
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001089 RID: 4233
		public extern Animation animation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x0600108A RID: 4234
		public extern ConstantForce constantForce
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x0600108B RID: 4235
		public extern Renderer renderer
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x0600108C RID: 4236
		public extern AudioSource audio
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x0600108D RID: 4237
		public extern GUIText guiText
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600108E RID: 4238
		public extern NetworkView networkView
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x0600108F RID: 4239
		[Obsolete("Please use guiTexture instead")]
		public extern GUIElement guiElement
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001090 RID: 4240
		public extern GUITexture guiTexture
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001091 RID: 4241
		public extern Collider collider
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001092 RID: 4242
		public extern Collider2D collider2D
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001093 RID: 4243
		public extern HingeJoint hingeJoint
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001094 RID: 4244
		public extern ParticleEmitter particleEmitter
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06001095 RID: 4245
		public extern ParticleSystem particleSystem
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06001096 RID: 4246
		// (set) Token: 0x06001097 RID: 4247
		public extern int layer
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06001098 RID: 4248
		// (set) Token: 0x06001099 RID: 4249
		[Obsolete("GameObject.active is obsolete. Use GameObject.SetActive(), GameObject.activeSelf or GameObject.activeInHierarchy.")]
		public extern bool active
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600109A RID: 4250
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetActive(bool value);

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600109B RID: 4251
		public extern bool activeSelf
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600109C RID: 4252
		public extern bool activeInHierarchy
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600109D RID: 4253
		[WrapperlessIcall]
		[Obsolete("gameObject.SetActiveRecursively() is obsolete. Use GameObject.SetActive(), which is now inherited by children.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetActiveRecursively(bool state);

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600109E RID: 4254
		// (set) Token: 0x0600109F RID: 4255
		public extern string tag
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060010A0 RID: 4256
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool CompareTag(string tag);

		// Token: 0x060010A1 RID: 4257
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject FindGameObjectWithTag(string tag);

		// Token: 0x060010A2 RID: 4258 RVA: 0x0001FADC File Offset: 0x0001DCDC
		public static GameObject FindWithTag(string tag)
		{
			return GameObject.FindGameObjectWithTag(tag);
		}

		// Token: 0x060010A3 RID: 4259
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject[] FindGameObjectsWithTag(string tag);

		// Token: 0x060010A4 RID: 4260
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessageUpwards(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060010A5 RID: 4261 RVA: 0x0001FAE4 File Offset: 0x0001DCE4
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessageUpwards(methodName, value, sendMessageOptions);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x0001FAFC File Offset: 0x0001DCFC
		[ExcludeFromDocs]
		public void SendMessageUpwards(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessageUpwards(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x0001FB18 File Offset: 0x0001DD18
		public void SendMessageUpwards(string methodName, SendMessageOptions options)
		{
			this.SendMessageUpwards(methodName, null, options);
		}

		// Token: 0x060010A8 RID: 4264
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SendMessage(string methodName, [DefaultValue("null")] object value, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060010A9 RID: 4265 RVA: 0x0001FB24 File Offset: 0x0001DD24
		[ExcludeFromDocs]
		public void SendMessage(string methodName, object value)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.SendMessage(methodName, value, sendMessageOptions);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x0001FB3C File Offset: 0x0001DD3C
		[ExcludeFromDocs]
		public void SendMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.SendMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x0001FB58 File Offset: 0x0001DD58
		public void SendMessage(string methodName, SendMessageOptions options)
		{
			this.SendMessage(methodName, null, options);
		}

		// Token: 0x060010AC RID: 4268
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void BroadcastMessage(string methodName, [DefaultValue("null")] object parameter, [DefaultValue("SendMessageOptions.RequireReceiver")] SendMessageOptions options);

		// Token: 0x060010AD RID: 4269 RVA: 0x0001FB64 File Offset: 0x0001DD64
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName, object parameter)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			this.BroadcastMessage(methodName, parameter, sendMessageOptions);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x0001FB7C File Offset: 0x0001DD7C
		[ExcludeFromDocs]
		public void BroadcastMessage(string methodName)
		{
			SendMessageOptions sendMessageOptions = SendMessageOptions.RequireReceiver;
			object obj = null;
			this.BroadcastMessage(methodName, obj, sendMessageOptions);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x0001FB98 File Offset: 0x0001DD98
		public void BroadcastMessage(string methodName, SendMessageOptions options)
		{
			this.BroadcastMessage(methodName, null, options);
		}

		// Token: 0x060010B0 RID: 4272
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Component AddComponent(string className);

		// Token: 0x060010B1 RID: 4273 RVA: 0x0001FBA4 File Offset: 0x0001DDA4
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public Component AddComponent(Type componentType)
		{
			return this.Internal_AddComponentWithType(componentType);
		}

		// Token: 0x060010B2 RID: 4274
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Component Internal_AddComponentWithType(Type componentType);

		// Token: 0x060010B3 RID: 4275 RVA: 0x0001FBB0 File Offset: 0x0001DDB0
		public T AddComponent<T>() where T : Component
		{
			return this.AddComponent(typeof(T)) as T;
		}

		// Token: 0x060010B4 RID: 4276
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateGameObject([Writable] GameObject mono, string name);

		// Token: 0x060010B5 RID: 4277
		[WrapperlessIcall]
		[Obsolete("gameObject.PlayAnimation is not supported anymore. Use animation.Play")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void PlayAnimation(AnimationClip animation);

		// Token: 0x060010B6 RID: 4278
		[WrapperlessIcall]
		[Obsolete("gameObject.StopAnimation is not supported anymore. Use animation.Stop")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopAnimation();

		// Token: 0x060010B7 RID: 4279
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern GameObject Find(string name);

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060010B8 RID: 4280 RVA: 0x0001FBCC File Offset: 0x0001DDCC
		public GameObject gameObject
		{
			get
			{
				return this;
			}
		}

		// Token: 0x060010B9 RID: 4281
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SampleAnimation(AnimationClip animation, float time);
	}
}
