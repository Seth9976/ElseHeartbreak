using System;

namespace System.IO.Pipes
{
	/// <summary>Defines the access rights to use when you create access and audit rules. </summary>
	// Token: 0x0200006C RID: 108
	[Flags]
	public enum PipeAccessRights
	{
		/// <summary>Specifies the right to read data from the pipe. This does not include the right to read file system attributes, extended file system attributes, or access and audit rules.</summary>
		// Token: 0x0400017D RID: 381
		ReadData = 1,
		/// <summary>Specifies the right to write data to a pipe. This does not include the right to write file system attributes or extended file system attributes.</summary>
		// Token: 0x0400017E RID: 382
		WriteData = 2,
		/// <summary>Specifies the right to read file system attributes from a pipe. This does not include the right to read data, extended file system attributes, or access and audit rules.</summary>
		// Token: 0x0400017F RID: 383
		ReadAttributes = 4,
		/// <summary>Specifies the right to write file system attributes to a pipe. This does not include the right to write data or extended file system attributes.</summary>
		// Token: 0x04000180 RID: 384
		WriteAttributes = 8,
		/// <summary>Specifies the right to read extended file system attributes from a pipe. This does not include the right to read data, file system attributes, or access and audit rules.</summary>
		// Token: 0x04000181 RID: 385
		ReadExtendedAttributes = 16,
		/// <summary>Specifies the right to write extended file system attributes to a pipe. This does not include the right to write file attributes or data.</summary>
		// Token: 0x04000182 RID: 386
		WriteExtendedAttributes = 32,
		/// <summary>Specifies the right to create a new pipe. Setting this right also sets the <see cref="F:System.IO.Pipes.PipeAccessRights.Synchronize" /> right.</summary>
		// Token: 0x04000183 RID: 387
		CreateNewInstance = 64,
		/// <summary>Specifies the right to delete a pipe.</summary>
		// Token: 0x04000184 RID: 388
		Delete = 128,
		/// <summary>Specifies the right to read access and audit rules from the pipe. This does not include the right to read data, file system attributes, or extended file system attributes.</summary>
		// Token: 0x04000185 RID: 389
		ReadPermissions = 256,
		/// <summary>Specifies the right to change the security and audit rules that are associated with a pipe.</summary>
		// Token: 0x04000186 RID: 390
		ChangePermissions = 512,
		/// <summary>Specifies the right to change the owner of a pipe. Note that owners of a pipe have full access to that resource.</summary>
		// Token: 0x04000187 RID: 391
		TakeOwnership = 1024,
		/// <summary>Specifies whether the application can wait for a pipe handle to synchronize with the completion of an I/O operation.</summary>
		// Token: 0x04000188 RID: 392
		Synchronize = 2048,
		/// <summary>Specifies the right to exert full control over a pipe, and to modify access control and audit rules. This value represents the combination of all rights in this enumeration.</summary>
		// Token: 0x04000189 RID: 393
		FullControl = 1855,
		/// <summary>Specifies the right to read from the pipe. This right includes the <see cref="F:System.IO.Pipes.PipeAccessRights.ReadAttributes" />, <see cref="F:System.IO.Pipes.PipeAccessRights.ReadData" />, <see cref="F:System.IO.Pipes.PipeAccessRights.ReadExtendedAttributes" />, and <see cref="F:System.IO.Pipes.PipeAccessRights.ReadPermissions" /> rights.</summary>
		// Token: 0x0400018A RID: 394
		Read = 277,
		/// <summary>Specifies the right to write to the pipe. This right includes the <see cref="F:System.IO.Pipes.PipeAccessRights.WriteAttributes" />, <see cref="F:System.IO.Pipes.PipeAccessRights.WriteData" />, and <see cref="F:System.IO.Pipes.PipeAccessRights.WriteExtendedAttributes" /> rights.</summary>
		// Token: 0x0400018B RID: 395
		Write = 554,
		/// <summary>Specifies the right to read and write from the pipe. This right includes the <see cref="F:System.IO.Pipes.PipeAccessRights.ReadAttributes" />, <see cref="F:System.IO.Pipes.PipeAccessRights.ReadData" />, <see cref="F:System.IO.Pipes.PipeAccessRights.ReadExtendedAttributes" />, <see cref="F:System.IO.Pipes.PipeAccessRights.ReadPermissions" />, <see cref="F:System.IO.Pipes.PipeAccessRights.WriteAttributes" />, <see cref="F:System.IO.Pipes.PipeAccessRights.WriteData" />, and <see cref="F:System.IO.Pipes.PipeAccessRights.WriteExtendedAttributes" /> rights.</summary>
		// Token: 0x0400018C RID: 396
		ReadWrite = 831,
		/// <summary>Specifies the right to make changes to the system access control list (SACL).</summary>
		// Token: 0x0400018D RID: 397
		AccessSystemSecurity = 1792
	}
}
