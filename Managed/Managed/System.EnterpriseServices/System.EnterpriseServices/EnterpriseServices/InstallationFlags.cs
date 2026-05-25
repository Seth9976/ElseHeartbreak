using System;

namespace System.EnterpriseServices
{
	/// <summary>Flags used with the <see cref="T:System.EnterpriseServices.RegistrationHelper" /> class.</summary>
	// Token: 0x0200001D RID: 29
	[Flags]
	[Serializable]
	public enum InstallationFlags
	{
		/// <summary>Should not be used.</summary>
		// Token: 0x04000053 RID: 83
		Configure = 1024,
		/// <summary>Configures components only, do not configure methods or interfaces.</summary>
		// Token: 0x04000054 RID: 84
		ConfigureComponentsOnly = 16,
		/// <summary>Creates the target application. An error occurs if the target already exists.</summary>
		// Token: 0x04000055 RID: 85
		CreateTargetApplication = 2,
		/// <summary>Do the default installation, which configures, installs, and registers, and assumes that the application already exists.</summary>
		// Token: 0x04000056 RID: 86
		Default = 0,
		/// <summary>Do not export the type library; one can be found either by the generated or supplied type library name.</summary>
		// Token: 0x04000057 RID: 87
		ExpectExistingTypeLib = 1,
		/// <summary>Creates the application if it does not exist; otherwise use the existing application.</summary>
		// Token: 0x04000058 RID: 88
		FindOrCreateTargetApplication = 4,
		/// <summary>Should not be used.</summary>
		// Token: 0x04000059 RID: 89
		Install = 512,
		/// <summary>If using an existing application, ensures that the properties on this application match those in the assembly.</summary>
		// Token: 0x0400005A RID: 90
		ReconfigureExistingApplication = 8,
		/// <summary>Should not be used.</summary>
		// Token: 0x0400005B RID: 91
		Register = 256,
		/// <summary>When alert text is encountered, writes it to the Console.</summary>
		// Token: 0x0400005C RID: 92
		ReportWarningsToConsole = 32
	}
}
