using System;
using System.Text;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000016 RID: 22
	public class TdsConnectionInfo
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000D06C File Offset: 0x0000B26C
		public TdsConnectionInfo(string dataSource, int port, int packetSize, int timeout, int minSize, int maxSize)
		{
			this.DataSource = dataSource;
			this.Port = port;
			this.PacketSize = packetSize;
			this.Timeout = timeout;
			this.PoolMinSize = minSize;
			this.PoolMaxSize = maxSize;
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("DataSouce: {0}\n", this.DataSource);
			stringBuilder.AppendFormat("Port: {0}\n", this.Port);
			stringBuilder.AppendFormat("PacketSize: {0}\n", this.PacketSize);
			stringBuilder.AppendFormat("Timeout: {0}\n", this.Timeout);
			stringBuilder.AppendFormat("PoolMinSize: {0}\n", this.PoolMinSize);
			stringBuilder.AppendFormat("PoolMaxSize: {0}", this.PoolMaxSize);
			return stringBuilder.ToString();
		}

		// Token: 0x040000CD RID: 205
		public string DataSource;

		// Token: 0x040000CE RID: 206
		public int Port;

		// Token: 0x040000CF RID: 207
		public int PacketSize;

		// Token: 0x040000D0 RID: 208
		public int Timeout;

		// Token: 0x040000D1 RID: 209
		public int PoolMinSize;

		// Token: 0x040000D2 RID: 210
		public int PoolMaxSize;
	}
}
