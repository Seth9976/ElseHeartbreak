using System;
using GameTypes;

namespace TingTing
{
	// Token: 0x0200000E RID: 14
	public class SimpleRoomBuilder
	{
		// Token: 0x060000C0 RID: 192 RVA: 0x00004D0C File Offset: 0x00002F0C
		public SimpleRoomBuilder(RoomRunner pRoomRunner)
		{
			this._roomRunner = pRoomRunner;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00004D1C File Offset: 0x00002F1C
		public Room CreateRoomWithSize(string pName, int pWidth, int pHeight)
		{
			Room room = this._roomRunner.CreateRoom<Room>(pName);
			for (int i = 0; i < pWidth; i++)
			{
				for (int j = 0; j < pHeight; j++)
				{
					room.AddTile(new PointTileNode(new IntPoint(i, j), room));
				}
			}
			return room;
		}

		// Token: 0x0400004E RID: 78
		private RoomRunner _roomRunner;
	}
}
