using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000FA RID: 250
	public class GUIUtility
	{
		// Token: 0x06000865 RID: 2149 RVA: 0x00013F5C File Offset: 0x0001215C
		public static int GetControlID(FocusType focus)
		{
			return GUIUtility.GetControlID(0, focus);
		}

		// Token: 0x06000866 RID: 2150
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetControlID(int hint, FocusType focus);

		// Token: 0x06000867 RID: 2151 RVA: 0x00013F68 File Offset: 0x00012168
		public static int GetControlID(GUIContent contents, FocusType focus)
		{
			return GUIUtility.GetControlID(contents.hash, focus);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00013F78 File Offset: 0x00012178
		public static int GetControlID(FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(0, focus, position);
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00013F84 File Offset: 0x00012184
		public static int GetControlID(int hint, FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(hint, focus, position);
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x00013F90 File Offset: 0x00012190
		public static int GetControlID(GUIContent contents, FocusType focus, Rect position)
		{
			return GUIUtility.Internal_GetNextControlID2(contents.hash, focus, position);
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x00013FA0 File Offset: 0x000121A0
		private static int Internal_GetNextControlID2(int hint, FocusType focusType, Rect rect)
		{
			return GUIUtility.INTERNAL_CALL_Internal_GetNextControlID2(hint, focusType, ref rect);
		}

		// Token: 0x0600086C RID: 2156
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int INTERNAL_CALL_Internal_GetNextControlID2(int hint, FocusType focusType, ref Rect rect);

		// Token: 0x0600086D RID: 2157 RVA: 0x00013FAC File Offset: 0x000121AC
		public static object GetStateObject(Type t, int controlID)
		{
			return GUIStateObjects.GetStateObject(t, controlID);
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x00013FB8 File Offset: 0x000121B8
		public static object QueryStateObject(Type t, int controlID)
		{
			return GUIStateObjects.QueryStateObject(t, controlID);
		}

		// Token: 0x0600086F RID: 2159
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetPermanentControlID();

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000870 RID: 2160 RVA: 0x00013FC4 File Offset: 0x000121C4
		// (set) Token: 0x06000871 RID: 2161 RVA: 0x00013FCC File Offset: 0x000121CC
		public static int hotControl
		{
			get
			{
				return GUIUtility.Internal_GetHotControl();
			}
			set
			{
				GUIUtility.Internal_SetHotControl(value);
			}
		}

		// Token: 0x06000872 RID: 2162
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetHotControl();

		// Token: 0x06000873 RID: 2163
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetHotControl(int value);

		// Token: 0x06000874 RID: 2164
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void UpdateUndoName();

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000875 RID: 2165
		// (set) Token: 0x06000876 RID: 2166
		public static extern int keyboardControl
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x00013FD4 File Offset: 0x000121D4
		public static void ExitGUI()
		{
			throw new ExitGUIException();
		}

		// Token: 0x06000878 RID: 2168
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetDidGUIWindowsEatLastEvent(bool value);

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000879 RID: 2169
		// (set) Token: 0x0600087A RID: 2170
		internal static extern string systemCopyBuffer
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x00013FDC File Offset: 0x000121DC
		internal static GUISkin GetDefaultSkin()
		{
			return GUIUtility.Internal_GetDefaultSkin(GUIUtility.s_SkinMode);
		}

		// Token: 0x0600087C RID: 2172
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern GUISkin Internal_GetDefaultSkin(int skinMode);

		// Token: 0x0600087D RID: 2173
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object Internal_GetBuiltinSkin(int skin);

		// Token: 0x0600087E RID: 2174 RVA: 0x00013FE8 File Offset: 0x000121E8
		internal static GUISkin GetBuiltinSkin(int skin)
		{
			return GUIUtility.Internal_GetBuiltinSkin(skin) as GUISkin;
		}

		// Token: 0x0600087F RID: 2175 RVA: 0x00013FF8 File Offset: 0x000121F8
		internal static void BeginGUI(int skinMode, int instanceID, int useGUILayout)
		{
			GUIUtility.s_SkinMode = skinMode;
			GUIUtility.s_OriginalID = instanceID;
			GUI.skin = null;
			if (useGUILayout != 0)
			{
				GUILayoutUtility.SelectIDList(instanceID, false);
				GUILayoutUtility.Begin(instanceID);
			}
			GUI.changed = false;
		}

		// Token: 0x06000880 RID: 2176
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_ExitGUI();

		// Token: 0x06000881 RID: 2177 RVA: 0x00014034 File Offset: 0x00012234
		internal static void EndGUI(int layoutType)
		{
			try
			{
				if (Event.current.type == EventType.Layout)
				{
					switch (layoutType)
					{
					case 1:
						GUILayoutUtility.Layout();
						break;
					case 2:
						GUILayoutUtility.LayoutFromEditorWindow();
						break;
					}
				}
				GUILayoutUtility.SelectIDList(GUIUtility.s_OriginalID, false);
				GUIContent.ClearStaticCache();
			}
			finally
			{
				GUIUtility.Internal_ExitGUI();
			}
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000140BC File Offset: 0x000122BC
		internal static bool EndGUIFromException(Exception exception)
		{
			if (exception == null)
			{
				return false;
			}
			if (!(exception is ExitGUIException) && !(exception.InnerException is ExitGUIException))
			{
				return false;
			}
			GUIUtility.Internal_ExitGUI();
			return true;
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000140EC File Offset: 0x000122EC
		internal static void CheckOnGUI()
		{
			if (GUIUtility.Internal_GetGUIDepth() <= 0)
			{
				throw new ArgumentException("You can only call GUI functions from inside OnGUI.");
			}
		}

		// Token: 0x06000884 RID: 2180
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int Internal_GetGUIDepth();

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000885 RID: 2181
		// (set) Token: 0x06000886 RID: 2182
		internal static extern bool mouseUsed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x00014104 File Offset: 0x00012304
		public static Vector2 GUIToScreenPoint(Vector2 guiPoint)
		{
			return GUIClip.Unclip(guiPoint) + GUIUtility.s_EditorScreenPointOffset;
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x00014118 File Offset: 0x00012318
		internal static Rect GUIToScreenRect(Rect guiRect)
		{
			Vector2 vector = GUIUtility.GUIToScreenPoint(new Vector2(guiRect.x, guiRect.y));
			guiRect.x = vector.x;
			guiRect.y = vector.y;
			return guiRect;
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x0001415C File Offset: 0x0001235C
		public static Vector2 ScreenToGUIPoint(Vector2 screenPoint)
		{
			return GUIClip.Clip(screenPoint) - GUIUtility.s_EditorScreenPointOffset;
		}

		// Token: 0x0600088A RID: 2186 RVA: 0x00014170 File Offset: 0x00012370
		public static Rect ScreenToGUIRect(Rect screenRect)
		{
			Vector2 vector = GUIUtility.ScreenToGUIPoint(new Vector2(screenRect.x, screenRect.y));
			screenRect.x = vector.x;
			screenRect.y = vector.y;
			return screenRect;
		}

		// Token: 0x0600088B RID: 2187 RVA: 0x000141B4 File Offset: 0x000123B4
		public static void RotateAroundPivot(float angle, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			GUI.matrix = Matrix4x4.identity;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.Euler(0f, 0f, angle), Vector3.one) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = matrix4x * matrix;
		}

		// Token: 0x0600088C RID: 2188 RVA: 0x00014224 File Offset: 0x00012424
		public static void ScaleAroundPivot(Vector2 scale, Vector2 pivotPoint)
		{
			Matrix4x4 matrix = GUI.matrix;
			Vector2 vector = GUIClip.Unclip(pivotPoint);
			Matrix4x4 matrix4x = Matrix4x4.TRS(vector, Quaternion.identity, new Vector3(scale.x, scale.y, 1f)) * Matrix4x4.TRS(-vector, Quaternion.identity, Vector3.one);
			GUI.matrix = matrix4x * matrix;
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x0600088D RID: 2189
		public static extern bool hasModalWindow
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x0600088E RID: 2190
		// (set) Token: 0x0600088F RID: 2191
		internal static extern bool textFieldInput
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x04000353 RID: 851
		[NotRenamed]
		internal static int s_SkinMode;

		// Token: 0x04000354 RID: 852
		[NotRenamed]
		internal static int s_OriginalID;

		// Token: 0x04000355 RID: 853
		internal static Vector2 s_EditorScreenPointOffset = Vector2.zero;

		// Token: 0x04000356 RID: 854
		internal static bool s_HasKeyboardFocus = false;
	}
}
