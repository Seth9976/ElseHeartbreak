using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;
using UnityEngineInternal;

namespace UnityEngine
{
	// Token: 0x020000EA RID: 234
	public class GUI
	{
		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006DE RID: 1758 RVA: 0x0000D174 File Offset: 0x0000B374
		// (set) Token: 0x060006DF RID: 1759 RVA: 0x0000D17C File Offset: 0x0000B37C
		internal static DateTime nextScrollStepTime { get; set; } = DateTime.Now;

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006E0 RID: 1760 RVA: 0x0000D184 File Offset: 0x0000B384
		// (set) Token: 0x060006E1 RID: 1761 RVA: 0x0000D18C File Offset: 0x0000B38C
		internal static int scrollTroughSide { get; set; }

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006E3 RID: 1763 RVA: 0x0000D1BC File Offset: 0x0000B3BC
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0000D194 File Offset: 0x0000B394
		public static GUISkin skin
		{
			get
			{
				GUIUtility.CheckOnGUI();
				return GUI.s_Skin;
			}
			set
			{
				GUIUtility.CheckOnGUI();
				if (!value)
				{
					value = GUIUtility.GetDefaultSkin();
				}
				GUI.s_Skin = value;
				value.MakeCurrent();
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006E4 RID: 1764 RVA: 0x0000D1C8 File Offset: 0x0000B3C8
		// (set) Token: 0x060006E5 RID: 1765 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		public static Color color
		{
			get
			{
				Color color;
				GUI.INTERNAL_get_color(out color);
				return color;
			}
			set
			{
				GUI.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x060006E6 RID: 1766
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_color(out Color value);

		// Token: 0x060006E7 RID: 1767
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_color(ref Color value);

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060006E8 RID: 1768 RVA: 0x0000D1EC File Offset: 0x0000B3EC
		// (set) Token: 0x060006E9 RID: 1769 RVA: 0x0000D204 File Offset: 0x0000B404
		public static Color backgroundColor
		{
			get
			{
				Color color;
				GUI.INTERNAL_get_backgroundColor(out color);
				return color;
			}
			set
			{
				GUI.INTERNAL_set_backgroundColor(ref value);
			}
		}

		// Token: 0x060006EA RID: 1770
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_backgroundColor(out Color value);

		// Token: 0x060006EB RID: 1771
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_backgroundColor(ref Color value);

		// Token: 0x170001C3 RID: 451
		// (get) Token: 0x060006EC RID: 1772 RVA: 0x0000D210 File Offset: 0x0000B410
		// (set) Token: 0x060006ED RID: 1773 RVA: 0x0000D228 File Offset: 0x0000B428
		public static Color contentColor
		{
			get
			{
				Color color;
				GUI.INTERNAL_get_contentColor(out color);
				return color;
			}
			set
			{
				GUI.INTERNAL_set_contentColor(ref value);
			}
		}

		// Token: 0x060006EE RID: 1774
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_contentColor(out Color value);

		// Token: 0x060006EF RID: 1775
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_set_contentColor(ref Color value);

		// Token: 0x170001C4 RID: 452
		// (get) Token: 0x060006F0 RID: 1776
		// (set) Token: 0x060006F1 RID: 1777
		public static extern bool changed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001C5 RID: 453
		// (get) Token: 0x060006F2 RID: 1778
		// (set) Token: 0x060006F3 RID: 1779
		public static extern bool enabled
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001C6 RID: 454
		// (get) Token: 0x060006F4 RID: 1780 RVA: 0x0000D234 File Offset: 0x0000B434
		// (set) Token: 0x060006F5 RID: 1781 RVA: 0x0000D23C File Offset: 0x0000B43C
		public static Matrix4x4 matrix
		{
			get
			{
				return GUIClip.GetMatrix();
			}
			set
			{
				GUIClip.SetMatrix(value);
			}
		}

		// Token: 0x170001C7 RID: 455
		// (get) Token: 0x060006F6 RID: 1782 RVA: 0x0000D244 File Offset: 0x0000B444
		// (set) Token: 0x060006F7 RID: 1783 RVA: 0x0000D264 File Offset: 0x0000B464
		public static string tooltip
		{
			get
			{
				string text = GUI.Internal_GetTooltip();
				if (text != null)
				{
					return text;
				}
				return string.Empty;
			}
			set
			{
				GUI.Internal_SetTooltip(value);
			}
		}

		// Token: 0x060006F8 RID: 1784
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetTooltip();

		// Token: 0x060006F9 RID: 1785
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_SetTooltip(string value);

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x060006FA RID: 1786 RVA: 0x0000D26C File Offset: 0x0000B46C
		protected static string mouseTooltip
		{
			get
			{
				return GUI.Internal_GetMouseTooltip();
			}
		}

		// Token: 0x060006FB RID: 1787
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetMouseTooltip();

		// Token: 0x170001C9 RID: 457
		// (get) Token: 0x060006FC RID: 1788 RVA: 0x0000D274 File Offset: 0x0000B474
		// (set) Token: 0x060006FD RID: 1789 RVA: 0x0000D27C File Offset: 0x0000B47C
		protected static Rect tooltipRect
		{
			get
			{
				return GUI.s_ToolTipRect;
			}
			set
			{
				GUI.s_ToolTipRect = value;
			}
		}

		// Token: 0x170001CA RID: 458
		// (get) Token: 0x060006FE RID: 1790
		// (set) Token: 0x060006FF RID: 1791
		public static extern int depth
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x06000700 RID: 1792 RVA: 0x0000D284 File Offset: 0x0000B484
		public static void Label(Rect position, string text)
		{
			GUI.Label(position, GUIContent.Temp(text), GUI.s_Skin.label);
		}

		// Token: 0x06000701 RID: 1793 RVA: 0x0000D29C File Offset: 0x0000B49C
		public static void Label(Rect position, Texture image)
		{
			GUI.Label(position, GUIContent.Temp(image), GUI.s_Skin.label);
		}

		// Token: 0x06000702 RID: 1794 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
		public static void Label(Rect position, GUIContent content)
		{
			GUI.Label(position, content, GUI.s_Skin.label);
		}

		// Token: 0x06000703 RID: 1795 RVA: 0x0000D2C8 File Offset: 0x0000B4C8
		public static void Label(Rect position, string text, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06000704 RID: 1796 RVA: 0x0000D2D8 File Offset: 0x0000B4D8
		public static void Label(Rect position, Texture image, GUIStyle style)
		{
			GUI.Label(position, GUIContent.Temp(image), style);
		}

		// Token: 0x06000705 RID: 1797 RVA: 0x0000D2E8 File Offset: 0x0000B4E8
		public static void Label(Rect position, GUIContent content, GUIStyle style)
		{
			GUI.DoLabel(position, content, style.m_Ptr);
		}

		// Token: 0x06000706 RID: 1798 RVA: 0x0000D2F8 File Offset: 0x0000B4F8
		private static void DoLabel(Rect position, GUIContent content, IntPtr style)
		{
			GUI.INTERNAL_CALL_DoLabel(ref position, content, style);
		}

		// Token: 0x06000707 RID: 1799
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DoLabel(ref Rect position, GUIContent content, IntPtr style);

		// Token: 0x06000708 RID: 1800
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InitializeGUIClipTexture();

		// Token: 0x06000709 RID: 1801 RVA: 0x0000D304 File Offset: 0x0000B504
		[ExcludeFromDocs]
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode, bool alphaBlend)
		{
			float num = 0f;
			GUI.DrawTexture(position, image, scaleMode, alphaBlend, num);
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000D324 File Offset: 0x0000B524
		[ExcludeFromDocs]
		public static void DrawTexture(Rect position, Texture image, ScaleMode scaleMode)
		{
			float num = 0f;
			bool flag = true;
			GUI.DrawTexture(position, image, scaleMode, flag, num);
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000D344 File Offset: 0x0000B544
		[ExcludeFromDocs]
		public static void DrawTexture(Rect position, Texture image)
		{
			float num = 0f;
			bool flag = true;
			ScaleMode scaleMode = ScaleMode.StretchToFill;
			GUI.DrawTexture(position, image, scaleMode, flag, num);
		}

		// Token: 0x0600070C RID: 1804 RVA: 0x0000D368 File Offset: 0x0000B568
		public static void DrawTexture(Rect position, Texture image, [DefaultValue("ScaleMode.StretchToFill")] ScaleMode scaleMode, [DefaultValue("true")] bool alphaBlend, [DefaultValue("0")] float imageAspect)
		{
			if (Event.current.type == EventType.Repaint)
			{
				if (image == null)
				{
					Debug.LogWarning("null texture passed to GUI.DrawTexture");
					return;
				}
				if (imageAspect == 0f)
				{
					imageAspect = (float)image.width / (float)image.height;
				}
				Material material = ((!alphaBlend) ? GUI.blitMaterial : GUI.blendMaterial);
				float num = position.width / position.height;
				InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
				internalDrawTextureArguments.texture = image;
				internalDrawTextureArguments.leftBorder = 0;
				internalDrawTextureArguments.rightBorder = 0;
				internalDrawTextureArguments.topBorder = 0;
				internalDrawTextureArguments.bottomBorder = 0;
				internalDrawTextureArguments.color = GUI.color;
				internalDrawTextureArguments.mat = material;
				switch (scaleMode)
				{
				case ScaleMode.StretchToFill:
					internalDrawTextureArguments.screenRect = position;
					internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
					Graphics.DrawTexture(ref internalDrawTextureArguments);
					break;
				case ScaleMode.ScaleAndCrop:
					if (num > imageAspect)
					{
						float num2 = imageAspect / num;
						internalDrawTextureArguments.screenRect = position;
						internalDrawTextureArguments.sourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					else
					{
						float num3 = num / imageAspect;
						internalDrawTextureArguments.screenRect = position;
						internalDrawTextureArguments.sourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					break;
				case ScaleMode.ScaleToFit:
					if (num > imageAspect)
					{
						float num4 = imageAspect / num;
						internalDrawTextureArguments.screenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
						internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					else
					{
						float num5 = num / imageAspect;
						internalDrawTextureArguments.screenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
						internalDrawTextureArguments.sourceRect = new Rect(0f, 0f, 1f, 1f);
						Graphics.DrawTexture(ref internalDrawTextureArguments);
					}
					break;
				}
			}
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0000D5EC File Offset: 0x0000B7EC
		internal static bool CalculateScaledTextureRects(Rect position, ScaleMode scaleMode, float imageAspect, ref Rect outScreenRect, ref Rect outSourceRect)
		{
			float num = position.width / position.height;
			bool flag = false;
			switch (scaleMode)
			{
			case ScaleMode.StretchToFill:
				outScreenRect = position;
				outSourceRect = new Rect(0f, 0f, 1f, 1f);
				flag = true;
				break;
			case ScaleMode.ScaleAndCrop:
				if (num > imageAspect)
				{
					float num2 = imageAspect / num;
					outScreenRect = position;
					outSourceRect = new Rect(0f, (1f - num2) * 0.5f, 1f, num2);
					flag = true;
				}
				else
				{
					float num3 = num / imageAspect;
					outScreenRect = position;
					outSourceRect = new Rect(0.5f - num3 * 0.5f, 0f, num3, 1f);
					flag = true;
				}
				break;
			case ScaleMode.ScaleToFit:
				if (num > imageAspect)
				{
					float num4 = imageAspect / num;
					outScreenRect = new Rect(position.xMin + position.width * (1f - num4) * 0.5f, position.yMin, num4 * position.width, position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					flag = true;
				}
				else
				{
					float num5 = num / imageAspect;
					outScreenRect = new Rect(position.xMin, position.yMin + position.height * (1f - num5) * 0.5f, position.width, num5 * position.height);
					outSourceRect = new Rect(0f, 0f, 1f, 1f);
					flag = true;
				}
				break;
			}
			return flag;
		}

		// Token: 0x0600070E RID: 1806 RVA: 0x0000D788 File Offset: 0x0000B988
		[ExcludeFromDocs]
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords)
		{
			bool flag = true;
			GUI.DrawTextureWithTexCoords(position, image, texCoords, flag);
		}

		// Token: 0x0600070F RID: 1807 RVA: 0x0000D7A0 File Offset: 0x0000B9A0
		public static void DrawTextureWithTexCoords(Rect position, Texture image, Rect texCoords, [DefaultValue("true")] bool alphaBlend)
		{
			if (Event.current.type == EventType.Repaint)
			{
				Material material = ((!alphaBlend) ? GUI.blitMaterial : GUI.blendMaterial);
				InternalDrawTextureArguments internalDrawTextureArguments = default(InternalDrawTextureArguments);
				internalDrawTextureArguments.texture = image;
				internalDrawTextureArguments.leftBorder = 0;
				internalDrawTextureArguments.rightBorder = 0;
				internalDrawTextureArguments.topBorder = 0;
				internalDrawTextureArguments.bottomBorder = 0;
				internalDrawTextureArguments.color = GUI.color;
				internalDrawTextureArguments.mat = material;
				internalDrawTextureArguments.screenRect = position;
				internalDrawTextureArguments.sourceRect = texCoords;
				Graphics.DrawTexture(ref internalDrawTextureArguments);
			}
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x06000710 RID: 1808
		private static extern Material blendMaterial
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170001CC RID: 460
		// (get) Token: 0x06000711 RID: 1809
		private static extern Material blitMaterial
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000D834 File Offset: 0x0000BA34
		public static void Box(Rect position, string text)
		{
			GUI.Box(position, GUIContent.Temp(text), GUI.s_Skin.box);
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000D84C File Offset: 0x0000BA4C
		public static void Box(Rect position, Texture image)
		{
			GUI.Box(position, GUIContent.Temp(image), GUI.s_Skin.box);
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000D864 File Offset: 0x0000BA64
		public static void Box(Rect position, GUIContent content)
		{
			GUI.Box(position, content, GUI.s_Skin.box);
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000D878 File Offset: 0x0000BA78
		public static void Box(Rect position, string text, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06000716 RID: 1814 RVA: 0x0000D888 File Offset: 0x0000BA88
		public static void Box(Rect position, Texture image, GUIStyle style)
		{
			GUI.Box(position, GUIContent.Temp(image), style);
		}

		// Token: 0x06000717 RID: 1815 RVA: 0x0000D898 File Offset: 0x0000BA98
		public static void Box(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.boxHash, FocusType.Passive);
			if (Event.current.type == EventType.Repaint)
			{
				style.Draw(position, content, controlID);
			}
		}

		// Token: 0x06000718 RID: 1816 RVA: 0x0000D8D0 File Offset: 0x0000BAD0
		public static bool Button(Rect position, string text)
		{
			return GUI.DoButton(position, GUIContent.Temp(text), GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0000D8F0 File Offset: 0x0000BAF0
		public static bool Button(Rect position, Texture image)
		{
			return GUI.DoButton(position, GUIContent.Temp(image), GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0000D910 File Offset: 0x0000BB10
		public static bool Button(Rect position, GUIContent content)
		{
			return GUI.DoButton(position, content, GUI.s_Skin.button.m_Ptr);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0000D928 File Offset: 0x0000BB28
		public static bool Button(Rect position, string text, GUIStyle style)
		{
			return GUI.DoButton(position, GUIContent.Temp(text), style.m_Ptr);
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0000D93C File Offset: 0x0000BB3C
		public static bool Button(Rect position, Texture image, GUIStyle style)
		{
			return GUI.DoButton(position, GUIContent.Temp(image), style.m_Ptr);
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0000D950 File Offset: 0x0000BB50
		public static bool Button(Rect position, GUIContent content, GUIStyle style)
		{
			return GUI.DoButton(position, content, style.m_Ptr);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0000D960 File Offset: 0x0000BB60
		private static bool DoButton(Rect position, GUIContent content, IntPtr style)
		{
			return GUI.INTERNAL_CALL_DoButton(ref position, content, style);
		}

		// Token: 0x0600071F RID: 1823
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_DoButton(ref Rect position, GUIContent content, IntPtr style);

		// Token: 0x06000720 RID: 1824 RVA: 0x0000D96C File Offset: 0x0000BB6C
		public static bool RepeatButton(Rect position, string text)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0000D988 File Offset: 0x0000BB88
		public static bool RepeatButton(Rect position, Texture image)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0000D9A4 File Offset: 0x0000BBA4
		public static bool RepeatButton(Rect position, GUIContent content)
		{
			return GUI.DoRepeatButton(position, content, GUI.s_Skin.button, FocusType.Native);
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0000D9B8 File Offset: 0x0000BBB8
		public static bool RepeatButton(Rect position, string text, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(text), style, FocusType.Native);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0000D9C8 File Offset: 0x0000BBC8
		public static bool RepeatButton(Rect position, Texture image, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, GUIContent.Temp(image), style, FocusType.Native);
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0000D9D8 File Offset: 0x0000BBD8
		public static bool RepeatButton(Rect position, GUIContent content, GUIStyle style)
		{
			return GUI.DoRepeatButton(position, content, style, FocusType.Native);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0000D9E4 File Offset: 0x0000BBE4
		private static bool DoRepeatButton(Rect position, GUIContent content, GUIStyle style, FocusType focusType)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.repeatButtonHash, focusType, position);
			EventType typeForControl = Event.current.GetTypeForControl(controlID);
			if (typeForControl == EventType.MouseDown)
			{
				if (position.Contains(Event.current.mousePosition))
				{
					GUIUtility.hotControl = controlID;
					Event.current.Use();
				}
				return false;
			}
			if (typeForControl != EventType.MouseUp)
			{
				if (typeForControl != EventType.Repaint)
				{
					return false;
				}
				style.Draw(position, content, controlID);
				return controlID == GUIUtility.hotControl && position.Contains(Event.current.mousePosition);
			}
			else
			{
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					return position.Contains(Event.current.mousePosition);
				}
				return false;
			}
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0000DAAC File Offset: 0x0000BCAC
		public static string TextField(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, GUI.skin.textField);
			return guicontent.text;
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000DAE0 File Offset: 0x0000BCE0
		public static string TextField(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, GUI.skin.textField);
			return guicontent.text;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000DB14 File Offset: 0x0000BD14
		public static string TextField(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, -1, style);
			return guicontent.text;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000DB40 File Offset: 0x0000BD40
		public static string TextField(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000DB6C File Offset: 0x0000BD6C
		public static string PasswordField(Rect position, string password, char maskChar)
		{
			return GUI.PasswordField(position, password, maskChar, -1, GUI.skin.textField);
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength)
		{
			return GUI.PasswordField(position, password, maskChar, maxLength, GUI.skin.textField);
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000DBAC File Offset: 0x0000BDAC
		public static string PasswordField(Rect position, string password, char maskChar, GUIStyle style)
		{
			return GUI.PasswordField(position, password, maskChar, -1, style);
		}

		// Token: 0x0600072E RID: 1838 RVA: 0x0000DBB8 File Offset: 0x0000BDB8
		public static string PasswordField(Rect position, string password, char maskChar, int maxLength, GUIStyle style)
		{
			string text = GUI.PasswordFieldGetStrToShow(password, maskChar);
			GUIContent guicontent = GUIContent.Temp(text);
			bool changed = GUI.changed;
			GUI.changed = false;
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			text = ((!GUI.changed) ? password : guicontent.text);
			GUI.changed = GUI.changed || changed;
			return text;
		}

		// Token: 0x0600072F RID: 1839 RVA: 0x0000DC18 File Offset: 0x0000BE18
		internal static string PasswordFieldGetStrToShow(string password, char maskChar)
		{
			return (Event.current.type != EventType.Repaint && Event.current.type != EventType.MouseDown) ? password : string.Empty.PadRight(password.Length, maskChar);
		}

		// Token: 0x06000730 RID: 1840 RVA: 0x0000DC5C File Offset: 0x0000BE5C
		public static string TextArea(Rect position, string text)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, GUI.skin.textArea);
			return guicontent.text;
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000DC90 File Offset: 0x0000BE90
		public static string TextArea(Rect position, string text, int maxLength)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, maxLength, GUI.skin.textArea);
			return guicontent.text;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000DCC4 File Offset: 0x0000BEC4
		public static string TextArea(Rect position, string text, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, true, -1, style);
			return guicontent.text;
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000DCF0 File Offset: 0x0000BEF0
		public static string TextArea(Rect position, string text, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(text);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x06000734 RID: 1844 RVA: 0x0000DD1C File Offset: 0x0000BF1C
		private static string TextArea(Rect position, GUIContent content, int maxLength, GUIStyle style)
		{
			GUIContent guicontent = GUIContent.Temp(content.text, content.image);
			GUI.DoTextField(position, GUIUtility.GetControlID(FocusType.Keyboard, position), guicontent, false, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x06000735 RID: 1845 RVA: 0x0000DD54 File Offset: 0x0000BF54
		internal static void DoTextField(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style)
		{
			if (maxLength >= 0 && content.text.Length > maxLength)
			{
				content.text = content.text.Substring(0, maxLength);
			}
			GUIUtility.CheckOnGUI();
			TextEditor textEditor = (TextEditor)GUIUtility.GetStateObject(typeof(TextEditor), id);
			textEditor.content.text = content.text;
			textEditor.SaveBackup();
			textEditor.position = position;
			textEditor.style = style;
			textEditor.multiline = multiline;
			textEditor.controlID = id;
			textEditor.ClampPos();
			if (GUIUtility.keyboardControl == id && Event.current.type != EventType.Layout)
			{
				textEditor.UpdateScrollOffsetIfNeeded();
			}
			GUI.HandleTextFieldEventForDesktop(position, id, content, multiline, maxLength, style, textEditor);
		}

		// Token: 0x06000736 RID: 1846 RVA: 0x0000DE14 File Offset: 0x0000C014
		private static void HandleTextFieldEventForDesktop(Rect position, int id, GUIContent content, bool multiline, int maxLength, GUIStyle style, TextEditor editor)
		{
			Event current = Event.current;
			bool flag = false;
			switch (current.type)
			{
			case EventType.MouseDown:
				if (position.Contains(current.mousePosition))
				{
					GUIUtility.hotControl = id;
					GUIUtility.keyboardControl = id;
					editor.m_HasFocus = true;
					editor.MoveCursorToPosition(Event.current.mousePosition);
					if (Event.current.clickCount == 2 && GUI.skin.settings.doubleClickSelectsWord)
					{
						editor.SelectCurrentWord();
						editor.DblClickSnap(TextEditor.DblClickSnapping.WORDS);
						editor.MouseDragSelectsWholeWords(true);
					}
					if (Event.current.clickCount == 3 && GUI.skin.settings.tripleClickSelectsLine)
					{
						editor.SelectCurrentParagraph();
						editor.MouseDragSelectsWholeWords(true);
						editor.DblClickSnap(TextEditor.DblClickSnapping.PARAGRAPHS);
					}
					current.Use();
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == id)
				{
					editor.MouseDragSelectsWholeWords(false);
					GUIUtility.hotControl = 0;
					current.Use();
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == id)
				{
					if (current.shift)
					{
						editor.MoveCursorToPosition(Event.current.mousePosition);
					}
					else
					{
						editor.SelectToPosition(Event.current.mousePosition);
					}
					current.Use();
				}
				break;
			case EventType.KeyDown:
				if (GUIUtility.keyboardControl != id)
				{
					return;
				}
				if (editor.HandleKeyEvent(current))
				{
					current.Use();
					flag = true;
					content.text = editor.content.text;
				}
				else
				{
					if (current.keyCode == KeyCode.Tab || current.character == '\t')
					{
						return;
					}
					char character = current.character;
					if (character == '\n' && !multiline && !current.alt)
					{
						return;
					}
					Font font = style.font;
					if (!font)
					{
						font = GUI.skin.font;
					}
					if (font.HasCharacter(character) || character == '\n')
					{
						editor.Insert(character);
						flag = true;
					}
					else if (character == '\0')
					{
						if (Input.compositionString.Length > 0)
						{
							editor.ReplaceSelection(string.Empty);
							flag = true;
						}
						current.Use();
					}
				}
				break;
			case EventType.Repaint:
				if (GUIUtility.keyboardControl != id)
				{
					style.Draw(position, content, id, false);
				}
				else
				{
					editor.DrawCursor(content.text);
				}
				break;
			}
			if (GUIUtility.keyboardControl == id)
			{
				GUIUtility.textFieldInput = true;
			}
			if (flag)
			{
				GUI.changed = true;
				content.text = editor.content.text;
				if (maxLength >= 0 && content.text.Length > maxLength)
				{
					content.text = content.text.Substring(0, maxLength);
				}
				current.Use();
			}
		}

		// Token: 0x06000737 RID: 1847
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetNextControlName(string name);

		// Token: 0x06000738 RID: 1848
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetNameOfFocusedControl();

		// Token: 0x06000739 RID: 1849
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FocusControl(string name);

		// Token: 0x0600073A RID: 1850 RVA: 0x0000E0F8 File Offset: 0x0000C2F8
		public static bool Toggle(Rect position, bool value, string text)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), GUI.s_Skin.toggle);
		}

		// Token: 0x0600073B RID: 1851 RVA: 0x0000E114 File Offset: 0x0000C314
		public static bool Toggle(Rect position, bool value, Texture image)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), GUI.s_Skin.toggle);
		}

		// Token: 0x0600073C RID: 1852 RVA: 0x0000E130 File Offset: 0x0000C330
		public static bool Toggle(Rect position, bool value, GUIContent content)
		{
			return GUI.Toggle(position, value, content, GUI.s_Skin.toggle);
		}

		// Token: 0x0600073D RID: 1853 RVA: 0x0000E144 File Offset: 0x0000C344
		public static bool Toggle(Rect position, bool value, string text, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(text), style);
		}

		// Token: 0x0600073E RID: 1854 RVA: 0x0000E154 File Offset: 0x0000C354
		public static bool Toggle(Rect position, bool value, Texture image, GUIStyle style)
		{
			return GUI.Toggle(position, value, GUIContent.Temp(image), style);
		}

		// Token: 0x0600073F RID: 1855 RVA: 0x0000E164 File Offset: 0x0000C364
		public static bool Toggle(Rect position, bool value, GUIContent content, GUIStyle style)
		{
			return GUI.DoToggle(position, GUIUtility.GetControlID(GUI.toggleHash, FocusType.Native, position), value, content, style.m_Ptr);
		}

		// Token: 0x06000740 RID: 1856 RVA: 0x0000E180 File Offset: 0x0000C380
		public static bool Toggle(Rect position, int id, bool value, GUIContent content, GUIStyle style)
		{
			return GUI.DoToggle(position, id, value, content, style.m_Ptr);
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0000E194 File Offset: 0x0000C394
		internal static bool DoToggle(Rect position, int id, bool value, GUIContent content, IntPtr style)
		{
			return GUI.INTERNAL_CALL_DoToggle(ref position, id, value, content, style);
		}

		// Token: 0x06000742 RID: 1858
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_DoToggle(ref Rect position, int id, bool value, GUIContent content, IntPtr style);

		// Token: 0x06000743 RID: 1859 RVA: 0x0000E1A4 File Offset: 0x0000C3A4
		public static int Toolbar(Rect position, int selected, string[] texts)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), GUI.s_Skin.button);
		}

		// Token: 0x06000744 RID: 1860 RVA: 0x0000E1C0 File Offset: 0x0000C3C0
		public static int Toolbar(Rect position, int selected, Texture[] images)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), GUI.s_Skin.button);
		}

		// Token: 0x06000745 RID: 1861 RVA: 0x0000E1DC File Offset: 0x0000C3DC
		public static int Toolbar(Rect position, int selected, GUIContent[] content)
		{
			return GUI.Toolbar(position, selected, content, GUI.s_Skin.button);
		}

		// Token: 0x06000746 RID: 1862 RVA: 0x0000E1F0 File Offset: 0x0000C3F0
		public static int Toolbar(Rect position, int selected, string[] texts, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(texts), style);
		}

		// Token: 0x06000747 RID: 1863 RVA: 0x0000E200 File Offset: 0x0000C400
		public static int Toolbar(Rect position, int selected, Texture[] images, GUIStyle style)
		{
			return GUI.Toolbar(position, selected, GUIContent.Temp(images), style);
		}

		// Token: 0x06000748 RID: 1864 RVA: 0x0000E210 File Offset: 0x0000C410
		public static int Toolbar(Rect position, int selected, GUIContent[] contents, GUIStyle style)
		{
			GUIStyle guistyle;
			GUIStyle guistyle2;
			GUIStyle guistyle3;
			GUI.FindStyles(ref style, out guistyle, out guistyle2, out guistyle3, "left", "mid", "right");
			return GUI.DoButtonGrid(position, selected, contents, contents.Length, style, guistyle, guistyle2, guistyle3);
		}

		// Token: 0x06000749 RID: 1865 RVA: 0x0000E248 File Offset: 0x0000C448
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, null);
		}

		// Token: 0x0600074A RID: 1866 RVA: 0x0000E25C File Offset: 0x0000C45C
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, null);
		}

		// Token: 0x0600074B RID: 1867 RVA: 0x0000E270 File Offset: 0x0000C470
		public static int SelectionGrid(Rect position, int selected, GUIContent[] content, int xCount)
		{
			return GUI.SelectionGrid(position, selected, content, xCount, null);
		}

		// Token: 0x0600074C RID: 1868 RVA: 0x0000E27C File Offset: 0x0000C47C
		public static int SelectionGrid(Rect position, int selected, string[] texts, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(texts), xCount, style);
		}

		// Token: 0x0600074D RID: 1869 RVA: 0x0000E290 File Offset: 0x0000C490
		public static int SelectionGrid(Rect position, int selected, Texture[] images, int xCount, GUIStyle style)
		{
			return GUI.SelectionGrid(position, selected, GUIContent.Temp(images), xCount, style);
		}

		// Token: 0x0600074E RID: 1870 RVA: 0x0000E2A4 File Offset: 0x0000C4A4
		public static int SelectionGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style)
		{
			if (style == null)
			{
				style = GUI.s_Skin.button;
			}
			return GUI.DoButtonGrid(position, selected, contents, xCount, style, style, style, style);
		}

		// Token: 0x0600074F RID: 1871 RVA: 0x0000E2D8 File Offset: 0x0000C4D8
		internal static void FindStyles(ref GUIStyle style, out GUIStyle firstStyle, out GUIStyle midStyle, out GUIStyle lastStyle, string first, string mid, string last)
		{
			if (style == null)
			{
				style = GUI.skin.button;
			}
			string name = style.name;
			midStyle = GUI.skin.FindStyle(name + mid);
			if (midStyle == null)
			{
				midStyle = style;
			}
			firstStyle = GUI.skin.FindStyle(name + first);
			if (firstStyle == null)
			{
				firstStyle = midStyle;
			}
			lastStyle = GUI.skin.FindStyle(name + last);
			if (lastStyle == null)
			{
				lastStyle = midStyle;
			}
		}

		// Token: 0x06000750 RID: 1872 RVA: 0x0000E360 File Offset: 0x0000C560
		internal static int CalcTotalHorizSpacing(int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			if (xCount < 2)
			{
				return 0;
			}
			if (xCount == 2)
			{
				return Mathf.Max(firstStyle.margin.right, lastStyle.margin.left);
			}
			int num = Mathf.Max(midStyle.margin.left, midStyle.margin.right);
			return Mathf.Max(firstStyle.margin.right, midStyle.margin.left) + Mathf.Max(midStyle.margin.right, lastStyle.margin.left) + num * (xCount - 3);
		}

		// Token: 0x06000751 RID: 1873 RVA: 0x0000E3F4 File Offset: 0x0000C5F4
		private static int DoButtonGrid(Rect position, int selected, GUIContent[] contents, int xCount, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle)
		{
			GUIUtility.CheckOnGUI();
			int num = contents.Length;
			if (num == 0)
			{
				return selected;
			}
			if (xCount <= 0)
			{
				Debug.LogWarning("You are trying to create a SelectionGrid with zero or less elements to be displayed in the horizontal direction. Set xCount to a positive value.");
				return selected;
			}
			int controlID = GUIUtility.GetControlID(GUI.buttonGridHash, FocusType.Native, position);
			int num2 = num / xCount;
			if (num % xCount != 0)
			{
				num2++;
			}
			float num3 = (float)GUI.CalcTotalHorizSpacing(xCount, style, firstStyle, midStyle, lastStyle);
			float num4 = (float)(Mathf.Max(style.margin.top, style.margin.bottom) * (num2 - 1));
			float num5 = (position.width - num3) / (float)xCount;
			float num6 = (position.height - num4) / (float)num2;
			if (style.fixedWidth != 0f)
			{
				num5 = style.fixedWidth;
			}
			if (style.fixedHeight != 0f)
			{
				num6 = style.fixedHeight;
			}
			switch (Event.current.GetTypeForControl(controlID))
			{
			case EventType.MouseDown:
				if (position.Contains(Event.current.mousePosition))
				{
					Rect[] array = GUI.CalcMouseRects(position, num, xCount, num5, num6, style, firstStyle, midStyle, lastStyle, false);
					if (GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, true) != -1)
					{
						GUIUtility.hotControl = controlID;
						Event.current.Use();
					}
				}
				break;
			case EventType.MouseUp:
				if (GUIUtility.hotControl == controlID)
				{
					GUIUtility.hotControl = 0;
					Event.current.Use();
					Rect[] array = GUI.CalcMouseRects(position, num, xCount, num5, num6, style, firstStyle, midStyle, lastStyle, false);
					int buttonGridMouseSelection = GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, true);
					GUI.changed = true;
					return buttonGridMouseSelection;
				}
				break;
			case EventType.MouseDrag:
				if (GUIUtility.hotControl == controlID)
				{
					Event.current.Use();
				}
				break;
			case EventType.Repaint:
			{
				GUIStyle guistyle = null;
				GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
				position = new Rect(0f, 0f, position.width, position.height);
				Rect[] array = GUI.CalcMouseRects(position, num, xCount, num5, num6, style, firstStyle, midStyle, lastStyle, false);
				int buttonGridMouseSelection2 = GUI.GetButtonGridMouseSelection(array, Event.current.mousePosition, controlID == GUIUtility.hotControl);
				bool flag = position.Contains(Event.current.mousePosition);
				GUIUtility.mouseUsed = GUIUtility.mouseUsed || flag;
				for (int i = 0; i < num; i++)
				{
					GUIStyle guistyle2;
					if (i != 0)
					{
						guistyle2 = midStyle;
					}
					else
					{
						guistyle2 = firstStyle;
					}
					if (i == num - 1)
					{
						guistyle2 = lastStyle;
					}
					if (num == 1)
					{
						guistyle2 = style;
					}
					if (i != selected)
					{
						guistyle2.Draw(array[i], contents[i], i == buttonGridMouseSelection2 && (GUI.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl && GUI.enabled, false, false);
					}
					else
					{
						guistyle = guistyle2;
					}
				}
				if (selected < num && selected > -1)
				{
					guistyle.Draw(array[selected], contents[selected], selected == buttonGridMouseSelection2 && (GUI.enabled || controlID == GUIUtility.hotControl) && (controlID == GUIUtility.hotControl || GUIUtility.hotControl == 0), controlID == GUIUtility.hotControl, true, false);
				}
				if (buttonGridMouseSelection2 >= 0)
				{
					GUI.tooltip = contents[buttonGridMouseSelection2].tooltip;
				}
				GUIClip.Pop();
				break;
			}
			}
			return selected;
		}

		// Token: 0x06000752 RID: 1874 RVA: 0x0000E790 File Offset: 0x0000C990
		private static Rect[] CalcMouseRects(Rect position, int count, int xCount, float elemWidth, float elemHeight, GUIStyle style, GUIStyle firstStyle, GUIStyle midStyle, GUIStyle lastStyle, bool addBorders)
		{
			int num = 0;
			int num2 = 0;
			float num3 = position.xMin;
			float num4 = position.yMin;
			GUIStyle guistyle = style;
			Rect[] array = new Rect[count];
			if (count > 1)
			{
				guistyle = firstStyle;
			}
			for (int i = 0; i < count; i++)
			{
				if (!addBorders)
				{
					array[i] = new Rect(num3, num4, elemWidth, elemHeight);
				}
				else
				{
					array[i] = guistyle.margin.Add(new Rect(num3, num4, elemWidth, elemHeight));
				}
				array[i].width = Mathf.Round(array[i].xMax) - Mathf.Round(array[i].x);
				array[i].x = Mathf.Round(array[i].x);
				GUIStyle guistyle2 = midStyle;
				if (i == count - 2)
				{
					guistyle2 = lastStyle;
				}
				num3 += elemWidth + (float)Mathf.Max(guistyle.margin.right, guistyle2.margin.left);
				num2++;
				if (num2 >= xCount)
				{
					num++;
					num2 = 0;
					num4 += elemHeight + (float)Mathf.Max(style.margin.top, style.margin.bottom);
					num3 = position.xMin;
				}
			}
			return array;
		}

		// Token: 0x06000753 RID: 1875 RVA: 0x0000E8F0 File Offset: 0x0000CAF0
		private static int GetButtonGridMouseSelection(Rect[] buttonRects, Vector2 mousePos, bool findNearest)
		{
			for (int i = 0; i < buttonRects.Length; i++)
			{
				if (buttonRects[i].Contains(mousePos))
				{
					return i;
				}
			}
			if (!findNearest)
			{
				return -1;
			}
			float num = 10000000f;
			int num2 = -1;
			for (int j = 0; j < buttonRects.Length; j++)
			{
				Rect rect = buttonRects[j];
				Vector2 vector = new Vector2(Mathf.Clamp(mousePos.x, rect.xMin, rect.xMax), Mathf.Clamp(mousePos.y, rect.yMin, rect.yMax));
				float sqrMagnitude = (mousePos - vector).sqrMagnitude;
				if (sqrMagnitude < num)
				{
					num2 = j;
					num = sqrMagnitude;
				}
			}
			return num2;
		}

		// Token: 0x06000754 RID: 1876 RVA: 0x0000E9B4 File Offset: 0x0000CBB4
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, true, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000E9F0 File Offset: 0x0000CBF0
		public static float HorizontalSlider(Rect position, float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, leftValue, rightValue, slider, thumb, true, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000EA1C File Offset: 0x0000CC1C
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, false, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000EA58 File Offset: 0x0000CC58
		public static float VerticalSlider(Rect position, float value, float topValue, float bottomValue, GUIStyle slider, GUIStyle thumb)
		{
			return GUI.Slider(position, value, 0f, topValue, bottomValue, slider, thumb, false, GUIUtility.GetControlID(GUI.sliderHash, FocusType.Native, position));
		}

		// Token: 0x06000758 RID: 1880 RVA: 0x0000EA84 File Offset: 0x0000CC84
		public static float Slider(Rect position, float value, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id)
		{
			GUIUtility.CheckOnGUI();
			SliderHandler sliderHandler = new SliderHandler(position, value, size, start, end, slider, thumb, horiz, id);
			return sliderHandler.Handle();
		}

		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000759 RID: 1881
		internal static extern bool usePageScrollbars
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600075A RID: 1882 RVA: 0x0000EAB4 File Offset: 0x0000CCB4
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, GUI.skin.horizontalScrollbarThumb, GUI.skin.horizontalScrollbarLeftButton, GUI.skin.horizontalScrollbarRightButton, true);
		}

		// Token: 0x0600075B RID: 1883 RVA: 0x0000EAF8 File Offset: 0x0000CCF8
		public static float HorizontalScrollbar(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, leftValue, rightValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "leftbutton"), GUI.skin.GetStyle(style.name + "rightbutton"), true);
		}

		// Token: 0x0600075C RID: 1884
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void InternalRepaintEditorWindow();

		// Token: 0x0600075D RID: 1885 RVA: 0x0000EB64 File Offset: 0x0000CD64
		internal static bool ScrollerRepeatButton(int scrollerID, Rect rect, GUIStyle style)
		{
			bool flag = false;
			if (GUI.DoRepeatButton(rect, GUIContent.none, style, FocusType.Passive))
			{
				bool flag2 = GUI.scrollControlID != scrollerID;
				GUI.scrollControlID = scrollerID;
				if (flag2)
				{
					flag = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(250.0);
				}
				else if (DateTime.Now >= GUI.nextScrollStepTime)
				{
					flag = true;
					GUI.nextScrollStepTime = DateTime.Now.AddMilliseconds(30.0);
				}
				if (Event.current.type == EventType.Repaint)
				{
					GUI.InternalRepaintEditorWindow();
				}
			}
			return flag;
		}

		// Token: 0x0600075E RID: 1886 RVA: 0x0000EC08 File Offset: 0x0000CE08
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, GUI.skin.verticalScrollbarThumb, GUI.skin.verticalScrollbarUpButton, GUI.skin.verticalScrollbarDownButton, false);
		}

