using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020000EE RID: 238
	public class GUILayoutUtility
	{
		// Token: 0x0600080F RID: 2063 RVA: 0x00010DB0 File Offset: 0x0000EFB0
		internal static GUILayoutUtility.LayoutCache SelectIDList(int instanceID, bool isWindow)
		{
			Dictionary<int, GUILayoutUtility.LayoutCache> dictionary = ((!isWindow) ? GUILayoutUtility.storedLayouts : GUILayoutUtility.storedWindows);
			GUILayoutUtility.LayoutCache layoutCache;
			if (!dictionary.TryGetValue(instanceID, out layoutCache))
			{
				layoutCache = new GUILayoutUtility.LayoutCache();
				dictionary[instanceID] = layoutCache;
			}
			GUILayoutUtility.current.topLevel = layoutCache.topLevel;
			GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
			GUILayoutUtility.current.windows = layoutCache.windows;
			return layoutCache;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00010E28 File Offset: 0x0000F028
		internal static void Begin(int instanceID)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(instanceID, false);
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x00010EDC File Offset: 0x0000F0DC
		internal static void BeginWindow(int windowID, GUIStyle style, GUILayoutOption[] options)
		{
			GUILayoutUtility.LayoutCache layoutCache = GUILayoutUtility.SelectIDList(windowID, true);
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel = (layoutCache.topLevel = new GUILayoutGroup());
				GUILayoutUtility.current.topLevel.style = style;
				GUILayoutUtility.current.topLevel.windowID = windowID;
				if (options != null)
				{
					GUILayoutUtility.current.topLevel.ApplyOptions(options);
				}
				GUILayoutUtility.current.layoutGroups.Clear();
				GUILayoutUtility.current.layoutGroups.Push(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.current.windows = (layoutCache.windows = new GUILayoutGroup());
			}
			else
			{
				GUILayoutUtility.current.topLevel = layoutCache.topLevel;
				GUILayoutUtility.current.layoutGroups = layoutCache.layoutGroups;
				GUILayoutUtility.current.windows = layoutCache.windows;
			}
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00010FC4 File Offset: 0x0000F1C4
		public static void BeginGroup(string GroupName)
		{
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00010FC8 File Offset: 0x0000F1C8
		public static void EndGroup(string groupName)
		{
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00010FCC File Offset: 0x0000F1CC
		internal static void Layout()
		{
			if (GUILayoutUtility.current.topLevel.windowID == -1)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, Mathf.Min((float)Screen.width, GUILayoutUtility.current.topLevel.maxWidth));
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height, GUILayoutUtility.current.topLevel.maxHeight));
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
			else
			{
				GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001109C File Offset: 0x0000F29C
		internal static void LayoutFromEditorWindow()
		{
			GUILayoutUtility.current.topLevel.CalcWidth();
			GUILayoutUtility.current.topLevel.SetHorizontal(0f, (float)Screen.width);
			GUILayoutUtility.current.topLevel.CalcHeight();
			GUILayoutUtility.current.topLevel.SetVertical(0f, (float)Screen.height);
			GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001110C File Offset: 0x0000F30C
		internal static float LayoutFromInspector(float width)
		{
			if (GUILayoutUtility.current.topLevel != null && GUILayoutUtility.current.topLevel.windowID == -1)
			{
				GUILayoutUtility.current.topLevel.CalcWidth();
				GUILayoutUtility.current.topLevel.SetHorizontal(0f, width);
				GUILayoutUtility.current.topLevel.CalcHeight();
				GUILayoutUtility.current.topLevel.SetVertical(0f, Mathf.Min((float)Screen.height, GUILayoutUtility.current.topLevel.maxHeight));
				float minHeight = GUILayoutUtility.current.topLevel.minHeight;
				GUILayoutUtility.LayoutFreeGroup(GUILayoutUtility.current.windows);
				return minHeight;
			}
			if (GUILayoutUtility.current.topLevel != null)
			{
				GUILayoutUtility.LayoutSingleGroup(GUILayoutUtility.current.topLevel);
			}
			return 0f;
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000111E4 File Offset: 0x0000F3E4
		internal static void LayoutFreeGroup(GUILayoutGroup toplevel)
		{
			foreach (GUILayoutEntry guilayoutEntry in toplevel.entries)
			{
				GUILayoutGroup guilayoutGroup = (GUILayoutGroup)guilayoutEntry;
				GUILayoutUtility.LayoutSingleGroup(guilayoutGroup);
			}
			toplevel.ResetCursor();
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x00011254 File Offset: 0x0000F454
		private static void LayoutSingleGroup(GUILayoutGroup i)
		{
			if (!i.isWindow)
			{
				float minWidth = i.minWidth;
				float maxWidth = i.maxWidth;
				i.CalcWidth();
				i.SetHorizontal(i.rect.x, Mathf.Clamp(i.maxWidth, minWidth, maxWidth));
				float minHeight = i.minHeight;
				float maxHeight = i.maxHeight;
				i.CalcHeight();
				i.SetVertical(i.rect.y, Mathf.Clamp(i.maxHeight, minHeight, maxHeight));
			}
			else
			{
				i.CalcWidth();
				Rect rect = GUILayoutUtility.Internal_GetWindowRect(i.windowID);
				i.SetHorizontal(rect.x, Mathf.Clamp(rect.width, i.minWidth, i.maxWidth));
				i.CalcHeight();
				i.SetVertical(rect.y, Mathf.Clamp(rect.height, i.minHeight, i.maxHeight));
				GUILayoutUtility.Internal_MoveWindow(i.windowID, i.rect);
			}
		}

		// Token: 0x06000819 RID: 2073
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Rect Internal_GetWindowRect(int windowID);

		// Token: 0x0600081A RID: 2074 RVA: 0x0001134C File Offset: 0x0000F54C
		private static void Internal_MoveWindow(int windowID, Rect r)
		{
			GUILayoutUtility.INTERNAL_CALL_Internal_MoveWindow(windowID, ref r);
		}

		// Token: 0x0600081B RID: 2075
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_MoveWindow(int windowID, ref Rect r);

		// Token: 0x0600081C RID: 2076
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Rect GetWindowsBounds();

		// Token: 0x0600081D RID: 2077 RVA: 0x00011358 File Offset: 0x0000F558
		[SecuritySafeCritical]
		private static GUILayoutGroup CreateGUILayoutGroupInstanceOfType(Type LayoutType)
		{
			if (!typeof(GUILayoutGroup).IsAssignableFrom(LayoutType))
			{
				throw new ArgumentException("LayoutType needs to be of type GUILayoutGroup");
			}
			return (GUILayoutGroup)Activator.CreateInstance(LayoutType);
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00011388 File Offset: 0x0000F588
		internal static GUILayoutGroup BeginLayoutGroup(GUIStyle style, GUILayoutOption[] options, Type LayoutType)
		{
			EventType type = Event.current.type;
			GUILayoutGroup guilayoutGroup;
			if (type != EventType.Layout && type != EventType.Used)
			{
				guilayoutGroup = GUILayoutUtility.current.topLevel.GetNext() as GUILayoutGroup;
				if (guilayoutGroup == null)
				{
					throw new ArgumentException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(LayoutType);
				guilayoutGroup.style = style;
				if (options != null)
				{
					guilayoutGroup.ApplyOptions(options);
				}
				GUILayoutUtility.current.topLevel.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00011448 File Offset: 0x0000F648
		internal static void EndLayoutGroup()
		{
			EventType type = Event.current.type;
			GUILayoutUtility.current.layoutGroups.Pop();
			GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x00011490 File Offset: 0x0000F690
		internal static GUILayoutGroup BeginLayoutArea(GUIStyle style, Type LayoutType)
		{
			EventType type = Event.current.type;
			GUILayoutGroup guilayoutGroup;
			if (type != EventType.Layout && type != EventType.Used)
			{
				guilayoutGroup = GUILayoutUtility.current.windows.GetNext() as GUILayoutGroup;
				if (guilayoutGroup == null)
				{
					throw new ArgumentException("GUILayout: Mismatched LayoutGroup." + Event.current.type);
				}
				guilayoutGroup.ResetCursor();
			}
			else
			{
				guilayoutGroup = GUILayoutUtility.CreateGUILayoutGroupInstanceOfType(LayoutType);
				guilayoutGroup.style = style;
				GUILayoutUtility.current.windows.Add(guilayoutGroup);
			}
			GUILayoutUtility.current.layoutGroups.Push(guilayoutGroup);
			GUILayoutUtility.current.topLevel = guilayoutGroup;
			return guilayoutGroup;
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x00011540 File Offset: 0x0000F740
		internal static GUILayoutGroup DoBeginLayoutArea(GUIStyle style, Type LayoutType)
		{
			return GUILayoutUtility.BeginLayoutArea(style, LayoutType);
		}

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000822 RID: 2082 RVA: 0x0001154C File Offset: 0x0000F74C
		internal static GUILayoutGroup topLevel
		{
			get
			{
				return GUILayoutUtility.current.topLevel;
			}
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x00011558 File Offset: 0x0000F758
		public static Rect GetRect(GUIContent content, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(content, style, null);
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00011564 File Offset: 0x0000F764
		public static Rect GetRect(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(content, style, options);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00011570 File Offset: 0x0000F770
		private static Rect DoGetRect(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				if (style.isHeightDependantOnWidth)
				{
					GUILayoutUtility.current.topLevel.Add(new GUIWordWrapSizer(style, content, options));
				}
				else
				{
					Vector2 vector = style.CalcSize(content);
					GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(vector.x, vector.x, vector.y, vector.y, style, options));
				}
				return GUILayoutUtility.kDummyRect;
			}
			if (type != EventType.Used)
			{
				return GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			return GUILayoutUtility.kDummyRect;
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00011624 File Offset: 0x0000F824
		public static Rect GetRect(float width, float height)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, null);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00011638 File Offset: 0x0000F838
		public static Rect GetRect(float width, float height, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, null);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x00011648 File Offset: 0x0000F848
		public static Rect GetRect(float width, float height, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, GUIStyle.none, options);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x0001165C File Offset: 0x0000F85C
		public static Rect GetRect(float width, float height, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(width, width, height, height, style, options);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x0001166C File Offset: 0x0000F86C
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, null);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x00011680 File Offset: 0x0000F880
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, null);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00011690 File Offset: 0x0000F890
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, GUIStyle.none, options);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x000116A4 File Offset: 0x0000F8A4
		public static Rect GetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetRect(minWidth, maxWidth, minHeight, maxHeight, style, options);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x000116B4 File Offset: 0x0000F8B4
		private static Rect DoGetRect(float minWidth, float maxWidth, float minHeight, float maxHeight, GUIStyle style, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel.Add(new GUILayoutEntry(minWidth, maxWidth, minHeight, maxHeight, style, options));
				return GUILayoutUtility.kDummyRect;
			}
			if (type != EventType.Used)
			{
				return GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			return GUILayoutUtility.kDummyRect;
		}

		// Token: 0x0600082F RID: 2095 RVA: 0x0001171C File Offset: 0x0000F91C
		public static Rect GetLastRect()
		{
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				return GUILayoutUtility.kDummyRect;
			}
			if (type != EventType.Used)
			{
				return GUILayoutUtility.current.topLevel.GetLast();
			}
			return GUILayoutUtility.kDummyRect;
		}

		// Token: 0x06000830 RID: 2096 RVA: 0x00011764 File Offset: 0x0000F964
		public static Rect GetAspectRect(float aspect)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, GUIStyle.none, null);
		}

		// Token: 0x06000831 RID: 2097 RVA: 0x00011774 File Offset: 0x0000F974
		public static Rect GetAspectRect(float aspect, GUIStyle style)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, style, null);
		}

		// Token: 0x06000832 RID: 2098 RVA: 0x00011780 File Offset: 0x0000F980
		public static Rect GetAspectRect(float aspect, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, GUIStyle.none, options);
		}

		// Token: 0x06000833 RID: 2099 RVA: 0x00011790 File Offset: 0x0000F990
		public static Rect GetAspectRect(float aspect, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayoutUtility.DoGetAspectRect(aspect, GUIStyle.none, options);
		}

		// Token: 0x06000834 RID: 2100 RVA: 0x000117A0 File Offset: 0x0000F9A0
		private static Rect DoGetAspectRect(float aspect, GUIStyle style, GUILayoutOption[] options)
		{
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				GUILayoutUtility.current.topLevel.Add(new GUIAspectSizer(aspect, options));
				return GUILayoutUtility.kDummyRect;
			}
			if (type != EventType.Used)
			{
				return GUILayoutUtility.current.topLevel.GetNext().rect;
			}
			return GUILayoutUtility.kDummyRect;
		}

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x06000835 RID: 2101 RVA: 0x00011804 File Offset: 0x0000FA04
		internal static GUIStyle spaceStyle
		{
			get
			{
				if (GUILayoutUtility.s_SpaceStyle == null)
				{
					GUILayoutUtility.s_SpaceStyle = new GUIStyle();
				}
				GUILayoutUtility.s_SpaceStyle.stretchWidth = false;
				return GUILayoutUtility.s_SpaceStyle;
			}
		}

		// Token: 0x04000305 RID: 773
		private static Dictionary<int, GUILayoutUtility.LayoutCache> storedLayouts = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x04000306 RID: 774
		private static Dictionary<int, GUILayoutUtility.LayoutCache> storedWindows = new Dictionary<int, GUILayoutUtility.LayoutCache>();

		// Token: 0x04000307 RID: 775
		internal static GUILayoutUtility.LayoutCache current = new GUILayoutUtility.LayoutCache();

		// Token: 0x04000308 RID: 776
		private static Rect kDummyRect = new Rect(0f, 0f, 1f, 1f);

		// Token: 0x04000309 RID: 777
		private static GUIStyle s_SpaceStyle;

		// Token: 0x020000EF RID: 239
		internal sealed class LayoutCache
		{
			// Token: 0x06000836 RID: 2102 RVA: 0x00011838 File Offset: 0x0000FA38
			internal LayoutCache()
			{
				this.layoutGroups.Push(this.topLevel);
			}

			// Token: 0x06000837 RID: 2103 RVA: 0x00011880 File Offset: 0x0000FA80
			internal LayoutCache(GUILayoutUtility.LayoutCache other)
			{
				this.topLevel = other.topLevel;
				this.layoutGroups = other.layoutGroups;
				this.windows = other.windows;
			}

			// Token: 0x0400030A RID: 778
			internal GUILayoutGroup topLevel = new GUILayoutGroup();

			// Token: 0x0400030B RID: 779
			internal GenericStack layoutGroups = new GenericStack();

			// Token: 0x0400030C RID: 780
			internal GUILayoutGroup windows = new GUILayoutGroup();
		}
	}
}
