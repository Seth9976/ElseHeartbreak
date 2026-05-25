using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000E0 RID: 224
	public sealed class Font : Object
	{
		// Token: 0x060006A5 RID: 1701 RVA: 0x0000CED0 File Offset: 0x0000B0D0
		public Font()
		{
			Font.Internal_CreateFont(this, null);
		}

		// Token: 0x060006A6 RID: 1702 RVA: 0x0000CEE0 File Offset: 0x0000B0E0
		public Font(string name)
		{
			Font.Internal_CreateFont(this, name);
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060006A7 RID: 1703 RVA: 0x0000CEF0 File Offset: 0x0000B0F0
		// (remove) Token: 0x060006A8 RID: 1704 RVA: 0x0000CF08 File Offset: 0x0000B108
		public static event Action<Font> textureRebuilt;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060006A9 RID: 1705 RVA: 0x0000CF20 File Offset: 0x0000B120
		// (remove) Token: 0x060006AA RID: 1706 RVA: 0x0000CF3C File Offset: 0x0000B13C
		private event Font.FontTextureRebuildCallback m_FontTextureRebuildCallback;

		// Token: 0x060006AB RID: 1707
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_CreateFont([Writable] Font _font, string name);

		// Token: 0x170001B2 RID: 434
		// (get) Token: 0x060006AC RID: 1708
		// (set) Token: 0x060006AD RID: 1709
		public extern Material material
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060006AE RID: 1710
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool HasCharacter(char c);

		// Token: 0x170001B3 RID: 435
		// (get) Token: 0x060006AF RID: 1711
		// (set) Token: 0x060006B0 RID: 1712
		public extern string[] fontNames
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060006B1 RID: 1713
		// (set) Token: 0x060006B2 RID: 1714
		public extern CharacterInfo[] characterInfo
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x060006B3 RID: 1715
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void RequestCharactersInTexture(string characters, [UnityEngine.Internal.DefaultValue("0")] int size, [UnityEngine.Internal.DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x060006B4 RID: 1716 RVA: 0x0000CF58 File Offset: 0x0000B158
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters, int size)
		{
			FontStyle fontStyle = FontStyle.Normal;
			this.RequestCharactersInTexture(characters, size, fontStyle);
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x0000CF70 File Offset: 0x0000B170
		[ExcludeFromDocs]
		public void RequestCharactersInTexture(string characters)
		{
			FontStyle fontStyle = FontStyle.Normal;
			int num = 0;
			this.RequestCharactersInTexture(characters, num, fontStyle);
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0000CF8C File Offset: 0x0000B18C
		private static void InvokeTextureRebuilt_Internal(Font font)
		{
			Action<Font> action = Font.textureRebuilt;
			if (action != null)
			{
				action(font);
			}
			if (font.m_FontTextureRebuildCallback != null)
			{
				font.m_FontTextureRebuildCallback();
			}
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006B7 RID: 1719 RVA: 0x0000CFC4 File Offset: 0x0000B1C4
		// (set) Token: 0x060006B8 RID: 1720 RVA: 0x0000CFCC File Offset: 0x0000B1CC
		[Obsolete("Font.textureRebuildCallback has been deprecated. Use Font.textureRebuilt instead.")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public Font.FontTextureRebuildCallback textureRebuildCallback
		{
			get
			{
				return this.m_FontTextureRebuildCallback;
			}
			set
			{
				this.m_FontTextureRebuildCallback = value;
			}
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x0000CFD8 File Offset: 0x0000B1D8
		public static int GetMaxVertsForString(string str)
		{
			return str.Length * 4 + 4;
		}

		// Token: 0x060006BA RID: 1722
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool GetCharacterInfo(char ch, out CharacterInfo info, [UnityEngine.Internal.DefaultValue("0")] int size, [UnityEngine.Internal.DefaultValue("FontStyle.Normal")] FontStyle style);

		// Token: 0x060006BB RID: 1723 RVA: 0x0000CFE4 File Offset: 0x0000B1E4
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info, int size)
		{
			FontStyle fontStyle = FontStyle.Normal;
			return this.GetCharacterInfo(ch, out info, size, fontStyle);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x0000D000 File Offset: 0x0000B200
		[ExcludeFromDocs]
		public bool GetCharacterInfo(char ch, out CharacterInfo info)
		{
			FontStyle fontStyle = FontStyle.Normal;
			int num = 0;
			return this.GetCharacterInfo(ch, out info, num, fontStyle);
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006BD RID: 1725
		public extern bool dynamic
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006BE RID: 1726
		public extern int fontSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x02000228 RID: 552
		// (Invoke) Token: 0x06001AC5 RID: 6853
		[EditorBrowsable(EditorBrowsableState.Never)]
		public delegate void FontTextureRebuildCallback();
	}
}
