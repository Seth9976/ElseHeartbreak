using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x0200017F RID: 383
	public sealed class Rigidbody : Component
	{
		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x000209B0 File Offset: 0x0001EBB0
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x000209C8 File Offset: 0x0001EBC8
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_velocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_velocity(ref value);
			}
		}

		// Token: 0x060011BA RID: 4538
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x060011BB RID: 4539
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_velocity(ref Vector3 value);

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x000209D4 File Offset: 0x0001EBD4
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x000209EC File Offset: 0x0001EBEC
		public Vector3 angularVelocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_angularVelocity(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_angularVelocity(ref value);
			}
		}

		// Token: 0x060011BE RID: 4542
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_angularVelocity(out Vector3 value);

		// Token: 0x060011BF RID: 4543
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_angularVelocity(ref Vector3 value);

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x060011C0 RID: 4544
		// (set) Token: 0x060011C1 RID: 4545
		public extern float drag
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x060011C2 RID: 4546
		// (set) Token: 0x060011C3 RID: 4547
		public extern float angularDrag
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060011C4 RID: 4548
		// (set) Token: 0x060011C5 RID: 4549
		public extern float mass
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x000209F8 File Offset: 0x0001EBF8
		public void SetDensity(float density)
		{
			Rigidbody.INTERNAL_CALL_SetDensity(this, density);
		}

		// Token: 0x060011C7 RID: 4551
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_SetDensity(Rigidbody self, float density);

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060011C8 RID: 4552
		// (set) Token: 0x060011C9 RID: 4553
		public extern bool useGravity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x060011CA RID: 4554
		// (set) Token: 0x060011CB RID: 4555
		public extern bool isKinematic
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x060011CC RID: 4556
		// (set) Token: 0x060011CD RID: 4557
		public extern bool freezeRotation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x060011CE RID: 4558
		// (set) Token: 0x060011CF RID: 4559
		public extern RigidbodyConstraints constraints
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x060011D0 RID: 4560
		// (set) Token: 0x060011D1 RID: 4561
		public extern CollisionDetectionMode collisionDetectionMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x00020A04 File Offset: 0x0001EC04
		public void AddForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddForce(this, ref force, mode);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x00020A10 File Offset: 0x0001EC10
		[ExcludeFromDocs]
		public void AddForce(Vector3 force)
		{
			ForceMode forceMode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddForce(this, ref force, forceMode);
		}

		// Token: 0x060011D4 RID: 4564
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddForce(Rigidbody self, ref Vector3 force, ForceMode mode);

		// Token: 0x060011D5 RID: 4565 RVA: 0x00020A28 File Offset: 0x0001EC28
		[ExcludeFromDocs]
		public void AddForce(float x, float y, float z)
		{
			ForceMode forceMode = ForceMode.Force;
			this.AddForce(x, y, z, forceMode);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x00020A44 File Offset: 0x0001EC44
		public void AddForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00020A58 File Offset: 0x0001EC58
		public void AddRelativeForce(Vector3 force, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddRelativeForce(this, ref force, mode);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00020A64 File Offset: 0x0001EC64
		[ExcludeFromDocs]
		public void AddRelativeForce(Vector3 force)
		{
			ForceMode forceMode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddRelativeForce(this, ref force, forceMode);
		}

		// Token: 0x060011D9 RID: 4569
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddRelativeForce(Rigidbody self, ref Vector3 force, ForceMode mode);

		// Token: 0x060011DA RID: 4570 RVA: 0x00020A7C File Offset: 0x0001EC7C
		[ExcludeFromDocs]
		public void AddRelativeForce(float x, float y, float z)
		{
			ForceMode forceMode = ForceMode.Force;
			this.AddRelativeForce(x, y, z, forceMode);
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00020A98 File Offset: 0x0001EC98
		public void AddRelativeForce(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeForce(new Vector3(x, y, z), mode);
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00020AAC File Offset: 0x0001ECAC
		public void AddTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddTorque(this, ref torque, mode);
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00020AB8 File Offset: 0x0001ECB8
		[ExcludeFromDocs]
		public void AddTorque(Vector3 torque)
		{
			ForceMode forceMode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddTorque(this, ref torque, forceMode);
		}

		// Token: 0x060011DE RID: 4574
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddTorque(Rigidbody self, ref Vector3 torque, ForceMode mode);

		// Token: 0x060011DF RID: 4575 RVA: 0x00020AD0 File Offset: 0x0001ECD0
		[ExcludeFromDocs]
		public void AddTorque(float x, float y, float z)
		{
			ForceMode forceMode = ForceMode.Force;
			this.AddTorque(x, y, z, forceMode);
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x00020AEC File Offset: 0x0001ECEC
		public void AddTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x00020B00 File Offset: 0x0001ED00
		public void AddRelativeTorque(Vector3 torque, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddRelativeTorque(this, ref torque, mode);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x00020B0C File Offset: 0x0001ED0C
		[ExcludeFromDocs]
		public void AddRelativeTorque(Vector3 torque)
		{
			ForceMode forceMode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddRelativeTorque(this, ref torque, forceMode);
		}

		// Token: 0x060011E3 RID: 4579
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddRelativeTorque(Rigidbody self, ref Vector3 torque, ForceMode mode);

		// Token: 0x060011E4 RID: 4580 RVA: 0x00020B24 File Offset: 0x0001ED24
		[ExcludeFromDocs]
		public void AddRelativeTorque(float x, float y, float z)
		{
			ForceMode forceMode = ForceMode.Force;
			this.AddRelativeTorque(x, y, z, forceMode);
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x00020B40 File Offset: 0x0001ED40
		public void AddRelativeTorque(float x, float y, float z, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			this.AddRelativeTorque(new Vector3(x, y, z), mode);
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00020B54 File Offset: 0x0001ED54
		public void AddForceAtPosition(Vector3 force, Vector3 position, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, mode);
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x00020B64 File Offset: 0x0001ED64
		[ExcludeFromDocs]
		public void AddForceAtPosition(Vector3 force, Vector3 position)
		{
			ForceMode forceMode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddForceAtPosition(this, ref force, ref position, forceMode);
		}

		// Token: 0x060011E8 RID: 4584
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddForceAtPosition(Rigidbody self, ref Vector3 force, ref Vector3 position, ForceMode mode);

		// Token: 0x060011E9 RID: 4585 RVA: 0x00020B80 File Offset: 0x0001ED80
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, [DefaultValue("0.0F")] float upwardsModifier, [DefaultValue("ForceMode.Force")] ForceMode mode)
		{
			Rigidbody.INTERNAL_CALL_AddExplosionForce(this, explosionForce, ref explosionPosition, explosionRadius, upwardsModifier, mode);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x00020B90 File Offset: 0x0001ED90
		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius, float upwardsModifier)
		{
			ForceMode forceMode = ForceMode.Force;
			Rigidbody.INTERNAL_CALL_AddExplosionForce(this, explosionForce, ref explosionPosition, explosionRadius, upwardsModifier, forceMode);
		}

		// Token: 0x060011EB RID: 4587 RVA: 0x00020BAC File Offset: 0x0001EDAC
		[ExcludeFromDocs]
		public void AddExplosionForce(float explosionForce, Vector3 explosionPosition, float explosionRadius)
		{
			ForceMode forceMode = ForceMode.Force;
			float num = 0f;
			Rigidbody.INTERNAL_CALL_AddExplosionForce(this, explosionForce, ref explosionPosition, explosionRadius, num, forceMode);
		}

		// Token: 0x060011EC RID: 4588
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_AddExplosionForce(Rigidbody self, float explosionForce, ref Vector3 explosionPosition, float explosionRadius, float upwardsModifier, ForceMode mode);

		// Token: 0x060011ED RID: 4589 RVA: 0x00020BD0 File Offset: 0x0001EDD0
		public Vector3 ClosestPointOnBounds(Vector3 position)
		{
			return Rigidbody.INTERNAL_CALL_ClosestPointOnBounds(this, ref position);
		}

		// Token: 0x060011EE RID: 4590
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_ClosestPointOnBounds(Rigidbody self, ref Vector3 position);

		// Token: 0x060011EF RID: 4591 RVA: 0x00020BDC File Offset: 0x0001EDDC
		public Vector3 GetRelativePointVelocity(Vector3 relativePoint)
		{
			return Rigidbody.INTERNAL_CALL_GetRelativePointVelocity(this, ref relativePoint);
		}

		// Token: 0x060011F0 RID: 4592
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_GetRelativePointVelocity(Rigidbody self, ref Vector3 relativePoint);

		// Token: 0x060011F1 RID: 4593 RVA: 0x00020BE8 File Offset: 0x0001EDE8
		public Vector3 GetPointVelocity(Vector3 worldPoint)
		{
			return Rigidbody.INTERNAL_CALL_GetPointVelocity(this, ref worldPoint);
		}

		// Token: 0x060011F2 RID: 4594
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_GetPointVelocity(Rigidbody self, ref Vector3 worldPoint);

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x060011F3 RID: 4595 RVA: 0x00020BF4 File Offset: 0x0001EDF4
		// (set) Token: 0x060011F4 RID: 4596 RVA: 0x00020C0C File Offset: 0x0001EE0C
		public Vector3 centerOfMass
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_centerOfMass(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_centerOfMass(ref value);
			}
		}

		// Token: 0x060011F5 RID: 4597
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_centerOfMass(out Vector3 value);

		// Token: 0x060011F6 RID: 4598
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_centerOfMass(ref Vector3 value);

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x060011F7 RID: 4599 RVA: 0x00020C18 File Offset: 0x0001EE18
		public Vector3 worldCenterOfMass
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_worldCenterOfMass(out vector);
				return vector;
			}
		}

		// Token: 0x060011F8 RID: 4600
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldCenterOfMass(out Vector3 value);

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060011F9 RID: 4601 RVA: 0x00020C30 File Offset: 0x0001EE30
		// (set) Token: 0x060011FA RID: 4602 RVA: 0x00020C48 File Offset: 0x0001EE48
		public Quaternion inertiaTensorRotation
		{
			get
			{
				Quaternion quaternion;
				this.INTERNAL_get_inertiaTensorRotation(out quaternion);
				return quaternion;
			}
			set
			{
				this.INTERNAL_set_inertiaTensorRotation(ref value);
			}
		}

		// Token: 0x060011FB RID: 4603
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_inertiaTensorRotation(out Quaternion value);

		// Token: 0x060011FC RID: 4604
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_inertiaTensorRotation(ref Quaternion value);

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060011FD RID: 4605 RVA: 0x00020C54 File Offset: 0x0001EE54
		// (set) Token: 0x060011FE RID: 4606 RVA: 0x00020C6C File Offset: 0x0001EE6C
		public Vector3 inertiaTensor
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_inertiaTensor(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_inertiaTensor(ref value);
			}
		}

		// Token: 0x060011FF RID: 4607
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_inertiaTensor(out Vector3 value);

		// Token: 0x06001200 RID: 4608
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_inertiaTensor(ref Vector3 value);

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001201 RID: 4609
		// (set) Token: 0x06001202 RID: 4610
		public extern bool detectCollisions
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001203 RID: 4611
		// (set) Token: 0x06001204 RID: 4612
		public extern bool useConeFriction
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001205 RID: 4613 RVA: 0x00020C78 File Offset: 0x0001EE78
		// (set) Token: 0x06001206 RID: 4614 RVA: 0x00020C90 File Offset: 0x0001EE90
		public Vector3 position
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_position(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_position(ref value);
			}
		}

		// Token: 0x06001207 RID: 4615
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_position(out Vector3 value);

		// Token: 0x06001208 RID: 4616
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_position(ref Vector3 value);

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001209 RID: 4617 RVA: 0x00020C9C File Offset: 0x0001EE9C
		// (set) Token: 0x0600120A RID: 4618 RVA: 0x00020CB4 File Offset: 0x0001EEB4
		public Quaternion rotation
		{
			get
			{
				Quaternion quaternion;
				this.INTERNAL_get_rotation(out quaternion);
				return quaternion;
			}
			set
			{
				this.INTERNAL_set_rotation(ref value);
			}
		}

		// Token: 0x0600120B RID: 4619
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rotation(out Quaternion value);

		// Token: 0x0600120C RID: 4620
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rotation(ref Quaternion value);

		// Token: 0x0600120D RID: 4621 RVA: 0x00020CC0 File Offset: 0x0001EEC0
		public void MovePosition(Vector3 position)
		{
			Rigidbody.INTERNAL_CALL_MovePosition(this, ref position);
		}

		// Token: 0x0600120E RID: 4622
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MovePosition(Rigidbody self, ref Vector3 position);

		// Token: 0x0600120F RID: 4623 RVA: 0x00020CCC File Offset: 0x0001EECC
		public void MoveRotation(Quaternion rot)
		{
			Rigidbody.INTERNAL_CALL_MoveRotation(this, ref rot);
		}

		// Token: 0x06001210 RID: 4624
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_MoveRotation(Rigidbody self, ref Quaternion rot);

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001211 RID: 4625
		// (set) Token: 0x06001212 RID: 4626
		public extern RigidbodyInterpolation interpolation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001213 RID: 4627 RVA: 0x00020CD8 File Offset: 0x0001EED8
		public void Sleep()
		{
			Rigidbody.INTERNAL_CALL_Sleep(this);
		}

		// Token: 0x06001214 RID: 4628
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Sleep(Rigidbody self);

		// Token: 0x06001215 RID: 4629 RVA: 0x00020CE0 File Offset: 0x0001EEE0
		public bool IsSleeping()
		{
			return Rigidbody.INTERNAL_CALL_IsSleeping(this);
		}

		// Token: 0x06001216 RID: 4630
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_IsSleeping(Rigidbody self);

		// Token: 0x06001217 RID: 4631 RVA: 0x00020CE8 File Offset: 0x0001EEE8
		public void WakeUp()
		{
			Rigidbody.INTERNAL_CALL_WakeUp(this);
		}

		// Token: 0x06001218 RID: 4632
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_WakeUp(Rigidbody self);

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001219 RID: 4633
		// (set) Token: 0x0600121A RID: 4634
		public extern int solverIterationCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x0600121B RID: 4635
		// (set) Token: 0x0600121C RID: 4636
		public extern float sleepVelocity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x0600121D RID: 4637
		// (set) Token: 0x0600121E RID: 4638
		public extern float sleepAngularVelocity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600121F RID: 4639
		// (set) Token: 0x06001220 RID: 4640
		public extern float maxAngularVelocity
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001221 RID: 4641 RVA: 0x00020CF0 File Offset: 0x0001EEF0
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return Rigidbody.INTERNAL_CALL_SweepTest(this, ref direction, out hitInfo, distance);
		}

		// Token: 0x06001222 RID: 4642 RVA: 0x00020CFC File Offset: 0x0001EEFC
		[ExcludeFromDocs]
		public bool SweepTest(Vector3 direction, out RaycastHit hitInfo)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Rigidbody.INTERNAL_CALL_SweepTest(this, ref direction, out hitInfo, positiveInfinity);
		}

		// Token: 0x06001223 RID: 4643
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_SweepTest(Rigidbody self, ref Vector3 direction, out RaycastHit hitInfo, float distance);

		// Token: 0x06001224 RID: 4644 RVA: 0x00020D1C File Offset: 0x0001EF1C
		public RaycastHit[] SweepTestAll(Vector3 direction, [DefaultValue("Mathf.Infinity")] float distance)
		{
			return Rigidbody.INTERNAL_CALL_SweepTestAll(this, ref direction, distance);
		}

		// Token: 0x06001225 RID: 4645 RVA: 0x00020D28 File Offset: 0x0001EF28
		[ExcludeFromDocs]
		public RaycastHit[] SweepTestAll(Vector3 direction)
		{
			float positiveInfinity = float.PositiveInfinity;
			return Rigidbody.INTERNAL_CALL_SweepTestAll(this, ref direction, positiveInfinity);
		}

		// Token: 0x06001226 RID: 4646
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RaycastHit[] INTERNAL_CALL_SweepTestAll(Rigidbody self, ref Vector3 direction, float distance);

		// Token: 0x06001227 RID: 4647 RVA: 0x00020D44 File Offset: 0x0001EF44
		[Obsolete("use Rigidbody.maxAngularVelocity instead.")]
		public void SetMaxAngularVelocity(float a)
		{
			this.maxAngularVelocity = a;
		}
	}
}
