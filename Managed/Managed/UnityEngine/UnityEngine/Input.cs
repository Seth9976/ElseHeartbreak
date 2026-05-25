using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200016F RID: 367
	public sealed class Input
	{
		// Token: 0x06000F99 RID: 3993
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int mainGyroIndex_Internal();

		// Token: 0x06000F9A RID: 3994
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyInt(int key);

		// Token: 0x06000F9B RID: 3995
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyString(string name);

		// Token: 0x06000F9C RID: 3996
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpInt(int key);

		// Token: 0x06000F9D RID: 3997
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyUpString(string name);

		// Token: 0x06000F9E RID: 3998
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownInt(int key);

		// Token: 0x06000F9F RID: 3999
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetKeyDownString(string name);

		// Token: 0x06000FA0 RID: 4000
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxis(string axisName);

		// Token: 0x06000FA1 RID: 4001
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float GetAxisRaw(string axisName);

		// Token: 0x06000FA2 RID: 4002
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButton(string buttonName);

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000FA3 RID: 4003
		// (set) Token: 0x06000FA4 RID: 4004
		public static extern bool compensateSensors
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000FA5 RID: 4005
		[Obsolete("isGyroAvailable property is deprecated. Please use SystemInfo.supportsGyroscope instead.")]
		public static extern bool isGyroAvailable
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x0001F1C8 File Offset: 0x0001D3C8
		public static Gyroscope gyro
		{
			get
			{
				if (Input.m_MainGyro == null)
				{
					Input.m_MainGyro = new Gyroscope(Input.mainGyroIndex_Internal());
				}
				return Input.m_MainGyro;
			}
		}

		// Token: 0x06000FA7 RID: 4007
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButtonDown(string buttonName);

		// Token: 0x06000FA8 RID: 4008
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetButtonUp(string buttonName);

		// Token: 0x06000FA9 RID: 4009 RVA: 0x0001F1E8 File Offset: 0x0001D3E8
		public static bool GetKey(string name)
		{
			return Input.GetKeyString(name);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0001F1F0 File Offset: 0x0001D3F0
		public static bool GetKey(KeyCode key)
		{
			return Input.GetKeyInt((int)key);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0001F1F8 File Offset: 0x0001D3F8
		public static bool GetKeyDown(string name)
		{
			return Input.GetKeyDownString(name);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x0001F200 File Offset: 0x0001D400
		public static bool GetKeyDown(KeyCode key)
		{
			return Input.GetKeyDownInt((int)key);
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x0001F208 File Offset: 0x0001D408
		public static bool GetKeyUp(string name)
		{
			return Input.GetKeyUpString(name);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x0001F210 File Offset: 0x0001D410
		public static bool GetKeyUp(KeyCode key)
		{
			return Input.GetKeyUpInt((int)key);
		}

		// Token: 0x06000FAF RID: 4015
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string[] GetJoystickNames();

		// Token: 0x06000FB0 RID: 4016
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButton(int button);

		// Token: 0x06000FB1 RID: 4017
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonDown(int button);

		// Token: 0x06000FB2 RID: 4018
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool GetMouseButtonUp(int button);

		// Token: 0x06000FB3 RID: 4019
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ResetInputAxes();

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000FB4 RID: 4020 RVA: 0x0001F218 File Offset: 0x0001D418
		public static Vector3 mousePosition
		{
			get
			{
				Vector3 vector;
				Input.INTERNAL_get_mousePosition(out vector);
				return vector;
			}
		}

		// Token: 0x06000FB5 RID: 4021
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_mousePosition(out Vector3 value);

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000FB6 RID: 4022 RVA: 0x0001F230 File Offset: 0x0001D430
		public static Vector3 mouseScrollDelta
		{
			get
			{
				Vector3 vector;
				Input.INTERNAL_get_mouseScrollDelta(out vector);
				return vector;
			}
		}

		// Token: 0x06000FB7 RID: 4023
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_mouseScrollDelta(out Vector3 value);

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000FB8 RID: 4024 RVA: 0x0001F248 File Offset: 0x0001D448
		public static bool mousePresent
		{
			get
			{
				return !Input.touchSupported;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06000FB9 RID: 4025
		// (set) Token: 0x06000FBA RID: 4026
		public static extern bool simulateMouseWithTouches
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000FBB RID: 4027
		public static extern bool anyKey
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000FBC RID: 4028
		public static extern bool anyKeyDown
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000FBD RID: 4029
		public static extern string inputString
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x06000FBE RID: 4030 RVA: 0x0001F254 File Offset: 0x0001D454
		public static Vector3 acceleration
		{
			get
			{
				Vector3 vector;
				Input.INTERNAL_get_acceleration(out vector);
				return vector;
			}
		}

		// Token: 0x06000FBF RID: 4031
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_acceleration(out Vector3 value);

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06000FC0 RID: 4032 RVA: 0x0001F26C File Offset: 0x0001D46C
		public static AccelerationEvent[] accelerationEvents
		{
			get
			{
				int accelerationEventCount = Input.accelerationEventCount;
				AccelerationEvent[] array = new AccelerationEvent[accelerationEventCount];
				for (int i = 0; i < accelerationEventCount; i++)
				{
					array[i] = Input.GetAccelerationEvent(i);
				}
				return array;
			}
		}

		// Token: 0x06000FC1 RID: 4033
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern AccelerationEvent GetAccelerationEvent(int index);

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06000FC2 RID: 4034
		public static extern int accelerationEventCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x0001F2AC File Offset: 0x0001D4AC
		public static Touch[] touches
		{
			get
			{
				int touchCount = Input.touchCount;
				Touch[] array = new Touch[touchCount];
				for (int i = 0; i < touchCount; i++)
				{
					array[i] = Input.GetTouch(i);
				}
				return array;
			}
		}

		// Token: 0x06000FC4 RID: 4036
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern Touch GetTouch(int index);

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000FC5 RID: 4037
		public static extern int touchCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000FC6 RID: 4038
		// (set) Token: 0x06000FC7 RID: 4039
		[Obsolete("eatKeyPressOnTextFieldFocus property is deprecated, and only provided to support legacy behavior.")]
		public static extern bool eatKeyPressOnTextFieldFocus
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x0001F2EC File Offset: 0x0001D4EC
		public static bool touchSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000FC9 RID: 4041
		// (set) Token: 0x06000FCA RID: 4042
		public static extern bool multiTouchEnabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x0001F2F0 File Offset: 0x0001D4F0
		public static LocationService location
		{
			get
			{
				if (Input.locationServiceInstance == null)
				{
					Input.locationServiceInstance = new LocationService();
				}
				return Input.locationServiceInstance;
			}
		}

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x0001F30C File Offset: 0x0001D50C
		public static Compass compass
		{
			get
			{
				if (Input.compassInstance == null)
				{
					Input.compassInstance = new Compass();
				}
				return Input.compassInstance;
			}
		}

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x06000FCD RID: 4045
		public static extern DeviceOrientation deviceOrientation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x0001F328 File Offset: 0x0001D528
		[Obsolete("Use ps3 move API instead", true)]
		public static Quaternion GetRotation(int deviceID)
		{
			return Quaternion.identity;
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x0001F330 File Offset: 0x0001D530
		[Obsolete("Use ps3 move API instead", true)]
		public static Vector3 GetPosition(int deviceID)
		{
			return Vector3.zero;
		}

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x06000FD0 RID: 4048
		// (set) Token: 0x06000FD1 RID: 4049
		public static extern IMECompositionMode imeCompositionMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x06000FD2 RID: 4050
		public static extern string compositionString
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06000FD3 RID: 4051
		public static extern bool imeIsSelected
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x0001F338 File Offset: 0x0001D538
		// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x0001F350 File Offset: 0x0001D550
		public static Vector2 compositionCursorPos
		{
			get
			{
				Vector2 vector;
				Input.INTERNAL_get_compositionCursorPos(out vector);
				return vector;
			}
			set
			{
				Input.INTERNAL_set_compositionCursorPos(ref value);
			}
		}

		// Token: 0x06000FD6 RID: 4054
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_compositionCursorPos(out Vector2 value);

		// Token: 0x06000FD7 RID: 4055
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_compositionCursorPos(ref Vector2 value);

		// Token: 0x04000613 RID: 1555
		private static Gyroscope m_MainGyro;

		// Token: 0x04000614 RID: 1556
		private static LocationService locationServiceInstance;

		// Token: 0x04000615 RID: 1557
		private static Compass compassInstance;
	}
}
