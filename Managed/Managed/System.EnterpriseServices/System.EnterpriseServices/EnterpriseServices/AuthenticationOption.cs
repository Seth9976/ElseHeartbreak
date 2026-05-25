using System;

namespace System.EnterpriseServices
{
	/// <summary>Specifies the remote procedure call (RPC) authentication mechanism. Applicable only when the <see cref="T:System.EnterpriseServices.ActivationOption" /> is set to Server.</summary>
	// Token: 0x0200000B RID: 11
	[Serializable]
	public enum AuthenticationOption
	{
		/// <summary>Authenticates credentials at the beginning of every call.</summary>
		// Token: 0x04000031 RID: 49
		Call = 3,
		/// <summary>Authenticates credentials only when the connection is made.</summary>
		// Token: 0x04000032 RID: 50
		Connect = 2,
		/// <summary>Uses the default authentication level for the specified authentication service. In COM+, this setting is provided by the DefaultAuthenticationLevel property in the LocalComputer collection.</summary>
		// Token: 0x04000033 RID: 51
		Default = 0,
		/// <summary>Authenticates credentials and verifies that no call data has been modified in transit.</summary>
		// Token: 0x04000034 RID: 52
		Integrity = 5,
		/// <summary>Authentication does not occur.</summary>
		// Token: 0x04000035 RID: 53
		None = 1,
		/// <summary>Authenticates credentials and verifies that all call data is received.</summary>
		// Token: 0x04000036 RID: 54
		Packet = 4,
		/// <summary>Authenticates credentials and encrypts the packet, including the data and the sender's identity and signature.</summary>
		// Token: 0x04000037 RID: 55
		Privacy = 6
	}
}
