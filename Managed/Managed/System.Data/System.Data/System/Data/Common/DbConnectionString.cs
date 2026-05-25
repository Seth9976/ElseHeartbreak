using System;
using System.Collections.Specialized;
using System.Runtime.Serialization;

namespace System.Data.Common
{
	// Token: 0x020000BC RID: 188
	[Obsolete]
	internal class DbConnectionString : DbConnectionOptions, ISerializable
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x0002C24C File Offset: 0x0002A44C
		protected internal DbConnectionString(DbConnectionString constr)
		{
			this.options = constr.options;
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x0002C260 File Offset: 0x0002A460
		public DbConnectionString(string connectionString)
			: base(connectionString)
		{
			this.options = new NameValueCollection();
			base.ParseConnectionString(connectionString);
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x0002C27C File Offset: 0x0002A47C
		[MonoTODO]
		protected DbConnectionString(SerializationInfo si, StreamingContext sc)
		{
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x0002C284 File Offset: 0x0002A484
		[MonoTODO]
		public DbConnectionString(string connectionString, string restrictions, KeyRestrictionBehavior behavior)
			: this(connectionString)
		{
			this.behavior = behavior;
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x0002C294 File Offset: 0x0002A494
		public KeyRestrictionBehavior Behavior
		{
			get
			{
				return this.behavior;
			}
		}

		// Token: 0x17000196 RID: 406
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x0002C29C File Offset: 0x0002A49C
		[MonoTODO]
		public string Restrictions
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x0002C2A4 File Offset: 0x0002A4A4
		[MonoTODO]
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x0002C2AC File Offset: 0x0002A4AC
		protected virtual string KeywordLookup(string keyname)
		{
			return keyname;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x0002C2B0 File Offset: 0x0002A4B0
		[MonoTODO]
		public virtual void PermissionDemand()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000324 RID: 804
		private KeyRestrictionBehavior behavior;
	}
}
