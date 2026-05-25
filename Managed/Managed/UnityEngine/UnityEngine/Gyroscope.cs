using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200016A RID: 362
	public sealed class Gyroscope
	{
		// Token: 0x06000F6D RID: 3949 RVA: 0x0001F084 File Offset: 0x0001D284
		internal Gyroscope(int index)
		{
			this.m_GyroIndex = index;
		}

		// Token: 0x06000F6E RID: 3950
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 rotationRate_Internal(int idx);

		// Token: 0x06000F6F RID: 3951
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 rotationRateUnbiased_Internal(int idx);

		// Token: 0x06000F70 RID: 3952
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 gravity_Internal(int idx);

		// Token: 0x06000F71 RID: 3953
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 userAcceleration_Internal(int idx);

		// Token: 0x06000F72 RID: 3954
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Quaternion attitude_Internal(int idx);

		// Token: 0x06000F73 RID: 3955
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool getEnabled_Internal(int idx);

		// Token: 0x06000F74 RID: 3956
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void setEnabled_Internal(int idx, bool enabled);

		// Token: 0x06000F75 RID: 3957
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float getUpdateInterval_Internal(int idx);

		// Token: 0x06000F76 RID: 3958
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void setUpdateInterval_Internal(int idx, float interval);

		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0001F094 File Offset: 0x0001D294
		public Vector3 rotationRate
		{
			get
			{
				return Gyroscope.rotationRate_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000F78 RID: 3960 RVA: 0x0001F0A4 File Offset: 0x0001D2A4
		public Vector3 rotationRateUnbiased
		{
			get
			{
				return Gyroscope.rotationRateUnbiased_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000F79 RID: 3961 RVA: 0x0001F0B4 File Offset: 0x0001D2B4
		public Vector3 gravity
		{
			get
			{
				return Gyroscope.gravity_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000F7A RID: 3962 RVA: 0x0001F0C4 File Offset: 0x0001D2C4
		public Vector3 userAcceleration
		{
			get
			{
				return Gyroscope.userAcceleration_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000F7B RID: 3963 RVA: 0x0001F0D4 File Offset: 0x0001D2D4
		public Quaternion attitude
		{
			get
			{
				return Gyroscope.attitude_Internal(this.m_GyroIndex);
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000F7C RID: 3964 RVA: 0x0001F0E4 File Offset: 0x0001D2E4
		// (set) Token: 0x06000F7D RID: 3965 RVA: 0x0001F0F4 File Offset: 0x0001D2F4
		public bool enabled
		{
			get
			{
				return Gyroscope.getEnabled_Internal(this.m_GyroIndex);
			}
			set
			{
				Gyroscope.setEnabled_Internal(this.m_GyroIndex, value);
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000F7E RID: 3966 RVA: 0x0001F104 File Offset: 0x0001D304
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x0001F114 File Offset: 0x0001D314
		public float updateInterval
		{
			get
			{
				return Gyroscope.getUpdateInterval_Internal(this.m_GyroIndex);
			}
			set
			{
				Gyroscope.setUpdateInterval_Internal(this.m_GyroIndex, value);
			}
		}

		// Token: 0x04000607 RID: 1543
		private int m_GyroIndex;
	}
}
