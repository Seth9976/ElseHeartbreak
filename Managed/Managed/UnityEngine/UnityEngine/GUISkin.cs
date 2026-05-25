using System;
using System.Collections;
using System.Collections.Generic;

namespace UnityEngine
{
	// Token: 0x020000FE RID: 254
	[ExecuteInEditMode]
	[Serializable]
	public sealed class GUISkin : ScriptableObject
	{
		// Token: 0x060008B8 RID: 2232 RVA: 0x00014420 File Offset: 0x00012620
		public GUISkin()
		{
			this.m_CustomStyles = new GUIStyle[1];
		}

		// Token: 0x060008B9 RID: 2233 RVA: 0x00014440 File Offset: 0x00012640
		internal void OnEnable()
		{
			this.Apply();
			foreach (GUIStyle guistyle in this.styles.Values)
			{
				guistyle.CreateObjectReferences();
			}
		}

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x000144B0 File Offset: 0x000126B0
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x000144B8 File Offset: 0x000126B8
		public Font font
		{
			get
			{
				return this.m_Font;
			}
			set
			{
				this.m_Font = value;
				if (GUISkin.current == this)
				{
					GUIStyle.SetDefaultFont(this.m_Font);
				}
				this.Apply();
			}
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x000144F0 File Offset: 0x000126F0
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x000144F8 File Offset: 0x000126F8
		public GUIStyle box
		{
			get
			{
				return this.m_box;
			}
			set
			{
				this.m_box = value;
				this.Apply();
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060008BE RID: 2238 RVA: 0x00014508 File Offset: 0x00012708
		// (set) Token: 0x060008BF RID: 2239 RVA: 0x00014510 File Offset: 0x00012710
		public GUIStyle label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
				this.Apply();
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008C0 RID: 2240 RVA: 0x00014520 File Offset: 0x00012720
		// (set) Token: 0x060008C1 RID: 2241 RVA: 0x00014528 File Offset: 0x00012728
		public GUIStyle textField
		{
			get
			{
				return this.m_textField;
			}
			set
			{
				this.m_textField = value;
				this.Apply();
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008C2 RID: 2242 RVA: 0x00014538 File Offset: 0x00012738
		// (set) Token: 0x060008C3 RID: 2243 RVA: 0x00014540 File Offset: 0x00012740
		public GUIStyle textArea
		{
			get
			{
				return this.m_textArea;
			}
			set
			{
				this.m_textArea = value;
				this.Apply();
			}
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060008C4 RID: 2244 RVA: 0x00014550 File Offset: 0x00012750
		// (set) Token: 0x060008C5 RID: 2245 RVA: 0x00014558 File Offset: 0x00012758
		public GUIStyle button
		{
			get
			{
				return this.m_button;
			}
			set
			{
				this.m_button = value;
				this.Apply();
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00014568 File Offset: 0x00012768
		// (set) Token: 0x060008C7 RID: 2247 RVA: 0x00014570 File Offset: 0x00012770
		public GUIStyle toggle
		{
			get
			{
				return this.m_toggle;
			}
			set
			{
				this.m_toggle = value;
				this.Apply();
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060008C8 RID: 2248 RVA: 0x00014580 File Offset: 0x00012780
		// (set) Token: 0x060008C9 RID: 2249 RVA: 0x00014588 File Offset: 0x00012788
		public GUIStyle window
		{
			get
			{
				return this.m_window;
			}
			set
			{
				this.m_window = value;
				this.Apply();
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00014598 File Offset: 0x00012798
		// (set) Token: 0x060008CB RID: 2251 RVA: 0x000145A0 File Offset: 0x000127A0
		public GUIStyle horizontalSlider
		{
			get
			{
				return this.m_horizontalSlider;
			}
			set
			{
				this.m_horizontalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x000145B0 File Offset: 0x000127B0
		// (set) Token: 0x060008CD RID: 2253 RVA: 0x000145B8 File Offset: 0x000127B8
		public GUIStyle horizontalSliderThumb
		{
			get
			{
				return this.m_horizontalSliderThumb;
			}
			set
			{
				this.m_horizontalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x000145C8 File Offset: 0x000127C8
		// (set) Token: 0x060008CF RID: 2255 RVA: 0x000145D0 File Offset: 0x000127D0
		public GUIStyle verticalSlider
		{
			get
			{
				return this.m_verticalSlider;
			}
			set
			{
				this.m_verticalSlider = value;
				this.Apply();
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x000145E0 File Offset: 0x000127E0
		// (set) Token: 0x060008D1 RID: 2257 RVA: 0x000145E8 File Offset: 0x000127E8
		public GUIStyle verticalSliderThumb
		{
			get
			{
				return this.m_verticalSliderThumb;
			}
			set
			{
				this.m_verticalSliderThumb = value;
				this.Apply();
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x000145F8 File Offset: 0x000127F8
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x00014600 File Offset: 0x00012800
		public GUIStyle horizontalScrollbar
		{
			get
			{
				return this.m_horizontalScrollbar;
			}
			set
			{
				this.m_horizontalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00014610 File Offset: 0x00012810
		// (set) Token: 0x060008D5 RID: 2261 RVA: 0x00014618 File Offset: 0x00012818
		public GUIStyle horizontalScrollbarThumb
		{
			get
			{
				return this.m_horizontalScrollbarThumb;
			}
			set
			{
				this.m_horizontalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00014628 File Offset: 0x00012828
		// (set) Token: 0x060008D7 RID: 2263 RVA: 0x00014630 File Offset: 0x00012830
		public GUIStyle horizontalScrollbarLeftButton
		{
			get
			{
				return this.m_horizontalScrollbarLeftButton;
			}
			set
			{
				this.m_horizontalScrollbarLeftButton = value;
				this.Apply();
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x060008D8 RID: 2264 RVA: 0x00014640 File Offset: 0x00012840
		// (set) Token: 0x060008D9 RID: 2265 RVA: 0x00014648 File Offset: 0x00012848
		public GUIStyle horizontalScrollbarRightButton
		{
			get
			{
				return this.m_horizontalScrollbarRightButton;
			}
			set
			{
				this.m_horizontalScrollbarRightButton = value;
				this.Apply();
			}
		}

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x060008DA RID: 2266 RVA: 0x00014658 File Offset: 0x00012858
		// (set) Token: 0x060008DB RID: 2267 RVA: 0x00014660 File Offset: 0x00012860
		public GUIStyle verticalScrollbar
		{
			get
			{
				return this.m_verticalScrollbar;
			}
			set
			{
				this.m_verticalScrollbar = value;
				this.Apply();
			}
		}

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x060008DC RID: 2268 RVA: 0x00014670 File Offset: 0x00012870
		// (set) Token: 0x060008DD RID: 2269 RVA: 0x00014678 File Offset: 0x00012878
		public GUIStyle verticalScrollbarThumb
		{
			get
			{
				return this.m_verticalScrollbarThumb;
			}
			set
			{
				this.m_verticalScrollbarThumb = value;
				this.Apply();
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x060008DE RID: 2270 RVA: 0x00014688 File Offset: 0x00012888
		// (set) Token: 0x060008DF RID: 2271 RVA: 0x00014690 File Offset: 0x00012890
		public GUIStyle verticalScrollbarUpButton
		{
			get
			{
				return this.m_verticalScrollbarUpButton;
			}
			set
			{
				this.m_verticalScrollbarUpButton = value;
				this.Apply();
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x000146A0 File Offset: 0x000128A0
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x000146A8 File Offset: 0x000128A8
		public GUIStyle verticalScrollbarDownButton
		{
			get
			{
				return this.m_verticalScrollbarDownButton;
			}
			set
			{
				this.m_verticalScrollbarDownButton = value;
				this.Apply();
			}
		}

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x000146B8 File Offset: 0x000128B8
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x000146C0 File Offset: 0x000128C0
		public GUIStyle scrollView
		{
			get
			{
				return this.m_ScrollView;
			}
			set
			{
				this.m_ScrollView = value;
				this.Apply();
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x000146D0 File Offset: 0x000128D0
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x000146D8 File Offset: 0x000128D8
		public GUIStyle[] customStyles
		{
			get
			{
				return this.m_CustomStyles;
			}
			set
			{
				this.m_CustomStyles = value;
				this.Apply();
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x000146E8 File Offset: 0x000128E8
		public GUISettings settings
		{
			get
			{
				return this.m_Settings;
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x000146F0 File Offset: 0x000128F0
		internal static GUIStyle error
		{
			get
			{
				if (GUISkin.ms_Error == null)
				{
					GUISkin.ms_Error = new GUIStyle();
				}
				return GUISkin.ms_Error;
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001470C File Offset: 0x0001290C
		internal void Apply()
		{
			if (this.m_CustomStyles == null)
			{
				Debug.Log("custom styles is null");
			}
			this.BuildStyleCache();
		}

		// Token: 0x060008E9 RID: 2281 RVA: 0x0001472C File Offset: 0x0001292C
		private void BuildStyleCache()
		{
			if (this.m_box == null)
			{
				this.m_box = new GUIStyle();
			}
			if (this.m_button == null)
			{
				this.m_button = new GUIStyle();
			}
			if (this.m_toggle == null)
			{
				this.m_toggle = new GUIStyle();
			}
			if (this.m_label == null)
			{
				this.m_label = new GUIStyle();
			}
			if (this.m_window == null)
			{
				this.m_window = new GUIStyle();
			}
			if (this.m_textField == null)
			{
				this.m_textField = new GUIStyle();
			}
			if (this.m_textArea == null)
			{
				this.m_textArea = new GUIStyle();
			}
			if (this.m_horizontalSlider == null)
			{
				this.m_horizontalSlider = new GUIStyle();
			}
			if (this.m_horizontalSliderThumb == null)
			{
				this.m_horizontalSliderThumb = new GUIStyle();
			}
			if (this.m_verticalSlider == null)
			{
				this.m_verticalSlider = new GUIStyle();
			}
			if (this.m_verticalSliderThumb == null)
			{
				this.m_verticalSliderThumb = new GUIStyle();
			}
			if (this.m_horizontalScrollbar == null)
			{
				this.m_horizontalScrollbar = new GUIStyle();
			}
			if (this.m_horizontalScrollbarThumb == null)
			{
				this.m_horizontalScrollbarThumb = new GUIStyle();
			}
			if (this.m_horizontalScrollbarLeftButton == null)
			{
				this.m_horizontalScrollbarLeftButton = new GUIStyle();
			}
			if (this.m_horizontalScrollbarRightButton == null)
			{
				this.m_horizontalScrollbarRightButton = new GUIStyle();
			}
			if (this.m_verticalScrollbar == null)
			{
				this.m_verticalScrollbar = new GUIStyle();
			}
			if (this.m_verticalScrollbarThumb == null)
			{
				this.m_verticalScrollbarThumb = new GUIStyle();
			}
			if (this.m_verticalScrollbarUpButton == null)
			{
				this.m_verticalScrollbarUpButton = new GUIStyle();
			}
			if (this.m_verticalScrollbarDownButton == null)
			{
				this.m_verticalScrollbarDownButton = new GUIStyle();
			}
			if (this.m_ScrollView == null)
			{
				this.m_ScrollView = new GUIStyle();
			}
			this.styles = new Dictionary<string, GUIStyle>(StringComparer.OrdinalIgnoreCase);
			this.styles["box"] = this.m_box;
			this.m_box.name = "box";
			this.styles["button"] = this.m_button;
			this.m_button.name = "button";
			this.styles["toggle"] = this.m_toggle;
			this.m_toggle.name = "toggle";
			this.styles["label"] = this.m_label;
			this.m_label.name = "label";
			this.styles["window"] = this.m_window;
			this.m_window.name = "window";
			this.styles["textfield"] = this.m_textField;
			this.m_textField.name = "textfield";
			this.styles["textarea"] = this.m_textArea;
			this.m_textArea.name = "textarea";
			this.styles["horizontalslider"] = this.m_horizontalSlider;
			this.m_horizontalSlider.name = "horizontalslider";
			this.styles["horizontalsliderthumb"] = this.m_horizontalSliderThumb;
			this.m_horizontalSliderThumb.name = "horizontalsliderthumb";
			this.styles["verticalslider"] = this.m_verticalSlider;
			this.m_verticalSlider.name = "verticalslider";
			this.styles["verticalsliderthumb"] = this.m_verticalSliderThumb;
			this.m_verticalSliderThumb.name = "verticalsliderthumb";
			this.styles["horizontalscrollbar"] = this.m_horizontalScrollbar;
			this.m_horizontalScrollbar.name = "horizontalscrollbar";
			this.styles["horizontalscrollbarthumb"] = this.m_horizontalScrollbarThumb;
			this.m_horizontalScrollbarThumb.name = "horizontalscrollbarthumb";
			this.styles["horizontalscrollbarleftbutton"] = this.m_horizontalScrollbarLeftButton;
			this.m_horizontalScrollbarLeftButton.name = "horizontalscrollbarleftbutton";
			this.styles["horizontalscrollbarrightbutton"] = this.m_horizontalScrollbarRightButton;
			this.m_horizontalScrollbarRightButton.name = "horizontalscrollbarrightbutton";
			this.styles["verticalscrollbar"] = this.m_verticalScrollbar;
			this.m_verticalScrollbar.name = "verticalscrollbar";
			this.styles["verticalscrollbarthumb"] = this.m_verticalScrollbarThumb;
			this.m_verticalScrollbarThumb.name = "verticalscrollbarthumb";
			this.styles["verticalscrollbarupbutton"] = this.m_verticalScrollbarUpButton;
			this.m_verticalScrollbarUpButton.name = "verticalscrollbarupbutton";
			this.styles["verticalscrollbardownbutton"] = this.m_verticalScrollbarDownButton;
			this.m_verticalScrollbarDownButton.name = "verticalscrollbardownbutton";
			this.styles["scrollview"] = this.m_ScrollView;
			this.m_ScrollView.name = "scrollview";
			if (this.m_CustomStyles != null)
			{
				for (int i = 0; i < this.m_CustomStyles.Length; i++)
				{
					if (this.m_CustomStyles[i] != null)
					{
						this.styles[this.m_CustomStyles[i].name] = this.m_CustomStyles[i];
					}
				}
			}
			GUISkin.error.stretchHeight = true;
			GUISkin.error.normal.textColor = Color.red;
		}

		// Token: 0x060008EA RID: 2282 RVA: 0x00014C70 File Offset: 0x00012E70
		public GUIStyle GetStyle(string styleName)
		{
			GUIStyle guistyle = this.FindStyle(styleName);
			if (guistyle != null)
			{
				return guistyle;
			}
			Debug.LogWarning(string.Concat(new object[]
			{
				"Unable to find style '",
				styleName,
				"' in skin '",
				base.name,
				"' ",
				Event.current.type
			}));
			return GUISkin.error;
		}

		// Token: 0x060008EB RID: 2283 RVA: 0x00014CDC File Offset: 0x00012EDC
		public GUIStyle FindStyle(string styleName)
		{
			if (this == null)
			{
				Debug.LogError("GUISkin is NULL");
				return null;
			}
			if (this.styles == null)
			{
				this.BuildStyleCache();
			}
			GUIStyle guistyle;
			if (this.styles.TryGetValue(styleName, out guistyle))
			{
				return guistyle;
			}
			return null;
		}

		// Token: 0x060008EC RID: 2284 RVA: 0x00014D28 File Offset: 0x00012F28
		internal void MakeCurrent()
		{
			GUISkin.current = this;
			GUIStyle.SetDefaultFont(this.font);
			if (GUISkin.m_SkinChanged != null)
			{
				GUISkin.m_SkinChanged();
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00014D50 File Offset: 0x00012F50
		public IEnumerator GetEnumerator()
		{
			if (this.styles == null)
			{
				this.BuildStyleCache();
			}
			return this.styles.Values.GetEnumerator();
		}

		// Token: 0x04000360 RID: 864
		[SerializeField]
		private Font m_Font;

		// Token: 0x04000361 RID: 865
		[SerializeField]
		private GUIStyle m_box;

		// Token: 0x04000362 RID: 866
		[SerializeField]
		private GUIStyle m_button;

		// Token: 0x04000363 RID: 867
		[SerializeField]
		private GUIStyle m_toggle;

		// Token: 0x04000364 RID: 868
		[SerializeField]
		private GUIStyle m_label;

		// Token: 0x04000365 RID: 869
		[SerializeField]
		private GUIStyle m_textField;

		// Token: 0x04000366 RID: 870
		[SerializeField]
		private GUIStyle m_textArea;

		// Token: 0x04000367 RID: 871
		[SerializeField]
		private GUIStyle m_window;

		// Token: 0x04000368 RID: 872
		[SerializeField]
		private GUIStyle m_horizontalSlider;

		// Token: 0x04000369 RID: 873
		[SerializeField]
		private GUIStyle m_horizontalSliderThumb;

		// Token: 0x0400036A RID: 874
		[SerializeField]
		private GUIStyle m_verticalSlider;

		// Token: 0x0400036B RID: 875
		[SerializeField]
		private GUIStyle m_verticalSliderThumb;

		// Token: 0x0400036C RID: 876
		[SerializeField]
		private GUIStyle m_horizontalScrollbar;

		// Token: 0x0400036D RID: 877
		[SerializeField]
		private GUIStyle m_horizontalScrollbarThumb;

		// Token: 0x0400036E RID: 878
		[SerializeField]
		private GUIStyle m_horizontalScrollbarLeftButton;

		// Token: 0x0400036F RID: 879
		[SerializeField]
		private GUIStyle m_horizontalScrollbarRightButton;

		// Token: 0x04000370 RID: 880
		[SerializeField]
		private GUIStyle m_verticalScrollbar;

		// Token: 0x04000371 RID: 881
		[SerializeField]
		private GUIStyle m_verticalScrollbarThumb;

		// Token: 0x04000372 RID: 882
		[SerializeField]
		private GUIStyle m_verticalScrollbarUpButton;

		// Token: 0x04000373 RID: 883
		[SerializeField]
		private GUIStyle m_verticalScrollbarDownButton;

		// Token: 0x04000374 RID: 884
		[SerializeField]
		private GUIStyle m_ScrollView;

		// Token: 0x04000375 RID: 885
		[SerializeField]
		internal GUIStyle[] m_CustomStyles;

		// Token: 0x04000376 RID: 886
		[SerializeField]
		private GUISettings m_Settings = new GUISettings();

		// Token: 0x04000377 RID: 887
		internal static GUIStyle ms_Error;

		// Token: 0x04000378 RID: 888
		private Dictionary<string, GUIStyle> styles;

		// Token: 0x04000379 RID: 889
		internal static GUISkin.SkinChangedDelegate m_SkinChanged;

		// Token: 0x0400037A RID: 890
		internal static GUISkin current;

		// Token: 0x0200022A RID: 554
		// (Invoke) Token: 0x06001ACD RID: 6861
		internal delegate void SkinChangedDelegate();
	}
}
