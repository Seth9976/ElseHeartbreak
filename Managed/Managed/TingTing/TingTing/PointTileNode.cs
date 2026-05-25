using System;
using System.Collections.Generic;
using System.Linq;
using GameTypes;
using Pathfinding;

namespace TingTing
{
	// Token: 0x02000003 RID: 3
	public class PointTileNode
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020EC File Offset: 0x000002EC
		public PointTileNode(IntPoint pLocalPoint, Room r)
		{
			this.room = r;
			this.group = -1;
			this.links = new List<PathLink>(5);
			this.localPoint = pLocalPoint;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002128 File Offset: 0x00000328
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002130 File Offset: 0x00000330
		public Room room { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x0000213C File Offset: 0x0000033C
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002144 File Offset: 0x00000344
		public int group { get; set; }

		// Token: 0x06000007 RID: 7 RVA: 0x00002150 File Offset: 0x00000350
		public void Reset()
		{
			this.distanceToGoal = 0f;
			this.isGoalNode = false;
			this.isStartNode = false;
			this.linkLeadingHere = null;
			this.pathCostHere = 0f;
			this.visited = false;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002190 File Offset: 0x00000390
		public override bool Equals(object obj)
		{
			if (!(obj is PointTileNode))
			{
				return false;
			}
			PointTileNode pointTileNode = obj as PointTileNode;
			return this.room == pointTileNode.room && this.group == pointTileNode.group && this._target == pointTileNode._target && this._occupants == pointTileNode._occupants;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021F8 File Offset: 0x000003F8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002200 File Offset: 0x00000400
		public override string ToString()
		{
			return string.Format("[{2} ({0}, {1}, group {3})]", new object[]
			{
				this.localPoint.x,
				this.localPoint.y,
				this.room.name,
				this.group
			});
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000B RID: 11 RVA: 0x00002260 File Offset: 0x00000460
		public WorldCoordinate position
		{
			get
			{
				return new WorldCoordinate(this.room.name, this.localPoint);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002278 File Offset: 0x00000478
		public IntPoint worldPoint
		{
			get
			{
				return this.room.worldPosition + this.localPoint;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000E RID: 14 RVA: 0x0000237C File Offset: 0x0000057C
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002290 File Offset: 0x00000490
		public PointTileNode teleportTarget
		{
			get
			{
				return this._target;
			}
			set
			{
				if (value == this)
				{
					throw new ArgumentException("Can't set target to self");
				}
				if (this.links != null)
				{
					for (int i = this.links.Count - 1; i >= 0; i--)
					{
						PathLink pathLink = this.links[i];
						PointTileNode otherNode = pathLink.GetOtherNode(this);
						if (otherNode == value)
						{
							return;
						}
					}
					for (int j = this.links.Count - 1; j >= 0; j--)
					{
						PathLink pathLink2 = this.links[j];
						PointTileNode otherNode2 = pathLink2.GetOtherNode(this);
						if (otherNode2 != null && otherNode2.room != this.room)
						{
							this.RemoveLink(pathLink2);
						}
					}
				}
				if (value != null)
				{
					PathLink pathLink3 = value.GetLinkTo(this);
					if (pathLink3 == null)
					{
						pathLink3 = new PathLink(this, value);
					}
					this.AddLink(pathLink3);
				}
				this._target = value;
			}
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002384 File Offset: 0x00000584
		public void AddLink(PathLink pLink)
		{
			this.links.Add(pLink);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002394 File Offset: 0x00000594
		public void RemoveLink(PathLink pLink)
		{
			this.links.Remove(pLink);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023A4 File Offset: 0x000005A4
		public void RemoveAllLinks()
		{
			this.links.Clear();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023B4 File Offset: 0x000005B4
		public PathLink GetLinkTo(PointTileNode pNode)
		{
			if (this.links != null)
			{
				foreach (PathLink pathLink in this.links)
				{
					if (pathLink.Contains(pNode))
					{
						return pathLink;
					}
				}
			}
			return null;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002438 File Offset: 0x00000638
		public bool isIsolated()
		{
			return this.links.Count == 0;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002448 File Offset: 0x00000648
		public virtual float DistanceTo(PointTileNode pPoint)
		{
			if (pPoint is PointTileNode)
			{
				return this.localPoint.EuclidianDistanceTo(pPoint.localPoint);
			}
			throw new NotImplementedException();
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000247C File Offset: 0x0000067C
		public int CompareTo(object obj)
		{
			PointTileNode pointTileNode = obj as PointTileNode;
			this.targetValue = pointTileNode.pathCostHere + pointTileNode.distanceToGoal;
			float num = this.pathCostHere + this.distanceToGoal;
			if (this.targetValue > num)
			{
				return 1;
			}
			if (this.targetValue == num)
			{
				return 0;
			}
			return -1;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024D0 File Offset: 0x000006D0
		public virtual long GetUniqueID()
		{
			return BitCruncher.PackTwoInts(this.localPoint.x, this.localPoint.y);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000024F0 File Offset: 0x000006F0
		public void AddOccupant(Ting pTing)
		{
			this.EnsureOccapantList();
			this._occupants.Add(pTing);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002504 File Offset: 0x00000704
		public void RemoveOccupant(Ting pTing)
		{
			if (this._occupants != null && this._occupants.Contains(pTing))
			{
				this._occupants.Remove(pTing);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002530 File Offset: 0x00000730
		public bool HasOccupants()
		{
			return this._occupants != null && this._occupants.Count > 0;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002550 File Offset: 0x00000750
		public bool HasOccupants(Ting pIgnoreThisTing)
		{
			return this._occupants != null && this._occupants.Count != 0 && (this._occupants.Count != 1 || this._occupants[0] != pIgnoreThisTing);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000025A4 File Offset: 0x000007A4
		public bool HasOccupants<T>(Ting pIgnoreThisTing)
		{
			if (this._occupants == null || this._occupants.Count == 0)
			{
				return false;
			}
			foreach (Ting ting in this._occupants)
			{
				if (ting != pIgnoreThisTing)
				{
					if (ting.GetType() == typeof(T))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002650 File Offset: 0x00000850
		public bool HasOccupantsButIgnoreSomeTypes(Type[] pTypesToIgnore)
		{
			if (this._occupants == null || this._occupants.Count == 0)
			{
				return false;
			}
			foreach (Ting ting in this._occupants)
			{
				if (!pTypesToIgnore.Contains(ting.GetType()))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026EC File Offset: 0x000008EC
		public Ting[] GetOccupants()
		{
			if (this._occupants == null)
			{
				return new Ting[0];
			}
			return this._occupants.ToArray();
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000270C File Offset: 0x0000090C
		public T GetOccupantOfType<T>() where T : Ting
		{
			foreach (Ting ting in this.GetOccupants())
			{
				if (ting.GetType() == typeof(T))
				{
					return ting as T;
				}
			}
			return (T)((object)null);
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002760 File Offset: 0x00000960
		public IEnumerable<T> GetOccupantsOfType<T>() where T : Ting
		{
			foreach (Ting t in this.GetOccupants())
			{
				if (t.GetType() == typeof(T))
				{
					yield return t as T;
				}
			}
			yield break;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00002784 File Offset: 0x00000984
		private void EnsureOccapantList()
		{
			if (this._occupants == null)
			{
				this._occupants = new List<Ting>();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000279C File Offset: 0x0000099C
		public string GetOccupantsAsString()
		{
			return string.Join(", ", (from o in this.GetOccupants()
				select o.ToString()).ToArray<string>());
		}

		// Token: 0x04000001 RID: 1
		private List<Ting> _occupants;

		// Token: 0x04000002 RID: 2
		private PointTileNode _target = null;

		// Token: 0x04000003 RID: 3
		public List<PathLink> links;

		// Token: 0x04000004 RID: 4
		public IntPoint localPoint;

		// Token: 0x04000005 RID: 5
		public float pathCostHere;

		// Token: 0x04000006 RID: 6
		public float distanceToGoal;

		// Token: 0x04000007 RID: 7
		public float targetValue;

		// Token: 0x04000008 RID: 8
		public bool isStartNode;

		// Token: 0x04000009 RID: 9
		public bool isGoalNode;

		// Token: 0x0400000A RID: 10
		public bool visited;

		// Token: 0x0400000B RID: 11
		public PathLink linkLeadingHere;

		// Token: 0x0400000C RID: 12
		public float baseCost;
	}
}
