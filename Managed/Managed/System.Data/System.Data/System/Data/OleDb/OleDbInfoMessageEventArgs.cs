using System;

namespace System.Data.OleDb
{
	/// <summary>Provides data for the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event. This class cannot be inherited.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000F4 RID: 244
	public sealed class OleDbInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06000BCB RID: 3019 RVA: 0x00033754 File Offset: 0x00031954
		internal OleDbInfoMessageEventArgs()
		{
		}

		/// <summary>Gets the HRESULT following the ANSI SQL standard for the database.</summary>
		/// <returns>The HRESULT, which identifies the source of the error, if the error can be issued from more than one place.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0003375C File Offset: 0x0003195C
		public int ErrorCode
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the collection of warnings sent from the data source.</summary>
		/// <returns>The collection of warnings sent from the data source.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000BCD RID: 3021 RVA: 0x00033764 File Offset: 0x00031964
		public OleDbErrorCollection Errors
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the full text of the error sent from the data source.</summary>
		/// <returns>The full text of the error.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000BCE RID: 3022 RVA: 0x0003376C File Offset: 0x0003196C
		public string Message
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the name of the object that generated the error.</summary>
		/// <returns>The name of the object that generated the error.</returns>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000BCF RID: 3023 RVA: 0x00033774 File Offset: 0x00031974
		public string Source
		{
			[MonoTODO]
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Retrieves a string representation of the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event.</summary>
		/// <returns>A string representing the <see cref="E:System.Data.OleDb.OleDbConnection.InfoMessage" /> event.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x06000BD0 RID: 3024 RVA: 0x0003377C File Offset: 0x0003197C
		[MonoTODO]
		public override string ToString()
		{
			throw new NotImplementedException();
		}
	}
}
