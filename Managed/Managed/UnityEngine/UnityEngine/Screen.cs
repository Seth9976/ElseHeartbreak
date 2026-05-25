using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x020000CB RID: 203
	public sealed class Screen
	{
		// Token: 0x1700016C RID: 364
		// (get) Token: 0x0600057A RID: 1402
		public static extern Resolution[] resolutions
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000C6A4 File Offset: 0x0000A8A4
		public static Resolution[] GetResolution
		{
			get
			{
				return Screen.resolutions;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x0600057C RID: 1404
		public static extern Resolution currentResolution
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x0600057D RID: 1405
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetResolution(int width, int height, bool fullscreen, [DefaultValue("0")] int preferredRefreshRate);

		// Token: 0x0600057E RID: 1406 RVA: 0x0000C6AC File Offset: 0x0000A8AC
		[ExcludeFromDocs]
		public static void SetResolution(int width, int height, bool fullscreen)
		{
			int num = 0;
			Screen.SetResolution(width, height, fullscreen, num);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600057F RID: 1407
		// (set) Token: 0x06000580 RID: 1408
		public static extern bool showCursor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000581 RID: 1409
		// (set) Token: 0x06000582 RID: 1410
		public static extern bool lockCursor
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000583 RID: 1411
		public static extern int width
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000584 RID: 1412
		public static extern int height
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x06000585 RID: 1413
		public static extern float dpi
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x06000586 RID: 1414
		// (set) Token: 0x06000587 RID: 1415
		public static extern bool fullScreen
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x06000588 RID: 1416
		// (set) Token: 0x06000589 RID: 1417
		public static extern bool autorotateToPortrait
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600058A RID: 1418
		// (set) Token: 0x0600058B RID: 1419
		public static extern bool autorotateToPortraitUpsideDown
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600058C RID: 1420
		// (set) Token: 0x0600058D RID: 1421
		public static extern bool autorotateToLandscapeLeft
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x0600058E RID: 1422
		// (set) Token: 0x0600058F RID: 1423
		public static extern bool autorotateToLandscapeRight
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000590 RID: 1424
		// (set) Token: 0x06000591 RID: 1425
		public static extern ScreenOrientation orientation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000592 RID: 1426
		// (set) Token: 0x06000593 RID: 1427
		public static extern int sleepTimeout
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}
	}
}
