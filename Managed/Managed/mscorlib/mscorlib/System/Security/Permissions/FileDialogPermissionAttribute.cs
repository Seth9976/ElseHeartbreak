using System;
using System.Runtime.InteropServices;

namespace System.Security.Permissions
{
	/// <summary>Allows security actions for <see cref="T:System.Security.Permissions.FileDialogPermission" /> to be applied to code using declarative security. This class cannot be inherited.</summary>
	// Token: 0x020005F6 RID: 1526
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[ComVisible(true)]
	[Serializable]
	public sealed class FileDialogPermissionAttribute : CodeAccessSecurityAttribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.FileDialogPermissionAttribute" /> class with the specified <see cref="T:System.Security.Permissions.SecurityAction" />.</summary>
		/// <param name="action">One of the <see cref="T:System.Security.Permissions.SecurityAction" /> values. </param>
		// Token: 0x06003A30 RID: 14896 RVA: 0x000C7EA0 File Offset: 0x000C60A0
		public FileDialogPermissionAttribute(SecurityAction action)
			: base(action)
		{
		}

		/// <summary>Gets or sets a value indicating whether permission to open files through the file dialog is declared.</summary>
		/// <returns>true if permission to open files through the file dialog is declared; otherwise, false.</returns>
		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x06003A31 RID: 14897 RVA: 0x000C7EAC File Offset: 0x000C60AC
		// (set) Token: 0x06003A32 RID: 14898 RVA: 0x000C7EB4 File Offset: 0x000C60B4
		public bool Open
		{
			get
			{
				return this.canOpen;
			}
			set
			{
				this.canOpen = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether permission to save files through the file dialog is declared.</summary>
		/// <returns>true if permission to save files through the file dialog is declared; otherwise, false.</returns>
		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06003A33 RID: 14899 RVA: 0x000C7EC0 File Offset: 0x000C60C0
		// (set) Token: 0x06003A34 RID: 14900 RVA: 0x000C7EC8 File Offset: 0x000C60C8
		public bool Save
		{
			get
			{
				return this.canSave;
			}
			set
			{
				this.canSave = value;
			}
		}

		/// <summary>Creates and returns a new <see cref="T:System.Security.Permissions.FileDialogPermission" />.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.FileDialogPermission" /> that corresponds to this attribute.</returns>
		// Token: 0x06003A35 RID: 14901 RVA: 0x000C7ED4 File Offset: 0x000C60D4
		public override IPermission CreatePermission()
		{
			FileDialogPermission fileDialogPermission;
			if (base.Unrestricted)
			{
				fileDialogPermission = new FileDialogPermission(PermissionState.Unrestricted);
			}
			else
			{
				FileDialogPermissionAccess fileDialogPermissionAccess = FileDialogPermissionAccess.None;
				if (this.canOpen)
				{
					fileDialogPermissionAccess |= FileDialogPermissionAccess.Open;
				}
				if (this.canSave)
				{
					fileDialogPermissionAccess |= FileDialogPermissionAccess.Save;
				}
				fileDialogPermission = new FileDialogPermission(fileDialogPermissionAccess);
			}
			return fileDialogPermission;
		}

		// Token: 0x0400193F RID: 6463
		private bool canOpen;

		// Token: 0x04001940 RID: 6464
		private bool canSave;
	}
}
