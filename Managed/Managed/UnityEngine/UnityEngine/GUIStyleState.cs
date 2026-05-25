using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x02000100 RID: 256
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyleState
	{
		// Token: 0x06000904 RID: 2308 RVA: 0x000150C0 File Offset: 0x000132C0
		public GUIStyleState()
		{
			this.Init();
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000150D0 File Offset: 0x000132D0
		internal GUIStyleState(GUIStyle sourceStyle, IntPtr source)
		{
			this.m_SourceStyle = sourceStyle;
			this.m_Ptr = source;
			this.RefreshAssetReference();
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x000150EC File Offset: 0x000132EC
		internal void RefreshAssetReference()
		{
			this.m_BackgroundInternal = this.GetBackgroundInternal();
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x000150FC File Offset: 0x000132FC
		~GUIStyleState()
		{
			if (this.m_SourceStyle == null)
			{
				this.Cleanup();
			}
		}

		// Token: 0x06000908 RID: 2312
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x06000909 RID: 2313
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x0600090A RID: 2314
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetBackgroundInternal(Texture2D value);

		// Token: 0x0600090B RID: 2315
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Texture2D GetBackgroundInternal();

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x00015144 File Offset: 0x00013344
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0001514C File Offset: 0x0001334C
		public Texture2D background
		{
			get
			{
				return this.GetBackgroundInternal();
			}
			set
			{
				this.SetBackgroundInternal(value);
				this.m_BackgroundInternal = value;
			}
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0001515C File Offset: 0x0001335C
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x00015174 File Offset: 0x00013374
		public Color textColor
		{
			get
			{
				Color color;
				this.INTERNAL_get_textColor(out color);
				return color;
			}
			set
			{
				this.INTERNAL_set_textColor(ref value);
			}
		}

		// Token: 0x06000910 RID: 2320
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_textColor(out Color value);

		// Token: 0x06000911 RID: 2321
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_textColor(ref Color value);

		// Token: 0x04000382 RID: 898
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x04000383 RID: 899
		private GUIStyle m_SourceStyle;

		// Token: 0x04000384 RID: 900
		[NonSerialized]
		private Texture2D m_BackgroundInternal;
	}
}
