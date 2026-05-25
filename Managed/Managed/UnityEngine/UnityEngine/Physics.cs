using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200017D RID: 381
	public class Physics
	{
		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600115F RID: 4447 RVA: 0x0002033C File Offset: 0x0001E53C
		// (set) Token: 0x06001160 RID: 4448 RVA: 0x00020354 File Offset: 0x0001E554
		public static Vector3 gravity
		{
			get
			{
				Vector3 vector;
				Physics.INTERNAL_get_gravity(out vector);
				return vector;
			}
			set
			{
				Physics.INTERNAL_set_gravity(ref value);
			}
		}

		// Token: 0x06001161 RID: 4449
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_gravity(out Vector3 value);

		// Token: 0x06001162 RID: 4450
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_gravity(ref Vector3 value);

		// Token: 0x1700042D RID: 1069
		// (get) Token: 0x06001163 RID: 4451
		// (set) Token: 0x06001164 RID: 4452
		public static extern float minPenetrationForPenalty
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700042E RID: 1070
		// (get) Token: 0x06001165 RID: 4453
		// (set) Token: 0x06001166 RID: 4454
		public static extern float bounceThreshold
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700042F RID: 1071
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00020360 File Offset: 0x0001E560
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x00020368 File Offset: 0x0001E568
		[Obsolete("Please use bounceThreshold instead.")]
		public static float bounceTreshold
		{
			get
			{
				return Physics.bounceThreshold;
			}
			set
			{
				Physics.bounceThreshold = value;
			}
		}

		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06001169 RID: 4457
		// (set) Token: 0x0600116A RID: 4458
		public static extern float sleepVelocity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x0600116B RID: 4459
		// (set) Token: 0x0600116C RID: 4460
		public static extern float sleepAngularVelocity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000432 RID: 1074
		// (get) Token: 0x0600116D RID: 4461
		// (set) Token: 0x0600116E RID: 4462
		public static extern float maxAngularVelocity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000433 RID: 1075
		// (get) Token: 0x0600116F RID: 4463
		// (set) Token: 0x06001170 RID: 4464
		public static extern int solverIterationCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00020370 File Offset: 0x0001E570
		private static bool Internal_Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance, int layermask)
		{
			return Physics.INTERNAL_CALL_Internal_Raycast(ref origin, ref direction, out hitInfo, distance, layermask);
		}

		// Token: 0x06001172 RID: 4466
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_Raycast(ref Vector3 origin, ref Vector3 direction, out RaycastHit hitInfo, float distance, int layermask);

		// Token: 0x06001173 RID: 4467 RVA: 0x00020380 File Offset: 0x0001E580
		private static bool Internal_CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float distance, int layermask)
		{
			return Physics.INTERNAL_CALL_Internal_CapsuleCast(ref point1, ref point2, radius, ref direction, out hitInfo, distance, layermask);
		}

		// Token: 0x06001174 RID: 4468
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_CapsuleCast(ref Vector3 point1, ref Vector3 point2, float radius, ref Vector3 direction, out RaycastHit hitInfo, float distance, int layermask);

		// Token: 0x06001175 RID: 4469 RVA: 0x00020394 File Offset: 0x0001E594
		private static bool Internal_RaycastTest(Vector3 origin, Vector3 direction, float distance, int layermask)
		{
			return Physics.INTERNAL_CALL_Internal_RaycastTest(ref origin, ref direction, distance, layermask);
		}

		// Token: 0x06001176 RID: 4470
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_RaycastTest(ref Vector3 origin, ref Vector3 direction, float distance, int layermask);

		// Token: 0x06001177 RID: 4471 RVA: 0x000203A4 File Offset: 0x0001E5A4
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, float distance)
		{
			int num = -5;
			return Physics.Raycast(origin, direction, distance, num);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x000203C0 File Offset: 0x0001E5C0
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(origin, direction, positiveInfinity, num);
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x000203E0 File Offset: 0x0001E5E0
		public static bool Raycast(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Internal_RaycastTest(origin, direction, distance, layerMask);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000203EC File Offset: 0x0001E5EC
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, float distance)
		{
			int num = -5;
			return Physics.Raycast(origin, direction, out hitInfo, distance, num);
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00020408 File Offset: 0x0001E608
		[ExcludeFromDocs]
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(origin, direction, out hitInfo, positiveInfinity, num);
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x00020428 File Offset: 0x0001E628
		public static bool Raycast(Vector3 origin, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Internal_Raycast(origin, direction, out hitInfo, distance, layerMask);
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x00020438 File Offset: 0x0001E638
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, float distance)
		{
			int num = -5;
			return Physics.Raycast(ray, distance, num);
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x00020450 File Offset: 0x0001E650
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(ray, positiveInfinity, num);
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x00020470 File Offset: 0x0001E670
		public static bool Raycast(Ray ray, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Raycast(ray.origin, ray.direction, distance, layerMask);
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x00020494 File Offset: 0x0001E694
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, float distance)
		{
			int num = -5;
			return Physics.Raycast(ray, out hitInfo, distance, num);
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000204B0 File Offset: 0x0001E6B0
		[ExcludeFromDocs]
		public static bool Raycast(Ray ray, out RaycastHit hitInfo)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.Raycast(ray, out hitInfo, positiveInfinity, num);
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000204D0 File Offset: 0x0001E6D0
		public static bool Raycast(Ray ray, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Raycast(ray.origin, ray.direction, out hitInfo, distance, layerMask);
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000204F4 File Offset: 0x0001E6F4
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray, float distance)
		{
			int num = -5;
			return Physics.RaycastAll(ray, distance, num);
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0002050C File Offset: 0x0001E70C
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Ray ray)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.RaycastAll(ray, positiveInfinity, num);
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x0002052C File Offset: 0x0001E72C
		public static RaycastHit[] RaycastAll(Ray ray, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.RaycastAll(ray.origin, ray.direction, distance, layerMask);
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x00020550 File Offset: 0x0001E750
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layermask)
		{
			return Physics.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layermask);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x00020560 File Offset: 0x0001E760
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction, float distance)
		{
			int num = -5;
			return Physics.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, num);
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x0002057C File Offset: 0x0001E77C
		[ExcludeFromDocs]
		public static RaycastHit[] RaycastAll(Vector3 origin, Vector3 direction)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.INTERNAL_CALL_RaycastAll(ref origin, ref direction, positiveInfinity, num);
		}

		// Token: 0x06001189 RID: 4489
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit[] INTERNAL_CALL_RaycastAll(ref Vector3 origin, ref Vector3 direction, float distance, int layermask);

		// Token: 0x0600118A RID: 4490 RVA: 0x000205A0 File Offset: 0x0001E7A0
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end)
		{
			int num = -5;
			return Physics.Linecast(start, end, num);
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000205B8 File Offset: 0x0001E7B8
		public static bool Linecast(Vector3 start, Vector3 end, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			Vector3 vector = end - start;
			return Physics.Raycast(start, vector, vector.magnitude, layerMask);
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000205DC File Offset: 0x0001E7DC
		[ExcludeFromDocs]
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo)
		{
			int num = -5;
			return Physics.Linecast(start, end, out hitInfo, num);
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000205F8 File Offset: 0x0001E7F8
		public static bool Linecast(Vector3 start, Vector3 end, out RaycastHit hitInfo, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			Vector3 vector = end - start;
			return Physics.Raycast(start, vector, out hitInfo, vector.magnitude, layerMask);
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x00020620 File Offset: 0x0001E820
		public static Collider[] OverlapSphere(Vector3 position, float radius, [DefaultValue("AllLayers")] int layerMask)
		{
			return Physics.INTERNAL_CALL_OverlapSphere(ref position, radius, layerMask);
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x0002062C File Offset: 0x0001E82C
		[ExcludeFromDocs]
		public static Collider[] OverlapSphere(Vector3 position, float radius)
		{
			int num = -1;
			return Physics.INTERNAL_CALL_OverlapSphere(ref position, radius, num);
		}

		// Token: 0x06001190 RID: 4496
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider[] INTERNAL_CALL_OverlapSphere(ref Vector3 position, float radius, int layerMask);

		// Token: 0x06001191 RID: 4497 RVA: 0x00020644 File Offset: 0x0001E844
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float distance)
		{
			int num = -5;
			return Physics.CapsuleCast(point1, point2, radius, direction, distance, num);
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x00020660 File Offset: 0x0001E860
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.CapsuleCast(point1, point2, radius, direction, positiveInfinity, num);
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x00020684 File Offset: 0x0001E884
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			RaycastHit raycastHit;
			return Physics.Internal_CapsuleCast(point1, point2, radius, direction, out raycastHit, distance, layerMask);
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000206A0 File Offset: 0x0001E8A0
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, float distance)
		{
			int num = -5;
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, distance, num);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000206C0 File Offset: 0x0001E8C0
		[ExcludeFromDocs]
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.CapsuleCast(point1, point2, radius, direction, out hitInfo, positiveInfinity, num);
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000206E4 File Offset: 0x0001E8E4
		public static bool CapsuleCast(Vector3 point1, Vector3 point2, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Internal_CapsuleCast(point1, point2, radius, direction, out hitInfo, distance, layerMask);
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000206F8 File Offset: 0x0001E8F8
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, float distance)
		{
			int num = -5;
			return Physics.SphereCast(origin, radius, direction, out hitInfo, distance, num);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00020714 File Offset: 0x0001E914
		[ExcludeFromDocs]
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCast(origin, radius, direction, out hitInfo, positiveInfinity, num);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00020738 File Offset: 0x0001E938
		public static bool SphereCast(Vector3 origin, float radius, Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Internal_CapsuleCast(origin, origin, radius, direction, out hitInfo, distance, layerMask);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x00020748 File Offset: 0x0001E948
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, float distance)
		{
			int num = -5;
			return Physics.SphereCast(ray, radius, distance, num);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x00020764 File Offset: 0x0001E964
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCast(ray, radius, positiveInfinity, num);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x00020784 File Offset: 0x0001E984
		public static bool SphereCast(Ray ray, float radius, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			RaycastHit raycastHit;
			return Physics.Internal_CapsuleCast(ray.origin, ray.origin, radius, ray.direction, out raycastHit, distance, layerMask);
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000207B0 File Offset: 0x0001E9B0
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, float distance)
		{
			int num = -5;
			return Physics.SphereCast(ray, radius, out hitInfo, distance, num);
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000207CC File Offset: 0x0001E9CC
		[ExcludeFromDocs]
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCast(ray, radius, out hitInfo, positiveInfinity, num);
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000207EC File Offset: 0x0001E9EC
		public static bool SphereCast(Ray ray, float radius, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.Internal_CapsuleCast(ray.origin, ray.origin, radius, ray.direction, out hitInfo, distance, layerMask);
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x00020818 File Offset: 0x0001EA18
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layermask)
		{
			return Physics.INTERNAL_CALL_CapsuleCastAll(ref point1, ref point2, radius, ref direction, distance, layermask);
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x0002082C File Offset: 0x0001EA2C
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction, float distance)
		{
			int num = -5;
			return Physics.INTERNAL_CALL_CapsuleCastAll(ref point1, ref point2, radius, ref direction, distance, num);
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x0002084C File Offset: 0x0001EA4C
		[ExcludeFromDocs]
		public static RaycastHit[] CapsuleCastAll(Vector3 point1, Vector3 point2, float radius, Vector3 direction)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.INTERNAL_CALL_CapsuleCastAll(ref point1, ref point2, radius, ref direction, positiveInfinity, num);
		}

		// Token: 0x060011A3 RID: 4515
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit[] INTERNAL_CALL_CapsuleCastAll(ref Vector3 point1, ref Vector3 point2, float radius, ref Vector3 direction, float distance, int layermask);

		// Token: 0x060011A4 RID: 4516 RVA: 0x00020870 File Offset: 0x0001EA70
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, float distance)
		{
			int num = -5;
			return Physics.SphereCastAll(origin, radius, direction, distance, num);
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x0002088C File Offset: 0x0001EA8C
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCastAll(origin, radius, direction, positiveInfinity, num);
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000208AC File Offset: 0x0001EAAC
		public static RaycastHit[] SphereCastAll(Vector3 origin, float radius, Vector3 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.CapsuleCastAll(origin, origin, radius, direction, distance, layerMask);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000208BC File Offset: 0x0001EABC
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, float distance)
		{
			int num = -5;
			return Physics.SphereCastAll(ray, radius, distance, num);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000208D8 File Offset: 0x0001EAD8
		[ExcludeFromDocs]
		public static RaycastHit[] SphereCastAll(Ray ray, float radius)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics.SphereCastAll(ray, radius, positiveInfinity, num);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000208F8 File Offset: 0x0001EAF8
		public static RaycastHit[] SphereCastAll(Ray ray, float radius, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.CapsuleCastAll(ray.origin, ray.origin, radius, ray.direction, distance, layerMask);
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x00020924 File Offset: 0x0001EB24
		public static bool CheckSphere(Vector3 position, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics.INTERNAL_CALL_CheckSphere(ref position, radius, layerMask);
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x00020930 File Offset: 0x0001EB30
		[ExcludeFromDocs]
		public static bool CheckSphere(Vector3 position, float radius)
		{
			int num = -5;
			return Physics.INTERNAL_CALL_CheckSphere(ref position, radius, num);
		}

		// Token: 0x060011AC RID: 4524
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_CheckSphere(ref Vector3 position, float radius, int layerMask);

		// Token: 0x060011AD RID: 4525 RVA: 0x0002094C File Offset: 0x0001EB4C
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius, [DefaultValue("DefaultRaycastLayers")] int layermask)
		{
			return Physics.INTERNAL_CALL_CheckCapsule(ref start, ref end, radius, layermask);
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x0002095C File Offset: 0x0001EB5C
		[ExcludeFromDocs]
		public static bool CheckCapsule(Vector3 start, Vector3 end, float radius)
		{
			int num = -5;
			return Physics.INTERNAL_CALL_CheckCapsule(ref start, ref end, radius, num);
		}

		// Token: 0x060011AF RID: 4527
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_CheckCapsule(ref Vector3 start, ref Vector3 end, float radius, int layermask);

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x060011B0 RID: 4528
		// (set) Token: 0x060011B1 RID: 4529
		[Obsolete("penetrationPenaltyForce has no effect.")]
		public static extern float penetrationPenaltyForce
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060011B2 RID: 4530
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreCollision(Collider collider1, Collider collider2, [DefaultValue("true")] bool ignore);

		// Token: 0x060011B3 RID: 4531 RVA: 0x00020978 File Offset: 0x0001EB78
		[ExcludeFromDocs]
		public static void IgnoreCollision(Collider collider1, Collider collider2)
		{
			bool flag = true;
			Physics.IgnoreCollision(collider1, collider2, flag);
		}

		// Token: 0x060011B4 RID: 4532
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreLayerCollision(int layer1, int layer2, [DefaultValue("true")] bool ignore);

		// Token: 0x060011B5 RID: 4533 RVA: 0x00020990 File Offset: 0x0001EB90
		[ExcludeFromDocs]
		public static void IgnoreLayerCollision(int layer1, int layer2)
		{
			bool flag = true;
			Physics.IgnoreLayerCollision(layer1, layer2, flag);
		}

		// Token: 0x060011B6 RID: 4534
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetIgnoreLayerCollision(int layer1, int layer2);

		// Token: 0x0400061E RID: 1566
		public const int kIgnoreRaycastLayer = 4;

		// Token: 0x0400061F RID: 1567
		public const int kDefaultRaycastLayers = -5;

		// Token: 0x04000620 RID: 1568
		public const int kAllLayers = -1;

		// Token: 0x04000621 RID: 1569
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x04000622 RID: 1570
		public const int DefaultRaycastLayers = -5;

		// Token: 0x04000623 RID: 1571
		public const int AllLayers = -1;
	}
}
