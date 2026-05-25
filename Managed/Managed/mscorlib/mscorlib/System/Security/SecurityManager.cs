using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Policy;
using System.Text;

namespace System.Security
{
	/// <summary>Provides the main access point for classes interacting with the security system. This class cannot be inherited.</summary>
	// Token: 0x0200054A RID: 1354
	[ComVisible(true)]
	public static class SecurityManager
	{
		/// <summary>Gets or sets a value indicating whether code must have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> in order to execute.</summary>
		/// <returns>true if code must have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Execution" /> in order to execute; otherwise, false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x0600355C RID: 13660
		// (set) Token: 0x0600355D RID: 13661
		public static extern bool CheckExecutionRights
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		/// <summary>Gets or sets a value indicating whether security is enabled.</summary>
		/// <returns>true if security is enabled; otherwise, false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />. </exception>
		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600355E RID: 13662
		// (set) Token: 0x0600355F RID: 13663
		[Obsolete("The security manager cannot be turned off on MS runtime")]
		public static extern bool SecurityEnabled
		{
			[MethodImpl(MethodImplOptions.InternalCall)]
			get;
			[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
			[MethodImpl(MethodImplOptions.InternalCall)]
			set;
		}

		/// <summary>Gets the granted zone identity and URL identity permission sets for the current assembly.</summary>
		/// <param name="zone">An output parameter that contains a <see cref="T:System.Collections.ArrayList" /> of granted <see cref="P:System.Security.Permissions.ZoneIdentityPermissionAttribute.Zone" /> objects.</param>
		/// <param name="origin">An output parameter that contains a <see cref="T:System.Collections.ArrayList" /> of granted <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> objects.</param>
		/// <exception cref="T:System.Security.SecurityException">The request for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> failed.</exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PublicKeyBlob="00000000000000000400000000000000" Name="System.Windows.Forms" />
		/// </PermissionSet>
		// Token: 0x06003560 RID: 13664 RVA: 0x000B0384 File Offset: 0x000AE584
		[MonoTODO("CAS support is experimental (and unsupported). This method only works in FullTrust.")]
		[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.StrongNameIdentityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                PublicKeyBlob=\"2100000000000000000400000000000000\"/>\n</PermissionSet>\n")]
		public static void GetZoneAndOrigin(out ArrayList zone, out ArrayList origin)
		{
			zone = new ArrayList();
			origin = new ArrayList();
		}

		/// <summary>Determines whether a permission is granted to the caller.</summary>
		/// <returns>true if the permissions granted to the caller include the permission <paramref name="perm" />; otherwise, false.</returns>
		/// <param name="perm">The permission to test against the grant of the caller. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06003561 RID: 13665 RVA: 0x000B0394 File Offset: 0x000AE594
		public static bool IsGranted(IPermission perm)
		{
			return perm == null || !SecurityManager.SecurityEnabled || SecurityManager.IsGranted(Assembly.GetCallingAssembly(), perm);
		}

