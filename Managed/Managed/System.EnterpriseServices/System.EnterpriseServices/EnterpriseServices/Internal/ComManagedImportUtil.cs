using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices.Internal
{
	/// <summary>Identifies and installs components in the COM+ catalog.</summary>
	// Token: 0x02000064 RID: 100
	[Guid("3b0398c9-7812-4007-85cb-18c771f2206f")]
	public class ComManagedImportUtil : IComManagedImportUtil
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.Internal.ComManagedImportUtil" /> class.</summary>
		// Token: 0x06000183 RID: 387 RVA: 0x00002FF8 File Offset: 0x000011F8
		[MonoTODO]
		public ComManagedImportUtil()
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets the component information from the assembly.</summary>
		/// <param name="assemblyPath">The path to the assembly. </param>
		/// <param name="numComponents">When this method returns, this parameter contains the number of components in the assembly. </param>
		/// <param name="componentInfo">When this method returns, this parameter contains the information about the components. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="assemblyPath" /> is an empty string, contains only white space, or contains one or more invalid characters as defined by <see cref="F:System.IO.Path.InvalidPathChars" />.-or- The system could not retrieve the absolute path. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permissions. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="assemblyPath" /> is null. </exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="assemblyPath" /> contains a colon (":"). </exception>
		/// <exception cref="T:System.IO.PathTooLongException">The specified path, file name, or both exceed the system-defined maximum length. For example, on Windows-based platforms, paths must be less than 248 characters, and file names must be less than 260 characters. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000184 RID: 388 RVA: 0x00003008 File Offset: 0x00001208
		[MonoTODO]
		public void GetComponentInfo(string assemblyPath, out string numComponents, out string componentInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Installs an assembly into a COM+ application.</summary>
		/// <param name="asmpath">The path for the assembly. </param>
		/// <param name="parname">The COM+ partition name. </param>
		/// <param name="appname">The COM+ application name. </param>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code. </exception>
		/// <exception cref="T:System.EnterpriseServices.RegistrationException">The input assembly does not have a strong name. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.RegistryPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000185 RID: 389 RVA: 0x00003010 File Offset: 0x00001210
		[MonoTODO]
		public void InstallAssembly(string asmpath, string parname, string appname)
		{
			throw new NotImplementedException();
		}
	}
}
