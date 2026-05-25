using System;
using System.Runtime.Serialization;

namespace System.EnterpriseServices
{
	/// <summary>The exception that is thrown when a registration error is detected.</summary>
	// Token: 0x02000036 RID: 54
	[Serializable]
	public sealed class RegistrationException : SystemException
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationException" /> class with a specified error message.</summary>
		/// <param name="msg">The message displayed to the client when the exception is thrown. </param>
		// Token: 0x060000BC RID: 188 RVA: 0x00002770 File Offset: 0x00000970
		[MonoTODO]
		public RegistrationException(string msg)
			: base(msg)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationException" /> class.</summary>
		// Token: 0x060000BD RID: 189 RVA: 0x0000277C File Offset: 0x0000097C
		public RegistrationException()
			: this("Registration error")
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.RegistrationException" /> class with a specified error message and nested exception.</summary>
		/// <param name="msg">The message displayed to the client when the exception is thrown. </param>
		/// <param name="inner">The nested exception.</param>
		// Token: 0x060000BE RID: 190 RVA: 0x0000278C File Offset: 0x0000098C
		public RegistrationException(string msg, Exception innerException)
			: base(msg, innerException)
		{
		}

		/// <summary>Gets an array of <see cref="T:System.EnterpriseServices.RegistrationErrorInfo" /> objects that describe registration errors.</summary>
		/// <returns>The array of <see cref="T:System.EnterpriseServices.RegistrationErrorInfo" /> objects.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002798 File Offset: 0x00000998
		public RegistrationErrorInfo[] ErrorInfo
		{
			get
			{
				return this.errorInfo;
			}
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the error information in <see cref="T:System.EnterpriseServices.RegistrationErrorInfo" />.</summary>
		/// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object that contains serialized object data. </param>
		/// <param name="ctx">The contextual information about the source or destination. </param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="info" /> parameter is null. </exception>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x060000C0 RID: 192 RVA: 0x000027A0 File Offset: 0x000009A0
		[MonoTODO]
		public override void GetObjectData(SerializationInfo info, StreamingContext ctx)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000075 RID: 117
		private RegistrationErrorInfo[] errorInfo;
	}
}
