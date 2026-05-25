using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x020000FF RID: 255
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	public sealed class GUIContent
	{
		// Token: 0x060008EE RID: 2286 RVA: 0x00014D84 File Offset: 0x00012F84
		public GUIContent()
		{
		}

		// Token: 0x060008EF RID: 2287 RVA: 0x00014DA4 File Offset: 0x00012FA4
		public GUIContent(string text)
		{
			this.m_Text = text;
		}

		// Token: 0x060008F0 RID: 2288 RVA: 0x00014DCC File Offset: 0x00012FCC
		public GUIContent(Texture image)
		{
			this.m_Image = image;
		}

		// Token: 0x060008F1 RID: 2289 RVA: 0x00014DF4 File Offset: 0x00012FF4
		public GUIContent(string text, Texture image)
		{
			this.m_Text = text;
			this.m_Image = image;
		}

		// Token: 0x060008F2 RID: 2290 RVA: 0x00014E2C File Offset: 0x0001302C
		public GUIContent(string text, string tooltip)
		{
			this.m_Text = text;
			this.m_Tooltip = tooltip;
		}

		// Token: 0x060008F3 RID: 2291 RVA: 0x00014E64 File Offset: 0x00013064
		public GUIContent(Texture image, string tooltip)
		{
			this.m_Image = image;
			this.m_Tooltip = tooltip;
		}

		// Token: 0x060008F4 RID: 2292 RVA: 0x00014E9C File Offset: 0x0001309C
		public GUIContent(string text, Texture image, string tooltip)
		{
			this.m_Text = text;
			this.m_Image = image;
			this.m_Tooltip = tooltip;
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x00014ED0 File Offset: 0x000130D0
		public GUIContent(GUIContent src)
		{
			this.m_Text = src.m_Text;
			this.m_Image = src.m_Image;
			this.m_Tooltip = src.m_Tooltip;
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x060008F7 RID: 2295 RVA: 0x00014F50 File Offset: 0x00013150
		// (set) Token: 0x060008F8 RID: 2296 RVA: 0x00014F58 File Offset: 0x00013158
		public string text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				this.m_Text = value;
			}
		}

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x060008F9 RID: 2297 RVA: 0x00014F64 File Offset: 0x00013164
		// (set) Token: 0x060008FA RID: 2298 RVA: 0x00014F6C File Offset: 0x0001316C
		public Texture image
		{
			get
			{
				return this.m_Image;
			}
			set
			{
				this.m_Image = value;
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x060008FB RID: 2299 RVA: 0x00014F78 File Offset: 0x00013178
		// (set) Token: 0x060008FC RID: 2300 RVA: 0x00014F80 File Offset: 0x00013180
		public string tooltip
		{
			get
			{
				return this.m_Tooltip;
			}
			set
			{
				this.m_Tooltip = value;
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x00014F8C File Offset: 0x0001318C
		internal int hash
		{
			get
			{
				int num = 0;
				if (this.m_Text != null && this.m_Text != string.Empty)
				{
					num = this.m_Text.GetHashCode() * 37;
				}
				return num;
			}
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00014FCC File Offset: 0x000131CC
		internal static GUIContent Temp(string t)
		{
			GUIContent.s_Text.m_Text = t;
			return GUIContent.s_Text;
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00014FE0 File Offset: 0x000131E0
		internal static GUIContent Temp(Texture i)
		{
			GUIContent.s_Image.m_Image = i;
			return GUIContent.s_Image;
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00014FF4 File Offset: 0x000131F4
		internal static GUIContent Temp(string t, Texture i)
		{
			GUIContent.s_TextImage.m_Text = t;
			GUIContent.s_TextImage.m_Image = i;
			return GUIContent.s_TextImage;
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x00015014 File Offset: 0x00013214
		internal static void ClearStaticCache()
		{
			GUIContent.s_Text.m_Text = null;
			GUIContent.s_Image.m_Image = null;
			GUIContent.s_TextImage.m_Text = null;
			GUIContent.s_TextImage.m_Image = null;
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00015050 File Offset: 0x00013250
		internal static GUIContent[] Temp(string[] texts)
		{
			GUIContent[] array = new GUIContent[texts.Length];
			for (int i = 0; i < texts.Length; i++)
			{
				array[i] = new GUIContent(texts[i]);
			}
			return array;
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00015088 File Offset: 0x00013288
		internal static GUIContent[] Temp(Texture[] images)
		{
			GUIContent[] array = new GUIContent[images.Length];
			for (int i = 0; i < images.Length; i++)
			{
				array[i] = new GUIContent(images[i]);
			}
			return array;
		}

		// Token: 0x0400037B RID: 891
		[SerializeField]
		private string m_Text = string.Empty;

		// Token: 0x0400037C RID: 892
		[SerializeField]
		private Texture m_Image;

		// Token: 0x0400037D RID: 893
		[SerializeField]
		private string m_Tooltip = string.Empty;

		// Token: 0x0400037E RID: 894
		public static GUIContent none = new GUIContent(string.Empty);

		// Token: 0x0400037F RID: 895
		private static GUIContent s_Text = new GUIContent();

		// Token: 0x04000380 RID: 896
		private static GUIContent s_Image = new GUIContent();

		// Token: 0x04000381 RID: 897
		private static GUIContent s_TextImage = new GUIContent();
	}
}
