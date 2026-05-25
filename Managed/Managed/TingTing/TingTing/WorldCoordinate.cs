using System;
using GameTypes;

namespace TingTing
{
	// Token: 0x02000013 RID: 19
	public struct WorldCoordinate
	{
		// Token: 0x060000D0 RID: 208 RVA: 0x00004FF8 File Offset: 0x000031F8
		public WorldCoordinate(string pRoomName, IntPoint pLocalPosition)
		{
			this.localPosition = pLocalPosition;
			this.roomName = pRoomName;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005008 File Offset: 0x00003208
		public WorldCoordinate(string pRoomName, int pX, int pY)
		{
			this.localPosition = new IntPoint(pX, pY);
			this.roomName = pRoomName;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00005038 File Offset: 0x00003238
		public override bool Equals(object obj)
		{
			if (obj is WorldCoordinate)
			{
				WorldCoordinate worldCoordinate = (WorldCoordinate)obj;
				if (worldCoordinate.localPosition == this.localPosition && worldCoordinate.roomName == this.roomName)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005088 File Offset: 0x00003288
		public override int GetHashCode()
		{
			return this.roomName.GetHashCode() ^ this.localPosition.GetHashCode();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000050A4 File Offset: 0x000032A4
		public override string ToString()
		{
			return string.Concat(new object[] { "[", this.roomName, " ", this.localPosition, "]" });
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000050EC File Offset: 0x000032EC
		public static bool operator ==(WorldCoordinate a, WorldCoordinate b)
		{
			return a.Equals(b);
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005104 File Offset: 0x00003304
		public static bool operator !=(WorldCoordinate a, WorldCoordinate b)
		{
			return !a.Equals(b);
		}

		// Token: 0x04000051 RID: 81
		public const string UNDEFINED_ROOM = "undefined_room";

		// Token: 0x04000052 RID: 82
		public static readonly WorldCoordinate NONE = new WorldCoordinate("undefined_room", IntPoint.Zero);

		// Token: 0x04000053 RID: 83
		public string roomName;

		// Token: 0x04000054 RID: 84
		public IntPoint localPosition;
	}
}
