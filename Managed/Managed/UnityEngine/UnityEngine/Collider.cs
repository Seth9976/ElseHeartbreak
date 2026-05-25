using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000192 RID: 402
	public class Collider : Component
	{
		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x0600130A RID: 4874
		// (set) Token: 0x0600130B RID: 4875
		public extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x0600130C RID: 4876
		public extern Rigidbody attachedRigidbody
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x0600130D RID: 4877
		// (set) Token: 0x0600130E RID: 4878
		public extern bool isTrigger
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x0600130F RID: 4879
		// (set) Token: 0x06001310 RID: 4880
		public extern PhysicMaterial material
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00021398 File Offset: 0x0001F598
		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			return Collider.INTERNAL_CALL_ClosestPointOnBounds(this, ref position);
		}

		// Token: 0x06001312 RID: 4882
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_ClosestPointOnBounds(Collider self, ref Vector3 position);

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06001313 RID: 4883
		// (set) Token: 0x06001314 RID: 4884
		public extern PhysicMaterial sharedMaterial
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x06001315 RID: 4885 RVA: 0x000213A4 File Offset: 0x0001F5A4
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_bounds(out bounds);
				return bounds;
			}
		}

		// Token: 0x06001316 RID: 4886
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x06001317 RID: 4887 RVA: 0x000213BC File Offset: 0x0001F5BC
		private static bool Internal_Raycast(Collider col, Ray ray, out RaycastHit hitInfo, float distance)
		{
			return Collider.INTERNAL_CALL_Internal_Raycast(col, ref ray, out hitInfo, distance);
		}

		// Token: 0x06001318 RID: 4888
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_Raycast(Collider col, ref Ray ray, out RaycastHit hitInfo, float distance);

		// Token: 0x06001319 RID: 4889 RVA: 0x000213C8 File Offset: 0x0001F5C8
		public bool Raycast(Ray ray, out RaycastHit hitInfo, float distance)
		{
			return Collider.Internal_Raycast(this, ray, out hitInfo, distance);
		}
	}
}
