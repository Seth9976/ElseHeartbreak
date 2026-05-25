using System;
using System.Collections.Generic;
using System.Reflection;
using GameTypes;
using RelayLib;

namespace TingTing
{
	// Token: 0x02000006 RID: 6
	public class TingRunner
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003278 File Offset: 0x00001478
		public TingRunner(RelayTwo pRelay, RoomRunner pRoomRunner)
		{
			D.isNull(pRelay);
			D.isNull(pRoomRunner);
			this._roomRunner = pRoomRunner;
			this._relay = pRelay;
			Type[] subclasses = InstantiatorTwo.GetSubclasses(typeof(Ting));
			List<Ting> list = new List<Ting>();
			foreach (Type type in subclasses)
			{
				TableTwo tableTwo = this.AssertTable(type, pRelay);
				if (!this._loadedTingTables.ContainsKey(tableTwo.name))
				{
					list.AddRange(InstantiatorTwo.Process<Ting>(tableTwo));
					this._loadedTingTables.Add(tableTwo.name, tableTwo);
				}
			}
			foreach (Ting ting in list)
			{
				this.AddTing(ting);
				ting.SetupBaseRunners(this, this._roomRunner);
			}
			this.actionTime = 0f;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000033E8 File Offset: 0x000015E8
		// (set) Token: 0x0600006B RID: 107 RVA: 0x000033F0 File Offset: 0x000015F0
		public GameTime gameClock { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000033FC File Offset: 0x000015FC
		// (set) Token: 0x0600006D RID: 109 RVA: 0x00003404 File Offset: 0x00001604
		public float actionTime { get; private set; }

		// Token: 0x0600006E RID: 110 RVA: 0x00003410 File Offset: 0x00001610
		private void AddTing(Ting t)
		{
			if (!this._tings.ContainsKey(t.name))
			{
				this._tings.Add(t.name, t);
				this._newTingsThatShouldGetUpdated.Add(t);
				return;
			}
			throw new TingDuplicateException(" can't have two tings with the same name: " + t.name);
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000346C File Offset: 0x0000166C
		private string GetTableName(Type t)
		{
			FieldInfo field = t.GetField("TABLE_NAME", BindingFlags.Static | BindingFlags.Public);
			if (field == null)
			{
				return Ting.TABLE_NAME;
			}
			return (string)field.GetValue(null);
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000034A0 File Offset: 0x000016A0
		private TableTwo AssertTable(Type t, RelayTwo pRelay)
		{
			string tableName = this.GetTableName(t);
			if (!pRelay.tables.ContainsKey(tableName))
			{
				pRelay.CreateTable(tableName);
			}
			return pRelay.tables[tableName];
		}

		// Token: 0x06000071 RID: 113 RVA: 0x000034DC File Offset: 0x000016DC
		protected T CreateTingWithoutAddingItToList<T>(string pName, WorldCoordinate pPosition, Direction pDirection, string pPrefabName) where T : Ting
		{
			Type typeFromHandle = typeof(T);
			TableTwo tableTwo = this.AssertTable(typeFromHandle, this._relay);
			if (!this._loadedTingTables.ContainsKey(tableTwo.name))
			{
				this._loadedTingTables.Add(tableTwo.name, tableTwo);
			}
			T t = Activator.CreateInstance(typeFromHandle) as T;
			t.SetupBaseRunners(this, this._roomRunner);
			t.SetInitCreateValues(pName, pPosition, pDirection);
			t.CreateNewRelayEntry(tableTwo, typeFromHandle.Name);
			t.prefab = pPrefabName;
			if (this.onTingHasNewRoom != null)
			{
				this.onTingHasNewRoom(t, t.position.roomName);
			}
			return t;
		}

		// Token: 0x06000072 RID: 114 RVA: 0x000035B4 File Offset: 0x000017B4
		public virtual T CreateTing<T>(string pName, WorldCoordinate pPosition, Direction pDirection) where T : Ting
		{
			T t = this.CreateTingWithoutAddingItToList<T>(pName, pPosition, pDirection, "");
			this.AddTing(t);
			return t;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000035E0 File Offset: 0x000017E0
		public virtual T CreateTing<T>(string pName, WorldCoordinate pPosition, Direction pDirection, string pPrefabName) where T : Ting
		{
			T t = this.CreateTingWithoutAddingItToList<T>(pName, pPosition, pDirection, pPrefabName);
			this.AddTing(t);
			return t;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003608 File Offset: 0x00001808
		public T CreateTing<T>(string pName, WorldCoordinate pWorldCoordinate) where T : Ting
		{
			return this.CreateTing<T>(pName, pWorldCoordinate, Direction.RIGHT);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x00003614 File Offset: 0x00001814
		public virtual T CreateTingAfterUpdate<T>(string pName, WorldCoordinate pWorldCoordinate, Direction pDirection, string pPrefabName) where T : Ting
		{
			T t = this.CreateTingWithoutAddingItToList<T>(pName, pWorldCoordinate, pDirection, pPrefabName);
			this._tingsToAddAfterUpdate.Add(t);
			return t;
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003640 File Offset: 0x00001840
		public Ting GetTing(string pName)
		{
			Ting tingUnsafe = this.GetTingUnsafe(pName);
			if (tingUnsafe != null)
			{
				return tingUnsafe;
			}
			throw new CantFindTingException("Can't find Ting with name " + pName + " in TingRunner");
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003674 File Offset: 0x00001874
		public TingType GetTing<TingType>(string pName) where TingType : Ting
		{
			return this.GetTing(pName) as TingType;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003688 File Offset: 0x00001888
		public Ting GetTingUnsafe(string pName)
		{
			Ting tingThatWillBeAdded;
			this._tings.TryGetValue(pName, out tingThatWillBeAdded);
			if (tingThatWillBeAdded == null)
			{
				tingThatWillBeAdded = this.GetTingThatWillBeAdded(pName);
			}
			return tingThatWillBeAdded;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000036B4 File Offset: 0x000018B4
		private Ting GetTingThatWillBeAdded(string pName)
		{
			return this._tingsToAddAfterUpdate.Find((Ting t) => t.name == pName);
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000036E8 File Offset: 0x000018E8
		public Ting[] GetTingsInRoom(string pRoomName)
		{
			Room room = this._roomRunner.GetRoom(pRoomName);
			return room.GetTings().ToArray();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003710 File Offset: 0x00001910
		public TingType[] GetTingsOfType<TingType>() where TingType : Ting
		{
			List<TingType> list = new List<TingType>();
			foreach (Ting ting in this._tings.Values)
			{
				if (ting is TingType)
				{
					list.Add(ting as TingType);
				}
			}
			return list.ToArray();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x000037A0 File Offset: 0x000019A0
		public TingType[] GetTingsOfTypeInRoom<TingType>(string pRoomName) where TingType : Ting
		{
			if (!this._roomRunner.HasRoom(pRoomName))
			{
				throw new Exception("Can't find room with name " + pRoomName);
			}
			Room room = this._roomRunner.GetRoom(pRoomName);
			return room.GetTingsOfType<TingType>().ToArray();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x000037E8 File Offset: 0x000019E8
		public bool HasTing(string pName)
		{
			return this._tings.ContainsKey(pName);
		}

		// Token: 0x0600007E RID: 126 RVA: 0x000037F8 File Offset: 0x000019F8
		public void RemoveTing(string pName)
		{
			Ting ting = this.GetTing(pName);
			this._tingsThatShouldGetUpdated.Remove(ting);
			ting.table.RemoveRowAt(ting.objectId);
			ting.isDeleted = true;
			this._tings.Remove(pName);
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003840 File Offset: 0x00001A40
		public void RemoveTingAfterUpdate(string pName)
		{
			this._tingsToRemoveAfterUpdate.Add(pName);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003850 File Offset: 0x00001A50
		public IEnumerable<Ting> GetTings()
		{
			return this._tings.Values;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003860 File Offset: 0x00001A60
		public void Update(float dt, GameTime pGameClock, float pActionTime)
		{
			this._tingsThatShouldGetUpdated.AddRange(this._newTingsThatShouldGetUpdated);
			this._newTingsThatShouldGetUpdated.Clear();
			foreach (Ting ting in this._tingsThatShouldStopGetUpdated)
			{
				this._tingsThatShouldGetUpdated.Remove(ting);
			}
			this._tingsThatShouldStopGetUpdated.Clear();
			this.gameClock = pGameClock;
			this.actionTime = pActionTime;
			foreach (Ting ting2 in this._tingsThatShouldGetUpdated)
			{
				ting2.Update(dt);
				ting2.UpdateAction(pActionTime);
			}
			foreach (Ting ting3 in this._tingsToAddAfterUpdate)
			{
				this.AddTing(ting3);
			}
			this._tingsToAddAfterUpdate.Clear();
			foreach (string text in this._tingsToRemoveAfterUpdate)
			{
				this.RemoveTing(text);
			}
			this._tingsToRemoveAfterUpdate.Clear();
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003A2C File Offset: 0x00001C2C
		public override string ToString()
		{
			return string.Format("TingRunner ({0} tings)", this._tings.Count);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000083 RID: 131 RVA: 0x00003A48 File Offset: 0x00001C48
		public IEnumerable<string> loadedTingTables
		{
			get
			{
				return this._loadedTingTables.Keys;
			}
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003A58 File Offset: 0x00001C58
		public void Register(Ting pRegisterThisTing)
		{
			if (this._tings.ContainsKey(pRegisterThisTing.name))
			{
				return;
			}
			if (this._newTingsThatShouldGetUpdated.Contains(pRegisterThisTing))
			{
				return;
			}
			this._newTingsThatShouldGetUpdated.Add(pRegisterThisTing);
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003A9C File Offset: 0x00001C9C
		public void Unregister(Ting pUnregisterThisTing)
		{
			if (this._tingsThatShouldStopGetUpdated.Contains(pUnregisterThisTing))
			{
				return;
			}
			this._tingsThatShouldStopGetUpdated.Add(pUnregisterThisTing);
		}

		// Token: 0x0400002D RID: 45
		public TingRunner.OnNewRoom onTingHasNewRoom;

		// Token: 0x0400002E RID: 46
		protected Dictionary<string, Ting> _tings = new Dictionary<string, Ting>();

		// Token: 0x0400002F RID: 47
		protected RoomRunner _roomRunner = null;

		// Token: 0x04000030 RID: 48
		private RelayTwo _relay = null;

		// Token: 0x04000031 RID: 49
		private Dictionary<string, TableTwo> _loadedTingTables = new Dictionary<string, TableTwo>();

		// Token: 0x04000032 RID: 50
		private List<Ting> _tingsToAddAfterUpdate = new List<Ting>();

		// Token: 0x04000033 RID: 51
		private List<string> _tingsToRemoveAfterUpdate = new List<string>();

		// Token: 0x04000034 RID: 52
		private List<Ting> _tingsThatShouldGetUpdated = new List<Ting>();

		// Token: 0x04000035 RID: 53
		private List<Ting> _newTingsThatShouldGetUpdated = new List<Ting>();

		// Token: 0x04000036 RID: 54
		private List<Ting> _tingsThatShouldStopGetUpdated = new List<Ting>();

		// Token: 0x02000007 RID: 7
		// (Invoke) Token: 0x06000087 RID: 135
		public delegate void OnNewRoom(Ting pTing, string pNewRoomName);
	}
}
