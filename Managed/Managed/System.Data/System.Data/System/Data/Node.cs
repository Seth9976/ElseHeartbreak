using System;

namespace System.Data
{
	// Token: 0x02000065 RID: 101
	internal class Node
	{
		// Token: 0x06000626 RID: 1574 RVA: 0x0001F020 File Offset: 0x0001D220
		public Node(DataRow row)
		{
			this._row = row;
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001F030 File Offset: 0x0001D230
		internal int GetBalance()
		{
			if (this._iBalance == -2)
			{
				throw new SystemException("Node is deleted.");
			}
			return this._iBalance;
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001F050 File Offset: 0x0001D250
		internal void Delete()
		{
			this._iBalance = -2;
			this._nLeft = null;
			this._nRight = null;
			this._nParent = null;
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000629 RID: 1577 RVA: 0x0001F070 File Offset: 0x0001D270
		internal DataRow Row
		{
			get
			{
				return this._row;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0001F078 File Offset: 0x0001D278
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0001F098 File Offset: 0x0001D298
		internal Node Left
		{
			get
			{
				if (this._iBalance == -2)
				{
					throw new SystemException("Node is deleted.");
				}
				return this._nLeft;
			}
			set
			{
				if (this._iBalance == -2)
				{
					throw new SystemException("Node is deleted.");
				}
				this._nLeft = value;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001F0BC File Offset: 0x0001D2BC
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0001F0DC File Offset: 0x0001D2DC
		internal Node Right
		{
			get
			{
				if (this._iBalance == -2)
				{
					throw new SystemException("Node is deleted.");
				}
				return this._nRight;
			}
			set
			{
				if (this._iBalance == -2)
				{
					throw new SystemException("Node is deleted.");
				}
				this._nRight = value;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0001F100 File Offset: 0x0001D300
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0001F120 File Offset: 0x0001D320
		internal Node Parent
		{
			get
			{
				if (this._iBalance == -2)
				{
					throw new SystemException("Node is deleted.");
				}
				return this._nParent;
			}
			set
			{
				if (this._iBalance == -2)
				{
					throw new SystemException("Node is deleted.");
				}
				this._nParent = value;
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001F144 File Offset: 0x0001D344
		internal bool IsRoot()
		{
			return this._nParent == null;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0001F150 File Offset: 0x0001D350
		internal void SetBalance(int b)
		{
			if (this._iBalance == -2)
			{
				throw new SystemException("Node is deleted.");
			}
			this._iBalance = b;
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x0001F174 File Offset: 0x0001D374
		internal bool From()
		{
			if (this.IsRoot())
			{
				return true;
			}
			if (this._iBalance == -2)
			{
				throw new SystemException("Node is deleted.");
			}
			Node parent = this.Parent;
			return this.Equals(parent.Left);
		}

		// Token: 0x06000633 RID: 1587 RVA: 0x0001F1BC File Offset: 0x0001D3BC
		internal object[] GetData()
		{
			if (this._iBalance == -2)
			{
				throw new SystemException("Node is deleted.");
			}
			return this._row.ItemArray;
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001F1E4 File Offset: 0x0001D3E4
		internal bool Equals(Node n)
		{
			if (this._iBalance == -2)
			{
				throw new SystemException("Node is deleted.");
			}
			return n == this;
		}

		// Token: 0x040001EE RID: 494
		protected int _iBalance;

		// Token: 0x040001EF RID: 495
		internal Node _nNext;

		// Token: 0x040001F0 RID: 496
		protected Node _nLeft;

		// Token: 0x040001F1 RID: 497
		protected Node _nRight;

		// Token: 0x040001F2 RID: 498
		protected Node _nParent;

		// Token: 0x040001F3 RID: 499
		protected DataRow _row;
	}
}
