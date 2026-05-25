using System;
using System.Diagnostics.SymbolStore;

namespace System.Reflection.Emit
{
	// Token: 0x020002E3 RID: 739
	internal class SequencePointList
	{
		// Token: 0x060025BB RID: 9659 RVA: 0x00085BDC File Offset: 0x00083DDC
		public SequencePointList(ISymbolDocumentWriter doc)
		{
			this.doc = doc;
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x060025BC RID: 9660 RVA: 0x00085BEC File Offset: 0x00083DEC
		public ISymbolDocumentWriter Document
		{
			get
			{
				return this.doc;
			}
		}

		// Token: 0x060025BD RID: 9661 RVA: 0x00085BF4 File Offset: 0x00083DF4
		public int[] GetOffsets()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Offset;
			}
			return array;
		}

		// Token: 0x060025BE RID: 9662 RVA: 0x00085C3C File Offset: 0x00083E3C
		public int[] GetLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Line;
			}
			return array;
		}

		// Token: 0x060025BF RID: 9663 RVA: 0x00085C84 File Offset: 0x00083E84
		public int[] GetColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].Col;
			}
			return array;
		}

		// Token: 0x060025C0 RID: 9664 RVA: 0x00085CCC File Offset: 0x00083ECC
		public int[] GetEndLines()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndLine;
			}
			return array;
		}

		// Token: 0x060025C1 RID: 9665 RVA: 0x00085D14 File Offset: 0x00083F14
		public int[] GetEndColumns()
		{
			int[] array = new int[this.count];
			for (int i = 0; i < this.count; i++)
			{
				array[i] = this.points[i].EndCol;
			}
			return array;
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x060025C2 RID: 9666 RVA: 0x00085D5C File Offset: 0x00083F5C
		public int StartLine
		{
			get
			{
				return this.points[0].Line;
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x060025C3 RID: 9667 RVA: 0x00085D70 File Offset: 0x00083F70
		public int EndLine
		{
			get
			{
				return this.points[this.count - 1].Line;
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x060025C4 RID: 9668 RVA: 0x00085D8C File Offset: 0x00083F8C
		public int StartColumn
		{
			get
			{
				return this.points[0].Col;
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x060025C5 RID: 9669 RVA: 0x00085DA0 File Offset: 0x00083FA0
		public int EndColumn
		{
			get
			{
				return this.points[this.count - 1].Col;
			}
		}

		// Token: 0x060025C6 RID: 9670 RVA: 0x00085DBC File Offset: 0x00083FBC
		public void AddSequencePoint(int offset, int line, int col, int endLine, int endCol)
		{
			SequencePoint sequencePoint = default(SequencePoint);
			sequencePoint.Offset = offset;
			sequencePoint.Line = line;
			sequencePoint.Col = col;
			sequencePoint.EndLine = endLine;
			sequencePoint.EndCol = endCol;
			if (this.points == null)
			{
				this.points = new SequencePoint[10];
			}
			else if (this.count >= this.points.Length)
			{
				SequencePoint[] array = new SequencePoint[this.count + 10];
				Array.Copy(this.points, array, this.points.Length);
				this.points = array;
			}
			this.points[this.count] = sequencePoint;
			this.count++;
		}

		// Token: 0x04000E30 RID: 3632
		private const int arrayGrow = 10;

		// Token: 0x04000E31 RID: 3633
		private ISymbolDocumentWriter doc;

		// Token: 0x04000E32 RID: 3634
		private SequencePoint[] points;

		// Token: 0x04000E33 RID: 3635
		private int count;
	}
}
