using System;
using System.Collections.Generic;
using GameTypes;
using RelayLib;

namespace TingTing
{
	// Token: 0x02000004 RID: 4
	public abstract class Ting : RelayObjectTwo
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000025 RID: 37 RVA: 0x0000285C File Offset: 0x00000A5C
		// (set) Token: 0x06000026 RID: 38 RVA: 0x00002864 File Offset: 0x00000A64
		public bool isDeleted { get; set; }

		// Token: 0x06000027 RID: 39 RVA: 0x00002870 File Offset: 0x00000A70
		internal void SetInitCreateValues(string pName, WorldCoordinate pPosition, Direction pDirection)
		{
			this._startingName = pName;
			this._startingPosition = pPosition;
			this._startingDirection = pDirection;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002888 File Offset: 0x00000A88
		public override int GetHashCode()
		{
			return this.CELL_name.data.GetHashCode();
		}

		// Token: 0x06000029 RID: 41 RVA: 0x0000289C File Offset: 0x00000A9C
		public virtual void Update(float dt)
		{
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000028A0 File Offset: 0x00000AA0
		protected void UnregisterFromUpdateLoop()
		{
			this._tingRunner.Unregister(this);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000028B0 File Offset: 0x00000AB0
		public virtual void FixBeforeSaving()
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000028B4 File Offset: 0x00000AB4
		protected override void SetupCells()
		{
			this.CELL_name = base.EnsureCell<string>("name", this._startingName);
			this.CELL_position = base.EnsureCell<WorldCoordinate>("position", this._startingPosition);
			this.CELL_direction = base.EnsureCell<Direction>("direction", this._startingDirection);
			this.CELL_dialogueLine = base.EnsureCell<string>("dialogueLine", "");
			this.CELL_actionName = base.EnsureCell<string>("action", "");
			this.CELL_actionHasFired = base.EnsureCell<bool>("actionHasFired", false);
			this.CELL_actionStartTime = base.EnsureCell<float>("startTime", 0f);
			this.CELL_actionTriggerTime = base.EnsureCell<float>("triggerTime", 0f);
			this.CELL_actionEndTime = base.EnsureCell<float>("endTime", 0f);
			this.CELL_actionOtherObjectName = base.EnsureCell<string>("otherObjectName", "");
			this.CELL_prefab = base.EnsureCell<string>("prefab", "unspecified");
			this.CELL_isBeingHeld = base.EnsureCell<bool>("isBeingHeld", false);
			this._dialogueLineIsEmpty_Cache = this.CELL_dialogueLine.data == "";
			this._actionName_Cache = this.CELL_actionName.data;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000029F0 File Offset: 0x00000BF0
		public virtual bool CanInteractWith(Ting pTingToInteractWith)
		{
			return false;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000029F4 File Offset: 0x00000BF4
		public virtual void InteractWith(Ting pTingToInteractWith)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000029FC File Offset: 0x00000BFC
		protected virtual void ActionTriggered(Ting pOtherTing)
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A00 File Offset: 0x00000C00
		public void UpdateAction(float pTime)
		{
			if (this.actionName != "")
			{
				if (!this.actionHasFired && pTime >= this.actionTriggerTime)
				{
					this.actionHasFired = true;
					this.ActionTriggered(this.actionOtherObject);
				}
				if (pTime > this.actionEndTime)
				{
					this.StopAction();
				}
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A60 File Offset: 0x00000C60
		public void ForceTriggerCurrentAction()
		{
			this.actionTriggerTime = this._tingRunner.actionTime;
			this.UpdateAction(0f);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002A80 File Offset: 0x00000C80
		public void StartAction(string pActionName, Ting pOtherObject, float pLengthUntilTrigger, float pActionLength)
		{
			string actionName = this.actionName;
			this.actionName = pActionName;
			float actionTime = this._tingRunner.actionTime;
			this.actionStartTime = actionTime;
			this.actionEndTime = actionTime + pActionLength;
			this.actionTriggerTime = actionTime + pLengthUntilTrigger;
			this.actionHasFired = false;
			this.actionOtherObject = pOtherObject;
			if (this.onNewAction != null)
			{
				this.onNewAction(actionName, pActionName);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002AE8 File Offset: 0x00000CE8
		public void StopAction()
		{
			string actionName = this.actionName;
			this.actionName = "";
			this.actionOtherObject = null;
			if (this.onNewAction != null)
			{
				this.onNewAction(actionName, "");
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B2C File Offset: 0x00000D2C
		public override string ToString()
		{
			return this.name;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002B34 File Offset: 0x00000D34
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002B44 File Offset: 0x00000D44
		public string name
		{
			get
			{
				return this.CELL_name.data;
			}
			private set
			{
				this.CELL_name.data = value;
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B54 File Offset: 0x00000D54
		public bool HasInteractionPointHere(WorldCoordinate finalTargetPosition)
		{
			if (this.room.name == finalTargetPosition.roomName)
			{
				foreach (IntPoint intPoint in this.interactionPoints)
				{
					if (finalTargetPosition.localPosition == intPoint)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002BBC File Offset: 0x00000DBC
		// (set) Token: 0x06000039 RID: 57 RVA: 0x00002BCC File Offset: 0x00000DCC
		[ShowInEditor]
		public WorldCoordinate position
		{
			get
			{
				return this.CELL_position.data;
			}
			set
			{
				string roomName = this.CELL_position.data.roomName;
				if (!this._roomRunner.HasRoom(value.roomName))
				{
					throw new WorldCoordinateException("Can't place a ting in a undefined room: " + value.roomName);
				}
				if (this._isOccupyingTile)
				{
					this.DisconnectFromCurrentTile();
				}
				this.CELL_position.data = value;
				this.ConnectToCurrentTile();
				this.SetCachedTile();
				if (roomName != this.CELL_position.data.roomName && this._tingRunner.onTingHasNewRoom != null)
				{
					this._tingRunner.onTingHasNewRoom(this, value.roomName);
				}
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002C8C File Offset: 0x00000E8C
		protected void DisconnectFromCurrentTile()
		{
			this.room.GetTile(this.position.localPosition).RemoveOccupant(this);
			this._isOccupyingTile = false;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002CC0 File Offset: 0x00000EC0
		protected void ConnectToCurrentTile()
		{
			this.SetCachedRoom();
			PointTileNode tile = this.room.GetTile(this.position.localPosition);
			if (tile != null)
			{
				tile.AddOccupant(this);
				this._isOccupyingTile = true;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002D08 File Offset: 0x00000F08
		public Room room
		{
			get
			{
				if (this._cachedRoom == null)
				{
					this.SetCachedRoom();
				}
				return this._cachedRoom;
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002D24 File Offset: 0x00000F24
		private void SetCachedRoom()
		{
			this._cachedRoom = this._roomRunner.GetRoom(this.CELL_position.data.roomName);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002D58 File Offset: 0x00000F58
		public PointTileNode tile
		{
			get
			{
				if (!this._hasSetCachedTile)
				{
					this.SetCachedTile();
				}
				return this._cachedTile;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002D74 File Offset: 0x00000F74
		[ShowInEditor]
		public int tileGroupJustForDebuggingView
		{
			get
			{
				if (this.tile != null)
				{
					return this.tile.group;
				}
				return -666;
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D94 File Offset: 0x00000F94
		protected void SetCachedTile()
		{
			this._cachedTile = this.room.GetTile(this.localPoint);
			this._hasSetCachedTile = true;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public IntPoint localPoint
		{
			get
			{
				return this.CELL_position.data.localPosition;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public IntPoint worldPoint
		{
			get
			{
				return this.room.worldPosition + this.localPoint;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000043 RID: 67 RVA: 0x00002DF8 File Offset: 0x00000FF8
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002E08 File Offset: 0x00001008
		[ShowInEditor]
		public Direction direction
		{
			get
			{
				return this.CELL_direction.data;
			}
			set
			{
				this.CELL_direction.data = value;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002E18 File Offset: 0x00001018
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00002E28 File Offset: 0x00001028
		[ShowInEditor]
		public string dialogueLine
		{
			get
			{
				return this.CELL_dialogueLine.data;
			}
			set
			{
				this.CELL_dialogueLine.data = value;
				this._dialogueLineIsEmpty_Cache = value == "";
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002E48 File Offset: 0x00001048
		// (set) Token: 0x06000048 RID: 72 RVA: 0x00002E50 File Offset: 0x00001050
		[EditableInEditor]
		public string actionName
		{
			get
			{
				return this._actionName_Cache;
			}
			set
			{
				this.CELL_actionName.data = value;
				this._actionName_Cache = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002E68 File Offset: 0x00001068
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00002E78 File Offset: 0x00001078
		[ShowInEditor]
		public bool actionHasFired
		{
			get
			{
				return this.CELL_actionHasFired.data;
			}
			set
			{
				this.CELL_actionHasFired.data = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002E88 File Offset: 0x00001088
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00002E98 File Offset: 0x00001098
		public float actionStartTime
		{
			get
			{
				return this.CELL_actionStartTime.data;
			}
			set
			{
				this.CELL_actionStartTime.data = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002EA8 File Offset: 0x000010A8
		// (set) Token: 0x0600004E RID: 78 RVA: 0x00002EB8 File Offset: 0x000010B8
		public float actionTriggerTime
		{
			get
			{
				return this.CELL_actionTriggerTime.data;
			}
			set
			{
				this.CELL_actionTriggerTime.data = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002EC8 File Offset: 0x000010C8
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00002ED8 File Offset: 0x000010D8
		public float actionEndTime
		{
			get
			{
				return this.CELL_actionEndTime.data;
			}
			set
			{
				this.CELL_actionEndTime.data = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002EE8 File Offset: 0x000010E8
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00002EF8 File Offset: 0x000010F8
		[EditableInEditor]
		public string prefab
		{
			get
			{
				return this.CELL_prefab.data;
			}
			set
			{
				this.CELL_prefab.data = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002F08 File Offset: 0x00001108
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00002F18 File Offset: 0x00001118
		[ShowInEditor]
		public bool isBeingHeld
		{
			get
			{
				return this.CELL_isBeingHeld.data;
			}
			set
			{
				this.CELL_isBeingHeld.data = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00002F28 File Offset: 0x00001128
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00002F68 File Offset: 0x00001168
		[ShowInEditor]
		public Ting actionOtherObject
		{
			get
			{
				if (this.CELL_actionOtherObjectName.data == "")
				{
					return null;
				}
				return this._tingRunner.GetTing(this.CELL_actionOtherObjectName.data);
			}
			set
			{
				if (value == null)
				{
					this.CELL_actionOtherObjectName.data = "";
				}
				else
				{
					this.CELL_actionOtherObjectName.data = value.name;
				}
			}
		}

		// Token: 0x06000057 RID: 87 RVA: 0x00002FA4 File Offset: 0x000011A4
		internal void SetupBaseRunners(TingRunner pTingRunner, RoomRunner pRoomRunner)
		{
			this._roomRunner = pRoomRunner;
			this._tingRunner = pTingRunner;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002FB4 File Offset: 0x000011B4
		public virtual bool canBePickedUp
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00002FB8 File Offset: 0x000011B8
		public virtual IntPoint[] interactionPoints
		{
			get
			{
				return new IntPoint[]
				{
					this.localPoint + IntPoint.Down,
					this.localPoint + IntPoint.Up,
					this.localPoint + IntPoint.Left,
					this.localPoint + IntPoint.Right
				};
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600005A RID: 90 RVA: 0x0000303C File Offset: 0x0000123C
		public virtual IntPoint[] interactionPointsTryTheseFirst
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003040 File Offset: 0x00001240
		[ShowInEditor]
		public virtual bool isBeingUsed
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600005C RID: 92 RVA: 0x00003044 File Offset: 0x00001244
		public bool AtLeastOneInteractionPointIsOccupied()
		{
			if (this.room == null)
			{
				D.Log("Room of " + this.name + " is null, can't check for occupied interaction points.");
				return false;
			}
			if (this.interactionPoints.Length == 0)
			{
				D.Log("Length of interactionPoints[] " + this.name + " is 0, can't check for occupied interaction points.");
				return false;
			}
			PointTileNode tile = this.room.GetTile(this.interactionPoints[0]);
			if (tile == null)
			{
				D.Log("No tile at interaction point, can't check for occupied interaction points.");
				return false;
			}
			return tile.HasOccupants();
		}

		// Token: 0x0600005D RID: 93 RVA: 0x000030D8 File Offset: 0x000012D8
		public bool HasNoFreeInteractionPoints()
		{
			bool flag = true;
			foreach (IntPoint intPoint in this.interactionPoints)
			{
				PointTileNode tile = this.room.GetTile(intPoint);
				if (tile != null)
				{
					if (!tile.HasOccupants())
					{
						flag = false;
						break;
					}
				}
			}
			return flag;
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00003140 File Offset: 0x00001340
		public bool AnotherTingSharesTheTile()
		{
			return this.room != null && this.tile != null && this.tile.HasOccupants(this);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003174 File Offset: 0x00001374
		[ShowInEditor]
		public string occupantsOnTile
		{
			get
			{
				if (this.tile == null)
				{
					return "Not on a tile";
				}
				Ting[] occupants = this.tile.GetOccupants();
				List<string> list = new List<string>();
				foreach (Ting ting in occupants)
				{
					list.Add(ting.name);
				}
				return string.Join(",", list.ToArray());
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000060 RID: 96 RVA: 0x000031DC File Offset: 0x000013DC
		public virtual string tooltipName
		{
			get
			{
				return "Ting";
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000061 RID: 97 RVA: 0x000031E4 File Offset: 0x000013E4
		public virtual string verbDescription
		{
			get
			{
				return "Use [NAME]";
			}
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000031EC File Offset: 0x000013EC
		public virtual string UseTingOnTingDescription(Ting pOtherTing)
		{
			return "use " + this.tooltipName + " on " + pOtherTing.tooltipName;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003214 File Offset: 0x00001414
		[ShowInEditor]
		public float actionPercentage
		{
			get
			{
				float num = (this._tingRunner.actionTime - this.actionStartTime) / (this.actionEndTime - this.actionStartTime);
				if (num < 0f)
				{
					num = 0f;
				}
				if (num > 1f)
				{
					num = 1f;
				}
				return num;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00003268 File Offset: 0x00001468
		protected GameTime gameClock
		{
			get
			{
				return this._tingRunner.gameClock;
			}
		}

		// Token: 0x04000010 RID: 16
		public static readonly string TABLE_NAME = "Ting_Base";

		// Token: 0x04000011 RID: 17
		public Logger logger = new Logger();

		// Token: 0x04000012 RID: 18
		protected TingRunner _tingRunner;

		// Token: 0x04000013 RID: 19
		protected RoomRunner _roomRunner;

		// Token: 0x04000014 RID: 20
		private bool _isOccupyingTile = false;

		// Token: 0x04000015 RID: 21
		public Ting.OnNewAction onNewAction;

		// Token: 0x04000016 RID: 22
		private WorldCoordinate _startingPosition = WorldCoordinate.NONE;

		// Token: 0x04000017 RID: 23
		private Direction _startingDirection = Direction.RIGHT;

		// Token: 0x04000018 RID: 24
		private string _startingName = "unnamed";

		// Token: 0x04000019 RID: 25
		private ValueEntry<string> CELL_name;

		// Token: 0x0400001A RID: 26
		private ValueEntry<WorldCoordinate> CELL_position;

		// Token: 0x0400001B RID: 27
		private ValueEntry<Direction> CELL_direction;

		// Token: 0x0400001C RID: 28
		private ValueEntry<string> CELL_dialogueLine;

		// Token: 0x0400001D RID: 29
		private ValueEntry<string> CELL_actionName;

		// Token: 0x0400001E RID: 30
		private ValueEntry<bool> CELL_actionHasFired;

		// Token: 0x0400001F RID: 31
		private ValueEntry<float> CELL_actionStartTime;

		// Token: 0x04000020 RID: 32
		private ValueEntry<float> CELL_actionTriggerTime;

		// Token: 0x04000021 RID: 33
		private ValueEntry<float> CELL_actionEndTime;

		// Token: 0x04000022 RID: 34
		private ValueEntry<string> CELL_actionOtherObjectName;

		// Token: 0x04000023 RID: 35
		private ValueEntry<string> CELL_prefab;

		// Token: 0x04000024 RID: 36
		private ValueEntry<bool> CELL_isBeingHeld;

		// Token: 0x04000025 RID: 37
		protected bool _dialogueLineIsEmpty_Cache;

		// Token: 0x04000026 RID: 38
		protected string _actionName_Cache;

		// Token: 0x04000027 RID: 39
		public string lastConversation = "";

		// Token: 0x04000028 RID: 40
		private Room _cachedRoom = null;

		// Token: 0x04000029 RID: 41
		private PointTileNode _cachedTile = null;

		// Token: 0x0400002A RID: 42
		private bool _hasSetCachedTile = false;

		// Token: 0x0400002B RID: 43
		private IntPoint _worldPointCache = IntPoint.Zero;

		// Token: 0x02000005 RID: 5
		// (Invoke) Token: 0x06000066 RID: 102
		public delegate void OnNewAction(string pOldAction, string pNewAction);
	}
}