		// Token: 0x0600075F RID: 1887 RVA: 0x0000EC4C File Offset: 0x0000CE4C
		public static float VerticalScrollbar(Rect position, float value, float size, float topValue, float bottomValue, GUIStyle style)
		{
			return GUI.Scroller(position, value, size, topValue, bottomValue, style, GUI.skin.GetStyle(style.name + "thumb"), GUI.skin.GetStyle(style.name + "upbutton"), GUI.skin.GetStyle(style.name + "downbutton"), false);
		}

		// Token: 0x06000760 RID: 1888 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		private static float Scroller(Rect position, float value, float size, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUIStyle leftButton, GUIStyle rightButton, bool horiz)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive, position);
			Rect rect;
			Rect rect2;
			Rect rect3;
			if (horiz)
			{
				rect = new Rect(position.x + leftButton.fixedWidth, position.y, position.width - leftButton.fixedWidth - rightButton.fixedWidth, position.height);
				rect2 = new Rect(position.x, position.y, leftButton.fixedWidth, position.height);
				rect3 = new Rect(position.xMax - rightButton.fixedWidth, position.y, rightButton.fixedWidth, position.height);
			}
			else
			{
				rect = new Rect(position.x, position.y + leftButton.fixedHeight, position.width, position.height - leftButton.fixedHeight - rightButton.fixedHeight);
				rect2 = new Rect(position.x, position.y, position.width, leftButton.fixedHeight);
				rect3 = new Rect(position.x, position.yMax - rightButton.fixedHeight, position.width, rightButton.fixedHeight);
			}
			value = GUI.Slider(rect, value, size, leftValue, rightValue, slider, thumb, horiz, controlID);
			bool flag = false;
			if (Event.current.type == EventType.MouseUp)
			{
				flag = true;
			}
			if (GUI.ScrollerRepeatButton(controlID, rect2, leftButton))
			{
				value -= GUI.scrollStepSize * ((leftValue >= rightValue) ? (-1f) : 1f);
			}
			if (GUI.ScrollerRepeatButton(controlID, rect3, rightButton))
			{
				value += GUI.scrollStepSize * ((leftValue >= rightValue) ? (-1f) : 1f);
			}
			if (flag && Event.current.type == EventType.Used)
			{
				GUI.scrollControlID = 0;
			}
			if (leftValue < rightValue)
			{
				value = Mathf.Clamp(value, leftValue, rightValue - size);
			}
			else
			{
				value = Mathf.Clamp(value, rightValue, leftValue - size);
			}
			return value;
		}

