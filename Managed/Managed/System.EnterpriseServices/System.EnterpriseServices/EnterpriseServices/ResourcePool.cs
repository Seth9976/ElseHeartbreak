using System;

namespace System.EnterpriseServices
{
	/// <summary>Stores objects in the current transaction. This class cannot be inherited.</summary>
	// Token: 0x02000039 RID: 57
	public sealed class ResourcePool
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.ResourcePool" /> class.</summary>
		/// <param name="cb">A <see cref="T:System.EnterpriseServices.ResourcePool.TransactionEndDelegate" />, that is called when a transaction is finished. All items currently stored in the transaction are handed back to the user through the delegate. </param>
		// Token: 0x060000D2 RID: 210 RVA: 0x00002854 File Offset: 0x00000A54
		[MonoTODO]
		public ResourcePool(ResourcePool.TransactionEndDelegate cb)
		{
		}

		/// <summary>Gets a resource from the current transaction.</summary>
		/// <returns>The resource object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060000D3 RID: 211 RVA: 0x0000285C File Offset: 0x00000A5C
		[MonoTODO]
		public object GetResource()
		{
			throw new NotImplementedException();
		}

		/// <summary>Adds a resource to the current transaction.</summary>
		/// <returns>true if the resource object was added to the pool; otherwise, false.</returns>
		/// <param name="resource">The resource to add. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060000D4 RID: 212 RVA: 0x00002864 File Offset: 0x00000A64
		[MonoTODO]
		public bool PutResource(object resource)
		{
			throw new NotImplementedException();
		}

		/// <summary>Represents the method that handles the ending of a transaction.</summary>
		/// <param name="resource">The object that is passed back to the delegate. </param>
		// Token: 0x02000078 RID: 120
		// (Invoke) Token: 0x060001D1 RID: 465
		public delegate void TransactionEndDelegate(object resource);
	}
}
