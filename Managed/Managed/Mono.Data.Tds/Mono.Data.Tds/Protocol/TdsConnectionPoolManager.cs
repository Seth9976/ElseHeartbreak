using System;
using System.Collections;

namespace Mono.Data.Tds.Protocol
{
	// Token: 0x02000015 RID: 21
	public class TdsConnectionPoolManager
	{
		// Token: 0x0600013D RID: 317 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public TdsConnectionPoolManager(TdsVersion version)
		{
			this.version = version;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x0000CF4C File Offset: 0x0000B14C
		public TdsConnectionPool GetConnectionPool(string connectionString, TdsConnectionInfo info)
		{
			TdsConnectionPool tdsConnectionPool = (TdsConnectionPool)this.pools[connectionString];
			if (tdsConnectionPool == null)
			{
				this.pools[connectionString] = new TdsConnectionPool(this, info);
				tdsConnectionPool = (TdsConnectionPool)this.pools[connectionString];
			}
			return tdsConnectionPool;
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000CF98 File Offset: 0x0000B198
		public TdsConnectionPool GetConnectionPool(string connectionString)
		{
			return (TdsConnectionPool)this.pools[connectionString];
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000CFAC File Offset: 0x0000B1AC
		public virtual Tds CreateConnection(TdsConnectionInfo info)
		{
			TdsVersion tdsVersion = this.version;
			if (tdsVersion == TdsVersion.tds42)
			{
				return new Tds42(info.DataSource, info.Port, info.PacketSize, info.Timeout);
			}
			if (tdsVersion == TdsVersion.tds50)
			{
				return new Tds50(info.DataSource, info.Port, info.PacketSize, info.Timeout);
			}
			if (tdsVersion == TdsVersion.tds70)
			{
				return new Tds70(info.DataSource, info.Port, info.PacketSize, info.Timeout);
			}
			if (tdsVersion != TdsVersion.tds80)
			{
				throw new NotSupportedException();
			}
			return new Tds80(info.DataSource, info.Port, info.PacketSize, info.Timeout);
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000D064 File Offset: 0x0000B264
		public IDictionary GetConnectionPool()
		{
			return this.pools;
		}

		// Token: 0x040000CB RID: 203
		private Hashtable pools = Hashtable.Synchronized(new Hashtable());

		// Token: 0x040000CC RID: 204
		private TdsVersion version;
	}
}
