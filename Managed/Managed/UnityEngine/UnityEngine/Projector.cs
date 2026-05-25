using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B2 RID: 178
	public sealed class Projector : Behaviour
	{
		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000426 RID: 1062
		// (set) Token: 0x06000427 RID: 1063
		public extern float nearClipPlane
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000428 RID: 1064
		// (set) Token: 0x06000429 RID: 1065
		public extern float farClipPlane
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600042A RID: 1066
		// (set) Token: 0x0600042B RID: 1067
		public extern float fieldOfView
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600042C RID: 1068
		// (set) Token: 0x0600042D RID: 1069
		public extern float aspectRatio
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600042E RID: 1070 RVA: 0x0000BAFC File Offset: 0x00009CFC
		// (set) Token: 0x0600042F RID: 1071 RVA: 0x0000BB04 File Offset: 0x00009D04
		public bool isOrthoGraphic
		{
			get
			{
				return this.orthographic;
			}
			set
			{
				this.orthographic = value;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000430 RID: 1072
		// (set) Token: 0x06000431 RID: 1073
		public extern bool orthographic
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000432 RID: 1074
		// (set) Token: 0x06000433 RID: 1075
		public extern float orthographicSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000434 RID: 1076 RVA: 0x0000BB10 File Offset: 0x00009D10
		// (set) Token: 0x06000435 RID: 1077 RVA: 0x0000BB18 File Offset: 0x00009D18
		public float orthoGraphicSize
		{
			get
			{
				return this.orthographicSize;
			}
			set
			{
				this.orthographicSize = value;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06000436 RID: 1078
		// (set) Token: 0x06000437 RID: 1079
		public extern int ignoreLayers
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000438 RID: 1080
		// (set) Token: 0x06000439 RID: 1081
		public extern Material material
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
