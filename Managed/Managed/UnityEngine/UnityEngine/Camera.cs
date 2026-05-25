using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000159 RID: 345
	public sealed class Camera : Behaviour
	{
		// Token: 0x17000350 RID: 848
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
		// (set) Token: 0x06000E76 RID: 3702 RVA: 0x0001E8D8 File Offset: 0x0001CAD8
		[Obsolete("use Camera.fieldOfView instead.")]
		public float fov
		{
			get
			{
				return this.fieldOfView;
			}
			set
			{
				this.fieldOfView = value;
			}
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000E77 RID: 3703 RVA: 0x0001E8E4 File Offset: 0x0001CAE4
		// (set) Token: 0x06000E78 RID: 3704 RVA: 0x0001E8EC File Offset: 0x0001CAEC
		[Obsolete("use Camera.nearClipPlane instead.")]
		public float near
		{
			get
			{
				return this.nearClipPlane;
			}
			set
			{
				this.nearClipPlane = value;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000E79 RID: 3705 RVA: 0x0001E8F8 File Offset: 0x0001CAF8
		// (set) Token: 0x06000E7A RID: 3706 RVA: 0x0001E900 File Offset: 0x0001CB00
		[Obsolete("use Camera.farClipPlane instead.")]
		public float far
		{
			get
			{
				return this.farClipPlane;
			}
			set
			{
				this.farClipPlane = value;
			}
		}

		// Token: 0x17000353 RID: 851
		// (get) Token: 0x06000E7B RID: 3707
		// (set) Token: 0x06000E7C RID: 3708
		public extern float fieldOfView
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000354 RID: 852
		// (get) Token: 0x06000E7D RID: 3709
		// (set) Token: 0x06000E7E RID: 3710
		public extern float nearClipPlane
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000355 RID: 853
		// (get) Token: 0x06000E7F RID: 3711
		// (set) Token: 0x06000E80 RID: 3712
		public extern float farClipPlane
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000E81 RID: 3713
		// (set) Token: 0x06000E82 RID: 3714
		public extern RenderingPath renderingPath
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06000E83 RID: 3715
		public extern RenderingPath actualRenderingPath
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06000E84 RID: 3716
		// (set) Token: 0x06000E85 RID: 3717
		public extern bool hdr
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000359 RID: 857
		// (get) Token: 0x06000E86 RID: 3718
		// (set) Token: 0x06000E87 RID: 3719
		public extern float orthographicSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000E88 RID: 3720
		// (set) Token: 0x06000E89 RID: 3721
		public extern bool orthographic
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000E8A RID: 3722
		// (set) Token: 0x06000E8B RID: 3723
		public extern TransparencySortMode transparencySortMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000E8C RID: 3724 RVA: 0x0001E90C File Offset: 0x0001CB0C
		// (set) Token: 0x06000E8D RID: 3725 RVA: 0x0001E914 File Offset: 0x0001CB14
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

		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000E8E RID: 3726
		// (set) Token: 0x06000E8F RID: 3727
		public extern float depth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700035E RID: 862
		// (get) Token: 0x06000E90 RID: 3728
		// (set) Token: 0x06000E91 RID: 3729
		public extern float aspect
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000E92 RID: 3730
		// (set) Token: 0x06000E93 RID: 3731
		public extern int cullingMask
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000E94 RID: 3732
		// (set) Token: 0x06000E95 RID: 3733
		public extern int eventMask
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000E96 RID: 3734 RVA: 0x0001E920 File Offset: 0x0001CB20
		// (set) Token: 0x06000E97 RID: 3735 RVA: 0x0001E938 File Offset: 0x0001CB38
		public Color backgroundColor
		{
			get
			{
				Color color;
				this.INTERNAL_get_backgroundColor(out color);
				return color;
			}
			set
			{
				this.INTERNAL_set_backgroundColor(ref value);
			}
		}

		// Token: 0x06000E98 RID: 3736
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_backgroundColor(out Color value);

		// Token: 0x06000E99 RID: 3737
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_backgroundColor(ref Color value);

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000E9A RID: 3738 RVA: 0x0001E944 File Offset: 0x0001CB44
		// (set) Token: 0x06000E9B RID: 3739 RVA: 0x0001E95C File Offset: 0x0001CB5C
		public Rect rect
		{
			get
			{
				Rect rect;
				this.INTERNAL_get_rect(out rect);
				return rect;
			}
			set
			{
				this.INTERNAL_set_rect(ref value);
			}
		}

		// Token: 0x06000E9C RID: 3740
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rect(out Rect value);

		// Token: 0x06000E9D RID: 3741
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_rect(ref Rect value);

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000E9E RID: 3742 RVA: 0x0001E968 File Offset: 0x0001CB68
		// (set) Token: 0x06000E9F RID: 3743 RVA: 0x0001E980 File Offset: 0x0001CB80
		public Rect pixelRect
		{
			get
			{
				Rect rect;
				this.INTERNAL_get_pixelRect(out rect);
				return rect;
			}
			set
			{
				this.INTERNAL_set_pixelRect(ref value);
			}
		}

		// Token: 0x06000EA0 RID: 3744
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_pixelRect(out Rect value);

		// Token: 0x06000EA1 RID: 3745
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_pixelRect(ref Rect value);

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000EA2 RID: 3746
		// (set) Token: 0x06000EA3 RID: 3747
		public extern RenderTexture targetTexture
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000EA4 RID: 3748
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTargetBuffersImpl(out RenderBuffer color, out RenderBuffer depth);

		// Token: 0x06000EA5 RID: 3749
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetTargetBuffersMRTImpl(RenderBuffer[] color, out RenderBuffer depth);

		// Token: 0x06000EA6 RID: 3750 RVA: 0x0001E98C File Offset: 0x0001CB8C
		public void SetTargetBuffers(RenderBuffer colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersImpl(out colorBuffer, out depthBuffer);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x0001E998 File Offset: 0x0001CB98
		public void SetTargetBuffers(RenderBuffer[] colorBuffer, RenderBuffer depthBuffer)
		{
			this.SetTargetBuffersMRTImpl(colorBuffer, out depthBuffer);
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000EA8 RID: 3752
		public extern float pixelWidth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000EA9 RID: 3753
		public extern float pixelHeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000EAA RID: 3754 RVA: 0x0001E9A4 File Offset: 0x0001CBA4
		public Matrix4x4 cameraToWorldMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_cameraToWorldMatrix(out matrix4x);
				return matrix4x;
			}
		}

		// Token: 0x06000EAB RID: 3755
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_cameraToWorldMatrix(out Matrix4x4 value);

		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x0001E9BC File Offset: 0x0001CBBC
		// (set) Token: 0x06000EAD RID: 3757 RVA: 0x0001E9D4 File Offset: 0x0001CBD4
		public Matrix4x4 worldToCameraMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_worldToCameraMatrix(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.INTERNAL_set_worldToCameraMatrix(ref value);
			}
		}

		// Token: 0x06000EAE RID: 3758
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_worldToCameraMatrix(out Matrix4x4 value);

		// Token: 0x06000EAF RID: 3759
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_worldToCameraMatrix(ref Matrix4x4 value);

		// Token: 0x06000EB0 RID: 3760 RVA: 0x0001E9E0 File Offset: 0x0001CBE0
		public void ResetWorldToCameraMatrix()
		{
			Camera.INTERNAL_CALL_ResetWorldToCameraMatrix(this);
		}

		// Token: 0x06000EB1 RID: 3761
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetWorldToCameraMatrix(Camera self);

		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000EB2 RID: 3762 RVA: 0x0001E9E8 File Offset: 0x0001CBE8
		// (set) Token: 0x06000EB3 RID: 3763 RVA: 0x0001EA00 File Offset: 0x0001CC00
		public Matrix4x4 projectionMatrix
		{
			get
			{
				Matrix4x4 matrix4x;
				this.INTERNAL_get_projectionMatrix(out matrix4x);
				return matrix4x;
			}
			set
			{
				this.INTERNAL_set_projectionMatrix(ref value);
			}
		}

		// Token: 0x06000EB4 RID: 3764
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_projectionMatrix(out Matrix4x4 value);

		// Token: 0x06000EB5 RID: 3765
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_projectionMatrix(ref Matrix4x4 value);

		// Token: 0x06000EB6 RID: 3766 RVA: 0x0001EA0C File Offset: 0x0001CC0C
		public void ResetProjectionMatrix()
		{
			Camera.INTERNAL_CALL_ResetProjectionMatrix(this);
		}

		// Token: 0x06000EB7 RID: 3767
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetProjectionMatrix(Camera self);

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0001EA14 File Offset: 0x0001CC14
		public void ResetAspect()
		{
			Camera.INTERNAL_CALL_ResetAspect(this);
		}

		// Token: 0x06000EB9 RID: 3769
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetAspect(Camera self);

		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0001EA1C File Offset: 0x0001CC1C
		public Vector3 velocity
		{
			get
			{
				Vector3 vector;
				this.INTERNAL_get_velocity(out vector);
				return vector;
			}
		}

		// Token: 0x06000EBB RID: 3771
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_velocity(out Vector3 value);

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000EBC RID: 3772
		// (set) Token: 0x06000EBD RID: 3773
		public extern CameraClearFlags clearFlags
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000EBE RID: 3774
		public extern bool stereoEnabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000EBF RID: 3775
		// (set) Token: 0x06000EC0 RID: 3776
		public extern float stereoSeparation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000EC1 RID: 3777
		// (set) Token: 0x06000EC2 RID: 3778
		public extern float stereoConvergence
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0001EA34 File Offset: 0x0001CC34
		public Vector3 WorldToScreenPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_WorldToScreenPoint(this, ref position);
		}

		// Token: 0x06000EC4 RID: 3780
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_WorldToScreenPoint(Camera self, ref Vector3 position);

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0001EA40 File Offset: 0x0001CC40
		public Vector3 WorldToViewportPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_WorldToViewportPoint(this, ref position);
		}

		// Token: 0x06000EC6 RID: 3782
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_WorldToViewportPoint(Camera self, ref Vector3 position);

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0001EA4C File Offset: 0x0001CC4C
		public Vector3 ViewportToWorldPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ViewportToWorldPoint(this, ref position);
		}

		// Token: 0x06000EC8 RID: 3784
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_ViewportToWorldPoint(Camera self, ref Vector3 position);

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0001EA58 File Offset: 0x0001CC58
		public Vector3 ScreenToWorldPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ScreenToWorldPoint(this, ref position);
		}

		// Token: 0x06000ECA RID: 3786
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_ScreenToWorldPoint(Camera self, ref Vector3 position);

		// Token: 0x06000ECB RID: 3787 RVA: 0x0001EA64 File Offset: 0x0001CC64
		public Vector3 ScreenToViewportPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ScreenToViewportPoint(this, ref position);
		}

		// Token: 0x06000ECC RID: 3788
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_ScreenToViewportPoint(Camera self, ref Vector3 position);

		// Token: 0x06000ECD RID: 3789 RVA: 0x0001EA70 File Offset: 0x0001CC70
		public Vector3 ViewportToScreenPoint(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ViewportToScreenPoint(this, ref position);
		}

		// Token: 0x06000ECE RID: 3790
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Vector3 INTERNAL_CALL_ViewportToScreenPoint(Camera self, ref Vector3 position);

		// Token: 0x06000ECF RID: 3791 RVA: 0x0001EA7C File Offset: 0x0001CC7C
		public Ray ViewportPointToRay(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ViewportPointToRay(this, ref position);
		}

		// Token: 0x06000ED0 RID: 3792
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Ray INTERNAL_CALL_ViewportPointToRay(Camera self, ref Vector3 position);

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0001EA88 File Offset: 0x0001CC88
		public Ray ScreenPointToRay(Vector3 position)
		{
			return Camera.INTERNAL_CALL_ScreenPointToRay(this, ref position);
		}

		// Token: 0x06000ED2 RID: 3794
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Ray INTERNAL_CALL_ScreenPointToRay(Camera self, ref Vector3 position);

		// Token: 0x1700036F RID: 879
		// (get) Token: 0x06000ED3 RID: 3795
		public static extern Camera main
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x06000ED4 RID: 3796
		public static extern Camera current
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000ED5 RID: 3797
		public static extern Camera[] allCameras
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000ED6 RID: 3798
		public static extern int allCamerasCount
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000ED7 RID: 3799
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetAllCameras(Camera[] cameras);

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000ED8 RID: 3800 RVA: 0x0001EA94 File Offset: 0x0001CC94
		[Obsolete("use Camera.main instead.")]
		public static Camera mainCamera
		{
			get
			{
				return Camera.main;
			}
		}

		// Token: 0x06000ED9 RID: 3801
		[Obsolete("use Screen.width instead.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetScreenWidth();

		// Token: 0x06000EDA RID: 3802
		[Obsolete("use Screen.height instead.")]
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern float GetScreenHeight();

		// Token: 0x06000EDB RID: 3803
		[WrapperlessIcall]
		[Obsolete("Camera.DoClear is deprecated and may be removed in the future.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void DoClear();

		// Token: 0x06000EDC RID: 3804
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Render();

		// Token: 0x06000EDD RID: 3805
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RenderWithShader(Shader shader, string replacementTag);

		// Token: 0x06000EDE RID: 3806
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetReplacementShader(Shader shader, string replacementTag);

		// Token: 0x06000EDF RID: 3807 RVA: 0x0001EA9C File Offset: 0x0001CC9C
		public void ResetReplacementShader()
		{
			Camera.INTERNAL_CALL_ResetReplacementShader(this);
		}

		// Token: 0x06000EE0 RID: 3808
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_ResetReplacementShader(Camera self);

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000EE1 RID: 3809
		// (set) Token: 0x06000EE2 RID: 3810
		public extern bool useOcclusionCulling
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000EE3 RID: 3811
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RenderDontRestore();

		// Token: 0x06000EE4 RID: 3812
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetupCurrent(Camera cur);

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0001EAA4 File Offset: 0x0001CCA4
		[ExcludeFromDocs]
		public bool RenderToCubemap(Cubemap cubemap)
		{
			int num = 63;
			return this.RenderToCubemap(cubemap, num);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0001EABC File Offset: 0x0001CCBC
		public bool RenderToCubemap(Cubemap cubemap, [DefaultValue("63")] int faceMask)
		{
			return this.Internal_RenderToCubemapTexture(cubemap, faceMask);
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0001EAC8 File Offset: 0x0001CCC8
		[ExcludeFromDocs]
		public bool RenderToCubemap(RenderTexture cubemap)
		{
			int num = 63;
			return this.RenderToCubemap(cubemap, num);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0001EAE0 File Offset: 0x0001CCE0
		public bool RenderToCubemap(RenderTexture cubemap, [DefaultValue("63")] int faceMask)
		{
			return this.Internal_RenderToCubemapRT(cubemap, faceMask);
		}

		// Token: 0x06000EE9 RID: 3817
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_RenderToCubemapRT(RenderTexture cubemap, int faceMask);

		// Token: 0x06000EEA RID: 3818
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool Internal_RenderToCubemapTexture(Cubemap cubemap, int faceMask);

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000EEB RID: 3819
		// (set) Token: 0x06000EEC RID: 3820
		public extern float[] layerCullDistances
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000EED RID: 3821
		// (set) Token: 0x06000EEE RID: 3822
		public extern bool layerCullSpherical
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000EEF RID: 3823
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void CopyFrom(Camera other);

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000EF0 RID: 3824
		// (set) Token: 0x06000EF1 RID: 3825
		public extern DepthTextureMode depthTextureMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000EF2 RID: 3826
		// (set) Token: 0x06000EF3 RID: 3827
		public extern bool clearStencilAfterLightingPass
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000EF4 RID: 3828
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern bool IsFiltered(GameObject go);

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0001EAEC File Offset: 0x0001CCEC
		public Matrix4x4 CalculateObliqueMatrix(Vector4 clipPlane)
		{
			return Camera.INTERNAL_CALL_CalculateObliqueMatrix(this, ref clipPlane);
		}

		// Token: 0x06000EF6 RID: 3830
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Matrix4x4 INTERNAL_CALL_CalculateObliqueMatrix(Camera self, ref Vector4 clipPlane);
	}
}
