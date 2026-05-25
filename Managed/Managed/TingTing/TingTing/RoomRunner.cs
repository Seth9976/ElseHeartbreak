using System;
using System.Collections.Generic;
using GameTypes;
using RelayLib;

namespace TingTing
{
	// Token: 0x0200000F RID: 15
	public class RoomRunner : IPreloadable
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x00004D70 File Offset: 0x00002F70
		public RoomRunner(RelayTwo pRelay)
		{
			D.isNull(pRelay);
			this._roomTable = pRelay.GetTable("Rooms");
			List<Room> list = InstantiatorTwo.Process<Room>(this._roomTable);
			foreach (Room room in list)
			{
				this._rooms.Add(room.name, room);
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004E14 File Offset: 0x00003014
		public T CreateRoom<T>(string pName) where T : Room
		{
			if (this.HasRoom(pName))
			{
				throw new TingTingException("There is already a room called '" + pName + "' in Room Runner");
			}
			T t = InstantiatorTwo.Create<T>(this._roomTable);
			t.name = pName;
			this._rooms.Add(pName, t);
			return t;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004E70 File Offset: 0x00003070
		public Room GetRoom(string pName)
		{
			Room room = null;
			this._rooms.TryGetValue(pName, out room);
			if (room != null)
			{
				return room;
			}
			throw new TingTingException("Can't find room '" + pName + "' in Room runner");
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004EAC File Offset: 0x000030AC
		public Room GetRoomUnsafe(string pName)
		{
			Room room = null;
			this._rooms.TryGetValue(pName, out room);
			return room;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004ECC File Offset: 0x000030CC
		public bool HasRoom(string pName)
		{
			return this._rooms.ContainsKey(pName);
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00004EDC File Offset: 0x000030DC
		public IEnumerable<Room> rooms
		{
			get
			{
				return this._rooms.Values;
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00004EEC File Offset: 0x000030EC
		public void DestroyRoom(string pName)
		{
			Room room = this.GetRoom(pName);
			int objectId = room.objectId;
			this._roomTable.RemoveRowAt(objectId);
			this._rooms.Remove(pName);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004F24 File Offset: 0x00003124
		public void Reset()
		{
			foreach (Room room in this._rooms.Values)
			{
				room.Reset();
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00004F90 File Offset: 0x00003190
		public IEnumerable<string> Preload()
		{
			foreach (Room r in this._rooms.Values)
			{
				yield return "Setting up links and groups in room " + r.name;
				r.SetupLinks();
				r.SetupGroups();
			}
			yield break;
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00004FB4 File Offset: 0x000031B4
		public override string ToString()
		{
			return string.Format("RoomRunner ({0} rooms)", this._rooms.Count);
		}

		// Token: 0x0400004F RID: 79
		private TableTwo _roomTable;

		// Token: 0x04000050 RID: 80
		private Dictionary<string, Room> _rooms = new Dictionary<string, Room>();
	}
}
