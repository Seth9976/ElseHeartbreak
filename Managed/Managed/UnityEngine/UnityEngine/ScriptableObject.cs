using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000090 RID: 144
	[StructLayout(LayoutKind.Sequential)]
	public class ScriptableObject : Object
	{
		// Token: 0x060002F0 RID: 752 RVA: 0x0000B124 File Offset: 0x00009324
		public ScriptableObject()
		{
			ScriptableObject.Internal_CreateScriptableObject(this);
		}

		// Token: 0x060002F1 RID: 753
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateScriptableObject([Writable] ScriptableObject self);

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B134 File Offset: 0x00009334
		[Obsolete("Use EditorUtility.SetDirty instead")]
		public void SetDirty()
		{
			ScriptableObject.INTERNAL_CALL_SetDirty(this);
		}

		// Token: 0x060002F3 RID: 755
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetDirty(ScriptableObject self);

		// Token: 0x060002F4 RID: 756
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ScriptableObject CreateInstance(string className);

		// Token: 0x060002F5 RID: 757 RVA: 0x0000B13C File Offset: 0x0000933C
		public static ScriptableObject CreateInstance(Type type)
		{
			return ScriptableObject.CreateInstanceFromType(type);
		}

		// Token: 0x060002F6 RID: 758
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject CreateInstanceFromType(Type type);

		// Token: 0x060002F7 RID: 759 RVA: 0x0000B144 File Offset: 0x00009344
		public static T CreateInstance<T>() where T : ScriptableObject
		{
			return (T)((object)ScriptableObject.CreateInstance(typeof(T)));
		}
	}
}
