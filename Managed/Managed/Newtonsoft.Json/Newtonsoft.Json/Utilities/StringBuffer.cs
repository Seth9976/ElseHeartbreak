using System;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000B9 RID: 185
	internal class StringBuffer
	{
		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000834 RID: 2100 RVA: 0x0001DC0C File Offset: 0x0001BE0C
		// (set) Token: 0x06000835 RID: 2101 RVA: 0x0001DC14 File Offset: 0x0001BE14
		public int Position
		{
			get
			{
				return this._position;
			}
			set
			{
				this._position = value;
			}
		}

		// Token: 0x06000836 RID: 2102 RVA: 0x0001DC1D File Offset: 0x0001BE1D
		public StringBuffer()
		{
			this._buffer = StringBuffer._emptyBuffer;
		}

		// Token: 0x06000837 RID: 2103 RVA: 0x0001DC30 File Offset: 0x0001BE30
		public StringBuffer(int initalSize)
		{
			this._buffer = new char[initalSize];
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x0001DC44 File Offset: 0x0001BE44
		public void Append(char value)
		{
			if (this._position == this._buffer.Length)
			{
				this.EnsureSize(1);
			}
			this._buffer[this._position++] = value;
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x0001DC81 File Offset: 0x0001BE81
		public void Clear()
		{
			this._buffer = StringBuffer._emptyBuffer;
			this._position = 0;
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x0001DC98 File Offset: 0x0001BE98
		private void EnsureSize(int appendLength)
		{
			char[] array = new char[(this._position + appendLength) * 2];
			Array.Copy(this._buffer, array, this._position);
			this._buffer = array;
		}

		// Token: 0x0600083B RID: 2107 RVA: 0x0001DCCE File Offset: 0x0001BECE
		public override string ToString()
		{
			return this.ToString(0, this._position);
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x0001DCDD File Offset: 0x0001BEDD
		public string ToString(int start, int length)
		{
			return new string(this._buffer, start, length);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x0001DCEC File Offset: 0x0001BEEC
		public char[] GetInternalBuffer()
		{
			return this._buffer;
		}

		// Token: 0x0400029C RID: 668
		private char[] _buffer;

		// Token: 0x0400029D RID: 669
		private int _position;

		// Token: 0x0400029E RID: 670
		private static readonly char[] _emptyBuffer = new char[0];
	}
}
