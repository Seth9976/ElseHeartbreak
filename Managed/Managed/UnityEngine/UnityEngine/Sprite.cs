using System;
using System.Runtime.CompilerServices;
using UnityEngine.Internal;

namespace UnityEngine
{
	// Token: 0x02000148 RID: 328
	public sealed class Sprite : Object
	{
		// Token: 0x06000DB9 RID: 3513 RVA: 0x0001D650 File Offset: 0x0001B850
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, [DefaultValue("100.0f")] float pixelsPerUnit, [DefaultValue("0")] uint extrude, [DefaultValue("SpriteMeshType.Tight")] SpriteMeshType meshType, [DefaultValue("Vector4.zero")] Vector4 border)
		{
			return Sprite.INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref border);
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x0001D664 File Offset: 0x0001B864
		[ExcludeFromDocs]
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType)
		{
			Vector4 zero = Vector4.zero;
			return Sprite.INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, meshType, ref zero);
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x0001D688 File Offset: 0x0001B888
		[ExcludeFromDocs]
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit, uint extrude)
		{
			Vector4 zero = Vector4.zero;
			SpriteMeshType spriteMeshType = SpriteMeshType.Tight;
			return Sprite.INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, extrude, spriteMeshType, ref zero);
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x0001D6B0 File Offset: 0x0001B8B0
		[ExcludeFromDocs]
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot, float pixelsPerUnit)
		{
			Vector4 zero = Vector4.zero;
			SpriteMeshType spriteMeshType = SpriteMeshType.Tight;
			uint num = 0U;
			return Sprite.INTERNAL_CALL_Create(texture, ref rect, ref pivot, pixelsPerUnit, num, spriteMeshType, ref zero);
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x0001D6D8 File Offset: 0x0001B8D8
		[ExcludeFromDocs]
		public static Sprite Create(Texture2D texture, Rect rect, Vector2 pivot)
		{
			Vector4 zero = Vector4.zero;
			SpriteMeshType spriteMeshType = SpriteMeshType.Tight;
			uint num = 0U;
			float num2 = 100f;
			return Sprite.INTERNAL_CALL_Create(texture, ref rect, ref pivot, num2, num, spriteMeshType, ref zero);
		}

		// Token: 0x06000DBE RID: 3518
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Sprite INTERNAL_CALL_Create(Texture2D texture, ref Rect rect, ref Vector2 pivot, float pixelsPerUnit, uint extrude, SpriteMeshType meshType, ref Vector4 border);

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x0001D704 File Offset: 0x0001B904
		public Bounds bounds
		{
			get
			{
				Bounds bounds;
				this.INTERNAL_get_bounds(out bounds);
				return bounds;
			}
		}

		// Token: 0x06000DC0 RID: 3520
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_bounds(out Bounds value);

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x0001D71C File Offset: 0x0001B91C
		public Rect rect
		{
			get
			{
				Rect rect;
				this.INTERNAL_get_rect(out rect);
				return rect;
			}
		}

		// Token: 0x06000DC2 RID: 3522
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_rect(out Rect value);

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000DC3 RID: 3523
		public extern float pixelsPerUnit
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000DC4 RID: 3524
		public extern Texture2D texture
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x0001D734 File Offset: 0x0001B934
		public Rect textureRect
		{
			get
			{
				Rect rect;
				this.INTERNAL_get_textureRect(out rect);
				return rect;
			}
		}

		// Token: 0x06000DC6 RID: 3526
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_textureRect(out Rect value);

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x0001D74C File Offset: 0x0001B94C
		public Vector2 textureRectOffset
		{
			get
			{
				Vector2 vector;
				Sprite.Internal_GetTextureRectOffset(this, out vector);
				return vector;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000DC8 RID: 3528
		public extern bool packed
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000DC9 RID: 3529
		public extern SpritePackingMode packingMode
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000DCA RID: 3530
		public extern SpritePackingRotation packingRotation
		{
			[WrapperlessIcall]
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
		}

		// Token: 0x06000DCB RID: 3531
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_GetTextureRectOffset(Sprite sprite, out Vector2 output);

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000DCC RID: 3532 RVA: 0x0001D764 File Offset: 0x0001B964
		public Vector4 border
		{
			get
			{
				Vector4 vector;
				this.INTERNAL_get_border(out vector);
				return vector;
			}
		}

		// Token: 0x06000DCD RID: 3533
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void INTERNAL_get_border(out Vector4 value);
	}
}
