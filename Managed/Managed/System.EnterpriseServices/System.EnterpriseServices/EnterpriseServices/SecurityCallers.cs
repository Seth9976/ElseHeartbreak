using System;
using System.Collections;

namespace System.EnterpriseServices
{
	/// <summary>Provides an ordered collection of identities in the current call chain.</summary>
	// Token: 0x0200003C RID: 60
	public sealed class SecurityCallers : IEnumerable
	{
		// Token: 0x060000E1 RID: 225 RVA: 0x000028CC File Offset: 0x00000ACC
		internal SecurityCallers()
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000028D4 File Offset: 0x00000AD4
		internal SecurityCallers(ISecurityCallersColl collection)
		{
		}

		/// <summary>Gets the number of callers in the chain.</summary>
		/// <returns>The number of callers in the chain.</returns>
		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x000028DC File Offset: 0x00000ADC
		public int Count
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the specified <see cref="T:System.EnterpriseServices.SecurityIdentity" /> item.</summary>
		/// <returns>A <see cref="T:System.EnterpriseServices.SecurityIdentity" /> object.</returns>
		/// <param name="idx">The item to access using an index number. </param>
		// Token: 0x17000045 RID: 69
		public SecurityIdentity this[int idx]
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Retrieves the enumeration interface for the object.</summary>
		/// <returns>The enumerator interface for the ISecurityCallersColl collection.</returns>
		// Token: 0x060000E5 RID: 229 RVA: 0x000028EC File Offset: 0x00000AEC
		[MonoTODO]
		public IEnumerator GetEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
