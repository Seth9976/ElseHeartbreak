using System;
using System.Collections.Generic;
using System.Linq;
using GameTypes;
using Pathfinding;
using RelayLib;

namespace TingTing
{
	// Token: 0x0200000C RID: 12
	public class Room : RelayObjectTwo
	{
		// Token: 0x0600008F RID: 143 RVA: 0x00003B1C File Offset: 0x00001D1C
		protected override void SetupCells()
		{
			this.CELL_name = base.EnsureCell<string>("name", "undefined");
			this.CELL_exterior = base.EnsureCell<bool>("exterior", false);
			this.CELL_worldPosition = base.EnsureCell<IntPoint>("worldPosition", IntPoint.Zero);
			this.CELL_optiGrid = base.EnsureCell<string>("optiGrid", "");
			this.CELL_optiGridSize = base.EnsureCell<IntPoint>("optiGridSize", new IntPoint(0, 0));
			this.CELL_optiGridOffset = base.EnsureCell<IntPoint>("optiGridOffset", new IntPoint(0, 0));
			this.CELL_tiles = base.EnsureCell<IntPoint[]>("tiles", new IntPoint[0]);
			if (this.optiGrid == "")
			{
				IntPoint[] data = this.CELL_tiles.data;
				if (data.Length > 0)
				{
					this.SetTiles(data);
				}
			}
			else
			{
				this.LoadTilesFromOptigrid();
			}
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003C00 File Offset: 0x00001E00
		public void SetupLinks()
		{
			foreach (PointTileNode pointTileNode in this._tilesByLocalPositionHash.Values)
			{
				this.AddTileLinks(pointTileNode);
			}
			this.RefreshTileData();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003C74 File Offset: 0x00001E74
		public void SetupGroups()
		{
			Dictionary<int, PointTileNode>.ValueCollection values = this._tilesByLocalPositionHash.Values;
			int num = 0;
			foreach (PointTileNode pointTileNode in values)
			{
				if (pointTileNode.group == -1)
				{
					pointTileNode.group = num;
					List<PointTileNode> list = this.GetNeighbours(pointTileNode);
					while (list.Count > 0)
					{
						List<PointTileNode> list2 = new List<PointTileNode>();
						foreach (PointTileNode pointTileNode2 in list)
						{
							if (pointTileNode2.group == -1)
							{
								pointTileNode2.group = num;
								list2.AddRange(this.GetNeighbours(pointTileNode2));
							}
						}
						list = list2;
					}
					num++;
				}
			}
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003D98 File Offset: 0x00001F98
		private List<PointTileNode> GetNeighbours(PointTileNode pNode)
		{
			List<PointTileNode> list = new List<PointTileNode>();
			foreach (PathLink pathLink in pNode.links)
			{
				PointTileNode otherNode = pathLink.GetOtherNode(pNode);
				if (otherNode.group == -1)
				{
					list.Add(otherNode);
				}
			}
			return list;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003E1C File Offset: 0x0000201C
		private void RecursiveGroupSet(PointTileNode pNode, int pGroupId)
		{
			if (pNode.group != -1)
			{
				return;
			}
			pNode.group = pGroupId;
			foreach (PathLink pathLink in pNode.links)
			{
				this.RecursiveGroupSet(pathLink.GetOtherNode(pNode), pGroupId);
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00003EA0 File Offset: 0x000020A0
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00003EB0 File Offset: 0x000020B0
		public string name
		{
			get
			{
				return this.CELL_name.data;
			}
			set
			{
				this.CELL_name.data = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00003EC0 File Offset: 0x000020C0
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00003ED0 File Offset: 0x000020D0
		public bool exterior
		{
			get
			{
				return this.CELL_exterior.data;
			}
			set
			{
				this.CELL_exterior.data = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000098 RID: 152 RVA: 0x00003EE0 File Offset: 0x000020E0
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00003EF0 File Offset: 0x000020F0
		public string optiGrid
		{
			get
			{
				return this.CELL_optiGrid.data;
			}
			set
			{
				this.CELL_optiGrid.data = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003F00 File Offset: 0x00002100
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00003F10 File Offset: 0x00002110
		public IntPoint optiGridSize
		{
			get
			{
				return this.CELL_optiGridSize.data;
			}
			set
			{
				this.CELL_optiGridSize.data = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600009C RID: 156 RVA: 0x00003F20 File Offset: 0x00002120
		// (set) Token: 0x0600009D RID: 157 RVA: 0x00003F30 File Offset: 0x00002130
		public IntPoint optiGridOffset
		{
			get
			{
				return this.CELL_optiGridOffset.data;
			}
			set
			{
				this.CELL_optiGridOffset.data = value;
			}
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00003F40 File Offset: 0x00002140
		public void Reset()
		{
			foreach (PointTileNode pointTileNode in this._tilesByLocalPositionHash.Values)
			{
				pointTileNode.Reset();
			}
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003FAC File Offset: 0x000021AC
		public void SetTiles(IList<IntPoint> pPoints)
		{
			this._tilesByLocalPositionHash.Clear();
			foreach (IntPoint intPoint in pPoints)
			{
				PointTileNode pointTileNode = new PointTileNode(intPoint, this);
				this._tilesByLocalPositionHash[pointTileNode.localPoint.GetHashCode()] = pointTileNode;
			}
			this.ApplyTileData();
			this.UpdateBounds();
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000403C File Offset: 0x0000223C
		public void SetOptigrid(IList<IntPoint> pPoints)
		{
			this._tilePointsOptigridCache = null;
			Console.WriteLine("Will set optigrid for room " + this.name);
			if (pPoints.Count == 0)
			{
				this.optiGrid = "";
				this.optiGridSize = new IntPoint(0, 0);
				this.optiGridOffset = new IntPoint(0, 0);
				Console.WriteLine("No tiles in room " + this.name + ", will return");
				return;
			}
			int num = int.MaxValue;
			int num2 = int.MaxValue;
			int num3 = int.MinValue;
			int num4 = int.MinValue;
			foreach (IntPoint intPoint in pPoints)
			{
				if (intPoint.x < num)
				{
					num = intPoint.x;
				}
				if (intPoint.y < num2)
				{
					num2 = intPoint.y;
				}
				if (intPoint.x > num3)
				{
					num3 = intPoint.x;
				}
				if (intPoint.y > num4)
				{
					num4 = intPoint.y;
				}
			}
			IntPoint intPoint2 = new IntPoint(num, num2);
			IntPoint intPoint3 = new IntPoint(num3, num4);
			this.optiGridOffset = intPoint2;
			this.optiGridSize = intPoint3 - intPoint2 + new IntPoint(1, 1);
			int num5 = this.optiGridSize.x * this.optiGridSize.y;
			char[] array = new char[num5];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = '0';
			}
			int x = this.optiGridSize.x;
			foreach (IntPoint intPoint4 in pPoints)
			{
				int num6 = intPoint4.x - this.optiGridOffset.x;
				int num7 = intPoint4.y - this.optiGridOffset.y;
				int num8 = num7 * x + num6;
				array[num8] = '1';
			}
			this.optiGrid = new string(array);
			this.CELL_tiles.data = new IntPoint[0];
			this.LoadTilesFromOptigrid();
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000042B4 File Offset: 0x000024B4
		private void LoadTilesFromOptigrid()
		{
			this._tilesByLocalPositionHash.Clear();
			string optiGrid = this.optiGrid;
			int x = this.optiGridSize.x;
			int y = this.optiGridSize.y;
			for (int i = 0; i < y; i++)
			{
				for (int j = 0; j < x; j++)
				{
					int num = i * x + j;
					int num2 = j + this.optiGridOffset.x;
					int num3 = i + this.optiGridOffset.y;
					if (optiGrid[num] == '1')
					{
						PointTileNode pointTileNode = new PointTileNode(new IntPoint(num2, num3), this);
						this._tilesByLocalPositionHash[pointTileNode.localPoint.GetHashCode()] = pointTileNode;
					}
				}
			}
			this.UpdateBounds();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004394 File Offset: 0x00002594
		public void ApplyTileData()
		{
			if (this.optiGrid != "")
			{
				return;
			}
			this.CELL_tiles.data = (from PointTileNode n in this._tilesByLocalPositionHash.Values
				select n.localPoint).ToArray<IntPoint>();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000043FC File Offset: 0x000025FC
		private void AddTileLinks(PointTileNode tileNode)
		{
			int x = tileNode.localPoint.x;
			int y = tileNode.localPoint.y;
			Dictionary<int, PointTileNode> tilesByLocalPositionHash = this._tilesByLocalPositionHash;
			IntPoint intPoint = new IntPoint(x + 1, y);
			PointTileNode pointTileNode;
			if (tilesByLocalPositionHash.TryGetValue(intPoint.GetHashCode(), out pointTileNode))
			{
				this.ConnectNodes(tileNode, pointTileNode);
			}
			Dictionary<int, PointTileNode> tilesByLocalPositionHash2 = this._tilesByLocalPositionHash;
			IntPoint intPoint2 = new IntPoint(x - 1, y);
			if (tilesByLocalPositionHash2.TryGetValue(intPoint2.GetHashCode(), out pointTileNode))
			{
				this.ConnectNodes(tileNode, pointTileNode);
			}
			Dictionary<int, PointTileNode> tilesByLocalPositionHash3 = this._tilesByLocalPositionHash;
			IntPoint intPoint3 = new IntPoint(x, y + 1);
			if (tilesByLocalPositionHash3.TryGetValue(intPoint3.GetHashCode(), out pointTileNode))
			{
				this.ConnectNodes(tileNode, pointTileNode);
			}
			Dictionary<int, PointTileNode> tilesByLocalPositionHash4 = this._tilesByLocalPositionHash;
			IntPoint intPoint4 = new IntPoint(x, y - 1);
			if (tilesByLocalPositionHash4.TryGetValue(intPoint4.GetHashCode(), out pointTileNode))
			{
				this.ConnectNodes(tileNode, pointTileNode);
			}
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000044D4 File Offset: 0x000026D4
		private void RefreshTileData()
		{
			if (this.optiGrid != "")
			{
				return;
			}
			this.CELL_tiles.data = (from PointTileNode n in this._tilesByLocalPositionHash.Values
				select n.localPoint).ToArray<IntPoint>();
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000453C File Offset: 0x0000273C
		public void AddTile(PointTileNode pTileNode)
		{
			try
			{
				this._tilesByLocalPositionHash.Add(pTileNode.localPoint.GetHashCode(), pTileNode);
			}
			catch (Exception ex)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				D.Log(string.Concat(new object[]
				{
					"Could not add tileNode at: ",
					pTileNode.localPoint.ToString(),
					" hashcode ",
					pTileNode.GetHashCode(),
					" EXCEPTION: ",
					ex
				}));
				Console.ForegroundColor = ConsoleColor.White;
			}
			this.AddTileLinks(pTileNode);
			this.RefreshTileData();
			this.UpdateBounds();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000045F0 File Offset: 0x000027F0
		private void UpdateBounds()
		{
			this.localMinBoundrary = IntPoint.Max;
			this.localMaxBoundrary = IntPoint.Min;
			foreach (PointTileNode pointTileNode in this._tilesByLocalPositionHash.Values)
			{
				if (pointTileNode.localPoint.x > this.localMaxBoundrary.x)
				{
					this.localMaxBoundrary = new IntPoint(pointTileNode.localPoint.x, this.localMaxBoundrary.y);
				}
				if (pointTileNode.localPoint.y > this.localMaxBoundrary.y)
				{
					this.localMaxBoundrary = new IntPoint(this.localMaxBoundrary.x, pointTileNode.localPoint.y);
				}
				if (pointTileNode.localPoint.x < this.localMinBoundrary.x)
				{
					this.localMinBoundrary = new IntPoint(pointTileNode.localPoint.x, this.localMinBoundrary.y);
				}
				if (pointTileNode.localPoint.y < this.localMinBoundrary.y)
				{
					this.localMinBoundrary = new IntPoint(this.localMinBoundrary.x, pointTileNode.localPoint.y);
				}
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x0000477C File Offset: 0x0000297C
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00004784 File Offset: 0x00002984
		public IntPoint localMinBoundrary { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004790 File Offset: 0x00002990
		// (set) Token: 0x060000AA RID: 170 RVA: 0x00004798 File Offset: 0x00002998
		public IntPoint localMaxBoundrary { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000047A4 File Offset: 0x000029A4
		public IntPoint worldMinBoundrary
		{
			get
			{
				return this.worldPosition + this.localMinBoundrary;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000047B8 File Offset: 0x000029B8
		public IntPoint worldMaxBoundrary
		{
			get
			{
				return this.worldPosition + this.localMaxBoundrary;
			}
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000047CC File Offset: 0x000029CC
		public PointTileNode GetTile(IntPoint pPoint)
		{
			return this.GetTile(pPoint.x, pPoint.y);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000047E4 File Offset: 0x000029E4
		public PointTileNode GetTile(int x, int y)
		{
			PointTileNode pointTileNode = null;
			this._tilesByLocalPositionHash.TryGetValue(BitCruncher.PackTwoShorts(x, y), out pointTileNode);
			return pointTileNode;
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000480C File Offset: 0x00002A0C
		public PointTileNode[] tiles
		{
			get
			{
				return this._tilesByLocalPositionHash.Values.ToArray<PointTileNode>();
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004820 File Offset: 0x00002A20
		private float ManhattanDistance(IntPoint pPosition1, IntPoint pPosition2)
		{
			return (float)(Math.Abs(pPosition1.x - pPosition2.x) + Math.Abs(pPosition1.y - pPosition2.y));
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004858 File Offset: 0x00002A58
		public PointTileNode FindClosestTile(IntPoint pPosition)
		{
			PointTileNode pointTileNode = null;
			float num = float.MaxValue;
			foreach (PointTileNode pointTileNode2 in this._tilesByLocalPositionHash.Values)
			{
				float num2 = this.ManhattanDistance(pPosition, pointTileNode2.localPoint);
				if (num2 < num)
				{
					pointTileNode = pointTileNode2;
					num = num2;
				}
			}
			return pointTileNode;
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x000048E4 File Offset: 0x00002AE4
		public PointTileNode FindClosestFreeTile(IntPoint pPosition, int tileGroup)
		{
			IEnumerable<PointTileNode> enumerable = this._tilesByLocalPositionHash.Values.Where((PointTileNode t) => t.group == tileGroup);
			return enumerable.OrderBy((PointTileNode t) => this.ManhattanDistance(pPosition, t.localPoint)).First<PointTileNode>();
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004944 File Offset: 0x00002B44
		public IntPoint WorldToLocalPoint(IntPoint pSource)
		{
			return pSource - this.worldPosition;
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00004954 File Offset: 0x00002B54
		public IntPoint[] points
		{
			get
			{
				if (this.optiGrid == "")
				{
					return this.CELL_tiles.data;
				}
				if (this._tilePointsOptigridCache == null)
				{
					this._tilePointsOptigridCache = this._tilesByLocalPositionHash.Values.Select((PointTileNode t) => t.localPoint).ToArray<IntPoint>();
				}
				return this._tilePointsOptigridCache;
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000049CC File Offset: 0x00002BCC
		private void ConnectNodes(PointTileNode pA, PointTileNode pB)
		{
			if (pB.GetLinkTo(pA) == null)
			{
				PathLink pathLink = new PathLink(pA, pB);
				pathLink.distance = 1f;
				pA.AddLink(pathLink);
				pB.AddLink(pathLink);
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00004A18 File Offset: 0x00002C18
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x00004A08 File Offset: 0x00002C08
		public IntPoint worldPosition
		{
			get
			{
				return this.CELL_worldPosition.data;
			}
			set
			{
				this.CELL_worldPosition.data = value;
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00004A28 File Offset: 0x00002C28
		public List<Ting> GetTings()
		{
			List<Ting> list = new List<Ting>();
			foreach (PointTileNode pointTileNode in this._tilesByLocalPositionHash.Values)
			{
				list.AddRange(pointTileNode.GetOccupants());
			}
			return list;
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004AA4 File Offset: 0x00002CA4
		public List<T> GetTingsOfType<T>() where T : Ting
		{
			List<T> list = new List<T>();
			foreach (PointTileNode pointTileNode in this._tilesByLocalPositionHash.Values)
			{
				list.AddRange(pointTileNode.GetOccupantsOfType<T>());
			}
			return list;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00004B20 File Offset: 0x00002D20
		public bool HasTinyTileGroup(int pLimit)
		{
			Dictionary<int, int> dictionary = new Dictionary<int, int>();
			foreach (PointTileNode pointTileNode in this.tiles)
			{
				if (dictionary.ContainsKey(pointTileNode.group))
				{
					Dictionary<int, int> dictionary3;
					Dictionary<int, int> dictionary2 = (dictionary3 = dictionary);
					int num2;
					int num = (num2 = pointTileNode.group);
					num2 = dictionary3[num2];
					dictionary2[num] = num2 + 1;
				}
				else
				{
					dictionary[pointTileNode.group] = 1;
				}
			}
			foreach (int num3 in dictionary.Keys)
			{
				if (dictionary[num3] <= pLimit)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0400003E RID: 62
		public const string TABLE_NAME = "Rooms";

		// Token: 0x0400003F RID: 63
		internal Dictionary<int, PointTileNode> _tilesByLocalPositionHash = new Dictionary<int, PointTileNode>();

		// Token: 0x04000040 RID: 64
		private ValueEntry<string> CELL_name = null;

		// Token: 0x04000041 RID: 65
		private ValueEntry<IntPoint[]> CELL_tiles = null;

		// Token: 0x04000042 RID: 66
		private ValueEntry<bool> CELL_exterior = null;

		// Token: 0x04000043 RID: 67
		private ValueEntry<IntPoint> CELL_worldPosition = null;

		// Token: 0x04000044 RID: 68
		private ValueEntry<string> CELL_optiGrid;

		// Token: 0x04000045 RID: 69
		private ValueEntry<IntPoint> CELL_optiGridSize;

		// Token: 0x04000046 RID: 70
		private ValueEntry<IntPoint> CELL_optiGridOffset;

		// Token: 0x04000047 RID: 71
		private IntPoint[] _tilePointsOptigridCache;
	}
}