		// Token: 0x06000761 RID: 1889 RVA: 0x0000EEC4 File Offset: 0x0000D0C4
		public static void BeginGroup(Rect position)
		{
			GUI.BeginGroup(position, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x06000762 RID: 1890 RVA: 0x0000EED8 File Offset: 0x0000D0D8
		public static void BeginGroup(Rect position, string text)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), GUIStyle.none);
		}

		// Token: 0x06000763 RID: 1891 RVA: 0x0000EEEC File Offset: 0x0000D0EC
		public static void BeginGroup(Rect position, Texture image)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), GUIStyle.none);
		}

		// Token: 0x06000764 RID: 1892 RVA: 0x0000EF00 File Offset: 0x0000D100
		public static void BeginGroup(Rect position, GUIContent content)
		{
			GUI.BeginGroup(position, content, GUIStyle.none);
		}

		// Token: 0x06000765 RID: 1893 RVA: 0x0000EF10 File Offset: 0x0000D110
		public static void BeginGroup(Rect position, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.none, style);
		}

		// Token: 0x06000766 RID: 1894 RVA: 0x0000EF20 File Offset: 0x0000D120
		public static void BeginGroup(Rect position, string text, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(text), style);
		}

		// Token: 0x06000767 RID: 1895 RVA: 0x0000EF30 File Offset: 0x0000D130
		public static void BeginGroup(Rect position, Texture image, GUIStyle style)
		{
			GUI.BeginGroup(position, GUIContent.Temp(image), style);
		}

		// Token: 0x06000768 RID: 1896 RVA: 0x0000EF40 File Offset: 0x0000D140
		public static void BeginGroup(Rect position, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.beginGroupHash, FocusType.Passive);
			if (content != GUIContent.none || style != GUIStyle.none)
			{
				EventType type = Event.current.type;
				if (type != EventType.Repaint)
				{
					if (position.Contains(Event.current.mousePosition))
					{
						GUIUtility.mouseUsed = true;
					}
				}
				else
				{
					style.Draw(position, content, controlID);
				}
			}
			GUIClip.Push(position, Vector2.zero, Vector2.zero, false);
		}

		// Token: 0x06000769 RID: 1897 RVA: 0x0000EFCC File Offset: 0x0000D1CC
		public static void EndGroup()
		{
			GUIClip.Pop();
		}

		// Token: 0x0600076A RID: 1898 RVA: 0x0000EFD4 File Offset: 0x0000D1D4
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x0600076B RID: 1899 RVA: 0x0000F00C File Offset: 0x0000D20C
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x0600076C RID: 1900 RVA: 0x0000F044 File Offset: 0x0000D244
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView);
		}

		// Token: 0x0600076D RID: 1901 RVA: 0x0000F068 File Offset: 0x0000D268
		public static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, null);
		}

		// Token: 0x0600076E RID: 1902 RVA: 0x0000F088 File Offset: 0x0000D288
		protected static Vector2 DoBeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			return GUI.BeginScrollView(position, scrollPosition, viewRect, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		// Token: 0x0600076F RID: 1903 RVA: 0x0000F0A8 File Offset: 0x0000D2A8
		internal static Vector2 BeginScrollView(Rect position, Vector2 scrollPosition, Rect viewRect, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background)
		{
			GUIUtility.CheckOnGUI();
			int controlID = GUIUtility.GetControlID(GUI.scrollviewHash, FocusType.Passive);
			GUI.ScrollViewState scrollViewState = (GUI.ScrollViewState)GUIUtility.GetStateObject(typeof(GUI.ScrollViewState), controlID);
			if (scrollViewState.apply)
			{
				scrollPosition = scrollViewState.scrollPosition;
				scrollViewState.apply = false;
			}
			scrollViewState.position = position;
			scrollViewState.scrollPosition = scrollPosition;
			scrollViewState.visibleRect = (scrollViewState.viewRect = viewRect);
			scrollViewState.visibleRect.width = position.width;
			scrollViewState.visibleRect.height = position.height;
			GUI.s_ScrollViewStates.Push(scrollViewState);
			Rect rect = new Rect(position);
			EventType type = Event.current.type;
			if (type != EventType.Layout)
			{
				if (type != EventType.Used)
				{
					bool flag = alwaysShowVertical;
					bool flag2 = alwaysShowHorizontal;
					if (flag2 || viewRect.width > rect.width)
					{
						scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						rect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
						flag2 = true;
					}
					if (flag || viewRect.height > rect.height)
					{
						scrollViewState.visibleRect.width = position.width - verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						rect.width -= verticalScrollbar.fixedWidth + (float)verticalScrollbar.margin.left;
						flag = true;
						if (!flag2 && viewRect.width > rect.width)
						{
							scrollViewState.visibleRect.height = position.height - horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							rect.height -= horizontalScrollbar.fixedHeight + (float)horizontalScrollbar.margin.top;
							flag2 = true;
						}
					}
					if (Event.current.type == EventType.Repaint && background != GUIStyle.none)
					{
						background.Draw(position, position.Contains(Event.current.mousePosition), false, flag2 && flag, false);
					}
					if (flag2 && horizontalScrollbar != GUIStyle.none)
					{
						scrollPosition.x = GUI.HorizontalScrollbar(new Rect(position.x, position.yMax - horizontalScrollbar.fixedHeight, rect.width, horizontalScrollbar.fixedHeight), scrollPosition.x, rect.width, 0f, viewRect.width, horizontalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						if (horizontalScrollbar != GUIStyle.none)
						{
							scrollPosition.x = 0f;
						}
						else
						{
							scrollPosition.x = Mathf.Clamp(scrollPosition.x, 0f, Mathf.Max(viewRect.width - position.width, 0f));
						}
					}
					if (flag && verticalScrollbar != GUIStyle.none)
					{
						scrollPosition.y = GUI.VerticalScrollbar(new Rect(rect.xMax + (float)verticalScrollbar.margin.left, rect.y, verticalScrollbar.fixedWidth, rect.height), scrollPosition.y, rect.height, 0f, viewRect.height, verticalScrollbar);
					}
					else
					{
						GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
						if (verticalScrollbar != GUIStyle.none)
						{
							scrollPosition.y = 0f;
						}
						else
						{
							scrollPosition.y = Mathf.Clamp(scrollPosition.y, 0f, Mathf.Max(viewRect.height - position.height, 0f));
						}
					}
				}
			}
			else
			{
				GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.sliderHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
				GUIUtility.GetControlID(GUI.repeatButtonHash, FocusType.Passive);
			}
			GUIClip.Push(rect, new Vector2(Mathf.Round(-scrollPosition.x - viewRect.x), Mathf.Round(-scrollPosition.y - viewRect.y)), Vector2.zero, false);
			return scrollPosition;
		}

		// Token: 0x06000770 RID: 1904 RVA: 0x0000F53C File Offset: 0x0000D73C
		public static void EndScrollView()
		{
			GUI.EndScrollView(true);
		}

		// Token: 0x06000771 RID: 1905 RVA: 0x0000F544 File Offset: 0x0000D744
		public static void EndScrollView(bool handleScrollWheel)
		{
			GUI.ScrollViewState scrollViewState = (GUI.ScrollViewState)GUI.s_ScrollViewStates.Peek();
			GUIUtility.CheckOnGUI();
			GUIClip.Pop();
			GUI.s_ScrollViewStates.Pop();
			if (handleScrollWheel && Event.current.type == EventType.ScrollWheel && scrollViewState.position.Contains(Event.current.mousePosition))
			{
				scrollViewState.scrollPosition.x = Mathf.Clamp(scrollViewState.scrollPosition.x + Event.current.delta.x * 20f, 0f, scrollViewState.viewRect.width - scrollViewState.visibleRect.width);
				scrollViewState.scrollPosition.y = Mathf.Clamp(scrollViewState.scrollPosition.y + Event.current.delta.y * 20f, 0f, scrollViewState.viewRect.height - scrollViewState.visibleRect.height);
				scrollViewState.apply = true;
				Event.current.Use();
			}
		}

		// Token: 0x06000772 RID: 1906 RVA: 0x0000F658 File Offset: 0x0000D858
		internal static GUI.ScrollViewState GetTopScrollView()
		{
			if (GUI.s_ScrollViewStates.Count != 0)
			{
				return (GUI.ScrollViewState)GUI.s_ScrollViewStates.Peek();
			}
			return null;
		}

		// Token: 0x06000773 RID: 1907 RVA: 0x0000F688 File Offset: 0x0000D888
		public static void ScrollTo(Rect position)
		{
			GUI.ScrollViewState topScrollView = GUI.GetTopScrollView();
			if (topScrollView != null)
			{
				topScrollView.ScrollTo(position);
			}
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x0000F6A8 File Offset: 0x0000D8A8
		public static bool ScrollTowards(Rect position, float maxDelta)
		{
			GUI.ScrollViewState topScrollView = GUI.GetTopScrollView();
			return topScrollView != null && topScrollView.ScrollTowards(position, maxDelta);
		}

		// Token: 0x06000775 RID: 1909 RVA: 0x0000F6CC File Offset: 0x0000D8CC
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06000776 RID: 1910 RVA: 0x0000F6F8 File Offset: 0x0000D8F8
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06000777 RID: 1911 RVA: 0x0000F724 File Offset: 0x0000D924
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			return GUI.DoWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin, true);
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x0000F74C File Offset: 0x0000D94C
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin, true);
		}

		// Token: 0x06000779 RID: 1913 RVA: 0x0000F770 File Offset: 0x0000D970
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			return GUI.DoWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin, true);
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000F794 File Offset: 0x0000D994
		public static Rect Window(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style)
		{
			return GUI.DoWindow(id, clientRect, func, title, style, GUI.skin, true);
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000F7B4 File Offset: 0x0000D9B4
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), GUI.skin.window, GUI.skin);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000F7E0 File Offset: 0x0000D9E0
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), GUI.skin.window, GUI.skin);
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000F80C File Offset: 0x0000DA0C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content)
		{
			return GUI.DoModalWindow(id, clientRect, func, content, GUI.skin.window, GUI.skin);
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000F834 File Offset: 0x0000DA34
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, string text, GUIStyle style)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(text), style, GUI.skin);
		}

		// Token: 0x0600077F RID: 1919 RVA: 0x0000F858 File Offset: 0x0000DA58
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, Texture image, GUIStyle style)
		{
			return GUI.DoModalWindow(id, clientRect, func, GUIContent.Temp(image), style, GUI.skin);
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000F87C File Offset: 0x0000DA7C
		public static Rect ModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style)
		{
			return GUI.DoModalWindow(id, clientRect, func, content, style, GUI.skin);
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000F890 File Offset: 0x0000DA90
		private static Rect DoModalWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin)
		{
			return GUI.INTERNAL_CALL_DoModalWindow(id, ref clientRect, func, content, style, skin);
		}

		// Token: 0x06000782 RID: 1922
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Rect INTERNAL_CALL_DoModalWindow(int id, ref Rect clientRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUISkin skin);

		// Token: 0x06000783 RID: 1923 RVA: 0x0000F8A0 File Offset: 0x0000DAA0
		internal static void CallWindowDelegate(GUI.WindowFunction func, int id, GUISkin _skin, int forceRect, float width, float height, GUIStyle style)
		{
			GUILayoutUtility.SelectIDList(id, true);
			GUISkin skin = GUI.skin;
			if (Event.current.type == EventType.Layout)
			{
				if (forceRect != 0)
				{
					GUILayoutOption[] array = new GUILayoutOption[]
					{
						GUILayout.Width(width),
						GUILayout.Height(height)
					};
					GUILayoutUtility.BeginWindow(id, style, array);
				}
				else
				{
					GUILayoutUtility.BeginWindow(id, style, null);
				}
			}
			GUI.skin = _skin;
			func(id);
			if (Event.current.type == EventType.Layout)
			{
				GUILayoutUtility.Layout();
			}
			GUI.skin = skin;
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000F92C File Offset: 0x0000DB2C
		private static Rect DoWindow(int id, Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout)
		{
			return GUI.INTERNAL_CALL_DoWindow(id, ref clientRect, func, title, style, skin, forceRectOnLayout);
		}

		// Token: 0x06000785 RID: 1925
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Rect INTERNAL_CALL_DoWindow(int id, ref Rect clientRect, GUI.WindowFunction func, GUIContent title, GUIStyle style, GUISkin skin, bool forceRectOnLayout);

		// Token: 0x06000786 RID: 1926 RVA: 0x0000F940 File Offset: 0x0000DB40
		public static void DragWindow(Rect position)
		{
			GUI.INTERNAL_CALL_DragWindow(ref position);
		}

		// Token: 0x06000787 RID: 1927
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_DragWindow(ref Rect position);

		// Token: 0x06000788 RID: 1928 RVA: 0x0000F94C File Offset: 0x0000DB4C
		public static void DragWindow()
		{
			GUI.DragWindow(new Rect(0f, 0f, 10000f, 10000f));
		}

		// Token: 0x06000789 RID: 1929
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BringWindowToFront(int windowID);

		// Token: 0x0600078A RID: 1930
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void BringWindowToBack(int windowID);

		// Token: 0x0600078B RID: 1931
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FocusWindow(int windowID);

		// Token: 0x0600078C RID: 1932
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UnfocusWindow();

		// Token: 0x0600078D RID: 1933 RVA: 0x0000F96C File Offset: 0x0000DB6C
		internal static void BeginWindows(int skinMode, int editorWindowInstanceID)
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			Matrix4x4 matrix = GUI.matrix;
			GUI.Internal_BeginWindows();
			GUI.matrix = matrix;
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x0600078E RID: 1934
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_BeginWindows();

		// Token: 0x0600078F RID: 1935 RVA: 0x0000F9CC File Offset: 0x0000DBCC
		internal static void EndWindows()
		{
			GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
			GenericStack layoutGroups = GUILayoutUtility.current.layoutGroups;
			GUILayoutGroup windows = GUILayoutUtility.current.windows;
			GUI.Internal_EndWindows();
			GUILayoutUtility.current.topLevel = topLevel;
			GUILayoutUtility.current.layoutGroups = layoutGroups;
			GUILayoutUtility.current.windows = windows;
		}

		// Token: 0x06000790 RID: 1936
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_EndWindows();

		// Token: 0x040002ED RID: 749
		private static float scrollStepSize = 10f;

		// Token: 0x040002EE RID: 750
		private static int scrollControlID;

		// Token: 0x040002EF RID: 751
		private static GUISkin s_Skin;

		// Token: 0x040002F0 RID: 752
		internal static Rect s_ToolTipRect;

		// Token: 0x040002F1 RID: 753
		private static int boxHash = "Box".GetHashCode();

		// Token: 0x040002F2 RID: 754
		private static int repeatButtonHash = "repeatButton".GetHashCode();

		// Token: 0x040002F3 RID: 755
		private static int toggleHash = "Toggle".GetHashCode();

		// Token: 0x040002F4 RID: 756
		private static int buttonGridHash = "ButtonGrid".GetHashCode();

		// Token: 0x040002F5 RID: 757
		private static int sliderHash = "Slider".GetHashCode();

		// Token: 0x040002F6 RID: 758
		private static int beginGroupHash = "BeginGroup".GetHashCode();

		// Token: 0x040002F7 RID: 759
		private static int scrollviewHash = "scrollView".GetHashCode();

		// Token: 0x040002F8 RID: 760
		private static GenericStack s_ScrollViewStates = new GenericStack();

		// Token: 0x020000EB RID: 235
		internal sealed class ScrollViewState
		{
			// Token: 0x06000792 RID: 1938 RVA: 0x0000FA28 File Offset: 0x0000DC28
			internal void ScrollTo(Rect position)
			{
				this.ScrollTowards(position, float.PositiveInfinity);
			}

			// Token: 0x06000793 RID: 1939 RVA: 0x0000FA38 File Offset: 0x0000DC38
			internal bool ScrollTowards(Rect position, float maxDelta)
			{
				Vector2 vector = this.ScrollNeeded(position);
				if (vector.sqrMagnitude < 0.0001f)
				{
					return false;
				}
				if (maxDelta == 0f)
				{
					return true;
				}
				if (vector.magnitude > maxDelta)
				{
					vector = vector.normalized * maxDelta;
				}
				this.scrollPosition += vector;
				this.apply = true;
				return true;
			}

			// Token: 0x06000794 RID: 1940 RVA: 0x0000FAA4 File Offset: 0x0000DCA4
			internal Vector2 ScrollNeeded(Rect position)
			{
				Rect rect = this.visibleRect;
				rect.x += this.scrollPosition.x;
				rect.y += this.scrollPosition.y;
				float num = position.width - this.visibleRect.width;
				if (num > 0f)
				{
					position.width -= num;
					position.x += num * 0.5f;
				}
				num = position.height - this.visibleRect.height;
				if (num > 0f)
				{
					position.height -= num;
					position.y += num * 0.5f;
				}
				Vector2 zero = Vector2.zero;
				if (position.xMax > rect.xMax)
				{
					zero.x += position.xMax - rect.xMax;
				}
				else if (position.xMin < rect.xMin)
				{
					zero.x -= rect.xMin - position.xMin;
				}
				if (position.yMax > rect.yMax)
				{
					zero.y += position.yMax - rect.yMax;
				}
				else if (position.yMin < rect.yMin)
				{
					zero.y -= rect.yMin - position.yMin;
				}
				Rect rect2 = this.viewRect;
				rect2.width = Mathf.Max(rect2.width, this.visibleRect.width);
				rect2.height = Mathf.Max(rect2.height, this.visibleRect.height);
				zero.x = Mathf.Clamp(zero.x, rect2.xMin - this.scrollPosition.x, rect2.xMax - this.visibleRect.width - this.scrollPosition.x);
				zero.y = Mathf.Clamp(zero.y, rect2.yMin - this.scrollPosition.y, rect2.yMax - this.visibleRect.height - this.scrollPosition.y);
				return zero;
			}

			// Token: 0x040002FB RID: 763
			public Rect position;

			// Token: 0x040002FC RID: 764
			public Rect visibleRect;

			// Token: 0x040002FD RID: 765
			public Rect viewRect;

			// Token: 0x040002FE RID: 766
			public Vector2 scrollPosition;

			// Token: 0x040002FF RID: 767
			public bool apply;

			// Token: 0x04000300 RID: 768
			public bool hasScrollTo;
		}

		// Token: 0x02000229 RID: 553
		// (Invoke) Token: 0x06001AC9 RID: 6857
		public delegate void WindowFunction(int id);
	}
}
