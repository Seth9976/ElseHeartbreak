using System;
using System.Runtime.InteropServices;

namespace System.EnterpriseServices
{
	/// <summary>Configures a role for an application or component. This class cannot be inherited.</summary>
	// Token: 0x0200003E RID: 62
	[ComVisible(false)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = true)]
	public sealed class SecurityRoleAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SecurityRoleAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.SecurityRoleAttribute.Role" /> property.</summary>
		/// <param name="role">A security role for the application, component, interface, or method. </param>
		// Token: 0x060000EC RID: 236 RVA: 0x00002924 File Offset: 0x00000B24
		public SecurityRoleAttribute(string role)
			: this(role, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.EnterpriseServices.SecurityRoleAttribute" /> class and sets the <see cref="P:System.EnterpriseServices.SecurityRoleAttribute.Role" /> and <see cref="P:System.EnterpriseServices.SecurityRoleAttribute.SetEveryoneAccess" /> properties.</summary>
		/// <param name="role">A security role for the application, component, interface, or method. </param>
		/// <param name="everyone">true to require that the newly created role have the Everyone user group added as a user; otherwise, false. </param>
		// Token: 0x060000ED RID: 237 RVA: 0x00002930 File Offset: 0x00000B30
		public SecurityRoleAttribute(string role, bool everyone)
		{
			this.description = string.Empty;
			this.everyone = everyone;
			this.role = role;
		}

		/// <summary>Gets or sets the role description.</summary>
		/// <returns>The description for the role.</returns>
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00002954 File Offset: 0x00000B54
		// (set) Token: 0x060000EF RID: 239 RVA: 0x0000295C File Offset: 0x00000B5C
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		/// <summary>Gets or sets the security role.</summary>
		/// <returns>The security role applied to an application, component, interface, or method.</returns>
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00002968 File Offset: 0x00000B68
		// (set) Token: 0x060000F1 RID: 241 RVA: 0x00002970 File Offset: 0x00000B70
		public string Role
		{
			get
			{
				return this.role;
			}
			set
			{
				this.role = value;
			}
		}

		/// <summary>Sets a value indicating whether to add the Everyone user group as a user.</summary>
		/// <returns>true to require that a newly created role have the Everyone user group added as a user (roles that already exist on the application are not modified); otherwise, false to suppress adding the Everyone user group as a user.</returns>
		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x0000297C File Offset: 0x00000B7C
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00002984 File Offset: 0x00000B84
		public bool SetEveryoneAccess
		{
			get
			{
				return this.everyone;
			}
			set
			{
				this.everyone = value;
			}
		}

		// Token: 0x04000076 RID: 118
		private string description;

		// Token: 0x04000077 RID: 119
		private bool everyone;

		// Token: 0x04000078 RID: 120
		private string role;
	}
}
