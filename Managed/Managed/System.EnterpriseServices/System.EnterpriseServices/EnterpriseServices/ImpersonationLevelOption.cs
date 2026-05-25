using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the level of impersonation allowed when calling targets of a server application.</summary>
	// Token: 0x0200001B RID: 27
	[Serializable]
	public enum ImpersonationLevelOption
	{
		/// <summary>The client is anonymous to the server. The server process can impersonate the client, but the impersonation token does not contain any information about the client.</summary>
		// Token: 0x0400004A RID: 74
		Anonymous = 1,
		/// <summary>Uses the default impersonation level for the specified authentication service. In COM+, this setting is provided by the DefaultImpersonationLevel property in the LocalComputer collection.</summary>
		// Token: 0x0400004B RID: 75
		Default = 0,
		/// <summary>The most powerful impersonation level. When this level is selected, the server (whether local or remote) can impersonate the client's security context while acting on behalf of the client </summary>
		// Token: 0x0400004C RID: 76
		Delegate = 4,
		/// <summary>The system default level. The server can obtain the client's identity, and the server can impersonate the client to do ACL checks.</summary>
		// Token: 0x0400004D RID: 77
		Identify = 2,
		/// <summary>The server can impersonate the client's security context while acting on behalf of the client. The server can access local resources as the client.</summary>
		// Token: 0x0400004E RID: 78
		Impersonate
	}
}
