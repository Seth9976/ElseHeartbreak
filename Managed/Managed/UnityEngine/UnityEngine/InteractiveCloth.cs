using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001A4 RID: 420
	public sealed class InteractiveCloth : Cloth
	{
		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x060013F8 RID: 5112
		// (set) Token: 0x060013F9 RID: 5113
		public extern Mesh mesh
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x060013FA RID: 5114
		// (set) Token: 0x060013FB RID: 5115
		public extern float friction
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000509 RID: 1289
		// (get) Token: 0x060013FC RID: 5116
		// (set) Token: 0x060013FD RID: 5117
		public extern float density
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700050A RID: 1290
		// (get) Token: 0x060013FE RID: 5118
		// (set) Token: 0x060013FF RID: 5119
		public extern float pressure
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700050B RID: 1291
		// (get) Token: 0x06001400 RID: 5120
		// (set) Token: 0x06001401 RID: 5121
		public extern float collisionResponse
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700050C RID: 1292
		// (get) Token: 0x06001402 RID: 5122
		// (set) Token: 0x06001403 RID: 5123
		public extern float tearFactor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700050D RID: 1293
		// (get) Token: 0x06001404 RID: 5124
		// (set) Token: 0x06001405 RID: 5125
		public extern float attachmentTearFactor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001406 RID: 5126
		// (set) Token: 0x06001407 RID: 5127
		public extern float attachmentResponse
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001408 RID: 5128
		public extern bool isTeared
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06001409 RID: 5129 RVA: 0x00021BA0 File Offset: 0x0001FDA0
		public void AddForceAtPosition(Vector3 force, Vector3 position, float radius, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			InteractiveCloth.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, radius, mode);
		}

		// Token: 0x0600140A RID: 5130 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector3 force, Vector3 position, float radius)
		{
			ForceMode forceMode = ForceMode.Force;
			InteractiveCloth.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, radius, forceMode);
		}

		// Token: 0x0600140B RID: 5131
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddForceAtPosition(InteractiveCloth self, ref Vector3 force, ref Vector3 position, float radius, ForceMode mode);

		// Token: 0x0600140C RID: 5132
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AttachToCollider(Collider collider, [DefaultValue("false")] bool tearable, [DefaultValue("false")] bool twoWayInteraction);

		// Token: 0x0600140D RID: 5133 RVA: 0x00021BCC File Offset: 0x0001FDCC
		[ExcludeFromDocs]
		public void AttachToCollider(Collider collider, bool tearable)
		{
			bool flag = false;
			this.AttachToCollider(collider, tearable, flag);
		}

		// Token: 0x0600140E RID: 5134 RVA: 0x00021BE4 File Offset: 0x0001FDE4
		[ExcludeFromDocs]
		public void AttachToCollider(Collider collider)
		{
			bool flag = false;
			bool flag2 = false;
			this.AttachToCollider(collider, flag2, flag);
		}

		// Token: 0x0600140F RID: 5135
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DetachFromCollider(Collider collider);
	}
}
