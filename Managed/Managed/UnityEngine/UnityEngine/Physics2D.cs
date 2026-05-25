using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001A9 RID: 425
	public class Physics2D
	{
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x06001421 RID: 5153
		// (set) Token: 0x06001422 RID: 5154
		public static extern int velocityIterations
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000516 RID: 1302
		// (get) Token: 0x06001423 RID: 5155
		// (set) Token: 0x06001424 RID: 5156
		public static extern int positionIterations
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000517 RID: 1303
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x00021C48 File Offset: 0x0001FE48
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x00021C60 File Offset: 0x0001FE60
		public static Vector2 gravity
		{
			get
			{
				Vector2 vector;
				Physics2D.INTERNAL_get_gravity(out vector);
				return vector;
			}
			set
			{
				Physics2D.INTERNAL_set_gravity(ref value);
			}
		}

		// Token: 0x06001427 RID: 5159
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_gravity(out Vector2 value);

		// Token: 0x06001428 RID: 5160
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_gravity(ref Vector2 value);

		// Token: 0x17000518 RID: 1304
		// (get) Token: 0x06001429 RID: 5161
		// (set) Token: 0x0600142A RID: 5162
		public static extern bool raycastsHitTriggers
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x0600142B RID: 5163
		// (set) Token: 0x0600142C RID: 5164
		public static extern bool raycastsStartInColliders
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x0600142D RID: 5165
		// (set) Token: 0x0600142E RID: 5166
		[Obsolete("This method is deprecated. Use Physics2D.changeStopsCallbacks instead.")]
		public static extern bool deleteStopsCallbacks
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x0600142F RID: 5167
		// (set) Token: 0x06001430 RID: 5168
		public static extern bool changeStopsCallbacks
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x06001431 RID: 5169
		// (set) Token: 0x06001432 RID: 5170
		public static extern float velocityThreshold
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06001433 RID: 5171
		// (set) Token: 0x06001434 RID: 5172
		public static extern float maxLinearCorrection
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06001435 RID: 5173
		// (set) Token: 0x06001436 RID: 5174
		public static extern float maxAngularCorrection
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x06001437 RID: 5175
		// (set) Token: 0x06001438 RID: 5176
		public static extern float maxTranslationSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000520 RID: 1312
		// (get) Token: 0x06001439 RID: 5177
		// (set) Token: 0x0600143A RID: 5178
		public static extern float maxRotationSpeed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000521 RID: 1313
		// (get) Token: 0x0600143B RID: 5179
		// (set) Token: 0x0600143C RID: 5180
		public static extern float minPenetrationForPenalty
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000522 RID: 1314
		// (get) Token: 0x0600143D RID: 5181
		// (set) Token: 0x0600143E RID: 5182
		public static extern float baumgarteScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x0600143F RID: 5183
		// (set) Token: 0x06001440 RID: 5184
		public static extern float baumgarteTOIScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06001441 RID: 5185
		// (set) Token: 0x06001442 RID: 5186
		public static extern float timeToSleep
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000525 RID: 1317
		// (get) Token: 0x06001443 RID: 5187
		// (set) Token: 0x06001444 RID: 5188
		public static extern float linearSleepTolerance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x06001445 RID: 5189
		// (set) Token: 0x06001446 RID: 5190
		public static extern float angularSleepTolerance
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001447 RID: 5191
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreCollision(Collider2D collider1, Collider2D collider2, [DefaultValue("true")] bool ignore);

		// Token: 0x06001448 RID: 5192 RVA: 0x00021C6C File Offset: 0x0001FE6C
		[ExcludeFromDocs]
		public static void IgnoreCollision(Collider2D collider1, Collider2D collider2)
		{
			bool flag = true;
			Physics2D.IgnoreCollision(collider1, collider2, flag);
		}

		// Token: 0x06001449 RID: 5193
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetIgnoreCollision(Collider2D collider1, Collider2D collider2);

		// Token: 0x0600144A RID: 5194
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void IgnoreLayerCollision(int layer1, int layer2, [DefaultValue("true")] bool ignore);

		// Token: 0x0600144B RID: 5195 RVA: 0x00021C84 File Offset: 0x0001FE84
		[ExcludeFromDocs]
		public static void IgnoreLayerCollision(int layer1, int layer2)
		{
			bool flag = true;
			Physics2D.IgnoreLayerCollision(layer1, layer2, flag);
		}

		// Token: 0x0600144C RID: 5196
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetIgnoreLayerCollision(int layer1, int layer2);

		// Token: 0x0600144D RID: 5197 RVA: 0x00021C9C File Offset: 0x0001FE9C
		internal static void SetEditorDragMovement(bool dragging, GameObject[] objs)
		{
			foreach (Rigidbody2D rigidbody2D in Physics2D.m_LastDisabledRigidbody2D)
			{
				rigidbody2D.isKinematic = false;
			}
			Physics2D.m_LastDisabledRigidbody2D.Clear();
			if (!dragging)
			{
				return;
			}
			foreach (GameObject gameObject in objs)
			{
				Rigidbody2D[] componentsInChildren = gameObject.GetComponentsInChildren<Rigidbody2D>(false);
				foreach (Rigidbody2D rigidbody2D2 in componentsInChildren)
				{
					if (!rigidbody2D2.isKinematic)
					{
						rigidbody2D2.isKinematic = true;
						Physics2D.m_LastDisabledRigidbody2D.Add(rigidbody2D2);
					}
				}
			}
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x00021D80 File Offset: 0x0001FF80
		private static void Internal_Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_Linecast(ref start, ref end, layerMask, minDepth, maxDepth, out raycastHit);
		}

		// Token: 0x0600144F RID: 5199
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_Linecast(ref Vector2 start, ref Vector2 end, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit);

		// Token: 0x06001450 RID: 5200 RVA: 0x00021D94 File Offset: 0x0001FF94
		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.Linecast(start, end, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00021DB4 File Offset: 0x0001FFB4
		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.Linecast(start, end, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00021DD8 File Offset: 0x0001FFD8
		[ExcludeFromDocs]
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.Linecast(start, end, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00021E00 File Offset: 0x00020000
		public static RaycastHit2D Linecast(Vector2 start, Vector2 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			RaycastHit2D raycastHit2D;
			Physics2D.Internal_Linecast(start, end, layerMask, minDepth, maxDepth, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00021E1C File Offset: 0x0002001C
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_LinecastAll(ref start, ref end, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001455 RID: 5205 RVA: 0x00021E2C File Offset: 0x0002002C
		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_LinecastAll(ref start, ref end, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001456 RID: 5206 RVA: 0x00021E4C File Offset: 0x0002004C
		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_LinecastAll(ref start, ref end, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x00021E74 File Offset: 0x00020074
		[ExcludeFromDocs]
		public static RaycastHit2D[] LinecastAll(Vector2 start, Vector2 end)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_LinecastAll(ref start, ref end, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001458 RID: 5208
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] INTERNAL_CALL_LinecastAll(ref Vector2 start, ref Vector2 end, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001459 RID: 5209 RVA: 0x00021E9C File Offset: 0x0002009C
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_LinecastNonAlloc(ref start, ref end, results, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600145A RID: 5210 RVA: 0x00021EB0 File Offset: 0x000200B0
		[ExcludeFromDocs]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_LinecastNonAlloc(ref start, ref end, results, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600145B RID: 5211 RVA: 0x00021ED4 File Offset: 0x000200D4
		[ExcludeFromDocs]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_LinecastNonAlloc(ref start, ref end, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600145C RID: 5212 RVA: 0x00021EFC File Offset: 0x000200FC
		[ExcludeFromDocs]
		public static int LinecastNonAlloc(Vector2 start, Vector2 end, RaycastHit2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_LinecastNonAlloc(ref start, ref end, results, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600145D RID: 5213
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_LinecastNonAlloc(ref Vector2 start, ref Vector2 end, RaycastHit2D[] results, int layerMask, float minDepth, float maxDepth);

		// Token: 0x0600145E RID: 5214 RVA: 0x00021F28 File Offset: 0x00020128
		private static void Internal_Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_Raycast(ref origin, ref direction, distance, layerMask, minDepth, maxDepth, out raycastHit);
		}

		// Token: 0x0600145F RID: 5215
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_Raycast(ref Vector2 origin, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit);

		// Token: 0x06001460 RID: 5216 RVA: 0x00021F3C File Offset: 0x0002013C
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.Raycast(origin, direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001461 RID: 5217 RVA: 0x00021F5C File Offset: 0x0002015C
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.Raycast(origin, direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001462 RID: 5218 RVA: 0x00021F80 File Offset: 0x00020180
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.Raycast(origin, direction, distance, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001463 RID: 5219 RVA: 0x00021FA8 File Offset: 0x000201A8
		[ExcludeFromDocs]
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.Raycast(origin, direction, positiveInfinity2, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001464 RID: 5220 RVA: 0x00021FD8 File Offset: 0x000201D8
		public static RaycastHit2D Raycast(Vector2 origin, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			RaycastHit2D raycastHit2D;
			Physics2D.Internal_Raycast(origin, direction, distance, layerMask, minDepth, maxDepth, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x06001465 RID: 5221 RVA: 0x00021FF8 File Offset: 0x000201F8
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001466 RID: 5222 RVA: 0x0002200C File Offset: 0x0002020C
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00022030 File Offset: 0x00020230
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00022058 File Offset: 0x00020258
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, distance, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00022084 File Offset: 0x00020284
		[ExcludeFromDocs]
		public static RaycastHit2D[] RaycastAll(Vector2 origin, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_RaycastAll(ref origin, ref direction, positiveInfinity2, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600146A RID: 5226
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] INTERNAL_CALL_RaycastAll(ref Vector2 origin, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x0600146B RID: 5227 RVA: 0x000220B4 File Offset: 0x000202B4
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x000220C8 File Offset: 0x000202C8
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600146D RID: 5229 RVA: 0x000220EC File Offset: 0x000202EC
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600146E RID: 5230 RVA: 0x00022114 File Offset: 0x00020314
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, distance, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600146F RID: 5231 RVA: 0x00022140 File Offset: 0x00020340
		[ExcludeFromDocs]
		public static int RaycastNonAlloc(Vector2 origin, Vector2 direction, RaycastHit2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_RaycastNonAlloc(ref origin, ref direction, results, positiveInfinity2, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001470 RID: 5232
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_RaycastNonAlloc(ref Vector2 origin, ref Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001471 RID: 5233 RVA: 0x00022170 File Offset: 0x00020370
		private static void Internal_CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_CircleCast(ref origin, radius, ref direction, distance, layerMask, minDepth, maxDepth, out raycastHit);
		}

		// Token: 0x06001472 RID: 5234
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_CircleCast(ref Vector2 origin, float radius, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit);

		// Token: 0x06001473 RID: 5235 RVA: 0x00022190 File Offset: 0x00020390
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.CircleCast(origin, radius, direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x000221B4 File Offset: 0x000203B4
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.CircleCast(origin, radius, direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000221DC File Offset: 0x000203DC
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.CircleCast(origin, radius, direction, distance, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00022204 File Offset: 0x00020404
		[ExcludeFromDocs]
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.CircleCast(origin, radius, direction, positiveInfinity2, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x00022234 File Offset: 0x00020434
		public static RaycastHit2D CircleCast(Vector2 origin, float radius, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			RaycastHit2D raycastHit2D;
			Physics2D.Internal_CircleCast(origin, radius, direction, distance, layerMask, minDepth, maxDepth, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x00022254 File Offset: 0x00020454
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_CircleCastAll(ref origin, radius, ref direction, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x00022268 File Offset: 0x00020468
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastAll(ref origin, radius, ref direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600147A RID: 5242 RVA: 0x0002228C File Offset: 0x0002048C
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastAll(ref origin, radius, ref direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600147B RID: 5243 RVA: 0x000222B4 File Offset: 0x000204B4
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_CircleCastAll(ref origin, radius, ref direction, distance, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600147C RID: 5244 RVA: 0x000222E0 File Offset: 0x000204E0
		[ExcludeFromDocs]
		public static RaycastHit2D[] CircleCastAll(Vector2 origin, float radius, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastAll(ref origin, radius, ref direction, positiveInfinity2, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600147D RID: 5245
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] INTERNAL_CALL_CircleCastAll(ref Vector2 origin, float radius, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x0600147E RID: 5246 RVA: 0x00022310 File Offset: 0x00020510
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_CircleCastNonAlloc(ref origin, radius, ref direction, results, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600147F RID: 5247 RVA: 0x00022330 File Offset: 0x00020530
		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastNonAlloc(ref origin, radius, ref direction, results, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001480 RID: 5248 RVA: 0x00022358 File Offset: 0x00020558
		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastNonAlloc(ref origin, radius, ref direction, results, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001481 RID: 5249 RVA: 0x00022384 File Offset: 0x00020584
		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_CircleCastNonAlloc(ref origin, radius, ref direction, results, distance, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001482 RID: 5250 RVA: 0x000223B0 File Offset: 0x000205B0
		[ExcludeFromDocs]
		public static int CircleCastNonAlloc(Vector2 origin, float radius, Vector2 direction, RaycastHit2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_CircleCastNonAlloc(ref origin, radius, ref direction, results, positiveInfinity2, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001483 RID: 5251
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_CircleCastNonAlloc(ref Vector2 origin, float radius, ref Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001484 RID: 5252 RVA: 0x000223E4 File Offset: 0x000205E4
		private static void Internal_BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_BoxCast(ref origin, ref size, angle, ref direction, distance, layerMask, minDepth, maxDepth, out raycastHit);
		}

		// Token: 0x06001485 RID: 5253
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_BoxCast(ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth, out RaycastHit2D raycastHit);

		// Token: 0x06001486 RID: 5254 RVA: 0x00022408 File Offset: 0x00020608
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001487 RID: 5255 RVA: 0x0002242C File Offset: 0x0002062C
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.BoxCast(origin, size, angle, direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001488 RID: 5256 RVA: 0x00022454 File Offset: 0x00020654
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.BoxCast(origin, size, angle, direction, distance, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001489 RID: 5257 RVA: 0x00022480 File Offset: 0x00020680
		[ExcludeFromDocs]
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.BoxCast(origin, size, angle, direction, positiveInfinity2, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600148A RID: 5258 RVA: 0x000224B0 File Offset: 0x000206B0
		public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			RaycastHit2D raycastHit2D;
			Physics2D.Internal_BoxCast(origin, size, angle, direction, distance, layerMask, minDepth, maxDepth, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x0600148B RID: 5259 RVA: 0x000224D4 File Offset: 0x000206D4
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_BoxCastAll(ref origin, ref size, angle, ref direction, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x0600148C RID: 5260 RVA: 0x000224F8 File Offset: 0x000206F8
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastAll(ref origin, ref size, angle, ref direction, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x0600148D RID: 5261 RVA: 0x00022520 File Offset: 0x00020720
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastAll(ref origin, ref size, angle, ref direction, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600148E RID: 5262 RVA: 0x0002254C File Offset: 0x0002074C
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_BoxCastAll(ref origin, ref size, angle, ref direction, distance, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x0600148F RID: 5263 RVA: 0x0002257C File Offset: 0x0002077C
		[ExcludeFromDocs]
		public static RaycastHit2D[] BoxCastAll(Vector2 origin, Vector2 size, float angle, Vector2 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastAll(ref origin, ref size, angle, ref direction, positiveInfinity2, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001490 RID: 5264
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] INTERNAL_CALL_BoxCastAll(ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001491 RID: 5265 RVA: 0x000225B0 File Offset: 0x000207B0
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_BoxCastNonAlloc(ref origin, ref size, angle, ref direction, results, distance, layerMask, minDepth, maxDepth);
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x000225D4 File Offset: 0x000207D4
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastNonAlloc(ref origin, ref size, angle, ref direction, results, distance, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x000225FC File Offset: 0x000207FC
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastNonAlloc(ref origin, ref size, angle, ref direction, results, distance, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0002262C File Offset: 0x0002082C
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results, float distance)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_BoxCastNonAlloc(ref origin, ref size, angle, ref direction, results, distance, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001495 RID: 5269 RVA: 0x0002265C File Offset: 0x0002085C
		[ExcludeFromDocs]
		public static int BoxCastNonAlloc(Vector2 origin, Vector2 size, float angle, Vector2 direction, RaycastHit2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			float positiveInfinity2 = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_BoxCastNonAlloc(ref origin, ref size, angle, ref direction, results, positiveInfinity2, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x06001496 RID: 5270
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_BoxCastNonAlloc(ref Vector2 origin, ref Vector2 size, float angle, ref Vector2 direction, RaycastHit2D[] results, float distance, int layerMask, float minDepth, float maxDepth);

		// Token: 0x06001497 RID: 5271 RVA: 0x00022690 File Offset: 0x00020890
		private static void Internal_GetRayIntersection(Ray ray, float distance, int layerMask, out RaycastHit2D raycastHit)
		{
			Physics2D.INTERNAL_CALL_Internal_GetRayIntersection(ref ray, distance, layerMask, out raycastHit);
		}

		// Token: 0x06001498 RID: 5272
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_GetRayIntersection(ref Ray ray, float distance, int layerMask, out RaycastHit2D raycastHit);

		// Token: 0x06001499 RID: 5273 RVA: 0x0002269C File Offset: 0x0002089C
		[ExcludeFromDocs]
		public static RaycastHit2D GetRayIntersection(Ray ray, float distance)
		{
			int num = -5;
			return Physics2D.GetRayIntersection(ray, distance, num);
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x000226B4 File Offset: 0x000208B4
		[ExcludeFromDocs]
		public static RaycastHit2D GetRayIntersection(Ray ray)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.GetRayIntersection(ray, positiveInfinity, num);
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x000226D4 File Offset: 0x000208D4
		public static RaycastHit2D GetRayIntersection(Ray ray, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			RaycastHit2D raycastHit2D;
			Physics2D.Internal_GetRayIntersection(ray, distance, layerMask, out raycastHit2D);
			return raycastHit2D;
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x000226EC File Offset: 0x000208EC
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.INTERNAL_CALL_GetRayIntersectionAll(ref ray, distance, layerMask);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x000226F8 File Offset: 0x000208F8
		[ExcludeFromDocs]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray, float distance)
		{
			int num = -5;
			return Physics2D.INTERNAL_CALL_GetRayIntersectionAll(ref ray, distance, num);
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00022714 File Offset: 0x00020914
		[ExcludeFromDocs]
		public static RaycastHit2D[] GetRayIntersectionAll(Ray ray)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_GetRayIntersectionAll(ref ray, positiveInfinity, num);
		}

		// Token: 0x0600149F RID: 5279
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit2D[] INTERNAL_CALL_GetRayIntersectionAll(ref Ray ray, float distance, int layerMask);

		// Token: 0x060014A0 RID: 5280 RVA: 0x00022734 File Offset: 0x00020934
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, [DefaultValue("Mathf.Infinity")] float distance, [DefaultValue("DefaultRaycastLayers")] int layerMask)
		{
			return Physics2D.INTERNAL_CALL_GetRayIntersectionNonAlloc(ref ray, results, distance, layerMask);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x00022740 File Offset: 0x00020940
		[ExcludeFromDocs]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results, float distance)
		{
			int num = -5;
			return Physics2D.INTERNAL_CALL_GetRayIntersectionNonAlloc(ref ray, results, distance, num);
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0002275C File Offset: 0x0002095C
		[ExcludeFromDocs]
		public static int GetRayIntersectionNonAlloc(Ray ray, RaycastHit2D[] results)
		{
			int num = -5;
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_GetRayIntersectionNonAlloc(ref ray, results, positiveInfinity, num);
		}

		// Token: 0x060014A3 RID: 5283
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_GetRayIntersectionNonAlloc(ref Ray ray, RaycastHit2D[] results, float distance, int layerMask);

		// Token: 0x060014A4 RID: 5284 RVA: 0x0002277C File Offset: 0x0002097C
		public static Collider2D OverlapPoint(Vector2 point, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapPoint(ref point, layerMask, minDepth, maxDepth);
		}

		// Token: 0x060014A5 RID: 5285 RVA: 0x00022788 File Offset: 0x00020988
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPoint(ref point, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x060014A6 RID: 5286 RVA: 0x000227A8 File Offset: 0x000209A8
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPoint(ref point, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014A7 RID: 5287 RVA: 0x000227CC File Offset: 0x000209CC
		[ExcludeFromDocs]
		public static Collider2D OverlapPoint(Vector2 point)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_OverlapPoint(ref point, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014A8 RID: 5288
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D INTERNAL_CALL_OverlapPoint(ref Vector2 point, int layerMask, float minDepth, float maxDepth);

		// Token: 0x060014A9 RID: 5289 RVA: 0x000227F4 File Offset: 0x000209F4
		public static Collider2D[] OverlapPointAll(Vector2 point, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapPointAll(ref point, layerMask, minDepth, maxDepth);
		}

		// Token: 0x060014AA RID: 5290 RVA: 0x00022800 File Offset: 0x00020A00
		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPointAll(ref point, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x060014AB RID: 5291 RVA: 0x00022820 File Offset: 0x00020A20
		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPointAll(ref point, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014AC RID: 5292 RVA: 0x00022844 File Offset: 0x00020A44
		[ExcludeFromDocs]
		public static Collider2D[] OverlapPointAll(Vector2 point)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_OverlapPointAll(ref point, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014AD RID: 5293
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] INTERNAL_CALL_OverlapPointAll(ref Vector2 point, int layerMask, float minDepth, float maxDepth);

		// Token: 0x060014AE RID: 5294 RVA: 0x0002286C File Offset: 0x00020A6C
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapPointNonAlloc(ref point, results, layerMask, minDepth, maxDepth);
		}

		// Token: 0x060014AF RID: 5295 RVA: 0x0002287C File Offset: 0x00020A7C
		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPointNonAlloc(ref point, results, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x060014B0 RID: 5296 RVA: 0x0002289C File Offset: 0x00020A9C
		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapPointNonAlloc(ref point, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014B1 RID: 5297 RVA: 0x000228C0 File Offset: 0x00020AC0
		[ExcludeFromDocs]
		public static int OverlapPointNonAlloc(Vector2 point, Collider2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_OverlapPointNonAlloc(ref point, results, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014B2 RID: 5298
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_OverlapPointNonAlloc(ref Vector2 point, Collider2D[] results, int layerMask, float minDepth, float maxDepth);

		// Token: 0x060014B3 RID: 5299 RVA: 0x000228E8 File Offset: 0x00020AE8
		public static Collider2D OverlapCircle(Vector2 point, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapCircle(ref point, radius, layerMask, minDepth, maxDepth);
		}

		// Token: 0x060014B4 RID: 5300 RVA: 0x000228F8 File Offset: 0x00020AF8
		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircle(ref point, radius, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x060014B5 RID: 5301 RVA: 0x00022918 File Offset: 0x00020B18
		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircle(ref point, radius, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014B6 RID: 5302 RVA: 0x0002293C File Offset: 0x00020B3C
		[ExcludeFromDocs]
		public static Collider2D OverlapCircle(Vector2 point, float radius)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_OverlapCircle(ref point, radius, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014B7 RID: 5303
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D INTERNAL_CALL_OverlapCircle(ref Vector2 point, float radius, int layerMask, float minDepth, float maxDepth);

		// Token: 0x060014B8 RID: 5304 RVA: 0x00022964 File Offset: 0x00020B64
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapCircleAll(ref point, radius, layerMask, minDepth, maxDepth);
		}

		// Token: 0x060014B9 RID: 5305 RVA: 0x00022974 File Offset: 0x00020B74
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircleAll(ref point, radius, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x060014BA RID: 5306 RVA: 0x00022994 File Offset: 0x00020B94
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircleAll(ref point, radius, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014BB RID: 5307 RVA: 0x000229B8 File Offset: 0x00020BB8
		[ExcludeFromDocs]
		public static Collider2D[] OverlapCircleAll(Vector2 point, float radius)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_OverlapCircleAll(ref point, radius, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014BC RID: 5308
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] INTERNAL_CALL_OverlapCircleAll(ref Vector2 point, float radius, int layerMask, float minDepth, float maxDepth);

		// Token: 0x060014BD RID: 5309 RVA: 0x000229E0 File Offset: 0x00020BE0
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapCircleNonAlloc(ref point, radius, results, layerMask, minDepth, maxDepth);
		}

		// Token: 0x060014BE RID: 5310 RVA: 0x000229F0 File Offset: 0x00020BF0
		[ExcludeFromDocs]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircleNonAlloc(ref point, radius, results, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x060014BF RID: 5311 RVA: 0x00022A10 File Offset: 0x00020C10
		[ExcludeFromDocs]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapCircleNonAlloc(ref point, radius, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014C0 RID: 5312 RVA: 0x00022A38 File Offset: 0x00020C38
		[ExcludeFromDocs]
		public static int OverlapCircleNonAlloc(Vector2 point, float radius, Collider2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_OverlapCircleNonAlloc(ref point, radius, results, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014C1 RID: 5313
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_OverlapCircleNonAlloc(ref Vector2 point, float radius, Collider2D[] results, int layerMask, float minDepth, float maxDepth);

		// Token: 0x060014C2 RID: 5314 RVA: 0x00022A60 File Offset: 0x00020C60
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapArea(ref pointA, ref pointB, layerMask, minDepth, maxDepth);
		}

		// Token: 0x060014C3 RID: 5315 RVA: 0x00022A70 File Offset: 0x00020C70
		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapArea(ref pointA, ref pointB, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x060014C4 RID: 5316 RVA: 0x00022A90 File Offset: 0x00020C90
		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapArea(ref pointA, ref pointB, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014C5 RID: 5317 RVA: 0x00022AB8 File Offset: 0x00020CB8
		[ExcludeFromDocs]
		public static Collider2D OverlapArea(Vector2 pointA, Vector2 pointB)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_OverlapArea(ref pointA, ref pointB, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014C6 RID: 5318
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D INTERNAL_CALL_OverlapArea(ref Vector2 pointA, ref Vector2 pointB, int layerMask, float minDepth, float maxDepth);

		// Token: 0x060014C7 RID: 5319 RVA: 0x00022AE0 File Offset: 0x00020CE0
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapAreaAll(ref pointA, ref pointB, layerMask, minDepth, maxDepth);
		}

		// Token: 0x060014C8 RID: 5320 RVA: 0x00022AF0 File Offset: 0x00020CF0
		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapAreaAll(ref pointA, ref pointB, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x00022B10 File Offset: 0x00020D10
		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapAreaAll(ref pointA, ref pointB, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x00022B38 File Offset: 0x00020D38
		[ExcludeFromDocs]
		public static Collider2D[] OverlapAreaAll(Vector2 pointA, Vector2 pointB)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_OverlapAreaAll(ref pointA, ref pointB, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014CB RID: 5323
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Collider2D[] INTERNAL_CALL_OverlapAreaAll(ref Vector2 pointA, ref Vector2 pointB, int layerMask, float minDepth, float maxDepth);

		// Token: 0x060014CC RID: 5324 RVA: 0x00022B60 File Offset: 0x00020D60
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, [DefaultValue("DefaultRaycastLayers")] int layerMask, [DefaultValue("-Mathf.Infinity")] float minDepth, [DefaultValue("Mathf.Infinity")] float maxDepth)
		{
			return Physics2D.INTERNAL_CALL_OverlapAreaNonAlloc(ref pointA, ref pointB, results, layerMask, minDepth, maxDepth);
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x00022B74 File Offset: 0x00020D74
		[ExcludeFromDocs]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask, float minDepth)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Physics2D.INTERNAL_CALL_OverlapAreaNonAlloc(ref pointA, ref pointB, results, layerMask, minDepth, positiveInfinity);
		}

		// Token: 0x060014CE RID: 5326 RVA: 0x00022B98 File Offset: 0x00020D98
		[ExcludeFromDocs]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results, int layerMask)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			return Physics2D.INTERNAL_CALL_OverlapAreaNonAlloc(ref pointA, ref pointB, results, layerMask, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014CF RID: 5327 RVA: 0x00022BC0 File Offset: 0x00020DC0
		[ExcludeFromDocs]
		public static int OverlapAreaNonAlloc(Vector2 pointA, Vector2 pointB, Collider2D[] results)
		{
			float positiveInfinity = float.PositiveInfinity;
			float negativeInfinity = float.NegativeInfinity;
			int num = -5;
			return Physics2D.INTERNAL_CALL_OverlapAreaNonAlloc(ref pointA, ref pointB, results, num, negativeInfinity, positiveInfinity);
		}

		// Token: 0x060014D0 RID: 5328
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_OverlapAreaNonAlloc(ref Vector2 pointA, ref Vector2 pointB, Collider2D[] results, int layerMask, float minDepth, float maxDepth);

		// Token: 0x0400068E RID: 1678
		public const int IgnoreRaycastLayer = 4;

		// Token: 0x0400068F RID: 1679
		public const int DefaultRaycastLayers = -5;

		// Token: 0x04000690 RID: 1680
		public const int AllLayers = -1;

		// Token: 0x04000691 RID: 1681
		private static List<Rigidbody2D> m_LastDisabledRigidbody2D = new List<Rigidbody2D>();
	}
}
