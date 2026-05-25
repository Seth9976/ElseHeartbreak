using System;

namespace UnityEngine
{
	// Token: 0x020000EC RID: 236
	public sealed class GUILayout
	{
		// Token: 0x06000796 RID: 1942 RVA: 0x0000FD18 File Offset: 0x0000DF18
		public static void Label(Texture image, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(image), GUI.skin.label, options);
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000FD30 File Offset: 0x0000DF30
		public static void Label(string text, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), GUI.skin.label, options);
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000FD48 File Offset: 0x0000DF48
		public static void Label(GUIContent content, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(content, GUI.skin.label, options);
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x0000FD5C File Offset: 0x0000DF5C
		public static void Label(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(image), style, options);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000FD6C File Offset: 0x0000DF6C
		public static void Label(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(GUIContent.Temp(text), style, options);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000FD7C File Offset: 0x0000DF7C
		public static void Label(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoLabel(content, style, options);
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000FD88 File Offset: 0x0000DF88
		private static void DoLabel(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Label(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000FD9C File Offset: 0x0000DF9C
		public static void Box(Texture image, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(image), GUI.skin.box, options);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000FDB4 File Offset: 0x0000DFB4
		public static void Box(string text, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(text), GUI.skin.box, options);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000FDCC File Offset: 0x0000DFCC
		public static void Box(GUIContent content, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(content, GUI.skin.box, options);
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000FDE0 File Offset: 0x0000DFE0
		public static void Box(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x0000FDF0 File Offset: 0x0000DFF0
		public static void Box(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x0000FE00 File Offset: 0x0000E000
		public static void Box(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.DoBox(content, style, options);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x0000FE0C File Offset: 0x0000E00C
		private static void DoBox(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUI.Box(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x0000FE20 File Offset: 0x0000E020
		public static bool Button(Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(image), GUI.skin.button, options);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x0000FE38 File Offset: 0x0000E038
		public static bool Button(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x0000FE50 File Offset: 0x0000E050
		public static bool Button(GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(content, GUI.skin.button, options);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000FE64 File Offset: 0x0000E064
		public static bool Button(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x0000FE74 File Offset: 0x0000E074
		public static bool Button(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x0000FE84 File Offset: 0x0000E084
		public static bool Button(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoButton(content, style, options);
		}

		// Token: 0x060007AA RID: 1962 RVA: 0x0000FE90 File Offset: 0x0000E090
		private static bool DoButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Button(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060007AB RID: 1963 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		public static bool RepeatButton(Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(image), GUI.skin.button, options);
		}

		// Token: 0x060007AC RID: 1964 RVA: 0x0000FEBC File Offset: 0x0000E0BC
		public static bool RepeatButton(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(text), GUI.skin.button, options);
		}

		// Token: 0x060007AD RID: 1965 RVA: 0x0000FED4 File Offset: 0x0000E0D4
		public static bool RepeatButton(GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(content, GUI.skin.button, options);
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0000FEE8 File Offset: 0x0000E0E8
		public static bool RepeatButton(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0000FEF8 File Offset: 0x0000E0F8
		public static bool RepeatButton(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060007B0 RID: 1968 RVA: 0x0000FF08 File Offset: 0x0000E108
		public static bool RepeatButton(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoRepeatButton(content, style, options);
		}

		// Token: 0x060007B1 RID: 1969 RVA: 0x0000FF14 File Offset: 0x0000E114
		private static bool DoRepeatButton(GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.RepeatButton(GUILayoutUtility.GetRect(content, style, options), content, style);
		}

		// Token: 0x060007B2 RID: 1970 RVA: 0x0000FF28 File Offset: 0x0000E128
		public static string TextField(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, false, GUI.skin.textField, options);
		}

		// Token: 0x060007B3 RID: 1971 RVA: 0x0000FF48 File Offset: 0x0000E148
		public static string TextField(string text, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, false, GUI.skin.textField, options);
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0000FF68 File Offset: 0x0000E168
		public static string TextField(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, false, style, options);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0000FF74 File Offset: 0x0000E174
		public static string TextField(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, true, style, options);
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0000FF80 File Offset: 0x0000E180
		public static string PasswordField(string password, char maskChar, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, -1, GUI.skin.textField, options);
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0000FFA0 File Offset: 0x0000E1A0
		public static string PasswordField(string password, char maskChar, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, maxLength, GUI.skin.textField, options);
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		public static string PasswordField(string password, char maskChar, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.PasswordField(password, maskChar, -1, style, options);
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0000FFCC File Offset: 0x0000E1CC
		public static string PasswordField(string password, char maskChar, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			GUIContent guicontent = GUIContent.Temp(GUI.PasswordFieldGetStrToShow(password, maskChar));
			return GUI.PasswordField(GUILayoutUtility.GetRect(guicontent, GUI.skin.textField, options), password, maskChar, maxLength, style);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x00010004 File Offset: 0x0000E204
		public static string TextArea(string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, true, GUI.skin.textArea, options);
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x00010024 File Offset: 0x0000E224
		public static string TextArea(string text, int maxLength, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, true, GUI.skin.textArea, options);
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x00010044 File Offset: 0x0000E244
		public static string TextArea(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, -1, true, style, options);
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x00010050 File Offset: 0x0000E250
		public static string TextArea(string text, int maxLength, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoTextField(text, maxLength, true, style, options);
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001005C File Offset: 0x0000E25C
		private static string DoTextField(string text, int maxLength, bool multiline, GUIStyle style, GUILayoutOption[] options)
		{
			int controlID = GUIUtility.GetControlID(FocusType.Keyboard);
			GUIContent guicontent = GUIContent.Temp(text);
			if (GUIUtility.keyboardControl != controlID)
			{
				guicontent = GUIContent.Temp(text);
			}
			else
			{
				guicontent = GUIContent.Temp(text + Input.compositionString);
			}
			Rect rect = GUILayoutUtility.GetRect(guicontent, style, options);
			if (GUIUtility.keyboardControl == controlID)
			{
				guicontent = GUIContent.Temp(text);
			}
			GUI.DoTextField(rect, controlID, guicontent, multiline, maxLength, style);
			return guicontent.text;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x000100CC File Offset: 0x0000E2CC
		public static bool Toggle(bool value, Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(image), GUI.skin.toggle, options);
		}

		// Token: 0x060007C0 RID: 1984 RVA: 0x000100F0 File Offset: 0x0000E2F0
		public static bool Toggle(bool value, string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(text), GUI.skin.toggle, options);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x00010114 File Offset: 0x0000E314
		public static bool Toggle(bool value, GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, content, GUI.skin.toggle, options);
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x00010128 File Offset: 0x0000E328
		public static bool Toggle(bool value, Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(image), style, options);
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x00010138 File Offset: 0x0000E338
		public static bool Toggle(bool value, string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, GUIContent.Temp(text), style, options);
		}

		// Token: 0x060007C4 RID: 1988 RVA: 0x00010148 File Offset: 0x0000E348
		public static bool Toggle(bool value, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoToggle(value, content, style, options);
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00010154 File Offset: 0x0000E354
		private static bool DoToggle(bool value, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			return GUI.Toggle(GUILayoutUtility.GetRect(content, style, options), value, content, style);
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00010168 File Offset: 0x0000E368
		public static int Toolbar(int selected, string[] texts, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(texts), GUI.skin.button, options);
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x0001018C File Offset: 0x0000E38C
		public static int Toolbar(int selected, Texture[] images, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(images), GUI.skin.button, options);
		}

		// Token: 0x060007C8 RID: 1992 RVA: 0x000101B0 File Offset: 0x0000E3B0
		public static int Toolbar(int selected, GUIContent[] content, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, content, GUI.skin.button, options);
		}

		// Token: 0x060007C9 RID: 1993 RVA: 0x000101C4 File Offset: 0x0000E3C4
		public static int Toolbar(int selected, string[] texts, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(texts), style, options);
		}

		// Token: 0x060007CA RID: 1994 RVA: 0x000101D4 File Offset: 0x0000E3D4
		public static int Toolbar(int selected, Texture[] images, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.Toolbar(selected, GUIContent.Temp(images), style, options);
		}

		// Token: 0x060007CB RID: 1995 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public static int Toolbar(int selected, GUIContent[] contents, GUIStyle style, params GUILayoutOption[] options)
		{
			GUIStyle guistyle;
			GUIStyle guistyle2;
			GUIStyle guistyle3;
			GUI.FindStyles(ref style, out guistyle, out guistyle2, out guistyle3, "left", "mid", "right");
			Vector2 vector = default(Vector2);
			int num = contents.Length;
			GUIStyle guistyle4 = ((num <= 1) ? style : guistyle);
			GUIStyle guistyle5 = ((num <= 1) ? style : guistyle2);
			GUIStyle guistyle6 = ((num <= 1) ? style : guistyle3);
			int num2 = guistyle4.margin.left;
			for (int i = 0; i < contents.Length; i++)
			{
				if (i == num - 2)
				{
					guistyle4 = guistyle5;
					guistyle5 = guistyle6;
				}
				if (i == num - 1)
				{
					guistyle4 = guistyle6;
				}
				Vector2 vector2 = guistyle4.CalcSize(contents[i]);
				if (vector2.x > vector.x)
				{
					vector.x = vector2.x;
				}
				if (vector2.y > vector.y)
				{
					vector.y = vector2.y;
				}
				if (i == num - 1)
				{
					num2 += guistyle4.margin.right;
				}
				else
				{
					num2 += Mathf.Max(guistyle4.margin.right, guistyle5.margin.left);
				}
			}
			vector.x = vector.x * (float)contents.Length + (float)num2;
			return GUI.Toolbar(GUILayoutUtility.GetRect(vector.x, vector.y, style, options), selected, contents, style);
		}

		// Token: 0x060007CC RID: 1996 RVA: 0x0001035C File Offset: 0x0000E55C
		public static int SelectionGrid(int selected, string[] texts, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(texts), xCount, GUI.skin.button, options);
		}

		// Token: 0x060007CD RID: 1997 RVA: 0x00010384 File Offset: 0x0000E584
		public static int SelectionGrid(int selected, Texture[] images, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(images), xCount, GUI.skin.button, options);
		}

		// Token: 0x060007CE RID: 1998 RVA: 0x000103AC File Offset: 0x0000E5AC
		public static int SelectionGrid(int selected, GUIContent[] content, int xCount, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, content, xCount, GUI.skin.button, options);
		}

		// Token: 0x060007CF RID: 1999 RVA: 0x000103CC File Offset: 0x0000E5CC
		public static int SelectionGrid(int selected, string[] texts, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(texts), xCount, style, options);
		}

		// Token: 0x060007D0 RID: 2000 RVA: 0x000103E0 File Offset: 0x0000E5E0
		public static int SelectionGrid(int selected, Texture[] images, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.SelectionGrid(selected, GUIContent.Temp(images), xCount, style, options);
		}

		// Token: 0x060007D1 RID: 2001 RVA: 0x000103F4 File Offset: 0x0000E5F4
		public static int SelectionGrid(int selected, GUIContent[] contents, int xCount, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.SelectionGrid(GUIGridSizer.GetRect(contents, xCount, style, options), selected, contents, xCount, style);
		}

		// Token: 0x060007D2 RID: 2002 RVA: 0x0001040C File Offset: 0x0000E60C
		public static float HorizontalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.DoHorizontalSlider(value, leftValue, rightValue, GUI.skin.horizontalSlider, GUI.skin.horizontalSliderThumb, options);
		}

		// Token: 0x060007D3 RID: 2003 RVA: 0x00010438 File Offset: 0x0000E638
		public static float HorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUILayout.DoHorizontalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x00010448 File Offset: 0x0000E648
		private static float DoHorizontalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, GUILayoutOption[] options)
		{
			return GUI.HorizontalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x00010474 File Offset: 0x0000E674
		public static float VerticalSlider(float value, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.DoVerticalSlider(value, leftValue, rightValue, GUI.skin.verticalSlider, GUI.skin.verticalSliderThumb, options);
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x000104A0 File Offset: 0x0000E6A0
		public static float VerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUILayout.DoVerticalSlider(value, leftValue, rightValue, slider, thumb, options);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x000104B0 File Offset: 0x0000E6B0
		private static float DoVerticalSlider(float value, float leftValue, float rightValue, GUIStyle slider, GUIStyle thumb, params GUILayoutOption[] options)
		{
			return GUI.VerticalSlider(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n\n"), slider, options), value, leftValue, rightValue, slider, thumb);
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x000104DC File Offset: 0x0000E6DC
		public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, params GUILayoutOption[] options)
		{
			return GUILayout.HorizontalScrollbar(value, size, leftValue, rightValue, GUI.skin.horizontalScrollbar, options);
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00010500 File Offset: 0x0000E700
		public static float HorizontalScrollbar(float value, float size, float leftValue, float rightValue, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.HorizontalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("mmmm"), style, options), value, size, leftValue, rightValue, style);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x0001052C File Offset: 0x0000E72C
		public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, params GUILayoutOption[] options)
		{
			return GUILayout.VerticalScrollbar(value, size, topValue, bottomValue, GUI.skin.verticalScrollbar, options);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00010550 File Offset: 0x0000E750
		public static float VerticalScrollbar(float value, float size, float topValue, float bottomValue, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUI.VerticalScrollbar(GUILayoutUtility.GetRect(GUIContent.Temp("\n\n\n\n"), style, options), value, size, topValue, bottomValue, style);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x0001057C File Offset: 0x0000E77C
		public static void Space(float pixels)
		{
			GUIUtility.CheckOnGUI();
			if (GUILayoutUtility.current.topLevel.isVertical)
			{
				GUILayoutUtility.GetRect(0f, pixels, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { GUILayout.Height(pixels) });
			}
			else
			{
				GUILayoutUtility.GetRect(pixels, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { GUILayout.Width(pixels) });
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x000105E8 File Offset: 0x0000E7E8
		public static void FlexibleSpace()
		{
			GUIUtility.CheckOnGUI();
			GUILayoutOption guilayoutOption;
			if (GUILayoutUtility.current.topLevel.isVertical)
			{
				guilayoutOption = GUILayout.ExpandHeight(true);
			}
			else
			{
				guilayoutOption = GUILayout.ExpandWidth(true);
			}
			guilayoutOption.value = 10000;
			GUILayoutUtility.GetRect(0f, 0f, GUILayoutUtility.spaceStyle, new GUILayoutOption[] { guilayoutOption });
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00010650 File Offset: 0x0000E850
		public static void BeginHorizontal(params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x00010664 File Offset: 0x0000E864
		public static void BeginHorizontal(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.none, style, options);
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00010674 File Offset: 0x0000E874
		public static void BeginHorizontal(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060007E1 RID: 2017 RVA: 0x00010684 File Offset: 0x0000E884
		public static void BeginHorizontal(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginHorizontal(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00010694 File Offset: 0x0000E894
		public static void BeginHorizontal(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = false;
			if (style != GUIStyle.none || content != GUIContent.none)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x000106E0 File Offset: 0x0000E8E0
		public static void EndHorizontal()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndHorizontal");
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x000106F4 File Offset: 0x0000E8F4
		public static void BeginVertical(params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, GUIStyle.none, options);
		}

		// Token: 0x060007E5 RID: 2021 RVA: 0x00010708 File Offset: 0x0000E908
		public static void BeginVertical(GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.none, style, options);
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x00010718 File Offset: 0x0000E918
		public static void BeginVertical(string text, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.Temp(text), style, options);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x00010728 File Offset: 0x0000E928
		public static void BeginVertical(Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayout.BeginVertical(GUIContent.Temp(image), style, options);
		}

		// Token: 0x060007E8 RID: 2024 RVA: 0x00010738 File Offset: 0x0000E938
		public static void BeginVertical(GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutGroup(style, options, typeof(GUILayoutGroup));
			guilayoutGroup.isVertical = true;
			if (style != GUIStyle.none)
			{
				GUI.Box(guilayoutGroup.rect, content, style);
			}
		}

		// Token: 0x060007E9 RID: 2025 RVA: 0x00010778 File Offset: 0x0000E978
		public static void EndVertical()
		{
			GUILayoutUtility.EndGroup("GUILayout.EndVertical");
			GUILayoutUtility.EndLayoutGroup();
		}

		// Token: 0x060007EA RID: 2026 RVA: 0x0001078C File Offset: 0x0000E98C
		public static void BeginArea(Rect screenRect)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x060007EB RID: 2027 RVA: 0x000107A0 File Offset: 0x0000E9A0
		public static void BeginArea(Rect screenRect, string text)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(text), GUIStyle.none);
		}

		// Token: 0x060007EC RID: 2028 RVA: 0x000107B4 File Offset: 0x0000E9B4
		public static void BeginArea(Rect screenRect, Texture image)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(image), GUIStyle.none);
		}

		// Token: 0x060007ED RID: 2029 RVA: 0x000107C8 File Offset: 0x0000E9C8
		public static void BeginArea(Rect screenRect, GUIContent content)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, GUIStyle.none);
		}

		// Token: 0x060007EE RID: 2030 RVA: 0x000107DC File Offset: 0x0000E9DC
		public static void BeginArea(Rect screenRect, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.none, style);
		}

		// Token: 0x060007EF RID: 2031 RVA: 0x000107EC File Offset: 0x0000E9EC
		public static void BeginArea(Rect screenRect, string text, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(text), style);
		}

		// Token: 0x060007F0 RID: 2032 RVA: 0x000107FC File Offset: 0x0000E9FC
		public static void BeginArea(Rect screenRect, Texture image, GUIStyle style)
		{
			GUILayout.BeginArea(screenRect, GUIContent.Temp(image), style);
		}

		// Token: 0x060007F1 RID: 2033 RVA: 0x0001080C File Offset: 0x0000EA0C
		public static void BeginArea(Rect screenRect, GUIContent content, GUIStyle style)
		{
			GUIUtility.CheckOnGUI();
			GUILayoutGroup guilayoutGroup = GUILayoutUtility.BeginLayoutArea(style, typeof(GUILayoutGroup));
			if (Event.current.type == EventType.Layout)
			{
				guilayoutGroup.resetCoords = true;
				guilayoutGroup.minWidth = (guilayoutGroup.maxWidth = screenRect.width);
				guilayoutGroup.minHeight = (guilayoutGroup.maxHeight = screenRect.height);
				guilayoutGroup.rect = Rect.MinMaxRect(screenRect.xMin, screenRect.yMin, guilayoutGroup.rect.xMax, guilayoutGroup.rect.yMax);
			}
			GUI.BeginGroup(guilayoutGroup.rect, content, style);
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x000108B0 File Offset: 0x0000EAB0
		public static void EndArea()
		{
			GUIUtility.CheckOnGUI();
			if (Event.current.type == EventType.Used)
			{
				return;
			}
			GUILayoutUtility.current.layoutGroups.Pop();
			GUILayoutUtility.current.topLevel = (GUILayoutGroup)GUILayoutUtility.current.layoutGroups.Peek();
			GUI.EndGroup();
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x00010908 File Offset: 0x0000EB08
		public static Vector2 BeginScrollView(Vector2 scrollPosition, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, false, false, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0001093C File Offset: 0x0000EB3C
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x00010970 File Offset: 0x0000EB70
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, false, false, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00010994 File Offset: 0x0000EB94
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style)
		{
			GUILayoutOption[] array = null;
			return GUILayout.BeginScrollView(scrollPosition, style, array);
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x000109AC File Offset: 0x0000EBAC
		public static Vector2 BeginScrollView(Vector2 scrollPosition, GUIStyle style, params GUILayoutOption[] options)
		{
			string name = style.name;
			GUIStyle guistyle = GUI.skin.FindStyle(name + "VerticalScrollbar");
			if (guistyle == null)
			{
				guistyle = GUI.skin.verticalScrollbar;
			}
			GUIStyle guistyle2 = GUI.skin.FindStyle(name + "HorizontalScrollbar");
			if (guistyle2 == null)
			{
				guistyle2 = GUI.skin.horizontalScrollbar;
			}
			return GUILayout.BeginScrollView(scrollPosition, false, false, guistyle2, guistyle, style, options);
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00010A1C File Offset: 0x0000EC1C
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, params GUILayoutOption[] options)
		{
			return GUILayout.BeginScrollView(scrollPosition, alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, GUI.skin.scrollView, options);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00010A40 File Offset: 0x0000EC40
		public static Vector2 BeginScrollView(Vector2 scrollPosition, bool alwaysShowHorizontal, bool alwaysShowVertical, GUIStyle horizontalScrollbar, GUIStyle verticalScrollbar, GUIStyle background, params GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			GUIScrollGroup guiscrollGroup = (GUIScrollGroup)GUILayoutUtility.BeginLayoutGroup(background, null, typeof(GUIScrollGroup));
			EventType type = Event.current.type;
			if (type == EventType.Layout)
			{
				guiscrollGroup.resetCoords = true;
				guiscrollGroup.isVertical = true;
				guiscrollGroup.stretchWidth = 1;
				guiscrollGroup.stretchHeight = 1;
				guiscrollGroup.verticalScrollbar = verticalScrollbar;
				guiscrollGroup.horizontalScrollbar = horizontalScrollbar;
				guiscrollGroup.needsVerticalScrollbar = alwaysShowVertical;
				guiscrollGroup.needsHorizontalScrollbar = alwaysShowHorizontal;
				guiscrollGroup.ApplyOptions(options);
			}
			return GUI.BeginScrollView(guiscrollGroup.rect, scrollPosition, new Rect(0f, 0f, guiscrollGroup.clientWidth, guiscrollGroup.clientHeight), alwaysShowHorizontal, alwaysShowVertical, horizontalScrollbar, verticalScrollbar, background);
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00010AFC File Offset: 0x0000ECFC
		public static void EndScrollView()
		{
			GUILayout.EndScrollView(true);
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00010B04 File Offset: 0x0000ED04
		internal static void EndScrollView(bool handleScrollWheel)
		{
			GUILayoutUtility.EndGroup("GUILayout.EndScrollView");
			GUILayoutUtility.EndLayoutGroup();
			GUI.EndScrollView(handleScrollWheel);
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00010B1C File Offset: 0x0000ED1C
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(text), GUI.skin.window, options);
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00010B44 File Offset: 0x0000ED44
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(image), GUI.skin.window, options);
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00010B6C File Offset: 0x0000ED6C
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, content, GUI.skin.window, options);
		}

		// Token: 0x060007FF RID: 2047 RVA: 0x00010B90 File Offset: 0x0000ED90
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, string text, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(text), style, options);
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00010BA4 File Offset: 0x0000EDA4
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, Texture image, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, GUIContent.Temp(image), style, options);
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		public static Rect Window(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, params GUILayoutOption[] options)
		{
			return GUILayout.DoWindow(id, screenRect, func, content, style, options);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00010BC8 File Offset: 0x0000EDC8
		private static Rect DoWindow(int id, Rect screenRect, GUI.WindowFunction func, GUIContent content, GUIStyle style, GUILayoutOption[] options)
		{
			GUIUtility.CheckOnGUI();
			GUILayout.LayoutedWindow layoutedWindow = new GUILayout.LayoutedWindow(func, screenRect, content, options, style);
			return GUI.Window(id, screenRect, new GUI.WindowFunction(layoutedWindow.DoWindow), content, style);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00010C00 File Offset: 0x0000EE00
		public static GUILayoutOption Width(float width)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedWidth, width);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00010C10 File Offset: 0x0000EE10
		public static GUILayoutOption MinWidth(float minWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.minWidth, minWidth);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00010C20 File Offset: 0x0000EE20
		public static GUILayoutOption MaxWidth(float maxWidth)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxWidth, maxWidth);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00010C30 File Offset: 0x0000EE30
		public static GUILayoutOption Height(float height)
		{
			return new GUILayoutOption(GUILayoutOption.Type.fixedHeight, height);
		}

		// Token: 0x06000807 RID: 2055 RVA: 0x00010C40 File Offset: 0x0000EE40
		public static GUILayoutOption MinHeight(float minHeight)
		{
			return new GUILayoutOption(GUILayoutOption.Type.minHeight, minHeight);
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x00010C50 File Offset: 0x0000EE50
		public static GUILayoutOption MaxHeight(float maxHeight)
		{
			return new GUILayoutOption(GUILayoutOption.Type.maxHeight, maxHeight);
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x00010C60 File Offset: 0x0000EE60
		public static GUILayoutOption ExpandWidth(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchWidth, (!expand) ? 0 : 1);
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x00010C7C File Offset: 0x0000EE7C
		public static GUILayoutOption ExpandHeight(bool expand)
		{
			return new GUILayoutOption(GUILayoutOption.Type.stretchHeight, (!expand) ? 0 : 1);
		}

		// Token: 0x020000ED RID: 237
		private sealed class LayoutedWindow
		{
			// Token: 0x0600080B RID: 2059 RVA: 0x00010C98 File Offset: 0x0000EE98
			internal LayoutedWindow(GUI.WindowFunction f, Rect _screenRect, GUIContent _content, GUILayoutOption[] _options, GUIStyle _style)
			{
				this.func = f;
				this.screenRect = _screenRect;
				this.options = _options;
				this.style = _style;
			}

			// Token: 0x0600080C RID: 2060 RVA: 0x00010CCC File Offset: 0x0000EECC
			public void DoWindow(int windowID)
			{
				GUILayoutGroup topLevel = GUILayoutUtility.current.topLevel;
				EventType type = Event.current.type;
				if (type != EventType.Layout)
				{
					topLevel.ResetCursor();
				}
				else
				{
					topLevel.resetCoords = true;
					topLevel.rect = this.screenRect;
					if (this.options != null)
					{
						topLevel.ApplyOptions(this.options);
					}
					topLevel.isWindow = true;
					topLevel.windowID = windowID;
					topLevel.style = this.style;
				}
				this.func(windowID);
			}

			// Token: 0x04000301 RID: 769
			private GUI.WindowFunction func;

			// Token: 0x04000302 RID: 770
			private Rect screenRect;

			// Token: 0x04000303 RID: 771
			private GUILayoutOption[] options;

			// Token: 0x04000304 RID: 772
			private GUIStyle style;
		}
	}
}