		// Token: 0x06003562 RID: 13666 RVA: 0x000B03B8 File Offset: 0x000AE5B8
		internal static bool IsGranted(Assembly a, IPermission perm)
		{
			PermissionSet grantedPermissionSet = a.GrantedPermissionSet;
			if (grantedPermissionSet != null && !grantedPermissionSet.IsUnrestricted())
			{
				CodeAccessPermission codeAccessPermission = (CodeAccessPermission)grantedPermissionSet.GetPermission(perm.GetType());
				if (!perm.IsSubsetOf(codeAccessPermission))
				{
					return false;
				}
			}
			PermissionSet deniedPermissionSet = a.DeniedPermissionSet;
			if (deniedPermissionSet != null && !deniedPermissionSet.IsEmpty())
			{
				if (deniedPermissionSet.IsUnrestricted())
				{
					return false;
				}
				CodeAccessPermission codeAccessPermission2 = (CodeAccessPermission)a.DeniedPermissionSet.GetPermission(perm.GetType());
				if (codeAccessPermission2 != null && perm.IsSubsetOf(codeAccessPermission2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06003563 RID: 13667 RVA: 0x000B0450 File Offset: 0x000AE650
		internal static IPermission CheckPermissionSet(Assembly a, PermissionSet ps, bool noncas)
		{
			if (ps.IsEmpty())
			{
				return null;
			}
			foreach (object obj in ps)
			{
				IPermission permission = (IPermission)obj;
				if (!noncas && permission is CodeAccessPermission)
				{
					if (!SecurityManager.IsGranted(a, permission))
					{
						return permission;
					}
				}
				else
				{
					try
					{
						permission.Demand();
					}
					catch (SecurityException)
					{
						return permission;
					}
				}
			}
			return null;
		}

		// Token: 0x06003564 RID: 13668 RVA: 0x000B051C File Offset: 0x000AE71C
		internal static IPermission CheckPermissionSet(AppDomain ad, PermissionSet ps)
		{
			if (ps == null || ps.IsEmpty())
			{
				return null;
			}
			PermissionSet grantedPermissionSet = ad.GrantedPermissionSet;
			if (grantedPermissionSet == null)
			{
				return null;
			}
			if (grantedPermissionSet.IsUnrestricted())
			{
				return null;
			}
			if (ps.IsUnrestricted())
			{
				return new SecurityPermission(SecurityPermissionFlag.NoFlags);
			}
			foreach (object obj in ps)
			{
				IPermission permission = (IPermission)obj;
				if (permission is CodeAccessPermission)
				{
					CodeAccessPermission codeAccessPermission = (CodeAccessPermission)grantedPermissionSet.GetPermission(permission.GetType());
					if (codeAccessPermission == null)
					{
						if ((!grantedPermissionSet.IsUnrestricted() || !(permission is IUnrestrictedPermission)) && !permission.IsSubsetOf(null))
						{
							return permission;
						}
					}
					else if (!permission.IsSubsetOf(codeAccessPermission))
					{
						return permission;
					}
				}
				else
				{
					try
					{
						permission.Demand();
					}
					catch (SecurityException)
					{
						return permission;
					}
				}
			}
			return null;
		}

		/// <summary>Loads a <see cref="T:System.Security.Policy.PolicyLevel" /> from the specified file.</summary>
		/// <returns>The loaded <see cref="T:System.Security.Policy.PolicyLevel" />.</returns>
		/// <param name="path">The physical file path to a file containing the security policy information. </param>
		/// <param name="type">One of the <see cref="T:System.Security.PolicyLevelType" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="path" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The file indicated by the <paramref name="path" /> parameter does not exist. </exception>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />.-or- The code that calls this method does not have <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Read" />.-or- The code that calls this method does not have <see cref="F:System.Security.Permissions.FileIOPermissionAccess.Write" />.-or- The code that calls this method does not have <see cref="F:System.Security.Permissions.FileIOPermissionAccess.PathDiscovery" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003565 RID: 13669 RVA: 0x000B0664 File Offset: 0x000AE864
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
		public static PolicyLevel LoadPolicyLevelFromFile(string path, PolicyLevelType type)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			PolicyLevel policyLevel = null;
			try
			{
				policyLevel = new PolicyLevel(type.ToString(), type);
				policyLevel.LoadFromFile(path);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(Locale.GetText("Invalid policy XML"), ex);
			}
			return policyLevel;
		}

		/// <summary>Loads a <see cref="T:System.Security.Policy.PolicyLevel" /> from the specified string.</summary>
		/// <returns>The loaded <see cref="T:System.Security.Policy.PolicyLevel" />.</returns>
		/// <param name="str">The XML representation of a security policy level in the same form in which it appears in a configuration file. </param>
		/// <param name="type">One of the <see cref="T:System.Security.PolicyLevelType" /> values. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter is null. </exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="str" /> parameter is not valid. </exception>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003566 RID: 13670 RVA: 0x000B06D8 File Offset: 0x000AE8D8
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
		public static PolicyLevel LoadPolicyLevelFromString(string str, PolicyLevelType type)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			PolicyLevel policyLevel = null;
			try
			{
				policyLevel = new PolicyLevel(type.ToString(), type);
				policyLevel.LoadFromString(str);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(Locale.GetText("Invalid policy XML"), ex);
			}
			return policyLevel;
		}

		/// <summary>Provides an enumerator to access the security policy hierarchy by levels, such as computer policy and user policy.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> for <see cref="T:System.Security.Policy.PolicyLevel" /> objects that compose the security policy hierarchy.</returns>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x06003567 RID: 13671 RVA: 0x000B074C File Offset: 0x000AE94C
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
		public static IEnumerator PolicyHierarchy()
		{
			return SecurityManager.Hierarchy;
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence.</summary>
		/// <returns>The set of permissions that can be granted by the security system.</returns>
		/// <param name="evidence">The evidence set used to evaluate policy. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003568 RID: 13672 RVA: 0x000B0754 File Offset: 0x000AE954
		public static PermissionSet ResolvePolicy(Evidence evidence)
		{
			if (evidence == null)
			{
				return new PermissionSet(PermissionState.None);
			}
			PermissionSet permissionSet = null;
			IEnumerator hierarchy = SecurityManager.Hierarchy;
			while (hierarchy.MoveNext())
			{
				object obj = hierarchy.Current;
				PolicyLevel policyLevel = (PolicyLevel)obj;
				if (SecurityManager.ResolvePolicyLevel(ref permissionSet, policyLevel, evidence))
				{
					break;
				}
			}
			SecurityManager.ResolveIdentityPermissions(permissionSet, evidence);
			return permissionSet;
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence.</summary>
		/// <returns>The set of permissions that is appropriate for all of the provided evidence.</returns>
		/// <param name="evidences">An array of <see cref="T:System.Security.Policy.Evidence" /> objects used to evaluate policy. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06003569 RID: 13673 RVA: 0x000B07B0 File Offset: 0x000AE9B0
		[MonoTODO("(2.0) more tests are needed")]
		public static PermissionSet ResolvePolicy(Evidence[] evidences)
		{
			if (evidences == null || evidences.Length == 0 || (evidences.Length == 1 && evidences[0].Count == 0))
			{
				return new PermissionSet(PermissionState.None);
			}
			PermissionSet permissionSet = SecurityManager.ResolvePolicy(evidences[0]);
			for (int i = 1; i < evidences.Length; i++)
			{
				permissionSet = permissionSet.Intersect(SecurityManager.ResolvePolicy(evidences[i]));
			}
			return permissionSet;
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence, excluding the policy for the <see cref="T:System.AppDomain" /> level.</summary>
		/// <returns>The set of permissions that can be granted by the security system.</returns>
		/// <param name="evidence">The evidence set used to evaluate policy.</param>
		// Token: 0x0600356A RID: 13674 RVA: 0x000B0818 File Offset: 0x000AEA18
		public static PermissionSet ResolveSystemPolicy(Evidence evidence)
		{
			if (evidence == null)
			{
				return new PermissionSet(PermissionState.None);
			}
			PermissionSet permissionSet = null;
			IEnumerator hierarchy = SecurityManager.Hierarchy;
			while (hierarchy.MoveNext())
			{
				object obj = hierarchy.Current;
				PolicyLevel policyLevel = (PolicyLevel)obj;
				if (policyLevel.Type == PolicyLevelType.AppDomain)
				{
					break;
				}
				if (SecurityManager.ResolvePolicyLevel(ref permissionSet, policyLevel, evidence))
				{
					break;
				}
			}
			SecurityManager.ResolveIdentityPermissions(permissionSet, evidence);
			return permissionSet;
		}

		/// <summary>Determines what permissions to grant to code based on the specified evidence and requests.</summary>
		/// <returns>The set of permissions that would be granted by the security system.</returns>
		/// <param name="evidence">The evidence set used to evaluate policy. </param>
		/// <param name="reqdPset">The required permissions the code needs to run. </param>
		/// <param name="optPset">The optional permissions that will be used if granted, but aren't required for the code to run. </param>
		/// <param name="denyPset">The denied permissions that must never be granted to the code even if policy otherwise permits it. </param>
		/// <param name="denied">An output parameter that contains the set of permissions not granted. </param>
		/// <exception cref="T:System.Security.Policy.PolicyException">Policy fails to grant the minimum required permissions specified by the <paramref name="reqdPset" /> parameter. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600356B RID: 13675 RVA: 0x000B0884 File Offset: 0x000AEA84
		public static PermissionSet ResolvePolicy(Evidence evidence, PermissionSet reqdPset, PermissionSet optPset, PermissionSet denyPset, out PermissionSet denied)
		{
			PermissionSet permissionSet = SecurityManager.ResolvePolicy(evidence);
			if (reqdPset != null && !reqdPset.IsSubsetOf(permissionSet))
			{
				throw new PolicyException(Locale.GetText("Policy doesn't grant the minimal permissions required to execute the assembly."));
			}
			if (SecurityManager.CheckExecutionRights)
			{
				bool flag = false;
				if (permissionSet != null)
				{
					if (permissionSet.IsUnrestricted())
					{
						flag = true;
					}
					else
					{
						IPermission permission = permissionSet.GetPermission(typeof(SecurityPermission));
						flag = SecurityManager._execution.IsSubsetOf(permission);
					}
				}
				if (!flag)
				{
					throw new PolicyException(Locale.GetText("Policy doesn't grant the right to execute the assembly."));
				}
			}
			denied = denyPset;
			return permissionSet;
		}

		/// <summary>Gets a collection of code groups matching the specified evidence.</summary>
		/// <returns>An <see cref="T:System.Collections.IEnumerator" /> enumeration of the set of code groups matching the evidence.</returns>
		/// <param name="evidence">The evidence set against which the policy is evaluated. </param>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x0600356C RID: 13676 RVA: 0x000B0918 File Offset: 0x000AEB18
		public static IEnumerator ResolvePolicyGroups(Evidence evidence)
		{
			if (evidence == null)
			{
				throw new ArgumentNullException("evidence");
			}
			ArrayList arrayList = new ArrayList();
			IEnumerator hierarchy = SecurityManager.Hierarchy;
			while (hierarchy.MoveNext())
			{
				object obj = hierarchy.Current;
				PolicyLevel policyLevel = (PolicyLevel)obj;
				CodeGroup codeGroup = policyLevel.ResolveMatchingCodeGroups(evidence);
				arrayList.Add(codeGroup);
			}
			return arrayList.GetEnumerator();
		}

		/// <summary>Saves the modified security policy state.</summary>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x0600356D RID: 13677 RVA: 0x000B0974 File Offset: 0x000AEB74
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
		public static void SavePolicy()
		{
			IEnumerator hierarchy = SecurityManager.Hierarchy;
			while (hierarchy.MoveNext())
			{
				object obj = hierarchy.Current;
				PolicyLevel policyLevel = obj as PolicyLevel;
				policyLevel.Save();
			}
		}

		/// <summary>Saves a modified security policy level loaded with <see cref="M:System.Security.SecurityManager.LoadPolicyLevelFromFile(System.String,System.Security.PolicyLevelType)" />.</summary>
		/// <param name="level">The <see cref="T:System.Security.Policy.PolicyLevel" /> object to be saved. </param>
		/// <exception cref="T:System.Security.SecurityException">The code that calls this method does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.ControlPolicy" />. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
		///   <IPermission class="System.Security.Permissions.ReflectionPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="MemberAccess" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence, ControlPolicy" />
		/// </PermissionSet>
		// Token: 0x0600356E RID: 13678 RVA: 0x000B09AC File Offset: 0x000AEBAC
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"ControlPolicy\"/>\n</PermissionSet>\n")]
		public static void SavePolicyLevel(PolicyLevel level)
		{
			level.Save();
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x0600356F RID: 13679 RVA: 0x000B09B4 File Offset: 0x000AEBB4
		private static IEnumerator Hierarchy
		{
			get
			{
				object lockObject = SecurityManager._lockObject;
				lock (lockObject)
				{
					if (SecurityManager._hierarchy == null)
					{
						SecurityManager.InitializePolicyHierarchy();
					}
				}
				return SecurityManager._hierarchy.GetEnumerator();
			}
		}

		// Token: 0x06003570 RID: 13680 RVA: 0x000B0A10 File Offset: 0x000AEC10
		private static void InitializePolicyHierarchy()
		{
			string directoryName = Path.GetDirectoryName(Environment.GetMachineConfigPath());
			string text = Path.Combine(Environment.InternalGetFolderPath(Environment.SpecialFolder.ApplicationData), "mono");
			PolicyLevel policyLevel = new PolicyLevel("Enterprise", PolicyLevelType.Enterprise);
			SecurityManager._level = policyLevel;
			policyLevel.LoadFromFile(Path.Combine(directoryName, "enterprisesec.config"));
			PolicyLevel policyLevel2 = new PolicyLevel("Machine", PolicyLevelType.Machine);
			SecurityManager._level = policyLevel2;
			policyLevel2.LoadFromFile(Path.Combine(directoryName, "security.config"));
			PolicyLevel policyLevel3 = new PolicyLevel("User", PolicyLevelType.User);
			SecurityManager._level = policyLevel3;
			policyLevel3.LoadFromFile(Path.Combine(text, "security.config"));
			SecurityManager._hierarchy = ArrayList.Synchronized(new ArrayList { policyLevel, policyLevel2, policyLevel3 });
			SecurityManager._level = null;
		}

		// Token: 0x06003571 RID: 13681 RVA: 0x000B0ADC File Offset: 0x000AECDC
		internal static bool ResolvePolicyLevel(ref PermissionSet ps, PolicyLevel pl, Evidence evidence)
		{
			PolicyStatement policyStatement = pl.Resolve(evidence);
			if (policyStatement != null)
			{
				if (ps == null)
				{
					ps = policyStatement.PermissionSet;
				}
				else
				{
					ps = ps.Intersect(policyStatement.PermissionSet);
					if (ps == null)
					{
						ps = new PermissionSet(PermissionState.None);
					}
				}
				if ((policyStatement.Attributes & PolicyStatementAttribute.LevelFinal) == PolicyStatementAttribute.LevelFinal)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003572 RID: 13682 RVA: 0x000B0B3C File Offset: 0x000AED3C
		internal static void ResolveIdentityPermissions(PermissionSet ps, Evidence evidence)
		{
			if (ps.IsUnrestricted())
			{
				return;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				object obj = hostEnumerator.Current;
				IIdentityPermissionFactory identityPermissionFactory = obj as IIdentityPermissionFactory;
				if (identityPermissionFactory != null)
				{
					IPermission permission = identityPermissionFactory.CreateIdentityPermission(evidence);
					ps.AddPermission(permission);
				}
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06003573 RID: 13683 RVA: 0x000B0B90 File Offset: 0x000AED90
		// (set) Token: 0x06003574 RID: 13684 RVA: 0x000B0B98 File Offset: 0x000AED98
		internal static PolicyLevel ResolvingPolicyLevel
		{
			get
			{
				return SecurityManager._level;
			}
			set
			{
				SecurityManager._level = value;
			}
		}

		// Token: 0x06003575 RID: 13685 RVA: 0x000B0BA0 File Offset: 0x000AEDA0
		internal static PermissionSet Decode(IntPtr permissions, int length)
		{
			PermissionSet permissionSet = null;
			object lockObject = SecurityManager._lockObject;
			lock (lockObject)
			{
				if (SecurityManager._declsecCache == null)
				{
					SecurityManager._declsecCache = new Hashtable();
				}
				object obj = (int)permissions;
				permissionSet = (PermissionSet)SecurityManager._declsecCache[obj];
				if (permissionSet == null)
				{
					byte[] array = new byte[length];
					Marshal.Copy(permissions, array, 0, length);
					permissionSet = SecurityManager.Decode(array);
					permissionSet.DeclarativeSecurity = true;
					SecurityManager._declsecCache.Add(obj, permissionSet);
				}
			}
			return permissionSet;
		}

		// Token: 0x06003576 RID: 13686 RVA: 0x000B0C48 File Offset: 0x000AEE48
		internal static PermissionSet Decode(byte[] encodedPermissions)
		{
			if (encodedPermissions == null || encodedPermissions.Length < 1)
			{
				throw new SecurityException("Invalid metadata format.");
			}
			byte b = encodedPermissions[0];
			if (b == 46)
			{
				return PermissionSet.CreateFromBinaryFormat(encodedPermissions);
			}
			if (b != 60)
			{
				throw new SecurityException(Locale.GetText("Unknown metadata format."));
			}
			string @string = Encoding.Unicode.GetString(encodedPermissions);
			return new PermissionSet(@string);
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06003577 RID: 13687 RVA: 0x000B0CB4 File Offset: 0x000AEEB4
		private static IPermission UnmanagedCode
		{
			get
			{
				object lockObject = SecurityManager._lockObject;
				lock (lockObject)
				{
					if (SecurityManager._unmanagedCode == null)
					{
						SecurityManager._unmanagedCode = new SecurityPermission(SecurityPermissionFlag.UnmanagedCode);
					}
				}
				return SecurityManager._unmanagedCode;
			}
		}

		// Token: 0x06003578 RID: 13688
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool GetLinkDemandSecurity(MethodBase method, RuntimeDeclSecurityActions* cdecl, RuntimeDeclSecurityActions* mdecl);

		// Token: 0x06003579 RID: 13689 RVA: 0x000B0D10 File Offset: 0x000AEF10
		internal unsafe static void ReflectedLinkDemandInvoke(MethodBase mb)
		{
			RuntimeDeclSecurityActions runtimeDeclSecurityActions;
			RuntimeDeclSecurityActions runtimeDeclSecurityActions2;
			if (!SecurityManager.GetLinkDemandSecurity(mb, &runtimeDeclSecurityActions, &runtimeDeclSecurityActions2))
			{
				return;
			}
			PermissionSet permissionSet = null;
			if (runtimeDeclSecurityActions.cas.size > 0)
			{
				permissionSet = SecurityManager.Decode(runtimeDeclSecurityActions.cas.blob, runtimeDeclSecurityActions.cas.size);
			}
			if (runtimeDeclSecurityActions.noncas.size > 0)
			{
				PermissionSet permissionSet2 = SecurityManager.Decode(runtimeDeclSecurityActions.noncas.blob, runtimeDeclSecurityActions.noncas.size);
				permissionSet = ((permissionSet != null) ? permissionSet.Union(permissionSet2) : permissionSet2);
			}
			if (runtimeDeclSecurityActions2.cas.size > 0)
			{
				PermissionSet permissionSet3 = SecurityManager.Decode(runtimeDeclSecurityActions2.cas.blob, runtimeDeclSecurityActions2.cas.size);
				permissionSet = ((permissionSet != null) ? permissionSet.Union(permissionSet3) : permissionSet3);
			}
			if (runtimeDeclSecurityActions2.noncas.size > 0)
			{
				PermissionSet permissionSet4 = SecurityManager.Decode(runtimeDeclSecurityActions2.noncas.blob, runtimeDeclSecurityActions2.noncas.size);
				permissionSet = ((permissionSet != null) ? permissionSet.Union(permissionSet4) : permissionSet4);
			}
			if (permissionSet != null)
			{
				permissionSet.Demand();
			}
		}

		// Token: 0x0600357A RID: 13690 RVA: 0x000B0E40 File Offset: 0x000AF040
		internal unsafe static bool ReflectedLinkDemandQuery(MethodBase mb)
		{
			RuntimeDeclSecurityActions runtimeDeclSecurityActions;
			RuntimeDeclSecurityActions runtimeDeclSecurityActions2;
			return !SecurityManager.GetLinkDemandSecurity(mb, &runtimeDeclSecurityActions, &runtimeDeclSecurityActions2) || SecurityManager.LinkDemand(mb.ReflectedType.Assembly, &runtimeDeclSecurityActions, &runtimeDeclSecurityActions2);
		}

		// Token: 0x0600357B RID: 13691 RVA: 0x000B0E74 File Offset: 0x000AF074
		private unsafe static bool LinkDemand(Assembly a, RuntimeDeclSecurityActions* klass, RuntimeDeclSecurityActions* method)
		{
			bool flag2;
			try
			{
				bool flag = true;
				if (klass->cas.size > 0)
				{
					PermissionSet permissionSet = SecurityManager.Decode(klass->cas.blob, klass->cas.size);
					flag = SecurityManager.CheckPermissionSet(a, permissionSet, false) == null;
				}
				if (flag && klass->noncas.size > 0)
				{
					PermissionSet permissionSet = SecurityManager.Decode(klass->noncas.blob, klass->noncas.size);
					flag = SecurityManager.CheckPermissionSet(a, permissionSet, true) == null;
				}
				if (flag && method->cas.size > 0)
				{
					PermissionSet permissionSet = SecurityManager.Decode(method->cas.blob, method->cas.size);
					flag = SecurityManager.CheckPermissionSet(a, permissionSet, false) == null;
				}
				if (flag && method->noncas.size > 0)
				{
					PermissionSet permissionSet = SecurityManager.Decode(method->noncas.blob, method->noncas.size);
					flag = SecurityManager.CheckPermissionSet(a, permissionSet, true) == null;
				}
				flag2 = flag;
			}
			catch (SecurityException)
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x0600357C RID: 13692 RVA: 0x000B0FB4 File Offset: 0x000AF1B4
		private static bool LinkDemandFullTrust(Assembly a)
		{
			PermissionSet grantedPermissionSet = a.GrantedPermissionSet;
			if (grantedPermissionSet != null && !grantedPermissionSet.IsUnrestricted())
			{
				return false;
			}
			PermissionSet deniedPermissionSet = a.DeniedPermissionSet;
			return deniedPermissionSet == null || deniedPermissionSet.IsEmpty();
		}

		// Token: 0x0600357D RID: 13693 RVA: 0x000B0FF8 File Offset: 0x000AF1F8
		private static bool LinkDemandUnmanaged(Assembly a)
		{
			return SecurityManager.IsGranted(a, SecurityManager.UnmanagedCode);
		}

		// Token: 0x0600357E RID: 13694 RVA: 0x000B1008 File Offset: 0x000AF208
		private static void LinkDemandSecurityException(int securityViolation, IntPtr methodHandle)
		{
			RuntimeMethodHandle runtimeMethodHandle = new RuntimeMethodHandle(methodHandle);
			MethodInfo methodInfo = (MethodInfo)MethodBase.GetMethodFromHandle(runtimeMethodHandle);
			Assembly assembly = methodInfo.DeclaringType.Assembly;
			AssemblyName assemblyName = null;
			PermissionSet permissionSet = null;
			PermissionSet permissionSet2 = null;
			object obj = null;
			IPermission permission = null;
			if (assembly != null)
			{
				assemblyName = assembly.UnprotectedGetName();
				permissionSet = assembly.GrantedPermissionSet;
				permissionSet2 = assembly.DeniedPermissionSet;
			}
			string text;
			switch (securityViolation)
			{
			case 1:
				text = Locale.GetText("Permissions refused to call this method.");
				goto IL_00E5;
			case 2:
				text = Locale.GetText("Partially trusted callers aren't allowed to call into this assembly.");
				obj = DefaultPolicies.FullTrust;
				goto IL_00E5;
			case 4:
				text = Locale.GetText("Calling internal calls is restricted to ECMA signed assemblies.");
				goto IL_00E5;
			case 8:
				text = Locale.GetText("Calling unmanaged code isn't allowed from this assembly.");
				obj = SecurityManager._unmanagedCode;
				permission = SecurityManager._unmanagedCode;
				goto IL_00E5;
			}
			text = Locale.GetText("JIT time LinkDemand failed.");
			IL_00E5:
			throw new SecurityException(text, assemblyName, permissionSet, permissionSet2, methodInfo, SecurityAction.LinkDemand, obj, permission, null);
		}

		// Token: 0x0600357F RID: 13695 RVA: 0x000B1110 File Offset: 0x000AF310
		private static void InheritanceDemandSecurityException(int securityViolation, Assembly a, Type t, MethodInfo method)
		{
			AssemblyName assemblyName = null;
			PermissionSet permissionSet = null;
			PermissionSet permissionSet2 = null;
			if (a != null)
			{
				assemblyName = a.UnprotectedGetName();
				permissionSet = a.GrantedPermissionSet;
				permissionSet2 = a.DeniedPermissionSet;
			}
			string text;
			if (securityViolation != 1)
			{
				if (securityViolation != 2)
				{
					text = Locale.GetText("Load time InheritDemand failed.");
				}
				else
				{
					text = Locale.GetText("Method override refused.");
				}
			}
			else
			{
				text = string.Format(Locale.GetText("Class inheritance refused for {0}."), t);
			}
			throw new SecurityException(text, assemblyName, permissionSet, permissionSet2, method, SecurityAction.InheritanceDemand, null, null, null);
		}

		// Token: 0x06003580 RID: 13696 RVA: 0x000B119C File Offset: 0x000AF39C
		private static void ThrowException(Exception ex)
		{
			throw ex;
		}

		// Token: 0x06003581 RID: 13697 RVA: 0x000B11A0 File Offset: 0x000AF3A0
		private unsafe static bool InheritanceDemand(AppDomain ad, Assembly a, RuntimeDeclSecurityActions* actions)
		{
			bool flag2;
			try
			{
				bool flag = true;
				if (actions->cas.size > 0)
				{
					PermissionSet permissionSet = SecurityManager.Decode(actions->cas.blob, actions->cas.size);
					flag = SecurityManager.CheckPermissionSet(a, permissionSet, false) == null;
					if (flag)
					{
						flag = SecurityManager.CheckPermissionSet(ad, permissionSet) == null;
					}
				}
				if (actions->noncas.size > 0)
				{
					PermissionSet permissionSet = SecurityManager.Decode(actions->noncas.blob, actions->noncas.size);
					flag = SecurityManager.CheckPermissionSet(a, permissionSet, true) == null;
					if (flag)
					{
						flag = SecurityManager.CheckPermissionSet(ad, permissionSet) == null;
					}
				}
				flag2 = flag;
			}
			catch (SecurityException)
			{
				flag2 = false;
			}
			return flag2;
		}

		// Token: 0x06003582 RID: 13698 RVA: 0x000B127C File Offset: 0x000AF47C
		private static void DemandUnmanaged()
		{
			SecurityManager.UnmanagedCode.Demand();
		}

		// Token: 0x06003583 RID: 13699 RVA: 0x000B1288 File Offset: 0x000AF488
		private static void InternalDemand(IntPtr permissions, int length)
		{
			PermissionSet permissionSet = SecurityManager.Decode(permissions, length);
			permissionSet.Demand();
		}

		// Token: 0x06003584 RID: 13700 RVA: 0x000B12A4 File Offset: 0x000AF4A4
		private static void InternalDemandChoice(IntPtr permissions, int length)
		{
			throw new SecurityException("SecurityAction.DemandChoice was removed from 2.0");
		}

		// Token: 0x0400166B RID: 5739
		private static object _lockObject = new object();

		// Token: 0x0400166C RID: 5740
		private static ArrayList _hierarchy;

		// Token: 0x0400166D RID: 5741
		private static IPermission _unmanagedCode;

		// Token: 0x0400166E RID: 5742
		private static Hashtable _declsecCache;

		// Token: 0x0400166F RID: 5743
		private static PolicyLevel _level;

		// Token: 0x04001670 RID: 5744
		private static SecurityPermission _execution = new SecurityPermission(SecurityPermissionFlag.Execution);
	}
}
