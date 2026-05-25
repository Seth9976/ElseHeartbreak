using System;
using System.Globalization;
using System.Security;
using System.Security.Permissions;

namespace System.Data.Common
{
	// Token: 0x020000D7 RID: 215
	internal sealed class PermissionHelper
	{
		// Token: 0x06000A7C RID: 2684 RVA: 0x00030D6C File Offset: 0x0002EF6C
		internal static SecurityElement Element(Type type, int version)
		{
			SecurityElement securityElement = new SecurityElement("IPermission");
			securityElement.AddAttribute("class", type.FullName + ", " + type.Assembly.ToString().Replace('"', '\''));
			securityElement.AddAttribute("version", version.ToString());
			return securityElement;
		}

		// Token: 0x06000A7D RID: 2685 RVA: 0x00030DC8 File Offset: 0x0002EFC8
		internal static PermissionState CheckPermissionState(PermissionState state, bool allowUnrestricted)
		{
			if (state != PermissionState.None)
			{
				if (state != PermissionState.Unrestricted)
				{
					string text = string.Format(Locale.GetText("Invalid enum {0}"), state);
					throw new ArgumentOutOfRangeException(text, "state");
				}
				if (!allowUnrestricted)
				{
					string text = Locale.GetText("Unrestricted isn't not allowed for identity permissions.");
					throw new ArgumentException(text, "state");
				}
			}
			return state;
		}

		// Token: 0x06000A7E RID: 2686 RVA: 0x00030E34 File Offset: 0x0002F034
		internal static int CheckSecurityElement(SecurityElement se, string parameterName, int minimumVersion, int maximumVersion)
		{
			if (se == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			if (se.Tag != "IPermission")
			{
				string text = Locale.GetText("Invalid tag '{0}' expected 'IPermission'.");
				throw new ArgumentException(string.Format(text, se.Tag), parameterName);
			}
			int num = minimumVersion;
			string text2 = se.Attribute("version");
			if (text2 != null)
			{
				try
				{
					num = int.Parse(text2);
				}
				catch (Exception ex)
				{
					string text3 = Locale.GetText("Couldn't parse version from '{0}'.");
					text3 = string.Format(text3, text2);
					throw new ArgumentException(text3, parameterName, ex);
				}
			}
			if (num < minimumVersion || num > maximumVersion)
			{
				string text4 = Locale.GetText("Unknown version '{0}', expected versions between ['{1}','{2}'].");
				text4 = string.Format(text4, num, minimumVersion, maximumVersion);
				throw new ArgumentException(text4, parameterName);
			}
			return num;
		}

		// Token: 0x06000A7F RID: 2687 RVA: 0x00030F24 File Offset: 0x0002F124
		internal static bool IsUnrestricted(SecurityElement se)
		{
			string text = se.Attribute("Unrestricted");
			return text != null && string.Compare(text, bool.TrueString, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x06000A80 RID: 2688 RVA: 0x00030F5C File Offset: 0x0002F15C
		internal static void ThrowInvalidPermission(IPermission target, Type expected)
		{
			string text = Locale.GetText("Invalid permission type '{0}', expected type '{1}'.");
			text = string.Format(text, target.GetType(), expected);
			throw new ArgumentException(text, "target");
		}
	}
}
