using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using Mono.Security.Authenticode;

namespace System.Security.Policy
{
	/// <summary>Defines the set of information that constitutes input to security policy decisions. This class cannot be inherited.</summary>
	// Token: 0x0200063A RID: 1594
	[MonoTODO("Serialization format not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public sealed class Evidence : IEnumerable, ICollection
	{
		/// <summary>Initializes a new empty instance of the <see cref="T:System.Security.Policy.Evidence" /> class.</summary>
		// Token: 0x06003CB0 RID: 15536 RVA: 0x000D0954 File Offset: 0x000CEB54
		public Evidence()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Evidence" /> class from a shallow copy of an existing one.</summary>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> instance from which to create the new instance. This instance is not deep copied. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="evidence" /> parameter is not a valid instance of <see cref="T:System.Security.Policy.Evidence" />. </exception>
		// Token: 0x06003CB1 RID: 15537 RVA: 0x000D095C File Offset: 0x000CEB5C
		public Evidence(Evidence evidence)
		{
			if (evidence != null)
			{
				this.Merge(evidence);
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Evidence" /> class from multiple sets of host and assembly evidence.</summary>
		/// <param name="hostEvidence">The host evidence from which to create the new instance. </param>
		/// <param name="assemblyEvidence">The assembly evidence from which to create the new instance. </param>
		// Token: 0x06003CB2 RID: 15538 RVA: 0x000D0974 File Offset: 0x000CEB74
		public Evidence(object[] hostEvidence, object[] assemblyEvidence)
		{
			if (hostEvidence != null)
			{
				this.HostEvidenceList.AddRange(hostEvidence);
			}
			if (assemblyEvidence != null)
			{
				this.AssemblyEvidenceList.AddRange(assemblyEvidence);
			}
		}

		/// <summary>Gets the number of evidence objects in the evidence set.</summary>
		/// <returns>The number of evidence objects in the evidence set.</returns>
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x06003CB3 RID: 15539 RVA: 0x000D09AC File Offset: 0x000CEBAC
		public int Count
		{
			get
			{
				int num = 0;
				if (this.hostEvidenceList != null)
				{
					num += this.hostEvidenceList.Count;
				}
				if (this.assemblyEvidenceList != null)
				{
					num += this.assemblyEvidenceList.Count;
				}
				return num;
			}
		}

		/// <summary>Gets a value indicating whether the evidence set is read-only.</summary>
		/// <returns>Always false because read-only evidence sets are not supported.</returns>
		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x06003CB4 RID: 15540 RVA: 0x000D09F0 File Offset: 0x000CEBF0
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets a value indicating whether the evidence set is thread-safe.</summary>
		/// <returns>Always false because thread-safe evidence sets are not supported.</returns>
		// Token: 0x17000B81 RID: 2945
		// (get) Token: 0x06003CB5 RID: 15541 RVA: 0x000D09F4 File Offset: 0x000CEBF4
		public bool IsSynchronized
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets a value indicating whether the evidence is locked.</summary>
		/// <returns>true if the evidence is locked; otherwise, false. The default is false.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000B82 RID: 2946
		// (get) Token: 0x06003CB6 RID: 15542 RVA: 0x000D09F8 File Offset: 0x000CEBF8
		// (set) Token: 0x06003CB7 RID: 15543 RVA: 0x000D0A00 File Offset: 0x000CEC00
		public bool Locked
		{
			get
			{
				return this._locked;
			}
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlEvidence\"/>\n</PermissionSet>\n")]
			set
			{
				this._locked = value;
			}
		}

		/// <summary>Gets the synchronization root.</summary>
		/// <returns>Always this (Me in Visual Basic) because synchronization of evidence sets is not supported.</returns>
		// Token: 0x17000B83 RID: 2947
		// (get) Token: 0x06003CB8 RID: 15544 RVA: 0x000D0A0C File Offset: 0x000CEC0C
		public object SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000B84 RID: 2948
		// (get) Token: 0x06003CB9 RID: 15545 RVA: 0x000D0A10 File Offset: 0x000CEC10
		internal ArrayList HostEvidenceList
		{
			get
			{
				if (this.hostEvidenceList == null)
				{
					this.hostEvidenceList = ArrayList.Synchronized(new ArrayList());
				}
				return this.hostEvidenceList;
			}
		}

		// Token: 0x17000B85 RID: 2949
		// (get) Token: 0x06003CBA RID: 15546 RVA: 0x000D0A34 File Offset: 0x000CEC34
		internal ArrayList AssemblyEvidenceList
		{
			get
			{
				if (this.assemblyEvidenceList == null)
				{
					this.assemblyEvidenceList = ArrayList.Synchronized(new ArrayList());
				}
				return this.assemblyEvidenceList;
			}
		}

		/// <summary>Adds the specified assembly evidence to the evidence set.</summary>
		/// <param name="id">Any evidence object. </param>
		// Token: 0x06003CBB RID: 15547 RVA: 0x000D0A58 File Offset: 0x000CEC58
		public void AddAssembly(object id)
		{
			this.AssemblyEvidenceList.Add(id);
			this._hashCode = 0;
		}

		/// <summary>Adds the specified evidence supplied by the host to the evidence set.</summary>
		/// <param name="id">Any evidence object. </param>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="P:System.Security.Policy.Evidence.Locked" /> is true and the code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlEvidence" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003CBC RID: 15548 RVA: 0x000D0A70 File Offset: 0x000CEC70
		public void AddHost(object id)
		{
			if (this._locked && SecurityManager.SecurityEnabled)
			{
				new SecurityPermission(SecurityPermissionFlag.ControlEvidence).Demand();
			}
			this.HostEvidenceList.Add(id);
			this._hashCode = 0;
		}

		/// <summary>Removes the host and assembly evidence from the evidence set.</summary>
		// Token: 0x06003CBD RID: 15549 RVA: 0x000D0AB4 File Offset: 0x000CECB4
		[ComVisible(false)]
		public void Clear()
		{
			if (this.hostEvidenceList != null)
			{
				this.hostEvidenceList.Clear();
			}
			if (this.assemblyEvidenceList != null)
			{
				this.assemblyEvidenceList.Clear();
			}
			this._hashCode = 0;
		}

		/// <summary>Copies evidence objects to an <see cref="T:System.Array" />.</summary>
		/// <param name="array">The target array to which to copy evidence objects. </param>
		/// <param name="index">The zero-based position in the array to which to begin copying evidence objects. </param>
		// Token: 0x06003CBE RID: 15550 RVA: 0x000D0AEC File Offset: 0x000CECEC
		public void CopyTo(Array array, int index)
		{
			int num = 0;
			if (this.hostEvidenceList != null)
			{
				num = this.hostEvidenceList.Count;
				if (num > 0)
				{
					this.hostEvidenceList.CopyTo(array, index);
				}
			}
			if (this.assemblyEvidenceList != null && this.assemblyEvidenceList.Count > 0)
			{
				this.assemblyEvidenceList.CopyTo(array, index + num);
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Security.Policy.Evidence" /> object is equal to the current <see cref="T:System.Security.Policy.Evidence" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Security.Policy.Evidence" /> object is equal to the current <see cref="T:System.Security.Policy.Evidence" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.Security.Policy.Evidence" /> object to compare with the current <see cref="T:System.Security.Policy.Evidence" />. </param>
		// Token: 0x06003CBF RID: 15551 RVA: 0x000D0B54 File Offset: 0x000CED54
		[ComVisible(false)]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Evidence evidence = obj as Evidence;
			if (evidence == null)
			{
				return false;
			}
			if (this.HostEvidenceList.Count != evidence.HostEvidenceList.Count)
			{
				return false;
			}
			if (this.AssemblyEvidenceList.Count != evidence.AssemblyEvidenceList.Count)
			{
				return false;
			}
			for (int i = 0; i < this.hostEvidenceList.Count; i++)
			{
				bool flag = false;
				int j = 0;
				while (j < evidence.hostEvidenceList.Count)
				{
					if (this.hostEvidenceList[i].Equals(evidence.hostEvidenceList[j]))
					{
						flag = true;
						break;
					}
					i++;
				}
				if (!flag)
				{
					return false;
				}
			}
			for (int k = 0; k < this.assemblyEvidenceList.Count; k++)
			{
				bool flag2 = false;
				int l = 0;
				while (l < evidence.assemblyEvidenceList.Count)
				{
					if (this.assemblyEvidenceList[k].Equals(evidence.assemblyEvidenceList[l]))
					{
						flag2 = true;
						break;
					}
					k++;
				}
				if (!flag2)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Enumerates all evidence in the set, both that provided by the host and that provided by the assembly.</summary>
		/// <returns>An enumerator for evidence added by both the <see cref="M:System.Security.Policy.Evidence.AddHost(System.Object)" /> method and the <see cref="M:System.Security.Policy.Evidence.AddAssembly(System.Object)" /> method.</returns>
		// Token: 0x06003CC0 RID: 15552 RVA: 0x000D0C98 File Offset: 0x000CEE98
		public IEnumerator GetEnumerator()
		{
			IEnumerator enumerator = null;
			if (this.hostEvidenceList != null)
			{
				enumerator = this.hostEvidenceList.GetEnumerator();
			}
			IEnumerator enumerator2 = null;
			if (this.assemblyEvidenceList != null)
			{
				enumerator2 = this.assemblyEvidenceList.GetEnumerator();
			}
			return new Evidence.EvidenceEnumerator(enumerator, enumerator2);
		}

		/// <summary>Enumerates evidence provided by the assembly.</summary>
		/// <returns>An enumerator for evidence added by the <see cref="M:System.Security.Policy.Evidence.AddAssembly(System.Object)" /> method.</returns>
		// Token: 0x06003CC1 RID: 15553 RVA: 0x000D0CE0 File Offset: 0x000CEEE0
		public IEnumerator GetAssemblyEnumerator()
		{
			return this.AssemblyEvidenceList.GetEnumerator();
		}

		/// <summary>Gets a hash code for the <see cref="T:System.Security.Policy.Evidence" /> object that is suitable for use in hashing algorithms and data structures such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Security.Policy.Evidence" /> object.</returns>
		// Token: 0x06003CC2 RID: 15554 RVA: 0x000D0CF0 File Offset: 0x000CEEF0
		[ComVisible(false)]
		public override int GetHashCode()
		{
			if (this._hashCode == 0)
			{
				if (this.hostEvidenceList != null)
				{
					for (int i = 0; i < this.hostEvidenceList.Count; i++)
					{
						this._hashCode ^= this.hostEvidenceList[i].GetHashCode();
					}
				}
				if (this.assemblyEvidenceList != null)
				{
					for (int j = 0; j < this.assemblyEvidenceList.Count; j++)
					{
						this._hashCode ^= this.assemblyEvidenceList[j].GetHashCode();
					}
				}
			}
			return this._hashCode;
		}

		/// <summary>Enumerates evidence supplied by the host.</summary>
		/// <returns>An enumerator for evidence added by the <see cref="M:System.Security.Policy.Evidence.AddHost(System.Object)" /> method.</returns>
		// Token: 0x06003CC3 RID: 15555 RVA: 0x000D0D98 File Offset: 0x000CEF98
		public IEnumerator GetHostEnumerator()
		{
			return this.HostEvidenceList.GetEnumerator();
		}

		/// <summary>Merges the specified evidence set into the current evidence set.</summary>
		/// <param name="evidence">The evidence set to be merged into the current evidence set. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="evidence" /> parameter is not a valid instance of <see cref="T:System.Security.Policy.Evidence" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">
		///   <see cref="P:System.Security.Policy.Evidence.Locked" /> is true, the code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlEvidence" />, and the <paramref name="evidence" /> parameter has a host list that is not empty. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003CC4 RID: 15556 RVA: 0x000D0DA8 File Offset: 0x000CEFA8
		public void Merge(Evidence evidence)
		{
			if (evidence != null && evidence.Count > 0)
			{
				if (evidence.hostEvidenceList != null)
				{
					foreach (object obj in evidence.hostEvidenceList)
					{
						this.AddHost(obj);
					}
				}
				if (evidence.assemblyEvidenceList != null)
				{
					foreach (object obj2 in evidence.assemblyEvidenceList)
					{
						this.AddAssembly(obj2);
					}
				}
				this._hashCode = 0;
			}
		}

		/// <summary>Removes the evidence for a given type from the host and assembly enumerations.</summary>
		/// <param name="t">The <see cref="T:System.Type" /> of the evidence to be removed. </param>
		// Token: 0x06003CC5 RID: 15557 RVA: 0x000D0EA0 File Offset: 0x000CF0A0
		[ComVisible(false)]
		public void RemoveType(Type t)
		{
			for (int i = this.hostEvidenceList.Count; i >= 0; i--)
			{
				if (this.hostEvidenceList.GetType() == t)
				{
					this.hostEvidenceList.RemoveAt(i);
					this._hashCode = 0;
				}
			}
			for (int j = this.assemblyEvidenceList.Count; j >= 0; j--)
			{
				if (this.assemblyEvidenceList.GetType() == t)
				{
					this.assemblyEvidenceList.RemoveAt(j);
					this._hashCode = 0;
				}
			}
		}

		// Token: 0x06003CC6 RID: 15558
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAuthenticodePresent(Assembly a);

		// Token: 0x06003CC7 RID: 15559 RVA: 0x000D0F30 File Offset: 0x000CF130
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Unrestricted=\"true\"/>\n</PermissionSet>\n")]
		internal static Evidence GetDefaultHostEvidence(Assembly a)
		{
			Evidence evidence = new Evidence();
			string escapedCodeBase = a.EscapedCodeBase;
			evidence.AddHost(Zone.CreateFromUrl(escapedCodeBase));
			evidence.AddHost(new Url(escapedCodeBase));
			evidence.AddHost(new Hash(a));
			if (string.Compare("FILE://", 0, escapedCodeBase, 0, 7, true, CultureInfo.InvariantCulture) != 0)
			{
				evidence.AddHost(Site.CreateFromUrl(escapedCodeBase));
			}
			AssemblyName assemblyName = a.UnprotectedGetName();
			byte[] publicKey = assemblyName.GetPublicKey();
			if (publicKey != null && publicKey.Length > 0)
			{
				StrongNamePublicKeyBlob strongNamePublicKeyBlob = new StrongNamePublicKeyBlob(publicKey);
				evidence.AddHost(new StrongName(strongNamePublicKeyBlob, assemblyName.Name, assemblyName.Version));
			}
			if (Evidence.IsAuthenticodePresent(a))
			{
				AuthenticodeDeformatter authenticodeDeformatter = new AuthenticodeDeformatter(a.Location);
				if (authenticodeDeformatter.SigningCertificate != null)
				{
					X509Certificate x509Certificate = new X509Certificate(authenticodeDeformatter.SigningCertificate.RawData);
					if (x509Certificate.GetHashCode() != 0)
					{
						evidence.AddHost(new Publisher(x509Certificate));
					}
				}
			}
			if (a.GlobalAssemblyCache)
			{
				evidence.AddHost(new GacInstalled());
			}
			AppDomainManager domainManager = AppDomain.CurrentDomain.DomainManager;
			if (domainManager != null && (domainManager.HostSecurityManager.Flags & HostSecurityManagerOptions.HostAssemblyEvidence) == HostSecurityManagerOptions.HostAssemblyEvidence)
			{
				evidence = domainManager.HostSecurityManager.ProvideAssemblyEvidence(a, evidence);
			}
			return evidence;
		}

		// Token: 0x04001A69 RID: 6761
		private bool _locked;

		// Token: 0x04001A6A RID: 6762
		private ArrayList hostEvidenceList;

		// Token: 0x04001A6B RID: 6763
		private ArrayList assemblyEvidenceList;

		// Token: 0x04001A6C RID: 6764
		private int _hashCode;

		// Token: 0x0200063B RID: 1595
		private class EvidenceEnumerator : IEnumerator
		{
			// Token: 0x06003CC8 RID: 15560 RVA: 0x000D1070 File Offset: 0x000CF270
			public EvidenceEnumerator(IEnumerator hostenum, IEnumerator assemblyenum)
			{
				this.hostEnum = hostenum;
				this.assemblyEnum = assemblyenum;
				this.currentEnum = this.hostEnum;
			}

			// Token: 0x06003CC9 RID: 15561 RVA: 0x000D10A0 File Offset: 0x000CF2A0
			public bool MoveNext()
			{
				if (this.currentEnum == null)
				{
					return false;
				}
				bool flag = this.currentEnum.MoveNext();
				if (!flag && this.hostEnum == this.currentEnum && this.assemblyEnum != null)
				{
					this.currentEnum = this.assemblyEnum;
					flag = this.assemblyEnum.MoveNext();
				}
				return flag;
			}

			// Token: 0x06003CCA RID: 15562 RVA: 0x000D1104 File Offset: 0x000CF304
			public void Reset()
			{
				if (this.hostEnum != null)
				{
					this.hostEnum.Reset();
					this.currentEnum = this.hostEnum;
				}
				else
				{
					this.currentEnum = this.assemblyEnum;
				}
				if (this.assemblyEnum != null)
				{
					this.assemblyEnum.Reset();
				}
			}

			// Token: 0x17000B86 RID: 2950
			// (get) Token: 0x06003CCB RID: 15563 RVA: 0x000D115C File Offset: 0x000CF35C
			public object Current
			{
				get
				{
					return this.currentEnum.Current;
				}
			}

			// Token: 0x04001A6D RID: 6765
			private IEnumerator currentEnum;

			// Token: 0x04001A6E RID: 6766
			private IEnumerator hostEnum;

			// Token: 0x04001A6F RID: 6767
			private IEnumerator assemblyEnum;
		}
	}
}
