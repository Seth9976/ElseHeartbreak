using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000164 RID: 356
	public class MonoBehaviour : Behaviour
	{
		// Token: 0x06000F4E RID: 3918
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern MonoBehaviour();

		// Token: 0x06000F4F RID: 3919
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Internal_CancelInvokeAll();

		// Token: 0x06000F50 RID: 3920
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_IsInvokingAll();

		// Token: 0x06000F51 RID: 3921
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Invoke(string methodName, float time);

		// Token: 0x06000F52 RID: 3922
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void InvokeRepeating(string methodName, float time, float repeatRate);

		// Token: 0x06000F53 RID: 3923 RVA: 0x0001EFE8 File Offset: 0x0001D1E8
		public void CancelInvoke()
		{
			this.Internal_CancelInvokeAll();
		}

		// Token: 0x06000F54 RID: 3924
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CancelInvoke(string methodName);

		// Token: 0x06000F55 RID: 3925
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsInvoking(string methodName);

		// Token: 0x06000F56 RID: 3926 RVA: 0x0001EFF0 File Offset: 0x0001D1F0
		public bool IsInvoking()
		{
			return this.Internal_IsInvokingAll();
		}

		// Token: 0x06000F57 RID: 3927 RVA: 0x0001EFF8 File Offset: 0x0001D1F8
		public Coroutine StartCoroutine(IEnumerator routine)
		{
			return this.StartCoroutine_Auto(routine);
		}

		// Token: 0x06000F58 RID: 3928
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Coroutine StartCoroutine_Auto(IEnumerator routine);

		// Token: 0x06000F59 RID: 3929
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Coroutine StartCoroutine(string methodName, [DefaultValue("null")] object value);

		// Token: 0x06000F5A RID: 3930 RVA: 0x0001F004 File Offset: 0x0001D204
		[ExcludeFromDocs]
		public Coroutine StartCoroutine(string methodName)
		{
			object obj = null;
			return this.StartCoroutine(methodName, obj);
		}

		// Token: 0x06000F5B RID: 3931
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopCoroutine(string methodName);

		// Token: 0x06000F5C RID: 3932 RVA: 0x0001F01C File Offset: 0x0001D21C
		public void StopCoroutine(IEnumerator routine)
		{
			this.StopCoroutineViaEnumerator_Auto(routine);
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0001F028 File Offset: 0x0001D228
		public void StopCoroutine(Coroutine routine)
		{
			this.StopCoroutine_Auto(routine);
		}

		// Token: 0x06000F5E RID: 3934
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void StopCoroutineViaEnumerator_Auto(IEnumerator routine);

		// Token: 0x06000F5F RID: 3935
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void StopCoroutine_Auto(Coroutine routine);

		// Token: 0x06000F60 RID: 3936
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void StopAllCoroutines();

		// Token: 0x06000F61 RID: 3937 RVA: 0x0001F034 File Offset: 0x0001D234
		public static void print(object message)
		{
			Debug.Log(message);
		}

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000F62 RID: 3938
		// (set) Token: 0x06000F63 RID: 3939
		public extern bool useGUILayout
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
