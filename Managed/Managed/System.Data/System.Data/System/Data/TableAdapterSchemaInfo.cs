using System;
using System.Collections;
using System.Data.Common;

namespace System.Data
{
	// Token: 0x02000082 RID: 130
	internal class TableAdapterSchemaInfo
	{
		// Token: 0x06000660 RID: 1632 RVA: 0x0001F42C File Offset: 0x0001D62C
		public TableAdapterSchemaInfo(DbProviderFactory provider)
		{
			this.Provider = provider;
			this.Adapter = provider.CreateDataAdapter();
			this.Connection = provider.CreateConnection();
			this.Commands = new ArrayList();
			this.ShortCommands = false;
		}

		// Token: 0x06000661 RID: 1633 RVA: 0x0001F468 File Offset: 0x0001D668
		public TableAdapterSchemaInfo()
		{
			this.Commands = new ArrayList();
			this.ShortCommands = false;
		}

		// Token: 0x04000245 RID: 581
		public DbProviderFactory Provider;

		// Token: 0x04000246 RID: 582
		public DbDataAdapter Adapter;

		// Token: 0x04000247 RID: 583
		public DbConnection Connection;

		// Token: 0x04000248 RID: 584
		public string ConnectionString;

		// Token: 0x04000249 RID: 585
		public string BaseClass;

		// Token: 0x0400024A RID: 586
		public string Name;

		// Token: 0x0400024B RID: 587
		public bool ShortCommands;

		// Token: 0x0400024C RID: 588
		public ArrayList Commands;
	}
}
