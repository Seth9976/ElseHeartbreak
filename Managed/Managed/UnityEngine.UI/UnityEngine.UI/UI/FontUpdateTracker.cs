using System;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	// Token: 0x0200003C RID: 60
	public static class FontUpdateTracker
	{
		// Token: 0x06000190 RID: 400 RVA: 0x00005D34 File Offset: 0x00003F34
		public static void TrackText(Text t)
		{
			if (t.font == null)
			{
				return;
			}
			List<Text> list;
			FontUpdateTracker.m_Tracked.TryGetValue(t.font, out list);
			if (list == null)
			{
				if (FontUpdateTracker.m_Tracked.Count == 0)
				{
					Font.textureRebuilt += FontUpdateTracker.RebuildForFont;
				}
				list = new List<Text>();
				FontUpdateTracker.m_Tracked.Add(t.font, list);
			}
			if (!list.Contains(t))
			{
				list.Add(t);
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005DB8 File Offset: 0x00003FB8
		private static void RebuildForFont(Font f)
		{
			List<Text> list;
			FontUpdateTracker.m_Tracked.TryGetValue(f, out list);
			if (list == null)
			{
				return;
			}
			for (int i = 0; i < list.Count; i++)
			{
				list[i].FontTextureChanged();
			}
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005E00 File Offset: 0x00004000
		public static void UntrackText(Text t)
		{
			if (t.font == null)
			{
				return;
			}
			List<Text> list;
			FontUpdateTracker.m_Tracked.TryGetValue(t.font, out list);
			if (list == null)
			{
				return;
			}
			list.Remove(t);
			if (list.Count == 0)
			{
				FontUpdateTracker.m_Tracked.Remove(t.font);
				if (FontUpdateTracker.m_Tracked.Count == 0)
				{
					Font.textureRebuilt -= FontUpdateTracker.RebuildForFont;
				}
			}
		}

		// Token: 0x040000C0 RID: 192
		private static Dictionary<Font, List<Text>> m_Tracked = new Dictionary<Font, List<Text>>();
	}
}
