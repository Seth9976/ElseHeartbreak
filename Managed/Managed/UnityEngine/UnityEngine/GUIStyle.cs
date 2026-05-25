using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000106 RID: 262
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIStyle
	{
		// Token: 0x06000927 RID: 2343 RVA: 0x00015298 File Offset: 0x00013498
		public GUIStyle()
		{
			this.Init();
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x000152A8 File Offset: 0x000134A8
		public GUIStyle(GUIStyle other)
		{
			this.InitCopy(other);
		}

		// Token: 0x0600092A RID: 2346 RVA: 0x000152C0 File Offset: 0x000134C0
		~GUIStyle()
		{
			this.Cleanup();
		}

		// Token: 0x0600092B RID: 2347 RVA: 0x000152FC File Offset: 0x000134FC
		internal void CreateObjectReferences()
		{
			this.m_FontInternal = this.GetFontInternal();
			this.normal.RefreshAssetReference();
			this.hover.RefreshAssetReference();
			this.active.RefreshAssetReference();
			this.focused.RefreshAssetReference();
			this.onNormal.RefreshAssetReference();
			this.onHover.RefreshAssetReference();
			this.onActive.RefreshAssetReference();
			this.onFocused.RefreshAssetReference();
		}

		// Token: 0x0600092C RID: 2348
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Init();

		// Token: 0x0600092D RID: 2349
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void InitCopy(GUIStyle other);

		// Token: 0x0600092E RID: 2350
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Cleanup();

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600092F RID: 2351
		// (set) Token: 0x06000930 RID: 2352
		public extern string name
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000931 RID: 2353 RVA: 0x00015370 File Offset: 0x00013570
		// (set) Token: 0x06000932 RID: 2354 RVA: 0x000153A4 File Offset: 0x000135A4
		public GUIStyleState normal
		{
			get
			{
				if (this.m_Normal == null)
				{
					this.m_Normal = new GUIStyleState(this, this.GetStyleStatePtr(0));
				}
				return this.m_Normal;
			}
			set
			{
				this.AssignStyleState(0, value.m_Ptr);
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000933 RID: 2355 RVA: 0x000153B4 File Offset: 0x000135B4
		// (set) Token: 0x06000934 RID: 2356 RVA: 0x000153E8 File Offset: 0x000135E8
		public GUIStyleState hover
		{
			get
			{
				if (this.m_Hover == null)
				{
					this.m_Hover = new GUIStyleState(this, this.GetStyleStatePtr(1));
				}
				return this.m_Hover;
			}
			set
			{
				this.AssignStyleState(1, value.m_Ptr);
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000935 RID: 2357 RVA: 0x000153F8 File Offset: 0x000135F8
		// (set) Token: 0x06000936 RID: 2358 RVA: 0x0001542C File Offset: 0x0001362C
		public GUIStyleState active
		{
			get
			{
				if (this.m_Active == null)
				{
					this.m_Active = new GUIStyleState(this, this.GetStyleStatePtr(2));
				}
				return this.m_Active;
			}
			set
			{
				this.AssignStyleState(2, value.m_Ptr);
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000937 RID: 2359 RVA: 0x0001543C File Offset: 0x0001363C
		// (set) Token: 0x06000938 RID: 2360 RVA: 0x00015470 File Offset: 0x00013670
		public GUIStyleState onNormal
		{
			get
			{
				if (this.m_OnNormal == null)
				{
					this.m_OnNormal = new GUIStyleState(this, this.GetStyleStatePtr(4));
				}
				return this.m_OnNormal;
			}
			set
			{
				this.AssignStyleState(4, value.m_Ptr);
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000939 RID: 2361 RVA: 0x00015480 File Offset: 0x00013680
		// (set) Token: 0x0600093A RID: 2362 RVA: 0x000154B4 File Offset: 0x000136B4
		public GUIStyleState onHover
		{
			get
			{
				if (this.m_OnHover == null)
				{
					this.m_OnHover = new GUIStyleState(this, this.GetStyleStatePtr(5));
				}
				return this.m_OnHover;
			}
			set
			{
				this.AssignStyleState(5, value.m_Ptr);
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x0600093B RID: 2363 RVA: 0x000154C4 File Offset: 0x000136C4
		// (set) Token: 0x0600093C RID: 2364 RVA: 0x000154F8 File Offset: 0x000136F8
		public GUIStyleState onActive
		{
			get
			{
				if (this.m_OnActive == null)
				{
					this.m_OnActive = new GUIStyleState(this, this.GetStyleStatePtr(6));
				}
				return this.m_OnActive;
			}
			set
			{
				this.AssignStyleState(6, value.m_Ptr);
			}
		}

		// Token: 0x1700020D RID: 525
		// (get) Token: 0x0600093D RID: 2365 RVA: 0x00015508 File Offset: 0x00013708
		// (set) Token: 0x0600093E RID: 2366 RVA: 0x0001553C File Offset: 0x0001373C
		public GUIStyleState focused
		{
			get
			{
				if (this.m_Focused == null)
				{
					this.m_Focused = new GUIStyleState(this, this.GetStyleStatePtr(3));
				}
				return this.m_Focused;
			}
			set
			{
				this.AssignStyleState(3, value.m_Ptr);
			}
		}

		// Token: 0x1700020E RID: 526
		// (get) Token: 0x0600093F RID: 2367 RVA: 0x0001554C File Offset: 0x0001374C
		// (set) Token: 0x06000940 RID: 2368 RVA: 0x00015580 File Offset: 0x00013780
		public GUIStyleState onFocused
		{
			get
			{
				if (this.m_OnFocused == null)
				{
					this.m_OnFocused = new GUIStyleState(this, this.GetStyleStatePtr(7));
				}
				return this.m_OnFocused;
			}
			set
			{
				this.AssignStyleState(7, value.m_Ptr);
			}
		}

		// Token: 0x06000941 RID: 2369
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetStyleStatePtr(int idx);

		// Token: 0x06000942 RID: 2370
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AssignStyleState(int idx, IntPtr srcStyleState);

		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000943 RID: 2371 RVA: 0x00015590 File Offset: 0x00013790
		// (set) Token: 0x06000944 RID: 2372 RVA: 0x000155C4 File Offset: 0x000137C4
		public RectOffset border
		{
			get
			{
				if (this.m_Border == null)
				{
					this.m_Border = new RectOffset(this, this.GetRectOffsetPtr(0));
				}
				return this.m_Border;
			}
			set
			{
				this.AssignRectOffset(0, value.m_Ptr);
			}
		}

		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x000155D4 File Offset: 0x000137D4
		// (set) Token: 0x06000946 RID: 2374 RVA: 0x00015608 File Offset: 0x00013808
		public RectOffset margin
		{
			get
			{
				if (this.m_Margin == null)
				{
					this.m_Margin = new RectOffset(this, this.GetRectOffsetPtr(1));
				}
				return this.m_Margin;
			}
			set
			{
				this.AssignRectOffset(1, value.m_Ptr);
			}
		}

		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000947 RID: 2375 RVA: 0x00015618 File Offset: 0x00013818
		// (set) Token: 0x06000948 RID: 2376 RVA: 0x0001564C File Offset: 0x0001384C
		public RectOffset padding
		{
			get
			{
				if (this.m_Padding == null)
				{
					this.m_Padding = new RectOffset(this, this.GetRectOffsetPtr(2));
				}
				return this.m_Padding;
			}
			set
			{
				this.AssignRectOffset(2, value.m_Ptr);
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000949 RID: 2377 RVA: 0x0001565C File Offset: 0x0001385C
		// (set) Token: 0x0600094A RID: 2378 RVA: 0x00015690 File Offset: 0x00013890
		public RectOffset overflow
		{
			get
			{
				if (this.m_Overflow == null)
				{
					this.m_Overflow = new RectOffset(this, this.GetRectOffsetPtr(3));
				}
				return this.m_Overflow;
			}
			set
			{
				this.AssignRectOffset(3, value.m_Ptr);
			}
		}

		// Token: 0x0600094B RID: 2379
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern IntPtr GetRectOffsetPtr(int idx);

		// Token: 0x0600094C RID: 2380
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void AssignRectOffset(int idx, IntPtr srcRectOffset);

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x0600094D RID: 2381
		// (set) Token: 0x0600094E RID: 2382
		public extern ImagePosition imagePosition
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x0600094F RID: 2383
		// (set) Token: 0x06000950 RID: 2384
		public extern TextAnchor alignment
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000951 RID: 2385
		// (set) Token: 0x06000952 RID: 2386
		public extern bool wordWrap
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000953 RID: 2387
		// (set) Token: 0x06000954 RID: 2388
		public extern TextClipping clipping
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000955 RID: 2389 RVA: 0x000156A0 File Offset: 0x000138A0
		// (set) Token: 0x06000956 RID: 2390 RVA: 0x000156B8 File Offset: 0x000138B8
		public Vector2 contentOffset
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_contentOffset(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_contentOffset(ref value);
			}
		}

		// Token: 0x06000957 RID: 2391
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_contentOffset(out Vector2 value);

		// Token: 0x06000958 RID: 2392
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_contentOffset(ref Vector2 value);

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x000156C4 File Offset: 0x000138C4
		// (set) Token: 0x0600095A RID: 2394 RVA: 0x000156CC File Offset: 0x000138CC
		[Obsolete("Don't use clipOffset - put things inside begingroup instead. This functionality will be removed in a later version.")]
		public Vector2 clipOffset
		{
			get
			{
				return this.Internal_clipOffset;
			}
			set
			{
				this.Internal_clipOffset = value;
			}
		}

		// Token: 0x17000219 RID: 537
		// (get) Token: 0x0600095B RID: 2395 RVA: 0x000156D8 File Offset: 0x000138D8
		// (set) Token: 0x0600095C RID: 2396 RVA: 0x000156F0 File Offset: 0x000138F0
		internal Vector2 Internal_clipOffset
		{
			get
			{
				Vector2 vector;
				this.INTERNAL_get_Internal_clipOffset(out vector);
				return vector;
			}
			set
			{
				this.INTERNAL_set_Internal_clipOffset(ref value);
			}
		}

		// Token: 0x0600095D RID: 2397
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_Internal_clipOffset(out Vector2 value);

		// Token: 0x0600095E RID: 2398
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_Internal_clipOffset(ref Vector2 value);

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x0600095F RID: 2399
		// (set) Token: 0x06000960 RID: 2400
		public extern float fixedWidth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x06000961 RID: 2401
		// (set) Token: 0x06000962 RID: 2402
		public extern float fixedHeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x06000963 RID: 2403
		// (set) Token: 0x06000964 RID: 2404
		public extern bool stretchWidth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700021D RID: 541
		// (get) Token: 0x06000965 RID: 2405
		// (set) Token: 0x06000966 RID: 2406
		public extern bool stretchHeight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000967 RID: 2407
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetLineHeight(IntPtr target);

		// Token: 0x06000968 RID: 2408
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetFontInternal(Font value);

		// Token: 0x06000969 RID: 2409
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Font GetFontInternal();

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600096A RID: 2410 RVA: 0x000156FC File Offset: 0x000138FC
		// (set) Token: 0x0600096B RID: 2411 RVA: 0x00015704 File Offset: 0x00013904
		public Font font
		{
			get
			{
				return this.GetFontInternal();
			}
			set
			{
				this.SetFontInternal(value);
				this.m_FontInternal = value;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600096C RID: 2412
		// (set) Token: 0x0600096D RID: 2413
		public extern int fontSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x0600096E RID: 2414
		// (set) Token: 0x0600096F RID: 2415
		public extern FontStyle fontStyle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000970 RID: 2416
		// (set) Token: 0x06000971 RID: 2417
		public extern bool richText
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000972 RID: 2418 RVA: 0x00015714 File Offset: 0x00013914
		public float lineHeight
		{
			get
			{
				return Mathf.Round(GUIStyle.Internal_GetLineHeight(this.m_Ptr));
			}
		}

		// Token: 0x06000973 RID: 2419 RVA: 0x00015728 File Offset: 0x00013928
		private static void Internal_Draw(IntPtr target, Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			Internal_DrawArguments internal_DrawArguments = default(Internal_DrawArguments);
			internal_DrawArguments.target = target;
			internal_DrawArguments.position = position;
			internal_DrawArguments.isHover = ((!isHover) ? 0 : 1);
			internal_DrawArguments.isActive = ((!isActive) ? 0 : 1);
			internal_DrawArguments.on = ((!on) ? 0 : 1);
			internal_DrawArguments.hasKeyboardFocus = ((!hasKeyboardFocus) ? 0 : 1);
			GUIStyle.Internal_Draw(content, ref internal_DrawArguments);
		}

		// Token: 0x06000974 RID: 2420
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Draw(GUIContent content, ref Internal_DrawArguments arguments);

		// Token: 0x06000975 RID: 2421 RVA: 0x000157A8 File Offset: 0x000139A8
		public void Draw(Rect position, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, GUIContent.none, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000976 RID: 2422 RVA: 0x000157C4 File Offset: 0x000139C4
		public void Draw(Rect position, string text, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, GUIContent.Temp(text), isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000977 RID: 2423 RVA: 0x000157EC File Offset: 0x000139EC
		public void Draw(Rect position, Texture image, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, GUIContent.Temp(image), isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000978 RID: 2424 RVA: 0x00015814 File Offset: 0x00013A14
		public void Draw(Rect position, GUIContent content, bool isHover, bool isActive, bool on, bool hasKeyboardFocus)
		{
			GUIStyle.Internal_Draw(this.m_Ptr, position, content, isHover, isActive, on, hasKeyboardFocus);
		}

		// Token: 0x06000979 RID: 2425 RVA: 0x0001582C File Offset: 0x00013A2C
		[ExcludeFromDocs]
		public void Draw(Rect position, GUIContent content, int controlID)
		{
			bool flag = false;
			this.Draw(position, content, controlID, flag);
		}

		// Token: 0x0600097A RID: 2426 RVA: 0x00015848 File Offset: 0x00013A48
		public void Draw(Rect position, GUIContent content, int controlID, [DefaultValue("false")] bool on)
		{
			if (content != null)
			{
				GUIStyle.Internal_Draw2(this.m_Ptr, position, content, controlID, on);
			}
			else
			{
				Debug.LogError("Style.Draw may not be called with GUIContent that is null.");
			}
		}

		// Token: 0x0600097B RID: 2427 RVA: 0x00015870 File Offset: 0x00013A70
		private static void Internal_Draw2(IntPtr style, Rect position, GUIContent content, int controlID, bool on)
		{
			GUIStyle.INTERNAL_CALL_Internal_Draw2(style, ref position, content, controlID, on);
		}

		// Token: 0x0600097C RID: 2428
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_Draw2(IntPtr style, ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x0600097D RID: 2429 RVA: 0x00015880 File Offset: 0x00013A80
		internal void DrawPrefixLabel(Rect position, GUIContent content, int controlID)
		{
			if (content != null)
			{
				GUIStyle.Internal_DrawPrefixLabel(this.m_Ptr, position, content, controlID, false);
			}
			else
			{
				Debug.LogError("Style.DrawPrefixLabel may not be called with GUIContent that is null.");
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x000158B4 File Offset: 0x00013AB4
		private static void Internal_DrawPrefixLabel(IntPtr style, Rect position, GUIContent content, int controlID, bool on)
		{
			GUIStyle.INTERNAL_CALL_Internal_DrawPrefixLabel(style, ref position, content, controlID, on);
		}

		// Token: 0x0600097F RID: 2431
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_DrawPrefixLabel(IntPtr style, ref Rect position, GUIContent content, int controlID, bool on);

		// Token: 0x06000980 RID: 2432
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetCursorFlashOffset();

		// Token: 0x06000981 RID: 2433 RVA: 0x000158C4 File Offset: 0x00013AC4
		private static void Internal_DrawCursor(IntPtr target, Rect position, GUIContent content, int pos, Color cursorColor)
		{
			GUIStyle.INTERNAL_CALL_Internal_DrawCursor(target, ref position, content, pos, ref cursorColor);
		}

		// Token: 0x06000982 RID: 2434
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_DrawCursor(IntPtr target, ref Rect position, GUIContent content, int pos, ref Color cursorColor);

		// Token: 0x06000983 RID: 2435 RVA: 0x000158D4 File Offset: 0x00013AD4
		public void DrawCursor(Rect position, GUIContent content, int controlID, int Character)
		{
			Event current = Event.current;
			if (current.type == EventType.Repaint)
			{
				Color cursorColor = new Color(0f, 0f, 0f, 0f);
				float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
				float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
				if (cursorFlashSpeed == 0f || num < 0.5f)
				{
					cursorColor = GUI.skin.settings.cursorColor;
				}
				GUIStyle.Internal_DrawCursor(this.m_Ptr, position, content, Character, cursorColor);
			}
		}

		// Token: 0x06000984 RID: 2436
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_DrawWithTextSelection(GUIContent content, ref Internal_DrawWithTextSelectionArguments arguments);

		// Token: 0x06000985 RID: 2437 RVA: 0x00015964 File Offset: 0x00013B64
		internal void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter, bool drawSelectionAsComposition)
		{
			Event current = Event.current;
			Color cursorColor = new Color(0f, 0f, 0f, 0f);
			float cursorFlashSpeed = GUI.skin.settings.cursorFlashSpeed;
			float num = (Time.realtimeSinceStartup - GUIStyle.Internal_GetCursorFlashOffset()) % cursorFlashSpeed / cursorFlashSpeed;
			if (cursorFlashSpeed == 0f || num < 0.5f)
			{
				cursorColor = GUI.skin.settings.cursorColor;
			}
			Internal_DrawWithTextSelectionArguments internal_DrawWithTextSelectionArguments = default(Internal_DrawWithTextSelectionArguments);
			internal_DrawWithTextSelectionArguments.target = this.m_Ptr;
			internal_DrawWithTextSelectionArguments.position = position;
			internal_DrawWithTextSelectionArguments.firstPos = firstSelectedCharacter;
			internal_DrawWithTextSelectionArguments.lastPos = lastSelectedCharacter;
			internal_DrawWithTextSelectionArguments.cursorColor = cursorColor;
			internal_DrawWithTextSelectionArguments.selectionColor = GUI.skin.settings.selectionColor;
			internal_DrawWithTextSelectionArguments.isHover = ((!position.Contains(current.mousePosition)) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.isActive = ((controlID != GUIUtility.hotControl) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.on = 0;
			internal_DrawWithTextSelectionArguments.hasKeyboardFocus = ((controlID != GUIUtility.keyboardControl || !GUIStyle.showKeyboardFocus) ? 0 : 1);
			internal_DrawWithTextSelectionArguments.drawSelectionAsComposition = ((!drawSelectionAsComposition) ? 0 : 1);
			GUIStyle.Internal_DrawWithTextSelection(content, ref internal_DrawWithTextSelectionArguments);
		}

		// Token: 0x06000986 RID: 2438 RVA: 0x00015AA8 File Offset: 0x00013CA8
		public void DrawWithTextSelection(Rect position, GUIContent content, int controlID, int firstSelectedCharacter, int lastSelectedCharacter)
		{
			this.DrawWithTextSelection(position, content, controlID, firstSelectedCharacter, lastSelectedCharacter, false);
		}

		// Token: 0x06000987 RID: 2439
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDefaultFont(Font font);

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000988 RID: 2440 RVA: 0x00015AB8 File Offset: 0x00013CB8
		public static GUIStyle none
		{
			get
			{
				if (GUIStyle.s_None == null)
				{
					GUIStyle.s_None = new GUIStyle();
				}
				return GUIStyle.s_None;
			}
		}

		// Token: 0x06000989 RID: 2441 RVA: 0x00015AD4 File Offset: 0x00013CD4
		public Vector2 GetCursorPixelPosition(Rect position, GUIContent content, int cursorStringIndex)
		{
			Vector2 vector;
			GUIStyle.Internal_GetCursorPixelPosition(this.m_Ptr, position, content, cursorStringIndex, out vector);
			return vector;
		}

		// Token: 0x0600098A RID: 2442 RVA: 0x00015AF4 File Offset: 0x00013CF4
		internal static void Internal_GetCursorPixelPosition(IntPtr target, Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret)
		{
			GUIStyle.INTERNAL_CALL_Internal_GetCursorPixelPosition(target, ref position, content, cursorStringIndex, out ret);
		}

		// Token: 0x0600098B RID: 2443
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_GetCursorPixelPosition(IntPtr target, ref Rect position, GUIContent content, int cursorStringIndex, out Vector2 ret);

		// Token: 0x0600098C RID: 2444 RVA: 0x00015B04 File Offset: 0x00013D04
		public int GetCursorStringIndex(Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return GUIStyle.Internal_GetCursorStringIndex(this.m_Ptr, position, content, cursorPixelPosition);
		}

		// Token: 0x0600098D RID: 2445 RVA: 0x00015B14 File Offset: 0x00013D14
		internal static int Internal_GetCursorStringIndex(IntPtr target, Rect position, GUIContent content, Vector2 cursorPixelPosition)
		{
			return GUIStyle.INTERNAL_CALL_Internal_GetCursorStringIndex(target, ref position, content, ref cursorPixelPosition);
		}

		// Token: 0x0600098E RID: 2446
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_Internal_GetCursorStringIndex(IntPtr target, ref Rect position, GUIContent content, ref Vector2 cursorPixelPosition);

		// Token: 0x0600098F RID: 2447 RVA: 0x00015B24 File Offset: 0x00013D24
		internal int GetNumCharactersThatFitWithinWidth(string text, float width)
		{
			return GUIStyle.Internal_GetNumCharactersThatFitWithinWidth(this.m_Ptr, text, width);
		}

		// Token: 0x06000990 RID: 2448
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_GetNumCharactersThatFitWithinWidth(IntPtr target, string text, float width);

		// Token: 0x06000991 RID: 2449 RVA: 0x00015B34 File Offset: 0x00013D34
		public Vector2 CalcSize(GUIContent content)
		{
			Vector2 vector;
			GUIStyle.Internal_CalcSize(this.m_Ptr, content, out vector);
			return vector;
		}

		// Token: 0x06000992 RID: 2450
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void Internal_CalcSize(IntPtr target, GUIContent content, out Vector2 ret);

		// Token: 0x06000993 RID: 2451 RVA: 0x00015B50 File Offset: 0x00013D50
		public Vector2 CalcScreenSize(Vector2 contentSize)
		{
			return new Vector2((this.fixedWidth == 0f) ? Mathf.Ceil(contentSize.x + (float)this.padding.left + (float)this.padding.right) : this.fixedWidth, (this.fixedHeight == 0f) ? Mathf.Ceil(contentSize.y + (float)this.padding.top + (float)this.padding.bottom) : this.fixedHeight);
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x00015BE4 File Offset: 0x00013DE4
		public float CalcHeight(GUIContent content, float width)
		{
			return GUIStyle.Internal_CalcHeight(this.m_Ptr, content, width);
		}

		// Token: 0x06000995 RID: 2453
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_CalcHeight(IntPtr target, GUIContent content, float width);

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00015BF4 File Offset: 0x00013DF4
		public bool isHeightDependantOnWidth
		{
			get
			{
				return this.fixedHeight == 0f && this.wordWrap && this.imagePosition != ImagePosition.ImageOnly;
			}
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x00015C30 File Offset: 0x00013E30
		public void CalcMinMaxWidth(GUIContent content, out float minWidth, out float maxWidth)
		{
			GUIStyle.Internal_CalcMinMaxWidth(this.m_Ptr, content, out minWidth, out maxWidth);
		}

		// Token: 0x06000998 RID: 2456
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CalcMinMaxWidth(IntPtr target, GUIContent content, out float minWidth, out float maxWidth);

		// Token: 0x06000999 RID: 2457 RVA: 0x00015C40 File Offset: 0x00013E40
		public override string ToString()
		{
			return UnityString.Format("GUIStyle '{0}'", new object[] { this.name });
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x00015C5C File Offset: 0x00013E5C
		public static implicit operator GUIStyle(string str)
		{
			if (GUISkin.current == null)
			{
				Debug.LogError("Unable to use a named GUIStyle without a current skin. Most likely you need to move your GUIStyle initialization code to OnGUI");
				return GUISkin.error;
			}
			return GUISkin.current.GetStyle(str);
		}

		// Token: 0x040003A2 RID: 930
		[NotRenamed]
		[NonSerialized]
		internal IntPtr m_Ptr;

		// Token: 0x040003A3 RID: 931
		[NonSerialized]
		private GUIStyleState m_Normal;

		// Token: 0x040003A4 RID: 932
		[NonSerialized]
		private GUIStyleState m_Hover;

		// Token: 0x040003A5 RID: 933
		[NonSerialized]
		private GUIStyleState m_Active;

		// Token: 0x040003A6 RID: 934
		[NonSerialized]
		private GUIStyleState m_Focused;

		// Token: 0x040003A7 RID: 935
		[NonSerialized]
		private GUIStyleState m_OnNormal;

		// Token: 0x040003A8 RID: 936
		[NonSerialized]
		private GUIStyleState m_OnHover;

		// Token: 0x040003A9 RID: 937
		[NonSerialized]
		private GUIStyleState m_OnActive;

		// Token: 0x040003AA RID: 938
		[NonSerialized]
		private GUIStyleState m_OnFocused;

		// Token: 0x040003AB RID: 939
		[NonSerialized]
		private RectOffset m_Border;

		// Token: 0x040003AC RID: 940
		[NonSerialized]
		private RectOffset m_Padding;

		// Token: 0x040003AD RID: 941
		[NonSerialized]
		private RectOffset m_Margin;

		// Token: 0x040003AE RID: 942
		[NonSerialized]
		private RectOffset m_Overflow;

		// Token: 0x040003AF RID: 943
		[NonSerialized]
		private Font m_FontInternal;

		// Token: 0x040003B0 RID: 944
		internal static bool showKeyboardFocus = true;

		// Token: 0x040003B1 RID: 945
		private static GUIStyle s_None;
	}
}
