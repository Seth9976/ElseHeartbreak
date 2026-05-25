using System;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000174 RID: 372
	public class Transform : Component, IEnumerable
	{
		// Token: 0x060010BA RID: 4282 RVA: 0x0001FBD0 File Offset: 0x0001DDD0
		protected Transform()
		{
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060010BB RID: 4283 RVA: 0x0001FBD8 File Offset: 0x0001DDD8
		// (set) Token: 0x060010BC RID: 4284 RVA: 0x0001FBF0 File Offset: 0x0001DDF0
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

		// Token: 0x060010BD RID: 4285
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_position(out Vector3 value);

		// Token: 0x060010BE RID: 4286
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_position(ref Vector3 value);

		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060010BF RID: 4287 RVA: 0x0001FBFC File Offset: 0x0001DDFC
		// (set) Token: 0x060010C0 RID: 4288 RVA: 0x0001FC14 File Offset: 0x0001DE14
		public Vector3 localPosition
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_localPosition(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_localPosition(ref value);
			}
		}

		// Token: 0x060010C1 RID: 4289
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localPosition(out Vector3 value);

		// Token: 0x060010C2 RID: 4290
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localPosition(ref Vector3 value);

		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060010C3 RID: 4291 RVA: 0x0001FC20 File Offset: 0x0001DE20
		// (set) Token: 0x060010C4 RID: 4292 RVA: 0x0001FC3C File Offset: 0x0001DE3C
		public Vector3 eulerAngles
		{
			get
			{
				return this.rotation.eulerAngles;
			}
			set
			{
				this.rotation = Quaternion.Euler(value);
			}
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060010C5 RID: 4293 RVA: 0x0001FC4C File Offset: 0x0001DE4C
		// (set) Token: 0x060010C6 RID: 4294 RVA: 0x0001FC64 File Offset: 0x0001DE64
		public Vector3 localEulerAngles
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_localEulerAngles(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_localEulerAngles(ref value);
			}
		}

		// Token: 0x060010C7 RID: 4295
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localEulerAngles(out Vector3 value);

		// Token: 0x060010C8 RID: 4296
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localEulerAngles(ref Vector3 value);

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060010C9 RID: 4297 RVA: 0x0001FC70 File Offset: 0x0001DE70
		// (set) Token: 0x060010CA RID: 4298 RVA: 0x0001FC84 File Offset: 0x0001DE84
		public Vector3 right
		{
			get
			{
				return this.rotation * Vector3.right;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.right, value);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060010CB RID: 4299 RVA: 0x0001FC98 File Offset: 0x0001DE98
		// (set) Token: 0x060010CC RID: 4300 RVA: 0x0001FCAC File Offset: 0x0001DEAC
		public Vector3 up
		{
			get
			{
				return this.rotation * Vector3.up;
			}
			set
			{
				this.rotation = Quaternion.FromToRotation(Vector3.up, value);
			}
		}

		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060010CD RID: 4301 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		// (set) Token: 0x060010CE RID: 4302 RVA: 0x0001FCD4 File Offset: 0x0001DED4
		public Vector3 forward
		{
			get
			{
				return this.rotation * Vector3.forward;
			}
			set
			{
				this.rotation = Quaternion.LookRotation(value);
			}
		}

		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060010CF RID: 4303 RVA: 0x0001FCE4 File Offset: 0x0001DEE4
		// (set) Token: 0x060010D0 RID: 4304 RVA: 0x0001FCFC File Offset: 0x0001DEFC
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

		// Token: 0x060010D1 RID: 4305
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rotation(out Quaternion value);

		// Token: 0x060010D2 RID: 4306
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rotation(ref Quaternion value);

		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060010D3 RID: 4307 RVA: 0x0001FD08 File Offset: 0x0001DF08
		// (set) Token: 0x060010D4 RID: 4308 RVA: 0x0001FD20 File Offset: 0x0001DF20
		public Quaternion localRotation
		{
			get
			{
				Quaternion quaternion;
				this.INTERNAL_get_localRotation(out quaternion);
				return quaternion;
			}
			set
			{
				this.INTERNAL_set_localRotation(ref value);
			}
		}

		// Token: 0x060010D5 RID: 4309
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localRotation(out Quaternion value);

		// Token: 0x060010D6 RID: 4310
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localRotation(ref Quaternion value);

		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060010D7 RID: 4311 RVA: 0x0001FD2C File Offset: 0x0001DF2C
		// (set) Token: 0x060010D8 RID: 4312 RVA: 0x0001FD44 File Offset: 0x0001DF44
		public Vector3 localScale
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_localScale(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_localScale(ref value);
			}
		}

		// Token: 0x060010D9 RID: 4313
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localScale(out Vector3 value);

		// Token: 0x060010DA RID: 4314
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_localScale(ref Vector3 value);

		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060010DB RID: 4315 RVA: 0x0001FD50 File Offset: 0x0001DF50
		// (set) Token: 0x060010DC RID: 4316 RVA: 0x0001FD58 File Offset: 0x0001DF58
		public Transform parent
		{
			get
			{
				return this.parentInternal;
			}
			set
			{
				if (this is RectTransform)
				{
					Debug.LogWarning("Parent of RectTransform is being set with parent property. Consider using the SetParent method instead, with the worldPositionStays argument set to false. This will retain local orientation and scale rather than world orientation and scale, which can prevent common UI scaling issues.", this);
				}
				this.parentInternal = value;
			}
		}

		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060010DD RID: 4317
		// (set) Token: 0x060010DE RID: 4318
		internal extern Transform parentInternal
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x0001FD78 File Offset: 0x0001DF78
		public void SetParent(Transform parent)
		{
			this.SetParent(parent, true);
		}

		// Token: 0x060010E0 RID: 4320
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetParent(Transform parent, bool worldPositionStays);

		// Token: 0x17000410 RID: 1040
		// (get) Token: 0x060010E1 RID: 4321 RVA: 0x0001FD84 File Offset: 0x0001DF84
		public Matrix4x4 worldToLocalMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_worldToLocalMatrix(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x060010E2 RID: 4322
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldToLocalMatrix(out Matrix4x4 value);

		// Token: 0x17000411 RID: 1041
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x0001FD9C File Offset: 0x0001DF9C
		public Matrix4x4 localToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_localToWorldMatrix(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x060010E4 RID: 4324
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_localToWorldMatrix(out Matrix4x4 value);

		// Token: 0x060010E5 RID: 4325 RVA: 0x0001FDB4 File Offset: 0x0001DFB4
		[ExcludeFromDocs]
		public void Translate(Vector3 translation)
		{
			Space space = Space.Self;
			this.Translate(translation, space);
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x0001FDCC File Offset: 0x0001DFCC
		public void Translate(Vector3 translation, [DefaultValue("Space.Self")] Space relativeTo)
		{
			if (relativeTo == Space.World)
			{
				this.position += translation;
			}
			else
			{
				this.position += this.TransformDirection(translation);
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x0001FE10 File Offset: 0x0001E010
		[ExcludeFromDocs]
		public void Translate(float x, float y, float z)
		{
			Space space = Space.Self;
			this.Translate(x, y, z, space);
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x0001FE2C File Offset: 0x0001E02C
		public void Translate(float x, float y, float z, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x0001FE40 File Offset: 0x0001E040
		public void Translate(Vector3 translation, Transform relativeTo)
		{
			if (relativeTo)
			{
				this.position += relativeTo.TransformDirection(translation);
			}
			else
			{
				this.position += translation;
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x0001FE88 File Offset: 0x0001E088
		public void Translate(float x, float y, float z, Transform relativeTo)
		{
			this.Translate(new Vector3(x, y, z), relativeTo);
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x0001FE9C File Offset: 0x0001E09C
		[ExcludeFromDocs]
		public void Rotate(Vector3 eulerAngles)
		{
			Space space = Space.Self;
			this.Rotate(eulerAngles, space);
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x0001FEB4 File Offset: 0x0001E0B4
		public void Rotate(Vector3 eulerAngles, [DefaultValue("Space.Self")] Space relativeTo)
		{
			Quaternion quaternion = Quaternion.Euler(eulerAngles.x, eulerAngles.y, eulerAngles.z);
			if (relativeTo == Space.Self)
			{
				this.localRotation *= quaternion;
			}
			else
			{
				this.rotation *= Quaternion.Inverse(this.rotation) * quaternion * this.rotation;
			}
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x0001FF28 File Offset: 0x0001E128
		[ExcludeFromDocs]
		public void Rotate(float xAngle, float yAngle, float zAngle)
		{
			Space space = Space.Self;
			this.Rotate(xAngle, yAngle, zAngle, space);
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x0001FF44 File Offset: 0x0001E144
		public void Rotate(float xAngle, float yAngle, float zAngle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			this.Rotate(new Vector3(xAngle, yAngle, zAngle), relativeTo);
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0001FF58 File Offset: 0x0001E158
		internal void RotateAroundInternal(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAroundInternal(this, ref axis, angle);
		}

		// Token: 0x060010F0 RID: 4336
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_RotateAroundInternal(Transform self, ref Vector3 axis, float angle);

		// Token: 0x060010F1 RID: 4337 RVA: 0x0001FF64 File Offset: 0x0001E164
		[ExcludeFromDocs]
		public void Rotate(Vector3 axis, float angle)
		{
			Space space = Space.Self;
			this.Rotate(axis, angle, space);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0001FF7C File Offset: 0x0001E17C
		public void Rotate(Vector3 axis, float angle, [DefaultValue("Space.Self")] Space relativeTo)
		{
			if (relativeTo == Space.Self)
			{
				this.RotateAroundInternal(base.transform.TransformDirection(axis), angle * 0.017453292f);
			}
			else
			{
				this.RotateAroundInternal(axis, angle * 0.017453292f);
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0001FFBC File Offset: 0x0001E1BC
		public void RotateAround(Vector3 point, Vector3 axis, float angle)
		{
			Vector3 vector = this.position;
			Quaternion quaternion = Quaternion.AngleAxis(angle, axis);
			Vector3 vector2 = vector - point;
			vector2 = quaternion * vector2;
			vector = point + vector2;
			this.position = vector;
			this.RotateAroundInternal(axis, angle * 0.017453292f);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x00020008 File Offset: 0x0001E208
		[ExcludeFromDocs]
		public void LookAt(Transform target)
		{
			Vector3 up = Vector3.up;
			this.LookAt(target, up);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x00020024 File Offset: 0x0001E224
		public void LookAt(Transform target, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			if (target)
			{
				this.LookAt(target.position, worldUp);
			}
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x00020040 File Offset: 0x0001E240
		public void LookAt(Vector3 worldPosition, [DefaultValue("Vector3.up")] Vector3 worldUp)
		{
			Transform.INTERNAL_CALL_LookAt(this, ref worldPosition, ref worldUp);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0002004C File Offset: 0x0001E24C
		[ExcludeFromDocs]
		public void LookAt(Vector3 worldPosition)
		{
			Vector3 up = Vector3.up;
			Transform.INTERNAL_CALL_LookAt(this, ref worldPosition, ref up);
		}

		// Token: 0x060010F8 RID: 4344
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_LookAt(Transform self, ref Vector3 worldPosition, ref Vector3 worldUp);

		// Token: 0x060010F9 RID: 4345 RVA: 0x0002006C File Offset: 0x0001E26C
		public Vector3 TransformDirection(Vector3 direction)
		{
			return Transform.INTERNAL_CALL_TransformDirection(this, ref direction);
		}

		// Token: 0x060010FA RID: 4346
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_TransformDirection(Transform self, ref Vector3 direction);

		// Token: 0x060010FB RID: 4347 RVA: 0x00020078 File Offset: 0x0001E278
		public Vector3 TransformDirection(float x, float y, float z)
		{
			return this.TransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x00020088 File Offset: 0x0001E288
		public Vector3 InverseTransformDirection(Vector3 direction)
		{
			return Transform.INTERNAL_CALL_InverseTransformDirection(this, ref direction);
		}

		// Token: 0x060010FD RID: 4349
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_InverseTransformDirection(Transform self, ref Vector3 direction);

		// Token: 0x060010FE RID: 4350 RVA: 0x00020094 File Offset: 0x0001E294
		public Vector3 InverseTransformDirection(float x, float y, float z)
		{
			return this.InverseTransformDirection(new Vector3(x, y, z));
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000200A4 File Offset: 0x0001E2A4
		public Vector3 TransformVector(Vector3 vector)
		{
			return Transform.INTERNAL_CALL_TransformVector(this, ref vector);
		}

		// Token: 0x06001100 RID: 4352
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_TransformVector(Transform self, ref Vector3 vector);

		// Token: 0x06001101 RID: 4353 RVA: 0x000200B0 File Offset: 0x0001E2B0
		public Vector3 TransformVector(float x, float y, float z)
		{
			return this.TransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000200C0 File Offset: 0x0001E2C0
		public Vector3 InverseTransformVector(Vector3 vector)
		{
			return Transform.INTERNAL_CALL_InverseTransformVector(this, ref vector);
		}

		// Token: 0x06001103 RID: 4355
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_InverseTransformVector(Transform self, ref Vector3 vector);

		// Token: 0x06001104 RID: 4356 RVA: 0x000200CC File Offset: 0x0001E2CC
		public Vector3 InverseTransformVector(float x, float y, float z)
		{
			return this.InverseTransformVector(new Vector3(x, y, z));
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000200DC File Offset: 0x0001E2DC
		public Vector3 TransformPoint(Vector3 position)
		{
			return Transform.INTERNAL_CALL_TransformPoint(this, ref position);
		}

		// Token: 0x06001106 RID: 4358
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_TransformPoint(Transform self, ref Vector3 position);

		// Token: 0x06001107 RID: 4359 RVA: 0x000200E8 File Offset: 0x0001E2E8
		public Vector3 TransformPoint(float x, float y, float z)
		{
			return this.TransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000200F8 File Offset: 0x0001E2F8
		public Vector3 InverseTransformPoint(Vector3 position)
		{
			return Transform.INTERNAL_CALL_InverseTransformPoint(this, ref position);
		}

		// Token: 0x06001109 RID: 4361
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_InverseTransformPoint(Transform self, ref Vector3 position);

		// Token: 0x0600110A RID: 4362 RVA: 0x00020104 File Offset: 0x0001E304
		public Vector3 InverseTransformPoint(float x, float y, float z)
		{
			return this.InverseTransformPoint(new Vector3(x, y, z));
		}

		// Token: 0x17000412 RID: 1042
		// (get) Token: 0x0600110B RID: 4363
		public extern Transform root
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x0600110C RID: 4364
		public extern int childCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600110D RID: 4365
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DetachChildren();

		// Token: 0x0600110E RID: 4366
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAsFirstSibling();

		// Token: 0x0600110F RID: 4367
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetAsLastSibling();

		// Token: 0x06001110 RID: 4368
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetSiblingIndex(int index);

		// Token: 0x06001111 RID: 4369
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetSiblingIndex();

		// Token: 0x06001112 RID: 4370
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Transform Find(string name);

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06001113 RID: 4371 RVA: 0x00020114 File Offset: 0x0001E314
		public Vector3 lossyScale
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_lossyScale(out vector);
				return vector;
			}
		}

		// Token: 0x06001114 RID: 4372
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_lossyScale(out Vector3 value);

		// Token: 0x06001115 RID: 4373
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsChildOf(Transform parent);

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06001116 RID: 4374
		// (set) Token: 0x06001117 RID: 4375
		public extern bool hasChanged
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x0002012C File Offset: 0x0001E32C
		public Transform FindChild(string name)
		{
			return this.Find(name);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x00020138 File Offset: 0x0001E338
		public IEnumerator GetEnumerator()
		{
			return new Transform.Enumerator(this);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x00020140 File Offset: 0x0001E340
		[Obsolete("use Transform.Rotate instead.")]
		public void RotateAround(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAround(this, ref axis, angle);
		}

		// Token: 0x0600111B RID: 4379
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_RotateAround(Transform self, ref Vector3 axis, float angle);

		// Token: 0x0600111C RID: 4380 RVA: 0x0002014C File Offset: 0x0001E34C
		[Obsolete("use Transform.Rotate instead.")]
		public void RotateAroundLocal(Vector3 axis, float angle)
		{
			Transform.INTERNAL_CALL_RotateAroundLocal(this, ref axis, angle);
		}

		// Token: 0x0600111D RID: 4381
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_RotateAroundLocal(Transform self, ref Vector3 axis, float angle);

		// Token: 0x0600111E RID: 4382
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern Transform GetChild(int index);

		// Token: 0x0600111F RID: 4383
		[Obsolete("use Transform.childCount instead.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int GetChildCount();

		// Token: 0x02000175 RID: 373
		private sealed class Enumerator : IEnumerator
		{
			// Token: 0x06001120 RID: 4384 RVA: 0x00020158 File Offset: 0x0001E358
			internal Enumerator(Transform outer)
			{
				this.outer = outer;
			}

			// Token: 0x17000416 RID: 1046
			// (get) Token: 0x06001121 RID: 4385 RVA: 0x00020170 File Offset: 0x0001E370
			public object Current
			{
				get
				{
					return this.outer.GetChild(this.currentIndex);
				}
			}

			// Token: 0x06001122 RID: 4386 RVA: 0x00020184 File Offset: 0x0001E384
			public bool MoveNext()
			{
				int childCount = this.outer.childCount;
				return ++this.currentIndex < childCount;
			}

			// Token: 0x06001123 RID: 4387 RVA: 0x000201B4 File Offset: 0x0001E3B4
			public void Reset()
			{
				this.currentIndex = -1;
			}

			// Token: 0x04000617 RID: 1559
			private Transform outer;

			// Token: 0x04000618 RID: 1560
			private int currentIndex = -1;
		}
	}
}
