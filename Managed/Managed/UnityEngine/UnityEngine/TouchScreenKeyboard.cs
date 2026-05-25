using System;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000109 RID: 265
	public sealed class TouchScreenKeyboard
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x0600099C RID: 2460 RVA: 0x00015C94 File Offset: 0x00013E94
		public static bool isSupported
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x00015C98 File Offset: 0x00013E98
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure, bool alert)
		{
			string empty = string.Empty;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, alert, empty);
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x00015CBC File Offset: 0x00013EBC
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline, bool secure)
		{
			string empty = string.Empty;
			bool flag = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, secure, flag, empty);
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x00015CE0 File Offset: 0x00013EE0
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection, bool multiline)
		{
			string empty = string.Empty;
			bool flag = false;
			bool flag2 = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, multiline, flag2, flag, empty);
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x00015D04 File Offset: 0x00013F04
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType, bool autocorrection)
		{
			string empty = string.Empty;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			return TouchScreenKeyboard.Open(text, keyboardType, autocorrection, flag3, flag2, flag, empty);
		}

		// Token: 0x060009A1 RID: 2465 RVA: 0x00015D2C File Offset: 0x00013F2C
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text, TouchScreenKeyboardType keyboardType)
		{
			string empty = string.Empty;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			return TouchScreenKeyboard.Open(text, keyboardType, flag4, flag3, flag2, flag, empty);
		}

		// Token: 0x060009A2 RID: 2466 RVA: 0x00015D58 File Offset: 0x00013F58
		[ExcludeFromDocs]
		public static TouchScreenKeyboard Open(string text)
		{
			string empty = string.Empty;
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			bool flag4 = true;
			TouchScreenKeyboardType touchScreenKeyboardType = TouchScreenKeyboardType.Default;
			return TouchScreenKeyboard.Open(text, touchScreenKeyboardType, flag4, flag3, flag2, flag, empty);
		}

		// Token: 0x060009A3 RID: 2467 RVA: 0x00015D88 File Offset: 0x00013F88
		public static TouchScreenKeyboard Open(string text, [DefaultValue("TouchScreenKeyboardType.Default")] TouchScreenKeyboardType keyboardType, [DefaultValue("true")] bool autocorrection, [DefaultValue("false")] bool multiline, [DefaultValue("false")] bool secure, [DefaultValue("false")] bool alert, [DefaultValue("\"\"")] string textPlaceholder)
		{
			return null;
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060009A4 RID: 2468 RVA: 0x00015D8C File Offset: 0x00013F8C
		// (set) Token: 0x060009A5 RID: 2469 RVA: 0x00015D94 File Offset: 0x00013F94
		public string text
		{
			get
			{
				return string.Empty;
			}
			set
			{
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060009A6 RID: 2470 RVA: 0x00015D98 File Offset: 0x00013F98
		// (set) Token: 0x060009A7 RID: 2471 RVA: 0x00015D9C File Offset: 0x00013F9C
		public static bool hideInput
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060009A8 RID: 2472 RVA: 0x00015DA0 File Offset: 0x00013FA0
		// (set) Token: 0x060009A9 RID: 2473 RVA: 0x00015DA4 File Offset: 0x00013FA4
		public bool active
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x00015DA8 File Offset: 0x00013FA8
		public bool done
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060009AB RID: 2475 RVA: 0x00015DAC File Offset: 0x00013FAC
		public bool wasCanceled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x00015DB0 File Offset: 0x00013FB0
		private static Rect area
		{
			get
			{
				return default(Rect);
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060009AD RID: 2477 RVA: 0x00015DC8 File Offset: 0x00013FC8
		private static bool visible
		{
			get
			{
				return false;
			}
		}
	}
}
