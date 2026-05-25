using System;
using System.IO;

namespace System.Data.SqlClient
{
	// Token: 0x02000177 RID: 375
	internal sealed class SqlXmlTextReader : TextReader, IDisposable
	{
		// Token: 0x06001422 RID: 5154 RVA: 0x00054574 File Offset: 0x00052774
		internal SqlXmlTextReader(SqlDataReader reader)
		{
			this.reader = reader;
		}

		// Token: 0x06001423 RID: 5155 RVA: 0x00054590 File Offset: 0x00052790
		void IDisposable.Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x000545A0 File Offset: 0x000527A0
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x000545B0 File Offset: 0x000527B0
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.Close();
					((IDisposable)this.reader).Dispose();
				}
				this.disposed = true;
			}
		}

		// Token: 0x06001426 RID: 5158 RVA: 0x000545DC File Offset: 0x000527DC
		private bool GetNextBuffer()
		{
			if (this.eof)
			{
				this.localBuffer = null;
				return false;
			}
			this.position = 0;
			if (this.reader.Read())
			{
				this.localBuffer = this.reader.GetString(0);
			}
			else if (this.reader.NextResult() && this.reader.Read())
			{
				this.localBuffer = this.reader.GetString(0);
			}
			else
			{
				this.eof = true;
				this.localBuffer = "</results>";
			}
			return true;
		}

		// Token: 0x06001427 RID: 5159 RVA: 0x00054678 File Offset: 0x00052878
		public override int Peek()
		{
			if ((this.localBuffer == null || this.localBuffer.Length == 0) && !this.GetNextBuffer())
			{
				return -1;
			}
			if (this.eof && this.position >= this.localBuffer.Length)
			{
				return -1;
			}
			return (int)this.localBuffer[this.position];
		}

		// Token: 0x06001428 RID: 5160 RVA: 0x000546E4 File Offset: 0x000528E4
		public override int Read()
		{
			int num = this.Peek();
			this.position++;
			if (!this.eof && this.position >= this.localBuffer.Length)
			{
				this.GetNextBuffer();
			}
			return num;
		}

		// Token: 0x06001429 RID: 5161 RVA: 0x00054730 File Offset: 0x00052930
		public override int Read(char[] buffer, int index, int count)
		{
			bool flag = true;
			int num = 0;
			if (this.localBuffer == null)
			{
				flag = this.GetNextBuffer();
			}
			while (flag && count - num > this.localBuffer.Length - this.position)
			{
				this.localBuffer.CopyTo(this.position, buffer, index + num, this.localBuffer.Length);
				num += this.localBuffer.Length;
				flag = this.GetNextBuffer();
			}
			if (flag && num < count)
			{
				this.localBuffer.CopyTo(this.position, buffer, index + num, count - num);
				this.position += count - num;
			}
			return num;
		}

		// Token: 0x0600142A RID: 5162 RVA: 0x000547E4 File Offset: 0x000529E4
		public override int ReadBlock(char[] buffer, int index, int count)
		{
			return this.Read(buffer, index, count);
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x000547F0 File Offset: 0x000529F0
		public override string ReadLine()
		{
			bool flag = true;
			if (this.localBuffer == null)
			{
				flag = this.GetNextBuffer();
			}
			if (!flag)
			{
				return null;
			}
			string text = this.localBuffer;
			this.GetNextBuffer();
			return text;
		}

		// Token: 0x0600142C RID: 5164 RVA: 0x00054828 File Offset: 0x00052A28
		public override string ReadToEnd()
		{
			string text = string.Empty;
			bool flag = true;
			if (this.localBuffer == null)
			{
				flag = this.GetNextBuffer();
			}
			while (flag)
			{
				text += this.localBuffer;
				flag = this.GetNextBuffer();
			}
			return text;
		}

		// Token: 0x04000809 RID: 2057
		private bool disposed;

		// Token: 0x0400080A RID: 2058
		private bool eof;

		// Token: 0x0400080B RID: 2059
		private SqlDataReader reader;

		// Token: 0x0400080C RID: 2060
		private string localBuffer = "<results>";

		// Token: 0x0400080D RID: 2061
		private int position;
	}
}
