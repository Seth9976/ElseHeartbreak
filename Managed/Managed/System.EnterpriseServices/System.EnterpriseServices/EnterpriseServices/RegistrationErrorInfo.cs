using System;

namespace System.EnterpriseServices
{
	/// <summary>Retrieves extended error information about methods related to multiple COM+ objects. This also includes methods that install, import, and export COM+ applications and components. This class cannot be inherited.</summary>
	// Token: 0x02000034 RID: 52
	[Serializable]
	public sealed class RegistrationErrorInfo
	{
		// Token: 0x060000A9 RID: 169 RVA: 0x000026B0 File Offset: 0x000008B0
		[MonoTODO]
		internal RegistrationErrorInfo(string name, string majorRef, string minorRef, int errorCode)
		{
			this.name = name;
			this.majorRef = majorRef;
			this.minorRef = minorRef;
			this.errorCode = errorCode;
		}

		/// <summary>Gets the error code for the object or file.</summary>
		/// <returns>The error code for the object or file.</returns>
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000026D8 File Offset: 0x000008D8
		public int ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		/// <summary>Gets the description of the <see cref="P:System.EnterpriseServices.RegistrationErrorInfo.ErrorCode" />.</summary>
		/// <returns>The description of the <see cref="P:System.EnterpriseServices.RegistrationErrorInfo.ErrorCode" />.</returns>
		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000026E0 File Offset: 0x000008E0
		public string ErrorString
		{
			get
			{
				return this.errorString;
			}
		}

		/// <summary>Gets the key value for the object that caused the error, if applicable.</summary>
		/// <returns>The key value for the object that caused the error, if applicable.</returns>
		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000026E8 File Offset: 0x000008E8
		public string MajorRef
		{
			get
			{
				return this.majorRef;
			}
		}

		/// <summary>Gets a precise specification of the item that caused the error, such as a property name.</summary>
		/// <returns>A precise specification of the item, such as a property name, that caused the error. If multiple errors occurred, or this does not apply, <see cref="P:System.EnterpriseServices.RegistrationErrorInfo.MinorRef" /> returns the string "&lt;Invalid&gt;".</returns>
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000026F0 File Offset: 0x000008F0
		public string MinorRef
		{
			get
			{
				return this.minorRef;
			}
		}

		/// <summary>Gets the name of the object or file that caused the error.</summary>
		/// <returns>The name of the object or file that caused the error.</returns>
		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000026F8 File Offset: 0x000008F8
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000070 RID: 112
		private int errorCode;

		// Token: 0x04000071 RID: 113
		private string errorString;

		// Token: 0x04000072 RID: 114
		private string majorRef;

		// Token: 0x04000073 RID: 115
		private string minorRef;

		// Token: 0x04000074 RID: 116
		private string name;
	}
}
