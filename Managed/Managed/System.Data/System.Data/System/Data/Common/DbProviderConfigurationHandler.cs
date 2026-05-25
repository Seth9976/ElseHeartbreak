using System;
using System.Configuration;
using System.Xml;

namespace System.Data.Common
{
	/// <filterpriority>2</filterpriority>
	// Token: 0x020000CA RID: 202
	public class DbProviderConfigurationHandler : IConfigurationSectionHandler
	{
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" PathDiscovery="*AllFiles*" />
		/// </PermissionSet>
		// Token: 0x060009E9 RID: 2537 RVA: 0x0002EB00 File Offset: 0x0002CD00
		[MonoTODO]
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			throw new NotImplementedException();
		}
	}
}
