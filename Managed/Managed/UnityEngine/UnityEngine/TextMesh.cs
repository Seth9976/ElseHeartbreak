using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000B4 RID: 180
	public sealed class TextMesh : Component
	{
		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600043E RID: 1086
		// (set) Token: 0x0600043F RID: 1087
		public extern string text
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000440 RID: 1088
		// (set) Token: 0x06000441 RID: 1089
		public extern Font font
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000442 RID: 1090
		// (set) Token: 0x06000443 RID: 1091
		public extern int fontSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000444 RID: 1092
		// (set) Token: 0x06000445 RID: 1093
		public extern FontStyle fontStyle
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000446 RID: 1094
		// (set) Token: 0x06000447 RID: 1095
		public extern float offsetZ
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000448 RID: 1096
		// (set) Token: 0x06000449 RID: 1097
		public extern TextAlignment alignment
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600044A RID: 1098
		// (set) Token: 0x0600044B RID: 1099
		public extern TextAnchor anchor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600044C RID: 1100
		// (set) Token: 0x0600044D RID: 1101
		public extern float characterSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600044E RID: 1102
		// (set) Token: 0x0600044F RID: 1103
		public extern float lineSpacing
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000450 RID: 1104
		// (set) Token: 0x06000451 RID: 1105
		public extern float tabSize
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000452 RID: 1106
		// (set) Token: 0x06000453 RID: 1107
		public extern bool richText
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000454 RID: 1108 RVA: 0x0000BB34 File Offset: 0x00009D34
		// (set) Token: 0x06000455 RID: 1109 RVA: 0x0000BB4C File Offset: 0x00009D4C
		public Color color
		{
			get
			{
				Color color;
				this.INTERNAL_get_color(out color);
				return color;
			}
			set
			{
				this.INTERNAL_set_color(ref value);
			}
		}

		// Token: 0x06000456 RID: 1110
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_color(out Color value);

		// Token: 0x06000457 RID: 1111
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_set_color(ref Color value);
	}
}
