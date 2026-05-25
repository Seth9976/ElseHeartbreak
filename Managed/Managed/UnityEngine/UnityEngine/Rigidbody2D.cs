using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020001B0 RID: 432
	public sealed class Rigidbody2D : Component
	{
		// Token: 0x1700052F RID: 1327
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00022D34 File Offset: 0x00020F34
		// (set) Token: 0x060014E2 RID: 5346 RVA: 0x00022D4C File Offset: 0x00020F4C
		public Vector2 position
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_position(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_position(ref value);
			}
		}

		// Token: 0x060014E3 RID: 5347
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_position(out Vector2 value);

		// Token: 0x060014E4 RID: 5348
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_position(ref Vector2 value);

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x060014E5 RID: 5349
		// (set) Token: 0x060014E6 RID: 5350
		public extern float rotation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00022D58 File Offset: 0x00020F58
		public void MovePosition(Vector2 position)
		{
			Rigidbody2D.INTERNAL_CALL_MovePosition(this, ref position);
		}

		// Token: 0x060014E8 RID: 5352
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MovePosition(Rigidbody2D self, ref Vector2 position);

		// Token: 0x060014E9 RID: 5353 RVA: 0x00022D64 File Offset: 0x00020F64
		public void MoveRotation(float angle)
		{
			Rigidbody2D.INTERNAL_CALL_MoveRotation(this, angle);
		}

		// Token: 0x060014EA RID: 5354
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MoveRotation(Rigidbody2D self, float angle);

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x00022D70 File Offset: 0x00020F70
		// (set) Token: 0x060014EC RID: 5356 RVA: 0x00022D88 File Offset: 0x00020F88
		public Vector2 velocity
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_velocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_velocity(ref value);
			}
		}

		// Token: 0x060014ED RID: 5357
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector2 value);

		// Token: 0x060014EE RID: 5358
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_velocity(ref Vector2 value);

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060014EF RID: 5359
		// (set) Token: 0x060014F0 RID: 5360
		public extern float angularVelocity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000533 RID: 1331
		// (get) Token: 0x060014F1 RID: 5361
		// (set) Token: 0x060014F2 RID: 5362
		public extern float mass
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000534 RID: 1332
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00022D94 File Offset: 0x00020F94
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x00022DAC File Offset: 0x00020FAC
		public Vector2 centerOfMass
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_centerOfMass(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_centerOfMass(ref value);
			}
		}

		// Token: 0x060014F5 RID: 5365
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_centerOfMass(out Vector2 value);

		// Token: 0x060014F6 RID: 5366
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_centerOfMass(ref Vector2 value);

		// Token: 0x17000535 RID: 1333
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x00022DB8 File Offset: 0x00020FB8
		public Vector2 worldCenterOfMass
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_worldCenterOfMass(out vector);
				return vector;
			}
		}

		// Token: 0x060014F8 RID: 5368
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldCenterOfMass(out Vector2 value);

		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060014F9 RID: 5369
		// (set) Token: 0x060014FA RID: 5370
		public extern float inertia
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060014FB RID: 5371
		// (set) Token: 0x060014FC RID: 5372
		public extern float drag
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000538 RID: 1336
		// (get) Token: 0x060014FD RID: 5373
		// (set) Token: 0x060014FE RID: 5374
		public extern float angularDrag
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x060014FF RID: 5375
		// (set) Token: 0x06001500 RID: 5376
		public extern float gravityScale
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700053A RID: 1338
		// (get) Token: 0x06001501 RID: 5377
		// (set) Token: 0x06001502 RID: 5378
		public extern bool isKinematic
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700053B RID: 1339
		// (get) Token: 0x06001503 RID: 5379
		// (set) Token: 0x06001504 RID: 5380
		public extern bool fixedAngle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001505 RID: 5381
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsSleeping();

		// Token: 0x06001506 RID: 5382
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsAwake();

		// Token: 0x06001507 RID: 5383
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Sleep();

		// Token: 0x06001508 RID: 5384
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void WakeUp();

		// Token: 0x1700053C RID: 1340
		// (get) Token: 0x06001509 RID: 5385
		// (set) Token: 0x0600150A RID: 5386
		public extern bool simulated
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700053D RID: 1341
		// (get) Token: 0x0600150B RID: 5387
		// (set) Token: 0x0600150C RID: 5388
		public extern RigidbodyInterpolation2D interpolation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700053E RID: 1342
		// (get) Token: 0x0600150D RID: 5389
		// (set) Token: 0x0600150E RID: 5390
		public extern RigidbodySleepMode2D sleepMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700053F RID: 1343
		// (get) Token: 0x0600150F RID: 5391
		// (set) Token: 0x06001510 RID: 5392
		public extern CollisionDetectionMode2D collisionDetectionMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001511 RID: 5393 RVA: 0x00022DD0 File Offset: 0x00020FD0
		public void AddForce(Vector2 force, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			Rigidbody2D.INTERNAL_CALL_AddForce(this, ref force, mode);
		}

		// Token: 0x06001512 RID: 5394 RVA: 0x00022DDC File Offset: 0x00020FDC
		[ExcludeFromDocs]
		public void AddForce(Vector2 force)
		{
			ForceMode2D forceMode2D = ForceMode2D.Force;
			Rigidbody2D.INTERNAL_CALL_AddForce(this, ref force, forceMode2D);
		}

		// Token: 0x06001513 RID: 5395
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddForce(Rigidbody2D self, ref Vector2 force, ForceMode2D mode);

		// Token: 0x06001514 RID: 5396 RVA: 0x00022DF4 File Offset: 0x00020FF4
		public void AddRelativeForce(Vector2 relativeForce, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			Rigidbody2D.INTERNAL_CALL_AddRelativeForce(this, ref relativeForce, mode);
		}

		// Token: 0x06001515 RID: 5397 RVA: 0x00022E00 File Offset: 0x00021000
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector2 relativeForce)
		{
			ForceMode2D forceMode2D = ForceMode2D.Force;
			Rigidbody2D.INTERNAL_CALL_AddRelativeForce(this, ref relativeForce, forceMode2D);
		}

		// Token: 0x06001516 RID: 5398
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddRelativeForce(Rigidbody2D self, ref Vector2 relativeForce, ForceMode2D mode);

		// Token: 0x06001517 RID: 5399 RVA: 0x00022E18 File Offset: 0x00021018
		public void AddForceAtPosition(Vector2 force, Vector2 position, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode)
		{
			Rigidbody2D.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, mode);
		}

		// Token: 0x06001518 RID: 5400 RVA: 0x00022E28 File Offset: 0x00021028
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector2 force, Vector2 position)
		{
			ForceMode2D forceMode2D = ForceMode2D.Force;
			Rigidbody2D.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, forceMode2D);
		}

		// Token: 0x06001519 RID: 5401
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddForceAtPosition(Rigidbody2D self, ref Vector2 force, ref Vector2 position, ForceMode2D mode);

		// Token: 0x0600151A RID: 5402
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void AddTorque(float torque, [DefaultValue("ForceMode2D.Force")] ForceMode2D mode);

		// Token: 0x0600151B RID: 5403 RVA: 0x00022E44 File Offset: 0x00021044
		[ExcludeFromDocs]
		public void AddTorque(float torque)
		{
			ForceMode2D forceMode2D = ForceMode2D.Force;
			this.AddTorque(torque, forceMode2D);
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00022E5C File Offset: 0x0002105C
		public Vector2 GetPoint(Vector2 point)
		{
			Vector2 vector;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetPoint(this, point, out vector);
			return vector;
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00022E74 File Offset: 0x00021074
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetPoint(Rigidbody2D rigidbody, Vector2 point, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetPoint(rigidbody, ref point, out value);
		}

		// Token: 0x0600151E RID: 5406
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetPoint(Rigidbody2D rigidbody, ref Vector2 point, out Vector2 value);

		// Token: 0x0600151F RID: 5407 RVA: 0x00022E80 File Offset: 0x00021080
		public Vector2 GetRelativePoint(Vector2 relativePoint)
		{
			Vector2 vector;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetRelativePoint(this, relativePoint, out vector);
			return vector;
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00022E98 File Offset: 0x00021098
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetRelativePoint(Rigidbody2D rigidbody, Vector2 relativePoint, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativePoint(rigidbody, ref relativePoint, out value);
		}

		// Token: 0x06001521 RID: 5409
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativePoint(Rigidbody2D rigidbody, ref Vector2 relativePoint, out Vector2 value);

		// Token: 0x06001522 RID: 5410 RVA: 0x00022EA4 File Offset: 0x000210A4
		public Vector2 GetVector(Vector2 vector)
		{
			Vector2 vector2;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetVector(this, vector, out vector2);
			return vector2;
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x00022EBC File Offset: 0x000210BC
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetVector(Rigidbody2D rigidbody, Vector2 vector, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetVector(rigidbody, ref vector, out value);
		}

		// Token: 0x06001524 RID: 5412
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetVector(Rigidbody2D rigidbody, ref Vector2 vector, out Vector2 value);

		// Token: 0x06001525 RID: 5413 RVA: 0x00022EC8 File Offset: 0x000210C8
		public Vector2 GetRelativeVector(Vector2 relativeVector)
		{
			Vector2 vector;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetRelativeVector(this, relativeVector, out vector);
			return vector;
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x00022EE0 File Offset: 0x000210E0
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetRelativeVector(Rigidbody2D rigidbody, Vector2 relativeVector, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativeVector(rigidbody, ref relativeVector, out value);
		}

		// Token: 0x06001527 RID: 5415
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativeVector(Rigidbody2D rigidbody, ref Vector2 relativeVector, out Vector2 value);

		// Token: 0x06001528 RID: 5416 RVA: 0x00022EEC File Offset: 0x000210EC
		public Vector2 GetPointVelocity(Vector2 point)
		{
			Vector2 vector;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetPointVelocity(this, point, out vector);
			return vector;
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x00022F04 File Offset: 0x00021104
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetPointVelocity(Rigidbody2D rigidbody, Vector2 point, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetPointVelocity(rigidbody, ref point, out value);
		}

		// Token: 0x0600152A RID: 5418
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetPointVelocity(Rigidbody2D rigidbody, ref Vector2 point, out Vector2 value);

		// Token: 0x0600152B RID: 5419 RVA: 0x00022F10 File Offset: 0x00021110
		public Vector2 GetRelativePointVelocity(Vector2 relativePoint)
		{
			Vector2 vector;
			Rigidbody2D.Rigidbody2D_CUSTOM_INTERNAL_GetRelativePointVelocity(this, relativePoint, out vector);
			return vector;
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00022F28 File Offset: 0x00021128
		private static void Rigidbody2D_CUSTOM_INTERNAL_GetRelativePointVelocity(Rigidbody2D rigidbody, Vector2 relativePoint, out Vector2 value)
		{
			Rigidbody2D.INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativePointVelocity(rigidbody, ref relativePoint, out value);
		}

		// Token: 0x0600152D RID: 5421
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Rigidbody2D_CUSTOM_INTERNAL_GetRelativePointVelocity(Rigidbody2D rigidbody, ref Vector2 relativePoint, out Vector2 value);
	}
}
