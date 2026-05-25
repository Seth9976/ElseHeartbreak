using System;

namespace Newtonsoft.Json.Serialization
{
	// Token: 0x0200007C RID: 124
	internal struct ResolverContractKey : IEquatable<ResolverContractKey>
	{
		// Token: 0x06000601 RID: 1537 RVA: 0x000150A4 File Offset: 0x000132A4
		public ResolverContractKey(Type resolverType, Type contractType)
		{
			this._resolverType = resolverType;
			this._contractType = contractType;
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x000150B4 File Offset: 0x000132B4
		public override int GetHashCode()
		{
			return this._resolverType.GetHashCode() ^ this._contractType.GetHashCode();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000150CD File Offset: 0x000132CD
		public override bool Equals(object obj)
		{
			return obj is ResolverContractKey && this.Equals((ResolverContractKey)obj);
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x000150E5 File Offset: 0x000132E5
		public bool Equals(ResolverContractKey other)
		{
			return this._resolverType == other._resolverType && this._contractType == other._contractType;
		}

		// Token: 0x0400019B RID: 411
		private readonly Type _resolverType;

		// Token: 0x0400019C RID: 412
		private readonly Type _contractType;
	}
}
