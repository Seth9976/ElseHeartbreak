using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x02000170 RID: 368
	[StructLayout(LayoutKind.Sequential)]
	public class Object
	{
		// Token: 0x06000FD9 RID: 4057 RVA: 0x0001F364 File Offset: 0x0001D564
		public override bool Equals(object o)
		{
			return Object.CompareBaseObjects(this, o as Object);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0001F374 File Offset: 0x0001D574
		public override int GetHashCode()
		{
			return this.GetInstanceID();
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0001F37C File Offset: 0x0001D57C
		private static bool CompareBaseObjects(Object lhs, Object rhs)
		{
			return Object.CompareBaseObjectsInternal(lhs, rhs);
		}

		// Token: 0x06000FDC RID: 4060
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool CompareBaseObjectsInternal([Writable] Object lhs, [Writable] Object rhs);

		// Token: 0x06000FDD RID: 4061 RVA: 0x0001F388 File Offset: 0x0001D588
		[NotRenamed]
		public int GetInstanceID()
		{
			return this.m_UnityRuntimeReferenceData.instanceID;
		}

		// Token: 0x06000FDE RID: 4062
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_CloneSingle(Object data);

		// Token: 0x06000FDF RID: 4063 RVA: 0x0001F398 File Offset: 0x0001D598
		private static Object Internal_InstantiateSingle(Object data, Vector3 pos, Quaternion rot)
		{
			return Object.INTERNAL_CALL_Internal_InstantiateSingle(data, ref pos, ref rot);
		}

		// Token: 0x06000FE0 RID: 4064
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object INTERNAL_CALL_Internal_InstantiateSingle(Object data, ref Vector3 pos, ref Quaternion rot);

		// Token: 0x06000FE1 RID: 4065 RVA: 0x0001F3A4 File Offset: 0x0001D5A4
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original, Vector3 position, Quaternion rotation)
		{
			Object.CheckNullArgument(original, "The prefab you want to instantiate is null.");
			return Object.Internal_InstantiateSingle(original, position, rotation);
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x0001F3BC File Offset: 0x0001D5BC
		[TypeInferenceRule(TypeInferenceRules.TypeOfFirstArgument)]
		public static Object Instantiate(Object original)
		{
			Object.CheckNullArgument(original, "The thing you want to instantiate is null.");
			return Object.Internal_CloneSingle(original);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x0001F3D0 File Offset: 0x0001D5D0
		private static void CheckNullArgument(object arg, string message)
		{
			if (arg == null)
			{
				throw new ArgumentException(message);
			}
		}

		// Token: 0x06000FE4 RID: 4068
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Destroy(Object obj, [DefaultValue("0.0F")] float t);

		// Token: 0x06000FE5 RID: 4069 RVA: 0x0001F3E0 File Offset: 0x0001D5E0
		[ExcludeFromDocs]
		public static void Destroy(Object obj)
		{
			float num = 0f;
			Object.Destroy(obj, num);
		}

		// Token: 0x06000FE6 RID: 4070
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyImmediate(Object obj, [DefaultValue("false")] bool allowDestroyingAssets);

		// Token: 0x06000FE7 RID: 4071 RVA: 0x0001F3FC File Offset: 0x0001D5FC
		[ExcludeFromDocs]
		public static void DestroyImmediate(Object obj)
		{
			bool flag = false;
			Object.DestroyImmediate(obj, flag);
		}

		// Token: 0x06000FE8 RID: 4072
		[WrapperlessIcall]
		[TypeInferenceRule(TypeInferenceRules.ArrayOfTypeReferencedByFirstArgument)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfType(Type type);

		// Token: 0x06000FE9 RID: 4073 RVA: 0x0001F414 File Offset: 0x0001D614
		public static T[] FindObjectsOfType<T>() where T : Object
		{
			return Resources.ConvertObjects<T>(Object.FindObjectsOfType(typeof(T)));
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x0001F42C File Offset: 0x0001D62C
		[TypeInferenceRule(TypeInferenceRules.TypeReferencedByFirstArgument)]
		public static Object FindObjectOfType(Type type)
		{
			Object[] array = Object.FindObjectsOfType(type);
			if (array.Length > 0)
			{
				return array[0];
			}
			return null;
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x0001F450 File Offset: 0x0001D650
		public static T FindObjectOfType<T>() where T : Object
		{
			return (T)((object)Object.FindObjectOfType(typeof(T)));
		}

		// Token: 0x170003BF RID: 959
		// (get) Token: 0x06000FEC RID: 4076
		// (set) Token: 0x06000FED RID: 4077
		public extern string name
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000FEE RID: 4078
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DontDestroyOnLoad(Object target);

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000FEF RID: 4079
		// (set) Token: 0x06000FF0 RID: 4080
		public extern HideFlags hideFlags
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000FF1 RID: 4081
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyObject(Object obj, [DefaultValue("0.0F")] float t);

		// Token: 0x06000FF2 RID: 4082 RVA: 0x0001F468 File Offset: 0x0001D668
		[ExcludeFromDocs]
		public static void DestroyObject(Object obj)
		{
			float num = 0f;
			Object.DestroyObject(obj, num);
		}

		// Token: 0x06000FF3 RID: 4083
		[WrapperlessIcall]
		[Obsolete("use Object.FindObjectsOfType instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindSceneObjectsOfType(Type type);

		// Token: 0x06000FF4 RID: 4084
		[Obsolete("use Resources.FindObjectsOfTypeAll instead.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Object[] FindObjectsOfTypeIncludingAssets(Type type);

		// Token: 0x06000FF5 RID: 4085 RVA: 0x0001F484 File Offset: 0x0001D684
		[Obsolete("Please use Resources.FindObjectsOfTypeAll instead")]
		public static Object[] FindObjectsOfTypeAll(Type type)
		{
			return Resources.FindObjectsOfTypeAll(type);
		}

		// Token: 0x06000FF6 RID: 4086
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern string ToString();

		// Token: 0x06000FF7 RID: 4087 RVA: 0x0001F48C File Offset: 0x0001D68C
		public static implicit operator bool(Object exists)
		{
			return !Object.CompareBaseObjects(exists, null);
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x0001F498 File Offset: 0x0001D698
		public static bool operator ==(Object x, Object y)
		{
			return Object.CompareBaseObjects(x, y);
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
		public static bool operator !=(Object x, Object y)
		{
			return !Object.CompareBaseObjects(x, y);
		}

		// Token: 0x04000616 RID: 1558
		private ReferenceData m_UnityRuntimeReferenceData;
	}
}
